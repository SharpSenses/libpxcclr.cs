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
///    @class PXCCursorData
///    This class holds all the output of the hand cursor tracking process.
///    Each instance of this class holds the information of a specific frame.
/// </summary>

public partial class PXCMCursorData : PXCMBase
{
    new public const Int32 CUID = 0x54444843;


    /* Enumerations */


    /// <summary>  BodySideType
    /// The side of the body to which a hand belongs.\n
    /// @note Body sides are reported from the player's point-of-view, not the sensor's.
    /// </summary>
    public enum BodySideType
    {
        /// The side was not determined    
        BODY_SIDE_UNKNOWN = 0
            ,BODY_SIDE_LEFT            /// Left side of the body   
            ,BODY_SIDE_RIGHT           /// Right side of the body
    };


    /// <summary> 
    /// AccessOrderType
    /// Orders in which the hands can be accessed.
    /// </summary>
    public enum AccessOrderType
    {
        /// From oldest to newest hand in the scene 
        ACCESS_ORDER_BY_TIME = 0
            ,ACCESS_ORDER_NEAR_TO_FAR        /// From nearest to farthest hand in scene
            ,ACCESS_ORDER_RIGHT_TO_LEFT      /// Right to Left hands        
    };

    /// <summary>  AlertType
    /// Identifiers for the events that can be detected and fired by the cursor module.
    /// </summary>
    public enum AlertType
    {
        ///  Cursor is detected
        CURSOR_DETECTED = 0x0001
            ,CURSOR_NOT_DETECTED = 0x0002           ///  A previously detected cursor is lost
            ,CURSOR_INSIDE_BORDERS = 0x0004         ///  Cursor is outside of the tracking boundaries
            ,CURSOR_OUT_OF_BORDERS = 0x0008         ///  Cursor has moved back inside the tracking boundaries   
            ,CURSOR_TOO_CLOSE = 0x0010              ///  Cursor is too far
            ,CURSOR_TOO_FAR = 0x0020                ///  Cursor is too close     
            ,CURSOR_OUT_OF_LEFT_BORDER = 0x0040     ///  The tracked object is touching the left border of the field of view
            ,CURSOR_OUT_OF_RIGHT_BORDER = 0x0080    ///  The tracked object is touching the right border of the field of view
            ,CURSOR_OUT_OF_TOP_BORDER = 0x0100      ///  The tracked object is touching the upper border of the field of view
            ,CURSOR_OUT_OF_BOTTOM_BORDER = 0x0200   ///  The tracked object is touching the lower border of the field of view
            ,CURSOR_ENGAGED = 0x0400                ///  Cursor enterd engagment mode (ready to interact)
            ,CURSOR_DISENGAGED = 0x0800             ///  Cursor finished engagment, or left the screen
    };


    /// <summary>  GestureType
    /// Identifiers for the events that can be detected and fired by the cursor module.
    /// </summary>
    public enum GestureType
    {
        /// Cursor click - hold your hand facing the camera, close and open your hand in smooth motion. 
        CURSOR_CLICK = 0x000001,
        CURSOR_CLOCKWISE_CIRCLE = 0x000002,         /// Cursor clockwise circle  - move your hand in clockwise circle while hand facing the camera.
        CURSOR_COUNTER_CLOCKWISE_CIRCLE = 0x000004,  /// Cursor counter clockwise circle  - move your hand in counter clockwise circle while hand facing the camera.
        CURSOR_HAND_CLOSING = 0x000008,             /// Cursor hand closing - hold an open hand towards the camera and close your hand.
        CURSOR_HAND_OPENING = 0x000010              /// Cursor hand opening - hold a closed hand towards the camera and open your hand.
    };






    /* Data Structures */

    /// <summary> 
    /// TrackingBounds
    /// Defines the properties of an alert event
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class TrackingBounds
    {
        public Single nearTrackingDistance;     /// nearest tracking distance (distance of small frustum base from sensor)
        public Single farTrackingDistance;      /// farthest tracking distance (distance of large frustum base from sensor)
        public Single nearTrackingWidth;	    /// width of small frustum base
        public Single nearTrackingHeight;	    /// height of small frustum base

    };



    /// <summary> 
    /// AlertData
    /// Defines the properties of an alert event
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class AlertData
    {
        public Int64 timeStamp;		    /// The time-stamp in which the event occurred
        public Int32 frameNumber;       /// The number of the frame in which the event occurred (relevant for recorded sequences)
        public Int32 handId;	    	/// The ID of the hand that triggered the alert, if relevant and known
        public AlertType label;	    	/// The type of alert            

    };

    /// <summary> 
    /// GestureData
    /// Defines the properties of a gesture.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public class GestureData
    {
        public Int64 timeStamp;		    	/// The time-stamp in which the event occurred
        public Int32 frameNumber;       	/// The number of the frame in which the event occurred (relevant for recorded sequences)
        public Int32 handId;	    		/// The ID of the hand that triggered the alert, if relevant and known
        public GestureType label;	    	/// The type of gesture            

    };

    /* Interfaces */

    /// <summary> 
    /// @class IHand
    /// Contains all the properties of the hand that were calculated by the tracking algorithm
    /// </summary>
    public partial class ICursor
    {
        /// <summary>	
        /// Return the hand's unique identifier.
        /// </summary>
        public Int32 QueryUniqueId()
        {
            return PXCMCursorData_ICursor_QueryUniqueId(instance);
        }

        /// <summary> 
        /// Return the time-stamp in which the collection of the hand data was completed.
        /// </summary>
        public Int64 QueryTimeStamp()
        {
            return PXCMCursorData_ICursor_QueryTimeStamp(instance);
        }

        /// <summary> 		
        /// Return the side of the body to which the hand belongs (when known).
        /// @note This information is available only in full-hand tracking mode (TRACKING_MODE_FULL_HAND).
        /// @see PXCMHandConfiguration.SetTrackingMode
        /// </summary>
        public BodySideType QueryBodySide()
        {
            return PXCMCursorData_ICursor_QueryBodySide(instance);
        }



        /// <summary> 		
        /// Get the geometric position of the cursor in 3D world coordinates, in meters.
		///	Return the cursor point in world coordinates.
        /// </summary>
        public PXCMPoint3DF32 QueryCursorPointWorld()
        {
            PXCMPoint3DF32 point = new PXCMPoint3DF32();
            PXCMCursorData_ICursor_QueryCursorWorldPoint(instance, ref point);
            return point;
        }

        /// <summary> 		
        ///  Get the geometric position of the cursor in 2D image coordinates, in pixels. (Note: the Z coordinate is the point's depth in millimeters.)
		///	@return the cursor point in image coordinates.
        /// </summary>
        public PXCMPoint3DF32 QueryCursorPointImage()
        {
            PXCMPoint3DF32 point = new PXCMPoint3DF32();
            PXCMCursorData_ICursor_QueryCursorImagePoint(instance, ref point);
            return point;
        }

        /// <summary> 		
        /// The module defines a bounding box around a hand in the world coordinate system, the adaptive point is a normalized point inside the bounding box with values between 0-1.
		///	Using this point allows an easy way to map the hand cursor to any resolution screen.
		///	Return the a 3D point with values between 0-1.
        /// </summary>
        public PXCMPoint3DF32 QueryAdaptivePoint()
        {
            PXCMPoint3DF32 point = new PXCMPoint3DF32();
            PXCMCursorData_ICursor_QueryAdaptivePoint(instance, ref point);
            return point;
        }

        /// <summary> 
        ///  Get the hand cursor engagement loading percentage.
        ///  Return 100 - percent indicates full engagement state.
        ///  Return -1  - percent indicates that the feature wasn't enabled.
        ///  @see PXCCursorConfiguration::EnableEngagement
        ///  @see AlertType::CURSOR_ENGAGED	
        ///  @see AlertType::CURSOR_DISENGAGED	
        /// </summary>      
        public Int32 QueryEngagementPercent()
        {
            return PXCMCursorData_ICursor_QueryEngagementPercent(instance);
        }

        private IntPtr instance;
        internal ICursor(IntPtr instance)
        {
            this.instance = instance;
        }


    };




    /* General */

    /// <summary>
    ///  Updates hand data to the most current output.
    /// </summary>
    public pxcmStatus Update()
    {
        return PXCMCursorData_Update(instance);
    }

    /* Alerts Outputs */

    /// <summary>
    /// Return the number of fired alerts in the current frame.
    /// </summary>
    public Int32 QueryFiredAlertsNumber()
    {
        return PXCMCursorData_QueryFiredAlertsNumber(instance);
    }

    /// <summary> 
    /// Get the details of the fired alert with the given index.
    /// 
    /// </summary>
    /// <param name="index"> - the zero-based index of the requested fired alert.</param>
    /// <param name="alertData"> - the information for the fired event.</param>
    /// 
    /// @note the index is between 0 and the result of QueryFiredAlertsNumber()
    /// 
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded.</returns>
    /// <returns> PXCM_STATUS_PARAM_UNSUPPORTED - invalid input parameter.</returns>
    /// 
    /// @see AlertData
    /// @see QueryFiredAlertsNumber
    public pxcmStatus QueryFiredAlertData(Int32 index, out AlertData alertData)
    {
        return QueryFiredAlertDataINT(instance, index, out alertData);
    }


    /// <summary>
    /// Return whether the specified alert is fired in the current frame, and retrieve its data.
    /// 
    /// </summary>
    /// <param name="alertEvent"> - the alert type.</param>
    /// <param name="alertData"> - the information for the fired event.</param>
    /// <returns> true if the alert is fired, false otherwise.</returns>
    /// 
    /// @see AlertType
    /// @see AlertData
    /// 
    public Boolean IsAlertFired(AlertType alertEvent, out AlertData alertData)
    {
        return IsAlertFiredINT(instance, alertEvent, out alertData);
    }



    /// <summary>
    /// Return whether the specified alert is fired for a specific hand in the current frame, and retrieve its data.
    /// 
    /// </summary>
    /// <param name="alertEvent"> - the alert type.</param>
    /// <param name="handID"> - the ID of the hand whose alert should be retrieved.</param>
    /// <param name="alertData"> - the information for the fired event.</param>
    /// <returns> true if the alert is fired, false otherwise.</returns>
    /// 
    /// @see AlertType
    /// @see AlertData
    /// 
    public Boolean IsAlertFiredByHand(AlertType alertEvent, Int32 handID, out AlertData alertData)
    {
        return IsAlertFiredByHandINT(instance, alertEvent, handID, out alertData);
    }

    /* Gestures Outputs */

    /// <summary> 
    /// Return the number of gestures fired in the current frame.
    /// </summary>
    public Int32 QueryFiredGesturesNumber()
    {
        return PXCMCursorData_QueryFiredGesturesNumber(instance);
    }

    /// <summary> 
    /// Get the details of the fired gesture with the given index.
    /// </summary>
    /// 
    /// <param name="index"> - the zero-based index of the requested fired gesture.</param>
    /// <param name="gestureData"> - the information for the fired gesture.</param>
    /// 
    /// @note The gesture index must be between 0 and [QueryFiredGesturesNumber() - 1]
    /// 
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded.</returns>
    /// <returns> PXCM_STATUS_PARAM_UNSUPPORTED - invalid input parameter.</returns>
    /// 
    /// @see GestureData
    /// @see QueryFiredGesturesNumber
    public pxcmStatus QueryFiredGestureData(Int32 index, out GestureData gestureData)
    {
        return QueryFiredGestureDataINT(instance, index, out gestureData);
    }

    /// <summary>
    /// Return whether the specified gesture is fired for a specific hand in the current frame, and if so retrieve its data.
    /// </summary>
    /// <param name="gestureEvent"> - the event type of the gesture to be checked.</param>
    /// <param name="gestureData"> - the information for the fired gesture.</param>
    /// 
    /// <returns> true if the gesture was fired, false otherwise.</returns>
    /// 
    /// @see GestureType 
    /// @see GestureData
    public Boolean IsGestureFired(GestureType gestureEvent, out GestureData gestureData)
    {
        return IsGestureFiredINT(instance, gestureEvent, out gestureData);
    }



    /// <summary>
    /// Return whether the specified gesture is fired for a specific hand in the current frame, and if so retrieve its data.
    /// </summary>
    /// <param name="gestureName"> - the name of the gesture to be checked.</param>
    /// <param name="handID"> - the ID of the hand whose alert should be retrieved.</param>
    /// <param name="gestureData"> - the information for the fired gesture.</param>
    /// 
    /// <returns> true if the gesture was fired, false otherwise.</returns>
    /// @see GestureData
    public Boolean IsGestureFiredByHand(GestureType gestureEvent, Int32 handID, out GestureData gestureData)
    {
        return IsGestureFiredByHandINT(instance, gestureEvent, handID, out gestureData);
    }

    /* Hands Outputs */

    /// <summary> 
    /// Return the number of hands detected in the current frame.            
    /// </summary>
    public Int32 QueryNumberOfCursors()
    {
        return PXCMCursorData_QueryNumberOfCursors(instance);
    }



    /// <summary> 
    /// Retrieve the cursor object data using a specific AccessOrder and related index.
    /// 
    /// </summary>
    /// <param name="accessOrder"> - the order in which the cursors are enumerated (accessed).</param>
    /// <param name="index"> - the index of the cursor to be retrieved, based on the given AccessOrder.</param>
    /// <param name="cursorData"> - the information for the cursor.</param>
    /// 
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded.</returns>
    /// <returns> PXCM_STATUS_DATA_UNAVAILABLE  - index >= number of detected hands.</returns>
    /// 
    /// @see AccessOrder
    /// @see ICursor		
    public pxcmStatus QueryCursorData(AccessOrderType accessOrder, Int32 index, out ICursor cursorData)
    {
        IntPtr hd2;
        pxcmStatus sts = PXCMCursorData_QueryCursorData(instance, accessOrder, index, out hd2);
        cursorData = (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR) ? new ICursor(hd2) : null;
        return sts;
    }

    /// <summary> 
    /// Retrieve the cursor object data by its unique Id.
    /// </summary>
    /// <param name="handID"> - the unique ID of the requested hand</param>
    /// <param name="cursorData"> - the information for the cursor.</param>
    /// 
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded.</returns>
    /// <returns> PXCM_STATUS_DATA_UNAVAILABLE  - there is no output data.</returns>
    /// <returns> PXCM_STATUS_PARAM_UNSUPPORTED - there is no hand data for the given cursor ID. </returns>
    /// 
    /// @see ICursor		
    public pxcmStatus QueryCursorDataById(Int32 cursorId, out ICursor cursorData)
    {
        IntPtr hd2;
        pxcmStatus sts = PXCMCursorData_QueryCursorDataById(instance, cursorId, out hd2);
        cursorData = (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR) ? new ICursor(hd2) : null;
        return sts;
    }

    internal PXCMCursorData(IntPtr instance, Boolean delete)
        : base(instance, delete)
    {
    }
};

#if RSSDK_IN_NAMESPACE
}
#endif
