Git Web Access
==============

Introduction
------------

For those who have interests in a Smart-HTTP Git server on IIS, the project provides an ASP.NET HttpHandler that let you run Smart HTTP Git on IIS. It is inspired by  [Grack](http://github.com/schacon/grack), a Ruby Rack based application for Smart HTTP Git and [git_http_backend.py](http://github.com/dvdotsenko/git_http_backend.py), a python implementation of Smart HTTP Git. 

The ASP.HET handler, GitHandler, is lightweight and can be used in any ASP.NET web applications and web sites. 


Features
--------
* Create remote repositories
* Clone, pull and push through IIS
* Leverage ASP.NET Membership for Authentication and Authorization
* Expose Git repository through [OData](http://www.odata.org) protocol
* Two Samples: Web Forms Application and ASP.NET WebPages (with Razor Syntax) Web Site

Installation
------------
* Install [Git for Windows 1.7.0](http://code.google.com/p/msysgit/downloads/list) or up on server
* Create a folder on server as root folder of all remote repositories
* Under IIS, create a web application and assign a new application pool that runs as Local System
* Browse the web application and to configure
* Browse the web application, sign in using admin account (password:12) and to configure

Folder Structure on Server
--------------------------
Repo root folder/[username]/					-> user folder
Repo root folder/[username]/[projectname].git/	-> user project repository

Screenshots
--------------------------

![Home](http://gitweb.codeplex.com/Project/Download/FileDownload.aspx?DownloadId=208217)

![Create Repo](http://gitweb.codeplex.com/Project/Download/FileDownload.aspx?DownloadId=160896)

![Git Push](http://gitweb.codeplex.com/Project/Download/FileDownload.aspx?DownloadId=160897)

![Web Site](http://gitweb.codeplex.com/Project/Download/FileDownload.aspx?DownloadId=208215)

![Repo View](http://gitweb.codeplex.com/Project/Download/FileDownload.aspx?DownloadId=208216)

Change Logs
-----------
#### V0.5.2
#### V0.4
#### V0.3
* Push authentication against ASP.NET membership. 
* New ASP.NET web pages sample web site (using Razor Syntax).
* HTML 5 view of repository using the odata services.

#### V0.2
* Enable trace (please turn it off in web.config for production use).

#### V0.1
* Git Smart HTTP spike. 
* WCF Data Services to expose Git repository information.
* Sample ASP.NET web forms application


Known Issues
NuGet package
http://nuget.codeplex.com/workitem/712