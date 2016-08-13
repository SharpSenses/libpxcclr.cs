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

public partial class PXCMTouchlessController : PXCMBase
{
    new public const Int32 CUID = 0x534b4c46;
    /// <summary> 
    /// ProfileInfo
    /// Containing the parameters that define a TouchlessController session     
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public class ProfileInfo
    {
        /// <summary>
        ///  Configuration
        /// an or value of UX options relevant to specific application
        /// </summary>
        [Flags]
        public enum Configuration : int
        {
            /// <summary>
            /// No option is selected - use default behavior 
            /// </summary>
            Configuration_None = 0x00000000,
            /// <summary>
            /// Should zoom be allowed
            /// </summary>
            Configuration_Allow_Zoom = 0x00000001,
            /// <summary>
            /// Use draw mode - should be used for applications the need continues interaction (touch + movement) like drawing
            /// </summary>
            Configuration_Use_Draw_Mode = 0x00000002,
            /// <summary>
            /// Enable horizontal scrolling
            /// </summary>
            Configuration_Scroll_Horizontally = 0x00000004,
            /// <summary>
            /// Enable vertical scrolling
            /// </summary>
            Configuration_Scroll_Vertically = 0x00000008,
            /// <summary>
            /// On a "V" gesture enables the *meta* UXEvents
            /// </summary>
            Configuration_Meta_Context_Menu = 0x00000010,
            /// <summary>
            /// Causes the OS to simulate the gesture with the appropriate injection
            /// </summary>
            Configuration_Enable_Injection = 0x00000020,
            /// <summary>
            /// Enable horizontal scrolling 
            /// </summary>
            Configuration_Edge_Scroll_Horizontally = 0x00000040,
            /// <summary>
            /// Enable vertical scrolling
            /// </summary>
            Configuration_Edge_Scroll_Vertically = 0x00000080,
            /// <summary>
            /// Enable Back Gesture
            /// </summary>
            Configuration_Allow_Back = 0x00000200,
            /// <summary>
            /// Enable Selection Gesture
            /// </summary>
            Configuration_Allow_Selection = 0x00000400,
            /// <summary>
            /// if enabled TouchlessController will stop tracking the hand while the mouse moves
            /// </summary>
            Configuration_Disable_On_Mouse_Move = 0x00000800
        };

        private IntPtr handInstance;   //the HandAnalysis module used by this module

        /// <summary>
        /// An or value of configuration options 
        /// </summary>
        public Configuration config;

        public PXCMHandModule handAnalysis
        {
            get { return handInstance != IntPtr.Zero ? new PXCMHandModule(handInstance, false) : null; }
            private set { handInstance = value.instance; }
        }
    };

    /// <summary>
    ///Describe a UXEvent,
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class UXEventData
    {

        /// <summary>
        /// 	UXEventType
        /// 	Values that represent UXEventType.
        /// </summary>
        public enum UXEventType : int
        {
            UXEvent_StartZoom,			// the user start performing a zoom operation - pan my also be performed during zoom
            UXEvent_Zoom,				// Fired while zoom operation is ongoing
            UXEvent_EndZoom,			// User stoped zoomig
            UXEvent_StartScroll,		// the user start performing a scroll or pan operation
            UXEvent_Scroll,				// Fired while scroll operation is ongoing
            UXEvent_EndScroll,			// User stoped scrolling (panning)
            UXEvent_StartDraw,			// User started drawing
            UXEvent_Draw,				// Fired while draw operation is ongoing
            UXEvent_EndDraw,			// User finshed drawing
            UXEvent_CursorMove,			// Cursor moved while not in any other mode
            UXEvent_Select,				// oser selected a button
            UXEvent_GotoStart,			// Got to windows 8 start screen
            UXEvent_CursorVisible,		// Cursor turned visible
            UXEvent_CursorNotVisible,	// Cursor turned invisible
            UXEvent_ReadyForAction,		// The user is ready to perform a zoom or scroll operation
            UXEvent_StartMetaCounter,   // Start Meta Menu counter visual
            UXEvent_StopMetaCounter,    // Abort Meta Menu Counter Visual
            UXEvent_ShowMetaMenu,       // Show Meta Menu
            UXEvent_HideMetaMenu,       // Hide Meta Menu
            UXEvent_MetaPinch,			// When a pinch was detected while in meta mode
            UXEvent_MetaOpenHand,       // When a pinch ends while in meta mode
            UXEvent_Back,				// User perform back gesture
            UXEvent_ScrollUp,			// Edge Scroll up was started by moving cursor to upper screen edge
            UXEvent_ScrollDown,			// Edge Scroll down was started by moving cursor to down screen edge
            UXEvent_ScrollLeft,			// Edge Scroll left was started by moving cursor to left screen edge
            UXEvent_ScrollRight,		// Edge Scroll right was started by moving cursor to right screen edge
        };
        public UXEventType type; // type of the event		
        public PXCMPoint3DF32 position; // position where event happen values are in rang [0,1]
        public PXCMHandData.BodySideType bodySide; // the hand that issued the event
    };

    /// <summary>
    /// 	An alert data, contain data describing an alert.    
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class AlertData
    {
        /// <summary>
        /// 	Values that represent AlertType.
        /// </summary>
        public enum AlertType
        {
            Alert_TooClose,         // The user hand is too close to the 3D camera
            Alert_TooFar,           // The user hand is too far from the 3D camera
            Alert_NoAlerts          // A previous alerted situation was ended
        };
        public AlertType type; // the  type of the alert
    };

    /// <summary>
    /// 	Values that represent Action. Those are actions the module will inject to the OS
    /// </summary>
    public enum Action
    {
        Action_None = 0,		// No action will be injected
        Action_LeftKeyPress,	// can be used to Go to the next item (Page/Slide/Photo etc.)
        Action_RightKeyPress,	//  can be used to Go to the previouse item (Page/Slide/Photo etc.)
        Action_BackKeyPress,	//  can be used to Go to the previouse item (Page/Slide/Photo etc.)
        Action_PgUpKeyPress,	//  can be used to Go to the previouse item (Page/Slide/Photo etc.)
        Action_PgDnKeyPress,	//  can be used to Go to the previouse item (Page/Slide/Photo etc.)
        Action_VolumeUp,
        Action_VolumeDown,
        Action_Mute,
        Action_NextTrack,
        Action_PrevTrack,
        Action_PlayPause,
        Action_Stop,
        Action_ToggleTabs,		// can be used to display tabs menu in Metro Internet Explorer
    };

    /// <summary>
    /// 	Values that represent Sensitivity level for the pointer movement
    /// </summary>
    public enum PointerSensitivity
    {
        PointerSensitivity_Smoothed,
        PointerSensitivity_Balanced,
        PointerSensitivity_Sensitive
    };

    /* Marshal Unmanaged Callbacks to Managed */
    public delegate void OnFiredUXEventDelegate(UXEventData data);

    /* Marshal Unmanaged Callbacks to Managed */
    public delegate void OnFiredAlertDelegate(AlertData data);

    /* Marshal Unmanaged Callbacks to Managed */
    public delegate void OnFiredActionDelegate(Action data);

    /// <summary> 
    /// Set configuration parameters of the SDK TouchlessController application.
    /// </summary>
    /// <param name=" pinfo"> the profile info structure of the configuration parameters.</param>
    /// <returns> PXC_STATUS_NO_ERROR if the parameters were set correctly; otherwise, return one of the following errors:</returns>
    /// PXC_STATUS_INIT_FAILED - Module failure during initialization.\n
    /// PXC_STATUS_DATA_NOT_INITIALIZED - Data failed to initialize.\n                        
    public pxcmStatus SetProfile(ProfileInfo pinfo)
    {
        return PXCMTouchlessController_SetProfile(instance, pinfo);
    }


    /// <summary>
    /// 	Adds a gesture action mapping.
    /// </summary>
    /// <param name=" gestureName">name of the gesture.</param>
    /// <param name=" action">The action to perform when gesture is recognized.</param>
    /// <returns>	The pxcmStatus.</returns>
    public pxcmStatus AddGestureActionMapping(String gestureName, Action action)
    {
        return AddGestureActionMapping(gestureName, action, null);
    }

    /// <summary> 
    /// Register an event handler for UX Event. The event handler's will be called each time a UX event is identified.
    /// </summary>
    /// <param name=" uxEventHandler"> a delegete event handle.</param>
    /// <returns> PXC_STATUS_NO_ERROR if the registering an event handler was successful; otherwise, return the following error:
    /// PXC_STATUS_DATA_NOT_INITIALIZED - Data failed to initialize.</returns>
    public pxcmStatus SubscribeEvent(OnFiredUXEventDelegate uxEventHandler)
    {
        if (uxEventHandler == null) return pxcmStatus.PXCM_STATUS_HANDLE_INVALID;

        Object proxy;
        pxcmStatus sts = SubscribeEventINT(instance, uxEventHandler, out proxy);
        if (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR)
        {
            lock (maps.cs)
            {
                maps.uxEvents[uxEventHandler] = proxy;
            }
        }
        return sts;
    }

    /// <summary> 
    /// Unsubscribe an event handler for UX events.
    /// </summary>
    /// <param name=" uxEventHandler"> a delegete event handle. that should be removed.</param>
    /// <returns> PXC_STATUS_NO_ERROR if the unregistering the event handler was successful, an error otherwise.</returns>
    public pxcmStatus UnsubscribeEvent(OnFiredUXEventDelegate uxEventHandler)
    {
        if (uxEventHandler == null) return pxcmStatus.PXCM_STATUS_HANDLE_INVALID;

        lock (maps.cs)
        {
            Object proxy;
            if (!maps.uxEvents.TryGetValue(uxEventHandler, out proxy))
                return pxcmStatus.PXCM_STATUS_HANDLE_INVALID;

            pxcmStatus sts = UnsubscribeEventINT(instance, proxy);
            maps.uxEvents.Remove(uxEventHandler);
            return sts;
        }
    }

    /// <summary> 
    /// Register an event handler for alerts. The event handler's will be called each time an alert is identified.
    /// </summary>
    /// <param name=" alertHandler"> a delegate event handler.</param>
    /// <returns> PXC_STATUS_NO_ERROR if the registering an event handler was successful; otherwise, return the following error:
    /// PXC_STATUS_DATA_NOT_INITIALIZED - Data failed to initialize.</returns>
    public pxcmStatus SubscribeAlert(OnFiredAlertDelegate alertHandler)
    {
        if (alertHandler == null) return pxcmStatus.PXCM_STATUS_HANDLE_INVALID;

        Object proxy;
        pxcmStatus sts = SubscribeAlertINT(instance, alertHandler, out proxy);
        if (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR)
        {
            lock (maps.cs)
            {
                maps.alerts[alertHandler] = proxy;
            }
        }
        return sts;
    }

    /// <summary> 
    /// Unsubscribe an event handler for alerts.
    /// </summary>
    /// <param name=" alertHandler"> a delegate event handler that should be removed.</param>
    /// <returns> PXC_STATUS_NO_ERROR if the unregistering the event handler was successful, an error otherwise.</returns>
    public pxcmStatus UnsubscribeAlert(OnFiredAlertDelegate alertHandler)
    {
        if (alertHandler == null) return pxcmStatus.PXCM_STATUS_HANDLE_INVALID;

        lock (maps.cs)
        {
            Object proxy;
            if (!maps.alerts.TryGetValue(alertHandler, out proxy))
                return pxcmStatus.PXCM_STATUS_HANDLE_INVALID;

            pxcmStatus sts = UnsubscribeAlertINT(instance, proxy);
            maps.alerts.Remove(alertHandler);
            return sts;
        }
    }

    /// <summary>
    /// Adds a gesture action mapping.
    /// </summary>
    /// <param name="gestureName">name of the gesture.</param>
    /// <param name="action">The action to perform when gesture is recognized .</param>
    /// <param name="actionHandler">actionHandler will be called when the action is performed</param>
    /// <returns>The pxcmStatus.</returns>
    public pxcmStatus AddGestureActionMapping(String gestureName, Action action, OnFiredActionDelegate actionHandler)
    {
        if (actionHandler == null)
            return PXCMTouchlessController_AddGestureActionMapping(instance, gestureName, action, IntPtr.Zero);

        Object proxy;
        pxcmStatus sts = AddGestureActionMappingINT(instance, gestureName, action, actionHandler, out proxy);
        if (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR)
        {
            lock (maps.cs)
            {
                maps.actions.Add(proxy);
            }
        }
        return sts;
    }

    /// <summary>
    /// Clear all previous Gesture to Action mappings
    /// </summary>
    public pxcmStatus ClearAllGestureActionMappings()
    {
        pxcmStatus res = PXCMTouchlessController_ClearAllGestureActionMappings(instance);
        lock (maps.cs)
        {
            maps.actions.Clear();
        }
        return res;
    }

    internal class EventMap
    {
        public Dictionary<OnFiredUXEventDelegate, Object> uxEvents = new Dictionary<OnFiredUXEventDelegate, object>();
        public Dictionary<OnFiredAlertDelegate, Object> alerts = new Dictionary<OnFiredAlertDelegate, object>();
        public List<Object> actions = new List<object>();
        public Object cs = new Object();
    };

    internal EventMap maps;

    internal PXCMTouchlessController(IntPtr instance, Boolean delete)
        : base(instance, delete)
    {
        maps = new EventMap();
    }

    internal PXCMTouchlessController(EventMap maps, IntPtr instance, Boolean delete)
        : base(instance, delete)
    {
        this.maps = maps;
    }
};

#if RSSDK_IN_NAMESPACE
}
#endif
