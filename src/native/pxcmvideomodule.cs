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

    public partial class PXCMVideoModule : PXCMBase
    {
        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMVideoModule_QueryCaptureProfile(IntPtr module, Int32 pidx, [Out] DataDesc inputs);

        /** 
            @brief Return the available module input descriptors.
            @param[in]  pidx					The zero-based index used to retrieve all configurations.
            @param[out] inputs					The module input descriptor, to be returned.
            @return PXCM_STATUS_NO_ERROR		Successful execution.
            @return PXCM_STATUS_ITEM_UNAVAILABLE	No specified input descriptor is not available.
        */
        public pxcmStatus QueryCaptureProfile(Int32 pidx, out DataDesc inputs)
        {
            inputs = new DataDesc();
            return PXCMVideoModule_QueryCaptureProfile(instance, pidx, inputs);
        }

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMVideoModule_SetCaptureProfile(IntPtr module, DataDesc inputs);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMVideoModule_ProcessImageAsync(IntPtr module, PXCMCapture.Sample sample, out IntPtr sp);
    };

#if RSSDK_IN_NAMESPACE
}
#endif
