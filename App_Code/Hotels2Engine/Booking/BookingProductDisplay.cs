using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.DataAccess;
using System.Data.SqlClient;
using System.Data;
using Hotels2thailand.Production;
using Hotels2thailand.Staffs;

using Hotels2thailand.LinqProvider.Production;

/// <summary>
/// Summary description for BookingProductShow
/// </summary>
/// 
namespace Hotels2thailand.Booking
{
    public class BookingProductDisplay : Hotels2BaseClass
    {
        public int BookingProductId { get; set; }
        public int BookingId { get; set; }
        public byte BookingPaymentId { get; set; }
        public int ProductID { get; set; }
        public short SupplierId { get; set; }
        public string ProductCode { get; set; }
        public string ProductTitle { get; set; }
        public byte ProductCategory { get; set; }
        public byte BookingLang { get; set; }
        public string ProductAddress { get; set; }
        public string CountryTitle { get; set; }
        public string BookingName { get; set; }
        public Nullable<DateTime> DateTimeCheckIn { get; set; }
        public Nullable<DateTime> DateTimeCheckOut { get; set; }
        public Nullable<DateTime> DateTimeConfirmCheckIn { get; set; }
        public byte NumAdult { get; set; }
        public byte NumChild { get; set; }
        public byte NunGolf { get; set; }

        public bool Status { get; set; }
        public byte PrefixId { get; set; }
        public string Prefixtitle { get; set; }
        public string FlightNumArr { get; set; }
        public string FlightNumDep { get; set; }
        public DateTime? FlightTimeArr { get; set; }
        public DateTime? FlightTimeDep { get; set; }

        
        //public string SupplierPhone { get; set; }
        //public string SupplierFax { get; set; }
        //public string SupplierEmail { get; set; }
        public string ProductPhone { get; set; }
        public bool IsExtranet { get; set; }
        public Byte StatusExtranet { get; set; }
        public string  ProductEmail { get; set; }
        public int? BookingAgencyId { get; set; }
        public decimal TotalPriceSales { get; set; }
        public decimal TotalPriceSupplier { get; set; }

        private Hotels2thailand.Production.ProductBookingEngine _c_class_product_BookingEngine = null;
        public Hotels2thailand.Production.ProductBookingEngine cProductBookingEngine
        {
            get
            {
                if (_c_class_product_BookingEngine == null)
                {
                    Hotels2thailand.Production.ProductBookingEngine cProduct = new ProductBookingEngine();

                    _c_class_product_BookingEngine = cProduct.GetProductbookingEngine(this.ProductID);
                }
                return _c_class_product_BookingEngine;
            }

            set { _c_class_product_BookingEngine = value; }
        }

        public  int? getBookingIdByBookingProductId(int intBookingProductID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT booking_id FROM tbl_booking_product WHERE booking_product_id=@booking_product_id", cn);
                cmd.Parameters.Add("@booking_product_id", SqlDbType.Int).Value = intBookingProductID;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                if (reader.Read())
                    return (int)reader[0];
                else
                    return null;

            }
        }

        public short getTrackBookingProductStatusByBookingID(int intBookingId)
        {
            short ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 status_id FROM tbl_booking_product WHERE booking_id=@booking_id", cn);
                cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = intBookingId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                if (reader.Read())
                    ret = (short)reader[0];
                
            }
            return ret;
        }

        public int GetBookingHotelId(int intBookingId)
        {
            int intBookingHotelID = 0;
            StringBuilder query = new StringBuilder();
            query.Append("SELECT TOP 1  bh.booking_hotel_id  FROM tbl_booking_product bp, tbl_booking_hotels bh");
            query.Append(" WHERE bp.product_id = bh.product_id AND bp.booking_id = bh.booking_id AND bp.booking_id  = " + intBookingId + "");
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(),cn);
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);

                if(reader.Read())
                {
                    intBookingHotelID = (int)reader[0];
                }
            }

            return intBookingHotelID;
        }

        public BookingProductDisplay getBookingProductDisplayByBookingProductId(int intBookingProductID)
        {

            StringBuilder query = new StringBuilder();
            query.Append("SELECT bp.booking_product_id, b.booking_id, b.payment_type_id,p.product_id, bp.supplier_id, p.product_code, pc.title, p.cat_id, b.lang_id, pc.address, c.title,");
            query.Append("  b.name_full, bp.date_time_check_in, bp.date_time_check_out, bp.date_time_check_in_confirm, bp.num_adult,bp.num_child, bp.num_golfer, bp.status, pn.prefix_id,  pn.title, ");
            query.Append("  b.flight_arrival_number,b.flight_departure_number,b.flight_arrival_time,b.flight_departure_time, ");
            //query.Append(" ISNULL((SELECT TOP 1  '(' + scp.code_country + ')'+ scp.code_local + '-' + scp.phone_number FROM tbl_supplier_staff_contact sc, tbl_supplier_staff_contact_phone scp");
            //query.Append(" WHERE sc.staff_id = scp.staff_id AND sc.department_id = 3 AND scp.status = 1 AND sc.status = 1 AND  scp.cat_id = 1 AND sc.supplier_id = bp.supplier_id),'N/A') AS PhoneSupplier,");
            //query.Append(" ISNULL((SELECT TOP 1  '(' + scp.code_country + ')'+ scp.code_local + '-' + scp.phone_number FROM tbl_supplier_staff_contact sc, tbl_supplier_staff_contact_phone scp");
            //query.Append(" WHERE sc.staff_id = scp.staff_id AND sc.department_id = 3 AND scp.status = 1 AND sc.status = 1 AND  scp.cat_id = 3 AND sc.supplier_id = bp.supplier_id),'N/A') AS FaxSupplier,");

            //query.Append(" ISNULL((SELECT TOP 1  sce.email FROM tbl_supplier_staff_contact sc, tbl_supplier_staff_contact_email sce");
            //query.Append(" WHERE sc.staff_id = sce.staff_id AND sc.department_id = 3 AND sce.status = 1 AND sc.status = 1  AND sc.supplier_id = bp.supplier_id),'N/A') AS EmailSupplier,");

            query.Append("(p.product_phone) AS PhoneProduct, b.is_extranet, b.status_extranet_id,p.email,b.agency_id,");
            query.Append(" (SELECT SUM(bix.price) FROM tbl_booking_item bix WHERE bix.status = 1 AND bp.booking_product_id = bix.booking_product_id) As TotalPriceSale,");
            query.Append(" (SELECT SUM(bix.price_supplier) FROM tbl_booking_item bix WHERE bix.status = 1 AND bp.booking_product_id = bix.booking_product_id) As TotalPriceOwn");
            query.Append(" FROM tbl_product p, tbl_booking_product bp, tbl_booking b, tbl_product_content pc, tbl_country c, tbl_prefix_name pn");
            query.Append(" WHERE p.product_id = bp.product_id AND bp.booking_product_id = @booking_product_id AND c.country_id = b.country_id");
            query.Append(" AND pc.product_id = bp.product_id AND bp.booking_id = b.booking_id AND b.lang_id = pc.lang_id AND pn.prefix_id = b.prefix_id");
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(),cn);
                cmd.Parameters.Add("@booking_product_id", SqlDbType.Int).Value = intBookingProductID;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (BookingProductDisplay)MappingObjectFromDataReader(reader);
                else
                    return null;
                
            }
        }

        public bool UpdateBookingProductByBookingProductId(int intBookingId, int intBookingProductId, DateTime dDateChkIn, DateTime dDateChkOut, byte bytNumAdult, byte bytNumChild, byte bytNumGolf)
        {
            int ret = 0;
            ArrayList arrOld_Value = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_booking_product", "date_time_check_in,date_time_check_out,num_adult,num_child,num_golfer", "booking_product_id", intBookingProductId);

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_booking_product SET date_time_check_in=@date_time_check_in, date_time_check_out=@date_time_check_out, num_adult=@num_adult, num_child=@num_child, num_golfer=@num_golfer WHERE booking_product_id=@booking_product_id", cn);
                cmd.Parameters.Add("@booking_product_id", SqlDbType.Int).Value = intBookingProductId;
                cmd.Parameters.Add("@date_time_check_in", SqlDbType.SmallDateTime).Value = dDateChkIn;
                cmd.Parameters.Add("@date_time_check_out", SqlDbType.SmallDateTime).Value = dDateChkOut;
                cmd.Parameters.Add("@num_adult", SqlDbType.TinyInt).Value = bytNumAdult;
                cmd.Parameters.Add("@num_child", SqlDbType.TinyInt).Value = bytNumChild;
                cmd.Parameters.Add("@num_golfer", SqlDbType.TinyInt).Value = bytNumGolf;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
                
            }

            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Booking, StaffLogActionType.Update, StaffLogSection.Booking, intBookingId,
                "tbl_booking_product", "date_time_check_in,date_time_check_out,num_adult,num_child,num_golfer", arrOld_Value, "booking_product_id", intBookingProductId);
            return (ret == 1);
        }

        public bool UpdateBookingProductStatusByBookingProductId(int intBookingId, int intBookingProductId, bool bolStatus)
        {
            int ret = 0;
            ArrayList arrOld_Value = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_booking_product", "status", "booking_product_id", intBookingProductId);

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_booking_product SET status=@status WHERE booking_product_id=@booking_product_id", cn);
                cmd.Parameters.Add("@booking_product_id", SqlDbType.Int).Value = intBookingProductId;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = bolStatus;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
                
            }

            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Booking, StaffLogActionType.Update, StaffLogSection.Booking, intBookingId,
                "tbl_booking_product", "status", arrOld_Value, "booking_product_id", intBookingProductId);
            return (ret == 1);
        }

        public bool UpdateBookingProductConfirmCheckInTime(int intBookingProductId, DateTime dDAteTimeCheckInCOnfirm)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_booking_product SET date_time_check_in_confirm=@date_time_check_in_confirm WHERE booking_product_id=@booking_product_id", cn);
                cmd.Parameters.Add("@booking_product_id", SqlDbType.Int).Value = intBookingProductId;
                cmd.Parameters.Add("@date_time_check_in_confirm", SqlDbType.SmallDateTime).Value = dDAteTimeCheckInCOnfirm;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }

        public bool UpdateBookingProductConfirmCheckInTimeRollBack(int intBookingProductId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_booking_product SET date_time_check_in_confirm=@date_time_check_in_confirm WHERE booking_product_id=@booking_product_id", cn);
                cmd.Parameters.Add("@booking_product_id", SqlDbType.Int).Value = intBookingProductId;
                cmd.Parameters.AddWithValue("@date_time_check_in_confirm", DBNull.Value); 
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }
    }
}