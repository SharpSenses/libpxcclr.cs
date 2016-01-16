/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2013 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections.Generic;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

    public partial class PXCMHandModule : PXCMBase
    {
        [DllImport(DLLNAME)]
        internal static extern IntPtr PXCMHandModule_CreateActiveConfiguration(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern IntPtr PXCMHandModule_CreateOutput(IntPtr instance);
    };

#if RSSDK_IN_NAMESPACE
}
#endif
