﻿using System;

[Serializable]
public struct PXCMPoint4DF32 {
    public float w;
    public float x;
    public float y;
    public float z;

    public PXCMPoint4DF32(float x, float y, float z, float w) {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }
}