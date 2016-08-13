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

/// <summary> 
/// The PXCMAudio interface manages the audio buffer access.
/// 
/// The interface extends PXCMAddRef. Use QueryInstance<PXCMAddRef>(), or the helper
/// function AddRef() to access the PXCMAddRef features.
/// 
/// The interface extends PXCMMetadata. Use QueryInstance<PXCMMetadata>() to access 
/// the PXCMMetadata features.
/// </summary>
public partial class PXCMAudio : PXCMBase
{
    new public const Int32 CUID = unchecked((Int32)0x395A39C8);

    /// <summary> 
    /// AudioFormat
    /// Describes the audio sample format
    /// </summary>
    [Flags]
    public enum AudioFormat
    {
        AUDIO_FORMAT_PCM = 0x4d435010,       /* 16-bit PCM			*/
        AUDIO_FORMAT_IEEE_FLOAT = 0x544c4620,       /* 32-bit float point	*/
    };

    /// <summary> 
    /// Get the audio format string representation
    /// </summary>
    /// <param name="format"> The Audio format enumerator.</param>
    /// <returns> the string representation of the audio format.</returns>
    public static String AudioFormatToString(AudioFormat format)
    {
        switch (format)
        {
            case AudioFormat.AUDIO_FORMAT_PCM: return "PCM";
            case AudioFormat.AUDIO_FORMAT_IEEE_FLOAT: return "Float";
        }
        return "Unknown";
    }

    /// <summary> 
    /// Return the audio sample size.
    /// </summary>
    /// <returns> the sample size in bytes</returns>
    public static Int32 AudioFormatToSize(AudioFormat format)
    {
        return ((Int32)format & 0xff) >> 3;
    }

    /// <summary> 
    /// ChannelMask
    /// Describes the channels of the audio source.
    /// </summary>
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

    /// <summary> 
    /// AudioInfo
    /// Describes the audio sample details.
    /// </summary>
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

    /// <summary> 
    /// AudioData
    /// Describes the audio storage details.
    /// </summary>
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

    /// <summary> 
    /// Option
    /// Describes the audio options.
    /// </summary>
    [Flags]
    public enum Option
    {
        OPTION_ANY = 0,				/* unknown/undefined */
    };

    /// <summary> 
    /// Access
    /// Describes the audio sample access mode.
    /// </summary>
    [Flags]
    public enum Access
    {
        ACCESS_READ = 1,                                /* read only access */
        ACCESS_WRITE = 2,                               /* write only access */
        ACCESS_READ_WRITE = ACCESS_READ | ACCESS_WRITE, /* read write access */
    };

    /* Member Functions */

    /// <summary> 
    /// Return the audio sample time stamp.
    /// </summary>
    /// <returns> the time stamp, in 100ns.</returns>
    public Int64 QueryTimeStamp()
    {
        return PXCMAudio_QueryTimeStamp(instance);
    }

    /// <summary> 
    /// Return the audio sample option flags.
    /// </summary>
    /// <returns> the option flags.</returns>
    public Option QueryOptions()
    {
        return PXCMAudio_QueryOptions(instance);
    }

    /// <summary> 
    /// Set the sample time stamp.
    /// </summary>
    /// <param name="ts"> The time stamp value, in 100ns.</param>
    public void SetTimeStamp(Int64 ts)
    {
        PXCMAudio_SetTimeStamp(instance, ts);
    }

    public void SetOptions(Option options)
    {
        PXCMAudio_SetOptions(instance, options);
    }

    /// <summary> 
    /// Copy data from another audio sample.
    /// </summary>
    /// <param name="src_audio"> The audio sample to copy data from.</param>
    /// <returns> PXCM_STATUS_NO_ERROR	Successful execution.</returns>
    public pxcmStatus CopyAudio(PXCMAudio src_audio)
    {
        return PXCMAudio_CopyAudio(instance, src_audio.instance);
    }

    /// <summary> 
    /// Lock to access the internal storage of a specified format. The function will perform format conversion if unmatched. 
    /// </summary>
    /// <param name="access"> The access mode.</param>
    /// <param name="format"> The requested smaple format.</param>
    /// <param name="data"> The sample data storage, to be returned.</param>
    /// <returns> PXCM_STATUS_NO_ERROR	Successful execution.</returns>
    public pxcmStatus AcquireAccess(Access access, AudioFormat format, out AudioData data)
    {
        return AcquireAccess(access, format, 0, out data);
    }

    /// <summary> 
    /// Lock to access the internal storage of a specified format. 
    /// </summary>
    /// <param name="access"> The access mode.</param>
    /// <param name="data">	The sample data storage, to be returned.</param>
    /// <returns> PXCM_STATUS_NO_ERROR	Successful execution.</returns>
    public pxcmStatus AcquireAccess(Access access, out AudioData data)
    {
        return AcquireAccess(access, (AudioFormat)0, out data);
    }

    /// <summary> 
    /// Increase a reference count of the sample.
    /// </summary>
    new public void AddRef()
    {
        using (PXCMAddRef bs = this.QueryInstance<PXCMAddRef>())
        {
            if (bs == null) return;
            bs.AddRef();
        }
    }

    /// <summary>
    ///  Properties
    /// </summary>
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
