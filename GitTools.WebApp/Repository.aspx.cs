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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) BindData();
        }

        private void BindData()
        {
            var baseFolder = ConfigurationManager.AppSettings["GitBaseFolder"];

            var directoryInfo = new DirectoryInfo(baseFolder);

            this.GridView1.DataSource = directoryInfo.EnumerateDirectories();
            this.GridView1.DataBind();
        }

        protected string GetUrl(object dataItem)
        {
            var dirInfo = dataItem as DirectoryInfo;
            var host = Request.Url.ToString().Replace("Repository.aspx", "");
            
            return string.Format("{0}{1}.git", host, dirInfo.Name);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var folder = this.TextBox1.Text;
            if (!string.IsNullOrEmpty(folder))
            {
                if (!Directory.Exists(folder))
                {
                    var gitBaseDir = ConfigurationManager.AppSettings["GitBaseFolder"];
                    Git.Run("init --bare " + folder, gitBaseDir);
                    BindData();
                }
            }
        }
    }
}