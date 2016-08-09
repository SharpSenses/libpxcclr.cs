/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2013-2016 Intel Corporation. All Rights Reserved.

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
/// @class PXCMCursorConfiguration
/// @brief Handles all the configuration options of the hand cursor module.
/// Use this interface to configure the tracking, alerts, gestures and output options.
/// @note Updated configuration is applied only when ApplyChanges is called.
/// </summary>

public partial class PXCMCursorConfiguration : PXCMBase
{
    new public const Int32 CUID = 0x47434843;

    public delegate void OnFiredAlertDelegate(PXCMCursorData.AlertData alertData);
    public delegate void OnFiredGestureDelegate(PXCMCursorData.GestureData gestureData);

    internal class EventMaps
    {
        internal Dictionary<PXCMCursorConfiguration.OnFiredAlertDelegate, Object> alert = new Dictionary<PXCMCursorConfiguration.OnFiredAlertDelegate, Object>();
        internal Dictionary<PXCMCursorConfiguration.OnFiredGestureDelegate, Object> gesture = new Dictionary<PXCMCursorConfiguration.OnFiredGestureDelegate, Object>();
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
        return PXCMCursorConfiguration_ApplyChanges(instance);
    }

    /// <summary>  
    /// Restore configuration settings to the default values.
    /// </summary>
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded.</returns>
    /// <returns> PXCM_STATUS_DATA_NOT_INITIALIZED - configuration was not initialized.\n                        </returns>
    public pxcmStatus RestoreDefaults()
    {
        return PXCMCursorConfiguration_RestoreDefaults(instance);
    }

    /// <summary>
    /// Read current configuration settings from the module into this object.
    /// </summary>
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded.</returns>
    /// <returns> PXCM_STATUS_DATA_NOT_INITIALIZED - configuration was not read.\n                        </returns>
    public pxcmStatus Update()
    {
        return PXCMCursorConfiguration_Update(instance);
    }

    /* Tracking Configuration */


    /// <summary>
    ///  Set the boundaries of the tracking area.
    ///  The tracking boundaries create a frustum shape in which the hand is tracked.\n
    ///  (A frustum is a truncated pyramid, with 4 side planes and two rectangular bases.)\n
    ///  When the tracked hand reaches one of the boundaries (near, far, left, right, top, or bottom), the appropriate alert is fired.     
	///	 @param[in] trackingBounds - the struct that defines the tracking boundaries.
	///	 @note The frustum base center are directly opposite the sensor.\n    /// 
    /// </summary>
    /// <param name="trackingBounds"> - the struct that defines the tracking boundaries.</param>
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded. </returns>
    /// <returns> PXCM_STATUS_PARAM_UNSUPPORTED - invalid argument.</returns>
    /// 
    /// @see PXCMCursorData.TrackingBounds
    public pxcmStatus SetTrackingBounds(PXCMCursorData.TrackingBounds trackingBounds)
    {
        return PXCMCursorConfiguration_SetTrackingBounds(instance, trackingBounds);
    }

    /// <summary>
    /// Get the values defining the tracking boundaries frustum.
    /// </summary>
    /// <returns> PXCMCursorData.TrackingBounds  </returns>
    public PXCMCursorData.TrackingBounds QueryTrackingBounds()
    {
        return PXCMCursorConfiguration_QueryTrackingBounds(instance);
    }

    /// <summary> 
    /// Enable/Disable Cursor engagement mode.
    /// </summary>
    /// The cursor engagement mode retrieves an indication that the hand is ready to interact with the user application
    /// <param name="enable"> - a boolean to turn off/on the feature.</param>
    /// <returns> PXC_STATUS_NO_ERROR - operation succeeded.    </returns>
    public pxcmStatus EnableEngagement(Boolean enable)
    {
        return PXCMCursorConfiguration_EnableEngagement(instance, enable);
    }

    /// <summary> 
    /// Set the duration time in milliseconds for engagement of the Cursor.
    /// The duration is the loading time since the algorithm recognized the cursor till the completion of the gesture
    /// </summary>
    /// <param name="timeInMilliseconds"> - time duration in milliseconds (min 32)</param>
    /// <returns> PXC_STATUS_NO_ERROR - operation succeeded.    </returns>
    /// <returns> PXC_STATUS_VALUE_OUT_OF_RANGE - time duration is under 32 milliseconds;</returns>
    public pxcmStatus SetEngagementTime(int timeInMilliseconds)
    {
        return PXCMCursorConfiguration_SetEngagementTime(instance, timeInMilliseconds);
    }

    /** 
        @brief Get the duration time in milliseconds for engagement of the Cursor.
		The duration is the time needed for the hand to be in front of the camera and static.
        @param[out] timeInMilliseconds - time duration in milliseconds.
    */
    public int QueryEngagementTime()
    {
        return PXCMCursorConfiguration_QueryEngagementTime(instance);
    }



    /* Alerts Configuration */

    /// <summary> 
    /// Enable alert messaging for a specific event.            
    /// <param name="alertEvent"> - the ID of the event to be enabled.</param>
    /// 
    /// </summary>
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded. </returns>
    /// <returns> PXCM_STATUS_PARAM_UNSUPPORTED - invalid alert type.</returns>
    /// 
    /// @see PXCMCursorData.AlertType
    public pxcmStatus EnableAlert(PXCMCursorData.AlertType alertEvent)
    {
        return PXCMCursorConfiguration_EnableAlert(instance, alertEvent);
    }

    /// <summary> 
    /// Enable all alert messaging events.            
    /// </summary>
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded. </returns>
    public pxcmStatus EnableAllAlerts()
    {
        return PXCMCursorConfiguration_EnableAllAlerts(instance);
    }

    /// <summary> 
    /// Test the activation status of the given alert.
    /// </summary>
    /// <param name="alertEvent"> - the ID of the event to be tested.</param>
    /// <returns> true if the alert is enabled, false otherwise.</returns>
    /// 
    /// @see PXCMCursorData.AlertType
    public Boolean IsAlertEnabled(PXCMCursorData.AlertType alertEvent)
    {
        return PXCMCursorConfiguration_IsAlertEnabled(instance, alertEvent);
    }

    /// <summary> 
    /// Disable alert messaging for a specific event.            
    /// <param name="alertEvent"> - the ID of the event to be disabled</param>
    /// 
    /// </summary>
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded. </returns>
    /// <returns> PXCM_STATUS_PARAM_UNSUPPORTED - unsupported parameter.</returns>
    /// <returns> PXCM_STATUS_DATA_NOT_INITIALIZED - data was not initialized.</returns>
    /// 
    /// @see PXCMCursorData.AlertType
    public pxcmStatus DisableAlert(PXCMCursorData.AlertType alertEvent)
    {
        return PXCMCursorConfiguration_DisableAlert(instance, alertEvent);
    }

    /// <summary> 
    /// Disable messaging for all alerts.                        
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded. </returns>
    /// <returns> PXCM_STATUS_DATA_NOT_INITIALIZED - data was not initialized.</returns>
    /// </summary>
    public pxcmStatus DisableAllAlerts()
    {
        return PXCMCursorConfiguration_DisableAllAlerts(instance);
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
    /// Enable a gesture, so that events are fired when the gesture is identified.
    /// </summary>
    /// <param name="gestureEvent"> -  the ID of the gesture to be enabled.</param>
    /// 
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded. </returns>
    /// <returns> PXCM_STATUS_PARAM_UNSUPPORTED - invalid parameter.</returns>
    public pxcmStatus EnableGesture(PXCMCursorData.GestureType gestureEvent)
    {
        return PXCMCursorConfiguration_EnableGesture(instance, gestureEvent);
    }





    /// <summary> 
    /// Enable all gestures, so that events are fired for every gesture identified.		
    /// </summary>
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded.   </returns>
    public pxcmStatus EnableAllGestures()
    {
        return PXCMCursorConfiguration_EnableAllGestures(instance);
    }

    /// <summary> 
    /// Check whether a gesture is enabled.
    /// </summary>
    /// <param name="gestureEvent"> - the ID of the gesture to be tested.</param>
    /// <returns> true if the gesture is enabled, false otherwise.</returns>
    public Boolean IsGestureEnabled(PXCMCursorData.GestureType gestureEvent)
    {
        return PXCMCursorConfiguration_IsGestureEnabled(instance, gestureEvent);
    }

    /// <summary> 
    /// Deactivate identification of a gesture. Events will no longer be fired for this gesture.            
    /// </summary>
    /// <param name="gestureEvent"> - the ID of the gesture to deactivate.</param>
    /// <returns> PXC_STATUS_NO_ERROR - operation succeeded. </returns>
    /// PXCM_STATUS_PARAM_UNSUPPORTED - invalid gesture name.
    public pxcmStatus DisableGesture(PXCMCursorData.GestureType gestureEvent)
    {
        return PXCMCursorConfiguration_DisableGesture(instance, gestureEvent);
    }


    /// <summary> 
    /// Deactivate identification of all gestures. Events will no longer be fired for any gesture.            
    /// </summary>
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded.  </returns>
    public pxcmStatus DisableAllGestures()
    {
        return PXCMCursorConfiguration_DisableAllGestures(instance);
    }

    /// <summary> 
    /// Register an event handler object to be called on gesture events.
    /// The event handler's OnFiredGesture method will be called each time a gesture is identified.
    /// 
    /// </summary>
    /// <param name="gestureHandler"> - a pointer to the gesture handler.</param>
    /// 
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded. </returns>
    /// <returns> PXCM_STATUS_PARAM_UNSUPPORTED - null gesture handler.</returns>
    /// 
    /// 
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

    internal PXCMCursorConfiguration(IntPtr instance, Boolean delete)
        : base(instance, delete)
    {
        maps = new EventMaps();
    }

    internal PXCMCursorConfiguration(EventMaps maps, IntPtr instance, Boolean delete)
        : base(instance, delete)
    {
        this.maps = maps;
    }
};

#if RSSDK_IN_NAMESPACE
}
#endif
