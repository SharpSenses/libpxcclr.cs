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

    public partial class PXCMPersonTrackingModule : PXCMBase
    {
        new public const Int32 CUID = 0x4d544f50;

        /// <summary>
        /// create a new copy of active configuration
        /// </summary>
        /// <returns></returns>
        public PXCMPersonTrackingConfiguration QueryConfiguration()
        {
            IntPtr cfg = PXCMPersonTrackingModule_QueryConfiguration(instance);
            return (cfg == IntPtr.Zero) ? null : new PXCMPersonTrackingConfiguration(maps, cfg, false);
        }

        /// <summary>
        ///  create a placeholder for output
        /// </summary>
        /// <returns></returns>
        public PXCMPersonTrackingData QueryOutput()
        {
            IntPtr data = PXCMPersonTrackingModule_QueryOutput(instance);
            return data == IntPtr.Zero ? null : new PXCMPersonTrackingData(data, false);
        }

        internal PXCMPersonTrackingConfiguration.EventMaps maps;

        internal PXCMPersonTrackingModule(IntPtr instance, Boolean delete)
            : base(instance, delete)
        {
            maps = new PXCMPersonTrackingConfiguration.EventMaps();
        }

        internal PXCMPersonTrackingModule(PXCMPersonTrackingConfiguration.EventMaps maps, IntPtr instance, Boolean delete)
            : base(instance, delete)
        {
            this.maps = maps;
        }
    };

#if RSSDK_IN_NAMESPACE
}
#endif