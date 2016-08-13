/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2016 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections.Generic;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

public partial class PXCMHandCursorModule : PXCMBase
    {
        [DllImport(DLLNAME)]
        internal static extern IntPtr PXCMHandCursorModule_CreateActiveConfiguration(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern IntPtr PXCMHandCursorModule_CreateOutput(IntPtr instance);

       
    };

#if RSSDK_IN_NAMESPACE
}
#endif
