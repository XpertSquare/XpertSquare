<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<XpertSquare.Core.Model.WikiArticle>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create</h2>

    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm()) {%>

        <fieldset>
            <legend>Fields</legend>           
            <p>
                <label for="Title">Title:</label>
                <%= Html.TextBox("Title") %>
                <%= Html.ValidationMessage("Title", "*") %>
            </p>
            <p>
                <label for="Content">Content:</label>
                <%= Html.TextArea("Content") %>
                <%= Html.ValidationMessage("Content", "*") %>
            </p>
            <p>
                <label for="Tags">Tags:</label>
                <%= Html.TextBox("ArticleTags") %>
                <%= Html.ValidationMessage("Tags", "*") %>
            </p>            
            <p>
                <button name="button" value="SaveDraft">Save as draft</button>
                <input type="submit" name="button" value="Publish" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

