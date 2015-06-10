using System;
using System.Runtime.InteropServices;

public class PXCMSurfaceVoxelsData : PXCMBase {
    public new const int CUID = 1448300627;

    internal PXCMSurfaceVoxelsData(IntPtr instance, bool delete)
        : base(instance, delete) {}

    [DllImport("libpxccpp2c")]
    internal static extern int PXCMSurfaceVoxelsData_QueryNumberOfSurfaceVoxels(IntPtr voxel);

    [DllImport("libpxccpp2c")]
    internal static extern IntPtr PXCMSurfaceVoxelsData_QueryCenterOfSurfaceVoxels(IntPtr voxel);

    internal static void QueryCenterOfSurfaceVoxelsINT(IntPtr voxel, float[] voxels, int nvoxels) {
        Marshal.Copy(PXCMSurfaceVoxelsData_QueryCenterOfSurfaceVoxels(voxel), voxels, 0, nvoxels*3);
    }

    [DllImport("libpxccpp2c")]
    internal static extern IntPtr PXCMSurfaceVoxelsData_QuerySurfaceVoxelsColor(IntPtr voxel);

    internal static void QuerySurfaceVoxelsColorINT(IntPtr voxel, byte[] colors, int nvoxels) {
        Marshal.Copy(PXCMSurfaceVoxelsData_QuerySurfaceVoxelsColor(voxel), colors, 0, nvoxels*3);
    }

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMSurfaceVoxelsData_Reset(IntPtr voxel);

    public int QueryNumberOfSurfaceVoxels() {
        return PXCMSurfaceVoxelsData_QueryNumberOfSurfaceVoxels(instance);
    }

    public float[] QueryCenterOfSurfaceVoxels() {
        var nvoxels = QueryNumberOfSurfaceVoxels();
        if (nvoxels <= 0) {
            return null;
        }
        var voxels = new float[3*nvoxels];
        QueryCenterOfSurfaceVoxelsINT(instance, voxels, nvoxels);
        return voxels;
    }

    public byte[] QuerySurfaceVoxelsColor() {
        var nvoxels = QueryNumberOfSurfaceVoxels();
        if (nvoxels <= 0) {
            return null;
        }
        var colors = new byte[3*nvoxels];
        QuerySurfaceVoxelsColorINT(instance, colors, nvoxels);
        return colors;
    }

    public pxcmStatus Reset() {
        return PXCMSurfaceVoxelsData_Reset(instance);
    }
}