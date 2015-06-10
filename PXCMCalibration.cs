using System;
using System.Runtime.InteropServices;

public class PXCMCalibration : PXCMBase {
    public new const int CUID = 1229620536;

    internal PXCMCalibration(IntPtr instance, bool delete)
        : base(instance, delete) {}

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMCalibration_QueryStreamProjectionParameters(IntPtr instance,
        PXCMCapture.StreamType streamType, [Out] StreamCalibration calibration, [Out] StreamTransform transformation);

    public pxcmStatus QueryStreamProjectionParameters(PXCMCapture.StreamType streamType,
        out StreamCalibration calibration, out StreamTransform transformation) {
        calibration = new StreamCalibration();
        transformation = new StreamTransform();
        return PXCMCalibration_QueryStreamProjectionParameters(instance, streamType, calibration, transformation);
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class StreamTransform {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public float[] translation;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)] public float[,] rotation;

        public StreamTransform() {
            translation = new float[3];
            rotation = new float[3, 3];
        }
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class StreamCalibration {
        public PXCMPointF32 focalLength;
        public PXCMPointF32 principalPoint;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public float[] radialDistortion;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] public float[] tangentialDistortion;
        public PXCMCapture.DeviceModel model;

        public StreamCalibration() {
            focalLength = new PXCMPointF32();
            principalPoint = new PXCMPointF32();
            radialDistortion = new float[3];
            tangentialDistortion = new float[2];
            model = PXCMCapture.DeviceModel.DEVICE_MODEL_GENERIC;
        }
    }
}