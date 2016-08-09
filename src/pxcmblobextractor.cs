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
/// @class PXCMBlobExtractor
/// A utility for extracting mask images (blobs) of objects in front of the camera.
/// Given a depth image, this utility will find the largest objects that are "close enough" to the camera.
/// For each object a segmentation mask image will be created, that is, all object pixels will be "white" (255) 
/// and all other pixels will be "black" (0).
/// The maximal number of blobs that can be extracted is 2.
/// The order of the blobs will be from the largest to the smallest (in number of pixels)
/// </summary>
[Obsolete("Not used anymore. Use PXCMBlob module instead")]
public partial class PXCMBlobExtractor : PXCMBase
{
    new public const Int32 CUID = unchecked((Int32)0xa52305bc);

    /// <summary> 
    /// BlobData
    /// Contains the parameters that define a blob
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class BlobData
    {
        public PXCMPointI32 closestPoint;		/// Image coordinates of the closest point in the blob
        public PXCMPointI32 leftPoint;			/// Image coordinates of the left-most point in the blob
        public PXCMPointI32 rightPoint;			/// Image coordinates of the right-most point in the blob
        public PXCMPointI32 topPoint;			/// Image coordinates of the top point in the blob
        public PXCMPointI32 bottomPoint;		/// Image coordinates of the bottom point in the blob
        public PXCMPointF32 centerPoint;		/// Image coordinates of the center of the blob
        public Int32 pixelCount;		    /// The number of pixels in the blob
    };

    /// <summary>
    /// initialize PXCMBlobExtractor instance for a specific image type (size)
    /// </summary>
    /// <param name="imageInfo"> definition of the images that should be processed</param>
    /// <see cref="PXCMImage.ImageInfo"/> 
    public void Init(PXCMImage.ImageInfo imageInfo)
    {
        PXCMBlobExtractor_Init(instance, imageInfo);
    }


    /// <summary>			
    /// Extract the 2D image mask of the blob in front of the camera. 	 
    /// In the image mask, each pixel occupied by the blob's is white (set to 255) and all other pixels are black (set to 0).
    /// </summary>
    /// <param name="depthImage"> the depth image to be segmented</param>
    /// <returns> PXC_STATUS_NO_ERROR if a valid depth exists and could be segmented; otherwise, return the following error:
    /// PXCM_STATUS_DATA_UNAVAILABLE - if image is not available or PXCM_STATUS_ITEM_UNAVAILABLE if processImage is running or PXCMBlobExtractor was not initialized.	</returns>	
    public pxcmStatus ProcessImage(PXCMImage depthImage)
    {
        return PXCMBlobExtractor_ProcessImage(instance, depthImage.instance);
    }

    /// <summary>
    /// Retrieve the 2D image mask of the blob and its associated blob data
    /// The blobs are ordered from the largest to the smallest (in number of pixels)
    /// </summary>
    /// <see cref="BlobData"/> 
    /// <param name="index"> the zero-based index of the requested blob (has to be between 0 to number of blobs)</param>
    /// <param name="segmentationImage"> the 2D image mask of the requested blob</param>
    /// <param name="blobData"> the data of the requested blob</param>
    /// <returns> PXCM_STATUS_NO_ERROR if index is valid and processImage terminated successfully; otherwise, return the following error:
    /// PXCM_STATUS_PARAM_UNSUPPORTED if index is invalid or PXCM_STATUS_ITEM_UNAVAILABLE if processImage is running or PXCMBlobExtractor was not initialized.</returns>
 
    public pxcmStatus QueryBlobData(Int32 index, PXCMImage segmentationImage, out BlobData blobData)
    {
        return QueryBlobDataINT(instance, index, segmentationImage.instance, out blobData);
    }

    /// <summary> 
    /// Set the maximal number of blobs that can be detected 
    /// The default number of blobs that will be detected is 1
    /// </summary>
    /// <param name="maxBlobs"> the maximal number of blobs that can be detected (limited to 2)</param>
    /// <returns> PXCM_STATUS_NO_ERROR if maxBlobs is valid; otherwise, return the following error:
    /// PXCM_STATUS_PARAM_UNSUPPORTED, maxBlobs will remain the last valid value</returns>
    public pxcmStatus SetMaxBlobs(Int32 maxBlobs)
    {
        return PXCMBlobExtractor_SetMaxBlobs(instance, maxBlobs);
    }

    /// <summary>
    /// Get the maximal number of blobs that can be detected 	
    /// </summary>
    /// <returns> the maximal number of blobs that can be detected 	</returns>
    
    public Int32 QueryMaxBlobs()
    {
        return PXCMBlobExtractor_QueryMaxBlobs(instance);
    }
    /// <summary> 
    /// Get the number of detected blobs  	
    /// </summary>
    /// <returns> the number of detected blobs  	</returns>
    public Int32 QueryNumberOfBlobs()
    {
        return PXCMBlobExtractor_QueryNumberOfBlobs(instance);
    }

    /// <summary> 
    /// Set the maximal distance limit from the camera. 
    /// Blobs will be objects that appear between the camera and the maxDistance limit.
    /// </summary>
    /// <param name="maxDistance"> the maximal distance from the camera (has to be a positive value)</param>
    /// <returns> PXCM_STATUS_NO_ERROR if maxDistance is valid; otherwise, return the following error:
    /// PXCM_STATUS_PARAM_UNSUPPORTED, maxDistance will remain the last valid value</returns>
    public pxcmStatus SetMaxDistance(Single maxDistance)
    {
        return PXCMBlobExtractor_SetMaxDistance(instance, maxDistance);
    }

    /// <summary> 
    /// Get the maximal distance from the camera, in which an object can be detected and segmented
    /// </summary>
    /// <returns> maximal distance from the camera</returns>
    public Single QueryMaxDistance()
    {
        return PXCMBlobExtractor_QueryMaxDistance(instance);
    }

    /// <summary>
    /// Set the maximal depth of a blob (maximal distance between closest and furthest points on blob)
    /// </summary>
    /// <param name="maxDepth"> the maximal depth of the blob (has to be a positive value)</param>
    /// <returns> PXCM_STATUS_NO_ERROR if maxDepth is valid; otherwise, return the following error:
    /// PXCM_STATUS_PARAM_UNSUPPORTED, maxDepth will remain the last valid value</returns>
    public pxcmStatus SetMaxObjectDepth(Single maxDepth)
    {
        return PXCMBlobExtractor_SetMaxObjectDepth(instance, maxDepth);
    }

    /// <summary> 
    /// Get the maximal depth of the blob that can be detected and segmented
    /// </summary>
    /// <returns> maximal depth of the blob</returns>
    public Single QueryMaxObjectDepth()
    {
        return PXCMBlobExtractor_QueryMaxObjectDepth(instance);
    }

    /// <summary> 
    /// Set the smoothing level of the shape of the blob
    /// The smoothing level ranges from 0 to 1, when 0 means no smoothing, and 1 implies a very smooth contour
    /// </summary>
    /// <param name="smoothing"> the smoothing level</param>
    /// <returns> PXCM_STATUS_NO_ERROR if smoothing is valid; otherwise, return the following error:
    /// PXCM_STATUS_PARAM_UNSUPPORTED, smoothing level will remain the last valid value</returns>
    public pxcmStatus SetSmoothing(Single smoothing)
    {
        return PXCMBlobExtractor_SetSmoothing(instance, smoothing);
    }

    /// <summary> 
    /// Get the smoothing level of the blob (0-1) 
    /// </summary>
    /// <returns> smoothing level of the blob</returns>
    public Single QuerySmoothing()
    {
        return PXCMBlobExtractor_QuerySmoothing(instance);
    }

    /// <summary> 
    /// Set the minimal blob size in pixels
    /// Any blob that is smaller than threshold will be cleared during "ProcessImage".
    /// </summary>
    /// <param name="minBlobSize"> the minimal blob size in pixels (cannot be more than a quarter of image-size)</param>
    /// <returns> PXCM_STATUS_NO_ERROR if minBlobSize is valid; otherwise, return the following error:
    /// PXCM_STATUS_PARAM_UNSUPPORTED, minimal blob size will remain the last valid size</returns>
    public pxcmStatus SetClearMinBlobSize(Single minBlobSize)
    {
        return PXCMBlobExtractor_SetClearMinBlobSize(instance, minBlobSize);
    }

    /// <summary> 
    /// Get the minimal blob size in pixels
    /// </summary>
    /// <returns> minimal blob size</returns>
    public Single QueryClearMinBlobSize()
    {
        return PXCMBlobExtractor_QueryClearMinBlobSize(instance);
    }



    /* constructors and misc */
    internal PXCMBlobExtractor(IntPtr instance, Boolean delete)
        : base(instance, delete)
    {
    }
};

#if RSSDK_IN_NAMESPACE
}
#endif

