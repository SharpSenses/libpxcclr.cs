/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2013-2014 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

    public partial class PXCMLoggingService : PXCMBase
    {
        new public const Int32 CUID = 0x3870DB28;

        public void Trace(object message) { Log(Level.LEVEL_TRACE, message); }
        public void Debug(object message) { Log(Level.LEVEL_DEBUG, message); }
        public void Info(object message) { Log(Level.LEVEL_INFO, message); }
        public void Warn(object message) { Log(Level.LEVEL_WARN, message); }
        public void Error(object message) { Log(Level.LEVEL_ERROR, message); }
        public void Fatal(object message) { Log(Level.LEVEL_FATAL, message); }

        public enum Level
        {
            LEVEL_FATAL = 50000,
            LEVEL_ERROR = 40000,
            LEVEL_WARN = 30000,
            LEVEL_INFO = 20000,
            LEVEL_DEBUG = 10000,
            LEVEL_TRACE = 5000,
        };

        public enum ConfigMode
        {
            CONFIG_DEFAULT = 0x1,
            CONFIG_PROPERTY_FILE_LOG4J = 0x2,
            CONFIG_XML_FILE_LOG4J = 0x4,
        };

        [DllImport(PXCMBase.DLLNAME, CharSet = CharSet.Unicode)]
        internal static extern pxcmStatus PXCMLoggingService_SetLoggerName(IntPtr instance, StringBuilder name);

        [DllImport(PXCMBase.DLLNAME, CharSet = CharSet.Unicode)]
        internal static extern pxcmStatus PXCMLoggingService_Configure(IntPtr instance, ConfigMode configMode, StringBuilder config, int fileWatchDelay);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern bool PXCMLoggingService_IsConfigured(IntPtr instance);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMLoggingService_SetLevel(IntPtr instance, Level level);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern bool PXCMLoggingService_IsLevelEnabled(IntPtr instance, Level level);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern Level PXCMLoggingService_GetLevel(IntPtr instance);

        [DllImport(PXCMBase.DLLNAME, CharSet = CharSet.Ansi)]
        internal static extern void PXCMLoggingService_Log(IntPtr instance, Level level, StringBuilder message, StringBuilder fileName, int lineNumber, StringBuilder functionName);

        [DllImport(PXCMBase.DLLNAME, CharSet = CharSet.Unicode)]
        internal static extern void PXCMLoggingService_LogW(IntPtr instance, Level level, StringBuilder message, StringBuilder fileName, int lineNumber, StringBuilder functionName);

        [DllImport(PXCMBase.DLLNAME, CharSet = CharSet.Ansi)]
        internal static extern void PXCMLoggingService_TaskBegin(IntPtr instance, Level level, StringBuilder taskName);

        [DllImport(PXCMBase.DLLNAME, CharSet = CharSet.Ansi)]
        internal static extern void PXCMLoggingService_TaskEnd(IntPtr instance, Level level, StringBuilder taskName);

        public pxcmStatus SetLoggerName(String name)
        {
            StringBuilder _name = new StringBuilder(name);
            return PXCMLoggingService_SetLoggerName(instance, _name);
        }

        public pxcmStatus Configure(ConfigMode configMode, String config, int fileWatchDelay)
        {
            StringBuilder _config = new StringBuilder(config);
            return PXCMLoggingService_Configure(instance, configMode, _config, fileWatchDelay);
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

        public void Log(Level level, String message, String fileName, int lineNumber, String functionName)
        {
            StringBuilder _message = new StringBuilder(message);
            StringBuilder _fileName = new StringBuilder(fileName);
            StringBuilder _functionName = new StringBuilder(functionName);
            PXCMLoggingService_Log(instance, level, _message, _fileName, lineNumber, _functionName);
        }

        public void TaskBegin(Level level, String taskName)
        {
            StringBuilder _taskName = new StringBuilder(taskName);
            PXCMLoggingService_TaskBegin(instance, level, _taskName);
        }

        public void TaskEnd(Level level, String taskName)
        {
            StringBuilder _taskName = new StringBuilder(taskName);
            PXCMLoggingService_TaskEnd(instance, level, _taskName);
        }

        /* constructors and misc */
        internal PXCMLoggingService(IntPtr instance, Boolean delete)
            : base(instance, delete)
        {
        }

        // protected because supposes that caller is two levels up
        protected void Log(Level level, object message)
        {
            if (IsLevelEnabled(level))
            {
                StackFrame frame = new System.Diagnostics.StackTrace(true).GetFrame(2);
                string method = frame.GetMethod().Name;
                string file = frame.GetFileName();
                int line = frame.GetFileLineNumber();
                Log(level, message.ToString(), file, line, method);
            }
        }
    };

    public partial class PXCMSession : PXCMBase
    {
        public PXCMLoggingService CreateLogger(String loggerName)
        {
            PXCMLoggingService logger;
            pxcmStatus sts = CreateImpl<PXCMLoggingService>(out logger);
            if (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR)
            {
                logger.SetLoggerName(loggerName);
            }
            else
            {
                logger = new PXCMLoggingService(IntPtr.Zero, false);
            }
            return logger;
        }

        public PXCMLoggingService CreateLogger()
        {
            StackFrame frame = new StackFrame(1);
            string loggerName = frame.GetMethod().DeclaringType.ToString();
            return CreateLogger(loggerName);
        }
    };

#if RSSDK_IN_NAMESPACE
}
#endif
