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

    public partial class PXCMCursorConfiguration : PXCMBase
    {
        internal partial class CursorGestureHandlerDIR : IDisposable
        {
            [DllImport(PXCMBase.DLLNAME)]
            internal static extern IntPtr PXCMCursorConfiguration_AllocGestureHandlerDIR(IntPtr handlerFunc);
            [DllImport(PXCMBase.DLLNAME)]
            internal static extern void PXCMCursorConfiguration_FreeGestureHandlerDIR(IntPtr hdir);

            private GCHandle gch;
            internal OnFiredGestureDelegate mfunc;
            internal IntPtr uDIR;

            public void Dispose()
            {
                if (uDIR == IntPtr.Zero) return;

                PXCMCursorConfiguration_FreeGestureHandlerDIR(uDIR);
                uDIR = IntPtr.Zero;

                if (gch.IsAllocated) gch.Free();
            }

            public CursorGestureHandlerDIR(OnFiredGestureDelegate handler)
            {
                mfunc = handler;
                gch = GCHandle.Alloc(handler);
                uDIR = PXCMCursorConfiguration_AllocGestureHandlerDIR(Marshal.GetFunctionPointerForDelegate(handler));
            }

            ~CursorGestureHandlerDIR()
            {
                Dispose();
            }
        };

        internal partial class CursorAlertHandlerDIR : IDisposable
        {
            [DllImport(PXCMBase.DLLNAME)]
            internal static extern IntPtr PXCMCursorConfiguration_AllocAlertHandlerDIR(IntPtr handlerFunc);
            [DllImport(PXCMBase.DLLNAME)]
            internal static extern void PXCMCursorConfiguration_FreeAlertHandlerDIR(IntPtr hdir);

            internal delegate void OnFiredAlertDIRDelegate(IntPtr data);

            private GCHandle gch;
            internal OnFiredAlertDelegate mfunc;
            internal IntPtr uDIR;

            public void Dispose()
            {
                if (uDIR == IntPtr.Zero) return;

                PXCMCursorConfiguration_FreeAlertHandlerDIR(uDIR);
                uDIR = IntPtr.Zero;

                if (gch.IsAllocated) gch.Free();
            }

            public CursorAlertHandlerDIR(OnFiredAlertDelegate handler)
            {
                mfunc = handler;
                gch = GCHandle.Alloc(handler);
                uDIR = PXCMCursorConfiguration_AllocAlertHandlerDIR(Marshal.GetFunctionPointerForDelegate(handler));
            }

            ~CursorAlertHandlerDIR()
            {
                Dispose();
            }
        };

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMCursorConfiguration_ApplyChanges(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMCursorConfiguration_RestoreDefaults(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMCursorConfiguration_Update(IntPtr instance);

     
        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMCursorConfiguration_SetTrackingBounds(IntPtr instance, PXCMCursorData.TrackingBounds trackingBounds);

        [DllImport(DLLNAME)]
        internal static extern PXCMCursorData.TrackingBounds PXCMCursorConfiguration_QueryTrackingBounds(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMCursorConfiguration_EnableEngagement(IntPtr instance, Boolean enable);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMCursorConfiguration_SetEngagementTime(IntPtr instance, int time);

        [DllImport(DLLNAME)]
        internal static extern int PXCMCursorConfiguration_QueryEngagementTime(IntPtr instance);


	    [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMCursorConfiguration_EnableAlert(IntPtr instance, PXCMCursorData.AlertType alertEvent);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMCursorConfiguration_EnableAllAlerts(IntPtr instance);

        [DllImport(DLLNAME)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern Boolean PXCMCursorConfiguration_IsAlertEnabled(IntPtr instance, PXCMCursorData.AlertType alertEvent);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMCursorConfiguration_DisableAlert(IntPtr instance, PXCMCursorData.AlertType alertEvent);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMCursorConfiguration_DisableAllAlerts(IntPtr instance);


        [DllImport(DLLNAME, CharSet = CharSet.Unicode)]
        internal static extern pxcmStatus PXCMCursorConfiguration_EnableGesture(IntPtr instance, PXCMCursorData.GestureType gestureEvent);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMCursorConfiguration_EnableAllGestures(IntPtr instance);

        [DllImport(DLLNAME, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern Boolean PXCMCursorConfiguration_IsGestureEnabled(IntPtr instance, PXCMCursorData.GestureType gestureEvent);

        [DllImport(DLLNAME, CharSet = CharSet.Unicode)]
        internal static extern pxcmStatus PXCMCursorConfiguration_DisableGesture(IntPtr instance, PXCMCursorData.GestureType gestureEvent);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMCursorConfiguration_DisableAllGestures(IntPtr instance);
		
		

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMCursorConfiguration_SubscribeAlert(IntPtr hand, IntPtr alertHandler);

        internal static pxcmStatus SubscribeAlertINT(IntPtr instance, OnFiredAlertDelegate handler, out Object proxy)
        {
            CursorAlertHandlerDIR adir = new CursorAlertHandlerDIR(handler);
            pxcmStatus sts = PXCMCursorConfiguration_SubscribeAlert(instance, adir.uDIR);
            if (sts < pxcmStatus.PXCM_STATUS_NO_ERROR)
                adir.Dispose();
            proxy = (Object)adir;
            return sts;
        }

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMCursorConfiguration_UnsubscribeAlert(IntPtr hand, IntPtr alertHandler);

        internal static pxcmStatus UnsubscribeAlertINT(IntPtr instance, Object proxy)
        {
            CursorAlertHandlerDIR adir = (CursorAlertHandlerDIR)proxy;
            pxcmStatus sts = PXCMCursorConfiguration_UnsubscribeAlert(instance, adir.uDIR);
            adir.Dispose();
            return sts;
        }

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMCursorConfiguration_SubscribeGesture(IntPtr hand, IntPtr gestureHandler);

        internal static pxcmStatus SubscribeGestureINT(IntPtr instance, OnFiredGestureDelegate handler, out Object proxy)
        {
            CursorGestureHandlerDIR gdir = new CursorGestureHandlerDIR(handler);
            pxcmStatus sts = PXCMCursorConfiguration_SubscribeGesture(instance, gdir.uDIR);
            if (sts < pxcmStatus.PXCM_STATUS_NO_ERROR)
                gdir.Dispose();
            proxy = (Object)gdir;
            return sts;
        }

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMCursorConfiguration_UnsubscribeGesture(IntPtr hand, IntPtr gestureHandler);

        internal static pxcmStatus UnsubscribeGestureINT(IntPtr instance, Object proxy)
        {
            CursorGestureHandlerDIR gdir = (CursorGestureHandlerDIR)proxy;
            pxcmStatus sts = PXCMCursorConfiguration_UnsubscribeGesture(instance, gdir.uDIR);
            gdir.Dispose();
            return sts;
        }    

      
        
    };

#if RSSDK_IN_NAMESPACE
}
#endif
