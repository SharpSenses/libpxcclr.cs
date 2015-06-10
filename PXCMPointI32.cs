using System;

[Serializable]
public struct PXCMPointI32 {
    public int x;
    public int y;

    public PXCMPointI32(int x, int y) {
        this.x = x;
        this.y = y;
    }
}