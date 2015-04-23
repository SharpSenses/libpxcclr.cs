using System;

[Serializable]
public struct PXCMSizeI32
{
  public int width;
  public int height;

  public PXCMSizeI32(int width, int height)
  {
    this.width = width;
    this.height = height;
  }
}
