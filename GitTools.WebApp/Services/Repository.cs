using System;
using System.Collections.Generic;
using System.Data.Services.Common;
using System.IO;
using System.Linq;

namespace GitTools.WebApp.Services
{
    [DataServiceKey("Name")]
    public class Repository
    {
        public string Name { get; set; }
        public string RepoFolder { get; set; }

        public static Repository Open(string directory)
        {
            return new Repository
            {
                Name = Path.GetFileName(directory),
                RepoFolder = directory
            };
        }

        public static bool IsValid(string path)
        {
            if (path == null)
                return false;
            if (!Directory.Exists(path))
                return false;
            if (!File.Exists(Path.Combine(path, "HEAD")))
                return false;
            if (!File.Exists(Path.Combine(path, "config")))
                return false;
            if (!Directory.Exists(Path.Combine(path, "objects")))
                return false;
            if (!Directory.Exists(Path.Combine(path, "objects/info")))
                return false;
            if (!Directory.Exists(Path.Combine(path, "objects/pack")))
                return false;
            if (!Directory.Exists(Path.Combine(path, "refs")))
                return false;
            if (!Directory.Exists(Path.Combine(path, "refs/heads")))
                return false;
            if (!Directory.Exists(Path.Combine(path, "refs/tags")))
                return false;
            return true;
        }
        
        public IEnumerable<Branch> Branches
        {
            get
            {
                var branches = from b in Git.Run("branch", this.RepoFolder).Split('\n')
                               where !string.IsNullOrWhiteSpace(b)
                               select new Branch { Name = b.Substring(2) };
                return branches;
            }
        }

        public string CurrentBranch
        {
            get
            {
                var branches = from b in Git.Run("branch", this.RepoFolder).Split('\n')
                               where b.StartsWith("*")
                               select b.Substring(2);
                return branches.FirstOrDefault();
            }
        }

        public IEnumerable<Commit> Commits
        {
            get
            {
                var logs = Git.Run("log --all --pretty=format:%H`%P`%s`%cr`%cn`%ce`%ci`%T", this.RepoFolder).Split('\n');
                foreach (string log in logs)
                {
                    string[] ss = log.Split('`');

                    if (ss[0].Contains("'")) ss[0] = ss[0].Replace("'", "");

                    yield return new Commit
                    {
                        Id = ss[0],
                        ParentIds = ss[1],
                        Message = ss[2],
                        CommitDateRelative = ss[3],
                        CommitterName = ss[4],
                        CommitterEmail = ss[5],
                        CommitDate = DateTime.Parse(ss[6]),
                        Tree = new Tree 
                        { 
                            Id = ss[7], 
                            RepoFolder = this.RepoFolder,
                            Name = "",
                        },
                    };
                }                
            }
        }

        public IEnumerable<Tag> Tags
        {
            get
            {
                var tags = from t in Git.Run("tag", this.RepoFolder).Split('\n')
                               where !string.IsNullOrWhiteSpace(t)
                               select new Tag { Name = t };
                return tags;
            }
        }
    }
}