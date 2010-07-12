<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<XpertSquare.Core.Model.XsQuestion>" %>
<%@ Import Namespace= "XpertSquare.Web.Core" %>
<%@ Import Namespace="XpertSquare.Web.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Settings.SITE_TITLE %> - ask a question
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="<%=AppContentHelper.CssUrl("prettify.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%=AppContentHelper.CssUrl("autocomplete.css") %>" rel="stylesheet" type="text/css" />
    <script src="<%=AppContentHelper.JavaScriptUrl(@"wmd/showdown.js")%>" type="text/javascript"></script>
	<script src="<%=AppContentHelper.JavaScriptUrl("jquery.textarearesizer.compressed.js")%>" type="text/javascript"></script>
	<script src="<%=AppContentHelper.JavaScriptUrl("jquery.timers-1.2.js")%>" type="text/javascript"></script>
    <script src="<%=AppContentHelper.JavaScriptUrl(@"prettify/prettify.js")%>" type="text/javascript"></script>    
    <script src="<%=AppContentHelper.JavaScriptUrl(@"prettify/lang-vb.js")%>" type="text/javascript"></script>
	<script type="text/javascript">
	    $(document).ready(function() {
    	
    	    //textarea resizer
    	    $('textarea.resizable:not(.processed)').TextAreaResizer();
    	    
    	    //syntax highlight
    	    function stylePreview() {
    	        $("#wmd-preview pre").addClass("prettyprint");
    	        $("#wmd-preview code").html(prettyPrintOne($("#wmd-preview code").html()));
    	    }

       	    $('#wmd-input').keydown(function() {
    	        $(this).stopTime();
    	        $(this).oneTime(1000, function() { stylePreview(); });
    	    });
		});
	</script>

     <div class="subheader">
        <h1 class="header-color">Questions > Ask</h1>
    </div>

    <div id="main-content">
        <%=Html.Hidden("AutoCompleteUrl",Url.Action("AutoComplete","Filter")) %>
        <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>
        <% using (Html.BeginForm()) {%>
        <%=Html.AntiForgeryToken() %>
            <div class="form-item">
                <label for="Title">Title:</label>
                <%= Html.TextBox("Title", "", new {@class="title-input"})%>
                <%= Html.ValidationMessage("Title", "*") %>
           </div>
			<script type="text/javascript">
				$(function() {
				$('#Title').focus(function() { $('#how-to-tag').hide(); $('#how-to-format').hide(); $('#how-to-title').fadeIn('slow'); });
				$('#wmd-input').focus(function() { $('#how-to-tag').hide(); $('#how-to-format').fadeIn('slow'); $('#how-to-title').hide(); });
				$('#QuestionTags').focus(function() { $('#how-to-tag').fadeIn('slow'); $('#how-to-format').hide(); $('#how-to-title').hide(); });
			});
			</script> 
            <div id="wmd-editor" class="wmd-panel">
		        <div id="wmd-button-bar"></div>
			        <%=Html.TextArea("Content", new {id="wmd-input", @class="resizable"}) %>			        
		    </div>
	        <div id="wmd-preview" class="wmd-panel"></div>                
             <%= Html.ValidationMessage("Content", "*") %>
            
            <div class="form-item">
                <label for="Tags">Tags:</label>
                <%= Html.TextBox("QuestionTags","", new {@class="tags-input"}) %>
                <%= Html.ValidationMessage("Tags", "*") %>
            </div>
            <div class="form-submit">
                <button name="button" value="SaveDraft" class="form-button">Save as draft</button>
                <input type="submit" name="button" class="form-button" value="Publish" />
            </div>
    <% } %>
    </div>

    <div id="sidebar">
        <div id="how-to-title" class="module newuser">
            <h4>How to Ask</h4>
            <p><b>Is your question about programming?</b></p><p>We prefer questions that can be <i>answered</i>, not just discussed.</p><p>Provide details. Write clearly and simply.</p>
        </div>
		<div style="display: none;" id="how-to-format" class="module newuser">
			<h4>How to Format</h4>    
			<p><span class="dingus">></span> put returns between paragraphs</p>
			<p><span class="dingus">></span> indent code by 4 spaces</p>
			<p><span class="dingus">></span> for linebreak add 2 spaces at end</p>    
			<p><span class="dingus">></span> backtick escapes <code>`like _so_`</code></p>
			<p><span class="dingus">></span> blockquote using &gt; at start of line</p>
			<p>&lt;http://foo.com&gt;<br>[foo](http://foo.com)<br>&lt;a href="http://foo.com"&gt;foo&lt;/a&gt;</p>			
		</div>
		<div style="display: none;" id="how-to-tag" class="module newuser">
			<h4>How to Tag</h4>
			<p>A tag is a keyword or label that categorizes your question with other, similar questions.</p>
			<p><span class="dingus">></span> favor existing popular tags</p>
			<p><span class="dingus">></span> avoid creating new tags</p>
			<p><span class="dingus">></span> use common abbreviations</p>
			<p><span class="dingus">></span> don't include synonyms</p>
			<p><span class="dingus">></span> combine multiple words into single-words with dashes</p>
			<p><span class="dingus">></span> maximum of 5 tags</p>
			<p><span class="dingus">></span> maximum of 25 chars per tag</p>
			<p><span class="dingus">></span> tag characters: [a-z 0-9 + # - .]</p>
			<p><span class="dingus">></span> delimit tags by space</p>
			<p class="ar"><a href="/tags">popular tags »</a></p>
		</div>
    </div>
    <br class="clrb" />
	<script type="text/javascript">
		wmd_options = {
			output: "Markdown"
		};
	</script>	

<script src="<%=AppContentHelper.JavaScriptUrl(@"wmd/wmd.js")%>" type="text/javascript"></script>
<script src="<%=AppContentHelper.JavaScriptUrl(@"jquery.autocomplete-min.js")%>" type="text/javascript"></script>
<script type="text/javascript">
//<![CDATA[
var a1; 
jQuery(function() {
var onAutocompleteSelect = function(value, data) {
    //notthing to set here
}
var urlAutoComplete = $('#AutoCompleteUrl').val();
var options = {
      serviceUrl: urlAutoComplete,
      width: 300,      
      delimiter: /(,|;)\s*/,
      onSelect: onAutocompleteSelect,
      deferRequestBy: 100, //miliseconds      
      noCache: false //set to true, to disable caching
};
a1 = $('#QuestionTags').autocomplete(options);
});
//]]>
</script>
</asp:Content>

