<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<XpertSquare.Core.Model.XsQuestion>>" %>
<%@ Import Namespace= "XpertSquare.Core" %>
<%@ Import Namespace= "XpertSquare.Core.Model" %>

<div class="widget widget_text" id="random-quote">
    <h2>Related questions</h2>
    <div class="textwidget">
        <%foreach (var question in Model)
        {%>
        <div class="spacer">
            <%=Html.ActionLink(question.Title, "Details", "Questions", new { id = question.ID, seoName = question.SlugTitle }, null)%>
        </div>                 
        <%
        }
                  if (Model.Count == 0)
                  {
                      %>There are no related questions
                      <%
                  }
        %>
    </div>
</div>