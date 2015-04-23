using System;
using System.Runtime.InteropServices;

public class PXCMRotation : PXCMBase
{
  public new const int CUID = 1398034258;

  internal PXCMRotation(IntPtr instance, bool delete)
    : base(instance, delete)
  {
  }

  [DllImport("libpxccpp2c")]
  internal static extern PXCMPoint3DF32 PXCMRotation_QueryEulerAngles(IntPtr instance, EulerOrder order);

  [DllImport("libpxccpp2c")]
  internal static extern PXCMPoint4DF32 PXCMRotation_QueryQuaternion(IntPtr instance);

  [DllImport("libpxccpp2c")]
  internal static extern void PXCMRotation_QueryRotationMatrix(IntPtr instance, [Out] float[,] rotationMatrix);

  [DllImport("libpxccpp2c")]
  internal static extern AngleAxis PXCMRotation_QueryAngleAxis(IntPtr instance);

  [DllImport("libpxccpp2c")]
  internal static extern float PXCMRotation_QueryRoll(IntPtr instance);

  [DllImport("libpxccpp2c")]
  internal static extern float PXCMRotation_QueryYaw(IntPtr instance);

  [DllImport("libpxccpp2c")]
  internal static extern float PXCMRotation_QueryPitch(IntPtr instance);

  [DllImport("libpxccpp2c")]
  internal static extern void PXCMRotation_Rotate(IntPtr instance, IntPtr rotation_instance);

  [DllImport("libpxccpp2c")]
  internal static extern PXCMPoint3DF32 PXCMRotation_RotateVector(IntPtr instance, PXCMPoint3DF32 vec);

  [DllImport("libpxccpp2c")]
  internal static extern void PXCMRotation_SetFromQuaternion(IntPtr instance, PXCMPoint4DF32 quaternion);

  [DllImport("libpxccpp2c")]
  internal static extern void PXCMRotation_SetFromEulerAngles(IntPtr instance, PXCMPoint3DF32 eulerAngles);

  [DllImport("libpxccpp2c")]
  internal static extern void PXCMRotation_SetFromRotationMatrix(IntPtr instance, float[,] rotationMatrix);

  [DllImport("libpxccpp2c")]
  internal static extern void PXCMRotation_SetFromAngleAxis(IntPtr instance, float angle, PXCMPoint3DF32 axis);

  [DllImport("libpxccpp2c")]
  internal static extern void PXCMRotation_SetFromSlerp(IntPtr instance, float factor, IntPtr startRotation_instance, IntPtr endRotation_instance);

  public PXCMPoint3DF32 QueryEulerAngles(EulerOrder order)
  {
    return PXCMRotation_QueryEulerAngles(instance, order);
  }

  public PXCMPoint3DF32 QueryEulerAngles()
  {
    return QueryEulerAngles(EulerOrder.PITCH_YAW_ROLL);
  }

  public PXCMPoint4DF32 QueryQuaternion()
  {
    return PXCMRotation_QueryQuaternion(instance);
  }

  public void QueryRotationMatrix(float[,] rotationMatrix)
  {
    PXCMRotation_QueryRotationMatrix(instance, rotationMatrix);
  }

  public AngleAxis QueryAngleAxis()
  {
    return PXCMRotation_QueryAngleAxis(instance);
  }

  public float QueryRoll()
  {
    return PXCMRotation_QueryRoll(instance);
  }

  public float QueryPitch()
  {
    return PXCMRotation_QueryPitch(instance);
  }

  public float QueryYaw()
  {
    return PXCMRotation_QueryYaw(instance);
  }

  public void Rotate(PXCMRotation rotation)
  {
    PXCMRotation_Rotate(instance, rotation.instance);
  }

  public PXCMPoint3DF32 RotateVector(PXCMPoint3DF32 vec)
  {
    return PXCMRotation_RotateVector(instance, vec);
  }

  public void SetFromQuaternion(PXCMPoint4DF32 quaternion)
  {
    PXCMRotation_SetFromQuaternion(instance, quaternion);
  }

  public void SetFromEulerAngles(PXCMPoint3DF32 eulerAngles)
  {
    PXCMRotation_SetFromEulerAngles(instance, eulerAngles);
  }

  public void SetFromRotationMatrix(float[,] rotationMatrix)
  {
    PXCMRotation_SetFromRotationMatrix(instance, rotationMatrix);
  }

  public void SetFromAngleAxis(float angle, PXCMPoint3DF32 axis)
  {
    PXCMRotation_SetFromAngleAxis(instance, angle, axis);
  }

  public void SetFromSlerp(float factor, PXCMRotation startRotation, PXCMRotation endRotation)
  {
    PXCMRotation_SetFromSlerp(instance, factor, startRotation.instance, endRotation.instance);
  }

  [Serializable]
  [StructLayout(LayoutKind.Sequential)]
  public class AngleAxis
  {
    public PXCMPoint3DF32 axis;
    public float angle;

    public AngleAxis()
    {
      axis = new PXCMPoint3DF32();
      angle = 0.0f;
    }
  }

  [Flags]
  public enum EulerOrder
  {
    ROLL_PITCH_YAW = 0,
    ROLL_YAW_PITCH = 1,
    PITCH_YAW_ROLL = 2,
    PITCH_ROLL_YAW = PITCH_YAW_ROLL | ROLL_YAW_PITCH,
    YAW_ROLL_PITCH = 4,
    YAW_PITCH_ROLL = YAW_ROLL_PITCH | ROLL_YAW_PITCH
  }
}
