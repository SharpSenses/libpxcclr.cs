/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2014 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

    public partial class PXCMAudioSource : PXCMBase
    {
        [DllImport(DLLNAME)]
        internal extern static void PXCMAudioSource_ScanDevices(IntPtr source);

        [DllImport(DLLNAME)]
        internal extern static Int32 PXCMAudioSource_QueryDeviceNum(IntPtr source);

        [DllImport(DLLNAME)]
        internal extern static pxcmStatus PXCMAudioSource_QueryDeviceInfo(IntPtr source, Int32 didx, [Out] DeviceInfo dinfo);

        internal static pxcmStatus QueryDeviceInfoINT(IntPtr source, Int32 didx, out DeviceInfo dinfo)
        {
            dinfo = new DeviceInfo();
            return PXCMAudioSource_QueryDeviceInfo(source, didx, dinfo);
        }

        [DllImport(DLLNAME)]
        internal extern static pxcmStatus PXCMAudioSource_SetDevice(IntPtr source, DeviceInfo dinfo);

        [DllImport(DLLNAME)]
        internal extern static Single PXCMAudioSource_QueryVolume(IntPtr source);

        [DllImport(DLLNAME)]
        internal extern static pxcmStatus PXCMAudioSource_SetVolume(IntPtr source, Single volume);
    };

#if RSSDK_IN_NAMESPACE
}
#endif
