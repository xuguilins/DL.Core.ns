using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using DL.Core.ns.Extensiton;
using DL.Core.utility.Configer;
using DL.Core.utility.Extendsition;

namespace DL.Core.utility.Logging
{
    /// <summary>
    /// 系统内置日志
    /// </summary>
    public class InternalLogger : LogBase
    {
        private static readonly object locker = new object();
        private static readonly ConcurrentDictionary<string, string> pathdic = new ConcurrentDictionary<string, string>();

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <param name="message">内容</param>
        /// <param name="exception">异常消息</param>
        protected override void Write(LogLevel level, object message, Exception exception = null)
        {
            lock (locker)
            {
                DateTime dateTimeNow = DateTime.Now;
                string logDirPath = string.Empty;
                if (pathdic.ContainsKey("internal"))
                {
                    logDirPath = pathdic["internal"];
                }
                else
                {
                    var config = ConfigerManager.Instance.getCofiger();
                    if (config != null)
                    {
                        if (string.IsNullOrWhiteSpace(config?.CodeConfig?.LogPath))
                        {
                            logDirPath = Directory.GetCurrentDirectory();
                            if (logDirPath.Contains("bin"))
                            {
                                int endIndex = logDirPath.IndexOf("bin");
                                int startIndex = 0;
                                logDirPath = logDirPath.Substring(startIndex, endIndex);
                            }
                            if (!logDirPath.EndsWith('\\'))
                            {
                                logDirPath = logDirPath + "\\";
                            }
                        }
                        else
                        {
                            logDirPath = config.CodeConfig.LogPath;
                            if (!logDirPath.EndsWith('\\'))
                            {
                                logDirPath = logDirPath + "\\";
                            }
                        }
                    }
                    else
                    {
                        logDirPath = Directory.GetCurrentDirectory();
                    }
                    pathdic.TryAdd("internal", logDirPath);
                }
                switch (level)
                {
                    case LogLevel.Info:
                        logDirPath = $"{logDirPath}Info";
                        break;

                    case LogLevel.Warn:
                        logDirPath = $"{logDirPath}Warn";
                        break;

                    case LogLevel.Debug:
                        logDirPath = $"{logDirPath}Debug";
                        break;

                    case LogLevel.Success:
                        logDirPath = $"{logDirPath}Success";
                        break;

                    case LogLevel.Error:
                        logDirPath = $"{logDirPath}Error";
                        break;
                }

                if (!logDirPath.CheckDirctoryIsExite())
                {
                    FileExtensition.CreateDic(logDirPath);
                }
                string logFilePath = string.Format("{0}\\{1}.log", logDirPath, $"Log_{ dateTimeNow.ToString("yyyy-MM-dd")}");
                using (StreamWriter writer = new StreamWriter(logFilePath, true, Encoding.UTF8))
                {
                    try
                    {
                        StackTrace t = new StackTrace();
                        StackFrame f = t.GetFrame(t.FrameCount - 1);
                        object source = f.GetMethod();
                        writer.WriteLine($"[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "]  " + source);
                        writer.WriteLine(message + "\r\n" + exception?.Message);
                        writer.WriteLine("===============================================================");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    writer.Close();
                }
            }
        }
    }
}