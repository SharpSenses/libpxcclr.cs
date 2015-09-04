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
		
	    /* 
	        DepthRefocus: refocus the image at input focusPoint by using depth data refocus
	        sample: is the color and depth sample
	        focusPoint: is the selected point foir refocussing.
	        aperture: Range of the blur area [1-100] 
	        Note: The application must release the returned refocussed image
	    */
        public PXCMPhoto DepthRefocus(PXCMPhoto sample, PXCMPointI32 focusPoint, Single aperture) 
        {
            IntPtr image = PXCMEnhancedPhoto_DepthRefocus(instance, sample.instance, focusPoint, aperture);
            return image != IntPtr.Zero ? new PXCMPhoto(image, true) : null;
        }

	    /*
	        DepthRefocus: refocus the image at input focusPoint by using depth data refocus
	        sample: is the color and depth sample
	        focusPoint: is the selected point foir refocussing.
	        Note: The application must release the returned refocussed image
	    */
        public PXCMPhoto DepthRefocus(PXCMPhoto sample, PXCMPointI32 focusPoint)
        {
            return DepthRefocus(sample, focusPoint, 50.0F);
	    }

        /* 
	        Input param for Depth fill Quality: 
	        High: better quality, slow execution mostly used for post processing (image)
	        Low : lower quality, fast execution mostly used for realtime processing (live video sequence)
	    */
	    public enum DepthFillQuality {
		    HIGH = 0, 
		    LOW = 1, 
	    };

	    /* 
	        EnhanceDepth: enhance the depth image quality by filling holes and denoising
	        outputs the enhanced depth image
	        sample: is the input color & depth sample
	        depthQuality: Depth fill Quality: HIGH or LOW for post or realtime processing respectively
	        Note: The application must release the returned enhanced depth image
	    */
        public PXCMPhoto EnhanceDepth(PXCMPhoto sample, DepthFillQuality depthQuality)
        {
            IntPtr image = PXCMEnhancedPhoto_EnhanceDepth(instance, sample.instance, depthQuality);
            return image != IntPtr.Zero ? new PXCMPhoto(image, true) : null;
        }

        ///* 
        //    GetDepthThreshold: calculates the depth threshold from a depth coordinate 
        //    depthMap: input depth Map
        //    coord: input (x,y) coordinates on the rgb map.  
        //    Returns a pxcF32 threshold that can be used to compute the mask for 2 layer segmentation
        //*/
        //public Single GetDepthThreshold(PXCMPhoto sample, PXCMPointI32 coord)
        //{
        //    return PXCMEnhancedPhoto_GetDepthThreshold(instance, sample.instance, coord);
        //}
	
	    /* 
	        ComputeMaskFromThreshold: calculates a maks from the threshold computed 
	        depthMap: input depth Map
	        depthThreshold: depth threshold.  
	        Returns a mask in the form of PXCMImage for blending with the current sample frame.
	    */
        public PXCMImage ComputeMaskFromThreshold(PXCMPhoto sample, Single depthThreshold)
        {
            IntPtr image = PXCMEnhancedPhoto_ComputeMaskFromThreshold(instance, sample.instance, depthThreshold);
            return image != IntPtr.Zero ? new PXCMImage(image, true) : null;
        }
	    
	    /* 
	        ComputeMaskFromCoordinate: convenience function that creates a mask directly from a depth coordinate.
	        depthMap: input depth Map
	        coord: input (x,y) coordinates on the depth map.  
	        Returns a mask in the form of PXCImage for blending with the current sample frame.
	        Note: this function simply calls GetDepthThreshold then ComputeMaskFromThreshold in sequence.
	    */
        public PXCMImage ComputeMaskFromCoordinate(PXCMPhoto sample, PXCMPointI32 coord)
        {
            IntPtr image = PXCMEnhancedPhoto_ComputeMaskFromCoordinate(instance, sample.instance, coord);
            return image != IntPtr.Zero ? new PXCMImage(image, true) : null;
        }

	    /* 
	        UpScaleDepth: Resize the depth map of a given Sample according to its color image size. 
	        High quality upscaling of depth image based on color image.
	        sample: input image color+depth
	        Returns a Depth map that matches the color image resolution.
	    */
        public PXCMPhoto DepthResize(PXCMPhoto sample, PXCMSizeI32 size)
        {
            IntPtr image = PXCMEnhancedPhoto_DepthResize(instance, sample.instance, size);
            return image != IntPtr.Zero ? new PXCMPhoto(image, true) : null;
        }
  
	    /* 
	        PasteOnPlane: This function is provided for texturing a smaller 2D image (foreground)
	        onto a bigger color + depth image (background). The smaller foreground image, is rendered according to a
	        user-specified position and an auto-detected plane orientation onto the background image.
	        The auto-oriented foreground image and the color data of the background image are composited together
	        according to the alpha channal of the foreground image.

	        imbedIm: the image to imbed in the sample (foreground image)
	        topLeftCoord, bottomLeftCoord: are the top left corner and the bottom left corner of where the user wants to embed the image.
	        Returns the imbeded foreground with background image.
	    */
        public PXCMPhoto PasteOnPlane(PXCMPhoto sample, PXCMImage imbedIm, PXCMPointI32 topLeftCoord, PXCMPointI32 bottomLeftCoord)
        {
            IntPtr image = PXCMEnhancedPhoto_PasteOnPlane(instance, sample.instance, (imbedIm == null) ? IntPtr.Zero : imbedIm.instance, topLeftCoord, bottomLeftCoord);
            return image != IntPtr.Zero ? new PXCMPhoto(image, true) : null;
        }

	    /*
	        DepthBlend: blend the a foreground 2D image into the background color+depth image. the function rotates and 
	        scales the foreground 2D image before inserting it at a user specified coordinate and depth value. 

	        sampleBackground: 2D+depth background image. The depth data should be hole-filled
	        imgForeground: RGBA 2D foreground image 
	        insertCoord: (x,y) user-specified location of the 2D image center onto the background image
	        insertDepth: user-specified depth value of the 2D image inside the background image. 
	        rotation[3]: is the Pitch, Yaw, Roll rotations in degrees in the Range: 0-360. 
		        rotaion[0]: pitch 
		        rotaion[1]: yaw
		        rotaion[2]: roll
	        scaleFG: scaling factor. E.g. 2.0 means 2x bigger.
	        PXCMImage: he returned blended 2D image foreground + background.
	    */
        public PXCMPhoto DepthBlend(PXCMPhoto sampleBackground, PXCMImage imgForeground,
            PXCMPointI32 insertCoord, Int32 insertDepth, Int32[] rotation, Single scaleFG)
        {
            IntPtr image = PXCMEnhancedPhoto_DepthBlend(instance, sampleBackground.instance, (imgForeground == null) ? IntPtr.Zero : imgForeground.instance, insertCoord, insertDepth, rotation, scaleFG);
            return image != IntPtr.Zero ? new PXCMPhoto(image, true) : null;
        }

	    /* 
	    ObjectSegment: generates an initial mask for any object selected by the bounding box. 
	    The mask can then be refined by hints supplied by the user in RefineMask() function. 
	    photo: input color and depth photo.
	    topLeftCoord    : top left corner of the object to segment.  
	    bottomRightCoord: Bottom right corner of the object to segment.
	    Returns a mask in the form of PXCImage with detected pixels set to 255 and undetected pixels set to 0.
	    */
	    public PXCMImage ObjectSegment(PXCMPhoto photo, PXCMPointI32 topLeftCoord, PXCMPointI32 bottomRightCoord) {
            IntPtr image=PXCMEnhancedPhoto_ObjectSegment(instance, photo.instance, topLeftCoord, bottomRightCoord);
            return image == IntPtr.Zero ? null : new PXCMImage(image, true);
        }

	    /* 
	    RefineMask: refines the mask generated by the ObjectSegment() function by using hints.
        hints: input mask with hints. hint values.
         0 = no hint
         1 = foreground
         2 = background
	    Returns a mask in the form of PXCImage with detected pixels set to 255 and undetected pixels set to 0.
	    */
	    public PXCMImage RefineMask(PXCMImage hints) {
            IntPtr image = PXCMEnhancedPhoto_RefineMask(instance, (hints == null)?IntPtr.Zero: hints.instance);
            return image == IntPtr.Zero ? null : new PXCMImage(image, true);
        }

        public enum TrackMethod: int {
            LAYER = 0,
            OBJECT,
        };

	    /* 
	        InitTracker: creates an object tracker with a specific tracking method and an initial bounding mask 
	        as a hint for the object to detect. 
	        firstFrame: first frame input color and depth photo.
	        boundingMask: a hint on what object to detect setting the target pixels to 255 and background to 0.  
	        method: Tracking method for depth layer tracking or object tracking.
	    */
	    //public pxcmStatus InitTracker(PXCMPhoto firstFrame, PXCMImage boundingMask, TrackMethod method) {
        //    return PXCMEnhancedPhoto_InitTracker(instance, firstFrame.instance, boundingMask.instance, method);
        //}

	    /* 
	        InitTracker: creates an object tracker with a specific tracking method and an initial bounding mask 
	        as a hint for the object to detect. 
	        firstFrame: first frame input color and depth photo.
	        boundingMask: a hint on what object to detect setting the target pixels to 255 and background to 0.  
	    */
        //public pxcmStatus InitTracker(PXCMPhoto firstFrame, PXCMImage boundingMask) {
        //    return InitTracker(firstFrame, boundingMask, TrackMethod.LAYER);
	    //}
	
    	/* 
	        TrackObject: tracks the object selected in InitTracker() in every nextFrame.
	        Returns a mask in the form of PXCImage with detected pixels set to 255 and undetected pixels set to 0.
	    */
    	//public PXCMImage TrackObject(PXCMPhoto nextFrame) {
        //    IntPtr image=PXCMEnhancedPhoto_TrackObject(instance, nextFrame.instance);
        //    return image == IntPtr.Zero ? null : new PXCMImage(image, true);}

        /** This represents a point in 3D world space in millimeter (mm) units. */
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

        /* This represents the distance between two world points in millimeters (mm). */
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
        
        /* 
           MeasureDistance: measure the distance between 2 points in mm
           sample: is the photo instance
           startPoint, endPoint: are the start pont and end point of the distance that need to be measured.
           Note: Depth data must be availible and accurate at the start and end point selected. 
        */

        public pxcmStatus MeasureDistance(PXCMPhoto sample, PXCMPointI32 startPoint, PXCMPointI32 endPoint, out MeasureData outData)
        {
            outData = new MeasureData();
            return PXCMEnhancedPhoto_MeasureDistance(instance, sample.instance, startPoint, endPoint, outData);
        }

        /**
	        InitMotionEffect: the function initializes the 6DoF parallax function with the photo that needs processing. 
        	sample: 2D+depth input image.
	        returns PXCMStatus.
	    */
            public pxcmStatus InitMotionEffect(PXCMPhoto sample)
            {
                return PXCMEnhancedPhoto_InitMotionEffect(instance, sample.instance);
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

        /* constructors & misc */
        internal PXCMEnhancedPhoto(IntPtr instance, Boolean delete)
            : base(instance, delete)
        {
        }
    };

#if RSSDK_IN_NAMESPACE
}
#endif
