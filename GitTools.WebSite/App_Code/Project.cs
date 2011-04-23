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

        private static string GetSafeId(string id)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
        {
            id = id.Replace(c, '-');
        }
        foreach (char c in @" ~`!@#$%^&+=,;""<>".ToCharArray())
        {
            id = id.Replace(c, '-');
        }
        return id;
    }
    
    public static void Create(string user, string name)
    {
        if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("project name (for new project)");
        var id = GetSafeId(name);       
        var folder = string.IsNullOrEmpty(user) ? id : string.Format("{0}/{1}", user, id);
        folder = folder + "." + Git.GIT_EXTENSION;

        var gitBaseDir = ConfigurationManager.AppSettings["GitBaseFolder"];
        if (!Directory.Exists(gitBaseDir)) throw new Exception(gitBaseDir + " is not available.");
        
        Git.Run("init --bare " + folder, gitBaseDir);
    }

}