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

        /**
            This represents a point in 3D world space in millimeter (mm) units. 
        */
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

        /**
            This represents the distance between two world points in millimeters (mm). 
        */
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public class MeasureData
        {
            public Single distance;/* The distance measured in mm. */
            public Single confidence; /* Confidence for this point. The confidence ranges from 0.0 (no confidence) to 1.0 (high confidence). This should be set to NaN if confidence is not available. */
            public Single precision; /* Precision for this point. Precision is given in mm and represents the percision of the depth value at this point in 3D space. This should be set to NaN if precision is not available. */
            public WorldPoint startPoint; /* The first of the two points from which the distance is measured. */
            public WorldPoint endPoint; /* The second of the two points from which the distance is measured. */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            internal Single[] reserved;

            public MeasureData()
            {
                reserved = new Single[6];
            }
        };

        /** 
           MeasureDistance: measure the distance between 2 points in mm
           photo: is the photo instance
           startPoint, endPoint: are the start pont and end point of the distance that need to be measured.
           Note: Depth data must be available and accurate at the start and end point selected. 
        */
        public pxcmStatus MeasureDistance(PXCMPhoto photo, PXCMPointI32 startPoint, PXCMPointI32 endPoint, out MeasureData outData)
        {
            outData = new MeasureData();
            return PXCMEnhancedPhoto_MeasureDistance(instance, photo.instance, startPoint, endPoint, outData);
        }

        /** 
            DepthRefocus: refocus the image at input focusPoint by using depth data refocus
            photo: is the color and depth photo instance
            focusPoint: is the selected point foir refocussing.
            aperture: Range of the blur area [1-100] 
            Note: The application must release the returned refocussed image
        */
        public PXCMPhoto DepthRefocus(PXCMPhoto photo, PXCMPointI32 focusPoint, Single aperture) 
        {
            IntPtr image = PXCMEnhancedPhoto_DepthRefocus(instance, photo.instance, focusPoint, aperture);
            return image != IntPtr.Zero ? new PXCMPhoto(image, true) : null;
        }

        public PXCMPhoto DepthRefocus(PXCMPhoto photo, PXCMPointI32 focusPoint)
        {
            return DepthRefocus(photo, focusPoint, 50.0F);
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

        /** 
             ComputeMaskFromCoordinate: convenience function that creates a mask directly from a depth coordinate.
             photo: input color and depth photo, only used the depth map.
             coord: input (x,y) coordinates on the depth map.  
             Returns a mask in the form of PXCMImage for blending with the current photo frame.
             Note: this function simply calls ComputeMaskFromThreshold underneath.
         */
        public PXCMImage ComputeMaskFromCoordinate(PXCMPhoto photo, PXCMPointI32 coord, MaskParams maskParams)
        {
            IntPtr image = PXCMEnhancedPhoto_ComputeMaskFromCoordinate(instance, photo.instance, coord, maskParams);
            return image != IntPtr.Zero ? new PXCMImage(image, true) : null;
        }
        
        public PXCMImage ComputeMaskFromCoordinate(PXCMPhoto photo, PXCMPointI32 coord)
        {
            MaskParams maskParams = new MaskParams();
            return ComputeMaskFromCoordinate(photo, coord, maskParams);
        }
        /** 
          ComputeMaskFromThreshold: calculates a mask from the threshold computed 
          photo: input color and depth photo, only used the depth map.
          depthThreshold: depth threshold. 
          frontObjectDepth: foreground depth
          backOjectDepth: background depth
          Returns a mask in the form of PXCMImage for blending with the current photo frame.
	
          Notes:
          For every pixel, if the mask is between the range of [POIdepth - frontObjectDepth, POIdepth + backObjectDepth], mask[p] -1.
          For every pixel p with depth in the range [POI - frontObjectDepth - nearFalloffDepth, POI - frontObjectDepth], mask[p] equals the "smoothstep" function value.
          For every pixel p with depth in the range [POI  + backObjectDepth , POI + backOjectDepth + farFallOffDepth], mask[p] equals the "smoothstep" function value.
          For every pixel p with other depth value, mask[p] = 1.
      */
        public PXCMImage ComputeMaskFromThreshold(PXCMPhoto photo, Single depthThreshold, MaskParams maskParams)
        {
            IntPtr image = PXCMEnhancedPhoto_ComputeMaskFromThreshold(instance, photo.instance, depthThreshold, maskParams);
            return image != IntPtr.Zero ? new PXCMImage(image, true) : null;

        }

        public PXCMImage ComputeMaskFromThreshold(PXCMPhoto photo, Single depthThreshold)
        {
            MaskParams maskParams = new MaskParams();
            return ComputeMaskFromThreshold(photo, depthThreshold, maskParams);
        }

        /**
	        InitMotionEffect: the function initializes the 6DoF parallax function with the photo that needs processing. 
        	sample: 2D+depth input image.
	        returns PXCMStatus.
	    */
        public pxcmStatus InitMotionEffect(PXCMPhoto photo)
        {
            return PXCMEnhancedPhoto_InitMotionEffect(instance, photo.instance);
        }

        /**
            ApplyMotionEffect: The function applies a 6DoF parallax effect which is the difference in the apparent position of an object
            when it is viewed from two different positions or viewpoints. 

            motion[3]: is the right, up, and forward motion when (+ve), and Left, down and backward motion when (-ve)
            motion[0]: + right   / - left 
            motion[1]: + up      / - down
            motion[2]: + forward / - backward
            rotation[3]: is the Pitch, Yaw, Roll rotations in degrees in the Range: 0-360. 
            rotation[0]: pitch 
            rotation[1]: yaw
            rotation[2]: roll
            zoomFactor: + zoom in / - zoom out
            PXCMImage: the returned parallaxed image.
        */
        public PXCMImage ApplyMotionEffect(Single[] motion, Single[] rotation, Single zoomFactor)
        {
            IntPtr image = PXCMEnhancedPhoto_ApplyMotionEffect(instance, motion, rotation, zoomFactor);
            return image != IntPtr.Zero ? new PXCMImage(image, true) : null;
        }

        /* Constructors & Misc */
        internal PXCMEnhancedPhoto(IntPtr instance, Boolean delete)
            : base(instance, delete)
        {
        }

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

            /** 
            Input param for Depth fill Quality: 
            High: better quality, slow execution mostly used for post processing (image)
            Low : lower quality, fast execution mostly used for realtime processing (live video sequence)
            */
            public enum DepthFillQuality
            {
                HIGH = 0,
                LOW = 1,
            };

            /** 
                EnhanceDepth: enhance the depth image quality by filling holes and denoising
                outputs the enhanced depth image
                photo: input color, depth photo, and calibration data
                depthQuality: Depth fill Quality: HIGH or LOW for post or realtime processing respectively
                Note: The application must release the returned enhanced depth image
            */
            public PXCMPhoto EnhanceDepth(PXCMPhoto photo, DepthFillQuality depthQuality)
            {
                IntPtr image = PXCMEnhancedPhoto_EnhanceDepth(instance, photo.instance, depthQuality);
                return image != IntPtr.Zero ? new PXCMPhoto(image, true) : null;
            }

            /** 
                DepthMapQuality: Output param for Depth Map Quality: 
                BAD, FAIR and GOOD
            */
            public enum DepthMapQuality
            {
                BAD = 0,
                FAIR = 1,
                GOOD = 2,
            };

            /**
                DepthQuality: retruns the quality of the the depth map
                photo: input color, raw depth map, and calibration data
                depthQuality: BAD, FAIR, GOOD
            */
            public DepthMapQuality GetDepthQuality(PXCMImage depthMap)
            {
                return PXCMEnhancedPhoto_GetDepthQuality(instance, depthMap.instance);
            }

            /** 
                Crop: The primary image, the camera[0] RGB and depth images are cropped 
                and the intrinsic / extrinsic info is updated.
                photo: input image color+depth
                rect : top left corner (x,y) plus width and height of the window to keep 
                and crop all the rest
                Returns a photo that has all its images cropped and metadata fixed accordingly.
                Note: Returns null if function fails
            */
            public PXCMPhoto PhotoCrop(PXCMPhoto photo, PXCMRectI32 rect)
            {
                IntPtr image = PXCMEnhancedPhoto_PhotoCrop(instance, photo.instance, rect);
                return image != IntPtr.Zero ? new PXCMPhoto(image, true) : null;

            }

            /** 
                UpScaleDepth: Change the size of the enhanced depth map. 
                This function preserves aspect ratio, so only new width is required.
                photo: input image color+depth
                width: the new width.
                enhancementType: if the inPhoto has no enhanced depth, then do this type of depth enhancement before resizing.
                Returns a Depth map that has the same aspect ratio as the color image resolution.
                Note: Returns null if the aspect ratio btw color and depth is not preserved
            */
            public PXCMPhoto DepthResize(PXCMPhoto photo, Int32 size, DepthFillQuality enhancementType)
            {
                IntPtr image = PXCMEnhancedPhoto_DepthResize(instance, photo.instance, size, enhancementType);
                return image != IntPtr.Zero ? new PXCMPhoto(image, true) : null;
            }

            public PXCMPhoto DepthResize(PXCMPhoto photo, Int32 size)
            {
                return DepthResize(photo, size, DepthFillQuality.HIGH);

            }

            /** 
                PhotoResize: Change the size of the reference (primary) image. 
                This function preserves aspect ratio, so only new width is required.
                Only the primary image is resized.
                photo - input photo.
                width - the new width.
                Returns a photo with the reference (primary) color image resized while maintaining the aspect ratio.
                Note: Returns null when the function fails
            */
            public PXCMPhoto ColorResize(PXCMPhoto photo, Int32 width)
            {
                IntPtr image = PXCMEnhancedPhoto_ColorResize(instance, photo.instance, width);
                return image == IntPtr.Zero ? null : new PXCMPhoto(image, true);
            }

            /**
                PhotoRotate: rotates a Photo (color, depth and metadata).
                this function rotates the primary image, the RGB and depth images in camera 0, and updates 
                the corresponding intrinsic/extrinsic info.
                photo: input photo.
                degrees: the angle of rotation around the center of the color image in degrees.
                Returns a rotated photo.
                Note: Returns null when the function fails
                THIS FUNCTION ONLY DOES FIXED ROTATIONS OF 90, 180 & 270 DEGREES
            */
            public PXCMPhoto PhotoRotate(PXCMPhoto photo, Single degrees)
            {
                IntPtr image = PXCMEnhancedPhoto_PhotoRotate(instance, photo.instance, degrees);
                return image == IntPtr.Zero ? null : new PXCMPhoto(image, true);
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

            /**
            ObjectSegment: generates an initial mask for any object selected by the bounding box. 
            The mask can then be refined by hints supplied by the user in RefineMask() function. 
            photo: input color and depth photo.
            topLeftCoord    : top left corner of the object to segment.  
            bottomRightCoord: Bottom right corner of the object to segment.
            Returns a mask in the form of PXCMImage with detected pixels set to 255 and undetected pixels set to 0.
            */
            public PXCMImage ObjectSegment(PXCMPhoto photo, PXCMPointI32 topLeftCoord, PXCMPointI32 bottomRightCoord)
            {
                IntPtr image = PXCMEnhancedPhoto_ObjectSegmentDeprecated(instance, photo.instance, topLeftCoord, bottomRightCoord);
                return image == IntPtr.Zero ? null : new PXCMImage(image, true);
            }

            /**
                ObjectSegment: generates an initial mask for any object selected by the bounding box. 
                The mask can then be refined by hints supplied by the user in RefineMask() function. 
                photo: input color and depth photo.
                Returns a mask in the form of PXCMImage with detected pixels set to 255 and undetected pixels set to 0.
            */
            public PXCMImage ObjectSegment(PXCMPhoto photo, PXCMImage mask)
            {
                IntPtr image = PXCMEnhancedPhoto_ObjectSegment(instance, photo.instance, (mask == null) ? IntPtr.Zero : mask.instance);
                return image == IntPtr.Zero ? null : new PXCMImage(image, true);
            }

            /** 
                RefineMask: refines the mask generated by the ObjectSegment() function by using hints.
                hints: input mask with hints. hint values.
                0 = no hint
                1 = foreground
                2 = background
                Returns a mask in the form of PXCMImage with detected pixels set to 255 and undetected pixels set to 0.
            */
            public PXCMImage RefineMask(PXCMImage hints)
            {
                IntPtr image = PXCMEnhancedPhoto_RefineMaskDeprecated(instance, (hints == null) ? IntPtr.Zero : hints.instance);
                return image == IntPtr.Zero ? null : new PXCMImage(image, true);
            }

            /** 
                RefineMask: refines the mask generated by the ObjectSegment() function by using hints.
                points: input arrays with hints' coordinates.
                length: length of the array
                isForeground: bool set to true if input hint locations are foreground and false if background
                Returns a mask in the form of PXCImage with detected pixels set to 255 and undetected pixels set to 0.
            */
            public PXCMImage RefineMask(PXCMPointI32 points, Int32 length, Boolean isForeground)
            {
                IntPtr image = PXCMEnhancedPhoto_RefineMask(instance, points, length, isForeground);
                return image == IntPtr.Zero ? null : new PXCMImage(image, true);
            }

            /** 
	            Undo: undo last hints.
		        Returns a mask in the form of PXCImage with detected pixels set to 255 and undetected pixels set to 0.
		    */
		    public PXCMImage Undo()
            {
                IntPtr image = PXCMEnhancedPhoto_Undo(instance);
                return image == IntPtr.Zero ? null : new PXCMImage(image, true);
            }
		
		    /** 
		        Redo: Redo the previously undone hint.
		        Returns a mask in the form of PXCImage with detected pixels set to 255 and undetected pixels set to 0.
		    */
            public PXCMImage Redo()
            {
                IntPtr image = PXCMEnhancedPhoto_Redo(instance);
                return image == IntPtr.Zero ? null : new PXCMImage(image, true);
            }

        };
        
        public partial class Paster : PXCMBase
        {
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

            /** 
                PasteEffects:  
                matchIllumination: flag to match Illumination, default value is true		
                transparency: (default) 0.0f = opaque, 1.0f = transparent sticker
                embossHighFreqPass: High Frequency Pass during emboss, default 0.0f no emboss, 1.0f max	
                byPixelCorrection: default false, flag to use by pixel illumination correction, takes shadows in account
                colorCorrection: default false, flag to add color correction		
            */
            [Serializable]
            [StructLayout(LayoutKind.Sequential)]
            public class PasteEffects
            {
                public bool matchIllumination;/* Flag to match Illumination, Default value is true*/
                public Single transparency; /* (Default) 0.0f = opaque, 1.0f = transparent sticker */
                public Single embossHighFreqPass; /* High Frequency Pass during emboss, default 0.0f no emboss, 1.0f max */
                public bool shadingCorrection; /* Default false, flag to use by pixel illumination correction, takes shadows in account */
                public bool colorCorrection; /* Default false, flag to add color correction */
                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
                internal Single[] reserved;

                public PasteEffects()
                {
                    reserved = new Single[6];
                    matchIllumination = true;
                    transparency = 0.0f;
                    embossHighFreqPass = 0.0f;
                    shadingCorrection = false;
                    colorCorrection = false;
                }
            };

            /** 
		    SetPhoto: sets the photo that needs to be processed.
		    photo: photo to be processed [color + depth] 
		    Returns PXC_STATUS_NO_ERROR if success. PXC_STATUS_PROCESS_FAILED if process failed
		    */
		    public pxcmStatus SetPhoto(PXCMPhoto photo){
                return PXCMEnhancedPhoto_SetPhoto(instance, photo.instance);
            }

		    /**
		    GetPlanesMap: return plane indices map for current SetPhoto
		    Returns a PXCImage of the plane indices in a form of a mask.
		    */
            public PXCMImage GetPlanesMap()
            {
                IntPtr image = PXCMEnhancedPhoto_GetPlanesMap(instance);
                return image == IntPtr.Zero ? null : new PXCMImage(image, true);

            }
            /** 
                PasteEffects:  
                matchIllumination: flag to match Illumination, default value is true		
                transparency: (default) 0.0f = opaque, 1.0f = transparent sticker
                embossHighFreqPass: High Frequency Pass during emboss, default 0.0f no emboss, 1.0f max	
                byPixelCorrection: default false, flag to use by pixel illumination correction, takes shadows in account
                colorCorrection: default false, flag to add color correction		
            */

            [Serializable]
            [StructLayout(LayoutKind.Sequential)]
		    /** 
		    StickerData:  
		    coord : insertion coordinates
		    height: sticker height in mm, default -1 auto-scale		
		    rotation: in-plane rotation in degree, default 0	
		    isCenter: Anchor point. False means coordinate is top left, true means coordinate is center.
		    */
		    public class StickerData {
			    public Single  height; 
			    public Single  rotation;
			    public bool    isCenter;
			    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
                public Single[] reserved;

			    public StickerData(){
				    reserved = new Single[6];
                    height = -1.0f;
				    rotation = 0.0f;	
				    isCenter = false;
			    }
		    };

            /** 
		    SetSticker: sets the sticker that will be pasted with all configurations needed and paste effects.
		    sticker: the image to paste onto the photo (foreground image)
		    coord : insertion coordinates
		    stickerData: the sticker size, paste location and anchor point.
		    pasteEffects: the pasting effects.
		    Returns PXC_STATUS_NO_ERROR if success. PXC_STATUS_PROCESS_FAILED if process failed
		    */
            public pxcmStatus SetSticker(PXCMImage sticker, PXCMPointI32 coord, StickerData stickerData, PasteEffects pasteEffects)
            {
                return PXCMEnhancedPhoto_SetSticker(instance, sticker.instance, coord, stickerData, pasteEffects);
            }
            
            public pxcmStatus SetSticker(PXCMImage sticker, PXCMPointI32 coord, StickerData stickerData)
            {
                PasteEffects pasteEffects = new PasteEffects();
                return PXCMEnhancedPhoto_SetSticker(instance, sticker.instance, coord, stickerData, pasteEffects);
            }

            public pxcmStatus SetSticker(PXCMImage sticker, PXCMPointI32 coord)
            {
                PasteEffects pasteEffects = new PasteEffects();
                StickerData stickerData = new StickerData();
                return PXCMEnhancedPhoto_SetSticker(instance, sticker.instance, coord, stickerData, pasteEffects);
            }

            /** 
		    PreviewSticker: returns a sticker mask showing the location of the pasted sticker.
		    Returns a PXCImage of the previewed sticker in a form of a mask.
		    */
            public PXCMImage PreviewSticker()
            {
                IntPtr image = PXCMEnhancedPhoto_PreviewSticker(instance);
                return image == IntPtr.Zero ? null : new PXCMImage(image, true);

            }

		    /** 
		    Paste: pastes a smaller 2D image (sticker) onto a bigger color + depth image (background).
		    The smaller foreground image, is rendered according to a
		    user-specified position and an auto-detected plane orientation onto the background image.
		    The auto-oriented foreground image and the color data of the background image are composited together
		    according to the alpha channal of the foreground image.

		    Returns the embeded foreground with background image.
		    */
            public PXCMPhoto Paste()
            {
                IntPtr outPhoto = PXCMEnhancedPhoto_Paste(instance);
                return outPhoto != IntPtr.Zero ? new PXCMPhoto(outPhoto, true) : null;

            }

            /** 
                PasteOnPlane: This function is provided for texturing a smaller 2D image (foreground)
                onto a bigger color + depth image (background). The smaller foreground image, is rendered according to a
                user-specified position and an auto-detected plane orientation onto the background image.
                The auto-oriented foreground image and the color data of the background image are composited together
                according to the alpha channal of the foreground image.

                imbedIm: the image to imbed in the photo (foreground image)
                topLeftCoord, bottomLeftCoord: are the top left corner and the bottom left corner of where the user wants to embed the image.
                Returns the imbeded foreground with background image.
            */
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
    };

#if RSSDK_IN_NAMESPACE
}
#endif
