/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2015 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections.Generic;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

    public partial class PXCMPhoto : PXCMBase
    {
        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMPhoto_ImportFromPreviewSample(IntPtr photo, PXCMCapture.Sample sample);

        [DllImport(PXCMBase.DLLNAME, CharSet = CharSet.Unicode)]
        internal static extern bool PXCMPhoto_IsXDM(IntPtr photo, String filename);
        
        [DllImport(PXCMBase.DLLNAME, CharSet = CharSet.Unicode)]
        internal static extern pxcmStatus PXCMPhoto_LoadXDM(IntPtr photo, String filename, Subsample subsample);

        [DllImport(PXCMBase.DLLNAME, CharSet = CharSet.Unicode)]
        internal static extern pxcmStatus PXCMPhoto_SaveXDM(IntPtr photo, String filename, Boolean removeOriginalImage);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMPhoto_CopyPhoto(IntPtr photo, IntPtr source);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCMPhoto_QueryContainerImage(IntPtr photo);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMPhoto_ResetContainerImage(IntPtr photo);
        
        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCMPhoto_QueryDepth(IntPtr photo, Int32 camIdx);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCMPhoto_QueryImage(IntPtr photo, Int32 camIdx);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCMPhoto_QueryRawDepth(IntPtr photo);

        [DllImport(PXCMBase.DLLNAME, CharSet = CharSet.Unicode)]
        private static extern void PXCMPhoto_QueryXDMRevision(IntPtr photo, StringBuilder name, int size);

        internal static String QueryXDMRevisionINT(IntPtr photo)
        {
            StringBuilder sb = new StringBuilder(PXCMPhoto.MAX_SIZE);
            PXCMPhoto_QueryXDMRevision(photo, sb, sb.Capacity);
            return sb.ToString();
        
        }

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern void PXCMPhoto_QueryDeviceVendorInfo(IntPtr photo, [Out] PXCMPhoto.VendorInfo vendorInfo);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern void PXCMPhoto_QueryCameraVendorInfo(IntPtr photo, Int32 camIdx, [Out] PXCMPhoto.VendorInfo vendorInfo);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern Int32 PXCMPhoto_QueryNumberOfCameras(IntPtr photo);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern void PXCMPhoto_QueryCameraPose(IntPtr photo, Int32 camIdx, ref PXCMPoint3DF32 translation, ref PXCMPoint4DF32 rotation);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern void PXCMPhoto_QueryCameraPerspectiveModel(IntPtr photo, Int32 camIdx, [Out] PXCMPhoto.PerspectiveCameraModel model);
    
        [DllImport(PXCMBase.DLLNAME)]
        internal static extern bool PXCMPhoto_CheckSignature(IntPtr photo);
    };

#if RSSDK_IN_NAMESPACE
}
#endif
