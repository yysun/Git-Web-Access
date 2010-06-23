using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Services.Common;

namespace GitTools.WebApp.Services
{
    [DataServiceKey("Id")]
    public class Tree
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string RepoFolder { get; set; }

        public IEnumerable<Tree> Trees
        {
            get
            {
                return from c in Git.Run("ls-tree " + this.Id, this.RepoFolder).Split('\n')
                       where !string.IsNullOrWhiteSpace(c) && 
                             c.Substring(7, 4) == "tree"
                       select new Tree
                       {
                           Id = c.Substring(12, 40),
                           RepoFolder = this.RepoFolder,
                           Name = this.Name + c.Substring(52) + "\\",
                       };
            }
        }

        public IEnumerable<Blob> Blobs
        {
            get
            {
                return from c in Git.Run("ls-tree " + this.Id, this.RepoFolder).Split('\n')
                       where !string.IsNullOrWhiteSpace(c) && 
                             c.Substring(7, 4) == "blob"
                       select new Blob
                       {
                           Id = c.Substring(12, 40),
                           RepoFolder = this.RepoFolder,
                           Name = c.Substring(52),
                       };
            }
        }
    }
}