using System;
using System.Runtime.InteropServices;

public class PXCMBlobConfiguration : PXCMBase {
    public new const int CUID = 1195593026;

    internal PXCMBlobConfiguration(IntPtr instance, bool delete)
        : base(instance, delete) {}

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMBlobConfiguration_ApplyChanges(IntPtr instance);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMBlobConfiguration_RestoreDefaults(IntPtr instance);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMBlobConfiguration_Update(IntPtr instance);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMBlobConfiguration_SetSegmentationSmoothing(IntPtr instance,
        float smoothingValue);

    [DllImport("libpxccpp2c")]
    internal static extern float PXCMBlobConfiguration_QuerySegmentationSmoothing(IntPtr instance);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMBlobConfiguration_SetContourSmoothing(IntPtr instance, float smoothingValue);

    [DllImport("libpxccpp2c")]
    internal static extern float PXCMBlobConfiguration_QueryContourSmoothing(IntPtr instance);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMBlobConfiguration_SetMaxBlobs(IntPtr instance, int maxBlobs);

    [DllImport("libpxccpp2c")]
    internal static extern int PXCMBlobConfiguration_QueryMaxBlobs(IntPtr instance);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMBlobConfiguration_SetMaxDistance(IntPtr instance, float maxDistance);

    [DllImport("libpxccpp2c")]
    internal static extern float PXCMBlobConfiguration_QueryMaxDistance(IntPtr instance);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMBlobConfiguration_SetMaxObjectDepth(IntPtr instance, float maxDepth);

    [DllImport("libpxccpp2c")]
    internal static extern float PXCMBlobConfiguration_QueryMaxObjectDepth(IntPtr instance);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMBlobConfiguration_SetMinPixelCount(IntPtr instance, int minBlobSize);

    [DllImport("libpxccpp2c")]
    internal static extern int PXCMBlobConfiguration_QueryMinPixelCount(IntPtr instance);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMBlobConfiguration_EnableSegmentationImage(IntPtr instance,
        [MarshalAs(UnmanagedType.Bool)] bool enableFlag);

    [DllImport("libpxccpp2c")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool PXCMBlobConfiguration_IsSegmentationImageEnabled(IntPtr instance);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMBlobConfiguration_EnableContourExtraction(IntPtr instance,
        [MarshalAs(UnmanagedType.Bool)] bool enableFlag);

    [DllImport("libpxccpp2c")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool PXCMBlobConfiguration_IsContourExtractionEnabled(IntPtr instance);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMBlobConfiguration_SetMinContourSize(IntPtr instance, int minContourSize);

    [DllImport("libpxccpp2c")]
    internal static extern int PXCMBlobConfiguration_QueryMinContourSize(IntPtr instance);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMBlobConfiguration_EnableStabilizer(IntPtr instance,
        [MarshalAs(UnmanagedType.Bool)] bool enableFlag);

    [DllImport("libpxccpp2c")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool PXCMBlobConfiguration_IsStabilizerEnabled(IntPtr instance);

    public pxcmStatus ApplyChanges() {
        return PXCMBlobConfiguration_ApplyChanges(instance);
    }

    public pxcmStatus RestoreDefaults() {
        return PXCMBlobConfiguration_RestoreDefaults(instance);
    }

    public pxcmStatus Update() {
        return PXCMBlobConfiguration_Update(instance);
    }

    public pxcmStatus SetSegmentationSmoothing(float smoothingValue) {
        return PXCMBlobConfiguration_SetSegmentationSmoothing(instance, smoothingValue);
    }

    public float QuerySegmentationSmoothing() {
        return PXCMBlobConfiguration_QuerySegmentationSmoothing(instance);
    }

    public pxcmStatus SetContourSmoothing(float smoothingValue) {
        return PXCMBlobConfiguration_SetContourSmoothing(instance, smoothingValue);
    }

    public float QueryContourSmoothing() {
        return PXCMBlobConfiguration_QueryContourSmoothing(instance);
    }

    public pxcmStatus SetMaxBlobs(int maxBlobs) {
        return PXCMBlobConfiguration_SetMaxBlobs(instance, maxBlobs);
    }

    public int QueryMaxBlobs() {
        return PXCMBlobConfiguration_QueryMaxBlobs(instance);
    }

    public pxcmStatus SetMaxDistance(float maxDistance) {
        return PXCMBlobConfiguration_SetMaxDistance(instance, maxDistance);
    }

    public float QueryMaxDistance() {
        return PXCMBlobConfiguration_QueryMaxDistance(instance);
    }

    public pxcmStatus SetMaxObjectDepth(float maxDepth) {
        return PXCMBlobConfiguration_SetMaxObjectDepth(instance, maxDepth);
    }

    public float QueryMaxObjectDepth() {
        return PXCMBlobConfiguration_QueryMaxObjectDepth(instance);
    }

    public pxcmStatus SetMinPixelCount(int minBlobSize) {
        return PXCMBlobConfiguration_SetMinPixelCount(instance, minBlobSize);
    }

    public int QueryMinPixelCount() {
        return PXCMBlobConfiguration_QueryMinPixelCount(instance);
    }

    public pxcmStatus EnableSegmentationImage(bool enableFlag) {
        return PXCMBlobConfiguration_EnableSegmentationImage(instance, enableFlag);
    }

    public bool IsSegmentationImageEnabled() {
        return PXCMBlobConfiguration_IsSegmentationImageEnabled(instance);
    }

    public pxcmStatus EnableContourExtraction(bool enableFlag) {
        return PXCMBlobConfiguration_EnableContourExtraction(instance, enableFlag);
    }

    public bool IsContourExtractionEnabled() {
        return PXCMBlobConfiguration_IsContourExtractionEnabled(instance);
    }

    public pxcmStatus SetMinContourSize(int minContourSize) {
        return PXCMBlobConfiguration_SetMinContourSize(instance, minContourSize);
    }

    public int QueryMinContourSize() {
        return PXCMBlobConfiguration_QueryMinContourSize(instance);
    }

    public pxcmStatus EnableStabilizer(bool enableFlag) {
        return PXCMBlobConfiguration_EnableStabilizer(instance, enableFlag);
    }

    public bool IsStabilizerEnabled() {
        return PXCMBlobConfiguration_IsStabilizerEnabled(instance);
    }
}