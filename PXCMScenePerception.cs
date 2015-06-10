using System;
using System.Runtime.InteropServices;

public class PXCMScenePerception : PXCMBase {
    public enum MeshResolution {
        LOW_RESOLUTION_MESH,
        MED_RESOLUTION_MESH,
        HIGH_RESOLUTION_MESH
    }

    public enum TrackingAccuracy {
        HIGH,
        LOW,
        MED,
        FAILED
    }

    public enum VoxelResolution {
        LOW_RESOLUTION,
        MED_RESOLUTION,
        HIGH_RESOLUTION
    }

    public new const int CUID = 1347306323;

    internal PXCMScenePerception(IntPtr instance, bool delete)
        : base(instance, delete) {}

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMScenePerception_SetVoxelResolution(IntPtr sp, VoxelResolution resolution);

    [DllImport("libpxccpp2c")]
    internal static extern VoxelResolution PXCMScenePerception_QueryVoxelResolution(IntPtr sp);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMScenePerception_EnableSceneReconstruction(IntPtr sp,
        [MarshalAs(UnmanagedType.Bool)] bool enabledFlag);

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
    internal static extern pxcmStatus PXCMScenePerception_SetMeshingThresholds(IntPtr sp,
        float maxDistanceChangeThreshold, float avgDistanceChangeThreshold);

    [DllImport("libpxccpp2c", CharSet = CharSet.Unicode)]
    internal static extern pxcmStatus PXCMScenePerception_SaveMesh(IntPtr sp, string fileName,
        [MarshalAs(UnmanagedType.Bool)] bool fillHoles);

    [DllImport("libpxccpp2c")]
    internal static extern float PXCMScenePerception_CheckSceneQuality(IntPtr sp, PXCMCapture.Sample sample);

    [DllImport("libpxccpp2c")]
    internal static extern IntPtr PXCMScenePerception_CreatePXCBlockMeshingData(IntPtr sp, int maxBlockMesh,
        int maxVertices, int maxFaces, [MarshalAs(UnmanagedType.Bool)] bool useColor);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMScenePerception_DoMeshingUpdate(IntPtr sp, IntPtr data,
        [MarshalAs(UnmanagedType.Bool)] bool fillHoles);

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

    [DllImport("libpxccpp2c")]
    internal static extern IntPtr PXCMScenePerception_CreatePXCSurfaceVoxelsData(IntPtr sp,
        int initialEstimateOfVoxelCount, bool bUseColor);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMScenePerception_ExportSurfaceVoxels(IntPtr sp, IntPtr pSurfaceVoxelsData,
        IntPtr lowerLeftFrontPoint, IntPtr upperRightRearPoint);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMScenePerception_ExtractPlanes(IntPtr sp, PXCMCapture.Sample sample,
        float minPlaneArea, int maxPlaneNumber, float[] pEquations, byte[] pIndexImage);

    internal pxcmStatus ExtractPlanesINT(IntPtr sp, PXCMCapture.Sample sample, float minPlaneArea, int maxPlaneNumber,
        float[] pPlaneEq, byte[] pPlaneIndexImg) {
        return PXCMScenePerception_ExtractPlanes(sp, sample, minPlaneArea, maxPlaneNumber, pPlaneEq, pPlaneIndexImg);
    }

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMScenePerception_SetMeshingResolution(IntPtr sp, MeshResolution meshResolution);

    [DllImport("libpxccpp2c")]
    internal static extern int PXCMScenePerception_QueryMeshingResolution(IntPtr sp);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMScenePerception_QueryMeshingThresholds(IntPtr sp,
        out float maxDistanceChangeThreshold, out float avgDistanceChange);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMScenePerception_SetMeshingRegion(IntPtr sp, IntPtr lowerLeftFrontPoint,
        IntPtr upperRightRearPoint);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMScenePerception_ClearMeshingRegion(IntPtr sp);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMScenePerception_SetCameraPose(IntPtr sp, float[] pose);

    [DllImport("libpxccpp2c")]
    internal static extern float PXCMScenePerception_QueryVoxelSize(IntPtr sp);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMScenePerception_GetInternalCameraIntrinsics(IntPtr sp, IntPtr spIntrinsics);

    public pxcmStatus SetVoxelResolution(VoxelResolution resolution) {
        return PXCMScenePerception_SetVoxelResolution(instance, resolution);
    }

    public VoxelResolution QueryVoxelResolution() {
        return PXCMScenePerception_QueryVoxelResolution(instance);
    }

    public pxcmStatus EnableSceneReconstruction(bool enabledFlag) {
        return PXCMScenePerception_EnableSceneReconstruction(instance, enabledFlag);
    }

    public bool IsSceneReconstructionEnabled() {
        return PXCMScenePerception_IsSceneReconstructionEnabled(instance);
    }

    public pxcmStatus SetInitialPose(float[] pose) {
        if (pose == null || pose.Length != 12) {
            return pxcmStatus.PXCM_STATUS_PARAM_UNSUPPORTED;
        }
        return PXCMScenePerception_SetInitialPose(instance, pose);
    }

    public TrackingAccuracy QueryTrackingAccuracy() {
        return PXCMScenePerception_QueryTrackingAccuracy(instance);
    }

    public pxcmStatus GetCameraPose(float[] pose) {
        if (pose == null || pose.Length != 12) {
            return pxcmStatus.PXCM_STATUS_PARAM_UNSUPPORTED;
        }
        return PXCMScenePerception_GetCameraPose(instance, pose);
    }

    public bool IsReconstructionUpdated() {
        return PXCMScenePerception_IsReconstructionUpdated(instance);
    }

    public PXCMImage QueryVolumePreview(float[] pose) {
        if (pose == null || pose.Length != 12) {
            return null;
        }
        return new PXCMImage(PXCMScenePerception_QueryVolumePreview(instance, pose), true);
    }

    public pxcmStatus Reset(float[] pose) {
        return PXCMScenePerception_Reset(instance, pose);
    }

    public pxcmStatus Reset() {
        var pose = new float[12];
        for (var index = 0; index < 12; ++index) {
            pose[index] = 0.0f;
        }
        return Reset(pose);
    }

    public pxcmStatus SetMeshingThresholds(float maxDistanceChangeThreshold, float avgDistanceChangeThreshold) {
        return PXCMScenePerception_SetMeshingThresholds(instance, maxDistanceChangeThreshold, avgDistanceChangeThreshold);
    }

    public PXCMBlockMeshingData CreatePXCMBlockMeshingData(int maxBlockMesh, int maxVertices, int maxFaces,
        bool useColor) {
        return
            new PXCMBlockMeshingData(
                PXCMScenePerception_CreatePXCBlockMeshingData(instance, maxBlockMesh, maxVertices, maxFaces, useColor),
                true);
    }

    public PXCMBlockMeshingData CreatePXCMBlockMeshingData(int maxBlockMesh, int maxVertices, int maxFaces) {
        return CreatePXCMBlockMeshingData(maxBlockMesh, maxFaces, maxVertices, true);
    }

    public PXCMBlockMeshingData CreatePXCMBlockMeshingData() {
        return CreatePXCMBlockMeshingData(-1, -1, -1, true);
    }

    public pxcmStatus DoMeshingUpdate(PXCMBlockMeshingData pBlockMeshingUpdateInfo, bool bFillHoles) {
        return PXCMScenePerception_DoMeshingUpdate(instance, pBlockMeshingUpdateInfo.instance, bFillHoles);
    }

    public pxcmStatus DoMeshingUpdate(PXCMBlockMeshingData pBlockMeshingUpdateInfo) {
        return DoMeshingUpdate(pBlockMeshingUpdateInfo, false);
    }

    public pxcmStatus SaveMesh(string fileName, bool fillHoles) {
        return PXCMScenePerception_SaveMesh(instance, fileName, fillHoles);
    }

    public pxcmStatus SaveMesh(string fileName) {
        return SaveMesh(fileName, false);
    }

    public float CheckSceneQuality(PXCMCapture.Sample sample) {
        return PXCMScenePerception_CheckSceneQuality(instance, sample);
    }

    public pxcmStatus FillDepthImage(PXCMImage depthImage) {
        return PXCMScenePerception_FillDepthImage(instance, depthImage.instance);
    }

    public pxcmStatus GetNormals(PXCMPoint3DF32[] normals) {
        return PXCMScenePerception_GetNormals(instance, normals);
    }

    public pxcmStatus GetVertices(PXCMPoint3DF32[] vertices) {
        return PXCMScenePerception_GetVertices(instance, vertices);
    }

    public pxcmStatus SaveCurrentState(string fileName) {
        return PXCMScenePerception_SaveCurrentState(instance, fileName);
    }

    public pxcmStatus LoadState(string fileName) {
        return PXCMScenePerception_LoadState(instance, fileName);
    }

    public PXCMSurfaceVoxelsData CreatePXCMSurfaceVoxelsData(int initialEstimateOfVoxelCount, bool bUseColor) {
        var surfaceVoxelsData = PXCMScenePerception_CreatePXCSurfaceVoxelsData(instance, initialEstimateOfVoxelCount,
            bUseColor);
        if (!(surfaceVoxelsData == IntPtr.Zero)) {
            return new PXCMSurfaceVoxelsData(surfaceVoxelsData, true);
        }
        return null;
    }

    public PXCMSurfaceVoxelsData CreatePXCMSurfaceVoxelsData() {
        return CreatePXCMSurfaceVoxelsData(-1, false);
    }

    public pxcmStatus ExportSurfaceVoxels(PXCMSurfaceVoxelsData surfaceVoxelsData, PXCMPoint3DF32 lowerLeftFrontPoint,
        PXCMPoint3DF32 upperRightRearPoint) {
        var num1 = Marshal.AllocHGlobal(Marshal.SizeOf(lowerLeftFrontPoint));
        var num2 = Marshal.AllocHGlobal(Marshal.SizeOf(upperRightRearPoint));
        Marshal.StructureToPtr(lowerLeftFrontPoint, num1, true);
        Marshal.StructureToPtr(upperRightRearPoint, num2, true);
        var pxcmStatus = PXCMScenePerception_ExportSurfaceVoxels(instance, surfaceVoxelsData.instance, num1, num2);
        Marshal.FreeHGlobal(num1);
        Marshal.FreeHGlobal(num2);
        var num3 = IntPtr.Zero;
        var num4 = IntPtr.Zero;
        return pxcmStatus;
    }

    public pxcmStatus ExportSurfaceVoxels(PXCMSurfaceVoxelsData surfaceVoxelsData) {
        return PXCMScenePerception_ExportSurfaceVoxels(instance, surfaceVoxelsData.instance, IntPtr.Zero, IntPtr.Zero);
    }

    public pxcmStatus ExtractPlanes(PXCMCapture.Sample sample, float minPlaneArea, int maxPlaneNumber,
        out float[,] pPlaneEq, out byte[] pPlaneIndexImg) {
        pPlaneIndexImg = new byte[76800];
        pPlaneEq = new float[maxPlaneNumber, 4];
        var pPlaneEq1 = new float[maxPlaneNumber*4];
        var planesInt = ExtractPlanesINT(instance, sample, minPlaneArea, maxPlaneNumber, pPlaneEq1, pPlaneIndexImg);
        for (var index1 = 0; index1 < maxPlaneNumber; ++index1) {
            for (var index2 = 0; index2 < 4; ++index2) {
                pPlaneEq[index1, index2] = pPlaneEq1[index1*4 + index2];
            }
        }
        return planesInt;
    }

    public pxcmStatus SetMeshingResolution(MeshResolution meshResolution) {
        return PXCMScenePerception_SetMeshingResolution(instance, meshResolution);
    }

    public MeshResolution QueryMeshingResolution() {
        return (MeshResolution) PXCMScenePerception_QueryMeshingResolution(instance);
    }

    public pxcmStatus QueryMeshingThresholds(out float maxDistanceChangeThreshold, out float avgDistanceChange) {
        maxDistanceChangeThreshold = 0.0f;
        avgDistanceChange = 0.0f;
        return PXCMScenePerception_QueryMeshingThresholds(instance, out maxDistanceChangeThreshold,
            out avgDistanceChange);
    }

    public pxcmStatus SetMeshingRegion(PXCMPoint3DF32 lowerLeftFrontPoint, PXCMPoint3DF32 upperRightRearPoint) {
        var num1 = Marshal.AllocHGlobal(Marshal.SizeOf(lowerLeftFrontPoint));
        var num2 = Marshal.AllocHGlobal(Marshal.SizeOf(upperRightRearPoint));
        Marshal.StructureToPtr(lowerLeftFrontPoint, num1, true);
        Marshal.StructureToPtr(upperRightRearPoint, num2, true);
        var pxcmStatus = PXCMScenePerception_SetMeshingRegion(instance, num1, num2);
        Marshal.FreeHGlobal(num1);
        Marshal.FreeHGlobal(num2);
        var num3 = IntPtr.Zero;
        var num4 = IntPtr.Zero;
        return pxcmStatus;
    }

    public pxcmStatus ClearMeshingRegion() {
        return PXCMScenePerception_ClearMeshingRegion(instance);
    }

    public pxcmStatus SetCameraPose(float[] pose) {
        return PXCMScenePerception_SetCameraPose(instance, pose);
    }

    public float QueryVoxelSize() {
        return PXCMScenePerception_QueryVoxelSize(instance);
    }

    public pxcmStatus GetInternalCameraIntrinsics(out ScenePerceptionIntrinsics spIntrinsics) {
        spIntrinsics = new ScenePerceptionIntrinsics();
        var num1 = Marshal.AllocHGlobal(Marshal.SizeOf(spIntrinsics));
        Marshal.StructureToPtr(spIntrinsics, num1, false);
        var cameraIntrinsics = PXCMScenePerception_GetInternalCameraIntrinsics(instance, num1);
        if (cameraIntrinsics >= pxcmStatus.PXCM_STATUS_NO_ERROR) {
            spIntrinsics = (ScenePerceptionIntrinsics) Marshal.PtrToStructure(num1, typeof (ScenePerceptionIntrinsics));
        }
        Marshal.FreeHGlobal(num1);
        var num2 = IntPtr.Zero;
        return cameraIntrinsics;
    }

    [Serializable]
    public struct MeshingUpdateInfo {
        public bool blockMeshesRequired;
        public bool colorsRequired;
        public bool countOfBlockMeshesRequired;
        public bool countOfFacesRequired;
        public bool countOfVeticesRequired;
        public bool facesRequired;
        public bool verticesRequired;
    }

    [Serializable]
    public struct ScenePerceptionIntrinsics {
        public PXCMPointF32 focalLength;
        public PXCMSizeI32 imageSize;
        public PXCMPointF32 principalPoint;
    }
}