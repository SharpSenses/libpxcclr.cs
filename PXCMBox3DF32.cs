using System;

[Serializable]
public struct PXCMBox3DF32 {
    public PXCMPoint3DF32 centerOffset;
    public PXCMPoint3DF32 dimension;

    public PXCMBox3DF32(PXCMPoint3DF32 centerOffset, PXCMPoint3DF32 dimension) {
        this.centerOffset = centerOffset;
        this.dimension = dimension;
    }
}