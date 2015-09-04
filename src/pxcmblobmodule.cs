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

    /**
	@Class PXCMBlobModule 
	@brief The main interface to the blob module's classes.
	
	The blob module allows you to extract "blobs" (silhouettes of objects identified by the sensor) and their contour lines.
	Use the PXCBlobModule interface to access to the module's configuration and blob and contour line data.
*/
    public partial class PXCMBlobModule : PXCMBase
    {
        new public const Int32 CUID = 0x444d4d42;

        /** 
        @brief Create a new instance of the blob module's active configuration.
        Use the PXCBlobConfiguration object to examine the current configuration or to set new configuration values.
        @return A pointer to the configuration instance.
        @see PXCMBlobConfiguration
        */
        public PXCMBlobConfiguration CreateActiveConfiguration()
        {
            IntPtr cfg = PXCMBlobModule_CreateActiveConfiguration(instance);
            return cfg == IntPtr.Zero ? null : new PXCMBlobConfiguration(cfg, true);
        }

        /** 
        @brief Create a new instance of the blob module's output data (extracted blobs and contour lines).
        @return A pointer to a PXCBlobData instance.
        @see PXCMBlobData
        */
        public PXCMBlobData CreateOutput()
        {
            IntPtr data = PXCMBlobModule_CreateOutput(instance);
            return data == IntPtr.Zero ? null : new PXCMBlobData(data, true);
        }

        internal PXCMBlobModule(IntPtr instance, Boolean delete)
            : base(instance, delete)
        {
        }
    };

#if RSSDK_IN_NAMESPACE
}
#endif
