using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class test_htmleditor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Write(DateTime.Now.ToString("yyyy-MM-d-hh-mm-ss"));
      
        
        //txtDescricao.Text = "asdasdasd";
    }

    protected void txtDescricao_HtmlEditorExtender_ImageUploadComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
    {
        //htmlEditor.AjaxFileUpload.SaveAs(@"~\\Container\\temp\\" + e.FileName);
        //e.PostedUrl = Page.ResolveUrl(@"~\\Container\\temp\\" + e.FileName);

        //get the file name of the posted image
        string imgName = e.FileName;
        // Generate file path
        string filePath = "~/Upload/"+DateTime.Now.ToString("yyyy-mm-d") + imgName;

        // Save uploaded file to the file system
        
        var ajaxFileUpload = (AjaxControlToolkit.AjaxFileUpload)sender;
        ajaxFileUpload.SaveAs(MapPath(filePath));

        // Update client with saved image path
        e.PostedUrl = Page.ResolveUrl(filePath);
    }

    protected void btnTest_Click(object sender, EventArgs e)
    {
        Response.Write(HttpUtility.HtmlDecode(txtDescricao.Text));
        Response.End();
    }
}