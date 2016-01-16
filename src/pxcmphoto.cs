/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2015 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

/**
    This interface defines the photo container. Call PXCMSession.CreatePhoto to create
	an instance of this object. Then initiailize it with different member functions.

	The interface extends PXCMMetadata. Use QueryInstance<PXCMMetadata>() to access
	the PXCMMetadata features.
*/
    public partial class PXCMPhoto : PXCMBase
    {
        new public const Int32 CUID = 0x32564447;
        public const Int32 MAX_SIZE = 16;

      	/**
	        @brief Copy the content from the source photo
	        @param[in]  photo	The source photo.
	        @return PXCM_STATUS_NO_ERROR	Successful execution.
	    */
        public pxcmStatus CopyPhoto(PXCMPhoto photo)
        {
            return PXCMPhoto_CopyPhoto(instance, photo.instance);
        }

        /** 
            @brief Import the preview sample content into the photo instance.
            @param[in]  sample				The PXCMCapture.Sample instance from the SenseManager QuerySample().
            @return PXC_STATUS_NO_ERROR     Successful execution.
        */
        public pxcmStatus ImportFromPreviewSample(PXCMCapture.Sample sample) {
            return PXCMPhoto_ImportFromPreviewSample(instance, sample);
        }

        /** 
            @brief Check if a file in an XDM file or not.
            @param[in]   filename           The file name.
            @return true if file is XDM and false otherwise.
        */ 
        public bool IsXDM(String filename) {
            return PXCMPhoto_IsXDM(instance, filename);
        }

        /** 
            @brief Import the photo content from the XDM File Format v2.0.
            @param[in]   filename           The file name.
            @return PXC_STATUS_NO_ERROR     Successful execution.
        */ 
        public pxcmStatus LoadXDM(String filename) {
            return PXCMPhoto_LoadXDM(instance, filename);
        }

        /** 
            @brief Export the photo content to the XDM File Format v2.0.
		    @param[in]   filename           The file name.
		    @return PXC_STATUS_NO_ERROR     Successful execution.
        */
        public pxcmStatus SaveXDM(String filename)
        {
            return PXCMPhoto_SaveXDM(instance, filename);
        }

        /** 
            @brief Get the reference image of the photo. The reference image is usually the processed color image.
            @return The PXCMImage instance.
        */
        public PXCMImage QueryContainerImage()
        {
            IntPtr image = PXCMPhoto_QueryContainerImage(instance);
            return image == IntPtr.Zero ? null : new PXCMImage(image, false);
        }

        /**
            @brief copy the camera[0] color image to the container image of the photo.
        */
        public void ResetContainerImage()
        {
            PXCMPhoto_ResetContainerImage(instance);
        }

        /**
            @brief Get the raw depth image of the photo. This would be the unprocessed depth captured from the camera or loaded from a file if it existed.
            @return The PXCMImage instance.
        */
        public PXCMImage QueryRawDepthImage()
        {
            IntPtr image = PXCMPhoto_QueryRawDepthImage(instance);
            return image == IntPtr.Zero ? null : new PXCMImage(image, false);
        }

        /**
           @brief Get the depth map in camera[camIdx] of the photo. The depth map in camera[0] is the holefilled depth.
           @return The PXCMImage instance.
       */
        public PXCMImage QueryDepthImage(Int32 camIdx)
        {
            IntPtr image = PXCMPhoto_QueryDepthImage(instance, camIdx);
            return image == IntPtr.Zero ? null : new PXCMImage(image, false);
        }

        /**
            @brief Get the depth image of the photo. This would be the processed depth if it undergoes processing.
            @return The PXCMImage instance.
        */
        public PXCMImage QueryDepthImage()
        {
            return QueryDepthImage(0);

        } //Deprecate
   
        /**
	        @brief Get the color image in camera[camIdx] of the photo. The unedited image is usually the unprocessed color image in camera[0].
	        @return The PXCMImage instance.
	    */
        public PXCMImage QueryColorImage(Int32 camIdx) 
        {
            IntPtr image = PXCMPhoto_QueryColorImage(instance, camIdx);
            return image == IntPtr.Zero ? null : new PXCMImage(image, false);
        }

        /**
           @brief Get the original image of the photo. The original image is usually the unprocessed color image.
           @return The PXCMImage instance.
       */
        public PXCMImage QueryColorImage()
        {
            return QueryColorImage(0);

        } //Deprecate
     
        /**
	        @brief Get the device revision. The revision of the XDM spec, e.g. “1.0”. 
	        Changes to a minor version number ("1.1", "1.2", etc.) do not break compatibility within
	        that major version. See the section on Versioning, under Schema, for more on this. Note 
	        that this field is informational; actual compatibility depends on namespace versions.
	        nchars input size of the requested buffer for overrun safety 
	        @return The Revision string.
	    */
        public String QueryXDMRevision() 
        {
            return QueryXDMRevisionINT(instance);
        }

        /** 
        @brief This checks that the signature of the container image did not change. 
        Using Adler 32 for signature. 
        @returns false if signature changed and true otherwise
        */
	    public bool CheckSignature()
        {
            return PXCMPhoto_CheckSignature(instance);
        }

        /**
	        @brief VendorInfo
	    */
        [Serializable]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public class VendorInfo
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public String model; /*The model of the element that created the content*/
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public String manufacturer; /*The manufacturer of the element that created the content*/
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public String notes; /*General Comments*/
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            internal String reserved;

            public VendorInfo()
            {
                model = "";
                manufacturer = "";
                notes = "";
                reserved = "";
            }

        };
       
        /**
	        @brief Get the device vendor info.
	        @return The VendorInfo struct.
	    */
        public void QueryDeviceVendorInfo(out VendorInfo vInfo)
        {
            vInfo = new VendorInfo();
            PXCMPhoto_QueryDeviceVendorInfo(instance, vInfo);
        
        }

        /**
            @brief Get the camera[camIdx] vendor info.
            @return The VendorInfo struct.
        */
        public void QueryCameraVendorInfo(Int32 camIdx, out VendorInfo vInfo)
        {
            vInfo = new VendorInfo();
            PXCMPhoto_QueryCameraVendorInfo(instance, camIdx, vInfo);

        }

        /**
            @brief Get the number of cameras in the device.
            @return The number of cameras.
        */
        public Int32 QueryNumberOfCameras()
        {
            return PXCMPhoto_QueryNumberOfCameras(instance);
        }

        /**
	        @brief Get the camera[camIdx] pose = Translation and rotation.
	        @return The translation(x,y,z) For the first camera, this is 0. For additional cameras, 
	        this is relative to the first camera. For the rotation(x,y,z,w) writers of this format 
	        should make an effort to normalize [x,y,z], but readers should not expect the rotation axis to be normalized..
	        trans.x = x position in meters. 
	        trans.y = y position in meters.
	        trans.z = z position in meters.
	        rot.x = x component of the rotation vector in the axis-angle representation.
	        rot.y = y component of the rotation vector in the axis-angle representation.
	        rot.z = z component of the rotation vector in the axis-angle representation.
	        rot.w = w rotation angle in radians in the axis-angle representation.
	    */
        public void QueryCameraPose(Int32 camIdx, out PXCMPoint3DF32 translation, out PXCMPoint4DF32 rotation)
        {
            translation = new PXCMPoint3DF32(); rotation = new PXCMPoint4DF32();
            PXCMPhoto_QueryCameraPose(instance, camIdx, ref translation, ref rotation);
  
        }

        /**
	        @brief PerspectiveModel.
	     */
        [Serializable]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public class PerspectiveCameraModel
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public String model;
            public PXCMPointF32 focalLength; /*(ƒx,fy) / max(width, height) in pixels*/
            public PXCMPointF32 principalPoint; /*(px/width,py/height) in pixels*/
            public Single skew; /*The skew of the image camera in degrees*/
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public Single[] radialDistortion;/*k1, k2, k3*/
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public Single[] tangentialDistortion; /*p1, p2*/
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            internal Single[] reserved;

            public PerspectiveCameraModel()
            {
                reserved = new Single[6];
                radialDistortion = new Single[3];
                tangentialDistortion = new Single[2];
                model = "";

            }
        };

        /**
            @brief Get the PerspectiveCameraModel of camera[camIdx].
            @return The PerspectiveCameraModel
        */
        public void QueryCameraPerspectiveModel(Int32 camIdx, out PerspectiveCameraModel pModel)
        {
            pModel = new PerspectiveCameraModel();
            PXCMPhoto_QueryCameraPerspectiveModel(instance, camIdx, pModel);
        }

        /** constructors and misc */
        internal PXCMPhoto(IntPtr instance, Boolean delete)
            : base(instance, delete)
        {
        }

        /** 
            @brief Increase a reference count of the sample.
        */
        public new void AddRef()
        {
            using (PXCMAddRef bs = this.QueryInstance<PXCMAddRef>())
            {
                if (bs == null) return;
                bs.AddRef();
            }
        }
    };

#if RSSDK_IN_NAMESPACE
}
#endif