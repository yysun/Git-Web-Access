using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Services.Common;

namespace GitTools.WebApp.Services
{
    [DataServiceKey("Name")]
    public class Graph
    {
        private Repository repository;
        private IList<GraphNode> nodes;
        private IList<GraphLink> links;

        public string Name { get; set; }

        public IEnumerable<GraphNode> Nodes
        {
            get
            {
                if (nodes == null) GenerateGraph();
                return nodes;
            }
        }
        
        public IEnumerable<GraphLink> Links
        {
            get
            {
                if (links == null) GenerateGraph();
                return links;
            }
        }

        public Graph(Repository repository)
        {
            //GenerateGraph(repository);
            this.repository = repository;
            this.Name = repository.Name;
        }

        private void GenerateGraph()
        {
            if (repository == null) return;

            nodes = new List<GraphNode>();
            links = new List<GraphLink>();
            var lanes = new List<string>();

            int i = 0;

            var commits = repository.Commits.ToList();

            foreach (var commit in commits)
            {
                var id = commit.Id;
                var children = from c in commits
                               where c.ParentIds.Contains(id)
                               select c;


                var lane = -1;
                if (children.Count() > 1)
                {
                    lanes.Clear();
                }
                else 
                {
                    var child = children.Where(c=>c.ParentIds.IndexOf(id)==0)
                                        .Select(c=>c.Id).FirstOrDefault();

                    lane = lanes.IndexOf(child);
                }

                if (lane < 0)
                {
                    lanes.Add(id);
                    lane = lanes.Count - 1;
                }
                else
                {
                    lanes[lane] = id;
                }
                var node = new GraphNode { X = lane, Y = i++, Id = id, Message = commit.Message };
                nodes.Add(node);

                foreach (var ch in children)
                {
                    var cnode = (from n in nodes
                                 where n.Id == ch.Id
                                 select n).FirstOrDefault();
                    if (cnode != null)
                    {
                        links.Add(new GraphLink
                        {
                            X1 = cnode.X,
                            Y1 = cnode.Y,
                            X2 = node.X,
                            Y2 = node.Y,
                            Id = id
                        });
                    }
                }
            }
        }
    }
}