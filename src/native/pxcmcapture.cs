/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2013-2014 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

    public partial class PXCMCapture : PXCMBase
    {
        [DllImport(PXCMBase.DLLNAME)]
        internal static extern Int32 PXCMCapture_QueryDeviceNum(IntPtr capture);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMCapture_QueryDeviceInfo(IntPtr capture, Int32 didx, [Out] DeviceInfo dinfo);

        /** 
            @brief Return the device infomation structure for a given device.
            @param[in]	didx					The zero-based device index.
            @param[out] dinfo					The pointer to the DeviceInfo structure, to be returned.
            @return PXCM_STATUS_NO_ERROR	    Successful execution.
            @return PXCM_STATUS_ITEM_UNAVAILABLE  The specified index does not exist.
        */
        public pxcmStatus QueryDeviceInfo(Int32 didx, out DeviceInfo dinfo)
        {
            dinfo = new DeviceInfo();
            return PXCMCapture_QueryDeviceInfo(instance, didx, dinfo);
        }

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCMCapture_CreateDevice(IntPtr capture, Int32 didx);

        public partial class Device : PXCMBase
        {
            [DllImport(PXCMBase.DLLNAME)]
            internal static extern void PXCMCapture_Device_QueryDeviceInfo(IntPtr device, [Out] DeviceInfo dinfo);

            /** 
                @brief Return the device infomation structure of the current device.
                @param[out] dinfo					The pointer to the DeviceInfo structure, to be returned.
            */
            public void QueryDeviceInfo(out DeviceInfo dinfo)
            {
                dinfo = new DeviceInfo();
                PXCMCapture_Device_QueryDeviceInfo(instance, dinfo);
            }

            [DllImport(PXCMBase.DLLNAME)]
            internal static extern Int32 PXCMCapture_Device_QueryStreamProfileSetNum(IntPtr device, StreamType scope);

            [DllImport(PXCMBase.DLLNAME)]
            private static extern pxcmStatus PXCMCapture_Device_QueryStreamProfileSet(IntPtr device, StreamType scope, Int32 index, [Out] StreamProfileSet profiles);

            internal static pxcmStatus QueryStreamProfileSetINT(IntPtr device, StreamType scope, Int32 index, out StreamProfileSet profiles)
            {
                profiles = new StreamProfileSet();
                return PXCMCapture_Device_QueryStreamProfileSet(device, scope, index, profiles);
            }

            [DllImport(PXCMBase.DLLNAME)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern Boolean PXCMCapture_Device_IsStreamProfileSetValid(IntPtr device, StreamProfileSet profiles);

            [DllImport(PXCMBase.DLLNAME)]
            internal static extern pxcmStatus PXCMCapture_Device_SetStreamProfileSet(IntPtr device, StreamProfileSet profiles);

            [DllImport(PXCMBase.DLLNAME)]
            internal static extern pxcmStatus PXCMCapture_Device_QueryProperty(IntPtr device, PXCMCapture.Device.Property label, out Single value);

            [DllImport(PXCMBase.DLLNAME)]
            private static extern pxcmStatus PXCMCapture_Device_QueryPropertyInfo(IntPtr device, PXCMCapture.Device.Property label, [Out] PropertyInfo info);

            internal static PropertyInfo QueryPropertyInfoINT(IntPtr device, PXCMCapture.Device.Property label)
            {
                PropertyInfo info = new PropertyInfo();
                PXCMCapture_Device_QueryPropertyInfo(device, label, info);
                return info;
            }

            [DllImport(PXCMBase.DLLNAME)]
            internal static extern pxcmStatus PXCMCapture_Device_QueryPropertyAuto(IntPtr device, PXCMCapture.Device.Property label, out Boolean ifauto);

            [DllImport(PXCMBase.DLLNAME)]
            internal static extern pxcmStatus PXCMCapture_Device_SetPropertyAuto(IntPtr device, PXCMCapture.Device.Property pty, [MarshalAs(UnmanagedType.Bool)] Boolean ifauto);

            [DllImport(PXCMBase.DLLNAME)]
            internal static extern pxcmStatus PXCMCapture_Device_SetProperty(IntPtr device, PXCMCapture.Device.Property pty, Single value);

            [DllImport(PXCMBase.DLLNAME)]
            internal static extern void PXCMCapture_Device_ResetProperties(IntPtr device, StreamType streams);

            [DllImport(PXCMBase.DLLNAME)]
            internal static extern void PXCMCapture_Device_RestorePropertiesUponFocus(IntPtr device);

            [DllImport(PXCMBase.DLLNAME)]
            internal static extern IntPtr PXCMCapture_Device_CreateProjection(IntPtr device);

            [DllImport(PXCMBase.DLLNAME)]
            internal static extern pxcmStatus PXCMCapture_Device_ReadStreamsAsync(IntPtr device, StreamType scope, [Out] IntPtr[] images, out IntPtr sp);

        };


        public partial class Sample
        {
            public void ReleaseImages()
            {
                for (int i = 0; i < STREAM_LIMIT; i++)
                {
                    if (images[i] == IntPtr.Zero) continue;
                    PXCMBase_Release(PXCMBase.PXCMBase_QueryInstance(images[i], PXCMBase.CUID));
                    images[i] = IntPtr.Zero;
                }
            }

            internal Sample(IntPtr instance)
            {
                images = new IntPtr[STREAM_LIMIT];
                Marshal.Copy(instance, images, 0, STREAM_LIMIT);
            }
        }

        public partial class Handler
        {
            [DllImport(PXCMBase.DLLNAME)]
            private static extern IntPtr PXCMCapture_AllocHandlerDIR(HandlerSet hs);
            [DllImport(PXCMBase.DLLNAME)]
            private static extern void PXCMCapture_FreeHandlerDIR(IntPtr hdir);

            internal delegate void OnDeviceListChangedDIRDelegate();

            [StructLayout(LayoutKind.Sequential)]
            private class HandlerSet
            {
                internal IntPtr onDeviceListChanged;
            };

            internal IntPtr dirUnmanaged;
            private List<GCHandle> gchandles;

            internal void Dispose()
            {
                if (dirUnmanaged == IntPtr.Zero) return;

                PXCMCapture_FreeHandlerDIR(dirUnmanaged);
                dirUnmanaged = IntPtr.Zero;

                foreach (GCHandle gch in gchandles)
                {
                    if (gch.IsAllocated) gch.Free();
                }
                gchandles.Clear();
            }

            internal void InitHandlerDIR()
            {
                HandlerSet handlerSet = new HandlerSet();
                gchandles = new List<GCHandle>();

                if (onDeviceListChanged != null)
                {
                    gchandles.Add(GCHandle.Alloc(onDeviceListChanged));
                    handlerSet.onDeviceListChanged = Marshal.GetFunctionPointerForDelegate(onDeviceListChanged);
                }
                dirUnmanaged = PXCMCapture_AllocHandlerDIR(handlerSet);
            }

            ~Handler()
            {
                Dispose();
            }
        };

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMCapture_SubscribeToCaptureCallbacks(IntPtr instance, IntPtr dirUnmanaged);

        /**
	        @brief    The function subscribes a handler for Capture callbacks. supports subscription of multiple callbacks.
	        @param[in] Handler     handler instance for Capture callbacks.
	        @return the status of the handler subscription.
	    */
	    public pxcmStatus SubscribeToCaptureCallbacks(Handler handler) {
            if (handler.dirUnmanaged != IntPtr.Zero) return pxcmStatus.PXCM_STATUS_HANDLE_INVALID;
            handler.InitHandlerDIR();
            return PXCMCapture_SubscribeToCaptureCallbacks(instance, handler.dirUnmanaged);
        }

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMCapture_UnscribeToCaptureCallbacks(IntPtr instance, IntPtr dirUnmanaged);

        /**
            @brief    The function unsubscribes a handler from the Capture callbacks. the rest of the handlers will still be triggered.
            @param[in] handler     handler instance of Capture callbacks to unsubscirbe.
            @return the status of the handler unsubscription.
        */
        public pxcmStatus UnsubscribeToCaptureCallbacks(Handler handler)
        {
            if (handler.dirUnmanaged == IntPtr.Zero) return pxcmStatus.PXCM_STATUS_HANDLE_INVALID;
            pxcmStatus sts=PXCMCapture_UnscribeToCaptureCallbacks(instance, handler.dirUnmanaged);
            handler.Dispose();
            return sts;
        }
    };

#if RSSDK_IN_NAMESPACE
}
#endif
