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

        /**
            @struct NBest
            The NBest data structure describes the NBest data returned from the recognition engine.
        */
        [Serializable]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public class NBest
        {
            public Int32 label;             /** The label that refers to the recognized speech (command list grammars only) */
            public Int32 confidence;        /** The confidence score of the recognition: 0-100. */
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = SENTENCE_BUFFER_SIZE)]
            public String sentence;         /** The recognized sentence text data. */
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = TAG_BUFFER_SIZE)]
            public String tags;             /** The (grammar) tags of the recognized utterance. */

            public NBest()
            {
                sentence = "";
                tags = "";
            }
        };

        /**
            @struct RecognitionData
            The data structure describes the recgonized speech data.
        */
        [Serializable]
        public class RecognitionData
        {
            public delegate void OnRecognizedDelegate(RecognitionData data);

            public Int64 timeStamp;     /** The time stamp of the recognition, in 100ns. */
            public Int32 grammar;       /** The grammar identifier for command and control, or zero for dictation. */
            public Int32 duration;      /** The duration of the speech, in ms. */
            public NBest[] scores;      /** The top-N recognition results. */

            public RecognitionData()
            {
                scores = new NBest[NBEST_SIZE];
                for (int i = 0; i < NBEST_SIZE; i++)
                {
                    scores[i] = new NBest();
                }
            }
        };

        /**
            @enum AlertType
            Enumeratea all supported alert events.
        */
        [Flags]
        public enum AlertType
        {
            ALERT_VOLUME_HIGH = 0x00001,      /** The volume is too high. */
            ALERT_VOLUME_LOW = 0x00002,      /** The volume is too low. */
            ALERT_SNR_LOW = 0x00004,      /** Too much noise. */
            ALERT_SPEECH_UNRECOGNIZABLE = 0x00008,      /** There is some speech available but not recognizeable. */
            ALERT_SPEECH_BEGIN = 0x00010,      /** The begining of a speech */
            ALERT_SPEECH_END = 0x00020,      /** The end of a speech */
            ALERT_RECOGNITION_ABORTED = 0x00040,      /** The recognition is aborted due to device lost, engine error, etc */
            ALERT_RECOGNITION_END = 0x00080,      /** The recognition is completed. The audio source no longer provides data. */
        };

        /**
            @struct AlertData
            Describe the alert parameters.
        */
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public class AlertData
        {
            public delegate void OnAlertDelegate(AlertData data);

            public Int64 timeStamp;     /** The time stamp of when the alert occurs, in 100ns. */
            public AlertType label;         /** The alert event label */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            internal Int32[] reserved;

            public AlertData()
            {
                reserved = new Int32[6];
            }
        };

        /** 
            @enum LanguageType
            Enumerate all supported languages.
        */
        [Flags]
        public enum LanguageType
        {
            LANGUAGE_US_ENGLISH = 0x53556e65,       /** US English */
            LANGUAGE_GB_ENGLISH = 0x42476e65,       /** British English */
            LANGUAGE_DE_GERMAN = 0x45446564,        /** German */
            LANGUAGE_US_SPANISH = 0x53557365,       /** US Spanish */
            LANGUAGE_LA_SPANISH = 0x414c7365,       /** Latin American Spanish */
            LANGUAGE_FR_FRENCH = 0x52467266,        /** French */
            LANGUAGE_IT_ITALIAN = 0x54497469,       /** Italian */
            LANGUAGE_JP_JAPANESE = 0x504a616a,      /** Japanese */
            LANGUAGE_CN_CHINESE = 0x4e43687a,       /** Simplified Chinese */
            LANGUAGE_BR_PORTUGUESE = 0x52427470,    /** Portuguese */
        };

        /**
            Describe the algorithm configuration parameters.
        */
        [Serializable]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public class ProfileInfo
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public String speaker;          /** The optional speaker name for adaptation */
            public LanguageType language;         /** The supported language */
            public Int32 endOfSentence;    /** The length of end of sentence silence in ms */
            public Int32 threshold;        /** The recognition confidence threshold: 0-100 */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
            internal Int32[] reserved;

            public ProfileInfo()
            {
                speaker = "";
                reserved = new Int32[13];
            }
        };

        /** 
            @enum GrammarFileType
            Enumerate all supported grammar file types.
        */
        public enum GrammarFileType
        {
            GFT_NONE = 0,  /**  unspecified type, use filename extension */
            GFT_LIST = 1,  /**  text file, list of commands */
            GFT_JSGF = 2,  /**  Java Speech Grammar Format */
            GFT_COMPILED_CONTEXT = 5,  /**  Previously compiled format (vendor specific) */
        };

        /** 
            @enum VocabFileType
            Enumerate all supported vocabulary file types.
        */
        public enum VocabFileType
        {
            VFT_NONE = 0,  /**  unspecified type, use filename extension */
            VFT_LIST = 1,  /**  text file*/
        };

        public class Handler
        {
            /**
                @brief The function is invoked when there is some speech recognized.
                @param[in] data			The data structure to describe the recognized speech.
            */
            public delegate void OnRecognitionDelegate(RecognitionData data);

            /**
                @brief The function is triggered by any alert event.
                @param[in] data			The data structure to describe the alert.
            */
            public delegate void OnAlertDelegate(AlertData data);

            public OnRecognitionDelegate onRecognition;
            public OnAlertDelegate onAlert;
        };

        /**
            @brief The function returns the available algorithm configurations.
            @param[in]	idx			The zero-based index to retrieve all algorithm configurations.
            @param[out] pinfo		The algorithm configuration, to be returned.
            @return PXCM_STATUS_NO_ERROR			Successful execution.
            @return PXCM_STATUS_ITEM_UNAVAILABLE	There is no more configuration.
        */
        public pxcmStatus QueryProfile(Int32 idx, out ProfileInfo pinfo)
        {
            return QueryProfileINT(idx, out pinfo);
        }

        /**
            @brief The function returns the working algorithm configurations.
            @param[out] pinfo		The algorithm configuration, to be returned.
            @return PXCM_STATUS_NO_ERROR			Successful execution.
        */
        public pxcmStatus QueryProfile(out ProfileInfo pinfo)
        {
            return QueryProfile(WORKING_PROFILE, out pinfo);
        }

        /**
            @brief The function sets the working algorithm configurations. 
            @param[in] config		The algorithm configuration.
            @return PXCM_STATUS_NO_ERROR			Successful execution.
        */
        public pxcmStatus SetProfile(ProfileInfo pinfo)
        {
            return PXCMSpeechRecognition_SetProfile(instance, pinfo);
        }

        /** 
            @brief The function builds the recognition grammar from the list of strings. 
            @param[in] gid			The grammar identifier. Can be any non-zero number.
            @param[in] cmds			The string list.
            @param[in] labels		Optional list of labels. If not provided, the labels are 1...ncmds.
            @param[in] ncmds		The number of strings in the string list.
            @return PXCM_STATUS_NO_ERROR			Successful execution.
        */
        public pxcmStatus BuildGrammarFromStringList(Int32 gid, String[] cmds, Int32[] labels)
        {
            return PXCMSpeechRecognition_BuildGrammarFromStringList(instance, gid, cmds, labels, cmds.Length);
        }

        /** 
            @brief The function deletes the specified grammar and releases any resources allocated.
            @param[in] gid			The grammar identifier.
            @return PXCM_STATUS_NO_ERROR				Successful execution.
            @return PXCM_STATUS_ITEM_UNAVAILABLE		The grammar is not found.
        */
        public pxcmStatus ReleaseGrammar(Int32 gid)
        {
            return PXCMSpeechRecognition_ReleaseGrammar(instance, gid);
        }

        /** 
            @brief The function sets the active grammar for recognition.
            @param[in] gid			The grammar identifier.
            @return PXCM_STATUS_NO_ERROR				Successful execution.
            @return PXCM_STATUS_ITEM_UNAVAILABLE		The grammar is not found.
        */
        public pxcmStatus SetGrammar(Int32 gid)
        {
            return PXCMSpeechRecognition_SetGrammar(instance, gid);
        }

        /** 
            @brief The function sets the dictation recognition mode. 
            The function may take some time to initialize.
            @return PXCM_STATUS_NO_ERROR				Successful execution.
        */
        public pxcmStatus SetDictation()
        {
            return SetGrammar(0);
        }

        /** 
            @brief The function starts voice recognition.
            @param[in] source				The audio source.
            @param[in] handler				The callback handler instance.
            @return PXCM_STATUS_NO_ERROR				Successful execution.
        */
        public pxcmStatus StartRec(PXCMAudioSource source, Handler handler)
        {
            if (handler == null) return pxcmStatus.PXCM_STATUS_HANDLE_INVALID;
            return StartRecINT(source != null ? source.instance : IntPtr.Zero, handler);
        }

        /** 
            @brief The function stops voice recognition.
        */
        public void StopRec()
        {
            StopRecINT();
        }

        /** 
            @brief The function create grammar from file
            @param[in] gid                  The grammar identifier. Can be any non-zero number.
            @param[in] fileType             The file type from GrammarFileType structure.
            @param[in] grammarFilename      The full path to file.
            @return PXC_STATUS_NO_ERROR                Successful execution.
            @return PXC_STATUS_EXEC_ABORTED            Incorrect file extension.
        */
        public pxcmStatus BuildGrammarFromFile(Int32 gid, GrammarFileType fileType, String grammarFilename)
        {
            return PXCMSpeechRecognition_BuildGrammarFromFile(instance, gid, fileType, grammarFilename);
        }

        /** 
            @brief The function create grammar from memory
            @param[in] gid                  The grammar identifier. Can be any non-zero number.
            @param[in] fileType             The file type from GrammarFileType structure.
            @param[in] grammarMemory        The grammar specification.
            @param[in] memSize              The size of grammar specification.
            @return PXC_STATUS_NO_ERROR                Successful execution.
            @return PXC_STATUS_EXEC_ABORTED            Incorrect file type.
            @return PXC_STATUS_HANDLE_INVALID          Incorect memSize or grammarMemory equal NULL.
        */
        public pxcmStatus BuildGrammarFromMemory(Int32 gid, GrammarFileType fileType, Byte[] grammarMemory)
        {
            return PXCMSpeechRecognition_BuildGrammarFromMemory(instance, gid, fileType, grammarMemory, grammarMemory.Length);
        }

        /** 
            @brief The function get array with error
            @param[in] gid                  The grammar identifier. Can be any non-zero number.
            @return pxcCHAR *                NULL terminated array with error or NULL in case of internal error.
        */
        public String GetGrammarCompileErrors(Int32 gid)
        {
            return GetGrammarCompileErrorsINT(instance, gid);
        }

        /**
            @brief The function add file with vocabulary
            @param[in] fileType             The vocabulary file type
            @param[in] vocabFileName        The full path to file.
            @return PXC_STATUS_NO_ERROR     Successful execution.
        */
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