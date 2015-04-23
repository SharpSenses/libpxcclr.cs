using System;
using System.Runtime.InteropServices;

public class PXCM3DScan : PXCMBase {
    public new const int CUID = 826884947;

    internal PXCM3DScan(IntPtr instance, bool delete)
        : base(instance, delete) {
    }

    [DllImport("libpxccpp2c")]
    internal static extern Mode PXCM3DScan_QueryMode(IntPtr scan);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCM3DScan_SetMode(IntPtr scan, Mode mode);

    [DllImport("libpxccpp2c")]
    internal static extern IntPtr PXCM3DScan_AcquirePreviewImage(IntPtr scan);

    [DllImport("libpxccpp2c", CharSet = CharSet.Unicode)]
    internal static extern pxcmStatus PXCM3DScan_Reconstruct(IntPtr scan, FileFormat format, string filename, ReconstructionOption options);

    [DllImport("libpxccpp2c")]
    internal static extern TargetingOption PXCM3DScan_QueryTargetingOptions(IntPtr scan);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCM3DScan_SetTargetingOptions(IntPtr scan, TargetingOption options);

    public Mode QueryMode() {
        return PXCM3DScan_QueryMode(instance);
    }

    public pxcmStatus SetMode(Mode mode) {
        return PXCM3DScan_SetMode(instance, mode);
    }

    public TargetingOption QueryTargetingOptions() {
        return PXCM3DScan_QueryTargetingOptions(instance);
    }

    public pxcmStatus SetTargetingOptions(TargetingOption options) {
        return PXCM3DScan_SetTargetingOptions(instance, options);
    }

    public PXCMImage AcquirePreviewImage() {
        IntPtr instance = PXCM3DScan_AcquirePreviewImage(this.instance);
        if (instance == IntPtr.Zero)
            return null;
        return new PXCMImage(instance, true);
    }

    public pxcmStatus Reconstruct(FileFormat type, string filename, ReconstructionOption options) {
        return PXCM3DScan_Reconstruct(instance, type, filename, options);
    }

    public pxcmStatus Reconstruct(FileFormat type, string filename) {
        return Reconstruct(type, filename, ReconstructionOption.NO_RECONSTRUCTION_OPTIONS);
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

    public enum Mode {
        TARGETING,
        SCANNING
    }

    [Flags]
    public enum TargetingOption {
        NO_TARGETING_OPTIONS = 0,
        OBJECT_ON_PLANAR_SURFACE_DETECTION = 1
    }

    public enum FileFormat {
        OBJ,
        PLY,
        STL
    }

    [Flags]
    public enum ReconstructionOption {
        NO_RECONSTRUCTION_OPTIONS = 0,
        SOLIDIFICATION = 1
    }
}
