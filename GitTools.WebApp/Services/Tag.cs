using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Services.Common;

namespace GitTools.WebApp.Services
{
    [DataServiceKey("Name")]
    public class Tag
    {
        public string Name { get; set; }
    }
}