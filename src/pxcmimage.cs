/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2013-2014 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif



    /**
This class defines a standard interface for image buffer access.

The interface extends PXCMAddRef. Use QueryInstance<PXCMAddRef>(), or the helper
function AddRef() to access the PXCMAddRef features.

The interface extends PXCMMetadata. Use QueryInstance<PXCMMetadata>() to access 
the PXCMMetadata features.
*/
    public partial class PXCMImage : PXCMBase
    {
        new public const Int32 CUID = 0x24740F76;
        public const Int32 NUM_OF_PLANES = 4;
        public const Int32 METADATA_DEVICE_PROPERTIES=0x61516733;
	    public const Int32 METADATA_DEVICE_PROJECTION=0x3546785a;

        /** 
        @enum PixelFormat
        Describes the image sample pixel format
        */
        [Flags]
        public enum PixelFormat
        {
            PIXEL_FORMAT_ANY = 0,		        /* Unknown/undefined */

            /* STREAM_TYPE_COLOR */
            PIXEL_FORMAT_YUY2 = 0x00010000,     /* YUY2 image */
            PIXEL_FORMAT_NV12,                  /* NV12 image */
            PIXEL_FORMAT_RGB32,                 /* BGRA layout on little-endian machines */
            PIXEL_FORMAT_RGB24,                 /* BGR layout on little-endian machines */
            PIXEL_FORMAT_Y8,                    /* 8-Bit Gray Image or IR 8-bit */

            /* STREAM_TYPE_DEPTH */
            PIXEL_FORMAT_DEPTH = 0x00020000,    /* 16-bit unsigned integer with precision mm. */
            PIXEL_FORMAT_DEPTH_RAW,             /* 16-bit unsigned integer with device specific precision. */
            PIXEL_FORMAT_DEPTH_F32,             /* 32-bit float-point with precision mm. */

            /* STREAM_TYPE_IR */
            PIXEL_FORMAT_Y16 = 0x00040000,    /* 16-Bit Gray Image or IR 16-bit */
            PIXEL_FORMAT_Y8_IR_RELATIVE = 0x00080000    /* Relative IR Image */
        };

        /** 
        @brief Convert pixel format to a string representation
        @param[in]  format        pixel format.
        @return string presentation.
        */
        public static String PixelFormatToString(PixelFormat format)
        {
            switch (format)
            {
                case PixelFormat.PIXEL_FORMAT_RGB24: return "RGB24";
                case PixelFormat.PIXEL_FORMAT_RGB32: return "RGB32";
                case PixelFormat.PIXEL_FORMAT_YUY2: return "YUY2";
                case PixelFormat.PIXEL_FORMAT_NV12: return "NV12";
                case PixelFormat.PIXEL_FORMAT_Y8: return "Y8";
                case PixelFormat.PIXEL_FORMAT_Y16: return "Y16";
                case PixelFormat.PIXEL_FORMAT_DEPTH: return "DEPTH";
                case PixelFormat.PIXEL_FORMAT_DEPTH_F32: return "DEPTH(FLOAT)";
                case PixelFormat.PIXEL_FORMAT_DEPTH_RAW: return "DEPTH(NATIVE)";
            }
            return "Unknown";
        }

        /**
           @enum Rotation
            Image rotation options.
        */
        public enum Rotation: int
        {
            ROTATION_ANY = 0x0,          /* No rotation */
            ROTATION_90_DEGREE = 90,     /* 90 degree clockwise rotation */
            ROTATION_180_DEGREE = 180,   /* 180 degree clockwise rotation */
            ROTATION_270_DEGREE = 270,   /* 270 degree clockwise rotation */
        };

        /** 
        @struct ImageInfo
        Describes the image sample detailed information.
        */
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public class ImageInfo
        {
            public Int32 width;       /* width of the image in pixels */
            public Int32 height;      /* height of the image in pixels */
            public PixelFormat format;      /* image pixel format */
            internal Int32 reserved;
        };

        /** 
        @struct ImageData
        Describes the image storage details.
        */
        public partial class ImageData
        {
            public PixelFormat format;                                                     /* image pixel format */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            internal Int32[] reserved;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public Int32[] pitches;   /* image pitches */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public IntPtr[] planes;   /* image buffers */

            public Byte[] ToByteArray(Int32 index, Int32 size)
            {
                return ToByteArray(index, new Byte[size]);
            }

            public Int16[] ToShortArray(Int32 index, Int32 size)
            {
                return ToShortArray(index, new short[size]);
            }

            [CLSCompliant(false)]
            public UInt16[] ToUShortArray(Int32 index, Int32 size)
            {
                return ToUShortArray(index, new ushort[size]);
            }

            public Int32[] ToIntArray(Int32 index, Int32 size)
            {
                return ToIntArray(index, new Int32[size]);
            }

            public Single[] ToFloatArray(Int32 index, Int32 size)
            {
                return ToFloatArray(index, new Single[size]);
            }
        };

        /** 
        @enum Access
        Describes the image access mode.
        */
        [Flags]
        public enum Access
        {
            ACCESS_READ = 1,
            ACCESS_WRITE = 2,
            ACCESS_READ_WRITE = ACCESS_READ | ACCESS_WRITE,
        };

        /** 
        @enum Option
        Describes the image options.
        */
        [Flags]
        public enum Option
        {
            OPTION_ANY = 0,
        };


        /* Class Body */

        /** 
        @brief Return the image sample time stamp.
        @return the time stamp, in 100ns.
        */
        public Int64 QueryTimeStamp()
        {
            return PXCMImage_QueryTimeStamp(instance);
        }

        /** 
        @brief Return the image stream type.
        @return the stream type.
        */
        public PXCMCapture.StreamType QueryStreamType()
        {
            return PXCMImage_QueryStreamType(instance);
        }

        /** 
        @brief Return the image option flags.
        @return the option flags.
        */
        public PXCMImage.Option QueryOptions()
        {
            return PXCMImage_QueryOptions(instance);
        }

        /** 
        @brief Set the sample time stamp.
        @param[in] ts        The time stamp value, in 100ns.
        */
        public void SetTimeStamp(Int64 ts)
        {
            PXCMImage_SetTimeStamp(instance, ts);
        }

        /** 
        @brief Set the sample stream type.
        @param[in] streamType    The sample stream type.
        */
        public void SetStreamType(PXCMCapture.StreamType type)
        {
            PXCMImage_SetStreamType(instance, type);
        }

        /** 
        @brief Set the sample options. This function overrides any previously set options.
        @param[in] options      The image options.
        */
        public void SetOptions(PXCMImage.Option options)
        {
            PXCMImage_SetOptions(instance, options);
        }

        /**    
        @brief Copy image data from another image sample.
        @param[in] src_image        The image sample to copy data from.
        @return PXC_STATUS_NO_ERROR     Successful execution.
        */
        public pxcmStatus CopyImage(PXCMImage src_image)
        {
            return PXCMImage_CopyImage(instance, src_image.instance);
        }

        /** 
        @brief Copy image data to the specified external buffer.
        @param[in] data             The ImageData structure that describes the image buffer.
        @param[in] options          Reserved.
        @return PXCM_STATUS_NO_ERROR     Successful execution.
        */
        public pxcmStatus ExportData(PXCMImage.ImageData data, Int32 flags)
        {
            return PXCMImage_ExportData(instance, data, flags);
        }

        /** 
        @brief Copy image data from the specified external buffer.
        @param[in] data             The ImageData structure that describes the image buffer.
        @param[in] options          Reserved.
        @return PXCM_STATUS_NO_ERROR     Successful execution.
        */
        public pxcmStatus ImportData(PXCMImage.ImageData data, Int32 flags)
        {
            return PXCMImage_ImportData(instance, data, flags);
        }


        /** 
        @brief Lock to access the internal storage of a specified format. 
        @param[in] access           The access mode.
        @param[out] data            The sample data storage, to be returned.
        @return PXCM_STATUS_NO_ERROR    Successful execution.
        */
        public pxcmStatus AcquireAccess(Access access, out ImageData data)
        {
            return AcquireAccess(access, (PixelFormat)0, out data);
        }

        /** 
        @brief Lock to access the internal storage of a specified format. The function will perform format conversion if unmatched. 
        @param[in] access           The access mode.
        @param[in] format           The requested smaple format.
        @param[out] data            The sample data storage, to be returned.
        @return PXCM_STATUS_NO_ERROR    Successful execution.
        */
        public pxcmStatus AcquireAccess(Access access, PixelFormat format, out ImageData data)
        {
            return AcquireAccess(access, format, 0, out data);
        }

        /** 
        @brief Lock to access the internal storage of a specified format. The function will perform format conversion if unmatched. 
        @param[in] access           The access mode.
        @param[in] format           The requested smaple format.
        @param[in] rotation         The image rotation
        @param[out] data            The sample data storage, to be returned.
        @return PXCM_STATUS_NO_ERROR    Successful execution.
        */
        public pxcmStatus AcquireAccess(Access access, PixelFormat format, Rotation rotation, Option options, out ImageData data) {
            return AcquireAccess(access, format, (Option)((int)rotation | (int)options), out data);
        }

        /** 
         @brief Increase a reference count of the sample.
        */
        public new void AddRef() {
            using (PXCMBase bs = this.QueryInstance(PXCMAddRef.CUID))
            {
                if (bs == null)
                    return;
                bs.AddRef();
            }
        }

        /* properties */
        public ImageInfo info
        {
            get
            {
                return QueryInfo();
            }
        }

        public PXCMCapture.StreamType streamType
        {
            get
            {
                return QueryStreamType();
            }
            set
            {
                SetStreamType(value);
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

        /* constructors & misc */
        internal PXCMImage(IntPtr instance, Boolean delete)
            : base(instance, delete)
        {
        }

        internal static IntPtr[] Array2IntPtrs(PXCMImage[] images)
        {
            IntPtr[] images2 = new IntPtr[images.Length];
            for (int i = 0; i < images.Length; i++)
                if (images[i] != null)
                    images2[i] = images[i].instance;
            return images2;
        }
    };

#if RSSDK_IN_NAMESPACE
}
#endif