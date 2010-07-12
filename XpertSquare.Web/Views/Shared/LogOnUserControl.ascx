<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace= "XpertSquare.Web.Helpers" %>
<%@ Import Namespace= "XpertSquare.Core" %>
<%@ Import Namespace= "XpertSquare.Core.Model" %>
<%
    if (Request.IsAuthenticated)
    {
        XsUser user = UserHelper.GetUserByUsername(Page.User.Identity.Name);
        if (null != user)
        {
%>
        <b><%=Html.ActionLink(UserHelper.GetDisplayNameForGUI(user), "Details", "Users", new { id = user.ID, seoName = Utils.GetSlug(user.DisplayName) }, null)%></b>        
<%
    }
        else
        {
%> 
        Welcome <b>Guest</b>!
<%
        }
    }
    else
    {
%> 
        Welcome <b>Guest</b>!
<%
    }
%>
