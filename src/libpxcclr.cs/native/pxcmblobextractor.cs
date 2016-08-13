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
    public partial class PXCMBlobExtractor : PXCMBase
    {

        [DllImport(DLLNAME)]
        internal static extern void PXCMBlobExtractor_Init(IntPtr instsance, PXCMImage.ImageInfo imageInfo);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMBlobExtractor_ProcessImage(IntPtr instance, IntPtr depthImage);

        [DllImport(DLLNAME)]
        private static extern pxcmStatus PXCMBlobExtractor_QueryBlobData(IntPtr instance, Int32 index, IntPtr segmentationImage, [Out] BlobData blobData);

        internal static pxcmStatus QueryBlobDataINT(IntPtr instance, Int32 index, IntPtr segmentationImage, out BlobData blobData)
        {
            blobData = new BlobData();

            return PXCMBlobExtractor_QueryBlobData(instance, index, segmentationImage, blobData);
        }

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMBlobExtractor_SetMaxBlobs(IntPtr instance, Int32 maxBlobs);

        [DllImport(DLLNAME)]
        internal static extern Int32 PXCMBlobExtractor_QueryMaxBlobs(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern Int32 PXCMBlobExtractor_QueryNumberOfBlobs(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMBlobExtractor_SetMaxDistance(IntPtr instance, Single maxDistance);

        [DllImport(DLLNAME)]
        internal static extern Single PXCMBlobExtractor_QueryMaxDistance(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMBlobExtractor_SetMaxObjectDepth(IntPtr instance, Single maxDepth);

        [DllImport(DLLNAME)]
        internal static extern Single PXCMBlobExtractor_QueryMaxObjectDepth(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMBlobExtractor_SetSmoothing(IntPtr instance, Single smoothing);

        [DllImport(DLLNAME)]
        internal static extern Single PXCMBlobExtractor_QuerySmoothing(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMBlobExtractor_SetClearMinBlobSize(IntPtr instance, Single minBlobSize);

        [DllImport(DLLNAME)]
        internal static extern Single PXCMBlobExtractor_QueryClearMinBlobSize(IntPtr instance);
    };

#if RSSDK_IN_NAMESPACE
}
#endif
