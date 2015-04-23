using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

public class PXCMSession : PXCMBase
{
  public new const int CUID = 542328147;

  internal PXCMSession(IntPtr instance, bool delete)
    : base(instance, delete)
  {
  }

  public PXCMLoggingService CreateLogger(string loggerName)
  {
    PXCMLoggingService tt;
    if (CreateImpl(out tt) >= pxcmStatus.PXCM_STATUS_NO_ERROR)
    {
      int num = (int) tt.SetLoggerName(loggerName);
    }
    else
      tt = new PXCMLoggingService(IntPtr.Zero, false);
    return tt;
  }

  public PXCMLoggingService CreateLogger()
  {
    return CreateLogger(new StackFrame(1).GetMethod().DeclaringType.ToString());
  }

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMSession_QueryVersion(IntPtr session, [Out] ImplVersion version);

  public ImplVersion QueryVersion()
  {
    ImplVersion version = new ImplVersion();
    int num = (int) PXCMSession_QueryVersion(instance, version);
    return version;
  }

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMSession_QueryImpl(IntPtr session, ImplDesc templat, int idx, [Out] ImplDesc desc);

  public pxcmStatus QueryImpl(ImplDesc templat, int idx, out ImplDesc desc)
  {
    desc = new ImplDesc();
    return PXCMSession_QueryImpl(instance, templat, idx, desc);
  }

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMSession_CreateImpl(IntPtr session, ImplDesc desc, int iuid, int cuid, out IntPtr instance);

  [DllImport("libpxccpp2c", CharSet = CharSet.Unicode)]
  internal static extern pxcmStatus PXCMSession_LoadImplFromFile(IntPtr session, string moduleName);

  [DllImport("libpxccpp2c", CharSet = CharSet.Unicode)]
  internal static extern pxcmStatus PXCMSession_UnloadImplFromFile(IntPtr session, string moduleName);

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMSession_QueryModuleDesc(IntPtr session, IntPtr module, [Out] ImplDesc desc);

  public pxcmStatus QueryModuleDesc(PXCMBase module, out ImplDesc desc)
  {
    desc = new ImplDesc();
    return PXCMSession_QueryModuleDesc(instance, module.instance, desc);
  }

  [DllImport("libpxccpp2c")]
  internal static extern IntPtr PXCMSession_CreateImage(IntPtr session, PXCMImage.ImageInfo info, PXCMImage.ImageData data);

  [DllImport("libpxccpp2c")]
  internal static extern IntPtr PXCMSession_CreateAudio(IntPtr session, PXCMAudio.AudioInfo info, PXCMAudio.AudioData data);

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMSession_SetCoordinateSystem(IntPtr session, CoordinateSystem cs);

  [DllImport("libpxccpp2c")]
  internal static extern CoordinateSystem PXCMSession_QueryCoordinateSystem(IntPtr session);

  [DllImport("libpxccpp2c")]
  internal static extern IntPtr PXCMSession_Create();

  public pxcmStatus CreateImpl(ImplDesc desc, int iuid, int cuid, out PXCMBase impl)
  {
    IntPtr instance;
    pxcmStatus impl1 = PXCMSession_CreateImpl(this.instance, desc, iuid, cuid, out instance);
    impl = null;
    if (impl1 >= pxcmStatus.PXCM_STATUS_NO_ERROR)
    {
      impl = new PXCMBase(instance, false).QueryInstance(cuid);
      impl.AddRef();
    }
    return impl1;
  }

  public pxcmStatus CreateImpl(ImplDesc desc, int cuid, out PXCMBase instance)
  {
    return CreateImpl(desc, 0, cuid, out instance);
  }

  public pxcmStatus CreateImpl(int iuid, int cuid, out PXCMBase instance)
  {
    return CreateImpl(null, iuid, cuid, out instance);
  }

  public pxcmStatus CreateImpl(int cuid, out PXCMBase instance)
  {
    return CreateImpl(null, 0, cuid, out instance);
  }

  public pxcmStatus CreateImpl<TT>(ImplDesc desc, int iuid, out TT tt) where TT : PXCMBase
  {
    PXCMBase impl1;
    pxcmStatus impl2 = CreateImpl(desc, iuid, Type2CUID[typeof (TT)], out impl1);
    tt = impl2 >= pxcmStatus.PXCM_STATUS_NO_ERROR ? (TT) impl1 : default (TT);
    return impl2;
  }

  public pxcmStatus CreateImpl<TT>(ImplDesc desc, out TT tt) where TT : PXCMBase
  {
    PXCMBase instance;
    pxcmStatus impl = CreateImpl(desc, Type2CUID[typeof (TT)], out instance);
    tt = impl >= pxcmStatus.PXCM_STATUS_NO_ERROR ? (TT) instance : default (TT);
    return impl;
  }

  public pxcmStatus CreateImpl<TT>(int iuid, out TT tt) where TT : PXCMBase
  {
    PXCMBase instance;
    pxcmStatus impl = CreateImpl(iuid, Type2CUID[typeof (TT)], out instance);
    tt = impl >= pxcmStatus.PXCM_STATUS_NO_ERROR ? (TT) instance : default (TT);
    return impl;
  }

  public pxcmStatus CreateImpl<TT>(out TT tt) where TT : PXCMBase
  {
    PXCMBase instance;
    pxcmStatus impl = CreateImpl(Type2CUID[typeof (TT)], out instance);
    tt = impl >= pxcmStatus.PXCM_STATUS_NO_ERROR ? (TT) instance : default (TT);
    return impl;
  }

  public PXCMSenseManager CreateSenseManager()
  {
    PXCMSenseManager tt;
    if (CreateImpl(out tt) < pxcmStatus.PXCM_STATUS_NO_ERROR)
      return null;
    return tt;
  }

  public PXCMCaptureManager CreateCaptureManager()
  {
    PXCMCaptureManager tt;
    if (CreateImpl(out tt) < pxcmStatus.PXCM_STATUS_NO_ERROR)
      return null;
    return tt;
  }

  public PXCMAudioSource CreateAudioSource()
  {
    PXCMAudioSource tt;
    if (CreateImpl(out tt) < pxcmStatus.PXCM_STATUS_NO_ERROR)
      return null;
    return tt;
  }

  public PXCMImage CreateImage(PXCMImage.ImageInfo info, PXCMImage.ImageData data)
  {
    IntPtr image = PXCMSession_CreateImage(instance, info, data);
    if (!(image != IntPtr.Zero))
      return null;
    return new PXCMImage(image, true);
  }

  public PXCMImage CreateImage(PXCMImage.ImageInfo info)
  {
    return CreateImage(info, null);
  }

  public PXCMAudio CreateAudio(PXCMAudio.AudioInfo info, PXCMAudio.AudioData data)
  {
    IntPtr audio = PXCMSession_CreateAudio(instance, info, data);
    if (!(audio != IntPtr.Zero))
      return null;
    return new PXCMAudio(audio, true);
  }

  public PXCMAudio CreateAudio(PXCMAudio.AudioInfo info)
  {
    return CreateAudio(info, null);
  }

  public pxcmStatus LoadImplFromFile(string moduleName)
  {
    return PXCMSession_LoadImplFromFile(instance, moduleName);
  }

  public pxcmStatus UnloadImplFromFile(string moduleName)
  {
    return PXCMSession_UnloadImplFromFile(instance, moduleName);
  }

  public pxcmStatus SetCoordinateSystem(CoordinateSystem cs)
  {
    return PXCMSession_SetCoordinateSystem(instance, cs);
  }

  public CoordinateSystem QueryCoordinateSystem()
  {
    return PXCMSession_QueryCoordinateSystem(instance);
  }

  public static PXCMSession CreateInstance()
  {
    IntPtr instance = PXCMSession_Create();
    PXCMSession pxcmSession = instance != IntPtr.Zero ? new PXCMSession(instance, true) : null;
    if (pxcmSession == null)
      return null;
    try
    {
      PXCMMetadata pxcmMetadata = pxcmSession.QueryInstance<PXCMMetadata>();
      if (pxcmMetadata == null)
        return pxcmSession;
      string s = "CSharp";
      if (!string.IsNullOrEmpty(s))
      {
        int num = (int) pxcmMetadata.AttachBuffer(1296451664, Encoding.Unicode.GetBytes(s));
      }
    }
    catch (Exception ex)
    {
    }
    return pxcmSession;
  }

  [Serializable]
  [StructLayout(LayoutKind.Sequential)]
  public class ImplVersion
  {
    public short major;
    public short minor;
  }

  [Flags]
  public enum ImplGroup
  {
    IMPL_GROUP_ANY = 0,
    IMPL_GROUP_OBJECT_RECOGNITION = 1,
    IMPL_GROUP_SPEECH_RECOGNITION = 2,
    IMPL_GROUP_SENSOR = 4,
    IMPL_GROUP_PHOTOGRAPHY = 8,
    IMPL_GROUP_UTILITIES = 16,
    IMPL_GROUP_CORE = -2147483648,
    IMPL_GROUP_USER = 1073741824
  }

  [Flags]
  public enum ImplSubgroup
  {
    IMPL_SUBGROUP_ANY = 0,
    IMPL_SUBGROUP_FACE_ANALYSIS = 1,
    IMPL_SUBGROUP_GESTURE_RECOGNITION = 16,
    IMPL_SUBGROUP_SEGMENTATION = 32,
    IMPL_SUBGROUP_PULSE_ESTIMATION = 64,
    IMPL_SUBGROUP_EMOTION_RECOGNITION = 128,
    IMPL_SUBGROUP_OBJECT_TRACKING = 256,
    IMPL_SUBGROUP_3DSEG = 512,
    IMPL_SUBGROUP_3DSCAN = 1024,
    IMPL_SUBGROUP_SCENE_PERCEPTION = 2048,
    IMPL_SUBGROUP_ENHANCED_PHOTOGRAPHY = 4096,
    IMPL_SUBGROUP_AUDIO_CAPTURE = IMPL_SUBGROUP_FACE_ANALYSIS,
    IMPL_SUBGROUP_VIDEO_CAPTURE = 2,
    IMPL_SUBGROUP_SPEECH_RECOGNITION = IMPL_SUBGROUP_AUDIO_CAPTURE,
    IMPL_SUBGROUP_SPEECH_SYNTHESIS = IMPL_SUBGROUP_VIDEO_CAPTURE
  }

  [Flags]
  public enum CoordinateSystem
  {
    COORDINATE_SYSTEM_REAR_DEFAULT = 256,
    COORDINATE_SYSTEM_REAR_OPENCV = 512,
    COORDINATE_SYSTEM_FRONT_DEFAULT = 1
  }

  [Serializable]
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public class ImplDesc
  {
    public ImplGroup group;
    public ImplSubgroup subgroup;
    public int algorithm;
    public int iuid;
    public ImplVersion version;
    public int reserved2;
    public int merit;
    public int vendor;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public int[] cuids;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
    public string friendlyName;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
    internal int[] reserved;

    public ImplDesc()
    {
      cuids = new int[4];
      friendlyName = "";
      reserved = new int[12];
    }
  }
}
