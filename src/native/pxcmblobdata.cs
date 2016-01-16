/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2013 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections.Generic;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

    public partial class PXCMBlobData : PXCMBase
    {


        public partial class IContour
        {
            [DllImport(DLLNAME)]
            internal static extern Int32 PXCMBlobData_IContour_QuerySize(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern pxcmStatus PXCMBlobData_IContour_QueryPoints(IntPtr instance,Int32 max, [Out] PXCMPointI32[] contour);

            internal static pxcmStatus QueryDataINT(IntPtr instance, out PXCMPointI32[] contour)
            {
                Int32 size = PXCMBlobData_IContour_QuerySize(instance);
                if (size > 0)
                {
                    contour = new PXCMPointI32[size];
                    return PXCMBlobData_IContour_QueryPoints(instance, size, contour);
                }
                else
                {
                    contour = null;
                    return pxcmStatus.PXCM_STATUS_DATA_UNAVAILABLE;
                }
            }

            [DllImport(DLLNAME)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern Boolean PXCMBlobData_IContour_IsOuter(IntPtr instance);
        };



        public partial class IBlob
        {
            [DllImport(DLLNAME)]
            internal static extern pxcmStatus PXCMBlobData_IBlob_QuerySegmentationImage(IntPtr instance, out IntPtr image);


            [DllImport(DLLNAME)]
            internal static extern PXCMPoint3DF32 PXCMBlobData_IBlob_QueryExtremityPoint(IntPtr instance, ExtremityType extremityLabel);
           

            internal static PXCMPoint3DF32 QueryExtremityPointINT(IntPtr instance, ExtremityType extremityLabel)
            {
                return PXCMBlobData_IBlob_QueryExtremityPoint(instance, extremityLabel);
            }

            [DllImport(DLLNAME)]
            internal static extern Int32 PXCMBlobData_IBlob_QueryPixelCount(IntPtr instance);

            
            [DllImport(DLLNAME)]
            internal static extern Int32 PXCMBlobData_IBlob_QueryNumberOfContours(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern pxcmStatus PXCMBlobData_IBlob_QueryContourPoints(IntPtr instsance, Int32 index, Int32 max, [Out] PXCMPointI32[] contour);
          
            internal static pxcmStatus QueryContourDataINT(IntPtr instance, Int32 index,out PXCMPointI32[] contour)
            {
                Int32 size = PXCMBlobData_IBlob_QueryContourSize(instance, index);
                if (size > 0)
                {
                    contour = new PXCMPointI32[size];
                    return PXCMBlobData_IBlob_QueryContourPoints(instance, index, size, contour);
                }
                else
                {
                    contour = null;
                    return pxcmStatus.PXCM_STATUS_DATA_UNAVAILABLE;
                }
            }

            [DllImport(DLLNAME)]
            internal static extern Int32 PXCMBlobData_IBlob_QueryContourSize(IntPtr instance, Int32 index);

            [DllImport(DLLNAME)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern Boolean PXCMBlobData_IBlob_IsContourOuter(IntPtr instance, Int32 index);

            [DllImport(DLLNAME)]
            internal static extern pxcmStatus PXCMBlobData_IBlob_QueryContour(IntPtr instance, Int32 index, out IntPtr contourData);

            [DllImport(DLLNAME)]
            internal static extern void PXCMBlobData_IBlob_QueryBoundingBoxImage(IntPtr instance, ref PXCMRectI32 rect);

            [DllImport(DLLNAME)]
            internal static extern void PXCMBlobData_IBlob_QueryBoundingBoxWorld(IntPtr instance, ref PXCMBox3DF32 rect);
            


        };

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMBlobData_Update(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern Int32 PXCMBlobData_QueryNumberOfBlobs(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMBlobData_QueryBlobByAccessOrder(IntPtr instance, Int32 index, AccessOrderType accessOrder, out IntPtr blobData);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMBlobData_QueryBlob(IntPtr instance, Int32 index, SegmentationImageType segmentationImageType, AccessOrderType accessOrder, out IntPtr blobData);
    };


#if RSSDK_IN_NAMESPACE
}
#endif
