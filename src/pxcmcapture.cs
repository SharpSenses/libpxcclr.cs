/*******************************************************************************

  INTEL CORPORATION PROPRIETARY INFORMATION
  This software is supplied under the terms of a license agreement or nondisclosure
  agreement with Intel Corporation and may not be copied or disclosed except in
  accordance with the terms of that agreement
  Copyright(c) 2013-2014 Intel Corporation. All Rights Reserved.

 *******************************************************************************/
using System;
using System.Runtime.InteropServices;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

/// <summary>
/// This interface provides member functions to create instances of and query stream capture devices.
/// </summary>  
public partial class PXCMCapture : PXCMBase
{
    new public const Int32 CUID = unchecked((Int32)0x83F72A50);
    public const Int32 STREAM_LIMIT = 8;
    public const Int32 PROPERTY_VALUE_INVALID = 32767;

    /// <summary> 
    /// StreamType
    /// Bit-OR'ed values of stream types, physical or virtual streams.
    /// </summary>
    [Flags]
    public enum StreamType
    {
        STREAM_TYPE_ANY = 0,            /* Unknown/undefined type */
        STREAM_TYPE_COLOR = 0x0001,     /* the color stream type  */
        STREAM_TYPE_DEPTH = 0x0002,     /* the depth stream type  */
        STREAM_TYPE_IR = 0x0004,     /* the infred stream type */
        STREAM_TYPE_LEFT = 0x0008,     /* the stereoscopic left intensity image  */
        STREAM_TYPE_RIGHT = 0x0010,     /* the stereoscopic right intensity image */
    };

    /// <summary> Get the stream type string representation </summary>
    /// <param name="stream">The stream type</param>
    /// <returns>The corresponding string representation</returns>
    public static String StreamTypeToString(StreamType stream)
    {
        switch (stream)
        {
            case StreamType.STREAM_TYPE_COLOR: return "Color";
            case StreamType.STREAM_TYPE_DEPTH: return "Depth";
            case StreamType.STREAM_TYPE_IR: return "IR";
            case StreamType.STREAM_TYPE_LEFT: return "Left";
            case StreamType.STREAM_TYPE_RIGHT: return "Right";
        }
        return "Unknown";
    }

    /// <summary> Get the stream type from an index number. </summary>
    /// <param name="index"> The strream index. </param>
    /// <returns> The corresponding stream type.</returns>
    public static StreamType StreamTypeFromIndex(Int32 index)
    {
        if (index < 0 || index >= STREAM_LIMIT) return StreamType.STREAM_TYPE_ANY;
        return (StreamType)(1 << index);
    }

    /// <summary> 
    /// Get the stream index number
    /// </summary>
    /// <param name="StreamType"> The stream type</param>
    /// <returns> The stream index numebr.</returns>
    public static Int32 StreamTypeToIndex(StreamType type)
    {
        Int32 s = 0;
        while ((Int32)type > 1)
        {
            type = (StreamType)((Int32)type >> 1);
            s++;
        }
        return s;
    }

    /// <summary> 
    /// @class Sample
    /// The capture sample that contains multiple streams.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial class Sample
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = STREAM_LIMIT)]
        internal IntPtr[] images;

        public PXCMImage color
        {
            get { return (images[0] != IntPtr.Zero) ? new PXCMImage(images[0], false) : null; }
            set { images[0] = value != null ? value.instance : IntPtr.Zero; }
        }

        public PXCMImage depth
        {
            get { return (images[1] != IntPtr.Zero) ? new PXCMImage(images[1], false) : null; }
            set { images[1] = value != null ? value.instance : IntPtr.Zero; }
        }

        public PXCMImage ir
        {
            get { return (images[2] != IntPtr.Zero) ? new PXCMImage(images[2], false) : null; }
            set { images[2] = value != null ? value.instance : IntPtr.Zero; }
        }

        public PXCMImage left
        {
            get { return (images[3] != IntPtr.Zero) ? new PXCMImage(images[3], false) : null; }
            set { images[3] = value != null ? value.instance : IntPtr.Zero; }
        }

        public PXCMImage right
        {
            get { return (images[4] != IntPtr.Zero) ? new PXCMImage(images[4], false) : null; }
            set { images[4] = value != null ? value.instance : IntPtr.Zero; }
        }

        /// <summary> 
        /// Return the image element by StreamType.
        /// </summary>
        /// <param name="type"> The stream type.</param>
        /// <returns> The image instance.</returns>
        public PXCMImage this[StreamType type]
        {
            get
            {
                Int32 index = StreamTypeToIndex(type);
                if (images[index] == IntPtr.Zero) return null;
                return new PXCMImage(images[index], false);
            }

            set
            {
                Int32 index = StreamTypeToIndex(type);
                images[index] = (value != null) ? value.instance : IntPtr.Zero;
            }
        }

        /// <summary> 
        /// The constructor zeros the image instance array
        /// </summary>
        public Sample()
        {
            images = new IntPtr[STREAM_LIMIT];
        }
    }

    /// <summary> 
    /// Describe device details.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public class DeviceInfo
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 224)]
        public String name;     /* device name */
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public String serial;   /* serial number */
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public String did;      /* device identifier or the device symbolic name */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public Int32[] firmware;                /* firmware version: limit to four parts of numbers */
        public PXCMPointF32 location;           /* device location in mm from the left bottom of the display panel. */
        public DeviceModel model;               /* device model */
        public DeviceOrientation orientation;   /* device orientation */
        public StreamType streams;              /* bit-OR'ed value of device stream types. */
        public Int32 didx;                      /* device index */
        public Int32 duid;                      /* device unique identifier within the SDK session */
        public PXCMImage.Rotation rotation;     /* how the camera device is physically mounted */
        public ConnectionType connectionType; /* how the camera device is physically connected */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
        internal Int32[] reserved;

        /// <summary> 
        /// Get the available stream numbers.
        /// </summary>
        /// <returns> the number of streams.</returns>
        public Int32 QueryNumStreams()
        {
            Int32 nstreams = 0;
            for (Int32 i = 0, j = 1; i < STREAM_LIMIT; i++, j = (j << 1))
                if ((((Int32)streams) & j) != 0) nstreams++;
            return nstreams;
        }

        public DeviceInfo()
        {
            name = did = serial = "";
            reserved = new Int32[12];
        }
    };

    /// <summary> 
    /// DeviceModel
    /// Describes the device type (0xFFF00000) and model (0xFFFFF)
    /// </summary>
    [Flags]
    public enum DeviceModel
    {
        DEVICE_MODEL_GENERIC = 0x00000000,   /* a generic device or unknown device */
        DEVICE_MODEL_F200 = 0x0020000E,      /* the Intel(R) RealSense(TM) 3D Camera: model F200 */
        DEVICE_MODEL_IVCAM = 0x0020000E,     /* deprecated: the Intel(R) RealSense(TM) 3D Camera: model F200 */
        DEVICE_MODEL_R200 = 0x0020000F,      /* the Intel(R) RealSense(TM) 3D Camera: model R200 */
        DEVICE_MODEL_DS4 = 0x0020000F,       /* deprecated: the Intel(R) RealSense(TM) 3D Camera: model R200 */
        DEVICE_MODEL_SR300 = 0x00200010,      /* the Intel(R) RealSense(TM) 3D Camera: model SR300 */
        DEVICE_MODEL_R200_ENHANCED = 0x0020001F, /* the Intel(R) RealSense(TM) 3D Camera: model R200 with enhanced color */
    };

    public static String DeviceModelToString(DeviceModel model)
    {
        switch (model)
        {
            case DeviceModel.DEVICE_MODEL_GENERIC: return "Generic";
            case DeviceModel.DEVICE_MODEL_F200: return "F200";
            case DeviceModel.DEVICE_MODEL_R200: return "R200";
            case DeviceModel.DEVICE_MODEL_SR300: return "SR300";
            case DeviceModel.DEVICE_MODEL_R200_ENHANCED: return "R200 Enhanced";
        }
        return "Unknown";
    }

    /// <summary> 
    /// DeviceOrientation
    /// Describes the device orientation  
    /// </summary>
    [Flags]
    public enum DeviceOrientation
    {
        DEVICE_ORIENTATION_ANY = 0x0,   /* Unknown orientation */
        DEVICE_ORIENTATION_USER_FACING = 0x1,   /* A user facing camera */
        DEVICE_ORIENTATION_FRONT_FACING = 0x1,   /* A front facing camera */
        DEVICE_ORIENTATION_WORLD_FACING = 0x2,   /* A world facing camera */
        DEVICE_ORIENTATION_REAR_FACING = 0x2,   /* A rear facing camera */
    };

    /// <summary> 
    /// ConnectionType
    /// Describes the Connection type of the deivce
    /// </summary>
    [Flags]
    public enum ConnectionType : int
    {
        CONNECTION_TYPE_UNKOWN = 0,				/* Any connection type */
        CONNECTION_TYPE_USB_INTEGRATED,			/* USB Integrated Camera */
        CONNECTION_TYPE_USB_PERIPHERAL			/* USB Peripheral Camera */
    };

    public partial class Device : PXCMBase
    {
        new public const Int32 CUID = unchecked((Int32)0x938401C4);

        /// <summary> 
        /// Return the device infomation structure of the current device.
        /// </summary>
        public DeviceInfo deviceInfo
        {
            get
            {
                DeviceInfo dinfo;
                QueryDeviceInfo(out dinfo);
                return dinfo;
            }
        }

        /// <summary> 
        /// PowerLineFrequency
        /// Describes the power line compensation filter values.
        /// </summary>
        public enum PowerLineFrequency
        {
            POWER_LINE_FREQUENCY_DISABLED = 0,		/* Disabled power line frequency */
            POWER_LINE_FREQUENCY_50HZ = 50,			/* 50HZ power line frequency */
            POWER_LINE_FREQUENCY_60HZ = 60,			/* 60HZ power line frequency */
        };

        /// <summary>
        /// MirrorMode
        /// Describes the mirroring options.
        /// </summary>
        public enum MirrorMode
        {
            MIRROR_MODE_DISABLED = 0,			/* Disabled. The images are displayed as in a world facing camera.  */
            MIRROR_MODE_HORIZONTAL = 1,			/* The images are horizontally mirrored as in a user facing camera. */
        };

        /// <summary>
        /// IVCAMAccuracy
        /// Describes the IVCAM accuracy.
        /// </summary>
        public enum IVCAMAccuracy
        {
            IVCAM_ACCURACY_FINEST = 1,			/* The finest accuracy: 9 patterns */
            IVCAM_ACCURACY_MEDIAN = 2,			/* The median accuracy: 8 patterns (default) */
            IVCAM_ACCURACY_COARSE = 3,			/* The coarse accuracy: 7 patterns */
        };

        /// <summary> 
        /// Property
        /// Describes the device properties.
        /// Use the inline functions to access specific device properties.
        /// </summary>
        [Flags]
        public enum Property
        {
            /* Color Stream Properties */
            PROPERTY_COLOR_EXPOSURE = 1,			/* RW	The color stream exposure, in log base 2 seconds. */
            PROPERTY_COLOR_BRIGHTNESS = 2,			/* RW	The color stream brightness from  -10,000 (pure black) to 10,000 (pure white). */
            PROPERTY_COLOR_CONTRAST = 3,			/* RW	The color stream contrast, from 0 to 10,000. */
            PROPERTY_COLOR_SATURATION = 4,			/* RW	The color stream saturation, from 0 to 10,000. */
            PROPERTY_COLOR_HUE = 5,			        /* RW	The color stream hue, from -180,000 to 180,000 (representing -180 to 180 degrees.) */
            PROPERTY_COLOR_GAMMA = 6,			    /* RW	The color stream gamma, from 1 to 500. */
            PROPERTY_COLOR_WHITE_BALANCE = 7,		/* RW	The color stream balance, as a color temperature in degrees Kelvin. */
            PROPERTY_COLOR_SHARPNESS = 8,			/* RW	The color stream sharpness, from 0 to 100. */
            PROPERTY_COLOR_BACK_LIGHT_COMPENSATION = 9, /* RW	The color stream back light compensation. */
            PROPERTY_COLOR_GAIN = 10,		        /* RW	The color stream gain adjustment, with negative values darker, positive values brighter, and zero as normal. */
            PROPERTY_COLOR_POWER_LINE_FREQUENCY = 11,	/* RW	The power line frequency in Hz. */
            PROPERTY_COLOR_FOCAL_LENGTH_MM = 12,	/* R	The color-sensor focal length in mm. */
            PROPERTY_COLOR_FIELD_OF_VIEW = 1000,	/* PXCMPointF32	R	The color-sensor horizontal and vertical field of view parameters, in degrees. */
            PROPERTY_COLOR_FOCAL_LENGTH = 1006,		/* PXCMPointF32	R	The color-sensor focal length in pixels. The parameters vary with the resolution setting. */
            PROPERTY_COLOR_PRINCIPAL_POINT = 1008,	/* PXCMPointF32	R	The color-sensor principal point in pixels. The parameters vary with the resolution setting. */

            /* Depth Stream Properties */
            PROPERTY_DEPTH_SATURATION_VALUE = 200,		/* R	The special depth map value to indicate that the corresponding depth map pixel is saturated. */
            PROPERTY_DEPTH_LOW_CONFIDENCE_VALUE = 201,	/* R	The special depth map value to indicate that the corresponding depth map pixel is of low-confidence. */
            PROPERTY_DEPTH_CONFIDENCE_THRESHOLD = 202,	/* RW	The confidence threshold that is used to floor the depth map values. The range is from 1 to 32767. */
            PROPERTY_DEPTH_UNIT = 204,		            /* R	The unit of depth values in micrometer, or mm. */
            PROPERTY_DEPTH_FOCAL_LENGTH_MM = 205,	    /* R	The depth-sensor focal length in mm. */
            PROPERTY_DEPTH_FIELD_OF_VIEW = 2000,		/* PXCMPointF32	R	The depth-sensor horizontal and vertical field of view parameters, in degrees. */
            PROPERTY_DEPTH_SENSOR_RANGE = 2002,		    /* PXCMRangeF32	R	The depth-sensor, sensing distance parameters, in millimeters. */
            PROPERTY_DEPTH_FOCAL_LENGTH = 2006,		    /* PXCMPointF32	R	The depth-sensor focal length in pixels. The parameters vary with the resolution setting. */
            PROPERTY_DEPTH_PRINCIPAL_POINT = 2008,		/* PXCMPointF32	R	The depth-sensor principal point in pixels. The parameters vary with the resolution setting. */

            /* Device Properties */
            PROPERTY_DEVICE_ALLOW_PROFILE_CHANGE = 302,	/* RW	If ture, allow resolution change and throw PXCM_STATUS_STREAM_CONFIG_CHANGED */
            PROPERTY_DEVICE_MIRROR = 304,	            /* MirrorMode	RW	The mirroring options. */

            /* Misc. Properties */
            PROPERTY_PROJECTION_SERIALIZABLE = 3003,	/* R	The meta data identifier of the Projection instance serialization data. */

            /* Device Specific Properties - IVCam */
            PROPERTY_IVCAM_LASER_POWER = 0x10000,	          /* Int32         RW	The laser power value from 0 (minimum) to 16 (maximum). */
            PROPERTY_IVCAM_ACCURACY = 0x10001,	              /* IVCAMAccuracy RW	The IVCAM accuracy value. */
            PROPERTY_IVCAM_FILTER_OPTION = 0x10003,           /* Int32         RW    The filter option (smoothing aggressiveness) ranged from 0 (close range) to 7 (far range). */
            PROPERTY_IVCAM_MOTION_RANGE_TRADE_OFF = 0x10004,  /* Int32         RW    This option specifies the motion and range trade off. The value ranged from 0 (short exposure, range, and better motion) to 100 (long exposure, range). */

            /* Device Specific Properties - DS */
            PROPERTY_DS_CROP = 0x20000,      /* Boolean       RW    Indicates whether to crop left and right images to match size of z image*/
            PROPERTY_DS_EMITTER = 0x20001,		/* Boolean	     RW    Enable or disable DS emitter*/
            PROPERTY_DS_DISPARITY_OUTPUT = 0x20003,		/* Boolean       RW    Switches the range output mode between distance (Z) and disparity (inverse distance)*/
            PROPERTY_DS_DISPARITY_MULTIPLIER = 0x20004,		/* Int32         RW    Sets the disparity scale factor used when in disparity output mode. Default value is 32.*/
            PROPERTY_DS_DISPARITY_SHIFT = 0x20005,		/* Int32         RW    Reduces both the minimum and maximum depth that can be computed. Allows range to be computed for points in the near field which would otherwise be beyond the disparity search range.*/

            PROPERTY_DS_MIN_MAX_Z = 0x20006,		/* PXCMRangeF32  RW    The minimum z and maximum z in Z units that will be output   */
            PROPERTY_DS_COLOR_RECTIFICATION = 0x20008,		/* Boolean       R     if true rectification is enabled to DS color*/
            PROPERTY_DS_DEPTH_RECTIFICATION = 0x20009,		/* Boolean       R     if true rectification is enabled to DS depth*/
            PROPERTY_DS_LEFTRIGHT_EXPOSURE = 0x2000A,		/* Single        RW    The depth stream exposure, in log base 2 seconds. */
            PROPERTY_DS_LEFTRIGHT_GAIN = 0x2000B,		/* Int32         RW   The depth stream gain adjustment, with negative values darker, positive values brighter, and zero as normal. */

            PROPERTY_DS_Z_TO_DISPARITY_CONSTANT = 0x2000C,      /* pxcF32        R     used to convert between Z distance (in mm) and disparity (in pixels)*/
            PROPERTY_DS_ROBINS_MUNROE_MINUS_INCREMENT = 0x2000D, /* pxcF32        RW    Sets the value to subtract when estimating the median of the correlation surface.*/
            PROPERTY_DS_ROBINS_MUNROE_PLUS_INCREMENT = 0x2000E, /* pxcF32        RW    Sets the value to add when estimating the median of the correlation surface. */
            PROPERTY_DS_MEDIAN_THRESHOLD = 0x2000F, /* pxcF32        RW    Sets the threshold for how much the winning score must beat the median to be judged a reliable depth measurement. */
            PROPERTY_DS_SCORE_MIN_THRESHOLD = 0x20010, /* pxcF32        RW    Sets the minimum correlation score that is considered acceptable. */
            PROPERTY_DS_SCORE_MAX_THRESHOLD = 0x20011, /* pxcF32        RW    Sets the maximum correlation score that is considered acceptable. */
            PROPERTY_DS_TEXTURE_COUNT_THRESHOLD = 0x20012, /* pxcF32        RW    Set parameter for determining how much texture in the region is sufficient to be judged a reliable depth measurement. */
            PROPERTY_DS_TEXTURE_DIFFERENCE_THRESHOLD = 0x20013, /* pxcF32        RW    Set parameter for determining whether the texture in the region is sufficient to justify a reliable depth measurement. */
            PROPERTY_DS_SECOND_PEAK_THRESHOLD = 0x20014, /* pxcF32        RW    Sets the threshold for how much the minimum correlation score must differ from the next best score to be judged a reliable depth measurement. */
            PROPERTY_DS_NEIGHBOR_THRESHOLD = 0x20015, /* pxcF32        RW    Sets the threshold for how much at least one adjacent disparity score must differ from the minimum score to be judged a reliable depth measurement. */
            PROPERTY_DS_LR_THRESHOLD = 0x20016, /* pxcF32        RW    Determines the current threshold for determining whether the left-right match agrees with the right-left match. */

            /* Customized properties */
            PROPERTY_CUSTOMIZED = 0x04000000,			/* CUSTOMIZED properties */
        };

        /// <summary> 
        /// Option
        /// Describes the stream options.
        /// </summary>
        [Flags]
        public enum StreamOption
        {
            STREAM_OPTION_ANY = 0,

            /* Optional options */
            STREAM_OPTION_OPTIONAL_MASK = 0x0000FFFF,   /* The option can be added to any profile, but not necessarily supported for any profile */
            STREAM_OPTION_DEPTH_PRECALCULATE_UVMAP = 0x00000001,   /* A flag to ask the device to precalculate UVMap */
            STREAM_OPTION_STRONG_STREAM_SYNC = 0x00000002,   /* A flag to ask the device to perform strong (HW-based) synchronization on the streams with this flag. */

            /* Mandatory options */
            STREAM_OPTION_MANDATORY_MASK = unchecked((Int32)0xFFFF0000),   /* If the option is supported - the device sets this flag in the profile */
            STREAM_OPTION_UNRECTIFIED = 0x00010000    /* A mandatory flag to ask the device to stream unrectified images on the stream with this flag */
        };

        /// <summary> 
        /// StreamProfile
        /// Describes the video stream configuration parameters
        /// </summary>
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public class StreamProfile
        {
            public PXCMImage.ImageInfo imageInfo;       /* resolution and color format */
            public PXCMRangeF32 frameRate;       /* frame rate range. Set max when configuring FPS */
            public StreamOption options;         /* bit-mask of stream options */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            internal Int32[] reserved;

            public StreamProfile()
            {
                imageInfo = new PXCMImage.ImageInfo();
                reserved = new Int32[5];
            }
        };


        /// <summary> 
        /// StreamProfileSet
        /// The set of StreamProfile that describes the configuration parameters of all streams.
        /// </summary>
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Size = 384)]
        public class StreamProfileSet
        {
            public StreamProfile color;
            public StreamProfile depth;
            public StreamProfile ir;
            public StreamProfile left;
            public StreamProfile right;
            internal StreamProfile reserved1;
            internal StreamProfile reserved2;
            internal StreamProfile reserved3;

            /// <summary>
            /// Access the configuration parameters by the stream type.
            /// </summary>
            /// <param name="type"> The stream type.</param>
            /// <returns> The StreamProfile instance.</returns>
            public StreamProfile this[StreamType type]
            {
                get
                {
                    if (type == StreamType.STREAM_TYPE_COLOR) return color;
                    if (type == StreamType.STREAM_TYPE_DEPTH) return depth;
                    if (type == StreamType.STREAM_TYPE_IR) return ir;
                    if (type == StreamType.STREAM_TYPE_LEFT) return left;
                    if (type == StreamType.STREAM_TYPE_RIGHT) return right;
                    if (type == StreamTypeFromIndex(5)) return reserved1;
                    if (type == StreamTypeFromIndex(6)) return reserved2;
                    if (type == StreamTypeFromIndex(7)) return reserved3;
                    return null;
                }
                set
                {
                    if (type == StreamType.STREAM_TYPE_COLOR) color = value;
                    if (type == StreamType.STREAM_TYPE_DEPTH) depth = value;
                    if (type == StreamType.STREAM_TYPE_IR) ir = value;
                    if (type == StreamType.STREAM_TYPE_LEFT) left = value;
                    if (type == StreamType.STREAM_TYPE_RIGHT) right = value;
                    if (type == StreamTypeFromIndex(5)) reserved1 = value;
                    if (type == StreamTypeFromIndex(6)) reserved2 = value;
                    if (type == StreamTypeFromIndex(7)) reserved3 = value;
                }
            }

            public StreamProfileSet()
            {
                color = new StreamProfile();
                depth = new StreamProfile();
                ir = new StreamProfile();
                left = new StreamProfile();
                right = new StreamProfile();
                reserved1 = new StreamProfile();
                reserved2 = new StreamProfile();
                reserved3 = new StreamProfile();
            }

            internal StreamProfileSet(StreamProfile[] streams)
            {
                color = streams[0];
                depth = streams[1];
                ir = streams[2];
                left = streams[3];
                right = streams[4];
                reserved1 = streams[5];
                reserved2 = streams[6];
                reserved3 = streams[7];
            }

            internal StreamProfile[] ToStreamProfileArray()
            {
                StreamProfile[] streams = new StreamProfile[PXCMCapture.STREAM_LIMIT];
                streams[0] = color;
                streams[1] = depth;
                streams[2] = ir;
                streams[3] = left;
                streams[4] = right;
                streams[5] = reserved1;
                streams[6] = reserved2;
                streams[7] = reserved3;
                return streams;
            }
        };

        /// <summary> 
        /// PropertyInfo
        /// Describe the property value range and attributes.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public class PropertyInfo
        {
            public PXCMRangeF32 range;			/* The value range */
            public Single step;			/* The value step */
            public Single defaultValue;	/* Teh default value */
            [MarshalAs(UnmanagedType.Bool)]
            public Boolean automatic;		/* Boolean if this property supports automatic */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            internal Int32[] reserved;

            public PropertyInfo()
            {
                reserved = new Int32[11];
            }
        };

        /* Device Functions */

        /// <summary>
        /// Get the number of valid stream configurations for the streams of interest.
        /// </summary>
        /// <param name="scope"> The bit-OR'ed value of stream types of interest. </param>
        /// <returns> the number of valid profile combinations. </returns>
        public Int32 QueryStreamProfileSetNum(StreamType scope)
        {
            return PXCMCapture_Device_QueryStreamProfileSetNum(instance, scope);
        }


        /// <summary>
        /// Check if profile set is valid.
        /// </summary>
        /// <param name="profiles"> The stream profile set to check </param>
        /// <returns>
        ///  true	if stream profile is valid. 
        ///  false if stream profile is invalid. 
        /// </returns>
        public Boolean IsStreamProfileSetValid(StreamProfileSet profiles)
        {
            return PXCMCapture_Device_IsStreamProfileSetValid(instance, profiles);
        }


        /// <summary>
        /// Query the unique configuration parameters for the selected video streams (types).
        /// </summary>
        /// <param name="scope"> The bit-OR'ed value of stream types of interest. </param>
        /// <param name="index"> Zero-based index to retrieve all valid profiles. </param>
        /// <param name="profiles"> The stream profile set. </param>
        /// <returns>
        ///  PXCM_STATUS_NO_ERROR for successful execution. 
        ///  PXCM_STATUS_ITEM_UNAVAILABLE if index out of range. 
        /// </returns>
        public pxcmStatus QueryStreamProfileSet(StreamType scope, Int32 index, out StreamProfileSet profiles)
        {
            return QueryStreamProfileSetINT(instance, scope, index, out profiles);
        }


        /// <summary>
        /// Query for the active stream configuration parameters (during streaming).
        /// </summary>
        /// <param name="profiles"> The stream profile set, to be returned. </param>
        /// <returns> PXCM_STATUS_NO_ERROR for successful execution. </returns>
        public pxcmStatus QueryStreamProfileSet(out StreamProfileSet profiles)
        {
            return QueryStreamProfileSet(StreamType.STREAM_TYPE_ANY, WORKING_PROFILE, out profiles);
        }


        /// <summary>
        /// Set the active profile for the all video streams. The application must configure all streams before streaming.
        /// </summary>
        /// <param name="profiles"> The stream profile set. </param>
        /// <returns> PXCM_STATUS_NO_ERROR successful execution. </returns>
        public pxcmStatus SetStreamProfileSet(StreamProfileSet profiles)
        {
            return PXCMCapture_Device_SetStreamProfileSet(instance, profiles);
        }

        protected pxcmStatus QueryProperty(Property label, out Single value)
        {
            return PXCMCapture_Device_QueryProperty(instance, label, out value);
        }

        protected pxcmStatus QueryPropertyAuto(Property label, out Boolean ifauto)
        {
            return PXCMCapture_Device_QueryPropertyAuto(instance, label, out ifauto);
        }

        protected pxcmStatus SetPropertyAuto(Property label, Boolean auto)
        {
            return PXCMCapture_Device_SetPropertyAuto(instance, label, auto);
        }

        protected pxcmStatus SetProperty(Property label, Single value)
        {
            return PXCMCapture_Device_SetProperty(instance, label, value);
        }

        /// <summary>
        /// Reset the device properties to their factory default values.
        /// </summary>
        /// <param name="streams"> The stream type to reset properties, or STREAM_TYPE_ANY for all streams. </param>
        public void ResetProperties(StreamType streams)
        {
            PXCMCapture_Device_ResetProperties(instance, streams);
        }

        /// <summary>
        /// Restore all device properties to what the application sets. Call this function upon receiving windows focus.
        /// </summary>
        public void RestorePropertiesUponFocus()
        {
            PXCMCapture_Device_RestorePropertiesUponFocus(instance);
        }

        /// <summary>
        /// Get the color stream exposure value.
        /// </summary>
        /// <returns> The color stream exposure, in log base 2 seconds. </returns>
        public Int32 QueryColorExposure()
        {
            Single value = 0;
            QueryProperty(Property.PROPERTY_COLOR_EXPOSURE, out value);
            return (Int32)value;
        }



        /// <summary>
        /// Get the color stream exposure property information.
        /// </summary>
        /// <returns> The color stream exposure property information </returns>
        public PropertyInfo QueryColorExposureInfo()
        {
            return QueryPropertyInfoINT(instance, Property.PROPERTY_COLOR_EXPOSURE);
        }


        /// <summary>
        /// Set the color stream exposure value.
        /// </summary>
        /// <param name="value"> The color stream exposure value, in log base 2 seconds. </param>
        /// <returns>
        ///  PXCM_STATUS_NO_ERROR	for successful execution. 
        ///  PXCM_STATUS_ITEM_UNAVAILABLE if the device property is not supported. 
        /// </returns>
        public pxcmStatus SetColorExposure(Int32 value)
        {
            return SetProperty(Property.PROPERTY_COLOR_EXPOSURE, (Single)value);
        }



        /// <summary>
        /// Set the color stream exposure value.
        /// </summary>
        /// <param name="auto1"> True to enable auto exposure. </param>
        /// <returns>
        /// PXCM_STATUS_NO_ERROR for successful execution. 
        /// PXCM_STATUS_ITEM_UNAVAILABLE if the device property is not supported. 
        /// </returns>
        public pxcmStatus SetColorAutoExposure(Boolean auto1)
        {
            return SetPropertyAuto(Property.PROPERTY_COLOR_EXPOSURE, auto1);
        }


        /// <summary>
        ///  Query the color stream auto exposure value.
        /// </summary>
        /// <returns> exposure auto mode </returns>
        public Boolean QueryColorAutoExposure()
        {
            Boolean ifauto = false;
            QueryPropertyAuto(Property.PROPERTY_COLOR_EXPOSURE, out ifauto);
            return ifauto;
        }


        /// <summary>
        /// Get the color stream brightness value.
        /// </summary>
        /// <returns> The color stream brightness from  -10,000 (pure black) to 10,000 (pure white). </returns>
        public Int32 QueryColorBrightness()
        {
            Single value = 0;
            QueryProperty(Property.PROPERTY_COLOR_BRIGHTNESS, out value);
            return (Int32)value;
        }


        /// <summary>
        /// Get the color stream brightness property information.
        /// </summary>
        /// <returns> The color stream brightness property information </returns>
        public PropertyInfo QueryColorBrightnessInfo()
        {
            return QueryPropertyInfoINT(instance, Property.PROPERTY_COLOR_BRIGHTNESS);
        }


        /// <summary>
        /// Set the color stream brightness value.
        /// </summary>
        /// <param name="value"> The color stream brightness from -10,000 (pure black) to 10,000 (pure white). </param>
        /// <returns>
        ///  PXCM_STATUS_NO_ERROR successful execution.
        ///  PXCM_STATUS_ITEM_UNAVAILABLE if the device property is not supported.
        /// </returns>
        public pxcmStatus SetColorBrightness(Int32 value)
        {
            return SetProperty(Property.PROPERTY_COLOR_BRIGHTNESS, (Single)value);
        }


        /// <summary>
        /// Get the color stream contrast value.
        /// </summary>
        /// <returns> The color stream contrast, from 0 to 10,000. </returns>
        public Int32 QueryColorContrast()
        {
            Single value = 0;
            QueryProperty(Property.PROPERTY_COLOR_CONTRAST, out value);
            return (Int32)value;
        }


        /// <summary>
        /// Get the color stream contrast property information.
        /// </summary>
        /// <returns> The color stream contrast property information </returns>
        public PropertyInfo QueryColorContrastInfo()
        {
            return QueryPropertyInfoINT(instance, Property.PROPERTY_COLOR_CONTRAST);
        }


        /// <summary>
        /// Set the color stream contrast value.
        /// </summary>
        /// <param name="value"> The color stream contrast, from 0 to 10,000. </param>
        /// <returns>
        ///  PXCM_STATUS_NO_ERROR	for successful execution. 
        ///  PXCM_STATUS_ITEM_UNAVAILABLE if the device property is not supported. 
        /// </returns>
        public pxcmStatus SetColorContrast(Int32 value)
        {
            return SetProperty(Property.PROPERTY_COLOR_CONTRAST, (Single)value);
        }


        /// <summary>
        /// Get the color stream saturation value.
        /// </summary>
        /// <returns> The color stream saturation, from 0 to 10,000. </returns>
        public Int32 QueryColorSaturation()
        {
            Single value = 0;
            QueryProperty(Property.PROPERTY_COLOR_SATURATION, out value);
            return (Int32)value;
        }


        /// <summary>
        /// Get the color stream saturation property information.
        /// </summary>
        /// <returns> The color stream saturation property information. </returns>
        public PropertyInfo QueryColorSaturationInfo()
        {
            return QueryPropertyInfoINT(instance, Property.PROPERTY_COLOR_SATURATION);
        }


        /// <summary>
        /// Set the color stream saturation value.
        /// </summary>
        /// <param name="value"> The color stream saturation, from 0 to 10,000. </param>
        /// <returns>
        ///  PXCM_STATUS_NO_ERROR for successful execution. 
        ///  PXCM_STATUS_ITEM_UNAVAILABLE if the device property is not supported.
        /// </returns>
        public pxcmStatus SetColorSaturation(Int32 value)
        {
            return SetProperty(Property.PROPERTY_COLOR_SATURATION, (Int32)value);
        }


        /// <summary>
        /// Get the color stream hue value.
        /// </summary>
        /// <returns> The color stream hue, from -180,000 to 180,000 (representing -180 to 180 degrees.) </returns>
        public Int32 QueryColorHue()
        {
            Single value = 0;
            QueryProperty(Property.PROPERTY_COLOR_HUE, out value);
            return (Int32)value;
        }


        /// <summary>
        /// Get the color stream Hue property information.
        /// </summary>
        /// <returns> The color stream Hue property information. </returns>
        public PropertyInfo QueryColorHueInfo()
        {
            return QueryPropertyInfoINT(instance, Property.PROPERTY_COLOR_HUE);
        }


        /// <summary>
        /// Set the color stream hue value.
        /// </summary>
        /// <param name="value"> The color stream hue, from -180,000 to 180,000 (representing -180 to 180 degrees.) </param>
        /// <returns>
        ///  PXCM_STATUS_NO_ERROR	for successful execution. 
        ///  PXCM_STATUS_ITEM_UNAVAILABLE if the device property is not supported. 
        /// </returns>
        public pxcmStatus SetColorHue(Int32 value)
        {
            return SetProperty(Property.PROPERTY_COLOR_HUE, (Single)value);
        }

        /// <summary>
        /// Get the color stream gamma value.
        /// </summary>
        /// <returns> The color stream gamma, from 1 to 500. </returns>
        public Int32 QueryColorGamma()
        {
            Single value = 0;
            QueryProperty(Property.PROPERTY_COLOR_GAMMA, out value);
            return (Int32)value;
        }


        /// <summary>
        /// Get the color stream gamma property information.
        /// </summary>
        /// <returns> The color stream gamma property information </returns>
        public PropertyInfo QueryColorGammaInfo()
        {
            return QueryPropertyInfoINT(instance, Property.PROPERTY_COLOR_GAMMA);
        }


        /// <summary>
        /// Set the color stream gamma value.
        /// </summary>
        /// <param name="value"> The color stream gamma, from 1 to 500. </param>
        /// <returns>
        ///  PXCM_STATUS_NO_ERROR	for successful execution.
        ///  PXCM_STATUS_ITEM_UNAVAILABLE if the device property is not supported. 
        /// </returns>
        public pxcmStatus SetColorGamma(Int32 value)
        {
            return SetProperty(Property.PROPERTY_COLOR_GAMMA, (Single)value);
        }


        /// <summary>
        /// Get the color stream white balance value.
        /// </summary>
        /// <returns> The color stream balance, as a color temperature in degrees Kelvin. </returns>
        public Int32 QueryColorWhiteBalance()
        {
            Single value = 0;
            QueryProperty(Property.PROPERTY_COLOR_WHITE_BALANCE, out value);
            return (Int32)value;
        }


        /// <summary>
        /// Get the color stream  white balance property information.
        /// </summary>
        /// <returns> The color stream  white balance property information. </returns>
        public PropertyInfo QueryColorWhiteBalanceInfo()
        {
            return QueryPropertyInfoINT(instance, Property.PROPERTY_COLOR_WHITE_BALANCE);
        }


        /// <summary>
        /// Set the color stream white balance value.
        /// </summary>
        /// <param name="value"> The color stream balance, as a color temperature in degrees Kelvin. </param>
        /// <returns>
        ///   PXCM_STATUS_NO_ERROR for successful execution. 
        ///  PXCM_STATUS_ITEM_UNAVAILABLE if the device property is not supported.
        /// </returns>
        public pxcmStatus SetColorWhiteBalance(Int32 value)
        {
            return SetProperty(Property.PROPERTY_COLOR_WHITE_BALANCE, (Single)value);
        }


        /// <summary>
        /// Query the color stream auto white balance mode status.
        /// </summary>
        /// <returns> White Balance auto mode status. </returns>
        public Boolean QueryColorAutoWhiteBalance()
        {
            Boolean ifauto = false;
            QueryPropertyAuto(Property.PROPERTY_COLOR_WHITE_BALANCE, out ifauto);
            return ifauto;
        }


        /// <summary>
        /// Set the color stream auto white balance mode.
        /// </summary>
        /// <param name="auto1"> The flag to tell if the auto is enabled or not.</param>
        /// <returns>
        ///  PXCM_STATUS_NO_ERROR	for successful execution. 
        ///  PXCM_STATUS_ITEM_UNAVAILABLE if the device property is not supported.
        /// </returns>
        public pxcmStatus SetColorAutoWhiteBalance(Boolean auto1)
        {
            return SetPropertyAuto(Property.PROPERTY_COLOR_WHITE_BALANCE, auto1);
        }


        /// <summary>
        /// Get the color stream sharpness value.
        /// </summary>
        /// <returns> The color stream sharpness, from 0 to 100. </returns>
        public Int32 QueryColorSharpness()
        {
            Single value = 0;
            QueryProperty(Property.PROPERTY_COLOR_SHARPNESS, out value);
            return (Int32)value;
        }


        /// <summary>
        /// Get the color stream Sharpness property information.
        /// </summary>
        /// <returns> The color stream Sharpness property information </returns>
        public PropertyInfo QueryColorSharpnessInfo()
        {
            return QueryPropertyInfoINT(instance, Property.PROPERTY_COLOR_SHARPNESS);
        }


        /// <summary>
        /// Set the color stream sharpness value.
        /// </summary>
        /// <param name="value">The color stream sharpness, from 0 to 100. </param>
        /// <returns>
        ///  PXCM_STATUS_NO_ERROR	for successful execution. 
        ///  PXCM_STATUS_ITEM_UNAVAILABLE if the device property is not supported. 
        /// </returns>
        public pxcmStatus SetColorSharpness(Int32 value)
        {
            return SetProperty(Property.PROPERTY_COLOR_SHARPNESS, (Single)value);
        }


        /// <summary>
        /// Get the color stream back light compensation status.
        /// </summary>
        /// <returns> The color stream back light compensation status. </returns>
        public Int32 QueryColorBackLightCompensation()
        {
            Single value = 0;
            QueryProperty(Property.PROPERTY_COLOR_BACK_LIGHT_COMPENSATION, out value);
            return (Int32)value;
        }


        /// <summary>
        /// Get the color stream back light compensation property information.
        /// </summary>
        /// <returns> The color stream back light compensation property information. </returns>
        public PropertyInfo QueryColorBackLightCompensationInfo()
        {
            return QueryPropertyInfoINT(instance, Property.PROPERTY_COLOR_BACK_LIGHT_COMPENSATION);
        }

        /// <summary>
        /// Set the color stream back light compensation status.
        /// </summary>
        /// <param name="value"> The color stream back light compensation status. </param>
        /// <returns>
        ///  PXCM_STATUS_NO_ERROR	for successful execution. 
        ///  PXCM_STATUS_ITEM_UNAVAILABLE if the device property is not supported. 
        /// </returns>
        public pxcmStatus SetColorBackLightCompensation(Int32 value)
        {
            return SetProperty(Property.PROPERTY_COLOR_BACK_LIGHT_COMPENSATION, (Single)value);
        }

        /// <summary>
        /// Get the color stream gain value.
        /// </summary>
        /// <returns> The color stream gain adjustment, with negative values darker, positive values brighter, and zero as normal. </returns>
        public Int32 QueryColorGain()
        {
            Single value = 0;
            QueryProperty(Property.PROPERTY_COLOR_GAIN, out value);
            return (Int32)value;
        }


        /// <summary>
        /// Get the color stream gain property information.
        /// </summary>
        /// <returns> The color stream gain property information </returns>
        public PropertyInfo QueryColorGainInfo()
        {
            return QueryPropertyInfoINT(instance, Property.PROPERTY_COLOR_GAIN);
        }

        /// <summary>
        /// Set the color stream gain value.
        /// </summary>
        /// <param name="value"> The color stream gain adjustment, with negative values darker, positive values brighter, and zero as normal. </param>
        /// <returns>
        ///  PXCM_STATUS_NO_ERROR	for successful execution. 
        ///  PXCM_STATUS_ITEM_UNAVAILABLE if the device property is not supported. 
        /// </returns>
        public pxcmStatus SetColorGain(Int32 value)
        {
            return SetProperty(Property.PROPERTY_COLOR_GAIN, (Single)value);
        }

        /// <summary> 
        /// Get the color stream power line frequency value.
        /// </summary>
        /// <returns> The power line frequency in Hz.</returns>
        public PowerLineFrequency QueryColorPowerLineFrequency()
        {
            Single value = 0;
            QueryProperty(Property.PROPERTY_COLOR_POWER_LINE_FREQUENCY, out value);
            return (PowerLineFrequency)(Int32)value;
        }

        /// <summary> 
        /// Set the color stream power line frequency value.
        /// </summary>
        /// <param name="value"> The power line frequency in Hz.</param>
        /// <returns> PXCM_STATUS_NO_ERROR			successful execution.</returns>
        /// <returns> PXCM_STATUS_ITEM_UNAVAILABLE the device property is not supported.</returns>
        public pxcmStatus SetColorPowerLineFrequency(PowerLineFrequency value)
        {
            return SetProperty(Property.PROPERTY_COLOR_POWER_LINE_FREQUENCY, (Single)value);
        }

        /// <summary> 
        /// Get the color stream power line frequency Property Info.
        /// </summary>
        /// <returns> The power line frequency default value.</returns>
        public PowerLineFrequency QueryColorPowerLineFrequencyDefaultValue()
        {
            PropertyInfo info = QueryPropertyInfoINT(instance, Property.PROPERTY_COLOR_POWER_LINE_FREQUENCY);
            return (PowerLineFrequency)(Int32)info.defaultValue;
        }


        /// <summary> 
        /// Query the color stream power line frequency auto value.
        /// </summary>
        /// <param name="value"> The power line frequency auto mode status.</param>
        /// <returns> PowerLineFrequency auto mode status</returns>
        public Boolean QueryColorAutoPowerLineFrequency()
        {
            Boolean ifauto = false;
            QueryPropertyAuto(Property.PROPERTY_COLOR_POWER_LINE_FREQUENCY, out ifauto);
            return ifauto;
        }


        /// <summary> 
        /// Set the color stream auto power line frequency.
        /// </summary>
        /// <param name="value"> The power line frequency auto mode.</param>
        /// <returns> PXCM_STATUS_NO_ERROR             successful execution.</returns>
        /// <returns> PXCM_STATUS_ITEM_UNAVAILABLE     the device property is not supported.</returns>
        public pxcmStatus SetColorAutoPowerLineFrequency(Boolean auto1)
        {
            return SetPropertyAuto(Property.PROPERTY_COLOR_POWER_LINE_FREQUENCY, auto1);
        }

        /// <summary> 
        /// Get the color stream field of view.
        /// </summary>
        /// <returns> The color-sensor horizontal and vertical field of view parameters, in degrees. </returns>
        public PXCMPointF32 QueryColorFieldOfView()
        {
            PXCMPointF32 value = new PXCMPointF32();
            QueryProperty(Property.PROPERTY_COLOR_FIELD_OF_VIEW, out value.x);
            QueryProperty((Property)(Property.PROPERTY_COLOR_FIELD_OF_VIEW + 1), out value.y);
            return value;
        }

        /// <summary> 
        /// Get the color stream focal length.
        /// </summary>
        /// <returns> The color-sensor focal length in pixels. The parameters vary with the resolution setting.</returns>
        public PXCMPointF32 QueryColorFocalLength()
        {
            PXCMPointF32 value = new PXCMPointF32();
            QueryProperty(Property.PROPERTY_COLOR_FOCAL_LENGTH, out value.x);
            QueryProperty((Property)(Property.PROPERTY_COLOR_FOCAL_LENGTH + 1), out value.y);
            return value;
        }

        /// <summary> 
        /// Get the color stream focal length in mm.
        /// </summary>
        /// <returns> The color-sensor focal length in mm.</returns>
        public Single QueryColorFocalLengthMM()
        {
            Single value = 0;
            QueryProperty(Property.PROPERTY_COLOR_FOCAL_LENGTH_MM, out value);
            return value;
        }

        /// <summary> 
        /// Get the color stream principal point.
        /// </summary>
        /// <returns> The color-sensor principal point in pixels. The parameters vary with the resolution setting.</returns>
        public PXCMPointF32 QueryColorPrincipalPoint()
        {
            PXCMPointF32 value = new PXCMPointF32();
            QueryProperty(Property.PROPERTY_COLOR_PRINCIPAL_POINT, out value.x);
            QueryProperty((Property)(Property.PROPERTY_COLOR_PRINCIPAL_POINT + 1), out value.y);
            return value;
        }

        /// <summary> 
        /// Get the depth stream low confidence value.
        /// </summary>
        /// <returns> The special depth map value to indicate that the corresponding depth map pixel is of low-confidence.</returns>
        [CLSCompliant(false)]
        public UInt16 QueryDepthLowConfidenceValue()
        {
            return 0;
        }

        /// <summary> 
        /// Get the depth stream confidence threshold.
        /// </summary>
        /// <returns> The confidence threshold that is used to floor the depth map values. The range is from 1 to 32767.</returns>
        [CLSCompliant(false)]
        public UInt16 QueryDepthConfidenceThreshold()
        {
            Single value = 0;
            QueryProperty(Property.PROPERTY_DEPTH_CONFIDENCE_THRESHOLD, out value);
            return (UInt16)value;
        }

        /// <summary> 
        /// Get the depth stream confidence threshold information.
        /// </summary>
        /// <returns> The property information for the confidence threshold that is used to floor the depth map values. The range is from 0 to 15.</returns>
        public PropertyInfo QueryDepthConfidenceThresholdInfo()
        {
            return QueryPropertyInfoINT(instance, Property.PROPERTY_DEPTH_CONFIDENCE_THRESHOLD);
        }

        /// <summary> 
        /// Set the depth stream confidence threshold.
        /// </summary>
        /// <param name="value"> The confidence threshold that is used to floor the depth map values. The range is from 1 to 32767.</param>
        /// <returns> PXCM_STATUS_NO_ERROR			successful execution.</returns>
        /// <returns> PXCM_STATUS_ITEM_UNAVAILABLE    the device property is not supported.</returns>
        [CLSCompliant(false)]
        public pxcmStatus SetDepthConfidenceThreshold(UInt16 value)
        {
            return SetProperty(Property.PROPERTY_DEPTH_CONFIDENCE_THRESHOLD, (Single)value);
        }

        /// <summary> 
        /// Get the depth stream unit value.
        /// </summary>
        /// <returns> The unit of depth values in micrometer.</returns>
        public Single QueryDepthUnit()
        {
            Single value = 0;
            QueryProperty(Property.PROPERTY_DEPTH_UNIT, out value);
            return value;
        }

        /// <summary> 
        /// Set the depth stream unit value.
        /// </summary>
        /// <param name="value"> The unit of depth values in micrometre</param>
        /// <returns> PXCM_STATUS_NO_ERROR             successful execution.</returns>
        /// <returns> PXCM_STATUS_ITEM_UNAVAILABLE     the device property is not supported.</returns>
        public pxcmStatus SetDepthUnit(Int32 value)
        {
            return SetProperty(Property.PROPERTY_DEPTH_UNIT, (Single)value);
        }

        /// <summary> 
        /// Get the depth stream field of view.
        /// </summary>
        /// <returns> The depth-sensor horizontal and vertical field of view parameters, in degrees. </returns>
        public PXCMPointF32 QueryDepthFieldOfView()
        {
            PXCMPointF32 value = new PXCMPointF32();
            QueryProperty(Property.PROPERTY_DEPTH_FIELD_OF_VIEW, out value.x);
            QueryProperty((Property)(Property.PROPERTY_DEPTH_FIELD_OF_VIEW + 1), out value.y);
            return value;
        }

        /// <summary> 
        /// Get the depth stream sensor range.
        /// </summary>
        /// <returns> The depth-sensor, sensing distance parameters, in millimeters.</returns>
        public PXCMRangeF32 QueryDepthSensorRange()
        {
            PXCMRangeF32 value = new PXCMRangeF32();
            QueryProperty(Property.PROPERTY_DEPTH_SENSOR_RANGE, out value.min);
            QueryProperty((Property)(Property.PROPERTY_DEPTH_SENSOR_RANGE + 1), out value.max);
            return value;
        }

        /// <summary> 
        /// Get the depth stream focal length.
        /// </summary>
        /// <returns> The depth-sensor focal length in pixels. The parameters vary with the resolution setting.</returns>
        public PXCMPointF32 QueryDepthFocalLength()
        {
            PXCMPointF32 value = new PXCMPointF32();
            QueryProperty(Property.PROPERTY_DEPTH_FOCAL_LENGTH, out value.x);
            QueryProperty((Property)(Property.PROPERTY_DEPTH_FOCAL_LENGTH + 1), out value.y);
            return value;
        }

        /// <summary> 
        /// Get the depth stream focal length in mm.
        /// </summary>
        /// <returns> The depth-sensor focal length in mm.</returns>
        public Single QueryDepthFocalLengthMM()
        {
            Single value = 0;
            QueryProperty(Property.PROPERTY_DEPTH_FOCAL_LENGTH_MM, out value);
            return value;
        }

        /// <summary> 
        /// Get the depth stream principal point.
        /// <returns> The depth-sensor principal point in pixels. The parameters vary with the resolution setting.</returns>
        /// </summary>
        public PXCMPointF32 QueryDepthPrincipalPoint()
        {
            PXCMPointF32 value = new PXCMPointF32();
            QueryProperty(Property.PROPERTY_DEPTH_PRINCIPAL_POINT, out value.x);
            QueryProperty((Property)(Property.PROPERTY_DEPTH_PRINCIPAL_POINT + 1), out value.y);
            return value;
        }

        /// <summary> 
        /// Get the device allow profile change status.
        /// </summary>
        /// <returns> If ture, allow resolution change and throw PXCM_STATUS_STREAM_CONFIG_CHANGED.</returns>
        public Boolean QueryDeviceAllowProfileChange()
        {
            Single value = 0;
            QueryProperty(Property.PROPERTY_DEVICE_ALLOW_PROFILE_CHANGE, out value);
            return value != 0;
        }

        /// <summary> 
        /// Set the device allow profile change status.
        /// </summary>
        /// <param name="value"> If ture, allow resolution change and throw PXCM_STATUS_STREAM_CONFIG_CHANGED.</param>
        /// <returns> PXCM_STATUS_NO_ERROR			successful execution.</returns>
        /// <returns> PXCM_STATUS_ITEM_UNAVAILABLE    the device property is not supported.</returns>
        public pxcmStatus SetDeviceAllowProfileChange(Boolean value)
        {
            return SetProperty(Property.PROPERTY_DEVICE_ALLOW_PROFILE_CHANGE, value ? 1 : 0);
        }

        /// <summary> 
        /// Get the mirror mode.
        /// </summary>
        /// <returns> The mirror mode</returns>
        public MirrorMode QueryMirrorMode()
        {
            Single value = 0;
            QueryProperty(Property.PROPERTY_DEVICE_MIRROR, out value);
            return (MirrorMode)(Int32)value;
        }

        /// <summary> 
        /// Set the mirror mode.
        /// <param name="value"> The mirror mode</param>
        /// </summary>
        /// <returns> PXCM_STATUS_NO_ERROR			successful execution.</returns>
        /// <returns> PXCM_STATUS_ITEM_UNAVAILABLE    the device property is not supported.</returns>
        public pxcmStatus SetMirrorMode(MirrorMode value)
        {
            return SetProperty(Property.PROPERTY_DEVICE_MIRROR, (Single)value);
        }

        /// <summary> 
        /// Get the IVCAM laser power.
        /// </summary>
        /// <returns> The laser power value from 0 (minimum) to 16 (maximum).</returns>
        public Int32 QueryIVCAMLaserPower()
        {
            Single value = 0;
            QueryProperty(Property.PROPERTY_IVCAM_LASER_POWER, out value);
            return (Int32)value;
        }

        /// <summary> 
        /// Get the IVCAM laser power property information.
        /// </summary>
        /// <returns> The laser power proeprty information. </returns>
        public PropertyInfo QueryIVCAMLaserPowerInfo()
        {
            return QueryPropertyInfoINT(instance, Property.PROPERTY_IVCAM_LASER_POWER);
        }

        /// <summary> 
        /// Set the IVCAM laser power.
        /// </summary>
        /// <param name="value"> The laser power value from 0 (minimum) to 16 (maximum).</param>
        /// <returns> PXCM_STATUS_NO_ERROR			successful execution.</returns>
        /// <returns> PXCM_STATUS_ITEM_UNAVAILABLE    the device property is not supported.</returns>
        public pxcmStatus SetIVCAMLaserPower(Int32 value)
        {
            return SetProperty(Property.PROPERTY_IVCAM_LASER_POWER, (Single)value);
        }

        /// <summary> 
        /// Get the IVCAM accuracy.
        /// </summary>
        /// <returns> The accuracy value</returns>
        public IVCAMAccuracy QueryIVCAMAccuracy()
        {
            Single value = 0;
            QueryProperty(Property.PROPERTY_IVCAM_ACCURACY, out value);
            return (IVCAMAccuracy)(Int32)value;
        }

        /// <summary> 
        /// Set the IVCAM accuracy.
        /// </summary>
        /// <param name="value"> The accuracy value</param>
        /// <returns> PXCM_STATUS_NO_ERROR			successful execution.</returns>
        /// <returns> PXCM_STATUS_ITEM_UNAVAILABLE    the device property is not supported.</returns>
        public pxcmStatus SetIVCAMAccuracy(IVCAMAccuracy value)
        {
            return SetProperty(Property.PROPERTY_IVCAM_ACCURACY, (Single)value);
        }

        /// <summary> 
        /// Get the IVCAM accuracy property Information.
        /// </summary>
        /// <returns> The accuracy value property Information</returns>
        public IVCAMAccuracy QueryIVCAMAccuracyDefaultValue()
        {
            PropertyInfo info = QueryPropertyInfoINT(instance, Property.PROPERTY_IVCAM_ACCURACY);
            return (IVCAMAccuracy)(Int32)info.defaultValue;
        }

        /// <summary> 
        /// Get the IVCAM filter option (smoothing aggressiveness) ranged from 0 (close range) to 7 (far range).
        /// </summary>
        /// <returns> The filter option value.</returns>
        public Int32 QueryIVCAMFilterOption()
        {
            Single value;
            pxcmStatus sts = QueryProperty(Property.PROPERTY_IVCAM_FILTER_OPTION, out value);
            if (sts < pxcmStatus.PXCM_STATUS_NO_ERROR) QueryProperty((Property)(Property.PROPERTY_CUSTOMIZED + 6), out value);
            return (Int32)value;
        }

        /// <summary> 
        /// Get the IVCAM Filter Option property information.
        /// </summary>
        /// <returns> The IVCAM Filter Option property information. </returns>
        public PropertyInfo QueryIVCAMFilterOptionInfo()
        {
            PropertyInfo info = QueryPropertyInfoINT(instance, Property.PROPERTY_IVCAM_FILTER_OPTION);
            if (info.range.max > 0) return info;
            return QueryPropertyInfoINT(instance, (Property)(Property.PROPERTY_CUSTOMIZED + 6));
        }

        /// <summary> 
        /// Set the IVCAM filter option (smoothing aggressiveness) ranged from 0 (close range) to 7 (far range).
        /// <param name="value"> The filter option value</param>
        /// </summary>
        /// <returns> PXC_STATUS_NO_ERROR			successful execution.</returns>
        /// <returns> PXC_STATUS_ITEM_UNAVAILABLE the device property is not supported.</returns>
        public pxcmStatus SetIVCAMFilterOption(Int32 value)
        {
            pxcmStatus sts = SetProperty(Property.PROPERTY_IVCAM_FILTER_OPTION, (Single)value);
            if (sts < pxcmStatus.PXCM_STATUS_NO_ERROR) sts = SetProperty((Property)(Property.PROPERTY_CUSTOMIZED + 6), (Single)value);
            return sts;
        }

        /// <summary> 
        /// Get the IVCAM motion range trade off option, ranged from 0 (short range, better motion) to 100 (far range, long exposure).
        /// </summary>
        /// <returns> The motion range trade option.</returns>
        public Int32 QueryIVCAMMotionRangeTradeOff()
        {
            Single value;
            pxcmStatus sts = QueryProperty(Property.PROPERTY_IVCAM_MOTION_RANGE_TRADE_OFF, out value);
            if (sts < pxcmStatus.PXCM_STATUS_NO_ERROR) QueryProperty((Property)(Property.PROPERTY_CUSTOMIZED + 4), out value);
            return (Int32)value;
        }

        /// <summary> 
        /// Get the IVCAM Filter Option property information.
        /// </summary>
        /// <returns> The IVCAM Filter Option property information. </returns>
        public PropertyInfo QueryIVCAMMotionRangeTradeOffInfo()
        {
            PropertyInfo info = QueryPropertyInfoINT(instance, Property.PROPERTY_IVCAM_MOTION_RANGE_TRADE_OFF);
            if (info.range.max > 0) return info;
            return QueryPropertyInfoINT(instance, (Property)(Property.PROPERTY_CUSTOMIZED + 4));
        }

        /// <summary> 
        /// Set the IVCAM motion range trade off option, ranged from 0 (short range, better motion) to 100 (far range, long exposure).
        /// </summary>
        /// <param name="value"> The motion range trade option.</param>
        /// <returns> PXC_STATUS_NO_ERROR			successful execution.</returns>
        /// <returns> PXC_STATUS_ITEM_UNAVAILABLE the device property is not supported.</returns>
        public pxcmStatus SetIVCAMMotionRangeTradeOff(Int32 value)
        {
            pxcmStatus sts = SetProperty(Property.PROPERTY_IVCAM_MOTION_RANGE_TRADE_OFF, (Single)value);
            if (sts < pxcmStatus.PXCM_STATUS_NO_ERROR) sts = SetProperty((Property)(Property.PROPERTY_CUSTOMIZED + 4), (Single)value);
            return sts;
        }

        /// <summary>
        /// Get the DS Left Right Cropping status.
        /// </summary>
        /// <returns> true if enabled</returns>
        public Boolean QueryDSLeftRightCropping()
        {
            Single value = 0;
            QueryProperty(Property.PROPERTY_DS_CROP, out value);
            return (value == 0) ? false : true;
        }

        /// <summary>
        /// enable\disable the DS Left Right Cropping.
        /// </summary>
        /// <param name="value"> The setting value</param>
        /// <returns> PXCM_STATUS_NO_ERROR             successful execution.</returns>
        /// <returns> PXCM_STATUS_ITEM_UNAVAILABLE     the device property is not supported.</returns>
        public pxcmStatus SetDSLeftRightCropping(Boolean value)
        {
            return SetProperty(Property.PROPERTY_DS_CROP, value ? 1.0F : 0.0F);
        }

        /// <summary>
        /// Get the DS emitter status
        /// </summary>
        /// <returns> true if enabled</returns>
        public Boolean QueryDSEmitterEnabled()
        {
            Single value = 0;
            QueryProperty(Property.PROPERTY_DS_EMITTER, out value);
            return (value == 0) ? false : true;
        }

        /// <summary>
        /// enable\disable the DS Emitter
        /// </summary>
        /// <param name="value"> The setting value</param>
        /// <returns> PXCM_STATUS_NO_ERROR             successful execution.</returns>
        /// <returns> PXCM_STATUS_ITEM_UNAVAILABLE     the device property is not supported.</returns>
        public pxcmStatus SetDSEnableEmitter(Boolean value)
        {
            return SetProperty(Property.PROPERTY_DS_EMITTER, value ? 1.0F : 0.0F);
        }

        /// <summary>
        /// Get the DS Disparity (inverse distance) Output status
        /// </summary>
        /// <returns> true if enabled</returns>
        public Boolean QueryDSDisparityOutputEnabled()
        {
            Single value = 0;
            QueryProperty(Property.PROPERTY_DS_DISPARITY_OUTPUT, out value);
            return (value == 0) ? false : true;
        }

        /// <summary>
        /// enable\disable DS Disparity Output, Switches the range output mode between distance (Z) and disparity (inverse distance)
        /// </summary>
        /// <param name="value"> The setting value</param>
        /// <returns> PXCM_STATUS_NO_ERROR             successful execution.</returns>
        /// <returns> PXCM_STATUS_ITEM_UNAVAILABLE     the device property is not supported.</returns>
        public pxcmStatus SetDSEnableDisparityOutput(Boolean value)
        {
            return SetProperty(Property.PROPERTY_DS_DISPARITY_OUTPUT, value ? 1.0F : 0.0F);
        }

        /// <summary>
        /// Gets the disparity scale factor used when in disparity output mode. 
        /// Default value is 32.
        /// </summary>
        /// <returns> the disparity scale factor.</returns>
        public Int32 QueryDSDisparityMultiplier()
        {
            Single value = 0;
            QueryProperty(Property.PROPERTY_DS_DISPARITY_MULTIPLIER, out value);
            return (Int32)value;
        }

        /// <summary>
        /// Sets the disparity scale factor used when in disparity output mode.
        /// </summary>
        /// <param name="value"> the disparity scale factor.</param>
        /// <returns> PXCM_STATUS_NO_ERROR             successful execution.</returns>
        /// <returns> PXCM_STATUS_ITEM_UNAVAILABLE     the device property is not supported.</returns>
        public pxcmStatus SetDSDisparityMultiplier(Int32 value)
        {
            return SetProperty(Property.PROPERTY_DS_DISPARITY_MULTIPLIER, (Single)value);
        }

        /// <summary>
        /// Gets the disparity shift used when in disparity output mode.
        /// </summary>
        /// <returns> the disparity shift.</returns>
        public Int32 QueryDSDisparityShift()
        {
            Single value = 0;
            QueryProperty(Property.PROPERTY_DS_DISPARITY_SHIFT, out value);
            return (Int32)value;
        }

        /// <summary>
        /// Sets the disparity shift used when in disparity output mode.
        /// </summary>
        /// <param name="value"> the disparity shift.</param>
        /// <returns> PXCM_STATUS_NO_ERROR             successful execution.</returns>
        /// <returns> PXCM_STATUS_ITEM_UNAVAILABLE     the device property is not supported.</returns>
        public pxcmStatus SetDSDisparityShift(Int32 value)
        {
            return SetProperty(Property.PROPERTY_DS_DISPARITY_SHIFT, value);
        }

        /// <summary>
        /// Get the current min and max limits of the z.
        /// <returns> min, max z.</returns>
        /// </summary>
        public PXCMRangeF32 QueryDSMinMaxZ()
        {
            PXCMRangeF32 value;
            value.min = value.max = 0.0F;
            QueryProperty(Property.PROPERTY_DS_MIN_MAX_Z, out value.min);
            QueryProperty((Property)(Property.PROPERTY_DS_MIN_MAX_Z + 1), out value.max);
            return value;
        }

        /// <summary>
        /// Set the min and max limits of the z units.
        /// </summary>
        /// <param name="value"> The setting value</param>
        /// <returns> PXCM_STATUS_NO_ERROR             successful execution.</returns>
        /// <returns> PXCM_STATUS_ITEM_UNAVAILABLE     the device property is not supported.</returns>
        public pxcmStatus SetDSMinMaxZ(PXCMRangeF32 value)
        {
            pxcmStatus sts = SetProperty(Property.PROPERTY_DS_MIN_MAX_Z, (Single)value.min);
            if (sts != pxcmStatus.PXCM_STATUS_NO_ERROR)
                sts = SetProperty((Property)(Property.PROPERTY_DS_MIN_MAX_Z + 1), (Single)value.max);
            return sts;
        }

        /// <summary>
        /// Get the color recatification status
        /// <returns> true if enabled</returns>
        /// </summary>
        public Boolean QueryDSColorRectificationEnabled()
        {
            Single value = 0;
            QueryProperty(Property.PROPERTY_DS_COLOR_RECTIFICATION, out value);
            return (value == 0) ? false : true;
        }

        /// <summary>
        /// Get the depth recatification status
        /// </summary>
        /// <returns> true if enabled</returns>
        public Boolean QueryDSDepthRectificationEnabled()
        {
            Single value = 0;
            QueryProperty(Property.PROPERTY_DS_DEPTH_RECTIFICATION, out value);
            return (value == 0) ? false : true;
        }

        /// <summary>
        /// Get DS left and right streams exposure value.
        /// </summary>
        /// <returns> DS left and right streams exposure, in log base 2 seconds.</returns>
        public Single QueryDSLeftRightExposure()
        {
            Single value = 0;
            QueryProperty(Property.PROPERTY_DS_LEFTRIGHT_EXPOSURE, out value);
            return value;
        }

        /// <summary>
        /// Get DS left and right streams exposure property information.
        /// </summary>
        /// <returns> The DS left and right streams exposure property information</returns>
        public PropertyInfo QueryDSLeftRightExposureInfo()
        {
            return QueryPropertyInfoINT(instance, Property.PROPERTY_DS_LEFTRIGHT_EXPOSURE);
        }

        /// <summary> Set DS left and right streams exposure value. </summary>
        /// <param name="value"> DS left and right streams exposure value, in log base 2 seconds. </param>
        /// <returns>
        ///      PXCM_STATUS_NO_ERROR             successful execution. 
        ///      XCM_STATUS_ITEM_UNAVAILABLE     the device property is not supported. 
        /// </returns>
        public pxcmStatus SetDSLeftRightExposure(Single value)
        {
            return SetProperty(Property.PROPERTY_DS_LEFTRIGHT_EXPOSURE, value);
        }

        /// <summary> Set DS left and right streams exposure value. </summary>
        /// <param name="auto1"> True to enable auto exposure. </param>
        /// <returns>
        ///      PXCM_STATUS_NO_ERROR             successful execution. 
        ///      XCM_STATUS_ITEM_UNAVAILABLE     the device property is not supported. 
        /// </returns>
        public pxcmStatus SetDSLeftRightAutoExposure(Boolean auto1)
        {
            return SetPropertyAuto(Property.PROPERTY_DS_LEFTRIGHT_EXPOSURE, auto1);
        }

        /// <summary>
        /// Get DS left and right streams gain value.
        /// </summary>
        /// <returns> DS left and right streams gain adjustment, with negative values darker, positive values brighter, and zero as normal.</returns>
        public Int32 QueryDSLeftRightGain()
        {
            Single value = 0;
            QueryProperty(Property.PROPERTY_DS_LEFTRIGHT_GAIN, out value);
            return (Int32)value;
        }

        /// <summary>
        /// Get DS left and right streams gain property information.
        /// </summary>
        /// <returns> DS left and right streams gain property information</returns>
        public PropertyInfo QueryDSLeftRightGainInfo()
        {
            return QueryPropertyInfoINT(instance, Property.PROPERTY_DS_LEFTRIGHT_GAIN);
        }

        /// <summary>
        /// Set DS left and right streams gain value.
        /// </summary>
        /// <param name="value"> DS left and right streams gain adjustment, with negative values darker, positive values brighter, and zero as normal.</param>
        /// <returns> PXCM_STATUS_NO_ERROR             successful execution.</returns>
        /// <returns> PXCM_STATUS_ITEM_UNAVAILABLE     the device property is not supported.</returns>
        public pxcmStatus SetDSLeftRightGain(Int32 value)
        {
            return SetProperty(Property.PROPERTY_DS_LEFTRIGHT_GAIN, (Single)value);
        }

        /// <summary> 
        /// Read the selected streams asynchronously. The function returns immediately. The application must
        /// synchronize SP to get the stream samples. The application can read more than a single stream using the scope
        /// parameter, provided that all streams have the same frame rate. Otherwise, the function will return error.
        /// </summary>
        /// <param name="scope"> The bit-OR'ed value of stream types of interest.</param>
        /// <param name="sample"> The output sample.</param>
        /// <param name="sp"> The pointer to the SP to be returned.</param>
        /// <returns> PXCM_STATUS_NO_ERROR			successful execution.</returns>
        /// <returns> PXCM_STATUS_DEVICE_LOST			the device is disconnected.</returns>
        /// <returns> PXCM_STATUS_PARAM_UNSUPPORTED	the streams are of different frame rates.</returns>
        public pxcmStatus ReadStreamsAsync(StreamType scope, Sample sample, out PXCMSyncPoint sp)
        {
            IntPtr sp2;
            pxcmStatus sts = PXCMCapture_Device_ReadStreamsAsync(instance, scope, sample.images, out sp2);
            sp = (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR) ? new PXCMSyncPoint(sp2, true) : null;
            return sts;
        }

        /// <summary> 
        /// Read the all configured streams asynchronously. The function returns immediately. The application must
        /// synchronize SP to get the stream samples. The configured streams must have the same frame rate or the function 
        /// will return error.
        /// </summary>
        /// <param name="sample"> The output sample.</param>
        /// <param name="sp"> The pointer to the SP to be returned.</param>
        /// <returns> PXCM_STATUS_NO_ERROR			successful execution.</returns>
        /// <returns> PXCM_STATUS_DEVICE_LOST			the device is disconnected.</returns>
        /// <returns> PXCM_STATUS_PARAM_UNSUPPORTED	the streams are of different frame rates.</returns>
        public pxcmStatus ReadStreamsAsync(Sample sample, out PXCMSyncPoint sp)
        {
            return ReadStreamsAsync(StreamType.STREAM_TYPE_ANY, sample, out sp);
        }

        /// <summary> 
        /// Read the selected streams synchronously. The function blocks until all stream samples are ready.
        /// The application can read more than a single stream using the scope parameter, provided that all streams 
        /// have the same frame rate. Otherwise, the function will return error.
        /// </summary>
        /// <param name="scope"> The bit-OR'ed value of stream types of interest.</param>
        /// <param name="sample"> The output sample.</param>
        /// <returns> PXCM_STATUS_NO_ERROR		    successful execution.</returns>
        /// <returns> PXCM_STATUS_DEVICE_LOST	        the device is disconnected.</returns>
        /// <returns> PXCM_STATUS_PARAM_UNSUPPORTED	the streams are of different frame rates.</returns>
        public pxcmStatus ReadStreams(StreamType scope, Sample sample)
        {
            PXCMSyncPoint sp;
            pxcmStatus sts = ReadStreamsAsync(scope, sample, out sp);
            if (sts < pxcmStatus.PXCM_STATUS_NO_ERROR) return sts;
            sts = sp.Synchronize();
            sp.Dispose();
            return sts;
        }


        /// <summary> 
        /// Return the instance of the PXCProjection interface. Must be called after initialization.
        /// </summary>
        /// <returns> the PXCProjection instance.</returns>
        public PXCMProjection CreateProjection()
        {
            IntPtr projection = PXCMCapture_Device_CreateProjection(instance);
            return (projection == IntPtr.Zero) ? null : new PXCMProjection(projection, true);
        }

        internal Device(IntPtr instance, Boolean delete)
            : base(instance, delete)
        {
        }
    };

    /// <summary> 
    /// 	Return the number of devices.
    /// </summary>
    /// <returns> the number of available devices</returns>
    public Int32 QueryDeviceNum()
    {
        return PXCMCapture_QueryDeviceNum(instance);
    }

    /// <summary> 
    /// 	Activate the device and return the video device instance.
    /// </summary>
    /// <param name="didx"> The zero-based device index</param>
    /// <returns> The device instance.</returns>
    public Device CreateDevice(Int32 didx)
    {
        IntPtr device = PXCMCapture_CreateDevice(instance, didx);
        return (device != IntPtr.Zero) ? new PXCMCapture.Device(device, true) : null;
    }

    public partial class Handler
    {
        /// <summary>
        ///  The Capture calls this function when a capture device is added or removed
        /// </summary>
        public delegate void OnDeviceListChangedDelegate();

        public OnDeviceListChangedDelegate onDeviceListChanged;
    };

    internal PXCMCapture(IntPtr instance, Boolean delete)
        : base(instance, delete)
    {
    }
};

#if RSSDK_IN_NAMESPACE
}
#endif
