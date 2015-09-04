/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2013 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Runtime.InteropServices;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

    /**
	The interface adds a reference count to the supported object.
*/
    public partial class PXCMAddRef : PXCMBase
    {
        new public const Int32 CUID = 0x53534142;

        /** 
        @brief Increase the reference counter of the underlying object.
        @return The increased reference counter value.
        */
        new public Int32 AddRef()
        {
            orig.AddRef();
            return PXCMAddRef_AddRef(instance);
        }

        /* constructors & misc */
        internal PXCMAddRef(IntPtr instance, Boolean delete)
            : base(instance, false)
        {
        }
    };

#if RSSDK_IN_NAMESPACE
}
#endif
