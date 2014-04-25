using System;
using System.Collections;
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
    public partial class Control_Lang_Policy_Content_Box : System.Web.UI.UserControl
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
            if (!string.IsNullOrEmpty((this.Page as Hotels2BasePage).qPolicyId))
            {
                ArrayList item = ProductPolicyAdmin.getPolicycontentBYIdAndLangId(int.Parse((this.Page as Hotels2BasePage).qPolicyId), (this.Page as Hotels2BasePage).CurrenStafftLangId);
                
                
                if (item != null)
                {
                    txtTitle.Text = item[0].ToString();
                    txtdetail.Text = item[1].ToString();
                    
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
               
                Response.Redirect(Request.UrlReferrer.ToString());
            }
        }
        protected void btSave_Click(object sender, EventArgs e)
        {
            //roductOptionContent cProductOptionContent = new ProductOptionContent();
            //cProductContent.GetProductContentById(int.Parse(this.strProductIdQueryString), (this.Page as Hotels2BasePage).CurrenStafftLangId);
            if (ProductPolicyAdmin.getPolicycontentBYIdAndLangId(int.Parse((this.Page as Hotels2BasePage).qPolicyId), (this.Page as Hotels2BasePage).CurrenStafftLangId) == null)
            {
                ProductPolicyAdmin.InsertNewContentLangPolicy(int.Parse((this.Page as Hotels2BasePage).qPolicyId), (this.Page as Hotels2BasePage).CurrenStafftLangId, txtTitle.Text, txtdetail.Text);
            }
            else
            {
                ProductPolicyAdmin.UpdateContentLangPolicy(int.Parse((this.Page as Hotels2BasePage).qPolicyId), (this.Page as Hotels2BasePage).CurrenStafftLangId, txtTitle.Text, txtdetail.Text);
            }
            BindContentLang();
            //Response.Redirect(Request.UrlReferrer.ToString());
        }

        

        

        

        

        
    }
}