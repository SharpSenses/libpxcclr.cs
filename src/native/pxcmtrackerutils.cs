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
namespace intel.rssdk {
#endif

    public partial class PXCMTrackerUtils : PXCMBase
    {
        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMTrackerUtils_Start3DMapCreation(IntPtr util, ObjectSize objSize);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMTrackerUtils_Cancel3DMapCreation(IntPtr util);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMTrackerUtils_Start3DMapExtension(IntPtr util);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMTrackerUtils_Cancel3DMapExtension(IntPtr util);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMTrackerUtils_Load3DMap(IntPtr util, String filename);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMTrackerUtils_Save3DMap(IntPtr util, String filename);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern Int32 PXCMTrackerUtils_QueryNumberFeaturePoints(IntPtr util);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern Int32 PXCMTrackerUtils_QueryFeaturePoints(IntPtr util, Int32 size, out IntPtr ppoints, Boolean returnActive, Boolean returnInactive);

        internal static Int32 QueryFeaturePointsINT(IntPtr util, Int32 size, PXCMPoint3DF32[] points, Boolean returnActive, Boolean returnInactive)
        {
            if (size == 0 || points==null) return 0;

            IntPtr ppoints = IntPtr.Zero;
            Int32 numPoints = PXCMTrackerUtils_QueryFeaturePoints(util, size, out ppoints, returnActive, returnInactive);
            for (int i = 0; i < Math.Min(size, numPoints); i++)
            {
                Marshal.PtrToStructure(ppoints, points[i]);
                ppoints = new IntPtr(ppoints.ToInt64() + Marshal.SizeOf(typeof(PXCMPoint3DF32)));
            }
            return Math.Min(size, numPoints);
        }


        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMTrackerUtils_Start3DMapAlignment(IntPtr util, Int32 markerID, Int32 markerSize);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMTrackerUtils_Cancel3DMapAlignment(IntPtr util);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern Boolean    PXCMTrackerUtils_Is3DMapAlignmentComplete(IntPtr util);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMTrackerUtils_StartCameraCalibration(IntPtr util, Int32 markerID, Int32 markerSize);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMTrackerUtils_CancelCameraCalibration(IntPtr util);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern Int32      PXCMTrackerUtils_QueryCalibrationProgress(IntPtr util);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMTrackerUtils_SaveCameraParametersToFile(IntPtr util, String filename);
 
    };

#if RSSDK_IN_NAMESPACE
}
#endif