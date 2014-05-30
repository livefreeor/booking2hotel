using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Hotels2thailand.Production;
using Hotels2thailand.Staffs;
/// <summary>
/// Summary description for Product
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public partial class ProductBookingEngine : Hotels2BaseClass
    {
        public int ProductID { get; set; }
        public byte BookingTypeID { get; set; }
        public byte GateWayId { get; set; }
        public string MerchatID { get; set; }
        public string TerminalID { get; set; }
        public string Folder { get; set; }
        public byte CurrencyID { get; set; }
        public string UrlReturn { get; set; }
        public string URLUpdate { get; set; }
        public string URLSiteRedirect { get; set; }
        

        public string Email
        {
            get { return this.EmailContactMail.Split(';')[0]; }
           
        }


        public string EmailPass { get; set; }
        public string WebsiteName { get; set; }
        public byte SalesID { get; set; }
        public byte ManageId { get; set; }
        public bool IsVat_Show { get; set; }
        public string EmailContactMail { get; set; }
        public bool IsMailNotice { get; set; }
        public byte DuePayment { get; set; }

        public byte ComNum { get; set; }
        public byte ComType { get; set; }
        public DateTime ComStart { get; set; }

        public bool Is_B2b { get; set; }

        public string B2bMap { get; set; }

        public byte B2bCat { get; set; }

        public string  AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string ProfileID { get; set; }

        public ProductBookingEngine() { }

        public IDictionary<byte, string> getdicBookingPaymentType()
        {
            IDictionary<byte, string> idcPaymentType = new Dictionary<byte, string>();

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT booking_type_id,title FROM tbl_product_booking_type ORDER BY title" , cn);
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                while(reader.Read())
                {
                    idcPaymentType.Add((byte)reader[0], reader[1].ToString());
                }
            }

            return idcPaymentType;
        }

        public IDictionary<byte, string> getdicCurreny()
        {
            IDictionary<byte, string> idcCurrency = new Dictionary<byte, string>();

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT currency_id, title + '(' + code + ')' FROM tbl_currency ORDER BY title", cn);
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                while (reader.Read())
                {
                    idcCurrency.Add((byte)reader[0], reader[1].ToString());
                }
            }

            return idcCurrency;
        }


        public IDictionary<byte, string> getdicSales()
        {
            IDictionary<byte, string> idcCurrency = new Dictionary<byte, string>();

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT sale_id,sale_name FROM tbl_product_sales", cn);
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                while (reader.Read())
                {
                    idcCurrency.Add((byte)reader[0], reader[1].ToString());
                }
            }

            return idcCurrency;
        }


        public ProductBookingEngine GetProductbookingEngine(int intProductId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_booking_engine WHERE product_id=@product_id",cn);
                cmd.Parameters.AddWithValue("@product_id", intProductId);
                cn.Open();

                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    return (ProductBookingEngine)MappingObjectFromDataReader(reader);
                }
                else
                {
                    return null;
                }
                
            }
        }
        public bool UpdatePaymentdetail(int intProductId, byte bytDuePayment, byte bytComNum, byte bytComtype, DateTime dDateComstart)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_booking_engine SET due_payment=@due_payment,monthly_commission_num=@monthly_commission_num,monthly_commission_type=@monthly_commission_type,commission_start=@commission_start WHERE product_id=@product_id", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@due_payment", SqlDbType.TinyInt).Value = bytDuePayment;
                cmd.Parameters.Add("@monthly_commission_num", SqlDbType.TinyInt).Value = bytComNum;
                cmd.Parameters.Add("@monthly_commission_type", SqlDbType.TinyInt).Value = bytComtype;
                cmd.Parameters.Add("@commission_start", SqlDbType.SmallDateTime).Value = dDateComstart;
                cn.Open();

                return (ExecuteNonQuery(cmd) == 1);
            }
        }

        public bool UpdateDuepayment(int intProductId, byte bytDuePayment)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_booking_engine SET due_payment=@due_payment WHERE product_id=@product_id", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@due_payment", SqlDbType.TinyInt).Value = bytDuePayment;
                cn.Open();

                return (ExecuteNonQuery(cmd) == 1);
            }
        }

        public int InsertOrUpdateProduct(string strHotelTitle, string strHotelCode, string strHotelAddress, string strComment, short shrDetinationId, byte bytstatus, bool bolStatus, bool ExtranetActive, string strPhone, string strLat, string strLong, string strEmail)
        {
            int ret = 0;

            int intProductId = 0;
            if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["pid"]))
            {
                using (SqlConnection cn = new SqlConnection(this.ConnectionString))
                {
                    //1 Insert Supplier Auto
                    SqlCommand cmd = new SqlCommand("INSERT INTO tbl_supplier (title,address) VALUES(@title,@address); SET @supplier_id = SCOPE_IDENTITY();", cn);
                    cmd.Parameters.AddWithValue("@title", strHotelTitle);
                    cmd.Parameters.AddWithValue("@address", strHotelAddress);
                    
                    cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Direction = ParameterDirection.Output;

                    cn.Open();

                    ret =  ExecuteNonQuery(cmd);
                    int SupplierId = (short)cmd.Parameters["@supplier_id"].Value;

                    SqlCommand cmdProduct = new SqlCommand("INSERT INTO tbl_product (title,product_code,supplier_price,destination_id,comment,date_submit,product_phone,coor_lat,coor_long,email) VALUES(@title,@product_code,@supplier_price,@destination_id,@comment,@date_submit,@product_phone,@coor_lat,@coor_long,@email); SET @product_id = SCOPE_IDENTITY();", cn);
                    cmdProduct.Parameters.AddWithValue("@title", strHotelTitle);
                    cmdProduct.Parameters.AddWithValue("@product_code", strHotelCode);
                    cmdProduct.Parameters.AddWithValue("@supplier_price", SupplierId);
                    cmdProduct.Parameters.AddWithValue("@destination_id", shrDetinationId);
                    cmdProduct.Parameters.AddWithValue("@product_phone", strPhone);
                    cmdProduct.Parameters.AddWithValue("@coor_lat", strLat);
                    cmdProduct.Parameters.AddWithValue("@coor_long", strLong);

                    cmdProduct.Parameters.AddWithValue("@comment", strComment);
                    cmdProduct.Parameters.AddWithValue("@email", strEmail);
                    cmdProduct.Parameters.AddWithValue("@date_submit", DateTime.Now.Hotels2ThaiDateTime());
                    cmdProduct.Parameters.Add("@product_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                    ret = ExecuteNonQuery(cmdProduct);

                    intProductId = (int)cmdProduct.Parameters["@product_id"].Value;


                    SqlCommand cmdsup = new SqlCommand("INSERT INTO tbl_product_supplier (supplier_id,product_id,status) VALUES (@supplier_id,@product_id,1)", cn);
                    cmdsup.Parameters.AddWithValue("@supplier_id", SupplierId);
                    cmdsup.Parameters.AddWithValue("@product_id", intProductId);
                    ret = ExecuteNonQuery(cmdsup);

                    string strFilemain = Hotels2FileName.Hotels2FilenameGenerate(strHotelTitle);
                    SqlCommand cmdProductContent = new SqlCommand("INSERT INTO tbl_product_content (product_id,lang_id,address,title,file_name_main,file_name_information,file_name_review,file_name_photo,file_name_why,file_name_pdf,file_name_contact)VALUES(@product_id,@lang_id,@address,@title,@file_name_main,@file_name_information,@file_name_review,@file_name_photo,@file_name_why,@file_name_pdf,@file_name_contact)", cn);

                    cmdProductContent.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                    cmdProductContent.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = 1;
                    cmdProductContent.Parameters.Add("@title", SqlDbType.NVarChar).Value = strHotelTitle;

                    cmdProductContent.Parameters.Add("@address", SqlDbType.NVarChar).Value = strHotelAddress;
                    cmdProductContent.Parameters.Add("@file_name_main", SqlDbType.VarChar).Value = Hotels2FileName.FilenameManage(strFilemain, 1);
                    cmdProductContent.Parameters.Add("@file_name_information", SqlDbType.VarChar).Value = Hotels2FileName.FilenameManage(strFilemain, "information", 1);
                    cmdProductContent.Parameters.Add("@file_name_review", SqlDbType.VarChar).Value = Hotels2FileName.FilenameManage(strFilemain, "review", 1);
                    cmdProductContent.Parameters.Add("@file_name_photo", SqlDbType.VarChar).Value = Hotels2FileName.FilenameManage(strFilemain, "photo", 1);
                    cmdProductContent.Parameters.Add("@file_name_why", SqlDbType.VarChar).Value = Hotels2FileName.FilenameManage(strFilemain, "why", 1);
                    cmdProductContent.Parameters.Add("@file_name_pdf", SqlDbType.VarChar).Value = Hotels2FileName.FilenameManagePDF(strFilemain, 1);
                    cmdProductContent.Parameters.Add("@file_name_contact", SqlDbType.VarChar).Value = Hotels2FileName.FilenameManage(strFilemain, "contact", 1);

                    ret = ExecuteNonQuery(cmdProductContent);

                }
                
            }
            else
            {
               
                intProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);

                short shrSupplierId = 0;
                using (SqlConnection cn = new SqlConnection(this.ConnectionString))
                {
                    SqlCommand cmdProduct = new SqlCommand("SELECT supplier_price FROM tbl_product WHERE product_id=" + intProductId + "",cn);
                    cn.Open();

                    IDataReader reader = ExecuteReader(cmdProduct);
                    reader.Read();
                    shrSupplierId = (short)reader["supplier_price"];
                    reader.Close();

                    SqlCommand cmd = new SqlCommand("UPDATE tbl_supplier SET title=@title , address=@address WHERE supplier_id=@supplier_id", cn);
                    cmd.Parameters.AddWithValue("@title", strHotelTitle);
                    cmd.Parameters.AddWithValue("@address", strHotelAddress);
                    cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                    ret = ExecuteNonQuery(cmd);


                    SqlCommand cmdupdateProduct = new SqlCommand("UPDATE tbl_product SET title=@title, product_code=@product_code, destination_id=@destination_id, status_id=@status_id,status=@status,extranet_active=@extranet_active,comment=@comment,product_phone=@product_phone, coor_lat=@coor_lat,coor_long=@coor_long, email=@email WHERE product_id=@product_id", cn);
                    cmdupdateProduct.Parameters.AddWithValue("@title", strHotelTitle);
                    cmdupdateProduct.Parameters.AddWithValue("@product_code", strHotelCode);
                    cmdupdateProduct.Parameters.AddWithValue("@status_id", bytstatus);
                    cmdupdateProduct.Parameters.AddWithValue("@status", bolStatus);
                    cmdupdateProduct.Parameters.AddWithValue("@extranet_active", ExtranetActive);


                    cmdupdateProduct.Parameters.Add("@destination_id", SqlDbType.SmallInt).Value = shrDetinationId;
                    cmdupdateProduct.Parameters.AddWithValue("@comment", strComment);
                    cmdupdateProduct.Parameters.AddWithValue("@email", strEmail);

                    cmdupdateProduct.Parameters.AddWithValue("@product_phone", strPhone);
                    cmdupdateProduct.Parameters.AddWithValue("@coor_lat", strLat);
                    cmdupdateProduct.Parameters.AddWithValue("@coor_long", strLong);
                    cmdupdateProduct.Parameters.AddWithValue("@product_id", intProductId);
                    ret = ExecuteNonQuery(cmdupdateProduct);


                    SqlCommand cmdUpdateContent = new SqlCommand("UPDATE tbl_product_content SET title=@title , address=@address WHERE product_id = @product_id AND lang_id=1",cn);
                    cmdUpdateContent.Parameters.AddWithValue("@title", strHotelTitle);
                    cmdUpdateContent.Parameters.AddWithValue("@address", strHotelAddress);
                    cmdUpdateContent.Parameters.AddWithValue("@product_id", intProductId);
                    ret = ExecuteNonQuery(cmdUpdateContent);
                }
                
            }

            return intProductId;
        }


        public int InsertOrUpdateProductEngine(int intProductId, byte bytBookingtypeid, byte bytGatewayId, string strMerchatId, string strTerminal, string strFolder, byte bytCurrency, string strUrlreturn, string strUrlupdate, string strUrlSiteRedirect, string strEmail, string strEmailPass, string strSiteName, byte bytSaleId, byte bytManageId, bool bolIsvat, string strEmailContact, bool Ismailnotice, bool Isb2b,string strB2bMap,byte bytB2bCat, string steAccessKey, string SecretKey, string ProfileId)
        {
            
                int ret = 0;

                
               

                if (this.GetProductbookingEngine(intProductId) == null)
                {
                    using (SqlConnection cn = new SqlConnection(this.ConnectionString))
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_booking_engine (product_id,booking_type_id,gateway_id,merchant_id,teminal_id,folder,currency_id,url_return,url_update,url_site_redirect,email_user,email_user_pass,web_site_name,sale_id,manage_id,is_vat,email_contact,is_mail_notice,is_b2b,b2b_map,cat_id,access_key,secret_key,profile_id) VALUES(@product_id,@booking_type_id,@gateway_id,@merchant_id,@teminal_id,@folder,@currency_id,@url_return,@url_update,@url_site_redirect,@email_user,@email_user_pass,@web_site_name,@sale_id,@manage_id,@is_vat,@email_contact,@is_mail_notice,@is_b2b,@b2b_map,@cat_id,@access_key,@secret_key,@profile_id)", cn);
                        cmd.Parameters.AddWithValue("@product_id", intProductId);
                        cmd.Parameters.AddWithValue("@booking_type_id", bytBookingtypeid);
                        cmd.Parameters.AddWithValue("@gateway_id", bytGatewayId);
                        cmd.Parameters.AddWithValue("@merchant_id", strMerchatId);
                        cmd.Parameters.AddWithValue("@teminal_id", strTerminal);
                        cmd.Parameters.AddWithValue("@folder", strFolder);
                        cmd.Parameters.AddWithValue("@currency_id", bytCurrency);
                        cmd.Parameters.AddWithValue("@url_return", strUrlreturn);
                        cmd.Parameters.AddWithValue("@url_update", strUrlupdate);
                        cmd.Parameters.AddWithValue("@url_site_redirect", strUrlSiteRedirect);
                        cmd.Parameters.AddWithValue("@email_user", strEmail);
                        cmd.Parameters.AddWithValue("@email_user_pass", strEmailPass);
                        cmd.Parameters.AddWithValue("@web_site_name", strSiteName);
                        cmd.Parameters.AddWithValue("@sale_id", bytSaleId);
                        cmd.Parameters.AddWithValue("@manage_id", bytManageId);
                        cmd.Parameters.AddWithValue("@is_vat", bolIsvat);
                        cmd.Parameters.AddWithValue("@email_contact", strEmailContact);
                        cmd.Parameters.AddWithValue("@is_mail_notice", Ismailnotice);
                        cmd.Parameters.AddWithValue("@is_b2b", Isb2b);
                        cmd.Parameters.AddWithValue("@b2b_map", strB2bMap);
                        cmd.Parameters.AddWithValue("@cat_id", bytB2bCat);
                        cmd.Parameters.AddWithValue("@access_key", steAccessKey);
                        cmd.Parameters.AddWithValue("@secret_key", SecretKey);
                        cmd.Parameters.AddWithValue("@profile_id", ProfileId);
                        cn.Open();
                       ret = ExecuteNonQuery(cmd);
                    }
                }
                else
                {
                    
                    using (SqlConnection cn = new SqlConnection(this.ConnectionString))
                    {

                        SqlCommand cmd = new SqlCommand("UPDATE tbl_product_booking_engine  SET product_id=@product_id,booking_type_id=@booking_type_id,gateway_id=@gateway_id,merchant_id=@merchant_id,teminal_id=@teminal_id,folder=@folder,currency_id=@currency_id,url_return=@url_return,url_update=@url_update,url_site_redirect=@url_site_redirect,email_user=@email_user,email_user_pass=@email_user_pass,web_site_name=@web_site_name,sale_id=@sale_id ,manage_id=@manage_id , is_vat = @is_vat, email_contact=@email_contact, is_mail_notice=@is_mail_notice, is_b2b=@is_b2b ,b2b_map=@b2b_map,cat_id=@cat_id,access_key=@access_key , secret_key=@secret_key, profile_id=@profile_id WHERE product_id =@product_id", cn);
                        cmd.Parameters.AddWithValue("@product_id", intProductId);
                        cmd.Parameters.AddWithValue("@booking_type_id", bytBookingtypeid);
                        cmd.Parameters.AddWithValue("@gateway_id", bytGatewayId);
                        cmd.Parameters.AddWithValue("@merchant_id", strMerchatId);
                        cmd.Parameters.AddWithValue("@teminal_id", strTerminal);
                        cmd.Parameters.AddWithValue("@folder", strFolder);
                        cmd.Parameters.AddWithValue("@currency_id", bytCurrency);
                        cmd.Parameters.AddWithValue("@url_return", strUrlreturn);
                        cmd.Parameters.AddWithValue("@url_update", strUrlupdate);
                        cmd.Parameters.AddWithValue("@url_site_redirect", strUrlSiteRedirect);
                        cmd.Parameters.AddWithValue("@email_user", strEmail);
                        cmd.Parameters.AddWithValue("@email_user_pass", strEmailPass);
                        cmd.Parameters.AddWithValue("@web_site_name", strSiteName);
                        cmd.Parameters.AddWithValue("@sale_id", bytSaleId);
                        cmd.Parameters.AddWithValue("@manage_id", bytManageId);
                        cmd.Parameters.AddWithValue("@is_vat", bolIsvat);
                        cmd.Parameters.AddWithValue("@email_contact", strEmailContact);
                        cmd.Parameters.AddWithValue("@is_mail_notice", Ismailnotice);
                        cmd.Parameters.AddWithValue("@is_b2b", Isb2b);
                        cmd.Parameters.AddWithValue("@b2b_map", strB2bMap);
                        cmd.Parameters.AddWithValue("@cat_id", bytB2bCat);
                        cmd.Parameters.AddWithValue("@access_key", steAccessKey);
                        cmd.Parameters.AddWithValue("@secret_key", SecretKey);
                        cmd.Parameters.AddWithValue("@profile_id", ProfileId);
                        cn.Open();
                        ret = ExecuteNonQuery(cmd);

                    }
                }

                return ret;
        }

    }
}