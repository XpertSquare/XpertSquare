<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="aboutTitle" ContentPlaceHolderID="TitleContent" runat="server">
    XpertSquare > About
</asp:Content>

<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">
   <div class="subheader">
    <h1 class="header-color">About</h1>
</div>
<br />
<div class="bubbleWrap">
    <table class="dataForm">
    <tbody>
        <tr>
            <td width="250">Coordinator and author</td>
            <td><strong>Marius Serban</strong></td>
        </tr>
    </tbody>
    </table>
</div>

<div class="bubbleWrap">
    <table class="dataForm">
    <tbody>
        <tr>
            <td width="250">Framework</td>
            <td><a href="http://en.wikipedia.org/wiki/ASP.NET">Microsoft ASP.NET</a> (version 3.5 SP1)</td>
        </tr>
        <tr>
            <td>Programming language</td>
            <td><a href="http://en.wikipedia.org/wiki/C_Sharp_(programming_language)">C#</a></td>
        </tr>
        <tr>
            <td>Development environment</td>
            <td><a href="http://msdn.microsoft.com/en-us/vsts2008/default.aspx">Visual Studio 2008 Team Suite</a></td>
        </tr>
        <tr>
            <td>Web framework</td>
            <td><a href="http://www.asp.net/mvc/">ASP.NET MVC</a></td>
        </tr>
        <tr>
            <td>Browser framework</td>
            <td><a href="http://jquery.com/">jQuery</a> and <a href="http://gitdub.com/cky/wmd">WMD editor</a></td>
        </tr>
        <tr>
            <td>Database</td>
            <td><a href="http://msdn.microsoft.com/en-us/sqlserver/default.aspx">SQL Server 2008</a> (any DB supported by NHibernate can be used)</td>
        </tr>
        <tr>
            <td>Data access layer</td>
            <td><a href="http://nhforge.org/Default.aspx">NHibernate</a> and <a href="http://fluentnhibernate.org/">Fluent NHibernate</a></td>
        </tr>
        <tr>
            <td>Error logging frameworks</td>
            <td><a href="http://logging.apache.org/log4net/index.html">log4net</a> and <a href="http://code.google.com/p/elmah/">elmah</a> </td>
        </tr>
        <tr>
            <td>Full-text search support</td>
            <td><a href="http://lucene.apache.org/lucene.net/">Lucene.NET</a></td>
        </tr>
        <tr>
            <td>Source code control</td>
            <td><a href="http://git-scm.com/">Git</a> hosted at: <a href="http://github.com/XpertSquare/XpertSquare">GitHub</a></td>
        </tr>

         <tr>
            <td>Inspiration from</td>
            <td><a href="http://stackoverflow.com/">StackOverflow</a>, <a href="http://code.google.com/p/sharp-architecture/">Sharp architecture</a> and <a href="http://mvccontrib.codeplex.com/">MVC Contrib</a></td>
        </tr>
    </tbody>
    </table>
</div>
    
</asp:Content>
