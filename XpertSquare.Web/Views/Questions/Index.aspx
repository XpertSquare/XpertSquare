<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<XpertSquare.Web.Models.QuestionsIndexViewModel>" %>
<%@ Import Namespace= "XpertSquare.Web.Helpers" %>
<%@ Import Namespace= "XpertSquare.Web.Core" %>
<%@ Import Namespace= "XpertSquare.Core" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Settings.SITE_TITLE %> - Questions > All
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="subheader">
    <h1 class="header-color">Questions > All</h1>
</div>
<div id="items">
<% foreach (var item in Model.Questions) { %>
    <% Html.RenderPartial("QuestionExcerpt", item); %>		
<% } %>
</div>
<div id="sidebar">
    <div class="widget widget_text">
        <h2>Popular tags </h2>
        <div class="textwidget">
            <div id="tags-wall">
            <table>
            <tbody>
                
            <%foreach(var tag in Model.PopularTags){ %>
                <tr>
                
                    <td> <a href="<%=WebUtils.UrlEncodeUnsafeChars(Url.Action("Tagged","Questions",new {tag = tag.Name}))%>" class="post-tag" title="<%=String.Concat("Show questions tagged '", tag.Name ,"'")%>"><%=tag.Name%></a>
                    <span class="item-multiplier">x&nbsp;<%=tag.QuestionCount %></span> 
                    </td>
                </tr>    
            <%} %>
            </tbody>    
            </table>
            </div>
        </div>
    </div>
</div>
<br class="clrb" />
<%= Html.Pager(Model.Questions) %> 
</asp:Content>

