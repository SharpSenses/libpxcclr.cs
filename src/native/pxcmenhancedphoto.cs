/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2015 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

    public partial class PXCMEnhancedPhoto : PXCMBase
    {
        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMEnhancedPhoto_MeasureDistance(IntPtr ep, IntPtr sample, PXCMPointI32 startPoint, PXCMPointI32 endPoint, [Out] MeasureData outData);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCMEnhancedPhoto_DepthRefocus(IntPtr ep, IntPtr sample, PXCMPointI32 focusPoint, Single aperture);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCMEnhancedPhoto_EnhanceDepth(IntPtr ep, IntPtr sample, DepthFillQuality depthQuality);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern Single PXCMEnhancedPhoto_GetDepthThreshold(IntPtr ep, IntPtr sample, PXCMPointI32 coord);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCMEnhancedPhoto_ComputeMaskFromThreshold(IntPtr ep, IntPtr sample, Single depthThreshold);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCMEnhancedPhoto_ComputeMaskFromCoordinate(IntPtr ep, IntPtr sample, PXCMPointI32 coord);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCMEnhancedPhoto_DepthResize(IntPtr ep, IntPtr sample, PXCMSizeI32 size);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCMEnhancedPhoto_PasteOnPlane(IntPtr ep, IntPtr sample, IntPtr imbedImInstance, PXCMPointI32 topLeftCoord, PXCMPointI32 bottomLeftCoord);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCMEnhancedPhoto_DepthBlend(IntPtr ep, IntPtr sampleBackground, IntPtr imgForegroundInstance,
            PXCMPointI32 insertCoord, Int32 insertDepth, Int32[] rotation, Single scaleFG);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCMEnhancedPhoto_ObjectSegment(IntPtr ep, IntPtr photo, PXCMPointI32 topLeftCoord, PXCMPointI32 bottomRightCoord);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCMEnhancedPhoto_RefineMask(IntPtr ep, IntPtr hints);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMEnhancedPhoto_InitMotionEffect(IntPtr ep, IntPtr sample);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCMEnhancedPhoto_ApplyMotionEffect(IntPtr ep, Single[] motion, Single[] rotation, Single zoomFactor);
       
       //[DllImport(PXCMBase.DLLNAME)]
       //internal static extern pxcmStatus PXCMEnhancedPhoto_InitTracker(IntPtr ep, IntPtr firstFrame, IntPtr boundingMask, TrackMethod method);

      //[DllImport(PXCMBase.DLLNAME)]
      //internal static extern IntPtr PXCMEnhancedPhoto_TrackObject(IntPtr ep, IntPtr nextFrame);
    };

#if RSSDK_IN_NAMESPACE
}
#endif
