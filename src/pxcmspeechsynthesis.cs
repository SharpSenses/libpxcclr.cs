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

public partial class PXCMSpeechSynthesis : PXCMBase
{
    new public const Int32 CUID = 0x53544956;

    /// <summary>
    /// LanguageType
    /// Enumerate all supported languages.
    /// </summary>
    [Flags]
    public enum LanguageType
    {
        LANGUAGE_US_ENGLISH = 0x53556e65,       // US English
        LANGUAGE_GB_ENGLISH = 0x42476e65,       // British English  
        LANGUAGE_DE_GERMAN = 0x45446564,        // German  
        LANGUAGE_US_SPANISH = 0x53557365,       // US Spanish  
        LANGUAGE_LA_SPANISH = 0x414c7365,       // Latin American Spanish  
        LANGUAGE_FR_FRENCH = 0x52467266,        // French  
        LANGUAGE_IT_ITALIAN = 0x54497469,       // Italian  
        LANGUAGE_JP_JAPANESE = 0x504a616a,      // Japanese  
        LANGUAGE_CN_CHINESE = 0x4e43687a,       // Simplified Chinese  
        LANGUAGE_BR_PORTUGUESE = 0x52427470,    // Portuguese  
    };

    /// <summary> 
    /// VoiceType
    /// Enumerate all supported voices.
    /// </summary>
    [Flags]
    public enum VoiceType
    {
        VOICE_ANY = 0,      /* Any available voice */
    };

    /// <summary>
    /// ProfileInfo
    /// Describe the algorithm configuration parameters.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class ProfileInfo
    {
        /// <summary>
        /// The synthesized audio format. Adjust bufferSize for the required latency.
        /// </summary>
        public PXCMAudio.AudioInfo outputs;
        /// <summary>
        /// The supported language
        /// </summary>
        public LanguageType language;
        /// <summary>
        /// The voice
        /// </summary>
        public VoiceType voice;
        /// <summary>
        /// The speaking speed. The default is 100. Smaller is slower and bigger is faster.
        /// </summary>
        public Single rate;
        /// <summary>
        /// The speaking volume from 0 to 100 (loudest).
        /// </summary>
        public Int32 volume;
        /// <summary>
        /// default pitch is 100. range [50 to 200]
        /// </summary>
        public Int32 pitch;
        /// <summary>
        /// End of sentence wait duration. range [0 to 9 multiplied by 200msec]
        /// </summary>
        public Int32 eosPauseDuration;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        internal Int32[] reserved;

        public ProfileInfo()
        {
            outputs = new PXCMAudio.AudioInfo();
            reserved = new Int32[4];
        }
    };

    /// <summary>
    /// The function returns the available algorithm configuration parameters.
    /// </summary>
    /// <param name="pidx"> The zero-based index to retrieve all configuration parameters.</param>
    /// <param name="pinfo"> The configuration parameters, to be returned.</param>
    /// <returns> PXCM_STATUS_NO_ERROR				Successful execution.</returns>
    /// <returns> PXCM_STATUS_ITEM_UNAVAILABLE		No more configurations.</returns>
    public pxcmStatus QueryProfile(Int32 pidx, out ProfileInfo pinfo)
    {
        return QueryProfileINT(instance, pidx, out pinfo);
    }

    /// <summary>
    /// The function returns the current working algorithm configuration parameters.
    /// </summary>
    /// <param name="pinfo"> The configuration parameters, to be returned.</param>
    /// <returns> PXCM_STATUS_NO_ERROR				Successful execution.</returns>
    public pxcmStatus QueryProfile(out ProfileInfo pinfo)
    {
        return QueryProfile(WORKING_PROFILE, out pinfo);
    }

    /// <summary>
    /// The function sets the current working algorithm configuration parameters.
    /// </summary>
    /// <param name="pinfo"> The configuration parameters.</param>
    /// <returns> PXCM_STATUS_NO_ERROR				Successful execution.</returns>
    public pxcmStatus SetProfile(ProfileInfo pinfo)
    {
        return PXCMSpeechSynthesis_SetProfile(instance, pinfo);
    }

    /// <summary>
    /// The function synthesizes the sentence for later use. The function may take some time
    /// to generate the fully synthesized speech.
    /// </summary>
    /// <param name="sid"> The sentence identifier. Can be any non-zero unique number.</param>
    /// <param name="sentence"> The sentence string.</param>
    /// <returns> PXC_STATUS_NO_ERROR		Successful execution.</returns>
    public pxcmStatus BuildSentence(Int32 sid, String sentence)
    {
        return PXCMSpeechSynthesis_BuildSentence(instance, sid, sentence);
    }

    /// <summary>
    /// The function retrieves the PXCAudio buffer for the specified sentence. There could be more
    /// than one PXCAudio buffer. The application should keep retrieving with increased index, until the 
    /// function returns NULL. The audio buffer is internally managed. Do not release the instance.
    /// </summary>
    /// <param name="sid"> The sentence identifier.</param>
    /// <param name="idx"> The zero-based index to retrieve multiple samples.</param>
    /// <returns> the Audio buffer, or NULL if there is no more.</returns>
    public PXCMAudio QueryBuffer(Int32 sid, Int32 idx)
    {
        IntPtr audio = PXCMSpeechSynthesis_QueryBuffer(instance, sid, idx);
        return audio == IntPtr.Zero ? null : new PXCMAudio(audio, false);
    }

    /// <summary>
    /// The function returns the number of PXCAudio buffers used for the specified 
    /// synthesized sentence.
    /// </summary>
    /// <param name="sid"> The sentence identifier.</param>
    /// <returns> the number of PXCAudio buffers, or 0 if the sentence is not found.</returns>
    public Int32 QueryBufferNum(Int32 sid)
    {
        return PXCMSpeechSynthesis_QueryBufferNum(instance, sid);
    }

    /// <summary>
    /// The function returns the number of audio samples for the specified synthesized sentence. 
    /// Each audio sample consists of multiple channels according to the format definition.
    /// </summary>
    /// <param name="sid"> The sentence identifier.</param>
    /// <returns> the sample number, or 0 if the sentence is not found.</returns>
    public Int32 QuerySampleNum(Int32 sid)
    {
        return PXCMSpeechSynthesis_QuerySampleNum(instance, sid);
    }

    /// <summary>
    /// The function releases any resources allocated for the sentence identifier.
    /// </summary>
    /// <param name="sid"> The sentence identifier.</param>
    public void ReleaseSentence(Int32 sid)
    {
        PXCMSpeechSynthesis_ReleaseSentence(instance, sid);
    }

    /* constructors and misc */
    internal PXCMSpeechSynthesis(IntPtr instance, Boolean delete)
        : base(instance, delete)
    {
    }
};

#if RSSDK_IN_NAMESPACE
}
#endif
