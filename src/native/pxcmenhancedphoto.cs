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
        internal static extern pxcmStatus PXCMEnhancedPhoto_MeasureDistance(IntPtr ep, IntPtr photo, PXCMPointI32 startPoint, PXCMPointI32 endPoint, [Out] MeasureData outData);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCMEnhancedPhoto_DepthRefocus(IntPtr ep, IntPtr photo, PXCMPointI32 focusPoint, Single aperture);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCMEnhancedPhoto_ComputeMaskFromThreshold(IntPtr ep, IntPtr photo, Single depthThreshold, PXCMEnhancedPhoto.MaskParams maskParams);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCMEnhancedPhoto_ComputeMaskFromCoordinate(IntPtr ep, IntPtr photo, PXCMPointI32 coord, PXCMEnhancedPhoto.MaskParams maskParams);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMEnhancedPhoto_InitMotionEffect(IntPtr ep, IntPtr photo);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCMEnhancedPhoto_ApplyMotionEffect(IntPtr ep, Single[] motion, Single[] rotation, Single zoomFactor);

        public partial class PhotoUtils : PXCMBase
        {
            [DllImport(PXCMBase.DLLNAME)]
            internal static extern IntPtr PXCMEnhancedPhoto_EnhanceDepth(IntPtr ep, IntPtr photo, DepthFillQuality depthQuality);

            [DllImport(PXCMBase.DLLNAME)]
            internal static extern DepthMapQuality PXCMEnhancedPhoto_GetDepthQuality(IntPtr ep, IntPtr depthMap);

            [DllImport(PXCMBase.DLLNAME)]
            internal static extern IntPtr PXCMEnhancedPhoto_PhotoCrop(IntPtr ep, IntPtr photo, PXCMRectI32 rect);

            [DllImport(PXCMBase.DLLNAME)]
            internal static extern IntPtr PXCMEnhancedPhoto_DepthResize(IntPtr ep, IntPtr photo, Int32 width, DepthFillQuality depthQuality);

            [DllImport(PXCMBase.DLLNAME)]
            internal static extern IntPtr PXCMEnhancedPhoto_ColorResize(IntPtr ep, IntPtr photo, Int32 width);

            [DllImport(PXCMBase.DLLNAME)]
            internal static extern IntPtr PXCMEnhancedPhoto_PhotoRotate(IntPtr ep, IntPtr photo, Single degrees);
        };

        public partial class Segmentation : PXCMBase
        {
            [DllImport(PXCMBase.DLLNAME)]
            internal static extern IntPtr PXCMEnhancedPhoto_ObjectSegmentDeprecated(IntPtr ep, IntPtr photo, PXCMPointI32 topLeftCoord, PXCMPointI32 bottomRightCoord);

            [DllImport(PXCMBase.DLLNAME)]
            internal static extern IntPtr PXCMEnhancedPhoto_ObjectSegment(IntPtr ep, IntPtr photo, IntPtr mask);

            [DllImport(PXCMBase.DLLNAME)]
            internal static extern IntPtr PXCMEnhancedPhoto_RefineMaskDeprecated(IntPtr ep, IntPtr hints);

            [DllImport(PXCMBase.DLLNAME)]
            internal static extern IntPtr PXCMEnhancedPhoto_RefineMask(IntPtr ep, PXCMPointI32 points, Int32 length, Boolean isForeground);

            [DllImport(PXCMBase.DLLNAME)]
            internal static extern IntPtr PXCMEnhancedPhoto_Undo(IntPtr ep);

            [DllImport(PXCMBase.DLLNAME)]
            internal static extern IntPtr PXCMEnhancedPhoto_Redo(IntPtr ep);
        };

        public partial class Paster : PXCMBase
        {
            [DllImport(PXCMBase.DLLNAME)]
            internal static extern pxcmStatus PXCMEnhancedPhoto_SetPhoto(IntPtr ep, IntPtr photo);

            [DllImport(PXCMBase.DLLNAME)]
            internal static extern IntPtr PXCMEnhancedPhoto_GetPlanesMap(IntPtr ep);
            
            [DllImport(PXCMBase.DLLNAME)]
            internal static extern pxcmStatus PXCMEnhancedPhoto_SetSticker(IntPtr ep, IntPtr sticker, PXCMPointI32 coord, StickerData stickerData, PasteEffects pasteEffects);

            [DllImport(PXCMBase.DLLNAME)]
            internal static extern IntPtr PXCMEnhancedPhoto_PreviewSticker(IntPtr ep);

            [DllImport(PXCMBase.DLLNAME)]
            internal static extern IntPtr PXCMEnhancedPhoto_Paste(IntPtr ep);

            [DllImport(PXCMBase.DLLNAME)]
            internal static extern IntPtr PXCMEnhancedPhoto_PasteOnPlaneDeprecated(IntPtr ep, IntPtr photo, IntPtr imbedImInstance, PXCMPointI32 topLeftCoord, PXCMPointI32 bottomLeftCoord, PasteEffects pasteEffects);
        };

    };

#if RSSDK_IN_NAMESPACE
}
#endif
