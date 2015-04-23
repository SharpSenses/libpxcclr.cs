using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class PXCMCaptureManager : PXCMBase
{
  private List<ObjectPair> descriptors = new List<ObjectPair>();
  public new const int CUID = -661576891;

  public PXCMCapture capture
  {
    get
    {
      return QueryCapture();
    }
  }

  public PXCMCapture.Device device
  {
    get
    {
      return QueryDevice();
    }
  }

  internal PXCMCaptureManager(IntPtr instance, bool delete)
    : base(instance, delete)
  {
  }

  [DllImport("libpxccpp2c")]
  internal static extern void PXCMCaptureManager_FilterByDeviceInfo(IntPtr cutil, PXCMCapture.DeviceInfo dinfo);

  [DllImport("libpxccpp2c")]
  internal static extern void PXCMCaptureManager_FilterByStreamProfiles(IntPtr cutil, PXCMCapture.Device.StreamProfileSet profiles);

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMCaptureManager_RequestStreams(IntPtr cutil, int mid, IntPtr inputs);

  public pxcmStatus RequestStreams(int mid, PXCMVideoModule.DataDesc inputs)
  {
    ObjectPair objectPair = new ObjectPair(inputs);
    descriptors.Add(objectPair);
    pxcmStatus pxcmStatus = PXCMCaptureManager_RequestStreams(instance, mid, objectPair.unmanaged);
    if (pxcmStatus < pxcmStatus.PXCM_STATUS_NO_ERROR)
      descriptors.Clear();
    return pxcmStatus;
  }

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMCaptureManager_LocateStreams(IntPtr cutil, IntPtr hs);

  public pxcmStatus LocateStreams(Handler handler)
  {
    HandlerDIR handlerDir = new HandlerDIR(handler);
    pxcmStatus pxcmStatus = PXCMCaptureManager_LocateStreams(instance, handlerDir.dirUnmanaged);
    if (pxcmStatus >= pxcmStatus.PXCM_STATUS_NO_ERROR)
    {
      foreach (ObjectPair objectPair in descriptors)
        Marshal.PtrToStructure(objectPair.unmanaged, objectPair.managed);
    }
    descriptors.Clear();
    handlerDir.Dispose();
    return pxcmStatus;
  }

  [DllImport("libpxccpp2c")]
  internal static extern void PXCMCaptureManager_CloseStreams(IntPtr cutil);

  public void CloseStreams()
  {
    PXCMCaptureManager_CloseStreams(instance);
    descriptors.Clear();
  }

  [DllImport("libpxccpp2c")]
  internal static extern IntPtr PXCMCaptureManager_QueryCapture(IntPtr cutil);

  [DllImport("libpxccpp2c")]
  internal static extern IntPtr PXCMCaptureManager_QueryDevice(IntPtr cutil);

  [DllImport("libpxccpp2c")]
  internal static extern void PXCMCaptureManager_QueryImageSize(IntPtr putil, PXCMCapture.StreamType type, ref PXCMSizeI32 size);

  public PXCMSizeI32 QueryImageSize(PXCMCapture.StreamType type)
  {
    PXCMSizeI32 size = new PXCMSizeI32();
    PXCMCaptureManager_QueryImageSize(instance, type, ref size);
    return size;
  }

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMCaptureManager_ReadModuleStreamsAsync(IntPtr cutil, int mid, [Out] IntPtr[] images, out IntPtr sp);

  [DllImport("libpxccpp2c", CharSet = CharSet.Unicode)]
  internal static extern pxcmStatus PXCMCaptureManager_SetFileName(IntPtr cutil, string file, [MarshalAs(UnmanagedType.Bool)] bool record);

  [DllImport("libpxccpp2c")]
  internal static extern void PXCMCaptureManager_SetMask(IntPtr cutil, PXCMCapture.StreamType types);

  [DllImport("libpxccpp2c")]
  internal static extern void PXCMCaptureManager_SetPause(IntPtr cutil, [MarshalAs(UnmanagedType.Bool)] bool pause);

  [DllImport("libpxccpp2c")]
  internal static extern void PXCMCaptureManager_SetRealtime(IntPtr cutil, [MarshalAs(UnmanagedType.Bool)] bool realtime);

  [DllImport("libpxccpp2c")]
  internal static extern void PXCMCaptureManager_SetFrameByIndex(IntPtr cutil, int frame);

  [DllImport("libpxccpp2c")]
  internal static extern int PXCMCaptureManager_QueryFrameIndex(IntPtr cutil);

  [DllImport("libpxccpp2c")]
  internal static extern void PXCMCaptureManager_SetFrameByTimeStamp(IntPtr cutil, long ts);

  [DllImport("libpxccpp2c")]
  internal static extern long PXCMCaptureManager_QueryFrameTimeStamp(IntPtr cutil);

  [DllImport("libpxccpp2c")]
  internal static extern int PXCMCaptureManager_QueryNumberOfFrames(IntPtr cutil);

  public void FilterByDeviceInfo(PXCMCapture.DeviceInfo dinfo)
  {
    PXCMCaptureManager_FilterByDeviceInfo(instance, dinfo);
  }

  public void FilterByDeviceInfo(string name, string did, int didx)
  {
    PXCMCapture.DeviceInfo dinfo = new PXCMCapture.DeviceInfo();
    if (name != null)
      dinfo.name = name;
    if (did != null)
      dinfo.did = did;
    dinfo.didx = didx;
    FilterByDeviceInfo(dinfo);
  }

  public void FilterByStreamProfiles(PXCMCapture.Device.StreamProfileSet profiles)
  {
    PXCMCaptureManager_FilterByStreamProfiles(instance, profiles);
  }

  public void FilterByStreamProfiles(PXCMCapture.StreamType type, int width, int height, float fps)
  {
    PXCMCapture.Device.StreamProfileSet profiles = new PXCMCapture.Device.StreamProfileSet();
    profiles[type].frameRate.min = fps;
    profiles[type].frameRate.max = fps;
    profiles[type].imageInfo.width = width;
    profiles[type].imageInfo.height = height;
    FilterByStreamProfiles(profiles);
  }

  public pxcmStatus LocateStreams()
  {
    return LocateStreams(null);
  }

  public PXCMCapture QueryCapture()
  {
    IntPtr instance = PXCMCaptureManager_QueryCapture(this.instance);
    if (instance == IntPtr.Zero)
      return null;
    return new PXCMCapture(instance, false);
  }

  public PXCMCapture.Device QueryDevice()
  {
    IntPtr instance = PXCMCaptureManager_QueryDevice(this.instance);
    if (instance == IntPtr.Zero)
      return null;
    return new PXCMCapture.Device(instance, false);
  }

  public pxcmStatus ReadModuleStreamsAsync(int mid, PXCMCapture.Sample sample, out PXCMSyncPoint sp)
  {
    IntPtr sp1;
    pxcmStatus pxcmStatus = PXCMCaptureManager_ReadModuleStreamsAsync(instance, mid, sample.images, out sp1);
    sp = pxcmStatus >= pxcmStatus.PXCM_STATUS_NO_ERROR ? new PXCMSyncPoint(sp1, true) : null;
    return pxcmStatus;
  }

  public pxcmStatus SetFileName(string file, bool record)
  {
    return PXCMCaptureManager_SetFileName(instance, file, record);
  }

  public void SetMask(PXCMCapture.StreamType types)
  {
    PXCMCaptureManager_SetMask(instance, types);
  }

  public void SetPause(bool pause)
  {
    PXCMCaptureManager_SetPause(instance, pause);
  }

  public void SetRealtime(bool realtime)
  {
    PXCMCaptureManager_SetRealtime(instance, realtime);
  }

  public void SetFrameByIndex(int frame)
  {
    PXCMCaptureManager_SetFrameByIndex(instance, frame);
  }

  public int QueryFrameIndex()
  {
    return PXCMCaptureManager_QueryFrameIndex(instance);
  }

  public void SetFrameByTimeStamp(long ts)
  {
    PXCMCaptureManager_SetFrameByTimeStamp(instance, ts);
  }

  public long QueryFrameTimeStamp()
  {
    return PXCMCaptureManager_QueryFrameTimeStamp(instance);
  }

  public int QueryNumberOfFrames()
  {
    return PXCMCaptureManager_QueryNumberOfFrames(instance);
  }

  internal class HandlerDIR : IDisposable
  {
    private Handler handler;
    private List<GCHandle> gchandles;
    internal IntPtr dirUnmanaged;

    public HandlerDIR(Handler handler)
    {
      this.handler = handler;
      gchandles = new List<GCHandle>();
      if (handler == null)
        return;
      HandlerSet hs = new HandlerSet();
      if (handler.onCreateDevice != null)
      {
        OnCreateDeviceDIRDelegate deviceDirDelegate = OnCreateDevice;
        gchandles.Add(GCHandle.Alloc(deviceDirDelegate));
        hs.onCreateDevice = Marshal.GetFunctionPointerForDelegate(deviceDirDelegate);
      }
      if (handler.onSetupStreams != null)
      {
        OnSetupStreamsDIRDelegate streamsDirDelegate = OnSetupStreams;
        gchandles.Add(GCHandle.Alloc(streamsDirDelegate));
        hs.onSetupStreams = Marshal.GetFunctionPointerForDelegate(streamsDirDelegate);
      }
      if (handler.onNextDevice != null)
      {
        OnNextDeviceDIRDelegate deviceDirDelegate = OnNextDevice;
        gchandles.Add(GCHandle.Alloc(deviceDirDelegate));
        hs.onNextDevice = Marshal.GetFunctionPointerForDelegate(deviceDirDelegate);
      }
      dirUnmanaged = PXCMCaptureManager_AllocHandlerDIR(hs);
    }

    ~HandlerDIR()
    {
      Dispose();
    }

    [DllImport("libpxccpp2c")]
    internal static extern IntPtr PXCMCaptureManager_AllocHandlerDIR(HandlerSet hs);

    [DllImport("libpxccpp2c")]
    internal static extern void PXCMCaptureManager_FreeHandlerDIR(IntPtr hdir);

    private pxcmStatus OnCreateDevice(PXCMSession.ImplDesc desc, IntPtr device)
    {
      PXCMCapture.Device device1 = new PXCMCapture.Device(device, false);
      return handler.onCreateDevice(desc, device1);
    }

    private pxcmStatus OnSetupStreams(IntPtr device, PXCMCapture.StreamType types)
    {
      return handler.onSetupStreams(new PXCMCapture.Device(device, false), types);
    }

    private pxcmStatus OnNextDevice(IntPtr device)
    {
      return handler.onNextDevice(new PXCMCapture.Device(device, false));
    }

    public void Dispose()
    {
      if (dirUnmanaged == IntPtr.Zero)
        return;
      PXCMCaptureManager_FreeHandlerDIR(dirUnmanaged);
      dirUnmanaged = IntPtr.Zero;
      foreach (GCHandle gcHandle in gchandles)
      {
        if (gcHandle.IsAllocated)
          gcHandle.Free();
      }
      gchandles.Clear();
    }

    internal delegate pxcmStatus OnCreateDeviceDIRDelegate(PXCMSession.ImplDesc desc, IntPtr device);

    internal delegate pxcmStatus OnSetupStreamsDIRDelegate(IntPtr device, PXCMCapture.StreamType types);

    internal delegate pxcmStatus OnNextDeviceDIRDelegate(IntPtr device);

    [StructLayout(LayoutKind.Sequential)]
    internal class HandlerSet
    {
      internal IntPtr onCreateDevice;
      internal IntPtr onSetupStreams;
      internal IntPtr onNextDevice;
    }
  }

  public class Handler
  {
    public OnCreateDeviceDelegate onCreateDevice;
    public OnSetupStreamsDelegate onSetupStreams;
    public OnNextDeviceDelegate onNextDevice;

    public delegate pxcmStatus OnCreateDeviceDelegate(PXCMSession.ImplDesc mdesc, PXCMCapture.Device device);

    public delegate pxcmStatus OnSetupStreamsDelegate(PXCMCapture.Device device, PXCMCapture.StreamType types);

    public delegate pxcmStatus OnNextDeviceDelegate(PXCMCapture.Device device);
  }
}
