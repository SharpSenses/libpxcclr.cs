using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

public class PXCMLoggingService : PXCMBase
{
  public new const int CUID = 946920232;

  internal PXCMLoggingService(IntPtr instance, bool delete)
    : base(instance, delete)
  {
  }

  public void Trace(object message)
  {
    Log(Level.LEVEL_TRACE, message);
  }

  public void Debug(object message)
  {
    Log(Level.LEVEL_DEBUG, message);
  }

  public void Info(object message)
  {
    Log(Level.LEVEL_INFO, message);
  }

  public void Warn(object message)
  {
    Log(Level.LEVEL_WARN, message);
  }

  public void Error(object message)
  {
    Log(Level.LEVEL_ERROR, message);
  }

  public void Fatal(object message)
  {
    Log(Level.LEVEL_FATAL, message);
  }

  [DllImport("libpxccpp2c", CharSet = CharSet.Unicode)]
  internal static extern pxcmStatus PXCMLoggingService_SetLoggerName(IntPtr instance, StringBuilder name);

  [DllImport("libpxccpp2c", CharSet = CharSet.Unicode)]
  internal static extern pxcmStatus PXCMLoggingService_Configure(IntPtr instance, ConfigMode configMode, StringBuilder config, int fileWatchDelay);

  [DllImport("libpxccpp2c")]
  internal static extern bool PXCMLoggingService_IsConfigured(IntPtr instance);

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMLoggingService_SetLevel(IntPtr instance, Level level);

  [DllImport("libpxccpp2c")]
  internal static extern bool PXCMLoggingService_IsLevelEnabled(IntPtr instance, Level level);

  [DllImport("libpxccpp2c")]
  internal static extern Level PXCMLoggingService_GetLevel(IntPtr instance);

  [DllImport("libpxccpp2c", CharSet = CharSet.Ansi)]
  internal static extern void PXCMLoggingService_Log(IntPtr instance, Level level, StringBuilder message, StringBuilder fileName, int lineNumber, StringBuilder functionName);

  [DllImport("libpxccpp2c", CharSet = CharSet.Unicode)]
  internal static extern void PXCMLoggingService_LogW(IntPtr instance, Level level, StringBuilder message, StringBuilder fileName, int lineNumber, StringBuilder functionName);

  [DllImport("libpxccpp2c", CharSet = CharSet.Ansi)]
  internal static extern void PXCMLoggingService_TaskBegin(IntPtr instance, Level level, StringBuilder taskName);

  [DllImport("libpxccpp2c", CharSet = CharSet.Ansi)]
  internal static extern void PXCMLoggingService_TaskEnd(IntPtr instance, Level level, StringBuilder taskName);

  public pxcmStatus SetLoggerName(string name)
  {
    return PXCMLoggingService_SetLoggerName(instance, new StringBuilder(name));
  }

  public pxcmStatus Configure(ConfigMode configMode, string config, int fileWatchDelay)
  {
    StringBuilder config1 = new StringBuilder(config);
    return PXCMLoggingService_Configure(instance, configMode, config1, fileWatchDelay);
  }

  public bool IsConfigured()
  {
    return PXCMLoggingService_IsConfigured(instance);
  }

  public pxcmStatus SetLevel(Level level)
  {
    return PXCMLoggingService_SetLevel(instance, level);
  }

  public bool IsLevelEnabled(Level level)
  {
    return PXCMLoggingService_IsLevelEnabled(instance, level);
  }

  public Level GetLevel()
  {
    return PXCMLoggingService_GetLevel(instance);
  }

  public void Log(Level level, string message, string fileName, int lineNumber, string functionName)
  {
    StringBuilder message1 = new StringBuilder(message);
    StringBuilder fileName1 = new StringBuilder(fileName);
    StringBuilder functionName1 = new StringBuilder(functionName);
    PXCMLoggingService_Log(instance, level, message1, fileName1, lineNumber, functionName1);
  }

  public void TaskBegin(Level level, string taskName)
  {
    StringBuilder taskName1 = new StringBuilder(taskName);
    PXCMLoggingService_TaskBegin(instance, level, taskName1);
  }

  public void TaskEnd(Level level, string taskName)
  {
    StringBuilder taskName1 = new StringBuilder(taskName);
    PXCMLoggingService_TaskEnd(instance, level, taskName1);
  }

  protected void Log(Level level, object message)
  {
    if (!IsLevelEnabled(level))
      return;
    StackFrame frame = new StackTrace(true).GetFrame(2);
    string name = frame.GetMethod().Name;
    string fileName = frame.GetFileName();
    int fileLineNumber = frame.GetFileLineNumber();
    Log(level, message.ToString(), fileName, fileLineNumber, name);
  }

  public enum Level
  {
    LEVEL_TRACE = 5000,
    LEVEL_DEBUG = 10000,
    LEVEL_INFO = 20000,
    LEVEL_WARN = 30000,
    LEVEL_ERROR = 40000,
    LEVEL_FATAL = 50000
  }

  public enum ConfigMode
  {
    CONFIG_DEFAULT = 1,
    CONFIG_PROPERTY_FILE_LOG4J = 2,
    CONFIG_XML_FILE_LOG4J = 4
  }
}
