public static class PXCMTypeExtension {
    public static string ToString(this PXCMImage.PixelFormat format) {
        return PXCMImage.PixelFormatToString(format);
    }

    public static int ToSize(this PXCMAudio.AudioFormat format) {
        return PXCMAudio.AudioFormatToSize(format);
    }

    public static string ToString(this PXCMAudio.AudioFormat format) {
        return PXCMAudio.AudioFormatToString(format);
    }

    public static string ToString(this PXCMCapture.StreamType stream) {
        return PXCMCapture.StreamTypeToString(stream);
    }

    public static int ToIndex(this PXCMCapture.StreamType type) {
        return PXCMCapture.StreamTypeToIndex(type);
    }

    public static string ToString(this PXCM3DScan.FileFormat format) {
        return PXCM3DScan.FileFormatToString(format);
    }
}