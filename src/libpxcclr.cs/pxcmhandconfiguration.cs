/*******************************************************************************

  INTEL CORPORATION PROPRIETARY INFORMATION
  This software is supplied under the terms of a license agreement or nondisclosure
  agreement with Intel Corporation and may not be copied or disclosed except in
  accordance with the terms of that agreement
  Copyright(c) 2013-2014 Intel Corporation. All Rights Reserved.

 *******************************************************************************/
using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections.Generic;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

/// <summary>
/// @class PXCMHandConfiguration
/// Handles all the configuration options of the hand module.
/// Use this interface to configure the tracking, alerts, gestures and output options.
/// @note Updated configuration is applied only when ApplyChanges is called.
/// </summary>

public partial class PXCMHandConfiguration : PXCMBase
{
    new public const Int32 CUID = 0x47434148;

    public delegate void OnFiredAlertDelegate(PXCMHandData.AlertData alertData);
    public delegate void OnFiredGestureDelegate(PXCMHandData.GestureData gestureData);

    internal class EventMaps
    {
        internal Dictionary<PXCMHandConfiguration.OnFiredAlertDelegate, Object> alert = new Dictionary<PXCMHandConfiguration.OnFiredAlertDelegate, Object>();
        internal Dictionary<PXCMHandConfiguration.OnFiredGestureDelegate, Object> gesture = new Dictionary<PXCMHandConfiguration.OnFiredGestureDelegate, Object>();
        internal Object cs = new Object();
    };

    /* General */

    /// <summary>
    /// Apply the configuration changes to the module.
    /// This method must be called in order to apply the current configuration changes.
    /// </summary>
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded.</returns>
    /// <returns> PXCM_STATUS_DATA_NOT_INITIALIZED - configuration was not initialized.\n                        </returns>
    public pxcmStatus ApplyChanges()
    {
        return PXCMHandConfiguration_ApplyChanges(instance);
    }

    /// <summary>  
    /// Restore configuration settings to the default values.
    /// </summary>
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded.</returns>
    /// <returns> PXCM_STATUS_DATA_NOT_INITIALIZED - configuration was not initialized.\n                        </returns>
    public pxcmStatus RestoreDefaults()
    {
        return PXCMHandConfiguration_RestoreDefaults(instance);
    }

    /// <summary>
    /// Read current configuration settings from the module into this object.
    /// </summary>
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded.</returns>
    /// <returns> PXCM_STATUS_DATA_NOT_INITIALIZED - configuration was not read.\n                        </returns>
    public pxcmStatus Update()
    {
        return PXCMHandConfiguration_Update(instance);
    }

    /* Tracking Configuration */

    /// <summary> 
    /// Restart the tracking process and reset all the skeleton information. 
    /// You might want to call this method, for example, when transitioning from one game level to another, \n
    /// in order to discard information that is not relevant to the new stage.
    /// 
    /// @note ResetTracking will be executed only when processing the next frame.
    /// </summary>
    /// 
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded.</returns>
    /// <returns> PXCM_STATUS_PROCESS_FAILED - there was a module failure during processing.</returns>
    /// <returns> PXCM_STATUS_DATA_NOT_INITIALIZED - the module was not initialized</returns>
    public pxcmStatus ResetTracking()
    {
        return PXCMHandConfiguration_ResetTracking(instance);
    }

    /// <summary>
    /// Specify the name of the current user for personalization.
    /// The user name will be used to save and retrieve specific measurements (calibration) for this user.
    /// </summary>
    /// <param name="userName"> - the name of the current user.</param>
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded.</returns>
    /// <returns> PXCM_STATUS_PARAM_UNSUPPORTED - illegal user name(e.g. an empty string)  or tracking mode is set to TRACKING_MODE_EXTREMITIES.</returns>
    public pxcmStatus SetUserName(String userName)
    {
        return PXCMHandConfiguration_SetUserName(instance, userName);
    }

    /// <summary>
    /// Get the name of the current user.
    /// </summary>
    /// <returns> A null-terminated string containing the user's name.	</returns>
    public String QueryUserName()
    {
        return QueryUserNameINT(instance);
    }

    /// <summary>
    /// Activate calculation of the speed of a specific joint, according to the given mode.\n
    /// 
    /// The output speed is a 3-dimensional vector, containing the the motion of the requested joint in each direction (x, y and z axis).\n
    /// By default, the joint speed calculation is disabled for all joints, in order to conserve CPU and memory resources.\n
    /// Typically the feature is only activated for a single fingertip or palm-center joint, as only the overall hand speed is useful.\n
    /// 
    /// </summary>
    /// <param name="jointLabel"> - the identifier of the joint.</param>
    /// <param name="jointSpeed"> - the speed calculation method. Possible values are:\n</param>
    /// JOINT_SPEED_AVERAGE  - calculate the average joint speed, over the time period defined in the "time" parameter.\n
    /// JOINT_SPEED_ABSOLUTE - calculate the average of the absolute-value joint speed, over the time period defined in the "time" parameter.\n
    /// <param name="time"> - the period in milliseconds over which the average speed will be calculated (a value of 0 will return the current speed).</param>
    /// 
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded. </returns>
    /// <returns> PXCM_STATUS_PARAM_UNSUPPORTED - one of the arguments is invalid  or tracking mode is set to TRACKING_MODE_EXTREMITIES.</returns>
    /// 
    /// @see PXCMHandData.JointType
    /// @see PXCMHandData.JointSpeedType
    public pxcmStatus EnableJointSpeed(PXCMHandData.JointType jointLabel, PXCMHandData.JointSpeedType jointSpeed, Int32 time)
    {
        return PXCMHandConfiguration_EnableJointSpeed(instance, jointLabel, jointSpeed, time);
    }


    /// <summary>
    /// Disable calculation of the speed of a specific joint.\n
    /// You may want to disable the feature when it is no longer needed, in order to conserve CPU and memory resources.\n
    /// </summary>
    /// <param name="jointLabel"> - the identifier of the joint</param>
    /// 
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded.</returns>
    /// <returns> PXCM_STATUS_PARAM_UNSUPPORTED - invalid joint label  or tracking mode is set to TRACKING_MODE_EXTREMITIES.</returns>
    /// 
    /// @see PXCMHandData.JointType
    public pxcmStatus DisableJointSpeed(PXCMHandData.JointType jointLabel)
    {
        return PXCMHandConfiguration_DisableJointSpeed(instance, jointLabel);
    }

    /// <summary>
    /// Set the boundaries of the tracking area.
    /// 
    /// The tracking boundaries create a frustum shape in which the hand is tracked.\n
    /// (A frustum is a truncated pyramid, with 4 side planes and two rectangular bases.)\n
    /// When the tracked hand reaches one of the boundaries (near, far, left, right, top, or bottom), the appropriate alert is fired.
    /// </summary>
    /// 
    /// <param name="nearTrackingDistance"> - nearest tracking distance (distance of small frustum base from sensor).</param>
    /// <param name="farTrackingDistance"> - farthest tracking distance (distance of large frustum base from sensor).</param>
    /// <param name="nearTrackingWidth"> - width of small frustum base.</param>
    /// <param name="nearTrackingHeight"> - height of small frustum base.</param>
    /// 
    /// @note The frustum base centers are directly opposite the sensor.\n
    /// 
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded. </returns>
    /// <returns> PXCM_STATUS_PARAM_UNSUPPORTED - invalid argument.</returns>
    /// 
    /// @see PXCMHandData.JointType
    public pxcmStatus SetTrackingBounds(Single nearTrackingDistance, Single farTrackingDistance, Single nearTrackingWidth, Single nearTrackingHeight)
    {
        return PXCMHandConfiguration_SetTrackingBounds(instance, nearTrackingDistance, farTrackingDistance, nearTrackingWidth, nearTrackingHeight);
    }

    /// <summary>
    /// Loading a calibration file for specific age.
    /// People of different ages have different hand sizes and usually there is a correlation between the two.
    /// Knowing in advance the age of the players can improve the tracking in most cases.
    /// Call this method to load a calibration file that matches specific age. 
    /// We support specific hand calibrations for ages 4-14. Ages above 14 will use the default calibration.
    /// 
    /// @Note: The best practice is to let the players perform online calibration, or load specific calibration per user.
    /// @Note: If you call SetUserName with an existing user name it will override SetDefaultAge
    /// @see SetUserName
    /// </summary>
    /// <param name="age"> the expected age of the players</param>
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded. </returns>
    /// <returns> PXCM_STATUS_PARAM_UNSUPPORTED for illegal ages  or tracking mode is set to TRACKING_MODE_EXTREMITIES.</returns>
    public pxcmStatus SetDefaultAge(Int32 age)
    {
        return PXCMHandConfiguration_SetDefaultAge(instance, age);
    }

    /// <summary>
    /// Retrieve the current calibration default age value.
    /// </summary>
    /// <returns> The current default age value.</returns>
    /// 
    /// @see SetDefaultAge
    public Int32 QueryDefaultAge()
    {
        return PXCMHandConfiguration_QueryDefaultAge(instance);
    }


    /// <summary>
    /// Get the values defining the tracking boundaries frustum.
    /// 
    /// </summary>
    /// <param name="nearTrackingDistance"> - nearest tracking distance (distance of small frustum base from sensor).</param>
    /// <param name="farTrackingDistance"> - farthest tracking distance (distance of large frustum base from sensor).</param>
    /// <param name="nearTrackingWidth"> - width of small frustum base.</param>
    /// <param name="nearTrackingHeight"> - height of small frustum base.</param>
    /// 
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded.  </returns>
    public pxcmStatus QueryTrackingBounds(out Single nearTrackingDistance, out Single farTrackingDistance, out Single nearTrackingWidth, out Single nearTrackingHeight)
    {
        nearTrackingDistance = 0;
        farTrackingDistance = 0;
        nearTrackingWidth = 0;
        nearTrackingHeight = 0;
        return PXCMHandConfiguration_QueryTrackingBounds(instance, out nearTrackingDistance, out farTrackingDistance, out nearTrackingWidth, out nearTrackingHeight);
    }


    /// <summary>
    /// Set the tracking mode, which determines the algorithm that will be applied for tracking hands.
    /// </summary>
    /// <param name="trackingMode"> - the tracking mode to be set. Possible values are:\n</param>
    /// TRACKING_MODE_FULL_HAND   - track the entire hand skeleton.\n
    /// TRACKING_MODE_EXTREMITIES - track only the mask and the extremities of the hand (the points that confine the tracked hand).\n
    /// 
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded. </returns>
    /// <returns> PXC_STATUS_PARAM_UNSUPPORTED - TrackingModeType is invalid.       </returns>
    /// @see PXCMHandData.TrackingModeType
    public pxcmStatus SetTrackingMode(PXCMHandData.TrackingModeType trackingMode)
    {
        return PXCMHandConfiguration_SetTrackingMode(instance, trackingMode);
    }

    /// <summary>
    /// Retrieve the current tracking mode, which indicates the algorithm that should be applied for tracking hands.
    /// </summary>
    /// <returns> TrackingModeType</returns>
    /// 
    /// @see SetTrackingMode
    /// @see PXCMHandData.TrackingModeType
    public PXCMHandData.TrackingModeType QueryTrackingMode()
    {
        return PXCMHandConfiguration_QueryTrackingMode(instance);
    }


    /// <summary>
    /// Sets the degree of hand motion smoothing.
    /// "Smoothing" is algorithm which overcomes local problems in tracking and produces smoother, more continuous tracking information.
    /// </summary>
    /// <param name="smoothingValue"> - a float value between 0 (not smoothed) and 1 (maximal smoothing).</param>
    /// 
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded. </returns>
    /// <returns> PXCM_STATUS_PARAM_UNSUPPORTED - invalid smoothing value  or tracking mode is set to TRACKING_MODE_EXTREMITIES.</returns>
    public pxcmStatus SetSmoothingValue(Single smoothingValue)
    {
        return PXCMHandConfiguration_SetSmoothingValue(instance, smoothingValue);
    }

    /// <summary>
    /// Retrieve the current smoothing value.
    /// </summary>
    /// <returns> The current smoothing value.</returns>
    /// 
    /// @see SetSmoothingValue
    public Single QuerySmoothingValue()
    {
        return PXCMHandConfiguration_QuerySmoothingValue(instance);
    }

    /// <summary>
    /// Enable or disable the hand stabilizer feature.\n
    /// 
    /// Enabling this feature produces smoother tracking of the hand motion, ignoring small shifts and "jitters".\n
    /// (As a result, in some cases the tracking may be less sensitive to minor movements).
    /// </summary>
    /// 
    /// <param name="enableFlag"> - true to enable the hand stabilizer; false to disable it.</param>
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded. </returns>
    /// <returns> PXC_STATUS_PARAM_UNSUPPORTED - tracking mode is set to TRACKING_MODE_EXTREMITIES.</returns>
    public pxcmStatus EnableStabilizer(Boolean enableFlag)
    {
        return PXCMHandConfiguration_EnableStabilizer(instance, enableFlag);
    }

    /// <summary>
    /// Return hand stabilizer activation status.
    /// </summary>
    /// <returns> true if hand stabilizer is enabled, false otherwise.</returns>
    public Boolean IsStabilizerEnabled()
    {
        return PXCMHandConfiguration_IsStabilizerEnabled(instance);
    }


    /// <summary>
    /// Enable the calculation of a normalized skeleton.\n
    /// Calculating the normalized skeleton transforms the tracked hand positions to those of a fixed-size skeleton.\n
    /// The positions of the normalized skeleton's joints can be retrieved by calling IHand::QueryNormalizedJoint.\n
    /// It is recommended to work with a normalized skeleton so that you can use the same code to identify poses and gestures,\n
    /// regardless of the hand size. (E.g. the same code can work for a child's hand and for an adult's hand.)
    /// 
    /// </summary>
    /// <param name="enableFlag"> - true if the normalized skeleton should be calculated, otherwise false.</param>
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded. </returns>
    /// <returns> PXC_STATUS_PARAM_UNSUPPORTED - tracking mode is set to TRACKING_MODE_EXTREMITIES.</returns>
    /// @see PXCMHandData.IHand.QueryNormalizedJoint
    public pxcmStatus EnableNormalizedJoints(Boolean enableFlag)
    {
        return PXCMHandConfiguration_EnableNormalizedJoints(instance, enableFlag);
    }

    /// <summary>
    /// Retrieve normalized joints calculation status.
    /// </summary>
    /// <returns> true if normalized joints calculation is enabled, false otherwise.</returns>
    public Boolean IsNormalizedJointsEnabled()
    {
        return PXCMHandConfiguration_IsNormalizedJointsEnabled(instance);
    }


    /// <summary>
    /// Enable calculation of the hand segmentation image.
    /// The hand segmentation image is an image mask of the tracked hand, where the hand pixels are white and all other pixels are black.
    /// </summary>
    /// <param name="enableFlag"> - true if the segmentation image should be calculated, false otherwise.</param>
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded.          </returns>
    public pxcmStatus EnableSegmentationImage(Boolean enableFlag)
    {
        return PXCMHandConfiguration_EnableSegmentationImage(instance, enableFlag);
    }

    /// <summary>
    /// Retrieve the hand segmentation image calculation status.
    /// </summary>
    /// <returns> true if calculation of the hand segmentation image is enabled, false otherwise.</returns>
    /// @see EnableSegmentationImage
    public Boolean IsSegmentationImageEnabled()
    {
        return PXCMHandConfiguration_IsSegmentationImageEnabled(instance);
    }

    /// <summary>
    /// Enable the retrieval of tracked joints information.
    /// Enable joint tracking if your application uses specific joint positions; otherwise disable in order to conserve CPU/memory resources.\n
    /// @note This option doesn't affect the quality of the tracking, but only the availability of the joints info.
    /// </summary>
    /// <param name="enableFlag"> - true to enable joint tracking, false to disable it.</param>
    /// <returns> PXCM_STATUS_NO_ERROR - operation was successful</returns>
    /// <returns> PXC_STATUS_PARAM_UNSUPPORTED - tracking mode is set to TRACKING_MODE_EXTREMITIES.</returns>
    public pxcmStatus EnableTrackedJoints(Boolean enableFlag)
    {
        return PXCMHandConfiguration_EnableTrackedJoints(instance, enableFlag);
    }

    /// <summary>
    /// Retrieve the joint tracking status.
    /// </summary>
    /// <returns> true if joint tracking is enabled, false otherwise.</returns>
    public Boolean IsTrackedJointsEnabled()
    {
        return PXCMHandConfiguration_IsTrackedJointsEnabled(instance);
    }


    /* Alerts Configuration */

    /// <summary> 
    /// Enable alert messaging for a specific event.            
    /// </summary>
    /// <param name="alertEvent"> - the ID of the event to be enabled.</param>
    /// 
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded. </returns>
    /// <returns> PXCM_STATUS_PARAM_UNSUPPORTED - invalid alert type.</returns>
    /// 
    /// @see PXCMHandData.AlertType
    public pxcmStatus EnableAlert(PXCMHandData.AlertType alertEvent)
    {
        return PXCMHandConfiguration_EnableAlert(instance, alertEvent);
    }

    /// <summary> 
    /// Enable all alert messaging events.            
    /// </summary>
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded. </returns>
    public pxcmStatus EnableAllAlerts()
    {
        return PXCMHandConfiguration_EnableAllAlerts(instance);
    }

    /// <summary> 
    /// Test the activation status of the given alert.
    /// </summary>
    /// <param name="alertEvent"> - the ID of the event to be tested.</param>
    /// <returns> true if the alert is enabled, false otherwise.</returns>
    /// 
    /// @see PXCMHandData.AlertType
    public Boolean IsAlertEnabled(PXCMHandData.AlertType alertEvent)
    {
        return PXCMHandConfiguration_IsAlertEnabled(instance, alertEvent);
    }

    /// <summary> 
    /// Disable alert messaging for a specific event.            
    /// </summary>
    /// <param name="alertEvent"> - the ID of the event to be disabled</param>
    /// 
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded. </returns>
    /// <returns> PXCM_STATUS_PARAM_UNSUPPORTED - unsupported parameter.</returns>
    /// <returns> PXCM_STATUS_DATA_NOT_INITIALIZED - data was not initialized.</returns>
    /// 
    /// @see PXCMHandData.AlertType
    public pxcmStatus DisableAlert(PXCMHandData.AlertType alertEvent)
    {
        return PXCMHandConfiguration_DisableAlert(instance, alertEvent);
    }

    /// <summary> 
    /// Disable messaging for all alerts.                        
    /// </summary>
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded. </returns>
    /// <returns> PXCM_STATUS_DATA_NOT_INITIALIZED - data was not initialized.</returns>
    public pxcmStatus DisableAllAlerts()
    {
        return PXCMHandConfiguration_DisableAllAlerts(instance);
    }

    /// <summary> 
    /// Register an event handler object for the alerts. 
    /// The event handler's OnFiredAlert method is called each time an alert fires.
    /// </summary>
    /// <param name="alertHandler"> - a pointer to the event handler.</param>
    /// 
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded. </returns>
    /// <returns> PXCM_STATUS_PARAM_UNSUPPORTED - null alertHandler pointer.</returns>
    /// 
    public pxcmStatus SubscribeAlert(OnFiredAlertDelegate handler)
    {
        if (handler == null) return pxcmStatus.PXCM_STATUS_HANDLE_INVALID;

        Object proxy;
        pxcmStatus sts = SubscribeAlertINT(instance, handler, out proxy);
        if (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR)
        {
            lock (maps.cs)
            {
                maps.alert[handler] = proxy;
            }
        }
        return sts;
    }

    /// <summary> 
    /// Unsubscribe an alert handler object.
    /// </summary>
    /// <param name="alertHandler"> - a pointer to the event handler to unsubscribe.</param>
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded. </returns>
    /// <returns> PXCM_STATUS_PARAM_UNSUPPORTED - illegal alertHandler (null pointer).</returns>
    public pxcmStatus UnsubscribeAlert(OnFiredAlertDelegate handler)
    {
        if (handler == null) return pxcmStatus.PXCM_STATUS_HANDLE_INVALID;

        lock (maps.cs)
        {
            Object proxy;
            if (!maps.alert.TryGetValue(handler, out proxy))
                return pxcmStatus.PXCM_STATUS_HANDLE_INVALID;
            pxcmStatus sts = UnsubscribeAlertINT(instance, proxy);
            maps.alert.Remove(handler);
            return sts;
        }
    }

    /* Gestures Configuration */

    /// <summary> 
    /// Load a set of gestures from a specified path. 
    /// A gesture pack is a collection of pre-trained gestures.\n
    /// After this call, the gestures that are contained in the pack are available for identification.\n
    /// </summary>
    /// <param name="gesturePackPath"> - the full path of the gesture pack location.</param>
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded. </returns>
    /// <returns> PXCM_STATUS_PARAM_UNSUPPORTED - empty path or empty list of gestures.</returns>
    /// @note This method should be used only for external gesture packs, and not for the default gesture pack, which is loaded automatically.
    public pxcmStatus LoadGesturePack(String gesturePackPath)
    {
        return PXCMHandConfiguration_LoadGesturePack(instance, gesturePackPath);
    }

    /// <summary> 
    /// Unload the set of gestures contained in the specified path.
    /// </summary>
    /// <param name="gesturePackPath"> - the full path of the the gesture pack location.</param>
    /// <returns> PXC_STATUS_NO_ERROR - operation succeeded.   </returns>
    public pxcmStatus UnloadGesturePack(String gesturePackPath)
    {
        return PXCMHandConfiguration_UnloadGesturePack(instance, gesturePackPath);
    }


    /// <summary> 
    /// Unload all the currently loaded sets of the gestures.\n
    /// If you are using multiple gesture packs, you may want to load only the packs that are relevant to a particular stage in your application\n
    /// and unload all others. This can boost the accuracy of gesture recognition, and conserves system resources.
    /// </summary>
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded. </returns>
    public pxcmStatus UnloadAllGesturesPacks()
    {
        return PXCMHandConfiguration_UnloadAllGesturesPacks(instance);
    }

    /// <summary>
    /// Retrieve the total number of available gestures that were loaded from all gesture packs.
    /// </summary>
    /// <returns> The total number of loaded gestures.</returns>
    public Int32 QueryGesturesTotalNumber()
    {
        return PXCMHandConfiguration_QueryGesturesTotalNumber(instance);
    }

    /// <summary> 
    /// Retrieve the gesture name that matches the given index.
    /// </summary>
    /// <param name="index"> - the index of the gesture whose name you want to retrieve.</param>
    /// <param name="bufferSize"> - the size of the preallocated gestureName buffer.</param>
    /// <param name="gestureName"> - preallocated buffer to be filled with the gesture name.</param>
    /// 
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded.  </returns>
    /// <returns> PXCM_STATUS_ITEM_UNAVAILABLE - no gesture for the given index value.</returns>
    public pxcmStatus QueryGestureNameByIndex(Int32 index, out String gestureName)
    {
        StringBuilder sb = new StringBuilder(PXCMHandData.MAX_NAME_SIZE);
        pxcmStatus sts = PXCMHandConfiguration_QueryGestureNameByIndex(instance, index, sb.Capacity + 1, sb);
        gestureName = sb.ToString();
        return sts;
    }

    /// <summary> 
    /// Enable a gesture, so that events are fired when the gesture is identified.
    /// </summary>
    /// <param name="gestureName"> - the name of the gesture to be enabled.</param>
    /// <param name="continuousGesture"> - set to "true" to get an "in progress" event at every frame for which the gesture is active, or "false" to get only "start" and "end" states of the gesture.</param>
    /// 
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded. </returns>
    /// <returns> PXCM_STATUS_PARAM_UNSUPPORTED - invalid parameter.</returns>
    public pxcmStatus EnableGesture(String gestureName, Boolean continuousGesture)
    {
        return PXCMHandConfiguration_EnableGesture(instance, gestureName, continuousGesture);
    }

    /// <summary> 
    /// Enable a gesture, so that events are fired when the gesture is identified.
    /// </summary>
    /// <param name="gestureName"> - the name of the gesture to be enabled.</param>
    /// 
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded. </returns>
    /// <returns> PXCM_STATUS_PARAM_UNSUPPORTED - invalid parameter.</returns>
    public pxcmStatus EnableGesture(String gestureName)
    {
        return EnableGesture(gestureName, false);
    }

    /// <summary> 
    /// Enable all gestures, so that events are fired for every gesture identified.		
    /// </summary>
    /// <param name="continuousGesture"> - set to "true" to get an "in progress" event at every frame for which the gesture is active, or "false" to get only "start" and "end" states of the gesture.</param>
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded.   </returns>
    public pxcmStatus EnableAllGestures(Boolean continuousGesture)
    {
        return PXCMHandConfiguration_EnableAllGestures(instance, continuousGesture);
    }

    /// <summary> 
    /// Enable all gestures, so that events are fired for every gesture identified.		
    /// </summary>
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded.   </returns>
    public pxcmStatus EnableAllGestures()
    {
        return EnableAllGestures(false);
    }

    /// <summary> 
    /// Check whether a gesture is enabled.
    /// </summary>
    /// <param name="gestureName"> - the name of the gesture to be tested.</param>
    /// <returns> true if the gesture is enabled, false otherwise.</returns>
    public Boolean IsGestureEnabled(String gestureName)
    {
        return PXCMHandConfiguration_IsGestureEnabled(instance, gestureName);
    }

    /// <summary> 
    /// Deactivate identification of a gesture. Events will no longer be fired for this gesture.            
    /// </summary>
    /// <param name="gestureName"> - the name of the gesture to deactivate.</param>
    /// <returns> PXC_STATUS_NO_ERROR - operation succeeded. </returns>
    /// PXCM_STATUS_PARAM_UNSUPPORTED - invalid gesture name.
    public pxcmStatus DisableGesture(String gestureName)
    {
        return PXCMHandConfiguration_DisableGesture(instance, gestureName);
    }


    /// <summary> 
    /// Deactivate identification of all gestures. Events will no longer be fired for any gesture.            
    /// </summary>
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded.  </returns>
    public pxcmStatus DisableAllGestures()
    {
        return PXCMHandConfiguration_DisableAllGestures(instance);
    }

    /// <summary> 
    /// Register an event handler object to be called on gesture events.
    /// The event handler's OnFiredGesture method will be called each time a gesture is identified.
    /// </summary>
    /// <param name="gestureHandler"> - a pointer to the gesture handler.</param>
    /// 
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded. </returns>
    /// <returns> PXCM_STATUS_PARAM_UNSUPPORTED - null gesture handler.</returns> 
    public pxcmStatus SubscribeGesture(OnFiredGestureDelegate handler)
    {
        if (handler == null) return pxcmStatus.PXCM_STATUS_HANDLE_INVALID;

        Object proxy;
        pxcmStatus sts = SubscribeGestureINT(instance, handler, out proxy);
        if (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR)
        {
            lock (maps.cs)
            {
                maps.gesture[handler] = proxy;
            }
        }
        return sts;
    }

    /// <summary> 
    /// Unsubscribe a gesture event handler object.
    /// After this call no callback events will be sent to the given gestureHandler.
    /// </summary>
    /// <param name="gestureHandler"> - a pointer to the event handler to unsubscribe.</param>
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded. </returns>
    /// <returns> PXCM_STATUS_PARAM_UNSUPPORTED - null gesture handler.     </returns>
    public pxcmStatus UnsubscribeGesture(OnFiredGestureDelegate handler)
    {
        if (handler == null) return pxcmStatus.PXCM_STATUS_HANDLE_INVALID;

        lock (maps.cs)
        {
            Object proxy;
            if (!maps.gesture.TryGetValue(handler, out proxy))
                return pxcmStatus.PXCM_STATUS_HANDLE_INVALID;

            pxcmStatus sts = UnsubscribeGestureINT(instance, proxy);
            maps.gesture.Remove(handler);
            return sts;
        }
    }



    internal EventMaps maps = null;

    internal PXCMHandConfiguration(IntPtr instance, Boolean delete)
        : base(instance, delete)
    {
        maps = new EventMaps();
    }

    internal PXCMHandConfiguration(EventMaps maps, IntPtr instance, Boolean delete)
        : base(instance, delete)
    {
        this.maps = maps;
    }
};

#if RSSDK_IN_NAMESPACE
}
#endif
