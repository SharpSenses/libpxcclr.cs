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

    public partial class PXCMCaptureManager : PXCMBase
    {
        internal partial class HandlerDIR : IDisposable
        {
            [DllImport(PXCMBase.DLLNAME)]
            internal static extern IntPtr PXCMCaptureManager_AllocHandlerDIR(HandlerSet hs);
            [DllImport(PXCMBase.DLLNAME)]
            internal static extern void PXCMCaptureManager_FreeHandlerDIR(IntPtr hdir);

            internal delegate pxcmStatus OnCreateDeviceDIRDelegate(PXCMSession.ImplDesc desc, IntPtr device);
            internal delegate pxcmStatus OnSetupStreamsDIRDelegate(IntPtr device, PXCMCapture.StreamType types);
            internal delegate pxcmStatus OnNextDeviceDIRDelegate(IntPtr device);

            [StructLayout(LayoutKind.Sequential)]
            internal class HandlerSet
            {
                internal IntPtr onCreateDevice;
                internal IntPtr onSetupStreams;
                internal IntPtr onNextDevice;
            };

            private Handler handler;
            private List<GCHandle> gchandles;
            internal IntPtr dirUnmanaged;

            private pxcmStatus OnCreateDevice(PXCMSession.ImplDesc desc, IntPtr device)
            {
                PXCMCapture.Device device2 = new PXCMCapture.Device(device, false);
                return handler.onCreateDevice(desc, device2);
            }

            private pxcmStatus OnSetupStreams(IntPtr device, PXCMCapture.StreamType types)
            {
                PXCMCapture.Device device2 = new PXCMCapture.Device(device, false);
                return handler.onSetupStreams(device2, types);
            }

            private pxcmStatus OnNextDevice(IntPtr device)
            {
                PXCMCapture.Device device2 = new PXCMCapture.Device(device, false);
                return handler.onNextDevice(device2);
            }

            public HandlerDIR(Handler handler)
            {
                this.handler = handler;
                gchandles = new List<GCHandle>();
                if (handler != null)
                {
                    HandlerSet handlerSet = new HandlerSet();

                    if (handler.onCreateDevice != null)
                    {
                        OnCreateDeviceDIRDelegate dir = new OnCreateDeviceDIRDelegate(OnCreateDevice);
                        gchandles.Add(GCHandle.Alloc(dir));
                        handlerSet.onCreateDevice = Marshal.GetFunctionPointerForDelegate(dir);
                    }
                    if (handler.onSetupStreams != null)
                    {
                        OnSetupStreamsDIRDelegate dir = new OnSetupStreamsDIRDelegate(OnSetupStreams);
                        gchandles.Add(GCHandle.Alloc(dir));
                        handlerSet.onSetupStreams = Marshal.GetFunctionPointerForDelegate(dir);
                    }
                    if (handler.onNextDevice != null)
                    {
                        OnNextDeviceDIRDelegate dir = new OnNextDeviceDIRDelegate(OnNextDevice);
                        gchandles.Add(GCHandle.Alloc(dir));
                        handlerSet.onNextDevice = Marshal.GetFunctionPointerForDelegate(dir);
                    }

                    dirUnmanaged = PXCMCaptureManager_AllocHandlerDIR(handlerSet);
                }
            }

            ~HandlerDIR()
            {
                Dispose();
            }

            public void Dispose()
            {
                if (dirUnmanaged == IntPtr.Zero) return;

                PXCMCaptureManager_FreeHandlerDIR(dirUnmanaged);
                dirUnmanaged = IntPtr.Zero;

                foreach (GCHandle gch in gchandles)
                {
                    if (gch.IsAllocated) gch.Free();
                }
                gchandles.Clear();
            }
        };

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern void PXCMCaptureManager_FilterByDeviceInfo(IntPtr cutil, PXCMCapture.DeviceInfo dinfo);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern void PXCMCaptureManager_FilterByStreamProfiles(IntPtr cutil, PXCMCapture.Device.StreamProfileSet profiles);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMCaptureManager_RequestStreams(IntPtr cutil, Int32 mid, IntPtr inputs);

        /**
            @brief	Add the module input needs to the CaptureManager device search. The application must call
            this function for all modules before the LocalStreams function, where the CaptureManager performs
            the device match.
            @param[in]	mid					The module identifier. The application can use any unique value to later identify the module.
            @param[in]	inputs				The module input descriptor.
            @return PXCM_STATUS_NO_ERROR	Successful executation.
        */
        public pxcmStatus RequestStreams(Int32 mid, PXCMVideoModule.DataDesc inputs)
        {
            ObjectPair op = new ObjectPair(inputs);
            descriptors.Add(op);
            pxcmStatus sts = PXCMCaptureManager_RequestStreams(instance, mid, op.unmanaged);
            if (sts < pxcmStatus.PXCM_STATUS_NO_ERROR)
                descriptors.Clear();
            return sts;
        }


        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMCaptureManager_LocateStreams(IntPtr cutil, IntPtr hs);

        /**
            @brief	The function locates an I/O device that meets any module input needs previously specified
            by the RequestStreams function. The device and its streams are configured upon a successful return.
            @param[in]	handler				The optional handler instance for callbacks during the device match.
            @return PXCM_STATUS_NO_ERROR	Successful executation.
        */
        public pxcmStatus LocateStreams(Handler handler)
        {
            HandlerDIR handlerDIR = new HandlerDIR(handler);
            pxcmStatus sts = PXCMCaptureManager_LocateStreams(instance, handlerDIR.dirUnmanaged);
            if (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR)
            {
                foreach (ObjectPair op in descriptors)
                {
                    Marshal.PtrToStructure(op.unmanaged, op.managed);
                }
            }
            descriptors.Clear();
            handlerDIR.Dispose();
            return sts;
        }

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern void PXCMCaptureManager_CloseStreams(IntPtr cutil);

        /**
            @brief	Close the streams.
        */
        public void CloseStreams()
        {
            PXCMCaptureManager_CloseStreams(instance);
            descriptors.Clear();
        }

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCMCaptureManager_QueryCapture(IntPtr cutil);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCMCaptureManager_QueryDevice(IntPtr cutil);


        [DllImport(PXCMBase.DLLNAME)]
        internal static extern void PXCMCaptureManager_QueryImageSize(IntPtr putil, PXCMCapture.StreamType type, ref PXCMSizeI32 size);

        /**
            @brief	Return the stream resolution of the specified stream type.
            @param[in]	type		The stream type.
            @return the stream resolution.
        */
        public PXCMSizeI32 QueryImageSize(PXCMCapture.StreamType type)
        {
            PXCMSizeI32 size = new PXCMSizeI32();
            PXCMCaptureManager_QueryImageSize(instance, type, ref size);
            return size;
        }

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMCaptureManager_ReadModuleStreamsAsync(IntPtr cutil, Int32 mid, [Out] IntPtr[] images, out IntPtr sp);


        [DllImport(PXCMBase.DLLNAME, CharSet = CharSet.Unicode)]
        internal static extern pxcmStatus PXCMCaptureManager_SetFileName(IntPtr cutil, String file, [MarshalAs(UnmanagedType.Bool)] Boolean record);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern void PXCMCaptureManager_SetMask(IntPtr cutil, PXCMCapture.StreamType types);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern void PXCMCaptureManager_SetPause(IntPtr cutil, [MarshalAs(UnmanagedType.Bool)] Boolean pause);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern void PXCMCaptureManager_SetRealtime(IntPtr cutil, [MarshalAs(UnmanagedType.Bool)] Boolean realtime);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern void PXCMCaptureManager_SetFrameByIndex(IntPtr cutil, Int32 frame);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern Int32 PXCMCaptureManager_QueryFrameIndex(IntPtr cutil);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern void PXCMCaptureManager_SetFrameByTimeStamp(IntPtr cutil, Int64 ts);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern Int64 PXCMCaptureManager_QueryFrameTimeStamp(IntPtr cutil);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern Int32 PXCMCaptureManager_QueryNumberOfFrames(IntPtr cutil);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMCaptureManager_EnableDeviceRotation(IntPtr cutil, bool enableFlag);
        
        [DllImport(PXCMBase.DLLNAME)]
        internal static extern bool PXCMCaptureManager_IsDeviceRotationEnabled(IntPtr cutil);

        /* Private Data */
        private List<ObjectPair> descriptors = new List<ObjectPair>();
    };

#if RSSDK_IN_NAMESPACE
}
#endif
