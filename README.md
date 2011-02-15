Git Web Access
==============

Introduction
------------
This project is to develop a Git based project hosting site running on IIS/ASP.NET.

Features
--------
* Create remote Git repositories
* Clone, push and pull through Git Smart-HTTP 
* View of Git repository (HTML 5)
* ASP.NET WebPages (with Razor Syntax)
* Expose Git repository through [OData](http://www.odata.org) protocol
* Support OAuth (e.g. link to facebook, twitter and google)
* Running on IIS Express or IIS
* Application Server runs timer jobs
* Extensible plug-ins architecture to support plug-ins like issue tracker and user story management

SEO URLs
--------

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
* Under IIS, create a web application and assign a new application pool that runs as Local System
* Browse the web application, sign in using admin account (password:12) and to configure

Storage Configuration
---------------------
Repo root folder/[username]/					-> user folder
Repo root folder/[username]/[projectname]/		-> user project web home
Repo root folder/[username]/[projectname].git/	-> user project repository

Change Logs
-----------
#### V0.3
* The Git Smart HTTP function is provided by an ASP.HET handler, GitHandler, which can be used in any web applications and web sites. 
* Clone and Pull do not require authentication. Push requires authentication against ASP.NET membership. 
* The web application project (web forms) in the solution is obsolete.  Use the web site (web pages with Razor syntax).

#### V0.2
* Enable trace (please turn it off in web.config for production use).

#### V0.1
* Git Smart HTTP spike. It is inspired by  [Grack](http://github.com/schacon/grack), a Ruby Rack based application for Smart HTTP Git and [git_http_backend.py](http://github.com/dvdotsenko/git_http_backend.py), a python implementation of Smart HTTP Git. 
* WCF Data Services to expose Git repository information using the OData protocol.