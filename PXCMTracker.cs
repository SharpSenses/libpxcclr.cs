using System;
using System.Runtime.InteropServices;

public class PXCMTracker : PXCMBase
{
  public new const int CUID = 1380667988;

  internal PXCMTracker(IntPtr instance, bool delete)
    : base(instance, delete)
  {
  }

  [DllImport("libpxccpp2c")]
  internal static extern bool PXCMTracker_IsTracking(ETrackingState state);

  [DllImport("libpxccpp2c", CharSet = CharSet.Unicode)]
  internal static extern pxcmStatus PXCMTracker_SetCameraParameters(IntPtr instsance, string filename);

  [DllImport("libpxccpp2c", CharSet = CharSet.Unicode)]
  internal static extern pxcmStatus PXCMTracker_Set2DTrackFromFile(IntPtr instance, string filename, out int cosID, float widthMM, float heightMM, float qualityThreshold, [MarshalAs(UnmanagedType.Bool)] bool extensible);

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMTracker_Set2DTrackFromImage(IntPtr instance, IntPtr image, out int cosID, float widthMM, float heightMM, float qualityThreshold, [MarshalAs(UnmanagedType.Bool)] bool extensible);

  [DllImport("libpxccpp2c", CharSet = CharSet.Unicode)]
  internal static extern pxcmStatus PXCMTracker_Set3DTrack(IntPtr instance, string filename, out int firstCosID, out int lastCosID, [MarshalAs(UnmanagedType.Bool)] bool extensible);

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMTracker_RemoveTrackingID(IntPtr instance, int cosID);

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMTracker_RemoveAllTrackingIDs(IntPtr instance);

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMTracker_Set3DInstantTrack(IntPtr instance, [MarshalAs(UnmanagedType.Bool)] bool egoMotion, int framesToSkip);

  [DllImport("libpxccpp2c")]
  internal static extern int PXCMTracker_QueryNumberTrackingValues(IntPtr instance);

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMTracker_QueryAllTrackingValues(IntPtr instance, IntPtr trackingValues);

  internal static pxcmStatus QueryAllTrackingValuesINT(IntPtr instance, out TrackingValues[] trackingValues)
  {
    int length = PXCMTracker_QueryNumberTrackingValues(instance);
    IntPtr num = Marshal.AllocHGlobal(Marshal.SizeOf(typeof (TrackingValues)) * length);
    pxcmStatus pxcmStatus = PXCMTracker_QueryAllTrackingValues(instance, num);
    if (pxcmStatus >= pxcmStatus.PXCM_STATUS_NO_ERROR)
    {
      trackingValues = new TrackingValues[length];
      for (int index = 0; index < length; ++index)
      {
        trackingValues[index] = new TrackingValues();
        Marshal.PtrToStructure(new IntPtr(num.ToInt64() + Marshal.SizeOf(typeof (TrackingValues)) * index), trackingValues[index]);
      }
    }
    else
      trackingValues = null;
    Marshal.FreeHGlobal(num);
    return pxcmStatus;
  }

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMTracker_QueryTrackingValues(IntPtr instance, int cosID, [Out] TrackingValues trackingValues);

  internal static pxcmStatus QueryTrackingValuesINT(IntPtr instance, int cosID, out TrackingValues trackingValues)
  {
    trackingValues = new TrackingValues();
    return PXCMTracker_QueryTrackingValues(instance, cosID, trackingValues);
  }

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMTracker_SetRegionOfInterest(IntPtr instance, PXCMRectI32 roi);

  public static bool IsTracking(ETrackingState state)
  {
    return PXCMTracker_IsTracking(state);
  }

  public pxcmStatus SetCameraParameters(string filename)
  {
    return PXCMTracker_SetCameraParameters(instance, filename);
  }

  public pxcmStatus Set2DTrackFromFile(string filename, out int cosID, float widthMM, float heightMM, float qualityThreshold, bool extensible)
  {
    return PXCMTracker_Set2DTrackFromFile(instance, filename, out cosID, widthMM, heightMM, qualityThreshold, extensible);
  }

  public pxcmStatus Set2DTrackFromFile(string filename, out int cosID, float widthMM, float heightMM, float qualityThreshold)
  {
    return Set2DTrackFromFile(filename, out cosID, widthMM, heightMM, qualityThreshold, false);
  }

  public pxcmStatus Set2DTrackFromFile(string filename, out int cosID, bool extensible)
  {
    return Set2DTrackFromFile(filename, out cosID, 0.0f, 0.0f, 0.8f, extensible);
  }

  public pxcmStatus Set2DTrackFromFile(string filename, out int cosID)
  {
    return Set2DTrackFromFile(filename, out cosID, 0.0f, 0.0f, 0.8f, false);
  }

  public pxcmStatus Set2DTrackFromImage(PXCMImage image, out int cosID, float widthMM, float heightMM, float qualityThreshold, bool extensible)
  {
    return PXCMTracker_Set2DTrackFromImage(instance, image.instance, out cosID, widthMM, heightMM, qualityThreshold, extensible);
  }

  public pxcmStatus Set2DTrackFromImage(PXCMImage image, out int cosID, float widthMM, float heightMM, float qualityThreshold)
  {
    return Set2DTrackFromImage(image, out cosID, widthMM, heightMM, qualityThreshold, false);
  }

  public pxcmStatus Set2DTrackFromImage(PXCMImage image, out int cosID, bool extensible)
  {
    return Set2DTrackFromImage(image, out cosID, 0.0f, 0.0f, 0.8f, extensible);
  }

  public pxcmStatus Set2DTrackFromImage(PXCMImage image, out int cosID)
  {
    return Set2DTrackFromImage(image, out cosID, 0.0f, 0.0f, 0.8f, false);
  }

  public pxcmStatus Set3DTrack(string filename, out int firstCosID, out int lastCosID, bool extensible)
  {
    return PXCMTracker_Set3DTrack(instance, filename, out firstCosID, out lastCosID, extensible);
  }

  public pxcmStatus Set3DTrack(string filename, out int firstCosID, out int lastCosID)
  {
    return Set3DTrack(filename, out firstCosID, out lastCosID, false);
  }

  public pxcmStatus RemoveTrackingID(int cosID)
  {
    return PXCMTracker_RemoveTrackingID(instance, cosID);
  }

  public pxcmStatus RemoveAllTrackingIDs()
  {
    return PXCMTracker_RemoveAllTrackingIDs(instance);
  }

  public pxcmStatus Set3DInstantTrack(bool egoMotion, int framesToSkip)
  {
    return PXCMTracker_Set3DInstantTrack(instance, egoMotion, framesToSkip);
  }

  public pxcmStatus Set3DInstantTrack(bool egoMotion)
  {
    return Set3DInstantTrack(egoMotion, 0);
  }

  public pxcmStatus Set3DInstantTrack()
  {
    return Set3DInstantTrack(false);
  }

  public int QueryNumberTrackingValues()
  {
    return PXCMTracker_QueryNumberTrackingValues(instance);
  }

  public pxcmStatus QueryAllTrackingValues(out TrackingValues[] trackingValues)
  {
    return QueryAllTrackingValuesINT(instance, out trackingValues);
  }

  public pxcmStatus QueryTrackingValues(int cosID, out TrackingValues trackingValues)
  {
    return QueryTrackingValuesINT(instance, cosID, out trackingValues);
  }

  public pxcmStatus SetRegionOfInterest(PXCMRectI32 roi)
  {
    return PXCMTracker_SetRegionOfInterest(instance, roi);
  }

  public enum ETrackingState
  {
    ETS_UNKNOWN,
    ETS_NOT_TRACKING,
    ETS_TRACKING,
    ETS_LOST,
    ETS_FOUND,
    ETS_EXTRAPOLATED,
    ETS_INITIALIZED,
    ETS_REGISTERED,
    ETS_INIT_FAILED
  }

  [Serializable]
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public class TrackingValues
  {
    public ETrackingState state;
    public PXCMPoint3DF32 translation;
    public PXCMPoint4DF32 rotation;
    public float quality;
    public double timeElapsed;
    public double trackingTimeMs;
    public int cosID;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
    public string targetName;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
    public string additionalValues;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
    public string sensor;
    public PXCMPointF32 translationImage;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)]
    internal int[] reserved;

    public TrackingValues()
    {
      targetName = "";
      additionalValues = "";
      sensor = "";
      reserved = new int[30];
    }
  }
}
