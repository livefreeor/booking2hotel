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
    public partial class Control_Lang_Option_Facility_Box : System.Web.UI.UserControl
    {


        //Hotels2BasePage BasePage = (this.Page as Hotels2BasePage);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                BindContentLang();
                BindTemplateBox();
                BindRadioFacOptionUsed();
                BindDatalistFacCurrentUsed();
            }
            else
            {
                
                //DlLang.DataBind();
            }
            
            
        }
        protected void BindContentLang()
        {

            if (!string.IsNullOrEmpty((this.Page as Hotels2BasePage).qOptionId)) 
            {
                //ProductOptionFacility cProductOptionFac = new ProductOptionFacility();
                //cProductOptionFac.getOptionFacilityByOptionId(int.Parse((this.Page as Hotels2BasePage).qOptionId), (this.Page as Hotels2BasePage).CurrenStafftLangId);
                //txtTitle.Text = cProductOptionFac.Title;


                DlLang.DataBind();
                BindDatalistFacCurrentUsed();
            }
        }

        protected void BindTemplateBox()
        {

            ProductFacilitytempalte cFacTemp = new ProductFacilitytempalte();


            chkFac_tamplate.DataSource = cFacTemp.getFacilityByCatId(2);
            chkFac_tamplate.DataTextField = "TitleShow";

            chkFac_tamplate.DataValueField = "TitleVal";

            chkFac_tamplate.DataBind();
            //chkFac_tamplate.DataSource = Hotels2Facility.GetFacilityOption((this.Page as Hotels2BasePage).CurrenStafftLangId);
            //chkFac_tamplate.DataBind();
        }

        protected void BindRadioFacOptionUsed()
        {
            if (!string.IsNullOrEmpty((this.Page as Hotels2BasePage).qOptionId))
            {
                // get ProductId From current Option
                Option cOption = new Option();
                cOption.GetProductOptionById(int.Parse((this.Page as Hotels2BasePage).qOptionId));

                // get Option List
                Option cOptions = new Option();
                RadioOption.DataSource = cOptions.GetProductOptionByCatIdandNotCurrentOption(cOption.GetProductOptionById(int.Parse((this.Page as Hotels2BasePage).qOptionId)).ProductID, 38, int.Parse((this.Page as Hotels2BasePage).qOptionId));
                RadioOption.DataTextField = "Title";
                RadioOption.DataValueField = "OptionID";
                RadioOption.DataBind();
            }
           
        }

        protected void BindDatalistFacCurrentUsed()
        {
            if (!string.IsNullOrEmpty((this.Page as Hotels2BasePage).qOptionId))
            {
                ProductOptionFacility cOptionFacility = new ProductOptionFacility();
                dlCurrentFac.DataSource = cOptionFacility.getOptionFacilityByOptionId(int.Parse((this.Page as Hotels2BasePage).qOptionId), (this.Page as Hotels2BasePage).CurrenStafftLangId);
                dlCurrentFac.DataBind();
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

                //BindContentLang();
                //System.Web.UI.UserControl userMaster = (System.Web.UI.UserControl)Page.Master.FindControl("languageBox");
                //userMaster.DataBind();

                Response.Redirect(Request.Url.ToString() + "#Fac");
            }
        }

        //protected void dlCurrentFac_OnitemCommand(object sender, DataListCommandEventArgs e)
        //{
        //    if (e.CommandName == "itemupdate")
        //    {

        //        TextBox Txttitle = dlCurrentFac.Items[e.Item.ItemIndex].FindControl("txtBoxItem") as TextBox;
        //        int intFacId = int.Parse(e.CommandArgument.ToString());
        //        ProductOptionFacility cUpdate = new ProductOptionFacility();
        //        cUpdate.getOptionFacilityById(intFacId);
        //        cUpdate.Title = Txttitle.Text;
        //        cUpdate.Update();
        //    }

        //    if (e.CommandName == "itemdelete")
        //    {
        //        int intFacId = int.Parse(e.CommandArgument.ToString());
        //        ProductOptionFacility cDelete = new ProductOptionFacility();
        //        cDelete.getOptionFacilityById(intFacId);
        //        cDelete.Delete();
        //    }
        //    BindContentLang(); 
        //    //Response.Redirect(Request.Url.ToString());
        //}

        public void FacilityUpdate_Click(object sender, EventArgs e)
        {
            Button btns = (Button)sender;
            if (btns.CommandName == "itemupdate")
            {
                string[] argument = btns.CommandArgument.Split(',');
                int ItemIndex = int.Parse(argument[1]);
                int FacId = int.Parse(argument[0]);
                foreach (DataListItem item in dlCurrentFac.Items)
                {
                    if (item.ItemIndex == ItemIndex)
                    {
                       
                        TextBox Txttitle = dlCurrentFac.Items[item.ItemIndex].FindControl("txtBoxItem") as TextBox;
                        ProductOptionFacility cUpdate = new ProductOptionFacility();
                        cUpdate.getOptionFacilityById(FacId);
                        cUpdate.Title = Txttitle.Text;
                        cUpdate.Update();
                    }

                }

            }

            BindContentLang();
        }

        public void FacilityDel_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            if (btn.CommandName == "itemdelete")
            {
                int intFacId = int.Parse(btn.CommandArgument);
                ProductOptionFacility cDelete = new ProductOptionFacility();
                cDelete.getOptionFacilityById(intFacId);
                cDelete.Delete();
            }
            //Response.Redirect(Request.Url.ToString() + "#Fac");

            BindContentLang();
        }
        //protected void btSave_Click(object sender, EventArgs e)
        //{

        //    ProductOptionFacility cProductOptionFac = new ProductOptionFacility
        //    {
        //        OptionId = int.Parse((this.Page as Hotels2BasePage).qOptionId),
        //        LangId = (this.Page as Hotels2BasePage).CurrenStafftLangId,
        //        Title = txtTitle.Text
        //    };
        //    cProductOptionFac.InsertNewOptionFacility(cProductOptionFac);

        //    txtTitle.Text = string.Empty;
        //    BindContentLang();
           
        //}

        protected void btchekbox_Onclick(object sender, EventArgs e)
        {
            foreach (ListItem item in chkFac_tamplate.Items)
            {
                if (item.Selected)
                {

                    //ProductFacility.InsertNewFac(int.Parse(this.qProductId), 1, Item.Split('%')[0]);
                    //ProductFacility.InsertNewFac(int.Parse(this.qProductId), 2, Item.Split('%')[1]);

                    ProductOptionFacility.InsertOptionFacility(int.Parse((this.Page as Hotels2BasePage).qOptionId), 1, item.Value.Split('%')[0]);
                    ProductOptionFacility.InsertOptionFacility(int.Parse((this.Page as Hotels2BasePage).qOptionId), 2, item.Value.Split('%')[1]);
                }
            }
            //Response.Redirect(Request.UrlReferrer.ToString());
            BindContentLang();
        }


        protected void RadioBt_OnClick(object sender, EventArgs e)
        {
            foreach (ListItem item in RadioOption.Items)
            {
                if (item.Selected)
                {
                    ProductOptionFacility cOptionFac = new ProductOptionFacility();
                    var result = cOptionFac.getOptionFacilityByOptionId(int.Parse(item.Value) ,1);
                    foreach (ProductOptionFacility facItem in result)
                    {
                        ProductOptionFacility.InsertOptionFacility(int.Parse((this.Page as Hotels2BasePage).qOptionId), 1, facItem.Title);
                    }

                    var resultThai = cOptionFac.getOptionFacilityByOptionId(int.Parse(item.Value), 2);
                    if(resultThai.Count > 0)
                    {
                        foreach (ProductOptionFacility facItemThai in resultThai)
                        {
                            ProductOptionFacility.InsertOptionFacility(int.Parse((this.Page as Hotels2BasePage).qOptionId), 2, facItemThai.Title);
                        }
                    }
                    
                }
            }
            BindContentLang();
            //Response.Redirect(Request.UrlReferrer.ToString());
        }
        

        

        
    }
}