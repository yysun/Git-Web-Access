using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;
using System.ServiceModel;

namespace GitTools.WebApp.Services
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)] 
    public class GitDataService : DataService<GitDataSource>
    {
        public static void InitializeService(DataServiceConfiguration config)
        {
            config.SetEntitySetAccessRule("*", EntitySetRights.AllRead);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2;

            //config.SetServiceOperationAccessRule("GetRepositoryGraph", ServiceOperationRights.AllRead);
        }

        ////[WebGet(UriTemplate = "/{name}")]
        //[WebGet]
        //public Graph GetRepositoryGraph(string name)
        //{
        //    var repo = (from r in this.CurrentDataSource.Repositories
        //                where string.Compare(r.Name, name, true) == 0
        //                select r).FirstOrDefault();

        //    return new Graph(repo);
        //}
    }
}
