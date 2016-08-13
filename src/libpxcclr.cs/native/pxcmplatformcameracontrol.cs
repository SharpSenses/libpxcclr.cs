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

public partial class PXCMPlatformCameraControl : PXCMBase
    {
        internal partial class HandlerDIR : IDisposable
        {
            [DllImport(PXCMBase.DLLNAME)]
            internal static extern IntPtr PXCMPlatformCameraControl_AllocHandlerDIR(HandlerSet hs);
            [DllImport(PXCMBase.DLLNAME)]
            internal static extern void PXCMPlatformCameraControl_FreeHandlerDIR(IntPtr hdir);

            internal delegate void OnPlatformCameraSampleDIRDelegate(IntPtr images);
            internal delegate void OnPlatformCameraErrorDIRDelegate();
           
            [StructLayout(LayoutKind.Sequential)]
            internal class HandlerSet
            {
                internal IntPtr onPlatformCameraSample;
                internal IntPtr onPlatformCameraError;
            };

            private PXCMPlatformCameraControl pcc;
            private Handler handler;
            internal IntPtr dirUnmanaged;
            private List<GCHandle> gchandles;


            private void OnPlatformCameraSample(IntPtr images)
            {
                PXCMCapture.Sample sample = new PXCMCapture.Sample(images);
                handler.onPlatformCameraSample(sample);
            }


            private void OnPlatformCameraError()
            {
                handler.onPlatformCameraError();
            }

            public void Dispose()
            {
                if (dirUnmanaged == IntPtr.Zero) return;

                PXCMPlatformCameraControl_FreeHandlerDIR(dirUnmanaged);
                dirUnmanaged = IntPtr.Zero;

                foreach (GCHandle gch in gchandles)
                {
                    if (gch.IsAllocated) gch.Free();
                }
                gchandles.Clear();
            }

            public HandlerDIR(PXCMPlatformCameraControl pcc, Handler handler)
            {
                this.pcc = pcc;
                this.handler = handler;
                HandlerSet handlerSet = new HandlerSet();
                gchandles = new List<GCHandle>();

                if (handler.onPlatformCameraSample != null)
                {
                    OnPlatformCameraSampleDIRDelegate dir = new OnPlatformCameraSampleDIRDelegate(OnPlatformCameraSample);
                    gchandles.Add(GCHandle.Alloc(dir));
                    handlerSet.onPlatformCameraSample = Marshal.GetFunctionPointerForDelegate(dir);
                }

                if (handler.onPlatformCameraError != null)
                {
                    OnPlatformCameraErrorDIRDelegate dir = new OnPlatformCameraErrorDIRDelegate(OnPlatformCameraError);
                    gchandles.Add(GCHandle.Alloc(dir));
                    handlerSet.onPlatformCameraError = Marshal.GetFunctionPointerForDelegate(dir);
                } 

                dirUnmanaged = PXCMPlatformCameraControl_AllocHandlerDIR(handlerSet);
            }

            ~HandlerDIR()
            {
                Dispose();
            }
        };

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMPlatformCameraControl_EnumPhotoProfile(IntPtr pcc, Int32 index, [Out] PXCMCapture.Device.StreamProfile photoProfile);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCMPlatformCameraControl_CreatePhotoProjection(IntPtr pcc, PXCMCapture.Device.StreamProfile photoProfile);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMPlatformCameraControl_TakePhoto(IntPtr pcc, PXCMCapture.Device.StreamProfile photoProfile, IntPtr hdir);

        public pxcmStatus TakePhoto(PXCMCapture.Device.StreamProfile photoProfile, Handler handler)
        {
            if (handler == null) return PXCMPlatformCameraControl_TakePhoto(instance, photoProfile, IntPtr.Zero);

            if (handlerDIR != null) 
                handlerDIR.Dispose();
            handlerDIR = new HandlerDIR(this, handler);
            return PXCMPlatformCameraControl_TakePhoto(instance, photoProfile, handlerDIR.dirUnmanaged);
        }

        private HandlerDIR handlerDIR = null;
    };

#if RSSDK_IN_NAMESPACE
}
#endif