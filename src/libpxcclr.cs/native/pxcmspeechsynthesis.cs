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

    public partial class PXCMSpeechSynthesis : PXCMBase
    {
        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMSpeechSynthesis_QueryProfile(IntPtr tts, Int32 pidx, [Out] ProfileInfo pinfo);

        internal static pxcmStatus QueryProfileINT(IntPtr tts, Int32 pidx, out ProfileInfo pinfo)
        {
            pinfo = new ProfileInfo();
            return PXCMSpeechSynthesis_QueryProfile(tts, pidx, pinfo);
        }

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMSpeechSynthesis_SetProfile(IntPtr tts, ProfileInfo pinfo);

        [DllImport(PXCMBase.DLLNAME, CharSet = CharSet.Unicode)]
        internal static extern pxcmStatus PXCMSpeechSynthesis_BuildSentence(IntPtr tts, Int32 sid, String sentence);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern IntPtr PXCMSpeechSynthesis_QueryBuffer(IntPtr tts, Int32 sid, Int32 idx);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern Int32 PXCMSpeechSynthesis_QueryBufferNum(IntPtr tts, Int32 sid);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern Int32 PXCMSpeechSynthesis_QuerySampleNum(IntPtr tts, Int32 sid);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern void PXCMSpeechSynthesis_ReleaseSentence(IntPtr tts, Int32 sid);
    };

#if RSSDK_IN_NAMESPACE
}
#endif
