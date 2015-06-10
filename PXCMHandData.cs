using System;
using System.Runtime.InteropServices;

public class PXCMHandData : PXCMBase {
    public enum AccessOrderType {
        ACCESS_ORDER_BY_ID,
        ACCESS_ORDER_BY_TIME,
        ACCESS_ORDER_NEAR_TO_FAR,
        ACCESS_ORDER_LEFT_HANDS,
        ACCESS_ORDER_RIGHT_HANDS,
        ACCESS_ORDER_FIXED
    }

    public enum AlertType {
        ALERT_HAND_DETECTED = 1,
        ALERT_HAND_NOT_DETECTED = 2,
        ALERT_HAND_TRACKED = 4,
        ALERT_HAND_NOT_TRACKED = 8,
        ALERT_HAND_CALIBRATED = 16,
        ALERT_HAND_NOT_CALIBRATED = 32,
        ALERT_HAND_OUT_OF_BORDERS = 64,
        ALERT_HAND_INSIDE_BORDERS = 128,
        ALERT_HAND_OUT_OF_LEFT_BORDER = 256,
        ALERT_HAND_OUT_OF_RIGHT_BORDER = 512,
        ALERT_HAND_OUT_OF_TOP_BORDER = 1024,
        ALERT_HAND_OUT_OF_BOTTOM_BORDER = 2048,
        ALERT_HAND_TOO_FAR = 4096,
        ALERT_HAND_TOO_CLOSE = 8192,
        ALERT_HAND_LOW_CONFIDENCE = 16384
    }

    public enum BodySideType {
        BODY_SIDE_UNKNOWN,
        BODY_SIDE_LEFT,
        BODY_SIDE_RIGHT
    }

    public enum ExtremityType {
        EXTREMITY_CLOSEST,
        EXTREMITY_LEFTMOST,
        EXTREMITY_RIGHTMOST,
        EXTREMITY_TOPMOST,
        EXTREMITY_BOTTOMMOST,
        EXTREMITY_CENTER
    }

    public enum FingerType {
        FINGER_THUMB,
        FINGER_INDEX,
        FINGER_MIDDLE,
        FINGER_RING,
        FINGER_PINKY
    }

    public enum GestureStateType {
        GESTURE_STATE_START,
        GESTURE_STATE_IN_PROGRESS,
        GESTURE_STATE_END
    }

    public enum JointSpeedType {
        JOINT_SPEED_AVERAGE,
        JOINT_SPEED_ABSOLUTE
    }

    public enum JointType {
        JOINT_WRIST,
        JOINT_CENTER,
        JOINT_THUMB_BASE,
        JOINT_THUMB_JT1,
        JOINT_THUMB_JT2,
        JOINT_THUMB_TIP,
        JOINT_INDEX_BASE,
        JOINT_INDEX_JT1,
        JOINT_INDEX_JT2,
        JOINT_INDEX_TIP,
        JOINT_MIDDLE_BASE,
        JOINT_MIDDLE_JT1,
        JOINT_MIDDLE_JT2,
        JOINT_MIDDLE_TIP,
        JOINT_RING_BASE,
        JOINT_RING_JT1,
        JOINT_RING_JT2,
        JOINT_RING_TIP,
        JOINT_PINKY_BASE,
        JOINT_PINKY_JT1,
        JOINT_PINKY_JT2,
        JOINT_PINKY_TIP
    }

    public enum TrackingModeType {
        TRACKING_MODE_FULL_HAND,
        TRACKING_MODE_EXTREMITIES
    }

    public enum TrackingStatusType {
        TRACKING_STATUS_GOOD = 0,
        TRACKING_STATUS_OUT_OF_FOV = 1,
        TRACKING_STATUS_OUT_OF_RANGE = 2,
        TRACKING_STATUS_HIGH_SPEED = 4,
        TRACKING_STATUS_POINTING_FINGERS = 8
    }

    public new const int CUID = 1413759304;
    public const int NUMBER_OF_FINGERS = 5;
    public const int NUMBER_OF_EXTREMITIES = 6;
    public const int NUMBER_OF_JOINTS = 22;
    public const int RESERVED_NUMBER_OF_JOINTS = 32;
    public const int MAX_NAME_SIZE = 64;
    public const int MAX_PATH_NAME = 256;

    internal PXCMHandData(IntPtr instance, bool delete)
        : base(instance, delete) {}

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMHandData_Update(IntPtr instance);

    [DllImport("libpxccpp2c")]
    internal static extern int PXCMHandData_QueryFiredAlertsNumber(IntPtr instance);

    [DllImport("libpxccpp2c")]
    private static extern pxcmStatus PXCMHandData_QueryFiredAlertData(IntPtr instance, int index,
        [Out] AlertData alertData);

    internal static pxcmStatus QueryFiredAlertDataINT(IntPtr instance, int index, out AlertData alertData) {
        alertData = new AlertData();
        return PXCMHandData_QueryFiredAlertData(instance, index, alertData);
    }

    [DllImport("libpxccpp2c")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool PXCMHandData_IsAlertFired(IntPtr instance, AlertType alertEvent,
        [Out] AlertData alertData);

    internal static bool IsAlertFiredINT(IntPtr instance, AlertType alertEvent, out AlertData alertData) {
        alertData = new AlertData();
        return PXCMHandData_IsAlertFired(instance, alertEvent, alertData);
    }

    [DllImport("libpxccpp2c")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool PXCMHandData_IsAlertFiredByHand(IntPtr instance, AlertType alertEvent, int handID,
        [Out] AlertData alertData);

    internal static bool IsAlertFiredByHandINT(IntPtr instance, AlertType alertEvent, int handID,
        out AlertData alertData) {
        alertData = new AlertData();
        return PXCMHandData_IsAlertFiredByHand(instance, alertEvent, handID, alertData);
    }

    [DllImport("libpxccpp2c")]
    internal static extern int PXCMHandData_QueryFiredGesturesNumber(IntPtr instance);

    [DllImport("libpxccpp2c")]
    private static extern pxcmStatus PXCMHandData_QueryFiredGestureData(IntPtr instance, int index,
        [Out] GestureData gestureData);

    internal static pxcmStatus QueryFiredGestureDataINT(IntPtr instance, int index, out GestureData gestureData) {
        gestureData = new GestureData();
        return PXCMHandData_QueryFiredGestureData(instance, index, gestureData);
    }

    [DllImport("libpxccpp2c", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool PXCMHandData_IsGestureFired(IntPtr instance, string gestureName,
        [Out] GestureData gestureData);

    internal static bool IsGestureFiredINT(IntPtr instance, string gestureName, out GestureData gestureData) {
        gestureData = new GestureData();
        return PXCMHandData_IsGestureFired(instance, gestureName, gestureData);
    }

    [DllImport("libpxccpp2c", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool PXCMHandData_IsGestureFiredByHand(IntPtr instance, string gestureName, int handID,
        [Out] GestureData gestureData);

    internal static bool IsGestureFiredByHandINT(IntPtr instance, string gestureName, int handID,
        out GestureData gestureData) {
        gestureData = new GestureData();
        return PXCMHandData_IsGestureFiredByHand(instance, gestureName, handID, gestureData);
    }

    [DllImport("libpxccpp2c")]
    internal static extern int PXCMHandData_QueryNumberOfHands(IntPtr instance);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMHandData_QueryHandId(IntPtr instance, AccessOrderType accessOrder, int index,
        out int handId);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMHandData_QueryHandData(IntPtr instance, AccessOrderType accessOrder, int index,
        out IntPtr handData);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMHandData_QueryHandDataById(IntPtr instance, int handId, out IntPtr handData);

    public pxcmStatus Update() {
        return PXCMHandData_Update(instance);
    }

    public int QueryFiredAlertsNumber() {
        return PXCMHandData_QueryFiredAlertsNumber(instance);
    }

    public pxcmStatus QueryFiredAlertData(int index, out AlertData alertData) {
        return QueryFiredAlertDataINT(instance, index, out alertData);
    }

    public bool IsAlertFired(AlertType alertEvent, out AlertData alertData) {
        return IsAlertFiredINT(instance, alertEvent, out alertData);
    }

    public bool IsAlertFiredByHand(AlertType alertEvent, int handID, out AlertData alertData) {
        return IsAlertFiredByHandINT(instance, alertEvent, handID, out alertData);
    }

    public int QueryFiredGesturesNumber() {
        return PXCMHandData_QueryFiredGesturesNumber(instance);
    }

    public pxcmStatus QueryFiredGestureData(int index, out GestureData gestureData) {
        return QueryFiredGestureDataINT(instance, index, out gestureData);
    }

    public bool IsGestureFired(string gestureName, out GestureData gestureData) {
        return IsGestureFiredINT(instance, gestureName, out gestureData);
    }

    public bool IsGestureFiredByHand(string gestureName, int handID, out GestureData gestureData) {
        return IsGestureFiredByHandINT(instance, gestureName, handID, out gestureData);
    }

    public int QueryNumberOfHands() {
        return PXCMHandData_QueryNumberOfHands(instance);
    }

    public pxcmStatus QueryHandId(AccessOrderType accessOrder, int index, out int handId) {
        return PXCMHandData_QueryHandId(instance, accessOrder, index, out handId);
    }

    public pxcmStatus QueryHandData(AccessOrderType accessOrder, int index, out IHand handData) {
        IntPtr handData1;
        var pxcmStatus = PXCMHandData_QueryHandData(instance, accessOrder, index, out handData1);
        handData = pxcmStatus >= pxcmStatus.PXCM_STATUS_NO_ERROR ? new IHand(handData1) : null;
        return pxcmStatus;
    }

    public pxcmStatus QueryHandDataById(int handId, out IHand handData) {
        IntPtr handData1;
        var pxcmStatus = PXCMHandData_QueryHandDataById(instance, handId, out handData1);
        handData = pxcmStatus >= pxcmStatus.PXCM_STATUS_NO_ERROR ? new IHand(handData1) : null;
        return pxcmStatus;
    }

    public class IHand {
        private IntPtr instance;

        internal IHand(IntPtr instance) {
            this.instance = instance;
        }

        [DllImport("libpxccpp2c")]
        internal static extern int PXCMHandData_IHand_QueryUniqueId(IntPtr instance);

        [DllImport("libpxccpp2c")]
        internal static extern int PXCMHandData_IHand_QueryUserId(IntPtr instance);

        [DllImport("libpxccpp2c")]
        internal static extern long PXCMHandData_IHand_QueryTimeStamp(IntPtr instance);

        [DllImport("libpxccpp2c")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool PXCMHandData_IHand_IsCalibrated(IntPtr instance);

        [DllImport("libpxccpp2c")]
        internal static extern BodySideType PXCMHandData_IHand_QueryBodySide(IntPtr instance);

        [DllImport("libpxccpp2c")]
        internal static extern void PXCMHandData_IHand_QueryBoundingBoxImage(IntPtr instance, ref PXCMRectI32 rect);

        [DllImport("libpxccpp2c")]
        internal static extern void PXCMHandData_IHand_QueryMassCenterImage(IntPtr instance, ref PXCMPointF32 point);

        [DllImport("libpxccpp2c")]
        internal static extern void PXCMHandData_IHand_QueryMassCenterWorld(IntPtr instance, ref PXCMPoint3DF32 point);

        [DllImport("libpxccpp2c")]
        internal static extern void PXCMHandData_IHand_QueryPalmOrientation(IntPtr instance, ref PXCMPoint4DF32 point);

        [DllImport("libpxccpp2c")]
        internal static extern int PXCMHandData_IHand_QueryOpenness(IntPtr instance);

        [DllImport("libpxccpp2c")]
        internal static extern float PXCMHandData_IHand_QueryPalmRadiusImage(IntPtr instance);

        [DllImport("libpxccpp2c")]
        internal static extern float PXCMHandData_IHand_QueryPalmRadiusWorld(IntPtr instance);

        [DllImport("libpxccpp2c")]
        internal static extern int PXCMHandData_IHand_QueryTrackingStatus(IntPtr instance);

        [DllImport("libpxccpp2c")]
        private static extern pxcmStatus PXCMHandData_IHand_QueryExtremityPoint(IntPtr instance,
            ExtremityType extremityLabel, [Out] ExtremityData extremityPoint);

        internal static pxcmStatus QueryExtremityPointINT(IntPtr instance, ExtremityType extremityLabel,
            out ExtremityData extremityPoint) {
            extremityPoint = new ExtremityData();
            return PXCMHandData_IHand_QueryExtremityPoint(instance, extremityLabel, extremityPoint);
        }

        [DllImport("libpxccpp2c")]
        private static extern pxcmStatus PXCMHandData_IHand_QueryFingerData(IntPtr instance, FingerType fingerLabel,
            [Out] FingerData fingerData);

        internal static pxcmStatus QueryFingerDataINT(IntPtr instance, FingerType fingerLabel, out FingerData fingerData) {
            fingerData = new FingerData();
            return PXCMHandData_IHand_QueryFingerData(instance, fingerLabel, fingerData);
        }

        [DllImport("libpxccpp2c")]
        private static extern pxcmStatus PXCMHandData_IHand_QueryTrackedJoint(IntPtr instance, JointType jointLabel,
            [Out] JointData jointData);

        internal static pxcmStatus QueryTrackedJointINT(IntPtr instance, JointType jointLabel, out JointData jointData) {
            jointData = new JointData();
            return PXCMHandData_IHand_QueryTrackedJoint(instance, jointLabel, jointData);
        }

        [DllImport("libpxccpp2c")]
        private static extern pxcmStatus PXCMHandData_IHand_QueryNormalizedJoint(IntPtr instance, JointType jointLabel,
            [Out] JointData jointData);

        internal static pxcmStatus QueryNormalizedJointINT(IntPtr instance, JointType jointLabel,
            out JointData jointData) {
            jointData = new JointData();
            return PXCMHandData_IHand_QueryNormalizedJoint(instance, jointLabel, jointData);
        }

        [DllImport("libpxccpp2c")]
        internal static extern pxcmStatus PXCMHandData_IHand_QuerySegmentationImage(IntPtr instance, out IntPtr image);

        [DllImport("libpxccpp2c")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool PXCMHandData_IHand_HasTrackedJoints(IntPtr instance);

        [DllImport("libpxccpp2c")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool PXCMHandData_IHand_HasNormalizedJoints(IntPtr instance);

        [DllImport("libpxccpp2c")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool PXCMHandData_IHand_HasSegmentationImage(IntPtr instance);

        public int QueryUniqueId() {
            return PXCMHandData_IHand_QueryUniqueId(instance);
        }

        public int QueryUserId() {
            return PXCMHandData_IHand_QueryUserId(instance);
        }

        public long QueryTimeStamp() {
            return PXCMHandData_IHand_QueryTimeStamp(instance);
        }

        public bool IsCalibrated() {
            return PXCMHandData_IHand_IsCalibrated(instance);
        }

        public BodySideType QueryBodySide() {
            return PXCMHandData_IHand_QueryBodySide(instance);
        }

        public PXCMRectI32 QueryBoundingBoxImage() {
            var rect = new PXCMRectI32();
            PXCMHandData_IHand_QueryBoundingBoxImage(instance, ref rect);
            return rect;
        }

        public PXCMPointF32 QueryMassCenterImage() {
            var point = new PXCMPointF32();
            PXCMHandData_IHand_QueryMassCenterImage(instance, ref point);
            return point;
        }

        public PXCMPoint3DF32 QueryMassCenterWorld() {
            var point = new PXCMPoint3DF32();
            PXCMHandData_IHand_QueryMassCenterWorld(instance, ref point);
            return point;
        }

        public PXCMPoint4DF32 QueryPalmOrientation() {
            var point = new PXCMPoint4DF32();
            PXCMHandData_IHand_QueryPalmOrientation(instance, ref point);
            return point;
        }

        public int QueryOpenness() {
            return PXCMHandData_IHand_QueryOpenness(instance);
        }

        public float QueryPalmRadiusImage() {
            return PXCMHandData_IHand_QueryPalmRadiusImage(instance);
        }

        public float QueryPalmRadiusWorld() {
            return PXCMHandData_IHand_QueryPalmRadiusWorld(instance);
        }

        public int QueryTrackingStatus() {
            return PXCMHandData_IHand_QueryTrackingStatus(instance);
        }

        public pxcmStatus QueryExtremityPoint(ExtremityType extremityLabel, out ExtremityData extremityPoint) {
            return QueryExtremityPointINT(instance, extremityLabel, out extremityPoint);
        }

        public pxcmStatus QueryFingerData(FingerType fingerLabel, out FingerData fingerData) {
            return QueryFingerDataINT(instance, fingerLabel, out fingerData);
        }

        public pxcmStatus QueryTrackedJoint(JointType jointLabel, out JointData jointData) {
            return QueryTrackedJointINT(instance, jointLabel, out jointData);
        }

        public pxcmStatus QueryNormalizedJoint(JointType jointLabel, out JointData jointData) {
            return QueryNormalizedJointINT(instance, jointLabel, out jointData);
        }

        public pxcmStatus QuerySegmentationImage(out PXCMImage image) {
            IntPtr image1;
            var pxcmStatus = PXCMHandData_IHand_QuerySegmentationImage(instance, out image1);
            image = pxcmStatus >= pxcmStatus.PXCM_STATUS_NO_ERROR ? new PXCMImage(image1, false) : null;
            return pxcmStatus;
        }

        public bool HasTrackedJoints() {
            return PXCMHandData_IHand_HasTrackedJoints(instance);
        }

        public bool HasNormalizedJoints() {
            return PXCMHandData_IHand_HasNormalizedJoints(instance);
        }

        public bool HasSegmentationImage() {
            return PXCMHandData_IHand_HasSegmentationImage(instance);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public class JointData {
        public int confidence;
        public PXCMPoint3DF32 positionWorld;
        public PXCMPoint3DF32 positionImage;
        public PXCMPoint4DF32 localRotation;
        public PXCMPoint4DF32 globalOrientation;
        public PXCMPoint3DF32 speed;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class ExtremityData {
        public PXCMPoint3DF32 pointWorld;
        public PXCMPoint3DF32 pointImage;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class FingerData {
        public int foldedness;
        public float radius;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class AlertData {
        public AlertType label;
        public int handId;
        public long timeStamp;
        public int frameNumber;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public class GestureData {
        public long timeStamp;
        public int handId;
        public GestureStateType state;
        public int frameNumber;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)] public string name;

        public GestureData() {
            name = "";
        }
    }
}