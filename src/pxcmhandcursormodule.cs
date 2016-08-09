/*******************************************************************************

  INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or non-disclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2013-2016 Intel Corporation. All Rights Reserved.

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
/// @Class PXCHandCursorModule 
/// The main interface to the hand cursor module's classes.\n
/// Use this interface to access the hand cursor module's configuration and output data.
/// </summary>

public partial class PXCMHandCursorModule : PXCMBase
{
    new public const Int32 CUID = 0x4e4d4348;

    /// <summary> 
    ///  @brief Create a new instance of the hand cursor module's active configuration.
    ///  Multiple configuration instances can be created in order to define different configurations for different stages of the application.
    ///   You can switch between the configurations by calling the ApplyChanges method of the required configuration instance.
    /// </summary> 
    /// <returns> A pointer to the configuration instance.</returns>
    ///   @see PXCMCursorConfiguration
    public PXCMCursorConfiguration CreateActiveConfiguration()
    {
        IntPtr hc = PXCMHandCursorModule_CreateActiveConfiguration(instance);
        if (hc == IntPtr.Zero) return null;
        return new PXCMCursorConfiguration(maps, hc, true);
    }

    /// <summary> 
    /// Create a new instance of the hand cursor module's current output data.
    /// Multiple instances of the output can be created in order to store previous tracking states. 
    /// </summary>
    /// <returns> A pointer to the output data instance.</returns>
    /// @see PXCMCursorData
    public PXCMCursorData CreateOutput()
    {
        IntPtr hd = PXCMHandCursorModule_CreateOutput(instance);
        return hd != IntPtr.Zero ? new PXCMCursorData(hd, true) : null;
    }





    internal PXCMCursorConfiguration.EventMaps maps;

    internal PXCMHandCursorModule(IntPtr instance, Boolean delete)
        : base(instance, delete)
    {
        maps = new PXCMCursorConfiguration.EventMaps();
    }

    internal PXCMHandCursorModule(PXCMCursorConfiguration.EventMaps maps, IntPtr instance, Boolean delete)
        : base(instance, delete)
    {
        this.maps = maps;
    }
};

#if RSSDK_IN_NAMESPACE
}
#endif
