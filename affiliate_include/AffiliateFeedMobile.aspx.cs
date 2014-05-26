using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Front;
using Hotels2thailand;
using Hotels2thailand.DataAccess;
using System.Data.SqlClient;
using System.Collections;
using Hotels2thailand.Reviews;
using Hotels2thailand.Booking;
using Hotels2thailand.ProductOption;
using System.Web.Configuration;
public partial class affiliate_include_AffiliateFeedMobile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime dateStart;
        DateTime dateEnd;
        string result = string.Empty;
        string sqlCommand = string.Empty;
        string xmlString = string.Empty;

        int productID = int.Parse(Request.QueryString["pid"]);

        DateTime dateCheck;
        DateTime dateCurrent;
        
        if (!string.IsNullOrEmpty(Request.QueryString["datein"]))
        {
            
            dateStart = Utility.ConvertDateInput(Request.QueryString["datein"]);
            dateEnd = Utility.ConvertDateInput(Request.QueryString["dateout"]);

            dateCheck = dateStart.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute);
            dateCurrent = DateTime.Now.Hotels2ThaiDateTime();

            if (dateCheck.Subtract(dateCurrent).Days < 1 && !checkHotelExcept(productID))
            {
                result=result+"<?xml version=\"1.0\" encoding=\"utf-8\"?>\n";
                result=result+"<RoomRate>\n";
                result=result+"<ErrorMessage>ValidDate</ErrorMessage>\n";
                result=result+"</RoomRate>\n";

                Response.Write(result);

            }else{
                //Response.Write(dateStart.ToShortDateString());
                ProductPriceMain objPrice = new ProductPriceMain(productID, dateStart, dateEnd, 1);
                objPrice.RefQuery = "";
                objPrice.RefUrl = "";


                if (!string.IsNullOrEmpty(Request.QueryString["uip"]))
                {
                    objPrice.IPAddress = Request.QueryString["uip"]; 
                }

                if (!string.IsNullOrEmpty(Request.QueryString["mm"]))
                {
                    if (int.Parse(Request.QueryString["mm"])>0)
                    {
                        
                        Customer customer = new Customer();
                        Customer member = customer.GetCustomerbyId(int.Parse(Request.QueryString["mm"]));
                        if (member.ProductID == productID)
                        {
                         objPrice.memberAuthen = true;
                         objPrice.memberID = member.CustomerID;
                        }else{
                         objPrice.memberAuthen = false;
                         objPrice.memberID = 0;
                        }
                    }
               
                }

                List<ProductPriceMain> results = objPrice.loadAll();

                Response.Write(objPrice.GenerateXmlforExpressCheckoutMobile(results));
            }


            
        }
        else
        {
            byte refCountryID = 0;
            if (Request.ServerVariables["REMOTE_ADDR"] != "::1")
            {
                refCountryID = Convert.ToByte(IPtoCounrty.GetCountryID(Request.ServerVariables["REMOTE_ADDR"]));
            }
            else
            {
                refCountryID = 241;
            }

            sqlCommand = "select distinct(po.option_id) as option_id,poct.title,po.priority,";
            sqlCommand = sqlCommand + "(";
            sqlCommand = sqlCommand + "select top 1 spoct.title from tbl_product_option_content spoct where spoct.option_id=po.option_id and spoct.lang_id=1";
            sqlCommand = sqlCommand + ") as second_lang";
            sqlCommand = sqlCommand + " from tbl_product p,tbl_product_option po,tbl_product_option_content poct,tbl_product_option_condition_extra_net poc_ex,tbl_product_option_condition_price_extranet pocp_ex";
            sqlCommand = sqlCommand + " where p.product_id=po.product_id  and po.option_id=poc_ex.option_id and poc_ex.condition_id=pocp_ex.condition_id and po.option_id=poct.option_id and po.product_id=" + productID;
            sqlCommand = sqlCommand + " and pocp_ex.date_price>GETDATE() and poct.lang_id=1 and po.cat_id IN (38,48,52,53,54,55,56) and po.status=1";
            sqlCommand = sqlCommand + " order by po.priority asc";

            string connString = WebConfigurationManager.ConnectionStrings["booking2hotelXConnectionString"].ConnectionString;
            xmlString = xmlString + "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n";
            xmlString = xmlString + "<Rooms>\n";
            using (SqlConnection cn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    xmlString = xmlString + "<Title>" + reader["title"].ToString() + "</Title>\n";
                }
            }

            if (Session["memberAccess"] != null)
            {
             Customer objCus=(Customer)Session["memberAccess"];
                xmlString = xmlString + "<member>"+objCus.FullName+"</member>\n";
            }
            xmlString = xmlString + "<CountryID>" + refCountryID + "</CountryID>\n";
            xmlString = xmlString + "</Rooms>\n";
            Response.Write(xmlString);
        }

    }

    private bool checkHotelExcept(int ProductID)
    {
        int[] arrProductID = { 3504, 3462, 3463, 3514, 3475, 3512, 3480, 3481, 3456, 3457, 3458, 3459, 3500, 3570, 3520, 3523, 3581, 3595, 3573, 3605, 3590, 3476, 3617, 3565, 3568, 3567, 3575, 3618, 3502,3693 };
        bool result = false;

        for (int countItem = 0; countItem < arrProductID.Length; countItem++)
        {
            if (ProductID == arrProductID[countItem])
            {
                result = true;
                break;
            }
        }
        return result;
    }
}