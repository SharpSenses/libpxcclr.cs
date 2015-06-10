using System;
using System.Runtime.InteropServices;

public class PXCMPhoto : PXCMBase {
    public new const int CUID = 844514375;

    internal PXCMPhoto(IntPtr instance, bool delete)
        : base(instance, delete) {}

    [DllImport("libpxccpp2c")]
    internal static extern IntPtr PXCMPhoto_QueryReferenceImage(IntPtr photo);

    [DllImport("libpxccpp2c")]
    internal static extern IntPtr PXCMPhoto_QueryDepthImage(IntPtr photo);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMPhoto_CopyPhoto(IntPtr photo, IntPtr source);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMPhoto_ImportFromPreviewSample(IntPtr photo, PXCMCapture.Sample sample);

    [DllImport("libpxccpp2c", CharSet = CharSet.Unicode)]
    internal static extern pxcmStatus PXCMPhoto_LoadXMP(IntPtr photo, string filename);

    [DllImport("libpxccpp2c", CharSet = CharSet.Unicode)]
    internal static extern pxcmStatus PXCMPhoto_SaveXMP(IntPtr photo, string filename);

    public PXCMImage QueryReferenceImage() {
        var instance = PXCMPhoto_QueryReferenceImage(this.instance);
        if (!(instance == IntPtr.Zero)) {
            return new PXCMImage(instance, false);
        }
        return null;
    }

    public PXCMImage QueryDepthImage() {
        var instance = PXCMPhoto_QueryDepthImage(this.instance);
        if (!(instance == IntPtr.Zero)) {
            return new PXCMImage(instance, false);
        }
        return null;
    }

    public pxcmStatus CopyPhoto(PXCMPhoto photo) {
        return PXCMPhoto_CopyPhoto(instance, photo.instance);
    }

    public pxcmStatus ImportFromPreviewSample(PXCMCapture.Sample sample) {
        return PXCMPhoto_ImportFromPreviewSample(instance, sample);
    }

    public pxcmStatus LoadXMP(string filename) {
        return PXCMPhoto_LoadXMP(instance, filename);
    }

    public pxcmStatus SaveXMP(string filename) {
        return PXCMPhoto_SaveXMP(instance, filename);
    }

    public void AddRef() {
        using (var pxcmBase = QueryInstance(1397965122)) {
            if (pxcmBase == null) {
                return;
            }
            pxcmBase.AddRef();
        }
    }
}