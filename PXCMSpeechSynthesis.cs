using System;
using System.Runtime.InteropServices;

public class PXCMSpeechSynthesis : PXCMBase
{
  public new const int CUID = 1398032726;

  internal PXCMSpeechSynthesis(IntPtr instance, bool delete)
    : base(instance, delete)
  {
  }

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMSpeechSynthesis_QueryProfile(IntPtr tts, int pidx, [Out] ProfileInfo pinfo);

  internal static pxcmStatus QueryProfileINT(IntPtr tts, int pidx, out ProfileInfo pinfo)
  {
    pinfo = new ProfileInfo();
    return PXCMSpeechSynthesis_QueryProfile(tts, pidx, pinfo);
  }

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMSpeechSynthesis_SetProfile(IntPtr tts, ProfileInfo pinfo);

  [DllImport("libpxccpp2c", CharSet = CharSet.Unicode)]
  internal static extern pxcmStatus PXCMSpeechSynthesis_BuildSentence(IntPtr tts, int sid, string sentence);

  [DllImport("libpxccpp2c")]
  internal static extern IntPtr PXCMSpeechSynthesis_QueryBuffer(IntPtr tts, int sid, int idx);

  [DllImport("libpxccpp2c")]
  internal static extern int PXCMSpeechSynthesis_QueryBufferNum(IntPtr tts, int sid);

  [DllImport("libpxccpp2c")]
  internal static extern int PXCMSpeechSynthesis_QuerySampleNum(IntPtr tts, int sid);

  [DllImport("libpxccpp2c")]
  internal static extern void PXCMSpeechSynthesis_ReleaseSentence(IntPtr tts, int sid);

  public pxcmStatus QueryProfile(int pidx, out ProfileInfo pinfo)
  {
    return QueryProfileINT(instance, pidx, out pinfo);
  }

  public pxcmStatus QueryProfile(out ProfileInfo pinfo)
  {
    return QueryProfile(-1, out pinfo);
  }

  public pxcmStatus SetProfile(ProfileInfo pinfo)
  {
    return PXCMSpeechSynthesis_SetProfile(instance, pinfo);
  }

  public pxcmStatus BuildSentence(int sid, string sentence)
  {
    return PXCMSpeechSynthesis_BuildSentence(instance, sid, sentence);
  }

  public PXCMAudio QueryBuffer(int sid, int idx)
  {
    IntPtr instance = PXCMSpeechSynthesis_QueryBuffer(this.instance, sid, idx);
    if (!(instance == IntPtr.Zero))
      return new PXCMAudio(instance, false);
    return null;
  }

  public int QueryBufferNum(int sid)
  {
    return PXCMSpeechSynthesis_QueryBufferNum(instance, sid);
  }

  public int QuerySampleNum(int sid)
  {
    return PXCMSpeechSynthesis_QuerySampleNum(instance, sid);
  }

  public void ReleaseSentence(int sid)
  {
    PXCMSpeechSynthesis_ReleaseSentence(instance, sid);
  }

  [Flags]
  public enum LanguageType
  {
    LANGUAGE_US_ENGLISH = 1398107749,
    LANGUAGE_GB_ENGLISH = 1111977573,
    LANGUAGE_DE_GERMAN = 1162110308,
    LANGUAGE_US_SPANISH = 1398109029,
    LANGUAGE_LA_SPANISH = 1095529317,
    LANGUAGE_FR_FRENCH = 1380348518,
    LANGUAGE_IT_ITALIAN = 1414100073,
    LANGUAGE_JP_JAPANESE = 1347051882,
    LANGUAGE_CN_CHINESE = 1313040506,
    LANGUAGE_BR_PORTUGUESE = 1380086896
  }

  [Flags]
  public enum VoiceType
  {
    VOICE_ANY = 0
  }

  [Serializable]
  [StructLayout(LayoutKind.Sequential)]
  public class ProfileInfo
  {
    public PXCMAudio.AudioInfo outputs;
    public LanguageType language;
    public VoiceType voice;
    public float rate;
    public int volume;
    public int pitch;
    public int eosPauseDuration;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    internal int[] reserved;

    public ProfileInfo()
    {
      outputs = new PXCMAudio.AudioInfo();
      reserved = new int[4];
    }
  }
}
