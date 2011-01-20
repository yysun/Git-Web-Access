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
    }
}
