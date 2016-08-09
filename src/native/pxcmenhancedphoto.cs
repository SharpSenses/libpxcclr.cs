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
        public partial class DepthMask : PXCMBase
        {
            [DllImport(PXCMBase.DLLNAME)]
            internal static extern pxcmStatus PXCMEnhancedPhoto_DepthMask_Init(IntPtr ep, IntPtr photo);

            [DllImport(PXCMBase.DLLNAME)]
            internal static extern IntPtr PXCMEnhancedPhoto_ComputeFromThreshold(IntPtr ep, Single depthThreshold, PXCMEnhancedPhoto.DepthMask.MaskParams maskParams);

            [DllImport(PXCMBase.DLLNAME)]
            internal static extern IntPtr PXCMEnhancedPhoto_ComputeFromCoordinate(IntPtr ep, PXCMPointI32 coord, PXCMEnhancedPhoto.DepthMask.MaskParams maskParams);
        };

        public partial class MotionEffect : PXCMBase
        {
            [DllImport(PXCMBase.DLLNAME)]
            internal static extern pxcmStatus PXCMEnhancedPhoto_MotionEffect_Init(IntPtr ep, IntPtr photo);

            [DllImport(PXCMBase.DLLNAME)]
            internal static extern IntPtr PXCMEnhancedPhoto_MotionEffect_Apply(IntPtr ep, Single[] motion, Single[] rotation, Single zoomFactor);
        };
        
        public partial class DepthRefocus : PXCMBase
        {
            [DllImport(PXCMBase.DLLNAME)]
            internal static extern pxcmStatus PXCMEnhancedPhoto_DepthRefocus_Init(IntPtr ep, IntPtr photo);

            [DllImport(PXCMBase.DLLNAME)]
            internal static extern IntPtr PXCMEnhancedPhoto_DepthRefocus_Apply(IntPtr ep, PXCMPointI32 focusPoint, Single aperture);      
        
        };

        public partial class PhotoUtils : PXCMBase
        {
            [DllImport(PXCMBase.DLLNAME)]
            internal static extern IntPtr PXCMEnhancedPhoto_EnhanceDepth(IntPtr ep, IntPtr photo, DepthFillQuality depthQuality);

            [DllImport(PXCMBase.DLLNAME)]
            internal static extern DepthMapQuality PXCMEnhancedPhoto_GetDepthQuality(IntPtr ep, IntPtr depthMap);

            [DllImport(PXCMBase.DLLNAME)]
            internal static extern IntPtr PXCMEnhancedPhoto_PhotoCrop(IntPtr ep, IntPtr photo, PXCMRectI32 rect);

            [DllImport(PXCMBase.DLLNAME)]
            internal static extern IntPtr PXCMEnhancedPhoto_CommonFOV(IntPtr ep, IntPtr photo);

            [DllImport(PXCMBase.DLLNAME)]
            internal static extern pxcmStatus PXCMEnhancedPhoto_PreviewCommonFOV(IntPtr ep, IntPtr photo, ref PXCMRectI32 rect);

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
            internal static extern IntPtr PXCMEnhancedPhoto_RefineMask(IntPtr ep, PXCMPointI32[] points, Int32 length, Boolean isForeground);

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

        public partial class Measurement : PXCMBase
        {
            [DllImport(PXCMBase.DLLNAME)]
            internal static extern pxcmStatus PXCMEnhancedPhoto_MeasureDistance(IntPtr ep, IntPtr photo, PXCMPointI32 startPoint, PXCMPointI32 endPoint, [Out] MeasureData outData);

            [DllImport(PXCMBase.DLLNAME)]
            internal static extern pxcmStatus PXCMEnhancedPhoto_MeasureUADistance(IntPtr ep, IntPtr photo, PXCMPointI32 startPoint, PXCMPointI32 endPoint, [Out] MeasureData outData);
        
            [DllImport(PXCMBase.DLLNAME)]
            internal static extern Int32 PXCMEnhancedPhoto_QueryUADataSize(IntPtr ep);

            [DllImport(PXCMBase.DLLNAME)]
            internal static extern pxcmStatus PXCMEnhancedPhoto_QueryUAData(IntPtr ep, IntPtr measureDataStruct);

            internal static pxcmStatus PXCMEnhancedPhoto_QueryUADataINT(IntPtr instance, out MeasureData[] outData)
            {
                int num = PXCMEnhancedPhoto_QueryUADataSize(instance);
                IntPtr nativeStruct = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(MeasureData)) * num);
                pxcmStatus sts = PXCMEnhancedPhoto_QueryUAData(instance, nativeStruct);
                if (sts == pxcmStatus.PXCM_STATUS_NO_ERROR)
                {
                    outData = new MeasureData[num];
                    for (int i = 0; i < num; i++)
                    {
                        outData[i] = new MeasureData();
                        Marshal.PtrToStructure(nativeStruct, outData[i]);
                    }
                }
                else 
                {
                    outData = null;
                }
                Marshal.FreeHGlobal(nativeStruct);
                return sts;
            }
        };

    };

#if RSSDK_IN_NAMESPACE
}
#endif
