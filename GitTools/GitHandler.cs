using System;
using System.Diagnostics;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Web;
using System.Configuration;
using System.Text.RegularExpressions;

namespace GitTools
{
    public class GitHandler : IHttpHandler
    {
        /// <summary>
        /// You will need to configure this handler in the web.config file of your 
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpHandler Members

        private string gitExePath, gitDir, gitBaseDir;

        private HttpContext context;

        public void ProcessRequest(HttpContext context)
        {
            this.context = context;

            gitExePath = ConfigurationManager.AppSettings["GitExePath"];
            gitBaseDir = ConfigurationManager.AppSettings["GitBaseFolder"];
            gitDir = GetGitDir(context.Request.RawUrl);

            if(string.IsNullOrEmpty(gitDir) || 
               !File.Exists(gitExePath) ||
               !Directory.Exists(Path.Combine(gitBaseDir, gitDir)))
            {
                context.Response.StatusCode = 400;
                context.Response.End();
                return;
            }

            gitDir = Path.Combine(gitBaseDir, gitDir);

            if (!HasAccess())
            {
                context.Response.StatusCode = 403;
                context.Response.End();
                return;
            }
            
            if (context.Request.RawUrl.IndexOf("/info/refs?service=git-receive-pack") >= 0)
            {
                GetInfoRefs("receive-pack");
            }
            else if (context.Request.RawUrl.IndexOf("/git-receive-pack") >= 0 && context.Request.RequestType == "POST")
            {
                try
                {
                    ServiceRpc("receive-pack");
                }
                finally
                {
                    RunGit(@"update-server-info");
                }
            }
            else if (context.Request.RawUrl.IndexOf("/info/refs?service=git-upload-pack") >= 0)
            {
                GetInfoRefs("upload-pack");
            }
            else if (context.Request.RawUrl.IndexOf("/git-upload-pack") >= 0 && context.Request.RequestType == "POST")
            {
                ServiceRpc("upload-pack");
            }
        }

        public string GetGitDir(string rawUrl)
        {
            var match = Regex.Match(rawUrl, "/(.[^\\.]+).git");
            return match.Success ? match.Groups[1].Value : null;
        }

        private bool HasAccess()
        {
            return true;
        }

        /// <summary>
        /// "transfer-encoding:chunked is not supported. 
        /// Workaround: Set 'git config --set --global http.postBuffer 10485760'
        /// </summary>
        /// <param name="serviceName"></param>
        private void ServiceRpc(string serviceName)
        {
            context.Response.ContentType = string.Format("application/x-git-{0}-result", serviceName);

            var fin = Path.GetTempFileName();
            var fout = Path.GetTempFileName();

            using (var file = File.Create(fin))
            {
                byte[] buffer = new byte[4096];
                int bytesRead;
                while ((bytesRead = context.Request.InputStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    file.Write(buffer, 0, bytesRead);
                }
            }

            RunGitCmd(string.Format("{0} --stateless-rpc \"{1}\" < \"{2}\" > \"{3}\"", serviceName, gitDir, fin, fout));
            context.Response.WriteFile(fout);
            context.Response.End();
            File.Delete(fin);
            File.Delete(fout);
        }

        private void GetInfoRefs(string serviceName)
        {
            var fout = Path.GetTempFileName();
            context.Response.ContentType = string.Format("application/x-git-{0}-advertisement", serviceName);
            context.Response.Write(GitString("# service=git-" + serviceName));
            context.Response.Write("0000");
            RunGitCmd(string.Format("{0} --stateless-rpc --advertise-refs \"{1}\" > \"{2}\"", serviceName, gitDir, fout));
            context.Response.WriteFile(fout);
            context.Response.End();
            File.Delete(fout);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private string GitString(string s)
        {
            var len = (s.Length + 4).ToString("x");
            while (len.Length < 4) len = "0" + len;
            return len + s;
        }

        private void WriteNoCache(HttpContext context)
        {
            context.Response.AddHeader("Expires", "Fri, 01 Jan 1980 00:00:00 GMT");
            context.Response.AddHeader("Pragma", "no-cache");
            context.Response.AddHeader("Cache-Control", "no-cache, max-age=0, must-revalidate");
        }

        #region invoke Git
        internal string RunGit(string args)
        {
            var pinfo = new ProcessStartInfo(gitExePath)
            {
                Arguments = args,
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                WorkingDirectory = gitDir,
            };

            using (var process = Process.Start(pinfo))
            {
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (!string.IsNullOrEmpty(error))
                    Error(error);

                return output;
            }
        }

        internal void RunGitCmd(string args)
        {
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

                if (!string.IsNullOrEmpty(error)) Error(error);
            }
        }

        private void Error(string error)
        {
            context.Response.StatusDescription = error;
            context.Response.StatusCode = 500;
            context.Response.End();
        }
        #endregion

        #endregion
    }
}
