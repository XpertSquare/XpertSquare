<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<XpertSquare.Core.Model.XsUser>>" %>
<%@ Import Namespace= "XpertSquare.Web.Core" %>
<%@ Import Namespace= "XpertSquare.Web.Helpers" %>
<%@ Import Namespace= "XpertSquare.Core" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Settings.SITE_TITLE %> - Users
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="subheader">
    <h1 class="header-color">Users > All</h1>
</div>
<br class="clrb" />
<div id="users-wall">
    <table>
    <tbody>
        <tr valign="top">
            <%
                int columnIndex = 0; 
                int rowIndex = 0;
                int userIndex = 0;

                while ((columnIndex < Settings.USERS_PAGINATION_COLUMNS) && (userIndex < Convert.ToInt32(ViewData["CurrentPageSize"])))
                {
                    rowIndex = 0;
                    %>
                <td valign="top">
                <%
                    while ((rowIndex < Settings.USERS_PAGINATION_SIZE / Settings.USERS_PAGINATION_COLUMNS) && (userIndex < Convert.ToInt32(ViewData["CurrentPageSize"])))
                    {
                        XpertSquare.Core.Model.XsUser user = Model.ElementAtOrDefault(userIndex);
                        %>
                        <div class="user-info">
                            <div class="user-gravatar32">
                                    <img height="32" width="32" src="<%=AppContentHelper.ImageUrl("AnonymousSmall.gif")%>" alt='<%=UserHelper.GetDisplayNameForGUI(user)%>'/>
                            </div>
                            <div class="user-details">
                                <%=Html.ActionLink(UserHelper.GetDisplayNameForGUI(user), "Details", new { id = user.ID, seoName = Utils.GetSlug(UserHelper.GetDisplayNameForGUI(user)) })%>
                                <br />
                                <span class="reputation-score"><%=UserHelper.GetUserStatsForGUI(user)%></span>
                            </div>
                        </div>
                        <%
                        userIndex++;
                        rowIndex++;
                    }
                    %>
                    </td>
                    <%
                    columnIndex++;
                }
                %>
        </tr>
   </tbody>
   </table>
</div>
<br class="clrb" />
    <%= Html.Pager("users") %>
</asp:Content>

