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

    public partial class PXCMEmotion : PXCMBase
    {
        [DllImport(PXCMBase.DLLNAME)]
        internal static extern Int32 PXCMEmotion_QueryNumFaces(IntPtr emotion);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern Int32 PXCMEmotion_QueryEmotionSize(IntPtr emotion);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMEmotion_QueryEmotionData(IntPtr emotion, Int32 fid, Emotion eid, [Out] EmotionData data);

        internal static pxcmStatus QueryEmotionDataINT(IntPtr emotion, Int32 fid, Emotion eid, out EmotionData data)
        {
            data = new EmotionData();
            return PXCMEmotion_QueryEmotionData(emotion, fid, eid, data);
        }

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMEmotion_QueryAllEmotionData(IntPtr emotion, Int32 fid, IntPtr data);

        internal static pxcmStatus QueryAllEmotionDataINT(IntPtr emotion, Int32 fid, out EmotionData[] data)
        {
            Int32 data_size = PXCMEmotion_QueryEmotionSize(emotion);
            IntPtr data2 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(EmotionData)) * data_size);
            pxcmStatus sts = PXCMEmotion_QueryAllEmotionData(emotion, fid, data2);
            if (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR)
            {
                data = new EmotionData[data_size];
                for (int i = 0; i < data.Length; i++)
                {
                    data[i] = new EmotionData();
                    Marshal.PtrToStructure(new IntPtr(data2.ToInt64() + i * Marshal.SizeOf(typeof(EmotionData))), data[i]);
                }
            }
            else
            {
                data = null;
            }
            Marshal.FreeHGlobal(data2);
            return sts;
        }
    };

#if RSSDK_IN_NAMESPACE
}
#endif
