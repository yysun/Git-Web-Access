Git Web Access
===========================

Introduction
------------

For those who have interests in a Smart-HTTP Git server on IIS, the project provides an ASP.NET HttpHandler that let you run Smart HTTP Git on IIS.

It is inspired by  [Grack](http://github.com/schacon/grack), a Ruby Rack based application for Smart HTTP Git and [git_http_backend.py](http://github.com/dvdotsenko/git_http_backend.py), a python implementation of Smart HTTP Git. 

Features
--------
* Clone, pull and push through IIS
* Create remote repositories
* Leverage ASP.NET Authentication and Authorization
* Support IIS 7 integrated pipeline mode

Installation
------------
* Install [msysGit 1.7.0](http://code.google.com/p/msysgit/downloads/list) or up on server
* Create a folder on server as root folder of all remote repositories
* Create web application and assign a new application pool that runs as Local System
* Browse the web application and to configure

![Home](http://gitweb.codeplex.com/Project/Download/FileDownload.aspx?DownloadId=128658)

![Repositories](http://gitweb.codeplex.com/Project/Download/FileDownload.aspx?DownloadId=128658)


