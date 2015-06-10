using System;
using System.Runtime.InteropServices;

public class PXCMBlobModule : PXCMBase {
    public new const int CUID = 1145916738;

    internal PXCMBlobModule(IntPtr instance, bool delete)
        : base(instance, delete) {}

    [DllImport("libpxccpp2c")]
    internal static extern IntPtr PXCMBlobModule_CreateActiveConfiguration(IntPtr instance);

    [DllImport("libpxccpp2c")]
    internal static extern IntPtr PXCMBlobModule_CreateOutput(IntPtr instance);

    public PXCMBlobConfiguration CreateActiveConfiguration() {
        var activeConfiguration = PXCMBlobModule_CreateActiveConfiguration(instance);
        if (!(activeConfiguration == IntPtr.Zero)) {
            return new PXCMBlobConfiguration(activeConfiguration, true);
        }
        return null;
    }

    public PXCMBlobData CreateOutput() {
        var output = PXCMBlobModule_CreateOutput(instance);
        if (!(output == IntPtr.Zero)) {
            return new PXCMBlobData(output, true);
        }
        return null;
    }
}