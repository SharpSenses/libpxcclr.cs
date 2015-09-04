/********\***********************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2013 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

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
            /**
                @brief The SenseManager calls back this function when there is a device connection or
                    disconnection. During initialization, the SenseManager callbacks this function when 
                    openning or closing any capture devices.
                @param[in] device		The video device instance.
                @param[in] connected	The device connection status.
                @return The return status is ignored during the PXCSenseManager initialization. During
                    streaming, the SenseManager aborts the execution pipeline if the status is an error.
            */
            public delegate pxcmStatus OnConnectDelegate(PXCMCapture.Device device, Boolean connected);

            /**
                @brief The SenseManager calls back this function during initialization after each device 
                configuration is set.
                @param[in] mid		The module identifier. Usually this is the interface identifier, or PXCMCapture.CUID+n for raw video streams. 
                @param[in] module	The module instance, or NULL for raw video streams.
                @return The SenseManager aborts the execution pipeline if the status is an error.
            */
            public delegate pxcmStatus OnModuleSetProfileDelegate(Int32 mid, PXCMBase module);

            /**
                @brief The SenseManager calls back this function after a module completed processing the frame data.
                @param[in] mid		The module identifier. Usually this is the interface identifier. 
                @param[in] module	The module instance.
                @return The SenseManager aborts the execution pipeline if the status is an error.
            */
            public delegate pxcmStatus OnModuleProcessedFrameDelegate(Int32 mid, PXCMBase module, PXCMCapture.Sample sample);

            /**
                @brief The SenseManager calls back this function when raw video streams (explicitly requested) are available.
                @param[in] mid		The module identifier. Usually this is the interface identifier. 
                @param[in] sample	The sample from capture device
                @return The SenseManager aborts the execution pipeline if the status is an error.
            */
            public delegate pxcmStatus OnNewSampleDelegate(Int32 mid, PXCMCapture.Sample sample);

            /**
                @brief The SenseManager calls back this function when streaming loop in StreamFrames() function terminated.
                @param[in] sts         The error code
            */
            public delegate void OnStatusDelegate(Int32 mid, pxcmStatus sts);

            public OnConnectDelegate onConnect;
            public OnModuleSetProfileDelegate onModuleSetProfile;
            public OnModuleProcessedFrameDelegate onModuleProcessedFrame;
            public OnNewSampleDelegate onNewSample;
            public OnStatusDelegate onStatus;
        };

        /**
            @brief	Return the PXCSession instance. Internally managed. Do not release the instance.
            The session instance is managed internally by the SenseManager. Do not release the session instance.
            @return The PXCMSession instance.
        */
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

        /**
            @brief	Return the PXCMCaptureManager instance. Internally managed. Do not release the instance.
            The instance is managed internally by the SenseManager. Do not release the instance.
            @return The PXCMCaptureManager instance.
        */
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

        /**
            @brief	Return the captured sample for the specified module or explicitly requested streams. 
            For modules, use mid=module interface identifier. 
            For explictly requested streams via multiple calls to EnableStream(s), use mid=PXCMCapture.CUID+0,1,2... 
            The captured sample is managed internally by the SenseManager. Do not release the instance.
            @param[in] mid		The module identifier. Usually this is the interface identifier, or PXCMCapture.CUID+n for raw video streams.
            @return The sample instance, or null if the captured sample is not available.
        */
        public PXCMCapture.Sample QuerySample()
        {
            return QuerySample(0);
        }

        /**
            @brief	Return the captured sample for the user segmentation module.
            The captured sample is managed internally by the SenseManager. Do not release the sample.
            @return The sample instance, or NULL if the captured sample is not available.
        */
        public PXCMCapture.Sample Query3DSegSample()
        {
            return QuerySample(PXCM3DSeg.CUID);
        }

        /**
            @brief	Return the captured sample for the scene perception module.
            The captured sample is managed internally by the SenseManager. Do not release the sample.
            @return The sample instance, or NULL if the captured sample is not available.
        */
        public PXCMCapture.Sample QueryScenePerceptionSample()
        {
            return QuerySample(PXCMScenePerception.CUID);
        }

        /**
		@brief	  Return the captured sample for the enhanced Videography module.
		The captured sample is managed internally by the SenseManager. Do not release the sample.
		@return The sample instance, or NULL if the captured sample is not available.
	    */

        public PXCMCapture.Sample QueryEnhancedVideoSample()
        {
            return QuerySample(PXCMEnhancedVideo.CUID);
        }

        /**
            @brief	Return the captured sample for the object tracking module.
            The captured sample is managed internally by the SenseManager. Do not release the sample.
            @return The sample instance, or NULL if the captured sample is not available.
        */
        public PXCMCapture.Sample QueryTrackerSample()
        {
            return QuerySample(PXCMTracker.CUID);
        }

        /**
            @brief	Return the captured sample for the face module.
            The captured sample is managed internally by the SenseManager. Do not release the sample.
            @return The sample instance, or NULL if the captured sample is not available.
        */
        public PXCMCapture.Sample QueryFaceSample()
        {
            return QuerySample(PXCMFaceModule.CUID);
        }

        /**
            @brief	Return the captured sample for the hand module.
            The captured sample is managed internally by the SenseManager. Do not release the sample.
            @return The sample instance, or NULL if the captured sample is not available.
        */
        public PXCMCapture.Sample QueryHandSample()
        {
            return QuerySample(PXCMHandModule.CUID);
        }

        /**
            @brief	Return the captured sample for the emotion module.
            The captured sample is managed internally by the SenseManager. Do not release the sample.
            @return The sample instance, or null if the captured sample is not available.
        */
        public PXCMCapture.Sample QueryEmotionSample()
        {
            return QuerySample(PXCMEmotion.CUID);
        }

        /**
          @brief	Return the module instance. Between AcquireFrame/ReleaseFrame, the function returns
          NULL if the specified module hasn't completed processing the current frame of image data.
          The instance is managed internally by the SenseManager. Do not release the instance.
          @param[in] mid		The module identifier. Usually this is the interface identifier.
          @return The module instance.
       */
        public PXCMBase QueryModule(Int32 mid)
        {
            if (mid == PXCMHandModule.CUID) return QueryFace();
            if (mid == PXCMFaceModule.CUID) return QueryHand();
            if (mid == PXCMTouchlessController.CUID) return QueryTouchlessController();

            IntPtr pbase = PXCMSenseManager_QueryModule(instance, mid);
            if (pbase == IntPtr.Zero) return null;
            return PXCMBase.IntPtr2PXCMBase(pbase, mid);
        }

        /**
            @brief	Return the user segmentation module instance. Between AcquireFrame/ReleaseFrame, the function returns
            NULL if the specified module hasn't completed processing the current frame of image data.
            The instance is managed internally by the SenseManager. Do not release the instance.
            @return The module instance.
        */
        public PXCM3DSeg Query3DSeg()
        {
            IntPtr module = PXCMSenseManager_QueryModule(instance, PXCM3DSeg.CUID);
            if (module == IntPtr.Zero) return null;
            return new PXCM3DSeg(module, false);
        }

        /**
            @brief	Return the 3D scanning module instance. Between AcquireFrame/ReleaseFrame, the function returns
            NULL if the specified module hasn't completed processing the current frame of image data.
            The instance is managed internally by the SenseManager. Do not release the instance.
            @return The module instance.
        */
        public PXCM3DScan Query3DScan()
        {
            IntPtr module = PXCMSenseManager_QueryModule(instance, PXCM3DScan.CUID);
            if (module == IntPtr.Zero) return null;
            return new PXCM3DScan(module, false);
        }

        /**
            @brief  Return the Scene Perception module instance. Between AcquireFrame/ReleaseFrame, 
                the function returns null if the specified module hasn't completed processing the current 
                frame of image data. The instance is managed internally by the SenseManager. 
                Do not release the instance.
            @return The module instance.
        */
        public PXCMScenePerception QueryScenePerception()
        {
            IntPtr module = PXCMSenseManager_QueryModule(instance, PXCMScenePerception.CUID);
            if (module == IntPtr.Zero) return null;
            return new PXCMScenePerception(module, false);
        }

        /**
            @brief	Return the object tracking module instance. Between AcquireFrame/ReleaseFrame, the function returns
            NULL if the specified module hasn't completed processing the current frame of image data.
            The instance is managed internally by the SenseManager. Do not release the instance.
            @return The module instance.
        */
        public PXCMTracker QueryTracker()
        {
            IntPtr module = PXCMSenseManager_QueryModule(instance, PXCMTracker.CUID);
            if (module == IntPtr.Zero) return null;
            return new PXCMTracker(module, false);
        }

        internal PXCMFaceConfiguration.EventMaps faceEvents = new PXCMFaceConfiguration.EventMaps();

        /**
            @brief	Return the Face module instance. Between AcquireFrame/ReleaseFrame, the function returns
            NULL if the specified module hasn't completed processing the current frame of image data.
            The instance is managed internally by the SenseManager. Do not release the instance.
            @return The module instance.
        */
        public PXCMFaceModule QueryFace()
        {
            IntPtr module = PXCMSenseManager_QueryModule(instance, PXCMFaceModule.CUID);
            if (module == IntPtr.Zero) return null;
            return new PXCMFaceModule(faceEvents, module, false);
        }

        /**
            @brief	Return the emotion module instance. Between AcquireFrame/ReleaseFrame, the function returns
            NULL if the specified module hasn't completed processing the current frame of image data.
            The instance is managed internally by the SenseManager. Do not release the instance.
            @return The module instance.
        */
        public PXCMEmotion QueryEmotion()
        {
            IntPtr module = PXCMSenseManager_QueryModule(instance, PXCMEmotion.CUID);
            if (module == IntPtr.Zero) return null;
            return new PXCMEmotion(module, false);
        }

        internal PXCMTouchlessController.EventMap tcEvents = new PXCMTouchlessController.EventMap();

        /**
            @brief	Return the Touchless module instance. Between AcquireFrame/ReleaseFrame, the function returns
            NULL if the specified module hasn't completed processing the current frame of image data.
            The instance is managed internally by the SenseManager. Do not release the instance.
            @return The module instance.
        */
        public PXCMTouchlessController QueryTouchlessController()
        {
            IntPtr module = PXCMSenseManager_QueryModule(instance, PXCMTouchlessController.CUID);
            if (module == IntPtr.Zero) return null;
            return new PXCMTouchlessController(tcEvents, module, false);
        }

        internal PXCMHandConfiguration.EventMaps handEvents = new PXCMHandConfiguration.EventMaps();

        /**
           @brief	Return the hand module instance. Between AcquireFrame/ReleaseFrame, the function returns
           NULL if the specified module hasn't completed processing the current frame of image data.
           The instance is managed internally by the SenseManager. Do not release the instance.
           @return The module instance.
        */
        public PXCMHandModule QueryHand()
        {
            IntPtr module = PXCMSenseManager_QueryModule(instance, PXCMHandModule.CUID);
            if (module == IntPtr.Zero) return null;
            return new PXCMHandModule(handEvents, module, false);
        }

        /**
            @brief	Return the Blob module instance. Between AcquireFrame/ReleaseFrame, the function returns
            NULL if the specified module hasn't completed processing the current frame of image data.
            The instance is managed internally by the SenseManager. Do not release the instance.
            @return The module instance.
        */
        public PXCMBlobModule QueryBlob()
        {
            IntPtr module = PXCMSenseManager_QueryModule(instance, PXCMBlobModule.CUID);
            if (module == IntPtr.Zero) return null;
            return new PXCMBlobModule(module, false);
        }

        /**
       @brief	Return the Enhanced Videography module instance. Between AcquireFrame/ReleaseFrame, the function returns
       NULL if the specified module hasn't completed processing the current frame of image data.
       The instance is managed internally by the SenseManager. Do not release the instance.
       @return The module instance.
       */
        public PXCMEnhancedVideo QueryEnhancedVideo()
        {
            IntPtr module = PXCMSenseManager_QueryModule(instance, PXCMEnhancedVideo.CUID);
            if (module == IntPtr.Zero) return null;
            return new PXCMEnhancedVideo(module, false);
        }

        /**
        @brief	Initialize the SenseManager pipeline for streaming. The application must enable raw 
        streams or algorithm modules before this function.
        @return PXCM_STATUS_NO_ERROR		Successful execution.
        */
        public pxcmStatus Init()
        {
            return Init(null);
        }

        /**
            @brief	Stream frames from the capture module to the algorithm modules. The application must 
            initialize the pipeline before calling this function. If blocking, the function blocks until
            the streaming stops (upon any capture device error or any callback function returns any error.
            If non-blocking, the function returns immediately while running streaming in a thread.
            @param[in]	blocking			The blocking status.
            @return PXCM_STATUS_NO_ERROR	Successful execution.
        */
        public bool IsConnected()
        {
            return PXCMSenseManager_IsConnected(instance);
        }

        /**
            @brief	This function starts streaming and waits until certain events occur. If ifall=true, 
            the function blocks until all samples are ready and the modules completed processing the samples.
            If ifall=false, the function blocks until any of the mentioned is ready. The SenseManager 
            pipeline pauses at this point for the application to retrieve the processed module data, until 
            the application calls ReleaseFrame.
            AcquireFrame/ReleaseFrame are not compatible with StreamFrames. Run the SenseManager in the pulling
            mode with AcquireFrame/ReleaseFrame, or the callback mode with StreamFrames.
            @param[in]	ifall				If true, wait for all modules to complete processing the data.
            @param[in]	timeout				The time out value in milliseconds.
            @return PXCM_STATUS_NO_ERROR	Successful execution.
        */
        public pxcmStatus AcquireFrame(Boolean ifall, Int32 timeout)
        {
            return PXCMSenseManager_AcquireFrame(instance, ifall, timeout);
        }

        /**
            @brief	This function starts streaming and waits until certain events occur. If ifall=true, 
            the function blocks until all samples are ready and the modules completed processing the samples.
            If ifall=false, the function blocks until any of the mentioned is ready. The SenseManager 
            pipeline pauses at this point for the application to retrieve the processed module data, until 
            the application calls ReleaseFrame.
            AcquireFrame/ReleaseFrame are not compatible with StreamFrames. Run the SenseManager in the pulling
            mode with AcquireFrame/ReleaseFrame, or the callback mode with StreamFrames.
            @param[in]	ifall				If true, wait for all modules to complete processing the data.
            @return PXCM_STATUS_NO_ERROR	Successful execution.
        */
        public pxcmStatus AcquireFrame(Boolean ifall)
        {
            return AcquireFrame(ifall, TIMEOUT_INFINITE);
        }

        /**
            @brief	This function starts streaming and waits until certain events occur. If ifall=true, 
            the function blocks until all samples are ready and the modules completed processing the samples.
            If ifall=false, the function blocks until any of the mentioned is ready. The SenseManager 
            pipeline pauses at this point for the application to retrieve the processed module data, until 
            the application calls ReleaseFrame.
            AcquireFrame/ReleaseFrame are not compatible with StreamFrames. Run the SenseManager in the pulling
            mode with AcquireFrame/ReleaseFrame, or the callback mode with StreamFrames..
            @return PXCM_STATUS_NO_ERROR	Successful execution.
        */
        public pxcmStatus AcquireFrame()
        {
            return AcquireFrame(true);
        }

        /**
            @brief	This function resumes streaming after AcquireFrame.
            AcquireFrame/ReleaseFrame are not compatible with StreamFrames. Run the SenseManager in the pulling
            mode with AcquireFrame/ReleaseFrame, or the callback mode with StreamFrames.
        */
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

        /**
            @brief	Explicitly request to stream the specified raw stream. If specified more than one stream, 
            SenseManager will synchronize these streams. If called multiple times, the function treats each
            stream request as independent (unaligned). The stream identifier is PXCCapture.CUID+n.
            @param[in] type					The stream type.
            @param[in] width				stream width.
            @param[in] height				stream height.
            @return PXC_STATUS_NO_ERROR		Successful execution.
        */
        public pxcmStatus EnableStream(PXCMCapture.StreamType type, Int32 width, Int32 height)
        {
            return EnableStream(type, width, height, 0);
        }

        /**
            @brief	Explicitly request to stream the specified raw stream. If specified more than one stream, 
            SenseManager will synchronize these streams. If called multiple times, the function treats each
            stream request as independent (unaligned). The stream identifier is PXCMCapture.CUID+n.
            @param[in] type					The stream type.
            @param[in] width				stream width.
            @param[in] height				stream height.
            @param[in] fps					stream frame rate.
            @return PXC_STATUS_NO_ERROR		Successful execution.
        */
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

        /**
            @brief	Enable a module in the pipeline.
            @param[in] mid					The module identifier. This is usually the interface identifier.
            @param[in] mdesc				The module descriptor.
            @return PXCM_STATUS_NO_ERROR	Successful execution.
        */
        public pxcmStatus EnableModule(Int32 mid, PXCMSession.ImplDesc mdesc)
        {
            return PXCMSenseManager_EnableModule(instance, mid, mdesc);
        }

        /**
            @brief	Enable the face module in the pipeline.
            @param[in] name		The optional module name.
            @param[in] mid					The module identifier. This is usually the interface identifier.
            @return PXCM_STATUS_NO_ERROR	Successful execution.
        */
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

        /**
            @brief	Enable the user segmentation module in the pipeline.
            @param[in] name		The module name.
            @return PXCM_STATUS_NO_ERROR		Successful execution.
        */
        public pxcmStatus Enable3DSeg(String name)
        {
            return EnableModule(PXCM3DSeg.CUID, name);
        }

        /**
            @brief	Enable the user segmentation module in the pipeline.
            @param[in] name		The module name.
            @return PXCM_STATUS_NO_ERROR		Successful execution.
        */
        public pxcmStatus Enable3DSeg()
        {
            return Enable3DSeg(null);
        }

        /**
            @brief	Enable the 3D scanning module in the pipeline.
            @param[in] name		The module name.
            @return PXCM_STATUS_NO_ERROR		Successful execution.
        */
        public pxcmStatus Enable3DScan(String name)
        {
            return EnableModule(PXCM3DScan.CUID, name);
        }


        /**
            @brief  Enable the Scene Perception module in the pipeline.
            @param[in] name		The module name.
            @return PXCM_STATUS_NO_ERROR        Successful execution.
        */
        public pxcmStatus EnableScenePerception(String name)
        {
            return EnableModule(PXCMScenePerception.CUID, name);
        }

        /**
            @brief  Enable the Scene Perception module in the pipeline.
            @return PXCM_STATUS_NO_ERROR        Successful execution.
        */
        public pxcmStatus EnableScenePerception()
        {
            return EnableScenePerception(null);
        }
        /**
          @brief  Enable the Enhanced Video module in the pipeline.
          @param[in] name		The module name.
          @return PXCM_STATUS_NO_ERROR        Successful execution.
      */
       
        public pxcmStatus EnableEnhancedVideo(String name)
        {
            return EnableModule(PXCMEnhancedVideo.CUID, name);
        }

        /**
             @brief	Enable the Enhanced Videogrphy module in the pipeline.
             @return PXCM_STATUS_NO_ERROR		Successful execution.
        */
        public pxcmStatus EnableEnhancedVideo()
        {
            return EnableEnhancedVideo(null);
        }


        /**
            @brief	Enable the 3D scanning module in the pipeline.
            @param[in] name		The module name.
            @return PXCM_STATUS_NO_ERROR		Successful execution.
        */
        public pxcmStatus Enable3DScan()
        {
            return Enable3DScan(null);
        }

        /**
            @brief	Enable the object tracking module in the pipeline.
            @param[in] name		The module name.
            @return PXCM_STATUS_NO_ERROR		Successful execution.
        */
        public pxcmStatus EnableTracker(String name)
        {
            return EnableModule(PXCMTracker.CUID, name);
        }

        /**
            @brief	Enable the object tracking module in the pipeline.
            @param[in] name		The module name.
            @return PXCM_STATUS_NO_ERROR		Successful execution.
        */
        public pxcmStatus EnableTracker()
        {
            return EnableTracker(null);
        }

        /**
            @brief	Enable the face module in the pipeline.
            @param[in] name		The module name.
            @return PXCM_STATUS_NO_ERROR		Successful execution.
        */
        public pxcmStatus EnableFace(String name)
        {
            return EnableModule(PXCMFaceModule.CUID, name);
        }

        /**
            @brief	Enable the face module in the pipeline.
            @return PXCM_STATUS_NO_ERROR		Successful execution.
        */
        public pxcmStatus EnableFace()
        {
            return EnableFace(null);
        }

        /**
            @brief	Enable the emotion module in the pipeline.
            @param[in] name		The module name.
            @return PXCM_STATUS_NO_ERROR	Successful execution.
        */
        public pxcmStatus EnableEmotion(String name)
        {
            return EnableModule(PXCMEmotion.CUID, name);
        }

        /**
            @brief	Enable the emotion module in the pipeline.
            @return PXCM_STATUS_NO_ERROR	Successful execution.
        */
        public pxcmStatus EnableEmotion()
        {
            return EnableEmotion(null);
        }

        /**
            @brief	Enable the touchless controller module in the pipeline.
            @param[in] name		The module name. 
            @return PXCM_STATUS_NO_ERROR	Successful execution.
        */
        public pxcmStatus EnableTouchlessController(String name)
        {
            return EnableModule(PXCMTouchlessController.CUID, name);
        }

        /**
            @brief	Enable the touchless controller module in the pipeline.
            @return PXCM_STATUS_NO_ERROR	Successful execution.
        */
        public pxcmStatus EnableTouchlessController()
        {
            return EnableTouchlessController(null);
        }

        /**
            @brief	Enable the hand module in the pipeline.
            @param[in] name		The module name.
            @return PXCM_STATUS_NO_ERROR	Successful execution.
        */
        public pxcmStatus EnableHand(String name)
        {
            return EnableModule(PXCMHandModule.CUID, name);
        }

        /**
           @brief	Enable the hand module in the pipeline.
           @return PXCM_STATUS_NO_ERROR	Successful execution.
        */
        public pxcmStatus EnableHand()
        {
            return EnableHand(null);
        }

        /**
            @brief	Enable the Blob module in the pipeline.
            @param[in] name		The module name.
            @return PXCM_STATUS_NO_ERROR	Successful execution.
        */
        public pxcmStatus EnableBlob(String name)
        {
            return EnableModule(PXCMBlobModule.CUID, name);
        }

        /**
           @brief	Enable the Blob module in the pipeline.
           @return PXCM_STATUS_NO_ERROR	Successful execution.
        */
        public pxcmStatus EnableBlob()
        {
            return EnableBlob(null);
        }

        /**
            @brief	Pause/Resume the execution of the specified module.
            @param[in] cuid		The module identifier. This is usually the interface identifier.
            @param[in] pause	If true, pause the module. Otherwise, resume the module.
        */
        public void PauseModule(Int32 cuid, Boolean pause)
        {
            PXCMSenseManager_PauseModule(instance, cuid, pause);
        }

        /**
            @brief	Pause/Resume the execution of the user segmentation module.
            @param[in] pause	If true, pause the module. Otherwise, resume the module.
        */
        public void Pause3DSeg(Boolean pause)
        {
            PauseModule(PXCM3DSeg.CUID, pause);
        }

        /**
            @brief    Pause/Resume the execution of the Scene Perception module.
            @param[in] pause        If true, pause the module. Otherwise, resume the module.
        */
        public void PauseScenePerception(Boolean pause)
        {
            PauseModule(PXCMScenePerception.CUID, pause);
        }

        /**
            @brief	Pause/Resume the execution of the object tracking module.
            @param[in] pause	If true, pause the module. Otherwise, resume the module.
        */
        public void PauseTracker(Boolean pause)
        {
            PauseModule(PXCMTracker.CUID, pause);
        }

        /**
            @brief	Pause/Resume the execution of the face module.
            @param[in] pause	If true, pause the module. Otherwise, resume the module.
        */
        public void PauseFace(Boolean pause)
        {
            PauseModule(PXCMFaceModule.CUID, pause);
        }

        /**
            @brief	Pause/Resume the execution of the emotion module.
            @param[in] pause	If true, pause the module. Otherwise, resume the module.
        */
        public void PauseEmotion(Boolean pause)
        {
            PauseModule(PXCMEmotion.CUID, pause);
        }

        /**
		@brief	Pause/Resume the execution of the Enhanced Videography module.
		@param[in] pause	If true, pause the module. Otherwise, resume the module.
	    */
        public void PauseEnhancedVideo(Boolean pause)
        {
            PauseModule(PXCMEnhancedVideo.CUID, pause);
        }

        /**
            @brief	Pause/Resume the execution of the touchless controller module.
            @param[in] pause	If true, pause the module. Otherwise, resume the module.
        */
        public void PauseTouchlessController(Boolean pause)
        {
            PauseModule(PXCMTouchlessController.CUID, pause);
        }

        /**
            @brief	Pause/Resume the execution of the hand module.
            @param[in] pause	If true, pause the module. Otherwise, resume the module.
        */
        public void PauseHand(Boolean pause)
        {
            PauseModule(PXCMHandModule.CUID, pause);
        }

        /**
            @brief	Pause/Resume the execution of the Blob module.
            @param[in] pause	If true, pause the module. Otherwise, resume the module.
        */
        public void PauseBlob(Boolean pause)
        {
            PauseModule(PXCMBlobModule.CUID, pause);
        }

        /**
            @brief	Create an instance of the PXCSenseManager instance.
            @return The PXCMSenseManager instance.
        */
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
                        md.AttachBuffer(1296451664, System.Text.Encoding.Unicode.GetBytes(frameworkName));
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