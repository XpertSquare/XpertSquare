<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<XpertSquare.Web.Models.QuestionViewModel>" %>
<%@ Import Namespace= "XpertSquare.Web.Helpers" %>
<%@ Import Namespace= "XpertSquare.Web.Core" %>
<%@ Import Namespace= "XpertSquare.Core" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Settings.SITE_TITLE %> - Questions > <%=Model.Question.Title%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="<%=AppContentHelper.CssUrl("prettify.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%=AppContentHelper.CssUrl("jquery.jgrowl.css") %>" rel="stylesheet" type="text/css" />
    <script src="<%=AppContentHelper.JavaScriptUrl(@"wmd/showdown.js")%>" type="text/javascript"></script>
	<script src="<%=AppContentHelper.JavaScriptUrl("jquery.textarearesizer.compressed.js")%>" type="text/javascript"></script>
	<script src="<%=AppContentHelper.JavaScriptUrl("jquery.jgrowl_minimized.js")%>" type="text/javascript"></script>
	<script src="<%=AppContentHelper.JavaScriptUrl("jquery.timers-1.2.js")%>" type="text/javascript"></script>
    <script src="<%=AppContentHelper.JavaScriptUrl(@"prettify/prettify.js")%>" type="text/javascript"></script>
    <script src="<%=AppContentHelper.JavaScriptUrl(@"prettify/lang-vb.js")%>" type="text/javascript"></script>
    
    <style type="text/css">

			div.jGrowl div.error {
				background-color: 		#DEDEDE;
				color: 					#FF0000;
			}
			
			div.jGrowl div.info {
				background-color: 		#669966;
				color: 					#FFFFFF;
			}
    </style>
    
	<script type="text/javascript">


	    $(document).ready(function() {
	        $('textarea.resizable:not(.processed)').TextAreaResizer();

	        function styleCode() {
	            if (typeof disableStyleCode != "undefined") {
	                return;
	            }

	            var a = false;

	            $("pre code").parent().each(function() {
	                if (!$(this).hasClass("prettyprint")) {
	                    $(this).addClass("prettyprint");
	                    a = true
	                }
	            });

	            if (a) { prettyPrint() }
	        }

	        styleCode();

	        function stylePreview() {
	            $("#wmd-preview pre").addClass("prettyprint");
	            $("#wmd-preview code").html(prettyPrintOne($("#wmd-preview code").html()));
	        }



	        $('#wmd-input').keydown(function() {
	            $(this).stopTime();
	            $(this).oneTime(1000, function() { stylePreview(); });
	        });



	        $('#wmd-input').keydown(function() {
	            $(this).stopTime();
	            $(this).oneTime(1000, function() { stylePreview(); });
	        });


	        $('div.vote img.vote-up').click
            (
                function() {
                    var uID = $('#Username').val();
                    var urlVote = $('#VoteUrl').val();
                    if (uID) {
                        var entityID = $(this).parents('div.vote').attr('id').split('_')[1];
                        var entityType = $(this).parents('div.vote').attr('id').split('_')[0];
                        var token = $('input[name=__RequestVerificationToken]').val();

                        if ($(this).hasClass('selected')) {
                            $.ajax
                            (
                                {
                                    type: "POST",
                                    url: urlVote,
                                    data: { EntityType: entityType, EntityID: entityID, VoteType: 'Up', VoteAction: 'recall-vote', Username: uID, '__RequestVerificationToken': token },
                                    dataType: "json",
                                    success: function(response) {
                                        if (!isNaN(response)) {
                                            $('#' + entityType + '_' + entityID)
                                            .find('span.score')
                                            .html(response);

                                            var srcVoteUpOff = $('#srcIconVoteUpOff').val();
                                            $('#' + entityType + '_' + entityID)
                                                .find('img.vote-up').removeAttr('src')
                                                .attr('src', srcVoteUpOff)
                                                .removeClass('selected');

                                            $.jGrowl("Your vote was registered!", {
                                                theme: 'info',
                                                speed: 'slow'
                                            });
                                        }
                                    },
                                    error: function(xhr, ajaxOptions, thrownError) {
                                        $.jGrowl("An error has occured while processing your vote.", {
                                            theme: 'error',
                                            speed: 'slow'
                                        });
                                    }
                                }
                            );
                        }
                        else {
                            $.ajax
                            (
                                {
                                    type: "POST",
                                    url: urlVote,
                                    data: { EntityType: entityType, EntityID: entityID, VoteType: 'Up', VoteAction: 'vote', Username: uID, '__RequestVerificationToken': token },
                                    dataType: "json",
                                    success: function(response) {
                                        if (!isNaN(response)) {
                                            $('#' + entityType + '_' + entityID)
                                            .find('span.score')
                                            .html(response);

                                            var srcVoteUpOn = $('#srcIconVoteUpOn').val();
                                            $('#' + entityType + '_' + entityID)
                                                .find('img.vote-up')
                                                .removeAttr('src')
                                                .attr('src', srcVoteUpOn)
                                                .addClass('selected');

                                            $.jGrowl("Your vote was registered!", {
                                                theme: 'info',
                                                speed: 'slow'
                                            });
                                        }
                                    },
                                    error: function(xhr, ajaxOptions, thrownError) {
                                        alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                                        alert("responseText: " + xhr.responseText);
                                        $.jGrowl("An error has occured while processing your vote.", {
                                            theme: 'error',
                                            speed: 'slow'
                                        });
                                    }
                                }
                            );
                        }
                    } else {
                        $.jGrowl("You need to be logged in to vote!", {
                            theme: 'error',
                            speed: 'slow'
                        });
                    }
                }
           );
	        $('div.vote img.vote-down').click
            (
                function() {
                    var uID = $('#Username').val();
                    var urlVote = $('#VoteUrl').val();
                    if (uID) {

                        var entityID = $(this).parents('div.vote').attr('id').split('_')[1];
                        var entityType = $(this).parents('div.vote').attr('id').split('_')[0];
                        var token = $('input[name=__RequestVerificationToken]').val();

                        if ($(this).hasClass('selected')) {
                            $.ajax
                            (
                                {
                                    type: "POST",
                                    url: urlVote,
                                    data: { EntityType: entityType, EntityID: entityID, VoteType: 'Down', VoteAction: 'recall-vote', Username: uID, '__RequestVerificationToken': token },
                                    dataType: "json",
                                    success: function(response) {
                                        if (!isNaN(response)) {
                                            $('#' + entityType + '_' + entityID)
                                            .find('span.score')
                                            .html(response);

                                            var srcVoteDownOff = $('#srcIconVoteDownOff').val();
                                            $('#' + entityType + '_' + entityID)
                                                .find('img.vote-down').removeAttr('src')
                                                .attr('src', srcVoteDownOff)
                                                .removeClass('selected');

                                            $.jGrowl("Your vote was registered!", {
                                                theme: 'info',
                                                speed: 'slow'
                                            });
                                        }
                                    },
                                    error: function(xhr, ajaxOptions, thrownError) {
                                        $.jGrowl("An error has occured while processing your vote.", {
                                            theme: 'error',
                                            speed: 'slow'
                                        });
                                    }
                                }
                            );
                        }
                        else {
                            $.ajax
                            (
                                {
                                    type: "POST",
                                    url: urlVote,
                                    data: { EntityType: entityType, EntityID: entityID, VoteType: 'Down', VoteAction: 'vote', Username: uID, '__RequestVerificationToken': token },
                                    dataType: "json",
                                    success: function(response) {
                                        if (!isNaN(response)) {
                                            $('#' + entityType + '_' + entityID)
                                            .find('span.score')
                                            .html(response);

                                            var srcVoteDownOn = $('#srcIconVoteDownOn').val();
                                            $('#' + entityType + '_' + entityID)
                                                .find('img.vote-down')
                                                .removeAttr('src')
                                                .attr('src', srcVoteDownOn)
                                                .addClass('selected');

                                            $.jGrowl("Your vote was registered!", {
                                                theme: 'info',
                                                speed: 'slow'
                                            });
                                        }
                                    },
                                    error: function(xhr, ajaxOptions, thrownError) {
                                        alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                                        alert("responseText: " + xhr.responseText);
                                        $.jGrowl("An error has occured while processing your vote.", {
                                            theme: 'error',
                                            speed: 'slow'
                                        });
                                    }
                                }
                            );
                        }
                    } else {
                        $.jGrowl("You need to be logged in to vote!", {
                            theme: 'error',
                            speed: 'slow'
                        });
                    }
                }
           );
	    });
	</script>

     <div id="question-header">        
        <h2><%=Html.ActionLink(Model.Question.Title,"Details","Questions",new {id=Model.Question.ID, seoName=Model.Question.SlugTitle},new {@class="question-hyperlink"}) %></h2>
    </div>     
    <%=Html.Hidden("VoteUrl",Url.Action("Vote","Questions")) %>
    <%=Html.Hidden("Username", User.Identity.Name)%>
    <%=Html.Hidden("srcIconVoteUpOn",AppContentHelper.ImageUrl("vote-arrow-up-on.png"))%>
    <%=Html.Hidden("srcIconVoteUpOff",AppContentHelper.ImageUrl("vote-arrow-up.png"))%>
    <%=Html.Hidden("srcIconVoteDownOn",AppContentHelper.ImageUrl("vote-arrow-down-on.png"))%>
    <%=Html.Hidden("srcIconVoteDownOff",AppContentHelper.ImageUrl("vote-arrow-down.png"))%>
     <div id="main-content">
        <div id="question">
        <table>
            <tbody>
                <tr>
                <td class="votecell">        
                     <div class="vote" id="<%="Question_" + Model.Question.ID  %>">                   
						<%if (VoteHelper.IsUserQuestionUpVote(User.Identity.Name, Model.Question))
							{ %>
							<img height="25" width="40" title="This question is useful (click again to undo)" alt="vote up" src="<%=AppContentHelper.ImageUrl("vote-arrow-up-on.png")%>" class="vote-up selected"/>
                            <%} else { %>
                              <img height="25" width="40" title="This question is useful (click again to undo)" alt="vote up" src="<%=AppContentHelper.ImageUrl("vote-arrow-up.png")%>" class="vote-up"/>
                         <% } %>					  
                        <span class="score"><%=Model.Question.TotalVoteValue%></span>
                        <%if (VoteHelper.IsUserAnswerDownVote(User.Identity.Name, Model.Question))
                        { %>
                        <img height="25" width="40" title="This question is not useful (click again to undo)" alt="vote down" src="<%=AppContentHelper.ImageUrl("vote-arrow-down-on.png")%>" class="vote-down selected"/>
                        <%} else { %>
                        <img height="25" width="40" title="This question is not useful (click again to undo)" alt="vote down" src="<%=AppContentHelper.ImageUrl("vote-arrow-down.png")%>" class="vote-down"/>
                        <% } %>
                    </div>
                </td>
                <td>
                    <div>          
                        <div class="post-text">
                            <%=Model.Question.ContentHtml %>
                        </div>
                        <div class="post-taglist">
                            <%foreach(XpertSquare.Core.Model.XsTag tag in Model.Question.Tags){ %>
                                <%=Html.ActionLink(
                                    tag.Name
                                    , "Tagged"
                                    , "Questions"
                                    , new { tag = tag.Name }
                                    , new { @class="post-tag", title="Show questions tagged '" + tag.Name +"'", rel="tag"}
                                  )%>                                
                            <%} %>
                        </div>
						<table class="fw">
						<tbody>
							<tr>
								<td class="vt">
								<% if(Model.Question.Author.Username.Equals(User.Identity.Name))
								{ %>
								<%=Html.ActionLink("Edit","Edit","Questions",new {id=Model.Question.ID, seoName=Model.Question.SlugTitle}, null) %>
								<% } %>
								</td>
								<td class="post-signature">
								</td>
								<td class="post-signature owner">
								<div class="started" style="float:right;">
									<div class="user-info">
										<div class="user-action-time">asked <span class="relativetime"><%=Html.ItemStartedLabel(Model.Question.CreationDT) %></span></div>
										<div class="user-gravatar32"><img height="32" width="32" alt="" src="<%=AppContentHelper.ImageUrl("AnonymousSmall.gif")%>"/></div>
										 <div class="user-details">
											<%=Html.ActionLink(UserHelper.GetDisplayNameForGUI(Model.Question.Author), "Details", "Users", new { id = Model.Question.Author.ID, seoName = Utils.GetSlug(UserHelper.GetDisplayNameForGUI(Model.Question.Author)) }, null)%>
											<br />
											<span class="reputation-score"><%=UserHelper.GetUserStatsForGUI(Model.Question.Author)%></span>
										</div>
									</div>
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
        </div> 
        
        <%if (Model.Question.Answers.Count > 0)
          { %>
            <div class="subheader">
                <h2> <%=Model.Question.Answers.Count %> 
                <% if (Model.Question.Answers.Count == 1)
                   { %> answer
                <% }
                   else
                   { %> answers
                <%} %>
                </h2>
            </div>
        <%} %>
        
        <%foreach (XpertSquare.Core.Model.XsAnswer answer in Model.Question.Answers){ %>
        <div class="answer">        
            <table>
            <tbody>
                <tr>
                    <td class="votecell">
                        <div class="vote" id="<%="Answer_" + answer.ID  %>">
                            <input type="hidden" value='<%=answer.ID%>' />
                            <%if (VoteHelper.IsUserAnswerUpVote(User.Identity.Name, answer))
                              { %>
                            <img height="25" width="40" title="This answer is useful (click again to undo)" alt="vote up" src="<%=AppContentHelper.ImageUrl("vote-arrow-up-on.png")%>" class="vote-up selected"/>
                            <%} else { %>
                              <img height="25" width="40" title="This answer is useful (click again to undo)" alt="vote up" src="<%=AppContentHelper.ImageUrl("vote-arrow-up.png")%>" class="vote-up"/>
                           <% } %>
                            <span class="score"><%=answer.TotalVoteValue%></span>
                            <%if (VoteHelper.IsUserAnswerDownVote(User.Identity.Name, answer))
                              { %>
                            <img height="25" width="40" title="This answer is not useful (click again to undo)" alt="vote down" src="<%=AppContentHelper.ImageUrl("vote-arrow-down-on.png")%>" class="vote-down selected"/>
                            <%} else { %>
                            <img height="25" width="40" title="This answer is not useful (click again to undo)" alt="vote down" src="<%=AppContentHelper.ImageUrl("vote-arrow-down.png")%>" class="vote-down"/>
                            <% } %>
                        </div>
                    </td>
                    <td>
                        <div class="post-text">
                             <%=answer.ContentHtml%>
                        </div>
                         <div class="started" style="float:right;">
                            <div class="user-info">
                                <div class="user-action-time">answered <span class="relativetime"><%=Html.ItemStartedLabel(answer.CreationDT) %></span></div>
                                <div class="user-gravatar32"><img height="32" width="32" alt="" src="<%=AppContentHelper.ImageUrl("AnonymousSmall.gif")%>"/></div>
                                <div class="user-details"><%=Html.ActionLink(UserHelper.GetDisplayNameForGUI(answer.Author), "Details", "Users", new { id = answer.Author.ID, seoName = "user" }, null)%></div>
                            </div>
                        </div>       
                    </td>
                </tr>        
            </tbody>
            </table>
        </div>        
        <%} %>
        
        <!-- Your answer -->
        <br />
        <div class="your-answer-title">
                <h2>Your answer</h2>
            </div>
            <% using (Html.BeginForm("Answer","Questions",new {id=Model.Question.ID})) {%>
            <%=Html.AntiForgeryToken() %>
            <%=Html.Hidden(Model.Question.ID.ToString()) %>        
            <div id="wmd-editor" class="wmd-panel">
		        <div id="wmd-button-bar"></div>
			        <%=Html.TextArea("AnswerContent", new {id="wmd-input", @class="resizable"}) %>			        
		        </div>
	        <div id="wmd-preview" class="wmd-panel"></div>                
        
            
            <div class="form-submit">
                <input type="submit" name="button" class="form-button" value="Post answer" />
            </div>
            <% } %>
    
     </div>

     <div id="sidebar">
     <%Html.RenderPartial("RelatedQuestions", Model.RelatedQuestions);%>
    </div>
    <br class="clrb" />
    <script src="<%=AppContentHelper.JavaScriptUrl(@"wmd/wmd.js")%>" type="text/javascript"></script>
</asp:Content>

