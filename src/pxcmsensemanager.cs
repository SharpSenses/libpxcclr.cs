/*******************************************************************************

  INTEL CORPORATION PROPRIETARY INFORMATION
  This software is supplied under the terms of a license agreement or nondisclosure
  agreement with Intel Corporation and may not be copied or disclosed except in
  accordance with the terms of that agreement
  Copyright(c) 2013 Intel Corporation. All Rights Reserved.

 *******************************************************************************/
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Reflection;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

public partial class PXCMSenseManager : PXCMBase
{
    new public const Int32 CUID = unchecked((Int32)0xD8954321);
    public const Int32 TIMEOUT_INFINITE = -1;

    public class Handler
    {
        /// <summary>
        /// The SenseManager calls back this function when there is a device connection or
        /// disconnection. During initialization, the SenseManager callbacks this function when 
        /// openning or closing any capture devices.
        /// </summary>
        /// <param name="device"> The video device instance.</param>
        /// <param name="connected"> The device connection status.</param>
        /// <returns> The return status is ignored during the PXCSenseManager initialization. During
        /// streaming, the SenseManager aborts the execution pipeline if the status is an error.</returns>
        public delegate pxcmStatus OnConnectDelegate(PXCMCapture.Device device, Boolean connected);

        /// <summary>
        /// The SenseManager calls back this function during initialization after each device 
        /// configuration is set.
        /// </summary>
        /// <param name="mid"> The module identifier. Usually this is the interface identifier, or PXCMCapture.CUID+n for raw video streams.</param>
        /// <param name="module"> The module instance, or NULL for raw video streams.</param>
        /// <returns> The SenseManager aborts the execution pipeline if the status is an error.</returns>
        public delegate pxcmStatus OnModuleSetProfileDelegate(Int32 mid, PXCMBase module);

        /// <summary>
        /// The SenseManager calls back this function after a module completed processing the frame data.
        /// </summary>
        /// <param name="mid"> The module identifier. Usually this is the interface identifier.</param>
        /// <param name="module"> The module instance.</param>
        /// <returns> The SenseManager aborts the execution pipeline if the status is an error.</returns>
        public delegate pxcmStatus OnModuleProcessedFrameDelegate(Int32 mid, PXCMBase module, PXCMCapture.Sample sample);

        /// <summary>
        /// The SenseManager calls back this function when raw video streams (explicitly requested) are available.
        /// </summary>
        /// <param name="mid"> The module identifier. Usually this is the interface identifier.</param>
        /// <param name="sample"> The sample from capture device</param>
        /// <returns> The SenseManager aborts the execution pipeline if the status is an error.</returns>
        public delegate pxcmStatus OnNewSampleDelegate(Int32 mid, PXCMCapture.Sample sample);

        /// <summary>
        /// The SenseManager calls back this function when streaming loop in StreamFrames() function terminated.
        /// </summary>
        /// <param name="sts"> The error code</param>
        public delegate void OnStatusDelegate(Int32 mid, pxcmStatus sts);

        public OnConnectDelegate onConnect;
        public OnModuleSetProfileDelegate onModuleSetProfile;
        public OnModuleProcessedFrameDelegate onModuleProcessedFrame;
        public OnNewSampleDelegate onNewSample;
        public OnStatusDelegate onStatus;
    };

    /// <summary>
    /// 	Return the PXCSession instance. Internally managed. Do not release the instance.
    /// The session instance is managed internally by the SenseManager. Do not release the session instance.
    /// </summary>
    /// <returns> The PXCMSession instance.</returns>
    public PXCMSession QuerySession()
    {
        IntPtr ss = PXCMSenseManager_QuerySession(instance);
        if (ss == IntPtr.Zero) return null;
        return new PXCMSession(ss, false);
    }

    public PXCMSession session
    {
        get { return QuerySession(); }
    }

    /// <summary>
    /// 	Return the PXCMCaptureManager instance. Internally managed. Do not release the instance.
    /// The instance is managed internally by the SenseManager. Do not release the instance.
    /// </summary>
    /// <returns> The PXCMCaptureManager instance.</returns>
    public PXCMCaptureManager QueryCaptureManager()
    {
        IntPtr cutil = PXCMSenseManager_QueryCaptureManager(instance);
        if (cutil == IntPtr.Zero) return null;
        return new PXCMCaptureManager(cutil, false);
    }

    public PXCMCaptureManager captureManager
    {
        get { return QueryCaptureManager(); }
    }

    /// <summary>
    /// 	Return the captured sample for the specified module or explicitly requested streams. 
    /// For modules, use mid=module interface identifier. 
    /// For explictly requested streams via multiple calls to EnableStream(s), use mid=PXCMCapture.CUID+0,1,2... 
    /// The captured sample is managed internally by the SenseManager. Do not release the instance.
    /// </summary>
    /// <param name="mid"> The module identifier. Usually this is the interface identifier, or PXCMCapture.CUID+n for raw video streams.</param>
    /// <returns> The sample instance, or null if the captured sample is not available.</returns>
    public PXCMCapture.Sample QuerySample()
    {
        return QuerySample(0);
    }

    /// <summary>
    /// 	Return the captured sample for the user segmentation module.
    /// The captured sample is managed internally by the SenseManager. Do not release the sample.
    /// </summary>
    /// <returns> The sample instance, or NULL if the captured sample is not available.</returns>
    public PXCMCapture.Sample Query3DSegSample()
    {
        return QuerySample(PXCM3DSeg.CUID);
    }

    /// <summary>
    /// 	Return the captured sample for the scene perception module.
    /// The captured sample is managed internally by the SenseManager. Do not release the sample.
    /// </summary>
    /// <returns> The sample instance, or NULL if the captured sample is not available.</returns>
    public PXCMCapture.Sample QueryScenePerceptionSample()
    {
        return QuerySample(PXCMScenePerception.CUID);
    }

    /// <summary>
    /// 	  Return the captured sample for the enhanced Videography module.
    /// The captured sample is managed internally by the SenseManager. Do not release the sample.
    /// </summary>
    /// <returns> The sample instance, or NULL if the captured sample is not available.</returns>
    public PXCMCapture.Sample QueryEnhancedVideoSample()
    {
        return QuerySample(PXCMEnhancedVideo.CUID);
    }

    /// <summary>
    /// 	Return the captured sample for the object tracking module.
    /// The captured sample is managed internally by the SenseManager. Do not release the sample.
    /// </summary>
    /// <returns> The sample instance, or NULL if the captured sample is not available.</returns>
    public PXCMCapture.Sample QueryTrackerSample()
    {
        return QuerySample(PXCMTracker.CUID);
    }

    /// <summary>
    /// 	Return the captured sample for the face module.
    /// The captured sample is managed internally by the SenseManager. Do not release the sample.
    /// </summary>
    /// <returns> The sample instance, or NULL if the captured sample is not available.</returns>
    public PXCMCapture.Sample QueryFaceSample()
    {
        return QuerySample(PXCMFaceModule.CUID);
    }

    /// <summary>
    /// 	Return the captured sample for the PersonTracking module.
    /// The captured sample is managed internally by the SenseManager. Do not release the sample.
    /// </summary>
    /// <returns> The sample instance, or NULL if the captured sample is not available.</returns>
    public PXCMCapture.Sample QueryPersonTrackingSample()
    {
        return QuerySample(PXCMPersonTrackingModule.CUID);
    }

    /// <summary>
    /// 	Return the captured sample for the hand module.
    /// The captured sample is managed internally by the SenseManager. Do not release the sample.
    /// </summary>
    /// <returns> The sample instance, or NULL if the captured sample is not available.</returns>
    public PXCMCapture.Sample QueryHandSample()
    {
        return QuerySample(PXCMHandModule.CUID);
    }

    /// <summary>
    /// 	Return the captured sample for the hand module.
    /// The captured sample is managed internally by the SenseManager. Do not release the sample.
    /// </summary>
    /// <returns> The sample instance, or NULL if the captured sample is not available.</returns>
    public PXCMCapture.Sample QueryHandCursorSample()
    {
        return QuerySample(PXCMHandCursorModule.CUID);
    }



    /// <summary>
    /// 	Return the captured sample for the blob module.
    /// The captured sample is managed internally by the SenseManager. Do not release the sample.
    /// </summary>
    /// <returns> The sample instance, or NULL if the captured sample is not available.</returns>
    public PXCMCapture.Sample QueryBlobSample()
    {
        return QuerySample(PXCMBlobModule.CUID);
    }

    /// <summary>
    /// 	Return the captured sample for the ObjectRecognition module.
    /// The captured sample is managed internally by the SenseManager. Do not release the sample.
    /// </summary>
    /// <returns> The sample instance, or null if the captured sample is not available.</returns>
    public PXCMCapture.Sample QueryObjectRecognitionSample()
    {
        return QuerySample(PXCMObjectRecognitionModule.CUID);
    }

    /// <summary>
    /// 	Return the module instance. Between AcquireFrame/ReleaseFrame, the function returns
    /// NULL if the specified module hasn't completed processing the current frame of image data.
    /// The instance is managed internally by the SenseManager. Do not release the instance.
    /// </summary>
    /// <param name="mid"> The module identifier. Usually this is the interface identifier.</param>
    /// <returns> The module instance.</returns>
    public PXCMBase QueryModule(Int32 mid)
    {
        if (mid == PXCMHandModule.CUID) return QueryHand();
        if (mid == PXCMFaceModule.CUID) return QueryFace();
        if (mid == PXCMHandCursorModule.CUID) return QueryHandCursor();
        if (mid == PXCMBlobModule.CUID) return QueryBlob();
        if (mid == PXCMTouchlessController.CUID) return QueryTouchlessController();
        if (mid == PXCM3DScan.CUID) return Query3DScan();

        IntPtr pbase = PXCMSenseManager_QueryModule(instance, mid);
        if (pbase == IntPtr.Zero) return null;
        return PXCMBase.IntPtr2PXCMBase(pbase, mid);
    }

    /// <summary>
    /// 	Return the user segmentation module instance. Between AcquireFrame/ReleaseFrame, the function returns
    /// NULL if the specified module hasn't completed processing the current frame of image data.
    /// The instance is managed internally by the SenseManager. Do not release the instance.
    /// </summary>
    /// <returns> The module instance.</returns>
    public PXCM3DSeg Query3DSeg()
    {
        IntPtr module = PXCMSenseManager_QueryModule(instance, PXCM3DSeg.CUID);
        if (module == IntPtr.Zero) return null;
        return new PXCM3DSeg(module, false);
    }

    internal PXCM3DScan.EventMaps scanEvents = new PXCM3DScan.EventMaps();

    /// <summary>
    /// 	Return the 3D scanning module instance. Between AcquireFrame/ReleaseFrame, the function returns
    /// NULL if the specified module hasn't completed processing the current frame of image data.
    /// The instance is managed internally by the SenseManager. Do not release the instance.
    /// </summary>
    /// <returns> The module instance.</returns>
    public PXCM3DScan Query3DScan()
    {
        IntPtr module = PXCMSenseManager_QueryModule(instance, PXCM3DScan.CUID);
        if (module == IntPtr.Zero) return null;
        return new PXCM3DScan(scanEvents, module, false);
    }

    /// <summary>
    ///  Return the Scene Perception module instance. Between AcquireFrame/ReleaseFrame, 
    /// the function returns null if the specified module hasn't completed processing the current 
    /// frame of image data. The instance is managed internally by the SenseManager. 
    /// Do not release the instance.
    /// </summary>
    /// <returns> The module instance.</returns>
    public PXCMScenePerception QueryScenePerception()
    {
        IntPtr module = PXCMSenseManager_QueryModule(instance, PXCMScenePerception.CUID);
        if (module == IntPtr.Zero) return null;
        return new PXCMScenePerception(module, false);
    }

    /// <summary>
    /// 	Return the object tracking module instance. Between AcquireFrame/ReleaseFrame, the function returns
    /// NULL if the specified module hasn't completed processing the current frame of image data.
    /// The instance is managed internally by the SenseManager. Do not release the instance.
    /// </summary>
    /// <returns> The module instance.</returns>
    public PXCMTracker QueryTracker()
    {
        IntPtr module = PXCMSenseManager_QueryModule(instance, PXCMTracker.CUID);
        if (module == IntPtr.Zero) return null;
        return new PXCMTracker(module, false);
    }

    internal PXCMFaceConfiguration.EventMaps faceEvents = new PXCMFaceConfiguration.EventMaps();

    /// <summary>
    /// 	Return the Face module instance. Between AcquireFrame/ReleaseFrame, the function returns
    /// NULL if the specified module hasn't completed processing the current frame of image data.
    /// The instance is managed internally by the SenseManager. Do not release the instance.
    /// </summary>
    /// <returns> The module instance.</returns>
    public PXCMFaceModule QueryFace()
    {
        IntPtr module = PXCMSenseManager_QueryModule(instance, PXCMFaceModule.CUID);
        if (module == IntPtr.Zero) return null;
        return new PXCMFaceModule(faceEvents, module, false);
    }

    internal PXCMTouchlessController.EventMap tcEvents = new PXCMTouchlessController.EventMap();

    /// <summary>
    /// 	Return the Touchless module instance. Between AcquireFrame/ReleaseFrame, the function returns
    /// NULL if the specified module hasn't completed processing the current frame of image data.
    /// The instance is managed internally by the SenseManager. Do not release the instance.
    /// </summary>
    /// <returns> The module instance.</returns>
    public PXCMTouchlessController QueryTouchlessController()
    {
        IntPtr module = PXCMSenseManager_QueryModule(instance, PXCMTouchlessController.CUID);
        if (module == IntPtr.Zero) return null;
        return new PXCMTouchlessController(tcEvents, module, false);
    }

    internal PXCMHandConfiguration.EventMaps handEvents = new PXCMHandConfiguration.EventMaps();

    internal PXCMCursorConfiguration.EventMaps cursorEvents = new PXCMCursorConfiguration.EventMaps();


    /// <summary>
    /// 	Return the hand module instance. Between AcquireFrame/ReleaseFrame, the function returns
    /// NULL if the specified module hasn't completed processing the current frame of image data.
    /// The instance is managed internally by the SenseManager. Do not release the instance.
    /// </summary>
    /// <returns> The module instance.</returns>
    public PXCMHandModule QueryHand()
    {
        IntPtr module = PXCMSenseManager_QueryModule(instance, PXCMHandModule.CUID);
        if (module == IntPtr.Zero) return null;
        return new PXCMHandModule(handEvents, module, false);
    }


    /// <summary>
    /// 	Return the hand module instance. Between AcquireFrame/ReleaseFrame, the function returns
    /// NULL if the specified module hasn't completed processing the current frame of image data.
    /// The instance is managed internally by the SenseManager. Do not release the instance.
    /// </summary>
    /// <returns> The module instance.</returns>
    public PXCMHandCursorModule QueryHandCursor()
    {
        IntPtr module = PXCMSenseManager_QueryModule(instance, PXCMHandCursorModule.CUID);
        if (module == IntPtr.Zero) return null;
        return new PXCMHandCursorModule(cursorEvents, module, false);
    }

    /// <summary>
    /// 	Return the ObjectRecognition module instance. Between AcquireFrame/ReleaseFrame, the function returns
    /// NULL if the specified module hasn't completed processing the current frame of image data.
    /// The instance is managed internally by the SenseManager. Do not release the instance.
    /// </summary>
    /// <returns> The module instance.</returns>
    public PXCMObjectRecognitionModule QueryObjectRecognition()
    {
        IntPtr module = PXCMSenseManager_QueryModule(instance, PXCMObjectRecognitionModule.CUID);
        if (module == IntPtr.Zero) return null;
        return new PXCMObjectRecognitionModule(module, false);
    }

    /// <summary>
    /// 	Return the Blob module instance. Between AcquireFrame/ReleaseFrame, the function returns
    /// NULL if the specified module hasn't completed processing the current frame of image data.
    /// The instance is managed internally by the SenseManager. Do not release the instance.
    /// </summary>
    /// <returns> The module instance.</returns>
    public PXCMBlobModule QueryBlob()
    {
        IntPtr module = PXCMSenseManager_QueryModule(instance, PXCMBlobModule.CUID);
        if (module == IntPtr.Zero) return null;
        return new PXCMBlobModule(module, false);
    }

    /// <summary>
    /// 	Return the Enhanced Videography module instance. Between AcquireFrame/ReleaseFrame, the function returns
    /// NULL if the specified module hasn't completed processing the current frame of image data.
    /// The instance is managed internally by the SenseManager. Do not release the instance.
    /// </summary>
    /// <returns> The module instance.</returns>
    public PXCMEnhancedVideo QueryEnhancedVideo()
    {
        IntPtr module = PXCMSenseManager_QueryModule(instance, PXCMEnhancedVideo.CUID);
        if (module == IntPtr.Zero) return null;
        return new PXCMEnhancedVideo(module, false);
    }

    /// <summary>
    /// 	Return the Enhanced Videography module instance. Between AcquireFrame/ReleaseFrame, the function returns
    /// NULL if the specified module hasn't completed processing the current frame of image data.
    /// The instance is managed internally by the SenseManager. Do not release the instance.
    /// </summary>
    /// <returns> The module instance.</returns>
    public PXCMPersonTrackingModule QueryPersonTracking()
    {
        IntPtr module = PXCMSenseManager_QueryModule(instance, PXCMPersonTrackingModule.CUID);
        if (module == IntPtr.Zero) return null;
        return new PXCMPersonTrackingModule(module, false);
    }

    /// <summary>
    /// 	Initialize the SenseManager pipeline for streaming. The application must enable raw 
    /// streams or algorithm modules before this function.
    /// </summary>
    /// <returns> PXCM_STATUS_NO_ERROR		Successful execution.</returns>
    public pxcmStatus Init()
    {
        return Init(null);
    }

    /// <summary>
    /// 	Stream frames from the capture module to the algorithm modules. The application must 
    /// initialize the pipeline before calling this function. If blocking, the function blocks until
    /// the streaming stops (upon any capture device error or any callback function returns any error.
    /// If non-blocking, the function returns immediately while running streaming in a thread.
    /// </summary>
    /// <param name="blocking"> The blocking status.</param>
    /// <returns> PXCM_STATUS_NO_ERROR	Successful execution.</returns>
    public bool IsConnected()
    {
        return PXCMSenseManager_IsConnected(instance);
    }

    /// <summary>
    /// 	This function starts streaming and waits until certain events occur. If ifall=true, 
    /// the function blocks until all samples are ready and the modules completed processing the samples.
    /// If ifall=false, the function blocks until any of the mentioned is ready. The SenseManager 
    /// pipeline pauses at this point for the application to retrieve the processed module data, until 
    /// the application calls ReleaseFrame.
    /// AcquireFrame/ReleaseFrame are not compatible with StreamFrames. Run the SenseManager in the pulling
    /// mode with AcquireFrame/ReleaseFrame, or the callback mode with StreamFrames.
    /// </summary>
    /// <param name="ifall"> If true, wait for all modules to complete processing the data.</param>
    /// <param name="timeout"> The time out value in milliseconds.</param>
    /// <returns> PXCM_STATUS_NO_ERROR	Successful execution.</returns>
    public pxcmStatus AcquireFrame(Boolean ifall, Int32 timeout)
    {
        return PXCMSenseManager_AcquireFrame(instance, ifall, timeout);
    }

    /// <summary>
    /// 	This function starts streaming and waits until certain events occur. If ifall=true, 
    /// the function blocks until all samples are ready and the modules completed processing the samples.
    /// If ifall=false, the function blocks until any of the mentioned is ready. The SenseManager 
    /// pipeline pauses at this point for the application to retrieve the processed module data, until 
    /// the application calls ReleaseFrame.
    /// AcquireFrame/ReleaseFrame are not compatible with StreamFrames. Run the SenseManager in the pulling
    /// mode with AcquireFrame/ReleaseFrame, or the callback mode with StreamFrames.
    /// </summary>
    /// <param name="ifall"> If true, wait for all modules to complete processing the data.</param>
    /// <returns> PXCM_STATUS_NO_ERROR	Successful execution.</returns>
    public pxcmStatus AcquireFrame(Boolean ifall)
    {
        return AcquireFrame(ifall, TIMEOUT_INFINITE);
    }

    /// <summary>
    /// 	This function starts streaming and waits until certain events occur. If ifall=true, 
    /// the function blocks until all samples are ready and the modules completed processing the samples.
    /// If ifall=false, the function blocks until any of the mentioned is ready. The SenseManager 
    /// pipeline pauses at this point for the application to retrieve the processed module data, until 
    /// the application calls ReleaseFrame.
    /// AcquireFrame/ReleaseFrame are not compatible with StreamFrames. Run the SenseManager in the pulling
    /// mode with AcquireFrame/ReleaseFrame, or the callback mode with StreamFrames..
    /// </summary>
    /// <returns> PXCM_STATUS_NO_ERROR	Successful execution.</returns>
    public pxcmStatus AcquireFrame()
    {
        return AcquireFrame(true);
    }

    /// <summary>
    /// 	This function resumes streaming after AcquireFrame.
    /// AcquireFrame/ReleaseFrame are not compatible with StreamFrames. Run the SenseManager in the pulling
    /// mode with AcquireFrame/ReleaseFrame, or the callback mode with StreamFrames.
    /// </summary>
    public void ReleaseFrame()
    {
        PXCMSenseManager_ReleaseFrame(instance);
    }

    public void FlushFrame()
    {
        PXCMSenseManager_FlushFrame(instance);
    }

    public pxcmStatus EnableStreams(PXCMVideoModule.DataDesc ddesc)
    {
        return PXCMSenseManager_EnableStreams(instance, ddesc);
    }

    /// <summary>
    /// 	Explicitly request to stream the specified raw stream. If specified more than one stream, 
    /// SenseManager will synchronize these streams. If called multiple times, the function treats each
    /// stream request as independent (unaligned). The stream identifier is PXCCapture.CUID+n.
    /// </summary>
    /// <param name="type"> The stream type.</param>
    /// <param name="width"> stream width.</param>
    /// <param name="height"> stream height.</param>
    /// <returns> PXC_STATUS_NO_ERROR		Successful execution.</returns>
    public pxcmStatus EnableStream(PXCMCapture.StreamType type, Int32 width, Int32 height)
    {
        return EnableStream(type, width, height, 0);
    }

    /// <summary>
    /// 	Explicitly request to stream the specified raw stream. If specified more than one stream, 
    /// SenseManager will synchronize these streams. If called multiple times, the function treats each
    /// stream request as independent (unaligned). The stream identifier is PXCMCapture.CUID+n.
    /// </summary>
    /// <param name="type"> The stream type.</param>
    /// <param name="width"> stream width.</param>
    /// <param name="height"> stream height.</param>
    /// <param name="fps"> stream frame rate.</param>
    /// <returns> PXC_STATUS_NO_ERROR		Successful execution.</returns>
    public pxcmStatus EnableStream(PXCMCapture.StreamType type, Int32 width, Int32 height, Single fps)
    {

        PXCMVideoModule.DataDesc ddesc = new PXCMVideoModule.DataDesc();
        ddesc.deviceInfo.streams = type;

        PXCMVideoModule.StreamDesc sdesc = ddesc.streams[type];
        sdesc.sizeMin.width = width;
        sdesc.sizeMax.width = width;
        sdesc.sizeMin.height = height;
        sdesc.sizeMax.height = height;
        sdesc.frameRate.min = fps;
        sdesc.frameRate.max = fps;
        return EnableStreams(ddesc);
    }

    /// <summary>
    /// 	Explicitly request to stream the specified raw stream. If specified more than one stream, 
    /// SenseManager will synchronize these streams. If called multiple times, the function treats each
    /// stream request as independent (unaligned). The stream identifier is PXCMCapture.CUID+n.
    /// </summary>
    /// <param name="type"> The stream type.</param>
    /// <param name="width"> stream width.</param>
    /// <param name="height"> stream height.</param>
    /// <param name="fps"> stream frame rate.</param>
    /// <param name="options"> stream flags.</param>
    /// <returns> PXC_STATUS_NO_ERROR		Successful execution.</returns>
    public pxcmStatus EnableStream(PXCMCapture.StreamType type, Int32 width, Int32 height, Single fps, PXCMCapture.Device.StreamOption options)
    {

        PXCMVideoModule.DataDesc ddesc = new PXCMVideoModule.DataDesc();
        ddesc.deviceInfo.streams = type;

        PXCMVideoModule.StreamDesc sdesc = ddesc.streams[type];
        sdesc.sizeMin.width = width;
        sdesc.sizeMax.width = width;
        sdesc.sizeMin.height = height;
        sdesc.sizeMax.height = height;
        sdesc.frameRate.min = fps;
        sdesc.frameRate.max = fps;
        sdesc.options = options;
        return EnableStreams(ddesc);
    }

    /// <summary>
    /// 	Enable a module in the pipeline.
    /// </summary>
    /// <param name="mid"> The module identifier. This is usually the interface identifier.</param>
    /// <param name="mdesc"> The module descriptor.</param>
    /// <returns> PXCM_STATUS_NO_ERROR	Successful execution.</returns>
    public pxcmStatus EnableModule(Int32 mid, PXCMSession.ImplDesc mdesc)
    {
        return PXCMSenseManager_EnableModule(instance, mid, mdesc);
    }

    /// <summary>
    /// 	Enable the face module in the pipeline.
    /// </summary>
    /// <param name="name"> The optional module name.</param>
    /// <param name="mid"> The module identifier. This is usually the interface identifier.</param>
    /// <returns> PXCM_STATUS_NO_ERROR	Successful execution.</returns>
    public pxcmStatus EnableModule(Int32 mid, String name)
    {
        PXCMSession.ImplDesc mdesc = null;
        if (name != null)
        {
            mdesc = new PXCMSession.ImplDesc();
            mdesc.cuids[0] = mid;
            mdesc.friendlyName = name;
        }
        return EnableModule(mid, mdesc);
    }

    /// <summary>
    /// 	Enable the user segmentation module in the pipeline.
    /// </summary>
    /// <param name="name"> The module name.</param>
    /// <returns> PXCM_STATUS_NO_ERROR		Successful execution.</returns>
    public pxcmStatus Enable3DSeg(String name)
    {
        return EnableModule(PXCM3DSeg.CUID, name);
    }

    /// <summary>
    /// 	Enable the user segmentation module in the pipeline.
    /// </summary>
    /// <param name="name"> The module name.</param>
    /// <returns> PXCM_STATUS_NO_ERROR		Successful execution.</returns>
    public pxcmStatus Enable3DSeg()
    {
        return Enable3DSeg(null);
    }

    /// <summary>
    /// 	Enable the 3D scanning module in the pipeline.
    /// </summary>
    /// <param name="name"> The module name.</param>
    /// <returns> PXCM_STATUS_NO_ERROR		Successful execution.</returns>
    public pxcmStatus Enable3DScan(String name)
    {
        return EnableModule(PXCM3DScan.CUID, name);
    }


    /// <summary>
    ///  Enable the Scene Perception module in the pipeline.
    /// </summary>
    /// <param name="name"> The module name.</param>
    /// <returns> PXCM_STATUS_NO_ERROR        Successful execution.</returns>
    public pxcmStatus EnableScenePerception(String name)
    {
        return EnableModule(PXCMScenePerception.CUID, name);
    }

    /// <summary>
    ///  Enable the Scene Perception module in the pipeline.
    /// </summary>
    /// <returns> PXCM_STATUS_NO_ERROR        Successful execution.</returns>
    public pxcmStatus EnableScenePerception()
    {
        return EnableScenePerception(null);
    }
    /// <summary>
    ///  Enable the Enhanced Video module in the pipeline.
    /// </summary>
    /// <param name="name"> The module name.</param>
    /// <returns> PXCM_STATUS_NO_ERROR        Successful execution.</returns>
    public pxcmStatus EnableEnhancedVideo(String name)
    {
        return EnableModule(PXCMEnhancedVideo.CUID, name);
    }

    /// <summary>
    /// 	Enable the Enhanced Videogrphy module in the pipeline.
    /// </summary>
    /// <returns> PXCM_STATUS_NO_ERROR		Successful execution.</returns>
    public pxcmStatus EnableEnhancedVideo()
    {
        return EnableEnhancedVideo(null);
    }


    /// <summary>
    /// 	Enable the 3D scanning module in the pipeline.
    /// </summary>
    /// <param name="name"> The module name.</param>
    /// <returns> PXCM_STATUS_NO_ERROR		Successful execution.</returns>
    public pxcmStatus Enable3DScan()
    {
        return Enable3DScan(null);
    }

    /// <summary>
    /// 	Enable the object tracking module in the pipeline.
    /// </summary>
    /// <param name="name"> The module name.</param>
    /// <returns> PXCM_STATUS_NO_ERROR		Successful execution.</returns>
    public pxcmStatus EnableTracker(String name)
    {
        return EnableModule(PXCMTracker.CUID, name);
    }

    /// <summary>
    /// 	Enable the object tracking module in the pipeline.
    /// </summary>
    /// <param name="name"> The module name.</param>
    /// <returns> PXCM_STATUS_NO_ERROR		Successful execution.</returns>
    public pxcmStatus EnableTracker()
    {
        return EnableTracker(null);
    }

    /// <summary>
    /// 	Enable the face module in the pipeline.
    /// </summary>
    /// <param name="name"> The module name.</param>
    /// <returns> PXCM_STATUS_NO_ERROR		Successful execution.</returns>
    public pxcmStatus EnableFace(String name)
    {
        return EnableModule(PXCMFaceModule.CUID, name);
    }

    /// <summary>
    /// 	Enable the face module in the pipeline.
    /// </summary>
    /// <returns> PXCM_STATUS_NO_ERROR		Successful execution.</returns>
    public pxcmStatus EnableFace()
    {
        return EnableFace(null);
    }

    /// <summary>
    /// 	Enable the touchless controller module in the pipeline.
    /// </summary>
    /// <param name="name"> The module name.</param>
    /// <returns> PXCM_STATUS_NO_ERROR	Successful execution.</returns>
    public pxcmStatus EnableTouchlessController(String name)
    {
        return EnableModule(PXCMTouchlessController.CUID, name);
    }

    /// <summary>
    /// 	Enable the touchless controller module in the pipeline.
    /// </summary>
    /// <returns> PXCM_STATUS_NO_ERROR	Successful execution.</returns>
    public pxcmStatus EnableTouchlessController()
    {
        return EnableTouchlessController(null);
    }

    /// <summary>
    /// 	Enable the hand module in the pipeline.
    /// </summary>
    /// <param name="name"> The module name.</param>
    /// <returns> PXCM_STATUS_NO_ERROR	Successful execution.</returns>
    public pxcmStatus EnableHand(String name)
    {
        return EnableModule(PXCMHandModule.CUID, name);
    }

    /// <summary>
    /// 	Enable the hand module in the pipeline.
    /// </summary>
    /// <returns> PXCM_STATUS_NO_ERROR	Successful execution.</returns>
    public pxcmStatus EnableHand()
    {
        return EnableHand(null);
    }


    /// <summary>
    /// 	Enable the hand module in the pipeline.
    /// </summary>
    /// <param name="name"> The module name.</param>
    /// <returns> PXCM_STATUS_NO_ERROR	Successful execution.</returns>
    public pxcmStatus EnableHandCursor(String name)
    {
        return EnableModule(PXCMHandCursorModule.CUID, name);
    }

    /// <summary>
    /// 	Enable the hand module in the pipeline.
    /// </summary>
    /// <returns> PXCM_STATUS_NO_ERROR	Successful execution.</returns>
    public pxcmStatus EnableHandCursor()
    {
        return EnableHandCursor(null);
    }


    /// <summary>
    /// 	Enable the Blob module in the pipeline.
    /// </summary>
    /// <param name="name"> The module name.</param>
    /// <returns> PXCM_STATUS_NO_ERROR	Successful execution.</returns>
    public pxcmStatus EnableBlob(String name)
    {
        return EnableModule(PXCMBlobModule.CUID, name);
    }

    /// <summary>
    /// 	Enable the Blob module in the pipeline.
    /// </summary>
    /// <returns> PXCM_STATUS_NO_ERROR	Successful execution.</returns>
    public pxcmStatus EnableBlob()
    {
        return EnableBlob(null);
    }

    /// <summary>
    /// 	Enable the ObjectRecognition module in the pipeline.
    /// </summary>
    /// <param name="name"> The module name.</param>
    /// <returns> PXCM_STATUS_NO_ERROR	Successful execution.</returns>
    public pxcmStatus EnableObjectRecognition(String name)
    {
        return EnableModule(PXCMObjectRecognitionModule.CUID, name);
    }

    /// <summary>
    /// 	Enable the ObjectRecognition module in the pipeline.
    /// </summary>
    /// <returns> PXCM_STATUS_NO_ERROR	Successful execution.</returns>
    public pxcmStatus EnableObjectRecognition()
    {
        return EnableObjectRecognition(null);
    }

    /// <summary>
    /// 	Enable the PersonTracking module in the pipeline.
    /// </summary>
    /// <param name="name"> The module name.</param>
    /// <returns> PXCM_STATUS_NO_ERROR	Successful execution.</returns>
    public pxcmStatus EnablePersonTracking(String name)
    {
        return EnableModule(PXCMPersonTrackingModule.CUID, name);
    }

    /// <summary>
    /// 	Enable the PersonTracking module in the pipeline.
    /// </summary>
    /// <returns> PXCM_STATUS_NO_ERROR	Successful execution.</returns>
    public pxcmStatus EnablePersonTracking()
    {
        return EnablePersonTracking(null);
    }

    /// <summary>
    /// 	Pause/Resume the execution of the specified module.
    /// </summary>
    /// <param name="cuid"> The module identifier. This is usually the interface identifier.</param>
    /// <param name="pause"> If true, pause the module. Otherwise, resume the module.</param>
    public void PauseModule(Int32 cuid, Boolean pause)
    {
        PXCMSenseManager_PauseModule(instance, cuid, pause);
    }

    /// <summary>
    /// 	Pause/Resume the execution of the user segmentation module.
    /// </summary>
    /// <param name="pause"> If true, pause the module. Otherwise, resume the module.</param>
    public void Pause3DSeg(Boolean pause)
    {
        PauseModule(PXCM3DSeg.CUID, pause);
    }

    /// <summary>
    ///    Pause/Resume the execution of the Scene Perception module.
    /// </summary>
    /// <param name="pause"> If true, pause the module. Otherwise, resume the module.</param>
    public void PauseScenePerception(Boolean pause)
    {
        PauseModule(PXCMScenePerception.CUID, pause);
    }

    /// <summary>
    /// 	Pause/Resume the execution of the object tracking module.
    /// </summary>
    /// <param name="pause"> If true, pause the module. Otherwise, resume the module.</param>
    public void PauseTracker(Boolean pause)
    {
        PauseModule(PXCMTracker.CUID, pause);
    }

    /// <summary>
    /// 	Pause/Resume the execution of the face module.
    /// </summary>
    /// <param name="pause"> If true, pause the module. Otherwise, resume the module.</param>
    public void PauseFace(Boolean pause)
    {
        PauseModule(PXCMFaceModule.CUID, pause);
    }

    /// <summary>
    /// 	Pause/Resume the execution of the Enhanced Videography module.
    /// </summary>
    /// <param name="pause"> If true, pause the module. Otherwise, resume the module.</param>
    public void PauseEnhancedVideo(Boolean pause)
    {
        PauseModule(PXCMEnhancedVideo.CUID, pause);
    }

    /// <summary>
    /// 	Pause/Resume the execution of the touchless controller module.
    /// </summary>
    /// <param name="pause"> If true, pause the module. Otherwise, resume the module.</param>
    public void PauseTouchlessController(Boolean pause)
    {
        PauseModule(PXCMTouchlessController.CUID, pause);
    }

    /// <summary>
    /// 	Pause/Resume the execution of the hand module.
    /// </summary>
    /// <param name="pause"> If true, pause the module. Otherwise, resume the module.</param>
    public void PauseHand(Boolean pause)
    {
        PauseModule(PXCMHandModule.CUID, pause);
    }

    /// <summary>
    /// 	Pause/Resume the execution of the hand module.
    /// </summary>
    /// <param name="pause"> If true, pause the module. Otherwise, resume the module.</param>
    public void PauseHandCursor(Boolean pause)
    {
        PauseModule(PXCMHandCursorModule.CUID, pause);
    }

    /// <summary>
    /// 	Pause/Resume the execution of the Blob module.
    /// </summary>
    /// <param name="pause"> If true, pause the module. Otherwise, resume the module.</param>
    public void PauseBlob(Boolean pause)
    {
        PauseModule(PXCMBlobModule.CUID, pause);
    }

    /// <summary>
    /// 	Pause/Resume the execution of the ObjectRecognition module.
    /// </summary>
    /// <param name="pause"> If true, pause the module. Otherwise, resume the module.</param>
    public void PauseObjectRecognition(Boolean pause)
    {
        PauseModule(PXCMObjectRecognitionModule.CUID, pause);
    }

    /// <summary>
    /// 	Pause/Resume the execution of the PersonTracking module.
    /// </summary>
    /// <param name="pause"> If true, pause the module. Otherwise, resume the module.</param>
    public void PausePersonTracking(Boolean pause)
    {
        PauseModule(PXCMPersonTrackingModule.CUID, pause);
    }

    /// <summary>
    /// 	Create an instance of the PXCSenseManager instance.
    /// </summary>
    /// <returns> The PXCMSenseManager instance.</returns>
    public static PXCMSenseManager CreateInstance()
    {
        IntPtr senseManager2 = PXCMSenseManager_CreateInstance();
        PXCMSenseManager senseManager = (senseManager2 != IntPtr.Zero) ? new PXCMSenseManager(senseManager2, true) : null;
        if (senseManager == null) return senseManager;

        try
        {
            PXCMSession session = senseManager.QuerySession();
            if (session == null) return senseManager;

            try
            {
                PXCMMetadata md = session.QueryInstance<PXCMMetadata>();
                if (md == null) return senseManager;

                string frameworkName = null;
#if PH_CS
					frameworkName = "CSharp";
#elif PH_UNITY
                frameworkName = "Unity";
#endif
                if (!string.IsNullOrEmpty(frameworkName))
                {
                    /* Unity Sample Info */
                    if (frameworkName.Equals("Unity"))
                    {
                        Assembly[] appDomainAssemblies = AppDomain.CurrentDomain.GetAssemblies();
                        if (appDomainAssemblies != null && appDomainAssemblies.Length > 0)
                        {
                            for (int i = 0; i < appDomainAssemblies.Length; i++)
                            {
                                if (appDomainAssemblies[i].GetName().Name.Contains("Assembly-CSharp"))
                                {
                                    string[] s = appDomainAssemblies[i].Location.Split('\\');
                                    string sampleProjectName = s[s.Length - 4];
                                    sampleProjectName += " Unity";
                                    md.AttachBuffer(PXCMSession.METADATA_FEEDBACK_SAMPLE_INFO, System.Text.Encoding.Unicode.GetBytes(sampleProjectName));
                                    break;
                                }
                            }
                        }
                    }

                    md.AttachBuffer(PXCMSession.METADATA_FEEDBACK_FRAMEWORK_INFO, System.Text.Encoding.Unicode.GetBytes(frameworkName));
                }
            }
            catch (Exception)
            {
                session.Dispose();
            }
        }
        catch { }
        return senseManager;
    }

    internal PXCMSenseManager(IntPtr instance, Boolean delete)
        : base(instance, delete)
    {
    }

};

#if RSSDK_IN_NAMESPACE
}
#endif
