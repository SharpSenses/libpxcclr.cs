using System;
using System.Runtime.InteropServices;

public class PXCMPointConverter : PXCMBase
{
  public new const int CUID = 1178816592;

  internal PXCMPointConverter(IntPtr instance, bool delete)
    : base(instance, delete)
  {
  }

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMPointConverter_Set2DSourceRectangle(IntPtr instance, PXCMRectF32 rectangle2D);

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMPointConverter_Set2DTargetRectangle(IntPtr instance, PXCMRectF32 rectangle2D);

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMPointConverter_Set2DPoint(IntPtr instance, PXCMPointF32 point2D);

  [DllImport("libpxccpp2c")]
  internal static extern PXCMPointF32 PXCMPointConverter_GetConverted2DPoint(IntPtr instance);

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMPointConverter_Invert2DAxis(IntPtr instance, bool x, bool y);

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMPointConverter_Set3DSourceBox(IntPtr instance, PXCMBox3DF32 box3D);

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMPointConverter_Set3DTargetBox(IntPtr instance, PXCMBox3DF32 box3D);

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMPointConverter_Set3DPoint(IntPtr instance, PXCMPoint3DF32 point3D);

  [DllImport("libpxccpp2c")]
  internal static extern PXCMPoint3DF32 PXCMPointConverter_GetConverted3DPoint(IntPtr instance);

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMPointConverter_Invert3DAxis(IntPtr instance, bool x, bool y, bool z);

  public pxcmStatus Set2DSourceRectangle(PXCMRectF32 rectangle2D)
  {
    return PXCMPointConverter_Set2DSourceRectangle(instance, rectangle2D);
  }

  public pxcmStatus Set2DTargetRectangle(PXCMRectF32 rectangle2D)
  {
    return PXCMPointConverter_Set2DTargetRectangle(instance, rectangle2D);
  }

  public pxcmStatus Set2DPoint(PXCMPointF32 point2D)
  {
    return PXCMPointConverter_Set2DPoint(instance, point2D);
  }

  public PXCMPointF32 GetConverted2DPoint()
  {
    return PXCMPointConverter_GetConverted2DPoint(instance);
  }

  public pxcmStatus Invert2DAxis(bool x, bool y)
  {
    return PXCMPointConverter_Invert2DAxis(instance, x, y);
  }

  public pxcmStatus Set3DSourceBox(PXCMBox3DF32 box3D)
  {
    return PXCMPointConverter_Set3DSourceBox(instance, box3D);
  }

  public pxcmStatus Set3DTargetBox(PXCMBox3DF32 box3D)
  {
    return PXCMPointConverter_Set3DTargetBox(instance, box3D);
  }

  public pxcmStatus Set3DPoint(PXCMPoint3DF32 point3D)
  {
    return PXCMPointConverter_Set3DPoint(instance, point3D);
  }

  public PXCMPoint3DF32 GetConverted3DPoint()
  {
    return PXCMPointConverter_GetConverted3DPoint(instance);
  }

  public pxcmStatus Invert3DAxis(bool x, bool y, bool z)
  {
    return PXCMPointConverter_Invert3DAxis(instance, x, y, z);
  }
}
