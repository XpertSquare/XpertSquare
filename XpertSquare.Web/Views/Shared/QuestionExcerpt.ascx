<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<XpertSquare.Core.Model.XsQuestion>" %>
<%@ Import Namespace= "XpertSquare.Web.Helpers" %>
<%@ Import Namespace= "XpertSquare.Core" %>
<%@ Import Namespace= "XpertSquare.Core.Model" %>

<div class="wiki-item">
            <div class="statscontainer">
                
                <div class="stats">
                    <div class="vote">
                        <div class="votes">
                            <span class="vote-count-post"><strong><%=Model.TotalVoteValue%></strong></span>
                            <div>votes</div>
                        </div>
                    </div>
                <div class="status answered">
                    <strong><%=Model.Answers.Count%></strong>answers
                </div>
            </div>
            
            </div>
            <div class="summary">
                <h3>
                    <%= Html.ActionLink(
                        Model.Title
                        , "Details"
                        , "Questions"
                        , new { id = Model.ID, seoName = Model.SlugTitle}
                        , new { @class="question-hyperlink", title=Model.Title }                        
                       )%>
                </h3> 
                <div class="excerpt"> <%=Model.Excerpt%>
                    <div class="started">
                        <div class="user-info">
                            <div class="user-action-time">asked <span class="relativetime"><%=Html.ItemStartedLabel(Model.CreationDT) %></span></div>
                            <div class="user-gravatar32"><img height="32" width="32" alt="" src="<%=AppContentHelper.ImageUrl("AnonymousSmall.gif")%>"/></div>
                            <div class="user-details">
								<%=Html.ActionLink(UserHelper.GetDisplayNameForGUI(Model.Author), "Details", "Users", new { id = Model.Author.ID, seoName = Utils.GetSlug(UserHelper.GetDisplayNameForGUI(Model.Author)) }, null)%>
								<br />
                                <span class="reputation-score"><%=UserHelper.GetUserStatsForGUI(Model.Author)%></span>
							</div>
                        </div>
                    </div>
                    <div class="tags">
                                <%foreach(XpertSquare.Core.Model.XsTag tag in Model.Tags){ %>
                                    <%=Html.ActionLink(
                                        tag.Name
                                        , "Tagged"
                                        , "Questions"
                                        , new { tag = tag.Name }
                                        , new { @class="post-tag", title="Show questions tagged '" + tag.Name +"'", rel="tag"}
                                      )%>                                
                                <%} %>
                    </div>
                </div>
            </div>               
        </div>    