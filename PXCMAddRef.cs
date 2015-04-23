using System;
using System.Runtime.InteropServices;

public class PXCMAddRef : PXCMBase {
    public new const int CUID = 1397965122;

    internal PXCMAddRef(IntPtr instance, bool delete)
        : base(instance, false) {
    }

    [DllImport("libpxccpp2c")]
    internal static extern int PXCMAddRef_AddRef(IntPtr addref);

    public new int AddRef() {
        orig.AddRef();
        return PXCMAddRef_AddRef(instance);
    }
}
