/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2013 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections.Generic;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif


    public partial class PXCMHandData : PXCMBase
    {
        public partial class IHand
        {
            [DllImport(DLLNAME)]
            internal static extern Int32 PXCMHandData_IHand_QueryUniqueId(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern Int32 PXCMHandData_IHand_QueryUserId(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern Int64 PXCMHandData_IHand_QueryTimeStamp(IntPtr instance);

            [DllImport(DLLNAME)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern Boolean PXCMHandData_IHand_IsCalibrated(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern BodySideType PXCMHandData_IHand_QueryBodySide(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern void PXCMHandData_IHand_QueryBoundingBoxImage(IntPtr instance, ref PXCMRectI32 rect);

            [DllImport(DLLNAME)]
            internal static extern void PXCMHandData_IHand_QueryMassCenterImage(IntPtr instance, ref PXCMPointF32 point);

            [DllImport(DLLNAME)]
            internal static extern void PXCMHandData_IHand_QueryMassCenterWorld(IntPtr instance, ref PXCMPoint3DF32 point);

            [DllImport(DLLNAME)]
            internal static extern void PXCMHandData_IHand_QueryPalmOrientation(IntPtr instance, ref PXCMPoint4DF32 point);

            [DllImport(DLLNAME)]
            internal static extern Int32 PXCMHandData_IHand_QueryOpenness(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern float PXCMHandData_IHand_QueryPalmRadiusImage(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern float PXCMHandData_IHand_QueryPalmRadiusWorld(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern Int32 PXCMHandData_IHand_QueryTrackingStatus(IntPtr instance);

            [DllImport(DLLNAME)]
            private static extern pxcmStatus PXCMHandData_IHand_QueryExtremityPoint(IntPtr instance, ExtremityType extremityLabel, [Out] ExtremityData extremityPoint);

            internal static pxcmStatus QueryExtremityPointINT(IntPtr instance, ExtremityType extremityLabel, out ExtremityData extremityPoint)
            {
                extremityPoint = new ExtremityData();
                return PXCMHandData_IHand_QueryExtremityPoint(instance, extremityLabel, extremityPoint);
            }

            [DllImport(DLLNAME)]
            private static extern pxcmStatus PXCMHandData_IHand_QueryFingerData(IntPtr instance, FingerType fingerLabel, [Out] FingerData fingerData);

            internal static pxcmStatus QueryFingerDataINT(IntPtr instance, FingerType fingerLabel, out FingerData fingerData)
            {
                fingerData = new FingerData();
                return PXCMHandData_IHand_QueryFingerData(instance, fingerLabel, fingerData);
            }

            [DllImport(DLLNAME)]
            private static extern pxcmStatus PXCMHandData_IHand_QueryTrackedJoint(IntPtr instance, JointType jointLabel, [Out] JointData jointData);

            internal static pxcmStatus QueryTrackedJointINT(IntPtr instance, JointType jointLabel, out JointData jointData)
            {
                jointData = new JointData();
                return PXCMHandData_IHand_QueryTrackedJoint(instance, jointLabel, jointData);
            }

            [DllImport(DLLNAME)]
            private static extern pxcmStatus PXCMHandData_IHand_QueryNormalizedJoint(IntPtr instance, JointType jointLabel, [Out] JointData jointData);

            internal static pxcmStatus QueryNormalizedJointINT(IntPtr instance, JointType jointLabel, out JointData jointData)
            {
                jointData = new JointData();
                return PXCMHandData_IHand_QueryNormalizedJoint(instance, jointLabel, jointData);
            }

            [DllImport(DLLNAME)]
            internal static extern pxcmStatus PXCMHandData_IHand_QuerySegmentationImage(IntPtr instance, out IntPtr image);

            [DllImport(DLLNAME)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern Boolean PXCMHandData_IHand_HasTrackedJoints(IntPtr instance);

            [DllImport(DLLNAME)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern Boolean PXCMHandData_IHand_HasNormalizedJoints(IntPtr instance);

            [DllImport(DLLNAME)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern Boolean PXCMHandData_IHand_HasSegmentationImage(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern Int32 PXCMHandData_IHand_QueryNumberOfContours(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern pxcmStatus PXCMHandData_IHand_QueryContour(IntPtr instance, Int32 index, out IntPtr contourData);


        };

        public partial class IContour
        {
            [DllImport(DLLNAME)]
            internal static extern Int32 PXCMHandData_IContour_QuerySize(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern pxcmStatus PXCMHandData_IContour_QueryPoints(IntPtr instance, Int32 max, [Out] PXCMPointI32[] contour);

            internal static pxcmStatus QueryDataINT(IntPtr instance, out PXCMPointI32[] contour)
            {
                Int32 size = PXCMHandData_IContour_QuerySize(instance);
                if (size > 0)
                {
                    contour = new PXCMPointI32[size];
                    return PXCMHandData_IContour_QueryPoints(instance, size, contour);
                }
                else
                {
                    contour = null;
                    return pxcmStatus.PXCM_STATUS_DATA_UNAVAILABLE;
                }
            }

            [DllImport(DLLNAME)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern Boolean PXCMHandData_IContour_IsOuter(IntPtr instance);
        };

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMHandData_Update(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern Int32 PXCMHandData_QueryFiredAlertsNumber(IntPtr instance);

        [DllImport(DLLNAME)]
        private static extern pxcmStatus PXCMHandData_QueryFiredAlertData(IntPtr instance, Int32 index, [Out] AlertData alertData);

        internal static pxcmStatus QueryFiredAlertDataINT(IntPtr instance, Int32 index, out AlertData alertData)
        {
            alertData = new AlertData();
            return PXCMHandData_QueryFiredAlertData(instance, index, alertData);
        }

        [DllImport(DLLNAME)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern Boolean PXCMHandData_IsAlertFired(IntPtr instance, AlertType alertEvent, [Out] AlertData alertData);

        internal static Boolean IsAlertFiredINT(IntPtr instance, AlertType alertEvent, out AlertData alertData)
        {
            alertData = new AlertData();
            return PXCMHandData_IsAlertFired(instance, alertEvent, alertData);
        }

        [DllImport(DLLNAME)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern Boolean PXCMHandData_IsAlertFiredByHand(IntPtr instance, AlertType alertEvent, Int32 handID, [Out] AlertData alertData);

        internal static Boolean IsAlertFiredByHandINT(IntPtr instance, AlertType alertEvent, Int32 handID, out AlertData alertData)
        {
            alertData = new AlertData();
            return PXCMHandData_IsAlertFiredByHand(instance, alertEvent, handID, alertData);
        }

        [DllImport(DLLNAME)]
        internal static extern Int32 PXCMHandData_QueryFiredGesturesNumber(IntPtr instance);

        [DllImport(DLLNAME)]
        private static extern pxcmStatus PXCMHandData_QueryFiredGestureData(IntPtr instance, Int32 index, [Out] GestureData gestureData);

        internal static pxcmStatus QueryFiredGestureDataINT(IntPtr instance, Int32 index, out GestureData gestureData)
        {
            gestureData = new GestureData();
            return PXCMHandData_QueryFiredGestureData(instance, index, gestureData);
        }

        [DllImport(DLLNAME, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern Boolean PXCMHandData_IsGestureFired(IntPtr instance, String gestureName, [Out] GestureData gestureData);

        internal static Boolean IsGestureFiredINT(IntPtr instance, String gestureName, out GestureData gestureData)
        {
            gestureData = new GestureData();
            return PXCMHandData_IsGestureFired(instance, gestureName, gestureData);
        }

        [DllImport(DLLNAME, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern Boolean PXCMHandData_IsGestureFiredByHand(IntPtr instance, String gestureName, Int32 handID, [Out] GestureData gestureData);

        internal static Boolean IsGestureFiredByHandINT(IntPtr instance, String gestureName, Int32 handID, out GestureData gestureData)
        {
            gestureData = new GestureData();
            return PXCMHandData_IsGestureFiredByHand(instance, gestureName, handID, gestureData);
        }

        [DllImport(DLLNAME)]
        internal static extern Int32 PXCMHandData_QueryNumberOfHands(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMHandData_QueryHandId(IntPtr instance, AccessOrderType accessOrder, Int32 index, out Int32 handId);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMHandData_QueryHandData(IntPtr instance, AccessOrderType accessOrder, Int32 index, out IntPtr handData);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMHandData_QueryHandDataById(IntPtr instance, Int32 handId, out IntPtr handData);
    };

#if RSSDK_IN_NAMESPACE
}
#endif
