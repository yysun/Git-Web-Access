Git Web Access
===========================

Introduction
------------
This is project is to develop a Git based project hosting site that is similar to Github but running on IIS/ASP.NET.

Features
--------
* Create remote Git repositories -
* Clone, push and pull through Git Smart-HTTP 
* View of Git repository (HTML 5)
* ASP.NET WebPages (with Razor Syntax)
* Expose Git repository through [OData](http://www.odata.org) protocol
* Sign on using OpenID
* Support IIS Express

SEO URLs (defined as rewrite rules in web.config)

* User Profile						http://host/[username]
* Project WWW Home Directory		http://host/[username]/[projectname]
* Project User Story Management		http://host/[username]/[projectname]/manage
* Project Issue Track				http://host/[username]/[projectname]/issues
* Git Repository Web View			http://host/[username]/[projectname]/repository
* Git Repository HTTP Access		http://host/[username]/[projectname].git

Installation
------------
* Install [Git for Windows 1.7.0](http://code.google.com/p/msysgit/downloads/list) or up on server
* Create a folder on server as root folder of all remote repositories
* Install ASP.NET WebPages (Razor) (comes with WebMatrix or MVC 3)
* Create web application and assign a new application pool that runs as Local System
* Browse the web application, sign in using admin account (password:12) and to configure

Storage Configuration
-------------
Repo root folder/[username]/					-> user folder
Repo root folder/[username]/[projectname]/		-> user project web home
Repo root folder/[username]/[projectname].git/	-> user project repository


Roadmap
-------
* User Registration
    * captcha
    * open id
* User Profile						http://host/username
* Create Project	
    * Git Repository HTTP			http://host/username/projectname.git
    * WWW Home Directory			http://host/username/projectname
* Agile User Story Management		http://host/username/projectname/manage
* Issue Track						http://host/username/projectname/issues
* Git Repository Web View			http://host/username/projectname/repository
