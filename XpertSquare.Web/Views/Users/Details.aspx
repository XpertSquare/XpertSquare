<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<XpertSquare.Core.Model.XsUser>" %>
<%@ Import Namespace= "XpertSquare.Core" %>
<%@ Import Namespace= "XpertSquare.Web.Core" %>
<%@ Import Namespace= "XpertSquare.Web.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Settings.SITE_TITLE %> - Users > <%=Model.DisplayName%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="subheader">
    <h1 class="header-color">Users > <%=Model.DisplayName %></h1>
</div>
<br />
<div id="user-details">
    <table border="0">
        <tbody>
            <tr>
                <td style="vertical-align: top; width: 120px; margin:auto;">
                    <img src="<%=AppContentHelper.ImageUrl("Anonymous.gif") %>" alt='<%=Model.DisplayName %>' />
                    <br />
                    <%if ((bool)ViewData["IsAllowedToEdit"])
                      { %>
                    <%=Html.ActionLink("Edit", "Edit", new { id = Model.ID, seoName = Utils.GetSlug(Model.DisplayName) })%>
                    <%} %>
                </td>
                <td width="400" style="padding-left:10px;">
                    <div class="bubbleWrap">
                        <table class="dataForm">
                        <tbody>
                            <tr>
                                <td width="120">Username</td>
                                <td><%=Model.Username%></td>
                            </tr>
                        </tbody>
                        </table>
                    </div>    
                    <div class="bubbleWrap">
                        <table class="dataForm">
                        <tbody>
                            <tr>
                                <td width="120">Display name</td>
                                <td><%=Model.DisplayName%></td>
                            </tr>
                            <tr>
                                <td>First name</td>
                                <td><%=Model.FirstName%></td>
                            </tr>
                            <tr>
                                <td>Last name</td>
                                <td><%=Model.LastName%></td>
                            </tr>
                            <tr>
                                <td>Email</td>
                                <td><%=Model.Email%></td>
                            </tr>
                            <tr>
                                <td>Location</td>
                                <td><%=Model.Location%></td>
                            </tr>
                        </tbody>
                        </table>
                    </div>    
                </td>
                <td style="vertical-align: top; width: 450px; padding-left:10px;">
                    <div class="bubbleWrap">
                        <table class="dataForm" style="height:300px;">
                        <tbody>
                            <tr>                                
                                <td><%=Model.Description%></td>
                            </tr>
                        </tbody>
                        </table>
                    </div>                             
                </td>
            </tr>
        </tbody>
    </table>
</div>   

<%if (null != ViewData["UserQuestion"])
{
%>
<div class="subheader">
    <h2> Questions</h2>
</div>
<div class="user-questions">
    <%
    foreach (XpertSquare.Core.Model.XsQuestion question in (IEnumerable)ViewData["UserQuestion"])
    {
    %>
        <div class="user-question">
            <%=Html.ActionLink(question.Title,"Details","Questions", new {id=question.ID, seoName=question.SlugTitle}, null) %>
        </div>
    <%
    }
    %>
</div>

<%
} %>

</asp:Content>

