<%@ Page Language="C#" AutoEventWireup="true" CodeFile="googleresult.aspx.cs" Inherits="test_googleresult" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
   <div id="cse-search-form" style="width: 100%;">Loading</div>
<script src="http://www.google.com/jsapi" type="text/javascript"></script>
<script type="text/javascript">
    google.load('search', '1', { language: 'en', style: google.loader.themes.V2_DEFAULT });
    google.setOnLoadCallback(function () {
        var customSearchOptions = {};
        var imageSearchOptions = {};
        imageSearchOptions['layout'] = google.search.ImageSearch.LAYOUT_POPUP;
        customSearchOptions['enableImageSearch'] = true;
        customSearchOptions['imageSearchOptions'] = imageSearchOptions; var customSearchControl = new google.search.CustomSearchControl(
      '012734418072090198957:rsak2m4d82e', customSearchOptions);
        customSearchControl.setResultSetSize(google.search.Search.FILTERED_CSE_RESULTSET);
        var options = new google.search.DrawOptions();
        options.setAutoComplete(true);
        options.enableSearchboxOnly("http://www.hotels2thailand.com/search_result.asp", "ht2");
        customSearchControl.draw('cse-search-form', options);
    }, true);
</script>

<style type="text/css">
  input.gsc-input, .gsc-input-box, .gsc-input-box-hover, .gsc-input-box-focus {
    border-color: #D9D9D9;
  }
  input.gsc-search-button, input.gsc-search-button:hover, input.gsc-search-button:focus {
    border-color: #2F5BB7;
    background-color: #357AE8;
    background-image: none;
    filter: none;
  }</style>
 
    </div>
    </form>
</body>
</html>
