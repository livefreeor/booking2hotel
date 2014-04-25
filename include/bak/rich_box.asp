
<%
Sub rich_box(strDetail)	

	'///////////////////////////////////////////////////////////////////////
	'//                                                                   //
	'//  Program Name    : EditWorks Professional                         //
	'//  Release Version : 2.0                                            //
	'//  Program Author  : SiteCubed Pty. Ltd.                            //
	'//  Supplied by     : CyKuH [WTN]                                    //
	'//  Packaged by     : WTN Team                                       //
	'//  Distribution    : via WebForum, ForumRU and associated file dumps//
	'//                                                                   //
	'//                      WTN Team `2000 - `2002                       //
	'///////////////////////////////////////////////////////////////////////
	'Create a new EditWorks class object
	dim myEW
	set myEW = new ew

	'These are the functions that you can call to hide varions buttons,
	'lists and tab buttons. By default, everything is enabled

	'myEW.HideBoldButton
	'myEW.HideUnderlineButton
	'myEW.HideItalicButton
	'myEW.HideNumberListButton
	'myEW.HideBulletListButton
	'myEW.HideDecreaseIndentButton
	'myEW.HideIncreaseIndentButton
	'myEW.HideLeftAlignButton
	'myEW.HideCenterAlignButton
	'myEW.HideRightAlignButton
	'myEW.HideJustifyButton
	'myEW.HideHorizontalRuleButton
	'myEW.HideLinkButton
	'myEW.HideAnchorButton
	'myEW.HideMailLinkButton
	'myEW.HideHelpButton
	'myEW.HideFontList
	'myEW.HideSizeList
	'myEW.HideFormatList
	'myEW.HideStyleList
	'myEW.HideForeColorButton
	'myEW.HideBackColorButton
	'myEW.HideTableButton
	'myEW.HideFormButton
	'myEW.HideImageButton
	'myEW.HideSymbolButton
	'myEW.HidePropertiesButton
	'myEW.HideCleanHTMLButton
	'myEW.HideGuidelinesButton
	'myEW.DisableSourceMode
	'myEW.DisablePreviewMode
	'myEW.DisableImageUploading
	'myEW.DisableImageDeleting

	'How do we want images to be inserted into our HTML content?
	'EW_PATH_TYPE_FULL will insert a link/image in this format: http://www.mysite.com/test.html
	'EWP_PATH_TYPE_ABSOLUTE will insert a link/image in this format: /myimage.gif
	myEW.SetPathType EW_PATH_TYPE_FULL
	
	'Are we editing a full HTML page, or just a snippet of HTML?
	'EW_DOC_TYPE_HTML_PAGE means we're editing a complete HTML page
	'EW_DOC_TYPE_SNIPPET means we're editing a snippet of HTML
	myEW.SetDocumentType EW_DOC_TYPE_HTML_PAGE
	
	'Do we want images to appear in the image manager as thumbnails or just in rows?
	'EW_IMAGE_TYPE_ROW means just list in a tabular format, without a thumbnail
	'EW_IMAGE_TYPE_THUMBNAIL means list in 4-per-line thumbnail mode
	myEW.SetImageDisplayType EW_IMAGE_TYPE_THUMBNAIL
	
	'Set the initial HTML value of our control
	myEW.SetValue(strDetail)

	'Display the EditWorks control. This *MUST* be called between <form> and </form> tags
	myEW.ShowControl "90%", "300", "/images"
	
	'Display the rest of the form
	End Sub
	%>