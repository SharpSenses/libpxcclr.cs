using System;
using System.Runtime.InteropServices;

public class PXCMContourExtractor : PXCMBase {
    public new const int CUID = -1728306093;

    internal PXCMContourExtractor(IntPtr instance, bool delete)
        : base(instance, delete) {}

    [DllImport("libpxccpp2c")]
    internal static extern void PXCMContourExtractor_Init(IntPtr instsance, PXCMImage.ImageInfo imageInfo);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMContourExtractor_ProcessImage(IntPtr instsance, IntPtr depthImage);

    [DllImport("libpxccpp2c")]
    private static extern pxcmStatus PXCMContourExtractor_QueryContourData(IntPtr instsance, int index, int max,
        [Out] PXCMPointI32[] contour);

    internal static pxcmStatus QueryContourDataINT(IntPtr instance, int index, out PXCMPointI32[] contour) {
        var max = PXCMContourExtractor_QueryContourSize(instance, index);
        if (max > 0) {
            contour = new PXCMPointI32[max];
            return PXCMContourExtractor_QueryContourData(instance, index, max, contour);
        }
        contour = null;
        return pxcmStatus.PXCM_STATUS_DATA_UNAVAILABLE;
    }

    [DllImport("libpxccpp2c")]
    private static extern int PXCMContourExtractor_QueryContourSize(IntPtr instsance, int index);

    [DllImport("libpxccpp2c")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool PXCMContourExtractor_IsContourOuter(IntPtr instsance, int index);

    [DllImport("libpxccpp2c")]
    internal static extern int PXCMContourExtractor_QueryNumberOfContours(IntPtr instance);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMContourExtractor_SetSmoothing(IntPtr instance, float smoothing);

    [DllImport("libpxccpp2c")]
    internal static extern float PXCMContourExtractor_QuerySmoothing(IntPtr instance);

    public void Init(PXCMImage.ImageInfo imageInfo) {
        PXCMContourExtractor_Init(instance, imageInfo);
    }

    public pxcmStatus ProcessImage(PXCMImage depthImage) {
        return PXCMContourExtractor_ProcessImage(instance, depthImage.instance);
    }

    public pxcmStatus QueryContourData(int index, out PXCMPointI32[] contour) {
        return QueryContourDataINT(instance, index, out contour);
    }

    public int QueryContourSize(int index) {
        return PXCMContourExtractor_QueryContourSize(instance, index);
    }

    public bool IsContourOuter(int index) {
        return PXCMContourExtractor_IsContourOuter(instance, index);
    }

    public int QueryNumberOfContours() {
        return PXCMContourExtractor_QueryNumberOfContours(instance);
    }

    public pxcmStatus SetSmoothing(float smoothing) {
        return PXCMContourExtractor_SetSmoothing(instance, smoothing);
    }

    public float QuerySmoothing() {
        return PXCMContourExtractor_QuerySmoothing(instance);
    }
}