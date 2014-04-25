using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand;
using System.Data;
using System.Data.SqlClient;
using Hotels2thailand.DataAccess;

using System.Web.Configuration;
/// <summary>
/// Summary description for BookingAcknowledge
/// </summary>
/// 
namespace Hotels2thailand.Booking
{
    public class BookingImport : Hotels2BaseClass
    {
        public int ORDERID { get; set; }
        public int CusID { get; set; }
        public byte StatusId { get; set; }
        public byte CountryID { get; set; }
        public byte Prefix { get; set; }
        public byte GateWayID { get; set; }
        public DateTime DateCheckIN { get; set; }
        public DateTime DateCheckOut { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public byte NumAdult { get; set; }
        public byte NumChild { get; set; }
        public DateTime DateSubmit { get; set; }
        public DateTime DateUpdate { get; set; }
        public bool ConfirmPayment { get; set; }
        public bool ConfirmInput { get; set; }
        public bool ConfirmSent { get; set; }
        public bool Status { get; set; }

        //int intOrderId = (int)reader["order_id"];
                    //int intCustomerId = (int)reader["customer_id"];
                    //byte bytStatusId = (byte)reader["status_id"];
                    //byte shrCountryId = (byte)reader["country_id"];
                    //byte bytprefix = (byte)reader["prefix_id"];
                    //byte bytGateWayID = (byte)reader["gateway_id"];
                    //DateTime dDatecheckin = (DateTime)reader["date_check_in"];
                    //DateTime dDatecheckout = (DateTime)reader["date_check_out"];
                    //string fullname = reader["full_name"].ToString();
                    //string email = reader["email"].ToString();
                    //string phone = reader["phone"].ToString();
                    //byte bytnum_adult = (byte)reader["num_adult"];
                    //byte bytnum_child = (byte)reader["num_children"];
                    //DateTime dDateSubmit = (DateTime)reader["date_submit"];
                    //DateTime dDateUpdate = (DateTime)reader["date_update"];
                    //bool bolConfirmPayment = (bool)reader["confirm_payment"];
                    //bool bolConfirminput = (bool)reader["confirm_input"];
                    //bool bolConfirmSent = (bool)reader["confirm_send"];
                    //bool Status = (bool)reader["status"];
                    //string status = "1";
                    //if (Status)
                    //    status = "0";
                    //int BookingNew_id = 0;
                    //int BookingProductNew_id = 0;


        public BookingImport()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public void ImportCustomer(int intHotelId, int intProductid)
        {
            
               int Count  = 0;
                using (SqlConnection cn = new SqlConnection(this.ConnectionString_booking_old))
                {
                    cn.Open();
                    SqlCommand cmdcus = new SqlCommand("SELECT * FROM tbl_customer WHERE hotel_id = " + intHotelId + "", cn);
                    IDataReader readercus = ExecuteReader(cmdcus);
                    while (readercus.Read())
                    {
                        int intCusOldId = (int)readercus["customer_id"];
                        byte bytPrefixId = (byte)readercus["prefix_id"];
                        byte shrCountryId = (byte)readercus["country_id"];
                        string strFullname = readercus["full_name"].ToString();
                        string strEmail = readercus["email"].ToString();
                        DateTime dDateSubmit = (DateTime)readercus["date_submit"];

                        using (SqlConnection cnnew = new SqlConnection(this.ConnectionString))
                        {
                            SqlCommand cmdcusnew = new SqlCommand("INSERT INTO tbl_customer (country_id,prefix_id,full_name,email,date_submit,product_id) VALUES(" + shrCountryId + "," + bytPrefixId + ",@full_name,'" + strEmail + "','" + dDateSubmit.ToString("yyyy-MM-dd") + "'," + intProductid + "); SET @cus_id = SCOPE_IDENTITY();", cnnew);
                            cmdcusnew.Parameters.Add("@cus_id",SqlDbType.Int).Direction = ParameterDirection.Output;
                            cmdcusnew.Parameters.AddWithValue("@full_name", strFullname);
                            cnnew.Open();
                            ExecuteNonQuery(cmdcusnew);

                            int CusNewID = (int)cmdcusnew.Parameters["@cus_id"].Value;

                            SqlCommand cmdcusTemp = new SqlCommand("INSERT INTO tbl_customer_temp_match (cus_old_id,cus_new_id) VALUES (" + intCusOldId + "," + CusNewID + ")", cnnew);
                            ExecuteNonQuery(cmdcusTemp);
                        }

                       Count = Count + 1;
                    }
                }            
        

                HttpContext.Current.Response.Write(Count + "Import Customer Completed!!");
                HttpContext.Current.Response.Flush();
}



        public void ImportBooking(int intHotelId, int intProductid, short intSupplierPrice)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString_booking_old))
            {
                cn.Open();

                string strREmark = string.Empty;
                string strCancellation = string.Empty;

                //SelectREmark From tbl_hotel
                SqlCommand cmdremark = new SqlCommand("SELECT remark ,policy_cancel FROM tbl_hotel WHERE hotel_id = " + intHotelId + "", cn);
                IDataReader readerREmark = ExecuteReader(cmdremark);

                if (readerREmark.Read())
                {
                    strREmark = readerREmark["remark"].ToString();
                    strCancellation = readerREmark["policy_cancel"].ToString();
                }
                readerREmark.Close();

                string sqlOptional = "SELECT TOP 200 * FROM tbl_order WHERE  hotel_id = " + intHotelId + "";
                sqlOptional = sqlOptional + " AND order_id NOT IN (SELECT TOP 881 order_id FROM tbl_order WHERE  hotel_id = " + intHotelId + " ORDER BY order_id DESC)";
                sqlOptional = sqlOptional + " ORDER BY order_id DESC";
               
                string SQlnormal = "SELECT  * FROM tbl_order WHERE  hotel_id = " + intHotelId + " ORDER BY order_id DESC";
                //Select From tbl_order from Old DB
                SqlCommand cmd = new SqlCommand(SQlnormal, cn);
                IDataReader reader = ExecuteReader(cmd);
                int countOrderOld = 0;

                IList<BookingImport> ilistBookingImport = new List<BookingImport>();

                while (reader.Read())
                {
                    ilistBookingImport.Add(new BookingImport { 
                     
                    ORDERID = (int)reader["order_id"],
                    CusID = (int)reader["customer_id"],
                    StatusId = (byte)reader["status_id"],
                    CountryID = (byte)reader["country_id"],
                    Prefix = (byte)reader["prefix_id"],
                    GateWayID = (byte)reader["gateway_id"],
                    DateCheckIN  = (DateTime)reader["date_check_in"],
                    DateCheckOut =(DateTime)reader["date_check_out"],
                    Fullname =reader["full_name"].ToString(),
                    Email =reader["email"].ToString(),
                    Phone = reader["phone"].ToString(),
                    NumAdult= (byte)reader["num_adult"],
                    NumChild=(byte)reader["num_children"],
                    DateSubmit=(DateTime)reader["date_submit"],
                    DateUpdate =(DateTime)reader["date_update"],
                    ConfirmPayment=(bool)reader["confirm_payment"],
                    ConfirmInput=(bool)reader["confirm_input"],
                    ConfirmSent=(bool)reader["confirm_send"],
                    Status= (bool)reader["status"]
                    });
                }

                reader.Close();

                foreach(BookingImport list in ilistBookingImport )
                {
                    int intOrderId = list.ORDERID;
                    int intCustomerId = list.CusID;
                    byte bytStatusId = list.StatusId;
                    byte shrCountryId = list.CountryID;
                    byte bytprefix = list.Prefix;
                    byte bytGateWayID = list.GateWayID;
                    DateTime dDatecheckin = list.DateCheckIN;
                    DateTime dDatecheckout = list.DateCheckOut;
                    string fullname = list.Fullname;
                    string email = list.Email;
                    string phone = list.Phone;
                    byte bytnum_adult = list.NumAdult;
                    byte bytnum_child = list.NumChild;
                    DateTime dDateSubmit = list.DateSubmit;
                    DateTime dDateUpdate = list.DateUpdate;
                    bool bolConfirmPayment = list.ConfirmPayment;
                    bool bolConfirminput = list.ConfirmInput;
                    bool bolConfirmSent = list.ConfirmSent;
                    bool Status = list.Status;
                    string status = "1";
                    if (Status)
                        status = "0";
                    int BookingNew_id = 0;
                    int BookingProductNew_id = 0;

                    countOrderOld = countOrderOld + 1;


                    HttpContext.Current.Response.Write("NO." + countOrderOld + "ORDER ID " + intOrderId + "Status=" + Status + "<br/>");
                    HttpContext.Current.Response.Flush();
                    using (SqlConnection cnreal = new SqlConnection(this.ConnectionString_booking_old))
                    {
                        SqlCommand cmdSelectHotelId = new SqlCommand("SELECT real_id FROM tbl_order_hotel WHERE order_id = " + intOrderId + " AND hotel_id = " + intHotelId + "", cnreal);
                        cnreal.Open();
                        IDataReader readerreal = ExecuteReader(cmdSelectHotelId);
                        if (readerreal.Read())
                        {
                            int intRealId = (int)readerreal["real_id"];
                            using (SqlConnection cnnew = new SqlConnection(this.ConnectionString))
                            {
                                cnnew.Open();

                                int MatchCus = intCustomerId;
                                SqlCommand cmdMatchCus = new SqlCommand("SELECT cus_new_id FROM tbl_customer_temp_match WHERE cus_old_id= " + intCustomerId + "", cnnew);
                                IDataReader readermatch = ExecuteReader(cmdMatchCus);
                                if (readermatch.Read())
                                    MatchCus = (int)readermatch["cus_new_id"];


                                readermatch.Close();

                                SqlCommand cmdinsertbookin = new SqlCommand("INSERT INTO tbl_booking (country_id,status_id,cus_id,lang_id,prefix_id,name_full,email,date_submit,date_modify,status,is_extranet) VALUES (" + shrCountryId + "," + getNewbookingStatus(bytStatusId) + "," + MatchCus + ",1," + GetnewPrefix(bytprefix) + ",@full_name,'" + email + "','" + dDateSubmit.ToString("yyyy-MM-dd") + "','" + dDateUpdate.ToString("yyyy-MM-dd") + "'," + status + ",1); SET @booking_id=SCOPE_IDENTITY();", cnnew);

                                cmdinsertbookin.Parameters.Add("@booking_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                                cmdinsertbookin.Parameters.AddWithValue("@full_name", fullname);
                                ExecuteNonQuery(cmdinsertbookin);
                                BookingNew_id = (int)cmdinsertbookin.Parameters["@booking_id"].Value;
                                HttpContext.Current.Response.Write(BookingNew_id + "Import tbl_booking Completed!! <br/>");
                                HttpContext.Current.Response.Flush();


                                using (SqlConnection cnclose = new SqlConnection(this.ConnectionString_booking_old))
                                {
                                    //Close Booking Old 
                                    SqlCommand cmdclose = new SqlCommand("UPDATE tbl_order SET status = 0 WHERE order_id = " + intOrderId + "", cnclose);
                                    cnclose.Open();
                                    ExecuteNonQuery(cmdclose);
                                    HttpContext.Current.Response.Write("order_id" + intOrderId + ": Close booking Conpleted <br/>");
                                    HttpContext.Current.Response.Flush();
                                }

                                //Booking Hotel ID
                                SqlCommand cmdBookinghotelId = new SqlCommand("INSERT INTO tbl_booking_hotels (booking_id,product_id,booking_hotel_id) VALUES(" + BookingNew_id + "," + intProductid + "," + intRealId + ")", cnnew);

                                ExecuteNonQuery(cmdBookinghotelId);
                                HttpContext.Current.Response.Write(intRealId + "Import tbl_booking_hotel_Id Completed!! <br/>");
                                HttpContext.Current.Response.Flush();


                                //Booking Activity
                                using(SqlConnection cnoldAc = new SqlConnection(this.ConnectionString_booking_old))
                                {
                                    cnoldAc.Open();
                                    SqlCommand cmdacold = new SqlCommand("SELECT hs.name_full,oa.detail, oa.date_submit  FROM tbl_order_activity oa, tbl_hotel_staff hs WHERE oa.staff_id = hs.staff_id AND oa.order_id = "+intOrderId+"", cnoldAc);
                                    IDataReader readerAcold = ExecuteReader(cmdacold);
                                    while(readerAcold.Read())
                                    {
                                        string NameFull = readerAcold["name_full"] .ToString();
                                        string Detail = readerAcold["detail"].ToString() + "<br/>[By " + NameFull + "]";
                                        DateTime dDateAc = (DateTime)readerAcold["date_submit"];

                                        //server == 398
                                        //local == 393
                                        SqlCommand cmdActivity = new SqlCommand("INSERT INTO tbl_booking_activity (booking_id,staff_id,detail,date_submit) VALUES (" + BookingNew_id + ",398,@ac_detail,@date_submit) ", cnnew);
                                        cmdActivity.Parameters.AddWithValue("@date_submit", dDateAc);

                                        cmdActivity.Parameters.Add("@ac_detail", SqlDbType.NVarChar).Value = Detail;
                                        ExecuteNonQuery(cmdActivity);
                                    }
                                }
                                HttpContext.Current.Response.Write(intRealId + "Import tbl_booking_activity Completed!! <br/>");
                                HttpContext.Current.Response.Flush();
                                

                                //Booking Phone
                                SqlCommand cmdBookingPhone = new SqlCommand("INSERT INTO tbl_booking_phone (cat_id,booking_id,code_country,code_local,number_phone) VALUES (1," + BookingNew_id + ",'','','" + phone + "')", cnnew);
                                ExecuteNonQuery(cmdBookingPhone);
                                HttpContext.Current.Response.Write(intRealId + "Import tbl_booking_Phone Completed!! <br/>");
                                HttpContext.Current.Response.Flush();

                                //Booking Product
                                SqlCommand cmdBookingProduct = new SqlCommand("INSERT INTO tbl_booking_product (booking_id,supplier_id,product_id,status_id,num_adult,num_child,date_time_check_in,date_time_check_out) VALUES (" + BookingNew_id + "," + intSupplierPrice + "," + intProductid + "," + getNewbookingProductStatus(bytStatusId) + "," + bytnum_adult + "," + bytnum_child + ",'" + dDatecheckin.ToString("yyyy-MM-dd") + "','" + dDatecheckout.ToString("yyyy-MM-dd") + "');SET @booking_product_id = SCOPE_IDENTITY(); ", cnnew);
                                cmdBookingProduct.Parameters.Add("@booking_product_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                                ExecuteNonQuery(cmdBookingProduct);

                                BookingProductNew_id = (int)cmdBookingProduct.Parameters["@booking_product_id"].Value;
                                HttpContext.Current.Response.Write(intRealId + "Import tbl_booking_Product Completed!! <br/>");
                                HttpContext.Current.Response.Flush();


                                decimal PriceTotal = 0;
                                //BookingItem
                                using (SqlConnection cnproductold = new SqlConnection(this.ConnectionString_booking_old))
                                {
                                    SqlCommand cmdproductorder = new SqlCommand("SELECT op.order_product_id,op.order_id,op.product_id,op.quantity,op.price,op.promotion_id,op.promotion_title,op.promotion_detail,p.product_cat_id,p.title as productTitle,p.breakfast FROM tbl_order_product op, tbl_product p WHERE p.product_id = op.product_id AND op.order_id = " + intOrderId + "", cnproductold);
                                    cnproductold.Open();
                                    IDataReader readerOrder_product = ExecuteReader(cmdproductorder);
                                    while (readerOrder_product.Read())
                                    {
                                        string OPtiontitle = readerOrder_product["productTitle"].ToString();
                                        bool ABF =  (bool)readerOrder_product["breakfast"];
                                        byte bytOptionCat = (byte)readerOrder_product["product_cat_id"];
                                        int order_product_id = (int)readerOrder_product["order_product_id"];
                                        byte bytQuan = (byte)readerOrder_product["quantity"];
                                        decimal Price = (decimal)readerOrder_product["price"];
                                        short Promotionid = (readerOrder_product["promotion_id"] == DBNull.Value ? (short)0 : (short)readerOrder_product["promotion_id"]);
                                        string Protitle = (readerOrder_product["promotion_title"] == DBNull.Value ? string.Empty : readerOrder_product["promotion_title"].ToString());
                                        string ProDetail = (readerOrder_product["promotion_detail"] == DBNull.Value ? string.Empty : readerOrder_product["promotion_detail"].ToString());

                                        SqlCommand cmdInsertOption = new SqlCommand("INSERT INTO tbl_product_option (cat_id,product_id,title,size,status) VALUES(" + GetnewOptionCat(bytOptionCat) + "," + intProductid + ",@title,0,0);SET @option_id = SCOPE_IDENTITY();", cnnew);

                                        cmdInsertOption.Parameters.Add("@title", SqlDbType.VarChar).Value = OPtiontitle;
                                        cmdInsertOption.Parameters.Add("@option_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                                        ExecuteNonQuery(cmdInsertOption);
                                        
                                        int OptionID = (int)cmdInsertOption.Parameters["@option_id"].Value;


                                        HttpContext.Current.Response.Write(OptionID + "InSert Option Completed!! <br/>");
                                        HttpContext.Current.Response.Flush();

                                        SqlCommand cmdOPtionContent = new SqlCommand("INSERT INTO tbl_product_option_content (option_id,lang_id,title) VALUES (" + OptionID + ",1,@title)", cnnew);
                                        cmdOPtionContent.Parameters.Add("@title", SqlDbType.NVarChar).Value = OPtiontitle;
                                        //title
                                        ExecuteNonQuery(cmdOPtionContent);

                                        HttpContext.Current.Response.Write(OptionID + "InSert Option Content Completed!! <br/>");
                                        HttpContext.Current.Response.Flush();


                                        byte bytABF = 0;
                                        if(ABF)
                                        {
                                            bytABF = (byte)(bytnum_adult + bytnum_child);
                                        }
                                        SqlCommand cmdInsertCondition = new SqlCommand("INSERT INTO tbl_product_option_condition_extra_net (option_id,title,breakfast,num_adult,num_children,num_extra,condition_name_id)VALUES(" + OptionID + ",'import condition'," + bytABF + "," + bytnum_adult + "," + bytnum_child + ",0,6);SET @condition_id = SCOPE_IDENTITY();", cnnew);
                                        cmdInsertCondition.Parameters.Add("@condition_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                                        ExecuteNonQuery(cmdInsertCondition);
                                        int inConditionId = (int)cmdInsertCondition.Parameters["@condition_id"].Value;
                                        HttpContext.Current.Response.Write(inConditionId + "InSert Condition Completed!! <br/>");
                                        HttpContext.Current.Response.Flush();

                                        //Booking Item 

                                        string ConditionDetailString = strREmark + strCancellation;

                                        SqlCommand cmdInsertBooingItem = new SqlCommand("INSERT INTO tbl_booking_item (booking_id,booking_product_id,option_id,condition_id,condition_title,condition_detail,unit,price_supplier,price,price_display,promotion_id,promotion_title,promotion_detail,status,breakfast,num_children,num_adult,option_title) VALUES (" + BookingNew_id + "," + BookingProductNew_id + "," + OptionID + "," + inConditionId + ",'import condition',@condition_detail," + bytQuan + "," + Price + "," + Price + "," + Price + ",@promotion_id,@promotion_title,@promotion_detail,1," + bytABF + "," + bytnum_child + "," + bytnum_adult + ",@title); SET @booking_item_id = SCOPE_IDENTITY();", cnnew);
                                        if (Promotionid == 0)
                                            cmdInsertBooingItem.Parameters.AddWithValue("@promotion_id",DBNull.Value);
                                        else
                                            cmdInsertBooingItem.Parameters.AddWithValue("@promotion_id", Promotionid);

                                        if (string.IsNullOrEmpty(Protitle))
                                            cmdInsertBooingItem.Parameters.AddWithValue("@promotion_title", DBNull.Value);
                                        else
                                            cmdInsertBooingItem.Parameters.AddWithValue("@promotion_title", Protitle);

                                        if (string.IsNullOrEmpty(ProDetail))
                                            cmdInsertBooingItem.Parameters.AddWithValue("@promotion_detail", DBNull.Value);
                                        else
                                            cmdInsertBooingItem.Parameters.AddWithValue("@promotion_detail", ProDetail);

                                        cmdInsertBooingItem.Parameters.Add("@condition_detail", SqlDbType.NVarChar).Value = ConditionDetailString;
                                        cmdInsertBooingItem.Parameters.Add("@title", SqlDbType.NVarChar).Value = OPtiontitle;
                                        
                                        cmdInsertBooingItem.Parameters.Add("@booking_item_id", SqlDbType.Int).Direction = ParameterDirection.Output;

                                        ExecuteNonQuery(cmdInsertBooingItem);

                                        int BookingItemID = (int)cmdInsertBooingItem.Parameters["@booking_item_id"].Value;

                                        HttpContext.Current.Response.Write(intRealId + "Import BookingItem Completed!! <br/>");
                                        HttpContext.Current.Response.Flush();


                                        using(SqlConnection cnReq = new SqlConnection(this.ConnectionString_booking_old))
                                        {
                                            SqlCommand cmdReq = new SqlCommand("SELECT * FROM tbl_require_room WHERE order_product_id = "+order_product_id+"",cnReq);
                                            cnReq.Open();
                                            IDataReader readerReq = ExecuteReader(cmdReq);
                                            if(readerReq.Read())
                                            {
                                                string ReqComment = readerReq["comment"].ToString();
                                                byte bytBed = (byte)readerReq["type_bed"];
                                                byte bytSmoke = (byte)readerReq["type_smoke"];
                                                byte bytfloor = (byte)readerReq["type_floor"];
                                                SqlCommand cmdBookingRequire = new SqlCommand("INSERT INTO tbl_booking_item_require_hotel (booking_item_id,comment,type_smoking,type_bed,type_floor) VALUES (" + BookingItemID + ",@comment," + bytSmoke + "," + bytBed + "," + bytfloor + ")", cnnew);

                                                cmdBookingRequire.Parameters.Add("@comment", SqlDbType.NVarChar).Value = ReqComment;
                                                ExecuteNonQuery(cmdBookingRequire);

                                                HttpContext.Current.Response.Write(intRealId + "Import BookingRequireMend Completed!! <br/>");
                                                HttpContext.Current.Response.Flush();
                                            }
                                        }
                                        

                                        PriceTotal = PriceTotal + Price;
                                    }
                                }


                                DateTime date = new DateTime(1900, 1, 1);
                                //BookingPayment bolConfirmPayment

                                int intCOuntPay = 0;
                                using (SqlConnection cnPayment = new SqlConnection(this.ConnectionString_booking_old))
                                {
                                    SqlCommand cmdCountOrderPayment = new SqlCommand("SELECT COUNT(*) FROM tbl_order_payment WHERE order_id = " + intOrderId + "", cnPayment);

                                    cnPayment.Open();
                                    intCOuntPay = (int)ExecuteScalar(cmdCountOrderPayment);

                                    if (intCOuntPay > 0)
                                    {
                                        SqlCommand cmdorderPayment = new SqlCommand("SELECT * FROM tbl_order_payment WHERE order_id = " + intOrderId + "", cnPayment);
                                       
                                        IDataReader readerPay = ExecuteReader(cmdorderPayment);
                                        while (readerPay.Read())
                                        {
                                            decimal dePrice = (decimal)readerPay["amount"];
                                            DateTime dDAtePay = (DateTime)readerPay["date_submit"];
                                            SqlCommand cmdBookingPayment = new SqlCommand("INSERT INTO tbl_booking_payment (booking_id,booking_payment_cat_id,gateway_id,amount,title,date_payment,confirm_payment,status) VAlUES (" + BookingNew_id + ",1," + GetGateWay(bytGateWayID) + "," + dePrice + ",'Import method',@date_payment,@confirm_payment,1); SET @booking_payment_id = SCOPE_IDENTITY();", cnnew);
                                            cmdBookingPayment.Parameters.Add("@booking_payment_id", SqlDbType.Int).Direction = ParameterDirection.Output;

                                            cmdBookingPayment.Parameters.Add("@date_payment", SqlDbType.SmallDateTime).Value = dDAtePay;
                                            if (bolConfirmPayment)
                                                cmdBookingPayment.Parameters.Add("@confirm_payment", SqlDbType.SmallDateTime).Value = date;
                                            else
                                                cmdBookingPayment.Parameters.AddWithValue("@confirm_payment", DBNull.Value);

                                            ExecuteNonQuery(cmdBookingPayment);

                                            int BookingPaymentId = (int)cmdBookingPayment.Parameters["@booking_payment_id"].Value;
                                            SqlCommand cmdpaymentBank = new SqlCommand("INSERT INTO tbl_booking_payment_bank (booking_payment_id,date_submit) VALUES(" + BookingPaymentId + ",@date_submit)", cnnew);
                                            cmdpaymentBank.Parameters.AddWithValue("@date_submit", dDAtePay);
                                            ExecuteNonQuery(cmdpaymentBank);
                                        }
                                    }
                                    else
                                    {
                                        SqlCommand cmdBookingPayment = new SqlCommand("INSERT INTO tbl_booking_payment (booking_id,booking_payment_cat_id,gateway_id,amount,title,date_payment,confirm_payment,status) VAlUES (" + BookingNew_id + ",1," + GetGateWay(bytGateWayID) + "," + PriceTotal + ",'Import method',@date_payment,@confirm_payment,1); SET @booking_payment_id = SCOPE_IDENTITY();", cnnew);
                                        cmdBookingPayment.Parameters.Add("@booking_payment_id", SqlDbType.Int).Direction = ParameterDirection.Output;

                                        cmdBookingPayment.Parameters.Add("@date_payment", SqlDbType.SmallDateTime).Value = dDateSubmit;
                                        if (bolConfirmPayment)
                                            cmdBookingPayment.Parameters.Add("@confirm_payment", SqlDbType.SmallDateTime).Value = date;
                                        else
                                            cmdBookingPayment.Parameters.AddWithValue("@confirm_payment", DBNull.Value);

                                        ExecuteNonQuery(cmdBookingPayment);

                                        int BookingPaymentId = (int)cmdBookingPayment.Parameters["@booking_payment_id"].Value;
                                        SqlCommand cmdpaymentBank = new SqlCommand("INSERT INTO tbl_booking_payment_bank (booking_payment_id,date_submit) VALUES(" + BookingPaymentId + ",@date_submit)", cnnew);
                                        cmdpaymentBank.Parameters.AddWithValue("@date_submit", dDateSubmit);
                                        ExecuteNonQuery(cmdpaymentBank);
                                    }
                                }
                               
                                HttpContext.Current.Response.Write(intRealId + "Import Booking Payment Completed!! <br/>");
                                HttpContext.Current.Response.Flush();
                                
                                

                                
                                
                                //GetGateWay
                                //1/1/1900

                               
                                //BookingConfirm bolConfirminput // 18
                                if (bolConfirminput)
                                {
                                    SqlCommand cmdConfirminputONE = new SqlCommand("INSERT INTO tbl_booking_confirm (booking_id,confirm_cat_id,date_submit,status) VALUES (" + BookingNew_id + ",18,@date_submit,1) ", cnnew);
                                    cmdConfirminputONE.Parameters.Add("@date_submit", SqlDbType.SmallDateTime).Value = date;
                                    ExecuteNonQuery(cmdConfirminputONE);
                                    HttpContext.Current.Response.Write(" Confirm INput +++ Import tbl_booking_Confirm Completed!! <br/>");
                                    HttpContext.Current.Response.Flush();
                                }

                                //BookingConfirm bolConfirmSent  == Confirm  Open  4
                                if (bolConfirmSent)
                                {
                                    SqlCommand cmdConfirminputTWO = new SqlCommand("INSERT INTO tbl_booking_confirm (booking_id,confirm_cat_id,date_submit,status) VALUES (" + BookingNew_id + ",4,@date_submit,1) ", cnnew);
                                    cmdConfirminputTWO.Parameters.AddWithValue("@date_submit", SqlDbType.SmallDateTime).Value = date;
                                    ExecuteNonQuery(cmdConfirminputTWO);
                                    HttpContext.Current.Response.Write("Confirm Sent +++ Import tbl_booking_Confirm Completed!! <br/>");
                                    HttpContext.Current.Response.Flush();
                                }

                                
                            }
                        }
                    }


                    HttpContext.Current.Response.Write("-------------------------------------------------- End<br/><br/><br/><br/>");
                    HttpContext.Current.Response.Flush();
                }
            }
        }


        public void InportREview(int intHotelId, int intProductid)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString_booking_old))
            {
                SqlCommand cmdold = new SqlCommand("SELECT * FROM tbl_review_hotel WHERE hotel_id = " + intHotelId + "", cn);
                cn.Open();
                IDataReader reader = ExecuteReader(cmdold);

                using (SqlConnection cnnew = new SqlConnection(this.ConnectionString))
                {
                    cnnew.Open();
                    while (reader.Read())
                    {
                        int MatchCus = 0;
                        
                        if(reader["customer_id"] != DBNull.Value)
                        {
                            SqlCommand cmdMatchCus = new SqlCommand("SELECT cus_new_id FROM tbl_customer_temp_match WHERE cus_old_id= " + reader["customer_id"] + "", cnnew);
                            IDataReader readermatch = ExecuteReader(cmdMatchCus);
                            if (readermatch.Read())
                                MatchCus = (int)readermatch["cus_new_id"];

                            readermatch.Close();
                        }

                        //HttpContext.Current.Response.Write(MatchCus);
                        //HttpContext.Current.Response.Flush();

                        string Title = reader["title"].ToString();
                        string Detail = reader["detail"].ToString();
                        string Name = reader["review_name"].ToString();
                        string REviewFrom = reader["review_from"].ToString();
                        byte bytOverall = (byte)reader["rate_overall"];
                        byte bytservice = (byte)reader["rate_service"];
                        byte bytlocation = (byte)reader["rate_location"];
                        byte bytroom = (byte)reader["rate_room"];
                        byte bytclean = (byte)reader["rate_clean"];
                        byte bytmoney = (byte)reader["rate_money"];
                        DateTime dDate = (DateTime)reader["date_submit"];
                        bool status = (bool)reader["status"];
                        bool StatusBin = (bool)reader["status_bin"];


                        if (!status && StatusBin)
                            StatusBin = false;

                        if (status && !StatusBin)
                            StatusBin = true;

                        SqlCommand cmdnew = new SqlCommand("INSERT INTO tbl_review_all (product_id,cus_id,recommend_id,from_id,cat_id,title,detail,full_name,review_from,rate_overall,rate_service,rate_location,rate_room,rate_clean,rate_money,date_submit,status,status_bin) VALUES (" + intProductid + ",@cus_id,1,1,29,@title,@detail,@review_name,@review_from," + bytOverall + "," + bytservice + "," + bytlocation + "," + bytroom + "," + bytclean + "," + bytmoney + ",'" + dDate.ToString("yyyy-MM-dd") + "',@status,@status_bin)", cnnew);

                        cmdnew.Parameters.AddWithValue("@status",status);
                        cmdnew.Parameters.AddWithValue("@status_bin", StatusBin);

                        cmdnew.Parameters.AddWithValue("@review_name", Name);
                        cmdnew.Parameters.AddWithValue("@review_from", REviewFrom);
                        cmdnew.Parameters.AddWithValue("@detail", Detail);
                        cmdnew.Parameters.AddWithValue("@title", Title);

                        //cmdnew.Parameters.AddWithValue("@status_bin", Detail);

                        if (MatchCus == 0)
                            cmdnew.Parameters.AddWithValue("@cus_id", DBNull.Value);
                        else
                            cmdnew.Parameters.AddWithValue("@cus_id", MatchCus);


                        ExecuteNonQuery(cmdnew);

                        HttpContext.Current.Response.Write(MatchCus + "Import Review!!<br/>");
                        HttpContext.Current.Response.Flush();
                    }
                }
            }
        }

        public void ReCheckPaymet(int hotelId, int ProductId)
        {
            IList<ArrayList> ilist = new List<ArrayList>();
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT  bh.booking_hotel_id, bp.booking_id , bp.booking_product_id,(SELECT SUM(bi.price) FROM tbl_booking_item bi WHERE bi.booking_id=bp.booking_id AND bi.status = 1) , b.date_submit FROM tbl_booking_product bp, tbl_booking_hotels bh, tbl_booking b WHERE (SELECT COUNT(*) FROM tbl_booking_payment bpp WHERE bpp.booking_id=bp.booking_id AND bpp.status= 1) = 0 AND bh.booking_id = bp.booking_id AND bp.product_id= @product_id AND b.booking_id = bp.booking_id ", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = ProductId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                while (reader.Read())
                {
                    ArrayList arrItem = new ArrayList();
                    arrItem.Add((int)reader[0]);
                    arrItem.Add((int)reader[1]);
                    arrItem.Add((int)reader[2]);
                    arrItem.Add((decimal)reader[3]);
                    arrItem.Add((DateTime)reader[4]);
                    ilist.Add(arrItem);
                }
            }

            foreach (ArrayList arritem in ilist)
            {
                //if ((int)arritem[1] > 229930)
                //{
                    bool isConfirm = false;
                    byte bytGateWayId = 0;
                    HttpContext.Current.Response.Write("BookingHotelID  " + arritem[0] + "   Booking Id=" + arritem[1] + "<br/>");
                    HttpContext.Current.Response.Flush();
                    using (SqlConnection cn = new SqlConnection(this.ConnectionString_booking_old))
                    {
                        string SQlnormal = "SELECT  o.confirm_payment, o.gateway_id FROM tbl_order o , tbl_order_hotel oh WHERE  o.hotel_id = " + hotelId + " AND oh.order_id= o.order_id AND oh.real_id = " + arritem[0] + "";
                        SqlCommand cmd = new SqlCommand(SQlnormal, cn);
                        cn.Open();
                        IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                        if (reader.Read())
                        {
                            isConfirm = (bool)reader[0];
                            bytGateWayId = (byte)reader[1];

                            using (SqlConnection cnnew = new SqlConnection(this.ConnectionString))
                            {
                                cnnew.Open();
                                SqlCommand cmdBookingPayment = new SqlCommand("INSERT INTO tbl_booking_payment (booking_id,booking_payment_cat_id,gateway_id,amount,title,date_payment,confirm_payment,status) VAlUES (" + arritem[1] + ",1," + GetGateWay(bytGateWayId) + "," + arritem[3] + ",'Import method',@date_payment,@confirm_payment,1); SET @booking_payment_id = SCOPE_IDENTITY();", cnnew);
                                cmdBookingPayment.Parameters.Add("@booking_payment_id", SqlDbType.Int).Direction = ParameterDirection.Output;

                                cmdBookingPayment.Parameters.Add("@date_payment", SqlDbType.SmallDateTime).Value = (DateTime)arritem[4];
                                if (isConfirm)
                                    cmdBookingPayment.Parameters.Add("@confirm_payment", SqlDbType.SmallDateTime).Value = (DateTime)arritem[4];
                                else
                                    cmdBookingPayment.Parameters.AddWithValue("@confirm_payment", DBNull.Value);

                                ExecuteNonQuery(cmdBookingPayment);

                                int BookingPaymentId = (int)cmdBookingPayment.Parameters["@booking_payment_id"].Value;
                                SqlCommand cmdpaymentBank = new SqlCommand("INSERT INTO tbl_booking_payment_bank (booking_payment_id,date_submit) VALUES(" + BookingPaymentId + ",@date_submit)", cnnew);
                                cmdpaymentBank.Parameters.AddWithValue("@date_submit", (DateTime)arritem[4]);
                                ExecuteNonQuery(cmdpaymentBank);
                            }

                            HttpContext.Current.Response.Write("check new payment " + arritem[1] + "Amount=" + arritem[3] + "<br/>");
                            HttpContext.Current.Response.Flush();
                        }


                    }


                    
                //}
               
                


            }
            
        }

        public byte GetGateWay(byte bytGateWayOld)
        {
            byte bytGate = 0;
            switch (bytGateWayOld)
            {
                case 1:
                    bytGate = 3;
                    break;
                case 2:
                    bytGate = 9;
                    break;
                case 3:
                    bytGate = 10;
                    break;
                case 4:
                    bytGate = 5;
                    break;
                case 5:
                    bytGate = 2;
                    break;
                case 6:
                    bytGate = 13;
                    break;
                case 7:
                    bytGate = 12;
                    break;
                case 8:
                    bytGate = 11;
                    break;
                case 49:
                    bytGate = 7;
                    break;
                case 50:
                    bytGate = 8;
                    break;
            }
            return bytGate;
        }

        public byte GetnewOptionCat(byte CatOld)
        {
            //            1	Room
            //2	Extra Bed
            //3	Transfer
            //4	Dinner
            //5	Gala Dinner
            //6	Package
            byte cat = 0;
            switch (CatOld)
            {
                case 1:
                    cat = 38;
                    break;
                case 2:
                    cat = 39;
                    break;
                case 3:
                    cat = 43;
                    break;
                case 4:
                case 5:
                    cat = 47;
                    break;
                
                case 6:
                    cat = 57;
                    break;
            }

            return cat;
        }
        public byte GetnewPrefix(byte oldPrefix)
        {
            byte prefix = 0;
            switch (oldPrefix)
            {
                case 1:
                    prefix = 2;
                    break;
                case 2:
                    prefix = 3;
                    break;
                case 3:
                    prefix = 4;
                    break;
            }

            return prefix;
        }
        
        public byte getNewbookingStatus(byte oldStatus)
        {
            byte booking_status = 0;
            switch (oldStatus)
            {
                case 1:
                    booking_status = 68;
                    break;
                case 2:
                    booking_status = 71;
                    break;
                case 3:
                    booking_status = 72;
                    break;
                case 4:
                    booking_status = 83;
                    break;
                case 5:
                    booking_status = 85;
                    break;
            }
            return booking_status;
        }

        public byte getNewbookingProductStatus(byte oldStatus)
        {
            byte booking_status = 0;
            switch (oldStatus)
            {
                case 1:
                    booking_status = 10;
                    break;
                case 2:
                    booking_status = 11;
                    break;
                case 3:
                    booking_status = 12;
                    break;
                case 4:
                    booking_status = 13;
                    break;
                case 5:
                    booking_status = 15;
                    break;
            }
            return booking_status;
        }
       
    }
}