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

/// <summary> 
/// @class PXCMContourExtractor
/// A utility for extracting contour lines from blob (mask) images.
/// Given a mask image, in which the blob pixels are white (255) and the rest are black (0)
/// this utility will extract the contour lines of the blob.
/// The contour lines are all the lines that define the borders of the blob.
/// Inner contour lines (i.e. "holes" in the blob) are defined by an array of clock-wise points.
/// The outer contour line (i.e. the external border) is defined by an array of counter-clock-wise points.
/// </summary>
public partial class PXCMContourExtractor : PXCMBase
{
    new public const Int32 CUID = unchecked((Int32)0x98fc2453);

    /// <summary>
    /// initialize PXCMContourExtractor instance for a specific image type (size)
    /// </summary>
    /// <param name="imageInfo"> definition of the images that should be processed</param>
    /// @see PXCMImage.ImageInfo
    public void Init(PXCMImage.ImageInfo imageInfo)
    {
        PXCMContourExtractor_Init(instance, imageInfo);
    }

    /// <summary>			
    /// Extract the contour of the blob in the given image
    /// Given an image of a blob, in which object pixels are white (set to 255) and all other pixels are black (set to 0),
    /// extract the contour lines of the blob.
    /// Note that there might be multiple contour lines, if the blob contains "holes".
    /// </summary>
    /// <param name="blobImage"> the blob-image to be processed</param>
    /// <returns> PXCM_STATUS_NO_ERROR if a valid blob image exists and could be processed; otherwise, return the following error:
    /// PXCM_STATUS_DATA_UNAVAILABLE - if image is not available or PXCM_STATUS_ITEM_UNAVAILABLE if processImage is running or PXCMContourExtractor was not initialized.	</returns>	
    public pxcmStatus ProcessImage(PXCMImage depthImage)
    {
        return PXCMContourExtractor_ProcessImage(instance, depthImage.instance);
    }

    /// <summary> 
    /// Get the data of the contour line
    /// A contour is composed of a line, an array of 2D points describing the contour path
    /// </summary>
    /// <param name="index"> the zero-based index of the requested contour</param>
    /// <param name="maxSize"> size of the allocated array for the contour points</param>
    /// <param name="contour"> points stored in the user allocated array</param>
    /// <returns> PXCM_STATUS_NO_ERROR if terminated successfully; otherwise, return the following error:
    /// PXCM_STATUS_PARAM_UNSUPPORTED if index is invalid or user allocated array is invalid 		
    /// PXCM__STATUS_ITEM_UNAVAILABLE if processImage is running or PXCMContourExtractor was not initialized.</returns>
    public pxcmStatus QueryContourData(Int32 index, out PXCMPointI32[] contour)
    {
        return QueryContourDataINT(instance, index, out contour);
    }

    /// <summary> 
    /// Get the contour size (number of points in the contour)
    /// This is the size of the points array that the user should allocate
    /// </summary>
    /// <returns> the contour size (number of points in the contour)</returns>
    /// <param name="index"> the zero-based index of the requested contour</param>
    public Int32 QueryContourSize(Int32 index)
    {
        return PXCMContourExtractor_QueryContourSize(instance, index);
    }

    /// <summary> 
    /// Return true if it is the blob's outer contour, false for internal contours.
    /// </summary>
    /// <returns> true if it is the blob's outer contour, false for internal contours.</returns>
    /// <param name="index"> the zero-based index of the requested contour</param>
    public Boolean IsContourOuter(Int32 index)
    {
        return PXCMContourExtractor_IsContourOuter(instance, index);
    }

    /// <summary> 
    /// Get the number of contours extracted
    /// </summary>
    /// <returns> the number of contours extracted</returns>
    public Int32 QueryNumberOfContours()
    {
        return PXCMContourExtractor_QueryNumberOfContours(instance);
    }

    /// <summary> 
    /// Set the smoothing level of the shape of the contour 
    /// The smoothing level ranges from 0 to 1, when 0 means no smoothing, and 1 implies a very smooth contour
    /// Note that a larger smoothing level will reduce the number of points, while "cleaning" small holes in the line
    /// </summary>
    /// <param name="smoothing"> the smoothing level</param>
    /// <returns> PXCM_STATUS_NO_ERROR if smoothing is valid; otherwise, return the following error:
    /// PXCM_STATUS_PARAM_UNSUPPORTED, smoothing level will remain the last valid value</returns>
    public pxcmStatus SetSmoothing(Single smoothing)
    {
        return PXCMContourExtractor_SetSmoothing(instance, smoothing);
    }

    /// <summary> 
    /// Get the smoothing level of the contour (0-1) 
    /// </summary>
    /// <returns> smoothing level of the contour</returns>
    public Single QuerySmoothing()
    {
        return PXCMContourExtractor_QuerySmoothing(instance);
    }

    /* constructors and misc */
    internal PXCMContourExtractor(IntPtr instance, Boolean delete)
        : base(instance, delete)
    {
    }
};

#if RSSDK_IN_NAMESPACE
}
#endif
