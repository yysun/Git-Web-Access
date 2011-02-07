using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Services.Common;

namespace GitTools.WebApp.Services
{
    [DataServiceKey("Id")]
    public class GraphNode
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}