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

/// <summary>
/// @class PXCMBlobConfiguration
/// Retrieve the current configuration of the blob module and set new configuration values.
/// @note Changes to PXCBlobConfiguration are applied only when ApplyChanges() is called.
/// </summary>
public partial class PXCMBlobConfiguration : PXCMBase
{
    new public const Int32 CUID = 0x47434d42;

    /* General */

    /// <summary>
    /// Apply the configuration changes to the blob module.
    /// This method must be called in order for any configuration changes to apply.
    /// </summary>
    /// <returns> PXCM_STATUS_NO_ERROR - successful operation.</returns>
    /// <returns> PXCM_STATUS_DATA_NOT_INITIALIZED - the configuration was not initialized.                        </returns>
    public pxcmStatus ApplyChanges()
    {
        return PXCMBlobConfiguration_ApplyChanges(instance);
    }

    /// <summary>  
    /// Restore configuration settings to the default values.
    /// </summary>
    /// <returns> PXCM_STATUS_NO_ERROR - successful operation.</returns>
    /// <returns> PXCM_STATUS_DATA_NOT_INITIALIZED - the configuration was not initialized.                  </returns>
    public pxcmStatus RestoreDefaults()
    {
        return PXCMBlobConfiguration_RestoreDefaults(instance);
    }

    /// <summary>
    /// Retrieve the blob module's current configuration settings.
    /// </summary>
    /// <returns> PXCM_STATUS_NO_ERROR - successful operation.</returns>
    /// <returns> PXCM_STATUS_DATA_NOT_INITIALIZED - failed to retrieve current configuration.</returns>
    public pxcmStatus Update()
    {
        return PXCMBlobConfiguration_Update(instance);
    }

    /* Configuration */

    /// <summary>
    /// Set the smoothing strength for the segmentation image.
    /// </summary>
    /// <param name="smoothingValue"> - a value between 0 (no smoothing) to 1 (very smooth).</param>
    /// <returns> PXCM_STATUS_NO_ERROR - successful operation.</returns>
    /// <returns> PXCM_STATUS_PARAM_UNSUPPORTED - invalid smoothing value.</returns>
    /// <see cref="QuerySegmentationSmoothing"/>
    public pxcmStatus SetSegmentationSmoothing(Single smoothingValue)
    {
        return PXCMBlobConfiguration_SetSegmentationSmoothing(instance, smoothingValue);
    }

    /// <summary>
    /// Get the segmentation image smoothing value.
    /// </summary>
    /// <returns> The segmentation image smoothing value.</returns>
    /// <see cref="SetSegmentationSmoothing"/>
    public Single QuerySegmentationSmoothing()
    {
        return PXCMBlobConfiguration_QuerySegmentationSmoothing(instance);
    }

    /// <summary>
    /// Sets the contour smoothing value.
    /// </summary>
    /// <param name="smoothingValue"> - a value between 0 (no smoothing) to 1 (very smooth).</param>
    /// <returns> PXCM_STATUS_NO_ERROR - successful operation.</returns>
    /// <returns> PXCM_STATUS_PARAM_UNSUPPORTED - invalid smoothing value.</returns>
    /// <see cref="QueryContourSmoothing"/>
    public pxcmStatus SetContourSmoothing(Single smoothingValue)
    {
        return PXCMBlobConfiguration_SetContourSmoothing(instance, smoothingValue);
    }

    /// <summary>
    /// Get the contour smoothing value.
    /// </summary>
    /// <returns> The contour smoothing value.</returns>
    /// <see cref="SetContourSmoothing"/>
    public Single QueryContourSmoothing()
    {
        return PXCMBlobConfiguration_QueryContourSmoothing(instance);
    }

    /// <summary> 
    /// Set the maximal number of blobs that can be detected (default is 1).
    /// </summary>
    /// <param name="maxBlobs"> - the maximal number of blobs that can be detected (limited to 4).</param>
    /// <returns> PXCM_STATUS_NO_ERROR - successful operation.</returns>
    /// <returns> PXCM_STATUS_PARAM_UNSUPPORTED - invalid maxBlobs value (in this case, the last valid value will be retained).</returns>
    /// <see cref="QueryMaxBlobs
    public pxcmStatus SetMaxBlobs(Int32 maxBlobs)
    {
        return PXCMBlobConfiguration_SetMaxBlobs(instance, maxBlobs);
    }

    /// <summary>
    /// Get the maximal number of blobs that can be detected.
    /// </summary>
    /// <returns> The maximal number of blobs that can be detected.</returns>
    /// <see cref="SetMaxBlobs"/>
    public Int32 QueryMaxBlobs()
    {
        return PXCMBlobConfiguration_QueryMaxBlobs(instance);
    }

    /// <summary> 
    /// Set the maximal distance in meters of a detected blob from the sensor. 
    /// Only objects that are within this limit will be identified as blobs.
    /// </summary>
    /// <param name="maxDistance"> - the maximal distance in meters of a blob from the sensor</param>
    /// <returns> PXCM_STATUS_NO_ERROR - successful operation</returns>
    /// <returns> PXCM_STATUS_PARAM_UNSUPPORTED - invalid maxDistance value (in this case, the last valid value will be retained).</returns>
    /// <see cref="QueryMaxDistance"/>
    public pxcmStatus SetMaxDistance(Single maxDistance)
    {
        return PXCMBlobConfiguration_SetMaxDistance(instance, maxDistance);
    }

    /// <summary> 
    /// Get the maximal distance in meters of a detected blob from the sensor. 
    /// </summary>
    /// <returns> The maximal distance of a detected blob from the sensor.</returns>
    /// <see cref="SetMaxDistance"/>
    public Single QueryMaxDistance()
    {
        return PXCMBlobConfiguration_QueryMaxDistance(instance);
    }

    /// <summary>
    /// Set the maximal depth in meters of a blob (maximal distance between closest and farthest points in the blob).
    /// </summary>
    /// <param name="maxDepth"> - the maximal depth in meters of the blob.</param>
    /// <returns> PXCM_STATUS_NO_ERROR - successful operation.</returns>
    /// <returns> PXCM_STATUS_PARAM_UNSUPPORTED - invalid maxDepth value (in this case, the last valid value will be retained).</returns>
    /// <see cref="QueryMaxObjectDepth"/>
    public pxcmStatus SetMaxObjectDepth(Single maxDepth)
    {
        return PXCMBlobConfiguration_SetMaxObjectDepth(instance, maxDepth);
    }

    /// <summary> 
    /// Get the maximal depth in meters of a blob.
    /// </summary>
    /// <returns> The maximal depth in meters of a blob.</returns>
    /// <see cref="SetMaxObjectDepth"/>
    public Single QueryMaxObjectDepth()
    {
        return PXCMBlobConfiguration_QueryMaxObjectDepth(instance);
    }

    /// <summary> 
    /// Set the minimal blob size in pixels.
    /// Only objects that are larger than this size are identified as blobs.
    /// </summary>
    /// <param name="minBlobSize"> - the minimal blob size in pixels (cannot be more than a quarter of the image size in pixels).</param>
    /// <returns> PXCM_STATUS_NO_ERROR - successful operation.</returns>
    /// <returns> PXCM_STATUS_PARAM_UNSUPPORTED - invalid minBlobSize value (in this case, the last valid value will be retained).</returns>
    /// <see cref="QueryMinPixelCount"/>
    public pxcmStatus SetMinPixelCount(Int32 minBlobSize)
    {
        return PXCMBlobConfiguration_SetMinPixelCount(instance, minBlobSize);
    }

    /// <summary> 
    /// Get the minimal blob size in pixels.
    /// </summary>
    /// <returns> The minimal blob size in pixels.</returns>
    /// <see cref="SetMinPixelCount"/>
    public Int32 QueryMinPixelCount()
    {
        return PXCMBlobConfiguration_QueryMinPixelCount(instance);
    }

    /// <summary>
    /// Enable extraction of the segmentation image.
    /// </summary>
    /// <param name="enableFlag"> - set to true if the segmentation image should be extracted; otherwise set to false.</param>
    /// <returns> PXCM_STATUS_NO_ERROR - successful operation.</returns>
    /// <see cref="IsSegmentationImageEnabled"/>
    public pxcmStatus EnableSegmentationImage(Boolean enableFlag)
    {
        return PXCMBlobConfiguration_EnableSegmentationImage(instance, enableFlag);
    }

    /// <summary>
    /// Return the segmentation image extraction flag.
    /// </summary>
    /// <returns> The segmentation image extraction flag.</returns>
    /// <see cref="EnableSegmentationImage"/>
    public Boolean IsSegmentationImageEnabled()
    {
        return PXCMBlobConfiguration_IsSegmentationImageEnabled(instance);
    }

    /// <summary>
    /// Enable extraction of the contour data.
    /// </summary>
    /// <param name="enableFlag"> - set to true if contours should be extracted; otherwise set to false.</param>
    /// <returns> PXCM_STATUS_NO_ERROR - successful operation.</returns>
    /// <see cref="IsContourExtractionEnabled"/>
    public pxcmStatus EnableContourExtraction(Boolean enableFlag)
    {
        return PXCMBlobConfiguration_EnableContourExtraction(instance, enableFlag);
    }

    /// <summary>
    /// Return the contour extraction flag.
    /// </summary>
    /// <returns> The contour extraction flag.</returns>
    /// <see cref="EnableContourExtraction"/>
    public Boolean IsContourExtractionEnabled()
    {
        return PXCMBlobConfiguration_IsContourExtractionEnabled(instance);
    }

    /// <summary> 
    /// Set the minimal contour size in points.
    /// Objects with external contours that are smaller than the limit are not identified as blobs.
    /// </summary>
    /// <param name="minContourSize"> - the minimal contour size in points.</param>
    /// <returns> PXCM_STATUS_NO_ERROR - successful operation.</returns>
    /// <returns> PXCM_STATUS_PARAM_UNSUPPORTED - invalid minContourSize value (in this case, the last valid value will be retained).</returns>
    /// <see cref="QueryMinContourSize"/>
    public pxcmStatus SetMinContourSize(Int32 minContourSize)
    {
        return PXCMBlobConfiguration_SetMinContourSize(instance, minContourSize);
    }

    /// <summary> 
    /// Get the minimal contour size in points.
    /// </summary>
    /// <returns> The minimal contour size in points.</returns>
    /// <see cref="SetMinContourSize"/>
    public Int32 QueryMinContourSize()
    {
        return PXCMBlobConfiguration_QueryMinContourSize(instance);
    }

    /// <summary>
    /// Enable or disable the stabilization feature.\n
    /// 
    /// Enabling this feature produces smoother tracking of the extremity points, ignoring small shifts and "jitters".\n
    /// @Note: in some cases the tracking may be less sensitive to minor movements and some blob pixels may be outside of the extremities. 
    /// </summary>
    /// <param name="enableFlag"> - true to enable the stabilization; false to disable it.</param>
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded. </returns>
    public pxcmStatus EnableStabilizer(Boolean enableFlag)
    {
        return PXCMBlobConfiguration_EnableStabilizer(instance, enableFlag);
    }

    /// <summary>
    /// Return blob stabilizer activation status.
    /// </summary>
    /// <returns> true if blob stabilizer is enabled, false otherwise.</returns>
    public Boolean IsStabilizerEnabled()
    {
        return PXCMBlobConfiguration_IsStabilizerEnabled(instance);
    }

    /// <summary> 
    /// Set the maximal blob size in pixels.
    /// Only objects that are smaller than this size are identified as blobs.
    /// </summary>
    /// <param name="maxBlobSize"> - the maximal blob size in pixels.</param>
    /// <returns> PXCM_STATUS_NO_ERROR - successful operation.</returns>
    /// <returns> PXCM_STATUS_PARAM_UNSUPPORTED - invalid maxBlobSize value (in this case, the last valid value will be retained).</returns>
    /// <see cref="QueryMaxPixelCount"/>
    public pxcmStatus SetMaxPixelCount(Int32 maxBlobSize)
    {
        return PXCMBlobConfiguration_SetMaxPixelCount(instance, maxBlobSize);
    }

    /// <summary> 
    /// Get the maximal blob size in pixels.
    /// </summary>
    /// <returns> The maximal blob size in pixels.</returns>
    /// <see cref="SetMinPixelCount"/>
    public Int32 QueryMaxPixelCount()
    {
        return PXCMBlobConfiguration_QueryMaxPixelCount(instance);
    }

    /// <summary> 
    /// Set the maximal blob area in square meter.
    /// Only objects that are smaller than this area are identified as blobs.
    /// </summary>
    /// <param name="maxBlobArea"> - the maximal blob area in square meter.</param>
    /// <returns> PXCM_STATUS_NO_ERROR - successful operation.</returns>
    /// <returns> PXCM_STATUS_PARAM_UNSUPPORTED - invalid maxBlobArea value (in this case, the last valid value will be retained).</returns>
    /// <see cref="QueryMaxBlobArea"/>
    public pxcmStatus SetMaxBlobArea(Single maxBlobArea)
    {
        return PXCMBlobConfiguration_SetMaxBlobArea(instance, maxBlobArea);
    }

    /// <summary> 
    /// Get the maximal blob area in square meter.
    /// </summary>
    /// <returns> The maximal blob area in square meter.</returns>
    /// <see cref="SetMaxBlobArea"/>
    public Single QueryMaxBlobArea()
    {
        return PXCMBlobConfiguration_QueryMaxBlobArea(instance);
    }

    /// <summary> 
    /// Set the minimal blob area in square meter.
    /// Only objects that are larger than this area are identified as blobs.
    /// </summary>
    /// <param name="minBlobArea"> - the minimal blob area in square meter.</param>
    /// <returns> PXCM_STATUS_NO_ERROR - successful operation.</returns>
    /// <returns> PXCM_STATUS_PARAM_UNSUPPORTED - invalid minBlobArea value (in this case, the last valid value will be retained).</returns>
    /// <see cref="QueryMinBlobArea"/>
    public pxcmStatus SetMinBlobArea(Single minBlobArea)
    {
        return PXCMBlobConfiguration_SetMinBlobArea(instance, minBlobArea);
    }

    /// <summary> 
    /// Get the minimal blob area in square meter.
    /// </summary>
    /// <returns> The minimal blob area in square meter.</returns>
    /// <see cref="SetMinBlobArea"/>
    public Single QueryMinBlobArea()
    {
        return PXCMBlobConfiguration_QueryMinBlobArea(instance);
    }


    /// <summary>
    /// Set the smoothing strength for the blob extraction.
    /// </summary>
    /// <param name="smoothingValue"> - a value between 0 (no smoothing) to 1 (strong smoothing).</param>
    /// <returns> PXCM_STATUS_NO_ERROR - successful operation.</returns>
    /// <returns> PXCM_STATUS_PARAM_UNSUPPORTED - invalid smoothing value (in this case, the last valid value will be retained).</returns>
    /// <see cref="QueryBlobSmoothing"/>
    public pxcmStatus SetBlobSmoothing(Single smoothingValue)
    {
        return PXCMBlobConfiguration_SetBlobSmoothing(instance, smoothingValue);
    }

    /// <summary>
    /// Get the segmentation blob smoothing value.
    /// </summary>
    /// <returns> The segmentation image smoothing value.</returns>
    /// <see cref="SetBlobSmoothing"/>
    public Single QueryBlobSmoothing()
    {
        return PXCMBlobConfiguration_QueryBlobSmoothing(instance);
    }


    /// <summary>
    /// Enable blob data extraction correlated to color stream.
    /// </summary>
    /// <param name="enableFlag"> - set to true if color mapping should be extracted; otherwise set to false.</param>
    /// <returns> PXCM_STATUS_NO_ERROR - successful operation.</returns>
    /// <see cref="IsColorMappingEnabled"/>
    /// <see cref="QueryBlob"/>
    /// <see cref="SegmentationImageType"/>		
    public pxcmStatus EnableColorMapping(Boolean enableFlag)
    {
        return PXCMBlobConfiguration_EnableColorMapping(instance, enableFlag);
    }

    /// <summary>
    /// Return the color mapping extraction flag.
    /// </summary>
    /// <returns> The color mapping extraction flag.</returns>
    /// <see cref="EnableColorMapping"/>
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
