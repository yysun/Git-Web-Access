using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;

namespace GitTools.WebApp
{
    public partial class Repository : System.Web.UI.Page
    {
        string baseFolder = ConfigurationManager.AppSettings["GitBaseFolder"];
        protected void Page_Load(object sender, EventArgs e)
        {
            var gitExePath = ConfigurationManager.AppSettings["GitExePath"];
            var gitRootFolder = ConfigurationManager.AppSettings["GitBaseFolder"];
            var msg = "";
            if (!File.Exists(gitExePath))
            {
                msg = gitExePath + " does not exist. ";
            }
            if (!Directory.Exists(gitRootFolder))
            {
                msg += gitRootFolder + " does not exist. ";
            }

            if (!string.IsNullOrWhiteSpace(msg))
            {
                Label1.Text = msg;
            }
            else
            {
                if (!IsPostBack) BindData();
            }
        }

        private void BindData()
        {
            
            var directoryInfo = new DirectoryInfo(baseFolder);
            var folders = directoryInfo.EnumerateDirectories(string.Format("*.{0}", Git.GIT_EXTENSION), SearchOption.AllDirectories)
                                       .Select(d => new {
                                           Name = d.Name.Replace("." + Git.GIT_EXTENSION, ""), 
                                           Id = d.FullName.Substring(baseFolder.Length + 1)
                                                          .Replace("\\", ".")
                                                          .Replace("." + Git.GIT_EXTENSION, "") 
                                       }).ToList();

            gwRepos.DataSource = folders;
            gwRepos.DataBind();
        }

        protected string GetUrl(dynamic dataItem)
        {
            var host = Request.Url.ToString().Replace("Repository.aspx", "");

            var directory = dataItem.Id as string;
            directory = directory.Replace("\\", "/");

            return string.Format("{0}{1}.git", host, directory);
        }

        protected void btnCreateFolder_Click(object sender, EventArgs e)
        {
            try
            {
                var folder = tbCreateFolderName.Text;

                if (string.IsNullOrEmpty(folder)) return;

                string ext = Path.GetExtension(folder);
                if (string.IsNullOrEmpty(ext) || ext != Git.GIT_EXTENSION)
                {
                    folder = Path.ChangeExtension(folder, Git.GIT_EXTENSION);
                }

                folder = folder.Replace(" ", "-");
                if (Directory.Exists(folder)) return;


                var gitBaseDir = ConfigurationManager.AppSettings["GitBaseFolder"];
                Git.Run("init --bare " + folder, gitBaseDir);
                BindData();
            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }
        }
    }
}