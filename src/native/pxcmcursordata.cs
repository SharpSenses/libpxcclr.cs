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


    public partial class PXCMCursorData : PXCMBase
    {
        public partial class ICursor
        {
            [DllImport(DLLNAME)]
            internal static extern Int32 PXCMCursorData_ICursor_QueryUniqueId(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern Int64 PXCMCursorData_ICursor_QueryTimeStamp(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern BodySideType PXCMCursorData_ICursor_QueryBodySide(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern void PXCMCursorData_ICursor_QueryCursorWorldPoint(IntPtr instance, ref PXCMPoint3DF32 point);

            [DllImport(DLLNAME)]
            internal static extern void PXCMCursorData_ICursor_QueryCursorImagePoint(IntPtr instance, ref PXCMPoint3DF32 point);

            [DllImport(DLLNAME)]
            internal static extern void PXCMCursorData_ICursor_QueryAdaptivePoint(IntPtr instance, ref PXCMPoint3DF32 point);

            [DllImport(DLLNAME)]
            internal static extern Int32 PXCMCursorData_ICursor_QueryEngagementPercent(IntPtr instance);

            
        };

        
    

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMCursorData_Update(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern Int32 PXCMCursorData_QueryFiredAlertsNumber(IntPtr instance);

        [DllImport(DLLNAME)]
        private static extern pxcmStatus PXCMCursorData_QueryFiredAlertData(IntPtr instance, Int32 index, [Out] AlertData alertData);

        internal static pxcmStatus QueryFiredAlertDataINT(IntPtr instance, Int32 index, out AlertData alertData)
        {
            alertData = new AlertData();
            return PXCMCursorData_QueryFiredAlertData(instance, index, alertData);
        }




        [DllImport(DLLNAME)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern Boolean PXCMCursorData_IsAlertFired(IntPtr instance, AlertType alertEvent, [Out] AlertData alertData);

        internal static Boolean IsAlertFiredINT(IntPtr instance, AlertType alertEvent, out AlertData alertData)
        {
            alertData = new AlertData();
            return PXCMCursorData_IsAlertFired(instance, alertEvent, alertData);
        }

        
        [DllImport(DLLNAME)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern Boolean PXCMCursorData_IsAlertFiredByHand(IntPtr instance, AlertType alertEvent, Int32 handID, [Out] AlertData alertData);

        internal static Boolean IsAlertFiredByHandINT(IntPtr instance, AlertType alertEvent, Int32 handID, out AlertData alertData)
        {
            alertData = new AlertData();
            return PXCMCursorData_IsAlertFiredByHand(instance, alertEvent, handID, alertData);
        }

        [DllImport(DLLNAME)]
        internal static extern Int32 PXCMCursorData_QueryFiredGesturesNumber(IntPtr instance);

        [DllImport(DLLNAME)]
        private static extern pxcmStatus PXCMCursorData_QueryFiredGestureData(IntPtr instance, Int32 index, [Out] GestureData gestureData);

        internal static pxcmStatus QueryFiredGestureDataINT(IntPtr instance, Int32 index, out GestureData gestureData)
        {
            gestureData = new GestureData();
            return PXCMCursorData_QueryFiredGestureData(instance, index, gestureData);
        }


        [DllImport(DLLNAME, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern Boolean PXCMCursorData_IsGestureFired(IntPtr instance, GestureType getureEvent, [Out] GestureData gestureData);

        internal static Boolean IsGestureFiredINT(IntPtr instance, GestureType getureEvent, out GestureData gestureData)
        {
            gestureData = new GestureData();
            return PXCMCursorData_IsGestureFired(instance, getureEvent, gestureData);
        }


        [DllImport(DLLNAME, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern Boolean PXCMCursorData_IsGestureFiredByHand(IntPtr instance, GestureType getureEvent, Int32 handID, [Out] GestureData gestureData);

        internal static Boolean IsGestureFiredByHandINT(IntPtr instance, GestureType getureEvent, Int32 handID, out GestureData gestureData)
        {
            gestureData = new GestureData();
            return PXCMCursorData_IsGestureFiredByHand(instance, getureEvent, handID, gestureData);
        }

        [DllImport(DLLNAME)]
        internal static extern Int32 PXCMCursorData_QueryNumberOfCursors(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMCursorData_QueryCursorData(IntPtr instance, AccessOrderType accessOrder, Int32 index, out IntPtr cursorData);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMCursorData_QueryCursorDataById(IntPtr instance, Int32 handId, out IntPtr cursorData);
    };

#if RSSDK_IN_NAMESPACE
}
#endif
