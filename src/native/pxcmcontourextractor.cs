/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2013-2014 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Runtime.InteropServices;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

    public partial class PXCMContourExtractor : PXCMBase
    {

        [DllImport(DLLNAME)]
        internal static extern void PXCMContourExtractor_Init(IntPtr instsance, PXCMImage.ImageInfo imageInfo);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMContourExtractor_ProcessImage(IntPtr instsance, IntPtr depthImage);

        [DllImport(DLLNAME)]
        private static extern pxcmStatus PXCMContourExtractor_QueryContourData(IntPtr instsance, Int32 index, Int32 max, [Out] PXCMPointI32[] contour);

        internal static pxcmStatus QueryContourDataINT(IntPtr instance, Int32 index, out PXCMPointI32[] contour)
        {
            Int32 size = PXCMContourExtractor_QueryContourSize(instance, index);
            if (size > 0)
            {
                contour = new PXCMPointI32[size];
                return PXCMContourExtractor_QueryContourData(instance, index, size, contour);
            }
            else
            {
                contour = null;
                return pxcmStatus.PXCM_STATUS_DATA_UNAVAILABLE;
            }
        }

        [DllImport(DLLNAME)]
        private static extern Int32 PXCMContourExtractor_QueryContourSize(IntPtr instsance, Int32 index);

        [DllImport(DLLNAME)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern Boolean PXCMContourExtractor_IsContourOuter(IntPtr instsance, Int32 index);

        [DllImport(DLLNAME)]
        internal static extern Int32 PXCMContourExtractor_QueryNumberOfContours(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMContourExtractor_SetSmoothing(IntPtr instance, Single smoothing);

        [DllImport(DLLNAME)]
        internal static extern Single PXCMContourExtractor_QuerySmoothing(IntPtr instance);
    };

#if RSSDK_IN_NAMESPACE
}
#endif
