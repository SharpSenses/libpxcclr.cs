/*******************************************************************************

  INTEL CORPORATION PROPRIETARY INFORMATION
  This software is supplied under the terms of a license agreement or nondisclosure
  agreement with Intel Corporation and may not be copied or disclosed except in
  accordance with the terms of that agreement
  Copyright(c) 2015-2016 Intel Corporation. All Rights Reserved.

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
/// @Class PXCMObjectRecognitionData 
/// A class that defines a standard data interface for object recognition algorithms
/// </summary>
public partial class PXCMObjectRecognitionData : PXCMBase
{
    new public const Int32 CUID = 0x444a424f;
    public const Int32 MAX_OJBECT_NAME_SIZE = 256;

    [StructLayout(LayoutKind.Sequential)]
    public class RecognizedObjectData
    {
        public Int32 label;
        public Single probability;
        public PXCMRectI32 roi;
        public PXCMPointF32 centerPos2D;
        public PXCMPoint3DF32 centerPos3D;
        public PXCMBox3DF32 boundingBox;
    };


    /// <summary>
    /// Updates object recognition data to the most current output.. 
    /// </summary>
    /// <returns> PXCM_STATUS_NO_ERROR - successful operation.</returns>
    /// <returns> PXCM_STATUS_DATA_NOT_INITIALIZED  - when the ObjectRecognitionData is not available. </returns>
    public pxcmStatus Update()
    {
        return PXCMObjectRecognitionbData_Update(instance);
    }

    /// <summary> 
    /// Get the number of recognized objects in the current frame.
    /// @note the number of recognized objects is between 0 and number of classes 
    /// @multiplied by number of ROIs.
    /// @The function returns the sum of all objects at all ROIs up to the 
    /// @specified threshold (by configuration class)
    /// </summary>
    /// <returns> the number of successfully recognized objects.</returns>
    public Int32 QueryNumberOfRecognizedObjects()
    {
        return PXCMObjectRecognitionbData_QueryNumberOfRecognizedObjects(instance);
    }


    /// <summary>            
    /// Return a PXCMImage instance of segmented image of the entire frame.
    /// </summary>
    /// <param name="roiIndex"> - the rectangle represents the ROI of the specified object index.</param>
    /// <param name="segmentedImage"> - the segmented image as output.</param>
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded.</returns>
    /// @see PXCMImage      
    public pxcmStatus QuerySegmentedImage(Int32 roiIndex, out PXCMImage image)
    {
        IntPtr image2;
        pxcmStatus sts = PXCMObjectRecognitionData_QuerySegmentedImage(instance, roiIndex, out image2);
        image = (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR) ? new PXCMImage(image2, false) : null;
        return sts;
    }

    /// <summary>
    /// Return object data of the specified index.
    /// @note the index is between 0 and QueryNumberOfRecognizedObjects() in the current frame.
    /// @see PXCMObjectRecognitionData.QueryNumberOfRecognizedObjects()    
    /// </summary>
    /// <param name="index"> - the index of recognition.</param>
    /// <param name="probIndex"> - the index of probabilty per the recognized object.</param>
    /// <param name="objectData"> - data structure filled with the data of the recognized object.</param>
    /// <returns> PXCM_STATUS_NO_ERROR - operation succeeded.</returns>
    /// <returns> PXCM_STATUS_PARAM_UNSUPPORTED</returns>
    public pxcmStatus QueryRecognizedObjectData(Int32 index, Int32 probIndex, out RecognizedObjectData objectData)
    {
        objectData = new RecognizedObjectData();
        return PXCMObjectRecognitionData_QueryRecognizedObjectData(instance, index, probIndex, objectData);
    }

    /// <summary> 
    /// Return object data of the specified index with the highest probability.
    /// @note the index is between 0 and QueryNumberOfRecognizedObjects() in the current frame.
    /// @see PXCObjectRecognitionData::QueryNumberOfRecognizedObjects() 
    /// </summary>
    /// <param name="index"> - the index of recognition.</param>
    /// <param name="objectData"> - data structure filled with the data of the recognized object.</param>
    /// <returns> PXC_STATUS_NO_ERROR - operation succeeded.</returns>
    /// <returns> PXC_STATUS_PARAM_UNSUPPORTED on error.            </returns>
    public pxcmStatus QueryRecognizedObjectData(Int32 index, out RecognizedObjectData objectData)
    {
        objectData = new RecognizedObjectData();
        return PXCMObjectRecognitionData_QueryRecognizedObjectData(instance, index, 0, objectData);
    }

    /// <summary> 
    /// Return the name of the object by its recognized label.
    /// </summary>    
    /// <param name="objectLabel"> - the label of recognized object.</param>
    /// <returns> the corresponding name of the label.</returns>
    public string QueryObjectNameByID(Int32 objectLabel)
    {
        return QueryObjectNameByIDINT(instance, objectLabel);
    }


    internal PXCMObjectRecognitionData(IntPtr instance, Boolean delete)
        : base(instance, delete)
    {
    }
};

#if RSSDK_IN_NAMESPACE
}
#endif
