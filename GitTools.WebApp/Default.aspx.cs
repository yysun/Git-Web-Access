using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Configuration;

namespace GitTools.WebApp
{
    public partial class _Default : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.TextBox1.Text = ConfigurationManager.AppSettings["GitExePath"];
                this.TextBox2.Text = ConfigurationManager.AppSettings["GitBaseFolder"];
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var config = WebConfigurationManager.OpenWebConfiguration("~");
            var appsettings = (AppSettingsSection)config.GetSection("appSettings");
            appsettings.Settings["GitExePath"].Value = this.TextBox1.Text;
            appsettings.Settings["GitBaseFolder"].Value = this.TextBox2.Text;
            config.Save();
        }
    }
}
