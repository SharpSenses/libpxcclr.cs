using System;
using System.Runtime.InteropServices;

public class PXCMEnhancedPhotography : PXCMBase {
    public enum DepthFillQuality {
        HIGH,
        LOW
    }

    public enum TrackMethod {
        LAYER,
        OBJECT
    }

    public new const int CUID = 1313427525;

    internal PXCMEnhancedPhotography(IntPtr instance, bool delete)
        : base(instance, delete) {}

    [DllImport("libpxccpp2c")]
    internal static extern float PXCMEnhancedPhotography_MeasureDistance(IntPtr ep, IntPtr sample,
        PXCMPointI32 startPoint, PXCMPointI32 endPoint);

    [DllImport("libpxccpp2c")]
    internal static extern IntPtr PXCMEnhancedPhotography_DepthRefocus(IntPtr ep, IntPtr sample, PXCMPointI32 focusPoint,
        float aperture);

    [DllImport("libpxccpp2c")]
    internal static extern IntPtr PXCMEnhancedPhotography_EnhanceDepth(IntPtr ep, IntPtr sample,
        DepthFillQuality depthQuality);

    [DllImport("libpxccpp2c")]
    internal static extern float PXCMEnhancedPhotography_GetDepthThreshold(IntPtr ep, IntPtr sample, PXCMPointI32 coord);

    [DllImport("libpxccpp2c")]
    internal static extern IntPtr PXCMEnhancedPhotography_ComputeMaskFromThreshold(IntPtr ep, IntPtr sample,
        float depthThreshold);

    [DllImport("libpxccpp2c")]
    internal static extern IntPtr PXCMEnhancedPhotography_ComputeMaskFromCoordinate(IntPtr ep, IntPtr sample,
        PXCMPointI32 coord);

    [DllImport("libpxccpp2c")]
    internal static extern IntPtr PXCMEnhancedPhotography_DepthResize(IntPtr ep, IntPtr sample, PXCMSizeI32 size);

    [DllImport("libpxccpp2c")]
    internal static extern IntPtr PXCMEnhancedPhotography_PasteOnPlane(IntPtr ep, IntPtr sample, IntPtr imbedImInstance,
        PXCMPointI32 topLeftCoord, PXCMPointI32 bottomLeftCoord);

    [DllImport("libpxccpp2c")]
    internal static extern IntPtr PXCMEnhancedPhotography_DepthBlend(IntPtr ep, IntPtr sampleBackground,
        IntPtr imgForegroundInstance, PXCMPointI32 insertCoord, int insertDepth, int[] rotation, float scaleFG);

    [DllImport("libpxccpp2c")]
    internal static extern IntPtr PXCMEnhancedPhotography_ObjectSegment(IntPtr ep, IntPtr photo,
        ref PXCMPointI32 topLeftCoord, ref PXCMPointI32 bottomRightCoord);

    [DllImport("libpxccpp2c")]
    internal static extern IntPtr PXCMEnhancedPhotography_RefineMask(IntPtr ep, IntPtr hints);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMEnhancedPhotography_InitTracker(IntPtr ep, IntPtr firstFrame,
        IntPtr boundingMask, TrackMethod method);

    [DllImport("libpxccpp2c")]
    internal static extern IntPtr PXCMEnhancedPhotography_TrackObject(IntPtr ep, IntPtr nextFrame);

    public float MeasureDistance(PXCMPhoto sample, PXCMPointI32 startPoint, PXCMPointI32 endPoint) {
        return PXCMEnhancedPhotography_MeasureDistance(instance, sample.instance, startPoint, endPoint);
    }

    public PXCMPhoto DepthRefocus(PXCMPhoto sample, PXCMPointI32 focusPoint, float aperture) {
        var instance = PXCMEnhancedPhotography_DepthRefocus(this.instance, sample.instance, focusPoint, aperture);
        if (!(instance != IntPtr.Zero)) {
            return null;
        }
        return new PXCMPhoto(instance, true);
    }

    public PXCMPhoto DepthRefocus(PXCMPhoto sample, PXCMPointI32 focusPoint) {
        return DepthRefocus(sample, focusPoint, 50f);
    }

    public PXCMPhoto EnhanceDepth(PXCMPhoto sample, DepthFillQuality depthQuality) {
        var instance = PXCMEnhancedPhotography_EnhanceDepth(this.instance, sample.instance, depthQuality);
        if (!(instance != IntPtr.Zero)) {
            return null;
        }
        return new PXCMPhoto(instance, true);
    }

    public PXCMImage ComputeMaskFromThreshold(PXCMPhoto sample, float depthThreshold) {
        var maskFromThreshold = PXCMEnhancedPhotography_ComputeMaskFromThreshold(instance, sample.instance,
            depthThreshold);
        if (!(maskFromThreshold != IntPtr.Zero)) {
            return null;
        }
        return new PXCMImage(maskFromThreshold, true);
    }

    public PXCMImage ComputeMaskFromCoordinate(PXCMPhoto sample, PXCMPointI32 coord) {
        var maskFromCoordinate = PXCMEnhancedPhotography_ComputeMaskFromCoordinate(instance, sample.instance, coord);
        if (!(maskFromCoordinate != IntPtr.Zero)) {
            return null;
        }
        return new PXCMImage(maskFromCoordinate, true);
    }

    public PXCMPhoto DepthResize(PXCMPhoto sample, PXCMSizeI32 size) {
        var instance = PXCMEnhancedPhotography_DepthResize(this.instance, sample.instance, size);
        if (!(instance != IntPtr.Zero)) {
            return null;
        }
        return new PXCMPhoto(instance, true);
    }

    public PXCMPhoto PasteOnPlane(PXCMPhoto sample, PXCMImage imbedIm, PXCMPointI32 topLeftCoord,
        PXCMPointI32 bottomLeftCoord) {
        var instance = PXCMEnhancedPhotography_PasteOnPlane(this.instance, sample.instance, imbedIm.instance,
            topLeftCoord, bottomLeftCoord);
        if (!(instance != IntPtr.Zero)) {
            return null;
        }
        return new PXCMPhoto(instance, true);
    }

    public PXCMPhoto DepthBlend(PXCMPhoto sampleBackground, PXCMImage imgForeground, PXCMPointI32 insertCoord,
        int insertDepth, int[] rotation, float scaleFG) {
        var instance = PXCMEnhancedPhotography_DepthBlend(this.instance, sampleBackground.instance,
            imgForeground.instance, insertCoord, insertDepth, rotation, scaleFG);
        if (!(instance != IntPtr.Zero)) {
            return null;
        }
        return new PXCMPhoto(instance, true);
    }

    public PXCMImage ObjectSegment(PXCMPhoto photo, PXCMPointI32 topLeftCoord, PXCMPointI32 bottomRightCoord) {
        var instance = PXCMEnhancedPhotography_ObjectSegment(this.instance, photo.instance, ref topLeftCoord,
            ref bottomRightCoord);
        if (!(instance == IntPtr.Zero)) {
            return new PXCMImage(instance, true);
        }
        return null;
    }

    public PXCMImage RefineMask(PXCMImage hints) {
        var instance = PXCMEnhancedPhotography_RefineMask(this.instance, hints.instance);
        if (!(instance == IntPtr.Zero)) {
            return new PXCMImage(instance, true);
        }
        return null;
    }

    public pxcmStatus InitTracker(PXCMPhoto firstFrame, PXCMImage boundingMask, TrackMethod method) {
        return PXCMEnhancedPhotography_InitTracker(instance, firstFrame.instance, boundingMask.instance, method);
    }

    public pxcmStatus InitTracker(PXCMPhoto firstFrame, PXCMImage boundingMask) {
        return InitTracker(firstFrame, boundingMask, TrackMethod.LAYER);
    }

    public PXCMImage TrackObject(PXCMPhoto nextFrame) {
        var instance = PXCMEnhancedPhotography_TrackObject(this.instance, nextFrame.instance);
        if (!(instance == IntPtr.Zero)) {
            return new PXCMImage(instance, true);
        }
        return null;
    }
}