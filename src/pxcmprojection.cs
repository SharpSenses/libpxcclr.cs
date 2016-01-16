/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2013 - 2014 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Runtime.InteropServices;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

    /**
	This interface defines mappings between various coordinate systems
	used by modules of the SDK. Call the PXCMCapture.Device.CreateProjection 
	to create an instance of this interface.

	The class extends PXCMSerializeableService. Use QueryInstance<PXCMSerializeableService> 
	to access the PXCMSerializableService interface.
 */
    public partial class PXCMProjection : PXCMBase
    {
        new public const Int32 CUID = 0x494A8537;

        /* Member Functions */

        /** 
            @brief Map depth coordinates to color coordinates for a few pixels.
            @param[in]	pos_uvz		The array of depth coordinates + depth value in the PXCMPoint3DF32 structure.
            @param[out]	pos_ij		The array of color coordinates, to be returned.
            @return PXCM_STATUS_NO_ERROR Successful execution.
        */
        public pxcmStatus MapDepthToColor(PXCMPoint3DF32[] pos_uvz, PXCMPointF32[] pos_ij)
        {
            return PXCMProjection_MapDepthToColor(instance, pos_uvz.Length, pos_uvz, pos_ij);
        }

        /** 
            @brief Map color coordinates to depth coordiantes for a few pixels.
            @param[in]  depth       The depthmap image.
            @param[in]	pos_ij		The array of color coordinates.
            @param[out]	pos_uv		The array of depth coordinates, to be returned.
            @return PXCM_STATUS_NO_ERROR Successful execution.
        */
        public pxcmStatus MapColorToDepth(PXCMImage depth, PXCMPointF32[] pos_ij, PXCMPointF32[] pos_uv)
        {
            return PXCMProjection_MapColorToDepth(instance, depth.instance, pos_ij.Length, pos_ij, pos_uv);
        }

        /** 
            @brief Map depth coordinates to world coordinates for a few pixels.
            @param[in]	pos_uvz		The array of depth coordinates + depth value in the PXCMPoint3DF32 structure.
            @param[out]	pos3d		The array of world coordinates, in mm, to be returned.
            @return PXCM_STATUS_NO_ERROR Successful execution.
        */
        public pxcmStatus ProjectDepthToCamera(PXCMPoint3DF32[] pos_uvz, PXCMPoint3DF32[] pos3d)
        {
            return PXCMProjection_ProjectDepthToCamera(instance, pos_uvz.Length, pos_uvz, pos3d);
        }

        /** 
            @brief Map color pixel coordinates to camera coordinates for a few pixels.
            @param[in]	pos_ijz		The array of color coordinates + depth value in the PXCMPoint3DF32 structure.
            @param[out]	pos3d		The array of camera coordinates, in mm, to be returned.
            @return PXCM_STATUS_NO_ERROR Successful execution.
        */
        public pxcmStatus ProjectColorToCamera(PXCMPoint3DF32[] pos_ijz, PXCMPoint3DF32[] pos3d)
        {
            return PXCMProjection_ProjectColorToCamera(instance, pos_ijz.Length, pos_ijz, pos3d);
        }

        /** 
            @brief Map camera coordinates to depth coordinates for a few pixels.
            @param[in]	pos3d		The array of world coordinates, in mm.
            @param[out]	pos_uv		The array of depth coordinates, to be returned.
            @return PXCM_STATUS_NO_ERROR Successful execution.
        */
        public pxcmStatus ProjectCameraToDepth(PXCMPoint3DF32[] pos3d, PXCMPointF32[] pos_uv)
        {
            return PXCMProjection_ProjectCameraToDepth(instance, pos3d.Length, pos3d, pos_uv);
        }

        /** 
            @brief Map camera coordinates to color coordinates for a few pixels.
            @param[in]	pos3d		The array of world coordinates, in mm.
            @param[out]	pos_ij		The array of color coordinates, to be returned.
            @return PXCM_STATUS_NO_ERROR Successful execution.
        */
        public pxcmStatus ProjectCameraToColor(PXCMPoint3DF32[] pos3d, PXCMPointF32[] pos_ij)
        {
            return PXCMProjection_ProjectCameraToColor(instance, pos3d.Length, pos3d, pos_ij);
        }

        /** 
            @brief Retrieve the UV map for the specific depth image. The UVMap is a PXCMPointF32 array of depth size width*height.
            @param[in]  depth		The depth image instance.
            @param[out] uvmap		The UV map, to be returned.
            @return PXCM_STATUS_NO_ERROR Successful execution.
        */
        public pxcmStatus QueryUVMap(PXCMImage depth, PXCMPointF32[] uvmap)
        {
            return PXCMProjection_QueryUVMap(instance, depth.instance, uvmap);
        }

        /** 
            @brief Retrieve the inverse UV map for the specific depth image. The inverse UV map maps color coordinates
            back to the depth coordinates. The inverse UVMap is a PXCMPointF32 array of color size width*height.
            @param[in]  depth		The depth image instance.
            @param[out] inv_uvmap	The inverse UV map, to be returned.
            @return PXCM_STATUS_NO_ERROR Successful execution.
        */
        public pxcmStatus QueryInvUVMap(PXCMImage depth, PXCMPointF32[] inv_uvmap)
        {
            return PXCMProjection_QueryInvUVMap(instance, depth.instance, inv_uvmap);
        }

        /** 
            @brief Retrieve the vertices for the specific depth image. The vertices is a PXCMPoint3DF32 array of depth 
            size width*height. The world coordiantes units are in mm.
            @param[in]  depth		The depth image instance.
            @param[out] inv_uvmap	The inverse UV map, to be returned.
            @return PXCM_STATUS_NO_ERROR Successful execution.
        */
        public pxcmStatus QueryVertices(PXCMImage depth, PXCMPoint3DF32[] vertices)
        {
            return PXCMProjection_QueryVertices(instance, depth.instance, vertices);
        }

        /** 
            @brief Get the color pixel for every depth pixel using the UV map, and output a color image, aligned in space 
                and resolution to the depth image.
            @param[in] depth		The depth image instance.
            @param[in] color		The color image instance.
            @return The output image in the depth image resolution.
        */
        public PXCMImage CreateColorImageMappedToDepth(PXCMImage depth, PXCMImage color)
        {
            IntPtr mappedColor = PXCMProjection_CreateColorImageMappedToDepth(instance, depth.instance, color.instance);
            return mappedColor != IntPtr.Zero ? new PXCMImage(mappedColor, true) : null;
        }

        /** 
            @brief Map every depth pixel to the color image resolution using the UV map, and output an incomplete 
            depth image (with holes), aligned in space and resolution to the color image. 
            @param[in] depth		The depth image instance.
            @param[in] color		The color image instance.
            @return The output image in the color image resolution.
        */
        public PXCMImage CreateDepthImageMappedToColor(PXCMImage depth, PXCMImage color)
        {
            IntPtr mappedDepth = PXCMProjection_CreateDepthImageMappedToColor(instance, depth.instance, color.instance);
            return mappedDepth != IntPtr.Zero ? new PXCMImage(mappedDepth, true) : null;
        }


        /* constructors and misc */
        internal PXCMProjection(IntPtr instance, Boolean delete)
            : base(instance, delete)
        {
        }
    }

#if RSSDK_IN_NAMESPACE
}
#endif