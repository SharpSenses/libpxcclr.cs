/*******************************************************************************

  INTEL CORPORATION PROPRIETARY INFORMATION
  This software is supplied under the terms of a license agreement or non-disclosure
  agreement with Intel Corporation and may not be copied or disclosed except in
  accordance with the terms of that agreement
  Copyright(c) 2014 Intel Corporation. All Rights Reserved.

 *******************************************************************************/

using System;
using System.Runtime.InteropServices;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

public partial class PXCMFaceData : PXCMBase
{
    new public const Int32 CUID = 0x54414446;
    public const Int32 ALERT_NAME_SIZE = 30;

    [Flags]
    public enum LandmarkType
    {
        LANDMARK_NOT_NAMED = 0,

        LANDMARK_EYE_RIGHT_CENTER,
        LANDMARK_EYE_LEFT_CENTER,

        LANDMARK_EYELID_RIGHT_TOP,
        LANDMARK_EYELID_RIGHT_BOTTOM,
        LANDMARK_EYELID_RIGHT_RIGHT,
        LANDMARK_EYELID_RIGHT_LEFT,

        LANDMARK_EYELID_LEFT_TOP,
        LANDMARK_EYELID_LEFT_BOTTOM,
        LANDMARK_EYELID_LEFT_RIGHT,
        LANDMARK_EYELID_LEFT_LEFT,

        LANDMARK_EYEBROW_RIGHT_CENTER,
        LANDMARK_EYEBROW_RIGHT_RIGHT,
        LANDMARK_EYEBROW_RIGHT_LEFT,

        LANDMARK_EYEBROW_LEFT_CENTER,
        LANDMARK_EYEBROW_LEFT_RIGHT,
        LANDMARK_EYEBROW_LEFT_LEFT,

        LANDMARK_NOSE_TIP,
        LANDMARK_NOSE_TOP,
        LANDMARK_NOSE_BOTTOM,
        LANDMARK_NOSE_RIGHT,
        LANDMARK_NOSE_LEFT,

        LANDMARK_LIP_RIGHT,
        LANDMARK_LIP_LEFT,

        LANDMARK_UPPER_LIP_CENTER,
        LANDMARK_UPPER_LIP_RIGHT,
        LANDMARK_UPPER_LIP_LEFT,

        LANDMARK_LOWER_LIP_CENTER,
        LANDMARK_LOWER_LIP_RIGHT,
        LANDMARK_LOWER_LIP_LEFT,

        LANDMARK_FACE_BORDER_TOP_RIGHT,
        LANDMARK_FACE_BORDER_TOP_LEFT,

        LANDMARK_CHIN,
    }

    [Flags]
    public enum LandmarksGroupType
    {
        LANDMARK_GROUP_LEFT_EYE = 0x0001,
        LANDMARK_GROUP_RIGHT_EYE = 0x0002,
        LANDMARK_GROUP_RIGHT_EYEBROW = 0x0004,
        LANDMARK_GROUP_LEFT_EYEBROW = 0x0008,
        LANDMARK_GROUP_NOSE = 0x0010,
        LANDMARK_GROUP_MOUTH = 0x0020,
        LANDMARK_GROUP_JAW = 0x0040,
    };

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Size = 40)]
    internal struct Reserved
    {
        internal Int32 res1, res2, res3, res4, res5, res6, res7, res8, res9, res10;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class LandmarkPointSource
    {
        public Int32 index;
        public LandmarkType alias;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        internal Int32[] reserved;

        public LandmarkPointSource()
        {
            reserved = new Int32[10];
        }
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class LandmarkPoint
    {
        public LandmarkPointSource source;
        public Int32 confidenceImage;
        public Int32 confidenceWorld;
        public PXCMPoint3DF32 world;
        public PXCMPointF32 image;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public Int32[] reserved;

        public LandmarkPoint()
        {
            reserved = new Int32[10];
        }
    }


    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class HeadPosition
    {
        public PXCMPoint3DF32 headCenter;
        public Int32 confidence;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        internal Int32[] reserved;

        public HeadPosition()
        {
            reserved = new Int32[9];
        }
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class PoseEulerAngles
    {
        public Single yaw;
        public Single pitch;
        public Single roll;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        internal Int32[] reserved;

        public PoseEulerAngles()
        {
            reserved = new Int32[10];
        }
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class PoseQuaternion
    {
        public Single x, y, z, w;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        internal Int32[] reserved;

        public PoseQuaternion()
        {
            reserved = new Int32[10];
        }
    }


    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial class DetectionData
    {
        /// <summary>
        /// Assigns average depth of detected face to outFaceAverageDepth, 
        ///returns true if data and outFaceAverageDepth exists, false otherwise. 
        /// </summary>
        public Boolean QueryFaceAverageDepth(out Single outFaceAverageDepth)
        {
            return PXCMFaceData_DetectionData_QueryFaceAverageDepth(instance, out outFaceAverageDepth);
        }

        /// <summary>
        ///  Assigns 2D bounding rectangle of detected face to outBoundingRect, 
        /// returns true if data and outBoundingRect exists, false otherwise.
        /// </summary>
        public Boolean QueryBoundingRect(out PXCMRectI32 outBoundingRect)
        {
            return PXCMFaceData_DetectionData_QueryBoundingRect(instance, out outBoundingRect);
        }

        private IntPtr instance;
        internal DetectionData(IntPtr instance)
        {
            this.instance = instance;
        }
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial class LandmarksData
    {
        /// <summary>
        ///  Returns the number of tracked landmarks.
        /// </summary>
        public Int32 QueryNumPoints()
        {
            return PXCMFaceData_LandmarksData_QueryNumPoints(instance);
        }

        /// <summary>
        ///  Assigns all the points to outNumPoints array.
        /// Returns true if data and parameters exists, false otherwise.
        /// </summary>
        public Boolean QueryPoints(out LandmarkPoint[] outPoints)
        {
            return QueryPointsINT(instance, out outPoints);
        }

        /// <summary>
        ///  Assigns point matched to index to outPoint.
        /// Returns true if data and outPoint exists and index is correct, false otherwise.
        /// </summary>
        public Boolean QueryPoint(Int32 index, out LandmarkPoint outPoint)
        {
            return QueryPointINT(instance, index, out outPoint);
        }

        /// <summary>
        ///  Returns the number of tracked landmarks in groupFlags.
        /// </summary>
        public Int32 QueryNumPointsByGroup(LandmarksGroupType groupFlags)
        {
            return PXCMFaceData_LandmarksData_QueryNumPointsByGroup(instance, groupFlags);
        }

        /// <summary>
        ///  Assigns points matched to groupFlags to outPoints.
        /// User is expected to allocate outPoints to size bigger than the group size - point contains the original source (index + name).
        /// Returns true if data and parameters exist, false otherwise.
        /// </summary>
        public Boolean QueryPointsByGroup(LandmarksGroupType groupFlags, out LandmarkPoint[] outPoints)
        {
            return QueryPointsByGroupINT(instance, groupFlags, out outPoints);
        }

        /// <summary>
        ///  Mapping function -> retrieves index corresponding to landmark's name.
        /// </summary>
        public Int32 QueryPointIndex(LandmarkType name)
        {
            return PXCMFaceData_LandmarksData_QueryPointIndex(instance, name);
        }

        private IntPtr instance;
        internal LandmarksData(IntPtr instance)
        {
            this.instance = instance;
        }
    };

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial class PoseData
    {
        /// <summary>
        ///  Assigns pose Euler angles to outPoseEulerAngles.
        /// Returns true if data and parameters exist, false otherwise.
        /// </summary>
        public Boolean QueryPoseAngles(out PoseEulerAngles outPoseEulerAngles)
        {
            return QueryPoseAnglesINT(instance, out outPoseEulerAngles);
        }

        /// <summary>
        ///  Assigns pose rotation as quaternion to outPoseQuaternion.
        /// Returns true if data and parameters exist, false otherwise.
        /// </summary>
        public Boolean QueryPoseQuaternion(out PoseQuaternion outPoseQuaternion)
        {
            return QueryPoseQuaternionINT(instance, out outPoseQuaternion);
        }

        /// <summary>
        ///  Assigns the head position to outHeadPosition.
        /// Returns true if data and parameters exist, false otherwise.
        /// </summary>
        public Boolean QueryHeadPosition(out HeadPosition outHeadPosition)
        {
            return QueryHeadPositionINT(instance, out outHeadPosition);
        }

        /// <summary>
        ///  Assigns 3x3 face's rotation matrix to outRotationMatrix.
        /// Returns true if data and parameters exist, false otherwise.
        /// </summary>
        public void QueryRotationMatrix(out Double[] outRotationMatrix)
        {
            QueryRotationMatrixINT(instance, out outRotationMatrix);
        }

        /// <summary>
        ///  Returns position(angle) confidence
        /// </summary>
        public Int32 QueryConfidence()
        {
            return PXCMFaceData_PoseData_QueryConfidence(instance);
        }

        private IntPtr instance;
        internal PoseData(IntPtr instance)
        {
            this.instance = instance;
        }
    }

    public partial class ExpressionsData
    {
        public enum FaceExpression
        {
            EXPRESSION_BROW_RAISER_LEFT = 0,
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
            public Int32 intensity;
            internal Reserved reserved;
        }

        /// <summary>
        /// Queries single expression result
        /// </summary>
        /// <param name="expression">Single expression</param>
        /// <param name="expressionResult">Expression result - such as intensity</param>
        /// <returns>Returns true if expression was calculated successfully, false otherwise.</returns>
        public Boolean QueryExpression(FaceExpression expression, out FaceExpressionResult expressionResult)
        {
            return QueryExpressionINT(instance, expression, out expressionResult);
        }

        private IntPtr instance;
        internal ExpressionsData(IntPtr instance)
        {
            this.instance = instance;
        }
    }

    public partial class RecognitionData
    {
        /// <summary> 
        /// Register a user in the Recognition database.
        /// </summary>
        /// <returns> The unique user ID assigned to the registered face by the Recognition module.</returns>
        public Int32 RegisterUser()
        {
            return PXCMFaceData_RecognitionData_RegisterUser(instance);
        }

        /// <summary> 
        /// Removes a user from the Recognition database.
        /// </summary>
        public void UnregisterUser()
        {
            PXCMFaceData_RecognitionData_UnregisterUser(instance);
        }

        /// <summary> 
        /// Checks if a user is registered in the Recognition database.
        /// </summary>
        /// <returns> true - if user is in the database, false otherwise.</returns>
        public Boolean IsRegistered()
        {
            return PXCMFaceData_RecognitionData_IsRegistered(instance);
        }
        /// <summary> 
        /// Returns the ID assigned to the current face by the Recognition module
        /// </summary>
        /// <returns> The ID assigned by the Recognition module, or -1 if face was not recognized.</returns>
        public Int32 QueryUserID()
        {
            return PXCMFaceData_RecognitionData_QueryUserID(instance);
        }


        private IntPtr instance;
        internal RecognitionData(IntPtr instance)
        {
            this.instance = instance;
        }
    }
    public partial class RecognitionModuleData
    {
        public Int32 QueryDatabaseSize()
        {
            return PXCMFaceData_RecognitionModuleData_QueryDatabaseSize(instance);
        }

        public Boolean QueryDatabaseBuffer(Byte[] buffer)
        {
            return PXCMFaceData_RecognitionModuleData_QueryDatabaseBuffer(instance, buffer);
        }

        public void UnregisterUserByID(Int32 userId)
        {
            PXCMFaceData_RecognitionModuleData_UnregisterUserByID(instance, userId);
        }

        private IntPtr instance;
        internal RecognitionModuleData(IntPtr instance)
        {
            this.instance = instance;
        }
    }

    public partial class PulseData
    {
        public float QueryHeartRate()
        {
            return PXCMFaceData_PulseData_QueryHeartRate(instance);
        }

        private IntPtr instance;
        internal PulseData(IntPtr instance)
        {
            this.instance = instance;
        }
    }

    public partial class Face
    {
        /// <summary>
        ///  Returns user ID.
        /// </summary>
        public Int32 QueryUserID()
        {
            return PXCMFaceData_Face_QueryUserID(instance);
        }

        /// <summary>
        ///  Returns user's detection data instance - null if it is not enabled.
        /// </summary>
        public DetectionData QueryDetection()
        {
            IntPtr instance2 = PXCMFaceData_Face_Detection(instance);
            return instance2 == IntPtr.Zero ? null : new DetectionData(instance2);
        }

        /// <summary>
        ///  Returns user's landmarks data instance - null if it is not enabled.
        /// </summary>
        public LandmarksData QueryLandmarks()
        {
            IntPtr instance2 = PXCMFaceData_Face_QueryLandmarks(instance);
            return instance2 == IntPtr.Zero ? null : new LandmarksData(instance2);
        }

        /// <summary>
        ///  Returns user's pose data - null if it is not enabled.
        /// </summary>
        public PoseData QueryPose()
        {
            IntPtr instance2 = PXCMFaceData_Face_QueryPose(instance);
            return instance2 == IntPtr.Zero ? null : new PoseData(instance2);
        }

        /// <summary>
        ///  Returns user's expressions data - null if it not enabled.
        /// </summary>
        public ExpressionsData QueryExpressions()
        {
            IntPtr data = PXCMFaceData_Face_QueryExpressions(instance);
            if (data == IntPtr.Zero) return null;
            return new ExpressionsData(data);
        }

        /// <summary>
        ///  Returns user's recognition data - null if it is not enabled.
        /// </summary>
        public RecognitionData QueryRecognition()
        {
            IntPtr data = PXCMFaceData_Face_QueryRecognition(instance);
            if (data == IntPtr.Zero) return null;
            return new RecognitionData(data);
        }

        public PulseData QueryPulse()
        {
            IntPtr data = PXCMFaceData_Face_QueryPulse(instance);
            if (data == IntPtr.Zero) return null;
            return new PulseData(data);
        }

        /// <summary>
        ///  Returns user's gaze data - null if it is not enabled.
        /// </summary>
        public GazeData QueryGaze()
        {
            IntPtr gaze = PXCMFaceData_Face_QueryGaze(instance);
            if (gaze == IntPtr.Zero) return null;
            return new GazeData(gaze);
        }

        /// <summary>
        ///  Returns user's gaze calibration data - null if it is not enabled.
        /// </summary>
        public GazeCalibData QueryGazeCalibration()
        {
            IntPtr calib = PXCMFaceData_Face_QueryGazeCalibration(instance);
            if (calib == IntPtr.Zero) return null;
            return new GazeCalibData(calib);
        }

        private IntPtr instance;
        internal Face(IntPtr instance)
        {
            this.instance = instance;
        }
    }

    /// <summary>
    ///  Updates data to latest available output.
    /// </summary>
    public pxcmStatus Update()
    {
        return PXCMFaceData_Update(instance);
    }

    /// <summary>
    ///  Returns detected frame timestamp.
    /// </summary>
    public Int64 QueryFrameTimestamp()
    {
        return PXCMFaceData_QueryFrameTimestamp(instance);
    }

    /// <summary>
    ///  Returns number of total detected faces in frame.
    /// </summary>
    public Int32 QueryNumberOfDetectedFaces()
    {
        return PXCMFaceData_QueryNumberOfDetectedFaces(instance);
    }

    /// <summary>
    ///  Returns tracked face corresponding with the algorithm-assigned faceId, null if no such faceId exists.
    /// </summary>
    public Face QueryFaceByID(Int32 faceId)
    {
        IntPtr face2 = PXCMFaceData_QueryFaceByID(instance, faceId);
        return (face2 != IntPtr.Zero) ? new Face(face2) : null;
    }

    /// <summary>
    ///  Returns tracked face corresponding with the given index, 0 being the first according the to chosen tracking strategy;
    /// Returns null if no such index exists.
    /// </summary>
    public Face QueryFaceByIndex(Int32 index)
    {
        IntPtr face2 = PXCMFaceData_QueryFaceByIndex(instance, index);
        return (face2 != IntPtr.Zero) ? new Face(face2) : null;
    }

    public RecognitionModuleData QueryRecognitionModule()
    {
        IntPtr data = PXCMFaceData_QueryRecognitionModule(instance);
        if (data == IntPtr.Zero) return null;
        return new RecognitionModuleData(data);
    }

    [StructLayout(LayoutKind.Sequential)]
    public class AlertData
    {
        /// <summary>  AlertType
        ///  Available events that can be detected by the system (alert types) 
        /// </summary>
        [Flags]
        public enum AlertType
        {
            ALERT_NEW_FACE_DETECTED = 1,        //  thrown when a new face enters the FOV and its position and bounding rectangle is available. 
            ALERT_FACE_OUT_OF_FOV,          //  thrown when a new face (even slightly) out of field of view. 
            ALERT_FACE_BACK_TO_FOV,          //  thrown when a tracked face is back fully to field of view. 
            ALERT_FACE_OCCLUDED,             //  thrown when face is occluded (even slightly) by any object or hand.
            ALERT_FACE_NO_LONGER_OCCLUDED,  //  thrown when face is not occluded by any object or hand. 		
            ALERT_FACE_LOST                  //  thrown when an existing face leaves the FOV and its position and bounding rectangle become unavailable.
        }

        public AlertType label;	    	// The label that identifies this alert/// </summary>
        public Int64 timeStamp;		// The time-stamp in which the event occurred
        public Int32 faceId;	    	// The ID of the relevant face, if relevant and known
        internal Reserved reserved;
    }

    /// <summary>
    /// Get the number of fired alerts in the current frame.
    /// </summary>
    /// <returns> the number of fired alerts.</returns>
    public Int32 QueryFiredAlertsNumber()
    {
        return PXCMFaceData_QueryFiredAlertsNumber(instance);
    }

    /// <summary>
    /// Return whether the specified alert is fired for a specific face in the current frame, and retrieve its data.
    /// </summary>
    /// <param name="alertEvent"> the label of the alert event.</param>
    /// <param name="outAlertName"> parameter to contain the name of the alert,maximum size - ALERT_NAME_SIZE</param>
    /// @see AlertData
    /// <returns> PXC_STATUS_NO_ERROR if returning the alert's name was successful; otherwise, return one of the following errors:
    /// PXCM_STATUS_PARAM_UNSUPPORTED - if outAlertName is null.
    /// PXCM_STATUS_DATA_UNAVAILABLE - if no alert corresponding to alertEvent was found.</returns>
    public pxcmStatus QueryAlertNameByID(AlertData.AlertType alertEvent, out String outAlertName)
    {
        return QueryAlertNameByIDINT(instance, alertEvent, out outAlertName);
    }

    [StructLayout(LayoutKind.Sequential)]
    public class GazePoint
    {
        public PXCMPointI32 screenPoint;
        public Int32 confidence;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        internal Int32[] reserved;

        public GazePoint()
        {
            reserved = new Int32[10];
        }
    };

    public partial class GazeData
    {
        /// <summary>
        ///  Assigns gaze result to outGazeResult.
        /// Returns true if data and parameters exist, false otherwise.
        /// </summary>
        public GazePoint QueryGazePoint()
        {
            GazePoint gp = new GazePoint();
            PXCMFaceData_GazeData_QueryGazePoint(instance, gp);
            return gp;
        }

        /// <summary>
        ///  Return gaze horizontal angle in degrees.
        /// </summary>
        public Double QueryGazeHorizontalAngle()
        {
            return PXCMFaceData_GazeData_QueryGazeHorizontalAngle(instance);
        }

        /// <summary>
        ///  Return gaze vertical angle in degrees.
        /// </summary>
        public Double QueryGazeVerticalAngle()
        {
            return PXCMFaceData_GazeData_QueryGazeVerticalAngle(instance);
        }

        private IntPtr instance;

        internal GazeData(IntPtr instance)
        {
            this.instance = instance;
        }
    }

    public partial class GazeCalibData
    {
        public enum CalibrationState : int
        {
            CALIBRATION_IDLE = 0,
            CALIBRATION_NEW_POINT,
            CALIBRATION_SAME_POINT,
            CALIBRATION_DONE
        };

        public enum CalibrationStatus : int
        {
            CALIBRATION_STATUS_SUCCESS = 0,
            CALIBRATION_STATUS_FAIR,
            CALIBRATION_STATUS_POOR,
            CALIBRATION_STATUS_FAILED
        };

        public enum DominantEye
        {
            DOMINANT_RIGHT_EYE = 0,
            DOMINANT_LEFT_EYE,
            DOMINANT_BOTH_EYES		// A state for averaging both eyes, currently not supported.
        };

        /// <summary>
        ///  Assigns gaze result to outGazeResult.
        /// Returns true if data and parameters exist, false otherwise.
        /// </summary>
        public CalibrationState QueryCalibrationState()
        {
            return PXCMFaceData_GazeCalibData_QueryCalibrationState(instance);
        }

        /// <summary>
        ///  Assigns gaze result to outGazeResult.
        /// Returns true if data and parameters exist, false otherwise.
        /// </summary>
        public PXCMPointI32 QueryCalibPoint()
        {
            PXCMPointI32 cp = new PXCMPointI32();
            PXCMFaceData_GazeCalibData_QueryClibPoint(instance, ref cp);
            return cp;
        }

        /// <summary>
        /// retrieves calib data size
        /// </summary>
        public Int32 QueryCalibDataSize()
        {
            return PXCMFaceData_GazeCalibData_QueryCalibDataSize(instance);
        }

        /// <summary>
        /// retrieves calib data buffer
        /// </summary>
        public CalibrationStatus QueryCalibData(out Byte[] buffer)
        {
            buffer = null;
            Int32 size = QueryCalibDataSize();
            if (size <= 0) return CalibrationStatus.CALIBRATION_STATUS_FAILED;

            buffer = new byte[size];
            return PXCMFaceData_GazeCalibData_QueryCalibData(instance, buffer);
        }

        /// <summary>
        /// The optimal eye of the current calibration - the one which yielded the highest accuracy between the two eyes, 
        /// aiming at hitting the user's dominant eye; Unless the user requested set of the dominant eye.
        /// This is the eye relied on in the gaze inference algorithm.				
        /// </summary>
        public DominantEye QueryCalibDominantEye()
        {
            return PXCMFaceData_GazeCalibData_QueryCalibDominantEye(instance);
        }

        private IntPtr instance;
        internal GazeCalibData(IntPtr instance)
        {
            this.instance = instance;
        }
    }

    internal PXCMFaceData(IntPtr instance, Boolean delete)
        : base(instance, delete)
    {
    }
};

#if RSSDK_IN_NAMESPACE
}
#endif
