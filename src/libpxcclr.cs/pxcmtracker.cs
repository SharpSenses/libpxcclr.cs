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

public partial class PXCMTracker : PXCMBase
{
    new public const Int32 CUID = 0x524b5254;

    /// <summary>
    /// The tracking states of a target.
    ///
    /// The state of a target usually starts with ETS_NOT_TRACKING.
    /// When it is found in the current camera image, the state change to
    /// ETS_FOUND for one image, the following images where the location of the
    /// target is successfully determined will have the state ETS_TRACKING.
    ///
    /// Once the tracking is lost, there will be one single frame ETS_LOST, then
    /// the state will be ETS_NOT_TRACKING again. In case there is extrapolation 
    /// of the pose requested, the transition may be from ETS_TRACKING to ETS_EXTRAPOLATED.
    ///
    /// To sum up, these are the state transitions to be expected:
    ///  ETS_NOT_TRACKING -> ETS_FOUND 
    ///  ETS_FOUND        -> ETS_TRACKING
    ///  ETS_TRACKING     -> ETS_LOST
    ///  ETS_LOST         -> ETS_NOT_TRACKING
    ///
    /// With additional extrapolation, these transitions can occur as well:
    ///  ETS_TRACKING     -> ETS_EXTRAPOLATED
    ///  ETS_EXTRAPOLATED -> ETS_LOST
    ///
    /// "Event-States" do not necessarily correspond to a complete frame but can be used to 
    /// flag individual tracking events or replace tracking states to clarify their context:
    ///  ETS_NOT_TRACKING -> ETS_REGISTERED -> ETS_FOUND for edge based initialization
    /// </summary>
    public enum ETrackingState
    {
        ETS_UNKNOWN = 0,	// Tracking state is unknown
        ETS_NOT_TRACKING = 1,	// Not tracking
        ETS_TRACKING = 2,	//Tracking
        ETS_LOST = 3,	//Target lost
        ETS_FOUND = 4,	//Target found
        ETS_EXTRAPOLATED = 5,	//Tracking by extrapolating
        ETS_INITIALIZED = 6,	//The tracking has just been loaded
        ETS_REGISTERED = 7,	//Event-State: Pose was just registered for tracking
        ETS_INIT_FAILED = 8    //Initialization failed (such as 3D instant tracking)
    };

    [Serializable]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public class TrackingValues
    {
        public ETrackingState state;		//The state of the tracking values
        public PXCMPoint3DF32 translation;	//Translation component of the pose
        public PXCMPoint4DF32 rotation; 	//Rotation component of the pose

        /// <summary>
        /// Quality of the tracking values.
        /// Value between 0 and 1 defining the tracking quality.
        /// A higher value means better tracking results. More specifically:
        ///- 1 means the system is tracking perfectly.
        ///- 0 means that we are not tracking at all.
        /// </summary>
        public Single quality;
        /// <summary>
        /// Time elapsed (in ms) since last state change of the tracking system
        /// </summary>
        public Double timeElapsed;
        /// <summary>
        /// Time (in milliseconds) used for tracking the respective frame
        /// </summary>
        public Double trackingTimeMs;
        /// <summary>
        /// The ID of the target object
        /// </summary>
        public Int32 cosID;
        /// <summary>
        /// The name of the target object
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public String targetName;
        /// <summary>
        /// Extra space for information provided by a sensor that cannot be expressed with translation and rotation properly.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public String additionalValues;
        /// <summary>
        /// The sensor that provided the values
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public String sensor;
        /// <summary>
        /// The translation component of the pose projected onto the color image, in pixels 
        /// </summary>
        public PXCMPointF32 translationImage;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)]
        internal Int32[] reserved;

        public TrackingValues()
        {
            targetName = "";
            additionalValues = "";
            sensor = "";
            reserved = new Int32[30];
        }
    };

    /// <summary>
    ///  Returns TRUE if the current state is actively tracking (valid pose information is available)
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    public static Boolean IsTracking(ETrackingState state)
    {
        return PXCMTracker_IsTracking(state);
    }

    /// <summary>
    ///  Set the camera parameters, which can be the result of camera calibration from the toolbox
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public pxcmStatus SetCameraParameters(String filename)
    {
        return PXCMTracker_SetCameraParameters(instance, filename);
    }

    /// <summary>
    /// Add a 2D reference image for tracking an object
    /// </summary>
    /// <param name="filename">path to image file</param>
    /// <param name="cosID">coordinate system ID of added target</param>
    /// <param name="widthMM">image width in mm (optional)</param>
    /// <param name="heightMM">image height in mm (optional)</param>
    /// <param name="qualityThreshold">minimal similarity measure [0..1] that has to be fulfilled for the image or one of its sub-patches.</param>
    /// <param name="extensible">Use features from the environment to improve tracking</param>
    /// <returns></returns>
    public pxcmStatus Set2DTrackFromFile(String filename, out Int32 cosID, Single widthMM, Single heightMM, Single qualityThreshold, Boolean extensible)
    {
        return PXCMTracker_Set2DTrackFromFile(instance, filename, out cosID, widthMM, heightMM, qualityThreshold, extensible);
    }

    public pxcmStatus Set2DTrackFromFile(String filename, out Int32 cosID, Single widthMM, Single heightMM, Single qualityThreshold)
    {
        return Set2DTrackFromFile(filename, out cosID, widthMM, heightMM, qualityThreshold, false);
    }

    public pxcmStatus Set2DTrackFromFile(String filename, out Int32 cosID, Boolean extensible)
    {
        return Set2DTrackFromFile(filename, out cosID, 0.0f, 0.0f, 0.8f, extensible);
    }

    public pxcmStatus Set2DTrackFromFile(String filename, out Int32 cosID)
    {
        return Set2DTrackFromFile(filename, out cosID, 0.0f, 0.0f, 0.8f, false);
    }

    /// <summary>
    /// Add a 2D reference image for tracking an object
    /// </summary>
    /// <param name="image">Target image data</param>
    /// <param name="cosID">coordinate system ID of added target</param>
    /// <param name="widthMM">image width in mm (optional)</param>
    /// <param name="heightMM">image height in mm (optional)</param>
    /// <param name="qualityThreshold">minimal similarity measure [0..1] that has to be fulfilled for the image or one of its sub-patches.</param>
    /// <param name="extensible">Use features from the environment to improve tracking</param>
    /// <returns></returns>
    public pxcmStatus Set2DTrackFromImage(PXCMImage image, out Int32 cosID, Single widthMM, Single heightMM, Single qualityThreshold, Boolean extensible)
    {
        if (image == null)
        {
            cosID = 0;
            return pxcmStatus.PXCM_STATUS_HANDLE_INVALID;
        }

        return PXCMTracker_Set2DTrackFromImage(instance, image.instance, out cosID, widthMM, heightMM, qualityThreshold, extensible);
    }

    public pxcmStatus Set2DTrackFromImage(PXCMImage image, out Int32 cosID, Single widthMM, Single heightMM, Single qualityThreshold)
    {
        return Set2DTrackFromImage(image, out cosID, widthMM, heightMM, qualityThreshold, false);
    }

    public pxcmStatus Set2DTrackFromImage(PXCMImage image, out Int32 cosID, Boolean extensible)
    {
        return Set2DTrackFromImage(image, out cosID, 0.0f, 0.0f, 0.8f, extensible);
    }

    public pxcmStatus Set2DTrackFromImage(PXCMImage image, out Int32 cosID)
    {
        return Set2DTrackFromImage(image, out cosID, 0.0f, 0.0f, 0.8f, false);
    }

    /// <summary>
    /// Add a 3D tracking configuration for a target
    /// This file can be generated with the Toolbox
    /// </summary>
    /// <param name="filename">The full path to the configuration file (*.slam, *.xml)</param>
    /// <param name="firstCosID">coordinate system ID of the first added target</param>
    /// <param name="lastCosID">coordinate system ID of the last added target (may be the same as firstCosID)</param>
    /// <param name="extensible">Use features from the environment to improve tracking</param>
    /// <returns></returns>
    public pxcmStatus Set3DTrack(String filename, out Int32 firstCosID, out Int32 lastCosID, Boolean extensible)
    {
        return PXCMTracker_Set3DTrack(instance, filename, out firstCosID, out lastCosID, extensible);
    }

    public pxcmStatus Set3DTrack(String filename, out Int32 firstCosID, out Int32 lastCosID)
    {
        return Set3DTrack(filename, out firstCosID, out lastCosID, false);
    }

    /// <summary>
    /// Remove a previously returned cosID from tracking.
    /// </summary>
    /// <param name="cosID"></param>
    /// <returns></returns>
    public pxcmStatus RemoveTrackingID(Int32 cosID)
    {
        return PXCMTracker_RemoveTrackingID(instance, cosID);
    }

    /// <summary>
    /// Remove all previous created cosIDs from tracking
    /// </summary>
    /// <returns></returns>
    public pxcmStatus RemoveAllTrackingIDs()
    {
        return PXCMTracker_RemoveAllTrackingIDs(instance);
    }

    /// <summary>
    /// Enable instant 3D tracking (SLAM).  This form of tracking does not require an object model to be previously created and loaded.
    /// </summary>
    /// <param name="egoMotion">Specify the coordinate system origin and orientation of the tracked object. 
    /// true uses the first image captured from the camera
    /// false (default) uses the "main plane" of the scene which is determined heuristically</param>
    /// <param name="framesToSkip">Instant tracking may fail to initialize correctly if the camera image has not stabilized
    /// or is not pointing at the desired object when the first frames are processed.  This parameter
    /// skips the initial frames which may have automatic adjustments such as contrast occuring.
    /// This parameter may be 0 if instant 3D tracking should initialize from the next frame.</param>
    /// <returns></returns>
    public pxcmStatus Set3DInstantTrack(Boolean egoMotion, Int32 framesToSkip)
    {
        return PXCMTracker_Set3DInstantTrack(instance, egoMotion, framesToSkip);
    }

    public pxcmStatus Set3DInstantTrack(Boolean egoMotion)
    {
        return Set3DInstantTrack(egoMotion, 0);
    }

    public pxcmStatus Set3DInstantTrack()
    {
        return Set3DInstantTrack(false);
    }

    /// <summary>
    /// Get the number of targets currently tracking
    /// see QueryTrackingValues, QueryAllTrackingValues
    /// </summary>
    /// <returns>The number of active tracking targets</returns>
    public Int32 QueryNumberTrackingValues()
    {
        return PXCMTracker_QueryNumberTrackingValues(instance);
    }

    /// <summary>
    /// Get information for all of the active tracking targets
    /// </summary>
    /// <param name="trackingValues"> Pointer to store the tracking results at.  The passed in block must be at least QueryNumberTrackingValues() elements long</param>
    /// <returns></returns>
    public pxcmStatus QueryAllTrackingValues(out TrackingValues[] trackingValues)
    {
        return QueryAllTrackingValuesINT(instance, out trackingValues);
    }

    /// <summary>
    /// Return information for a particular coordinate system ID.  This value can be returned from Set2DTrackFromFile(),
    /// Set2DTrackFromImage(), or Set3DTrack().  coordinate system IDs for Set3DInstantTrack() are generated dynamically as
    /// targets that are determined in the scene.
    /// </summary>
    /// <param name="cosID">The coordinate system ID to return the status for</param>
    /// <param name="trackingValues">The returned tracking values. the user needs to manage the mapping between the cosIDs and targets in loaded.</param>
    /// <returns></returns>
    public pxcmStatus QueryTrackingValues(Int32 cosID, out TrackingValues trackingValues)
    {
        return QueryTrackingValuesINT(instance, cosID, out trackingValues);
    }

    public pxcmStatus SetRegionOfInterest(PXCMRectI32 roi)
    {
        return PXCMTracker_SetRegionOfInterest(instance, roi);
    }

    public PXCMTrackerUtils QueryTrackerUtils()
    {
        IntPtr utils = PXCMTracker_QueryTrackerUtils(instance);
        return utils == IntPtr.Zero ? null : new PXCMTrackerUtils(utils, false);
    }

    /* constructors and misc */
    internal PXCMTracker(IntPtr instance, Boolean delete)
        : base(instance, delete)
    {
    }
};

#if RSSDK_IN_NAMESPACE
}
#endif