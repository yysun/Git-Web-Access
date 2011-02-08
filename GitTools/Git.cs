using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Configuration;
using System.IO;

namespace GitTools
{
    public abstract class Git
    {
        private const string TRACE_CATEGORY = "git";
        public const string GIT_EXTENSION = "git";

        public static string Run(string args, string workingDirectory)
        {
            var gitExePath = ConfigurationManager.AppSettings["GitExePath"];

            Trace.WriteLine(string.Format("{2}>{0} {1}", gitExePath, args, workingDirectory), TRACE_CATEGORY);

            var pinfo = new ProcessStartInfo(gitExePath)
            {
                Arguments = args,
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                WorkingDirectory = workingDirectory,
            };

            using (var process = Process.Start(pinfo))
            {
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                Trace.WriteLine(output, TRACE_CATEGORY);

                if (!string.IsNullOrEmpty(error))
                {
                    Trace.WriteLine("STDERR: " + error, TRACE_CATEGORY);
                    throw new Exception(error);
                }
                return output;
            }
        }


        public static void RunCmd(string args, string workingDirectory)
        {

            var gitExePath = ConfigurationManager.AppSettings["GitExePath"];

            Trace.WriteLine(string.Format("{2}>{0} {1}", gitExePath, args, workingDirectory), TRACE_CATEGORY);

            var pinfo = new ProcessStartInfo("cmd.exe")
            {
                Arguments = "/C \"\"" + gitExePath + "\"\" " + args,
                CreateNoWindow = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                WorkingDirectory = workingDirectory,
            };

            using (var process = Process.Start(pinfo))
            {
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (!string.IsNullOrEmpty(error))
                    throw new Exception(error);
            }
        }

        //public static void Run(string args, string workingDirectory, Action<string> action)
        //{
        //    var gitExePath = ConfigurationManager.AppSettings["GitExePath"];

        //    Trace.WriteLine(string.Format("{2}>{0} {1}", gitExePath, args, workingDirectory), TRACE_CATEGORY);

        //    var pinfo = new ProcessStartInfo(gitExePath)
        //    {
        //        Arguments = args,
        //        CreateNoWindow = true,
        //        RedirectStandardError = true,
        //        RedirectStandardOutput = true,
        //        UseShellExecute = false,
        //        WorkingDirectory = workingDirectory,
        //    };

        //    var process = new Process();
        //    process.StartInfo = pinfo;
        //    process.EnableRaisingEvents = true;

        //    process.OutputDataReceived += (_, e) => { action(e.Data); };
        //    //process.ErrorDataReceived += (_, e) => { throw new Exception(e.Data); };

        //    process.Start();

        //    process.BeginErrorReadLine();
        //    process.BeginOutputReadLine();

        //    process.WaitForExit();

        //}

        public static void RunGitCmd(string args)
        {
            var gitExePath = ConfigurationManager.AppSettings["GitExePath"];

            Trace.WriteLine(string.Format("{2}>{0} {1}", gitExePath, args, ""), TRACE_CATEGORY);

            var pinfo = new ProcessStartInfo("cmd.exe")
            {
                Arguments = "/C " + Path.GetFileName(gitExePath) + " " + args,
                CreateNoWindow = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                WorkingDirectory = Path.GetDirectoryName(gitExePath),
            };

            using (var process = Process.Start(pinfo))
            {
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (!string.IsNullOrEmpty(error)) throw new Exception(error);
            }
        }
    }
}
