/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2013-2014 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Runtime.InteropServices;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk {
#endif

public partial class PXCM3DScan:PXCMBase {

    internal partial class AlertHandlerDIR : IDisposable
    {
        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCM3DScan_AllocHandlerDIR(IntPtr d);
        [DllImport(PXCMBase.DLLNAME)]
        internal static extern void PXCM3DScan_FreeHandlerDIR(IntPtr hdir);

        private GCHandle gch;
        internal OnAlertDelegate handler;
        internal IntPtr dirUnmanaged;

        public void Dispose()
        {
            if (dirUnmanaged == IntPtr.Zero) return;

            PXCM3DScan_FreeHandlerDIR(dirUnmanaged);
            dirUnmanaged = IntPtr.Zero;

            if (gch.IsAllocated) gch.Free();
        }

        public AlertHandlerDIR(OnAlertDelegate d)
        {
            handler = d;
            gch = GCHandle.Alloc(handler);
            dirUnmanaged = PXCM3DScan_AllocHandlerDIR(Marshal.GetFunctionPointerForDelegate(d));
        }

        ~AlertHandlerDIR()
        {
            Dispose();
        }
    };

    [DllImport(PXCMBase.DLLNAME)]
    internal static extern void PXCM3DScan_QueryConfiguration(IntPtr scan, [Out] Configuration config);

    [DllImport(PXCMBase.DLLNAME)]
    internal static extern pxcmStatus PXCM3DScan_SetConfiguration(IntPtr scan, Configuration config);

    [DllImport(PXCMBase.DLLNAME)]
    internal static extern void PXCM3DScan_QueryArea(IntPtr scan, [Out] Area area);

    [DllImport(PXCMBase.DLLNAME)]
    internal static extern pxcmStatus PXCM3DScan_SetArea(IntPtr scan, Area area);

    [DllImport(PXCMBase.DLLNAME)]
    internal static extern IntPtr PXCM3DScan_AcquirePreviewImage(IntPtr scan);

    [DllImport(PXCMBase.DLLNAME)]
    internal static extern bool PXCM3DScan_IsScanning(IntPtr scan);

    [DllImport(PXCMBase.DLLNAME,CharSet=CharSet.Unicode)]
    internal static extern pxcmStatus PXCM3DScan_Reconstruct(IntPtr scan, FileFormat format, String filename);

    [DllImport(PXCMBase.DLLNAME)]
    internal static extern void PXCM3DScan_Subscribe(IntPtr seg, IntPtr handler);


    private void SubscribeINT(OnAlertDelegate d)
    {
        if (handler != null)
        {
            handler.Dispose();
            handler = null;
        }

        if (d != null)
        {
            handler = new AlertHandlerDIR(d);
            PXCM3DScan_Subscribe(instance, handler.dirUnmanaged);
        }
        else
        {
            PXCM3DScan_Subscribe(instance, IntPtr.Zero);
        }
    }

    private AlertHandlerDIR handler = null;
};

#if RSSDK_IN_NAMESPACE
}
#endif