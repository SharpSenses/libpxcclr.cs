/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2013 - 2014 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

    /** 
	The CaptureManager interface provides the following features:
	(1) Locate an I/O device that meets all module input needs.
	(2) Record any streamming data to a file and playback from the file.
*/
    public partial class PXCMCaptureManager : PXCMBase
    {
        new public const Int32 CUID = unchecked((Int32)0xD8912345);

        /** 
            This is the PXCMCaptureManager callback interface.
        */
        public class Handler
        {
            /**
                @brief	The CaptureManager callbacks this function when creating a device instance.
                @param[in]  mdesc	The I/O module descriptor.
                @param[in]	device	The device instance.
                @return The CaptureManager aborts the device match if the status is an error.
            */
            public delegate pxcmStatus OnCreateDeviceDelegate(PXCMSession.ImplDesc mdesc, PXCMCapture.Device device);

            /**
                @brief	The CaptureManager callbacks this function when configuring the device streams.
                @param[in]	device	The device instance.
                @param[in]	types	The bit-OR'ed value of all streams.
                @return The CaptureManager aborts the device match if the status is an error.
            */
            public delegate pxcmStatus OnSetupStreamsDelegate(PXCMCapture.Device device, PXCMCapture.StreamType types);

            /**
                @brief	The CaptureManager callbacks this function when the current device failed to 
                meet the algorithm needs. If the function returns any error, the CaptureManager performs
                the current device match again, allowing to try multple configurations on the same device.
                @param[in]	device	The device instance.
                @return The CaptureManager repeats the match on the same device if the status code is any 
                error, or go onto the next device if the status code is no error.
            */
            public delegate pxcmStatus OnNextDeviceDelegate(PXCMCapture.Device device);

            public OnCreateDeviceDelegate onCreateDevice;
            public OnSetupStreamsDelegate onSetupStreams;
            public OnNextDeviceDelegate onNextDevice;
        };

        /**
            @brief	The functinon adds the specified DeviceInfo to the DeviceInfo filter list.
            @param[in] dinfo	The DeviceInfo structure to be added to the filter list, or NULL to clean up the filter list.
        */
        public void FilterByDeviceInfo(PXCMCapture.DeviceInfo dinfo)
        {
            PXCMCaptureManager_FilterByDeviceInfo(instance, dinfo);
        }

        /**
            @brief	The functinon adds the specified device information to the DeviceInfo filter list.
            @param[in] name		The optional device friendly name.
            @param[in] did		The optional device symbolic name.
            @param[in] didx		The optional device index.
        */
        public void FilterByDeviceInfo(String name, String did, Int32 didx)
        {
            PXCMCapture.DeviceInfo dinfo = new PXCMCapture.DeviceInfo();
            if (name != null) dinfo.name = name;
            if (did != null) dinfo.did = did;
            dinfo.didx = didx;
            FilterByDeviceInfo(dinfo);
        }

        /**
            @brief	The functinon adds the specified StreamProfile to the StreamProfile filter list.
            @param[in] dinfo	The stream configuration to be added to the filter list, or NULL to clean up the filter list.
        */
        public void FilterByStreamProfiles(PXCMCapture.Device.StreamProfileSet profiles)
        {
            PXCMCaptureManager_FilterByStreamProfiles(instance, profiles);
        }

        /**
            @brief	The functinon adds the specified StreamProfile to the StreamProfile filter list.
            @param[in] type		The stream type.
            @param[in] width	The optional image width.
            @param[in] height	The optional image height.
            @param[in] fps		The optional frame rate.
        */
        public void FilterByStreamProfiles(PXCMCapture.StreamType type, Int32 width, Int32 height, Single fps)
        {
            PXCMCapture.Device.StreamProfileSet profiles = new PXCMCapture.Device.StreamProfileSet();
            profiles[type].frameRate.min = fps;
            profiles[type].frameRate.max = fps;
            profiles[type].imageInfo.width = width;
            profiles[type].imageInfo.height = height;
            FilterByStreamProfiles(profiles);
        }

        /**
            @brief	The function locates an I/O device that meets any module input needs previously specified
            by the RequestStreams function. The device and its streams are configured upon a successful return.
            @return PXCM_STATUS_NO_ERROR		Successful executation.
        */
        public pxcmStatus LocateStreams()
        {
            return LocateStreams(null);
        }

        /**
            @brief	Return the capture instance.
            @return the capture instance.
        */
        public PXCMCapture QueryCapture()
        {
            IntPtr capture = PXCMCaptureManager_QueryCapture(instance);
            if (capture == IntPtr.Zero) return null;
            return new PXCMCapture(capture, false);
        }

        public PXCMCapture capture
        {
            get { return QueryCapture(); }
        }

        /**
            @brief	Return the device instance.
            @return the device instance.
        */
        public PXCMCapture.Device QueryDevice()
        {
            IntPtr device = PXCMCaptureManager_QueryDevice(instance);
            if (device == IntPtr.Zero) return null;
            return new PXCMCapture.Device(device, false);
        }

        public PXCMCapture.Device device
        {
            get { return QueryDevice(); }
        }


        /**
            @brief	Read the image samples for a specified module.
            @param[in]	mid					The module identifier.
            @param[out]	sample				The captured sample, to be returned.
            @param[out] sp					The SP, to be returned.
            @return PXCM_STATUS_NO_ERROR	Successful execution.
        */
        public pxcmStatus ReadModuleStreamsAsync(Int32 mid, PXCMCapture.Sample sample, out PXCMSyncPoint sp)
        {
            IntPtr sp2;
            pxcmStatus sts = PXCMCaptureManager_ReadModuleStreamsAsync(instance, mid, sample.images, out sp2);
            sp = (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR) ? new PXCMSyncPoint(sp2, true) : null;
            return sts;
        }

        /**
            @brief	Setup file recording or playback.
            @param[in]	file				The file name.
            @param[in]	record				If true, the file is opened for recording. Otherwise, the file is opened for playback.
            @return PXCM_STATUS_NO_ERROR	Successful execution.
        */
        public pxcmStatus SetFileName(String file, bool record)
        {
            return PXCMCaptureManager_SetFileName(instance, file, record);
        }

        /**
            @brief	Set up to record or playback certain stream types.
            @param[in]	types		The bit-OR'ed stream types.
        */
        public void SetMask(PXCMCapture.StreamType types)
        {
            PXCMCaptureManager_SetMask(instance, types);
        }

        /**
            @brief	Pause/Resume recording or playing back.
            @param[in]	pause		True for pause and false for resume.
        */
        public void SetPause(bool pause)
        {
            PXCMCaptureManager_SetPause(instance, pause);
        }

        /**
            @brief	Set the realtime playback mode.
            @param[in]	realtime	True to playback in real time, or false to playback as fast as possible.
        */
        public void SetRealtime(bool realtime)
        {
            PXCMCaptureManager_SetRealtime(instance, realtime);
        }

        /**
            @brief	Reset the playback position by the frame index.
            @param[in]	frame		The frame index.
        */
        public void SetFrameByIndex(Int32 frame)
        {
            PXCMCaptureManager_SetFrameByIndex(instance, frame);
        }

        /**
            @brief	Return the current playback position in frame index.
            @return The frame index.
        */
        public Int32 QueryFrameIndex()
        {
            return PXCMCaptureManager_QueryFrameIndex(instance);
        }

        /**
            @brief	Reset the playback position by the nearest time stamp.
            @param[in]	ts		The time stamp, in 100ns.
        */
        public void SetFrameByTimeStamp(Int64 ts)
        {
            PXCMCaptureManager_SetFrameByTimeStamp(instance, ts);
        }

        /**
            @brief	Return the current playback frame time stamp.
            @return The time stamp, in 100ns.
        */
        public Int64 QueryFrameTimeStamp()
        {
            return PXCMCaptureManager_QueryFrameTimeStamp(instance);
        }

        /**
            @brief	Return the frame number in the recorded file.
            @return The number of frames.
        */
        public Int32 QueryNumberOfFrames()
        {
            return PXCMCaptureManager_QueryNumberOfFrames(instance);
        }

        /* Constructors & Disposers */
        internal PXCMCaptureManager(IntPtr instance, Boolean delete)
            : base(instance, delete)
        {
        }
    };

#if RSSDK_IN_NAMESPACE
}
#endif
