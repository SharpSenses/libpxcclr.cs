using System;
using System.Runtime.InteropServices;

public class PXCMMetadata : PXCMBase {
    public new const int CUID = 1647936547;

    internal PXCMMetadata(IntPtr instance, bool delete)
        : base(instance, delete) {}

    [DllImport("libpxccpp2c")]
    internal static extern int PXCMMetadata_QueryUID(IntPtr md);

    [DllImport("libpxccpp2c")]
    internal static extern int PXCMMetadata_QueryMetadata(IntPtr md, int idx);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMMetadata_DetachMetadata(IntPtr md, int id);

    [DllImport("libpxccpp2c")]
    private static extern pxcmStatus PXCMMetadata_AttachBuffer(IntPtr md, int id, IntPtr buffer, int size);

    internal static pxcmStatus AttachBufferINT(IntPtr md, int id, byte[] buffer) {
        var gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
        var pxcmStatus = PXCMMetadata_AttachBuffer(md, id, gcHandle.AddrOfPinnedObject(), buffer.Length);
        gcHandle.Free();
        return pxcmStatus;
    }

    [DllImport("libpxccpp2c")]
    internal static extern int PXCMMetadata_QueryBufferSize(IntPtr md, int id);

    [DllImport("libpxccpp2c")]
    private static extern pxcmStatus PXCMMetadata_QueryBuffer(IntPtr md, int id, [Out] byte[] buffer, int size);

    internal static pxcmStatus QueryBufferINT(IntPtr md, int id, out byte[] buffer) {
        var size = PXCMMetadata_QueryBufferSize(md, id);
        if (size == 0) {
            buffer = null;
            return pxcmStatus.PXCM_STATUS_ITEM_UNAVAILABLE;
        }
        buffer = new byte[size];
        return PXCMMetadata_QueryBuffer(md, id, buffer, size);
    }

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMMetadata_AttachSerializable(IntPtr md, int id, IntPtr instance);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMMetadata_CreateSerializable(IntPtr md, int id, int cuid, out IntPtr instance);

    public int QueryUID() {
        return PXCMMetadata_QueryUID(instance);
    }

    public int QueryMetadata(int idx) {
        return PXCMMetadata_QueryMetadata(instance, idx);
    }

    public pxcmStatus DetachMetadata(int id) {
        return PXCMMetadata_DetachMetadata(instance, id);
    }

    public pxcmStatus AttachBuffer(int id, byte[] buffer) {
        return AttachBufferINT(instance, id, buffer);
    }

    public int QueryBufferSize(int id) {
        return PXCMMetadata_QueryBufferSize(instance, id);
    }

    public pxcmStatus QueryBuffer(int id, out byte[] buffer) {
        return QueryBufferINT(instance, id, out buffer);
    }

    public pxcmStatus AttachSerializable(int id, PXCMBase slz) {
        return PXCMMetadata_AttachSerializable(instance, id, slz.instance);
    }

    public pxcmStatus CreateSerializable(int id, int cuid, out PXCMBase slz) {
        IntPtr instance;
        var serializable = PXCMMetadata_CreateSerializable(this.instance, id, cuid, out instance);
        slz = null;
        if (serializable >= pxcmStatus.PXCM_STATUS_NO_ERROR) {
            slz = new PXCMBase(instance, false).QueryInstance(cuid);
            slz.AddRef();
        }
        return serializable;
    }

    public pxcmStatus CreateSerializable<TT>(int id, out TT slz) where TT : PXCMBase {
        PXCMBase slz1;
        var serializable = CreateSerializable(id, Type2CUID[typeof (TT)], out slz1);
        slz = serializable >= pxcmStatus.PXCM_STATUS_NO_ERROR ? (TT) slz1 : default(TT);
        return serializable;
    }
}