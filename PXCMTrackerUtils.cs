using System;
using System.Runtime.InteropServices;

public class PXCMTrackerUtils : PXCMBase {
    [Flags]
    public enum ObjectSize {
        VERY_SMALL = 0,
        SMALL = 5,
        MEDIUM = 10,
        LARGE = MEDIUM | SMALL
    }

    [Flags]
    public enum UtilityCosID {
        CALIBRATION_MARKER = -1,
        ALIGNMENT_MARKER = -2,
        IN_PROGRESS_MAP = -3
    }

    public new const int CUID = 1430999636;

    internal PXCMTrackerUtils(IntPtr instance, bool delete)
        : base(instance, delete) {}

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMTrackerUtils_Start3DMapCreation(IntPtr util, ObjectSize objSize);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMTrackerUtils_Cancel3DMapCreation(IntPtr util);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMTrackerUtils_Start3DMapExtension(IntPtr util);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMTrackerUtils_Cancel3DMapExtension(IntPtr util);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMTrackerUtils_Load3DMap(IntPtr util, string filename);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMTrackerUtils_Save3DMap(IntPtr util, string filename);

    [DllImport("libpxccpp2c")]
    internal static extern int PXCMTrackerUtils_QueryNumberFeaturePoints(IntPtr util);

    [DllImport("libpxccpp2c")]
    internal static extern int PXCMTrackerUtils_QueryFeaturePoints(IntPtr util, int size, out IntPtr ppoints,
        bool returnActive, bool returnInactive);

    internal static int QueryFeaturePointsINT(IntPtr util, int size, PXCMPoint3DF32[] points, bool returnActive,
        bool returnInactive) {
        if (size == 0 || points == null) {
            return 0;
        }
        var ppoints = IntPtr.Zero;
        var val2 = PXCMTrackerUtils_QueryFeaturePoints(util, size, out ppoints, returnActive, returnInactive);
        for (var index = 0; index < Math.Min(size, val2); ++index) {
            Marshal.PtrToStructure(ppoints, points[index]);
            ppoints = new IntPtr(ppoints.ToInt64() + Marshal.SizeOf(typeof (PXCMPoint3DF32)));
        }
        return Math.Min(size, val2);
    }

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMTrackerUtils_Start3DMapAlignment(IntPtr util, int markerID, int markerSize);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMTrackerUtils_Cancel3DMapAlignment(IntPtr util);

    [DllImport("libpxccpp2c")]
    internal static extern bool PXCMTrackerUtils_Is3DMapAlignmentComplete(IntPtr util);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMTrackerUtils_StartCameraCalibration(IntPtr util, int markerID, int markerSize);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMTrackerUtils_CancelCameraCalibration(IntPtr util);

    [DllImport("libpxccpp2c")]
    internal static extern int PXCMTrackerUtils_QueryCalibrationProgress(IntPtr util);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMTrackerUtils_SaveCameraParametersToFile(IntPtr util, string filename);

    public pxcmStatus Start3DMapCreation(ObjectSize objSize) {
        return PXCMTrackerUtils_Start3DMapCreation(instance, objSize);
    }

    public pxcmStatus Cancel3DMapCreation() {
        return PXCMTrackerUtils_Cancel3DMapCreation(instance);
    }

    public pxcmStatus Start3DMapExtension() {
        return PXCMTrackerUtils_Start3DMapExtension(instance);
    }

    public pxcmStatus Cancel3DMapExtension() {
        return PXCMTrackerUtils_Cancel3DMapExtension(instance);
    }

    public pxcmStatus Load3DMap(string filename) {
        return PXCMTrackerUtils_Load3DMap(instance, filename);
    }

    public pxcmStatus Save3DMap(string filename) {
        return PXCMTrackerUtils_Save3DMap(instance, filename);
    }

    public int QueryNumberFeaturePoints() {
        return PXCMTrackerUtils_QueryNumberFeaturePoints(instance);
    }

    public int QueryFeaturePoints(PXCMPoint3DF32[] points, bool returnActive, bool returnInactive) {
        return QueryFeaturePointsINT(instance, points == null ? 0 : points.Length, points, returnActive, returnInactive);
    }

    public pxcmStatus Start3DMapAlignment(int markerID, int markerSize) {
        return PXCMTrackerUtils_Start3DMapAlignment(instance, markerID, markerSize);
    }

    public pxcmStatus Cancel3DMapAlignment() {
        return PXCMTrackerUtils_Cancel3DMapAlignment(instance);
    }

    public bool Is3DMapAlignmentComplete() {
        return PXCMTrackerUtils_Is3DMapAlignmentComplete(instance);
    }

    public pxcmStatus StartCameraCalibration(int markerID, int markerSize) {
        return PXCMTrackerUtils_StartCameraCalibration(instance, markerID, markerSize);
    }

    public pxcmStatus CancelCameraCalibration() {
        return PXCMTrackerUtils_CancelCameraCalibration(instance);
    }

    public int QueryCalibrationProgress() {
        return PXCMTrackerUtils_QueryCalibrationProgress(instance);
    }

    public pxcmStatus SaveCameraParametersToFile(string filename) {
        return PXCMTrackerUtils_SaveCameraParametersToFile(instance, filename);
    }
}