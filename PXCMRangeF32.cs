using System;

[Serializable]
public struct PXCMRangeF32 {
    public float max;
    public float min;

    public PXCMRangeF32(float min, float max) {
        this.min = min;
        this.max = max;
    }
}