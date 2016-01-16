/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2014 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections.Generic;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

    /**
	@class PXCMBlobConfiguration
	@brief Retrieve the current configuration of the blob module and set new configuration values.
	@note Changes to PXCBlobConfiguration are applied only when ApplyChanges() is called.
*/
    public partial class PXCMBlobConfiguration : PXCMBase
    {
        new public const Int32 CUID = 0x47434d42;

        /* General */

        /**
            @brief Apply the configuration changes to the blob module.
            This method must be called in order for any configuration changes to apply.
            @return PXCM_STATUS_NO_ERROR - successful operation.
            @return PXCM_STATUS_DATA_NOT_INITIALIZED - the configuration was not initialized.                        
        */
        public pxcmStatus ApplyChanges()
        {
            return PXCMBlobConfiguration_ApplyChanges(instance);
        }

        /**  
            @brief Restore configuration settings to the default values.
            @return PXCM_STATUS_NO_ERROR - successful operation.
            @return PXCM_STATUS_DATA_NOT_INITIALIZED - the configuration was not initialized.                  
        */
        public pxcmStatus RestoreDefaults()
        {
            return PXCMBlobConfiguration_RestoreDefaults(instance);
        }

        /**
            @brief Retrieve the blob module's current configuration settings.
            @return PXCM_STATUS_NO_ERROR - successful operation.
            @return PXCM_STATUS_DATA_NOT_INITIALIZED - failed to retrieve current configuration.
        */
        public pxcmStatus Update()
        {
            return PXCMBlobConfiguration_Update(instance);
        }

        /* Configuration */

        /**
            @brief Set the smoothing strength for the segmentation image.
            @param[in] smoothingValue - a value between 0 (no smoothing) to 1 (very smooth).
            @return PXCM_STATUS_NO_ERROR - successful operation.
            @return PXCM_STATUS_PARAM_UNSUPPORTED - invalid smoothing value.
            @see QuerySegmentationSmoothing
        */
        public pxcmStatus SetSegmentationSmoothing(Single smoothingValue)
        {
            return PXCMBlobConfiguration_SetSegmentationSmoothing(instance, smoothingValue);
        }

        /**
            @brief Get the segmentation image smoothing value.
            @return The segmentation image smoothing value.
            @see SetSegmentationSmoothing
        */
        public Single QuerySegmentationSmoothing()
        {
            return PXCMBlobConfiguration_QuerySegmentationSmoothing(instance);
        }

        /**
             @brief Sets the contour smoothing value.
             @param[in] smoothingValue - a value between 0 (no smoothing) to 1 (very smooth).
             @return PXCM_STATUS_NO_ERROR - successful operation.
             @return PXCM_STATUS_PARAM_UNSUPPORTED - invalid smoothing value.
             @see QueryContourSmoothing
         */
        public pxcmStatus SetContourSmoothing(Single smoothingValue)
        {
            return PXCMBlobConfiguration_SetContourSmoothing(instance, smoothingValue);
        }

        /**
             @brief Get the contour smoothing value.
             @return The contour smoothing value.
             @see SetContourSmoothing
         */
        public Single QueryContourSmoothing()
        {
            return PXCMBlobConfiguration_QueryContourSmoothing(instance);
        }

        /** 
            @brief Set the maximal number of blobs that can be detected (default is 1).
            @param[in] maxBlobs - the maximal number of blobs that can be detected (limited to 4).
            @return PXCM_STATUS_NO_ERROR - successful operation.
            @return PXCM_STATUS_PARAM_UNSUPPORTED - invalid maxBlobs value (in this case, the last valid value will be retained).
            @see QueryMaxBlobs
        */
        public pxcmStatus SetMaxBlobs(Int32 maxBlobs)
        {
            return PXCMBlobConfiguration_SetMaxBlobs(instance, maxBlobs);
        }

        /**
            @brief Get the maximal number of blobs that can be detected.
            @return The maximal number of blobs that can be detected.
            @see SetMaxBlobs
        */
        public Int32 QueryMaxBlobs()
        {
            return PXCMBlobConfiguration_QueryMaxBlobs(instance);
        }

        /** 
             @brief Set the maximal distance in meters of a detected blob from the sensor. 
             Only objects that are within this limit will be identified as blobs.
             @param[in] maxDistance - the maximal distance in meters of a blob from the sensor
             @return PXCM_STATUS_NO_ERROR - successful operation
             @return PXCM_STATUS_PARAM_UNSUPPORTED - invalid maxDistance value (in this case, the last valid value will be retained).
             @see QueryMaxDistance
         */
        public pxcmStatus SetMaxDistance(Single maxDistance)
        {
            return PXCMBlobConfiguration_SetMaxDistance(instance, maxDistance);
        }

        /** 
            @brief Get the maximal distance in meters of a detected blob from the sensor. 
            @return The maximal distance of a detected blob from the sensor.
            @see SetMaxDistance
        */
        public Single QueryMaxDistance()
        {
            return PXCMBlobConfiguration_QueryMaxDistance(instance);
        }

        /**
            @brief Set the maximal depth in meters of a blob (maximal distance between closest and farthest points in the blob).
            @param[in] maxDepth - the maximal depth in meters of the blob.
            @return PXCM_STATUS_NO_ERROR - successful operation.
            @return PXCM_STATUS_PARAM_UNSUPPORTED - invalid maxDepth value (in this case, the last valid value will be retained).
            @see QueryMaxObjectDepth
        */
        public pxcmStatus SetMaxObjectDepth(Single maxDepth)
        {
            return PXCMBlobConfiguration_SetMaxObjectDepth(instance, maxDepth);
        }

        /** 
            @brief Get the maximal depth in meters of a blob.
            @return The maximal depth in meters of a blob.
            @see SetMaxObjectDepth
        */
        public Single QueryMaxObjectDepth()
        {
            return PXCMBlobConfiguration_QueryMaxObjectDepth(instance);
        }

        /** 
            @brief Set the minimal blob size in pixels.
            Only objects that are larger than this size are identified as blobs.
            @param[in] minBlobSize - the minimal blob size in pixels (cannot be more than a quarter of the image size in pixels).
            @return PXCM_STATUS_NO_ERROR - successful operation.
            @return PXCM_STATUS_PARAM_UNSUPPORTED - invalid minBlobSize value (in this case, the last valid value will be retained).
            @see QueryMinPixelCount
        */
        public pxcmStatus SetMinPixelCount(Int32 minBlobSize)
        {
            return PXCMBlobConfiguration_SetMinPixelCount(instance, minBlobSize);
        }

        /** 
            @brief Get the minimal blob size in pixels.
            @return The minimal blob size in pixels.
            @see SetMinPixelCount
        */
        public Int32 QueryMinPixelCount()
        {
            return PXCMBlobConfiguration_QueryMinPixelCount(instance);
        }

        /**
             @brief Enable extraction of the segmentation image.
             @param[in] enableFlag - set to true if the segmentation image should be extracted; otherwise set to false. 
             @return PXCM_STATUS_NO_ERROR - successful operation.
             @see IsSegmentationImageEnabled
         */
        public pxcmStatus EnableSegmentationImage(Boolean enableFlag)
        {
            return PXCMBlobConfiguration_EnableSegmentationImage(instance, enableFlag);
        }

        /**
            @brief Return the segmentation image extraction flag.
            @return The segmentation image extraction flag.
            @see EnableSegmentationImage
        */
        public Boolean IsSegmentationImageEnabled()
        {
            return PXCMBlobConfiguration_IsSegmentationImageEnabled(instance);
        }

        /**
            @brief Enable extraction of the contour data.
            @param[in] enableFlag - set to true if contours should be extracted; otherwise set to false. 
            @return PXCM_STATUS_NO_ERROR - successful operation.
            @see IsContourExtractionEnabled
        */
        public pxcmStatus EnableContourExtraction(Boolean enableFlag)
        {
            return PXCMBlobConfiguration_EnableContourExtraction(instance, enableFlag);
        }

        /**
        @brief Return the contour extraction flag.
        @return The contour extraction flag.
        @see EnableContourExtraction
        */
        public Boolean IsContourExtractionEnabled()
        {
            return PXCMBlobConfiguration_IsContourExtractionEnabled(instance);
        }

        /** 
            @brief Set the minimal contour size in points.
            Objects with external contours that are smaller than the limit are not identified as blobs.
            @param[in] minContourSize - the minimal contour size in points.
            @return PXCM_STATUS_NO_ERROR - successful operation.
            @return PXCM_STATUS_PARAM_UNSUPPORTED - invalid minContourSize value (in this case, the last valid value will be retained).
            @see QueryMinContourSize
        */
        public pxcmStatus SetMinContourSize(Int32 minContourSize)
        {
            return PXCMBlobConfiguration_SetMinContourSize(instance, minContourSize);
        }

        /** 
             @brief Get the minimal contour size in points.
             @return The minimal contour size in points.
             @see SetMinContourSize
         */
        public Int32 QueryMinContourSize()
        {
            return PXCMBlobConfiguration_QueryMinContourSize(instance);
        }

        /**
            @brief Enable or disable the stabilization feature.\n
		
            Enabling this feature produces smoother tracking of the extremity points, ignoring small shifts and "jitters".\n
            @Note: in some cases the tracking may be less sensitive to minor movements and some blob pixels may be outside of the extremities. 
		
            @param[in] enableFlag - true to enable the stabilization; false to disable it.
            @return PXCM_STATUS_NO_ERROR - operation succeeded. 
        */
        public pxcmStatus EnableStabilizer(Boolean enableFlag)
        {
            return PXCMBlobConfiguration_EnableStabilizer(instance, enableFlag);
        }

        /**
        @brief Return blob stabilizer activation status.
        @return true if blob stabilizer is enabled, false otherwise.
        */
        public Boolean IsStabilizerEnabled()
        {
            return PXCMBlobConfiguration_IsStabilizerEnabled(instance);
        }

        /** 
		@brief Set the maximal blob size in pixels.
		Only objects that are smaller than this size are identified as blobs.
		@param[in] maxBlobSize - the maximal blob size in pixels.
		@return PXCM_STATUS_NO_ERROR - successful operation.
		@return PXCM_STATUS_PARAM_UNSUPPORTED - invalid maxBlobSize value (in this case, the last valid value will be retained).
		@see QueryMaxPixelCount
	    */
        public pxcmStatus SetMaxPixelCount(Int32 maxBlobSize)
        {
            return PXCMBlobConfiguration_SetMaxPixelCount(instance, maxBlobSize);
        }

        /** 
		@brief Get the maximal blob size in pixels.
		@return The maximal blob size in pixels.
		@see SetMinPixelCount
	    */
        public Int32 QueryMaxPixelCount()
        {
            return PXCMBlobConfiguration_QueryMaxPixelCount(instance);
        }

        /** 
         @brief Set the maximal blob area in square meter.
         Only objects that are smaller than this area are identified as blobs.
         @param[in] maxBlobArea - the maximal blob area in square meter.
         @return PXCM_STATUS_NO_ERROR - successful operation.
         @return PXCM_STATUS_PARAM_UNSUPPORTED - invalid maxBlobArea value (in this case, the last valid value will be retained).
         @see QueryMaxBlobArea
         */
        public pxcmStatus SetMaxBlobArea(Single maxBlobArea)
        {
            return PXCMBlobConfiguration_SetMaxBlobArea(instance, maxBlobArea);
        }

        /** 
		@brief Get the maximal blob area in square meter.
		@return The maximal blob area in square meter.
		@see SetMaxBlobArea
	    */
        public Single QueryMaxBlobArea()
        {
            return PXCMBlobConfiguration_QueryMaxBlobArea(instance);
        }

        /** 
          @brief Set the minimal blob area in square meter.
          Only objects that are larger than this area are identified as blobs.
          @param[in] minBlobArea - the minimal blob area in square meter.
          @return PXCM_STATUS_NO_ERROR - successful operation.
          @return PXCM_STATUS_PARAM_UNSUPPORTED - invalid minBlobArea value (in this case, the last valid value will be retained).
          @see QueryMinBlobArea
      */
        public pxcmStatus SetMinBlobArea(Single minBlobArea)
        {
            return PXCMBlobConfiguration_SetMinBlobArea(instance, minBlobArea);
        }

        /** 
         @brief Get the minimal blob area in square meter.
         @return The minimal blob area in square meter.
         @see SetMinBlobArea
        */
        public Single QueryMinBlobArea()
        {
            return PXCMBlobConfiguration_QueryMinBlobArea(instance);
        }


        /**
           @brief Set the smoothing strength for the blob extraction.
           @param[in] smoothingValue - a value between 0 (no smoothing) to 1 (strong smoothing).
           @return PXCM_STATUS_NO_ERROR - successful operation.
           @return PXCM_STATUS_PARAM_UNSUPPORTED - invalid smoothing value (in this case, the last valid value will be retained).
           @see QueryBlobSmoothing
       */
        public pxcmStatus SetBlobSmoothing(Single smoothingValue)
        {
            return PXCMBlobConfiguration_SetBlobSmoothing(instance, smoothingValue);
        }

        /**
		@brief Get the segmentation blob smoothing value.
		@return The segmentation image smoothing value.
		@see SetBlobSmoothing
	    */
        public Single QueryBlobSmoothing()
        {
            return PXCMBlobConfiguration_QueryBlobSmoothing(instance);
        }


        /**
         @brief Enable blob data extraction correlated to color stream.
         @param[in] enableFlag - set to true if color mapping should be extracted; otherwise set to false. 
         @return PXCM_STATUS_NO_ERROR - successful operation.
         @see IsColorMappingEnabled
         @see  QueryBlob
         @see  enum SegmentationImageType		
     */
        public pxcmStatus EnableColorMapping(Boolean enableFlag)
        {
            return PXCMBlobConfiguration_EnableColorMapping(instance, enableFlag);
        }

        /**
	        @brief Return the color mapping extraction flag.
	        @return The color mapping extraction flag.
	        @see EnableColorMapping
	    */
        public Boolean IsColorMappingEnabled()
        {
            return PXCMBlobConfiguration_IsColorMappingEnabled(instance);
        }



       

        internal PXCMBlobConfiguration(IntPtr instance, Boolean delete)
            : base(instance, delete) { }
    };

#if RSSDK_IN_NAMESPACE
}
#endif
