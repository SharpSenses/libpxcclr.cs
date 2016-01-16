/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2015 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Runtime.InteropServices;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

/**
	@class PXCMObjectRecognition
    Defines the PXCMObjectRecognition interface, which programs may use to process  
    snapshots of captured frames to recognize pre-trained objects.
*/
public partial class PXCMObjectRecognitionModule : PXCMBase
{
    new public const Int32 CUID = 0x4d4a424f;

    /** 
        @brief Create a new instance of the OR module's active configuration.
            Multiple configuration instances can be created in order to define different configurations for different stages of the application.
            The configurations can be switched by calling the ApplyChanges method of the required configuration instance.
        @return an object to the configuration instance.
        @see PXCMObjectRecognitionConfiguaration
    */
    public PXCMObjectRecognitionConfiguration CreateActiveConfiguration()
    {
        IntPtr cfg = PXCMObjectRecognitionModule_CreateActiveConfiguration(instance);
        return cfg == IntPtr.Zero ? null : new PXCMObjectRecognitionConfiguration(cfg, true);
    }

    /** 
        @brief Create a new instance of the hand module's current output data.
            Multiple instances of the output can be created in order to store previous tracking states. 
        @return an object to the output data instance.
        @see PXCMObjectRecognitionData 
    */
    public PXCMObjectRecognitionData CreateOutput()
    {
        IntPtr data = PXCMObjectRecognitionModule_CreateOutput(instance);
        return data == IntPtr.Zero ? null : new PXCMObjectRecognitionData(data, true);
    }

    internal PXCMObjectRecognitionModule(IntPtr instance, Boolean delete)
        : base(instance, delete)
    {
    }
};

#if RSSDK_IN_NAMESPACE
}
#endif
