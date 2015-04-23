using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class PXCMFaceConfiguration : PXCMBase {
    public new const int CUID = 1195787078;
    internal AllFaceConfigurations configs;
    internal EventMaps maps;

    public DetectionConfiguration detection {
        get {
            return configs.detection;
        }
        set {
            configs.detection = value;
        }
    }

    public LandmarksConfiguration landmarks {
        get {
            return configs.landmarks;
        }
        set {
            configs.landmarks = value;
        }
    }

    public PoseConfiguration pose {
        get {
            return configs.pose;
        }
        set {
            configs.pose = value;
        }
    }

    public TrackingStrategyType strategy {
        get {
            return configs.strategy;
        }
        set {
            configs.strategy = value;
        }
    }

    internal PXCMFaceConfiguration(IntPtr instance, bool delete)
        : base(instance, delete) {
        GetConfigurationsINT(instance, out configs);
        maps = new EventMaps();
    }

    internal PXCMFaceConfiguration(EventMaps maps, IntPtr instance, bool delete)
        : base(instance, delete) {
        GetConfigurationsINT(instance, out configs);
        this.maps = maps;
    }

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMFaceConfiguration_EnableAlert(IntPtr instance, PXCMFaceData.AlertData.AlertType alertEvent);

    [DllImport("libpxccpp2c")]
    internal static extern void PXCMFaceConfiguration_EnableAllAlerts(IntPtr instance);

    [DllImport("libpxccpp2c")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool PXCMFaceConfiguration_IsAlertEnabled(IntPtr instance, PXCMFaceData.AlertData.AlertType alertEvent);

    [DllImport("libpxccpp2c")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool PXCMFaceConfiguration_DisableAlert(IntPtr instance, PXCMFaceData.AlertData.AlertType alertEvent);

    [DllImport("libpxccpp2c")]
    internal static extern void PXCMFaceConfiguration_DisableAllAlerts(IntPtr instance);

    [DllImport("libpxccpp2c")]
    private static extern pxcmStatus PXCMFaceConfiguration_SubscribeAlert(IntPtr instance, IntPtr alertHandler);

    internal static pxcmStatus SubscribeAlertINT(IntPtr instance, OnFiredAlertDelegate handler, out object proxy) {
        AlertHandlerDIR alertHandlerDir = new AlertHandlerDIR(handler);
        pxcmStatus pxcmStatus = PXCMFaceConfiguration_SubscribeAlert(instance, alertHandlerDir.uDIR);
        if (pxcmStatus < pxcmStatus.PXCM_STATUS_NO_ERROR)
            alertHandlerDir.Dispose();
        proxy = alertHandlerDir;
        return pxcmStatus;
    }

    [DllImport("libpxccpp2c")]
    private static extern pxcmStatus PXCMFaceConfiguration_UnsubscribeAlert(IntPtr instance, IntPtr alertHandler);

    internal static pxcmStatus UnsubscribeAlertINT(IntPtr instance, object proxy) {
        AlertHandlerDIR alertHandlerDir = (AlertHandlerDIR) proxy;
        pxcmStatus pxcmStatus = PXCMFaceConfiguration_UnsubscribeAlert(instance, alertHandlerDir.uDIR);
        alertHandlerDir.Dispose();
        return pxcmStatus;
    }

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMFaceConfiguration_ApplyChanges(IntPtr instance, AllFaceConfigurations configs);

    [DllImport("libpxccpp2c")]
    private static extern void PXCMFaceConfiguration_GetConfigurations(IntPtr instance, [Out] AllFaceConfigurations configs);

    internal static void GetConfigurationsINT(IntPtr instance, out AllFaceConfigurations configs) {
        configs = new AllFaceConfigurations();
        PXCMFaceConfiguration_GetConfigurations(instance, configs);
    }

    [DllImport("libpxccpp2c")]
    private static extern void PXCMFaceConfiguration_RestoreDefaults(IntPtr instance, [Out] AllFaceConfigurations configs);

    internal static void RestoreDefaultsINT(IntPtr instance, out AllFaceConfigurations configs) {
        configs = new AllFaceConfigurations();
        PXCMFaceConfiguration_RestoreDefaults(instance, configs);
    }

    [DllImport("libpxccpp2c")]
    private static extern pxcmStatus PXCMFaceConfiguration_Update(IntPtr instance, [Out] AllFaceConfigurations configs);

    internal static pxcmStatus UpdateINT(IntPtr instance, out AllFaceConfigurations configs) {
        configs = new AllFaceConfigurations();
        return PXCMFaceConfiguration_Update(instance, configs);
    }

    [DllImport("libpxccpp2c")]
    internal static extern TrackingModeType PXCMFaceConfiguration_GetTrackingMode(IntPtr instance);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMFaceConfiguration_SetTrackingMode(IntPtr instance, TrackingModeType trackingMode);

    public ExpressionsConfiguration QueryExpressions() {
        if (configs.expressionInstance == IntPtr.Zero)
            return null;
        return new ExpressionsConfiguration(this);
    }

    public RecognitionConfiguration QueryRecognition() {
        if (configs.recognitionInstance == IntPtr.Zero)
            return null;
        return new RecognitionConfiguration(this);
    }

    public PulseConfiguration QueryPulse() {
        if (configs.pulseInsance == IntPtr.Zero)
            return null;
        return new PulseConfiguration(this);
    }

    public pxcmStatus SetTrackingMode(TrackingModeType trackingMode) {
        return PXCMFaceConfiguration_SetTrackingMode(instance, trackingMode);
    }

    public TrackingModeType GetTrackingMode() {
        return PXCMFaceConfiguration_GetTrackingMode(instance);
    }

    public pxcmStatus EnableAlert(PXCMFaceData.AlertData.AlertType alertEvent) {
        return PXCMFaceConfiguration_EnableAlert(instance, alertEvent);
    }

    public void EnableAllAlerts() {
        PXCMFaceConfiguration_EnableAllAlerts(instance);
    }

    public bool IsAlertEnabled(PXCMFaceData.AlertData.AlertType alertEvent) {
        return PXCMFaceConfiguration_IsAlertEnabled(instance, alertEvent);
    }

    public bool DisableAlert(PXCMFaceData.AlertData.AlertType alertEvent) {
        return PXCMFaceConfiguration_DisableAlert(instance, alertEvent);
    }

    public void DisableAllAlerts() {
        PXCMFaceConfiguration_DisableAllAlerts(instance);
    }

    public pxcmStatus SubscribeAlert(OnFiredAlertDelegate handler) {
        if (handler == null)
            return pxcmStatus.PXCM_STATUS_HANDLE_INVALID;
        object proxy;
        pxcmStatus status = SubscribeAlertINT(instance, handler, out proxy);
        if (status < pxcmStatus.PXCM_STATUS_NO_ERROR)
            return status;
        lock (maps.cs)
            maps.alert[handler] = proxy;
        return status;
    }

    public pxcmStatus UnsubscribeAlert(OnFiredAlertDelegate handler) {
        if (handler == null)
            return pxcmStatus.PXCM_STATUS_HANDLE_INVALID;
        lock (maps.cs) {
            object local_0;
            if (!maps.alert.TryGetValue(handler, out local_0))
                return pxcmStatus.PXCM_STATUS_HANDLE_INVALID;
            pxcmStatus local_1 = UnsubscribeAlertINT(instance, local_0);
            maps.alert.Remove(handler);
            return local_1;
        }
    }

    public pxcmStatus ApplyChanges() {
        return PXCMFaceConfiguration_ApplyChanges(instance, configs);
    }

    public void RestoreDefaults() {
        RestoreDefaultsINT(instance, out configs);
    }

    public pxcmStatus Update() {
        return UpdateINT(instance, out configs);
    }

    private class AlertHandlerDIR : IDisposable {
        private GCHandle gch;
        internal OnFiredAlertDelegate mfunc;
        internal IntPtr uDIR;

        public AlertHandlerDIR(OnFiredAlertDelegate handler) {
            mfunc = handler;
            gch = GCHandle.Alloc(handler);
            uDIR = PXCMFaceConfiguration_AllocAlertHandlerDIR(Marshal.GetFunctionPointerForDelegate(handler));
        }

        ~AlertHandlerDIR() {
            Dispose();
        }

        [DllImport("libpxccpp2c")]
        private static extern IntPtr PXCMFaceConfiguration_AllocAlertHandlerDIR(IntPtr handlerFunc);

        [DllImport("libpxccpp2c")]
        private static extern void PXCMFaceConfiguration_FreeAlertHandlerDIR(IntPtr hdir);

        public void Dispose() {
            if (uDIR == IntPtr.Zero)
                return;
            PXCMFaceConfiguration_FreeAlertHandlerDIR(uDIR);
            uDIR = IntPtr.Zero;
            if (!gch.IsAllocated)
                return;
            gch.Free();
        }

        internal delegate void OnFiredAlertDIRDelegate(IntPtr data);
    }

    public class ExpressionsConfiguration {
        private PXCMFaceConfiguration instance;

        public ExpressionsProperties properties {
            get {
                return instance.configs.expressionProperties;
            }
            set {
                instance.configs.expressionProperties = value;
            }
        }

        internal ExpressionsConfiguration(PXCMFaceConfiguration instance) {
            this.instance = instance;
        }

        [DllImport("libpxccpp2c")]
        internal static extern void PXCMFaceConfiguration_ExpressionsConfiguration_EnableAllExpressions(IntPtr instance);

        [DllImport("libpxccpp2c")]
        internal static extern void PXCMFaceConfiguration_ExpressionsConfiguration_DisableAllExpressions(IntPtr instance);

        [DllImport("libpxccpp2c")]
        internal static extern pxcmStatus PXCMFaceConfiguration_ExpressionsConfiguration_EnableExpression(IntPtr instance, PXCMFaceData.ExpressionsData.FaceExpression expression);

        [DllImport("libpxccpp2c")]
        internal static extern void PXCMFaceConfiguration_ExpressionsConfiguration_DisableExpression(IntPtr instsance, PXCMFaceData.ExpressionsData.FaceExpression expression);

        [DllImport("libpxccpp2c")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool PXCMFaceConfiguration_ExpressionsConfiguration_IsExpressionEnabled(IntPtr instance, PXCMFaceData.ExpressionsData.FaceExpression expression);

        public void Enable() {
            properties.isEnabled = true;
        }

        public void Disable() {
            properties.isEnabled = false;
        }

        public bool IsEnabled() {
            return properties.isEnabled;
        }

        public void EnableAllExpressions() {
            PXCMFaceConfiguration_ExpressionsConfiguration_EnableAllExpressions(instance.configs.expressionInstance);
        }

        public void DisableAllExpressions() {
            PXCMFaceConfiguration_ExpressionsConfiguration_DisableAllExpressions(instance.configs.expressionInstance);
        }

        public pxcmStatus EnableExpression(PXCMFaceData.ExpressionsData.FaceExpression expression) {
            return PXCMFaceConfiguration_ExpressionsConfiguration_EnableExpression(instance.configs.expressionInstance, expression);
        }

        public void DisableExpression(PXCMFaceData.ExpressionsData.FaceExpression expression) {
            PXCMFaceConfiguration_ExpressionsConfiguration_DisableExpression(instance.configs.expressionInstance, expression);
        }

        public bool IsExpressionEnabled(PXCMFaceData.ExpressionsData.FaceExpression expression) {
            return PXCMFaceConfiguration_ExpressionsConfiguration_IsExpressionEnabled(instance.configs.expressionInstance, expression);
        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public class ExpressionsProperties {
            [MarshalAs(UnmanagedType.Bool)]
            public bool isEnabled;
            public int maxTrackedFaces;
            internal Reserved reserved;
        }
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public class RecognitionConfiguration {
        public const int STORAGE_NAME_SIZE = 50;
        internal PXCMFaceConfiguration instance;

        public RecognitionStorageDesc storageDesc {
            get {
                return instance.configs.storageDesc;
            }
            set {
                instance.configs.storageDesc = value;
            }
        }

        public string storageName {
            get {
                return instance.configs.storageName;
            }
            set {
                instance.configs.storageName = value;
            }
        }

        public RecognitionProperties properties {
            get {
                return instance.configs.recognitionProperties;
            }
            set {
                instance.configs.recognitionProperties = value;
            }
        }

        internal RecognitionConfiguration(PXCMFaceConfiguration instance) {
            this.instance = instance;
        }

        [DllImport("libpxccpp2c")]
        private static extern pxcmStatus PXCMFaceConfiguration_RecognitionConfiguration_QueryActiveStorage(IntPtr instance, [Out] RecognitionStorageDesc outStorage);

        internal static pxcmStatus QueryActiveStorageINT(IntPtr instance, out RecognitionStorageDesc outStorage) {
            outStorage = new RecognitionStorageDesc();
            return PXCMFaceConfiguration_RecognitionConfiguration_QueryActiveStorage(instance, outStorage);
        }

        [DllImport("libpxccpp2c", CharSet = CharSet.Unicode)]
        private static extern pxcmStatus PXCMFaceConfiguration_RecognitionConfiguration_CreateStorage(IntPtr instance, string storageName, [Out] RecognitionStorageDesc outStroage);

        internal static pxcmStatus CreateStorageINT(IntPtr instance, string storageName, out RecognitionStorageDesc outStorage) {
            outStorage = new RecognitionStorageDesc();
            return PXCMFaceConfiguration_RecognitionConfiguration_CreateStorage(instance, storageName, outStorage);
        }

        [DllImport("libpxccpp2c", CharSet = CharSet.Unicode)]
        internal static extern pxcmStatus PXCMFaceConfiguration_RecognitionConfiguration_SetStorageDesc(IntPtr instance, string storageName, RecognitionStorageDesc storage);

        [DllImport("libpxccpp2c", CharSet = CharSet.Unicode)]
        internal static extern pxcmStatus PXCMFaceConfiguration_RecognitionConfiguration_DeleteStorage(IntPtr instance, string storageName);

        [DllImport("libpxccpp2c", CharSet = CharSet.Unicode)]
        internal static extern pxcmStatus PXCMFaceConfiguration_RecognitionConfiguration_UseStorage(IntPtr instance, string storageName);

        [DllImport("libpxccpp2c")]
        internal static extern void PXCMFaceConfiguration_RecognitionConfiguration_SetDatabaseBuffer(IntPtr instance, byte[] buffer, int size);

        public void Enable() {
            properties.isEnabled = true;
        }

        public void Disable() {
            properties.isEnabled = false;
        }

        public void SetAccuracyThreshold(int threshold) {
            properties.accuracyThreshold = threshold;
        }

        public int GetAccuracryThreshold() {
            return properties.accuracyThreshold;
        }

        public void SetRegistrationMode(RecognitionRegistrationMode mode) {
            properties.registrationMode = mode;
        }

        public RecognitionRegistrationMode GetRegistrationMode() {
            return properties.registrationMode;
        }

        public void SetDatabaseBuffer(byte[] buffer) {
            PXCMFaceConfiguration_RecognitionConfiguration_SetDatabaseBuffer(instance.configs.recognitionInstance, buffer, buffer.Length);
        }

        public pxcmStatus UseStorage(string storageName) {
            this.storageName = storageName;
            return PXCMFaceConfiguration_RecognitionConfiguration_UseStorage(instance.configs.recognitionInstance, this.storageName);
        }

        public pxcmStatus QueryActiveStorage(out RecognitionStorageDesc outStorage) {
            return QueryActiveStorageINT(instance.configs.recognitionInstance, out outStorage);
        }

        public pxcmStatus CreateStorage(string storageName, out RecognitionStorageDesc storageDesc) {
            return CreateStorageINT(instance.configs.recognitionInstance, storageName, out storageDesc);
        }

        public pxcmStatus SetStorageDesc(string storageName, RecognitionStorageDesc storageDesc) {
            return PXCMFaceConfiguration_RecognitionConfiguration_SetStorageDesc(instance.configs.recognitionInstance, storageName, storageDesc);
        }

        public pxcmStatus DeleteStorage(string storageName) {
            return PXCMFaceConfiguration_RecognitionConfiguration_DeleteStorage(instance.configs.recognitionInstance, storageName);
        }

        public enum RecognitionRegistrationMode {
            REGISTRATION_MODE_CONTINUOUS,
            REGISTRATION_MODE_ON_DEMAND
        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public class RecognitionStorageDesc {
            [MarshalAs(UnmanagedType.Bool)]
            public bool isPersistent;
            public int maxUsers;
            internal Reserved reserved;
        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public class RecognitionProperties {
            [MarshalAs(UnmanagedType.Bool)]
            public bool isEnabled;
            public int accuracyThreshold;
            public RecognitionRegistrationMode registrationMode;
            internal Reserved reserved;
        }
    }

    [StructLayout(LayoutKind.Sequential, Size = 40)]
    internal struct Reserved {
        internal int res1;
        internal int res2;
        internal int res3;
        internal int res4;
        internal int res5;
        internal int res6;
        internal int res7;
        internal int res8;
        internal int res9;
        internal int res10;
    }

    public enum TrackingStrategyType {
        STRATEGY_APPEARANCE_TIME,
        STRATEGY_CLOSEST_TO_FARTHEST,
        STRATEGY_FARTHEST_TO_CLOSEST,
        STRATEGY_LEFT_TO_RIGHT,
        STRATEGY_RIGHT_TO_LEFT
    }

    public enum SmoothingLevelType {
        SMOOTHING_DISABLED,
        SMOOTHING_MEDIUM,
        SMOOTHING_HIGH
    }

    [StructLayout(LayoutKind.Sequential)]
    public class DetectionConfiguration {
        [MarshalAs(UnmanagedType.Bool)]
        public bool isEnabled;
        public int maxTrackedFaces;
        public SmoothingLevelType smoothingLevel;
        internal Reserved reserved;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class LandmarksConfiguration {
        [MarshalAs(UnmanagedType.Bool)]
        public bool isEnabled;
        public int maxTrackedFaces;
        public SmoothingLevelType smoothingLevel;
        public int numLandmarks;
        internal Reserved reserved;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class PoseConfiguration {
        [MarshalAs(UnmanagedType.Bool)]
        public bool isEnabled;
        public int maxTrackedFaces;
        public SmoothingLevelType smoothingLevel;
        internal Reserved reserved;
    }

    public class PulseConfiguration {
        private PXCMFaceConfiguration instance;

        public PulseProperties properties {
            get {
                return instance.configs.pulseProperties;
            }
            set {
                instance.configs.pulseProperties = value;
            }
        }

        internal PulseConfiguration(PXCMFaceConfiguration instance) {
            this.instance = instance;
        }

        public void Enable() {
            properties.isEnabled = true;
        }

        public void Disable() {
            properties.isEnabled = false;
        }

        public bool IsEnabled() {
            return properties.isEnabled;
        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public class PulseProperties {
            [MarshalAs(UnmanagedType.Bool)]
            public bool isEnabled;
            public int maxTrackedFaces;
            internal Reserved reserved;
        }
    }

    public enum TrackingModeType {
        FACE_MODE_COLOR,
        FACE_MODE_COLOR_PLUS_DEPTH
    }

    public delegate void OnFiredAlertDelegate(PXCMFaceData.AlertData alertData);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal class AllFaceConfigurations {
        public IntPtr recognitionInstance;
        public IntPtr expressionInstance;
        public IntPtr pulseInsance;
        public DetectionConfiguration detection;
        public LandmarksConfiguration landmarks;
        public PoseConfiguration pose;
        public ExpressionsConfiguration.ExpressionsProperties expressionProperties;
        public RecognitionConfiguration.RecognitionProperties recognitionProperties;
        public PulseConfiguration.PulseProperties pulseProperties;
        public RecognitionConfiguration.RecognitionStorageDesc storageDesc;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
        public string storageName;
        public TrackingStrategyType strategy;

        public AllFaceConfigurations() {
            storageName = "";
            detection = new DetectionConfiguration();
            landmarks = new LandmarksConfiguration();
            pose = new PoseConfiguration();
            expressionProperties = new ExpressionsConfiguration.ExpressionsProperties();
            recognitionProperties = new RecognitionConfiguration.RecognitionProperties();
            pulseProperties = new PulseConfiguration.PulseProperties();
            storageDesc = new RecognitionConfiguration.RecognitionStorageDesc();
        }
    }

    internal class EventMaps {
        internal Dictionary<OnFiredAlertDelegate, object> alert = new Dictionary<OnFiredAlertDelegate, object>();
        internal object cs = new object();
    }
}
