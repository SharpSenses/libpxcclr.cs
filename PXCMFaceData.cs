using System;
using System.Runtime.InteropServices;
using System.Text;

public class PXCMFaceData : PXCMBase
{
  public new const int CUID = 1413563462;
  public const int ALERT_NAME_SIZE = 30;

  internal PXCMFaceData(IntPtr instance, bool delete)
    : base(instance, delete)
  {
  }

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMFaceData_Update(IntPtr instance);

  [DllImport("libpxccpp2c")]
  internal static extern long PXCMFaceData_QueryFrameTimestamp(IntPtr instance);

  [DllImport("libpxccpp2c")]
  internal static extern int PXCMFaceData_QueryNumberOfDetectedFaces(IntPtr instance);

  [DllImport("libpxccpp2c")]
  internal static extern IntPtr PXCMFaceData_QueryFaceByID(IntPtr instance, int faceId);

  [DllImport("libpxccpp2c")]
  internal static extern IntPtr PXCMFaceData_QueryFaceByIndex(IntPtr instance, int index);

  [DllImport("libpxccpp2c")]
  internal static extern IntPtr PXCMFaceData_QueryFaces(IntPtr instance, out int numDetectedFaces);

  [DllImport("libpxccpp2c")]
  internal static extern IntPtr PXCMFaceData_QueryRecognitionModule(IntPtr instance);

  [DllImport("libpxccpp2c", CharSet = CharSet.Unicode)]
  private static extern pxcmStatus PXCMFaceData_QueryAlertNameByID(IntPtr instance, AlertData.AlertType alertEvent, StringBuilder outAlertName);

  internal static pxcmStatus QueryAlertNameByIDINT(IntPtr instance, AlertData.AlertType alertEvent, out string outAlertName)
  {
    StringBuilder outAlertName1 = new StringBuilder(30);
    pxcmStatus pxcmStatus = PXCMFaceData_QueryAlertNameByID(instance, alertEvent, outAlertName1);
    outAlertName = pxcmStatus >= pxcmStatus.PXCM_STATUS_NO_ERROR ? outAlertName1.ToString() : "";
    return pxcmStatus;
  }

  public Face[] QueryFaces()
  {
    int numDetectedFaces = 0;
    IntPtr source = PXCMFaceData_QueryFaces(instance, out numDetectedFaces);
    if (source == IntPtr.Zero)
      return null;
    IntPtr[] destination = new IntPtr[numDetectedFaces];
    Marshal.Copy(source, destination, 0, numDetectedFaces);
    Face[] faceArray = new Face[numDetectedFaces];
    for (int index = 0; index < numDetectedFaces; ++index)
      faceArray[index] = new Face(destination[index]);
    return faceArray;
  }

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMFaceData_QueryFiredAlertData(IntPtr instance, int index, [Out] AlertData alertData);

  public pxcmStatus QueryFiredAlertData(int index, out AlertData alertData)
  {
    alertData = new AlertData();
    return PXCMFaceData_QueryFiredAlertData(instance, index, alertData);
  }

  [DllImport("libpxccpp2c")]
  internal static extern int PXCMFaceData_QueryFiredAlertsNumber(IntPtr instance);

  [DllImport("libpxccpp2c")]
  [return: MarshalAs(UnmanagedType.Bool)]
  internal static extern bool PXCMFaceData_IsAlertFired(IntPtr instance, AlertData.AlertType alertEvent, [Out] AlertData alertData);

  public bool IsAlertFired(AlertData.AlertType alertEvent, out AlertData alertData)
  {
    alertData = new AlertData();
    return PXCMFaceData_IsAlertFired(instance, alertEvent, alertData);
  }

  [DllImport("libpxccpp2c")]
  [return: MarshalAs(UnmanagedType.Bool)]
  internal static extern bool PXCMFaceData_IsAlertFiredByFace(IntPtr instance, AlertData.AlertType alertEvent, int faceID, [Out] AlertData alertData);

  public bool IsAlertFiredByFace(AlertData.AlertType alertEvent, int faceID, out AlertData alertData)
  {
    alertData = new AlertData();
    return PXCMFaceData_IsAlertFiredByFace(instance, alertEvent, faceID, alertData);
  }

  public pxcmStatus Update()
  {
    return PXCMFaceData_Update(instance);
  }

  public long QueryFrameTimestamp()
  {
    return PXCMFaceData_QueryFrameTimestamp(instance);
  }

  public int QueryNumberOfDetectedFaces()
  {
    return PXCMFaceData_QueryNumberOfDetectedFaces(instance);
  }

  public Face QueryFaceByID(int faceId)
  {
    IntPtr instance = PXCMFaceData_QueryFaceByID(this.instance, faceId);
    if (!(instance != IntPtr.Zero))
      return null;
    return new Face(instance);
  }

  public Face QueryFaceByIndex(int index)
  {
    IntPtr instance = PXCMFaceData_QueryFaceByIndex(this.instance, index);
    if (!(instance != IntPtr.Zero))
      return null;
    return new Face(instance);
  }

  public RecognitionModuleData QueryRecognitionModule()
  {
    IntPtr instance = PXCMFaceData_QueryRecognitionModule(this.instance);
    if (instance == IntPtr.Zero)
      return null;
    return new RecognitionModuleData(instance);
  }

  public int QueryFiredAlertsNumber()
  {
    return PXCMFaceData_QueryFiredAlertsNumber(instance);
  }

  public pxcmStatus QueryAlertNameByID(AlertData.AlertType alertEvent, out string outAlertName)
  {
    return QueryAlertNameByIDINT(instance, alertEvent, out outAlertName);
  }

  [Serializable]
  [StructLayout(LayoutKind.Sequential)]
  public class DetectionData
  {
    private IntPtr instance;

    internal DetectionData(IntPtr instance)
    {
      this.instance = instance;
    }

    [DllImport("libpxccpp2c")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool PXCMFaceData_DetectionData_QueryFaceAverageDepth(IntPtr instance, out float outFaceAverageDepth);

    [DllImport("libpxccpp2c")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool PXCMFaceData_DetectionData_QueryBoundingRect(IntPtr instance, out PXCMRectI32 outBoundingRect);

    public bool QueryFaceAverageDepth(out float outFaceAverageDepth)
    {
      return PXCMFaceData_DetectionData_QueryFaceAverageDepth(instance, out outFaceAverageDepth);
    }

    public bool QueryBoundingRect(out PXCMRectI32 outBoundingRect)
    {
      return PXCMFaceData_DetectionData_QueryBoundingRect(instance, out outBoundingRect);
    }
  }

  [Serializable]
  [StructLayout(LayoutKind.Sequential)]
  public class LandmarksData
  {
    private IntPtr instance;

    internal LandmarksData(IntPtr instance)
    {
      this.instance = instance;
    }

    [DllImport("libpxccpp2c")]
    internal static extern int PXCMFaceData_LandmarksData_QueryNumPoints(IntPtr instance);

    [DllImport("libpxccpp2c")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool PXCMFaceData_LandmarksData_QueryPoints(IntPtr instance, IntPtr points);

    internal static bool QueryPointsINT(IntPtr instance, out LandmarkPoint[] points)
    {
      int length = PXCMFaceData_LandmarksData_QueryNumPoints(instance);
      IntPtr num1 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof (LandmarkPoint)) * length);
      bool flag = PXCMFaceData_LandmarksData_QueryPoints(instance, num1);
      if (flag)
      {
        points = new LandmarkPoint[length];
        int index = 0;
        int num2 = 0;
        while (index < length)
        {
          points[index] = new LandmarkPoint();
          Marshal.PtrToStructure(new IntPtr(num1.ToInt64() + num2), points[index]);
          ++index;
          num2 += Marshal.SizeOf(typeof (LandmarkPoint));
        }
      }
      else
        points = null;
      Marshal.FreeHGlobal(num1);
      return flag;
    }

    [DllImport("libpxccpp2c")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool PXCMFaceData_LandmarksData_QueryPoint(IntPtr instance, int index, [Out] LandmarkPoint outPoint);

    internal static bool QueryPointINT(IntPtr instance, int index, out LandmarkPoint point)
    {
      point = new LandmarkPoint();
      return PXCMFaceData_LandmarksData_QueryPoint(instance, index, point);
    }

    [DllImport("libpxccpp2c")]
    internal static extern int PXCMFaceData_LandmarksData_QueryNumPointsByGroup(IntPtr instance, LandmarksGroupType group);

    [DllImport("libpxccpp2c")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool PXCMFaceData_LandmarksData_QueryPointsByGroup(IntPtr instance, LandmarksGroupType groupFlags, IntPtr outPoints);

    internal static bool QueryPointsByGroupINT(IntPtr instance, LandmarksGroupType group, out LandmarkPoint[] points)
    {
      int length = PXCMFaceData_LandmarksData_QueryNumPointsByGroup(instance, group);
      IntPtr num1 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof (LandmarkPoint)) * length);
      bool flag = PXCMFaceData_LandmarksData_QueryPointsByGroup(instance, group, num1);
      if (flag)
      {
        points = new LandmarkPoint[length];
        int index = 0;
        int num2 = 0;
        while (index < length)
        {
          points[index] = new LandmarkPoint();
          Marshal.PtrToStructure(new IntPtr(num1.ToInt64() + num2), points[index]);
          ++index;
          num2 += Marshal.SizeOf(typeof (LandmarkPoint));
        }
      }
      else
        points = null;
      Marshal.FreeHGlobal(num1);
      return flag;
    }

    [DllImport("libpxccpp2c")]
    internal static extern int PXCMFaceData_LandmarksData_QueryPointIndex(IntPtr instance, LandmarkType name);

    public int QueryNumPoints()
    {
      return PXCMFaceData_LandmarksData_QueryNumPoints(instance);
    }

    public bool QueryPoints(out LandmarkPoint[] outPoints)
    {
      return QueryPointsINT(instance, out outPoints);
    }

    public bool QueryPoint(int index, out LandmarkPoint outPoint)
    {
      return QueryPointINT(instance, index, out outPoint);
    }

    public int QueryNumPointsByGroup(LandmarksGroupType groupFlags)
    {
      return PXCMFaceData_LandmarksData_QueryNumPointsByGroup(instance, groupFlags);
    }

    public bool QueryPointsByGroup(LandmarksGroupType groupFlags, out LandmarkPoint[] outPoints)
    {
      return QueryPointsByGroupINT(instance, groupFlags, out outPoints);
    }

    public int QueryPointIndex(LandmarkType name)
    {
      return PXCMFaceData_LandmarksData_QueryPointIndex(instance, name);
    }
  }

  [Serializable]
  [StructLayout(LayoutKind.Sequential)]
  public class PoseData
  {
    private IntPtr instance;

    internal PoseData(IntPtr instance)
    {
      this.instance = instance;
    }

    [DllImport("libpxccpp2c")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool PXCMFaceData_PoseData_QueryPoseAngles(IntPtr instance, [Out] PoseEulerAngles outPoseEulerAngles);

    internal static bool QueryPoseAnglesINT(IntPtr instance, out PoseEulerAngles outPoseEulerAngles)
    {
      outPoseEulerAngles = new PoseEulerAngles();
      return PXCMFaceData_PoseData_QueryPoseAngles(instance, outPoseEulerAngles);
    }

    [DllImport("libpxccpp2c")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool PXCMFaceData_PoseData_QueryPoseQuaternion(IntPtr instance, [Out] PoseQuaternion outPoseQuaternion);

    internal static bool QueryPoseQuaternionINT(IntPtr instance, out PoseQuaternion outPoseQuaternion)
    {
      outPoseQuaternion = new PoseQuaternion();
      return PXCMFaceData_PoseData_QueryPoseQuaternion(instance, outPoseQuaternion);
    }

    [DllImport("libpxccpp2c")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool PXCMFaceData_PoseData_QueryHeadPosition(IntPtr instance, [Out] HeadPosition outHeadPosition);

    internal static bool QueryHeadPositionINT(IntPtr instance, out HeadPosition outHeadPosition)
    {
      outHeadPosition = new HeadPosition();
      return PXCMFaceData_PoseData_QueryHeadPosition(instance, outHeadPosition);
    }

    [DllImport("libpxccpp2c")]
    internal static extern void PXCMFaceData_PoseData_QueryRotationMatrix(IntPtr instance, [Out] double[] outRotationMatrix);

    internal static void QueryRotationMatrixINT(IntPtr instance, out double[] outRotationMatrix)
    {
      outRotationMatrix = new double[9];
      PXCMFaceData_PoseData_QueryRotationMatrix(instance, outRotationMatrix);
    }

    [DllImport("libpxccpp2c")]
    internal static extern int PXCMFaceData_PoseData_QueryConfidence(IntPtr instance);

    public bool QueryPoseAngles(out PoseEulerAngles outPoseEulerAngles)
    {
      return QueryPoseAnglesINT(instance, out outPoseEulerAngles);
    }

    public bool QueryPoseQuaternion(out PoseQuaternion outPoseQuaternion)
    {
      return QueryPoseQuaternionINT(instance, out outPoseQuaternion);
    }

    public bool QueryHeadPosition(out HeadPosition outHeadPosition)
    {
      return QueryHeadPositionINT(instance, out outHeadPosition);
    }

    public void QueryRotationMatrix(out double[] outRotationMatrix)
    {
      QueryRotationMatrixINT(instance, out outRotationMatrix);
    }

    public int QueryConfidence()
    {
      return PXCMFaceData_PoseData_QueryConfidence(instance);
    }
  }

  public class Face
  {
    private IntPtr instance;

    internal Face(IntPtr instance)
    {
      this.instance = instance;
    }

    [DllImport("libpxccpp2c")]
    internal static extern int PXCMFaceData_Face_QueryUserID(IntPtr instance);

    [DllImport("libpxccpp2c")]
    internal static extern IntPtr PXCMFaceData_Face_Detection(IntPtr instance);

    [DllImport("libpxccpp2c")]
    internal static extern IntPtr PXCMFaceData_Face_QueryLandmarks(IntPtr instance);

    [DllImport("libpxccpp2c")]
    internal static extern IntPtr PXCMFaceData_Face_QueryPose(IntPtr instance);

    [DllImport("libpxccpp2c")]
    internal static extern IntPtr PXCMFaceData_Face_QueryExpressions(IntPtr instance);

    [DllImport("libpxccpp2c")]
    internal static extern IntPtr PXCMFaceData_Face_QueryRecognition(IntPtr instance);

    [DllImport("libpxccpp2c")]
    internal static extern IntPtr PXCMFaceData_Face_QueryPulse(IntPtr instance);

    public int QueryUserID()
    {
      return PXCMFaceData_Face_QueryUserID(instance);
    }

    public DetectionData QueryDetection()
    {
      IntPtr instance = PXCMFaceData_Face_Detection(this.instance);
      if (!(instance == IntPtr.Zero))
        return new DetectionData(instance);
      return null;
    }

    public LandmarksData QueryLandmarks()
    {
      IntPtr instance = PXCMFaceData_Face_QueryLandmarks(this.instance);
      if (!(instance == IntPtr.Zero))
        return new LandmarksData(instance);
      return null;
    }

    public PoseData QueryPose()
    {
      IntPtr instance = PXCMFaceData_Face_QueryPose(this.instance);
      if (!(instance == IntPtr.Zero))
        return new PoseData(instance);
      return null;
    }

    public ExpressionsData QueryExpressions()
    {
      IntPtr instance = PXCMFaceData_Face_QueryExpressions(this.instance);
      if (instance == IntPtr.Zero)
        return null;
      return new ExpressionsData(instance);
    }

    public RecognitionData QueryRecognition()
    {
      IntPtr instance = PXCMFaceData_Face_QueryRecognition(this.instance);
      if (instance == IntPtr.Zero)
        return null;
      return new RecognitionData(instance);
    }

    public PulseData QueryPulse()
    {
      IntPtr instance = PXCMFaceData_Face_QueryPulse(this.instance);
      if (instance == IntPtr.Zero)
        return null;
      return new PulseData(instance);
    }
  }

  public class ExpressionsData
  {
    private IntPtr instance;

    internal ExpressionsData(IntPtr instance)
    {
      this.instance = instance;
    }

    [DllImport("libpxccpp2c")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool PXCMFaceData_ExpressionsData_QueryExpression(IntPtr instance, FaceExpression au, [Out] FaceExpressionResult auResult);

    internal static bool QueryExpressionINT(IntPtr instance, FaceExpression au, out FaceExpressionResult auResult)
    {
      auResult = new FaceExpressionResult();
      return PXCMFaceData_ExpressionsData_QueryExpression(instance, au, auResult);
    }

    public bool QueryExpression(FaceExpression expression, out FaceExpressionResult expressionResult)
    {
      return QueryExpressionINT(instance, expression, out expressionResult);
    }

    public enum FaceExpression
    {
      EXPRESSION_BROW_RAISER_LEFT,
      EXPRESSION_BROW_RAISER_RIGHT,
      EXPRESSION_BROW_LOWERER_LEFT,
      EXPRESSION_BROW_LOWERER_RIGHT,
      EXPRESSION_SMILE,
      EXPRESSION_KISS,
      EXPRESSION_MOUTH_OPEN,
      EXPRESSION_EYES_CLOSED_LEFT,
      EXPRESSION_EYES_CLOSED_RIGHT,
      EXPRESSION_HEAD_TURN_LEFT,
      EXPRESSION_HEAD_TURN_RIGHT,
      EXPRESSION_HEAD_UP,
      EXPRESSION_HEAD_DOWN,
      EXPRESSION_HEAD_TILT_LEFT,
      EXPRESSION_HEAD_TILT_RIGHT,
      EXPRESSION_EYES_TURN_LEFT,
      EXPRESSION_EYES_TURN_RIGHT,
      EXPRESSION_EYES_UP,
      EXPRESSION_EYES_DOWN,
      EXPRESSION_TONGUE_OUT,
      EXPRESSION_PUFF_RIGHT,
      EXPRESSION_PUFF_LEFT
    }

    [StructLayout(LayoutKind.Sequential)]
    public class FaceExpressionResult
    {
      public int intensity;
      internal Reserved reserved;
    }
  }

  public class RecognitionData
  {
    private IntPtr instance;

    internal RecognitionData(IntPtr instance)
    {
      this.instance = instance;
    }

    [DllImport("libpxccpp2c")]
    internal static extern int PXCMFaceData_RecognitionData_RegisterUser(IntPtr instance);

    [DllImport("libpxccpp2c")]
    internal static extern void PXCMFaceData_RecognitionData_UnregisterUser(IntPtr instance);

    [DllImport("libpxccpp2c")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool PXCMFaceData_RecognitionData_IsRegistered(IntPtr instance);

    [DllImport("libpxccpp2c")]
    internal static extern int PXCMFaceData_RecognitionData_QueryUserID(IntPtr instance);

    public int RegisterUser()
    {
      return PXCMFaceData_RecognitionData_RegisterUser(instance);
    }

    public void UnregisterUser()
    {
      PXCMFaceData_RecognitionData_UnregisterUser(instance);
    }

    public bool IsRegistered()
    {
      return PXCMFaceData_RecognitionData_IsRegistered(instance);
    }

    public int QueryUserID()
    {
      return PXCMFaceData_RecognitionData_QueryUserID(instance);
    }
  }

  public class RecognitionModuleData
  {
    private IntPtr instance;

    internal RecognitionModuleData(IntPtr instance)
    {
      this.instance = instance;
    }

    [DllImport("libpxccpp2c")]
    internal static extern int PXCMFaceData_RecognitionModuleData_QueryDatabaseSize(IntPtr instance);

    [DllImport("libpxccpp2c")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool PXCMFaceData_RecognitionModuleData_QueryDatabaseBuffer(IntPtr instance, [Out] byte[] buffer);

    [DllImport("libpxccpp2c")]
    internal static extern void PXCMFaceData_RecognitionModuleData_UnregisterUserByID(IntPtr instance, int userId);

    public int QueryDatabaseSize()
    {
      return PXCMFaceData_RecognitionModuleData_QueryDatabaseSize(instance);
    }

    public bool QueryDatabaseBuffer(byte[] buffer)
    {
      return PXCMFaceData_RecognitionModuleData_QueryDatabaseBuffer(instance, buffer);
    }

    public void UnregisterUserByID(int userId)
    {
      PXCMFaceData_RecognitionModuleData_UnregisterUserByID(instance, userId);
    }
  }

  public class PulseData
  {
    private IntPtr instance;

    internal PulseData(IntPtr instance)
    {
      this.instance = instance;
    }

    [DllImport("libpxccpp2c")]
    internal static extern float PXCMFaceData_PulseData_QueryHeartRate(IntPtr instance);

    public float QueryHeartRate()
    {
      return PXCMFaceData_PulseData_QueryHeartRate(instance);
    }
  }

  [Flags]
  public enum LandmarkType
  {
    LANDMARK_NOT_NAMED = 0,
    LANDMARK_EYE_RIGHT_CENTER = 1,
    LANDMARK_EYE_LEFT_CENTER = 2,
    LANDMARK_EYELID_RIGHT_TOP = LANDMARK_EYE_LEFT_CENTER | LANDMARK_EYE_RIGHT_CENTER,
    LANDMARK_EYELID_RIGHT_BOTTOM = 4,
    LANDMARK_EYELID_RIGHT_RIGHT = LANDMARK_EYELID_RIGHT_BOTTOM | LANDMARK_EYE_RIGHT_CENTER,
    LANDMARK_EYELID_RIGHT_LEFT = LANDMARK_EYELID_RIGHT_BOTTOM | LANDMARK_EYE_LEFT_CENTER,
    LANDMARK_EYELID_LEFT_TOP = LANDMARK_EYELID_RIGHT_LEFT | LANDMARK_EYE_RIGHT_CENTER,
    LANDMARK_EYELID_LEFT_BOTTOM = 8,
    LANDMARK_EYELID_LEFT_RIGHT = LANDMARK_EYELID_LEFT_BOTTOM | LANDMARK_EYE_RIGHT_CENTER,
    LANDMARK_EYELID_LEFT_LEFT = LANDMARK_EYELID_LEFT_BOTTOM | LANDMARK_EYE_LEFT_CENTER,
    LANDMARK_EYEBROW_RIGHT_CENTER = LANDMARK_EYELID_LEFT_LEFT | LANDMARK_EYE_RIGHT_CENTER,
    LANDMARK_EYEBROW_RIGHT_RIGHT = LANDMARK_EYELID_LEFT_BOTTOM | LANDMARK_EYELID_RIGHT_BOTTOM,
    LANDMARK_EYEBROW_RIGHT_LEFT = LANDMARK_EYEBROW_RIGHT_RIGHT | LANDMARK_EYE_RIGHT_CENTER,
    LANDMARK_EYEBROW_LEFT_CENTER = LANDMARK_EYEBROW_RIGHT_RIGHT | LANDMARK_EYE_LEFT_CENTER,
    LANDMARK_EYEBROW_LEFT_RIGHT = LANDMARK_EYEBROW_LEFT_CENTER | LANDMARK_EYE_RIGHT_CENTER,
    LANDMARK_EYEBROW_LEFT_LEFT = 16,
    LANDMARK_NOSE_TIP = LANDMARK_EYEBROW_LEFT_LEFT | LANDMARK_EYE_RIGHT_CENTER,
    LANDMARK_NOSE_TOP = LANDMARK_EYEBROW_LEFT_LEFT | LANDMARK_EYE_LEFT_CENTER,
    LANDMARK_NOSE_BOTTOM = LANDMARK_NOSE_TOP | LANDMARK_EYE_RIGHT_CENTER,
    LANDMARK_NOSE_RIGHT = LANDMARK_EYEBROW_LEFT_LEFT | LANDMARK_EYELID_RIGHT_BOTTOM,
    LANDMARK_NOSE_LEFT = LANDMARK_NOSE_RIGHT | LANDMARK_EYE_RIGHT_CENTER,
    LANDMARK_LIP_RIGHT = LANDMARK_NOSE_RIGHT | LANDMARK_EYE_LEFT_CENTER,
    LANDMARK_LIP_LEFT = LANDMARK_LIP_RIGHT | LANDMARK_EYE_RIGHT_CENTER,
    LANDMARK_UPPER_LIP_CENTER = LANDMARK_EYEBROW_LEFT_LEFT | LANDMARK_EYELID_LEFT_BOTTOM,
    LANDMARK_UPPER_LIP_RIGHT = LANDMARK_UPPER_LIP_CENTER | LANDMARK_EYE_RIGHT_CENTER,
    LANDMARK_UPPER_LIP_LEFT = LANDMARK_UPPER_LIP_CENTER | LANDMARK_EYE_LEFT_CENTER,
    LANDMARK_LOWER_LIP_CENTER = LANDMARK_UPPER_LIP_LEFT | LANDMARK_EYE_RIGHT_CENTER,
    LANDMARK_LOWER_LIP_RIGHT = LANDMARK_UPPER_LIP_CENTER | LANDMARK_EYELID_RIGHT_BOTTOM,
    LANDMARK_LOWER_LIP_LEFT = LANDMARK_LOWER_LIP_RIGHT | LANDMARK_EYE_RIGHT_CENTER,
    LANDMARK_FACE_BORDER_TOP_RIGHT = LANDMARK_LOWER_LIP_RIGHT | LANDMARK_EYE_LEFT_CENTER,
    LANDMARK_FACE_BORDER_TOP_LEFT = LANDMARK_FACE_BORDER_TOP_RIGHT | LANDMARK_EYE_RIGHT_CENTER,
    LANDMARK_CHIN = 32
  }

  [Flags]
  public enum LandmarksGroupType
  {
    LANDMARK_GROUP_LEFT_EYE = 1,
    LANDMARK_GROUP_RIGHT_EYE = 2,
    LANDMARK_GROUP_RIGHT_EYEBROW = 4,
    LANDMARK_GROUP_LEFT_EYEBROW = 8,
    LANDMARK_GROUP_NOSE = 16,
    LANDMARK_GROUP_MOUTH = 32,
    LANDMARK_GROUP_JAW = 64
  }

  [Serializable]
  [StructLayout(LayoutKind.Sequential, Size = 40)]
  internal struct Reserved
  {
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

  [Serializable]
  [StructLayout(LayoutKind.Sequential)]
  public class LandmarkPointSource
  {
    public int index;
    public LandmarkType alias;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    internal int[] reserved;

    public LandmarkPointSource()
    {
      reserved = new int[10];
    }
  }

  [Serializable]
  [StructLayout(LayoutKind.Sequential)]
  public class LandmarkPoint
  {
    public LandmarkPointSource source;
    public int confidenceImage;
    public int confidenceWorld;
    public PXCMPoint3DF32 world;
    public PXCMPointF32 image;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public int[] reserved;

    public LandmarkPoint()
    {
      reserved = new int[10];
    }
  }

  [Serializable]
  [StructLayout(LayoutKind.Sequential)]
  public class HeadPosition
  {
    public PXCMPoint3DF32 headCenter;
    public int confidence;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
    internal int[] reserved;

    public HeadPosition()
    {
      reserved = new int[9];
    }
  }

  [Serializable]
  [StructLayout(LayoutKind.Sequential)]
  public class PoseEulerAngles
  {
    public float yaw;
    public float pitch;
    public float roll;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    internal int[] reserved;

    public PoseEulerAngles()
    {
      reserved = new int[10];
    }
  }

  [Serializable]
  [StructLayout(LayoutKind.Sequential)]
  public class PoseQuaternion
  {
    public float x;
    public float y;
    public float z;
    public float w;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    internal int[] reserved;

    public PoseQuaternion()
    {
      reserved = new int[10];
    }
  }

  [StructLayout(LayoutKind.Sequential)]
  public class AlertData
  {
    public AlertType label;
    public long timeStamp;
    public int faceId;
    internal Reserved reserved;

    [Flags]
    public enum AlertType
    {
      ALERT_NEW_FACE_DETECTED = 1,
      ALERT_FACE_OUT_OF_FOV = 2,
      ALERT_FACE_BACK_TO_FOV = ALERT_FACE_OUT_OF_FOV | ALERT_NEW_FACE_DETECTED,
      ALERT_FACE_OCCLUDED = 4,
      ALERT_FACE_NO_LONGER_OCCLUDED = ALERT_FACE_OCCLUDED | ALERT_NEW_FACE_DETECTED,
      ALERT_FACE_LOST = ALERT_FACE_OCCLUDED | ALERT_FACE_OUT_OF_FOV
    }
  }
}
