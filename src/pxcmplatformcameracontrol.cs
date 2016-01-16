/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2015 Intel Corporation. All Rights Reserved.

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
        new public const Int32 CUID = unchecked((Int32)0x43435044);

        public class Handler
        {
            public delegate void OnPlatformCameraSampleDelegate(PXCMCapture.Sample sample);
            public delegate void OnPlatformCameraErrorDelegate();

            public OnPlatformCameraSampleDelegate onPlatformCameraSample;
            public OnPlatformCameraErrorDelegate onPlatformCameraError;
        };


        public pxcmStatus EnumPhotoProfile(Int32 index, out PXCMCapture.Device.StreamProfile photoProfile)
        {
            photoProfile = new PXCMCapture.Device.StreamProfile();
            return PXCMPlatformCameraControl_EnumPhotoProfile(instance, index, photoProfile); 
        }


        public PXCMProjection CreatePhotoProjection(PXCMCapture.Device.StreamProfile photoProfile)
        {
            IntPtr cutil = PXCMPlatformCameraControl_CreatePhotoProjection(instance, photoProfile);
            if (cutil == IntPtr.Zero) return null;
            return new PXCMProjection(cutil, false);
        }

        internal PXCMPlatformCameraControl(IntPtr instance, Boolean delete)
            : base(instance, delete)
        {
        }
    };

#if RSSDK_IN_NAMESPACE
}
#endif