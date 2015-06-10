using System;
using System.Runtime.InteropServices;

public class PXCMFaceModule : PXCMBase {
    public new const int CUID = 1144209734;
    internal PXCMFaceConfiguration.EventMaps maps;

    internal PXCMFaceModule(IntPtr instance, bool delete)
        : base(instance, delete) {
        maps = new PXCMFaceConfiguration.EventMaps();
    }

    internal PXCMFaceModule(PXCMFaceConfiguration.EventMaps maps, IntPtr instance, bool delete)
        : base(instance, delete) {
        this.maps = maps;
    }

    [DllImport("libpxccpp2c")]
    internal static extern IntPtr PXCMFaceModule_CreateActiveConfiguration(IntPtr instance);

    [DllImport("libpxccpp2c")]
    internal static extern IntPtr PXCMFaceModule_CreateOutput(IntPtr instance);

    public PXCMFaceConfiguration CreateActiveConfiguration() {
        var activeConfiguration = PXCMFaceModule_CreateActiveConfiguration(instance);
        if (!(activeConfiguration == IntPtr.Zero)) {
            return new PXCMFaceConfiguration(maps, activeConfiguration, true);
        }
        return null;
    }

    public PXCMFaceData CreateOutput() {
        var output = PXCMFaceModule_CreateOutput(instance);
        if (!(output == IntPtr.Zero)) {
            return new PXCMFaceData(output, true);
        }
        return null;
    }
}