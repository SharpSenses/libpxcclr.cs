/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2013-2015 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Runtime.InteropServices;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

    public partial class PXCMCalibration : PXCMBase
    {
        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMCalibration_QueryStreamProjectionParameters(IntPtr instance, PXCMCapture.StreamType streamType, [Out] StreamCalibration calibration, [Out] StreamTransform transformation);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMCalibration_QueryStreamProjectionParametersEx(IntPtr instance, PXCMCapture.StreamType streamType, PXCMCapture.Device.StreamOption options, [Out] StreamCalibration calibration, [Out] StreamTransform transformation);
    }

#if RSSDK_IN_NAMESPACE
}
#endif
