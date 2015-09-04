/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2013 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

    public partial class PXCMSession : PXCMBase
    {
        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMSession_QueryVersion(IntPtr session, [Out] ImplVersion version);

        /** 
            @brief Return the SDK version.
            @return the SDK version.
        */
        public ImplVersion QueryVersion()
        {
            ImplVersion version = new ImplVersion();
            PXCMSession_QueryVersion(instance, version);
            return version;
        }

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMSession_QueryImpl(IntPtr session, ImplDesc templat, Int32 idx, [Out] ImplDesc desc);

        /** 
            @brief Search a module implementation.
            @param[in]	templat					The template for the module search. Zero field values match any.
            @param[in]	idx						The zero-based index to retrieve multiple matches.
            @param[out]	desc					The matched module descritpor, to be returned.
            @return PXCM_STATUS_NO_ERROR			Successful execution.
            @return PXCM_STATUS_ITEM_UNAVAILABLE	No matched module implementation.
        */
        public pxcmStatus QueryImpl(ImplDesc templat, Int32 idx, out ImplDesc desc)
        {
            desc = new ImplDesc();
            pxcmStatus sts = PXCMSession_QueryImpl(instance, templat, idx, desc);
            return sts;
        }

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMSession_CreateImpl(IntPtr session, ImplDesc desc, Int32 iuid, Int32 cuid, out IntPtr instance);

        [DllImport(PXCMBase.DLLNAME, CharSet = CharSet.Unicode)]
        internal static extern pxcmStatus PXCMSession_LoadImplFromFile(IntPtr session, String moduleName);

        [DllImport(PXCMBase.DLLNAME, CharSet = CharSet.Unicode)]
        internal static extern pxcmStatus PXCMSession_UnloadImplFromFile(IntPtr session, String moduleName);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMSession_QueryModuleDesc(IntPtr session, IntPtr module, [Out] PXCMSession.ImplDesc desc);

        /** 
            @brief Return the module descriptor
            @param[in]  module  The module instance
            @param[out] desc	The module descriptor, to be returned.
            @return PXCM_STATUS_NO_ERROR         Successful execution.
            @return PXCM_STATUS_ITEM_UNAVAILABLE Failed to identify the module instance.
        */
        public pxcmStatus QueryModuleDesc(PXCMBase module, out PXCMSession.ImplDesc desc)
        {
            desc = new PXCMSession.ImplDesc();
            return PXCMSession_QueryModuleDesc(instance, module.instance, desc);
        }

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCMSession_CreateImage(IntPtr session, PXCMImage.ImageInfo info, PXCMImage.ImageData data);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCMSession_CreateAudio(IntPtr session, PXCMAudio.AudioInfo info, PXCMAudio.AudioData data);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMSession_SetCoordinateSystem(IntPtr session, CoordinateSystem cs);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern CoordinateSystem PXCMSession_QueryCoordinateSystem(IntPtr session);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCMSession_CreateInstance();
    };

#if RSSDK_IN_NAMESPACE
}
#endif
