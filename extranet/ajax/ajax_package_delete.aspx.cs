using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Staffs;
using Hotels2thailand.ProductOption;
using Hotels2thailand.Production;
using System.Text;


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_package_delete : Hotels2BasePageExtra_Ajax
    {
        
        public string qOptionId
        {
            get { return Request.QueryString["oid"]; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!this.Page.IsPostBack)
            {
                if (this.IsstaffDelete())
                {
                    if (!string.IsNullOrEmpty(this.qOptionId))
                    {
                        try
                        {

                            Option cOption = new Option();
                            
                            
                            Response.Write(cOption.UpdateStatus(int.Parse(this.qOptionId)));

                        }
                        catch (Exception ex)
                        {
                            Response.Write("error:" + ex.Message + "<br/>" + ex.StackTrace);
                            Response.End();
                        }
                       
                    }
                }
                else
                {
                    Response.Write("method_invalid");
                }
                Response.End();
            }
            
        }


        
    }
}