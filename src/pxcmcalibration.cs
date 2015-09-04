/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2013 - 2015 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Runtime.InteropServices;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

    public partial class PXCMCalibration : PXCMBase
    {
        new public const Int32 CUID = 0x494A8538;

        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public class StreamTransform
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public Single[]  translation;  /* The translation in mm of the camera coordinate system origin to the world coordinate system origin. The world coordinate system coincides with the depth camera coordinate system. */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public Single[,] rotation;     /* The rotation of the camera coordinate system with respect to the world coordinate system. The world coordinate system coincides with the depth camera coordinate system. */

            public StreamTransform() {
                translation = new Single[3];
                rotation = new Single[3,3];
            }
        };

        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public class StreamCalibration 
        {
            public PXCMPointF32 focalLength;      /* The sensor focal length in pixels along the x and y axes. The parameters vary with the stream resolution setting. */
            public PXCMPointF32 principalPoint;   /*  The sensor principal point in pixels along the x and y axes. The parameters vary with the stream resolution setting. */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public Single[] radialDistortion;     /*  The radial distortion coefficients, as described by camera model equations. */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public Single[] tangentialDistortion; /* The tangential distortion coefficients, as described by camera model equations. */
            public PXCMCapture.DeviceModel model; /* Defines the distortion model of the device - different device models may use different distortion models */
        
            public StreamCalibration () {
                focalLength = new PXCMPointF32(); 
                principalPoint = new PXCMPointF32();
                radialDistortion = new Single[3];
                tangentialDistortion = new Single[2];
                model = PXCMCapture.DeviceModel.DEVICE_MODEL_GENERIC;
            }
        };

        /** 
            @brief Query camera calibration and transformation data for a sensor.
            @param[in]  streamType      The stream type which is produced by the sensor.
            @param[out] calibration     The intrinsics calibration parameters of the sensor.
            @param[out] transformation  The extrinsics  parameters from the sensor to the camera coordinate system origin.
            @return PXCM_STATUS_NO_ERROR Successful execution.
         */ 
        public pxcmStatus QueryStreamProjectionParameters(PXCMCapture.StreamType streamType, out StreamCalibration calibration, out StreamTransform transformation)
        {
            calibration = new StreamCalibration();
            transformation = new StreamTransform(); 
            return PXCMCalibration_QueryStreamProjectionParameters(instance, streamType, calibration, transformation); 
        }  


	/**
	@brief Query camera calibration and transformation data for a sensor according to user defined options.
	@param[in]  streamType      The stream type which is produced by the sensor.
	@param[in]  options         The options that selects specific calibration and transformation data which is produced by the sensor.
	@param[out] calibration     The intrinsics calibration parameters of the sensor.
	@param[out] transformation  The extrinsics transformation parameters from the sensor to the camera coordinate system origin.
	@return PXCM_STATUS_NO_ERROR Successful execution.
	*/
        public pxcmStatus QueryStreamProjectionParametersEx(PXCMCapture.StreamType streamType, PXCMCapture.Device.StreamOption options, out StreamCalibration calibration, out StreamTransform transformation)
        {
            calibration = new StreamCalibration();
            transformation = new StreamTransform();
            return PXCMCalibration_QueryStreamProjectionParametersEx(instance, streamType, options, calibration, transformation);
        }

        /* constructors & misc */
        internal PXCMCalibration(IntPtr instance, Boolean delete)
            : base(instance, delete)
        {
        }
    };

#if RSSDK_IN_NAMESPACE
}
#endif