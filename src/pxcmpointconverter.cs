/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2015 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Runtime.InteropServices;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

/** @class PXCMPointConverter 
	A utility for converting 2D/3D data points from defined source to a defined target.
	The class provides with setters for both source and target rectangle/box
	The class provides with getters for 2D/3D converted points
	2D Rectangle - A data structure that represents a 2D rectangle
	3D Box - A data structure representing a "box" in 3D space (a 3D cube).
	Example: convert from hand-joint image coordinates to user defined screen area coordinates.
	Example: convert from face-landmark 3d position to a 3d game coordinate system.
**/
public partial class PXCMPointConverter : PXCMBase
{
    new public const Int32 CUID = unchecked((Int32)0x46435050);

    // 2D
    /**			
        @brief Set 2D image rectangle source
        @param[in] rectangle2D containing desired source rectangle dimensions
        @return PXCM_STATUS_VALUE_OUT_OF_RANGE if rectangle2D w, h params less than or equal to 0
        @return PXCM_STATUS_V_NO_ERROR if rectangle2D w,h params > 0
    */
    public pxcmStatus Set2DSourceRectangle(PXCMRectF32 rectangle2D)
    {
        return PXCMPointConverter_Set2DSourceRectangle(instance, rectangle2D);
    }

    /**			
		@brief Set 2D target rectangle
		@param[in] rectangle2D containing desired target rectangle dimensions
		@return PXCM_STATUS_VALUE_OUT_OF_RANGE if rectangle2D w,h params less than or equal to 0
		@return PXCM_STATUS_NO_ERROR if rectangle2D w,h params > 0
	*/
	public pxcmStatus Set2DTargetRectangle(PXCMRectF32 rectangle2D) 
    {
        return PXCMPointConverter_Set2DTargetRectangle(instance, rectangle2D);
    }

	/**			
		@brief Get converted 2D point from source to target
		@param[out] PXCMPointF32 converted 2D point
		@note Call handData.Update() before calling this function in order to get converted point based on updated frame data.
		@return Converted 2D Point
	*/
	public pxcmStatus Set2DPoint(PXCMPointF32 point2D) 
    {
        return PXCMPointConverter_Set2DPoint(instance, point2D);
    }

	/**			
		@brief Set 3D point to be converted
		@note only relevant when using CreateCustomPointConverter 
		@return PXCM_STATUS_ITEM_UNAVAILABLE if not using CustomPointConverter
	*/
	public PXCMPointF32 GetConverted2DPoint() 
            {
        return PXCMPointConverter_GetConverted2DPoint(instance);
    }

	/**	
		@brief inverting x,y axis
		Use if you wish to invert converted point axis.
		@example Use Invert2DAxis(false, true) if user app y axis is facing up
		@param[in] x invert x axis
		@param[in] y invert y axis
		@return PXCM_STATUS_NO_ERROR
	*/
	public pxcmStatus Invert2DAxis(Boolean x, Boolean y)         
    {
        return PXCMPointConverter_Invert2DAxis(instance, x, y);
    }

	// 3D
	/**			
		@brief Set 3D world box source
		@param[in] box3D containing desired source world box dimensions
		@return PXCM_STATUS_VALUE_OUT_OF_RANGE if any of box3D dimension params less than or equal to 0
		@return PXCM_STATUS_NO_ERROR if box3D dimension params > 0
	*/
	public pxcmStatus Set3DSourceBox(PXCMBox3DF32 box3D)
    {
        return PXCMPointConverter_Set3DSourceBox(instance, box3D);
    }

	/**			
		@brief Set 3D target box
		@param[in] box3D containing desired target box dimensions
		@return PXCM_STATMUS_VALUE_OUT_OF_RANGE if any of box3D dimension params less than or equal to 0
		@return PXCM_STAUS_NO_ERROR if box3D dimension params > 0
	*/
	public pxcmStatus Set3DTargetBox(PXCMBox3DF32 box3D) 
    {
        return PXCMPointConverter_Set3DTargetBox(instance, box3D);
    }

	/**			
		@brief Set 2D point to be converted
		@note only relevant when using CreateCustomPointConverter 
		@return PXCM_STATUS_ITEM_UNAVAILABLE if not using CustomPointConverter
	*/
	public pxcmStatus Set3DPoint(PXCMPoint3DF32 point3D) 
    {
        return PXCMPointConverter_Set3DPoint(instance, point3D);
    }

	/**			
		@brief Get converted 3D point from source to target
		@note Call handData.Update() before calling this function in order to get converted point based on updated frame data.
		@param[out] PXCMPoint3DF32 converted 3D point
		@return Converted 3D Point
	*/
	public PXCMPoint3DF32 GetConverted3DPoint()         {
        return PXCMPointConverter_GetConverted3DPoint(instance);
    }

	/**			
		@brief inverting x,y,z axis
		Use if you wish to invert converted point axis.
		@example Use Invert3DAxis(false,true,false ) if user app y axis is facing down
		@param[in] x invert x axis
		@param[in] y invert y axis
		@param[in] z invert z axis
		@return PXCM_STATUS_NO_ERROR
	*/
	public pxcmStatus Invert3DAxis(Boolean x, Boolean y, Boolean z)         {
        return PXCMPointConverter_Invert3DAxis(instance, x, y, z);
    }


    /* constructors and misc */
    internal PXCMPointConverter(IntPtr instance, Boolean delete)
        : base(instance, delete)
    {
    }
};

#if RSSDK_IN_NAMESPACE
}
#endif