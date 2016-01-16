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

    public partial class PXCMTrackerUtils: PXCMBase {
        new public const Int32 CUID=0x554b5254;
		
	    /**
	    * The relative size of a target object.  
        * Specifying the appropriate size helps improve the training initialization process.
	    */
        [Flags]
	    public enum ObjectSize
	    {
		    VERY_SMALL = 0,		/// Cup sized
		    SMALL	   = 5,		/// Desktop sized
		    MEDIUM	   = 10,	/// Room sized
		    LARGE	   = 15		/// Building sized
	    };

	    /**
	    * Special purpose cosIDs which may be passed in to \c QueryTrackingValues. These values may be used to get the
	    * current tracking position of the map creation operations.
	    *
	    * \sa PXCMTracker.QueryTrackingValues
	    */
        [Flags]
	    public enum UtilityCosID
	    {
		    CALIBRATION_MARKER = -1,	/// Pose of the detected calibration marker
		    ALIGNMENT_MARKER   = -2,	/// New pose of the tracked object based on the alignment marker
		    IN_PROGRESS_MAP    = -3		/// Current pose of the new map as it is being created
	    };

	    /**
	    *	Interactive version of map creation, similar to the toolbox functionality. Depth is automatically
	    *   used if supported in the current camera profile.  Map creation is stopped either explicitly with \c Cancel3DMapCreation
	    *	or by pausing the tracking module using pSenseManager->PauseTracker(TRUE).
	    *
	    *	The map file may be saved at any time with \c Save3DMap.
	    *
	    *   \param objSize: Relative size of the object to create a map for, an accurate value helps improve the initialization time.
	    *
	    *	\sa Cancel3DMapCreation
	    *	\sa Save3DMap
	    *	\sa QueryNumberFeaturePoints
	    *	\sa QueryFeaturePoints
	    */	
	    public pxcmStatus Start3DMapCreation(ObjectSize objSize) {
            return PXCMTrackerUtils_Start3DMapCreation(instance, objSize); 
        }

	    /** Cancel map creation without saving a file, resetting the internal state.
	    *
	    *	\sa Start3DMapCreation
	    */
	    public pxcmStatus Cancel3DMapCreation() {
            return PXCMTrackerUtils_Cancel3DMapCreation(instance);
        }

	    /**
	    *	Begins extending a previously created 3D Map with additional feature points. Map extension is an interactive process only.
	    *	The extended map may be saved using \c Save3DMap at any time.
	    *
	    *	\sa Load3DMap	
	    */
	    public pxcmStatus Start3DMapExtension() {
           return PXCMTrackerUtils_Start3DMapExtension(instance);
        }

	    /**
	    *	Cancel map extension without saving a file, and reset the internal state.
	    *
	    *	\sa Cancel3DMapExtension
	    */
	    public pxcmStatus Cancel3DMapExtension() {
            return PXCMTrackerUtils_Cancel3DMapExtension(instance);
        }

	    /**
	    *	Load a 3D Map from disk in preparation for map extension or alignment operations.
	    *
	    *	\param filename: Name of the filename to be loaded
	    */
	    public pxcmStatus Load3DMap(String filename){
            return PXCMTrackerUtils_Load3DMap(instance, filename);
        }

	    /**
	    *	Save a 3D Map.  Maps must be saved to disk for further usage, it is not possible to generate a map in memory
	    *	and use it for tracking or extension later.
	    *
	    *	\param filename: Name of the filename to be saved
	    */
	    public pxcmStatus Save3DMap(String filename) {
            return PXCMTrackerUtils_Save3DMap(instance, filename);
        }

	    /**
	    *	Returns the number of detected feature points during map creation.
	    *
	    *	\sa QueryFeaturePoints
	    */	
	    public Int32 QueryNumberFeaturePoints() {
            return PXCMTrackerUtils_QueryNumberFeaturePoints(instance);
        }

	    /**
	    *	Retrieve the detected feature points for map creation.  Active points are ones which have been detected
	    *	in the current frame, inactive points were detected previously but are not detected in the current frame.
	    *
	    *	\param points:		Array where the feature points will be stored
	    *	\param returnActive: Return the active (currently tracked) features in the array
	    *	\param returnInactive: Return the inactive (not currently tracked) features in the array
	    *	
	    *	\return The number of feature points copied into \c points
	    *
	    *	\sa QueryNumberFeaturePoints
	    *	\sa Start3DMapCreation
	    *	\sa Start3DMapExtension
	    */
	    public Int32 QueryFeaturePoints(PXCMPoint3DF32[] points, Boolean returnActive, Boolean returnInactive) {
            return QueryFeaturePointsINT(instance, (points==null)?0:points.Length, points, returnActive, returnInactive);
        }
		
	    /**
	    *	Aligns a loaded 3D map to the specified marker. Alignment defines the initial pose of the model relative to the axes
	    *	printed on the marker (+Z points up out of the page).  By default, the coordinate system pose (origin and rotation)
	    *	is in an undefined position with respect to the object. The placement of the marker specifies the (0,0,0) origin as
	    *	well as the alignment of the coordinate axes (initial rotation).
	    *
	    *	Alignment also enhances the returned pose coordinates to be in units of millimeters, instead of an undefined unit.
	    *
	    *	\param markerID: Integer identifier for the marker (from the marker PDF)
	    *	\param markerSize: Size of the marker in millimeters
	    *
	    *	\sa Stop3DMapAlignment, Is3dMapAlignmentComplete
	    */	
	    public pxcmStatus Start3DMapAlignment(Int32 markerID, Int32 markerSize) {
            return PXCMTrackerUtils_Start3DMapAlignment(instance, markerID, markerSize);
        }

	    /**
	    *	Cancel the current 3D Map alignment operation before it is complete.  Any in-progress state will be lost.
	    *
	    *	\sa Start3DMapAlignment
	    *	\sa Is3DMapAlignmentComplete
	    */	
	    public pxcmStatus Cancel3DMapAlignment() {
            return PXCMTrackerUtils_Cancel3DMapAlignment(instance);
        }

	    /**
	    *	Returns \c TRUE if alignment is complete.  At that point the file may be saved with the new alignment values.
	    *
	    *	\sa Start3DMapAlignment
	    */	
	    public Boolean Is3DMapAlignmentComplete() {
            return PXCMTrackerUtils_Is3DMapAlignmentComplete(instance);
        }

	    /**
	    *	Start the camera calibration process. Calibration can improve the tracking results by compensating for
	    *	camera distortion and other intrinsic camera values.  A successful calibration requires several frames,
	    *	with the marker in different orientations and rotations relative to the camera.
	    *
	    *	\param markerID: Integer identifier for the marker (from the marker PDF file)
	    *	\param markerSize: Size of the printed marker in millimeters
	    *
	    *	\sa QueryCalibrationProgress
	    *	\sa SaveCameraParameters
	    */
	    public pxcmStatus StartCameraCalibration(Int32 markerID, Int32 markerSize) {
            return PXCMTrackerUtils_StartCameraCalibration(instance, markerID, markerSize);
        }

	    /**
	    *	Stop the camera calibration process before it is complete.  No new calibration parameters may be saved.	
	    *
	    *	\sa StartCameraCalibration
	    *	\sa QueryCalibrationProgress
	    */	
	    public pxcmStatus CancelCameraCalibration() {
            return PXCMTrackerUtils_CancelCameraCalibration(instance);
        }

	    /**
	    *	Return the calibration progress as a percentage (0 - 100%). Calibration requires several different views of the
	    *	marker to produce an accurate result, this function returns the relative progress.  A calibration file may be saved
	    *	before this function returns 100% but the quality will be degraded.
	    *
	    *	If calibration has not been started this function returns a negative value
	    *
	    *	\sa StartCameraCalibration
	    */
	    public Int32 QueryCalibrationProgress() {
            return PXCMTrackerUtils_QueryCalibrationProgress(instance);
        }
	
	    /**
	    *	Save the current camera intrinsic parameters to an XML file.
	    *
	    *	\param filename: Filename to save the XML camera parameters in
	    *
	    *	\sa SetCameraParameters
	    *	\sa	StartCameraCalibration	
	    */
	    public pxcmStatus SaveCameraParametersToFile(String filename){
           return PXCMTrackerUtils_SaveCameraParametersToFile(instance, filename);
        }

            /* constructors and misc */
        internal PXCMTrackerUtils(IntPtr instance, Boolean delete)
            : base(instance, delete)
        {
        }
    };

#if RSSDK_IN_NAMESPACE
}
#endif