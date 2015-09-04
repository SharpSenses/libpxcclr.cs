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
        new public const Int32 CUID = 0x4e544d45;

        [Flags]
        public enum Emotion
        {
            EMOTION_PRIMARY_ANGER = 0x00000001,	// primary emotion ANGER
            EMOTION_PRIMARY_CONTEMPT = 0x00000002,	// primary emotion CONTEMPT
            EMOTION_PRIMARY_DISGUST = 0x00000004,	// primary emotion DISGUST
            EMOTION_PRIMARY_FEAR = 0x00000008,	// primary emotion FEAR
            EMOTION_PRIMARY_JOY = 0x00000010,	// primary emotion JOY
            EMOTION_PRIMARY_SADNESS = 0x00000020,	// primary emotion SADNESS
            EMOTION_PRIMARY_SURPRISE = 0x00000040,	// primary emotion SURPRISE

            EMOTION_SENTIMENT_POSITIVE = 0x00010000,	// Overall sentiment: POSITIVE
            EMOTION_SENTIMENT_NEGATIVE = 0x00020000,	// Overall sentiment: NEGATIVE
            EMOTION_SENTIMENT_NEUTRAL = 0x00040000,	// Overall sentiment: NEUTRAL
        };

        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public class EmotionData
        {
            public Int64 timeStamp;      // Time stamp in 100ns when the emotion data is detected   
            public Emotion emotion;
            public Int32 fid;            // Face ID
            public Emotion eid;            // Emotion identifier
            public Single intensity;      // In range [0,1]. The intensity value is the detection output
            public Int32 evidence;       // The evidence value, between -5 and 5.
            public PXCMRectI32 rectangle;      // Detected face rectangle
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            internal Int32[] reserved;

            public EmotionData()
            {
                reserved = new Int32[8];
            }
        };


        /// Query the total number of detected faces for a given frame.
        public Int32 QueryNumFaces()
        {
            return PXCMEmotion_QueryNumFaces(instance);
        }

        /// Query the total number of detected emotions for a given frame.
        public Int32 QueryEmotionSize()
        {
            return PXCMEmotion_QueryEmotionSize(instance);
        }

        /// Get Emotion data of the specified face and emotion.
        /// fid     The face ID, zero-based
        /// eid     The emotion identifier
        /// data    The EmotionData data structure, to be returned
        public pxcmStatus QueryEmotionData(Int32 fid, Emotion eid, out EmotionData data)
        {
            return QueryEmotionDataINT(instance, fid, eid, out data);
        }

        /// Get all Emotion data of a specified face.
        /// fid      The face ID, zero-based
        /// data     The array of EmotionData data structures, to be returned
        public pxcmStatus QueryAllEmotionData(Int32 fid, out EmotionData[] data)
        {
            return QueryAllEmotionDataINT(instance, fid, out data);
        }

        internal PXCMEmotion(IntPtr instance, Boolean delete)
            : base(instance, delete)
        {
        }
    };

#if RSSDK_IN_NAMESPACE
}
#endif
