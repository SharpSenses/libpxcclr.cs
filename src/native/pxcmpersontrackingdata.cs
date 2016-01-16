/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or non-disclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2015 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Runtime.InteropServices;
using System.Text;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

    public partial class PXCMPersonTrackingData : PXCMBase
    {

#if PT_MW_DEV
        public partial class PersonPose
        {
            [DllImport(DLLNAME)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern PositionInfo PXCMPersonTrackingData_PersonPose_QueryPositionState(IntPtr instance);

            [DllImport(DLLNAME)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern void PXCMPersonTrackingData_PersonPose_QueryLeanData(IntPtr instance, [Out] LeanInfo leanInfo);
        };
#endif

        public partial class PersonRecognition
	    {  
		    [DllImport(DLLNAME)]
            internal static extern Int32 PXCMPersonTrackingData_PersonRecognition_RegisterUser(IntPtr instance);

		    [DllImport(DLLNAME)]
            internal static extern void PXCMPersonTrackingData_PersonRecognition_UnregisterUser(IntPtr instance);

		    [DllImport(DLLNAME)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern Boolean PXCMPersonTrackingData_PersonRecognition_IsRegistered(IntPtr instance);

		    [DllImport(DLLNAME)]
            internal static extern Int32 PXCMPersonTrackingData_PersonRecognition_QueryRecognitionID(IntPtr instance);
	    };

        public partial class RecognitionModuleData
        {
            [DllImport(DLLNAME)]
            internal static extern Int32 PXCMPersonTrackingData_RecognitionModuleData_QueryDatabaseSize(IntPtr instance);

            [DllImport(DLLNAME)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern Boolean PXCMPersonTrackingData_RecognitionModuleData_QueryDatabaseBuffer(IntPtr instance, [Out] byte[] buffer);

            internal static Boolean QueryDatabaseBufferINT(IntPtr instance, out byte[] buffer)
            {
                Int32 size = PXCMPersonTrackingData_RecognitionModuleData_QueryDatabaseSize(instance);
                if (size > 0)
                {
                    buffer = new byte[size];
                    return PXCMPersonTrackingData_RecognitionModuleData_QueryDatabaseBuffer(instance, buffer);
                }
                else
                {
                    buffer = null;
                    return false;
                }
            }

            [DllImport(DLLNAME)]
            internal static extern void PXCMPersonTrackingData_RecognitionModuleData_UnregisterUserByID(IntPtr instance, Int32 userID);
        };

        public partial class PersonJoints
        {
            [DllImport(DLLNAME)]
            internal static extern Int32 PXCMPersonTrackingData_PersonJoints_QueryNumJoints(IntPtr instance);

#if PT_MW_DEV
            [DllImport(DLLNAME)]
            internal static extern Int32 PXCMPersonTrackingData_PersonJoints_QueryNumBones(IntPtr instance);

            [DllImport(DLLNAME)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern Boolean PXCMPersonTrackingData_PersonJoints_QueryJoints(IntPtr instance, IntPtr joints);

            internal static Boolean QueryJointsINT(IntPtr instance, out SkeletonPoint[] joints)
            {
                int njoints = PXCMPersonTrackingData_PersonJoints_QueryNumJoints(instance);
                IntPtr joints2 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SkeletonPoint)) * njoints);
                Boolean sts = PXCMPersonTrackingData_PersonJoints_QueryJoints(instance, joints2);
                if (sts)
                {
                    joints = new SkeletonPoint[njoints];
                    for (int i = 0, j = 0; i < njoints; i++, j += Marshal.SizeOf(typeof(SkeletonPoint)))
                    {
                        joints[i] = new SkeletonPoint();
                        Marshal.PtrToStructure(new IntPtr(joints2.ToInt64() + j), joints[i]);
                    }
                }
                else
                {
                    joints = null;
                }
                Marshal.FreeHGlobal(joints2);
                return sts;
            }

            [DllImport(DLLNAME)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern Boolean PXCMPersonTrackingData_PersonJoints_QueryBones(IntPtr instance, IntPtr bones);

            internal static Boolean QueryBonesINT(IntPtr instance, out Bone[] bones)
            {
                int nbones = PXCMPersonTrackingData_PersonJoints_QueryNumBones(instance);
                IntPtr bones2 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Bone)) * nbones);
                Boolean sts = PXCMPersonTrackingData_PersonJoints_QueryBones(instance, bones2);
                if (sts)
                {
                    bones = new Bone[nbones];
                    for (int i = 0, j = 0; i < nbones; i++, j += Marshal.SizeOf(typeof(Bone)))
                    {
                        bones[i] = new Bone();
                        Marshal.PtrToStructure(new IntPtr(bones2.ToInt64() + j), bones[i]);
                    }
                }
                else
                {
                    bones = null;
                }
                Marshal.FreeHGlobal(bones2);
                return sts;
            }
#endif
        };

        public partial class PersonTracking
        {
            [DllImport(DLLNAME)]
            internal static extern Int32 PXCMPersonTrackingData_PersonTracking_QueryId(IntPtr instance);
            
            [DllImport(DLLNAME)]
            internal static extern void PXCMPersonTrackingData_PersonTracking_Query2DBoundingBox(IntPtr instance, [Out] BoundingBox2D box);

            internal BoundingBox2D Query2DBoundingBoxINT(IntPtr instance)
            {
                BoundingBox2D box = new BoundingBox2D();
                PXCMPersonTrackingData_PersonTracking_Query2DBoundingBox(instance, box);
                return box;
            }

            [DllImport(DLLNAME)]
            internal static extern IntPtr PXCMPersonTrackingData_PersonTracking_QuerySegmentationImage(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern PointCombined PXCMPersonTrackingData_PersonTracking_QueryCenterMass(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern void PXCMPersonTrackingData_PersonTracking_QueryHeadBoundingBox(IntPtr instance, [Out] BoundingBox2D box);

            internal BoundingBox2D QueryHeadBoundingBoxINT(IntPtr instance)
            {
                BoundingBox2D box = new BoundingBox2D();
                PXCMPersonTrackingData_PersonTracking_QueryHeadBoundingBox(instance, box);
                return box;
            }

#if PT_MW_DEV
            [DllImport(DLLNAME)]
            internal static extern void PXCMPersonTrackingData_PersonTracking_Query3DBoundingBox(IntPtr instance, [Out] BoundingBox3D box);

            internal BoundingBox3D Query3DBoundingBoxINT(IntPtr instance)
            {
                BoundingBox3D box = new BoundingBox3D();
                PXCMPersonTrackingData_PersonTracking_Query3DBoundingBox(instance, box);
                return box;
            }

            [DllImport(DLLNAME)]
            internal static extern void PXCMPersonTrackingData_PersonTracking_QuerySpeed(IntPtr instance, ref SpeedInfo speed);

            [DllImport(DLLNAME)]
            internal static extern Int32 PXCMPersonTrackingData_PersonTracking_Query3DBlobPixelCount(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern pxcmStatus PXCMPersonTrackingData_PersonTracking_Query3DBlob(IntPtr instance, ref PXCMPoint3DF32 blob);

            [DllImport(DLLNAME)]
            internal static extern Int32 PXCMPersonTrackingData_PersonTracking_QueryContourSize(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern pxcmStatus PXCMPersonTrackingData_PersonTracking_QueryContourPoints(IntPtr instance, ref PXCMPointI32 contour);
#endif
        };

        public partial class Person
        {
            [DllImport(DLLNAME)]
            internal static extern IntPtr PXCMPersonTrackingData_Person_QueryTracking(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern IntPtr PXCMPersonTrackingData_Person_QueryRecognition(IntPtr instance);

#if PT_MW_DEV
            [DllImport(DLLNAME)]
            internal static extern IntPtr PXCMPersonTrackingData_Person_QuerySkeletonJoints(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern IntPtr PXCMPersonTrackingData_Person_QueryPose(IntPtr instance);
#endif
        };

        [DllImport(DLLNAME)]
        internal static extern Int32 PXCMPersonTrackingData_QueryNumberOfPeople(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern IntPtr PXCMPersonTrackingData_QueryPersonData(IntPtr instance, AccessOrderType accessOrder, Int32 index);

        internal Person QueryPersonDataINT(IntPtr instance, AccessOrderType accessOrder, Int32 index)
        {
            IntPtr person2 = PXCMPersonTrackingData_QueryPersonData(instance, accessOrder, index);
            return (person2 != IntPtr.Zero) ? new Person(person2) : null;
        }

        [DllImport(DLLNAME)]
        internal static extern IntPtr PXCMPersonTrackingData_QueryPersonDataById(IntPtr instance, Int32 personID);

        internal Person QueryPersonDataByIdINT(IntPtr instance, Int32 personID)
        {
            IntPtr person2 = PXCMPersonTrackingData_QueryPersonDataById(instance, personID);
            return (person2 != IntPtr.Zero) ? new Person(person2) : null;
        }

        [DllImport(DLLNAME)]
        internal static extern void PXCMPersonTrackingData_StartTracking(IntPtr instance, Int32 personID);

        [DllImport(DLLNAME)]
        internal static extern void PXCMPersonTrackingData_StopTracking(IntPtr instance, Int32 personID);


        [DllImport(DLLNAME)]
        internal static extern TrackingState PXCMPersonTrackingData_GetTrackingState(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern IntPtr PXCMPersonTrackingData_QueryRecognitionModuleData(IntPtr instance);

#if PT_MW_DEV
        [DllImport(DLLNAME)]
        internal static extern Int32 PXCMPersonTrackingData_QueryFiredAlertsNumber(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMPersonTrackingData_QueryFiredAlertData(IntPtr instance, Int32 index, ref AlertData alertData);

        [DllImport(DLLNAME)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern Boolean PXCMPersonTrackingData_IsAlertFired(IntPtr instance, AlertType alertType, ref AlertData alertData);
#endif

    };

#if RSSDK_IN_NAMESPACE
}
#endif

