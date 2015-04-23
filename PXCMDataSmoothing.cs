using System;
using System.Runtime.InteropServices;

public class PXCMDataSmoothing : PXCMBase
{
  public new const int CUID = 1330467652;

  internal PXCMDataSmoothing(IntPtr instance, bool delete)
    : base(instance, delete)
  {
  }

  [DllImport("libpxccpp2c")]
  internal static extern IntPtr PXCMDataSmoothing_Create1DStabilizer(IntPtr instance, float stabilizeStrength, float stabilizeRadius);

  [DllImport("libpxccpp2c")]
  internal static extern IntPtr PXCMDataSmoothing_Create1DWeighted(IntPtr instance, int nweights, float[] weights);

  [DllImport("libpxccpp2c")]
  internal static extern IntPtr PXCMDataSmoothing_Create1DQuadratic(IntPtr instance, float smoothStrength);

  [DllImport("libpxccpp2c")]
  internal static extern IntPtr PXCMDataSmoothing_Create1DSpring(IntPtr instance, float smoothStrength);

  [DllImport("libpxccpp2c")]
  internal static extern IntPtr PXCMDataSmoothing_Create2DStabilizer(IntPtr instance, float stabilizeStrength, float stabilizeRadius);

  [DllImport("libpxccpp2c")]
  internal static extern IntPtr PXCMDataSmoothing_Create2DWeighted(IntPtr instance, int nweights, float[] weights);

  [DllImport("libpxccpp2c")]
  internal static extern IntPtr PXCMDataSmoothing_Create2DQuadratic(IntPtr instance, float smoothStrength);

  [DllImport("libpxccpp2c")]
  internal static extern IntPtr PXCMDataSmoothing_Create2DSpring(IntPtr instance, float smoothStrength);

  [DllImport("libpxccpp2c")]
  internal static extern IntPtr PXCMDataSmoothing_Create3DStabilizer(IntPtr instance, float stabilizeStrength, float stabilizeRadius);

  [DllImport("libpxccpp2c")]
  internal static extern IntPtr PXCMDataSmoothing_Create3DWeighted(IntPtr instance, int nweights, float[] weights);

  [DllImport("libpxccpp2c")]
  internal static extern IntPtr PXCMDataSmoothing_Create3DQuadratic(IntPtr instance, float smoothStrength);

  [DllImport("libpxccpp2c")]
  internal static extern IntPtr PXCMDataSmoothing_Create3DSpring(IntPtr instance, float smoothStrength);

  public Smoother1D Create1DStabilizer(float stabilizeStrength, float stabilizeRadius)
  {
    IntPtr instance = PXCMDataSmoothing_Create1DStabilizer(this.instance, stabilizeStrength, stabilizeRadius);
    if (!(instance == IntPtr.Zero))
      return new Smoother1D(instance, true);
    return null;
  }

  public Smoother1D Create1DStabilizer()
  {
    return Create1DStabilizer(0.5f, 0.03f);
  }

  public Smoother1D Create1DWeighted(float[] weights)
  {
    IntPtr instance = PXCMDataSmoothing_Create1DWeighted(this.instance, weights.Length, weights);
    if (!(instance == IntPtr.Zero))
      return new Smoother1D(instance, true);
    return null;
  }

  public Smoother1D Create1DWeighted(int numWeights)
  {
    IntPtr instance = PXCMDataSmoothing_Create1DWeighted(this.instance, numWeights, null);
    if (!(instance == IntPtr.Zero))
      return new Smoother1D(instance, true);
    return null;
  }

  public Smoother1D Create1DQuadratic(float smoothStrength)
  {
    IntPtr instance = PXCMDataSmoothing_Create1DQuadratic(this.instance, smoothStrength);
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
    IntPtr instance = PXCMDataSmoothing_Create1DSpring(this.instance, smoothStrength);
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
    IntPtr instance = PXCMDataSmoothing_Create2DStabilizer(this.instance, stabilizeStrength, stabilizeRadius);
    if (!(instance == IntPtr.Zero))
      return new Smoother2D(instance, true);
    return null;
  }

  public Smoother2D Create2DStabilizer()
  {
    return Create2DStabilizer(0.5f, 0.03f);
  }

  public Smoother2D Create2DWeighted(float[] weights)
  {
    IntPtr instance = PXCMDataSmoothing_Create2DWeighted(this.instance, weights.Length, weights);
    if (!(instance == IntPtr.Zero))
      return new Smoother2D(instance, true);
    return null;
  }

  public Smoother2D Create2DWeighted(int numWeighted)
  {
    IntPtr instance = PXCMDataSmoothing_Create2DWeighted(this.instance, numWeighted, null);
    if (!(instance == IntPtr.Zero))
      return new Smoother2D(instance, true);
    return null;
  }

  public Smoother2D Create2DQuadratic(float smoothStrength)
  {
    IntPtr instance = PXCMDataSmoothing_Create2DQuadratic(this.instance, smoothStrength);
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
    IntPtr instance = PXCMDataSmoothing_Create2DSpring(this.instance, smoothStrength);
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
    IntPtr instance = PXCMDataSmoothing_Create3DStabilizer(this.instance, stabilizeStrength, stabilizeRadius);
    if (!(instance == IntPtr.Zero))
      return new Smoother3D(instance, true);
    return null;
  }

  public Smoother3D Create3DStabilizer()
  {
    return Create3DStabilizer(0.5f, 0.03f);
  }

  public Smoother3D Create3DWeighted(float[] weights)
  {
    IntPtr instance = PXCMDataSmoothing_Create3DWeighted(this.instance, weights.Length, weights);
    if (!(instance == IntPtr.Zero))
      return new Smoother3D(instance, true);
    return null;
  }

  public Smoother3D Create3DWeighted(int numWeights)
  {
    IntPtr instance = PXCMDataSmoothing_Create3DWeighted(this.instance, numWeights, null);
    if (!(instance == IntPtr.Zero))
      return new Smoother3D(instance, true);
    return null;
  }

  public Smoother3D Create3DQuadratic(float smoothStrength)
  {
    IntPtr instance = PXCMDataSmoothing_Create3DQuadratic(this.instance, smoothStrength);
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
    IntPtr instance = PXCMDataSmoothing_Create3DSpring(this.instance, smoothStrength);
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
    public new const int CUID = 21844804;

    internal Smoother1D(IntPtr instance, bool delete)
      : base(instance, delete)
    {
    }

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMDataSmoothing_Smoother1D_AddSample(IntPtr instance, float newSample, long timestamp);

    [DllImport("libpxccpp2c")]
    internal static extern float PXCMDataSmoothing_Smoother1D_GetSample(IntPtr instance, long timestamp);

    public pxcmStatus AddSample(float newSample, long timestamp)
    {
      return PXCMDataSmoothing_Smoother1D_AddSample(instance, newSample, timestamp);
    }

    public pxcmStatus AddSample(float newSample)
    {
      return AddSample(newSample, 0L);
    }

    public float GetSample(long timestamp)
    {
      return PXCMDataSmoothing_Smoother1D_GetSample(instance, timestamp);
    }

    public float GetSample()
    {
      return GetSample(0L);
    }
  }

  public class Smoother2D : PXCMBase
  {
    public new const int CUID = 38622020;

    internal Smoother2D(IntPtr instance, bool delete)
      : base(instance, delete)
    {
    }

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMDataSmoothing_Smoother2D_AddSample(IntPtr instance, PXCMPointF32 newSample, long timestamp);

    [DllImport("libpxccpp2c")]
    internal static extern void PXCMDataSmoothing_Smoother2D_GetSample(IntPtr instance, long timestamp, out PXCMPointF32 sample);

    public pxcmStatus AddSample(PXCMPointF32 newSample, long timestamp)
    {
      return PXCMDataSmoothing_Smoother2D_AddSample(instance, newSample, timestamp);
    }

    public pxcmStatus AddSample(PXCMPointF32 newSample)
    {
      return AddSample(newSample, 0L);
    }

    public PXCMPointF32 GetSample(long timestamp)
    {
      PXCMPointF32 sample = new PXCMPointF32();
      PXCMDataSmoothing_Smoother2D_GetSample(instance, timestamp, out sample);
      return sample;
    }

    public PXCMPointF32 GetSample()
    {
      return GetSample(0L);
    }
  }

  public class Smoother3D : PXCMBase
  {
    public new const int CUID = 55399236;

    internal Smoother3D(IntPtr instance, bool delete)
      : base(instance, delete)
    {
    }

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMDataSmoothing_Smoother3D_AddSample(IntPtr instance, PXCMPoint3DF32 newSample, long timestamp);

    [DllImport("libpxccpp2c")]
    internal static extern void PXCMDataSmoothing_Smoother3D_GetSample(IntPtr instance, long timestamp, out PXCMPoint3DF32 sample);

    public pxcmStatus AddSample(PXCMPoint3DF32 newSample, long timestamp)
    {
      return PXCMDataSmoothing_Smoother3D_AddSample(instance, newSample, timestamp);
    }

    public pxcmStatus AddSample(PXCMPoint3DF32 newSample)
    {
      return AddSample(newSample, 0L);
    }

    public PXCMPoint3DF32 GetSample(long timestamp)
    {
      PXCMPoint3DF32 sample = new PXCMPoint3DF32();
      PXCMDataSmoothing_Smoother3D_GetSample(instance, timestamp, out sample);
      return sample;
    }

    public PXCMPoint3DF32 GetSample()
    {
      return GetSample(0L);
    }
  }
}
