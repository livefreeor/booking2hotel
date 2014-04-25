using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Staffs;
using Hotels2thailand.Production;

namespace Hotels2thailand.UI.Controls
{
    public partial class Control_langswitch : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

           LangBind();
           
        }

        public void LangBind()
        {
            
            byte LangId = (this.Page as Hotels2BasePage).CurrenStafftLangId;
            lblCountry.Text = (this.Page as Hotels2BasePage).LangShowtitle(LangId);
            imgLang.ImageUrl = "~/images/flag" + (this.Page as Hotels2BasePage).LangShowtitle(LangId) + ".png";
        }
        

        protected void GvLangList_RowdataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "changein(this)");
                e.Row.Attributes.Add("onmouseout", "changeout(this)");
                byte bytLangId = (byte)DataBinder.Eval(e.Row.DataItem, "LanguageID");
                string strLangTitle = (this.Page as Hotels2BasePage).LangShowtitle(bytLangId);
                LinkButton linkLangTitle = e.Row.Cells[0].FindControl("linkBt") as LinkButton;
                linkLangTitle.Text = strLangTitle;

                Image imgFlag = e.Row.Cells[0].FindControl("imgflagS") as Image;
                imgFlag.ImageUrl = "~/images/flag" + strLangTitle + ".png";
       
            }
        }

        protected void GvLangList_Rowcommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "switch")
            {
                StaffSessionAuthorize cStaffSession = new StaffSessionAuthorize();
                bool updated = StaffSessionAuthorize.UpdateSessionLog(cStaffSession.CurrentCookieLog, byte.Parse(e.CommandArgument.ToString()));
                Response.Redirect(Request.UrlReferrer.ToString());
            }
        }

        
    }
}