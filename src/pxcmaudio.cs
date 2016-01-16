/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2013-2014 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

    /** 
	The PXCMAudio interface manages the audio buffer access.

	The interface extends PXCMAddRef. Use QueryInstance<PXCMAddRef>(), or the helper
	function AddRef() to access the PXCMAddRef features.

	The interface extends PXCMMetadata. Use QueryInstance<PXCMMetadata>() to access 
	the PXCMMetadata features.
*/
    public partial class PXCMAudio : PXCMBase
    {
        new public const Int32 CUID = unchecked((Int32)0x395A39C8);

        /** 
        @enum AudioFormat
        Describes the audio sample format
        */
        [Flags]
        public enum AudioFormat
        {
            AUDIO_FORMAT_PCM = 0x4d435010,       /* 16-bit PCM			*/
            AUDIO_FORMAT_IEEE_FLOAT = 0x544c4620,       /* 32-bit float point	*/
        };

        /** 
        @brief Get the audio format string representation
        @param[in]	format		The Audio format enumerator.
        @return the string representation of the audio format.
        */
        public static String AudioFormatToString(AudioFormat format)
        {
            switch (format)
            {
                case AudioFormat.AUDIO_FORMAT_PCM: return "PCM";
                case AudioFormat.AUDIO_FORMAT_IEEE_FLOAT: return "Float";
            }
            return "Unknown";
        }

        /** 
        @brief Return the audio sample size.
        @return the sample size in bytes
        */
        public static Int32 AudioFormatToSize(AudioFormat format)
        {
            return ((Int32)format & 0xff) >> 3;
        }

        /** 
        @enum ChannelMask
        Describes the channels of the audio source.
        */
        [Flags]
        public enum ChannelMask
        {
            CHANNEL_MASK_FRONT_LEFT = 0x00000001,    /* The source is at front left		*/
            CHANNEL_MASK_FRONT_RIGHT = 0x00000002,    /* The source is at front right	*/
            CHANNEL_MASK_FRONT_CENTER = 0x00000004,    /* The source is at front center	*/
            CHANNEL_MASK_LOW_FREQUENCY = 0x00000008,    /* The source is for low frequency	*/
            CHANNEL_MASK_BACK_LEFT = 0x00000010,    /* The source is from back left	*/
            CHANNEL_MASK_BACK_RIGHT = 0x00000020,    /* The source is from back right	*/
            CHANNEL_MASK_SIDE_LEFT = 0x00000200,    /* The source is from side left	*/
            CHANNEL_MASK_SIDE_RIGHT = 0x00000400,    /* The source is from side right	*/
        };

        /** 
        @structure AudioInfo
        Describes the audio sample details.
        */
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public class AudioInfo
        {
            public Int32 bufferSize;      /* buffer size in number samples */
            public AudioFormat format;          /* sample format */
            public Int32 sampleRate;      /* samples per second */
            public Int32 nchannels;       /* number of channels */
            public ChannelMask channelMask;     /* channel mask */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            internal Int32[] reserved;

            public AudioInfo()
            {
                reserved = new Int32[3];
            }
        };

        /** 
            @structure AudioData
            Describes the audio storage details.
        */
        [Serializable]
        public partial class AudioData
        {
            public AudioFormat format;         /* sample format */
            public Int32 dataSize;       /* sample data size in number of samples */
            public IntPtr dataPtr;        /* the sample buffer */

            public Byte[] ToByteArray()
            {
                int nbytes = (int)dataSize * ((int)format & 0x00ff) / 8;
                return ToByteArray(new Byte[nbytes]);
            }

            public Int16[] ToShortArray()
            {
                return ToShortArray(new Int16[dataSize]);
            }

            public Single[] ToFloatArray()
            {
                return ToFloatArray(new Single[dataSize]);
            }
        };

        /** 
        @enum Option
        Describes the audio options.
        */
        [Flags]
        public enum Option
        {
            OPTION_ANY = 0,				/* unknown/undefined */
        };

        /** 
        @enum Access
        Describes the audio sample access mode.
        */
        [Flags]
        public enum Access
        {
            ACCESS_READ = 1,                                /* read only access */
            ACCESS_WRITE = 2,                               /* write only access */
            ACCESS_READ_WRITE = ACCESS_READ | ACCESS_WRITE, /* read write access */
        };

        /* Member Functions */

        /** 
        @brief Return the audio sample time stamp.
        @return the time stamp, in 100ns.
        */
        public Int64 QueryTimeStamp()
        {
            return PXCMAudio_QueryTimeStamp(instance);
        }

        /** 
        @brief Return the audio sample option flags.
        @return the option flags.
        */
        public Option QueryOptions()
        {
            return PXCMAudio_QueryOptions(instance);
        }

        /** 
        @brief Set the sample time stamp.
        @param[in] ts		The time stamp value, in 100ns.
        */
        public void SetTimeStamp(Int64 ts)
        {
            PXCMAudio_SetTimeStamp(instance, ts);
        }

        public void SetOptions(Option options)
        {
            PXCMAudio_SetOptions(instance, options);
        }

        /** 
        @brief Copy data from another audio sample.
        @param[in] src_audio		The audio sample to copy data from.
        @return PXCM_STATUS_NO_ERROR	Successful execution.
        */
        public pxcmStatus CopyAudio(PXCMAudio src_audio)
        {
            return PXCMAudio_CopyAudio(instance, src_audio.instance);
        }

        /** 
            @brief Lock to access the internal storage of a specified format. The function will perform format conversion if unmatched. 
            @param[in]  access		The access mode.
            @param[in]  format		The requested smaple format.
            @param[out] data		The sample data storage, to be returned.
            @return PXCM_STATUS_NO_ERROR	Successful execution.
        */
        public pxcmStatus AcquireAccess(Access access, AudioFormat format, out AudioData data)
        {
            return AcquireAccess(access, format, 0, out data);
        }

        /** 
            @brief Lock to access the internal storage of a specified format. 
            @param[in]  access		The access mode.
            @param[out] data		The sample data storage, to be returned.
            @return PXCM_STATUS_NO_ERROR	Successful execution.
        */
        public pxcmStatus AcquireAccess(Access access, out AudioData data)
        {
            return AcquireAccess(access, (AudioFormat)0, out data);
        }

        /** 
            @brief Increase a reference count of the sample.
        */
        new public void AddRef()
        {
            using (PXCMAddRef bs = this.QueryInstance<PXCMAddRef>())
            {
                if (bs == null) return;
                bs.AddRef();
            }
        }

        /* Properties */
        public AudioInfo info
        {
            get
            {
                return QueryInfo();
            }
        }


        public Int64 timeStamp
        {
            get
            {
                return QueryTimeStamp();
            }
            set
            {
                SetTimeStamp(value);
            }
        }

        internal PXCMAudio(IntPtr instance, Boolean delete)
            : base(instance, delete)
        {
        }
    };

#if RSSDK_IN_NAMESPACE
}
#endif
