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
/// @class PXCMHandData
/// This class holds all the output of the hand tracking process.
/// 
/// Each instance of this class holds the information of a specific frame.
/// </summary>

public partial class PXCMHandData : PXCMBase
{
    new public const Int32 CUID = 0x54444148;
    public const Int32 NUMBER_OF_FINGERS = 5;
    public const Int32 NUMBER_OF_EXTREMITIES = 6;
    public const Int32 NUMBER_OF_JOINTS = 22;
    public const Int32 RESERVED_NUMBER_OF_JOINTS = 32;
    public const Int32 MAX_NAME_SIZE = 64;
    public const Int32 MAX_PATH_NAME = 256;

    /* Enumerations */

    /// <summary>  JointType
    /// Identifiers of joints that can be tracked by the hand module.
    /// </summary>
    public enum JointType
    {

        JOINT_WRIST = 0			/// The center of the wrist
            ,
        JOINT_CENTER			/// The center of the palm
            ,
        JOINT_THUMB_BASE		/// Thumb finger joint 1 (base)
            ,
        JOINT_THUMB_JT1		/// Thumb finger joint 2
            ,
        JOINT_THUMB_JT2		/// Thumb finger joint 3
            ,
        JOINT_THUMB_TIP		/// Thumb finger joint 4 (fingertip)
            ,
        JOINT_INDEX_BASE		/// Index finger joint 1 (base)
            ,
        JOINT_INDEX_JT1		/// Index finger joint 2
            ,
        JOINT_INDEX_JT2		/// Index finger joint 3
            ,
        JOINT_INDEX_TIP		/// Index finger joint 4 (fingertip)
            ,
        JOINT_MIDDLE_BASE		/// Middle finger joint 1 (base)
            ,
        JOINT_MIDDLE_JT1		/// Middle finger joint 2
            ,
        JOINT_MIDDLE_JT2		/// Middle finger joint 3
            ,
        JOINT_MIDDLE_TIP		/// Middle finger joint 4 (fingertip)
            ,
        JOINT_RING_BASE		/// Ring finger joint 1 (base)
            ,
        JOINT_RING_JT1		/// Ring finger joint 2
            ,
        JOINT_RING_JT2		/// Ring finger joint 3
            ,
        JOINT_RING_TIP		/// Ring finger joint 4 (fingertip)
            ,
        JOINT_PINKY_BASE		/// Pinky finger joint 1 (base)
            ,
        JOINT_PINKY_JT1		/// Pinky finger joint 2
            ,
        JOINT_PINKY_JT2		/// Pinky finger joint 3
            , JOINT_PINKY_TIP		/// Pinky finger joint 4 (fingertip)		
    };

    /// <summary>
    /// ExtremityType
    /// Identifier of extremity points of the tracked hand.
    /// </summary>
    public enum ExtremityType
    {
        /// The closest point to the camera in the tracked hand
        EXTREMITY_CLOSEST = 0
            ,
        EXTREMITY_LEFTMOST 		/// The left-most point of the tracked hand
            ,
        EXTREMITY_RIGHTMOST		/// The right-most point of the tracked hand 
            ,
        EXTREMITY_TOPMOST			/// The top-most point of the tracked hand
            ,
        EXTREMITY_BOTTOMMOST		/// The bottom-most point of the tracked hand
            , EXTREMITY_CENTER			/// The center point of the tracked hand			
    };

    /// <summary>  FingerType
    /// Finger identifiers.
    /// </summary>
    public enum FingerType
    {
        /// Thumb finger
        FINGER_THUMB = 0
            ,
        FINGER_INDEX           /// Index finger  
            ,
        FINGER_MIDDLE          /// Middle finger
            ,
        FINGER_RING            /// Ring finger
            , FINGER_PINKY           /// Pinky finger
    };

    /// <summary>  BodySideType
    /// The side of the body to which a hand belongs.\n
    /// @note Body sides are reported from the player's point-of-view, not the sensor's.
    /// </summary>
    public enum BodySideType
    {
        /// The side was not determined    
        BODY_SIDE_UNKNOWN = 0
            ,
        BODY_SIDE_LEFT            /// Left side of the body   
            , BODY_SIDE_RIGHT           /// Right side of the body
    };

    /// <summary>  AlertType
    /// Identifiers for the events that can be detected and fired by the hand module.
    /// </summary>
    public enum AlertType
    {
        ALERT_HAND_DETECTED = 0x0001        ///  A hand is identified and its mask is available    
            ,
        ALERT_HAND_NOT_DETECTED = 0x0002        ///  A previously detected hand is lost, either because it left the field of view or because it is occluded
            ,
        ALERT_HAND_TRACKED = 0x0004        ///  Full tracking information is available for a hand
            ,
        ALERT_HAND_NOT_TRACKED = 0x0008        ///  No tracking information is available for a hand (none of the joints are tracked)
            ,
        ALERT_HAND_CALIBRATED = 0x0010        ///  Hand measurements are ready and accurate 
            ,
        ALERT_HAND_NOT_CALIBRATED = 0x0020        ///  Hand measurements are not yet finalized, and are not fully accurate
            ,
        ALERT_HAND_OUT_OF_BORDERS = 0x0040        ///  Hand is outside of the tracking boundaries
            ,
        ALERT_HAND_INSIDE_BORDERS = 0x0080        ///  Hand has moved back inside the tracking boundaries         
            ,
        ALERT_HAND_OUT_OF_LEFT_BORDER = 0x0100        ///  The tracked object is touching the left border of the field of view
            ,
        ALERT_HAND_OUT_OF_RIGHT_BORDER = 0x0200        ///  The tracked object is touching the right border of the field of view
            ,
        ALERT_HAND_OUT_OF_TOP_BORDER = 0x0400        ///  The tracked object is touching the upper border of the field of view
            ,
        ALERT_HAND_OUT_OF_BOTTOM_BORDER = 0x0800        ///  The tracked object is touching the lower border of the field of view
            ,
        ALERT_HAND_TOO_FAR = 0x1000        ///  The tracked object is too far
            ,
        ALERT_HAND_TOO_CLOSE = 0x2000        ///  The tracked object is too close	                
            , ALERT_HAND_LOW_CONFIDENCE = 0x4000	    ///  The tracked object is low confidence	


    };

    /// <summary> 
    /// GestureStateType
    /// Enumerates the possible states of a gesture (start/in progress/end).
    /// @note Depending on the configuration, you can either get "start" and "end" events when the gesture starts/ends,\n
    /// or get the "in_progress" event for every frame in which the gesture is detected. 
    /// See the "continuousGesture" flag in PXCHandConfiguration::enableGesture for more details.
    /// @see PXCMHandConfiguration.enableGesture
    /// </summary>
    public enum GestureStateType
    {
        /// Gesture started - fired at the first frame where the gesture is identified
        GESTURE_STATE_START = 0
            ,
        GESTURE_STATE_IN_PROGRESS	/// Gesture is in progress - fired for every frame where the gesture is identified
            , GESTURE_STATE_END			/// Gesture ended - fired after the last frame where the gestures was identified
    };

    /// <summary> 
    /// TrackingModeType
    /// Defines the possible tracking modes.
    /// TRACKING_MODE_FULL_HAND - enables full tracking of the hand skeleton, including all the joints' information.
    /// TRACKING_MODE_EXTREMITIES - tracks only the hand's mask and its extremity points.		   
    /// </summary>
    public enum TrackingModeType
    {
        /// Track the full skeleton (22 joints)
        TRACKING_MODE_FULL_HAND = 0x0001
            , TRACKING_MODE_EXTREMITIES = 0x0002 	/// Track the hand extremities
    };

    /// <summary> 
    /// JointSpeedType
    /// Modes for calculating the joints' speed.
    /// </summary>
    public enum JointSpeedType
    {
        /// Average of signed speed values (which are positive or negative depending on direction) across time
        JOINT_SPEED_AVERAGE = 0
            , JOINT_SPEED_ABSOLUTE	/// Average of absolute speed values (always positive regardless of direction) across time.	
    };


    /// <summary>
    /// TrackingStatusType
    /// Status values of hand tracking.
    /// In case of problematic tracking conditions, this value indicates the problem type.
    /// </summary>
    public enum TrackingStatusType
    {
        /// Optimal tracking conditions
        TRACKING_STATUS_GOOD = 0
            ,
        TRACKING_STATUS_OUT_OF_FOV = 1		/// The hand is outside the field of view (in the x/y axis)
            ,
        TRACKING_STATUS_OUT_OF_RANGE = 2	/// The hand is outside the depth range
            ,
        TRACKING_STATUS_HIGH_SPEED = 4		/// The hand is moving at high speed 	
            , TRACKING_STATUS_POINTING_FINGERS = 8	/// The hand fingers pointing the camera 
    };

    /// <summary> 
    /// AccessOrderType
    /// Orders in which the hands can be accessed.
    /// </summary>
    public enum AccessOrderType
    {
        /// By unique ID of the hand
        ACCESS_ORDER_BY_ID = 0
            ,
        ACCESS_ORDER_BY_TIME 			/// From oldest to newest hand in the scene      
            ,
        ACCESS_ORDER_NEAR_TO_FAR		/// From nearest to farthest hand in scene
            ,
        ACCESS_ORDER_LEFT_HANDS		/// All left hands
            ,
        ACCESS_ORDER_RIGHT_HANDS		/// All right hands
            , ACCESS_ORDER_FIXED			/// The index of each hand (either 0 or 1) is fixed as long as it is detected
    };

    /* Data Structures */

    /// <summary>  JointData
    /// A structure containing information about the position and rotation of a joint in the hand's skeleton.
    /// See the Hand Module Developer Guide for more details.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class JointData
    {
        public Int32 confidence;                    /// RESERVED: for future confidence score feature
        public PXCMPoint3DF32 positionWorld;        /// The geometric position in 3D world coordinates, in meters
        public PXCMPoint3DF32 positionImage;        /// The geometric position in 2D image coordinates, in pixels. (Note: the Z coordinate is the point's depth in millimeters.)
        public PXCMPoint4DF32 localRotation;        /// A quaternion representing the local 3D orientation of the joint, relative to its parent joint
        public PXCMPoint4DF32 globalOrientation;    /// A quaternion representing the global 3D orientation, relative to the "world" y axis
        public PXCMPoint3DF32 speed;				/// The speed of the joints in 3D world coordinates (X speed, Y speed, Z speed, in meters/second)
    };

    /// <summary> 
    /// ExtremityData
    /// Defines the positions of an extremity point.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class ExtremityData
    {
        public PXCMPoint3DF32 pointWorld;		/// 3D world coordinates of the extremity point
        public PXCMPoint3DF32 pointImage;		/// 2D image coordinates of the extremity point
    };

    /// <summary> 
    /// FingerData
    /// Defines the properties of a finger.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class FingerData
    {
        public Int32 foldedness;			/// The degree of "foldedness" of the tracked finger, ranging from 0 (least folded / straight) to 100 (most folded).
        public Single radius;				/// The radius of the tracked fingertip. The default value is 0.017m while the hand is not calibrated.
    };

    /// <summary> 
    /// AlertData
    /// Defines the properties of an alert event
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class AlertData
    {
        public AlertType label;	    	/// The type of alert
        public Int32 handId;	    	/// The ID of the hand that triggered the alert, if relevant and known
        public Int64 timeStamp;		    /// The time-stamp in which the event occurred
        public Int32 frameNumber;       /// The number of the frame in which the event occurred (relevant for recorded sequences)
    };

    /// <summary> 
    /// GestureData
    /// Defines the properties of a gesture.
    /// 
    /// The gestures in the default gesture package (installed with the hand module by default) are:
    /// Gesture that are available for TRACKING_MODE_FULL_HAND:
    /// "spreadfingers"  - hand open facing the camera.
    /// "thumb_up" - hand closed with thumb pointing up.
    /// "thumb_down"  - hand closed with thumb pointing down.
    /// "two_fingers_pinch_open"  - hand open with thumb and index finger touching each other.
    /// "v_sign" - hand closed with index finger and middle finger pointing up.
    /// "fist" - all fingers folded into a fist. The fist can be in different orientations as long as the palm is in the general direction of the camera.
    /// "full_pinch" - all fingers extended and touching the thumb. The pinched fingers can be anywhere between pointing directly to the screen or in profile. 
    /// "tap" - a hand in a natural relaxed pose is moved forward as if pressing a button.
    /// "wave" - an open hand facing the screen. The wave gesture's length can be any number of repetitions.
    /// "click" - hand facing the camera either with open palm or closed move the index finger fast toward the palm center as if clicking on a mouse.
    /// "swipe_down" - hold hand towards the camera and moves it down and then return it toward the starting position.
    /// "swipe_up" - hold hand towards the camera and moves it up and then return it toward the starting position.
    /// "swipe_right" - hold hand towards the camera and moves it right and then return it toward the starting position.
    /// "swipe_left" - hold hand towards the camera and moves it left and then return it toward the starting position.            
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public class GestureData
    {
        public Int64 timeStamp;				/// Time-stamp in which the gesture occurred
        public Int32 handId;	    			/// The ID of the hand that made the gesture, if relevant and known
        public GestureStateType state;						/// The state of the gesture (start, in progress, end)		
        public Int32 frameNumber;			/// The number of the frame in which the gesture occurred (relevant for recorded sequences)		
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_NAME_SIZE)]
        public String name;		/// The gesture name

        public GestureData()
        {
            name = "";
        }
    };

    /* Interfaces */

    /// <summary> 
    /// @class IHand
    /// Contains all the properties of the hand that were calculated by the tracking algorithm
    /// </summary>
    public partial class IHand
    {
        /// <summary>	
        /// Return the hand's unique identifier.
        /// </summary>
        public Int32 QueryUniqueId()
        {
            return PXCMHandData_IHand_QueryUniqueId(instance);
        }

        /// <summary> 
        /// Return the identifier of the user whose hand is represented.
        /// </summary>
        public Int32 QueryUserId()
        {
            return PXCMHandData_IHand_QueryUserId(instance);
        }

        /// <summary> 
        /// Return the time-stamp in which the collection of the hand data was completed.
        /// </summary>
        public Int64 QueryTimeStamp()
        {
            return PXCMHandData_IHand_QueryTimeStamp(instance);
        }

        /// <summary>
        /// Return true if there is a valid hand calibration, otherwise false.
        /// A valid calibration results in more accurate tracking data, that is better fitted to the user's hand.\n
        /// After identifying a new hand, the hand module calculates its calibration. When calibration is complete, an alert is issued.\n
        /// Tracking is more robust for a calibrated hand.
        /// </summary>
        public Boolean IsCalibrated()
        {
            return PXCMHandData_IHand_IsCalibrated(instance);
        }

        /// <summary> 		
        /// Return the side of the body to which the hand belongs (when known).
        /// @note This information is available only in full-hand tracking mode (TRACKING_MODE_FULL_HAND).
        /// @see PXCMHandConfiguration.SetTrackingMode
        /// </summary>
        public BodySideType QueryBodySide()
        {
            return PXCMHandData_IHand_QueryBodySide(instance);
        }

        /// <summary>	
        /// Return the location and dimensions of the tracked hand, represented by a 2D bounding box (defined in pixels).
        /// </summary>
        /// <returns> The location and dimensions of the 2D bounding box.</returns>
        public PXCMRectI32 QueryBoundingBoxImage()
        {
            PXCMRectI32 rect = new PXCMRectI32();
            PXCMHandData_IHand_QueryBoundingBoxImage(instance, ref rect);
            return rect;
        }

        /// <summary> 		
        /// Return the 2D center of mass of the hand in image space (in pixels).
        /// </summary>
        public PXCMPointF32 QueryMassCenterImage()
        {
            PXCMPointF32 point = new PXCMPointF32();
            PXCMHandData_IHand_QueryMassCenterImage(instance, ref point);
            return point;
        }

        /// <summary> 		
        /// Return the 3D center of mass of the hand in world space (in meters).
        /// </summary>
        public PXCMPoint3DF32 QueryMassCenterWorld()
        {
            PXCMPoint3DF32 point = new PXCMPoint3DF32();
            PXCMHandData_IHand_QueryMassCenterWorld(instance, ref point);
            return point;
        }


        /// <summary> 		
        /// A quaternion representing the global 3D orientation of the palm.
        /// @note This information is available only in full-hand tracking mode (TRACKING_MODE_FULL_HAND).
        /// @see PXCMHandConfiguration.SetTrackingMode
        /// </summary>
        public PXCMPoint4DF32 QueryPalmOrientation()
        {
            PXCMPoint4DF32 point = new PXCMPoint4DF32();
            PXCMHandData_IHand_QueryPalmOrientation(instance, ref point);
            return point;
        }

        /// <summary> 		
        /// Return the degree of openness of the hand.
        /// The possible degree values range from 0 (all fingers completely folded) to 100 (all fingers fully spread).
        /// @note This information is available only in full-hand tracking mode (TRACKING_MODE_FULL_HAND)
        /// @see PXCMHandConfiguration.SetTrackingMode
        /// </summary>
        public Int32 QueryOpenness()
        {
            return PXCMHandData_IHand_QueryOpenness(instance);
        }
        /// <summary> 		
        /// Return the palm radius in image space (number of pixels). 
        /// The palm radius is the radius of the minimal circle that contains the hand's palm.
        /// </summary>
        public float QueryPalmRadiusImage()
        {
            return PXCMHandData_IHand_QueryPalmRadiusImage(instance);
        }
        /// <summary> 		
        /// Return the palm radius in world space (meters).
        /// The palm radius is the radius of the minimal circle that contains the hand's palm.
        /// </summary>
        public float QueryPalmRadiusWorld()
        {
            return PXCMHandData_IHand_QueryPalmRadiusWorld(instance);
        }
        /// <summary>  Return the tracking status (a bit-mask of one or more TrackingStatusType enum values).
        /// @see TrackingStatusType 
        /// </summary>
        public Int32 QueryTrackingStatus()
        {
            return PXCMHandData_IHand_QueryTrackingStatus(instance);
        }

        /// <summary> 		
        /// Return the data of a specific extremity point
        /// </summary>
        /// <param name="extremityLabel"> - the id of the requested extremity point.</param>
        /// <param name="extremityPoint"> - the location data of the requested extremity point.</param>
        /// 
        /// <returns> PXC_STATUS_NO_ERROR - operation succeeded.</returns>
        /// 
        /// @see ExtremityType
        /// @see ExtremityData
        public pxcmStatus QueryExtremityPoint(ExtremityType extremityLabel, out ExtremityData extremityPoint)
        {
            return QueryExtremityPointINT(instance, extremityLabel, out extremityPoint);
        }

        /// <summary> 
        /// Return the data of the requested finger
        /// @note This information is available only in full-hand tracking mode (TRACKING_MODE_FULL_HAND)
        /// @see PXCHandConfiguration::SetTrackingMode
        /// </summary> 
        /// <param name="fingerLabel"> - the ID of the requested finger.</param>
        /// <param name="fingerData"> - the tracking data of the requested finger.</param>
        /// 
        /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded.</returns>
        /// 
        /// @see FingerType
        /// @see FingerData
        public pxcmStatus QueryFingerData(FingerType fingerLabel, out FingerData fingerData)
        {
            return QueryFingerDataINT(instance, fingerLabel, out fingerData);
        }

        /// <summary> 
        /// Return the tracking data of a single hand joint
        /// @note This information is available only in full-hand tracking mode (TRACKING_MODE_FULL_HAND), when tracked-joints are enabled.
        /// @see PXCHandConfiguration::SetTrackingMode
        /// @see PXCHandConfiguration::EnableTrackedJoints
        /// </summary> 
        /// <param name="jointLabel"> - the ID of the requested joint.</param>
        /// <param name="jointData"> - the tracking data of the requested hand joint.</param>
        /// 
        /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded.</returns>
        /// 
        /// @see JointType
        /// @see JointData
        public pxcmStatus QueryTrackedJoint(JointType jointLabel, out JointData jointData)
        {
            return QueryTrackedJointINT(instance, jointLabel, out jointData);
        }

        /// <summary> 
        /// Return the tracking data of a single normalized-hand joint.
        /// @note This information is available only in full-hand tracking mode, when normalized-skeleton is enabled.
        /// @see PXCHandConfiguration::SetTrackingMode
        /// @see PXCHandConfiguration::EnableNormalizedJoints
        /// </summary> 
        /// <param name="jointLabel"> - the ID of the requested joint.</param>
        /// <param name="jointData"> - the tracking data of the requested normalized-hand joint.</param>
        /// 
        /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded.</returns>
        /// 
        /// @see JointType
        /// @see JointData
        public pxcmStatus QueryNormalizedJoint(JointType jointLabel, out JointData jointData)
        {
            return QueryNormalizedJointINT(instance, jointLabel, out jointData);
        }

        /// <summary>			
        /// Retrieve the 2D image mask of the tracked hand. 	 
        /// In the image mask, each pixel occupied by the hand is white (value of 255) and all other pixels are black (value of 0).
        /// @note This information is available only when the segmentation image is enabled.
        /// @see PXCHandConfiguration::EnableSegmentationImage
        /// </summary>
        /// <param name="image"> - the 2D image mask.</param>
        /// 
        /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded.</returns>
        /// <returns> PXCM_STATUS_DATA_UNAVAILABLE - image mask is not available.		</returns>
        public pxcmStatus QuerySegmentationImage(out PXCMImage image)
        {
            IntPtr image2;
            pxcmStatus sts = PXCMHandData_IHand_QuerySegmentationImage(instance, out image2);
            image = (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR) ? new PXCMImage(image2, false) : null;
            return sts;
        }

        /// <summary> 
        ///  Return true/false if tracked joints data exists 
        /// @note This information is available only when full-hand tracking mode is enabled.
        /// @see PXCMHandConfiguration.SetTrackingMode
        /// @see PXCMHandConfiguration.EnableTrackedJoints
        /// </summary>
        public Boolean HasTrackedJoints()
        {
            return PXCMHandData_IHand_HasTrackedJoints(instance);
        }

        /// <summary> 
        ///  Return true/false if normalized joint data exists .
        /// @note This information is available only in full-hand tracking mode, when normalized-skeleton is enabled.
        /// @see PXCMHandConfiguration.SetTrackingMode
        /// @see PXCMHandConfiguration.EnableNormalizedJoints
        /// </summary>
        public Boolean HasNormalizedJoints()
        {
            return PXCMHandData_IHand_HasNormalizedJoints(instance);
        }

        /// <summary> 
        ///  Return true/false if hand segmentation image exists. 
        /// @see PXCMHandConfiguration.EnableSegmentationImage
        /// </summary>
        public Boolean HasSegmentationImage()
        {
            return PXCMHandData_IHand_HasSegmentationImage(instance);
        }


        private IntPtr instance;
        internal IHand(IntPtr instance)
        {
            this.instance = instance;
        }

        /// <summary> 
        /// Get the number of contour lines extracted (both external and internal).
        /// </summary>
        /// <returns> The number of contour lines extracted.</returns>
        public Int32 QueryNumberOfContours()
        {
            return PXCMHandData_IHand_QueryNumberOfContours(instance);
        }

        /// <summary>
        /// Retrieve an IContour object using index (that relates to the given order).
        /// </summary>
        /// <param name="index"> - the zero-based index of the requested contour (between 0 and QueryNumberOfContours()-1 ).</param>
        /// <param name="contourData"> - contains the extracted contour line data.</param>
        /// 
        /// <returns> PXCM_STATUS_NO_ERROR - successful operation.</returns>
        /// <returns> PXCM_STATUS_DATA_UNAVAILABLE  - index >= number of detected contours. </returns>
        /// 
        /// @see IContour       
        public pxcmStatus QueryContour(Int32 index, out PXCMHandData.IContour contourData)
        {
            IntPtr cd2;
            pxcmStatus sts = PXCMHandData_IHand_QueryContour(instance, index, out cd2);
            contourData = (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR) ? new IContour(cd2) : null;
            return sts;
        }


    };

    /* Interfaces */
    /// <summary> 
    /// @class IContour
    /// An interface that provides access to the contour line data.
    /// </summary>
    public partial class IContour
    {

        /// <summary> 
        /// Get the point array representing a contour line.
        /// </summary>
        /// <param name="maxSize"> - the size of the array allocated for the contour points.</param>
        /// <param name="contour"> - the contour points stored in the user-allocated array.</param>
        /// 
        /// <returns> PXCM_STATUS_NO_ERROR - successful operation.           </returns>
        public pxcmStatus QueryPoints(out PXCMPointI32[] contour)
        {
            return QueryDataINT(instance, out contour);
        }


        /// <summary> 
        /// Return true for the blob's outer contour; false for inner contours.
        /// <param name="index"> - the zero-based index of the requested contour.</param>
        /// <returns> true for the blob's outer contour; false for inner contours.</returns>
        /// </summary>
        public Boolean IsOuter()
        {
            return PXCMHandData_IContour_IsOuter(instance);
        }

        /// <summary> 
        /// Get the contour size (number of points in the contour line).
        /// This is the size of the points array that you should allocate.
        /// </summary>
        /// <param name="index"> - the zero-based index of the requested contour line.</param>
        /// <returns> The contour size (number of points in the contour line).</returns>
        public Int32 QuerySize()
        {
            return PXCMHandData_IContour_QuerySize(instance);
        }



        private IntPtr instance;
        internal IContour(IntPtr instance)
        {
            this.instance = instance;
        }
    };




    /* General */

    /// <summary>
    /// Updates hand data to the most current output.
    /// </summary>
    public pxcmStatus Update()
    {
        return PXCMHandData_Update(instance);
    }

    /* Alerts Outputs */

    /// <summary>
    /// Return the number of fired alerts in the current frame.
    /// </summary>
    public Int32 QueryFiredAlertsNumber()
    {
        return PXCMHandData_QueryFiredAlertsNumber(instance);
    }

    /// <summary> 
    /// Get the details of the fired alert with the given index.
    /// 
    /// <param name="index"> - the zero-based index of the requested fired alert.</param>
    /// <param name="alertData"> - the information for the fired event.</param>
    /// 
    /// @note the index is between 0 and the result of QueryFiredAlertsNumber()
    /// </summary>
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
    /// Return whether the specified alert is fired in the current frame, and retrieve its data if it is.
    /// </summary> 
    /// <param name="alertEvent"> - the ID of the fired event.</param>
    /// <param name="alertData"> - the information for the fired event.</param>
    /// 
    /// <returns> true if the alert is fired, false otherwise.</returns>
    /// 
    /// @see AlertType
    /// @see AlertData
    public Boolean IsAlertFired(AlertType alertEvent, out AlertData alertData)
    {
        return IsAlertFiredINT(instance, alertEvent, out alertData);
    }

    /// <summary>
    /// Return whether the specified alert is fired for a specific hand in the current frame, and retrieve its data.
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
        return PXCMHandData_QueryFiredGesturesNumber(instance);
    }

    /// <summary> 
    /// Get the details of the fired gesture with the given index.
    /// </summary> 
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
    /// Check whether a gesture was fired and if so return its details.
    /// </summary> 
    /// <param name="gestureName"> - the name of the gesture to be checked.</param>
    /// <param name="gestureData"> - the information for the fired gesture.</param>
    /// 
    /// <returns> true if the gesture was fired, false otherwise.</returns>
    /// 
    /// @see GestureData
    public Boolean IsGestureFired(String gestureName, out GestureData gestureData)
    {
        return IsGestureFiredINT(instance, gestureName, out gestureData);
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
    public Boolean IsGestureFiredByHand(String gestureName, Int32 handID, out GestureData gestureData)
    {
        return IsGestureFiredByHandINT(instance, gestureName, handID, out gestureData);
    }

    /* Hands Outputs */

    /// <summary> 
    /// Return the number of hands detected in the current frame.            
    /// </summary>
    public Int32 QueryNumberOfHands()
    {
        return PXCMHandData_QueryNumberOfHands(instance);
    }

    /// <summary> 
    /// Retrieve the given hand's uniqueId.		
    /// </summary>
    /// 
    /// <param name="accessOrder"> - the order in which the hands are enumerated (accessed).</param>
    /// <param name="index"> - the index of the hand to be retrieved, based on the given AccessOrder.</param>
    /// <param name="handId"> - the hand's uniqueId.</param>
    /// 
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded.</returns>
    /// <returns> PXCM_STATUS_PARAM_UNSUPPORTED - invalid parameter.</returns>
    /// 
    /// @see AccessOrderType		
    public pxcmStatus QueryHandId(AccessOrderType accessOrder, Int32 index, out Int32 handId)
    {
        return PXCMHandData_QueryHandId(instance, accessOrder, index, out handId);
    }

    /// <summary> 
    /// Retrieve the hand object data using a specific AccessOrder and related index.
    /// </summary>
    /// 
    /// <param name="accessOrder"> - the order in which the hands are enumerated (accessed).</param>
    /// <param name="index"> - the index of the hand to be retrieved, based on the given AccessOrder.</param>
    /// <param name="handData"> - the information for the hand.</param>
    /// 
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded.</returns>
    /// <returns> PXCM_STATUS_PARAM_UNSUPPORTED - index >= MAX_NUM_HANDS.</returns>
    /// <returns> PXCM_STATUS_DATA_UNAVAILABLE  - index >= number of detected hands.</returns>
    /// 
    /// @see AccessOrder
    /// @see IHand		
    public pxcmStatus QueryHandData(AccessOrderType accessOrder, Int32 index, out IHand handData)
    {
        IntPtr hd2;
        pxcmStatus sts = PXCMHandData_QueryHandData(instance, accessOrder, index, out hd2);
        handData = (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR) ? new IHand(hd2) : null;
        return sts;
    }

    /// <summary> 
    /// Retrieve the hand object data by its unique Id.
    /// </summary>
    /// <param name="handID"> - the unique ID of the requested hand</param>
    /// <param name="handData"> - the information for the hand.</param>
    /// 
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded.</returns>
    /// <returns> PXCM_STATUS_DATA_UNAVAILABLE  - there is no output hand data.</returns>
    /// <returns> PXCM_STATUS_PARAM_UNSUPPORTED - there is no hand data for the given hand ID. </returns>
    /// 
    /// @see IHand		
    public pxcmStatus QueryHandDataById(Int32 handId, out IHand handData)
    {
        IntPtr hd2;
        pxcmStatus sts = PXCMHandData_QueryHandDataById(instance, handId, out hd2);
        handData = (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR) ? new IHand(hd2) : null;
        return sts;
    }

    internal PXCMHandData(IntPtr instance, Boolean delete)
        : base(instance, delete)
    {
    }
};

#if RSSDK_IN_NAMESPACE
}
#endif
