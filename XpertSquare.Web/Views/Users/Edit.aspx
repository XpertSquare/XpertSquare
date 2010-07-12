<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<XpertSquare.Core.Model.XsUser>" %>
<%@ Import Namespace="XpertSquare.Web.Core" %>
<%@ Import Namespace="XpertSquare.Web.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Settings.SITE_TITLE %> > Users > Edit > <%=Model.DisplayName %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="subheader">
    <h1 class="header-color">Users > Edit > <%=Model.DisplayName %></h1>
</div>
    <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm())
       {%>
       <%=Html.AntiForgeryToken() %>
    <table border="0">
        <tbody>
            <tr>               
                <td  style="vertical-align: top;">
                     <div class="user-edit">
                        <table class="dataForm">
                        <tbody>
                            <tr>
                                <td width="120"><img src="<%=AppContentHelper.ImageUrl("Anonymous.gif")%>" alt='<%=Model.DisplayName %>' /></td>
                                <td style="vertical-align:bottom;"><%=Model.Username%></td>
                            </tr>
                        </tbody>
                        </table>
                    </div>
                    
                    <div class="user-edit">
                        <table class="dataForm" width="800">
                        <tbody>
                            <tr>
                                <td width="120"><label for="DisplayName">Display name:</label></td>
                                <td>
                                    <%= Html.TextBox("DisplayName", Model.DisplayName, new {maxlength=30} )%>
                                    <%= Html.ValidationMessage("DisplayName", "*")%>
                                </td>
                            </tr>
                            <tr>
                                <td><label for="FirstName">First name:</label></td>
                                <td>
                                    <%= Html.TextBox("FirstName", Model.FirstName, new {maxlength = 20 })%>
                                    <%= Html.ValidationMessage("FirstName", "*")%>
                                </td>                            
                            </tr>
                            <tr>
                                <td><label for="LastName">Last name:</label></td>
                                <td>
                                    <%= Html.TextBox("LastName", Model.LastName, new {maxlength = 20 })%>
                                    <%= Html.ValidationMessage("LastName", "*")%>
                                </td>                            
                            </tr>
                            <tr>
                                <td><label for="Email">Email:</label></td>
                                <td>
                                    <%= Html.TextBox("Email", Model.Email, new {maxlength = 100 })%>
                                    <%= Html.ValidationMessage("Email", "*")%>
                                </td>                            
                            </tr>
                            <tr>
                                <td><label for="Location">Location:</label></td>
                                <td>
                                    <%= Html.TextBox("Location", Model.Location, new {maxlength = 50 })%>
                                    <%= Html.ValidationMessage("Location", "*")%>

                                </td>                            
                            </tr>
                            <tr>
                                <td><label for="Description">Description:</label></td>
                                <td>
                                    <%= Html.TextArea("Description", Model.Description, new {maxlength = 250 })%>
                                    <%= Html.ValidationMessage("Description", "*")%>

                                </td>                            
                            </tr>
                            <tr>                                
                                <td colspan="2">
                                    <div class="form-submit">
                                        <input type="submit" name="button" value="Cancel" class="form-button" style="width:100px" />&nbsp;&nbsp;&nbsp;
                                        <input type="submit" name="button" class="form-button" value="Save"  style="width:100px" />
                                    </div>
                                </td>                            
                            </tr>
                        </tbody>
                        </table>
                    </div>        
                </td>                
            </tr>
        </tbody>
    </table>

    <%} %>
        

</asp:Content>

