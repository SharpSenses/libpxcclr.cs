/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2014 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

    public partial class PXCMFaceModule : PXCMBase
    {
        new public const Int32 CUID = 0x44334146;

        /// <summary>
        /// create a new copy of active configuration
        /// </summary>
        /// <returns></returns>
        public PXCMFaceConfiguration CreateActiveConfiguration()
        {
            IntPtr cfg = PXCMFaceModule_CreateActiveConfiguration(instance);
            return cfg == IntPtr.Zero ? null : new PXCMFaceConfiguration(maps, cfg, true);
        }

        /// <summary>
        /// create a placeholder for output
        /// </summary>
        /// <returns></returns>
        public PXCMFaceData CreateOutput()
        {
            IntPtr data = PXCMFaceModule_CreateOutput(instance);
            return data == IntPtr.Zero ? null : new PXCMFaceData(data, true);
        }

        internal PXCMFaceConfiguration.EventMaps maps;

        internal PXCMFaceModule(IntPtr instance, Boolean delete)
            : base(instance, delete)
        {
            maps = new PXCMFaceConfiguration.EventMaps();
        }

        internal PXCMFaceModule(PXCMFaceConfiguration.EventMaps maps, IntPtr instance, Boolean delete)
            : base(instance, delete)
        {
            this.maps = maps;
        }
    };

#if RSSDK_IN_NAMESPACE
}
#endif