/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or non-disclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2014 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Runtime.InteropServices;
using System.Text;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

    public partial class PXCMFaceData : PXCMBase
    {
        public partial class DetectionData
        {
            [DllImport(DLLNAME)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern Boolean PXCMFaceData_DetectionData_QueryFaceAverageDepth(IntPtr instance, out Single outFaceAverageDepth);

            [DllImport(DLLNAME)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern Boolean PXCMFaceData_DetectionData_QueryBoundingRect(IntPtr instance, out PXCMRectI32 outBoundingRect);
        };

        public partial class LandmarksData
        {
            [DllImport(DLLNAME)]
            internal static extern Int32 PXCMFaceData_LandmarksData_QueryNumPoints(IntPtr instance);

            [DllImport(DLLNAME)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern Boolean PXCMFaceData_LandmarksData_QueryPoints(IntPtr instance, IntPtr points);

            internal static Boolean QueryPointsINT(IntPtr instance, out LandmarkPoint[] points)
            {
                int npoints = PXCMFaceData_LandmarksData_QueryNumPoints(instance);
                IntPtr points2 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(LandmarkPoint)) * npoints);
                Boolean sts = PXCMFaceData_LandmarksData_QueryPoints(instance, points2);
                if (sts)
                {
                    points = new LandmarkPoint[npoints];
                    for (int i = 0, j = 0; i < npoints; i++, j += Marshal.SizeOf(typeof(LandmarkPoint)))
                    {
                        points[i] = new LandmarkPoint();
                        Marshal.PtrToStructure(new IntPtr(points2.ToInt64() + j), points[i]);
                    }
                }
                else
                {
                    points = null;
                }
                Marshal.FreeHGlobal(points2);
                return sts;
            }

            [DllImport(DLLNAME)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern Boolean PXCMFaceData_LandmarksData_QueryPoint(IntPtr instance, Int32 index, [Out] LandmarkPoint outPoint);

            internal static Boolean QueryPointINT(IntPtr instance, Int32 index, out LandmarkPoint point)
            {
                point = new LandmarkPoint();
                return PXCMFaceData_LandmarksData_QueryPoint(instance, index, point);
            }

            [DllImport(DLLNAME)]
            internal static extern Int32 PXCMFaceData_LandmarksData_QueryNumPointsByGroup(IntPtr instance, LandmarksGroupType group);

            [DllImport(DLLNAME)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern Boolean PXCMFaceData_LandmarksData_QueryPointsByGroup(IntPtr instance, LandmarksGroupType groupFlags, IntPtr outPoints);

            internal static Boolean QueryPointsByGroupINT(IntPtr instance, LandmarksGroupType group, out LandmarkPoint[] points)
            {
                int npoints = PXCMFaceData_LandmarksData_QueryNumPointsByGroup(instance, group);
                IntPtr outPoints2 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(LandmarkPoint)) * npoints);
                Boolean sts = PXCMFaceData_LandmarksData_QueryPointsByGroup(instance, group, outPoints2);
                if (sts)
                {
                    points = new LandmarkPoint[npoints];
                    for (int i = 0, j = 0; i < npoints; i++, j += Marshal.SizeOf(typeof(LandmarkPoint)))
                    {
                        points[i] = new LandmarkPoint();
                        Marshal.PtrToStructure(new IntPtr(outPoints2.ToInt64() + j), points[i]);
                    }
                }
                else
                {
                    points = null;
                }
                Marshal.FreeHGlobal(outPoints2);
                return sts;
            }

            [DllImport(DLLNAME)]
            internal static extern Int32 PXCMFaceData_LandmarksData_QueryPointIndex(IntPtr instance, LandmarkType name);
        };

        public partial class PoseData
        {
            [DllImport(DLLNAME)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern Boolean PXCMFaceData_PoseData_QueryPoseAngles(IntPtr instance, [Out] PoseEulerAngles outPoseEulerAngles);

            internal static Boolean QueryPoseAnglesINT(IntPtr instance, out PoseEulerAngles outPoseEulerAngles)
            {
                outPoseEulerAngles = new PoseEulerAngles();
                return PXCMFaceData_PoseData_QueryPoseAngles(instance, outPoseEulerAngles);
            }

            [DllImport(DLLNAME)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern Boolean PXCMFaceData_PoseData_QueryPoseQuaternion(IntPtr instance, [Out] PoseQuaternion outPoseQuaternion);

            internal static Boolean QueryPoseQuaternionINT(IntPtr instance, out PoseQuaternion outPoseQuaternion)
            {
                outPoseQuaternion = new PoseQuaternion();
                return PXCMFaceData_PoseData_QueryPoseQuaternion(instance, outPoseQuaternion);
            }

            [DllImport(DLLNAME)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern Boolean PXCMFaceData_PoseData_QueryHeadPosition(IntPtr instance, [Out] HeadPosition outHeadPosition);

            internal static Boolean QueryHeadPositionINT(IntPtr instance, out HeadPosition outHeadPosition)
            {
                outHeadPosition = new HeadPosition();
                return PXCMFaceData_PoseData_QueryHeadPosition(instance, outHeadPosition);
            }

            [DllImport(DLLNAME)]
            internal static extern void PXCMFaceData_PoseData_QueryRotationMatrix(IntPtr instance, [Out] Double[] outRotationMatrix);

            internal static void QueryRotationMatrixINT(IntPtr instance, out Double[] outRotationMatrix)
            {
                outRotationMatrix = new Double[9];
                PXCMFaceData_PoseData_QueryRotationMatrix(instance, outRotationMatrix);
            }

            [DllImport(DLLNAME)]
            internal static extern Int32 PXCMFaceData_PoseData_QueryConfidence(IntPtr instance);
        };

        public partial class Face
        {
            [DllImport(DLLNAME)]
            internal static extern Int32 PXCMFaceData_Face_QueryUserID(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern IntPtr PXCMFaceData_Face_Detection(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern IntPtr PXCMFaceData_Face_QueryLandmarks(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern IntPtr PXCMFaceData_Face_QueryPose(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern IntPtr PXCMFaceData_Face_QueryExpressions(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern IntPtr PXCMFaceData_Face_QueryRecognition(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern IntPtr PXCMFaceData_Face_QueryPulse(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern IntPtr PXCMFaceData_Face_QueryGaze(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern IntPtr PXCMFaceData_Face_QueryGazeCalibration(IntPtr instance);
        };

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMFaceData_Update(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern Int64 PXCMFaceData_QueryFrameTimestamp(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern Int32 PXCMFaceData_QueryNumberOfDetectedFaces(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern IntPtr PXCMFaceData_QueryFaceByID(IntPtr instance, Int32 faceId);

        [DllImport(DLLNAME)]
        internal static extern IntPtr PXCMFaceData_QueryFaceByIndex(IntPtr instance, Int32 index);

        [DllImport(DLLNAME)]
        internal static extern IntPtr PXCMFaceData_QueryFaces(IntPtr instance, out Int32 numDetectedFaces);

        [DllImport(DLLNAME)]
        internal static extern IntPtr PXCMFaceData_QueryRecognitionModule(IntPtr instance);

        [DllImport(DLLNAME, CharSet = CharSet.Unicode)]
        private static extern pxcmStatus PXCMFaceData_QueryAlertNameByID(IntPtr instance, AlertData.AlertType alertEvent, StringBuilder outAlertName);

        internal static pxcmStatus QueryAlertNameByIDINT(IntPtr instance, AlertData.AlertType alertEvent, out String outAlertName)
        {
            StringBuilder alertName = new StringBuilder(ALERT_NAME_SIZE);
            pxcmStatus sts = PXCMFaceData_QueryAlertNameByID(instance, alertEvent, alertName);
            outAlertName = (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR) ? alertName.ToString() : "";
            return sts;
        }

        /*
        * Returns allocated array of numDetectedFaces tracked faces where the order is indicated by heuristic (GetNumberOfDetectedFaces == numDetectedFaces).
        * Size of returned array is guaranteed to be greater or equal to numDetectedFaces.
        */
        public Face[] QueryFaces()
        {
            Int32 nfaces = 0;
            IntPtr faces2 = PXCMFaceData_QueryFaces(instance, out nfaces);
            if (faces2 == IntPtr.Zero) return null;

            IntPtr[] faces3 = new IntPtr[nfaces];
            Marshal.Copy(faces2, faces3, 0, nfaces);

            Face[] faces4 = new Face[nfaces];
            for (int i = 0; i < nfaces; i++)
                faces4[i] = new Face(faces3[i]);
            return faces4;
        }

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMFaceData_QueryFiredAlertData(IntPtr instance, Int32 index, [Out] AlertData alertData);

        /** 
            @brief Get the details of the fired alert at the requested index.
            @param[in] index the zero-based index of the requested fired alert .
            @param[out] alertData contains all the information for the fired event. 
            @see AlertData
            @note the index is between 0 and the result of GetFiredAlertsNumber()
            @see GetFiredAlertsNumber()
            @return PXC_STATUS_NO_ERROR if returning fired alert data was successful; otherwise, return one of the following errors:
            PXC_STATUS_PROCESS_FAILED - Module failure during processing.\n
            PXC_STATUS_DATA_NOT_INITIALIZED - Data failed to initialize.\n
        */
        public pxcmStatus QueryFiredAlertData(Int32 index, out AlertData alertData)
        {
            alertData = new AlertData();
            return PXCMFaceData_QueryFiredAlertData(instance, index, alertData);
        }

        [DllImport(DLLNAME)]
        internal static extern Int32 PXCMFaceData_QueryFiredAlertsNumber(IntPtr instance);

        [DllImport(DLLNAME)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern Boolean PXCMFaceData_IsAlertFired(IntPtr instance, AlertData.AlertType alertEvent, [Out] AlertData alertData);

        /**
            @brief Return whether the specified alert is fired in the current frame, and retrieve its data if it is.
            @param[in] alertEvent the ID of the event.
            @param[out] alertData contains all the information for the fired event.
            @see AlertData
            @return true if the alert is fired, false otherwise.
        */
        public Boolean IsAlertFired(AlertData.AlertType alertEvent, out AlertData alertData)
        {
            alertData = new AlertData();
            return PXCMFaceData_IsAlertFired(instance, alertEvent, alertData);
        }

        [DllImport(DLLNAME)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern Boolean PXCMFaceData_IsAlertFiredByFace(IntPtr instance, AlertData.AlertType alertEvent, Int32 faceID, [Out] AlertData alertData);

        /**
            @brief Return whether the specified alert is fired for a specific face in the current frame, and retrieve its data.
            @param[in] alertEvent the label of the alert event.
            @param[in] faceID the ID of the face who's alert should be retrieved. 
            @param[out] alertData contains all the information for the fired event.
            @see AlertData
            @return true if the alert is fired, false otherwise.
        */
        public Boolean IsAlertFiredByFace(AlertData.AlertType alertEvent, Int32 faceID, out AlertData alertData)
        {
            alertData = new AlertData();
            return PXCMFaceData_IsAlertFiredByFace(instance, alertEvent, faceID, alertData);
        }


        public partial class ExpressionsData
        {
            [DllImport(DLLNAME)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern Boolean PXCMFaceData_ExpressionsData_QueryExpression(IntPtr instance, FaceExpression au, [Out] FaceExpressionResult auResult);

            internal static Boolean QueryExpressionINT(IntPtr instance, FaceExpression au, out FaceExpressionResult auResult)
            {
                auResult = new FaceExpressionResult();
                return PXCMFaceData_ExpressionsData_QueryExpression(instance, au, auResult);
            }
        }

        public partial class RecognitionData
        {
            [DllImport(DLLNAME)]
            internal static extern Int32 PXCMFaceData_RecognitionData_RegisterUser(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern void PXCMFaceData_RecognitionData_UnregisterUser(IntPtr instance);

            [DllImport(DLLNAME)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern Boolean PXCMFaceData_RecognitionData_IsRegistered(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern Int32 PXCMFaceData_RecognitionData_QueryUserID(IntPtr instance);
        }

        public partial class RecognitionModuleData
        {
            [DllImport(DLLNAME)]
            internal static extern Int32 PXCMFaceData_RecognitionModuleData_QueryDatabaseSize(IntPtr instance);

            [DllImport(DLLNAME)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern Boolean PXCMFaceData_RecognitionModuleData_QueryDatabaseBuffer(IntPtr instance, [Out] Byte[] buffer);

            [DllImport(DLLNAME)]
            internal static extern void PXCMFaceData_RecognitionModuleData_UnregisterUserByID(IntPtr instance, Int32 userId);
        }

        public partial class PulseData
        {
            [DllImport(DLLNAME)]
            internal static extern float PXCMFaceData_PulseData_QueryHeartRate(IntPtr instance);
        }

        public partial class GazeData
        {
            [DllImport(DLLNAME)]
            internal static extern void PXCMFaceData_GazeData_QueryGazePoint(IntPtr instance, [Out] GazePoint gp);

            [DllImport(DLLNAME)]
            internal static extern Double PXCMFaceData_GazeData_QueryGazeHorizontalAngle(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern Double PXCMFaceData_GazeData_QueryGazeVerticalAngle(IntPtr instance);
        }

        public partial class GazeCalibData
        {
            [DllImport(DLLNAME)]
            internal static extern CalibrationState PXCMFaceData_GazeCalibData_QueryCalibrationState(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern void PXCMFaceData_GazeCalibData_QueryClibPoint(IntPtr instance, ref PXCMPointI32 cp);

            [DllImport(DLLNAME)]
            internal static extern Int32 PXCMFaceData_GazeCalibData_QueryCalibDataSize(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern CalibrationStatus PXCMFaceData_GazeCalibData_QueryCalibData(IntPtr instance, [Out] Byte[] buffer);

            [DllImport(DLLNAME)]
            internal static extern DominantEye PXCMFaceData_GazeCalibData_QueryCalibDominantEye(IntPtr instance);
        }

    };

#if RSSDK_IN_NAMESPACE
}
#endif

