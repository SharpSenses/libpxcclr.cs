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

public partial class PXCMSpeechRecognition : PXCMBase
{
    new public const Int32 CUID = unchecked((Int32)0x8013C527);
    public const Int32 NBEST_SIZE = 4;
    public const Int32 SENTENCE_BUFFER_SIZE = 1024;
    public const Int32 TAG_BUFFER_SIZE = 1024;

    /// <summary>
    /// NBest
    /// The NBest data structure describes the NBest data returned from the recognition engine.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public class NBest
    {
        /// <summary>
        /// The label that refers to the recognized speech (command list grammars only)
        /// </summary>
        public Int32 label;
        /// <summary>
        /// The confidence score of the recognition: 0-100.
        /// </summary>
        public Int32 confidence;
        /// <summary>
        /// The recognized sentence text data.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = SENTENCE_BUFFER_SIZE)]
        public String sentence;
        /// <summary>
        /// The (grammar) tags of the recognized utterance.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = TAG_BUFFER_SIZE)]
        public String tags;

        public NBest()
        {
            sentence = "";
            tags = "";
        }
    };

    /// <summary>
    /// RecognitionData
    /// The data structure describes the recgonized speech data.
    /// </summary>
    [Serializable]
    public class RecognitionData
    {
        public delegate void OnRecognizedDelegate(RecognitionData data);

        /// <summary>
        /// The time stamp of the recognition, in 100ns. 
        /// </summary>
        public Int64 timeStamp;
        /// <summary>
        /// The grammar identifier for command and control, or zero for dictation. 
        /// </summary>
        public Int32 grammar;
        /// <summary>
        /// The duration of the speech, in ms.
        /// </summary>
        public Int32 duration;
        /// <summary>
        /// The top-N recognition results.
        /// </summary>
        public NBest[] scores;

        public RecognitionData()
        {
            scores = new NBest[NBEST_SIZE];
            for (int i = 0; i < NBEST_SIZE; i++)
            {
                scores[i] = new NBest();
            }
        }
    };

    /// <summary>
    /// AlertType
    /// Enumeratea all supported alert events.
    /// </summary>
    [Flags]
    public enum AlertType
    {
        /// <summary>
        /// The volume is too high.
        /// </summary>
        ALERT_VOLUME_HIGH = 0x00001,
        /// <summary>
        /// The volume is too low. 
        /// </summary>
        ALERT_VOLUME_LOW = 0x00002,
        /// <summary>
        /// Too much noise. 
        /// </summary>
        ALERT_SNR_LOW = 0x00004,
        /// <summary>
        /// There is some speech available but not recognizeable.
        /// </summary>
        ALERT_SPEECH_UNRECOGNIZABLE = 0x00008,
        /// <summary>
        /// The begining of a speech.
        /// </summary>
        ALERT_SPEECH_BEGIN = 0x00010,
        /// <summary>
        /// The end of a speech.
        /// </summary>
        ALERT_SPEECH_END = 0x00020,
        /// <summary>
        /// The recognition is aborted due to device lost, engine error, etc
        /// </summary>
        ALERT_RECOGNITION_ABORTED = 0x00040,
        /// <summary>
        /// The recognition is completed. The audio source no longer provides data.
        /// </summary>
        ALERT_RECOGNITION_END = 0x00080,
    };

    /// <summary>
    /// AlertData
    /// Describe the alert parameters.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class AlertData
    {
        public delegate void OnAlertDelegate(AlertData data);

        /// <summary>
        /// The time stamp of when the alert occurs, in 100ns.
        /// </summary>
        public Int64 timeStamp;
        /// <summary>
        /// The alert event label
        /// </summary>
        public AlertType label;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        internal Int32[] reserved;

        public AlertData()
        {
            reserved = new Int32[6];
        }
    };

    /// <summary> 
    /// LanguageType
    /// Enumerate all supported languages.
    /// </summary>
    [Flags]
    public enum LanguageType
    {
        LANGUAGE_US_ENGLISH = 0x53556e65,       // US English 
        LANGUAGE_GB_ENGLISH = 0x42476e65,       //  British English 
        LANGUAGE_DE_GERMAN = 0x45446564,        //  German 
        LANGUAGE_US_SPANISH = 0x53557365,       //  US Spanish 
        LANGUAGE_LA_SPANISH = 0x414c7365,       //  Latin American Spanish 
        LANGUAGE_FR_FRENCH = 0x52467266,        //  French 
        LANGUAGE_IT_ITALIAN = 0x54497469,       //  Italian 
        LANGUAGE_JP_JAPANESE = 0x504a616a,      //  Japanese 
        LANGUAGE_CN_CHINESE = 0x4e43687a,       //  Simplified Chinese 
        LANGUAGE_BR_PORTUGUESE = 0x52427470,    //  Portuguese 
    };

    /// <summary>
    /// Describe the algorithm configuration parameters.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public class ProfileInfo
    {
        /// <summary>
        /// The optional speaker name for adaptation 
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public String speaker;
        /// <summary>
        /// The supported language 
        /// </summary>
        public LanguageType language;
        /// <summary>
        /// The length of end of sentence silence in ms
        /// </summary>
        public Int32 endOfSentence;
        /// <summary>
        /// The recognition confidence threshold: 0-100
        /// </summary>
        public Int32 threshold;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
        internal Int32[] reserved;

        public ProfileInfo()
        {
            speaker = "";
            reserved = new Int32[13];
        }
    };

    /// <summary> 
    /// GrammarFileType
    /// Enumerate all supported grammar file types.
    /// </summary>
    public enum GrammarFileType
    {
        /// <summary>
        ///  unspecified type, use filename extension
        /// </summary>
        GFT_NONE = 0,
        /// <summary>
        /// text file, list of commands
        /// </summary>
        GFT_LIST = 1,
        /// <summary>
        /// Java Speech Grammar Format
        /// </summary>
        GFT_JSGF = 2,
        /// <summary>
        /// Previously compiled format (vendor specific)
        /// </summary>
        GFT_COMPILED_CONTEXT = 5,
    };

    /// <summary> 
    /// VocabFileType
    /// Enumerate all supported vocabulary file types.
    /// </summary>
    public enum VocabFileType
    {
        /// <summary>
        /// unspecified type, use filename extension
        /// </summary>
        VFT_NONE = 0,
        /// <summary>
        /// text file
        /// </summary>
        VFT_LIST = 1,
    };

    public class Handler
    {
        /// <summary>
        /// The function is invoked when there is some speech recognized.
        /// </summary>
        /// <param name="data"> The data structure to describe the recognized speech.</param>
        public delegate void OnRecognitionDelegate(RecognitionData data);

        /// <summary>
        /// The function is triggered by any alert event.
        /// </summary>
        /// <param name="data"> The data structure to describe the alert.</param>
        public delegate void OnAlertDelegate(AlertData data);

        public OnRecognitionDelegate onRecognition;
        public OnAlertDelegate onAlert;
    };

    /// <summary>
    /// The function returns the available algorithm configurations.
    /// </summary>
    /// <param name="idx"> The zero-based index to retrieve all algorithm configurations.</param>
    /// <param name="pinfo"> The algorithm configuration, to be returned.</param>
    /// <returns> PXCM_STATUS_NO_ERROR			Successful execution.</returns>
    /// <returns> PXCM_STATUS_ITEM_UNAVAILABLE	There is no more configuration.</returns>
    public pxcmStatus QueryProfile(Int32 idx, out ProfileInfo pinfo)
    {
        return QueryProfileINT(idx, out pinfo);
    }

    /// <summary>
    /// The function returns the working algorithm configurations.
    /// </summary>
    /// <param name="pinfo"> The algorithm configuration, to be returned.</param>
    /// <returns> PXCM_STATUS_NO_ERROR			Successful execution.</returns>
    public pxcmStatus QueryProfile(out ProfileInfo pinfo)
    {
        return QueryProfile(WORKING_PROFILE, out pinfo);
    }

    /// <summary>
    /// The function sets the working algorithm configurations. 
    /// </summary>
    /// <param name="config"> The algorithm configuration.</param>
    /// <returns> PXCM_STATUS_NO_ERROR			Successful execution.</returns>
    public pxcmStatus SetProfile(ProfileInfo pinfo)
    {
        return PXCMSpeechRecognition_SetProfile(instance, pinfo);
    }

    /// <summary> 
    /// The function builds the recognition grammar from the list of strings. 
    /// </summary>
    /// <param name="gid"> The grammar identifier. Can be any non-zero number.</param>
    /// <param name="cmds"> The string list.</param>
    /// <param name="labels"> Optional list of labels. If not provided, the labels are 1...ncmds.</param>
    /// <param name="ncmds"> The number of strings in the string list.</param>
    /// <returns> PXCM_STATUS_NO_ERROR			Successful execution.</returns>
    public pxcmStatus BuildGrammarFromStringList(Int32 gid, String[] cmds, Int32[] labels)
    {
        return PXCMSpeechRecognition_BuildGrammarFromStringList(instance, gid, cmds, labels, cmds.Length);
    }

    /// <summary> 
    /// The function deletes the specified grammar and releases any resources allocated.
    /// </summary>
    /// <param name="gid"> The grammar identifier.</param>
    /// <returns> PXCM_STATUS_NO_ERROR				Successful execution.</returns>
    /// <returns> PXCM_STATUS_ITEM_UNAVAILABLE		The grammar is not found.</returns>
    public pxcmStatus ReleaseGrammar(Int32 gid)
    {
        return PXCMSpeechRecognition_ReleaseGrammar(instance, gid);
    }

    /// <summary> 
    /// The function sets the active grammar for recognition.
    /// </summary>
    /// <param name="gid"> The grammar identifier.</param>
    /// <returns> PXCM_STATUS_NO_ERROR				Successful execution.</returns>
    /// <returns> PXCM_STATUS_ITEM_UNAVAILABLE		The grammar is not found.</returns>
    public pxcmStatus SetGrammar(Int32 gid)
    {
        return PXCMSpeechRecognition_SetGrammar(instance, gid);
    }

    /// <summary> 
    /// The function sets the dictation recognition mode. 
    /// The function may take some time to initialize.
    /// </summary>
    /// <returns> PXCM_STATUS_NO_ERROR				Successful execution.</returns>
    public pxcmStatus SetDictation()
    {
        return SetGrammar(0);
    }

    /// <summary> 
    /// The function starts voice recognition.
    /// </summary>
    /// <param name="source"> The audio source.</param>
    /// <param name="handler"> The callback handler instance.</param>
    /// <returns> PXCM_STATUS_NO_ERROR				Successful execution.</returns>
    public pxcmStatus StartRec(PXCMAudioSource source, Handler handler)
    {
        if (handler == null) return pxcmStatus.PXCM_STATUS_HANDLE_INVALID;
        return StartRecINT(source != null ? source.instance : IntPtr.Zero, handler);
    }

    /// <summary> 
    /// The function stops voice recognition.
    /// </summary>
    public void StopRec()
    {
        StopRecINT();
    }

    /// <summary> 
    /// The function create grammar from file
    /// </summary>
    /// <param name="gid"> The grammar identifier. Can be any non-zero number.</param>
    /// <param name="fileType"> The file type from GrammarFileType structure.</param>
    /// <param name="grammarFilename"> The full path to file.</param>
    /// <returns> PXC_STATUS_NO_ERROR                Successful execution.</returns>
    /// <returns> PXC_STATUS_EXEC_ABORTED            Incorrect file extension.</returns>
    public pxcmStatus BuildGrammarFromFile(Int32 gid, GrammarFileType fileType, String grammarFilename)
    {
        return PXCMSpeechRecognition_BuildGrammarFromFile(instance, gid, fileType, grammarFilename);
    }

    /// <summary> 
    /// The function create grammar from memory
    /// </summary>
    /// <param name="gid"> The grammar identifier. Can be any non-zero number.</param>
    /// <param name="fileType"> The file type from GrammarFileType structure.</param>
    /// <param name="grammarMemory"> The grammar specification.</param>
    /// <param name="memSize"> The size of grammar specification.</param>
    /// <returns> PXC_STATUS_NO_ERROR                Successful execution.</returns>
    /// <returns> PXC_STATUS_EXEC_ABORTED            Incorrect file type.</returns>
    /// <returns> PXC_STATUS_HANDLE_INVALID          Incorect memSize or grammarMemory equal NULL.</returns>
    public pxcmStatus BuildGrammarFromMemory(Int32 gid, GrammarFileType fileType, Byte[] grammarMemory)
    {
        return PXCMSpeechRecognition_BuildGrammarFromMemory(instance, gid, fileType, grammarMemory, grammarMemory.Length);
    }

    /// <summary> 
    /// The function get array with error
    /// </summary>
    /// <param name="gid"> The grammar identifier. Can be any non-zero number.</param>
    /// <returns> pxcCHAR *                NULL terminated array with error or NULL in case of internal error.</returns>
    public String GetGrammarCompileErrors(Int32 gid)
    {
        return GetGrammarCompileErrorsINT(instance, gid);
    }

    /// <summary>
    /// The function add file with vocabulary
    /// </summary>
    /// <param name="fileType"> The vocabulary file type</param>
    /// <param name="vocabFileName"> The full path to file.</param>
    /// <returns> PXC_STATUS_NO_ERROR     Successful execution.</returns>
    public pxcmStatus AddVocabToDictation(VocabFileType fileType, String vocabFileName)
    {
        return PXCMSpeechRecognition_AddVocabToDictation(instance, fileType, vocabFileName);
    }

    internal PXCMSpeechRecognition(IntPtr instance, Boolean delete)
        : base(instance, delete)
    {
    }
};

#if RSSDK_IN_NAMESPACE
}
#endif
