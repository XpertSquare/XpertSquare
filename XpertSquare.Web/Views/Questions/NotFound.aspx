<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<XpertSquare.Core.Model.XsQuestion>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	NotFound
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   <h2>The question with the ID = "<%=ViewData["EntityID"] %>" was not found. To be continued...</h2>    

</asp:Content>
