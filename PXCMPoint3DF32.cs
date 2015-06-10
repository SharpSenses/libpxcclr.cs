using System;

[Serializable]
public struct PXCMPoint3DF32 {
    public float x;
    public float y;
    public float z;

    public PXCMPoint3DF32(float x, float y, float z) {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}