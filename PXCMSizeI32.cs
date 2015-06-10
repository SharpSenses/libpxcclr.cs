using System;

[Serializable]
public struct PXCMSizeI32 {
    public int height;
    public int width;

    public PXCMSizeI32(int width, int height) {
        this.width = width;
        this.height = height;
    }
}