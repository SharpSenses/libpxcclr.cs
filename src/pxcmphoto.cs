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

        /* constructors & misc */
        internal PXCMPhoto(IntPtr instance, Boolean delete)
            : base(instance, delete)
        {
        }

        /** 
            @brief Get the reference image of the photo. The reference image is usually the processed color image.
            @return The PXCMImage instance.
        */ 
        public PXCMImage QueryReferenceImage() {
            IntPtr image=PXCMPhoto_QueryReferenceImage(instance);
            return image == IntPtr.Zero ? null : new PXCMImage(image, false);
        }

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
            @brief Import the photo content from the Google* Depth File Format v2.0.
            @param[in]   filename           The file name.
            @return PXC_STATUS_NO_ERROR     Successful execution.
        */ 
        public pxcmStatus LoadXDM(String filename) {
            return PXCMPhoto_LoadXDM(instance, filename);
        }

        /** 
            @brief Export the photo content to the Google Depth File Format v2.0.
		    @param[in]   filename           The file name.
		    @return PXC_STATUS_NO_ERROR     Successful execution.
        */
        public pxcmStatus SaveXDM(String filename)
        {
            return PXCMPhoto_SaveXDM(instance, filename);
        }

        /** 
            @brief Increase a reference count of the sample.
        */
        public new void AddRef() {
            using (PXCMBase bs = this.QueryInstance(PXCMAddRef.CUID))
            {
                if (bs == null) return;
                bs.AddRef();
            }
        }

        /**
            @brief Get the depth image of the photo. This would be the processed depth if it undergoes processing.
            @return The PXCMImage instance.
         */
        public PXCMImage QueryDepthImage()
        {
            IntPtr image = PXCMPhoto_QueryDepthImage(instance);
            return image == IntPtr.Zero ? null : new PXCMImage(image, false);
        }

        /**
	        @brief Get the original image of the photo. The original image is usually the unprocessed color image.
	        @return The PXCMImage instance.
	    */
        public PXCMImage QueryOriginalImage()
        {
            IntPtr image = PXCMPhoto_QueryOriginalImage(instance);
            return image == IntPtr.Zero ? null : new PXCMImage(image, false);
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

    };

#if RSSDK_IN_NAMESPACE
}
#endif