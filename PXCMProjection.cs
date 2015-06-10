using System;
using System.Runtime.InteropServices;

public class PXCMProjection : PXCMBase {
    public new const int CUID = 1229620535;

    internal PXCMProjection(IntPtr instance, bool delete)
        : base(instance, delete) {}

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMProjection_MapDepthToColor(IntPtr p, int npoints, PXCMPoint3DF32[] pos_uvz,
        [Out] PXCMPointF32[] pos_ij);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMProjection_MapColorToDepth(IntPtr p, IntPtr depth, int npoints,
        PXCMPointF32[] pos_ij, [Out] PXCMPointF32[] pos_uv);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMProjection_ProjectDepthToCamera(IntPtr p, int npoints,
        PXCMPoint3DF32[] pos_uvz, [Out] PXCMPoint3DF32[] pos3d);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMProjection_ProjectColorToCamera(IntPtr p, int npoints,
        PXCMPoint3DF32[] pos_ijz, [Out] PXCMPoint3DF32[] pos3d);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMProjection_ProjectCameraToDepth(IntPtr p, int npoints, PXCMPoint3DF32[] pos3d,
        [Out] PXCMPointF32[] pos_uv);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMProjection_ProjectCameraToColor(IntPtr p, int npoints, PXCMPoint3DF32[] pos3d,
        [Out] PXCMPointF32[] pos_ij);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMProjection_QueryUVMap(IntPtr instance, IntPtr depth,
        [Out] PXCMPointF32[] uvmap);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMProjection_QueryInvUVMap(IntPtr instance, IntPtr depth,
        [Out] PXCMPointF32[] inv_uvmap);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMProjection_QueryVertices(IntPtr instance, IntPtr depth,
        [Out] PXCMPoint3DF32[] vertices);

    [DllImport("libpxccpp2c")]
    internal static extern IntPtr PXCMProjection_CreateColorImageMappedToDepth(IntPtr instance, IntPtr depth,
        IntPtr color);

    [DllImport("libpxccpp2c")]
    internal static extern IntPtr PXCMProjection_CreateDepthImageMappedToColor(IntPtr instance, IntPtr depth,
        IntPtr color);

    public pxcmStatus MapDepthToColor(PXCMPoint3DF32[] pos_uvz, PXCMPointF32[] pos_ij) {
        return PXCMProjection_MapDepthToColor(instance, pos_uvz.Length, pos_uvz, pos_ij);
    }

    public pxcmStatus MapColorToDepth(PXCMImage depth, PXCMPointF32[] pos_ij, PXCMPointF32[] pos_uv) {
        return PXCMProjection_MapColorToDepth(instance, depth.instance, pos_ij.Length, pos_ij, pos_uv);
    }

    public pxcmStatus ProjectDepthToCamera(PXCMPoint3DF32[] pos_uvz, PXCMPoint3DF32[] pos3d) {
        return PXCMProjection_ProjectDepthToCamera(instance, pos_uvz.Length, pos_uvz, pos3d);
    }

    public pxcmStatus ProjectColorToCamera(PXCMPoint3DF32[] pos_ijz, PXCMPoint3DF32[] pos3d) {
        return PXCMProjection_ProjectColorToCamera(instance, pos_ijz.Length, pos_ijz, pos3d);
    }

    public pxcmStatus ProjectCameraToDepth(PXCMPoint3DF32[] pos3d, PXCMPointF32[] pos_uv) {
        return PXCMProjection_ProjectCameraToDepth(instance, pos3d.Length, pos3d, pos_uv);
    }

    public pxcmStatus ProjectCameraToColor(PXCMPoint3DF32[] pos3d, PXCMPointF32[] pos_ij) {
        return PXCMProjection_ProjectCameraToColor(instance, pos3d.Length, pos3d, pos_ij);
    }

    public pxcmStatus QueryUVMap(PXCMImage depth, PXCMPointF32[] uvmap) {
        return PXCMProjection_QueryUVMap(instance, depth.instance, uvmap);
    }

    public pxcmStatus QueryInvUVMap(PXCMImage depth, PXCMPointF32[] inv_uvmap) {
        return PXCMProjection_QueryInvUVMap(instance, depth.instance, inv_uvmap);
    }

    public pxcmStatus QueryVertices(PXCMImage depth, PXCMPoint3DF32[] vertices) {
        return PXCMProjection_QueryVertices(instance, depth.instance, vertices);
    }

    public PXCMImage CreateColorImageMappedToDepth(PXCMImage depth, PXCMImage color) {
        var imageMappedToDepth = PXCMProjection_CreateColorImageMappedToDepth(instance, depth.instance, color.instance);
        if (!(imageMappedToDepth != IntPtr.Zero)) {
            return null;
        }
        return new PXCMImage(imageMappedToDepth, true);
    }

    public PXCMImage CreateDepthImageMappedToColor(PXCMImage depth, PXCMImage color) {
        var imageMappedToColor = PXCMProjection_CreateDepthImageMappedToColor(instance, depth.instance, color.instance);
        if (!(imageMappedToColor != IntPtr.Zero)) {
            return null;
        }
        return new PXCMImage(imageMappedToColor, true);
    }
}