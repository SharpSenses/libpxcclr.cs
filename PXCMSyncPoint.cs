using System;
using System.Runtime.InteropServices;

public class PXCMSyncPoint : PXCMBase
{
  public new const int CUID = 1347635283;
  public const int TIMEOUT_INFINITE = -1;
  public const int SYNCEX_LIMIT = 64;

  internal PXCMSyncPoint(IntPtr instance, bool delete)
    : base(instance, delete)
  {
  }

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMSyncPoint_Synchronize(IntPtr sp, int timeout);

  [DllImport("libpxccpp2c", EntryPoint = "PXCMSyncPoint_SynchronizeEx")]
  internal static extern pxcmStatus PXCMSyncPoint_SynchronizeExA(int n1, IntPtr[] sps, out int idx, int timeout);

  [DllImport("libpxccpp2c", EntryPoint = "PXCMSyncPoint_SynchronizeEx")]
  internal static extern pxcmStatus PXCMSyncPoint_SynchronizeExB(int n1, IntPtr[] sps, out int idx, int timeout);

  public pxcmStatus Synchronize(int timeout)
  {
    return PXCMSyncPoint_Synchronize(instance, timeout);
  }

  public pxcmStatus Synchronize()
  {
    return Synchronize(-1);
  }

  public static pxcmStatus SynchronizeEx(PXCMSyncPoint[] sps, out int idx, int timeout)
  {
    IntPtr[] sps1 = new IntPtr[sps.Length];
    for (int index = 0; index < sps.Length; ++index)
      sps1[index] = sps[index] != null ? sps[index].instance : IntPtr.Zero;
    return PXCMSyncPoint_SynchronizeExA(sps.Length, sps1, out idx, timeout);
  }

  public static pxcmStatus SynchronizeEx(PXCMSyncPoint[] sps, int timeout)
  {
    IntPtr[] sps1 = new IntPtr[sps.Length];
    for (int index = 0; index < sps.Length; ++index)
      sps1[index] = sps[index] != null ? sps[index].instance : IntPtr.Zero;
    int idx = 0;
    return PXCMSyncPoint_SynchronizeExB(sps.Length, sps1, out idx, timeout);
  }

  [CLSCompliant(false)]
  public static pxcmStatus SynchronizeEx(PXCMSyncPoint[] sps, out int idx)
  {
    return SynchronizeEx(sps, out idx, -1);
  }

  public static pxcmStatus SynchronizeEx(PXCMSyncPoint[] sps)
  {
    return SynchronizeEx(sps, -1);
  }

  public static void ReleaseSP(PXCMSyncPoint[] sps, int startIndex, int nsps)
  {
    for (int index = startIndex; index < startIndex + nsps; ++index)
    {
      if (sps[index] != null)
      {
        sps[index].Dispose();
        sps[index] = null;
      }
    }
  }

  public static void ReleaseSP(PXCMSyncPoint[] sps)
  {
    ReleaseSP(sps, 0, sps.Length);
  }
}
