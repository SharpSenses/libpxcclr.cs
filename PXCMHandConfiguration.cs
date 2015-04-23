using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

public class PXCMHandConfiguration : PXCMBase {
    public delegate void OnFiredAlertDelegate(PXCMHandData.AlertData alertData);

    public delegate void OnFiredGestureDelegate(PXCMHandData.GestureData gestureData);

    public new const int CUID = 1195589960;
    internal EventMaps maps;

    internal PXCMHandConfiguration(IntPtr instance, bool delete)
        : base(instance, delete) {
        maps = new EventMaps();
    }

    internal PXCMHandConfiguration(EventMaps maps, IntPtr instance, bool delete)
        : base(instance, delete) {
        this.maps = maps;
    }

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMHandConfiguration_ApplyChanges(IntPtr instance);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMHandConfiguration_RestoreDefaults(IntPtr instance);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMHandConfiguration_Update(IntPtr instance);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMHandConfiguration_ResetTracking(IntPtr instance);

    [DllImport("libpxccpp2c", CharSet = CharSet.Unicode)]
    internal static extern pxcmStatus PXCMHandConfiguration_SetUserName(IntPtr instance, string userName);

    [DllImport("libpxccpp2c", CharSet = CharSet.Unicode)]
    private static extern void PXCMHandConfiguration_QueryUserName(IntPtr instance, StringBuilder userName);

    internal static string QueryUserNameINT(IntPtr instance) {
        var userName = new StringBuilder(64);
        PXCMHandConfiguration_QueryUserName(instance, userName);
        return userName.ToString();
    }

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMHandConfiguration_EnableJointSpeed(IntPtr instance,
        PXCMHandData.JointType jointLabel, PXCMHandData.JointSpeedType jointSpeed, int time);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMHandConfiguration_DisableJointSpeed(IntPtr instance,
        PXCMHandData.JointType jointLabel);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMHandConfiguration_SetTrackingBounds(IntPtr instance,
        float nearTrackingDistance, float farTrackingDistance, float nearTrackingWidth, float nearTrackingHeight);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMHandConfiguration_QueryTrackingBounds(IntPtr instance,
        out float nearTrackingDistance, out float farTrackingDistance, out float nearTrackingWidth,
        out float nearTrackingHeight);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMHandConfiguration_SetTrackingMode(IntPtr instance,
        PXCMHandData.TrackingModeType trackingMode);

    [DllImport("libpxccpp2c")]
    internal static extern PXCMHandData.TrackingModeType PXCMHandConfiguration_QueryTrackingMode(IntPtr instance);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMHandConfiguration_SetSmoothingValue(IntPtr instance, float smoothingValue);

    [DllImport("libpxccpp2c")]
    internal static extern float PXCMHandConfiguration_QuerySmoothingValue(IntPtr instance);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMHandConfiguration_EnableStabilizer(IntPtr instance,
        [MarshalAs(UnmanagedType.Bool)] bool enableFlag);

    [DllImport("libpxccpp2c")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool PXCMHandConfiguration_IsStabilizerEnabled(IntPtr instance);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMHandConfiguration_EnableNormalizedJoints(IntPtr instance,
        [MarshalAs(UnmanagedType.Bool)] bool enableFlag);

    [DllImport("libpxccpp2c")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool PXCMHandConfiguration_IsNormalizedJointsEnabled(IntPtr instance);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMHandConfiguration_EnableSegmentationImage(IntPtr instance,
        [MarshalAs(UnmanagedType.Bool)] bool enableFlag);

    [DllImport("libpxccpp2c")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool PXCMHandConfiguration_IsSegmentationImageEnabled(IntPtr instance);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMHandConfiguration_EnableTrackedJoints(IntPtr instance,
        [MarshalAs(UnmanagedType.Bool)] bool enableFlag);

    [DllImport("libpxccpp2c")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool PXCMHandConfiguration_IsTrackedJointsEnabled(IntPtr instance);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMHandConfiguration_EnableAlert(IntPtr instance,
        PXCMHandData.AlertType alertEvent);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMHandConfiguration_EnableAllAlerts(IntPtr instance);

    [DllImport("libpxccpp2c")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool PXCMHandConfiguration_IsAlertEnabled(IntPtr instance, PXCMHandData.AlertType alertEvent);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMHandConfiguration_DisableAlert(IntPtr instance,
        PXCMHandData.AlertType alertEvent);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMHandConfiguration_DisableAllAlerts(IntPtr instance);

    [DllImport("libpxccpp2c", CharSet = CharSet.Unicode)]
    internal static extern pxcmStatus PXCMHandConfiguration_LoadGesturePack(IntPtr instance, string gesturePackPath);

    [DllImport("libpxccpp2c", CharSet = CharSet.Unicode)]
    internal static extern pxcmStatus PXCMHandConfiguration_UnloadGesturePack(IntPtr instance, string gesturePackPath);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMHandConfiguration_UnloadAllGesturesPacks(IntPtr instance);

    [DllImport("libpxccpp2c")]
    internal static extern int PXCMHandConfiguration_QueryGesturesTotalNumber(IntPtr instance);

    [DllImport("libpxccpp2c", CharSet = CharSet.Unicode)]
    internal static extern pxcmStatus PXCMHandConfiguration_QueryGestureNameByIndex(IntPtr instance, int index,
        int nchars, StringBuilder gestureName);

    [DllImport("libpxccpp2c", CharSet = CharSet.Unicode)]
    internal static extern pxcmStatus PXCMHandConfiguration_EnableGesture(IntPtr instance, string gestureName,
        [MarshalAs(UnmanagedType.Bool)] bool continuousGesture);

    [DllImport("libpxccpp2c", CharSet = CharSet.Unicode)]
    internal static extern pxcmStatus PXCMHandConfiguration_EnableGesture(IntPtr instance, string gestureName);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMHandConfiguration_EnableAllGestures(IntPtr instance,
        [MarshalAs(UnmanagedType.Bool)] bool continuousGesture);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMHandConfiguration_EnableAllGestures(IntPtr instance);

    [DllImport("libpxccpp2c", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool PXCMHandConfiguration_IsGestureEnabled(IntPtr instance, string gestureName);

    [DllImport("libpxccpp2c", CharSet = CharSet.Unicode)]
    internal static extern pxcmStatus PXCMHandConfiguration_DisableGesture(IntPtr instance, string gestureName);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMHandConfiguration_DisableAllGestures(IntPtr instance);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMHandConfiguration_SubscribeAlert(IntPtr hand, IntPtr alertHandler);

    internal static pxcmStatus SubscribeAlertINT(IntPtr instance, OnFiredAlertDelegate handler, out object proxy) {
        var alertHandlerDir = new AlertHandlerDIR(handler);
        var pxcmStatus = PXCMHandConfiguration_SubscribeAlert(instance, alertHandlerDir.uDIR);
        if (pxcmStatus < pxcmStatus.PXCM_STATUS_NO_ERROR) {
            alertHandlerDir.Dispose();
        }
        proxy = alertHandlerDir;
        return pxcmStatus;
    }

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMHandConfiguration_UnsubscribeAlert(IntPtr hand, IntPtr alertHandler);

    internal static pxcmStatus UnsubscribeAlertINT(IntPtr instance, object proxy) {
        var alertHandlerDir = (AlertHandlerDIR) proxy;
        var pxcmStatus = PXCMHandConfiguration_UnsubscribeAlert(instance, alertHandlerDir.uDIR);
        alertHandlerDir.Dispose();
        return pxcmStatus;
    }

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMHandConfiguration_SubscribeGesture(IntPtr hand, IntPtr gestureHandler);

    internal static pxcmStatus SubscribeGestureINT(IntPtr instance, OnFiredGestureDelegate handler, out object proxy) {
        var gestureHandlerDir = new GestureHandlerDIR(handler);
        var pxcmStatus = PXCMHandConfiguration_SubscribeGesture(instance, gestureHandlerDir.uDIR);
        if (pxcmStatus < pxcmStatus.PXCM_STATUS_NO_ERROR) {
            gestureHandlerDir.Dispose();
        }
        proxy = gestureHandlerDir;
        return pxcmStatus;
    }

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMHandConfiguration_UnsubscribeGesture(IntPtr hand, IntPtr gestureHandler);

    internal static pxcmStatus UnsubscribeGestureINT(IntPtr instance, object proxy) {
        var gestureHandlerDir = (GestureHandlerDIR) proxy;
        var pxcmStatus = PXCMHandConfiguration_UnsubscribeGesture(instance, gestureHandlerDir.uDIR);
        gestureHandlerDir.Dispose();
        return pxcmStatus;
    }

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMHandConfiguration_SetDefaultAge(IntPtr instance, int age);

    [DllImport("libpxccpp2c")]
    internal static extern int PXCMHandConfiguration_QueryDefaultAge(IntPtr instance);

    public pxcmStatus ApplyChanges() {
        return PXCMHandConfiguration_ApplyChanges(instance);
    }

    public pxcmStatus RestoreDefaults() {
        return PXCMHandConfiguration_RestoreDefaults(instance);
    }

    public pxcmStatus Update() {
        return PXCMHandConfiguration_Update(instance);
    }

    public pxcmStatus ResetTracking() {
        return PXCMHandConfiguration_ResetTracking(instance);
    }

    public pxcmStatus SetUserName(string userName) {
        return PXCMHandConfiguration_SetUserName(instance, userName);
    }

    public string QueryUserName() {
        return QueryUserNameINT(instance);
    }

    public pxcmStatus EnableJointSpeed(PXCMHandData.JointType jointLabel, PXCMHandData.JointSpeedType jointSpeed,
        int time) {
        return PXCMHandConfiguration_EnableJointSpeed(instance, jointLabel, jointSpeed, time);
    }

    public pxcmStatus DisableJointSpeed(PXCMHandData.JointType jointLabel) {
        return PXCMHandConfiguration_DisableJointSpeed(instance, jointLabel);
    }

    public pxcmStatus SetTrackingBounds(float nearTrackingDistance, float farTrackingDistance, float nearTrackingWidth,
        float nearTrackingHeight) {
        return PXCMHandConfiguration_SetTrackingBounds(instance, nearTrackingDistance, farTrackingDistance,
            nearTrackingWidth, nearTrackingHeight);
    }

    public pxcmStatus SetDefaultAge(int age) {
        return PXCMHandConfiguration_SetDefaultAge(instance, age);
    }

    public int QueryDefaultAge() {
        return PXCMHandConfiguration_QueryDefaultAge(instance);
    }

    public pxcmStatus QueryTrackingBounds(out float nearTrackingDistance, out float farTrackingDistance,
        out float nearTrackingWidth, out float nearTrackingHeight) {
        nearTrackingDistance = 0.0f;
        farTrackingDistance = 0.0f;
        nearTrackingWidth = 0.0f;
        nearTrackingHeight = 0.0f;
        return PXCMHandConfiguration_QueryTrackingBounds(instance, out nearTrackingDistance, out farTrackingDistance,
            out nearTrackingWidth, out nearTrackingHeight);
    }

    public pxcmStatus SetTrackingMode(PXCMHandData.TrackingModeType trackingMode) {
        return PXCMHandConfiguration_SetTrackingMode(instance, trackingMode);
    }

    public PXCMHandData.TrackingModeType QueryTrackingMode() {
        return PXCMHandConfiguration_QueryTrackingMode(instance);
    }

    public pxcmStatus SetSmoothingValue(float smoothingValue) {
        return PXCMHandConfiguration_SetSmoothingValue(instance, smoothingValue);
    }

    public float QuerySmoothingValue() {
        return PXCMHandConfiguration_QuerySmoothingValue(instance);
    }

    public pxcmStatus EnableStabilizer(bool enableFlag) {
        return PXCMHandConfiguration_EnableStabilizer(instance, enableFlag);
    }

    public bool IsStabilizerEnabled() {
        return PXCMHandConfiguration_IsStabilizerEnabled(instance);
    }

    public pxcmStatus EnableNormalizedJoints(bool enableFlag) {
        return PXCMHandConfiguration_EnableNormalizedJoints(instance, enableFlag);
    }

    public bool IsNormalizedJointsEnabled() {
        return PXCMHandConfiguration_IsNormalizedJointsEnabled(instance);
    }

    public pxcmStatus EnableSegmentationImage(bool enableFlag) {
        return PXCMHandConfiguration_EnableSegmentationImage(instance, enableFlag);
    }

    public bool IsSegmentationImageEnabled() {
        return PXCMHandConfiguration_IsSegmentationImageEnabled(instance);
    }

    public pxcmStatus EnableTrackedJoints(bool enableFlag) {
        return PXCMHandConfiguration_EnableTrackedJoints(instance, enableFlag);
    }

    public bool IsTrackedJointsEnabled() {
        return PXCMHandConfiguration_IsTrackedJointsEnabled(instance);
    }

    public pxcmStatus EnableAlert(PXCMHandData.AlertType alertEvent) {
        return PXCMHandConfiguration_EnableAlert(instance, alertEvent);
    }

    public pxcmStatus EnableAllAlerts() {
        return PXCMHandConfiguration_EnableAllAlerts(instance);
    }

    public bool IsAlertEnabled(PXCMHandData.AlertType alertEvent) {
        return PXCMHandConfiguration_IsAlertEnabled(instance, alertEvent);
    }

    public pxcmStatus DisableAlert(PXCMHandData.AlertType alertEvent) {
        return PXCMHandConfiguration_DisableAlert(instance, alertEvent);
    }

    public pxcmStatus DisableAllAlerts() {
        return PXCMHandConfiguration_DisableAllAlerts(instance);
    }

    public pxcmStatus SubscribeAlert(OnFiredAlertDelegate handler) {
        if (handler == null) {
            return pxcmStatus.PXCM_STATUS_HANDLE_INVALID;
        }
        object proxy;
        var status = SubscribeAlertINT(instance, handler, out proxy);
        if (status >= pxcmStatus.PXCM_STATUS_NO_ERROR) {
            lock (maps.cs)
                maps.alert[handler] = proxy;
        }
        return status;
    }

    public pxcmStatus UnsubscribeAlert(OnFiredAlertDelegate handler) {
        if (handler == null) {
            return pxcmStatus.PXCM_STATUS_HANDLE_INVALID;
        }
        lock (maps.cs) {
            object local_0;
            if (!maps.alert.TryGetValue(handler, out local_0)) {
                return pxcmStatus.PXCM_STATUS_HANDLE_INVALID;
            }
            var local_1 = UnsubscribeAlertINT(instance, local_0);
            maps.alert.Remove(handler);
            return local_1;
        }
    }

    public pxcmStatus LoadGesturePack(string gesturePackPath) {
        return PXCMHandConfiguration_LoadGesturePack(instance, gesturePackPath);
    }

    public pxcmStatus UnloadGesturePack(string gesturePackPath) {
        return PXCMHandConfiguration_UnloadGesturePack(instance, gesturePackPath);
    }

    public pxcmStatus UnloadAllGesturesPacks() {
        return PXCMHandConfiguration_UnloadAllGesturesPacks(instance);
    }

    public int QueryGesturesTotalNumber() {
        return PXCMHandConfiguration_QueryGesturesTotalNumber(instance);
    }

    public pxcmStatus QueryGestureNameByIndex(int index, out string gestureName) {
        var gestureName1 = new StringBuilder(64);
        var pxcmStatus = PXCMHandConfiguration_QueryGestureNameByIndex(instance, index, gestureName1.Capacity + 1,
            gestureName1);
        gestureName = gestureName1.ToString();
        return pxcmStatus;
    }

    public pxcmStatus EnableGesture(string gestureName, bool continuousGesture) {
        return PXCMHandConfiguration_EnableGesture(instance, gestureName, continuousGesture);
    }

    public pxcmStatus EnableGesture(string gestureName) {
        return EnableGesture(gestureName, false);
    }

    public pxcmStatus EnableAllGestures(bool continuousGesture) {
        return PXCMHandConfiguration_EnableAllGestures(instance, continuousGesture);
    }

    public pxcmStatus EnableAllGestures() {
        return EnableAllGestures(false);
    }

    public bool IsGestureEnabled(string gestureName) {
        return PXCMHandConfiguration_IsGestureEnabled(instance, gestureName);
    }

    public pxcmStatus DisableGesture(string gestureName) {
        return PXCMHandConfiguration_DisableGesture(instance, gestureName);
    }

    public pxcmStatus DisableAllGestures() {
        return PXCMHandConfiguration_DisableAllGestures(instance);
    }

    public pxcmStatus SubscribeGesture(OnFiredGestureDelegate handler) {
        if (handler == null) {
            return pxcmStatus.PXCM_STATUS_HANDLE_INVALID;
        }
        object proxy;
        var status = SubscribeGestureINT(instance, handler, out proxy);
        if (status >= pxcmStatus.PXCM_STATUS_NO_ERROR) {
            lock (maps.cs)
                maps.gesture[handler] = proxy;
        }
        return status;
    }

    public pxcmStatus UnsubscribeGesture(OnFiredGestureDelegate handler) {
        if (handler == null) {
            return pxcmStatus.PXCM_STATUS_HANDLE_INVALID;
        }
        lock (maps.cs) {
            object local_0;
            if (!maps.gesture.TryGetValue(handler, out local_0)) {
                return pxcmStatus.PXCM_STATUS_HANDLE_INVALID;
            }
            var local_1 = UnsubscribeGestureINT(instance, local_0);
            maps.gesture.Remove(handler);
            return local_1;
        }
    }

    internal class GestureHandlerDIR : IDisposable {
        private GCHandle gch;
        internal OnFiredGestureDelegate mfunc;
        internal IntPtr uDIR;

        public GestureHandlerDIR(OnFiredGestureDelegate handler) {
            mfunc = handler;
            gch = GCHandle.Alloc(handler);
            uDIR = PXCMHandConfiguration_AllocGestureHandlerDIR(Marshal.GetFunctionPointerForDelegate(handler));
        }

        public void Dispose() {
            if (uDIR == IntPtr.Zero) {
                return;
            }
            PXCMHandConfiguration_FreeGestureHandlerDIR(uDIR);
            uDIR = IntPtr.Zero;
            if (!gch.IsAllocated) {
                return;
            }
            gch.Free();
        }

        ~GestureHandlerDIR() {
            Dispose();
        }

        [DllImport("libpxccpp2c")]
        internal static extern IntPtr PXCMHandConfiguration_AllocGestureHandlerDIR(IntPtr handlerFunc);

        [DllImport("libpxccpp2c")]
        internal static extern void PXCMHandConfiguration_FreeGestureHandlerDIR(IntPtr hdir);
    }

    internal class AlertHandlerDIR : IDisposable {
        private GCHandle gch;
        internal OnFiredAlertDelegate mfunc;
        internal IntPtr uDIR;

        public AlertHandlerDIR(OnFiredAlertDelegate handler) {
            mfunc = handler;
            gch = GCHandle.Alloc(handler);
            uDIR = PXCMHandConfiguration_AllocAlertHandlerDIR(Marshal.GetFunctionPointerForDelegate(handler));
        }

        public void Dispose() {
            if (uDIR == IntPtr.Zero) {
                return;
            }
            PXCMHandConfiguration_FreeAlertHandlerDIR(uDIR);
            uDIR = IntPtr.Zero;
            if (!gch.IsAllocated) {
                return;
            }
            gch.Free();
        }

        ~AlertHandlerDIR() {
            Dispose();
        }

        [DllImport("libpxccpp2c")]
        internal static extern IntPtr PXCMHandConfiguration_AllocAlertHandlerDIR(IntPtr handlerFunc);

        [DllImport("libpxccpp2c")]
        internal static extern void PXCMHandConfiguration_FreeAlertHandlerDIR(IntPtr hdir);

        internal delegate void OnFiredAlertDIRDelegate(IntPtr data);
    }

    internal class EventMaps {
        internal Dictionary<OnFiredAlertDelegate, object> alert = new Dictionary<OnFiredAlertDelegate, object>();
        internal object cs = new object();
        internal Dictionary<OnFiredGestureDelegate, object> gesture = new Dictionary<OnFiredGestureDelegate, object>();
    }
}