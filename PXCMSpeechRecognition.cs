using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

public class PXCMSpeechRecognition : PXCMBase
{
  public new const int CUID = -2146187993;
  public const int NBEST_SIZE = 4;
  public const int SENTENCE_BUFFER_SIZE = 1024;
  public const int TAG_BUFFER_SIZE = 1024;
  private HandlerDIR handlerDIR;

  internal PXCMSpeechRecognition(IntPtr instance, bool delete)
    : base(instance, delete)
  {
  }

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMSpeechRecognition_QueryProfile(IntPtr voice, int pidx, [Out] ProfileInfo pinfo);

  private pxcmStatus QueryProfileINT(int pidx, out ProfileInfo pinfo)
  {
    pinfo = new ProfileInfo();
    return PXCMSpeechRecognition_QueryProfile(instance, pidx, pinfo);
  }

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMSpeechRecognition_SetProfile(IntPtr voice, ProfileInfo pinfo);

  [DllImport("libpxccpp2c", CharSet = CharSet.Unicode)]
  internal static extern pxcmStatus PXCMSpeechRecognition_BuildGrammarFromStringList(IntPtr voice, int gid, string[] cmds, int[] labels, int ncmds);

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMSpeechRecognition_ReleaseGrammar(IntPtr voice, int gid);

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMSpeechRecognition_SetGrammar(IntPtr voice, int gid);

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMSpeechRecognition_DeleteGrammar(IntPtr voice, int gid);

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMSpeechRecognition_StartRec(IntPtr voice, IntPtr source, IntPtr handler);

  [DllImport("libpxccpp2c", CharSet = CharSet.Unicode)]
  internal static extern pxcmStatus PXCMSpeechRecognition_BuildGrammarFromFile(IntPtr voice, int gid, GrammarFileType fileType, string grammarFilename);

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMSpeechRecognition_BuildGrammarFromMemory(IntPtr voice, int gid, GrammarFileType fileType, byte[] grammaryMemory, int memSize);

  [DllImport("libpxccpp2c", CharSet = CharSet.Unicode)]
  private static extern void PXCMSpeechRecognition_GetGrammarCompileErrors(IntPtr voice, int gid, StringBuilder error, int nchars);

  internal static string GetGrammarCompileErrorsINT(IntPtr voice, int gid)
  {
    StringBuilder error = new StringBuilder(1024);
    PXCMSpeechRecognition_GetGrammarCompileErrors(voice, gid, error, error.Capacity);
    return error.ToString();
  }

  [DllImport("libpxccpp2c", CharSet = CharSet.Unicode)]
  internal static extern pxcmStatus PXCMSpeechRecognition_AddVocabToDictation(IntPtr voice, VocabFileType fileType, string vocabFileName);

  public pxcmStatus StartRecINT(IntPtr source, Handler handler)
  {
    if (handlerDIR != null)
      handlerDIR.Dispose();
    handlerDIR = new HandlerDIR(handler);
    return PXCMSpeechRecognition_StartRec(instance, source, handlerDIR.dirUnmanaged);
  }

  [DllImport("libpxccpp2c")]
  internal static extern void PXCMSpeechRecognition_StopRec(IntPtr voice);

  private void StopRecINT()
  {
    PXCMSpeechRecognition_StopRec(instance);
    if (handlerDIR == null)
      return;
    handlerDIR.Dispose();
  }

  public pxcmStatus QueryProfile(int idx, out ProfileInfo pinfo)
  {
    return QueryProfileINT(idx, out pinfo);
  }

  public pxcmStatus QueryProfile(out ProfileInfo pinfo)
  {
    return QueryProfile(-1, out pinfo);
  }

  public pxcmStatus SetProfile(ProfileInfo pinfo)
  {
    return PXCMSpeechRecognition_SetProfile(instance, pinfo);
  }

  public pxcmStatus BuildGrammarFromStringList(int gid, string[] cmds, int[] labels)
  {
    return PXCMSpeechRecognition_BuildGrammarFromStringList(instance, gid, cmds, labels, cmds.Length);
  }

  public pxcmStatus ReleaseGrammar(int gid)
  {
    return PXCMSpeechRecognition_ReleaseGrammar(instance, gid);
  }

  public pxcmStatus SetGrammar(int gid)
  {
    return PXCMSpeechRecognition_SetGrammar(instance, gid);
  }

  public pxcmStatus SetDictation()
  {
    return SetGrammar(0);
  }

  public pxcmStatus StartRec(PXCMAudioSource source, Handler handler)
  {
    if (handler == null)
      return pxcmStatus.PXCM_STATUS_HANDLE_INVALID;
    return StartRecINT(source != null ? source.instance : IntPtr.Zero, handler);
  }

  public void StopRec()
  {
    StopRecINT();
  }

  public pxcmStatus BuildGrammarFromFile(int gid, GrammarFileType fileType, string grammarFilename)
  {
    return PXCMSpeechRecognition_BuildGrammarFromFile(instance, gid, fileType, grammarFilename);
  }

  public pxcmStatus BuildGrammarFromMemory(int gid, GrammarFileType fileType, byte[] grammarMemory)
  {
    return PXCMSpeechRecognition_BuildGrammarFromMemory(instance, gid, fileType, grammarMemory, grammarMemory.Length);
  }

  public string GetGrammarCompileErrors(int gid)
  {
    return GetGrammarCompileErrorsINT(instance, gid);
  }

  public pxcmStatus AddVocabToDictation(VocabFileType fileType, string vocabFileName)
  {
    return PXCMSpeechRecognition_AddVocabToDictation(instance, fileType, vocabFileName);
  }

  internal class HandlerDIR : IDisposable
  {
    private Handler handler;
    private List<GCHandle> gchandles;
    internal IntPtr dirUnmanaged;

    public HandlerDIR(Handler handler)
    {
      this.handler = handler;
      gchandles = new List<GCHandle>();
      if (handler == null)
        return;
      HandlerSet hs = new HandlerSet();
      if (handler.onRecognition != null)
      {
        OnRecognitionDIRDelegate recognitionDirDelegate = OnRecognition;
        gchandles.Add(GCHandle.Alloc(recognitionDirDelegate));
        hs.onRecognition = Marshal.GetFunctionPointerForDelegate(recognitionDirDelegate);
      }
      if (handler.onAlert != null)
      {
        OnAlertDIRDelegate alertDirDelegate = OnAlert;
        gchandles.Add(GCHandle.Alloc(alertDirDelegate));
        hs.onAlert = Marshal.GetFunctionPointerForDelegate(alertDirDelegate);
      }
      dirUnmanaged = PXCMSpeechRecognition_AllocHandlerDIR(hs);
    }

    ~HandlerDIR()
    {
      Dispose();
    }

    [DllImport("libpxccpp2c")]
    internal static extern IntPtr PXCMSpeechRecognition_AllocHandlerDIR(HandlerSet hs);

    [DllImport("libpxccpp2c")]
    internal static extern void PXCMSpeechRecognition_FreeHandlerDIR(IntPtr hdir);

    private void OnRecognition(IntPtr data)
    {
      RecognitionData data1 = new RecognitionData();
      data1.timeStamp = Marshal.ReadInt64(data, 0);
      data1.grammar = Marshal.ReadInt32(data, 8);
      data1.duration = Marshal.ReadInt32(data, 12);
      long num = data.ToInt64() + 16L;
      for (int index = 0; index < 4; ++index)
      {
        Marshal.PtrToStructure(new IntPtr(num), data1.scores[index]);
        num += Marshal.SizeOf(typeof (NBest));
      }
      handler.onRecognition(data1);
    }

    private void OnAlert(AlertData data)
    {
      handler.onAlert(data);
    }

    public void Dispose()
    {
      if (dirUnmanaged == IntPtr.Zero)
        return;
      PXCMSpeechRecognition_FreeHandlerDIR(dirUnmanaged);
      dirUnmanaged = IntPtr.Zero;
      foreach (GCHandle gcHandle in gchandles)
      {
        if (gcHandle.IsAllocated)
          gcHandle.Free();
      }
      gchandles.Clear();
    }

    internal delegate void OnRecognitionDIRDelegate(IntPtr data);

    internal delegate void OnAlertDIRDelegate(AlertData data);

    [StructLayout(LayoutKind.Sequential)]
    internal class HandlerSet
    {
      internal IntPtr onRecognition;
      internal IntPtr onAlert;
    }
  }

  [Serializable]
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public class NBest
  {
    public int label;
    public int confidence;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
    public string sentence;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
    public string tags;

    public NBest()
    {
      sentence = "";
      tags = "";
    }
  }

  [Serializable]
  public class RecognitionData
  {
    public long timeStamp;
    public int grammar;
    public int duration;
    public NBest[] scores;

    public RecognitionData()
    {
      scores = new NBest[4];
      for (int index = 0; index < 4; ++index)
        scores[index] = new NBest();
    }

    public delegate void OnRecognizedDelegate(RecognitionData data);
  }

  [Flags]
  public enum AlertType
  {
    ALERT_VOLUME_HIGH = 1,
    ALERT_VOLUME_LOW = 2,
    ALERT_SNR_LOW = 4,
    ALERT_SPEECH_UNRECOGNIZABLE = 8,
    ALERT_SPEECH_BEGIN = 16,
    ALERT_SPEECH_END = 32,
    ALERT_RECOGNITION_ABORTED = 64,
    ALERT_RECOGNITION_END = 128
  }

  [Serializable]
  [StructLayout(LayoutKind.Sequential)]
  public class AlertData
  {
    public long timeStamp;
    public AlertType label;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    internal int[] reserved;

    public AlertData()
    {
      reserved = new int[6];
    }

    public delegate void OnAlertDelegate(AlertData data);
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

  [Serializable]
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public class ProfileInfo
  {
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
    public string speaker;
    public LanguageType language;
    public int endOfSentence;
    public int threshold;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
    internal int[] reserved;

    public ProfileInfo()
    {
      speaker = "";
      reserved = new int[13];
    }
  }

  public enum GrammarFileType
  {
    GFT_NONE = 0,
    GFT_LIST = 1,
    GFT_JSGF = 2,
    GFT_COMPILED_CONTEXT = 5
  }

  public enum VocabFileType
  {
    VFT_NONE,
    VFT_LIST
  }

  public class Handler
  {
    public OnRecognitionDelegate onRecognition;
    public OnAlertDelegate onAlert;

    public delegate void OnRecognitionDelegate(RecognitionData data);

    public delegate void OnAlertDelegate(AlertData data);
  }
}
