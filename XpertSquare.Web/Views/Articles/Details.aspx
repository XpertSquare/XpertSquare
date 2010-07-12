<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<XpertSquare.Core.Model.WikiArticle>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Details</h2>

    <fieldset>
        <legend>Fields</legend>
        <p>
            ID:
            <%= Html.Encode(Model.ID) %>
        </p>
        <p>
            Title:
            <%= Html.Encode(Model.Title) %>
        </p>
        <p>
            Content:
            <%= Html.Encode(Model.Content) %>
        </p>
        <p>
            PublishedDT:
            <%= Html.Encode(String.Format("{0:g}", Model.PublishedDT)) %>
        </p>
        <p>
            AllTags:
            <%= Html.Encode(Model.AllTags) %>
        </p>
        <p>
            CreationDT:
            <%= Html.Encode(String.Format("{0:g}", Model.CreationDT)) %>
        </p>
        <p>
            UpdateDT:
            <%= Html.Encode(String.Format("{0:g}", Model.UpdateDT)) %>
        </p>
        <p>
            LastUpdator:
            <%= Html.Encode(Model.LastUpdator) %>
        </p>
    </fieldset>
    <p>
        <%=Html.ActionLink("Edit", "Edit", new { /* id=Model.PrimaryKey */ }) %> |
        <%=Html.ActionLink("Back to List", "Index") %>
    </p>

</asp:Content>

