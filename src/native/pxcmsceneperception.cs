/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2013-2014 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Runtime.InteropServices;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

    public partial class PXCMSurfaceVoxelsData : PXCMBase
    {
        [DllImport(PXCMBase.DLLNAME)]
        internal static extern Int32 PXCMSurfaceVoxelsData_QueryNumberOfSurfaceVoxels(IntPtr voxel);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCMSurfaceVoxelsData_QueryCenterOfSurfaceVoxels(IntPtr voxel);

        internal static void QueryCenterOfSurfaceVoxelsINT(IntPtr voxel, Single[] voxels, Int32 nvoxels)
        {
            IntPtr pvoxels = PXCMSurfaceVoxelsData_QueryCenterOfSurfaceVoxels(voxel);
            Marshal.Copy(pvoxels, voxels, 0, nvoxels * 3);
        }

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCMSurfaceVoxelsData_QuerySurfaceVoxelsColor(IntPtr voxel);

        internal static void QuerySurfaceVoxelsColorINT(IntPtr voxel, Byte[] colors, Int32 nvoxels) 
        {
            IntPtr pcolors = PXCMSurfaceVoxelsData_QuerySurfaceVoxelsColor(voxel);
            Marshal.Copy(pcolors, colors, 0, nvoxels * 3);
        }

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMSurfaceVoxelsData_Reset(IntPtr voxel);
    };

    public partial class PXCMBlockMeshingData : PXCMBase
    {
        [DllImport(PXCMBase.DLLNAME)]
        internal static extern Int32 PXCMBlockMeshingData_QueryNumberOfBlockMeshes(IntPtr mesh);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern Int32 PXCMBlockMeshingData_QueryNumberOfVertices(IntPtr mesh);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern Int32 PXCMBlockMeshingData_QueryNumberOfFaces(IntPtr mesh);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern Int32 PXCMBlockMeshingData_QueryMaxNumberOfBlockMeshes(IntPtr mesh);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern Int32 PXCMBlockMeshingData_QueryMaxNumberOfVertices(IntPtr mesh);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern Int32 PXCMBlockMeshingData_QueryMaxNumberOfFaces(IntPtr mesh);

        [DllImport(PXCMBase.DLLNAME)]
        private static extern IntPtr PXCMBlockMeshingData_QueryBlockMeshes(IntPtr mesh);
        internal static void QueryBlockMeshesINT(IntPtr mesh, PXCMBlockMesh[] meshes, Int32 nmeshes)
        {
            IntPtr umeshes=PXCMBlockMeshingData_QueryBlockMeshes(mesh);
            for (int i = 0; i < nmeshes; i++)
            {
                if (meshes[i]==null) meshes[i] = new PXCMBlockMesh();
                Marshal.PtrToStructure(umeshes, meshes[i]);
                umeshes = new IntPtr(umeshes.ToInt64()+Marshal.SizeOf(typeof(PXCMBlockMesh)));
            }
        }

        [DllImport(PXCMBase.DLLNAME)]
        private static extern IntPtr PXCMBlockMeshingData_QueryVertices(IntPtr mesh);

        internal static void QueryVerticesINT(IntPtr mesh, Single[] vertices, Int32 nvertices)
        {
            IntPtr uvertices=PXCMBlockMeshingData_QueryVertices(mesh);
            Marshal.Copy(uvertices, vertices, 0, nvertices * 4);
        }

        [DllImport(PXCMBase.DLLNAME)]
        private static extern IntPtr PXCMBlockMeshingData_QueryVerticesColor(IntPtr mesh);
        internal static void QueryVerticesColorINT(IntPtr mesh, Byte[] colors, Int32 nvertices)
        {
            IntPtr ucolors = PXCMBlockMeshingData_QueryVerticesColor(mesh);
            Marshal.Copy(ucolors, colors, 0, nvertices * 3);
        }

        [DllImport(PXCMBase.DLLNAME)]
        private static extern IntPtr PXCMBlockMeshingData_QueryFaces(IntPtr mesh);
        internal static void QueryFacesINT(IntPtr mesh, Int32[] faces, Int32 nfaces)
        {
            IntPtr ufaces = PXCMBlockMeshingData_QueryFaces(mesh);
            Marshal.Copy(ufaces, faces, 0, nfaces * 3);
        }

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMBlockMeshingData_Reset(IntPtr mesh);
    };


    public partial class PXCMScenePerception : PXCMBase
    {
        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMScenePerception_SetVoxelResolution(IntPtr sp, PXCMScenePerception.VoxelResolution resolution);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern PXCMScenePerception.VoxelResolution PXCMScenePerception_QueryVoxelResolution(IntPtr sp);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMScenePerception_EnableSceneReconstruction(IntPtr sp, [MarshalAs(UnmanagedType.Bool)] Boolean enabledFlag);

        [DllImport(PXCMBase.DLLNAME)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern Boolean PXCMScenePerception_IsSceneReconstructionEnabled(IntPtr sp);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMScenePerception_SetInitialPose(IntPtr sp, Single[] pose);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMScenePerception_GetCameraPose(IntPtr sp, [Out] Single[] pose);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern TrackingAccuracy PXCMScenePerception_QueryTrackingAccuracy(IntPtr sp);

        [DllImport(PXCMBase.DLLNAME)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern Boolean PXCMScenePerception_IsReconstructionUpdated(IntPtr sp);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCMScenePerception_QueryVolumePreview(IntPtr sp, Single[] pose);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMScenePerception_Reset(IntPtr sp, Single[] pose);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMScenePerception_SetMeshingThresholds(IntPtr sp, Single maxDistanceChangeThreshold, Single avgDistanceChangeThreshold);

        [DllImport(PXCMBase.DLLNAME, CharSet=CharSet.Unicode)]
        internal static extern pxcmStatus PXCMScenePerception_SaveMesh(IntPtr sp, String fileName, [MarshalAs(UnmanagedType.Bool)] Boolean fillHoles);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern Single PXCMScenePerception_CheckSceneQuality(IntPtr sp, PXCMCapture.Sample sample);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCMScenePerception_CreatePXCBlockMeshingData(IntPtr sp, Int32 maxBlockMesh, Int32 maxVertices, Int32 maxFaces, [MarshalAs(UnmanagedType.Bool)] Boolean useColor);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMScenePerception_DoMeshingUpdate(IntPtr sp, IntPtr data, [MarshalAs(UnmanagedType.Bool)] Boolean fillHoles);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMScenePerception_FillDepthImage(IntPtr sp, IntPtr dImage);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMScenePerception_GetNormals(IntPtr sp, [Out] PXCMPoint3DF32[] normals);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMScenePerception_GetVertices(IntPtr sp, [Out] PXCMPoint3DF32[] vertices);

        [DllImport(PXCMBase.DLLNAME, CharSet=CharSet.Unicode)]
        internal static extern pxcmStatus PXCMScenePerception_SaveCurrentState(IntPtr sp, String fileName);

        [DllImport(PXCMBase.DLLNAME, CharSet=CharSet.Unicode)]
        internal static extern pxcmStatus PXCMScenePerception_LoadState(IntPtr sp, String fileName);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCMScenePerception_CreatePXCSurfaceVoxelsData(IntPtr sp, Int32 initialEstimateOfVoxelCount, Boolean bUseColor); 
 
        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMScenePerception_ExportSurfaceVoxels(IntPtr sp, IntPtr pSurfaceVoxelsData, IntPtr lowerLeftFrontPoint, IntPtr upperRightRearPoint); 

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMScenePerception_ExtractPlanes(IntPtr sp, PXCMCapture.Sample sample, Single minPlaneArea, Int32 maxPlaneNumber, Single[] pEquations, byte[] pIndexImage);

        internal pxcmStatus ExtractPlanesINT(IntPtr sp, PXCMCapture.Sample sample, Single minPlaneArea, Int32 maxPlaneNumber, Single[] pPlaneEq, byte[] pPlaneIndexImg)
        {
            return PXCMScenePerception_ExtractPlanes(sp, sample, minPlaneArea, maxPlaneNumber, pPlaneEq, pPlaneIndexImg);
        }

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMScenePerception_SetMeshingResolution(IntPtr sp, MeshResolution meshResolution);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern Int32 PXCMScenePerception_QueryMeshingResolution(IntPtr sp);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMScenePerception_QueryMeshingThresholds(IntPtr sp, out Single maxDistanceChangeThreshold, out Single avgDistanceChange);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMScenePerception_SetMeshingRegion(IntPtr sp, IntPtr lowerLeftFrontPoint, IntPtr upperRightRearPoint);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMScenePerception_ClearMeshingRegion(IntPtr sp);
        
        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMScenePerception_SetCameraPose(IntPtr sp, Single[] pose);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern Single PXCMScenePerception_QueryVoxelSize(IntPtr sp);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMScenePerception_GetInternalCameraIntrinsics(IntPtr sp, IntPtr spIntrinsics);


        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMScenePerception_DoReconstruction(IntPtr sp, PXCMCapture.Sample sample, Single[] pose);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMScenePerception_EnableRelocalization(IntPtr sp, Boolean enableRelocalization);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern Int32 PXCMScenePerception_TransformPlaneEquationToWorld(IntPtr sp, Int32 maxPlaneNumber, Single[] pEquations, Single[] pose);
    };

#if RSSDK_IN_NAMESPACE
}
#endif
