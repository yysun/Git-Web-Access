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
            if (!IsPostBack) BindData();
        }

        private void BindData()
        {
            
            var directoryInfo = new DirectoryInfo(baseFolder);

            gwRepos.DataSource = directoryInfo.EnumerateDirectories(string.Format("*.{0}", Git.GIT_EXTENSION), SearchOption.AllDirectories);
            gwRepos.DataBind();
        }

        protected string GetUrl(object dataItem)
        {
            var dirInfo = dataItem as DirectoryInfo;
            var host = Request.Url.ToString().Replace("Repository.aspx", "");

            var directory = dirInfo.FullName;
            directory = directory.Substring(baseFolder.Length + 1).Replace("\\", "/");

            return string.Format("{0}{1}", host, directory);
        }

        protected void btnCreateFolder_Click(object sender, EventArgs e)
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
    }
}