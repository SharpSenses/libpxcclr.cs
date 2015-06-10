using System;

[Serializable]
public struct PXCMRectF32 {
    public float h;
    public float w;
    public float x;
    public float y;

    public PXCMRectF32(float x, float y, float w, float h) {
        this.x = x;
        this.y = y;
        this.w = w;
        this.h = h;
    }
}