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

/// <summary> 
/// The CaptureManager interface provides the following features:
/// (1) Locate an I/O device that meets all module input needs.
/// (2) Record any streamming data to a file and playback from the file.
/// </summary>
public partial class PXCMCaptureManager : PXCMBase
{
    new public const Int32 CUID = unchecked((Int32)0xD8912345);

    /// <summary> 
    /// This is the PXCMCaptureManager callback interface.
    /// </summary>
    public class Handler
    {
        /// <summary>
        /// 	The CaptureManager callbacks this function when creating a device instance.
        /// </summary>
        /// <param name="mdesc"> The I/O module descriptor.</param>
        /// <param name="device"> The device instance.</param>
        /// <returns> The CaptureManager aborts the device match if the status is an error.</returns>
        public delegate pxcmStatus OnCreateDeviceDelegate(PXCMSession.ImplDesc mdesc, PXCMCapture.Device device);

        /// <summary>
        /// 	The CaptureManager callbacks this function when configuring the device streams.
        /// </summary>
        /// <param name="device"> The device instance.</param>
        /// <param name="types"> The bit-OR'ed value of all streams.</param>
        /// <returns> The CaptureManager aborts the device match if the status is an error.</returns>
        public delegate pxcmStatus OnSetupStreamsDelegate(PXCMCapture.Device device, PXCMCapture.StreamType types);

        /// <summary>
        /// 	The CaptureManager callbacks this function when the current device failed to 
        /// meet the algorithm needs. If the function returns any error, the CaptureManager performs
        /// the current device match again, allowing to try multple configurations on the same device.
        /// </summary>
        /// <param name="device"> The device instance.</param>
        /// <returns> The CaptureManager repeats the match on the same device if the status code is any </returns>
        /// error, or go onto the next device if the status code is no error.
        public delegate pxcmStatus OnNextDeviceDelegate(PXCMCapture.Device device);

        public OnCreateDeviceDelegate onCreateDevice;
        public OnSetupStreamsDelegate onSetupStreams;
        public OnNextDeviceDelegate onNextDevice;
    };

    /// <summary>
    /// 	The functinon adds the specified DeviceInfo to the DeviceInfo filter list.
    /// </summary>
    /// <param name="dinfo"> The DeviceInfo structure to be added to the filter list, or NULL to clean up the filter list.</param>
    public void FilterByDeviceInfo(PXCMCapture.DeviceInfo dinfo)
    {
        PXCMCaptureManager_FilterByDeviceInfo(instance, dinfo);
    }

    /// <summary>
    /// 	The functinon adds the specified device information to the DeviceInfo filter list.
    /// </summary>
    /// <param name="name"> The optional device friendly name.</param>
    /// <param name="did"> The optional device symbolic name.</param>
    /// <param name="didx"> The optional device index.</param>
    public void FilterByDeviceInfo(String name, String did, Int32 didx)
    {
        PXCMCapture.DeviceInfo dinfo = new PXCMCapture.DeviceInfo();
        if (name != null) dinfo.name = name;
        if (did != null) dinfo.did = did;
        dinfo.didx = didx;
        FilterByDeviceInfo(dinfo);
    }

    /// <summary>
    /// 	The functinon adds the specified StreamProfile to the StreamProfile filter list.
    /// </summary>
    /// <param name="dinfo"> The stream configuration to be added to the filter list, or NULL to clean up the filter list.</param>
    public void FilterByStreamProfiles(PXCMCapture.Device.StreamProfileSet profiles)
    {
        PXCMCaptureManager_FilterByStreamProfiles(instance, profiles);
    }

    /// <summary>
    /// 	The functinon adds the specified StreamProfile to the StreamProfile filter list.
    /// </summary>
    /// <param name="type"> The stream type.</param>
    /// <param name="width"> The optional image width.</param>
    /// <param name="height"> The optional image height.</param>
    /// <param name="fps"> The optional frame rate.</param>
    public void FilterByStreamProfiles(PXCMCapture.StreamType type, Int32 width, Int32 height, Single fps)
    {
        PXCMCapture.Device.StreamProfileSet profiles = new PXCMCapture.Device.StreamProfileSet();
        profiles[type].frameRate.min = fps;
        profiles[type].frameRate.max = fps;
        profiles[type].imageInfo.width = width;
        profiles[type].imageInfo.height = height;
        FilterByStreamProfiles(profiles);
    }

    /// <summary>
    /// 	The function locates an I/O device that meets any module input needs previously specified
    /// by the RequestStreams function. The device and its streams are configured upon a successful return.
    /// </summary>
    /// <returns> PXCM_STATUS_NO_ERROR		Successful executation.</returns>
    public pxcmStatus LocateStreams()
    {
        return LocateStreams(null);
    }

    /// <summary>
    /// 	Return the capture instance.
    /// </summary>
    /// <returns> the capture instance.</returns>
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

    /// <summary>
    /// 	Return the device instance.
    /// </summary>
    /// <returns> the device instance.</returns>
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


    /// <summary>
    /// 	Read the image samples for a specified module.
    /// </summary>
    /// <param name="mid"> The module identifier.</param>
    /// <param name="sample"> The captured sample, to be returned.</param>
    /// <param name="sp"> The SP, to be returned.</param>
    /// <returns> PXCM_STATUS_NO_ERROR	Successful execution.</returns>
    public pxcmStatus ReadModuleStreamsAsync(Int32 mid, PXCMCapture.Sample sample, out PXCMSyncPoint sp)
    {
        IntPtr sp2;
        pxcmStatus sts = PXCMCaptureManager_ReadModuleStreamsAsync(instance, mid, sample.images, out sp2);
        sp = (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR) ? new PXCMSyncPoint(sp2, true) : null;
        return sts;
    }

    /// <summary>
    /// 	Setup file recording or playback.
    /// </summary>
    /// <param name="file"> The file name.</param>
    /// <param name="record"> If true, the file is opened for recording. Otherwise, the file is opened for playback.</param>
    /// <returns> PXCM_STATUS_NO_ERROR	Successful execution.</returns>
    public pxcmStatus SetFileName(String file, bool record)
    {
        return PXCMCaptureManager_SetFileName(instance, file, record);
    }

    /// <summary>
    /// 	Set up to record or playback certain stream types.
    /// </summary>
    /// <param name="types"> The bit-OR'ed stream types.</param>
    public void SetMask(PXCMCapture.StreamType types)
    {
        PXCMCaptureManager_SetMask(instance, types);
    }

    /// <summary>
    /// 	Pause/Resume recording or playing back.
    /// </summary>
    /// <param name="pause"> True for pause and false for resume.</param>
    public void SetPause(bool pause)
    {
        PXCMCaptureManager_SetPause(instance, pause);
    }

    /// <summary>
    /// 	Set the realtime playback mode.
    /// </summary>
    /// <param name="realtime"> True to playback in real time, or false to playback as fast as possible.</param>
    public void SetRealtime(bool realtime)
    {
        PXCMCaptureManager_SetRealtime(instance, realtime);
    }

    /// <summary>
    /// 	Reset the playback position by the frame index.
    /// </summary>
    /// <param name="frame"> The frame index.</param>
    public void SetFrameByIndex(Int32 frame)
    {
        PXCMCaptureManager_SetFrameByIndex(instance, frame);
    }

    /// <summary>
    /// 	Return the current playback position in frame index.
    /// </summary>
    /// <returns> The frame index.</returns>
    public Int32 QueryFrameIndex()
    {
        return PXCMCaptureManager_QueryFrameIndex(instance);
    }

    /// <summary>
    /// 	Reset the playback position by the nearest time stamp.
    /// </summary>
    /// <param name="ts"> The time stamp, in 100ns.</param>
    public void SetFrameByTimeStamp(Int64 ts)
    {
        PXCMCaptureManager_SetFrameByTimeStamp(instance, ts);
    }

    /// <summary>
    /// 	Return the current playback frame time stamp.
    /// </summary>
    /// <returns> The time stamp, in 100ns.</returns>
    public Int64 QueryFrameTimeStamp()
    {
        return PXCMCaptureManager_QueryFrameTimeStamp(instance);
    }

    /// <summary>
    /// 	Return the frame number in the recorded file.
    /// </summary>
    /// <returns> The number of frames.</returns>
    public Int32 QueryNumberOfFrames()
    {
        return PXCMCaptureManager_QueryNumberOfFrames(instance);
    }

    /// <summary>
    /// Enable detection of device rotation. Call function PXCImage::QueryRotation on current image to query rotation data.
    /// </summary>
    /// <param name="enableFlag"> If true, enable detection of device rotation, otherwise disable.</param>
    public pxcmStatus EnableDeviceRotation(bool enableFlag)
    {
        return PXCMCaptureManager_EnableDeviceRotation(instance, enableFlag);
    }

    /// <summary>
    /// Query if device rotation enabled.
    /// </summary>
    public bool IsDeviceRotationEnabled()
    {
        return PXCMCaptureManager_IsDeviceRotationEnabled(instance);
    }

    /* Constructors and Disposers */
    internal PXCMCaptureManager(IntPtr instance, Boolean delete)
        : base(instance, delete)
    {
    }
};

#if RSSDK_IN_NAMESPACE
}
#endif
