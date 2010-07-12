<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<XpertSquare.Web.Models.TaggedViewModel>" %>
<%@ Import Namespace= "XpertSquare.Web.Helpers" %>
<%@ Import Namespace= "XpertSquare.Web.Core" %>
<%@ Import Namespace= "XpertSquare.Core" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Settings.SITE_TITLE %> - Questions > Tagged
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

 <div class="subheader">
        <h1 class="header-color">Questions > Tagged > <%=Model.TagName %></h1>
    </div>
    
    <div id="items">
    <%  if (null != Model.Questions)
        {
            foreach (var item in Model.Questions)
            { %>
			<% Html.RenderPartial("QuestionExcerpt", item); %>		
			<% }
		}
    else {%>
    There are no questions tagged "<%=Model.TagName%>"
    <%} %>
    </div>
    <div id="sidebar">
        <div class="widget widget_text">
            <h2>Sidebar content</h2>
            <div class="textwidget">
                <p>Sidebar content goes here</p>
            </div>
       </div>
    </div>
<br class="clrb" />       
<%= Html.Pager(Model.Questions) %>    
<br class="clrb" /> 

</asp:Content>

