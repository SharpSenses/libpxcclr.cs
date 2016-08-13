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

    public partial class PXCMPowerState : PXCMBase
    {
        [DllImport(PXCMBase.DLLNAME)]
        internal static extern State PXCMPowerState_QueryState(IntPtr ps);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMPowerState_SetState(IntPtr ps, State state);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMPowerState_SetInactivityInterval(IntPtr ps, int timeInSeconds);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern int PXCMPowerState_QueryInactivityInterval(IntPtr ps);
};

#if RSSDK_IN_NAMESPACE
}
#endif
