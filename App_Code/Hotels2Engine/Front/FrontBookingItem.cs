using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Booking;
using System.Data.SqlClient;
using System.Data;
/// <summary>
/// Summary description for Booking
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class FrontBookingItem:Hotels2BaseClass
    {

        private LinqBookingDataContext dcBookingItem = new LinqBookingDataContext();
        public int BookingItemID { get; set; }
        public int BookingID { get; set; }
        public int BookingProductID { get; set; }
        
        
        public int OptionID { get; set; }
        public string OptionTitle { get; set; }
        public string OptionDetail { get; set; }
        public int ConditionID { get; set; }
        public string ConditionTitle { get; set; }
        public string ConditionDetail { get; set; }
        public byte Unit { get; set; }
        public decimal PriceSupplier { get; set; }
        public decimal Price { get; set; }
        public decimal PriceDisplay { get; set; }
        public int? PromotionID { get; set; }
        public string PromotionTitle { get; set; }
        public string PromotionDetail { get; set; }
        public string Detail { get; set; }
        public bool Status { get; set; }
        public byte Breakfast { get; set; }
        public byte NumChildren { get; set; }
        public byte NumAdult { get; set; }
        public bool? ConditionIsAdult { get; set; }
        public string MemberDetail { get; set; }

        public FrontBookingItem()
        {
            this.PromotionID = null;
            this.PromotionTitle = null;
            this.PromotionDetail = null;
        }

        public int Insert(FrontBookingItem data)
        {
            
            //New
            using(SqlConnection cn=new SqlConnection(this.ConnectionString))
            {

                string strCommand="insert into tbl_booking_item(booking_id,booking_product_id,option_id,option_title,option_detail,condition_id,condition_title,condition_detail,unit,price_supplier,price,price_display,promotion_id,promotion_title,promotion_detail,detail,status,breakfast,num_children,num_adult,condition_isadult,member_detail)";
                strCommand = strCommand + " values(@booking_id,@booking_product_id,@option_id,@option_title,@option_detail,@condition_id,@condition_title,@condition_detail,@unit,@price_supplier,@price,@price_display,@promotion_id,@promotion_title,@promotion_detail,@detail,@status,@breakfast,@num_children,@num_adult,@condition_isadult,@member_detail);SET @booking_item_id = SCOPE_IDENTITY()";
                SqlCommand cmd = new SqlCommand(strCommand, cn);
                cmd.Parameters.AddWithValue("@booking_id", data.BookingID);
                cmd.Parameters.AddWithValue("@booking_product_id", data.BookingProductID);
                cmd.Parameters.AddWithValue("@option_id", data.OptionID);
                cmd.Parameters.AddWithValue("@option_title", data.OptionTitle);
                if (data.OptionDetail != null)
                {
                    cmd.Parameters.AddWithValue("@option_detail", data.OptionDetail);
                }
                else {
                    cmd.Parameters.AddWithValue("@option_detail", DBNull.Value);
                }
                
                cmd.Parameters.AddWithValue("@condition_id", data.ConditionID);
                cmd.Parameters.AddWithValue("@condition_title", data.ConditionTitle);
                cmd.Parameters.AddWithValue("@condition_detail", data.ConditionDetail);
                cmd.Parameters.AddWithValue("@unit", data.Unit);
                cmd.Parameters.AddWithValue("@price_supplier", data.PriceSupplier);
                cmd.Parameters.AddWithValue("@price", data.Price);
                cmd.Parameters.AddWithValue("@price_display", data.PriceDisplay);
                if(data.PromotionID!=null)
                {
                    cmd.Parameters.AddWithValue("@promotion_id",data.PromotionID);
                    cmd.Parameters.AddWithValue("@promotion_title",data.PromotionTitle);
                    cmd.Parameters.AddWithValue("@promotion_detail", data.PromotionDetail);
                }else{
                    cmd.Parameters.AddWithValue("@promotion_id",DBNull.Value);
                    cmd.Parameters.AddWithValue("@promotion_title",DBNull.Value);
                    cmd.Parameters.AddWithValue("@promotion_detail", DBNull.Value);
                }

                if (!data.ConditionIsAdult.HasValue)
                {
                    cmd.Parameters.AddWithValue("@condition_isadult", data.ConditionIsAdult);
                }
                else {
                    cmd.Parameters.AddWithValue("@condition_isadult", DBNull.Value);
                }
                cmd.Parameters.AddWithValue("@detail", String.IsNullOrEmpty(data.Detail)? string.Empty:data.Detail);
                cmd.Parameters.AddWithValue("@member_detail", String.IsNullOrEmpty(data.MemberDetail) ? string.Empty : data.MemberDetail);
                cmd.Parameters.AddWithValue("@status", data.Status);
                cmd.Parameters.AddWithValue("@breakfast", data.Breakfast);
                cmd.Parameters.AddWithValue("@num_children", data.NumChildren);
                cmd.Parameters.AddWithValue("@num_adult", data.NumAdult);
                cmd.Parameters.AddWithValue("@booking_item_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                cmd.ExecuteNonQuery();
                int ret = (int)cmd.Parameters["@booking_item_id"].Value;
                return ret;
                }
            //New Insert


            //tbl_booking_item bookingItem = new tbl_booking_item { 
            //    //booking_item_id=data.BookingItemID,
                
            //    booking_id=data.BookingID,
            //    booking_product_id=data.BookingProductID,
            //    option_id = data.OptionID,
            //    condition_id = data.ConditionID,
            //    condition_title = data.ConditionTitle,
            //    condition_detail = data.ConditionDetail,
            //    unit = data.Unit,
            //    price_supplier = data.PriceSupplier,
            //    price = data.Price,
            //    price_display=data.PriceDisplay,
            //    promotion_id=data.PromotionID,
            //    promotion_title=data.PromotionTitle,
            //    promotion_detail=data.PromotionDetail,
            //    detail = data.Detail,
            //    status=data.Status
                
                
            //};
            
            //dcBookingItem.tbl_booking_items.InsertOnSubmit(bookingItem);
            //dcBookingItem.SubmitChanges();
            //return bookingItem.booking_item_id;
        }
        public int Insert2(FrontBookingItem data)
        {
            string sqlCommand = "Insert into tbl_booking_item(booking_id,booking_product_id,option_id,condition_id,condition_title,condition_detail,unit,price_supplier,price,price_display,promotion_id,promotion_title,promotion_detail,status) ";
            sqlCommand = sqlCommand + "Values(@booking_id,@booking_product_id,@option_id,@condition_id,@condition_title,@condition_detail,@unit,@price_supplier,@price,@price_display,@promotion_id,@promotion_title,@promotion_detail,@status); SET @Product_id = SCOPE_IDENTITY()";
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlCommand.ToString(), cn);
                //cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytCatId;
                cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = data.BookingID;
                cmd.Parameters.Add("@booking_product_id", SqlDbType.Int).Value = data.BookingProductID;
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = data.BookingProductID;
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = data.ConditionID;
                cmd.Parameters.Add("@condition_title", SqlDbType.NVarChar).Value = data.ConditionTitle;
                cmd.Parameters.Add("@condition_detail", SqlDbType.Text).Value = data.ConditionDetail;
                cmd.Parameters.Add("@unit", SqlDbType.TinyInt).Value = data.Unit;
                cmd.Parameters.Add("@price_supplier", SqlDbType.Money).Value = data.PriceSupplier;
                cmd.Parameters.Add("@price", SqlDbType.Money).Value = data.Price;
                cmd.Parameters.Add("@price_display", SqlDbType.Money).Value = data.PriceDisplay;
                cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = data.PromotionID;
                cmd.Parameters.Add("@promotion_title", SqlDbType.NVarChar).Value = data.PromotionTitle;
                cmd.Parameters.Add("@promotion_detail", SqlDbType.Text).Value = data.PromotionDetail;
                //cmd.Parameters.Add("@detail", SqlDbType.NVarChar).Value = data.Detail;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = data.Status;
                cmd.Parameters.Add("@Product_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();

                ExecuteNonQuery(cmd);
                int ret = (int)cmd.Parameters["@Product_id"].Value;
                return ret;
            } 
        }
    }
}