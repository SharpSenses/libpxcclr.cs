﻿using System;

[Serializable]
public struct PXCMRectI32
{
  public int x;
  public int y;
  public int w;
  public int h;

  public PXCMRectI32(int x, int y, int w, int h)
  {
    this.x = x;
    this.y = y;
    this.w = w;
    this.h = h;
  }
}
