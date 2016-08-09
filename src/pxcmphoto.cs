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

/// <summary>
/// This interface defines the photo container. Call PXCMSession.CreatePhoto to create
/// an instance of this object. Then initiailize it with different member functions.
/// 
/// The interface extends PXCMMetadata. Use QueryInstance<PXCMMetadata>() to access
/// the PXCMMetadata features.
/// </summary>
public partial class PXCMPhoto : PXCMBase
{
    new public const Int32 CUID = 0x32564447;
    public const Int32 MAX_SIZE = 16;

    /// <summary> 
    /// Import the preview sample content into the photo instance.
    /// </summary>
    /// <param name="sample"> The PXCMCapture.Sample instance from the SenseManager QuerySample().</param>
    /// <returns> PXCM_STATUS_NO_ERROR     Successful execution.</returns>
    public pxcmStatus ImportFromPreviewSample(PXCMCapture.Sample sample)
    {
        return PXCMPhoto_ImportFromPreviewSample(instance, sample);
    }

    /// <summary> 
    /// Check if a file in an XDM file or not.
    /// </summary> 
    /// <param name="filename"> The file name.</param>
    /// <returns> true if file is XDM and false otherwise.</returns>
    public bool IsXDM(String filename)
    {
        return PXCMPhoto_IsXDM(instance, filename);
    }


    /// <summary> 
    /// SubSample: Subsampling rate to load High Res images faster 
    /// NO_SUBSAMPLING = 0
    /// SUBSAMPLE_2 = 2
    /// SUBSAMPLE_4 = 4
    /// SUBSAMPLE_8 = 8
    /// </summary>
    public enum Subsample
    {
        NO_SUBSAMPLING,
        SUBSAMPLE_2,
        SUBSAMPLE_4,
        SUBSAMPLE_8,
    };


    /// <summary> 
    /// Import the photo content from the XDM File Format v2.0.
    /// </summary> 
    /// <param name="filename"> The file name.</param>
    /// <param name="subsample"> subsampling rate.</param>
    /// <returns> PXCM_STATUS_NO_ERROR     Successful execution.</returns>
    public pxcmStatus LoadXDM(String filename, Subsample subsample)
    {
        return PXCMPhoto_LoadXDM(instance, filename, subsample);
    }

    public pxcmStatus LoadXDM(String filename)
    {
        return LoadXDM(filename, Subsample.NO_SUBSAMPLING);
    }

    /// <summary> 
    /// Export the photo content to the XDM File Format v2.0.
    /// </summary>
    /// <param name="filename"> The file name.</param>
    /// <returns> PXCM_STATUS_NO_ERROR     Successful execution.</returns>
    public pxcmStatus SaveXDM(String filename)
    {
        return PXCMPhoto_SaveXDM(instance, filename);
    }

    /// <summary>
    /// Copy the content from the source photo
    /// </summary>
    /// <param name="photo"> The source photo.</param>
    /// <returns> PXCM_STATUS_NO_ERROR	Successful execution.</returns>
    public pxcmStatus CopyPhoto(PXCMPhoto photo)
    {
        return PXCMPhoto_CopyPhoto(instance, photo.instance);
    }

    /// <summary> 
    /// Get the reference image of the photo. The reference image is usually the processed color image.
    /// </summary>
    /// <returns> The PXCMImage instance.</returns>
    public PXCMImage QueryContainerImage()
    {
        IntPtr image = PXCMPhoto_QueryContainerImage(instance);
        return image == IntPtr.Zero ? null : new PXCMImage(image, false);
    }

    /// <summary>
    /// copy the camera[0] color image to the container image of the photo.
    /// </summary>
    public pxcmStatus ResetContainerImage()
    {
        return PXCMPhoto_ResetContainerImage(instance);
    }

    /// <summary>
    /// Get the color image in camera[camIdx] of the photo. The unedited image is usually the unprocessed color image in camera[0].
    /// </summary>
    /// <returns> The PXCMImage instance.</returns>
    public PXCMImage QueryImage(Int32 camIdx)
    {
        IntPtr image = PXCMPhoto_QueryImage(instance, camIdx);
        return image == IntPtr.Zero ? null : new PXCMImage(image, false);
    }

    /// <summary>
    /// Get the original image of the photo. The original image is usually the unprocessed color image.
    /// </summary>
    /// <returns> The PXCMImage instance.</returns>
    public PXCMImage QueryImage()
    {
        return QueryImage(0);

    }

    /// <summary>
    /// Get the raw depth image of the photo. This would be the unprocessed depth captured from the camera or loaded from a file if it existed.
    /// </summary>
    /// <returns> The PXCMImage instance.</returns>
    public PXCMImage QueryRawDepth()
    {
        IntPtr image = PXCMPhoto_QueryRawDepth(instance);
        return image == IntPtr.Zero ? null : new PXCMImage(image, false);
    }

    /// <summary>
    /// Get the depth map in camera[camIdx] of the photo. The depth map in camera[0] is the holefilled depth.
    /// <returns> The PXCMImage instance.</returns>
    /// </summary>
    public PXCMImage QueryDepth(Int32 camIdx)
    {
        IntPtr image = PXCMPhoto_QueryDepth(instance, camIdx);
        return image == IntPtr.Zero ? null : new PXCMImage(image, false);
    }

    /// <summary>
    /// Get the depth image of the photo. This would be the processed depth if it undergoes processing.
    /// </summary>
    /// <returns> The PXCMImage instance.</returns>
    public PXCMImage QueryDepth()
    {
        return QueryDepth(0);

    }

    /// <summary>
    /// Get the device revision. The revision of the XDM spec, e.g. “1.0”. 
    /// Changes to a minor version number ("1.1", "1.2", etc.) do not break compatibility within
    /// that major version. See the section on Versioning, under Schema, for more on this. Note 
    /// that this field is informational; actual compatibility depends on namespace versions.
    /// nchars input size of the requested buffer for overrun safety 
    /// </summary>
    /// <returns> The Revision string.</returns>
    public String QueryXDMRevision()
    {
        return QueryXDMRevisionINT(instance);
    }

    /// <summary>
    /// VendorInfo
    /// </summary>
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

    /// <summary>
    /// Get the device vendor info.
    /// </summary>
    /// <returns> The VendorInfo struct.</returns>
    public void QueryDeviceVendorInfo(out VendorInfo vInfo)
    {
        vInfo = new VendorInfo();
        PXCMPhoto_QueryDeviceVendorInfo(instance, vInfo);

    }

    /// <summary>
    /// Get the camera[camIdx] vendor info.
    /// </summary>
    /// <returns> The VendorInfo struct.</returns>
    public void QueryCameraVendorInfo(Int32 camIdx, out VendorInfo vInfo)
    {
        vInfo = new VendorInfo();
        PXCMPhoto_QueryCameraVendorInfo(instance, camIdx, vInfo);

    }

    /// <summary>
    /// Get the number of cameras in the device.
    /// </summary>
    /// <returns> The number of cameras.</returns>
    public Int32 QueryNumberOfCameras()
    {
        return PXCMPhoto_QueryNumberOfCameras(instance);
    }

    /// <summary>
    /// Get the camera[camIdx] pose = Translation and rotation.
    /// <returns> The translation(x,y,z) For the first camera, this is 0. For additional cameras, </returns>
    /// this is relative to the first camera. For the rotation(x,y,z,w) writers of this format 
    /// should make an effort to normalize [x,y,z], but readers should not expect the rotation axis to be normalized..
    /// trans.x = x position in meters. 
    /// trans.y = y position in meters.
    /// trans.z = z position in meters.
    /// rot.x = x component of the rotation vector in the axis-angle representation.
    /// rot.y = y component of the rotation vector in the axis-angle representation.
    /// rot.z = z component of the rotation vector in the axis-angle representation.
    /// rot.w = w rotation angle in radians in the axis-angle representation.
    /// </summary>
    public void QueryCameraPose(Int32 camIdx, out PXCMPoint3DF32 translation, out PXCMPoint4DF32 rotation)
    {
        translation = new PXCMPoint3DF32(); rotation = new PXCMPoint4DF32();
        PXCMPhoto_QueryCameraPose(instance, camIdx, ref translation, ref rotation);

    }

    /// <summary>
    /// PerspectiveModel.
    /// </summary>
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

    /// <summary>
    /// Get the PerspectiveCameraModel of camera[camIdx].
    /// </summary>
    /// <returns> The PerspectiveCameraModel</returns>
    public void QueryCameraPerspectiveModel(Int32 camIdx, out PerspectiveCameraModel pModel)
    {
        pModel = new PerspectiveCameraModel();
        PXCMPhoto_QueryCameraPerspectiveModel(instance, camIdx, pModel);
    }

    /// <summary> 
    /// This checks that the signature of the container image did not change. 
    /// Using Adler 32 for signature. 
    /// </summary>
    /// <returns>s false if signature changed and true otherwise</returns>
    public bool CheckSignature()
    {
        return PXCMPhoto_CheckSignature(instance);
    }

    /// <summary> constructors and misc 
    /// </summary>
    internal PXCMPhoto(IntPtr instance, Boolean delete)
        : base(instance, delete)
    {
    }

    /// <summary> 
    /// Increase a reference count of the sample.
    /// </summary>
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
