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

    /**
	@class PXCMHandConfiguration
	@brief Handles all the configuration options of the hand module.
	Use this interface to configure the tracking, alerts, gestures and output options.
	@note Updated configuration is applied only when ApplyChanges is called.
*/

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

        /**
            @brief Apply the configuration changes to the module.
            This method must be called in order to apply the current configuration changes.
            @return PXCM_STATUS_NO_ERROR - operation succeeded.
            @return PXCM_STATUS_DATA_NOT_INITIALIZED - configuration was not initialized.\n                        
        */
        public pxcmStatus ApplyChanges()
        {
            return PXCMHandConfiguration_ApplyChanges(instance);
        }

        /**  
             @brief Restore configuration settings to the default values.
             @return PXCM_STATUS_NO_ERROR - operation succeeded.
             @return PXCM_STATUS_DATA_NOT_INITIALIZED - configuration was not initialized.\n                        
         */
        public pxcmStatus RestoreDefaults()
        {
            return PXCMHandConfiguration_RestoreDefaults(instance);
        }

        /**
            @brief Read current configuration settings from the module into this object.
            @return PXCM_STATUS_NO_ERROR - operation succeeded.
            @return PXCM_STATUS_DATA_NOT_INITIALIZED - configuration was not read.\n                        
        */
        public pxcmStatus Update()
        {
            return PXCMHandConfiguration_Update(instance);
        }

        /* Tracking Configuration */

        /** 
            @brief Restart the tracking process and reset all the skeleton information. 
            You might want to call this method, for example, when transitioning from one game level to another, \n
            in order to discard information that is not relevant to the new stage.
		
            @note ResetTracking will be executed only when processing the next frame.
		
            @return PXCM_STATUS_NO_ERROR - operation succeeded.
            @return PXCM_STATUS_PROCESS_FAILED - there was a module failure during processing.
            @return PXCM_STATUS_DATA_NOT_INITIALIZED - the module was not initialized
         */
        public pxcmStatus ResetTracking()
        {
            return PXCMHandConfiguration_ResetTracking(instance);
        }

        /**
             @brief Specify the name of the current user for personalization.
             The user name will be used to save and retrieve specific measurements (calibration) for this user.
             @param[in] userName - the name of the current user.
             @return PXCM_STATUS_NO_ERROR - operation succeeded.
             @return PXCM_STATUS_PARAM_UNSUPPORTED - illegal user name(e.g. an empty string)
         */
        public pxcmStatus SetUserName(String userName)
        {
            return PXCMHandConfiguration_SetUserName(instance, userName);
        }

        /**
             @brief Get the name of the current user.
             @return A null-terminated string containing the user's name.	
         */
        public String QueryUserName()
        {
            return QueryUserNameINT(instance);
        }

        /**
             @brief Activate calculation of the speed of a specific joint, according to the given mode.\n
		
             The output speed is a 3-dimensional vector, containing the the motion of the requested joint in each direction (x, y and z axis).\n
             By default, the joint speed calculation is disabled for all joints, in order to conserve CPU and memory resources.\n
             Typically the feature is only activated for a single fingertip or palm-center joint, as only the overall hand speed is useful.\n
		
             @param[in] jointLabel - the identifier of the joint.
             @param[in] jointSpeed - the speed calculation method. Possible values are:\n
               JOINT_SPEED_AVERAGE  - calculate the average joint speed, over the time period defined in the "time" parameter.\n
               JOINT_SPEED_ABSOLUTE - calculate the average of the absolute-value joint speed, over the time period defined in the "time" parameter.\n
             @param[in] time - the period in milliseconds over which the average speed will be calculated (a value of 0 will return the current speed).
		
             @return PXCM_STATUS_NO_ERROR - operation succeeded. 
             @return PXCM_STATUS_PARAM_UNSUPPORTED - one of the arguments is invalid.
		
             @see PXCMHandData.JointType
             @see PXCMHandData.JointSpeedType
         */
        public pxcmStatus EnableJointSpeed(PXCMHandData.JointType jointLabel, PXCMHandData.JointSpeedType jointSpeed, Int32 time)
        {
            return PXCMHandConfiguration_EnableJointSpeed(instance, jointLabel, jointSpeed, time);
        }


        /**
            @brief Disable calculation of the speed of a specific joint.\n
            You may want to disable the feature when it is no longer needed, in order to conserve CPU and memory resources.\n
            @param[in] jointLabel - the identifier of the joint
		
            @return PXCM_STATUS_NO_ERROR - operation succeeded.
            @return PXCM_STATUS_PARAM_UNSUPPORTED - invalid joint label.
		
            @see PXCMHandData.JointType
        */
        public pxcmStatus DisableJointSpeed(PXCMHandData.JointType jointLabel)
        {
            return PXCMHandConfiguration_DisableJointSpeed(instance, jointLabel);
        }

        /**
            @brief Set the boundaries of the tracking area.
		
            The tracking boundaries create a frustum shape in which the hand is tracked.\n
            (A frustum is a truncated pyramid, with 4 side planes and two rectangular bases.)\n
            When the tracked hand reaches one of the boundaries (near, far, left, right, top, or bottom), the appropriate alert is fired.
		
            @param[in] nearTrackingDistance - nearest tracking distance (distance of small frustum base from sensor).
            @param[in] farTrackingDistance - farthest tracking distance (distance of large frustum base from sensor).
            @param[in] nearTrackingWidth - width of small frustum base.
            @param[in] nearTrackingHeight - height of small frustum base.
		
            @note The frustum base centers are directly opposite the sensor.\n

            @return PXCM_STATUS_NO_ERROR - operation succeeded. 
            @return PXCM_STATUS_PARAM_UNSUPPORTED - invalid argument.
		
            @see PXCMHandData.JointType
        */
        public pxcmStatus SetTrackingBounds(Single nearTrackingDistance, Single farTrackingDistance, Single nearTrackingWidth, Single nearTrackingHeight)
        {
            return PXCMHandConfiguration_SetTrackingBounds(instance, nearTrackingDistance, farTrackingDistance, nearTrackingWidth, nearTrackingHeight);
        }

        /**
        @brief Loading a calibration file for specific age.
        People of different ages have different hand sizes and usually there is a correlation between the two.
        Knowing in advance the age of the players can improve the tracking in most cases.
        Call this method to load a calibration file that matches specific age. 
        We support specific hand calibrations for ages 4-14. Ages above 14 will use the default calibration.
	
        @Note: The best practice is to let the players perform online calibration, or load specific calibration per user.
        @Note: If you call SetUserName with an existing user name it will override SetDefaultAge
        @see SetUserName
        @param[in] age the expected age of the players
        @return PXCM_STATUS_NO_ERROR - operation succeeded. 
        @return PXCM_STATUS_PARAM_UNSUPPORTED for illegal ages 
        */
        public pxcmStatus SetDefaultAge(Int32 age)
        {
            return PXCMHandConfiguration_SetDefaultAge(instance, age);
        }

        /**
            @brief Retrieve the current calibration default age value.
            @return The current default age value.

            @see SetDefaultAge
        */
        public Int32 QueryDefaultAge()
        {
            return PXCMHandConfiguration_QueryDefaultAge(instance);
        }

       


       


        /**
            @brief Get the values defining the tracking boundaries frustum.
		
            @param[out] nearTrackingDistance - nearest tracking distance (distance of small frustum base from sensor).
            @param[out] farTrackingDistance - farthest tracking distance (distance of large frustum base from sensor).
            @param[out] nearTrackingWidth - width of small frustum base.
            @param[out] nearTrackingHeight - height of small frustum base.
		
            @return PXCM_STATUS_NO_ERROR - operation succeeded.  
        */
        public pxcmStatus QueryTrackingBounds(out Single nearTrackingDistance, out Single farTrackingDistance, out Single nearTrackingWidth, out Single nearTrackingHeight)
        {
            nearTrackingDistance = 0;
            farTrackingDistance = 0;
            nearTrackingWidth = 0;
            nearTrackingHeight = 0;
            return PXCMHandConfiguration_QueryTrackingBounds(instance, out nearTrackingDistance, out farTrackingDistance, out nearTrackingWidth, out nearTrackingHeight);
        }


        /**
             @brief Set the tracking mode, which determines the algorithm that will be applied for tracking hands.
             @param[in] trackingMode - the tracking mode to be set. Possible values are:\n
               TRACKING_MODE_FULL_HAND   - track the entire hand skeleton.\n
               TRACKING_MODE_EXTREMITIES - track only the mask and the extremities of the hand (the points that confine the tracked hand).\n
		
             @return PXCM_STATUS_NO_ERROR - operation succeeded. 
		
             @see PXCMHandData.TrackingModeType
        */
        public pxcmStatus SetTrackingMode(PXCMHandData.TrackingModeType trackingMode)
        {
            return PXCMHandConfiguration_SetTrackingMode(instance, trackingMode);
        }

        /**
            @brief Retrieve the current tracking mode, which indicates the algorithm that should be applied for tracking hands.
            @return TrackingModeType
		
            @see SetTrackingMode
            @see PXCMHandData.TrackingModeType
        */
        public PXCMHandData.TrackingModeType QueryTrackingMode()
        {
            return PXCMHandConfiguration_QueryTrackingMode(instance);
        }


        /**
            @brief Sets the degree of hand motion smoothing.
            "Smoothing" is algorithm which overcomes local problems in tracking and produces smoother, more continuous tracking information.
		
            @param[in] smoothingValue - a float value between 0 (not smoothed) and 1 (maximal smoothing).
		
            @return PXCM_STATUS_NO_ERROR - operation succeeded. 
            @return PXCM_STATUS_PARAM_UNSUPPORTED - invalid smoothing value.
        */
        public pxcmStatus SetSmoothingValue(Single smoothingValue)
        {
            return PXCMHandConfiguration_SetSmoothingValue(instance, smoothingValue);
        }

        /**
            @brief Retrieve the current smoothing value.
            @return The current smoothing value.
		
            @see SetSmoothingValue
        */
        public Single QuerySmoothingValue()
        {
            return PXCMHandConfiguration_QuerySmoothingValue(instance);
        }

        /**
            @brief Enable or disable the hand stabilizer feature.\n
		
            Enabling this feature produces smoother tracking of the hand motion, ignoring small shifts and "jitters".\n
            (As a result, in some cases the tracking may be less sensitive to minor movements).
		
            @param[in] enableFlag - true to enable the hand stabilizer; false to disable it.
            @return PXCM_STATUS_NO_ERROR - operation succeeded. 
        */
        public pxcmStatus EnableStabilizer(Boolean enableFlag)
        {
            return PXCMHandConfiguration_EnableStabilizer(instance, enableFlag);
        }

        /**
        @brief Return hand stabilizer activation status.
        @return true if hand stabilizer is enabled, false otherwise.
        */
        public Boolean IsStabilizerEnabled()
        {
            return PXCMHandConfiguration_IsStabilizerEnabled(instance);
        }


        /**
            @brief Enable the calculation of a normalized skeleton.\n
            Calculating the normalized skeleton transforms the tracked hand positions to those of a fixed-size skeleton.\n
            The positions of the normalized skeleton's joints can be retrieved by calling IHand::QueryNormalizedJoint.\n
            It is recommended to work with a normalized skeleton so that you can use the same code to identify poses and gestures,\n
            regardless of the hand size. (E.g. the same code can work for a child's hand and for an adult's hand.)

            @param[in] enableFlag - true if the normalized skeleton should be calculated, otherwise false.
            @return PXCM_STATUS_NO_ERROR - operation succeeded. 
		
            @see PXCMHandData.IHand.QueryNormalizedJoint
        */
        public pxcmStatus EnableNormalizedJoints(Boolean enableFlag)
        {
            return PXCMHandConfiguration_EnableNormalizedJoints(instance, enableFlag);
        }

        /**
         @brief Retrieve normalized joints calculation status.
         @return true if normalized joints calculation is enabled, false otherwise.
        */
        public Boolean IsNormalizedJointsEnabled()
        {
            return PXCMHandConfiguration_IsNormalizedJointsEnabled(instance);
        }


        /**
         @brief Enable calculation of the hand segmentation image.
         The hand segmentation image is an image mask of the tracked hand, where the hand pixels are white and all other pixels are black.
         @param[in] enableFlag - true if the segmentation image should be calculated, false otherwise.
         @return PXCM_STATUS_NO_ERROR - operation succeeded. 
        */
        public pxcmStatus EnableSegmentationImage(Boolean enableFlag)
        {
            return PXCMHandConfiguration_EnableSegmentationImage(instance, enableFlag);
        }

        /**
          @brief Retrieve the hand segmentation image calculation status.
          @return true if calculation of the hand segmentation image is enabled, false otherwise.
          @see EnableSegmentationImage
         */
        public Boolean IsSegmentationImageEnabled()
        {
            return PXCMHandConfiguration_IsSegmentationImageEnabled(instance);
        }

        /**
         @brief Enable the retrieval of tracked joints information.
         Enable joint tracking if your application uses specific joint positions; otherwise disable in order to conserve CPU/memory resources.\n
         @note This option doesn't affect the quality of the tracking, but only the availability of the joints info.
         @param[in] enableFlag - true to enable joint tracking, false to disable it.
         @return PXCM_STATUS_NO_ERROR - operation was successful
        */
        public pxcmStatus EnableTrackedJoints(Boolean enableFlag)
        {
            return PXCMHandConfiguration_EnableTrackedJoints(instance, enableFlag);
        }

        /**
          @brief Retrieve the joint tracking status.
          @return true if joint tracking is enabled, false otherwise.
         */
        public Boolean IsTrackedJointsEnabled()
        {
            return PXCMHandConfiguration_IsTrackedJointsEnabled(instance);
        }


        /* Alerts Configuration */

        /** 
            @brief Enable alert messaging for a specific event.            
            @param[in] alertEvent - the ID of the event to be enabled.
		
            @return PXCM_STATUS_NO_ERROR - operation succeeded. 
            @return PXCM_STATUS_PARAM_UNSUPPORTED - invalid alert type.
		
            @see PXCMHandData.AlertType
        */
        public pxcmStatus EnableAlert(PXCMHandData.AlertType alertEvent)
        {
            return PXCMHandConfiguration_EnableAlert(instance, alertEvent);
        }

        /** 
            @brief Enable all alert messaging events.            
            @return PXCM_STATUS_NO_ERROR - operation succeeded. 
        */
        public pxcmStatus EnableAllAlerts()
        {
            return PXCMHandConfiguration_EnableAllAlerts(instance);
        }

        /** 
            @brief Test the activation status of the given alert.
            @param[in] alertEvent - the ID of the event to be tested.
            @return true if the alert is enabled, false otherwise.
		
            @see PXCMHandData.AlertType
        */
        public Boolean IsAlertEnabled(PXCMHandData.AlertType alertEvent)
        {
            return PXCMHandConfiguration_IsAlertEnabled(instance, alertEvent);
        }

        /** 
            @brief Disable alert messaging for a specific event.            
            @param[in] alertEvent - the ID of the event to be disabled
		
            @return PXCM_STATUS_NO_ERROR - operation succeeded. 
            @return PXCM_STATUS_PARAM_UNSUPPORTED - unsupported parameter.
            @return PXCM_STATUS_DATA_NOT_INITIALIZED - data was not initialized.
		
            @see PXCMHandData.AlertType
        */
        public pxcmStatus DisableAlert(PXCMHandData.AlertType alertEvent)
        {
            return PXCMHandConfiguration_DisableAlert(instance, alertEvent);
        }

        /** 
            @brief Disable messaging for all alerts.                        
            @return PXCM_STATUS_NO_ERROR - operation succeeded. 
            @return PXCM_STATUS_DATA_NOT_INITIALIZED - data was not initialized.
        */
        public pxcmStatus DisableAllAlerts()
        {
            return PXCMHandConfiguration_DisableAllAlerts(instance);
        }

        /** 
             @brief Register an event handler object for the alerts. 
             The event handler's OnFiredAlert method is called each time an alert fires.
             @param[in] alertHandler - a pointer to the event handler.
		
             @return PXCM_STATUS_NO_ERROR - operation succeeded. 
             @return PXCM_STATUS_PARAM_UNSUPPORTED - null alertHandler pointer.
        	
         */
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

        /** 
             @brief Unsubscribe an alert handler object.
             @param[in] alertHandler - a pointer to the event handler to unsubscribe.
             @return PXCM_STATUS_NO_ERROR - operation succeeded. 
             @return PXCM_STATUS_PARAM_UNSUPPORTED - illegal alertHandler (null pointer).
         */
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

        /** 
            @brief Load a set of gestures from a specified path. 
            A gesture pack is a collection of pre-trained gestures.\n
            After this call, the gestures that are contained in the pack are available for identification.\n
            @param[in] gesturePackPath - the full path of the gesture pack location.
            @return PXCM_STATUS_NO_ERROR - operation succeeded. 
            @return PXCM_STATUS_PARAM_UNSUPPORTED - empty path or empty list of gestures.
            @note This method should be used only for external gesture packs, and not for the default gesture pack, which is loaded automatically.
        */
        public pxcmStatus LoadGesturePack(String gesturePackPath)
        {
            return PXCMHandConfiguration_LoadGesturePack(instance, gesturePackPath);
        }

        /** 
           @brief Unload the set of gestures contained in the specified path.
           @param[in] gesturePackPath - the full path of the the gesture pack location.
           @return PXC_STATUS_NO_ERROR - operation succeeded.   
       */
        public pxcmStatus UnloadGesturePack(String gesturePackPath)
        {
            return PXCMHandConfiguration_UnloadGesturePack(instance, gesturePackPath);
        }


        /** 
            @brief Unload all the currently loaded sets of the gestures.\n
            If you are using multiple gesture packs, you may want to load only the packs that are relevant to a particular stage in your application\n
            and unload all others. This can boost the accuracy of gesture recognition, and conserves system resources.
            @return PXCM_STATUS_NO_ERROR - operation succeeded. 
        */
        public pxcmStatus UnloadAllGesturesPacks()
        {
            return PXCMHandConfiguration_UnloadAllGesturesPacks(instance);
        }

        /**
             @brief Retrieve the total number of available gestures that were loaded from all gesture packs.
             @return The total number of loaded gestures.
         */
        public Int32 QueryGesturesTotalNumber()
        {
            return PXCMHandConfiguration_QueryGesturesTotalNumber(instance);
        }

        /** 
             @brief Retrieve the gesture name that matches the given index.
		
             @param[in] index - the index of the gesture whose name you want to retrieve.
             @param[in] bufferSize - the size of the preallocated gestureName buffer.						
             @param[out] gestureName - preallocated buffer to be filled with the gesture name.
		
             @return PXCM_STATUS_NO_ERROR - operation succeeded.  
             @return PXCM_STATUS_ITEM_UNAVAILABLE - no gesture for the given index value.
         */
        public pxcmStatus QueryGestureNameByIndex(Int32 index, out String gestureName)
        {
            StringBuilder sb = new StringBuilder(PXCMHandData.MAX_NAME_SIZE);
            pxcmStatus sts = PXCMHandConfiguration_QueryGestureNameByIndex(instance, index, sb.Capacity + 1, sb);
            gestureName = sb.ToString();
            return sts;
        }

        /** 
            @brief Enable a gesture, so that events are fired when the gesture is identified.
            @param[in] gestureName - the name of the gesture to be enabled. 
            @param[in] continuousGesture - set to "true" to get an "in progress" event at every frame for which the gesture is active, or "false" to get only "start" and "end" states of the gesture.
		
            @return PXCM_STATUS_NO_ERROR - operation succeeded. 
            @return PXCM_STATUS_PARAM_UNSUPPORTED - invalid parameter.
        */
        public pxcmStatus EnableGesture(String gestureName, Boolean continuousGesture)
        {
            return PXCMHandConfiguration_EnableGesture(instance, gestureName, continuousGesture);
        }

        /** 
            @brief Enable a gesture, so that events are fired when the gesture is identified.
            @param[in] gestureName - the name of the gesture to be enabled. 
		
            @return PXCM_STATUS_NO_ERROR - operation succeeded. 
            @return PXCM_STATUS_PARAM_UNSUPPORTED - invalid parameter.
        */
        public pxcmStatus EnableGesture(String gestureName)
        {
            return EnableGesture(gestureName, false);
        }

        /** 
             @brief Enable all gestures, so that events are fired for every gesture identified.		
             @param[in] continuousGesture - set to "true" to get an "in progress" event at every frame for which the gesture is active, or "false" to get only "start" and "end" states of the gesture.
             @return PXCM_STATUS_NO_ERROR - operation succeeded.   
         */
        public pxcmStatus EnableAllGestures(Boolean continuousGesture)
        {
            return PXCMHandConfiguration_EnableAllGestures(instance, continuousGesture);
        }

        /** 
             @brief Enable all gestures, so that events are fired for every gesture identified.		
             @return PXCM_STATUS_NO_ERROR - operation succeeded.   
         */
        public pxcmStatus EnableAllGestures()
        {
            return EnableAllGestures(false);
        }

        /** 
             @brief Check whether a gesture is enabled.
             @param[in] gestureName - the name of the gesture to be tested.
             @return true if the gesture is enabled, false otherwise.
         */
        public Boolean IsGestureEnabled(String gestureName)
        {
            return PXCMHandConfiguration_IsGestureEnabled(instance, gestureName);
        }

        /** 
            @brief Deactivate identification of a gesture. Events will no longer be fired for this gesture.            
            @param[in] gestureName - the name of the gesture to deactivate.            
            @return PXC_STATUS_NO_ERROR - operation succeeded. 
            PXCM_STATUS_PARAM_UNSUPPORTED - invalid gesture name.
        */
        public pxcmStatus DisableGesture(String gestureName)
        {
            return PXCMHandConfiguration_DisableGesture(instance, gestureName);
        }


        /** 
            @brief Deactivate identification of all gestures. Events will no longer be fired for any gesture.            
            @return PXCM_STATUS_NO_ERROR - operation succeeded.  
        */
        public pxcmStatus DisableAllGestures()
        {
            return PXCMHandConfiguration_DisableAllGestures(instance);
        }

        /** 
        @brief Register an event handler object to be called on gesture events.
        The event handler's OnFiredGesture method will be called each time a gesture is identified.
		
        @param[in] gestureHandler - a pointer to the gesture handler.
		
        @return PXCM_STATUS_NO_ERROR - operation succeeded. 
        @return PXCM_STATUS_PARAM_UNSUPPORTED - null gesture handler.

    
    */
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

        /** 
            @brief Unsubscribe a gesture event handler object.
            After this call no callback events will be sent to the given gestureHandler.
            @param[in] gestureHandler - a pointer to the event handler to unsubscribe.
            @return PXCM_STATUS_NO_ERROR - operation succeeded. 
            @return PXCM_STATUS_PARAM_UNSUPPORTED - null gesture handler.     
        */
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