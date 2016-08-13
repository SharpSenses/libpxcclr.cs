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
    public partial class PXCMPersonTrackingConfiguration : PXCMBase
    {


        private partial class AlertHandlerDIR : IDisposable
        {
            [DllImport(PXCMBase.DLLNAME)]
            private static extern IntPtr PXCMPersonTrackingConfiguration_AllocAlertHandlerDIR(IntPtr handlerFunc);
            [DllImport(PXCMBase.DLLNAME)]
            private static extern void PXCMPersonTrackingConfiguration_FreeAlertHandlerDIR(IntPtr hdir);

            internal delegate void OnFiredAlertDIRDelegate(IntPtr data);

            private GCHandle gch;
            internal OnFiredAlertDelegate mfunc;
            internal IntPtr uDIR;

            public void Dispose()
            {
                if (uDIR == IntPtr.Zero) return;

                PXCMPersonTrackingConfiguration_FreeAlertHandlerDIR(uDIR);
                uDIR = IntPtr.Zero;
                if (gch.IsAllocated) gch.Free();
            }

            public AlertHandlerDIR(OnFiredAlertDelegate handler)
            {
                mfunc = handler;
                gch = GCHandle.Alloc(handler);
                uDIR = PXCMPersonTrackingConfiguration_AllocAlertHandlerDIR(Marshal.GetFunctionPointerForDelegate(handler));
            }

            ~AlertHandlerDIR()
            {
                Dispose();
            }
        };

#if PT_MW_DEV

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMPersonTrackingConfiguration_EnableAlert(IntPtr instance, PXCMPersonTrackingData.AlertType alertEvent);

        [DllImport(DLLNAME)]
        internal static extern void PXCMPersonTrackingConfiguration_EnableAllAlerts(IntPtr instance);

        [DllImport(DLLNAME)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern Boolean PXCMPersonTrackingConfiguration_IsAlertEnabled(IntPtr instance, PXCMPersonTrackingData.AlertType alertEvent);

        [DllImport(DLLNAME)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern Boolean PXCMPersonTrackingConfiguration_DisableAlert(IntPtr instance, PXCMPersonTrackingData.AlertType alertEvent);

        [DllImport(DLLNAME)]
        internal static extern void PXCMPersonTrackingConfiguration_DisableAllAlerts(IntPtr instance);

        [DllImport(PXCMBase.DLLNAME)]
        private static extern pxcmStatus PXCMPersonTrackingConfiguration_SubscribeAlert(IntPtr instance, IntPtr alertHandler);

        internal static pxcmStatus SubscribeAlertINT(IntPtr instance, OnFiredAlertDelegate handler, out Object proxy)
        {
            AlertHandlerDIR adir = new AlertHandlerDIR(handler);
            pxcmStatus sts = PXCMPersonTrackingConfiguration_SubscribeAlert(instance, adir.uDIR);
            if (sts < pxcmStatus.PXCM_STATUS_NO_ERROR)
                adir.Dispose();
            proxy = (Object)adir;
            return sts;
        }

        [DllImport(PXCMBase.DLLNAME)]
        private static extern pxcmStatus PXCMPersonTrackingConfiguration_UnsubscribeAlert(IntPtr instance, IntPtr alertHandler);

        internal static pxcmStatus UnsubscribeAlertINT(IntPtr instance, Object proxy)
        {
            AlertHandlerDIR adir = (AlertHandlerDIR)proxy;
            pxcmStatus sts = PXCMPersonTrackingConfiguration_UnsubscribeAlert(instance, adir.uDIR);
            adir.Dispose();
            return sts;
        }
#endif

        public partial class TrackingConfiguration
        {
            [DllImport(DLLNAME)]
            internal static extern void PXCMPersonTrackingConfiguration_TrackingConfiguration_Enable(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern void PXCMPersonTrackingConfiguration_TrackingConfiguration_Disable(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern void PXCMPersonTrackingConfiguration_TrackingConfiguration_EnableSegmentation(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern void PXCMPersonTrackingConfiguration_TrackingConfiguration_DisableSegmentation(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern void PXCMPersonTrackingConfiguration_TrackingConfiguration_SetMaxTrackedPersons(IntPtr instance, Int32 maxTrackedPersons);
        }

        public partial class SkeletonJointsConfiguration
        {
            [DllImport(DLLNAME)]
            internal static extern void PXCMPersonTrackingConfiguration_SkeletonJointsConfiguration_Enable(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern void PXCMPersonTrackingConfiguration_SkeletonJointsConfiguration_Disable(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern void PXCMPersonTrackingConfiguration_SkeletonJointsConfiguration_SetMaxTrackedPersons(IntPtr instance, Int32 maxTrackedPersons);

            [DllImport(DLLNAME)]
            internal static extern pxcmStatus PXCMPersonTrackingConfiguration_SkeletonJointsConfiguration_SetTrackingArea(IntPtr instance, SkeletonMode area);
        }


        public partial class PoseConfiguration
        {
            [DllImport(DLLNAME)]
            internal static extern void PXCMPersonTrackingConfiguration_PoseConfiguration_Enable(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern void PXCMPersonTrackingConfiguration_PoseConfiguration_Disable(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern void PXCMPersonTrackingConfiguration_PoseConfiguration_MaxTrackedPersons(IntPtr instance, Int32 maxTrackedPersons);
        }


        public partial class RecognitionConfiguration
        {
           
            [DllImport(DLLNAME)]
            internal static extern void PXCMPersonTrackingConfiguration_RecognitionConfiguration_Enable(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern void PXCMPersonTrackingConfiguration_RecognitionConfiguration_Disable(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern void PXCMPersonTrackingConfiguration_RecognitionConfiguration_SetDatabaseBuffer(IntPtr instance, Byte[] buffer, Int32 size);
        }

        [DllImport(DLLNAME)]
        internal static extern IntPtr PXCMPersonTrackingConfiguration_QueryTracking(IntPtr instance);
        internal TrackingConfiguration QueryTrackingINT(IntPtr instance)
        {
            IntPtr instance2 = PXCMPersonTrackingConfiguration_QueryTracking(instance);
            if (instance2 == IntPtr.Zero)
                return null;
            else
                return new TrackingConfiguration(instance2);
        }

        [DllImport(DLLNAME)]
        internal static extern IntPtr PXCMPersonTrackingConfiguration_QuerySkeletonJoints(IntPtr instance);
        internal SkeletonJointsConfiguration QuerySkeletonJointsINT(IntPtr instance)
        {
            IntPtr instance2 = PXCMPersonTrackingConfiguration_QuerySkeletonJoints(instance);
            if (instance2 == IntPtr.Zero)
                return null;
            else
                return new SkeletonJointsConfiguration(instance2);
        }

        [DllImport(DLLNAME)]
        internal static extern IntPtr PXCMPersonTrackingConfiguration_QueryPose(IntPtr instance);
        internal PoseConfiguration QueryPoseINT(IntPtr instance)
        {
            IntPtr instance2 = PXCMPersonTrackingConfiguration_QueryPose(instance);
            if (instance2 == IntPtr.Zero)
                return null;
            else
                return new PoseConfiguration(instance2);
        }

        [DllImport(DLLNAME)]
        internal static extern IntPtr PXCMPersonTrackingConfiguration_QueryRecognition(IntPtr instance);
        internal RecognitionConfiguration QueryRecognitionINT(IntPtr instance)
        {
            IntPtr instance2 = PXCMPersonTrackingConfiguration_QueryRecognition(instance);
            if (instance2 == IntPtr.Zero)
                return null;
            else
                return new RecognitionConfiguration(instance2);
        } 

        [DllImport(DLLNAME)]
        internal static extern void PXCMPersonTrackingConfiguration_SetTrackedAngles(IntPtr instance, TrackingAngles angles);
    
    };

#if RSSDK_IN_NAMESPACE
}
#endif
