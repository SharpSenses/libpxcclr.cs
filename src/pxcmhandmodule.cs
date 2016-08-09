/*******************************************************************************

  INTEL CORPORATION PROPRIETARY INFORMATION
  This software is supplied under the terms of a license agreement or nondisclosure
  agreement with Intel Corporation and may not be copied or disclosed except in
  accordance with the terms of that agreement
  Copyright(c) 2013 Intel Corporation. All Rights Reserved.

 *******************************************************************************/
using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections.Generic;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

/// <summary>
/// @Class PXCMHandModule 
/// The main interface to the hand module's classes.\n
/// Use this interface to access the hand module's configuration and output data.
/// </summary>

public partial class PXCMHandModule : PXCMBase
{
    new public const Int32 CUID = 0x4e4e4148;

    /// <summary> 
    /// Create a new instance of the hand module's active configuration.
    /// Multiple configuration instances can be created in order to define different configurations for different stages of the application.
    /// You can switch between the configurations by calling the ApplyChanges method of the required configuration instance.
    /// </summary>
    /// <returns> A pointer to the configuration instance.</returns>
    /// @see PXCMHandConfiguration
    public PXCMHandConfiguration CreateActiveConfiguration()
    {
        IntPtr hc = PXCMHandModule_CreateActiveConfiguration(instance);
        if (hc == IntPtr.Zero) return null;
        return new PXCMHandConfiguration(maps, hc, true);
    }

    /// <summary> 
    /// Create a new instance of the hand module's current output data.
    /// Multiple instances of the output can be created in order to store previous tracking states. 
    /// </summary>
    /// <returns> A pointer to the output data instance.</returns>
    /// @see PXCMHandData
    public PXCMHandData CreateOutput()
    {
        IntPtr hd = PXCMHandModule_CreateOutput(instance);
        return hd != IntPtr.Zero ? new PXCMHandData(hd, true) : null;
    }





    internal PXCMHandConfiguration.EventMaps maps;

    internal PXCMHandModule(IntPtr instance, Boolean delete)
        : base(instance, delete)
    {
        maps = new PXCMHandConfiguration.EventMaps();
    }

    internal PXCMHandModule(PXCMHandConfiguration.EventMaps maps, IntPtr instance, Boolean delete)
        : base(instance, delete)
    {
        this.maps = maps;
    }
};

#if RSSDK_IN_NAMESPACE
}
#endif
