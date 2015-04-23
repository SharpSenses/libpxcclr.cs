using System;
using System.Runtime.InteropServices;

public class PXCMCapture : PXCMBase {
    public new const int CUID = -2080953776;
    public const int STREAM_LIMIT = 8;

    internal PXCMCapture(IntPtr instance, bool delete)
        : base(instance, delete) {
    }

    [DllImport("libpxccpp2c")]
    internal static extern int PXCMCapture_QueryDeviceNum(IntPtr capture);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMCapture_QueryDeviceInfo(IntPtr capture, int didx, [Out] DeviceInfo dinfo);

    public pxcmStatus QueryDeviceInfo(int didx, out DeviceInfo dinfo) {
        dinfo = new DeviceInfo();
        return PXCMCapture_QueryDeviceInfo(instance, didx, dinfo);
    }

    [DllImport("libpxccpp2c")]
    internal static extern IntPtr PXCMCapture_CreateDevice(IntPtr capture, int didx);

    public static string StreamTypeToString(StreamType stream) {
        switch (stream) {
            case StreamType.STREAM_TYPE_COLOR:
                return "Color";
            case StreamType.STREAM_TYPE_DEPTH:
                return "Depth";
            case StreamType.STREAM_TYPE_IR:
                return "IR";
            case StreamType.STREAM_TYPE_LEFT:
                return "Left";
            case StreamType.STREAM_TYPE_RIGHT:
                return "Right";
            default:
                return "Unknown";
        }
    }

    public static StreamType StreamTypeFromIndex(int index) {
        if (index < 0 || index >= 8)
            return StreamType.STREAM_TYPE_ANY;
        return (StreamType) (1 << index);
    }

    public static int StreamTypeToIndex(StreamType type) {
        int num = 0;
        while (type > StreamType.STREAM_TYPE_COLOR) {
            type = (StreamType) ((int) type >> 1);
            ++num;
        }
        return num;
    }

    public int QueryDeviceNum() {
        return PXCMCapture_QueryDeviceNum(instance);
    }

    public Device CreateDevice(int didx) {
        IntPtr device = PXCMCapture_CreateDevice(instance, didx);
        if (!(device != IntPtr.Zero))
            return null;
        return new Device(device, true);
    }

    public class Device : PXCMBase {
        public new const int CUID = -1820065340;

        internal Device(IntPtr instance, bool delete)
            : base(instance, delete) {
        }

        [DllImport("libpxccpp2c")]
        internal static extern void PXCMCapture_Device_QueryDeviceInfo(IntPtr device, [Out] DeviceInfo dinfo);

        public void QueryDeviceInfo(out DeviceInfo dinfo) {
            dinfo = new DeviceInfo();
            PXCMCapture_Device_QueryDeviceInfo(instance, dinfo);
        }

        [DllImport("libpxccpp2c")]
        internal static extern int PXCMCapture_Device_QueryStreamProfileSetNum(IntPtr device, StreamType scope);

        [DllImport("libpxccpp2c")]
        private static extern pxcmStatus PXCMCapture_Device_QueryStreamProfileSet(IntPtr device, StreamType scope, int index, [Out] StreamProfileSet profiles);

        internal static pxcmStatus QueryStreamProfileSetINT(IntPtr device, StreamType scope, int index, out StreamProfileSet profiles) {
            profiles = new StreamProfileSet();
            return PXCMCapture_Device_QueryStreamProfileSet(device, scope, index, profiles);
        }

        [DllImport("libpxccpp2c")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool PXCMCapture_Device_IsStreamProfileSetValid(IntPtr device, StreamProfileSet profiles);

        [DllImport("libpxccpp2c")]
        internal static extern pxcmStatus PXCMCapture_Device_SetStreamProfileSet(IntPtr device, StreamProfileSet profiles);

        [DllImport("libpxccpp2c")]
        internal static extern pxcmStatus PXCMCapture_Device_QueryProperty(IntPtr device, Property label, out float value);

        [DllImport("libpxccpp2c")]
        private static extern pxcmStatus PXCMCapture_Device_QueryPropertyInfo(IntPtr device, Property label, [Out] PropertyInfo info);

        internal static PropertyInfo QueryPropertyInfoINT(IntPtr device, Property label) {
            PropertyInfo info = new PropertyInfo();
            int num = (int) PXCMCapture_Device_QueryPropertyInfo(device, label, info);
            return info;
        }

        [DllImport("libpxccpp2c")]
        internal static extern pxcmStatus PXCMCapture_Device_QueryPropertyAuto(IntPtr device, Property label, out bool ifauto);

        [DllImport("libpxccpp2c")]
        internal static extern pxcmStatus PXCMCapture_Device_SetPropertyAuto(IntPtr device, Property pty, [MarshalAs(UnmanagedType.Bool)] bool ifauto);

        [DllImport("libpxccpp2c")]
        internal static extern pxcmStatus PXCMCapture_Device_SetProperty(IntPtr device, Property pty, float value);

        [DllImport("libpxccpp2c")]
        internal static extern void PXCMCapture_Device_ResetProperties(IntPtr device, StreamType streams);

        [DllImport("libpxccpp2c")]
        internal static extern void PXCMCapture_Device_RestorePropertiesUponFocus(IntPtr device);

        [DllImport("libpxccpp2c")]
        internal static extern IntPtr PXCMCapture_Device_CreateProjection(IntPtr device);

        [DllImport("libpxccpp2c")]
        internal static extern pxcmStatus PXCMCapture_Device_ReadStreamsAsync(IntPtr device, StreamType scope, [Out] IntPtr[] images, out IntPtr sp);

        public int QueryStreamProfileSetNum(StreamType scope) {
            return PXCMCapture_Device_QueryStreamProfileSetNum(instance, scope);
        }

        public bool IsStreamProfileSetValid(StreamProfileSet profiles) {
            return PXCMCapture_Device_IsStreamProfileSetValid(instance, profiles);
        }

        public pxcmStatus QueryStreamProfileSet(StreamType scope, int index, out StreamProfileSet profiles) {
            return QueryStreamProfileSetINT(instance, scope, index, out profiles);
        }

        public pxcmStatus QueryStreamProfileSet(out StreamProfileSet profiles) {
            return QueryStreamProfileSet(StreamType.STREAM_TYPE_ANY, -1, out profiles);
        }

        public pxcmStatus SetStreamProfileSet(StreamProfileSet profiles) {
            return PXCMCapture_Device_SetStreamProfileSet(instance, profiles);
        }

        protected pxcmStatus QueryProperty(Property label, out float value) {
            return PXCMCapture_Device_QueryProperty(instance, label, out value);
        }

        protected pxcmStatus QueryPropertyAuto(Property label, out bool ifauto) {
            return PXCMCapture_Device_QueryPropertyAuto(instance, label, out ifauto);
        }

        protected pxcmStatus SetPropertyAuto(Property label, bool auto) {
            return PXCMCapture_Device_SetPropertyAuto(instance, label, auto);
        }

        protected pxcmStatus SetProperty(Property label, float value) {
            return PXCMCapture_Device_SetProperty(instance, label, value);
        }

        public void ResetProperties(StreamType streams) {
            PXCMCapture_Device_ResetProperties(instance, streams);
        }

        public void RestorePropertiesUponFocus() {
            PXCMCapture_Device_RestorePropertiesUponFocus(instance);
        }

        public int QueryColorExposure() {
            float num1 = 0.0f;
            int num2 = (int) QueryProperty(Property.PROPERTY_COLOR_EXPOSURE, out num1);
            return (int) num1;
        }

        public PropertyInfo QueryColorExposureInfo() {
            return QueryPropertyInfoINT(instance, Property.PROPERTY_COLOR_EXPOSURE);
        }

        public pxcmStatus SetColorExposure(int value) {
            return SetProperty(Property.PROPERTY_COLOR_EXPOSURE, value);
        }

        public pxcmStatus SetColorAutoExposure(bool auto1) {
            return SetPropertyAuto(Property.PROPERTY_COLOR_EXPOSURE, auto1);
        }

        public bool QueryColorAutoExposure() {
            bool ifauto = false;
            int num = (int) QueryPropertyAuto(Property.PROPERTY_COLOR_EXPOSURE, out ifauto);
            return ifauto;
        }

        public int QueryColorBrightness() {
            float num1 = 0.0f;
            int num2 = (int) QueryProperty(Property.PROPERTY_COLOR_BRIGHTNESS, out num1);
            return (int) num1;
        }

        public PropertyInfo QueryColorBrightnessInfo() {
            return QueryPropertyInfoINT(instance, Property.PROPERTY_COLOR_BRIGHTNESS);
        }

        public pxcmStatus SetColorBrightness(int value) {
            return SetProperty(Property.PROPERTY_COLOR_BRIGHTNESS, value);
        }

        public int QueryColorContrast() {
            float num1 = 0.0f;
            int num2 = (int) QueryProperty(Property.PROPERTY_COLOR_CONTRAST, out num1);
            return (int) num1;
        }

        public PropertyInfo QueryColorContrastInfo() {
            return QueryPropertyInfoINT(instance, Property.PROPERTY_COLOR_CONTRAST);
        }

        public pxcmStatus SetColorContrast(int value) {
            return SetProperty(Property.PROPERTY_COLOR_CONTRAST, value);
        }

        public int QueryColorSaturation() {
            float num1 = 0.0f;
            int num2 = (int) QueryProperty(Property.PROPERTY_COLOR_SATURATION, out num1);
            return (int) num1;
        }

        public PropertyInfo QueryColorSaturationInfo() {
            return QueryPropertyInfoINT(instance, Property.PROPERTY_COLOR_SATURATION);
        }

        public pxcmStatus SetColorSaturation(int value) {
            return SetProperty(Property.PROPERTY_COLOR_SATURATION, value);
        }

        public int QueryColorHue() {
            float num1 = 0.0f;
            int num2 = (int) QueryProperty(Property.PROPERTY_COLOR_HUE, out num1);
            return (int) num1;
        }

        public PropertyInfo QueryColorHueInfo() {
            return QueryPropertyInfoINT(instance, Property.PROPERTY_COLOR_HUE);
        }

        public pxcmStatus SetColorHue(int value) {
            return SetProperty(Property.PROPERTY_COLOR_HUE, value);
        }

        public int QueryColorGamma() {
            float num1 = 0.0f;
            int num2 = (int) QueryProperty(Property.PROPERTY_COLOR_GAMMA, out num1);
            return (int) num1;
        }

        public PropertyInfo QueryColorGammaInfo() {
            return QueryPropertyInfoINT(instance, Property.PROPERTY_COLOR_GAMMA);
        }

        public pxcmStatus SetColorGamma(int value) {
            return SetProperty(Property.PROPERTY_COLOR_GAMMA, value);
        }

        public int QueryColorWhiteBalance() {
            float num1 = 0.0f;
            int num2 = (int) QueryProperty(Property.PROPERTY_COLOR_WHITE_BALANCE, out num1);
            return (int) num1;
        }

        public PropertyInfo QueryColorWhiteBalanceInfo() {
            return QueryPropertyInfoINT(instance, Property.PROPERTY_COLOR_WHITE_BALANCE);
        }

        public pxcmStatus SetColorWhiteBalance(int value) {
            return SetProperty(Property.PROPERTY_COLOR_WHITE_BALANCE, value);
        }

        public bool QueryColorAutoWhiteBalance() {
            bool ifauto = false;
            int num = (int) QueryPropertyAuto(Property.PROPERTY_COLOR_WHITE_BALANCE, out ifauto);
            return ifauto;
        }

        public pxcmStatus SetColorAutoWhiteBalance(bool auto1) {
            return SetPropertyAuto(Property.PROPERTY_COLOR_WHITE_BALANCE, auto1);
        }

        public int QueryColorSharpness() {
            float num1 = 0.0f;
            int num2 = (int) QueryProperty(Property.PROPERTY_COLOR_SHARPNESS, out num1);
            return (int) num1;
        }

        public PropertyInfo QueryColorSharpnessInfo() {
            return QueryPropertyInfoINT(instance, Property.PROPERTY_COLOR_SHARPNESS);
        }

        public pxcmStatus SetColorSharpness(int value) {
            return SetProperty(Property.PROPERTY_COLOR_SHARPNESS, value);
        }

        public int QueryColorBackLightCompensation() {
            float num1 = 0.0f;
            int num2 = (int) QueryProperty(Property.PROPERTY_COLOR_BACK_LIGHT_COMPENSATION, out num1);
            return (int) num1;
        }

        public PropertyInfo QueryColorBackLightCompensationInfo() {
            return QueryPropertyInfoINT(instance, Property.PROPERTY_COLOR_BACK_LIGHT_COMPENSATION);
        }

        public pxcmStatus SetColorBackLightCompensation(int value) {
            return SetProperty(Property.PROPERTY_COLOR_BACK_LIGHT_COMPENSATION, value);
        }

        public int QueryColorGain() {
            float num1 = 0.0f;
            int num2 = (int) QueryProperty(Property.PROPERTY_COLOR_GAIN, out num1);
            return (int) num1;
        }

        public PropertyInfo QueryColorGainInfo() {
            return QueryPropertyInfoINT(instance, Property.PROPERTY_COLOR_GAIN);
        }

        public pxcmStatus SetColorGain(int value) {
            return SetProperty(Property.PROPERTY_COLOR_GAIN, value);
        }

        public PowerLineFrequency QueryColorPowerLineFrequency() {
            float num1 = 0.0f;
            int num2 = (int) QueryProperty(Property.PROPERTY_COLOR_POWER_LINE_FREQUENCY, out num1);
            return (PowerLineFrequency) num1;
        }

        public pxcmStatus SetColorPowerLineFrequency(PowerLineFrequency value) {
            return SetProperty(Property.PROPERTY_COLOR_POWER_LINE_FREQUENCY, (float) value);
        }

        public PowerLineFrequency QueryColorPowerLineFrequencyDefaultValue() {
            return (PowerLineFrequency) QueryPropertyInfoINT(instance, Property.PROPERTY_COLOR_POWER_LINE_FREQUENCY).defaultValue;
        }

        public bool QueryColorAutoPowerLineFrequency() {
            bool ifauto = false;
            int num = (int) QueryPropertyAuto(Property.PROPERTY_COLOR_POWER_LINE_FREQUENCY, out ifauto);
            return ifauto;
        }

        public pxcmStatus SetColorAutoPowerLineFrequency(bool auto1) {
            return SetPropertyAuto(Property.PROPERTY_COLOR_POWER_LINE_FREQUENCY, auto1);
        }

        public PXCMPointF32 QueryColorFieldOfView() {
            PXCMPointF32 pxcmPointF32 = new PXCMPointF32();
            int num1 = (int) QueryProperty(Property.PROPERTY_COLOR_FIELD_OF_VIEW, out pxcmPointF32.x);
            int num2 = (int) QueryProperty(Property.PROPERTY_COLOR_FIELD_OF_VIEW | Property.PROPERTY_COLOR_EXPOSURE, out pxcmPointF32.y);
            return pxcmPointF32;
        }

        public PXCMPointF32 QueryColorFocalLength() {
            PXCMPointF32 pxcmPointF32 = new PXCMPointF32();
            int num1 = (int) QueryProperty(Property.PROPERTY_COLOR_FOCAL_LENGTH, out pxcmPointF32.x);
            int num2 = (int) QueryProperty(Property.PROPERTY_COLOR_FOCAL_LENGTH | Property.PROPERTY_COLOR_EXPOSURE, out pxcmPointF32.y);
            return pxcmPointF32;
        }

        public float QueryColorFocalLengthMM() {
            float num1 = 0.0f;
            int num2 = (int) QueryProperty(Property.PROPERTY_COLOR_FOCAL_LENGTH_MM, out num1);
            return num1;
        }

        public PXCMPointF32 QueryColorPrincipalPoint() {
            PXCMPointF32 pxcmPointF32 = new PXCMPointF32();
            int num1 = (int) QueryProperty(Property.PROPERTY_COLOR_PRINCIPAL_POINT, out pxcmPointF32.x);
            int num2 = (int) QueryProperty(Property.PROPERTY_COLOR_PRINCIPAL_POINT | Property.PROPERTY_COLOR_EXPOSURE, out pxcmPointF32.y);
            return pxcmPointF32;
        }

        [CLSCompliant(false)]
        public ushort QueryDepthLowConfidenceValue() {
            return 0;
        }

        [CLSCompliant(false)]
        public ushort QueryDepthConfidenceThreshold() {
            float num1 = 0.0f;
            int num2 = (int) QueryProperty(Property.PROPERTY_DEPTH_CONFIDENCE_THRESHOLD, out num1);
            return (ushort) num1;
        }

        public PropertyInfo QueryDepthConfidenceThresholdInfo() {
            return QueryPropertyInfoINT(instance, Property.PROPERTY_DEPTH_CONFIDENCE_THRESHOLD);
        }

        [CLSCompliant(false)]
        public pxcmStatus SetDepthConfidenceThreshold(ushort value) {
            return SetProperty(Property.PROPERTY_DEPTH_CONFIDENCE_THRESHOLD, value);
        }

        public float QueryDepthUnit() {
            float num1 = 0.0f;
            int num2 = (int) QueryProperty(Property.PROPERTY_DEPTH_UNIT, out num1);
            return num1;
        }

        public pxcmStatus SetDepthUnit(int value) {
            return SetProperty(Property.PROPERTY_DEPTH_UNIT, value);
        }

        public PXCMPointF32 QueryDepthFieldOfView() {
            PXCMPointF32 pxcmPointF32 = new PXCMPointF32();
            int num1 = (int) QueryProperty(Property.PROPERTY_DEPTH_FIELD_OF_VIEW, out pxcmPointF32.x);
            int num2 = (int) QueryProperty(Property.PROPERTY_DEPTH_FIELD_OF_VIEW | Property.PROPERTY_COLOR_EXPOSURE, out pxcmPointF32.y);
            return pxcmPointF32;
        }

        public PXCMRangeF32 QueryDepthSensorRange() {
            PXCMRangeF32 pxcmRangeF32 = new PXCMRangeF32();
            int num1 = (int) QueryProperty(Property.PROPERTY_DEPTH_SENSOR_RANGE, out pxcmRangeF32.min);
            int num2 = (int) QueryProperty(Property.PROPERTY_DEPTH_SENSOR_RANGE | Property.PROPERTY_COLOR_EXPOSURE, out pxcmRangeF32.max);
            return pxcmRangeF32;
        }

        public PXCMPointF32 QueryDepthFocalLength() {
            PXCMPointF32 pxcmPointF32 = new PXCMPointF32();
            int num1 = (int) QueryProperty(Property.PROPERTY_DEPTH_FOCAL_LENGTH, out pxcmPointF32.x);
            int num2 = (int) QueryProperty(Property.PROPERTY_DEPTH_FOCAL_LENGTH | Property.PROPERTY_COLOR_EXPOSURE, out pxcmPointF32.y);
            return pxcmPointF32;
        }

        public float QueryDepthFocalLengthMM() {
            float num1 = 0.0f;
            int num2 = (int) QueryProperty(Property.PROPERTY_DEPTH_FOCAL_LENGTH_MM, out num1);
            return num1;
        }

        public PXCMPointF32 QueryDepthPrincipalPoint() {
            PXCMPointF32 pxcmPointF32 = new PXCMPointF32();
            int num1 = (int) QueryProperty(Property.PROPERTY_DEPTH_PRINCIPAL_POINT, out pxcmPointF32.x);
            int num2 = (int) QueryProperty(Property.PROPERTY_DEPTH_PRINCIPAL_POINT | Property.PROPERTY_COLOR_EXPOSURE, out pxcmPointF32.y);
            return pxcmPointF32;
        }

        public bool QueryDeviceAllowProfileChange() {
            float num1 = 0.0f;
            int num2 = (int) QueryProperty(Property.PROPERTY_DEVICE_ALLOW_PROFILE_CHANGE, out num1);
            return num1 != 0.0;
        }

        public pxcmStatus SetDeviceAllowProfileChange(bool value) {
            return SetProperty(Property.PROPERTY_DEVICE_ALLOW_PROFILE_CHANGE, value ? 1f : 0.0f);
        }

        public MirrorMode QueryMirrorMode() {
            float num1 = 0.0f;
            int num2 = (int) QueryProperty(Property.PROPERTY_DEVICE_MIRROR, out num1);
            return (MirrorMode) num1;
        }

        public pxcmStatus SetMirrorMode(MirrorMode value) {
            return SetProperty(Property.PROPERTY_DEVICE_MIRROR, (float) value);
        }

        public int QueryIVCAMLaserPower() {
            float num1 = 0.0f;
            int num2 = (int) QueryProperty(Property.PROPERTY_IVCAM_LASER_POWER, out num1);
            return (int) num1;
        }

        public PropertyInfo QueryIVCAMLaserPowerInfo() {
            return QueryPropertyInfoINT(instance, Property.PROPERTY_IVCAM_LASER_POWER);
        }

        public pxcmStatus SetIVCAMLaserPower(int value) {
            return SetProperty(Property.PROPERTY_IVCAM_LASER_POWER, value);
        }

        public IVCAMAccuracy QueryIVCAMAccuracy() {
            float num1 = 0.0f;
            int num2 = (int) QueryProperty(Property.PROPERTY_IVCAM_ACCURACY, out num1);
            return (IVCAMAccuracy) num1;
        }

        public pxcmStatus SetIVCAMAccuracy(IVCAMAccuracy value) {
            return SetProperty(Property.PROPERTY_IVCAM_ACCURACY, (float) value);
        }

        public IVCAMAccuracy QueryIVCAMAccuracyDefaultValue() {
            return (IVCAMAccuracy) QueryPropertyInfoINT(instance, Property.PROPERTY_IVCAM_ACCURACY).defaultValue;
        }

        public int QueryIVCAMFilterOption() {
            float num1;
            if (QueryProperty(Property.PROPERTY_IVCAM_FILTER_OPTION, out num1) < pxcmStatus.PXCM_STATUS_NO_ERROR) {
                int num2 = (int) QueryProperty(Property.PROPERTY_COLOR_GAMMA | Property.PROPERTY_CUSTOMIZED, out num1);
            }
            return (int) num1;
        }

        public PropertyInfo QueryIVCAMFilterOptionInfo() {
            PropertyInfo propertyInfo = QueryPropertyInfoINT(instance, Property.PROPERTY_IVCAM_FILTER_OPTION);
            if (propertyInfo.range.max > 0.0)
                return propertyInfo;
            return QueryPropertyInfoINT(instance, Property.PROPERTY_COLOR_GAMMA | Property.PROPERTY_CUSTOMIZED);
        }

        public pxcmStatus SetIVCAMFilterOption(int value) {
            pxcmStatus pxcmStatus = SetProperty(Property.PROPERTY_IVCAM_FILTER_OPTION, value);
            if (pxcmStatus < pxcmStatus.PXCM_STATUS_NO_ERROR)
                pxcmStatus = SetProperty(Property.PROPERTY_COLOR_GAMMA | Property.PROPERTY_CUSTOMIZED, value);
            return pxcmStatus;
        }

        public int QueryIVCAMMotionRangeTradeOff() {
            float num1;
            if (QueryProperty(Property.PROPERTY_IVCAM_MOTION_RANGE_TRADE_OFF, out num1) < pxcmStatus.PXCM_STATUS_NO_ERROR) {
                int num2 = (int) QueryProperty(Property.PROPERTY_COLOR_SATURATION | Property.PROPERTY_CUSTOMIZED, out num1);
            }
            return (int) num1;
        }

        public PropertyInfo QueryIVCAMMotionRangeTradeOffInfo() {
            PropertyInfo propertyInfo = QueryPropertyInfoINT(instance, Property.PROPERTY_IVCAM_MOTION_RANGE_TRADE_OFF);
            if (propertyInfo.range.max > 0.0)
                return propertyInfo;
            return QueryPropertyInfoINT(instance, Property.PROPERTY_COLOR_SATURATION | Property.PROPERTY_CUSTOMIZED);
        }

        public pxcmStatus SetIVCAMMotionRangeTradeOff(int value) {
            pxcmStatus pxcmStatus = SetProperty(Property.PROPERTY_IVCAM_MOTION_RANGE_TRADE_OFF, value);
            if (pxcmStatus < pxcmStatus.PXCM_STATUS_NO_ERROR)
                pxcmStatus = SetProperty(Property.PROPERTY_COLOR_SATURATION | Property.PROPERTY_CUSTOMIZED, value);
            return pxcmStatus;
        }

        public bool QueryDSLeftRightCropping() {
            float num1 = 0.0f;
            int num2 = (int) QueryProperty(Property.PROPERTY_DS_CROP, out num1);
            return num1 != 0.0;
        }

        public pxcmStatus SetDSLeftRightCropping(bool value) {
            return SetProperty(Property.PROPERTY_DS_CROP, value ? 1f : 0.0f);
        }

        public bool QueryDSEmitterEnabled() {
            float num1 = 0.0f;
            int num2 = (int) QueryProperty(Property.PROPERTY_DS_EMITTER, out num1);
            return num1 != 0.0;
        }

        public pxcmStatus SetDSEnableEmitter(bool value) {
            return SetProperty(Property.PROPERTY_DS_EMITTER, value ? 1f : 0.0f);
        }

        public float QueryDSTemperature() {
            float num1 = 0.0f;
            int num2 = (int) QueryProperty(Property.PROPERTY_DS_TEMPERATURE, out num1);
            return num1;
        }

        public bool QueryDSDisparityOutputEnabled() {
            float num1 = 0.0f;
            int num2 = (int) QueryProperty(Property.PROPERTY_DS_DISPARITY_OUTPUT, out num1);
            return num1 != 0.0;
        }

        public pxcmStatus SetDSEnableDisparityOutput(bool value) {
            return SetProperty(Property.PROPERTY_DS_DISPARITY_OUTPUT, value ? 1f : 0.0f);
        }

        public int QueryDSDisparityMultiplier() {
            float num1 = 0.0f;
            int num2 = (int) QueryProperty(Property.PROPERTY_DS_DISPARITY_MULTIPLIER, out num1);
            return (int) num1;
        }

        public pxcmStatus SetDSDisparityMultiplier(int value) {
            return SetProperty(Property.PROPERTY_DS_DISPARITY_MULTIPLIER, value);
        }

        public int QueryDSDisparityShift() {
            float num1 = 0.0f;
            int num2 = (int) QueryProperty(Property.PROPERTY_DS_DISPARITY_SHIFT, out num1);
            return (int) num1;
        }

        public pxcmStatus SetDSDisparityShift(int value) {
            return SetProperty(Property.PROPERTY_DS_DISPARITY_SHIFT, value);
        }

        public PXCMRangeF32 QueryDSMinMaxZ() {
            PXCMRangeF32 pxcmRangeF32;
            pxcmRangeF32.min = pxcmRangeF32.max = 0.0f;
            int num1 = (int) QueryProperty(Property.PROPERTY_DS_MIN_MAX_Z, out pxcmRangeF32.min);
            int num2 = (int) QueryProperty(Property.PROPERTY_COLOR_WHITE_BALANCE | Property.PROPERTY_DS_CROP, out pxcmRangeF32.max);
            return pxcmRangeF32;
        }

        public pxcmStatus SetDSMinMaxZ(PXCMRangeF32 value) {
            pxcmStatus pxcmStatus = SetProperty(Property.PROPERTY_DS_MIN_MAX_Z, value.min);
            if (pxcmStatus != pxcmStatus.PXCM_STATUS_NO_ERROR)
                pxcmStatus = SetProperty(Property.PROPERTY_COLOR_WHITE_BALANCE | Property.PROPERTY_DS_CROP, value.max);
            return pxcmStatus;
        }

        public bool QueryDSColorRectificationEnabled() {
            float num1 = 0.0f;
            int num2 = (int) QueryProperty(Property.PROPERTY_DS_COLOR_RECTIFICATION, out num1);
            return num1 != 0.0;
        }

        public bool QueryDSDepthRectificationEnabled() {
            float num1 = 0.0f;
            int num2 = (int) QueryProperty(Property.PROPERTY_DS_DEPTH_RECTIFICATION, out num1);
            return num1 != 0.0;
        }

        public float QueryDSLeftRightExposure() {
            float num1 = 0.0f;
            int num2 = (int) QueryProperty(Property.PROPERTY_DS_LEFTRIGHT_EXPOSURE, out num1);
            return num1;
        }

        public PropertyInfo QueryDSLeftRightExposureInfo() {
            return QueryPropertyInfoINT(instance, Property.PROPERTY_DS_LEFTRIGHT_EXPOSURE);
        }

        public pxcmStatus SetDSLeftRightExposure(float value) {
            return SetProperty(Property.PROPERTY_DS_LEFTRIGHT_EXPOSURE, value);
        }

        public pxcmStatus SetDSLeftRightAutoExposure(bool auto1) {
            return SetPropertyAuto(Property.PROPERTY_DS_LEFTRIGHT_EXPOSURE, auto1);
        }

        public int QueryDSLeftRightGain() {
            float num1 = 0.0f;
            int num2 = (int) QueryProperty(Property.PROPERTY_DS_LEFTRIGHT_GAIN, out num1);
            return (int) num1;
        }

        public PropertyInfo QueryDSLeftRightGainInfo() {
            return QueryPropertyInfoINT(instance, Property.PROPERTY_DS_LEFTRIGHT_GAIN);
        }

        public pxcmStatus SetDSLeftRightGain(int value) {
            return SetProperty(Property.PROPERTY_DS_LEFTRIGHT_GAIN, value);
        }

        public pxcmStatus ReadStreamsAsync(StreamType scope, Sample sample, out PXCMSyncPoint sp) {
            IntPtr sp1;
            pxcmStatus pxcmStatus = PXCMCapture_Device_ReadStreamsAsync(instance, scope, sample.images, out sp1);
            sp = pxcmStatus >= pxcmStatus.PXCM_STATUS_NO_ERROR ? new PXCMSyncPoint(sp1, true) : null;
            return pxcmStatus;
        }

        public pxcmStatus ReadStreamsAsync(Sample sample, out PXCMSyncPoint sp) {
            return ReadStreamsAsync(StreamType.STREAM_TYPE_ANY, sample, out sp);
        }

        public pxcmStatus ReadStreams(StreamType scope, Sample sample) {
            PXCMSyncPoint sp;
            pxcmStatus pxcmStatus1 = ReadStreamsAsync(scope, sample, out sp);
            if (pxcmStatus1 < pxcmStatus.PXCM_STATUS_NO_ERROR)
                return pxcmStatus1;
            pxcmStatus pxcmStatus2 = sp.Synchronize();
            sp.Dispose();
            return pxcmStatus2;
        }

        public PXCMProjection CreateProjection() {
            IntPtr projection = PXCMCapture_Device_CreateProjection(instance);
            if (!(projection == IntPtr.Zero))
                return new PXCMProjection(projection, true);
            return null;
        }

        public enum PowerLineFrequency {
            POWER_LINE_FREQUENCY_DISABLED = 0,
            POWER_LINE_FREQUENCY_50HZ = 50,
            POWER_LINE_FREQUENCY_60HZ = 60
        }

        public enum MirrorMode {
            MIRROR_MODE_DISABLED,
            MIRROR_MODE_HORIZONTAL
        }

        public enum IVCAMAccuracy {
            IVCAM_ACCURACY_FINEST = 1,
            IVCAM_ACCURACY_MEDIAN = 2,
            IVCAM_ACCURACY_COARSE = 3
        }

        [Flags]
        public enum Property {
            PROPERTY_COLOR_EXPOSURE = 1,
            PROPERTY_COLOR_BRIGHTNESS = 2,
            PROPERTY_COLOR_CONTRAST = PROPERTY_COLOR_BRIGHTNESS | PROPERTY_COLOR_EXPOSURE,
            PROPERTY_COLOR_SATURATION = 4,
            PROPERTY_COLOR_HUE = PROPERTY_COLOR_SATURATION | PROPERTY_COLOR_EXPOSURE,
            PROPERTY_COLOR_GAMMA = PROPERTY_COLOR_SATURATION | PROPERTY_COLOR_BRIGHTNESS,
            PROPERTY_COLOR_WHITE_BALANCE = PROPERTY_COLOR_GAMMA | PROPERTY_COLOR_EXPOSURE,
            PROPERTY_COLOR_SHARPNESS = 8,
            PROPERTY_COLOR_BACK_LIGHT_COMPENSATION = PROPERTY_COLOR_SHARPNESS | PROPERTY_COLOR_EXPOSURE,
            PROPERTY_COLOR_GAIN = PROPERTY_COLOR_SHARPNESS | PROPERTY_COLOR_BRIGHTNESS,
            PROPERTY_COLOR_POWER_LINE_FREQUENCY = PROPERTY_COLOR_GAIN | PROPERTY_COLOR_EXPOSURE,
            PROPERTY_COLOR_FOCAL_LENGTH_MM = PROPERTY_COLOR_SHARPNESS | PROPERTY_COLOR_SATURATION,
            PROPERTY_COLOR_FIELD_OF_VIEW = 1000,
            PROPERTY_COLOR_FOCAL_LENGTH = PROPERTY_COLOR_FIELD_OF_VIEW | PROPERTY_COLOR_GAMMA,
            PROPERTY_COLOR_PRINCIPAL_POINT = 1008,
            PROPERTY_DEPTH_SATURATION_VALUE = 200,
            PROPERTY_DEPTH_LOW_CONFIDENCE_VALUE = PROPERTY_DEPTH_SATURATION_VALUE | PROPERTY_COLOR_EXPOSURE,
            PROPERTY_DEPTH_CONFIDENCE_THRESHOLD = PROPERTY_DEPTH_SATURATION_VALUE | PROPERTY_COLOR_BRIGHTNESS,
            PROPERTY_DEPTH_UNIT = PROPERTY_DEPTH_SATURATION_VALUE | PROPERTY_COLOR_SATURATION,
            PROPERTY_DEPTH_FOCAL_LENGTH_MM = PROPERTY_DEPTH_UNIT | PROPERTY_COLOR_EXPOSURE,
            PROPERTY_DEPTH_FIELD_OF_VIEW = 2000,
            PROPERTY_DEPTH_SENSOR_RANGE = PROPERTY_DEPTH_FIELD_OF_VIEW | PROPERTY_COLOR_BRIGHTNESS,
            PROPERTY_DEPTH_FOCAL_LENGTH = PROPERTY_DEPTH_SENSOR_RANGE | PROPERTY_COLOR_SATURATION,
            PROPERTY_DEPTH_PRINCIPAL_POINT = PROPERTY_DEPTH_FIELD_OF_VIEW | PROPERTY_COLOR_SHARPNESS,
            PROPERTY_DEVICE_ALLOW_PROFILE_CHANGE = 302,
            PROPERTY_DEVICE_MIRROR = 304,
            PROPERTY_PROJECTION_SERIALIZABLE = 3003,
            PROPERTY_IVCAM_LASER_POWER = 65536,
            PROPERTY_IVCAM_ACCURACY = PROPERTY_IVCAM_LASER_POWER | PROPERTY_COLOR_EXPOSURE,
            PROPERTY_IVCAM_FILTER_OPTION = PROPERTY_IVCAM_ACCURACY | PROPERTY_COLOR_BRIGHTNESS,
            PROPERTY_IVCAM_MOTION_RANGE_TRADE_OFF = PROPERTY_IVCAM_LASER_POWER | PROPERTY_COLOR_SATURATION,
            PROPERTY_DS_CROP = 131072,
            PROPERTY_DS_EMITTER = PROPERTY_DS_CROP | PROPERTY_COLOR_EXPOSURE,
            PROPERTY_DS_TEMPERATURE = PROPERTY_DS_CROP | PROPERTY_COLOR_BRIGHTNESS,
            PROPERTY_DS_DISPARITY_OUTPUT = PROPERTY_DS_TEMPERATURE | PROPERTY_COLOR_EXPOSURE,
            PROPERTY_DS_DISPARITY_MULTIPLIER = PROPERTY_DS_CROP | PROPERTY_COLOR_SATURATION,
            PROPERTY_DS_DISPARITY_SHIFT = PROPERTY_DS_DISPARITY_MULTIPLIER | PROPERTY_COLOR_EXPOSURE,
            PROPERTY_DS_MIN_MAX_Z = PROPERTY_DS_DISPARITY_MULTIPLIER | PROPERTY_COLOR_BRIGHTNESS,
            PROPERTY_DS_COLOR_RECTIFICATION = PROPERTY_DS_CROP | PROPERTY_COLOR_SHARPNESS,
            PROPERTY_DS_DEPTH_RECTIFICATION = PROPERTY_DS_COLOR_RECTIFICATION | PROPERTY_COLOR_EXPOSURE,
            PROPERTY_DS_LEFTRIGHT_EXPOSURE = PROPERTY_DS_COLOR_RECTIFICATION | PROPERTY_COLOR_BRIGHTNESS,
            PROPERTY_DS_LEFTRIGHT_GAIN = PROPERTY_DS_LEFTRIGHT_EXPOSURE | PROPERTY_COLOR_EXPOSURE,
            PROPERTY_CUSTOMIZED = 67108864
        }

        [Flags]
        public enum StreamOption {
            STREAM_OPTION_ANY = 0,
            STREAM_OPTION_DEPTH_PRECALCULATE_UVMAP = 1,
            STREAM_OPTION_STRONG_STREAM_SYNC = 2
        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public class StreamProfile {
            public PXCMImage.ImageInfo imageInfo;
            public PXCMRangeF32 frameRate;
            public StreamOption options;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            internal int[] reserved;

            public StreamProfile() {
                imageInfo = new PXCMImage.ImageInfo();
                reserved = new int[5];
            }
        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, Size = 384)]
        public class StreamProfileSet {
            public StreamProfile color;
            public StreamProfile depth;
            public StreamProfile ir;
            public StreamProfile left;
            public StreamProfile right;
            internal StreamProfile reserved1;
            internal StreamProfile reserved2;
            internal StreamProfile reserved3;

            public StreamProfile this[StreamType type] {
                get {
                    if (type == StreamType.STREAM_TYPE_COLOR)
                        return color;
                    if (type == StreamType.STREAM_TYPE_DEPTH)
                        return depth;
                    if (type == StreamType.STREAM_TYPE_IR)
                        return ir;
                    if (type == StreamType.STREAM_TYPE_LEFT)
                        return left;
                    if (type == StreamType.STREAM_TYPE_RIGHT)
                        return right;
                    if (type == StreamTypeFromIndex(5))
                        return reserved1;
                    if (type == StreamTypeFromIndex(6))
                        return reserved2;
                    if (type == StreamTypeFromIndex(7))
                        return reserved3;
                    return null;
                }
                set {
                    if (type == StreamType.STREAM_TYPE_COLOR)
                        color = value;
                    if (type == StreamType.STREAM_TYPE_DEPTH)
                        depth = value;
                    if (type == StreamType.STREAM_TYPE_IR)
                        ir = value;
                    if (type == StreamType.STREAM_TYPE_LEFT)
                        left = value;
                    if (type == StreamType.STREAM_TYPE_RIGHT)
                        right = value;
                    if (type == StreamTypeFromIndex(5))
                        reserved1 = value;
                    if (type == StreamTypeFromIndex(6))
                        reserved2 = value;
                    if (type != StreamTypeFromIndex(7))
                        return;
                    reserved3 = value;
                }
            }

            public StreamProfileSet() {
                color = new StreamProfile();
                depth = new StreamProfile();
                ir = new StreamProfile();
                left = new StreamProfile();
                right = new StreamProfile();
                reserved1 = new StreamProfile();
                reserved2 = new StreamProfile();
                reserved3 = new StreamProfile();
            }

            internal StreamProfileSet(StreamProfile[] streams) {
                color = streams[0];
                depth = streams[1];
                ir = streams[2];
                left = streams[3];
                right = streams[4];
                reserved1 = streams[5];
                reserved2 = streams[6];
                reserved3 = streams[7];
            }

            internal StreamProfile[] ToStreamProfileArray() {
                return new StreamProfile[8]
        {
          color,
          depth,
          ir,
          left,
          right,
          reserved1,
          reserved2,
          reserved3
        };
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public class PropertyInfo {
            public PXCMRangeF32 range;
            public float step;
            public float defaultValue;
            [MarshalAs(UnmanagedType.Bool)]
            public bool automatic;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            internal int[] reserved;

            public PropertyInfo() {
                reserved = new int[11];
            }
        }
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class Sample {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        internal IntPtr[] images;

        public PXCMImage color {
            get {
                if (!(images[0] != IntPtr.Zero))
                    return null;
                return new PXCMImage(images[0], false);
            }
            set {
                images[0] = value != null ? value.instance : IntPtr.Zero;
            }
        }

        public PXCMImage depth {
            get {
                if (!(images[1] != IntPtr.Zero))
                    return null;
                return new PXCMImage(images[1], false);
            }
            set {
                images[1] = value != null ? value.instance : IntPtr.Zero;
            }
        }

        public PXCMImage ir {
            get {
                if (!(images[2] != IntPtr.Zero))
                    return null;
                return new PXCMImage(images[2], false);
            }
            set {
                images[2] = value != null ? value.instance : IntPtr.Zero;
            }
        }

        public PXCMImage left {
            get {
                if (!(images[3] != IntPtr.Zero))
                    return null;
                return new PXCMImage(images[3], false);
            }
            set {
                images[3] = value != null ? value.instance : IntPtr.Zero;
            }
        }

        public PXCMImage right {
            get {
                if (!(images[4] != IntPtr.Zero))
                    return null;
                return new PXCMImage(images[4], false);
            }
            set {
                images[4] = value != null ? value.instance : IntPtr.Zero;
            }
        }

        public PXCMImage this[StreamType type] {
            get {
                int index = StreamTypeToIndex(type);
                if (images[index] == IntPtr.Zero)
                    return null;
                return new PXCMImage(images[index], false);
            }
            set {
                images[StreamTypeToIndex(type)] = value != null ? value.instance : IntPtr.Zero;
            }
        }

        internal Sample(IntPtr instance) {
            images = new IntPtr[8];
            Marshal.Copy(instance, images, 0, 8);
        }

        public Sample() {
            images = new IntPtr[8];
        }

        public void ReleaseImages() {
            for (int index = 0; index < 8; ++index) {
                if (!(images[index] == IntPtr.Zero)) {
                    PXCMBase_Release(PXCMBase_QueryInstance(images[index], 0));
                    images[index] = IntPtr.Zero;
                }
            }
        }
    }

    [Flags]
    public enum StreamType {
        STREAM_TYPE_ANY = 0,
        STREAM_TYPE_COLOR = 1,
        STREAM_TYPE_DEPTH = 2,
        STREAM_TYPE_IR = 4,
        STREAM_TYPE_LEFT = 8,
        STREAM_TYPE_RIGHT = 16
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Size = 1120, CharSet = CharSet.Unicode)]
    public class DeviceInfo {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 224)]
        public string name;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string serial;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string did;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public int[] firmware;
        public PXCMPointF32 location;
        public DeviceModel model;
        public DeviceOrientation orientation;
        public StreamType streams;
        public int didx;
        public int duid;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        internal int[] reserved;

        public DeviceInfo() {
            name = did = serial = "";
            reserved = new int[4];
        }

        public int QueryNumStreams() {
            int num1 = 0;
            int num2 = 0;
            int num3 = 1;
            while (num2 < 8) {
                if ((streams & (StreamType) num3) != StreamType.STREAM_TYPE_ANY)
                    ++num1;
                ++num2;
                num3 <<= 1;
            }
            return num1;
        }
    }

    [Flags]
    public enum DeviceModel {
        DEVICE_MODEL_GENERIC = 0,
        DEVICE_MODEL_F200 = 2097166,
        DEVICE_MODEL_IVCAM = DEVICE_MODEL_F200,
        DEVICE_MODEL_R200 = 2097167,
        DEVICE_MODEL_DS4 = DEVICE_MODEL_R200
    }

    [Flags]
    public enum DeviceOrientation {
        DEVICE_ORIENTATION_ANY = 0,
        DEVICE_ORIENTATION_USER_FACING = 1,
        DEVICE_ORIENTATION_FRONT_FACING = DEVICE_ORIENTATION_USER_FACING,
        DEVICE_ORIENTATION_WORLD_FACING = 2,
        DEVICE_ORIENTATION_REAR_FACING = DEVICE_ORIENTATION_WORLD_FACING
    }
}
