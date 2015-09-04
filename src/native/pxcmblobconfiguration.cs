/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2013-2014 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections.Generic;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif


    public partial class PXCMBlobConfiguration : PXCMBase
    {
        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMBlobConfiguration_ApplyChanges(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMBlobConfiguration_RestoreDefaults(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMBlobConfiguration_Update(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMBlobConfiguration_SetSegmentationSmoothing(IntPtr instance, Single smoothingValue);

        [DllImport(DLLNAME)]
        internal static extern Single PXCMBlobConfiguration_QuerySegmentationSmoothing(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMBlobConfiguration_SetContourSmoothing(IntPtr instance, Single smoothingValue);

        [DllImport(DLLNAME)]
        internal static extern Single PXCMBlobConfiguration_QueryContourSmoothing(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMBlobConfiguration_SetMaxBlobs(IntPtr instance, Int32 maxBlobs);

        [DllImport(DLLNAME)]
        internal static extern Int32 PXCMBlobConfiguration_QueryMaxBlobs(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMBlobConfiguration_SetMaxDistance(IntPtr instance, Single maxDistance);

        [DllImport(DLLNAME)]
        internal static extern Single PXCMBlobConfiguration_QueryMaxDistance(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMBlobConfiguration_SetMaxObjectDepth(IntPtr instance, Single maxDepth);

        [DllImport(DLLNAME)]
        internal static extern Single PXCMBlobConfiguration_QueryMaxObjectDepth(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMBlobConfiguration_SetMinPixelCount(IntPtr instance, Int32 minBlobSize);

        [DllImport(DLLNAME)]
        internal static extern Int32 PXCMBlobConfiguration_QueryMinPixelCount(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMBlobConfiguration_EnableSegmentationImage(IntPtr instance, [MarshalAs(UnmanagedType.Bool)] Boolean enableFlag);

        [DllImport(DLLNAME)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern Boolean PXCMBlobConfiguration_IsSegmentationImageEnabled(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMBlobConfiguration_EnableContourExtraction(IntPtr instance, [MarshalAs(UnmanagedType.Bool)] Boolean enableFlag);

        [DllImport(DLLNAME)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern Boolean PXCMBlobConfiguration_IsContourExtractionEnabled(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMBlobConfiguration_SetMinContourSize(IntPtr instance, Int32 minContourSize);

        [DllImport(DLLNAME)]
        internal static extern Int32 PXCMBlobConfiguration_QueryMinContourSize(IntPtr instance);


        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMBlobConfiguration_EnableStabilizer(IntPtr instance, [MarshalAs(UnmanagedType.Bool)] Boolean enableFlag);

        [DllImport(DLLNAME)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern Boolean PXCMBlobConfiguration_IsStabilizerEnabled(IntPtr instance);


       [DllImport(DLLNAME)]
       internal static extern pxcmStatus PXCMBlobConfiguration_SetMaxPixelCount(IntPtr instance, Int32 maxBlobSize);

       [DllImport(DLLNAME)]
       internal static extern Int32 PXCMBlobConfiguration_QueryMaxPixelCount(IntPtr instance);

       [DllImport(DLLNAME)]
       internal static extern pxcmStatus PXCMBlobConfiguration_SetMaxBlobArea(IntPtr instance, Single maxBlobArea);

       [DllImport(DLLNAME)]
       internal static extern Single PXCMBlobConfiguration_QueryMaxBlobArea(IntPtr instance);

       [DllImport(DLLNAME)]
       internal static extern pxcmStatus PXCMBlobConfiguration_SetMinBlobArea(IntPtr instance, Single minBlobArea);

       [DllImport(DLLNAME)]
       internal static extern Single PXCMBlobConfiguration_QueryMinBlobArea(IntPtr instance);

       [DllImport(DLLNAME)]
       internal static extern pxcmStatus PXCMBlobConfiguration_SetBlobSmoothing(IntPtr instance, Single smoothingValue);

       [DllImport(DLLNAME)]
       internal static extern Single PXCMBlobConfiguration_QueryBlobSmoothing(IntPtr instance); 

        [DllImport(DLLNAME)]
       internal static extern pxcmStatus PXCMBlobConfiguration_EnableColorMapping(IntPtr instance, [MarshalAs(UnmanagedType.Bool)] Boolean enableFlag);

        [DllImport(DLLNAME)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern Boolean PXCMBlobConfiguration_IsColorMappingEnabled(IntPtr instance);


    };

#if RSSDK_IN_NAMESPACE
}
#endif
