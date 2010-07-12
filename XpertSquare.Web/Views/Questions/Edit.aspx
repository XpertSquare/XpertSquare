<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<XpertSquare.Core.Model.XsQuestion>" %>
<%@ Import Namespace= "XpertSquare.Web.Core" %>
<%@ Import Namespace="XpertSquare.Web.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Settings.SITE_TITLE %> - Edit - <%=Model.Title%>
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
	    
		 inputPane = $('#wmd-input');
		 inputPane.focus();
		 
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
    <h1 class="header-color">Questions > Edit
</div>

<%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>

<div id="main-content">
    <%=Html.Hidden("AutoCompleteUrl",Url.Action("AutoComplete","Filter")) %>
    <% using (Html.BeginForm()) {%>
    <%=Html.AntiForgeryToken() %>
	<%=Html.Hidden("ID", Model.ID) %>
        <div class="form-item">
            <label for="Title">Title:</label>
            <%= Html.TextBox("Title", Model.Title, new {@class="title-input"})%>
            <%= Html.ValidationMessage("Title", "*") %>
       </div>
        <div id="wmd-editor" class="wmd-panel">
	        <div id="wmd-button-bar"></div>
		        <%=Html.TextArea("Content", Model.Content, new {id="wmd-input", @class="resizable"}) %>			        
	    </div>
        <div id="wmd-preview" class="wmd-panel"></div>                
         <%= Html.ValidationMessage("Content", "*") %>
        
        <div class="form-item">
            <label for="Tags">Tags:</label>
            <%= Html.TextBox("QuestionTags", Model.AllTags , new {@class="tags-input"}) %>
            <%= Html.ValidationMessage("Tags", "*") %>
        </div>
        <div class="form-submit">
            <input type="submit" name="button" class="form-button" value="Save" />
        </div>
<% } %>
</div>

<div id="sidebar">
    <div class="widget widget_text" id="random-quote">
        <h2>Sidebar widget</h2>
        <div class="textwidget">
            <p>Sidebar widget text</p>
        </div>
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

