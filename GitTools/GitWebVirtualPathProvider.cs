using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Hosting;
using System.Web;
using System.IO;

namespace GitTools
{
    public class GitWebVirtualPathProvider : VirtualPathProvider
    {
        //we will register this provider in global.asax
        //public static void AppInitialize()
        //{
        //    HostingEnvironment.RegisterVirtualPathProvider(new GitWebVirtualPathProvider()); 
        //}

        
        public override bool FileExists(string virtualPath)
        {
            var path = HttpContext.Current.Server.MapPath(virtualPath);
            if (File.Exists(path))
            {
                return true;
            }
            else
            {
                //depends
                return false;
            }
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            return null;
        }
    }
}
