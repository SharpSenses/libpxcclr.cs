/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2013 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Runtime.InteropServices;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

    public partial class PXCMSyncPoint : PXCMBase
    {
        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMSyncPoint_Synchronize(IntPtr sp, Int32 timeout);

        [DllImport(PXCMBase.DLLNAME, EntryPoint = "PXCMSyncPoint_SynchronizeEx")]
        internal static extern pxcmStatus PXCMSyncPoint_SynchronizeExA(Int32 n1, IntPtr[] sps, out Int32 idx, Int32 timeout);

        [DllImport(PXCMBase.DLLNAME, EntryPoint = "PXCMSyncPoint_SynchronizeEx")]
        internal static extern pxcmStatus PXCMSyncPoint_SynchronizeExB(Int32 n1, IntPtr[] sps, out Int32 idx, Int32 timeout);
    };

#if RSSDK_IN_NAMESPACE
}
#endif
