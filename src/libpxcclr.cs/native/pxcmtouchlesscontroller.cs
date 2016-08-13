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

    public partial class PXCMTouchlessController : PXCMBase
    {
        internal class UXEventHandlerDIR : IDisposable
        {
            /* P/Invoke Functions */
            [DllImport(PXCMBase.DLLNAME)]
            private static extern IntPtr PXCMTouchlessController_AllocUXEventHandlerDIR(IntPtr handlerFunc);
            [DllImport(PXCMBase.DLLNAME)]
            private static extern void PXCMTouchlessController_FreeUXEventHandlerDIR(IntPtr hdir);

            private GCHandle gch;
            internal OnFiredUXEventDelegate mfunc;
            internal IntPtr uDIR;

            public void Dispose()
            {
                if (uDIR == IntPtr.Zero) return;
                PXCMTouchlessController_FreeUXEventHandlerDIR(uDIR);
                uDIR = IntPtr.Zero;
                if (gch.IsAllocated) gch.Free();
            }

            public UXEventHandlerDIR(OnFiredUXEventDelegate handler)
            {
                mfunc = handler;
                gch = GCHandle.Alloc(handler);
                uDIR = PXCMTouchlessController_AllocUXEventHandlerDIR(Marshal.GetFunctionPointerForDelegate(handler));
            }

            ~UXEventHandlerDIR()
            {
                Dispose();
            }
        };

        internal class AlertHandlerDIR : IDisposable
        {
            /* P/Invoke Functions */
            [DllImport(PXCMBase.DLLNAME)]
            private static extern IntPtr PXCMTouchlessController_AllocAlertHandlerDIR(IntPtr handlerFunc);
            [DllImport(PXCMBase.DLLNAME)]
            private static extern void PXCMTouchlessController_FreeAlertHandlerDIR(IntPtr hdir);

            private GCHandle gch;
            internal OnFiredAlertDelegate mfunc;
            internal IntPtr uDIR;

            public void Dispose()
            {
                if (uDIR == IntPtr.Zero) return;
                PXCMTouchlessController_FreeAlertHandlerDIR(uDIR);
                uDIR = IntPtr.Zero;
                if (gch.IsAllocated) gch.Free();
            }

            public AlertHandlerDIR(OnFiredAlertDelegate handler)
            {
                mfunc = handler;
                gch = GCHandle.Alloc(handler);
                uDIR = PXCMTouchlessController_AllocAlertHandlerDIR(Marshal.GetFunctionPointerForDelegate(handler));
            }

            ~AlertHandlerDIR()
            {
                Dispose();
            }
        };

        internal class ActionHandlerDIR : IDisposable
        {
            /* P/Invoke Functions */
            [DllImport(PXCMBase.DLLNAME)]
            private static extern IntPtr PXCMTouchlessController_AllocActionHandlerDIR(IntPtr handlerFunc);
            [DllImport(PXCMBase.DLLNAME)]
            private static extern void PXCMTouchlessController_FreeActionHandlerDIR(IntPtr hdir);

            private GCHandle gch;
            internal OnFiredActionDelegate mfunc;
            internal IntPtr uDIR;

            public void Dispose()
            {
                if (uDIR == IntPtr.Zero) return;
                PXCMTouchlessController_FreeActionHandlerDIR(uDIR);
                uDIR = IntPtr.Zero;
                if (gch.IsAllocated) gch.Free();
            }

            public ActionHandlerDIR(OnFiredActionDelegate handler)
            {
                mfunc = handler;
                gch = GCHandle.Alloc(handler);
                uDIR = PXCMTouchlessController_AllocActionHandlerDIR(Marshal.GetFunctionPointerForDelegate(handler));
            }

            ~ActionHandlerDIR()
            {
                Dispose();
            }
        };

        /* P/Invoke Functions */
        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMTouchlessController_QueryProfile(IntPtr instance, [Out] ProfileInfo pinfo);
        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMTouchlessController_SetProfile(IntPtr instance, ProfileInfo pinfo);
        [DllImport(PXCMBase.DLLNAME)]
        private static extern pxcmStatus PXCMTouchlessController_SubscribeEvent(IntPtr instance, IntPtr handler);
        [DllImport(PXCMBase.DLLNAME)]
        private static extern pxcmStatus PXCMTouchlessController_UnsubscribeEvent(IntPtr instance, IntPtr handler);
        [DllImport(PXCMBase.DLLNAME)]
        private static extern pxcmStatus PXCMTouchlessController_SubscribeAlert(IntPtr instance, IntPtr handler);
        [DllImport(PXCMBase.DLLNAME)]
        private static extern pxcmStatus PXCMTouchlessController_UnsubscribeAlert(IntPtr instance, IntPtr handler);
        [DllImport(PXCMBase.DLLNAME, CharSet = CharSet.Unicode)]
        private static extern pxcmStatus PXCMTouchlessController_AddGestureActionMapping(IntPtr instance, String gestureName, Action action, IntPtr actionHandler);
        [DllImport(PXCMBase.DLLNAME)]
        private static extern pxcmStatus PXCMTouchlessController_ClearAllGestureActionMappings(IntPtr instance);
        [DllImport(PXCMBase.DLLNAME)]
        private static extern pxcmStatus PXCMTouchlessController_SetScrollSensitivity(IntPtr instance, float sensitivity);
        [DllImport(PXCMBase.DLLNAME)]
        private static extern pxcmStatus PXCMTouchlessController_SetPointerSensitivity(IntPtr instance, PointerSensitivity sensitivity);
        /** 
            @brief Return the configuration parameters of the SDK's TouchlessController application.
            @param[out] pinfo the profile info structure of the configuration parameters.
            @return PXC_STATUS_NO_ERROR if the parameters were returned successfully; otherwise, return one of the following errors:
            PXC_STATUS_ITEM_UNAVAILABLE - Item not found/not available.\n
            PXC_STATUS_DATA_NOT_INITIALIZED - Data failed to initialize.\n                        
        */


        public pxcmStatus QueryProfile(out ProfileInfo pinfo)
        {
            pinfo = new ProfileInfo();
            return PXCMTouchlessController_QueryProfile(instance, pinfo);
        }

        internal static pxcmStatus SubscribeEventINT(IntPtr instance, OnFiredUXEventDelegate uxEventHandler, out Object proxy)
        {
            UXEventHandlerDIR uxdir = new UXEventHandlerDIR(uxEventHandler);
            pxcmStatus sts = PXCMTouchlessController_SubscribeEvent(instance, uxdir.uDIR);
            if (sts < pxcmStatus.PXCM_STATUS_NO_ERROR)
                uxdir.Dispose();
            proxy = (Object)uxdir;
            return sts;
        }

        internal static pxcmStatus UnsubscribeEventINT(IntPtr instance, Object proxy)
        {
            UXEventHandlerDIR uxdir = (UXEventHandlerDIR)proxy;
            pxcmStatus sts = PXCMTouchlessController_UnsubscribeEvent(instance, uxdir.uDIR);
            uxdir.Dispose();
            return sts;
        }

        internal static pxcmStatus SubscribeAlertINT(IntPtr instance, OnFiredAlertDelegate alertHandler, out Object proxy)
        {
            AlertHandlerDIR adir = new AlertHandlerDIR(alertHandler);
            pxcmStatus sts = PXCMTouchlessController_SubscribeAlert(instance, adir.uDIR);
            if (sts < pxcmStatus.PXCM_STATUS_NO_ERROR)
                adir.Dispose();
            proxy = (Object)adir;
            return sts;
        }

        internal static pxcmStatus UnsubscribeAlertINT(IntPtr instance, Object proxy)
        {
            AlertHandlerDIR adir = (AlertHandlerDIR)proxy;
            pxcmStatus sts = PXCMTouchlessController_UnsubscribeAlert(instance, adir.uDIR);
            adir.Dispose();
            return sts;
        }

        internal static pxcmStatus AddGestureActionMappingINT(IntPtr instance, String gestureName, Action action, OnFiredActionDelegate actionHandler, out Object proxy)
        {
            ActionHandlerDIR adir = new ActionHandlerDIR(actionHandler);
            pxcmStatus sts = PXCMTouchlessController_AddGestureActionMapping(instance, gestureName, action, adir.uDIR);
            if (sts < pxcmStatus.PXCM_STATUS_NO_ERROR)
            {
                adir.Dispose();
            }
            proxy = (Object)adir;
            return sts;
        }

        public pxcmStatus SetScrollSensitivity(float sensitivity)
        {
            pxcmStatus sts = PXCMTouchlessController_SetScrollSensitivity(instance, sensitivity);
            return sts;
        }

        public pxcmStatus SetPointerSensitivity(PointerSensitivity sensitivity)
        {
            pxcmStatus sts = PXCMTouchlessController_SetPointerSensitivity(instance, sensitivity);
            return sts;
        }
    };

#if RSSDK_IN_NAMESPACE
}
#endif
