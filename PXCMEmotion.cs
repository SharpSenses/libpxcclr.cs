using System;
using System.Runtime.InteropServices;

public class PXCMEmotion : PXCMBase
{
  public new const int CUID = 1314147653;

  internal PXCMEmotion(IntPtr instance, bool delete)
    : base(instance, delete)
  {
  }

  [DllImport("libpxccpp2c")]
  internal static extern int PXCMEmotion_QueryNumFaces(IntPtr emotion);

  [DllImport("libpxccpp2c")]
  internal static extern int PXCMEmotion_QueryEmotionSize(IntPtr emotion);

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMEmotion_QueryEmotionData(IntPtr emotion, int fid, Emotion eid, [Out] EmotionData data);

  internal static pxcmStatus QueryEmotionDataINT(IntPtr emotion, int fid, Emotion eid, out EmotionData data)
  {
    data = new EmotionData();
    return PXCMEmotion_QueryEmotionData(emotion, fid, eid, data);
  }

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMEmotion_QueryAllEmotionData(IntPtr emotion, int fid, IntPtr data);

  internal static pxcmStatus QueryAllEmotionDataINT(IntPtr emotion, int fid, out EmotionData[] data)
  {
    int length = PXCMEmotion_QueryEmotionSize(emotion);
    IntPtr num = Marshal.AllocHGlobal(Marshal.SizeOf(typeof (EmotionData)) * length);
    pxcmStatus pxcmStatus = PXCMEmotion_QueryAllEmotionData(emotion, fid, num);
    if (pxcmStatus >= pxcmStatus.PXCM_STATUS_NO_ERROR)
    {
      data = new EmotionData[length];
      for (int index = 0; index < data.Length; ++index)
      {
        data[index] = new EmotionData();
        Marshal.PtrToStructure(new IntPtr(num.ToInt64() + index * Marshal.SizeOf(typeof (EmotionData))), data[index]);
      }
    }
    else
      data = null;
    Marshal.FreeHGlobal(num);
    return pxcmStatus;
  }

  public int QueryNumFaces()
  {
    return PXCMEmotion_QueryNumFaces(instance);
  }

  public int QueryEmotionSize()
  {
    return PXCMEmotion_QueryEmotionSize(instance);
  }

  public pxcmStatus QueryEmotionData(int fid, Emotion eid, out EmotionData data)
  {
    return QueryEmotionDataINT(instance, fid, eid, out data);
  }

  public pxcmStatus QueryAllEmotionData(int fid, out EmotionData[] data)
  {
    return QueryAllEmotionDataINT(instance, fid, out data);
  }

  [Flags]
  public enum Emotion
  {
    EMOTION_PRIMARY_ANGER = 1,
    EMOTION_PRIMARY_CONTEMPT = 2,
    EMOTION_PRIMARY_DISGUST = 4,
    EMOTION_PRIMARY_FEAR = 8,
    EMOTION_PRIMARY_JOY = 16,
    EMOTION_PRIMARY_SADNESS = 32,
    EMOTION_PRIMARY_SURPRISE = 64,
    EMOTION_SENTIMENT_POSITIVE = 65536,
    EMOTION_SENTIMENT_NEGATIVE = 131072,
    EMOTION_SENTIMENT_NEUTRAL = 262144
  }

  [Serializable]
  [StructLayout(LayoutKind.Sequential)]
  public class EmotionData
  {
    public long timeStamp;
    public Emotion emotion;
    public int fid;
    public Emotion eid;
    public float intensity;
    public int evidence;
    public PXCMRectI32 rectangle;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    internal int[] reserved;

    public EmotionData()
    {
      reserved = new int[8];
    }
  }
}
