/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2015 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

    public partial class PXCMEnhancedVideo : PXCMBase
    {
        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMEnhancedVideo_EnableTracker(IntPtr ev, IntPtr boundingMask, TrackMethod method);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCMEnhancedVideo_QueryTrackedObject(IntPtr ev);
    
    };

#if RSSDK_IN_NAMESPACE
}
#endif
