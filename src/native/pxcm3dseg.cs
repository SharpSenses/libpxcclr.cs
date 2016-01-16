/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2013-2014 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

// This implementation is based off of the handconfiguration callback:
// \sw\sdk\sdk\trunk\framework\common\pxcclr.cs\src\native\pxcmhandconfiguration.cs

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

    public partial class PXCM3DSeg : PXCMBase
    {
        internal partial class AlertHandlerDIR : IDisposable
        {
            [DllImport(PXCMBase.DLLNAME)]
            internal static extern IntPtr PXCM3DSeg_AllocHandlerDIR(IntPtr d);
            [DllImport(PXCMBase.DLLNAME)]
            internal static extern void PXCM3DSeg_FreeHandlerDIR(IntPtr hdir);

            // internal delegate void OnAlertDelegate(IntPtr data);

            private GCHandle gch;
            internal OnAlertDelegate handler;
            internal IntPtr dirUnmanaged;

            public void Dispose()
            {
                if (dirUnmanaged == IntPtr.Zero) return;

                PXCM3DSeg_FreeHandlerDIR(dirUnmanaged);
                dirUnmanaged = IntPtr.Zero;

                if (gch.IsAllocated) gch.Free();
            }

            public AlertHandlerDIR(OnAlertDelegate d)
            {
                handler = d;
                gch = GCHandle.Alloc(handler);
                dirUnmanaged = PXCM3DSeg_AllocHandlerDIR(Marshal.GetFunctionPointerForDelegate(d));
            }

            ~AlertHandlerDIR()
            {
                Dispose();
            }
        };

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCM3DSeg_AcquireSegmentedImage(IntPtr seg);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern void PXCM3DSeg_Subscribe(IntPtr seg, IntPtr handler);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCM3DSeg_SetFrameSkipInterval(IntPtr seg, int skipInterval);

        private void SubscribeINT(OnAlertDelegate d)
        {
            if (handler != null)
            {
                handler.Dispose();
                handler = null;
            }

            if (d != null)
            {
                handler=new AlertHandlerDIR(d);
                PXCM3DSeg_Subscribe(instance, handler.dirUnmanaged);
            }
            else
            {
                PXCM3DSeg_Subscribe(instance, IntPtr.Zero);
            }
        }

        private AlertHandlerDIR handler = null;
    };

#if RSSDK_IN_NAMESPACE
}
#endif
