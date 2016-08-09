/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2013-2014 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Runtime.InteropServices;
using UnityEngine;

public partial class PXCMSpeechSynthesis:PXCMBase {
   
    /// <summary>
    /// The function retrieves the synthesized speech as an Unity AudioClip object.
    /// </summary>
    /// <param name="name">The audio clip name</param>
    /// <param name="sid">The sentence identifier</param>
    /// <returns>the AudioClip instance, or null if there is any error.</returns>
    public AudioClip QueryAudioClip(String name, Int32 sid) {
        int nsamples=QuerySampleNum(sid);
        if (nsamples==0) return null;
        ProfileInfo pinfo;
        QueryProfile(out pinfo);
        AudioClip clip = AudioClip.Create(name, nsamples * pinfo.outputs.nchannels,
            pinfo.outputs.nchannels, pinfo.outputs.sampleRate, false, false);
        int nbuffers = QueryBufferNum(sid);
        for (int i = 0, offset=0; i < nbuffers; i++)
        {
            PXCMAudio audio = QueryBuffer(sid, i);
            PXCMAudio.AudioData data;
            pxcmStatus sts=audio.AcquireAccess(PXCMAudio.Access.ACCESS_READ,PXCMAudio.AudioFormat.AUDIO_FORMAT_IEEE_FLOAT, out data);
            if (sts < pxcmStatus.PXCM_STATUS_NO_ERROR) break;
            float[] samples=data.ToFloatArray();
            clip.SetData(data.ToFloatArray(), offset);
            offset += samples.Length;
            audio.ReleaseAccess(data);
        }
        return clip;
    }
};
