/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2013-2014 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections.Generic;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

    public partial class PXCMHandConfiguration : PXCMBase
    {
        internal partial class GestureHandlerDIR : IDisposable
        {
            [DllImport(PXCMBase.DLLNAME)]
            internal static extern IntPtr PXCMHandConfiguration_AllocGestureHandlerDIR(IntPtr handlerFunc);
            [DllImport(PXCMBase.DLLNAME)]
            internal static extern void PXCMHandConfiguration_FreeGestureHandlerDIR(IntPtr hdir);

            private GCHandle gch;
            internal OnFiredGestureDelegate mfunc;
            internal IntPtr uDIR;

            public void Dispose()
            {
                if (uDIR == IntPtr.Zero) return;

                PXCMHandConfiguration_FreeGestureHandlerDIR(uDIR);
                uDIR = IntPtr.Zero;

                if (gch.IsAllocated) gch.Free();
            }

            public GestureHandlerDIR(OnFiredGestureDelegate handler)
            {
                mfunc = handler;
                gch = GCHandle.Alloc(handler);
                uDIR = PXCMHandConfiguration_AllocGestureHandlerDIR(Marshal.GetFunctionPointerForDelegate(handler));
            }

            ~GestureHandlerDIR()
            {
                Dispose();
            }
        };

        internal partial class AlertHandlerDIR : IDisposable
        {
            [DllImport(PXCMBase.DLLNAME)]
            internal static extern IntPtr PXCMHandConfiguration_AllocAlertHandlerDIR(IntPtr handlerFunc);
            [DllImport(PXCMBase.DLLNAME)]
            internal static extern void PXCMHandConfiguration_FreeAlertHandlerDIR(IntPtr hdir);

            internal delegate void OnFiredAlertDIRDelegate(IntPtr data);

            private GCHandle gch;
            internal OnFiredAlertDelegate mfunc;
            internal IntPtr uDIR;

            public void Dispose()
            {
                if (uDIR == IntPtr.Zero) return;

                PXCMHandConfiguration_FreeAlertHandlerDIR(uDIR);
                uDIR = IntPtr.Zero;

                if (gch.IsAllocated) gch.Free();
            }

            public AlertHandlerDIR(OnFiredAlertDelegate handler)
            {
                mfunc = handler;
                gch = GCHandle.Alloc(handler);
                uDIR = PXCMHandConfiguration_AllocAlertHandlerDIR(Marshal.GetFunctionPointerForDelegate(handler));
            }

            ~AlertHandlerDIR()
            {
                Dispose();
            }
        };

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMHandConfiguration_ApplyChanges(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMHandConfiguration_RestoreDefaults(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMHandConfiguration_Update(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMHandConfiguration_ResetTracking(IntPtr instance);

        [DllImport(DLLNAME, CharSet = CharSet.Unicode)]
        internal static extern pxcmStatus PXCMHandConfiguration_SetUserName(IntPtr instance, String userName);

        [DllImport(DLLNAME, CharSet = CharSet.Unicode)]
        private static extern void PXCMHandConfiguration_QueryUserName(IntPtr instance, StringBuilder userName);

        internal static String QueryUserNameINT(IntPtr instance)
        {
            StringBuilder sb = new StringBuilder(PXCMHandData.MAX_NAME_SIZE);
            PXCMHandConfiguration_QueryUserName(instance, sb);
            return sb.ToString();
        } 

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMHandConfiguration_EnableJointSpeed(IntPtr instance, PXCMHandData.JointType jointLabel, PXCMHandData.JointSpeedType jointSpeed, Int32 time);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMHandConfiguration_DisableJointSpeed(IntPtr instance, PXCMHandData.JointType jointLabel);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMHandConfiguration_SetTrackingBounds(IntPtr instance, Single nearTrackingDistance, Single farTrackingDistance, Single nearTrackingWidth, Single nearTrackingHeight);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMHandConfiguration_QueryTrackingBounds(IntPtr instance, out Single nearTrackingDistance, out Single farTrackingDistance, out Single nearTrackingWidth, out Single nearTrackingHeight);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMHandConfiguration_SetTrackingMode(IntPtr instance, PXCMHandData.TrackingModeType trackingMode);

        [DllImport(DLLNAME)]
        internal static extern PXCMHandData.TrackingModeType PXCMHandConfiguration_QueryTrackingMode(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMHandConfiguration_SetSmoothingValue(IntPtr instance, Single smoothingValue);

        [DllImport(DLLNAME)]
        internal static extern Single PXCMHandConfiguration_QuerySmoothingValue(IntPtr instance);


        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMHandConfiguration_EnableStabilizer(IntPtr instance, [MarshalAs(UnmanagedType.Bool)] Boolean enableFlag);

        [DllImport(DLLNAME)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern Boolean PXCMHandConfiguration_IsStabilizerEnabled(IntPtr instance);


        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMHandConfiguration_EnableNormalizedJoints(IntPtr instance, [MarshalAs(UnmanagedType.Bool)] Boolean enableFlag);

        [DllImport(DLLNAME)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern Boolean PXCMHandConfiguration_IsNormalizedJointsEnabled(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMHandConfiguration_EnableSegmentationImage(IntPtr instance, [MarshalAs(UnmanagedType.Bool)] Boolean enableFlag);

        [DllImport(DLLNAME)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern Boolean PXCMHandConfiguration_IsSegmentationImageEnabled(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMHandConfiguration_EnableTrackedJoints(IntPtr instance, [MarshalAs(UnmanagedType.Bool)] Boolean enableFlag);

        [DllImport(DLLNAME)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern Boolean PXCMHandConfiguration_IsTrackedJointsEnabled(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMHandConfiguration_EnableAlert(IntPtr instance, PXCMHandData.AlertType alertEvent);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMHandConfiguration_EnableAllAlerts(IntPtr instance);

        [DllImport(DLLNAME)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern Boolean PXCMHandConfiguration_IsAlertEnabled(IntPtr instance, PXCMHandData.AlertType alertEvent);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMHandConfiguration_DisableAlert(IntPtr instance, PXCMHandData.AlertType alertEvent);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMHandConfiguration_DisableAllAlerts(IntPtr instance);

        [DllImport(DLLNAME, CharSet = CharSet.Unicode)]
        internal static extern pxcmStatus PXCMHandConfiguration_LoadGesturePack(IntPtr instance, String gesturePackPath);

        [DllImport(DLLNAME, CharSet = CharSet.Unicode)]
        internal static extern pxcmStatus PXCMHandConfiguration_UnloadGesturePack(IntPtr instance, String gesturePackPath);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMHandConfiguration_UnloadAllGesturesPacks(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern Int32 PXCMHandConfiguration_QueryGesturesTotalNumber(IntPtr instance);

        [DllImport(DLLNAME, CharSet = CharSet.Unicode)]
        internal static extern pxcmStatus PXCMHandConfiguration_QueryGestureNameByIndex(IntPtr instance, Int32 index, Int32 nchars, StringBuilder gestureName);

        [DllImport(DLLNAME, CharSet = CharSet.Unicode)]
        internal static extern pxcmStatus PXCMHandConfiguration_EnableGesture(IntPtr instance, String gestureName, [MarshalAs(UnmanagedType.Bool)] Boolean continuousGesture);

        [DllImport(DLLNAME, CharSet = CharSet.Unicode)]
        internal static extern pxcmStatus PXCMHandConfiguration_EnableGesture(IntPtr instance, String gestureName);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMHandConfiguration_EnableAllGestures(IntPtr instance, [MarshalAs(UnmanagedType.Bool)] Boolean continuousGesture);

        [DllImport(DLLNAME, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern Boolean PXCMHandConfiguration_IsGestureEnabled(IntPtr instance, String gestureName);

        [DllImport(DLLNAME, CharSet = CharSet.Unicode)]
        internal static extern pxcmStatus PXCMHandConfiguration_DisableGesture(IntPtr instance, String gestureName);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMHandConfiguration_DisableAllGestures(IntPtr instance);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMHandConfiguration_SubscribeAlert(IntPtr hand, IntPtr alertHandler);

        internal static pxcmStatus SubscribeAlertINT(IntPtr instance, OnFiredAlertDelegate handler, out Object proxy)
        {
            AlertHandlerDIR adir = new AlertHandlerDIR(handler);
            pxcmStatus sts = PXCMHandConfiguration_SubscribeAlert(instance, adir.uDIR);
            if (sts < pxcmStatus.PXCM_STATUS_NO_ERROR)
                adir.Dispose();
            proxy = (Object)adir;
            return sts;
        }

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMHandConfiguration_UnsubscribeAlert(IntPtr hand, IntPtr alertHandler);

        internal static pxcmStatus UnsubscribeAlertINT(IntPtr instance, Object proxy)
        {
            AlertHandlerDIR adir = (AlertHandlerDIR)proxy;
            pxcmStatus sts = PXCMHandConfiguration_UnsubscribeAlert(instance, adir.uDIR);
            adir.Dispose();
            return sts;
        }

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMHandConfiguration_SubscribeGesture(IntPtr hand, IntPtr gestureHandler);

        internal static pxcmStatus SubscribeGestureINT(IntPtr instance, OnFiredGestureDelegate handler, out Object proxy)
        {
            GestureHandlerDIR gdir = new GestureHandlerDIR(handler);
            pxcmStatus sts = PXCMHandConfiguration_SubscribeGesture(instance, gdir.uDIR);
            if (sts < pxcmStatus.PXCM_STATUS_NO_ERROR)
                gdir.Dispose();
            proxy = (Object)gdir;
            return sts;
        }

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMHandConfiguration_UnsubscribeGesture(IntPtr hand, IntPtr gestureHandler);

        internal static pxcmStatus UnsubscribeGestureINT(IntPtr instance, Object proxy)
        {
            GestureHandlerDIR gdir = (GestureHandlerDIR)proxy;
            pxcmStatus sts = PXCMHandConfiguration_UnsubscribeGesture(instance, gdir.uDIR);
            gdir.Dispose();
            return sts;
        }

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMHandConfiguration_SetDefaultAge(IntPtr instance, Int32 age);

        [DllImport(DLLNAME)]
        internal static extern Int32 PXCMHandConfiguration_QueryDefaultAge(IntPtr instance);

        [DllImport(DLLNAME, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern Boolean PXCMHandConfiguration_IsTrackingModeEnabled(IntPtr instance, PXCMHandData.TrackingModeType trackingMode);
        
    };

#if RSSDK_IN_NAMESPACE
}
#endif
