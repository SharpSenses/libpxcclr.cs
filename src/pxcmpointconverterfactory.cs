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

/** @class PXCMPointConverterFactory
	Factory class for creating module based point converter
*/
public partial class PXCMPointConverterFactory : PXCMBase
{
    new public const Int32 CUID = unchecked((Int32)0x52435050);

    /** @brief Create hand joint data PointConverter for PXCMHandModule
		The converter will convert joint position to target rectangle/3dbox based on requested hand.
		@note Make sure the handData is constantly updated throughtout the session.
		@example pointConverter.CreateHandJointConverter(handData,PXCMHandData.ACCESS_ORDER_BY_TIME,0,PXCMHandData.JOINT_WRIST);
		@param[in] handData a pointer to PXCHandData
		@param[in] accessOrder The desired hand access order
		@param[in] index hand index
		@param[in] jointType desired joint type to be converted
		@return an object of the created PointConverter, or null in case of illegal arguments
	*/
	public PXCMPointConverter CreateHandJointConverter(PXCMHandData handData,PXCMHandData.AccessOrderType accessOrder, Int32 index,PXCMHandData.JointType jointType)  
    {
        IntPtr inst2 = PXCMPointConverterFactory_CreateHandJointConverter(instance, handData.instance, accessOrder, index, jointType);
        return (inst2 == IntPtr.Zero) ? null : new PXCMPointConverter(inst2, true);
    }

	/** @brief Create hand Extremity data PointConverter for PXCMHandModule
		The converter will convert extremity data to target rectangle/3dbox based on requested hand.
		@note Make sure the handData is constantly updated throughtout the session.
		@example pointConverter.CreateHandExtremityConverter(handData,PXCMHandData.ACCESS_ORDER_BY_TIME,0,PXCMHandData.EXTREMITY_CENTER);
		@param[in] handData a pointer to PXCMHandData
		@param[in] accessOrder The desired hand access order
		@param[in] index hand index
		@param[in] extremityType desired extremity type to be converted
		@return an object of the created PointConverter, or null in case of illegal arguments
	*/
	public PXCMPointConverter  CreateHandExtremityConverter(PXCMHandData handData,PXCMHandData.AccessOrderType accessOrder, Int32 index,PXCMHandData.ExtremityType extremityType)  
    {
        IntPtr inst2 = PXCMPointConverterFactory_CreateHandExtremityConverter(instance, handData.instance, accessOrder, index, extremityType);
        return (inst2 == IntPtr.Zero) ? null : new PXCMPointConverter(inst2, true);
    }


	/** @brief Create blob data PointConverter for PXCMBlobModule
		The converter will convert exrtemity data to target rectangle/3dbox based on requested access order and index.
		@note Make sure the blobData is constantly updated throughtout the session.
		@example pointConverter.CreateBlobPointConverter(blobData,PXCMBlobData.ACCESS_ORDER_BY_TIME,0,PXCMBlobData.EXTREMITY_CENTER);
		@param[in] blobData a pointer to PXCBlobData
		@param[in] accessOrder The desired blob access order
		@param[in] index blob index
		@param[in] extremityType desired exrtemity point to be converted
		@return an object to the created PointConverter, or null in case of illegal arguments
	*/
	public PXCMPointConverter CreateBlobPointConverter(PXCMBlobData blobData,PXCMBlobData.AccessOrderType accessOrder,Int32 index,PXCMBlobData.ExtremityType extremityType)
    {
        IntPtr inst2 = PXCMPointConverterFactory_CreateBlobPointConverter(instance, blobData.instance, accessOrder, index, extremityType);
        return (inst2 == IntPtr.Zero) ? null : new PXCMPointConverter(inst2, true);
    }


	/** @brief Create custom PointConverter
		The converter will convert any data point to target rectangle/3dbox
		@note make sure to call Set2DPoint or Set3DPoint
		@example pointConverter.CreateCustomPointConverter();
		PXCMPointF32 point = {22.f,40.f};
		pointConverter.Set2DPoint(point);
		pointConveter.GetConverted2DPoint();
		@return pointer to the created PointConverter
	*/
    public PXCMPointConverter CreateCustomPointConverter()
    {
        IntPtr inst2 = PXCMPointConverterFactory_CreateCustomPointConverter(instance);
        return (inst2 == IntPtr.Zero) ? null : new PXCMPointConverter(inst2, true);
    }


    /* constructors & misc */
    internal PXCMPointConverterFactory(IntPtr instance, Boolean delete)
        : base(instance, delete)
    {
    }
};

#if RSSDK_IN_NAMESPACE
}
#endif