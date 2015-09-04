/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
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

    public partial class PXCMBlobModule : PXCMBase
    {
        [DllImport(DLLNAME)]
        internal static extern IntPtr PXCMBlobModule_CreateActiveConfiguration(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern IntPtr PXCMBlobModule_CreateOutput(IntPtr instance);
    };

#if RSSDK_IN_NAMESPACE
}
#endif
