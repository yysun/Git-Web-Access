<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewRepository.aspx.cs" Inherits="GitTools.WebApp.ViewRepository" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="Scripts/jquery-1.5.min.js"></script>
    <script type="text/javascript" src="Scripts/jQuery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2 id="title"></h2>
<canvas id="canvas" width="120" height="100" style="margin-top:10px"></canvas>
<div>
<div style="float:left;">
    <canvas id="canvas2" width="120" height="1000"></canvas>
</div>
<ul id="nodeList" style="list-style-type: none; font:16px/24px arial;margin-top:-30px;"></ul>
</div>
<script id="nodeTemplate" type="text/x-jquery-tmpl"> 
<li class="commit">
<span class="span-2">${Id}</span>
<span class="span-12"><span class="branch">${Branches}</span><span class="tag">${Tags}</span> <a href="#${Id}">${Message}</a></span>
<span class="span-3">${CommitterName}</span>
<span class="span-4 last">${CommitDateRelative}</span>
</li>
</script>


<script type="text/javascript">
    var repo = '<%= Request["name"] %>';
    var ctx = $("#canvas")[0].getContext('2d');
    var ctx2 = $("#canvas2")[0].getContext('2d');
    var nodes;
    var links;

    $(function () {

        $("#title").text(repo);

        $.ajaxSetup({
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });

        $.ajax({
            url: "/odata/RepositoryGraph('" + repo + "')/Nodes?$top=50&$orderby=Y",
            success: function (data) {
                nodes = data.d;
                $.ajax({
                    url: "/odata/RepositoryGraph('" + repo + "')/Links",
                    success: function (data) {
                        links = data.d;
                        draw(nodes, links);
                    },
                    error: function (xhr) { alert(xhr.responseText); }
                });

            },
            error: function (xhr) { alert(xhr.responseText); }
        });
    });

    var h = 60;
    var w = 78;
    var r = 10;
    var xmax;

    var h2 = 24;
    var w2 = 16;
    var r2 = 5;

    function draw(nodes, links) {
        var ww = 0;
        var nl = nodes.length > 12 ? 12 : nodes.length;

        for (var i = 0; i < nl; i++) {
            if (nodes[i].X > ww) ww = nodes[i].X;
        }
        ctx.canvas.width = xmax = nl * w;
        ctx.canvas.height = (ww + 1.5) * h;

        for (var i = 0; i < nodes.length; i++) {
            if (nodes[i].X > ww) ww = nodes[i].X;
        }
        //vertical graph
        ctx2.canvas.width = (ww + 2) * w2;
        ctx2.canvas.height = nodes.length * h2;

        drawLinks(links);
        drawNodes(nodes);
        $("#nodeTemplate").tmpl(nodes).appendTo("#nodeList");
    }

    function drawNodes(nodes) {

        for (var i = 0; i < nodes.length; i++) {
            var node = nodes[i];
            node.Id = node.Id.substring(0, 5);
            node.Message = node.Message.substring(0, 60);
            var x = xmax - node.Y * w - w / 2;
            var y = node.X * h + h / 2;

            if (i <= 12) {
                ctx.fillStyle = "#c3fac0";
                ctx.strokeStyle = "#336a3a";
                ctx.lineWidth = 3;

                ctx.beginPath();
                ctx.rect(x - 30, y - 15, 60, 30);

                ctx.stroke();
                ctx.fill();
                ctx.closePath();

                ctx.fillStyle = "#000";
                ctx.font = "14px Calibri";

                ctx.fillText(node.Id, x - 22, y + 5);
                ctx.fillText(node.Branches, x - 30, y + 30);
            }

            //vertical graph
            x = node.X * w2 + w2 / 2;
            y = node.Y * h2 + h2 / 2;

            ctx2.fillStyle = "#ff8080";
            ctx2.beginPath();
            ctx2.arc(x, y, r2, 0, Math.PI * 2, true);
            ctx2.fill();
            ctx2.closePath();
        }
    }

    function drawLinks(links) {

        ctx.lineWidth = 1;
        ctx.strokeStyle = "#808080";

        ctx2.lineWidth = 1;
        ctx2.strokeStyle = "#808080";

        for (var i = 0; i < links.length; i++) {

            var link = links[i];
            var x1 = xmax - link.Y1 * w - w / 2;
            var y1 = link.X1 * h + h / 2;
            var x2 = xmax - link.Y2 * w - w / 2;
            var y2 = link.X2 * h + h / 2;
            var x3 = xmax - (link.Y2 - 1) * w - w / 2;

            if (link.X1 == link.X2) {
                ctx.moveTo(x1, y1);
                ctx.lineTo(x2, y2);
            }
            else {
                ctx.moveTo(x1, y1);
                ctx.lineTo(x3, y1);
                ctx.bezierCurveTo(x2, y1, x3, y2, x2, y2);
            }
            ctx.stroke();

            //vertical graph
            x1 = link.X1 * w2 + w2 / 2;
            y1 = link.Y1 * h2 + h2 / 2;
            x2 = link.X2 * w2 + w2 / 2;
            y2 = link.Y2 * h2 + h2 / 2;
            y3 = (link.Y2 - 1) * h2 + h2 / 2;

            if (link.X1 == link.X2) {
                ctx2.moveTo(x1, y1);
                ctx2.lineTo(x2, y2);
            }
            else {
                ctx2.moveTo(x1, y1);
                ctx2.lineTo(x1, y3);
                ctx2.bezierCurveTo(x1, y2, x2, y3, x2, y2);
            }
            ctx2.stroke();
        }
    }
    
</script>

</asp:Content>
