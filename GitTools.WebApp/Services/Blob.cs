using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Services.Common;
using System.Text;

namespace GitTools.WebApp.Services
{
    [DataServiceKey("Id")]
    public class Blob
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string RepoFolder { get; set; }        

        public string Content
        {
            get
            {
                var output = Git.Run("cat-file -p " + this.Id, this.RepoFolder);

                return output;
            }
        }
    }
}