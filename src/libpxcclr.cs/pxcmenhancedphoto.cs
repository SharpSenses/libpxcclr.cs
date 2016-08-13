/*******************************************************************************

  INTEL CORPORATION PROPRIETARY INFORMATION
  This software is supplied under the terms of a license agreement or nondisclosure
  agreement with Intel Corporation and may not be copied or disclosed except in
  accordance with the terms of that agreement
  Copyright(c) 2015 Intel Corporation. All Rights Reserved.

 *******************************************************************************/
using System;
using System.Runtime.InteropServices;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

public partial class PXCMEnhancedPhoto : PXCMBase
{
    new public const Int32 CUID = 0x4e495045;

    public partial class DepthMask : PXCMBase
    {
        new public const Int32 CUID = 0x4d445045;

        /* Constructors & Misc */
        internal DepthMask(IntPtr instance, Boolean delete)
            : base(instance, delete)
        {
        }

        public static DepthMask CreateInstance(PXCMSession session)
        {
            DepthMask tmp;
            session.CreateImpl<DepthMask>(out tmp);
            return tmp;
        }


        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public class MaskParams
        {
            public Single frontObjectDepth;
            public Single backOjectDepth;
            public Single nearFallOffDepth;
            public Single farFallOffDepth;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            internal Single[] reserved;

            public MaskParams()
            {
                reserved = new Single[4];
                frontObjectDepth = -1;
                backOjectDepth = -1;
                nearFallOffDepth = -1;
                farFallOffDepth = -1;
            }
        };

        /// <summary>
        /// Init: the function initializes the Depth Mask function with the photo that needs processing. 
        /// photo: 2D+depth input image.
        /// returns PXCMStatus.
        /// </summary>
        public pxcmStatus Init(PXCMPhoto photo)
        {
            return PXCMEnhancedPhoto_DepthMask_Init(instance, photo.instance);
        }

        /// <summary> 
        /// ComputeFromCoordinate: convenience function that creates a mask directly from a depth coordinate.
        /// coord: input (x,y) coordinates on the depth map.  
        /// Returns a mask in the form of PXCMImage for blending with the current photo frame.
        /// Note: this function simply calls ComputeMaskFromThreshold underneath.
        /// </summary>
        public PXCMImage ComputeFromCoordinate(PXCMPointI32 coord, MaskParams maskParams)
        {
            IntPtr image = PXCMEnhancedPhoto_ComputeFromCoordinate(instance, coord, maskParams);
            return image != IntPtr.Zero ? new PXCMImage(image, true) : null;
        }

        public PXCMImage ComputeFromCoordinate(PXCMPointI32 coord)
        {
            MaskParams maskParams = new MaskParams();
            return ComputeFromCoordinate(coord, maskParams);
        }
        /// <summary> 
        /// ComputeFromThreshold: calculates a mask from the threshold computed 
        /// depthThreshold: depth threshold. 
        /// frontObjectDepth: foreground depth
        /// backOjectDepth: background depth
        /// Returns a mask in the form of PXCMImage for blending with the current photo frame.
        /// 
        /// Notes:
        /// For every pixel, if the mask is between the range of [POIdepth - frontObjectDepth, POIdepth + backObjectDepth], mask[p] -1.
        /// For every pixel p with depth in the range [POI - frontObjectDepth - nearFalloffDepth, POI - frontObjectDepth], mask[p] equals the "smoothstep" function value.
        /// For every pixel p with depth in the range [POI  + backObjectDepth , POI + backOjectDepth + farFallOffDepth], mask[p] equals the "smoothstep" function value.
        /// For every pixel p with other depth value, mask[p] = 1.
        /// </summary>
        public PXCMImage ComputeFromThreshold(Single depthThreshold, MaskParams maskParams)
        {
            IntPtr image = PXCMEnhancedPhoto_ComputeFromThreshold(instance, depthThreshold, maskParams);
            return image != IntPtr.Zero ? new PXCMImage(image, true) : null;

        }

        public PXCMImage ComputeFromThreshold(Single depthThreshold)
        {
            MaskParams maskParams = new MaskParams();
            return ComputeFromThreshold(depthThreshold, maskParams);
        }


    };

    public partial class MotionEffect : PXCMBase
    {

        new public const Int32 CUID = 0x454d5045;

        /* Constructors & Misc */
        internal MotionEffect(IntPtr instance, Boolean delete)
            : base(instance, delete)
        {
        }

        public static MotionEffect CreateInstance(PXCMSession session)
        {
            MotionEffect tmp;
            session.CreateImpl<MotionEffect>(out tmp);
            return tmp;
        }

        /// <summary>
        /// Init: the function initializes the 6DoF parallax function with the photo that needs processing. 
        /// photo: 2D+depth input image.
        /// returns PXCMStatus.
        /// </summary>
        public pxcmStatus Init(PXCMPhoto photo)
        {
            return PXCMEnhancedPhoto_MotionEffect_Init(instance, photo.instance);
        }

        /// <summary>
        /// Apply: The function applies a 6DoF parallax effect which is the difference in the apparent position of an object
        /// when it is viewed from two different positions or viewpoints. 
        /// 
        /// motion[3]: is the right, up, and forward motion when (+ve), and Left, down and backward motion when (-ve)
        /// motion[0]: + right   / - left 
        /// motion[1]: + up      / - down
        /// motion[2]: + forward / - backward
        /// rotation[3]: is the Pitch, Yaw, Roll rotations in degrees in the Range: 0-360. 
        /// rotation[0]: pitch 
        /// rotation[1]: yaw
        /// rotation[2]: roll
        /// zoomFactor: + zoom in / - zoom out
        /// PXCMImage: the returned parallaxed image.
        /// </summary>
        public PXCMImage Apply(Single[] motion, Single[] rotation, Single zoomFactor)
        {
            IntPtr image = PXCMEnhancedPhoto_MotionEffect_Apply(instance, motion, rotation, zoomFactor);
            return image != IntPtr.Zero ? new PXCMImage(image, true) : null;
        }

    };

    public partial class DepthRefocus : PXCMBase
    {
        new public const Int32 CUID = 0x52445045;

        /* Constructors & Misc */
        internal DepthRefocus(IntPtr instance, Boolean delete)
            : base(instance, delete)
        {
        }

        public static DepthRefocus CreateInstance(PXCMSession session)
        {
            DepthRefocus tmp;
            session.CreateImpl<DepthRefocus>(out tmp);
            return tmp;
        }

        /// <summary>
        /// Init: the function initializes the Depth Refocus function with the photo that needs processing. 
        /// sample: 2D+depth input image.
        /// returns PXCMStatus.
        /// </summary>
        public pxcmStatus Init(PXCMPhoto photo)
        {
            return PXCMEnhancedPhoto_DepthRefocus_Init(instance, photo.instance);
        }


        /// <summary> 
        /// Apply: Refocus the image at input focusPoint by using depth data refocus
        /// focusPoint: is the selected point foir refocussing.
        /// aperture: Range of the blur area [1-100] 
        /// Note: The application must release the returned refocussed image
        /// </summary>
        public PXCMPhoto Apply(PXCMPointI32 focusPoint, Single aperture)
        {
            IntPtr image = PXCMEnhancedPhoto_DepthRefocus_Apply(instance, focusPoint, aperture);
            return image != IntPtr.Zero ? new PXCMPhoto(image, true) : null;
        }

        public PXCMPhoto Apply(PXCMPointI32 focusPoint)
        {
            return Apply(focusPoint, 50.0F);
        }

    };

    public partial class PhotoUtils : PXCMBase
    {
        new public const Int32 CUID = 0x54555045;

        /* Constructors & Misc */
        internal PhotoUtils(IntPtr instance, Boolean delete)
            : base(instance, delete)
        {
        }

        public static PhotoUtils CreateInstance(PXCMSession session)
        {
            PhotoUtils tmp;
            session.CreateImpl<PhotoUtils>(out tmp);
            return tmp;
        }

        /// <summary> 
        /// Input param for Depth fill Quality: 
        /// High: better quality, slow execution mostly used for post processing (image)
        /// Low : lower quality, fast execution mostly used for realtime processing (live video sequence)
        /// </summary>
        public enum DepthFillQuality
        {
            HIGH = 0,
            LOW = 1,
        };

        /// <summary> 
        /// EnhanceDepth: enhance the depth image quality by filling holes and denoising
        /// outputs the enhanced depth image
        /// photo: input color, depth photo, and calibration data
        /// depthQuality: Depth fill Quality: HIGH or LOW for post or realtime processing respectively
        /// Note: The application must release the returned enhanced depth photo
        /// </summary>
        public PXCMPhoto EnhanceDepth(PXCMPhoto photo, DepthFillQuality depthQuality)
        {
            IntPtr image = PXCMEnhancedPhoto_EnhanceDepth(instance, photo.instance, depthQuality);
            return image != IntPtr.Zero ? new PXCMPhoto(image, true) : null;
        }

        /// <summary> 
        /// PreviewEnhanceDepth: enhance the depth image quality by filling holes and denoising
        /// outputs the enhanced depth image
        /// sample: The PXCMCapture::Sample instance from the SenseManager QuerySample().
        /// depthQuality: Depth fill Quality: HIGH or LOW for post or realtime processing respectively
        /// Note: The application must release the returned enhanced depth image
        /// </summary>
        public PXCMImage PreviewEnhanceDepth(PXCMCapture.Sample sample, DepthFillQuality depthQuality)
        {
            IntPtr image = PXCMEnhancedPhoto_PreviewEnhanceDepth(instance, sample, depthQuality);
            return image != IntPtr.Zero ? new PXCMImage(image, true) : null;
        }

        /// <summary> 
        /// DepthMapQuality: Output param for Depth Map Quality: 
        /// BAD, FAIR and GOOD
        /// </summary>
        public enum DepthMapQuality
        {
            BAD = 0,
            FAIR = 1,
            GOOD = 2,
        };

        /// <summary>
        /// DepthQuality: retruns the quality of the the depth map
        /// photo: input color, raw depth map, and calibration data
        /// depthQuality: BAD, FAIR, GOOD
        /// </summary>
        public DepthMapQuality GetDepthQuality(PXCMImage depthMap)
        {
            return PXCMEnhancedPhoto_GetDepthQuality(instance, (depthMap == null) ? IntPtr.Zero : depthMap.instance);
        }

        /// <summary> 
        /// Crop: The primary image, the camera[0] RGB and depth images are cropped 
        /// and the intrinsic / extrinsic info is updated.
        /// photo: input image color+depth
        /// rect : top left corner (x,y) plus width and height of the window to keep 
        /// and crop all the rest
        /// Returns a photo that has all its images cropped and metadata fixed accordingly.
        /// Note: Returns null if function fails
        /// </summary>
        public PXCMPhoto PhotoCrop(PXCMPhoto photo, PXCMRectI32 rect)
        {
            IntPtr image = PXCMEnhancedPhoto_PhotoCrop(instance, photo.instance, rect);
            return image != IntPtr.Zero ? new PXCMPhoto(image, true) : null;

        }

        /// <summary> 
        /// UpScaleDepth: Change the size of the enhanced depth map. 
        /// This function preserves aspect ratio, so only new width is required.
        /// photo: input image color+depth
        /// width: the new width.
        /// enhancementType: if the inPhoto has no enhanced depth, then do this type of depth enhancement before resizing.
        /// Returns a Depth map that has the same aspect ratio as the color image resolution.
        /// Note: Returns null if the aspect ratio btw color and depth is not preserved
        /// </summary>
        public PXCMPhoto DepthResize(PXCMPhoto photo, Int32 size, DepthFillQuality enhancementType)
        {
            IntPtr image = PXCMEnhancedPhoto_DepthResize(instance, photo.instance, size, enhancementType);
            return image != IntPtr.Zero ? new PXCMPhoto(image, true) : null;
        }

        public PXCMPhoto DepthResize(PXCMPhoto photo, Int32 size)
        {
            return DepthResize(photo, size, DepthFillQuality.HIGH);

        }

        /// <summary> 
        /// PhotoResize: Change the size of the reference (primary) image. 
        /// This function preserves aspect ratio, so only new width is required.
        /// Only the primary image is resized.
        /// photo - input photo.
        /// width - the new width.
        /// Returns a photo with the reference (primary) color image resized while maintaining the aspect ratio.
        /// Note: Returns null when the function fails
        /// </summary>
        public PXCMPhoto ColorResize(PXCMPhoto photo, Int32 width)
        {
            IntPtr image = PXCMEnhancedPhoto_ColorResize(instance, photo.instance, width);
            return image == IntPtr.Zero ? null : new PXCMPhoto(image, true);
        }

        /// <summary>
        /// PhotoRotate: rotates a Photo (color, depth and metadata).
        /// this function rotates the primary image, the RGB and depth images in camera 0, and updates 
        /// the corresponding intrinsic/extrinsic info.
        /// photo: input photo.
        /// degrees: the angle of rotation around the center of the color image in degrees.
        /// Returns a rotated photo.
        /// Note: Returns null when the function fails
        /// THIS FUNCTION ONLY DOES FIXED ROTATIONS OF 90, 180 & 270 DEGREES
        /// </summary>
        public PXCMPhoto PhotoRotate(PXCMPhoto photo, Single degrees)
        {
            IntPtr image = PXCMEnhancedPhoto_PhotoRotate(instance, photo.instance, degrees);
            return image == IntPtr.Zero ? null : new PXCMPhoto(image, true);
        }

        /// <summary> 
        /// CommonFOV: Matches the Feild Of View (FOV) of color and depth in the photo. Useful for still images.
        /// photo: input image color+depth
        /// Returns a photo with primary,unedited color images, and depthmaps cropped to the 
        /// common FOV and the camera meatadata recalculated accordingly.
        /// Note: Returns a nullptr if function fails
        /// </summary>
        public PXCMPhoto CommonFOV(PXCMPhoto photo)
        {
            IntPtr image = PXCMEnhancedPhoto_CommonFOV(instance, photo.instance);
            return image == IntPtr.Zero ? null : new PXCMPhoto(image, true);
        }

        /// <summary>
        /// PreviewCommonFOV: Matches the Field of View (FOV) of color and depth in depth photo. Useful for live stream.
        /// Use the returned roi to crop the photo
        /// photo: input image color+depth
        /// rect: Output. Returns roi in color image that matches to FOV of depth image that is suitable for all photos in the live stream.
        /// @return pxcmStatus : PXCM_STATUS_NO_ERRROR for successfu operation; PXCM_STATUS_DATA_UNAVAILABLE otherwise
        /// </summary>

        public pxcmStatus PreviewCommonFOV(PXCMCapture.Sample sample, out PXCMRectI32 rect)
        {
            rect = new PXCMRectI32();
            return PXCMEnhancedPhoto_PreviewCommonFOV(instance, sample, ref rect);
        }

        /// <summary>
        /// PreviewCommonFOV [Deprecated]: Matches the Field of View (FOV) of color and depth in depth photo. Useful for live stream.
        /// Use the returned roi to crop the photo
        /// photo: input image color+depth
        /// rect: Output. Returns roi in color image that matches to FOV of depth image that is suitable for all photos in the live stream.
        /// @return pxcStatus : PXC_STATUS_NO_ERRROR for successfu operation; PXC_STATUS_DATA_UNAVAILABLE otherwise
        /// </summary>

        public pxcmStatus PreviewCommonFOV(PXCMPhoto photo, out PXCMRectI32 rect)
        {
            rect = new PXCMRectI32();
            return PXCMEnhancedPhoto_PreviewCommonFOVDeprecated(instance, photo.instance, ref rect);
        }
    };

    public partial class Segmentation : PXCMBase
    {
        new public const Int32 CUID = 0x47535045;

        /* Constructors & Misc */
        internal Segmentation(IntPtr instance, Boolean delete)
            : base(instance, delete)
        {
        }

        public static Segmentation CreateInstance(PXCMSession session)
        {
            Segmentation tmp;
            session.CreateImpl<Segmentation>(out tmp);
            return tmp;
        }

        /// <summary>
        /// ObjectSegment: generates an initial mask for any object selected by the bounding box. 
        /// The mask can then be refined by hints supplied by the user in RefineMask() function. 
        /// photo: input color and depth photo.
        /// topLeftCoord    : top left corner of the object to segment.  
        /// bottomRightCoord: Bottom right corner of the object to segment.
        /// Returns a mask in the form of PXCMImage with detected pixels set to 255 and undetected pixels set to 0.
        /// </summary>
        public PXCMImage ObjectSegment(PXCMPhoto photo, PXCMPointI32 topLeftCoord, PXCMPointI32 bottomRightCoord)
        {
            IntPtr image = PXCMEnhancedPhoto_ObjectSegmentDeprecated(instance, photo.instance, topLeftCoord, bottomRightCoord);
            return image == IntPtr.Zero ? null : new PXCMImage(image, true);
        }

        /// <summary>
        /// ObjectSegment: generates an initial mask for any object selected by the bounding box. 
        /// The mask can then be refined by hints supplied by the user in RefineMask() function. 
        /// photo: input color and depth photo.
        /// Returns a mask in the form of PXCMImage with detected pixels set to 255 and undetected pixels set to 0.
        /// </summary>
        public PXCMImage ObjectSegment(PXCMPhoto photo, PXCMImage mask)
        {
            IntPtr image = PXCMEnhancedPhoto_ObjectSegment(instance, photo.instance, (mask == null) ? IntPtr.Zero : mask.instance);
            return image == IntPtr.Zero ? null : new PXCMImage(image, true);
        }

        /// <summary> 
        /// RefineMask: refines the mask generated by the ObjectSegment() function by using hints.
        /// hints: input mask with hints. hint values.
        /// 0 = no hint
        /// 1 = foreground
        /// 2 = background
        /// Returns a mask in the form of PXCMImage with detected pixels set to 255 and undetected pixels set to 0.
        /// </summary>
        public PXCMImage RefineMask(PXCMImage hints)
        {
            IntPtr image = PXCMEnhancedPhoto_RefineMaskDeprecated(instance, (hints == null) ? IntPtr.Zero : hints.instance);
            return image == IntPtr.Zero ? null : new PXCMImage(image, true);
        }

        /// <summary> 
        /// RefineMask: refines the mask generated by the ObjectSegment() function by using hints.
        /// points: input arrays with hints' coordinates.
        /// length: length of the array
        /// isForeground: bool set to true if input hint locations are foreground and false if background
        /// Returns a mask in the form of PXCImage with detected pixels set to 255 and undetected pixels set to 0.
        /// </summary>
        public PXCMImage RefineMask(PXCMPointI32[] points, Int32 length, Boolean isForeground)
        {
            IntPtr image = PXCMEnhancedPhoto_RefineMask(instance, points, length, isForeground);
            return image == IntPtr.Zero ? null : new PXCMImage(image, true);
        }

        /// <summary> 
        /// Undo: undo last hints.
        /// Returns a mask in the form of PXCImage with detected pixels set to 255 and undetected pixels set to 0.
        /// </summary>
        public PXCMImage Undo()
        {
            IntPtr image = PXCMEnhancedPhoto_Undo(instance);
            return image == IntPtr.Zero ? null : new PXCMImage(image, true);
        }

        /// <summary> 
        /// Redo: Redo the previously undone hint.
        /// Returns a mask in the form of PXCImage with detected pixels set to 255 and undetected pixels set to 0.
        /// </summary>
        public PXCMImage Redo()
        {
            IntPtr image = PXCMEnhancedPhoto_Redo(instance);
            return image == IntPtr.Zero ? null : new PXCMImage(image, true);
        }

    };

    public partial class Paster : PXCMBase
    {                                   //Text to Hex (EPPP)
        new public const Int32 CUID = 0x50505045;

        /* Constructors & Misc */
        internal Paster(IntPtr instance, Boolean delete)
            : base(instance, delete)
        {
        }

        public static Paster CreateInstance(PXCMSession session)
        {
            Paster tmp;
            session.CreateImpl<Paster>(out tmp);
            return tmp;
        }

        /// <summary> 
        /// PasteEffects:  
        ///  matchIllumination: Matches sticker illumination to the global RGB scene. Default is true.	
        ///	transparency: Sets Transparency level of the sticker. 0.0f = Opaque (Default); 1.0f = Transparent
        ///	embossHighFreqPass: High Frequency Pass during emboss, default 0.0f no emboss, 1.0f max	
        ///	shadingCorrection: Matches sticker illumination to local RGB scene, takes shadows in account. Default is false.
        ///	colorCorrection: Flag to add color correction; Default is false.
        ///	embossingAmplifier: Embossing Intensity Multiplier. default: 1.0f. should be positive
        ///	skinDetection: Flag to detect skin under pasted sticker; default is false		
        /// </summary>
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public class PasteEffects
        {
            public bool matchIllumination; // Flag to match global Illumination, default value is true	
            public Single transparency;    // (default) 0.0f = opaque, 1.0f = transparent sticker
            public Single embossHighFreqPass; // Level of details to emboss, (default) 0.0f = no emboss, 1.0f =max
            public bool shadingCorrection; // Flag to match local illumination. Default is false
            public bool colorCorrection;     // Flag to add color correction; Default is false.
            public Single embossingAmplifier; // Embossing Intesity Multiplier. Default is 1.0f. Should be positive
            public bool skinDetection; // Flag to detect skin under pasted sticker; default is false
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            internal Single[] reserved;

            public PasteEffects()
            {
                matchIllumination = true;
                transparency = 0.0f;
                embossHighFreqPass = 0.0f;
                shadingCorrection = false;
                colorCorrection = false;
                embossingAmplifier = 1.0f;
                skinDetection = false;
                reserved = new Single[6];
            }
        };

        /// <summary> 
        /// PasteType: Indicates whether sticker is pasted on detected planes or on any surface 
        /// PLANE and SURFACE
        /// </summary>
        public enum PasteType
        {
            PLANE = 0,
            SURFACE,
        };


        /// <summary> 
        /// SetPhoto: sets the photo that needs to be processed.
        /// photo: photo to be processed [color + depth] 
        /// pasteMode: Indicates whether pasteOnPlane or pasteOnSurface
        /// Returns PXC_STATUS_NO_ERROR if success. PXC_STATUS_PROCESS_FAILED if process failed
        /// </summary>
        public pxcmStatus SetPhoto(PXCMPhoto photo, PXCMEnhancedPhoto.Paster.PasteType pasteMode)
        {
            return PXCMEnhancedPhoto_SetPhoto(instance, photo.instance, pasteMode);
        }

        public pxcmStatus SetPhoto(PXCMPhoto photo)
        {
            return SetPhoto(photo, PXCMEnhancedPhoto.Paster.PasteType.PLANE);
        }

        /// <summary>
        /// GetPlanesMap: return plane indices map for current SetPhoto
        /// Returns a PXCImage of the plane indices in a form of a mask.
        /// </summary>
        public PXCMImage GetPlanesMap()
        {
            IntPtr image = PXCMEnhancedPhoto_GetPlanesMap(instance);
            return image == IntPtr.Zero ? null : new PXCMImage(image, true);

        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        /// <summary> 
        /// StickerData:  
        /// height: sticker height in mm, default -1 auto-scale		
        /// rotation: in-plane rotation in degree, default 0	
        /// isCenter: No Longer Supported. Anchor point. True means coordinate is center. 
        /// </summary>
        public class StickerData
        {
            public Single height;
            public Single rotation;
            public bool isCenter;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public Single[] reserved;

            public StickerData()
            {
                height = 200.0f;
                rotation = 0.0f;
                isCenter = true;
                reserved = new Single[6];
            }
        };

        /// <summary> 
        /// AddSticker: adds a sticker that will be pasted with all configurations needed and paste effects.
        /// sticker: the image to paste onto the photo (foreground image)
        /// coord : insertion coordinates
        /// stickerData: the sticker size, paste location and anchor point.
        /// pasteEffects: the pasting effects.
        /// Returns a stickerID number that can be used as input to the UpdateSticker(), RemoveSticker(), and PreviewSticker()
        /// functions. A negative return value indicates failure.
        /// </summary>
        public Int32 AddSticker(PXCMImage sticker, PXCMPointI32 coord, StickerData stickerData, PasteEffects pasteEffects)
        {
            return PXCMEnhancedPhoto_AddSticker(instance, sticker.instance, coord, stickerData, pasteEffects);
        }

        public Int32 AddSticker(PXCMImage sticker, PXCMPointI32 coord, StickerData stickerData)
        {
            PasteEffects pasteEffects = new PasteEffects();
            return PXCMEnhancedPhoto_AddSticker(instance, sticker.instance, coord, stickerData, pasteEffects);
        }

        public Int32 AddSticker(PXCMImage sticker, PXCMPointI32 coord, PasteEffects pasteEffects)
        {
            StickerData stickerData = new StickerData();
            return PXCMEnhancedPhoto_AddSticker(instance, sticker.instance, coord, stickerData, pasteEffects);
        }

        public Int32 AddSticker(PXCMImage sticker, PXCMPointI32 coord)
        {
            PasteEffects pasteEffects = new PasteEffects();
            StickerData stickerData = new StickerData();
            return PXCMEnhancedPhoto_AddSticker(instance, sticker.instance, coord, stickerData, pasteEffects);
        }

        /// <summary>
        /// RemoveSticker: Removes the sticker represented by stickerID
        /// </summary>
        /// <param name="stickerID">The stickerID for the sticker to remove.</param>
        /// <returns>PXC_STATUS_NO_ERROR for success. PXC_STATUS_ITEM_UNAVAILABLE if the stickerID was not valid</returns>
        public pxcmStatus RemoveSticker(Int32 stickerID)
        {
            return PXCMEnhancedPhoto_RemoveSticker(instance, stickerID);
        }

        /// <summary>
        /// RemoveAllStickers: Removes all stickers from the scene. If there are no stickers
        /// in the scene, this function has no effect.
        /// </summary>
        public void RemoveAllStickers()
        {
            PXCMEnhancedPhoto_RemoveAllStickers(instance);
        }

        /// <summary> 
        /// UpdateSticker: Update the configuration and paste effects for the given sticker. You can pass null
        /// for any argument that you do not wish to update.
        /// sticker: the image to paste onto the photo (foreground image)
        /// coord : insertion coordinates
        /// stickerData: the sticker size, paste location and anchor point.
        /// pasteEffects: the pasting effects.
        /// Returns PXC_STATUS_NO_ERROR for success. Returns PXC_STATUS_ITEM_UNAVAILABLE if the stickerID was not valid
        /// </summary>
        public pxcmStatus UpdateSticker(Int32 stickerID, PXCMPointI32 coord, StickerData stickerData, PasteEffects pasteEffects)
        {
            return PXCMEnhancedPhoto_UpdateSticker(instance, stickerID, coord, stickerData, pasteEffects);
        }

        /// <summary> 
        /// SetSticker: sets the sticker that will be pasted with all configurations needed and paste effects.
        /// sticker: the image to paste onto the photo (foreground image)
        /// coord : insertion coordinates
        /// stickerData: the sticker size, paste location and anchor point.
        /// pasteEffects: the pasting effects.
        /// Returns PXC_STATUS_NO_ERROR if success. PXC_STATUS_PROCESS_FAILED if process failed
        /// </summary>
        [Obsolete("SetSticker is deprececated; Use AddSticker instead")]
        public pxcmStatus SetSticker(PXCMImage sticker, PXCMPointI32 coord, StickerData stickerData, PasteEffects pasteEffects)
        {
            return PXCMEnhancedPhoto_SetSticker(instance, sticker.instance, coord, stickerData, pasteEffects);
        }

        [Obsolete("SetSticker is deprececated; Use AddSticker instead")]
        public pxcmStatus SetSticker(PXCMImage sticker, PXCMPointI32 coord, StickerData stickerData)
        {
            PasteEffects pasteEffects = new PasteEffects();
            return PXCMEnhancedPhoto_SetSticker(instance, sticker.instance, coord, stickerData, pasteEffects);
        }

        [Obsolete("SetSticker is deprececated; Use AddSticker instead")]
        public pxcmStatus SetSticker(PXCMImage sticker, PXCMPointI32 coord, PasteEffects pasteEffects)
        {
            StickerData stickerData = new StickerData();
            return PXCMEnhancedPhoto_SetSticker(instance, sticker.instance, coord, stickerData, pasteEffects);
        }

        [Obsolete("SetSticker is deprececated; Use AddSticker instead")]
        public pxcmStatus SetSticker(PXCMImage sticker, PXCMPointI32 coord)
        {
            PasteEffects pasteEffects = new PasteEffects();
            StickerData stickerData = new StickerData();
            return PXCMEnhancedPhoto_SetSticker(instance, sticker.instance, coord, stickerData, pasteEffects);
        }

        /// <summary> 
        /// PreviewSticker: returns a sticker mask showing the location of the pasted sticker.
        /// Returns PXCImage with returns values of 0, 1, 2 :
        /// 2 U 1 - region where the sticker could be pasted if there were no constraints
        /// 1 - apropriate region to paste sticker considering constraints: e.g. plane
        /// 0 - all other pixels
        /// </summary>
        public PXCMImage PreviewSticker(Int32 stickerID = 0)
        {
            IntPtr image = PXCMEnhancedPhoto_PreviewSticker(instance, stickerID);
            return image == IntPtr.Zero ? null : new PXCMImage(image, true);

        }
        /// <summary>
        /// GetStickerROI: Gives a bounding box showing the location of the sticker
        /// Returns PXCM_STATUS_NO_ERROR if operation succeeds
        /// </summary>
        public pxcmStatus GetStickerROI(out PXCMRectI32 rect, Int32 stickerID = 0)
        {
            rect = new PXCMRectI32();
            return PXCMEnhancedPhoto_GetStickerROI(instance, ref rect, stickerID);

        }

        /// <summary> 
        /// Paste: pastes a smaller 2D image (sticker) onto a bigger color + depth image (background).
        /// The smaller foreground image, is rendered according to a
        /// user-specified position and an auto-detected plane orientation onto the background image.
        /// The auto-oriented foreground image and the color data of the background image are composited together
        /// according to the alpha channal of the foreground image.
        /// Returns the embeded foreground with background image.
        /// </summary>
        public PXCMPhoto Paste()
        {
            IntPtr outPhoto = PXCMEnhancedPhoto_Paste(instance);
            return outPhoto != IntPtr.Zero ? new PXCMPhoto(outPhoto, true) : null;

        }

        /// <summary> 
        /// PasteOnPlane: This function is provided for texturing a smaller 2D image (foreground)
        /// onto a bigger color + depth image (background). The smaller foreground image, is rendered according to a
        /// user-specified position and an auto-detected plane orientation onto the background image.
        /// The auto-oriented foreground image and the color data of the background image are composited together
        /// according to the alpha channal of the foreground image.
        /// 
        /// imbedIm: the image to imbed in the photo (foreground image)
        /// topLeftCoord, bottomLeftCoord: are the top left corner and the bottom left corner of where the user wants to embed the image.
        /// Returns the imbeded foreground with background image.
        /// </summary>
        public PXCMPhoto PasteOnPlane(PXCMPhoto photo, PXCMImage imbedIm, PXCMPointI32 topLeftCoord, PXCMPointI32 bottomLeftCoord, PasteEffects pasteEffects)
        {
            IntPtr outPhoto = PXCMEnhancedPhoto_PasteOnPlaneDeprecated(instance, photo.instance, (imbedIm == null) ? IntPtr.Zero : imbedIm.instance, topLeftCoord, bottomLeftCoord, pasteEffects);
            return outPhoto != IntPtr.Zero ? new PXCMPhoto(outPhoto, true) : null;

        }

        public PXCMPhoto PasteOnPlane(PXCMPhoto photo, PXCMImage imbedIm, PXCMPointI32 topLeftCoord, PXCMPointI32 bottomLeftCoord)
        {
            PasteEffects pasteEffects = new PasteEffects();
            return PasteOnPlane(photo, imbedIm, topLeftCoord, bottomLeftCoord, pasteEffects);
        }

    };

    public partial class Measurement : PXCMBase
    {
        new public const Int32 CUID = 0x444d5045;

        /* Constructors & Misc */
        internal Measurement(IntPtr instance, Boolean delete)
            : base(instance, delete)
        {
        }

        public static Measurement CreateInstance(PXCMSession session)
        {
            Measurement tmp;
            session.CreateImpl<Measurement>(out tmp);
            return tmp;
        }

        /// <summary>	
        /// DistanceType: indicator whether the Two points measured lie on a the same planar surface 
        /// </summary>
        public enum DistanceType
        {
            UNKNOWN = 0,
            COPLANAR,
            NONCOPLANAR,
        };

        /// <summary>
        /// This represents a point in 3D world space in millimeter (mm) units. 
        /// </summary>
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public class WorldPoint
        {
            public PXCMPoint3DF32 coord;/* Coordinates in mm. */
            public Single confidence; /* Confidence for this point. The confidence ranges from 0.0 (no confidence) to 1.0 (high confidence). This should be set to NaN if confidence is not available. */
            public Single precision; /* Precision for this point. Precision is given in mm and represents the percision of the depth value at this point in 3D space. This should be set to NaN if precision is not available. */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            internal Single[] reserved;

            public WorldPoint()
            {
                reserved = new Single[6];
            }
        };

        /// <summary>
        /// This represents the distance between two world points in millimeters (mm). 
        /// </summary>
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public class MeasureData
        {
            public Single distance;/* The distance measured in mm. */
            public Single confidence; /* Confidence for this point. The confidence ranges from 0.0 (no confidence) to 1.0 (high confidence). This should be set to NaN if confidence is not available. */
            public Single precision; /* Precision for this point. Precision is given in mm and represents the percision of the depth value at this point in 3D space. This should be set to NaN if precision is not available. */
            public WorldPoint startPoint; /* The first of the two points from which the distance is measured. */
            public WorldPoint endPoint; /* The second of the two points from which the distance is measured. */
            public DistanceType distanceType;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            internal Single[] reserved;

            public MeasureData()
            {
                distance = 0;
                confidence = 0;
                precision = 0;
                distanceType = DistanceType.UNKNOWN;
                reserved = new Single[6];
            }
        };

        /// <summary> 
        /// MeasureDistance: measure the distance between 2 points in mm
        /// photo: is the photo instance
        /// startPoint, endPoint: are the start pont and end point of the distance that need to be measured.
        /// Note: Depth data must be available and accurate at the start and end point selected. 
        /// </summary>
        public pxcmStatus MeasureDistance(PXCMPhoto photo, PXCMPointI32 startPoint, PXCMPointI32 endPoint, out MeasureData outData)
        {
            outData = new MeasureData();
            return PXCMEnhancedPhoto_MeasureDistance(instance, photo.instance, startPoint, endPoint, outData);
        }

        /// <summary> 
        /// MeasureUADistance: (Experimental) measure the distance between 2 points in mm by using a experimental algortihm for a User Assisted (UA) measure.
        /// photo: is the photo instance
        /// startPoint, endPoint: are the user selected start point and end point of the distance that needs to be measured.
        /// returns the MeasureData that has the highest confidence value.  
        /// Note: depth data must be available and accurate at the start and end point selected. 
        /// </summary>
        public pxcmStatus MeasureUADistance(PXCMPhoto photo, PXCMPointI32 startPoint, PXCMPointI32 endPoint, out MeasureData outData)
        {
            outData = new MeasureData();
            return PXCMEnhancedPhoto_MeasureUADistance(instance, photo.instance, startPoint, endPoint, outData);
        }

        /// <summary> 
        /// QueryUADataSize: (Experimental) returns the size of the MeasureData possibilites. The number of possibilities varries according 
        /// to the selected points, if they lie on a common plane or independent planes.
        /// </summary>
        public Int32 QueryUADataSize()
        {
            return PXCMEnhancedPhoto_QueryUADataSize(instance);
        }

        /// <summary> 
        /// QueryUAData: (Experimental) Returns an array of the MeasureData possibilites. The size of the array is equal to the value returned
        /// by the QueryUADataSize().
        /// </summary>
        public pxcmStatus QueryUAData(out MeasureData[] outDataArr)
        {
            return PXCMEnhancedPhoto_QueryUADataINT(instance, out outDataArr);
        }
    };

    /* Constructors & Misc */
    internal PXCMEnhancedPhoto(IntPtr instance, Boolean delete)
        : base(instance, delete)
    {
    }
};

#if RSSDK_IN_NAMESPACE
}
#endif
