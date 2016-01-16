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


    public partial class PXCMObjectRecognitionConfiguration : PXCMBase
    {
        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMObjectRecognitionConfiguration_ApplyChanges(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMObjectRecognitionConfiguration_RestoreDefaults(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern void PXCMObjectRecognitionConfiguration_QueryRecognitionConfiguration(IntPtr instance, RecognitionConfiguration rc);

        [DllImport(DLLNAME)]
        internal static extern Int32 PXCMObjectRecognitionConfiguration_QueryNumberOfClasses(IntPtr instance);
         
        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMObjectRecognitionConfiguration_SetRecognitionConfiguration(IntPtr instance, RecognitionConfiguration config);
         

        [DllImport(DLLNAME, CharSet = CharSet.Unicode)]
        private static extern void PXCMObjectRecognitionConfiguration_QueryActiveClassifier(IntPtr instance, StringBuilder classifer);

        internal static String QueryActiveClassifierINT(IntPtr instance)
        {
            StringBuilder classifer = new StringBuilder(PXCMObjectRecognitionConfiguration.MAX_PATH_NAME);
            PXCMObjectRecognitionConfiguration_QueryActiveClassifier(instance, classifer);
            return classifer.ToString();
        } 

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMObjectRecognitionConfiguration_SetActiveClassifier(IntPtr instance, [MarshalAs(UnmanagedType.LPWStr)] String configFilePath);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMObjectRecognitionConfiguration_EnableSegmentation(IntPtr instance, Boolean enableFlag);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMObjectRecognitionConfiguration_EnableExplicitProcessing(IntPtr instance, Boolean enableFlag);

        [DllImport(DLLNAME)]
        internal static extern Boolean PXCMObjectRecognitionConfiguration_IsSegmentationEnabled(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern Boolean PXCMObjectRecognitionConfiguration_IsAbsoluteRoiEnabled(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern Boolean PXCMObjectRecognitionConfiguration_IsExplicitProcessingEnabled(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMObjectRecognitionConfiguration_AddROI(IntPtr instance, PXCMRectF32 roi);
        
        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMObjectRecognitionConfiguration_AddAbsoluteROI(IntPtr instance, PXCMRectF32 roi);

        [DllImport(DLLNAME)]
        internal static extern void PXCMObjectRecognitionConfiguration_QueryROI(IntPtr instance, ref PXCMRectF32 roi);

        [DllImport(DLLNAME)]
        internal static extern void PXCMObjectRecognitionConfiguration_QueryAbsoluteROI(IntPtr instance, ref PXCMRectI32 roi);


        [DllImport(DLLNAME)]
        internal static extern void PXCMObjectRecognitionConfiguration_ClearAllROIs(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern void PXCMObjectRecognitionConfiguration_EnableAbsoluteROI(IntPtr instance, Boolean enable);
    };

#if RSSDK_IN_NAMESPACE
}
#endif
