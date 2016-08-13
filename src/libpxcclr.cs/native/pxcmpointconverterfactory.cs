/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2013-2014 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Runtime.InteropServices;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

    public partial class PXCMPointConverterFactory : PXCMBase
    {
        [DllImport(DLLNAME)]
        internal extern static IntPtr PXCMPointConverterFactory_CreateHandJointConverter(IntPtr instance, IntPtr handData, PXCMHandData.AccessOrderType accessOrder, Int32 index, PXCMHandData.JointType jointType);

        [DllImport(DLLNAME)]
        internal extern static IntPtr PXCMPointConverterFactory_CreateHandExtremityConverter(IntPtr instance, IntPtr handData, PXCMHandData.AccessOrderType accessOrder, Int32 index, PXCMHandData.ExtremityType extremityType);

        [DllImport(DLLNAME)]
        internal extern static IntPtr PXCMPointConverterFactory_CreateBlobPointConverter(IntPtr instance, IntPtr blobData, PXCMBlobData.AccessOrderType accessOrder, Int32 index, PXCMBlobData.ExtremityType extremityType);
       
        [DllImport(DLLNAME)]
        internal extern static IntPtr PXCMPointConverterFactory_CreateCustomPointConverter(IntPtr instance);
    };

#if RSSDK_IN_NAMESPACE
}
#endif



