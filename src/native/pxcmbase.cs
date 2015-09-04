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

    public partial class PXCMBase : IDisposable
    {
        [DllImport(DLLNAME)]
        internal static extern void PXCMBase_Release(IntPtr pbase);

        [DllImport(DLLNAME)]
        internal static extern IntPtr PXCMBase_QueryInstance(IntPtr pbase, Int32 cuid);

        internal partial class ObjectPair : IDisposable
        {
            public ObjectPair(object tt)
            {
                managed = tt;
                unmanaged = Marshal.AllocHGlobal(Marshal.SizeOf(managed));
                Marshal.StructureToPtr(managed, unmanaged, false);
            }

            public ObjectPair(object tt, Type type)
            {
                managed = tt;
                unmanaged = Marshal.AllocHGlobal(Marshal.SizeOf(type));
            }

            ~ObjectPair()
            {
                Dispose();
            }

            public void Dispose()
            {
                if (unmanaged != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(unmanaged);
                    unmanaged = IntPtr.Zero;
                }
            }
        }
    };

#if RSSDK_IN_NAMESPACE
}
#endif
