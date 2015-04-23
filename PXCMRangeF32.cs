using System;

[Serializable]
public struct PXCMRangeF32
{
  public float min;
  public float max;

  public PXCMRangeF32(float min, float max)
  {
    this.min = min;
    this.max = max;
  }
}
