/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2013-2014 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

    public partial class PXCMImage : PXCMBase
    {
        [StructLayout(LayoutKind.Sequential, Size = 48)]
        public partial class ImageData
        {
            [DllImport(DLLNAME)]
            private static extern void PXCMImage_ImageData_Copy(IntPtr src, Int32 spitch, IntPtr dst, Int32 dpitch, Int32 width, Int32 height);

            public Byte[] ToByteArray(Int32 index, Byte[] dest)
            {
                Marshal.Copy(planes[index], dest, 0, dest.Length);
                return dest;
            }

            public Int16[] ToShortArray(Int32 index, Int16[] dest)
            {
                Marshal.Copy(planes[index], dest, 0, dest.Length);
                return dest;
            }

            [CLSCompliant(false)]
            public UInt16[] ToUShortArray(Int32 index, UInt16[] dest)
            {
                GCHandle gch = GCHandle.Alloc(dest, GCHandleType.Pinned);
                Int32 width = pitches[index] / sizeof(UInt16);
                PXCMImage_ImageData_Copy(planes[index], pitches[index], gch.AddrOfPinnedObject(), pitches[index], width * sizeof(UInt16), dest.Length / width);
                gch.Free();
                //Marshal.Copy(planes[index], (Int16[])(Object)dest, 0, dest.Length);
                return dest;
            }

            public Int32[] ToIntArray(Int32 index, Int32[] dest)
            {
                Marshal.Copy(planes[index], dest, 0, dest.Length);
                return dest;
            }

            public Single[] ToFloatArray(Int32 index, Single[] dest)
            {
                Marshal.Copy(planes[index], dest, 0, dest.Length);
                return dest;
            }

            public void FromByteArray(Int32 index, Byte[] src)
            {
                Marshal.Copy(src, 0, planes[index], src.Length);
            }

            public void FromShortArray(Int32 index, Int16[] src)
            {
                Marshal.Copy(src, 0, planes[index], src.Length);
            }

            [CLSCompliant(false)]
            public void FromUShortArray(Int32 index, UInt16[] src)
            {
                GCHandle gch = GCHandle.Alloc(src, GCHandleType.Pinned);
                Int32 width = pitches[index] / sizeof(UInt16);
                PXCMImage_ImageData_Copy(gch.AddrOfPinnedObject(), pitches[index], planes[index], pitches[index], width * sizeof(UInt16), src.Length / width);
                //Marshal.Copy((Int16[])(Object)src, 0, planes[index], src.Length);
            }

            public void FromIntArray(Int32 index, Int32[] src)
            {
                Marshal.Copy(src, 0, planes[index], src.Length);
            }

            public void FromFloatArray(Int32 index, Single[] src)
            {
                Marshal.Copy(src, 0, planes[index], src.Length);
            }

            public ImageData()
            {
                pitches = new Int32[4];
                planes = new IntPtr[4];
                reserved = new Int32[3];
            }
        };

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern void PXCMImage_QueryInfo(IntPtr image, [Out] ImageInfo info);

        /** 
        @brief Return the image sample information.
        @return the image sample information in the ImageInfo structure.
        */
        public ImageInfo QueryInfo()
        {
            ImageInfo info = new ImageInfo();
            PXCMImage_QueryInfo(instance, info);
            return info;
        }

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern PXCMCapture.StreamType PXCMImage_QueryStreamType(IntPtr image);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern Int64 PXCMImage_QueryTimeStamp(IntPtr image);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern PXCMImage.Option PXCMImage_QueryOptions(IntPtr image);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern void PXCMImage_SetStreamType(IntPtr image, PXCMCapture.StreamType type);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern void PXCMImage_SetTimeStamp(IntPtr image, Int64 ts);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern void PXCMImage_SetOptions(IntPtr image, PXCMImage.Option options);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMImage_CopyImage(IntPtr image, IntPtr src_image);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMImage_ExportData(IntPtr image, PXCMImage.ImageData data, Int32 flags);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMImage_ImportData(IntPtr image, PXCMImage.ImageData data, Int32 flags);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMImage_AcquireAccess(IntPtr image, Access access, PixelFormat format, Option options, IntPtr data);

        [StructLayout(LayoutKind.Sequential)]
        private class ImageDataNative : ImageData
        {
            public IntPtr native;
        };

        /** 
        @brief Lock to access the internal storage of a specified format. The function will perform format conversion if unmatched. 
        @param[in] access           The access mode.
        @param[in] format           The requested smaple format.
        @param[in] options          The option flags. 
        @param[out] data            The sample data storage, to be returned.
        @return PXCM_STATUS_NO_ERROR    Successful execution.
        */
        public pxcmStatus AcquireAccess(Access access, PixelFormat format, Option options, out ImageData data)
        {
            ImageDataNative dataNative = new ImageDataNative();
            dataNative.native = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ImageDataNative)));
            data = dataNative;

            pxcmStatus sts = PXCMImage_AcquireAccess(instance, access, format, options, dataNative.native);
            if (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR)
            {
                IntPtr native = dataNative.native;
                Marshal.PtrToStructure(dataNative.native, dataNative);
                dataNative.native = native;
            }
            else
            {
                Marshal.FreeHGlobal(dataNative.native);
                dataNative.native = IntPtr.Zero;
            }
            return sts;
        }

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMImage_ReleaseAccess(IntPtr image, IntPtr data);

        /** 
        @brief Unlock the previously acquired buffer.
        @param[in] data             The sample data storage previously acquired.
        @return PXCM_STATUS_NO_ERROR    Successful execution.
        */
        public pxcmStatus ReleaseAccess(ImageData data)
        {
            ImageDataNative dataNative = data as ImageDataNative;
            if (dataNative == null) return pxcmStatus.PXCM_STATUS_HANDLE_INVALID;
            if (dataNative.native == IntPtr.Zero) return pxcmStatus.PXCM_STATUS_HANDLE_INVALID;
            pxcmStatus sts = PXCMImage_ReleaseAccess(instance, dataNative.native);
            Marshal.FreeHGlobal(dataNative.native);
            dataNative.native = IntPtr.Zero;
            return sts;
        }

        /** 
        @brief Query rotation data.
        */
        [DllImport(PXCMBase.DLLNAME)]
        internal static extern PXCMImage.Rotation PXCMImage_QueryRotation(IntPtr image);
    };

#if RSSDK_IN_NAMESPACE
}
#endif
