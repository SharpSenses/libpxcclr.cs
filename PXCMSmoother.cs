using System;
using System.Runtime.InteropServices;

public class PXCMSmoother : PXCMBase
{
  public new const int CUID = 1330468179;

  internal PXCMSmoother(IntPtr instance, bool delete)
    : base(instance, delete)
  {
  }

  [DllImport("libpxccpp2c")]
  internal static extern IntPtr PXCMSmoother_Create1DStabilizer(IntPtr instance, float stabilizeStrength, float stabilizeRadius);

  [DllImport("libpxccpp2c")]
  internal static extern IntPtr PXCMSmoother_Create1DWeighted(IntPtr instance, int nweights, float[] weights);

  [DllImport("libpxccpp2c")]
  internal static extern IntPtr PXCMSmoother_Create1DQuadratic(IntPtr instance, float smoothStrength);

  [DllImport("libpxccpp2c")]
  internal static extern IntPtr PXCMSmoother_Create1DSpring(IntPtr instance, float smoothStrength);

  [DllImport("libpxccpp2c")]
  internal static extern IntPtr PXCMSmoother_Create2DStabilizer(IntPtr instance, float stabilizeStrength, float stabilizeRadius);

  [DllImport("libpxccpp2c")]
  internal static extern IntPtr PXCMSmoother_Create2DWeighted(IntPtr instance, int nweights, float[] weights);

  [DllImport("libpxccpp2c")]
  internal static extern IntPtr PXCMSmoother_Create2DQuadratic(IntPtr instance, float smoothStrength);

  [DllImport("libpxccpp2c")]
  internal static extern IntPtr PXCMSmoother_Create2DSpring(IntPtr instance, float smoothStrength);

  [DllImport("libpxccpp2c")]
  internal static extern IntPtr PXCMSmoother_Create3DStabilizer(IntPtr instance, float stabilizeStrength, float stabilizeRadius);

  [DllImport("libpxccpp2c")]
  internal static extern IntPtr PXCMSmoother_Create3DWeighted(IntPtr instance, int nweights, float[] weights);

  [DllImport("libpxccpp2c")]
  internal static extern IntPtr PXCMSmoother_Create3DQuadratic(IntPtr instance, float smoothStrength);

  [DllImport("libpxccpp2c")]
  internal static extern IntPtr PXCMSmoother_Create3DSpring(IntPtr instance, float smoothStrength);

  public Smoother1D Create1DStabilizer(float stabilizeStrength, float stabilizeRadius)
  {
    IntPtr instance = PXCMSmoother_Create1DStabilizer(this.instance, stabilizeStrength, stabilizeRadius);
    if (!(instance == IntPtr.Zero))
      return new Smoother1D(instance, true);
    return null;
  }

  public Smoother1D Create1DStabilizer(float stabilizeRadius)
  {
    return Create1DStabilizer(0.5f, stabilizeRadius);
  }

  public Smoother1D Create1DWeighted(float[] weights)
  {
    IntPtr instance = PXCMSmoother_Create1DWeighted(this.instance, weights.Length, weights);
    if (!(instance == IntPtr.Zero))
      return new Smoother1D(instance, true);
    return null;
  }

  public Smoother1D Create1DWeighted(int numWeights)
  {
    IntPtr instance = PXCMSmoother_Create1DWeighted(this.instance, numWeights, null);
    if (!(instance == IntPtr.Zero))
      return new Smoother1D(instance, true);
    return null;
  }

  public Smoother1D Create1DQuadratic(float smoothStrength)
  {
    IntPtr instance = PXCMSmoother_Create1DQuadratic(this.instance, smoothStrength);
    if (!(instance == IntPtr.Zero))
      return new Smoother1D(instance, true);
    return null;
  }

  public Smoother1D Create1DQuadratic()
  {
    return Create1DQuadratic(0.5f);
  }

  public Smoother1D Create1DSpring(float smoothStrength)
  {
    IntPtr instance = PXCMSmoother_Create1DSpring(this.instance, smoothStrength);
    if (!(instance == IntPtr.Zero))
      return new Smoother1D(instance, true);
    return null;
  }

  public Smoother1D Create1DSpring()
  {
    return Create1DSpring(0.5f);
  }

  public Smoother2D Create2DStabilizer(float stabilizeStrength, float stabilizeRadius)
  {
    IntPtr instance = PXCMSmoother_Create2DStabilizer(this.instance, stabilizeStrength, stabilizeRadius);
    if (!(instance == IntPtr.Zero))
      return new Smoother2D(instance, true);
    return null;
  }

  public Smoother2D Create2DStabilizer(float stabilizeRadius)
  {
    return Create2DStabilizer(0.5f, stabilizeRadius);
  }

  public Smoother2D Create2DWeighted(float[] weights)
  {
    IntPtr instance = PXCMSmoother_Create2DWeighted(this.instance, weights.Length, weights);
    if (!(instance == IntPtr.Zero))
      return new Smoother2D(instance, true);
    return null;
  }

  public Smoother2D Create2DWeighted(int numWeights)
  {
    IntPtr instance = PXCMSmoother_Create2DWeighted(this.instance, numWeights, null);
    if (!(instance == IntPtr.Zero))
      return new Smoother2D(instance, true);
    return null;
  }

  public Smoother2D Create2DQuadratic(float smoothStrength)
  {
    IntPtr instance = PXCMSmoother_Create2DQuadratic(this.instance, smoothStrength);
    if (!(instance == IntPtr.Zero))
      return new Smoother2D(instance, true);
    return null;
  }

  public Smoother2D Create2DQuadratic()
  {
    return Create2DQuadratic(0.5f);
  }

  public Smoother2D Create2DSpring(float smoothStrength)
  {
    IntPtr instance = PXCMSmoother_Create2DSpring(this.instance, smoothStrength);
    if (!(instance == IntPtr.Zero))
      return new Smoother2D(instance, true);
    return null;
  }

  public Smoother2D Create2DSpring()
  {
    return Create2DSpring(0.5f);
  }

  public Smoother3D Create3DStabilizer(float stabilizeStrength, float stabilizeRadius)
  {
    IntPtr instance = PXCMSmoother_Create3DStabilizer(this.instance, stabilizeStrength, stabilizeRadius);
    if (!(instance == IntPtr.Zero))
      return new Smoother3D(instance, true);
    return null;
  }

  public Smoother3D Create3DStabilizer(float stabilizeRadius)
  {
    return Create3DStabilizer(0.5f, stabilizeRadius);
  }

  public Smoother3D Create3DWeighted(float[] weights)
  {
    IntPtr instance = PXCMSmoother_Create3DWeighted(this.instance, weights.Length, weights);
    if (!(instance == IntPtr.Zero))
      return new Smoother3D(instance, true);
    return null;
  }

  public Smoother3D Create3DWeighted(int numWeights)
  {
    IntPtr instance = PXCMSmoother_Create3DWeighted(this.instance, numWeights, null);
    if (!(instance == IntPtr.Zero))
      return new Smoother3D(instance, true);
    return null;
  }

  public Smoother3D Create3DQuadratic(float smoothStrength)
  {
    IntPtr instance = PXCMSmoother_Create3DQuadratic(this.instance, smoothStrength);
    if (!(instance == IntPtr.Zero))
      return new Smoother3D(instance, true);
    return null;
  }

  public Smoother3D Create3DQuadratic()
  {
    return Create3DQuadratic(0.5f);
  }

  public Smoother3D Create3DSpring(float smoothStrength)
  {
    IntPtr instance = PXCMSmoother_Create3DSpring(this.instance, smoothStrength);
    if (!(instance == IntPtr.Zero))
      return new Smoother3D(instance, true);
    return null;
  }

  public Smoother3D Create3DSpring()
  {
    return Create3DQuadratic(0.5f);
  }

  public class Smoother1D : PXCMBase
  {
    public new const int CUID = 21845331;

    internal Smoother1D(IntPtr instance, bool delete)
      : base(instance, delete)
    {
    }

    [DllImport("libpxccpp2c")]
    internal static extern float PXCMSmoother_Smoother1D_SmoothValue(IntPtr instance, float value);

    public float SmoothValue(float value)
    {
      return PXCMSmoother_Smoother1D_SmoothValue(instance, value);
    }
  }

  public class Smoother2D : PXCMBase
  {
    public new const int CUID = 38622547;

    internal Smoother2D(IntPtr instance, bool delete)
      : base(instance, delete)
    {
    }

    [DllImport("libpxccpp2c")]
    internal static extern PXCMPointF32 PXCMSmoother_Smoother2D_SmoothValue(IntPtr instance, PXCMPointF32 value);

    public PXCMPointF32 SmoothValue(PXCMPointF32 value)
    {
      return PXCMSmoother_Smoother2D_SmoothValue(instance, value);
    }
  }

  public class Smoother3D : PXCMBase
  {
    public new const int CUID = 55399763;

    internal Smoother3D(IntPtr instance, bool delete)
      : base(instance, delete)
    {
    }

    [DllImport("libpxccpp2c")]
    internal static extern PXCMPoint3DF32 PXCMSmoother_Smoother3D_SmoothValue(IntPtr instance, PXCMPoint3DF32 value);

    public PXCMPoint3DF32 SmoothValue(PXCMPoint3DF32 value)
    {
      return PXCMSmoother_Smoother3D_SmoothValue(instance, value);
    }
  }
}
