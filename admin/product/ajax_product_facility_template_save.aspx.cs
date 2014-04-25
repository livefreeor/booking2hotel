using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
using Hotels2thailand;

namespace Hotels2thailand.UI
{
    public partial class admin_ajax_product_facility_template_save : System.Web.UI.Page
    {
        public string qProductId
        {
            get { return Request.QueryString["pid"]; }
        }

        public string qProductCat
        {
            get { return Request.QueryString["pdcid"]; }
        }

        //public string qLocationChecked
        //{
        //    get { return Request.QueryString["CheckedLoc"]; }
        //}

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
            if (this.Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(this.qProductId) && !string.IsNullOrEmpty(this.qProductCat))
                {
                    Response.Write(SaveFac());
                    Response.End();

                }
            }
            
        }

        public string SaveFac()
        {
            string Iscompleted = "False";
            try
            {



                if (!string.IsNullOrEmpty(Request.Form["CheckedLoc"]))
                {
                    string[] arrCheck = Request.Form["CheckedLoc"].Hotels2RightCrl(1).Split(',');
                    foreach (string Item in arrCheck)
                    {
                        
                        ProductFacility.InsertNewFac(int.Parse(this.qProductId), 1, Item.Split('%')[0]);
                        ProductFacility.InsertNewFac(int.Parse(this.qProductId), 2, Item.Split('%')[1]);
                        Iscompleted = "True";
                    }

                }


            }
            catch (Exception ex)
            {
                Iscompleted = ex.Message;
            }

            return Iscompleted;


        }
        
        
    }
}