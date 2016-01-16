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
	@Class PXCMBlobData 
	@brief A class that contains extracted blob and contour line data.
	The extracted data refers to the sensor's frame image at the time PXCBlobModule.CreateOutput() was called.
*/
    public partial class PXCMBlobData : PXCMBase
    {
        new public const Int32 CUID = 0x54444d42;

        public const Int32 NUMBER_OF_EXTREMITIES = 6;

        /* Enumerations */

        /* Enumerations */

        /** 
        @enum AccessOrderType
        @brief Each AccessOrderType value indicates the order in which the extracted blobs can be accessed.
        Use one of these values when calling QueryBlobByAccessOrder().
        */
        public enum AccessOrderType
        {
            ACCESS_ORDER_NEAR_TO_FAR = 0,	/// From the nearest to the farthest blob in the scene  
            ACCESS_ORDER_LARGE_TO_SMALL,    /// From the largest to the smallest blob in the scene   				
            ACCESS_ORDER_RIGHT_TO_LEFT      /// From the right-most to the left-most blob in the scene   		
        };

        /**
            @enum ExtremityType
            @brief The identifier of an extremity point of the extracted blob.
            6 extremity points are identified (see values below).\n
            Use one of the extremity types when calling IBlob.QueryExtremityPoint().
        */
        public enum ExtremityType
        {
            EXTREMITY_CLOSEST = 0,  /// The closest point to the sensor in the tracked blob
            EXTREMITY_LEFT_MOST,	/// The left-most point of the tracked blob
            EXTREMITY_RIGHT_MOST,	/// The right-most point of the tracked blob 
            EXTREMITY_TOP_MOST,		/// The top-most point of the tracked blob
            EXTREMITY_BOTTOM_MOST,	/// The bottom-most point of the tracked blob
            EXTREMITY_CENTER		/// The center point of the tracked blob			
        };

        /**
         @enum SegmentationImageType
         @brief Each SegmentationImageType value indicates the extracted blobs data mapping.
         Use one of these values when calling QueryBlob().
        */
        public enum SegmentationImageType
        {
            SEGMENTATION_IMAGE_DEPTH = 0, /// The blob data mapped to depth image
            SEGMENTATION_IMAGE_COLOR      /// The blob data mapped to color image   
        };


        /* Interfaces */
        /** 
            @class IContour
            @brief An interface that provides access to the contour line data.
            A contour is represented by an array of 2D points, which are the vertices of the contour's polygon.
        */
        public partial class IContour
        {

           
            /** 
              @brief Get the point array representing a contour line.
                
              @param[in] maxSize - the size of the array allocated for the contour points.
              @param[out] contour - the contour points stored in the user-allocated array.
        
              @return PXCM_STATUS_NO_ERROR - successful operation.        
              */
            public pxcmStatus QueryPoints(out PXCMPointI32[] contour)
            {
                return QueryDataINT(instance, out contour);
            }


            /** 
                @brief Return true for the blob's outer contour; false for inner contours.
                @return true for the blob's outer contour; false for inner contours.
            */
            public Boolean IsOuter()
            {
                return PXCMBlobData_IContour_IsOuter(instance);
            }

            /** 
               @brief Get the contour size (number of points in the contour line).
               This is the size of the points array that you should allocate.
               @return The contour size (number of points in the contour line).
           */
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





        /** 
            @class IBlob
            @brief An interface that provides access to the blob and contour line data.
        */
        public partial class IBlob
        {

            /**			
                @brief Retrieves the 2D segmentation image of a tracked blob. 	 
                In the segmentation image, each pixel occupied by the blob is white (value of 255) and all other pixels are black (value of 0).
                @param[out] image - the segmentation image of the tracked blob.
                @return PXCM_STATUS_NO_ERROR - successful operation.
                @return PXCM_STATUS_DATA_UNAVAILABLE - the segmentation image is not available.		
        */
            public pxcmStatus QuerySegmentationImage(out PXCMImage image)
            {
                IntPtr image2;
                pxcmStatus sts = PXCMBlobData_IBlob_QuerySegmentationImage(instance, out image2);
                image = (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR) ? new PXCMImage(image2, false) : null;
                return sts;
            }



            /**         
                @brief Get an extremity point location using a specific ExtremityType.
                @param[in] extremityLabel - the extremity type to be retrieved.
                @return The extremity point location data.        
                @see ExtremityType       
            */
            public PXCMPoint3DF32 QueryExtremityPoint(ExtremityType extremityLabel)
            {
                return QueryExtremityPointINT(instance, extremityLabel);
            }

           

            /** 
                    @brief Get the number of pixels in the blob.
                    @return The number of pixels in the blob.
            */
            public Int32 QueryPixelCount()
            {
                return PXCMBlobData_IBlob_QueryPixelCount(instance);
            }

           

            /** 
                @brief Get the number of contour lines extracted (both external and internal).
                @return The number of contour lines extracted.
            */
            public Int32 QueryNumberOfContours()
            {
                return PXCMBlobData_IBlob_QueryNumberOfContours(instance);
            }



            /** 
                 @brief Get the point array representing a contour line.
		
                 A contour is represented by an array of 2D points, which are the vertices of the contour's polygon.
		
                 @param[in] index - the zero-based index of the requested contour line (the maximal value is QueryNumberOfContours()-1).
                 @param[in] maxSize - the size of the array allocated for the contour points.
                 @param[out] contour - the contour points stored in the user-allocated array.
		
                 @return PXCM_STATUS_NO_ERROR - successful operation.
                 @return PXCM_STATUS_PARAM_UNSUPPORTED - the given index or the user-allocated array is invalid.
                 @return PXCM_STATUS_ITEM_UNAVAILABLE - processImage() is running.
             */

            public pxcmStatus QueryContourPoints(Int32 index, out PXCMPointI32[] contour)
            {
                return QueryContourDataINT(instance, index, out contour);
            }

           
            /** 
             @brief Return true for the blob's outer contour; false for inner contours.
             @param[in] index - the zero-based index of the requested contour.
             @return true for the blob's outer contour; false for inner contours.
         */
            public Boolean IsContourOuter(Int32 index)
            {
                return PXCMBlobData_IBlob_IsContourOuter(instance, index);
            }

            /** 
            @brief Get the contour size (number of points in the contour line).
            This is the size of the points array that you should allocate.
            @param[in] index - the zero-based index of the requested contour line.
            @return The contour size (number of points in the contour line).
        */
            public Int32 QueryContourSize(Int32 index)
            {
                return PXCMBlobData_IBlob_QueryContourSize(instance, index);
            }

            /**
                 @brief Retrieve an IContour object using index (that relates to the given order).
                 @param[in] index - the zero-based index of the requested contour (between 0 and QueryNumberOfContours()-1 ).
                 @param[out] contourData - contains the extracted contour line data.
        
                 @return PXCM_STATUS_NO_ERROR - successful operation.
                 @return PXCM_STATUS_DATA_UNAVAILABLE  - index >= number of detected contours. 

                 @see IContour       
             */
            public pxcmStatus QueryContour(Int32 index, out IContour contourData)
            {
                IntPtr cd2;
                pxcmStatus sts = PXCMBlobData_IBlob_QueryContour(instance, index, out cd2);
                contourData = (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR) ? new IContour(cd2) : null;
                return sts;
            }

            /**	
             @brief Return the location and dimensions of the blob, represented by a 2D bounding box (defined in pixels).
             @return The location and dimensions of the 2D bounding box.
           */
            public PXCMRectI32 QueryBoundingBoxImage()
            {
                PXCMRectI32 rect = new PXCMRectI32();
                PXCMBlobData_IBlob_QueryBoundingBoxImage(instance, ref rect);
                return rect;
            }
            /**	
                @brief Return the location and dimensions of the blob, represented by a 3D bounding box.
                @return The location and dimensions of the 3D bounding box.
            */
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

        /**
          @brief Updates the extracted blob data to the latest available output. 

          @return PXCM_STATUS_NO_ERROR - successful operation.
          @return PXCM_STATUS_DATA_NOT_INITIALIZED  - when the BlobData is not available. 
       */
        public pxcmStatus Update()
        {
            return PXCMBlobData_Update(instance);
        }

        /* Blob module's Outputs */

        /** 
           @brief Get the number of extracted blobs.    
           @return The number of extracted blobs.
         */
        public Int32 QueryNumberOfBlobs()
        {
            return PXCMBlobData_QueryNumberOfBlobs(instance);
        }

        /**
             @brief Retrieve an IBlob object using a specific AccessOrder and index (that relates to the given order).
             @param[in] index - the zero-based index of the requested blob (between 0 and QueryNumberOfBlobs()-1 ).
             @param[in] accessOrder - the order in which the blobs are enumerated.
             @param[out] blobData - contains the extracted blob and contour line data.
		
             @return PXCM_STATUS_NO_ERROR - successful operation.
             @return PXCM_STATUS_PARAM_UNSUPPORTED - index >= configured maxBlobs.
             @return PXCM_STATUS_DATA_UNAVAILABLE  - index >= number of detected blobs.  
		
             @see AccessOrderType
         */
        public pxcmStatus QueryBlobByAccessOrder(Int32 index, AccessOrderType accessOrderType, out IBlob blobData)
        {
            IntPtr bd2;
            pxcmStatus sts = PXCMBlobData_QueryBlobByAccessOrder(instance, index, accessOrderType, out bd2);
            blobData = (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR) ? new IBlob(bd2) : null;
            return sts;
        }

        /**
           @brief Retrieve an IBlob object using a specific AccessOrder and index (that relates to the given order).
           @param[in] index - the zero-based index of the requested blob (between 0 and QueryNumberOfBlobs()-1 ).
           @param[in] segmentationImageType - the image type which the blob will be mapped to. To get data mapped to color see PXCBlobConfiguration::EnableColorMapping.
           @param[in] accessOrder - the order in which the blobs are enumerated.
           @param[out] blobData - contains the extracted blob data.
        
           @return PXCM_STATUS_NO_ERROR - successful operation.
           @return PXCM_STATUS_PARAM_UNSUPPORTED - index >= configured maxBlobs.
           @return PXCM_STATUS_DATA_UNAVAILABLE  - index >= number of detected blobs or blob data is invalid.  
        
           @see AccessOrderType
           @see SegmentationImageType
       */
        public pxcmStatus QueryBlob(Int32 index,SegmentationImageType segmentationImageType, AccessOrderType accessOrderType, out IBlob blobData)
        {
            IntPtr bd2;
            pxcmStatus sts = PXCMBlobData_QueryBlob(instance, index,segmentationImageType, accessOrderType, out bd2);
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
