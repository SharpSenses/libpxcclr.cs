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

    public partial class PXCMMetadata : PXCMBase
    {
        [DllImport(PXCMBase.DLLNAME)]
        internal static extern Int32 PXCMMetadata_QueryUID(IntPtr md);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern Int32 PXCMMetadata_QueryMetadata(IntPtr md, Int32 idx);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMMetadata_DetachMetadata(IntPtr md, Int32 id);

        [DllImport(PXCMBase.DLLNAME)]
        private static extern pxcmStatus PXCMMetadata_AttachBuffer(IntPtr md, Int32 id, IntPtr buffer, Int32 size);

        internal static pxcmStatus AttachBufferINT(IntPtr md, Int32 id, byte[] buffer)
        {
            GCHandle gch = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            pxcmStatus sts = PXCMMetadata_AttachBuffer(md, id, gch.AddrOfPinnedObject(), buffer.Length);
            gch.Free();
            return sts;
        }

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern Int32 PXCMMetadata_QueryBufferSize(IntPtr md, Int32 id);

        [DllImport(PXCMBase.DLLNAME)]
        private static extern pxcmStatus PXCMMetadata_QueryBuffer(IntPtr md, Int32 id, [Out] Byte[] buffer, Int32 size);

        internal static pxcmStatus QueryBufferINT(IntPtr md, Int32 id, out Byte[] buffer)
        {
            Int32 size = PXCMMetadata_QueryBufferSize(md, id);
            if (size == 0)
            {
                buffer = null;
                return pxcmStatus.PXCM_STATUS_ITEM_UNAVAILABLE;
            }

            buffer = new Byte[size];
            return PXCMMetadata_QueryBuffer(md, id, buffer, size);
        }

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMMetadata_AttachSerializable(IntPtr md, Int32 id, IntPtr instance);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMMetadata_CreateSerializable(IntPtr md, Int32 id, Int32 cuid, out IntPtr instance);
    };

#if RSSDK_IN_NAMESPACE
}
#endif
