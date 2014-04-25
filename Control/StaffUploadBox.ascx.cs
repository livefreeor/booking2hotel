using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;


namespace Hotels2thailand.UI.Controls
{
    public partial class Control_StaffUploadBox : System.Web.UI.UserControl
    {
        public event EventHandler DataPictureUploaded; 

        private string _pathPicture = string.Empty;
        public string GetStringPathStaffFolderPic
        {
            get
            {
                string dirUrl = (this.Page as Hotels2thailand.UI.Hotels2BasePage).BaseUrl;

                DirectoryInfo folStaffPic = new DirectoryInfo(Server.MapPath( dirUrl + "images_staffs"));
                if (folStaffPic.Exists)
                {
                    _pathPicture = Server.MapPath(dirUrl + "images_staffs");
                    return _pathPicture;
                }
                else
                {
                    folStaffPic.Create();
                    _pathPicture = Server.MapPath(dirUrl + "images_staffs");
                    return _pathPicture;
                }
            }
        }

        public byte GetbyteQuerySring
        {
            get
            {
                if (string.IsNullOrEmpty(Request.QueryString["sid"]))
                {
                    return 0;
                }
                else
                {
                    return Convert.ToByte(Request.QueryString["sid"]);
                }
            }
        }

        public string GetStringfileNamedirectory
        {
            get
            {
                string strFilename = this.GetStringPathStaffFolderPic + "\\StaffPic" + Request.QueryString["sid"] + ".gif";
                return strFilename;
            }
        }

       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                lblAlertUpload.Visible = false;
            }
            
        }
        
        protected void btUploadd_Click(object sender, EventArgs e)
        {
            
            if (uploadPic.PostedFile != null && uploadPic.PostedFile.ContentLength > 0)
            {
                string strExtension = Path.GetExtension(uploadPic.FileName);
                if (strExtension.ToLower() == ".gif")
                {
                    try
                    {
                        System.Drawing.Image image = System.Drawing.Image.FromStream(uploadPic.PostedFile.InputStream);
                        if (image.Width == 145 && image.Height == 165)
                        {
                           uploadPic.PostedFile.SaveAs(this.GetStringfileNamedirectory);
                           lblAlertUpload.Text = "Upload Success";

                           if (DataPictureUploaded != null)
                           {
                               DataPictureUploaded(this, new EventArgs());
                           }
                           Response.Redirect("staffmanage.aspx?sid=" + GetbyteQuerySring.ToString());
                        }
                        else
                        {
                            lblAlertUpload.Visible = true;
                            lblAlertUpload.Text = "Error:Please make sure your image size is 145px * 165 px only.!";
                        }
                    }
                    catch (Exception ex)
                    {
                        lblAlertUpload.Visible = true;
                        lblAlertUpload.Text = ex.Message;
                    }
                }
                else
               { 
                    lblAlertUpload.Visible = true;
                    lblAlertUpload.Text = "Error: Please gif file Only!!!";
                }

               
            }
            else
            {
                lblAlertUpload.Visible = true;
                lblAlertUpload.Text = "Please Select Files";
            }
        }
    }
}
