using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Staffs;
using Hotels2thailand.Production;
using Hotels2thailand.ProductOption;

namespace Hotels2thailand.UI.Controls
{
    public partial class Control_Lang_Option_Content_Box : System.Web.UI.UserControl
    {
        //proudct_id
        public string strProductIdQueryString
        {
            get
            {
                return Request.QueryString["pid"];
            }
        }

        //cat_id
        public string strPdcidString
        {
            get
            {
                return Request.QueryString["pdcid"];
            }
        }

        public string qOptionId
        {
            get { return Request.QueryString["oid"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                BindContentLang();
            }
            else
            {
                //DlLang.DataBind();
            }
            
            
        }
        protected void BindContentLang()
        {
            if (!string.IsNullOrEmpty(this.qOptionId))
            { 
                ProductOptionContent cProductOptionContent = new ProductOptionContent();
                cProductOptionContent.GetProductOptionContentById(int.Parse(this.qOptionId), (this.Page as Hotels2BasePage).CurrenStafftLangId);
                if (cProductOptionContent != null)
                {
                    txtTitle.Text = cProductOptionContent.Title;
                    txtdetail.Text = cProductOptionContent.Detail;
                    
                }

                DlLang.DataBind();
                
            }
        }
        protected void DlLang_OnitemdataBound(object sender,  DataListItemEventArgs e)
        {
            //if (e.Item.ItemType == ListItemType.Item)
            //{
                byte bytLangId = (byte)DataBinder.Eval(e.Item.DataItem,"LanguageID");
                string strLangtitle = (string)DataBinder.Eval(e.Item.DataItem, "Title");
                LinkButton linkbt = e.Item.FindControl("LinkBtLang") as LinkButton;
                if (bytLangId == 1)
                {
                    linkbt.Text = "<img src=\"../../images/flagENG.png\" />&nbsp;" + strLangtitle;
                }
                else if (bytLangId == 2)
                {
                    linkbt.Text = "<img src=\"../../images/flagTH.png\" />&nbsp;" + strLangtitle;
                }
                
                string flagname = (this.Page as Hotels2BasePage).LangShowtitle(bytLangId) + ".png";
                
                if ((this.Page as Hotels2BasePage).CurrenStafftLangId == bytLangId)
                {
                    e.Item.CssClass = "itemactiveLang";
                    
                }
                else
                {
                    e.Item.CssClass = "iteminactiveLang";
                }
               
            //}
        }

        protected void DlLang_OnitemCommand(object sender, DataListCommandEventArgs e)
        {
            if (e.CommandName == "selectLang")
            {
                StaffSessionAuthorize cStaffSession = new StaffSessionAuthorize();
                bool updated = StaffSessionAuthorize.UpdateSessionLog(cStaffSession.CurrentCookieLog, byte.Parse(e.CommandArgument.ToString()));

                BindContentLang();
                System.Web.UI.UserControl userMaster = (System.Web.UI.UserControl)Page.Master.FindControl("languageBox");
                userMaster.DataBind();
               
                Response.Redirect(Request.Url.ToString());
            }
        }
        protected void btSave_Click(object sender, EventArgs e)
        {
            ProductOptionContent cProductOptionContent = new ProductOptionContent();
            //cProductContent.GetProductContentById(int.Parse(this.strProductIdQueryString), (this.Page as Hotels2BasePage).CurrenStafftLangId);
            if (cProductOptionContent.GetProductOptionContentById(int.Parse(this.qOptionId),
                (this.Page as Hotels2BasePage).CurrenStafftLangId) == null)
            {

                ProductOptionContent cInserting = new ProductOptionContent
                {
                    OptionID = int.Parse(this.qOptionId),
                    LanguageID = (this.Page as Hotels2BasePage).CurrenStafftLangId,
                    Title = txtTitle.Text,
                    Detail = txtdetail.Text,
                };
                cInserting.Insert(cInserting);
            }
            else
            {

                ProductOptionContent cupdating = new ProductOptionContent();
                cupdating.UpdateOptionContentLang(int.Parse(this.qOptionId), (this.Page as Hotels2BasePage).CurrenStafftLangId, txtTitle.Text, txtdetail.Text);
                
            }
            //BindContentLang();
            Response.Redirect(Request.Url.ToString());
        }

        

        

        

        

        
    }
}