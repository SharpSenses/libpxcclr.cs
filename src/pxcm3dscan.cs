/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2013-2015 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Runtime.InteropServices;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk {
#endif

public partial class PXCM3DScan: PXCMBase {
    new public const Int32 CUID=0x31494353;

    public delegate void OnAlertDelegate(PXCM3DScan.AlertData data);

    /// Reconstruction options
    [Flags]
    public enum ReconstructionOption: int { /// Bit-OR'ed values
        NONE           = 0,
        SOLIDIFICATION = (1 << 0), // Generate a closed manifold mesh.
        TEXTURE        = (1 << 1), // Disable vertex color, generate texture map (.jpg), uv coordinates and material (.mtl) files.
        LANDMARKS      = (1 << 2), // Use face module generate output landmark data file (.json) relative to mesh.
    };

    /// Scanning modes
    public enum ScanningMode {
        VARIABLE = 0,                        // Fixed (in this release) to the largest scanning area per camera
        OBJECT_ON_PLANAR_SURFACE_DETECTION,  // Scanning area is auto-fit to detected object, surface is removed automaticaly
        FACE,                                // Fixed scanning size for scanning human faces.
        HEAD,                                // Fixed scanning size for scanning human head (and shoulders, or hat)
        BODY                                 // Fixed scanning size for scanning human body (with arms at sides)
    };

    /// Configuration structure
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class Configuration {
        [MarshalAs(UnmanagedType.Bool)] 
        public Boolean              startScan;
        public ScanningMode         mode;
        public ReconstructionOption options;
        public Int32                maxTriangles;
        public Int32                maxVertices;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 63)]
        public Int32[]              reserved;

        public Configuration()
        {
            reserved = new Int32[64];
        }
    };

   /// <summary>
   /// Query the current PXCM3DScan Configuration
   /// </summary>
    /// <returns> PXCM3DScan Configuration object </returns>
    public Configuration QueryConfiguration()
    {
        Configuration config = new Configuration();
        PXCM3DScan_QueryConfiguration(instance, config);
        return config;
    }

    /// <summary>
    /// Set the PXCM3DScan configuration.
    /// </summary>
    /// <param name="config"> The configuration object to be set. </param>
    /// <returns> the method executation status code </returns>
    public pxcmStatus SetConfiguration(Configuration config)
    {
        return PXCM3DScan_SetConfiguration(instance, config);
    }

    /// Area structure
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class Area
    {
        public PXCMSize3DF32 shape;      // Scanning volume (width, height, depth) in camera space (m). Set to zero (0) to auto select.
        public Int32         resolution; // Voxel resolution (along longest shape axis). Set to zero (0) to auto select.
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public Int32[] reserved;

        public Area()
        {
            reserved = new Int32[64];
        }
    };

    /// Query the current PXC3DScan Area
    public Area QueryArea()
    {
        Area area = new Area();
        PXCM3DScan_QueryArea(instance, area);
        return area;
    }

    /// Set the PXC3DScan Area
    public pxcmStatus SetArea(Area area)
    {
        return PXCM3DScan_SetArea(instance, area);
    } 

    /// <summary>
    /// <para> PXC3DScan preview image access </para>
    /// <para> The preview image is a rendered approximation of the scanning volume </para>
    /// <para> from the perspective of the camera. A different image is available </para>
    /// <para> each time a frame is processed. </para>
    /// </summary>
    /// <returns> A privew image </returns>
    public PXCMImage AcquirePreviewImage() {
        IntPtr image=PXCM3DScan_AcquirePreviewImage(instance);
        if (image==IntPtr.Zero) return null;
        return new PXCMImage(image, true);
    }

    /// Return the visible extent from the perspective of the most recent frame.
    /// Values are in normalized coordinates (0.0-1.0).
    public PXCMRectF32 QueryBoundingBox()
    {
        PXCMRectF32 rect = new PXCMRectF32();
        PXCM3DScan_QueryBoundingBox(instance, ref rect);
        return rect;
    }


    /// Determine if the scan has started
    public Boolean IsScanning()
    {
        return PXCM3DScan_IsScanning(instance);
    }

    /// PXC3DScan mesh formats supported by Reconstruct
    public enum FileFormat { 
        OBJ, 
        PLY, 
        STL 
    };

    /// PXC3DScan generation of standard mesh formats from the scanning volume.
    public pxcmStatus Reconstruct(FileFormat type, String filename) {
        return PXCM3DScan_Reconstruct(instance, type, filename);
    }

    /// PXC3DScan utility to convert FileFormat value to a string
    public static String FileFormatToString(FileFormat format) {
        switch (format) {
        case FileFormat.OBJ: return "obj";
        case FileFormat.PLY: return "ply";
        case FileFormat.STL: return "stl";
        }
        return "Unknown";
    }

    /* constructors and misc */
    internal PXCM3DScan(IntPtr instance, Boolean delete)
        : base(instance, delete)
    {
    }

    /**
        @enum AlertEvent
        Enumeratea all supported alert events.
    */
    [Flags]
    public enum AlertEvent
    {
        // Scanning alerts (fired AFTER the system is scanning)

        // Range alerts
        ALERT_IN_RANGE = 0,
        ALERT_TOO_CLOSE,
        ALERT_TOO_FAR,

        // Tracking alerts
        ALERT_TRACKING,
        ALERT_LOST_TRACKING,

        // Pre-scanning alerts (fired BEFORE the system is scanning)
        // Each group represents a pre-conditions which must satisfied 
        // before scanning will start.

        // Tracking alerts
        ALERT_SUFFICIENT_STRUCTURE,
        ALERT_INSUFFICIENT_STRUCTURE,

        // Face alerts (if ReconstructionOption::LANDMARKS is set)
        ALERT_FACE_DETECTED,
        ALERT_FACE_NOT_DETECTED,

        ALERT_FACE_X_IN_RANGE,
        ALERT_FACE_X_TOO_FAR_LEFT,
        ALERT_FACE_X_TOO_FAR_RIGHT,

        ALERT_FACE_Y_IN_RANGE,
        ALERT_FACE_Y_TOO_FAR_UP,
        ALERT_FACE_Y_TOO_FAR_DOWN,

        ALERT_FACE_Z_IN_RANGE,
        ALERT_FACE_Z_TOO_CLOSE,
        ALERT_FACE_Z_TOO_FAR,

        ALERT_FACE_YAW_IN_RANGE,
        ALERT_FACE_YAW_TOO_FAR_LEFT,
        ALERT_FACE_YAW_TOO_FAR_RIGHT,

        ALERT_FACE_PITCH_IN_RANGE,
        ALERT_FACE_PITCH_TOO_FAR_UP,
        ALERT_FACE_PITCH_TOO_FAR_DOWN,
    };

    /**
        @struct AlertData
        Describe the alert parameters.
    */
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class AlertData
    {
        public Int64 timeStamp;
        public AlertEvent label;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        internal Int32[] reserved;

        public AlertData()
        {
            reserved = new Int32[5];
        }
    }

    public void Subscribe(OnAlertDelegate d)
    {
        SubscribeINT(d);
    }
};

#if RSSDK_IN_NAMESPACE
}
#endif