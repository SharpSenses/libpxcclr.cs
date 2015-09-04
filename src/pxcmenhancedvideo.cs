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
namespace intel.rssdk {
#endif

public partial class PXCMEnhancedVideo : PXCMBase
{
    new public const Int32 CUID = 0x4e495645;

    [Flags]
    public enum TrackMethod {
		LAYER = 0,      // track the depth layer in each frame
		OBJECT          // track the slected object in each frame
	};
        
 
    /**
	 *  EnableTracker: creates an object tracker with a specific tracking method and an 
     *  initial bounding mask as a hint for the object to detect. 
	 *  boundingMask: a hint on what object to detect setting the target pixels to 255 and background to 0.  
	 *  method: Tracking method for depth layer tracking or object tracking.
	*/
	public pxcmStatus EnableTracker(PXCMImage boundingMask, TrackMethod method) 
    {
        return PXCMEnhancedVideo_EnableTracker(instance, (boundingMask == null)? IntPtr.Zero: boundingMask.instance, method);
    }
	
    public pxcmStatus EnableTracker(PXCMImage boundingMask) { 
		return EnableTracker(boundingMask, TrackMethod.LAYER);
	}

	/* 
	 *  QueryTrackedObject: returns the tracked object selected in EnableTracker() after every processed frame.
	 *  Returns a mask in the form of PXCMImage with detected pixels set to 255 and undetected pixels set to 0.
	 *  returned PXCMImage is managed internally APP should not release: TO DO!!
	*/
    public PXCMImage QueryTrackedObject()
    {
        IntPtr image = PXCMEnhancedVideo_QueryTrackedObject(instance);
        return image != IntPtr.Zero ? new PXCMImage(image, true) : null;
    }
 
    /* constructors & misc */
    internal PXCMEnhancedVideo(IntPtr instance, Boolean delete)
        : base(instance, delete)
    {
    }
};

#if RSSDK_IN_NAMESPACE
}
#endif