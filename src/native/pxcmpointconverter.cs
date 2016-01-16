



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

public partial class PXCMPointConverter : PXCMBase
    {
        [DllImport(DLLNAME)]
        internal extern static pxcmStatus PXCMPointConverter_Set2DSourceRectangle(IntPtr instance, PXCMRectF32 rectangle2D);

        [DllImport(DLLNAME)]
        internal extern static pxcmStatus PXCMPointConverter_Set2DTargetRectangle(IntPtr instance, PXCMRectF32 rectangle2D);

        [DllImport(DLLNAME)]
        internal extern static pxcmStatus PXCMPointConverter_Set2DPoint(IntPtr instance, PXCMPointF32 point2D);

        [DllImport(DLLNAME)]
        internal extern static PXCMPointF32 PXCMPointConverter_GetConverted2DPoint(IntPtr instance);

        [DllImport(DLLNAME)]
        internal extern static pxcmStatus PXCMPointConverter_Invert2DAxis(IntPtr instance, Boolean x, Boolean y);

        [DllImport(DLLNAME)]
        internal extern static pxcmStatus PXCMPointConverter_Set3DSourceBox(IntPtr instance, PXCMBox3DF32 box3D);

        [DllImport(DLLNAME)]
        internal extern static pxcmStatus PXCMPointConverter_Set3DTargetBox(IntPtr instance, PXCMBox3DF32 box3D);

        [DllImport(DLLNAME)]
        internal extern static pxcmStatus PXCMPointConverter_Set3DPoint(IntPtr instance, PXCMPoint3DF32 point3D);

        [DllImport(DLLNAME)]
        internal extern static PXCMPoint3DF32 PXCMPointConverter_GetConverted3DPoint(IntPtr instance);

        [DllImport(DLLNAME)]
        internal extern static pxcmStatus PXCMPointConverter_Invert3DAxis(IntPtr instance, Boolean x, Boolean y, Boolean z);

    }

#if RSSDK_IN_NAMESPACE
}
#endif