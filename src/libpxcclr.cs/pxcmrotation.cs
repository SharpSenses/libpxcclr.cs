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
namespace intel.rssdk
{
#endif

/// <summary> @class PXCMRotation
/// A class factory that allows the creation of a Rotation instance using different input representations. 
/// Available representations: Quaternions, Euler Angles, Rotations Matrices, Axis + Angle.     
/// 
/// example code sample - create Rotation instance from quaternion and get Euler angles representation.
/// 
/// PXCPoint4DF32 quaternion; // You should set the quaternion
/// 
/// Create PXCMSession instance
/// PXCMSession session = PXCMSession_Create();
/// 
/// Create PXCMRotation instance
/// PXCMRotation rotation;
/// sessionCreateImpl<PXCMRotation>(rotation);
/// 
/// //Set rotation from quaternion
/// rotation.SetFromQuaternion(quaternion)
/// 
/// Query rotation in Euler angles representation
/// PXCMPoint3DF32 eulerAngles = rotation.QueryEulerAngles();
/// </summary>

public partial class PXCMRotation : PXCMBase
{
    new public const Int32 CUID = unchecked((Int32)0x53544f52);

    /// <summary> 
    /// @class AngleAxis
    /// Rotation in Angle-Axis representation. Based on a rotation angle (RADIANS) around an axis
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class AngleAxis
    {
        public PXCMPoint3DF32 axis;
        public Single angle;

        public AngleAxis()
        {
            axis = new PXCMPoint3DF32();
            angle = 0;
        }
    };

    /// <summary> 
    /// EulerOrder
    /// EulerOrder indicates the order in which to get the Euler angles.
    /// This order matters. (ROLL_PITCH_YAW !=  ROLL_YAW_PITCH)
    /// Roll, Pitch and Yaw are the angles of rotation around the x, y and z axis accordingly.
    /// </summary>
    [Flags]
    public enum EulerOrder
    {
        ROLL_PITCH_YAW,
        ROLL_YAW_PITCH,
        PITCH_YAW_ROLL,
        PITCH_ROLL_YAW,
        YAW_ROLL_PITCH,
        YAW_PITCH_ROLL
    };


    // Query representations
    /// <summary>			
    /// get rotation in Euler angles representation.
    /// Euler angles are a 3D point that represents rotation in 3D. Each variable is the angle 
    /// of rotation around a certain axis (x/y/z). 
    /// </summary>		 
    /// <param name="order"> the order in which we get the Euler angles (ROLL_PITCH_YAW as default)</param>
    /// <returns> 3D point containing Euler angles (RADIANS) in the given order.</returns>
    public PXCMPoint3DF32 QueryEulerAngles(EulerOrder order)
    {
        return PXCMRotation_QueryEulerAngles(instance, order);
    }

    public PXCMPoint3DF32 QueryEulerAngles()
    {
        return QueryEulerAngles(PXCMRotation.EulerOrder.PITCH_YAW_ROLL);
    }

    /// <summary>			
    /// get rotation in quaternion representation.
    /// Quaternion is a 4D point that represents rotation in 3D.
    /// </summary>	
    /// <returns> 4D point containing a quaternion representation (w,x,y,z)</returns>
    public PXCMPoint4DF32 QueryQuaternion()
    {
        return PXCMRotation_QueryQuaternion(instance);
    }

    /// <summary>			.
    /// get rotation matrix representation.
    /// @param rotation matrix - 3x3 float array, containing the rotation matrix
    /// </summary>	
    public void QueryRotationMatrix(Single[,] rotationMatrix)
    {
        PXCMRotation_QueryRotationMatrix(instance, rotationMatrix);
    }

    /// <summary>			
    /// get rotation in angle-axis representation.
    /// angle-axis represents rotation (angle in RADIANS) around an axis
    /// </summary>	
    /// <returns> AngleAxis struct containing an axis and angle of rotation around this axis.</returns>
    public AngleAxis QueryAngleAxis()
    {
        return PXCMRotation_QueryAngleAxis(instance);
    }


    /* Query roll/pitch/yaw equivalents */

    /// <summary>			
    /// get roll - angle of rotation around z axis using ROLL_PITCH_YAW eulerOrder. 
    /// </summary>		
    /// <returns> roll - angle of rotation around the z axis 		</returns>
    public Single QueryRoll()
    {
        return PXCMRotation_QueryRoll(instance);
    }

    /// <summary>			
    /// get pitch - angle of rotation around x axis using ROLL_PITCH_YAW eulerOrder. 
    /// <returns> pitch - angle of rotation around the x axis 		</returns>
    /// </summary>		
    public Single QueryPitch()
    {
        return PXCMRotation_QueryPitch(instance);
    }

    /// <summary>			
    /// get yaw - angle of rotation around y axis using ROLL_PITCH_YAW eulerOrder. 
    /// </summary>	
    /// <returns> pitch - angle of rotation around the y axis 		</returns>
    public Single QueryYaw()
    {
        return PXCMRotation_QueryYaw(instance);
    }


    /* Useful functions */

    /// <summary>			
    /// Set rotation as a concatenation of current rotation and the given Rotation.
    /// </summary>	
    /// <param name="rotation"> - the given rotation</param>
    public void Rotate(PXCMRotation rotation)
    {
        PXCMRotation_Rotate(instance, rotation.instance);
    }

    /// <summary>			
    /// Get rotated vector according to current rotation.
    /// <param name="vec"> - the vector we want to rotate</param>
    /// <returns> rotated vector according to current rotation.</returns>
    /// </summary>	
    public PXCMPoint3DF32 RotateVector(PXCMPoint3DF32 vec)
    {
        return PXCMRotation_RotateVector(instance, vec);
    }


    /* Set rotation from each of the representations. */

    /// <summary>			
    /// Set rotation based on a quaternion.
    /// Quaternion is a 4D point that represents rotation in 3D.
    /// </summary>	
    /// <param name="quaternion"> rotation in quaternion representation.</param>
    public void SetFromQuaternion(PXCMPoint4DF32 quaternion)
    {
        PXCMRotation_SetFromQuaternion(instance, quaternion);
    }

    /// <summary>			
    /// Set rotation based on Euler angles representation.
    /// Euler angles are a 3D point that represents rotation in 3D. Each variable is the angle 
    /// of rotation around a certain axis (x/y/z). 
    /// </summary>
    /// <param name="eulerAngles"> the rotation in Euler angles representation.</param>
    /// <param name="order"> the order in which we set the rotation (ROLL_PITCH_YAW as default).</param>
    public void SetFromEulerAngles(PXCMPoint3DF32 eulerAngles, PXCMRotation.EulerOrder order)
    {
        PXCMRotation_SetFromEulerAngles(instance, eulerAngles, order);
    }

    /// <summary>			
    /// Set rotation based on a 3x3 rotation matrix.
    /// Note that only rotation (not scale or translation) is taken into acount from the rotation matrix.
    /// That is, two matrices with the same rotation will yield the same Rotation instance regardless of their inequality.
    /// </summary>		
    /// <param name="rotationMatrix"> rotation in rotation matrix representation.</param>
    public void SetFromRotationMatrix(Single[,] rotationMatrix)
    {
        PXCMRotation_SetFromRotationMatrix(instance, rotationMatrix);
    }

    /// <summary>			
    /// Set rotation based on a rotation angle(RADIANS) around an axis.
    /// angle-axis represents rotation (angle in RADIANS) around an axis
    /// <param name="angle"> rotation angle (RADIANS).</param>
    /// <param name="axis"> rotation around this axis.</param>
    /// </summary>		
    public void SetFromAngleAxis(Single angle, PXCMPoint3DF32 axis)
    {
        PXCMRotation_SetFromAngleAxis(instance, angle, axis);
    }

    /// <summary>			
    /// Set rotation from Spherical linear interpolation between two rotations.
    /// </summary>	
    /// <param name="startRotation"> - start rotation</param>
    /// <param name="endRotation"> - end rotation</param>
    /// <param name="factor"> - interpolation factor</param>
    public void SetFromSlerp(Single factor, PXCMRotation startRotation, PXCMRotation endRotation)
    {
        PXCMRotation_SetFromSlerp(instance, factor, startRotation.instance, endRotation.instance);
    }

    /// <summary>
    /// constructors and misc
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="delete"></param>
    internal PXCMRotation(IntPtr instance, Boolean delete)
        : base(instance, delete)
    {
    }

};
