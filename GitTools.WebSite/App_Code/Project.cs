using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using GitTools;
using System.Configuration;

/// <summary>
/// Summary description for Project
/// </summary>
public class Project
{
    public Project()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static void Create(string user, string name, string desc)
    {
        if (string.IsNullOrEmpty(user)) throw new ArgumentNullException("user name (for new project)");
        if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("project name (for new project)");

        var folder = string.Format("{0}/{1}", user, name);

        string ext = Path.GetExtension(folder);
        if (string.IsNullOrEmpty(ext) || ext != Git.GIT_EXTENSION)
        {
            folder = Path.ChangeExtension(folder, Git.GIT_EXTENSION);
        }

        folder = folder.Replace(" ", "-");
        if (Directory.Exists(folder)) throw new Exception(name + " is not available.");

        var gitBaseDir = ConfigurationManager.AppSettings["GitBaseFolder"];
        Git.Run("init --bare " + folder, gitBaseDir);

    }

}