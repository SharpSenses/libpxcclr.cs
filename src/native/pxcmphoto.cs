/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2015 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

    public partial class PXCMPhoto : PXCMBase
    {
        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCMPhoto_QueryReferenceImage(IntPtr photo);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCMPhoto_QueryDepthImage(IntPtr photo);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMPhoto_CopyPhoto(IntPtr photo, IntPtr source);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMPhoto_ImportFromPreviewSample(IntPtr photo, PXCMCapture.Sample sample);

        [DllImport(PXCMBase.DLLNAME, CharSet = CharSet.Unicode)]
        internal static extern pxcmStatus PXCMPhoto_LoadXDM(IntPtr photo, String filename);

        [DllImport(PXCMBase.DLLNAME, CharSet = CharSet.Unicode)]
        internal static extern pxcmStatus PXCMPhoto_SaveXDM(IntPtr photo, String filename);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCMPhoto_QueryOriginalImage(IntPtr photo);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCMPhoto_QueryRawDepthImage(IntPtr photo);
    };

#if RSSDK_IN_NAMESPACE
}
#endif
