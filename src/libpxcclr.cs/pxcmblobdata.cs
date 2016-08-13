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
/// @Class PXCMBlobData 
/// A class that contains extracted blob and contour line data.
/// The extracted data refers to the sensor's frame image at the time PXCBlobModule.CreateOutput() was called.
/// </summary>
public partial class PXCMBlobData : PXCMBase
{
    new public const Int32 CUID = 0x54444d42;

    public const Int32 NUMBER_OF_EXTREMITIES = 6;

    /* Enumerations */

    /* Enumerations */

    /// <summary> 
    /// AccessOrderType
    /// Each AccessOrderType value indicates the order in which the extracted blobs can be accessed.
    /// Use one of these values when calling QueryBlobByAccessOrder().
    /// </summary>
    public enum AccessOrderType
    {
        ACCESS_ORDER_NEAR_TO_FAR = 0,	/// From the nearest to the farthest blob in the scene  
        ACCESS_ORDER_LARGE_TO_SMALL,    /// From the largest to the smallest blob in the scene   				
        ACCESS_ORDER_RIGHT_TO_LEFT      /// From the right-most to the left-most blob in the scene   		
    };

    /// <summary>
    /// ExtremityType
    /// The identifier of an extremity point of the extracted blob.
    /// 6 extremity points are identified (see values below).\n
    /// Use one of the extremity types when calling IBlob.QueryExtremityPoint().
    /// </summary>
    public enum ExtremityType
    {
        EXTREMITY_CLOSEST = 0,  /// The closest point to the sensor in the tracked blob
        EXTREMITY_LEFT_MOST,	/// The left-most point of the tracked blob
        EXTREMITY_RIGHT_MOST,	/// The right-most point of the tracked blob 
        EXTREMITY_TOP_MOST,		/// The top-most point of the tracked blob
        EXTREMITY_BOTTOM_MOST,	/// The bottom-most point of the tracked blob
        EXTREMITY_CENTER		/// The center point of the tracked blob			
    };

    /// <summary>
    /// SegmentationImageType
    /// Each SegmentationImageType value indicates the extracted blobs data mapping.
    /// Use one of these values when calling QueryBlob().
    /// </summary>
    public enum SegmentationImageType
    {
        SEGMENTATION_IMAGE_DEPTH = 0, /// The blob data mapped to depth image
        SEGMENTATION_IMAGE_COLOR      /// The blob data mapped to color image   
    };


    /* Interfaces */
    /// <summary> 
    /// @class IContour
    /// An interface that provides access to the contour line data.
    /// A contour is represented by an array of 2D points, which are the vertices of the contour's polygon.
    /// </summary>
    public partial class IContour
    {


        /// <summary> 
        /// Get the point array representing a contour line.
        /// </summary>
        /// 
        /// <param name="maxSize"> - the size of the array allocated for the contour points.</param>
        /// <param name="contour"> - the contour points stored in the user-allocated array.</param>
        /// 
        /// <returns> PXCM_STATUS_NO_ERROR - successful operation.        </returns>
        public pxcmStatus QueryPoints(out PXCMPointI32[] contour)
        {
            return QueryDataINT(instance, out contour);
        }


        /// <summary> 
        /// Return true for the blob's outer contour; false for inner contours.
        /// </summary>
        /// <returns> true for the blob's outer contour; false for inner contours.</returns>
        public Boolean IsOuter()
        {
            return PXCMBlobData_IContour_IsOuter(instance);
        }

        /// <summary> 
        /// Get the contour size (number of points in the contour line).
        /// This is the size of the points array that you should allocate.
        /// </summary>
        /// <returns> The contour size (number of points in the contour line).</returns>
        public Int32 QuerySize()
        {
            return PXCMBlobData_IContour_QuerySize(instance);
        }



        private IntPtr instance;
        internal IContour(IntPtr instance)
        {
            this.instance = instance;
        }
    };





    /// <summary> 
    /// @class IBlob
    /// An interface that provides access to the blob and contour line data.
    /// </summary>
    public partial class IBlob
    {

        /// <summary>			
        /// Retrieves the 2D segmentation image of a tracked blob. 	 
        /// In the segmentation image, each pixel occupied by the blob is white (value of 255) and all other pixels are black (value of 0).
        /// </summary>
        /// <param name="image"> - the segmentation image of the tracked blob.</param>
        /// <returns> PXCM_STATUS_NO_ERROR - successful operation.</returns>
        /// <returns> PXCM_STATUS_DATA_UNAVAILABLE - the segmentation image is not available.		</returns>
        public pxcmStatus QuerySegmentationImage(out PXCMImage image)
        {
            IntPtr image2;
            pxcmStatus sts = PXCMBlobData_IBlob_QuerySegmentationImage(instance, out image2);
            image = (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR) ? new PXCMImage(image2, false) : null;
            return sts;
        }



        /// <summary>         
        /// Get an extremity point location using a specific ExtremityType.
        /// </summary>
        /// <param name="extremityLabel"> - the extremity type to be retrieved.</param>
        /// <returns> The extremity point location data.        </returns>
        /// <see cref="ExtremityType"/>       
        public PXCMPoint3DF32 QueryExtremityPoint(ExtremityType extremityLabel)
        {
            return QueryExtremityPointINT(instance, extremityLabel);
        }



        /// <summary> 
        /// Get the number of pixels in the blob.
        /// </summary>
        /// <returns> The number of pixels in the blob.</returns>
        public Int32 QueryPixelCount()
        {
            return PXCMBlobData_IBlob_QueryPixelCount(instance);
        }



        /// <summary> 
        /// Get the number of contour lines extracted (both external and internal).
        /// </summary>
        /// <returns> The number of contour lines extracted.</returns>
        public Int32 QueryNumberOfContours()
        {
            return PXCMBlobData_IBlob_QueryNumberOfContours(instance);
        }



        /// <summary> 
        /// Get the point array representing a contour line.
        /// 
        /// A contour is represented by an array of 2D points, which are the vertices of the contour's polygon.
        /// </summary>
        /// 
        /// <param name="index"> - the zero-based index of the requested contour line (the maximal value is QueryNumberOfContours()-1).</param>
        /// <param name="maxSize"> - the size of the array allocated for the contour points.</param>
        /// <param name="contour"> - the contour points stored in the user-allocated array.</param>
        /// 
        /// <returns> PXCM_STATUS_NO_ERROR - successful operation.</returns>
        /// <returns> PXCM_STATUS_PARAM_UNSUPPORTED - the given index or the user-allocated array is invalid.</returns>
        /// <returns> PXCM_STATUS_ITEM_UNAVAILABLE - processImage() is running.</returns>
        public pxcmStatus QueryContourPoints(Int32 index, out PXCMPointI32[] contour)
        {
            return QueryContourDataINT(instance, index, out contour);
        }


        /// <summary> 
        /// Return true for the blob's outer contour; false for inner contours.
        /// </summary>
        /// <param name="index"> - the zero-based index of the requested contour.</param>
        /// <returns> true for the blob's outer contour; false for inner contours.</returns>
        public Boolean IsContourOuter(Int32 index)
        {
            return PXCMBlobData_IBlob_IsContourOuter(instance, index);
        }

        /// <summary> 
        /// Get the contour size (number of points in the contour line).
        /// This is the size of the points array that you should allocate.
        /// </summary>
        /// <param name="index"> - the zero-based index of the requested contour line.</param>
        /// <returns> The contour size (number of points in the contour line).</returns>
        public Int32 QueryContourSize(Int32 index)
        {
            return PXCMBlobData_IBlob_QueryContourSize(instance, index);
        }

        /// <summary>
        /// Retrieve an IContour object using index (that relates to the given order).
        /// </summary>
        /// <param name="index"> - the zero-based index of the requested contour (between 0 and QueryNumberOfContours()-1 ).</param>
        /// <param name="contourData"> - contains the extracted contour line data.</param>
        /// 
        /// <returns> PXCM_STATUS_NO_ERROR - successful operation.</returns>
        /// <returns> PXCM_STATUS_DATA_UNAVAILABLE  - index >= number of detected contours. </returns>
        /// 
        /// <see cref="IContour"/>       
        public pxcmStatus QueryContour(Int32 index, out IContour contourData)
        {
            IntPtr cd2;
            pxcmStatus sts = PXCMBlobData_IBlob_QueryContour(instance, index, out cd2);
            contourData = (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR) ? new IContour(cd2) : null;
            return sts;
        }

        /// <summary>	
        /// Return the location and dimensions of the blob, represented by a 2D bounding box (defined in pixels).
        /// </summary>
        /// <returns> The location and dimensions of the 2D bounding box.</returns>
        public PXCMRectI32 QueryBoundingBoxImage()
        {
            PXCMRectI32 rect = new PXCMRectI32();
            PXCMBlobData_IBlob_QueryBoundingBoxImage(instance, ref rect);
            return rect;
        }

        /// <summary>	
        /// Return the location and dimensions of the blob, represented by a 3D bounding box.
        /// </summary>
        /// <returns> The location and dimensions of the 3D bounding box.</returns>
        public PXCMBox3DF32 QueryBoundingBoxWorld()
        {
            PXCMBox3DF32 rect = new PXCMBox3DF32();
            PXCMBlobData_IBlob_QueryBoundingBoxWorld(instance, ref rect);
            return rect;
        }

        public override int GetHashCode()
        {
            return instance.GetHashCode();
        }

        private IntPtr instance;
        internal IBlob(IntPtr instance)
        {
            this.instance = instance;
        }
    };

    /* General */

    /// <summary>
    /// Updates the extracted blob data to the latest available output. 
    /// </summary>
    /// 
    /// <returns> PXCM_STATUS_NO_ERROR - successful operation.</returns>
    /// <returns> PXCM_STATUS_DATA_NOT_INITIALIZED  - when the BlobData is not available. </returns>
    public pxcmStatus Update()
    {
        return PXCMBlobData_Update(instance);
    }

    /* Blob module's Outputs */

    /// <summary> 
    /// Get the number of extracted blobs.    
    /// </summary>
    /// <returns> The number of extracted blobs.</returns>
    public Int32 QueryNumberOfBlobs()
    {
        return PXCMBlobData_QueryNumberOfBlobs(instance);
    }

    /// <summary>
    /// Retrieve an IBlob object using a specific AccessOrder and index (that relates to the given order).
    /// </summary>
    /// <param name="index"> - the zero-based index of the requested blob (between 0 and QueryNumberOfBlobs()-1 ).</param>
    /// <param name="accessOrder"> - the order in which the blobs are enumerated.</param>
    /// <param name="blobData"> - contains the extracted blob and contour line data.</param>
    /// 
    /// <returns> PXCM_STATUS_NO_ERROR - successful operation.</returns>
    /// <returns> PXCM_STATUS_PARAM_UNSUPPORTED - index >= configured maxBlobs.</returns>
    /// <returns> PXCM_STATUS_DATA_UNAVAILABLE  - index >= number of detected blobs.  </returns>
    /// 
    /// <see cref="AccessOrderType"/>
    public pxcmStatus QueryBlobByAccessOrder(Int32 index, AccessOrderType accessOrderType, out IBlob blobData)
    {
        IntPtr bd2;
        pxcmStatus sts = PXCMBlobData_QueryBlobByAccessOrder(instance, index, accessOrderType, out bd2);
        blobData = (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR) ? new IBlob(bd2) : null;
        return sts;
    }

    /// <summary>
    /// Retrieve an IBlob object using a specific AccessOrder and index (that relates to the given order).
    /// </summary>
    /// <param name="index"> - the zero-based index of the requested blob (between 0 and QueryNumberOfBlobs()-1 ).</param>
    /// <param name="segmentationImageType"> - the image type which the blob will be mapped to. To get data mapped to color see PXCBlobConfiguration::EnableColorMapping.</param>
    /// <param name="accessOrder"> - the order in which the blobs are enumerated.</param>
    /// <param name="blobData"> - contains the extracted blob data.</param>
    /// 
    /// <returns> PXCM_STATUS_NO_ERROR - successful operation.</returns>
    /// <returns> PXCM_STATUS_PARAM_UNSUPPORTED - index >= configured maxBlobs.</returns>
    /// <returns> PXCM_STATUS_DATA_UNAVAILABLE  - index >= number of detected blobs or blob data is invalid.  </returns>
    /// 
    /// <see cref="AccessOrderType"/>
    /// <see cref="SegmentationImageType"/>
    public pxcmStatus QueryBlob(Int32 index, SegmentationImageType segmentationImageType, AccessOrderType accessOrderType, out IBlob blobData)
    {
        IntPtr bd2;
        pxcmStatus sts = PXCMBlobData_QueryBlob(instance, index, segmentationImageType, accessOrderType, out bd2);
        blobData = (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR) ? new IBlob(bd2) : null;
        return sts;
    }

    internal PXCMBlobData(IntPtr instance, Boolean delete)
        : base(instance, delete)
    {
    }
};

#if RSSDK_IN_NAMESPACE
}
#endif
