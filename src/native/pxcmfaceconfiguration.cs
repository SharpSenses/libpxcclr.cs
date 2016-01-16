/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or non-disclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2014 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Runtime.InteropServices;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

    public partial class PXCMFaceConfiguration : PXCMBase
    {
        private partial class AlertHandlerDIR : IDisposable
        {
            [DllImport(PXCMBase.DLLNAME)]
            private static extern IntPtr PXCMFaceConfiguration_AllocAlertHandlerDIR(IntPtr handlerFunc);
            [DllImport(PXCMBase.DLLNAME)]
            private static extern void PXCMFaceConfiguration_FreeAlertHandlerDIR(IntPtr hdir);

            internal delegate void OnFiredAlertDIRDelegate(IntPtr data);

            private GCHandle gch;
            internal OnFiredAlertDelegate mfunc;
            internal IntPtr uDIR;

            public void Dispose()
            {
                if (uDIR == IntPtr.Zero) return;

                PXCMFaceConfiguration_FreeAlertHandlerDIR(uDIR);
                uDIR = IntPtr.Zero;
                if (gch.IsAllocated) gch.Free();
            }

            public AlertHandlerDIR(OnFiredAlertDelegate handler)
            {
                mfunc = handler;
                gch = GCHandle.Alloc(handler);
                uDIR = PXCMFaceConfiguration_AllocAlertHandlerDIR(Marshal.GetFunctionPointerForDelegate(handler));
            }

            ~AlertHandlerDIR()
            {
                Dispose();
            }
        };

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMFaceConfiguration_EnableAlert(IntPtr instance, PXCMFaceData.AlertData.AlertType alertEvent);

        [DllImport(DLLNAME)]
        internal static extern void PXCMFaceConfiguration_EnableAllAlerts(IntPtr instance);

        [DllImport(DLLNAME)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern Boolean PXCMFaceConfiguration_IsAlertEnabled(IntPtr instance, PXCMFaceData.AlertData.AlertType alertEvent);

        [DllImport(DLLNAME)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern Boolean PXCMFaceConfiguration_DisableAlert(IntPtr instance, PXCMFaceData.AlertData.AlertType alertEvent);

        [DllImport(DLLNAME)]
        internal static extern void PXCMFaceConfiguration_DisableAllAlerts(IntPtr instance);

        [DllImport(PXCMBase.DLLNAME)]
        private static extern pxcmStatus PXCMFaceConfiguration_SubscribeAlert(IntPtr instance, IntPtr alertHandler);

        internal static pxcmStatus SubscribeAlertINT(IntPtr instance, OnFiredAlertDelegate handler, out Object proxy)
        {
            AlertHandlerDIR adir = new AlertHandlerDIR(handler);
            pxcmStatus sts = PXCMFaceConfiguration_SubscribeAlert(instance, adir.uDIR);
            if (sts < pxcmStatus.PXCM_STATUS_NO_ERROR)
                adir.Dispose();
            proxy = (Object)adir;
            return sts;
        }

        [DllImport(PXCMBase.DLLNAME)]
        private static extern pxcmStatus PXCMFaceConfiguration_UnsubscribeAlert(IntPtr instance, IntPtr alertHandler);

        internal static pxcmStatus UnsubscribeAlertINT(IntPtr instance, Object proxy)
        {
            AlertHandlerDIR adir = (AlertHandlerDIR)proxy;
            pxcmStatus sts = PXCMFaceConfiguration_UnsubscribeAlert(instance, adir.uDIR);
            adir.Dispose();
            return sts;
        }

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMFaceConfiguration_ApplyChanges(IntPtr instance, AllFaceConfigurations configs);

        [DllImport(DLLNAME)]
        private static extern void PXCMFaceConfiguration_GetConfigurations(IntPtr instance, [Out] AllFaceConfigurations configs);

        internal static void GetConfigurationsINT(IntPtr instance, out AllFaceConfigurations configs)
        {
            configs = new AllFaceConfigurations();
            PXCMFaceConfiguration_GetConfigurations(instance, configs);
        }

        [DllImport(DLLNAME)]
        private static extern void PXCMFaceConfiguration_RestoreDefaults(IntPtr instance, [Out] AllFaceConfigurations configs);

        internal static void RestoreDefaultsINT(IntPtr instance, out AllFaceConfigurations configs)
        {
            configs = new AllFaceConfigurations();
            PXCMFaceConfiguration_RestoreDefaults(instance, configs);
        }

        [DllImport(DLLNAME)]
        private static extern pxcmStatus PXCMFaceConfiguration_Update(IntPtr instance, [Out] AllFaceConfigurations configs);

        internal static pxcmStatus UpdateINT(IntPtr instance, out AllFaceConfigurations configs)
        {
            configs = new AllFaceConfigurations();
            return PXCMFaceConfiguration_Update(instance, configs);
        }

        [DllImport(DLLNAME)]
        internal static extern TrackingModeType PXCMFaceConfiguration_GetTrackingMode(IntPtr instance);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMFaceConfiguration_SetTrackingMode(IntPtr instance, TrackingModeType trackingMode);

        public partial class ExpressionsConfiguration
        {
            [DllImport(DLLNAME)]
            internal static extern void PXCMFaceConfiguration_ExpressionsConfiguration_EnableAllExpressions(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern void PXCMFaceConfiguration_ExpressionsConfiguration_DisableAllExpressions(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern pxcmStatus PXCMFaceConfiguration_ExpressionsConfiguration_EnableExpression(IntPtr instance, PXCMFaceData.ExpressionsData.FaceExpression expression);

            [DllImport(DLLNAME)]
            internal static extern void PXCMFaceConfiguration_ExpressionsConfiguration_DisableExpression(IntPtr instsance, PXCMFaceData.ExpressionsData.FaceExpression expression);

            [DllImport(DLLNAME)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern Boolean PXCMFaceConfiguration_ExpressionsConfiguration_IsExpressionEnabled(IntPtr instance, PXCMFaceData.ExpressionsData.FaceExpression expression);
        }

        public partial class RecognitionConfiguration
        {
            [DllImport(DLLNAME)]
            private static extern pxcmStatus PXCMFaceConfiguration_RecognitionConfiguration_QueryActiveStorage(IntPtr instance, [Out] RecognitionConfiguration.RecognitionStorageDesc outStorage);

            internal static pxcmStatus QueryActiveStorageINT(IntPtr instance, out RecognitionConfiguration.RecognitionStorageDesc outStorage)
            {
                outStorage = new RecognitionConfiguration.RecognitionStorageDesc();
                return PXCMFaceConfiguration_RecognitionConfiguration_QueryActiveStorage(instance, outStorage);
            }

            [DllImport(DLLNAME, CharSet = CharSet.Unicode)]
            private static extern pxcmStatus PXCMFaceConfiguration_RecognitionConfiguration_CreateStorage(IntPtr instance, String storageName, [Out] RecognitionConfiguration.RecognitionStorageDesc outStroage);

            internal static pxcmStatus CreateStorageINT(IntPtr instance, String storageName, out RecognitionConfiguration.RecognitionStorageDesc outStorage)
            {
                outStorage = new RecognitionConfiguration.RecognitionStorageDesc();
                return PXCMFaceConfiguration_RecognitionConfiguration_CreateStorage(instance, storageName, outStorage);
            }

            [DllImport(DLLNAME, CharSet = CharSet.Unicode)]
            internal static extern pxcmStatus PXCMFaceConfiguration_RecognitionConfiguration_SetStorageDesc(IntPtr instance, String storageName, RecognitionConfiguration.RecognitionStorageDesc storage);

            [DllImport(DLLNAME, CharSet = CharSet.Unicode)]
            internal static extern pxcmStatus PXCMFaceConfiguration_RecognitionConfiguration_DeleteStorage(IntPtr instance, String storageName);

            [DllImport(DLLNAME, CharSet = CharSet.Unicode)]
            internal static extern pxcmStatus PXCMFaceConfiguration_RecognitionConfiguration_UseStorage(IntPtr instance, String storageName);

            [DllImport(DLLNAME)]
            internal static extern void PXCMFaceConfiguration_RecognitionConfiguration_SetDatabaseBuffer(IntPtr instance, Byte[] buffer, Int32 size);
        }

        public partial class GazeConfiguration {
            [DllImport(DLLNAME)]
            internal static extern pxcmStatus PXCMFaceConfiguration_GazeConfiguration_LoadCalibration(IntPtr instance, Byte[] buffer, Int32 size);

            [DllImport(DLLNAME)]
            internal static extern Int32 PXCMFaceConfiguration_GazeConfiguration_QueryCalibDataSize(IntPtr instance);

            [DllImport(DLLNAME)]
            internal static extern void PXCMFaceConfiguration_GazeConfiguration_SetDominantEye(IntPtr instance, PXCMFaceData.GazeCalibData.DominantEye eye);

        }

        [DllImport(DLLNAME)]
        internal static extern IntPtr PXCMFaceConfiguration_QueryGaze(IntPtr instance);
    };

#if RSSDK_IN_NAMESPACE
}
#endif
