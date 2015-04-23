using System;
using System.Runtime.InteropServices;

public class PXCMVideoModule : PXCMBase
{
  public new const int CUID = 1775611958;
  public const int DEVCAP_LIMIT = 120;

  internal PXCMVideoModule(IntPtr instance, bool delete)
    : base(instance, delete)
  {
  }

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMVideoModule_QueryCaptureProfile(IntPtr module, int pidx, [Out] DataDesc inputs);

  public pxcmStatus QueryCaptureProfile(int pidx, out DataDesc inputs)
  {
    inputs = new DataDesc();
    return PXCMVideoModule_QueryCaptureProfile(instance, pidx, inputs);
  }

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMVideoModule_SetCaptureProfile(IntPtr module, DataDesc inputs);

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMVideoModule_ProcessImageAsync(IntPtr module, PXCMCapture.Sample sample, out IntPtr sp);

  public pxcmStatus QueryCaptureProfile(out DataDesc inputs)
  {
    return QueryCaptureProfile(-1, out inputs);
  }

  public pxcmStatus SetCaptureProfile(DataDesc inputs)
  {
    return PXCMVideoModule_SetCaptureProfile(instance, inputs);
  }

  public pxcmStatus ProcessImageAsync(PXCMCapture.Sample sample, out PXCMSyncPoint sp)
  {
    IntPtr sp1;
    pxcmStatus pxcmStatus = PXCMVideoModule_ProcessImageAsync(instance, sample, out sp1);
    sp = pxcmStatus >= pxcmStatus.PXCM_STATUS_NO_ERROR ? new PXCMSyncPoint(sp1, true) : null;
    return pxcmStatus;
  }

  [Serializable]
  public struct DeviceCap
  {
    public PXCMCapture.Device.Property label;
    public float value;
  }

  [Serializable]
  [StructLayout(LayoutKind.Sequential)]
  public class StreamDesc
  {
    public PXCMSizeI32 sizeMin;
    public PXCMSizeI32 sizeMax;
    public PXCMRangeF32 frameRate;
    public PXCMCapture.Device.StreamOption options;
    public int propertySet;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    internal int[] reserved;

    public StreamDesc()
    {
      reserved = new int[4];
    }
  }

  [Serializable]
  [StructLayout(LayoutKind.Sequential, Size = 384)]
  public class StreamDescSet
  {
    public StreamDesc color;
    public StreamDesc depth;
    public StreamDesc ir;
    public StreamDesc left;
    public StreamDesc right;
    internal StreamDesc reserved1;
    internal StreamDesc reserved2;
    internal StreamDesc reserved3;

    public StreamDesc this[PXCMCapture.StreamType type]
    {
      get
      {
        if (type == PXCMCapture.StreamType.STREAM_TYPE_COLOR)
          return color;
        if (type == PXCMCapture.StreamType.STREAM_TYPE_DEPTH)
          return depth;
        if (type == PXCMCapture.StreamType.STREAM_TYPE_IR)
          return ir;
        if (type == PXCMCapture.StreamType.STREAM_TYPE_LEFT)
          return left;
        if (type == PXCMCapture.StreamType.STREAM_TYPE_RIGHT)
          return right;
        if (type == PXCMCapture.StreamTypeFromIndex(5))
          return reserved1;
        if (type == PXCMCapture.StreamTypeFromIndex(6))
          return reserved2;
        return reserved3;
      }
      set
      {
        if (type == PXCMCapture.StreamType.STREAM_TYPE_COLOR)
          color = value;
        if (type == PXCMCapture.StreamType.STREAM_TYPE_DEPTH)
          depth = value;
        if (type == PXCMCapture.StreamType.STREAM_TYPE_IR)
          ir = value;
        if (type == PXCMCapture.StreamType.STREAM_TYPE_LEFT)
          left = value;
        if (type == PXCMCapture.StreamType.STREAM_TYPE_RIGHT)
          right = value;
        if (type == PXCMCapture.StreamTypeFromIndex(5))
          reserved1 = value;
        if (type == PXCMCapture.StreamTypeFromIndex(6))
          reserved2 = value;
        if (type != PXCMCapture.StreamTypeFromIndex(7))
          return;
        reserved3 = value;
      }
    }

    public StreamDescSet()
    {
      color = new StreamDesc();
      depth = new StreamDesc();
      ir = new StreamDesc();
      left = new StreamDesc();
      right = new StreamDesc();
      reserved1 = new StreamDesc();
      reserved2 = new StreamDesc();
      reserved3 = new StreamDesc();
    }

    internal StreamDescSet(StreamDesc[] descs)
    {
      color = descs[0];
      depth = descs[1];
      ir = descs[2];
      left = descs[3];
      right = descs[4];
      reserved1 = descs[5];
      reserved2 = descs[6];
      reserved3 = descs[7];
    }

    internal StreamDesc[] ToStreamDescArray()
    {
      return new StreamDesc[8]
      {
        color,
        depth,
        ir,
        left,
        right,
        reserved1,
        reserved2,
        reserved3
      };
    }
  }

  internal class DataDescINT
  {
    public StreamDesc[] streams;
    public DeviceCap[] devCaps;
    public PXCMCapture.DeviceInfo deviceInfo;
  }

  [Serializable]
  [StructLayout(LayoutKind.Sequential)]
  public class DataDesc
  {
    public StreamDescSet streams;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 120)]
    public DeviceCap[] devCaps;
    public PXCMCapture.DeviceInfo deviceInfo;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    internal int[] reserved;

    public DataDesc()
    {
      streams = new StreamDescSet();
      devCaps = new DeviceCap[120];
      deviceInfo = new PXCMCapture.DeviceInfo();
      reserved = new int[8];
    }

    internal DataDesc(DataDescINT data)
    {
      devCaps = data.devCaps;
      deviceInfo = data.deviceInfo;
      streams = new StreamDescSet(data.streams);
      reserved = new int[8];
    }

    internal DataDescINT ToDataDescINT()
    {
      return new DataDescINT {
        devCaps = devCaps,
        deviceInfo = deviceInfo,
        streams = streams.ToStreamDescArray()
      };
    }
  }
}
