<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<XpertSquare.Core.Model.WikiArticle>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>

    <table>
        <tr>
            <th></th>
            <th>
                ID
            </th>
            <th>
                Title
            </th>
            <th>
                Tags
            </th>

            <th>
                Content
            </th>
            <th>
                PublishedDT
            </th>
            <th>
                CreationDT
            </th>
            <th>
                UpdateDT
            </th>
            <th>
                Author
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", "Articles", new {  id=item.ID, seoName= item.Title }, null) %> |
                <%= Html.ActionLink("Details", "Details", "Articles", new { id = item.ID, seoName = item.Title }, null)%>
            </td>
            <td>
                <%= Html.Encode(item.ID) %>
            </td>
            <td>
                <%= Html.Encode(item.Title) %>
            </td>
            <td>
                <%= Html.Encode(item.AllTags) %>
            </td>
            <td>
                <%= Html.Encode(item.Content) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.PublishedDT)) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.CreationDT)) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.UpdateDT)) %>
            </td>
            <td>
                <%= Html.Encode(item.Author.Username) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Create New", "Create","articles") %>
    </p>

</asp:Content>

