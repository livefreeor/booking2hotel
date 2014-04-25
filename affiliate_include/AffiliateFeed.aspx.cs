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
public partial class affiliate_include_AffiliateFeed : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime dateStart;
        DateTime dateEnd;
        string result = string.Empty;
        string sqlCommand = string.Empty;
        string sqlCommandPack = string.Empty;
        string xmlString = string.Empty;
        byte manageID = 1;
        string emailHotel = string.Empty;

        int productID = int.Parse(Request.QueryString["pid"]);

        DateTime dateCheck;
        DateTime dateCurrent;

        

        string connString = WebConfigurationManager.ConnectionStrings["booking2hotelXConnectionString"].ConnectionString;
        using (SqlConnection cn = new SqlConnection(connString))
        {
            sqlCommand = "select p.extranet_active,pbe.manage_id,p.email";
            sqlCommand = sqlCommand + " from tbl_product p,tbl_product_booking_engine pbe";
            sqlCommand = sqlCommand + " where p.product_id=pbe.product_id and p.product_id=" + productID;
            //Response.Write(sqlCommand);
            SqlCommand cmd = new SqlCommand(sqlCommand, cn);
            
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                emailHotel=reader["email"].ToString();
                manageID = (byte)reader["manage_id"];
                //manageID 1.hotel manage
                //manageDI 2.BHT manage
            }

        }

        if (!string.IsNullOrEmpty(Request.QueryString["datein"]))
        {
            
            dateStart = Utility.ConvertDateInput(Request.QueryString["datein"]);
            dateEnd = Utility.ConvertDateInput(Request.QueryString["dateout"]);

            dateCheck = dateStart.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute);
            dateCurrent = DateTime.Now.Hotels2ThaiDateTime();
            string errorDescription = string.Empty;

            

            if (checkVacationDate(dateStart) && productID != 3605 && productID != 3487 && manageID == 2)
            {
                result = result + "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n";
                result = result + "<RoomRate>\n";
                result = result + "<ErrorMessage>ErrorVacationDate</ErrorMessage>\n";
                result = result + "<ErrorDescription>";

                errorDescription = errorDescription + "<div style=\"padding:10px;background:#fff;\">";
                errorDescription=errorDescription+"<strong style=\"display:block;font-size:18px;color:#5D85B8\">Attention!</strong><p>";
                errorDescription = errorDescription + "Hotel website will be under maintenance for upgrading the whole system during Feb 6 – Feb 10, 2014. Website will not be able to allow the book for check in during the system upgrading period.";
                errorDescription = errorDescription + "</p>";
                errorDescription = errorDescription + "<p>";
                errorDescription = errorDescription + "For guests who wish to reserve room check in during Feb 6 – Feb 10, 2014, you can contact our staff at this email <a href=\"mailto:" + emailHotel + "\">" + emailHotel + "</a>";
                errorDescription = errorDescription + "</p>";
                errorDescription = errorDescription + "<p>";
                errorDescription = errorDescription + "We are sorry for this inconvenience. Thank you very much.";
                errorDescription = errorDescription + "</p></div>";
                result = result + HttpContext.Current.Server.HtmlEncode(errorDescription);
                result = result + "</ErrorDescription>\n";
                result = result + "</RoomRate>\n";
                Response.Write(result);
                Response.End();
            }
            

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

                Response.Write(objPrice.GenerateXmlforExpressCheckout(results));
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
            sqlCommand = sqlCommand + " and pocp_ex.date_price>GETDATE() and poct.lang_id=1 and po.cat_id IN (38,48,52,53,54,55,56,57,58) and po.status=1";
            sqlCommand = sqlCommand + " order by po.priority asc";


            sqlCommandPack = "select distinct(po.option_id) as option_id,poct.title,po.priority,";
            sqlCommandPack = sqlCommandPack + " (select top 1 spoct.title from tbl_product_option_content spoct where spoct.option_id=po.option_id and spoct.lang_id=1) as second_lang";
            sqlCommandPack = sqlCommandPack + " from tbl_product p,tbl_product_option po,tbl_product_option_content poct,tbl_product_option_condition_extra_net poc_ex,tbl_product_option_condition_price_extranet_period pocp_ex";
            sqlCommandPack = sqlCommandPack + " where p.product_id=po.product_id  and po.option_id=poc_ex.option_id and poc_ex.condition_id=pocp_ex.condition_id and po.option_id=poct.option_id and po.product_id=" + productID;
            sqlCommandPack = sqlCommandPack + "  and pocp_ex.date_end>GETDATE()  AND pocp_ex.status = 1 AND poct.lang_id=1 and po.cat_id IN (38,48,52,53,54,55,56,57,58) and po.status=1";
            sqlCommandPack = sqlCommandPack + "  order by po.priority asc";

            xmlString = xmlString + "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n";
            xmlString = xmlString + "<Rooms>\n";




            using (SqlConnection cn = new SqlConnection(connString))
            {
                SqlCommand cmdPack = new SqlCommand(sqlCommandPack, cn);
                cn.Open();
                SqlDataReader readerPack = cmdPack.ExecuteReader();
                while (readerPack.Read())
                {
                    xmlString = xmlString + "<Title>" + readerPack["title"].ToString() + "</Title>\n";
                }
                readerPack.Close();
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
               
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

            //Response.Write("YYYY");
            //Response.End();
            Response.Write(xmlString);
        }

    }

    private bool checkHotelExcept(int ProductID)
    {
        int[] arrProductID = { 3504, 3462, 3463, 3514, 3475, 3512, 3480,3481,3456,3457,3458,3459,3500,3570,3520,3523,3581,3595,3573,3605,3590,3476,3617,3565,3568,3567,3575 };
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
    
public bool checkVacationDate(DateTime dateIn)
    {
        bool result = false;
        string[] vacationDate = { "2014-02-06", "2014-02-07", "2014-02-08", "2014-02-09" };
        foreach (string datecheck in vacationDate)
        {
            if (DateTime.Compare(Utility.ConvertDateInput(datecheck), dateIn) == 0)
            {
                result = true;
            };
        }
        return result;
    }
    
}