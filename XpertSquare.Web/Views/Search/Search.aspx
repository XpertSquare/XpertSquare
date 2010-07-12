<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<XpertSquare.Web.Models.SearchResultViewModel>" %>
<%@ Import Namespace= "XpertSquare.Web.Core" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Settings.SITE_TITLE %> - Questions > Search results
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="subheader">
   <h1 class="header-color">Questions > Search results for: <%=Model.SearchQuery %></h1>
</div>
<%foreach (var question in Model.Questions)
{
%>
    <% Html.RenderPartial("QuestionExcerpt", question); %>		
<%
}
if (Model.Questions.TotalItems == 0)
{
%> 
<div class="no-result">There are no questions found for the search criteria.
<%
    if (!String.IsNullOrEmpty(Model.SearchSuggestion))
    {
    %>
    Did you mean <%=Html.ActionLink(Model.SearchSuggestion, "Search", "Search", new { q = Model.SearchSuggestion }, null)%> ?
    <%
    } %>
</div>
<%
}
%>
<br class="clrb" />
<%= Html.Pager(Model.Questions) %>    
<br class="clrb" />
</asp:Content>
