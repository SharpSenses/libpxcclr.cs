/*******************************************************************************

  INTEL CORPORATION PROPRIETARY INFORMATION
  This software is supplied under the terms of a license agreement or non-disclosure
  agreement with Intel Corporation and may not be copied or disclosed except in
  accordance with the terms of that agreement
  Copyright(c) 2015 Intel Corporation. All Rights Reserved.

 *******************************************************************************/
using System;
using System.Runtime.InteropServices;


#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

public partial class PXCMPersonTrackingData : PXCMBase
{
    new public const Int32 CUID = 0x44544f50;

    /// <summary>  
    /// AlertType
    /// Identifiers for the events that can be detected and fired by the person module.
    /// </summary>
    [Flags]
    public enum AlertType
    {
        ALERT_PERSON_DETECTED = 0x0001,
        ALERT_PERSON_NOT_DETECTED = 0x0002,
        ALERT_PERSON_OCCLUDED = 0X0004,
        ALERT_PERSON_TOO_CLOSE = 0X0008,
        ALERT_PERSON_TOO_FAR = 0X0010
    };

    /// <summary> 
    /// AccessOrderType
    /// Orders in which the person can be accessed.
    /// </summary>
    [Flags]
    public enum AccessOrderType
    {
        ACCESS_ORDER_BY_ID = 0,			/// By unique ID of the person
#if PT_MW_DEV
						   ACCESS_ORDER_BY_TIME, 			/// From oldest to newest person in the scene           
						   ACCESS_ORDER_NEAR_TO_FAR,		/// From nearest to farthest person in scene
						   ACCESS_ORDER_LEFT_TO_RIGHT	    /// Ordered from left to right in scene
#endif
    };

    /// <summary>
    /// TrackingState
    /// The current state of the module, either tracking specific people or performing full detection
    /// </summary>
    [Flags]
    public enum TrackingState
    {
        TRACKING_STATE_TRACKING = 0,
        TRACKING_STATE_DETECTING
    };


    [StructLayout(LayoutKind.Sequential)]
    public class AlertData
    {
        public AlertType label;	    /// The type of alert
        public Int32 personId;	    /// The ID of the person that triggered the alert, if relevant and known
        public Int64 timeStamp;		/// The time-stamp in which the event occurred
        public Int32 frameNumber;   /// The number of the frame in which the event occurred (relevant for recorded sequences)
    }


    [Serializable]
    [StructLayout(LayoutKind.Sequential, Size = 40)]
    internal struct Reserved
    {
        internal Int32 res1, res2, res3, res4, res5, res6, res7, res8, res9, res10;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class PoseEulerAngles
    {
        public Single yaw;
        public Single pitch;
        public Single roll;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        internal Int32[] reserved;

        public PoseEulerAngles()
        {
            reserved = new Int32[10];
        }
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class BoundingBox2D
    {
        public PXCMRectI32 rect;
        public Int32 confidence;
    };


#if PT_MW_DEV
		public partial class PersonPose
		{
			/// <summary> 
			/// Position
			/// Describes the position of the person
			/// </summary>
			[Flags]
				public enum PositionState
				{
					POSITION_LYING_DOWN = 0,
							    POSITION_SITTING,
							    POSITION_CROUCHING,
							    POSITION_STANDING,
							    POSITION_JUMPING
				};

			[Serializable]
				[StructLayout(LayoutKind.Sequential)]
				public class PositionInfo
				{
					public PositionState position;
					public Int32 confidence;
				};

			[Serializable]
				[StructLayout(LayoutKind.Sequential)]
				public class LeanInfo
				{
					public PoseEulerAngles leanData;
					public Int32 confidence;
				};

			/// <summary>
			/// Get the person's position data
			/// </summary>
            /// <param name="position"> describes the person's position</param>
			/// <param name="confidence"> is the algorithm's confidence in the determined position</param>
			public void QueryPositionState(PositionInfo position)
			{
				position = PXCMPersonTrackingData_PersonPose_QueryPositionState(instance);
			}

			/// <summary>
			/// Get the direction a person is leaning towards
			/// </summary>
			/// <param name="leanData"> describes where the person is leaning to in terms of yaw, pitch, and roll</param>
			/// <param name="confidence"> is the algorithm's confidence in the determined orientation</param>
			public void QueryLeanData(LeanInfo leanInfo)
			{
				PXCMPersonTrackingData_PersonPose_QueryLeanData(instance, leanInfo);
			}

			private IntPtr instance;
			public PersonPose(IntPtr instance)
			{
				this.instance = instance;
			}
		}
#endif

    public partial class PersonRecognition
    {
        /// <summary>
        /// Register a user in the Recognition database.
        /// </summary>
        /// <returns> The unique user ID assigned to the registered recognition by the Recognition module.</returns>
        public Int32 RegisterUser()
        {
            return PXCMPersonTrackingData_PersonRecognition_RegisterUser(instance);
        }

        /// <summary>
        /// Removes a user from the Recognition database.
        /// </summary>
        public void UnregisterUser()
        {
            PXCMPersonTrackingData_PersonRecognition_UnregisterUser(instance);
        }

        /// <summary>
        /// Checks if a user is registered in the Recognition database.
        /// </summary>
        /// <returns> true - if user is in the database, false otherwise.</returns>
        public Boolean IsRegistered()
        {
            return PXCMPersonTrackingData_PersonRecognition_IsRegistered(instance);
        }

        /// <summary>
        /// Returns the ID assigned to the current recognition by the Recognition module
        /// </summary>
        /// <returns> The ID assigned by the Recognition module, or -1 if person was not recognized.</returns>
        public Int32 QueryRecognitionID()
        {
            return PXCMPersonTrackingData_PersonRecognition_QueryRecognitionID(instance);
        }

        private IntPtr instance;
        public PersonRecognition(IntPtr instance)
        {
            this.instance = instance;
        }
    };

    public partial class RecognitionModuleData
    {
        /// <summary>
        /// Retrieves the size of the recognition database for the user to be able to allocate the db buffer in the correct size
        /// </summary>
        /// <returns> The size of the database in bytes.</returns>
        public Int32 QueryDatabaseSize()
        {
            return PXCMPersonTrackingData_RecognitionModuleData_QueryDatabaseSize(instance);
        }

        /// <summary>
        /// Copies the recognition database buffer to the user. Allows user to store it for later usage.
        /// </summary>
        /// <param name="buffer"> A user allocated buffer to copy the database into. The user must make sure the buffer is large enough (can be determined by calling QueryDatabaseSize()).</param>
        /// <returns> true if database has been successfully copied to db. false - otherwise.</returns>
        public Boolean QueryDatabaseBuffer(byte[] buffer)
        {
            return QueryDatabaseBufferINT(instance, buffer);
        }

        /// <summary>
        /// Unregisters a user from the database by user ID
        /// <param name="the"> ID of the user to unregister</param>
        /// </summary>
        public void UnregisterUserByID(Int32 userID)
        {
            PXCMPersonTrackingData_RecognitionModuleData_UnregisterUserByID(instance, userID);
        }

        private IntPtr instance;
        public RecognitionModuleData(IntPtr instance)
        {
            this.instance = instance;
        }
    };

    public partial class PersonJoints
    {
        /// <summary> 
        /// Position
        /// Describes the position of the person
        /// </summary>
        [Flags]
        public enum JointType
        {
            JOINT_ANKLE_LEFT,
            JOINT_ANKLE_RIGHT,
            JOINT_ELBOW_LEFT,
            JOINT_ELBOW_RIGHT,
            JOINT_FOOT_LEFT,
            JOINT_FOOT_RIGHT,
            JOINT_HAND_LEFT,
            JOINT_HAND_RIGHT,
            JOINT_HAND_TIP_LEFT,
            JOINT_HAND_TIP_RIGHT,
            JOINT_HEAD,
            JOINT_HIP_LEFT,
            JOINT_HIP_RIGHT,
            JOINT_KNEE_LEFT,
            JOINT_KNEE_RIGHT,
            JOINT_NECK,
            JOINT_SHOULDER_LEFT,
            JOINT_SHOULDER_RIGHT,
            JOINT_SPINE_BASE,
            JOINT_SPINE_MID,
            JOINT_SPINE_SHOULDER,
            JOINT_THUMB_LEFT,
            JOINT_THUMB_RIGHT,
            JOINT_WRIST_LEFT,
            JOINT_WRIST_RIGHT
        };


        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public class SkeletonPoint
        {
            public JointType jointType;
            public Int32 confidenceImage;
            public Int32 confidenceWorld;
            public PXCMPoint3DF32 world;
            public PXCMPointF32 image;
            public Int32[] reserved;

            public SkeletonPoint()
            {
                reserved = new Int32[10];
            }
        };

        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public class Bone
        {
            public JointType startJoint;
            public JointType endJoint;
            public PoseEulerAngles orientation;
            public Int32 orientationConfidence;
            public Int32[] reserved;

            public Bone()
            {
                reserved = new Int32[10];
            }
        };

        /// <summary>
        /// Returns the number of tracked joints
        /// </summary>
        public Int32 QueryNumJoints()
        {
            return PXCMPersonTrackingData_PersonJoints_QueryNumJoints(instance);
        }


#if PT_MW_DEV
			/// <summary>
			/// Retrieves all joints
			/// </summary>
            /// <param name="joints"> the joints' locations are copied into this array. The application is expected to allocate this array (size retrieved from QueryNumJoints())</param>
			/// Returns true if data and parameters exists, false otherwise.
			public Boolean QueryJoints(SkeletonPoint[] joints)
			{
				return QueryJointsINT(instance, out joints);
			}

			/// <summary>
			/// Returns the number of tracked bones
			/// </summary>
			public Int32 QueryNumBones()
			{
				return PXCMPersonTrackingData_PersonJoints_QueryNumBones(instance);
			}

			/// <summary>
			/// Retrieves all bones
			/// </summary>
            /// <param name="bones"> the bones' locations are copied into this array. The application is expected to allocate this array (size retrieved from QueryNumBones())</param>
			/// Returns true if data and parameters exists, false otherwise.
			public Boolean QueryBones(Bone[] bones)
			{
				return QueryBonesINT(instance, out bones);
			}
#endif

        private IntPtr instance;
        public PersonJoints(IntPtr instance)
        {
            this.instance = instance;
        }
    }


    public partial class PersonTracking
    {
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public class BoundingBox3D
        {
            public PXCMBox3DF32 box;
            public Int32 confidence;
        };

        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public class SpeedInfo
        {
            public PXCMPoint3DF32 direction;
            public Single magnitude;     // GZ: --> should be lower case in API
        };

        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public class PointInfo
        {
            public PXCMPoint3DF32 point;
            public Int32 confidence;
        };

        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public class PointCombined
        {
            public PointInfo image;
            public PointInfo world;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            internal Int32[] reserved;

            public PointCombined()
            {
                reserved = new Int32[10];
            }
        };

        /// <summary>	
        /// Return the person's unique identifier.
        /// </summary>
        public Int32 QueryId()
        {
            return PXCMPersonTrackingData_PersonTracking_QueryId(instance);
        }

        /// <summary>	
        /// Return the location and dimensions of the tracked person, represented by a 2D bounding box (defined in pixels).
        /// </summary>
        public BoundingBox2D Query2DBoundingBox()
        {
            return Query2DBoundingBoxINT(instance);
        }


        /// <summary>			
        /// Retrieve the 2D image mask of the tracked person. 	 			
        /// </summary>
        public PXCMImage QuerySegmentationImage()
        {
            IntPtr image2 = PXCMPersonTrackingData_PersonTracking_QuerySegmentationImage(instance);
            return (image2 != IntPtr.Zero) ? new PXCMImage(image2, false) : null;
        }


        /// <summary> 
        /// Retrieves the center mass of the tracked person
        /// </summary>
        /// <param name="centerMass"> The center mass of the tracked person in world coordinates</param>
        /// <param name="confidence"> The confidence of the calculated center mass location</param>
        public PointCombined QueryCenterMass()
        {
            return QueryCenterMassINT(instance);
        }

        /// <summary>	
        /// Return the location and dimensions of the tracked person's head, represented by a 2D bounding box (defined in pixels).
        /// </summary>
        public BoundingBox2D QueryHeadBoundingBox()
        {
            return QueryHeadBoundingBoxINT(instance);
        }

        public PXCMImage QueryBlobMask()
        {
            IntPtr image = PXCMPersonTrackingData_PersonTracking_QueryBlobMask(instance);
            return image != IntPtr.Zero ? new PXCMImage(image, true) : null;
        }


#if PT_MW_DEV
			/// <summary>
			/// Return the location and dimensions of the tracked person, represented by a 3D bounding box.
			/// </summary>
			public BoundingBox3D Query3DBoundingBox()
			{
				return Query3DBoundingBoxINT(instance);
			}

			/// <summary>
			/// Return the speed of person in 3D world coordinates
			/// </summary>
            /// <param name="direction"> the direction of the movement</param>
			/// <param name="magnitude"> the magnitude of the movement in meters/second</param>
			public void QuerySpeed(SpeedInfo speed)
			{
				PXCMPersonTrackingData_PersonTracking_QuerySpeed(instance, ref speed);
			}

			/// <summary> 
			/// Get the number of pixels in the blob
			/// </summary>
			public Int32 Query3DBlobPixelCount()
			{
				return PXCMPersonTrackingData_PersonTracking_Query3DBlobPixelCount(instance);
			}

			/// <summary>
			/// Retrieves the 3d blob of the tracked person
			/// </summary>
            /// <param name="blob"> The array of 3d points to which the blob will be copied. Must be allocated by the application</param>
			public pxcmStatus Query3dBlob(PXCMPoint3DF32 blob)
			{
				return PXCMPersonTrackingData_PersonTracking_Query3DBlob(instance, ref blob);
			}

			/// <summary> 
			/// Get the contour size (number of points in the contour)
			/// </summary>
			public Int32 QueryContourSize()
			{
				return PXCMPersonTrackingData_PersonTracking_QueryContourSize(instance);
			}

			/// <summary> 
			/// Get the data of the contour line
			/// </summary>
			public pxcmStatus QueryContourPoints(PXCMPointI32 contour)
			{
				return PXCMPersonTrackingData_PersonTracking_QueryContourPoints(instance, ref contour);
			}
#endif

        private IntPtr instance;
        public PersonTracking(IntPtr instance)
        {
            this.instance = instance;
        }
    }

    public partial class Person
    {
        /// <summary>
        /// Returns the Person Detection interface
        /// </summary>
        public PersonTracking QueryTracking()
        {
            IntPtr instance2 = PXCMPersonTrackingData_Person_QueryTracking(instance);
            return instance2 == IntPtr.Zero ? null : new PersonTracking(instance2);
        }

        /// <summary>
        /// Returns the Person Recognition interface
        /// </summary>
        public PersonRecognition QueryRecognition()
        {
            IntPtr instance2 = PXCMPersonTrackingData_Person_QueryRecognition(instance);
            return instance2 == IntPtr.Zero ? null : new PersonRecognition(instance2);
        }


#if PT_MW_DEV
			/// <summary>
			/// Returns the Person Joints interface
			/// </summary>
			public PersonJoints QuerySkeletonJoints()
			{
				IntPtr instance2 = PXCMPersonTrackingData_Person_QuerySkeletonJoints(instance);
				return instance2 == IntPtr.Zero ? null : new PersonJoints(instance2);
			}

			/// <summary>
			/// Returns the Person Pose interface
			/// </summary>
			public PersonPose QueryPose()
			{
				IntPtr instance2 = PXCMPersonTrackingData_Person_QueryPose(instance);
				return instance2 == IntPtr.Zero ? null : new PersonPose(instance2);
			}
#endif


        private IntPtr instance;
        public Person(IntPtr instance)
        {
            this.instance = instance;
        }
    }


    /// <summary>
    ///  Return the number of persons detected in the current frame.
    /// </summary>
    public Int32 QueryNumberOfPeople()
    {
        return PXCMPersonTrackingData_QueryNumberOfPeople(instance);
    }

    /// <summary>
    /// Retrieve the person object data using a specific AccessOrder and related index.
    /// </summary>
    public Person QueryPersonData(AccessOrderType accessOrder, Int32 index)
    {
        return QueryPersonDataINT(instance, accessOrder, index);
    }

    /// <summary>
    /// Retrieve the person object data by its unique Id.
    /// </summary>
    public Person QueryPersonDataById(Int32 personID)
    {
        return QueryPersonDataByIdINT(instance, personID);
    }


    /// <summary>
    /// Enters the person to the list of tracked people starting from next frame
    /// </summary>
    public void StartTracking(Int32 personID)
    {
        PXCMPersonTrackingData_StartTracking(instance, personID);
    }

    /// <summary>
    /// Removes the person from tracking
    /// </summary>
    public void StopTracking(Int32 personID)
    {
        PXCMPersonTrackingData_StopTracking(instance, personID);
    }

    /// <summary>
    /// Retrieve current tracking state of the Person Tracking module
    /// </summary>
    public TrackingState GetTrackingState()
    {
        return PXCMPersonTrackingData_GetTrackingState(instance);
    }

    /// <summary>
    /// Returns the Person Recognition module interface
    /// </summary>
    public RecognitionModuleData QueryRecognitionModuleData()
    {
        IntPtr rmd2 = PXCMPersonTrackingData_QueryRecognitionModuleData(instance);
        return (rmd2 != IntPtr.Zero) ? new RecognitionModuleData(rmd2) : null;
    }


#if PT_MW_DEV
		/// <summary>
		/// Return the number of fired alerts in the current frame.
		/// </summary>
		public Int32 QueryFiredAlertsNumber()
		{
		return PXCMPersonTrackingData_QueryFiredAlertsNumber(instance);
		}

		/// <summary>
		/// @Get the details of the fired alert with the given index.
		/// </summary>
		public pxcmStatus QueryFiredAlertData(Int32 index, AlertData alertData)
		{
		return PXCMPersonTrackingData_QueryFiredAlertData(instance, index, ref alertData);
		}

		/// <summary>
		/// Return whether the specified alert is fired in the current frame, 
		/// and retrieve its data if it is.		
		/// </summary>
		public Boolean IsAlertFired(AlertType alertEvent, AlertData alertData)
		{
		return PXCMPersonTrackingData_IsAlertFired(instance, alertEvent, ref alertData);
		}
#endif

    internal PXCMPersonTrackingData(IntPtr instance, Boolean delete)
        : base(instance, delete)
    {
    }

}

#if RSSDK_IN_NAMESPACE
}
#endif
