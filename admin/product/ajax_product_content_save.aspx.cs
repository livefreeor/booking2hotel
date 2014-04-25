using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;

namespace Hotels2thailand.UI
{
    public partial class admin_ajax_product_content_save : System.Web.UI.Page
    {
        public string qProductId
        {
            get { return Request.QueryString["pid"]; }
        }

        public string qProductCat
        {
            get { return Request.QueryString["pdcid"]; }
        }

        public byte Current_StaffLangId
        {
            get
            {
                Hotels2BasePage cBasePage = new Hotels2BasePage();
                return cBasePage.CurrenStafftLangId;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           
            int intProductId = int.Parse(this.qProductId);
            bool IsCompleted = false;


            if (!string.IsNullOrEmpty(this.qProductId) && !string.IsNullOrEmpty(this.qProductCat))
            {
                
                
                
                if (this.Current_StaffLangId == 1)
                {
                    IsCompleted = ProductContent.UpdatePContent(intProductId, 1, Request.Form["txtTitle"], Request.Form["txtTitleSec"], Request.Form["txtaddress"], Request.Form["txtShort"], Request.Form["txtdetail"], Request.Form["txtInternet"], Request.Form["txtDirection"]);
                }


                if (this.Current_StaffLangId == 2)
                {
                    ProductContent cProductContent = new ProductContent();
                    cProductContent = cProductContent.GetProductContentById(intProductId, 2);
                    if (cProductContent != null)
                    {
                        IsCompleted = ProductContent.UpdatePContent(intProductId, 2, Request.Form["txtTitle"], Request.Form["txtTitleSec"], Request.Form["txtaddress"], Request.Form["txtShort"], Request.Form["txtdetail"], Request.Form["txtInternet"], Request.Form["txtDirection"]);
                    }
                    else
                    {
                        try
                        {
                            //ProductContent cProductContentcheck = new ProductContent();
                            //cProductContentcheck = cProductContentcheck.GetProductContentById(intProductId, 1);
                            ProductContent cInserting = new ProductContent
                            {
                                ProductID = intProductId,
                                LanguageID = 2,
                                Title = Request.Form["txtTitle"],
                                TitleSecound = Request.Form["txtTitleSec"],
                                DetailShort = Request.Form["txtShort"],
                                Detail = Request.Form["txtdetail"],
                                Direction = Request.Form["txtDirection"],
                                DetailInterNet = Request.Form["txtInternet"],
                                Address = Request.Form["txtaddress"],
                                FileMain = Request.Form["txtFilename"],
                                Status = true
                            };

                            int ret = cInserting.Insert(cInserting);
                            IsCompleted = (ret == 1);
                        }
                        catch (Exception ex)
                        {
                            Response.Write("error: " + ex.Message);
                            Response.End();
                        }
                       
                    }
                }
                
               
               
               Response.Write(IsCompleted);
               Response.End();
            }
            
        }

        
    }
}