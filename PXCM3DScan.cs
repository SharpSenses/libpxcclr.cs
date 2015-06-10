using System;
using System.Runtime.InteropServices;

public class PXCM3DScan : PXCMBase {
    public enum FileFormat {
        OBJ,
        PLY,
        STL
    }

    [Flags]
    public enum ReconstructionOption {
        NONE = 0,
        SOLIDIFICATION = 1,
        TEXTURE = 2
    }

    public enum ScanningMode {
        VARIABLE,
        OBJECT_ON_PLANAR_SURFACE_DETECTION,
        FACE,
        HEAD,
        BODY
    }

    public new const int CUID = 826884947;

    internal PXCM3DScan(IntPtr instance, bool delete)
        : base(instance, delete) {}

    [DllImport("libpxccpp2c")]
    internal static extern void PXCM3DScan_QueryConfiguration(IntPtr scan, [Out] Configuration config);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCM3DScan_SetConfiguration(IntPtr scan, Configuration config);

    [DllImport("libpxccpp2c")]
    internal static extern IntPtr PXCM3DScan_AcquirePreviewImage(IntPtr scan);

    [DllImport("libpxccpp2c")]
    internal static extern bool PXCM3DScan_IsScanning(IntPtr scan);

    [DllImport("libpxccpp2c", CharSet = CharSet.Unicode)]
    internal static extern pxcmStatus PXCM3DScan_Reconstruct(IntPtr scan, FileFormat format, string filename);

    public Configuration QueryConfiguration() {
        var config = new Configuration();
        PXCM3DScan_QueryConfiguration(instance, config);
        return config;
    }

    public pxcmStatus SetConfiguration(Configuration config) {
        return PXCM3DScan_SetConfiguration(instance, config);
    }

    public PXCMImage AcquirePreviewImage() {
        var instance = PXCM3DScan_AcquirePreviewImage(this.instance);
        if (instance == IntPtr.Zero) {
            return null;
        }
        return new PXCMImage(instance, true);
    }

    public bool IsScanning() {
        return PXCM3DScan_IsScanning(instance);
    }

    public pxcmStatus Reconstruct(FileFormat type, string filename) {
        return PXCM3DScan_Reconstruct(instance, type, filename);
    }

    public static string FileFormatToString(FileFormat format) {
        switch (format) {
            case FileFormat.OBJ:
                return "obj";
            case FileFormat.PLY:
                return "ply";
            case FileFormat.STL:
                return "stl";
            default:
                return "Unknown";
        }
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class Configuration {
        public int minFramesBeforeScanStart;
        public ScanningMode mode;
        public ReconstructionOption options;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)] public int[] reserved;

        public Configuration() {
            reserved = new int[256];
        }
    }
}