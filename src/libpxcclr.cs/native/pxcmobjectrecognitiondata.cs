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

    public partial class PXCMObjectRecognitionData : PXCMBase
    {
        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMObjectRecognitionbData_Update(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern Int32 PXCMObjectRecognitionbData_QueryNumberOfRecognizedObjects(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMObjectRecognitionData_QuerySegmentedImage(IntPtr instance, Int32 roiIndex, out IntPtr image);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMObjectRecognitionData_QueryRecognizedObjectData(IntPtr instance, Int32 index, Int32 probIndex, [Out] RecognizedObjectData objectData);

        [DllImport(DLLNAME, CharSet = CharSet.Unicode)]
        private static extern void PXCMObjectRecognitionData_QueryObjectNameByID(IntPtr instance, Int32 objectLabel, StringBuilder objectName);

        internal static String QueryObjectNameByIDINT(IntPtr instance, Int32 objectLabel)
        {
            StringBuilder sb = new StringBuilder(PXCMObjectRecognitionData.MAX_OJBECT_NAME_SIZE);
            PXCMObjectRecognitionData_QueryObjectNameByID(instance, objectLabel, sb);
            return sb.ToString();
        }
    
    };


#if RSSDK_IN_NAMESPACE
}
#endif
