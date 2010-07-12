<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<XpertSquare.Web.Models.TagsIndexViewModel>" %>
<%@ Import Namespace= "XpertSquare.Web.Core" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
 <%=Settings.SITE_TITLE%> - Tags > All
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="subheader">
    <h1 class="header-color">Tags > All</h1>
</div>
<br class="clrb" />
<div id="tags-wall">
    <table>
    <tbody>
        <tr>
            <%
                int columnIndex = 0; 
                int rowIndex = 0;
                int tagIndex = 0;

                while ((columnIndex < Settings.TAGSS_PAGINATION_COLUMNS) && (tagIndex < Convert.ToInt32(Model.CurrentPageSize)))
                {
                    rowIndex = 0;
                    %>
                <td valign="top" width="150">
                <%
                    while ((rowIndex < Settings.TAGS_PAGINATION_SIZE / Settings.TAGSS_PAGINATION_COLUMNS) && (tagIndex < Convert.ToInt32(Model.CurrentPageSize)))
                    {
                        XpertSquare.Core.Model.XsTag tag = ((IEnumerable<XpertSquare.Core.Model.XsTag>)Model.Tags).ElementAtOrDefault(tagIndex);
                        %>                        
                            <%=Html.ActionLink(
                                tag.Name 
                                , "Tagged"
                                , "Questions"
                                , new { tag = tag.Name }
                                , new { @class="post-tag", title="Show questions tagged '" + tag.Name +"'", rel="tag"}
                            )%>
                        <span class="item-multiplier">x&nbsp;<%=tag.QuestionCount %></span>
                        <br />
                        <%
                        tagIndex++;
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

<%= Html.Pager(Model.Tags) %>    
<br class="clrb" />
</asp:Content>
