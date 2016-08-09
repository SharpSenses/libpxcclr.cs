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

public partial class PXCMTrackerUtils : PXCMBase
{
    new public const Int32 CUID = 0x554b5254;

    /// <summary>
    /// The relative size of a target object.  
    /// Specifying the appropriate size helps improve the training initialization process.
    /// </summary>
    [Flags]
    public enum ObjectSize
    {
        VERY_SMALL = 0,		/// Cup sized
        SMALL = 5,		/// Desktop sized
        MEDIUM = 10,	/// Room sized
        LARGE = 15		/// Building sized
    };

    /// <summary>
    /// Special purpose cosIDs which may be passed in to QueryTrackingValues. These values may be used to get the
    /// current tracking position of the map creation operations.
    ///
    /// see also PXCMTracker.QueryTrackingValues
    /// </summary>
    [Flags]
    public enum UtilityCosID
    {
        CALIBRATION_MARKER = -1,	/// Pose of the detected calibration marker
        ALIGNMENT_MARKER = -2,	/// New pose of the tracked object based on the alignment marker
        IN_PROGRESS_MAP = -3		/// Current pose of the new map as it is being created
    };

    /// <summary>
    ///	Interactive version of map creation, similar to the toolbox functionality. Depth is automatically
    ///   used if supported in the current camera profile.  Map creation is stopped either explicitly with \c Cancel3DMapCreation
    ///	or by pausing the tracking module using pSenseManager->PauseTracker(TRUE).
    ///
    ///	The map file may be saved at any time with Save3DMap.
    ///	see also Cancel3DMapCreation, Save3DMap, QueryNumberFeaturePoints, QueryFeaturePoints
    /// </summary>	
    /// <param name="objSize">Relative size of the object to create a map for, an accurate value helps improve the initialization time.</param>
    public pxcmStatus Start3DMapCreation(ObjectSize objSize)
    {
        return PXCMTrackerUtils_Start3DMapCreation(instance, objSize);
    }

    /// <summary> Cancel map creation without saving a file, resetting the internal state.
    ///
    ///	see also Start3DMapCreation
    /// </summary>
    public pxcmStatus Cancel3DMapCreation()
    {
        return PXCMTrackerUtils_Cancel3DMapCreation(instance);
    }

    /// <summary>
    ///	Begins extending a previously created 3D Map with additional feature points. Map extension is an interactive process only.
    ///	The extended map may be saved using \c Save3DMap at any time.
    ///
    ///	see also Load3DMap	
    /// </summary>
    public pxcmStatus Start3DMapExtension()
    {
        return PXCMTrackerUtils_Start3DMapExtension(instance);
    }

    /// <summary>
    ///	Cancel map extension without saving a file, and reset the internal state.
    ///
    ///	see also Cancel3DMapExtension
    /// </summary>
    public pxcmStatus Cancel3DMapExtension()
    {
        return PXCMTrackerUtils_Cancel3DMapExtension(instance);
    }

    /// <summary>
    ///	Load a 3D Map from disk in preparation for map extension or alignment operations.
    /// </summary>
    /// <param name="filename">Name of the filename to be loaded</param>
    public pxcmStatus Load3DMap(String filename)
    {
        return PXCMTrackerUtils_Load3DMap(instance, filename);
    }

    /// <summary>
    ///	Save a 3D Map.  Maps must be saved to disk for further usage, it is not possible to generate a map in memory
    ///	and use it for tracking or extension later.
    /// </summary>
    /// <param name="filename">Name of the filename to be saved</param>
    public pxcmStatus Save3DMap(String filename)
    {
        return PXCMTrackerUtils_Save3DMap(instance, filename);
    }

    /// <summary>
    ///	Returns the number of detected feature points during map creation.
    ///
    ///	see also QueryFeaturePoints
    /// </summary>	
    public Int32 QueryNumberFeaturePoints()
    {
        return PXCMTrackerUtils_QueryNumberFeaturePoints(instance);
    }

    /// <summary>
    ///	Retrieve the detected feature points for map creation.  Active points are ones which have been detected
    ///	in the current frame, inactive points were detected previously but are not detected in the current frame.
    ///
    ///	see also QueryNumberFeaturePoints, Start3DMapCreation, Start3DMapExtension
    /// </summary>
    /// <param name="points">Array where the feature points will be stored</param>
    /// <param name="returnActive">Return the active (currently tracked) features in the array</param>
    /// <param name="returnInactive">Return the inactive (not currently tracked) features in the array</param>
    /// <returns>The number of feature points copied into points</returns>
    public Int32 QueryFeaturePoints(PXCMPoint3DF32[] points, Boolean returnActive, Boolean returnInactive)
    {
        return QueryFeaturePointsINT(instance, (points == null) ? 0 : points.Length, points, returnActive, returnInactive);
    }

    /// <summary>
    ///	Aligns a loaded 3D map to the specified marker. Alignment defines the initial pose of the model relative to the axes
    ///	printed on the marker (+Z points up out of the page).  By default, the coordinate system pose (origin and rotation)
    ///	is in an undefined position with respect to the object. The placement of the marker specifies the (0,0,0) origin as
    ///	well as the alignment of the coordinate axes (initial rotation).
    ///
    ///	Alignment also enhances the returned pose coordinates to be in units of millimeters, instead of an undefined unit.
    ///	see also Stop3DMapAlignment, Is3dMapAlignmentComplete
    /// </summary>	
    /// <param name="markerID">Integer identifier for the marker (from the marker PDF)</param>
    /// <param name="markerSize">Size of the marker in millimeters</param>
    public pxcmStatus Start3DMapAlignment(Int32 markerID, Int32 markerSize)
    {
        return PXCMTrackerUtils_Start3DMapAlignment(instance, markerID, markerSize);
    }

    /// <summary>
    ///	Cancel the current 3D Map alignment operation before it is complete.  Any in-progress state will be lost.
    ///
    ///	see also Start3DMapAlignment, Is3DMapAlignmentComplete
    /// </summary>	
    public pxcmStatus Cancel3DMapAlignment()
    {
        return PXCMTrackerUtils_Cancel3DMapAlignment(instance);
    }

    /// <summary>
    ///	Returns TRUE if alignment is complete.  At that point the file may be saved with the new alignment values.
    ///
    ///	see also Start3DMapAlignment
    /// </summary>	
    public Boolean Is3DMapAlignmentComplete()
    {
        return PXCMTrackerUtils_Is3DMapAlignmentComplete(instance);
    }

    /// <summary>
    ///	Start the camera calibration process. Calibration can improve the tracking results by compensating for
    ///	camera distortion and other intrinsic camera values.  A successful calibration requires several frames,
    ///	with the marker in different orientations and rotations relative to the camera.
    ///	
    ///	see also QueryCalibrationProgress, SaveCameraParameters
    /// </summary>
    /// <param name="markerID">Integer identifier for the marker (from the marker PDF file)</param>
    /// <param name="markerSize">Size of the printed marker in millimeters</param>
    public pxcmStatus StartCameraCalibration(Int32 markerID, Int32 markerSize)
    {
        return PXCMTrackerUtils_StartCameraCalibration(instance, markerID, markerSize);
    }

    /// <summary>
    ///	Stop the camera calibration process before it is complete.  No new calibration parameters may be saved.	
    ///
    ///	see also StartCameraCalibration, QueryCalibrationProgress
    /// </summary>	
    public pxcmStatus CancelCameraCalibration()
    {
        return PXCMTrackerUtils_CancelCameraCalibration(instance);
    }

    /// <summary>
    ///	Return the calibration progress as a percentage (0 - 100%). Calibration requires several different views of the
    ///	marker to produce an accurate result, this function returns the relative progress.  A calibration file may be saved
    ///	before this function returns 100% but the quality will be degraded.
    ///
    ///	If calibration has not been started this function returns a negative value
    ///
    ///	see also StartCameraCalibration
    /// </summary>
    public Int32 QueryCalibrationProgress()
    {
        return PXCMTrackerUtils_QueryCalibrationProgress(instance);
    }

    /// <summary>
    ///	Save the current camera intrinsic parameters to an XML file.
    ///	see also SetCameraParameters, StartCameraCalibration	
    /// </summary>
    /// <param name="filename">Filename to save the XML camera parameters in</param>
    public pxcmStatus SaveCameraParametersToFile(String filename)
    {
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
