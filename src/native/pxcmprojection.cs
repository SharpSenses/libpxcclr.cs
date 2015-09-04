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

    public partial class PXCMProjection : PXCMBase
    {
        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMProjection_MapDepthToColor(IntPtr p, Int32 npoints, PXCMPoint3DF32[] pos_uvz, [Out] PXCMPointF32[] pos_ij);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMProjection_MapColorToDepth(IntPtr p, IntPtr depth, Int32 npoints, PXCMPointF32[] pos_ij, [Out] PXCMPointF32[] pos_uv);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMProjection_ProjectDepthToCamera(IntPtr p, Int32 npoints, PXCMPoint3DF32[] pos_uvz, [Out] PXCMPoint3DF32[] pos3d);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMProjection_ProjectColorToCamera(IntPtr p, Int32 npoints, PXCMPoint3DF32[] pos_ijz, [Out] PXCMPoint3DF32[] pos3d);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMProjection_ProjectCameraToDepth(IntPtr p, Int32 npoints, PXCMPoint3DF32[] pos3d, [Out] PXCMPointF32[] pos_uv);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMProjection_ProjectCameraToColor(IntPtr p, Int32 npoints, PXCMPoint3DF32[] pos3d, [Out] PXCMPointF32[] pos_ij);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMProjection_QueryUVMap(IntPtr instance, IntPtr depth, [Out] PXCMPointF32[] uvmap);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMProjection_QueryInvUVMap(IntPtr instance, IntPtr depth, [Out] PXCMPointF32[] inv_uvmap);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMProjection_QueryVertices(IntPtr instance, IntPtr depth, [Out] PXCMPoint3DF32[] vertices);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCMProjection_CreateColorImageMappedToDepth(IntPtr instance, IntPtr depth, IntPtr color);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCMProjection_CreateDepthImageMappedToColor(IntPtr instance, IntPtr depth, IntPtr color);
    };

#if RSSDK_IN_NAMESPACE
}
#endif
