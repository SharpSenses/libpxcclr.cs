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


    public partial class PXCMAudio : PXCMBase
    {
        [StructLayout(LayoutKind.Sequential)]
        public partial class AudioData
        {
            public Byte[] ToByteArray(Byte[] dest)
            {
                Marshal.Copy(dataPtr, dest, 0, dest.Length);
                return dest;
            }

            public Int16[] ToShortArray(Int16[] dest)
            {
                Marshal.Copy(dataPtr, dest, 0, dest.Length);
                return dest;
            }

            public Single[] ToFloatArray(Single[] dest)
            {
                Marshal.Copy(dataPtr, dest, 0, dest.Length);
                return dest;
            }

            public void FromByteArray(Byte[] src)
            {
                dataSize = (src.Length / (((int)format & 0xff) / 8));
                Marshal.Copy(src, 0, dataPtr, src.Length);
            }

            public void FromShortArray(Int16[] src)
            {
                dataSize = src.Length;
                Marshal.Copy(src, 0, dataPtr, src.Length);
            }

            public void FromFloatArray(Single[] src)
            {
                dataSize = src.Length;
                Marshal.Copy(src, 0, dataPtr, src.Length);
            }
        };

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern void PXCMAudio_QueryInfo(IntPtr audio, [Out] AudioInfo info);

        /** 
        @brief Return the audio sample information.
        @return the audio sample information in the AudioInfo structure.
        */
        public AudioInfo QueryInfo()
        {
            AudioInfo info = new AudioInfo();
            PXCMAudio_QueryInfo(instance, info);
            return info;
        }


        [DllImport(PXCMBase.DLLNAME)]
        internal static extern Int64 PXCMAudio_QueryTimeStamp(IntPtr audio);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern Option PXCMAudio_QueryOptions(IntPtr audio);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern void PXCMAudio_SetTimeStamp(IntPtr audio, Int64 ts);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern void PXCMAudio_SetOptions(IntPtr audio, Option options);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMAudio_CopyAudio(IntPtr audio, IntPtr src_audio);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMAudio_AcquireAccess(IntPtr audio, Access access, AudioFormat format, Option options, IntPtr data);

        [StructLayout(LayoutKind.Sequential)]
        private class AudioDataNative : AudioData
        {
            public IntPtr native;
        }

        /** 
        @brief Lock to access the internal storage of a specified format. The function will perform format conversion if unmatched. 
        @param[in]  access		The access mode.
        @param[in]  format		The requested smaple format.
        @param[in]  options     The option flags
        @param[out] data		The sample data storage, to be returned.
        @return PXC_STATUS_NO_ERROR	Successful execution.
        */
        public pxcmStatus AcquireAccess(Access access, AudioFormat format, Option options, out AudioData data)
        {
            AudioDataNative dataNative = new AudioDataNative();
            dataNative.native = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(AudioDataNative)));
            data = dataNative;

            pxcmStatus sts = PXCMAudio_AcquireAccess(instance, access, format, options, dataNative.native);
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
        internal static extern pxcmStatus PXCMAudio_ReleaseAccess(IntPtr audio, IntPtr data);

        /** 
        @brief Unlock the previously acquired buffer.
        @param[in] data		The sample data storage previously acquired.
        @return PXCM_STATUS_NO_ERROR	Successful execution.
        */
        public pxcmStatus ReleaseAccess(AudioData data)
        {
            AudioDataNative dataNative = data as AudioDataNative;
            if (dataNative == null) return pxcmStatus.PXCM_STATUS_HANDLE_INVALID;
            if (dataNative.native == IntPtr.Zero) return pxcmStatus.PXCM_STATUS_HANDLE_INVALID;

            Marshal.WriteInt32(dataNative.native, Marshal.OffsetOf(typeof(AudioData), "dataSize").ToInt32(), unchecked((Int32)data.dataSize));
            pxcmStatus sts = PXCMAudio_ReleaseAccess(instance, dataNative.native);
            Marshal.FreeHGlobal(dataNative.native);
            dataNative.native = IntPtr.Zero;
            return sts;
        }
    };


#if RSSDK_IN_NAMESPACE
}
#endif
