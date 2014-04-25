using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Booking;
using Hotels2thailand.DataAccess;
using System.Data.SqlClient;
/// <summary>
/// Summary description for Booking
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class FrontBookingProduct:Hotels2BaseClass
    {
        private LinqBookingDataContext dcBookingProduct = new LinqBookingDataContext();

        public int BookingProductID { get; set; }
        public int BookingID { get; set; }
        public short SupplierID { get; set; }
        public int ProductID { get; set; }
        public short StatusID { get; set; }
        public byte NumAdult { get; set; }
        public byte NumChild { get; set; }
        public byte NumGolfer { get; set; }
        
        public Nullable<DateTime> DateCheckIn { get; set; }
        public Nullable<DateTime> DateCheckOut { get; set; }
        public Nullable<DateTime> ConfirmDateTime { get; set; }
        public string Detail { get; set; }
        public bool Status { get; set; }
        public DateTime PrepaidDate { get; set; }

        public FrontBookingProduct()
        {
            NumAdult = 0;
            NumChild = 0;
            NumGolfer = 0;
            
            DateCheckIn = null;
            DateCheckOut = null;
            ConfirmDateTime = null;
        }

        public void  GetBookingProductByID(int BookingProductID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = "select booking_product_id,booking_id,supplier_id,product_id,status_id,num_adult,num_child,num_golfer,date_time_check_in,date_time_check_out,date_time_check_in_confirm,detail,status,prepaid_date ";
                sqlCommand = sqlCommand + " from tbl_booking_product";
                sqlCommand = sqlCommand + " where booking_product_id=" + BookingProductID;
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                BookingProductID = (int)reader["booking_product_id"];
                BookingID = (int)reader["booking_id"];
                SupplierID = (short)reader["supplier_id"];
                ProductID = (int)reader["product_id"];
                StatusID = (short)reader["status_id"];
                NumAdult = (byte)reader["num_adult"];
                NumChild = (byte)reader["num_child"];
                NumGolfer = (byte)reader["num_golfer"];
                DateCheckIn = (reader["date_time_check_in"] == DBNull.Value ? null : (DateTime?)reader["date_time_check_in"]);
                DateCheckOut = (reader["date_time_check_out"] == DBNull.Value ? null : (DateTime?)reader["date_time_check_out"]);
                ConfirmDateTime = (reader["date_time_check_in_confirm"] == DBNull.Value ? null : (DateTime?)reader["date_time_check_in_confirm"]);
                Detail = reader["detail"].ToString();
                Status = (bool)reader["status"];
                PrepaidDate = (DateTime)reader["prepaid_date"];
            }
            

            //item.Detail = reader["detail"].ToString();
            //item.Status = (bool)reader["status"];
            //item.PrepaidDate = (DateTime)reader["prepaid_date"];
            //FrontBookingProduct item = new FrontBookingProduct();
            //item.BookingProductID=(int)reader["booking_product_id"];
            //item.BookingID=(int)reader["booking_id"];
            //item.SupplierID=(short)reader["supplier_id"];
            //item.ProductID=(int)reader["product_id"];
            //item.StatusID=(short)reader["status_id"];
            //item.NumAdult=(byte)reader["num_adult"];
            //item.NumChild = (byte)reader["num_child"];
            //item.NumGolfer = (byte)reader["num_golfer"];

            //if(reader["date_time_check_in"]!=null){
            //    item.DateCheckIn=(DateTime)reader["date_time_check_in"];
            //}

            //if (reader["date_time_check_out"]!=null)
            //{
            //    item.DateCheckOut = (DateTime)reader["date_time_check_out"];
            //}

            //if (!string.IsNullOrEmpty(reader["date_time_check_in_confirm"].ToString()))
            //{
            //    item.ConfirmDateTime = (DateTime)reader["date_time_check_in_confirm"];
            //}

            //HttpContext.Current.Response.Write("check-in"+reader["date_time_check_in"].ToString()+"<br>");
            //HttpContext.Current.Response.Write("check-out" + reader["date_time_check_out"].ToString() + "<br>");
            //HttpContext.Current.Response.Write("confirm check-in" + reader["date_time_check_in_confirm"].ToString() + "<br>");
            //HttpContext.Current.Response.End();
            
            
        }

        public int Insert(FrontBookingProduct data)
        {

            tbl_booking_product bookingProduct = new tbl_booking_product
            {
                //booking_product_id=data.BookingProductID,
                booking_id = data.BookingID,
                supplier_id = data.SupplierID,
                product_id = data.ProductID,
                status_id = data.StatusID,
                num_adult = data.NumAdult,
                num_child=data.NumChild,
                num_golfer=data.NumGolfer,
               
                date_time_check_in = data.DateCheckIn,
                date_time_check_out=data.DateCheckOut,

                date_time_check_in_confirm = data.ConfirmDateTime,
                detail=data.Detail,
                status=data.Status,
                prepaid_date=data.PrepaidDate
            };

            dcBookingProduct.tbl_booking_products.InsertOnSubmit(bookingProduct);
            dcBookingProduct.SubmitChanges();
            return bookingProduct.booking_product_id;
        }
    }
}