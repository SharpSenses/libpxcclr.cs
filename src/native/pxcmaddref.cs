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


    public partial class PXCMAddRef : PXCMBase
    {
        [DllImport(PXCMBase.DLLNAME)]
        internal static extern Int32 PXCMAddRef_AddRef(IntPtr addref);
    };


#if RSSDK_IN_NAMESPACE
}
#endif
