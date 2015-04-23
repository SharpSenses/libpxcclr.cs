using System;
using System.Runtime.InteropServices;

public class PXCMScenePerception : PXCMBase
{
  public new const int CUID = 1347306323;

  internal PXCMScenePerception(IntPtr instance, bool delete)
    : base(instance, delete)
  {
  }

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMScenePerception_SetVoxelResolution(IntPtr sp, VoxelResolution resolution);

  [DllImport("libpxccpp2c")]
  internal static extern VoxelResolution PXCMScenePerception_QueryVoxelResolution(IntPtr sp);

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMScenePerception_EnableSceneReconstruction(IntPtr sp, [MarshalAs(UnmanagedType.Bool)] bool enabledFlag);

  [DllImport("libpxccpp2c")]
  [return: MarshalAs(UnmanagedType.Bool)]
  internal static extern bool PXCMScenePerception_IsSceneReconstructionEnabled(IntPtr sp);

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMScenePerception_SetInitialPose(IntPtr sp, float[] pose);

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMScenePerception_GetCameraPose(IntPtr sp, [Out] float[] pose);

  [DllImport("libpxccpp2c")]
  internal static extern TrackingAccuracy PXCMScenePerception_QueryTrackingAccuracy(IntPtr sp);

  [DllImport("libpxccpp2c")]
  [return: MarshalAs(UnmanagedType.Bool)]
  internal static extern bool PXCMScenePerception_IsReconstructionUpdated(IntPtr sp);

  [DllImport("libpxccpp2c")]
  internal static extern IntPtr PXCMScenePerception_QueryVolumePreview(IntPtr sp, float[] pose);

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMScenePerception_Reset(IntPtr sp, float[] pose);

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMScenePerception_SetMeshingThresholds(IntPtr sp, float maxDistanceChangeThreshold, float avgDistanceChangeThreshold);

  [DllImport("libpxccpp2c", CharSet = CharSet.Unicode)]
  internal static extern pxcmStatus PXCMScenePerception_SaveMesh(IntPtr sp, string fileName, [MarshalAs(UnmanagedType.Bool)] bool fillHoles);

  [DllImport("libpxccpp2c")]
  internal static extern float PXCMScenePerception_CheckSceneQuality(IntPtr sp, PXCMCapture.Sample sample);

  [DllImport("libpxccpp2c")]
  internal static extern IntPtr PXCMScenePerception_CreatePXCBlockMeshingData(IntPtr sp, int maxBlockMesh, int maxVertices, int maxFaces, [MarshalAs(UnmanagedType.Bool)] bool useColor);

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMScenePerception_DoMeshingUpdate(IntPtr sp, IntPtr data, [MarshalAs(UnmanagedType.Bool)] bool fillHoles);

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMScenePerception_FillDepthImage(IntPtr sp, IntPtr dImage);

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMScenePerception_GetNormals(IntPtr sp, [Out] PXCMPoint3DF32[] normals);

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMScenePerception_GetVertices(IntPtr sp, [Out] PXCMPoint3DF32[] vertices);

  [DllImport("libpxccpp2c", CharSet = CharSet.Unicode)]
  internal static extern pxcmStatus PXCMScenePerception_SaveCurrentState(IntPtr sp, string fileName);

  [DllImport("libpxccpp2c", CharSet = CharSet.Unicode)]
  internal static extern pxcmStatus PXCMScenePerception_LoadState(IntPtr sp, string fileName);

  public pxcmStatus SetVoxelResolution(VoxelResolution resolution)
  {
    return PXCMScenePerception_SetVoxelResolution(instance, resolution);
  }

  public VoxelResolution QueryVoxelResolution()
  {
    return PXCMScenePerception_QueryVoxelResolution(instance);
  }

  public pxcmStatus EnableSceneReconstruction(bool enabledFlag)
  {
    return PXCMScenePerception_EnableSceneReconstruction(instance, enabledFlag);
  }

  public bool IsSceneReconstructionEnabled()
  {
    return PXCMScenePerception_IsSceneReconstructionEnabled(instance);
  }

  public pxcmStatus SetInitialPose(float[] pose)
  {
    if (pose == null || pose.Length != 12)
      return pxcmStatus.PXCM_STATUS_PARAM_UNSUPPORTED;
    return PXCMScenePerception_SetInitialPose(instance, pose);
  }

  public TrackingAccuracy QueryTrackingAccuracy()
  {
    return PXCMScenePerception_QueryTrackingAccuracy(instance);
  }

  public pxcmStatus GetCameraPose(float[] pose)
  {
    if (pose == null || pose.Length != 12)
      return pxcmStatus.PXCM_STATUS_PARAM_UNSUPPORTED;
    return PXCMScenePerception_GetCameraPose(instance, pose);
  }

  public bool IsReconstructionUpdated()
  {
    return PXCMScenePerception_IsReconstructionUpdated(instance);
  }

  public PXCMImage QueryVolumePreview(float[] pose)
  {
    if (pose == null || pose.Length != 12)
      return null;
    return new PXCMImage(PXCMScenePerception_QueryVolumePreview(instance, pose), true);
  }

  public pxcmStatus Reset(float[] pose)
  {
    return PXCMScenePerception_Reset(instance, pose);
  }

  public pxcmStatus Reset()
  {
    return Reset(new float[12]);
  }

  public pxcmStatus SetMeshingThresholds(float maxDistanceChangeThreshold, float avgDistanceChangeThreshold)
  {
    return PXCMScenePerception_SetMeshingThresholds(instance, maxDistanceChangeThreshold, avgDistanceChangeThreshold);
  }

  public PXCMBlockMeshingData CreatePXCMBlockMeshingData(int maxBlockMesh, int maxVertices, int maxFaces, bool useColor)
  {
    return new PXCMBlockMeshingData(PXCMScenePerception_CreatePXCBlockMeshingData(instance, maxBlockMesh, maxVertices, maxFaces, useColor), true);
  }

  public PXCMBlockMeshingData CreatePXCMBlockMeshingData(int maxBlockMesh, int maxVertices, int maxFaces)
  {
    return CreatePXCMBlockMeshingData(maxBlockMesh, maxFaces, maxVertices, true);
  }

  public PXCMBlockMeshingData CreatePXCMBlockMeshingData()
  {
    return CreatePXCMBlockMeshingData(-1, -1, -1, true);
  }

  public pxcmStatus DoMeshingUpdate(PXCMBlockMeshingData pBlockMeshingUpdateInfo, bool bFillHoles)
  {
    return PXCMScenePerception_DoMeshingUpdate(instance, pBlockMeshingUpdateInfo.instance, bFillHoles);
  }

  public pxcmStatus DoMeshingUpdate(PXCMBlockMeshingData pBlockMeshingUpdateInfo)
  {
    return DoMeshingUpdate(pBlockMeshingUpdateInfo, false);
  }

  public pxcmStatus SaveMesh(string fileName, bool fillHoles)
  {
    return PXCMScenePerception_SaveMesh(instance, fileName, fillHoles);
  }

  public pxcmStatus SaveMesh(string fileName)
  {
    return SaveMesh(fileName, false);
  }

  public float CheckSceneQuality(PXCMCapture.Sample sample)
  {
    return PXCMScenePerception_CheckSceneQuality(instance, sample);
  }

  public pxcmStatus FillDepthImage(PXCMImage depthImage)
  {
    return PXCMScenePerception_FillDepthImage(instance, depthImage.instance);
  }

  public pxcmStatus GetNormals(PXCMPoint3DF32[] normals)
  {
    return PXCMScenePerception_GetNormals(instance, normals);
  }

  public pxcmStatus GetVertices(PXCMPoint3DF32[] vertices)
  {
    return PXCMScenePerception_GetVertices(instance, vertices);
  }

  public pxcmStatus SaveCurrentState(string fileName)
  {
    return PXCMScenePerception_SaveCurrentState(instance, fileName);
  }

  public pxcmStatus LoadState(string fileName)
  {
    return PXCMScenePerception_LoadState(instance, fileName);
  }

  public enum TrackingAccuracy
  {
    HIGH,
    LOW,
    MED,
    FAILED
  }

  public enum VoxelResolution
  {
    LOW_RESOLUTION,
    MED_RESOLUTION,
    HIGH_RESOLUTION
  }
}
