using System;
using System.Runtime.InteropServices;

public class PXCMHandModule : PXCMBase
{
  public new const int CUID = 1313751368;
  internal PXCMHandConfiguration.EventMaps maps;

  internal PXCMHandModule(IntPtr instance, bool delete)
    : base(instance, delete)
  {
    maps = new PXCMHandConfiguration.EventMaps();
  }

  internal PXCMHandModule(PXCMHandConfiguration.EventMaps maps, IntPtr instance, bool delete)
    : base(instance, delete)
  {
    this.maps = maps;
  }

  [DllImport("libpxccpp2c")]
  internal static extern IntPtr PXCMHandModule_CreateActiveConfiguration(IntPtr instance);

  [DllImport("libpxccpp2c")]
  internal static extern IntPtr PXCMHandModule_CreateOutput(IntPtr instance);

  public PXCMHandConfiguration CreateActiveConfiguration()
  {
    IntPtr activeConfiguration = PXCMHandModule_CreateActiveConfiguration(instance);
    if (activeConfiguration == IntPtr.Zero)
      return null;
    return new PXCMHandConfiguration(maps, activeConfiguration, true);
  }

  public PXCMHandData CreateOutput()
  {
    IntPtr output = PXCMHandModule_CreateOutput(instance);
    if (!(output != IntPtr.Zero))
      return null;
    return new PXCMHandData(output, true);
  }
}
