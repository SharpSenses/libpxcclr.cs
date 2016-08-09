/*******************************************************************************

  INTEL CORPORATION PROPRIETARY INFORMATION
  This software is supplied under the terms of a license agreement or nondisclosure
  agreement with Intel Corporation and may not be copied or disclosed except in
  accordance with the terms of that agreement
  Copyright(c) 2014 Intel Corporation. All Rights Reserved.

 *******************************************************************************/
using System;
using System.Runtime.InteropServices;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif


/// <summary>
/// Instance of this interface class can be created using 
/// PXCMScenePerception.CreatePXCSurfaceVoxelsData(...). ExportSurfaceVoxels
/// function fills the data buffer. It's client's responsibility to 
/// explicitly release the memory by calling Dispose() on PXCMSurfaceVoxelsData.
/// </summary>

public partial class PXCMSurfaceVoxelsData : PXCMBase
{
    new public const Int32 CUID = 0x56535053;

    /// <summary>
    /// Returns number of surface voxels present in the buffer. 
    /// This function is expected to be used after successful
    /// call to ExportSurfaceVoxels().
    /// </summary>
    public Int32 QueryNumberOfSurfaceVoxels()
    {
        return PXCMSurfaceVoxelsData_QueryNumberOfSurfaceVoxels(instance);
    }

    /// <summary>
    /// Returns an array to center of surface voxels extracted by 
    /// ExportSurfaceVoxels. This function is expected to be used after successful
    /// call to ExportSurfaceVoxels(). Valid range is [0, 3*QueryNumberOfSurfaceVoxels()).
    /// </summary>
    public Single[] QueryCenterOfSurfaceVoxels()
    {
        int nvoxels = QueryNumberOfSurfaceVoxels();
        if (nvoxels <= 0) return null;

        Single[] voxels = new Single[3 * nvoxels];

        QueryCenterOfSurfaceVoxelsINT(instance, voxels, nvoxels);
        return voxels;
    }

    /// <summary>
    /// Returns an array of colors with length 3*QueryNumberOfSurfaceVoxels(). Three
    /// color channels (RGB) per voxel. This function will return null, if 
    /// PXCMSurfaceVoxelsData was created using PXCMScenePerception.CreatePXCMSurfaceVoxelsData with bUseColor set to false.
    /// </summary>
    public Byte[] QuerySurfaceVoxelsColor()
    {
        int nvoxels = QueryNumberOfSurfaceVoxels();
        if (nvoxels <= 0) return null;

        Byte[] colors = new Byte[3 * nvoxels];

        QuerySurfaceVoxelsColorINT(instance, colors, nvoxels);
        return colors;
    }

    /// <summary>
    /// Sets number of surface voxels to 0. However it doesn't release memory. 
    /// It should be used when you reset scene perception using 
    /// PXCMScenePerception.Reset() client should Reset PXCMSurfaceVoxelsData 
    /// when scene perception is Reset to stay in sync with the scene perception.
    /// </summary>
    public pxcmStatus Reset()
    {
        return PXCMSurfaceVoxelsData_Reset(instance);
    }

    /* constructors and misc */
    internal PXCMSurfaceVoxelsData(IntPtr instance, Boolean delete)
        : base(instance, delete)
    {
    }
};


/// <summary>
/// An instance of this interface can be created using 
/// PXCMScenePerception.CreatePXCMBlockMeshingData method DoMeshingUpdate
/// function fills all the buffer with the data. It's client's responsibility to 
/// explicitly release the memory by calling Dispose() on PXCMBlockMeshingData.
/// </summary>
public partial class PXCMBlockMeshingData : PXCMBase
{
    new public const Int32 CUID = 0x4d425053;

    /// <summary>
    /// Returns number of PXCMBlockMesh present inside the buffer
    /// returned by QueryBlockMeshes(). This function is expected to be used 
    /// after successful call to DoMeshingUpdate(...).
    /// </summary>
    public Int32 QueryNumberOfBlockMeshes()
    {
        return PXCMBlockMeshingData_QueryNumberOfBlockMeshes(instance);
    }

    /// <summary>
    /// Returns number of vertices present in the buffer returned by 
    /// QueryVertices(). This function is expected to be used after successful
    /// call to DoMeshingUpdate(...).
    /// </summary>
    public Int32 QueryNumberOfVertices()
    {
        return PXCMBlockMeshingData_QueryNumberOfVertices(instance);
    }

    /// <summary>
    /// Returns number of faces in the buffer returned by 
    /// QueryFaces(). This function is expected to be used after successful
    /// call to DoMeshingUpdate(...).
    /// </summary>
    public Int32 QueryNumberOfFaces()
    {
        return PXCMBlockMeshingData_QueryNumberOfFaces(instance);
    }

    /// <summary>
    /// Returns maximum number of PXCMBlockMesh that can be returned by 
    /// DoMeshingUpdate. This value remains same throughout the lifetime of the
    /// instance.
    /// </summary>
    public Int32 QueryMaxNumberOfBlockMeshes()
    {
        return PXCMBlockMeshingData_QueryMaxNumberOfBlockMeshes(instance);
    }

    /// <summary>
    /// Returns maximum number of vertices that can be returned by 
    /// PXCMBlockMeshingData. This value remains same throughout the lifetime of
    /// the instance.
    /// </summary>
    public Int32 QueryMaxNumberOfVertices()
    {
        return PXCMBlockMeshingData_QueryMaxNumberOfVertices(instance);
    }

    /// <summary>
    /// Returns maximum number of faces that can be returned by 
    /// PXCMBlockMeshingData. This value remains same throughout the lifetime of 
    /// the instance.
    /// </summary>
    public Int32 QueryMaxNumberOfFaces()
    {
        return PXCMBlockMeshingData_QueryMaxNumberOfFaces(instance);
    }

    /// <summary>
    /// Describes each BlockMesh present inside list returned by 
    /// QueryBlockMeshes().
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class PXCMBlockMesh
    {
        public Int32 meshId;				// Unique ID to identify each PXCBlockMesh
        public Int32 vertexStartIndex;      // Starting index of the vertex inside Vertex Buffer obtained using QueryVertices()
        public Int32 numVertices;	        // Total number of vertices inside this PXCMBlockMesh
        public Int32 faceStartIndex;		// Starting index of the face list in a MeshFaces Buffer obtained using QueryFaces()
        public Int32 numFaces;	 		    // Number of faces forming the mesh inside this PXCMBlockMesh 
        public PXCMPoint3DF32 min3dPoint;   // Minimum point for the axis aligned bounding box containing the mesh piece
        public PXCMPoint3DF32 max3dPoint;   // Maximum point for the axis aligned bounding box containing the mesh piece
        public Single maxDistanceChange;    // Maximum change in the distance field due to accumulation since this block was last meshed
        public Single avgDistanceChange;	// Average change in the distance field due to accumulation since this block was last meshed
    };

    /// <summary>
    /// Returns an array of PXCMBlockMesh objects with length same as 
    /// QueryNumberOfBlockMeshes().
    /// </summary>
    public PXCMBlockMesh[] QueryBlockMeshes(PXCMBlockMesh[] meshes)
    {
        int nmeshes = QueryNumberOfBlockMeshes();
        if (nmeshes <= 0) return null;

        if (meshes == null)
            meshes = new PXCMBlockMesh[nmeshes];

        QueryBlockMeshesINT(instance, meshes, nmeshes);
        return meshes;
    }

    /// <summary>
    /// Returns an array of PXCMBlockMesh objects with length same as 
    /// QueryNumberOfBlockMeshes().
    /// </summary>
    public PXCMBlockMesh[] QueryBlockMeshes()
    {
        return QueryBlockMeshes(null);
    }

    /// <summary>
    /// Returns an array of float points with length 4*QueryNumberOfVertices()
    /// Each vertex consists of 4 float points: (x, y, z) coordinates in meter
    /// unit + a confidence value. The confidence value is in the range [0, 1] 
    /// indicating how confident scene perception is about the presence of 
    /// the vertex. 
    /// </summary>
    public Single[] QueryVertices(Single[] vertices)
    {
        int nvertices = QueryNumberOfVertices();
        if (nvertices <= 0) return null;

        if (vertices == null)
            vertices = new Single[4 * nvertices];

        QueryVerticesINT(instance, vertices, nvertices);
        return vertices;
    }

    /// <summary>
    /// Returns an array of float points with length 4*QueryNumberOfVertices()
    /// Each vertex is consist of 4 float points: (x, y, z) coordinates in meter
    /// unit + a confidence value. The confidence value is in the range [0, 1] 
    /// indicating how confident scene perception is about the presence of 
    /// the vertex. 
    /// </summary>
    public Single[] QueryVertices()
    {
        return QueryVertices(null);
    }

    /// <summary>
    /// Returns an array of colors with length 3*QueryNumberOfVertices(). Three
    /// color channels (RGB) per vertex. This function will return null, if 
    /// PXCMBlockMeshingData was created using 
    /// PXCMScenePerception.CreatePXCMBlockMeshingData(...) with bUseColor set to false.
    /// </summary>
    public Byte[] QueryVerticesColor(Byte[] colors)
    {
        int nvertices = QueryNumberOfVertices();
        if (nvertices <= 0) return null;

        if (colors == null)
            colors = new Byte[3 * nvertices];

        QueryVerticesColorINT(instance, colors, nvertices);
        return colors;
    }

    /// <summary>
    /// Returns an array of colors with length 3*QueryNumberOfVertices(). Three
    /// color channels (RGB) per vertex. This function will return NULL, if 
    /// PXCMBlockMeshingData was created using 
    /// PXCMScenePerception.CreatePXCMBlockMeshingData(...) with bUseColor set to false.
    /// </summary>
    public Byte[] QueryVerticesColor()
    {
        return QueryVerticesColor(null);
    }

    /// <summary>
    /// Returns an array of faces forming the mesh (3 Int32 indices 
    /// per triangle) valid range is from [0, 3*QueryNumberOfFaces()].
    /// </summary>
    public Int32[] QueryFaces(Int32[] faces)
    {
        int nfaces = QueryNumberOfFaces();
        if (nfaces <= 0) return null;

        if (faces == null)
            faces = new Int32[3 * nfaces];

        QueryFacesINT(instance, faces, nfaces);
        return faces;
    }

    /// <summary>
    /// Returns an array of faces forming the mesh (3 Int32 indices 
    /// per triangle) valid range is from [0, 3*QueryNumberOfFaces()].
    /// </summary>
    public Int32[] QueryFaces()
    {
        return QueryFaces(null);
    }

    /// <summary>
    /// Sets number of BlockMeshes, number of vertices and number of faces to
    /// 0. However it doesn't release memory. It should be used when you reset 
    /// scene perception using PXCMScenePerception.Reset(). Client should Reset 
    /// PXCMBlockMeshingData when scene perception is reset to stay in sync with
    /// the scene perception.
    /// </summary>
    public pxcmStatus Reset()
    {
        return PXCMBlockMeshingData_Reset(instance);
    }

    /* constructors and misc */
    internal PXCMBlockMeshingData(IntPtr instance, Boolean delete)
        : base(instance, delete)
    {
    }
};

public partial class PXCMScenePerception : PXCMBase
{
    new public const Int32 CUID = 0x504e4353;

    public enum TrackingAccuracy
    {
        HIGH,
        LOW,
        MED,
        FAILED
    };

    public enum VoxelResolution
    {
        LOW_RESOLUTION = 0,
        MED_RESOLUTION = 1,
        HIGH_RESOLUTION = 2
    };

    public enum MeshResolution
    {
        LOW_RESOLUTION_MESH,
        MED_RESOLUTION_MESH,
        HIGH_RESOLUTION_MESH
    };

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class MeshingUpdateInfo
    {
        public Boolean countOfBlockMeshesRequired;
        public Boolean blockMeshesRequired;
        public Boolean countOfVeticesRequired;
        public Boolean verticesRequired;
        public Boolean countOfFacesRequired;
        public Boolean facesRequired;
        public Boolean colorsRequired;
    };

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class ScenePerceptionIntrinsics
    {
        public PXCMSizeI32 imageSize;
        public PXCMPointF32 focalLength;
        public PXCMPointF32 principalPoint;
    };

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class SaveMeshInfo
    {
        public Boolean fillMeshHoles;
        public Boolean saveMeshColor;
        public MeshResolution meshResolution;
    };

    /// <summary>
    /// SetVoxelResolution sets volume resolution for the scene 
    /// perception. The VoxelResolution is locked when 
    /// PXCMSenseManager.Init() is called.
    /// Afterwards value for VoxelResolution remains same throughout the 
    /// lifetime of PXCMSenseManager. The default value of voxel 
    /// resolution is LOW_RESOLUTION.
    /// </summary>
    /// <param name="voxelResolution"> Resolution of the three dimensional
    /// reconstruction. 
    /// Possible values are:
    /// LOW_RESOLUTION:  For room-sized scenario (4/256m)
    /// MED_RESOLUTION:  For table-top-sized scenario (2/256m)
    /// HIGH_RESOLUTION: For object-sized scenario (1/256m)
    /// Choosing HIGH_RESOLUTION in a room-size environment may degrade the
    /// tracking robustness and quality. Choosing LOW_RESOLUTION in an 
    /// object-sized scenario may result in a reconstructed model missing 
    /// the fine details. </param>
    /// 
    /// <returns>PXCM_STATUS_NO_ERROR if it succeeds, returns 
    /// PXCM_STATUS_ITEM_UNAVAILABLE if called after making call to 
    /// PXCMSenseManager.Init().</returns>
    public pxcmStatus SetVoxelResolution(VoxelResolution resolution)
    {
        return PXCMScenePerception_SetVoxelResolution(instance, resolution);
    }

    /// <summary>
    /// To get voxel resolution used by the scene 
    /// perception module. Please refer to SetVoxelResolution(...) for more details.
    /// </summary> 
    /// <returns>Returns current value of VoxelResolution used by the 
    /// scene perception module.</returns>
    public VoxelResolution QueryVoxelResolution()
    {
        return PXCMScenePerception_QueryVoxelResolution(instance);
    }

    /// <summary>
    /// Allows user to enable/disable integration of upcoming 
    /// camera stream into 3D volume. If disabled the volume will not be 
    /// updated. However scene perception will still keep tracking the 
    /// camera. This is a control parameter which can be updated before 
    /// passing every frame to the module.
    /// </summary> 
    /// <param name="enableFlag"> Enable/Disable flag for integrating depth
    /// data into the 3D volumetric representation.</param>
    /// 
    /// <returns>  PXCM_STATUS_NO_ERROR if it succeeds, otherwise returns 
    /// the error code.</returns>
    public pxcmStatus EnableSceneReconstruction(Boolean enabledFlag)
    {
        return PXCMScenePerception_EnableSceneReconstruction(instance, enabledFlag);
    }

    /// <summary>
    /// Allows user to to check Whether integration of upcoming 
    /// camera stream into 3D volume is enabled or disabled. 
    /// </summary>
    /// <returns>  True, if integrating depth data into the 3D volumetric</returns>
    /// representation is enabled.
    public Boolean IsSceneReconstructionEnabled()
    {
        return PXCMScenePerception_IsSceneReconstructionEnabled(instance);
    }

    /// <summary>
    /// Allows user to set the initial camera pose.
    /// This function is only available before first frame is passed to the
    /// module. Once the first frame is passed the initial camera pose is 
    /// locked and this function will be unavailable. If this function is 
    /// not used then the module default pose as the 
    /// initial pose for tracking for the device with no platform IMU and 
    /// for device with platform IMU the tracking pose will be computed 
    /// using gravity vector to align 3D volume with gravity when the 
    /// first frame is passed to the module.
    /// </summary>
    /// <param name="pose"> Array of 12 pxcF32 that stores initial camera pose
    /// user wishes to set in row-major order. Camera pose is specified in a 
    /// 3 by 4 matrix [R | T] = [Rotation Matrix | Translation Vector]
    /// where R = [ r11 r12 r13 ]
    /// [ r21 r22 r23 ] 
    /// [ r31 r32 r33 ]
    /// T = [ tx  ty  tz  ]
    /// Pose Array Layout = [r11 r12 r13 tx r21 r22 r23 ty r31 r32 r33 tz]
    /// Translation vector is in meters.</param>
    /// 
    /// <returns>  If successful it returns PXCM_STATUS_NO_ERROR,
    /// otherwise returns error code if invalid pose is passed or the 
    /// function is called after passing the first frame.</returns>
    public pxcmStatus SetInitialPose(Single[] pose)
    {
        if (pose == null || pose.Length != 12)
            return pxcmStatus.PXCM_STATUS_PARAM_UNSUPPORTED;
        return PXCMScenePerception_SetInitialPose(instance, pose);
    }

    /// <summary>
    /// Allows user to get tracking accuracy of the last frame 
    /// processed by the module. We expect users to call this function 
    /// after successful PXCMSenseManager.AcquireFrame(...) call and before 
    /// calling PXCMSenesManager.ReleaseFrame(). If tracking accuracy is FAILED 
    /// the volume data and camera pose are not updated.
    /// </summary>
    /// <returns>  TrackingAccuracy which can be HIGH, LOW, MED or FAILED.</returns>
    public TrackingAccuracy QueryTrackingAccuracy()
    {
        return PXCMScenePerception_QueryTrackingAccuracy(instance);
    }

    /// <summary>
    /// Allows user to access camera's latest pose. The 
    /// correctness of the pose depends on value obtained from 
    /// QueryTrackingAccuracy().
    /// </summary>
    /// <param name="pose"> Array of 12 Single to store camera pose in
    /// row-major order. Camera pose is specified in a 3 by 4 matrix 
    /// [R | T] = [Rotation Matrix | Translation Vector]
    /// where R = [ r11 r12 r13 ]
    /// [ r21 r22 r23 ] 
    /// [ r31 r32 r33 ]
    /// T = [ tx  ty  tz  ]
    /// Pose Array Layout = [r11 r12 r13 tx r21 r22 r23 ty r31 r32 r33 tz]
    /// Translation vector is in meters.</param>
    /// <returns>  PXCM_STATUS_NO_ERROR, If the function succeeds.
    /// Otherwise error code will be returned.</returns>
    public pxcmStatus GetCameraPose(Single[] pose)
    {
        if (pose == null || pose.Length != 12)
            return pxcmStatus.PXCM_STATUS_PARAM_UNSUPPORTED;
        return PXCMScenePerception_GetCameraPose(instance, pose);
    }

    /// <summary>
    /// Allows user to check whether the 3D volume was updated 
    /// since last call to DoMeshingUpdate(...).
    /// This function is useful for determining when to call 
    /// DoMeshingUpdate. 
    /// </summary> 
    /// <returns>  flag indicating that reconstruction was updated.</returns>
    public Boolean IsReconstructionUpdated()
    {
        return PXCMScenePerception_IsReconstructionUpdated(instance);
    }

    /// <summary>
    /// Allows user to access 2D projection image of reconstructed 
    /// volume from a given camera pose by ray-casting. This function is 
    /// optimized for real time performance. It is also useful for 
    /// visualizing progress of the scene reconstruction. User should 
    /// explicitly call Dispose() on PXCMImage after copying the data.
    /// or before making subsequent call to QueryVolumePreview(...).
    /// </summary>
    /// <param name="pose"> Array of 12 Singles that stores camera pose
    /// in row-major order. Camera pose is specified in a 
    /// 3 by 4 matrix [R | T] = [Rotation Matrix | Translation Vector]
    /// where R = [ r11 r12 r13 ]
    /// [ r21 r22 r23 ] 
    /// [ r31 r32 r33 ]
    /// T = [ tx  ty  tz  ]
    /// Pose Array Layout = [r11 r12 r13 tx r21 r22 r23 ty r31 r32 r33 tz]
    /// Translation vector is in meters.</param>
    /// <returns>  Instance of PXCMImage whose content can be used for volume
    /// rendering. Returns null if there is an internal state error	or when
    /// the rendering is failed or when an invalid pose matrix is passed.</returns>
    public PXCMImage QueryVolumePreview(Single[] pose)
    {
        if (pose == null || pose.Length != 12)
            return null;

        IntPtr imgInstance = PXCMScenePerception_QueryVolumePreview(instance, pose);
        return new PXCMImage(imgInstance, true);
    }

    /// <summary>
    /// Reset removes all reconstructed model (volume) information 
    /// and the module will reinitialize the model when next stream is 
    /// passed to the module. It also resets the camera pose to the one 
    /// provided. If the pose is not provided then the module will use 
    /// default pose if	there is no platform IMU on the device and in case 
    /// of device with platform IMU the pose will be computed using gravity
    /// vector to align 3D volume with gravity when the next frame is 
    /// passed to the module.
    /// 
    /// However it doesn't Reset instance of PXCMBlockMeshingData created using 
    /// PXCMScenePerception.CreatePXCMBlockMeshingData(...). User should 
    /// explicitly call PXCMBlockMeshingData.Reset() to stay in sync with the 
    /// reconstruction model inside scene perception.
    /// </summary>
    /// <param name="pose"> Array of 12 Singles that stores initial camera pose
    /// user wishes to set in row-major order. Camera pose is specified in a 
    /// 3 by 4 matrix [R | T] = [Rotation Matrix | Translation Vector]
    /// where R = [ r11 r12 r13 ]
    /// [ r21 r22 r23 ] 
    /// [ r31 r32 r33 ]
    /// T = [ tx  ty  tz  ]
    /// Pose Array Layout = [r11 r12 r13 tx r21 r22 r23 ty r31 r32 r33 tz]
    /// Translation vector is in meters.</param>
    /// <returns>  On success returns PXCM_STATUS_NO_ERROR. Otherwise returns
    /// error code like when an invalid pose argument is passed.</returns>
    public pxcmStatus Reset(Single[] pose)
    {
        return PXCMScenePerception_Reset(instance, pose);
    }

    /// <summary>
    /// Reset removes all reconstructed model (volume) information 
    /// and the module will reinitializes the model when next Stream is 
    /// passed to the module. It also resets the camera pose to the one 
    /// provided or otherwise uses default initial pose. However If the 
    /// platform IMU is detected then the rotation matrix set by Reset will 
    /// be modified using gravity vector to align 3D volume with gravity
    /// when the next frame frame is passed to the module And the 
    /// translation vector will be retained. If the reset is called without Pose
    /// Platform with IMU then the module will use default translation and 
    /// rotation will be obtained based on value of gravity vector when the 
    /// next frame is passed.
    /// 
    /// However it doesn't Reset instance of PXCMBlockMeshingData created using 
    /// PXCMScenePerception.CreatePXCMBlockMeshingData. User should 
    /// explicitly call PXCMBlockMeshingData.Reset to stay in sync with the 
    /// reconstruction model inside scene perception.
    /// </summary> 
    /// <param name="pose"> Array of 12 Singles that stores initial camera pose
    /// user wishes to set in row-major order. Camera pose is specified in a 
    /// 3 by 4 matrix [R | T] = [Rotation Matrix | Translation Vector]
    /// where R = [ r11 r12 r13 ]
    /// [ r21 r22 r23 ] 
    /// [ r31 r32 r33 ]
    /// T = [ tx  ty  tz  ]
    /// Pose Array Layout = [r11 r12 r13 tx r21 r22 r23 ty r31 r32 r33 tz]
    /// Translation vector is in meters.</param>
    /// 
    /// <returns>  On success returns PXCM_STATUS_NO_ERROR. Otherwise returns
    /// error code like when an invalid pose is argument passed.</returns>
    public pxcmStatus Reset()
    {
        Single[] pose = new Single[12];
        for (int idx = 0; idx < 12; idx++) pose[idx] = 0;
        return Reset(pose);
    }

    /// <summary>
    /// Is an optional function meant for expert users. It allows 
    /// users to set meshing thresholds for DoMeshingUpdate(...)
    /// The values set by this function will be used by succeeding calls to
    /// DoMeshingUpdate(...). Sets the thresholds indicating the magnitude of 
    /// changes occurring in any block that would be considered significant
    /// for re-meshing.
    /// </summary>
    /// <param name="maxDistanceChangeThreshold"> If the maximum change in a
    /// block exceeds this value, then the block will be re-meshed. Setting
    /// the value to zero will retrieve all blocks.</param>
    /// 
    /// <param name="avgDistanceChange"> If the average change in a block
    /// exceeds this value, then the block will be re-meshed. 
    /// Setting the value to zero will retrieve all blocks.</param>
    /// 
    /// <returns>  PXCM_STATUS_NO_ERROR, on success otherwise returns error code.</returns>
    public pxcmStatus SetMeshingThresholds(Single maxDistanceChangeThreshold, Single avgDistanceChangeThreshold)
    {
        return PXCMScenePerception_SetMeshingThresholds(instance, maxDistanceChangeThreshold, avgDistanceChangeThreshold);
    }

    /// <summary>
    /// Allows user to allocate PXCMBlockMeshingData which can be 
    /// passed to DoMeshingUpdate. It's user's responsibility to explicitly 
    /// release the memory by calling Dispose().
    /// </summary>
    /// <param name="maxBlockMesh"> Maximum number of mesh blocks client can
    /// handle in one update from DoMeshingUpdate(...), If non-positive value is
    /// passed then it uses the default value. Use 
    /// PXCMBlockMeshingData::QueryMaxNumberOfBlockMeshes() to check the value.</param>
    /// 
    /// <param name="maxFaces"> Maximum number of faces that client can handle
    /// in one update from DoMeshingUpdate(...), If non-positive value is passed 
    /// then it uses the default value. Use 
    /// PXCMBlockMeshingData::QueryMaxNumberOfFaces() to check the value.</param>
    /// 
    /// <param name="maxVertices"> Maximum number of vertices that client can
    /// handle in one update from DoMeshingUpdate(...). If non-positive value is
    /// passed then it uses the default value. Use 
    /// PXCMBlockMeshingData::QueryMaxNumberOfVertices() to check the value.</param>
    /// 
    /// <param name="bUseColor"> Flag indicating whether user wants
    /// scene perception to return color per vertex in the mesh update. If 
    /// set the color buffer will be created in PXCMBlockMeshingData 
    /// otherwise color buffer will not be created and any calls made to 
    /// PXCMBlockMeshingData::QueryVerticesColor() will return null.</param>
    /// 
    /// <returns>  on success returns valid handle to the instance otherwise returns null.</returns>
    public PXCMBlockMeshingData CreatePXCMBlockMeshingData(Int32 maxBlockMesh, Int32 maxVertices, Int32 maxFaces, Boolean useColor)
    {
        IntPtr mdInstance = PXCMScenePerception_CreatePXCBlockMeshingData(instance, maxBlockMesh, maxVertices, maxFaces, useColor);
        return new PXCMBlockMeshingData(mdInstance, true);
    }

    /// <summary>
    /// Allows user to allocate PXCMBlockMeshingData with color enabled
    /// which can be passed to DoMeshingUpdate. It's user's responsibility to 
    /// explicitly release the memory by calling Dispose().
    /// </summary>
    /// <param name="maxBlockMesh"> Maximum number of mesh blocks client can
    /// handle in one update from DoMeshingUpdate(...), If non-positive value is
    /// passed then it uses the default value. Use 
    /// PXCMBlockMeshingData::QueryMaxNumberOfBlockMeshes() to check the value.</param>
    /// 
    /// <param name="maxFaces"> Maximum number of faces that client can handle
    /// in one update from DoMeshingUpdate(...), If non-positive value is passed 
    /// then it uses the default value. Use 
    /// PXCMBlockMeshingData::QueryMaxNumberOfFaces() to check the value.</param>
    /// 
    /// <param name="maxVertices"> Maximum number of vertices that client can
    /// handle in one update from DoMeshingUpdate(...). If non-positive value is
    /// passed then it uses the default value. Use 
    /// PXCMBlockMeshingData::QueryMaxNumberOfVertices() to check the value.</param>
    /// 
    /// <returns>  on success returns valid handle to the instance otherwise returns null.</returns>
    public PXCMBlockMeshingData CreatePXCMBlockMeshingData(Int32 maxBlockMesh, Int32 maxVertices, Int32 maxFaces)
    {
        return CreatePXCMBlockMeshingData(maxBlockMesh, maxFaces, maxVertices, true);
    }

    /// <summary>
    /// Allows user to allocate PXCMBlockMeshingData with default number 
    /// of mesh blocks, vertices, face and color enabled which can be passed to 
    /// DoMeshingUpdate(...). It's user's responsibility to explicitly 
    /// release the memory by calling Dispose().
    /// </summary>
    /// <returns>  on success returns valid handle to the instance otherwise returns null.</returns>
    public PXCMBlockMeshingData CreatePXCMBlockMeshingData()
    {
        return CreatePXCMBlockMeshingData(-1, -1, -1, true);
    }

    /// <summary>
    /// Performs meshing and hole filling if requested. This 
    /// function can be slow if there is a lot of data to be meshed. For
    /// Efficiency reason we recommend running this function on a separate
    /// thread. This call is designed to be thread safe if called in 
    /// parallel with ProcessImageAsync. 
    /// </summary>
    /// <param name="blockMeshingData"> Instance of pre-allocated
    /// PXCMBlockMeshingData. Refer to
    /// PXCMScenePerception::CreatePXCMBlockMeshingData(...) for how 
    /// to allocate PXCMBlockMeshingData.</param>
    /// 
    /// <param name="fillHoles"> Argument to indicate whether to fill holes in
    /// mesh blocks. If set, it will fill missing details in each mesh block 
    /// that is visible from scene perception's camera current pose and 
    /// completely surrounded by closed surface(holes) by smooth linear 
    /// interpolation of adjacent mesh data.</param>
    /// 
    /// <param name="meshingUpdateInfo"> Argument to indicate which mesh
    /// data you wish to use
    /// -countOfBlockMeshesRequired: If set, on successful call 
    /// this function will set number of block meshes available for 
    /// meshing which can be retrieved using QueryNumberOfBlockMeshes()</param>
    /// 
    /// -blockMeshesRequired: Can only be set to true if 
    /// countOfBlockMeshesRequired is set to true otherwise the value is 
    /// ignored, If set, on successful call to this function it 
    /// will update block meshes array in pBlockMeshingUpdateInfo which can
    /// be retrieved using QueryBlockMeshes()</param>
    /// 
    /// -countOfVeticesRequired: If set, on successful call this 
    /// function it will set number of vertices available for meshing 
    /// which can be retrieved using QueryNumberOfVertices()
    /// 
    /// -verticesRequired: Can only be set if 
    /// countOfVeticesRequired is set to true otherwise the value is ignored, 
    /// If set, on successful call to this function it will update vertices
    /// array in pBlockMeshingUpdateInfo which can be retrieved using 
    /// QueryVertices()</param>
    /// 
    /// -countOfFacesRequired: If set, on successful call this 
    /// function it will set number of faces available for meshing which 
    /// can be retrieved using QueryNumberOfFaces()</param>
    /// 
    /// -facesRequired: Can only be set, If countOfFacesRequired 
    /// is set to true otherwise the value is ignored, If set, on 
    /// successful call to this function it will update faces array in 
    /// pBlockMeshingUpdateInfo which can be retrieved using QueryFaces()</param>
    /// 
    /// -colorsRequired: If set and PXCMBlockMeshingData was created with color, on 
    /// success function will fill in colors array which can be accessed using 
    /// QueryVerticesColor()</param>
    /// 
    /// +NOTE: set meshing threshold to (0, 0) prior to calling DoMeshingUpdate
    /// with hole filling enabled to fill mesh regions that are not changed.
    /// <returns>  On success PXCM_STATUS_NO_ERROR otherwise error code will 
    /// be returned</returns>
    public pxcmStatus DoMeshingUpdate(PXCMBlockMeshingData pBlockMeshingUpdateInfo, Boolean bFillHoles, MeshingUpdateInfo info)
    {
        return PXCMScenePerception_DoMeshingUpdate(instance, pBlockMeshingUpdateInfo.instance, bFillHoles, info);
    }

    /// <summary>
    /// Performs meshing and hole filling if requested. This 
    /// function can be slow if there is a lot of data to be meshed. For
    /// Efficiency reason we recommend running this function on a separate
    /// thread. This call is designed to be thread safe if called in 
    /// parallel with ProcessImageAsync. 
    /// </summary>
    /// <param name="blockMeshingData"> Instance of pre-allocated
    /// PXCMBlockMeshingData. Refer to
    /// PXCMScenePerception::CreatePXCMBlockMeshingData(...) for how 
    /// to allocate PXCMBlockMeshingData.</param>
    /// 
    /// <param name="fillHoles"> Argument to indicate whether to fill holes in
    /// mesh blocks. If set, it will fill missing details in each mesh block 
    /// that is visible from scene perception's camera current pose and 
    /// completely surrounded by closed surface(holes) by smooth linear 
    /// interpolation of adjacent mesh data.</param>
    /// 
    /// <param name="meshingUpdateInfo"> Argument to indicate which mesh
    /// data you wish to use
    /// -countOfBlockMeshesRequired: If set, on successful call 
    /// this function will set number of block meshes available for 
    /// meshing which can be retrieved using QueryNumberOfBlockMeshes()
    /// 
    /// -blockMeshesRequired: Can only be set to true if 
    /// countOfBlockMeshesRequired is set to true otherwise the value is 
    /// ignored, If set, on successful call to this function it 
    /// will update block meshes array in pBlockMeshingUpdateInfo which can
    /// be retrieved using QueryBlockMeshes()
    /// 
    /// -countOfVeticesRequired: If set, on successful call this 
    /// function it will set number of vertices available for meshing 
    /// which can be retrieved using QueryNumberOfVertices()
    /// 
    /// -verticesRequired: Can only be set if 
    /// countOfVeticesRequired is set to true otherwise the value is ignored, 
    /// If set, on successful call to this function it will update vertices
    /// array in pBlockMeshingUpdateInfo which can be retrieved using 
    /// QueryVertices()
    /// 
    /// -countOfFacesRequired: If set, on successful call this 
    /// function it will set number of faces available for meshing which 
    /// can be retrieved using QueryNumberOfFaces()
    /// 
    /// -facesRequired: Can only be set, If countOfFacesRequired 
    /// is set to true otherwise the value is ignored, If set, on 
    /// successful call to this function it will update faces array in 
    /// pBlockMeshingUpdateInfo which can be retrieved using QueryFaces()
    /// 
    /// -colorsRequired: If set and PXCMBlockMeshingData was created with color, on 
    /// success function will fill in colors array which can be accessed using 
    /// QueryVerticesColor()
    /// 
    /// +NOTE: set meshing threshold to (0, 0) prior to calling DoMeshingUpdate
    /// with hole filling enabled to fill mesh regions that are not changed.</param>
    /// 
    /// <returns>  On success PXCM_STATUS_NO_ERROR otherwise error code will 
    /// be returned</returns>
    public pxcmStatus DoMeshingUpdate(PXCMBlockMeshingData pBlockMeshingUpdateInfo, Boolean bFillHoles)
    {
        return DoMeshingUpdate(pBlockMeshingUpdateInfo, false, null);
    }

    /// <summary>
    /// Performs DoMeshingUpdate(...) with hole filling disabled
    /// and requests all mesh data (vertices, faces, block meshes and color). 
    /// </summary>
    /// <param name="blockMeshingData"> Instance of pre-allocated
    /// PXCMBlockMeshingData. Refer to 
    /// PXCMScenePerception::CreatePXCMBlockMeshingData For how to allocate 
    /// PXCMBlockMeshingData.</param>
    /// 
    /// <returns>  On success PXCM_STATUS_NO_ERROR otherwise error code will 
    /// be returned.</returns>
    public pxcmStatus DoMeshingUpdate(PXCMBlockMeshingData pBlockMeshingUpdateInfo)
    {
        return DoMeshingUpdate(pBlockMeshingUpdateInfo, false);
    }

    /// <summary>
    /// Allows users to save mesh in an ASCII obj file in 
    /// MeshResolution::HIGH_RESOLUTION_MESH.
    /// </summary> 
    /// <param name="pFile"> the path of the file to use for saving the mesh.</param>
    /// 
    /// <param name="bFillHoles"> Indicates whether to fill holes in mesh
    /// before saving the mesh.</param>
    /// 
    /// <returns>  On success PXCM_STATUS_NO_ ERROR, Otherwise error code is 
    /// returned on failure.</returns>
    public pxcmStatus SaveMesh(String fileName, Boolean fillHoles)
    {
        return PXCMScenePerception_SaveMesh(instance, fileName, fillHoles);
    }

    /// <summary>
    /// Allows users to save mesh in an ASCII obj file in 
    /// MeshResolution::HIGH_RESOLUTION_MESH.
    /// </summary> 
    /// <param name="pFile"> the path of the file to use for saving the mesh.</param>
    /// 
    /// <param name="bFillHoles"> Indicates whether to fill holes in mesh
    /// before saving the mesh.</param>
    /// 
    /// <returns>  On success PXCM_STATUS_NO_ ERROR, Otherwise error code is
    /// returned on failure.</param>
    public pxcmStatus SaveMesh(String fileName)
    {
        return SaveMesh(fileName, false);
    }

    /// <summary> 
    /// Allows user to check whether the input stream is suitable 
    /// for starting, resetting/restarting or tracking scene perception.
    /// </summary> 
    /// <param name="PXCMCapture.Sample"> Input stream sample required by
    /// scene perception module.</param>
    /// 
    /// <returns>  Returns positive values between 0.0 and 1.0 to indicate how good is scene for 
    /// starting, tracking or resetting scene perception.
    /// 1.0 -> represents ideal scene for starting scene perception.
    /// 0.0 -> represents unsuitable scene for starting scene perception.
    /// 
    /// Returns negative values to indicate potential reason for tracking failure
    /// -1.0 -> represents a scene without enough structure/geomtery
    /// -2.0 -> represents a scene without enough depth pixels 
    /// (Too far or too close to the target scene or 
    /// outside the range of depth camera)
    /// Also, value 0.0 is returned when an invalid argument is passed or 
    /// if the function is called before calling PXCSenseManager::Init().</returns>
    public Single CheckSceneQuality(PXCMCapture.Sample sample)
    {
        return PXCMScenePerception_CheckSceneQuality(instance, sample);
    }

    /// <summary> 
    /// Fills holes in the supplied depth image.
    /// </summary>
    /// <param name="depthImage"> Instance of depth image to be filled.
    /// Pixels with depth value equal to zero will be linearly interpolated 
    /// with adjacent depth pixels. The image resolution should be 320X240.</param>
    /// 
    /// 
    /// <returns>  On success PXCM_STATUS_NO_ERROR,
    /// Otherwise error code will be returned on failure.</returns>
    public pxcmStatus FillDepthImage(PXCMImage depthImage)
    {
        return PXCMScenePerception_FillDepthImage(instance, depthImage.instance);
    }

    /// <summary>
    /// Allows user to access normals of surface that are within
    /// view from the camera's current pose.
    /// </summary> 
    /// <param name="normals"> Array of pre-allocated PXCMPoint3DF32 to store
    /// normal vectors. Each normal vector has three components namely x, y 
    /// and z. The size in pixels must be QVGA and hence the array size in 
    /// bytes should be: (PXCMPoint3DF32's byte size) x (320 x 240). </param>
    /// 
    /// <returns>  On success PXCM_STATUS_NO_ERROR,			
    /// Otherwise error code will be returned on failure.</returns>
    public pxcmStatus GetNormals(PXCMPoint3DF32[] normals)
    {
        return PXCMScenePerception_GetNormals(instance, normals);
    }

    /// <summary>
    /// Allows user to access the surface's vertices
    /// that are within view from camera's current pose.
    /// </summary> 
    /// <param name="vertices"> Array of pre-allocated PXCMPoint3DF32 to store
    /// vertices. Each element is a vector of x, y and z components. The 
    /// image size in pixels must be QVGA and hence the array
    /// size in bytes should be: (PXCMPoint3DF32's byte size) x (320 x 240).</param>
    /// 
    /// <returns>  On success PXCM_STATUS_NO_ERROR,			
    /// Otherwise error code will be returned on failure.</returns>
    public pxcmStatus GetVertices(PXCMPoint3DF32[] vertices)
    {
        return PXCMScenePerception_GetVertices(instance, vertices);
    }

    /// <summary>
    /// Allows user to save the current scene perception's state to
    /// a file and later supply the file to LoadState() to restore scene 
    /// perception to the saved state.
    /// </summary> 
    /// <param name="fileName"> The path of the file to use for saving the
    /// scene perception state.</param>
    /// 
    /// <returns>  On success PXCM_STATUS_NO_ERROR,			
    /// Otherwise error code will be returned on failure.</returns>
    public pxcmStatus SaveCurrentState(String fileName)
    {
        return PXCMScenePerception_SaveCurrentState(instance, fileName);
    }

    /// <summary>
    /// Allows user to load the current scene perception's state 
    /// from the file that has been created using SaveCurrentState. 
    /// This function is only available before calling 
    /// PXCMSenseManager::Init().
    /// </summary>
    /// <param name="fileName"> The path of the file to load scene perception
    /// state from.</param>
    /// 
    /// <returns>  On success PXCM_STATUS_NO_ERROR,			
    /// Otherwise error code will be returned on failure.</returns>
    public pxcmStatus LoadState(String fileName)
    {
        return PXCMScenePerception_LoadState(instance, fileName);
    }

    /// <summary>
    /// Allows user to allocate CreatePXCMSurfaceVoxelsData which can be 
    /// passed to ExportSurfaceVoxels. It's user's responsibility to explicitly 
    /// release the instance by calling Dispose().
    /// </summary>
    /// <param name="voxelCount"> Maximum number of voxels
    /// client is expecting in each call to ExportSurfaceVoxels(...).  </param>
    /// 
    /// <param name="bUseColor"> Flag indicating whether user wants
    /// scene perception to return voxel when ExportSurfaceVoxels(...) is 
    /// called. If set, the color buffer will be allocated in PXCMSurfaceVoxelsData 
    /// otherwise color buffer will not be created and any calls made to 
    /// PXCMSurfaceVoxelsData.QuerySurfaceVoxelsColor() will return null.</param>
    /// 
    /// <returns>  on success returns valid handle to the instance otherwise returns null.</returns>
    public PXCMSurfaceVoxelsData CreatePXCMSurfaceVoxelsData(Int32 voxelCount, Boolean bUseColor)
    {
        IntPtr svdInstance = PXCMScenePerception_CreatePXCSurfaceVoxelsData(instance, voxelCount, bUseColor);
        return (svdInstance == IntPtr.Zero) ? null : (new PXCMSurfaceVoxelsData(svdInstance, true));
    }
    /// <summary>
    /// Allows user to allocate CreatePXCMSurfaceVoxelsData without color,
    /// which can be passed to ExportSurfaceVoxels with default estimate of number of voxels. 
    /// It's user's responsibility to explicitly release the instance by calling Dispose().
    /// </summary>
    public PXCMSurfaceVoxelsData CreatePXCMSurfaceVoxelsData()
    {
        return CreatePXCMSurfaceVoxelsData(-1, false);
    }

    /// <summary>
    /// Allows user to export the voxels intersected by the surface
    /// scanned. Optionally allows to specify region of interest for 
    /// surface voxels to be exported. voxels will be exported in parts over 
    /// multiple calls to this function. Client is expected to check return code to 
    /// determine if all voxels are exported successfully or not.
    /// </summary>
    /// <param name="surfaceVoxelsData"> Pre-allocated instance of PXCMSurfaceVoxelsData
    /// using CreatePXCMSurfaceVoxelsData(...). On success the function will fill 
    /// in center of each surface voxel in an array which can be obtained using QueryCenterOfSurfaceVoxels
    /// and number of voxels which can be retrieved using QueryNumberOfSurfaceVoxels().</param>
    /// 
    /// <param name="lowerLeftFrontPoint"> Optional, PXCMPoint3DF32 represents lower
    /// left corner of the front face of the bounding box which specifies region of interest for exporting 
    /// surface voxels.</param>
    /// 
    /// <param name="upperRightRearPoint"> Optional, PXCMPoint3DF32 represents upper
    /// right corner of the rear face of the bounding box which specifies region of interest for exporting 
    /// surface voxels.</param>
    /// 
    /// <returns>  If scene perception module is able to export all the surface 
    /// voxels it has acquired it will return PXCM_STATUS_NO_ERROR and after that 
    /// any calls made to ExportSurfaceVoxels(...) will restart exporting all the
    /// voxels again.
    /// If all voxels cannot be fit into specified surfaceVoxelsData, it will 
    /// return warning code PXCM_STATUS_DATA_PENDING indicating that client 
    /// should make additional calls to ExportSurfaceVoxels to get remaining 
    /// voxels until PXCM_STATUS_NO_ERROR is returned.</returns>
    public pxcmStatus ExportSurfaceVoxels(PXCMSurfaceVoxelsData surfaceVoxelsData, PXCMPoint3DF32 lowerLeftFrontPoint, PXCMPoint3DF32 upperRightRearPoint)
    {
        IntPtr unmanagedAddrlowerLeftFrontPoint = Marshal.AllocHGlobal(Marshal.SizeOf(lowerLeftFrontPoint));
        IntPtr unmanagedAddrupperRightRearPoint = Marshal.AllocHGlobal(Marshal.SizeOf(upperRightRearPoint));

        Marshal.StructureToPtr(lowerLeftFrontPoint, unmanagedAddrlowerLeftFrontPoint, false);
        Marshal.StructureToPtr(upperRightRearPoint, unmanagedAddrupperRightRearPoint, false);

        pxcmStatus retStatus = PXCMScenePerception_ExportSurfaceVoxels(instance, surfaceVoxelsData.instance, unmanagedAddrlowerLeftFrontPoint, unmanagedAddrupperRightRearPoint); ;

        Marshal.FreeHGlobal(unmanagedAddrlowerLeftFrontPoint);
        Marshal.FreeHGlobal(unmanagedAddrupperRightRearPoint);

        unmanagedAddrlowerLeftFrontPoint = IntPtr.Zero;
        unmanagedAddrupperRightRearPoint = IntPtr.Zero;
        return retStatus;
    }

    /// <summary>
    /// Allows to Export Surface Voxels present in the entire volume.
    /// </summary>
    public pxcmStatus ExportSurfaceVoxels(PXCMSurfaceVoxelsData surfaceVoxelsData)
    {
        return ExportSurfaceVoxels(surfaceVoxelsData, new PXCMPoint3DF32(0, 0, 0), new PXCMPoint3DF32(0, 0, 0));
    }

    /// <summary>
    /// Allows user to get information regarding the planar 
    /// surfaces in the scene.
    /// </summary>
    /// <param name="sample"> Input stream sample required by scene perception module.</param>
    /// 
    /// <param name="minPlaneArea:"> Minimum plane area to be detected in physical
    /// dimension (m^2). This parameter refers to the physical size of the
    /// frontal planar surface at 1 meter to 3 meter from the camera. It controls 
    /// the threshold for the number of planes to be returned. Setting it to a
    /// smaller value makes the function to return smaller planes as well.
    /// E.g 0.213X0.213 m^2. The maximum acceptable value is 16.</param>
    /// 
    /// <param name="maxPlaneNumber"> Maximum number of planes that user wishes
    /// to detect, It should also match number of rows of the equation 
    /// array pPlaneEq.</param>
    /// 
    ///  Pre-allocated float array for storing the
    /// plane equations detected by the function. Each row contains the 
    /// coefficients {a,b,c,w} of the detected plane, Hence the number of 
    /// rows are equal to maxPlaneNumber. a, b, c are co-efficients of 
    /// normalized plane equation and w is in meters.
    /// E.g. Row 0 of pPlaneEq will contain the plane equation: ax+by+cz+w
    /// in the form	pPlaneEq[0][0] to pPlaneEq[0][3] = {a,b,c,w}. Similarly
    /// rest of the rows will provide the equations for the remaining 
    /// planes. Rows for which planes are not detected will have all values 0.</param>
    /// 
    /// <param name="pPlaneIndexImg:"> Pre-allocated array of (320X240) to store plane
    /// ids. On success each index will have one of the following values 
    /// - 0: If the pixel is not part of any detected planes 
    /// - 1: If the pixel is part of the first plane detected
    /// - 2: If the pixel is part of the second plane is detected
    /// and so on.</param>
    /// 
    /// <returns>  On success PXCM_STATUS_NO_ERROR,			
    /// Otherwise error code will be returned on failure</returns>
    public pxcmStatus ExtractPlanes(PXCMCapture.Sample sample, Single minPlaneArea, Int32 maxPlaneNumber, out Single[,] pPlaneEq,
            out byte[] pPlaneIndexImg)
    {
        pPlaneIndexImg = new byte[320 * 240];
        pPlaneEq = new Single[maxPlaneNumber, 4];
        Single[] pPlaneEq1D = new Single[maxPlaneNumber * 4];
        pxcmStatus status = ExtractPlanesINT(instance, sample, minPlaneArea, maxPlaneNumber, pPlaneEq1D, pPlaneIndexImg);
        for (int i = 0; i < maxPlaneNumber; i++)
            for (int j = 0; j < 4; j++)
                pPlaneEq[i, j] = pPlaneEq1D[i * 4 + j];
        return status;
    }

    /// <summary>
    /// Allows user to set meshing resolution for DoMeshingUpdate(...).
    /// </summary>
    /// <param name="meshResolution:"> Mesh Resolution user wishes to set.</param>
    /// 
    /// <returns>  On success PXCM_STATUS_NO_ERROR,			
    /// Otherwise error code will be returned on failure.</returns>
    public pxcmStatus SetMeshingResolution(MeshResolution meshResolution)
    {
        return PXCMScenePerception_SetMeshingResolution(instance, meshResolution);
    }

    /// <summary>
    /// Allows user to get meshing resolution used by DoMeshingUpdate(...).
    /// </summary>
    /// <returns>  MeshResolution used by DoMeshingUpdate(...).</returns>
    public MeshResolution QueryMeshingResolution()
    {
        return (MeshResolution)PXCMScenePerception_QueryMeshingResolution(instance);
    }

    /// <summary>
    /// Allows user to get meshing thresholds used by scene 
    /// perception.
    /// </summary>
    /// <param name="maxDistanceChangeThreshold:"> retrieve max distance change threshold.</param>
    /// 
    /// <param name="avgDistanceChange:"> retrieve average distance change threshold.</param>
    /// 
    /// <returns>  On success PXCM_STATUS_NO_ERROR,			
    /// Otherwise error code will be returned on failure.</returns>
    public pxcmStatus QueryMeshingThresholds(out Single maxDistanceChangeThreshold, out Single avgDistanceChange)
    {
        maxDistanceChangeThreshold = 0.0F;
        avgDistanceChange = 0.0F;
        return PXCMScenePerception_QueryMeshingThresholds(instance, out maxDistanceChangeThreshold, out avgDistanceChange);
    }

    /// <summary> 
    /// Allows user to set region of interest for Meshing. If used,
    /// The DoMeshingUpdate(...) function will only 
    /// mesh these specified regions. Use ClearMeshingRegion() to clear the 
    /// meshing region set by this function.
    /// </summary>
    /// <param name="lowerLeftFrontPoint:"> Pre-allocated PXCMPoint3DF32 which
    /// specifies lower left corner of the front face of the bounding box.</param>
    /// 
    /// <param name="upperRightRearPoint:"> Pre-allocated PXCMPoint3DF32 which specifies
    /// upper right corner of the rear face of the bounding box.</param>
    /// 
    /// <returns>  On success PXCM_STATUS_NO_ERROR,			
    /// Otherwise error code will be returned on failure.</returns>
    public pxcmStatus SetMeshingRegion(PXCMPoint3DF32 lowerLeftFrontPoint, PXCMPoint3DF32 upperRightRearPoint)
    {
        IntPtr unmanagedAddrlowerLeftFrontPoint = Marshal.AllocHGlobal(Marshal.SizeOf(lowerLeftFrontPoint));
        IntPtr unmanagedAddrupperRightRearPoint = Marshal.AllocHGlobal(Marshal.SizeOf(upperRightRearPoint));

        Marshal.StructureToPtr(lowerLeftFrontPoint, unmanagedAddrlowerLeftFrontPoint, true);
        Marshal.StructureToPtr(upperRightRearPoint, unmanagedAddrupperRightRearPoint, true);

        pxcmStatus retStatus = PXCMScenePerception_SetMeshingRegion(instance, unmanagedAddrlowerLeftFrontPoint, unmanagedAddrupperRightRearPoint);

        Marshal.FreeHGlobal(unmanagedAddrlowerLeftFrontPoint);
        Marshal.FreeHGlobal(unmanagedAddrupperRightRearPoint);

        unmanagedAddrlowerLeftFrontPoint = IntPtr.Zero;
        unmanagedAddrupperRightRearPoint = IntPtr.Zero;
        return retStatus;
    }

    /// <summary>
    /// Allows user to clear meshing region set by SetMeshingRegion(...).
    /// </summary>
    /// <returns>  On success PXCM_STATUS_NO_ERROR,			
    /// Otherwise error code will be returned on failure.</returns>
    public pxcmStatus ClearMeshingRegion()
    {
        return PXCMScenePerception_ClearMeshingRegion(instance);
    }

    /// <summary>
    /// Allows user to enforce the supplied pose as the camera pose 
    /// .The module will track the camera from this pose when the next 
    /// frame is passed. This function can be called any time after module finishes 
    /// processing first frame or any time after module successfully processes the 
    /// first frame post a call to Reset scene perception.
    /// </summary>
    /// <param name="pose:"> Array of 12 pxcF32 that stores the camera pose
    /// user wishes to set in row-major order. Camera pose is specified in a 
    /// 3 by 4 matrix [R | T] = [Rotation Matrix | Translation Vector]
    /// where R = [ r11 r12 r13 ]
    /// [ r21 r22 r23 ] 
    /// [ r31 r32 r33 ]
    /// T = [ tx  ty  tz  ]
    /// Pose Array Layout = [r11 r12 r13 tx r21 r22 r23 ty r31 r32 r33 tz]
    /// Translation vector is in meters.
    /// </param>
    /// <returns>  PXCM_STATUS_NO_ERROR, if the function succeeds.
    /// Otherwise error code will be returned.</returns>
    public pxcmStatus SetCameraPose(Single[] pose)
    {
        return PXCMScenePerception_SetCameraPose(instance, pose);
    }

    /// <summary>
    /// Allows user to get length of side of voxel cube in meters.
    /// </summary>
    /// <returns>  Returns length of side of voxel cube in meters.</returns>
    public Single QueryVoxelSize()
    {
        return PXCMScenePerception_QueryVoxelSize(instance);
    }

    /// <summary>
    /// Allows user to get the intrinsics of internal scene perception 
    /// camera.	These intrinsics should be used with output images obtained from 
    /// the module. Such as QueryVolumePreview(...), GetVertices(...) and 
    /// GetNormals(..). This function should only be used after calling 
    /// PXCMSenseManager.Init() otherwise it would return an error code.
    /// </summary>
    /// <param name="spIntrinsic "> Handle to pre-allocated instance of
    /// ScenePerceptionIntrinsics. On success this instance will be filled with 
    /// appropriate values.</param>
    /// 
    /// <returns>  PXCM_STATUS_NO_ERROR, if the function succeeds.
    /// Otherwise error code will be returned.</returns>
    public pxcmStatus GetInternalCameraIntrinsics(out ScenePerceptionIntrinsics spIntrinsics)
    {
        spIntrinsics = new ScenePerceptionIntrinsics();
        return PXCMScenePerception_GetInternalCameraIntrinsics(instance, spIntrinsics);
    }


    /// <summary>
    /// Allows user to integrate specified stream from supplied pose in to 
    /// the reconstructed volume.
    /// </summary>
    /// <param name="sample:"> Input stream sample required by scene perception module.
    /// Obtained using PXCMSenseManager.QueryScenePerceptionSample().</param>
    /// 
    /// <param name="pose:"> Estimated pose for the supplied input stream.
    /// Array of 12 pxcF32 that stores the camera pose
    /// user wishes to set in row-major order. Camera pose is specified in a 
    /// 3 by 4 matrix [R | T] = [Rotation Matrix | Translation Vector]
    /// where R = [ r11 r12 r13 ]
    /// [ r21 r22 r23 ] 
    /// [ r31 r32 r33 ]
    /// T = [ tx  ty  tz  ]
    /// Pose Array Layout = [r11 r12 r13 tx r21 r22 r23 ty r31 r32 r33 tz]
    /// Translation vector is in meters.</param>
    /// 
    /// <returns>  PXCM_STATUS_NO_ERROR, if the function succeeds.
    /// Otherwise error code will be returned.</returns>
    public pxcmStatus DoReconstruction(PXCMCapture.Sample sample, Single[] pose)
    {
        return PXCMScenePerception_DoReconstruction(instance, sample, pose);
    }

    /// <summary>
    /// Allows user to enable/disable re-localization feature of scene 
    /// perception's camera tracking. By default re-localization is enabled. This 
    /// functionality is only available after PXCMSenseManager.Init() is called.
    /// </summary>
    /// <param name="enableRelocalization:"> Flag specifying whether to enable or
    /// disable re-localization.</param>
    /// 
    /// <returns>  PXCM_STATUS_NO_ERROR, if the function succeeds.
    /// Otherwise error code will be returned.</returns>
    public pxcmStatus EnableRelocalization(Boolean enableRelocalization)
    {
        return PXCMScenePerception_EnableRelocalization(instance, enableRelocalization);
    }

    /// <summary>
    /// Allows user to transform plane equations obtained from
    /// ExtractPlanes(...) to world co-ordinate system using the provided 
    /// pose and returns number of planes found in supplied plane equation.
    /// </summary>
    /// <param name="maxPlaneNumber"> Number of rows of the equation 
    /// array pPlaneEq.</param>
    /// 
    /// <param name="pPlaneEq"> Pre-allocated float array plane equations
    /// obtained from  ExtractPlanes(...). On success the plane equations will 
    /// be transformed in to world coordinate system using supplied camera pose.</param>
    /// 
    /// <param name="pose:"> Array of 12 pxcF32 that stores camera pose
    /// of the capture sample that was supplied to the ExtractPlanes(...).
    /// stored in row-major order. Camera pose is specified in a 
    /// 3 by 4 matrix [R | T] = [Rotation Matrix | Translation Vector]
    /// where R = [ r11 r12 r13 ]
    /// [ r21 r22 r23 ] 
    /// [ r31 r32 r33 ]
    /// T = [ tx  ty  tz  ]
    /// Pose Array Layout = [r11 r12 r13 tx r21 r22 r23 ty r31 r32 r33 tz]
    /// Translation vector is in meters.
    /// 
    /// +NOTE: Use the pose obtained from GetCameraPose(...) 
    /// to transform plane equations.
    ///  </param>
    /// <returns>  On success, returns positive number indicating 
    /// number of planes found in pPlaneEq. Negative number indicates errors like 
    /// invalid argument.</returns>
    public Int32 TransformPlaneEquationToWorld(Int32 maxPlaneNumber, Single[,] pPlaneEq, Single[] pose)
    {
        Single[] pPlaneEq1D = new Single[maxPlaneNumber * 4];

        for (int i = 0, j = 0; i < maxPlaneNumber; i++)
        {
            pPlaneEq1D[j++] = pPlaneEq[i, 0];
            pPlaneEq1D[j++] = pPlaneEq[i, 1];
            pPlaneEq1D[j++] = pPlaneEq[i, 2];
            pPlaneEq1D[j++] = pPlaneEq[i, 3];
        }
        Int32 numberOfPlanes = PXCMScenePerception_TransformPlaneEquationToWorld(instance, maxPlaneNumber, pPlaneEq1D, pose);

        for (int i = 0, j = 0; i < maxPlaneNumber; i++)
        {
            pPlaneEq[i, 0] = pPlaneEq1D[j++];
            pPlaneEq[i, 1] = pPlaneEq1D[j++];
            pPlaneEq[i, 2] = pPlaneEq1D[j++];
            pPlaneEq[i, 3] = pPlaneEq1D[j++];
        }

        return numberOfPlanes;
    }

    public Int32 TransformPlaneEquationToWorld(Single[,] pPlaneEq, Single[] pose)
    {
        Int32 maxPlaneNumber = pPlaneEq.GetLength(0);
        return TransformPlaneEquationToWorld(maxPlaneNumber, pPlaneEq, pose);
    }

    /// <summary>
    /// Allows users to save different configuration of mesh in an 
    /// ASCII obj file.
    /// </summary>
    /// <param name="fileName:"> the path of the file to use for saving the mesh.</param>
    /// 
    /// <param name="saveMeshInfo"> Argument to indicate mesh configuration
    /// you wish to save.
    /// 
    /// -fillMeshHoles: Flag indicates whether to fill holes in saved mesh. 
    /// 
    /// saveMeshColor: Flag indicates whether to save mesh with color.
    /// 
    /// meshResolution: Indicates resolution for mesh to be saved.
    /// </param>
    /// <returns> On success PXCM_STATUS_NO_ERROR, Otherwise error code is
    /// returned on failure.</returns>
    public pxcmStatus SaveMeshExtended(String fileName, SaveMeshInfo saveMeshInfo)
    {
        return PXCMScenePerception_SaveMeshExtended(instance, fileName, saveMeshInfo);
    }

    /// <summary>
    /// Allows user to enable or disable inertial sensor support for scene perception,
    /// by default it is disabled. This function is only available before calling 
    /// PXCSenseManager::Init().
    /// </summary>
    /// <returns> On success PXCM_STATUS_NO_ ERROR, Otherwise error code is 
    /// returned on failure.</returns>
    public pxcmStatus EnableInertialSensorSupport(Boolean enable)
    {
        return PXCMScenePerception_EnableInertialSensorSupport(instance, enable);
    }

    /// <summary>
    /// Allows user to enable or disable gravity sensor support for scene perception,
    /// by default it is enabled. This function is only available before calling 
    /// PXCSenseManager::Init().
    /// </summary>
    /// <returns>  On success PXCM_STATUS_NO_ ERROR, Otherwise error code is 
    /// returned on failure.</returns>
    public pxcmStatus EnableGravitySensorSupport(Boolean enable)
    {
        return PXCMScenePerception_EnableGravitySensorSupport(instance, enable);
    }

    /// <summary>
    /// Allows user to get status(enabled/disabled) of gravity sensor support 
    /// for scene perception.
    /// </summary>
    /// <returns>  On success PXCM_STATUS_NO_ ERROR, Otherwise error code is </returns>
    /// returned on failure.
    public Boolean IsGravitySensorSupportEnabled()
    {
        return PXCMScenePerception_IsGravitySensorSupportEnabled(instance);
    }

    /// <summary>
    /// Allows user to get status(enabled/disabled) of inertial sensor support
    /// for scene perception.
    /// </summary>
    /// <returns>  On success PXCM_STATUS_NO_ ERROR, Otherwise error code is 
    /// returned on failure.</returns>
    public Boolean IsInertialSensorSupportEnabled()
    {
        return PXCMScenePerception_IsInertialSensorSupportEnabled(instance);
    }

    /// <summary>
    /// Allows user to access volume details like 2D projection image of reconstructed 
    /// volume by ray-casting, surface volume normals and surface volume faces from a given camera pose
    /// </summary>
    /// <param name="pose"> Array of 12 pxcF32 that stores camera pose</param>
    /// in row-major order. Camera pose is specified in a 
    /// 3 by 4 matrix [R | T] = [Rotation Matrix | Translation Vector]
    /// where R = [ r11 r12 r13 ]
    /// [ r21 r22 r23 ] 
    /// [ r31 r32 r33 ]
    /// T = [ tx  ty  tz  ]
    /// Pose Array Layout = [r11 r12 r13 tx r21 r22 r23 ty r31 r32 r33 tz]
    /// Translation vector is in meters.
    /// 
    /// <param name="volumeImageData"> Optional pre-allocated Byte array of size:
    /// 4 X ScenePerceptionIntrinsics.imageSize.width X ScenePerceptionIntrinsics.imageSize.height
    /// where user wishes to store the projection data of the volume. ScenePerceptionIntrinsics is obtained using 
    /// GetInternalCameraIntrinsics(...). It contains 4 channels per pixel in RGBA order.</param>
    /// 
    /// <param name="vertices"> Optional pre-allocated array of Single to store volume
    /// vertices. Each vertex is represented by 3 components x, y and z in order. Therefor 
    /// the array size should be: 
    /// ScenePerceptionIntrinsics.imageSize.width X ScenePerceptionIntrinsics.imageSize.height.
    /// Where ScenePerceptionIntrinsics is obtained using GetInternalCameraIntrinsics(...). </param>
    /// 
    /// <param name="normals"> Optional pre-allocated array of Single to store volume</param>
    /// normals. Each normal is represented 3 components x, y and z in order. 
    /// Therefor the array size should be:  
    /// ScenePerceptionIntrinsics.imageSize.width X ScenePerceptionIntrinsics.imageSize.height.
    /// Where ScenePerceptionIntrinsics is obtained using GetInternalCameraIntrinsics(...). </param>
    /// 
    /// <returns>  On success PXCM_STATUS_NO_ ERROR, Otherwise error code is
    /// returned on failure. </returns>
    public pxcmStatus GetVolumePreview(Single[] pose, Byte[] volumeImageData, Single[] vertices, Single[] normals)
    {
        return PXCMScenePerception_GetVolumePreview(instance, pose, volumeImageData, vertices, normals);
    }

    /* constructors and misc */
    internal PXCMScenePerception(IntPtr instance, Boolean delete)
        : base(instance, delete)
    {
    }
};

#if RSSDK_IN_NAMESPACE
}
#endif
