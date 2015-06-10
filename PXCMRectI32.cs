using System;

[Serializable]
public struct PXCMRectI32 {
    public int h;
    public int w;
    public int x;
    public int y;

    public PXCMRectI32(int x, int y, int w, int h) {
        this.x = x;
        this.y = y;
        this.w = w;
        this.h = h;
    }
}