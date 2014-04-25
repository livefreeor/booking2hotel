using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Text;
using Hotels2thailand.Production;
using Hotels2thailand;

namespace Hotels2thailand.UI
{
    public partial class test_duplicate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.Page.IsPostBack)
            {
                lblTotal.Text = getDuplicatePrice().ToString();
            }

        }

        public int getDuplicatePrice()
        {
            using (SqlConnection cn = new SqlConnection(WebConfigurationManager.ConnectionStrings["booking2hotelXConnectionString"].ConnectionString))
            {
                StringBuilder result = new StringBuilder();


                result.Append("SELECT COUNT(p.product_id)");

                result.Append(" FROM tbl_product p , tbl_product_option op, tbl_product_option_condition_extra_net conx , tbl_product_option_condition_price_extranet conp");
                result.Append(" WHERE p.isextranet = 1 AND p.extranet_active = 1 AND conx.option_id = op.option_id AND conx.condition_id = conp.condition_id");
                result.Append(" AND p.product_id = op.product_id  AND p.status = 1 AND op.status = 1 AND conx.status = 1 AND conp.date_price >= DATEADD(HH,14,GETDATE())");
                result.Append(" AND p.cat_id = 29  AND conp.status = 1");
                result.Append(" AND (");
                result.Append(" SELECT COUNT(pr.price_id) FROM tbl_product_option_condition_price_extranet pr ");
                result.Append(" WHERE pr.condition_id = conx.condition_id AND pr.date_price = conp.date_price 	AND pr.status = 1");
                result.Append(" ) > 1");



                SqlCommand cmd = new SqlCommand(result.ToString(), cn);
                cn.Open();
                return (int)cmd.ExecuteScalar();



            }
        }

        protected void btnRun_Click(object sender, EventArgs e)
        {
            DateTime dDateStart = txtdateStart.Text.Hotels2DateSplitYear("-");
            DateTime dDateEnd = txtDateEnd.Text.Hotels2DateSplitYear("-");
            int DateDiff = dDateEnd.Subtract(dDateStart).Days;
            DateTime dDateCurrent = DateTime.Now;

            PoductPriceExtra cPriceExtra = new PoductPriceExtra();
            List<object> ListPrice = cPriceExtra.getPriceExtraAll(int.Parse(txtCondition.Text));

            string strPriceId = string.Empty;
            for (int days = 0; days <= DateDiff; days++)
            {
                dDateCurrent = dDateStart.AddDays(days);

                string decPriceRate = string.Empty;
                DateTime dDatePrice = dDateCurrent;
                string CheckedBoxKeyValue = string.Empty;
                string Key = string.Empty;
            
                int count = 0;
                foreach (PoductPriceExtra PriceItem in ListPrice)
                {
                    if (PriceItem.DatePrice.Date == dDateCurrent.Date)
                    {
                        if (count>0)
                            strPriceId = strPriceId + "," + PriceItem.PriceId;
                        else
                            strPriceId = strPriceId + PriceItem.PriceId.ToString();  

                        break;

                    }
                    count = count + 1;
                }

            }



            Response.Write(strPriceId);
            Response.End();

        
        }
    }
}