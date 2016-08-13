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

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

    public partial class PXCMSenseManager : PXCMBase
    {
        internal partial class HandlerDIR : IDisposable
        {
            [DllImport(PXCMBase.DLLNAME)]
            internal static extern IntPtr PXCMSenseManager_AllocHandlerDIR(HandlerSet hs);
            [DllImport(PXCMBase.DLLNAME)]
            internal static extern void PXCMSenseManager_FreeHandlerDIR(IntPtr hdir);

            internal delegate pxcmStatus OnConnectDIRDelegate(IntPtr device, Boolean connected);
            internal delegate pxcmStatus OnModuleSetProfileDIRDelegate(Int32 mid, IntPtr module);
            internal delegate pxcmStatus OnModuleProcessedFrameDIRDelegate(Int32 mid, IntPtr module, IntPtr images);
            internal delegate pxcmStatus OnNewSampleDIRDelegate(Int32 mid, IntPtr images);
            internal delegate void OnStatusDIRDelegate(Int32 mid, pxcmStatus sts);

            [StructLayout(LayoutKind.Sequential)]
            internal class HandlerSet
            {
                internal IntPtr onConnect;
                internal IntPtr onModuleSetProfile;
                internal IntPtr onModuleProcessedFrame;
                internal IntPtr onNewSample;
                internal IntPtr onStatus;
            };

            private PXCMSenseManager sm;
            private Handler handler;
            internal IntPtr dirUnmanaged;
            private List<GCHandle> gchandles;

            private pxcmStatus OnConnect(IntPtr device, Boolean connected)
            {
                PXCMCapture.Device device2 = new PXCMCapture.Device(device, false);
                return handler.onConnect(device2, connected);
            }

            private pxcmStatus OnModuleSetProfile(Int32 mid, IntPtr module)
            {
                if (mid == PXCMFaceModule.CUID)
                    return handler.onModuleSetProfile(mid, new PXCMFaceModule(sm.faceEvents, module, false));
                if (mid == PXCMHandModule.CUID)
                    return handler.onModuleSetProfile(mid, new PXCMHandModule(sm.handEvents, module, false));
                if (mid == PXCMHandCursorModule.CUID)
                    return handler.onModuleSetProfile(mid, new PXCMHandCursorModule(sm.cursorEvents, module, false));
                return handler.onModuleSetProfile(mid, PXCMBase.IntPtr2PXCMBase(module, mid));
            }

            private pxcmStatus OnModuleProcessedFrame(Int32 mid, IntPtr module, IntPtr images)
            {
                PXCMCapture.Sample sample = new PXCMCapture.Sample(images);
                if (mid == PXCMFaceModule.CUID)
                    return handler.onModuleProcessedFrame(mid, new PXCMFaceModule(sm.faceEvents, module, false), sample);
                if (mid == PXCMHandModule.CUID)
                    return handler.onModuleProcessedFrame(mid, new PXCMHandModule(sm.handEvents, module, false), sample);
                if (mid == PXCMHandCursorModule.CUID)
                    return handler.onModuleProcessedFrame(mid, new PXCMHandCursorModule(sm.cursorEvents, module, false), sample);
                return handler.onModuleProcessedFrame(mid, PXCMBase.IntPtr2PXCMBase(module, mid), sample);
            }

            private pxcmStatus OnNewSample(Int32 mid, IntPtr images)
            {
                PXCMCapture.Sample sample = new PXCMCapture.Sample(images);
                return handler.onNewSample(mid, sample);
            }

            private void OnStatus(Int32 mid, pxcmStatus sts)
            {
                handler.onStatus(mid, sts);
            }

            public void Dispose()
            {
                if (dirUnmanaged == IntPtr.Zero) return;

                PXCMSenseManager_FreeHandlerDIR(dirUnmanaged);
                dirUnmanaged = IntPtr.Zero;

                foreach (GCHandle gch in gchandles)
                {
                    if (gch.IsAllocated) gch.Free();
                }
                gchandles.Clear();
            }

            public HandlerDIR(PXCMSenseManager sm, Handler handler)
            {
                this.sm = sm;
                this.handler = handler;
                HandlerSet handlerSet = new HandlerSet();
                gchandles = new List<GCHandle>();

                if (handler.onConnect != null)
                {
                    OnConnectDIRDelegate dir = new OnConnectDIRDelegate(OnConnect);
                    gchandles.Add(GCHandle.Alloc(dir));
                    handlerSet.onConnect = Marshal.GetFunctionPointerForDelegate(dir);
                }
                if (handler.onModuleSetProfile != null)
                {
                    OnModuleSetProfileDIRDelegate dir = new OnModuleSetProfileDIRDelegate(OnModuleSetProfile);
                    gchandles.Add(GCHandle.Alloc(dir));
                    handlerSet.onModuleSetProfile = Marshal.GetFunctionPointerForDelegate(dir);
                }
                if (handler.onModuleProcessedFrame != null)
                {
                    OnModuleProcessedFrameDIRDelegate dir = new OnModuleProcessedFrameDIRDelegate(OnModuleProcessedFrame);
                    gchandles.Add(GCHandle.Alloc(dir));
                    handlerSet.onModuleProcessedFrame = Marshal.GetFunctionPointerForDelegate(dir);
                }
                if (handler.onNewSample != null)
                {
                    OnNewSampleDIRDelegate dir = new OnNewSampleDIRDelegate(OnNewSample);
                    gchandles.Add(GCHandle.Alloc(dir));
                    handlerSet.onNewSample = Marshal.GetFunctionPointerForDelegate(dir);
                }
                if (handler.onStatus != null)
                {
                    OnStatusDIRDelegate dir = new OnStatusDIRDelegate(OnStatus);
                    gchandles.Add(GCHandle.Alloc(dir));
                    handlerSet.onStatus = Marshal.GetFunctionPointerForDelegate(dir);
                }

                dirUnmanaged = PXCMSenseManager_AllocHandlerDIR(handlerSet);
            }

            ~HandlerDIR()
            {
                Dispose();
            }
        };

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCMSenseManager_QuerySession(IntPtr putil);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCMSenseManager_QueryCaptureManager(IntPtr putil);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCMSenseManager_QuerySample(IntPtr putil, Int32 mid);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCMSenseManager_QueryModule(IntPtr putil, Int32 mid);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMSenseManager_Init(IntPtr putil, IntPtr hdir);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMSenseManager_StreamFrames(IntPtr putil, [MarshalAs(UnmanagedType.Bool)] Boolean blocking);

        [DllImport(PXCMBase.DLLNAME)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern Boolean PXCMSenseManager_IsConnected(IntPtr putil);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMSenseManager_AcquireFrame(IntPtr putil,
            [MarshalAs(UnmanagedType.Bool)] Boolean ifall, Int32 timeout);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern void PXCMSenseManager_ReleaseFrame(IntPtr putil);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern void PXCMSenseManager_FlushFrame(IntPtr putil);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern void PXCMSenseManager_Close(IntPtr putil);

        /**
            @brief	This function closes the execution pipeline.
        */
        public void Close()
        {
            PXCMSenseManager_Close(instance);
            if (handlerDIR != null) handlerDIR.Dispose();
        }

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMSenseManager_EnableStreams(IntPtr putil, PXCMVideoModule.DataDesc sdesc);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMSenseManager_EnableModule(IntPtr putil, Int32 mid, PXCMSession.ImplDesc mdesc);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern void PXCMSenseManager_PauseModule(IntPtr putil, Int32 cuid, [MarshalAs(UnmanagedType.Bool)] Boolean pause);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCMSenseManager_CreateInstance();

        /// <summary>
        /// Initialize the SenseManager pipeline for streaming with callbacks. The application must 
        /// enable raw streams or algorithm modules before this function.
        /// </summary>
        /// <param name="handler">Optional callback instance. </param>
        /// <returns>PXC_STATUS_NO_ERROR		Successful execution.</returns>
        public pxcmStatus Init(Handler handler)
        {
            if (handler == null) return PXCMSenseManager_Init(instance, IntPtr.Zero);

            if (handlerDIR != null) handlerDIR.Dispose();
            handlerDIR = new HandlerDIR(this, handler);
            return PXCMSenseManager_Init(instance, handlerDIR.dirUnmanaged);
        }

        /// <summary>
        /// Stream frames from the capture module to the algorithm modules. The application must 
        /// initialize the pipeline before calling this function. AcquireFrame/ReleaseFrame are not compatible with StreamFrames. Run the SenseManager in the pulling
        /// mode with AcquireFrame/ReleaseFrame, or the callback mode with StreamFrames.
        /// </summary>
        /// <param name="blocking">True: the function blocks until the streaming stops (upon any capture device error or any callback function returns any error. 
        /// False: the function returns immediately while running streaming in a thread.</param>
        /// <returns>PXCM_STATUS_NO_ERROR	Successful execution.</returns>
        public pxcmStatus StreamFrames(Boolean blocking)
        {
            return PXCMSenseManager_StreamFrames(instance, blocking);
        }

        /// <summary>
        /// Return the captured sample for the specified module or explicitly/impl requested streams. 
        /// For modules, use mid=module interface identifier. 
        /// For explictly requested streams via multiple calls to EnableStream(s), use mid=PXCCapture::CUID+0,1,2... 
        /// The captured sample is managed internally by the SenseManager. Do not release the instance.
        /// </summary>
        /// <param name="mid">The module identifier. Usually this is the interface identifier, or PXCCapture::CUID+n for raw video streams.</param>
        /// <returns>The sample instance, or null if the captured sample is not available.</returns>
        public PXCMCapture.Sample QuerySample(Int32 mid)
        {
            IntPtr sample2 = PXCMSenseManager_QuerySample(instance, mid);
            if (sample2 == IntPtr.Zero) return null;
            return new PXCMCapture.Sample(sample2);
        }

        private HandlerDIR handlerDIR = null;
    };

#if RSSDK_IN_NAMESPACE
}
#endif
