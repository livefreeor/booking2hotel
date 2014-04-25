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
    public partial class Control_ProductTypeLang : System.Web.UI.UserControl
    {
        //proudct_id
        public string strProductTypeQuery
        {
            get
            {
                return Request.QueryString["ptid"];
            }
        }

        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                BindContentLang();
            }
            else
            {
                DlLang.DataBind();
            }
            
            
            
        }
        protected void BindContentLang()
        {
            if (!string.IsNullOrEmpty(this.strProductTypeQuery))
            {
                ProductTypeName cProductType = new ProductTypeName();
                cProductType.getTypeNameByLangIdAndTypeId(byte.Parse(this.strProductTypeQuery), (this.Page as Hotels2BasePage).CurrenStafftLangId);
                if (cProductType != null)
                {
                    txtTitle.Text = cProductType.ProductTypeNameTitle;
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
                //txtContantLang.Text = string.Empty;

                Response.Redirect(Request.UrlReferrer.ToString());
            }
        }
        protected void btSave_Click(object sender, EventArgs e)
        {
            ProductTypeName cProductType = new ProductTypeName();
            //cProductContent.GetProductContentById(int.Parse(this.strProductIdQueryString), (this.Page as Hotels2BasePage).CurrenStafftLangId);
            //int.Parse(this.strProductTypeQuery),(this.Page as Hotels2BasePage).CurrenStafftLangId) == null
            if (cProductType.getTypeNameByLangIdAndTypeId(byte.Parse(this.strProductTypeQuery),(this.Page as Hotels2BasePage).CurrenStafftLangId )== null)
            {

                ProductTypeName cInserting = new ProductTypeName
                {
                Type_Id = byte.Parse(this.strProductTypeQuery),
                 LangId = (this.Page as Hotels2BasePage).CurrenStafftLangId,
                 ProductTypeNameTitle = txtTitle.Text,
                
                };
                cInserting.insertNewLanguage(cInserting);
            }
            else
            {

                ProductTypeName cupdating = new ProductTypeName();
                cupdating.getTypeNameByLangIdAndTypeId(byte.Parse(this.strProductTypeQuery), (this.Page as Hotels2BasePage).CurrenStafftLangId);
                cupdating.ProductTypeNameTitle = txtTitle.Text;
                cupdating.Update();
            }
            BindContentLang();
            //Response.Redirect(Request.UrlReferrer.ToString());
        }

       

        
        
    }
}