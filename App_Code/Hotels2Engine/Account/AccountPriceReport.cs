using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.Staffs;
using Hotels2thailand;

/// <summary>
/// Summary description for AccountSettleReport
/// </summary>
/// 
namespace Hotels2thailand.Account
{
    public class AccountPriceReport : Hotels2BaseClass
    {

        public int BookingId { get; set; }
        public int BookingHotelId { get; set; }
        public string BookingName { get; set; }
        public DateTime  DateCheckIn { get; set; }
        public DateTime DateCheckOut { get; set; }
        public short StatusProcess { get; set; }
        public string StatusProcessTitle { get; set; }
        
        public bool Status { get; set; }
        public decimal Price { get; set; }


        public AccountPriceReport()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public IList<object> GetReport(int intProductId,DateTime dDateMonthStart, DateTime dDateMonthEnd, byte bytOrderBy)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string strOrderBy = "";
                switch (bytOrderBy)
                {
                    case 0:
                        strOrderBy = "ORDER BY b.status_id ASC";
                        break;
                    case 1:
                        strOrderBy = "ORDER BY b.status_id DESC";
                        break;
                    case 2:
                        strOrderBy = "ORDER BY b.booking_id ASC";
                        break;
                    case 3:
                        strOrderBy = "ORDER BY b.booking_id DESC";
                        break;
                    case 4:
                        strOrderBy = "ORDER BY bp.date_time_check_in ASC";
                        break;
                    case 5:
                        strOrderBy = "ORDER BY bp.date_time_check_in DESC";
                        break;
                    case 6:
                        strOrderBy = "ORDER BY bp.date_time_check_out ASC";
                        break;
                    case 7:
                        strOrderBy = "ORDER BY bp.date_time_check_out DESC";
                        break;
                    case 8:
                        strOrderBy = "ORDER BY b.status DESC";
                        break;
                    case 9:
                        strOrderBy = "ORDER BY b.status ASC";
                        break;
                    case 10:
                        strOrderBy = "ORDER BY Price ASC";
                        break;
                    case 11:
                        strOrderBy = "ORDER BY Price DESC";
                        break;
                }

                SqlCommand cmd = new SqlCommand("SELECT b.booking_id, bh.booking_hotel_id, b.name_full, bp.date_time_check_in, bp.date_time_check_out, b.status_id, bs.title, b.status,(SELECT SUM(bi.price) FROM tbl_booking_item bi WHERE bi.booking_product_id = bp.booking_product_id AND bi.status = 1) AS Price FROM tbl_booking b, tbl_booking_product bp , tbl_product p, tbl_booking_hotels bh, tbl_status bs WHERE bp.product_id=p.product_id AND p.product_id = bh.product_id  AND bp.status= 1 AND b.status_id = bs.status_id AND bp.booking_id = bh.booking_id AND b.booking_id = bp.booking_id AND bp.product_id = @product_id AND (bp.date_time_check_out  BETWEEN @dateStart AND @dateEnd) " + strOrderBy, cn);

                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@dateStart", SqlDbType.SmallDateTime).Value = dDateMonthStart;
                cmd.Parameters.Add("@dateEnd", SqlDbType.SmallDateTime).Value = dDateMonthEnd;
                cn.Open();

                return  MappingObjectCollectionFromDataReader(ExecuteReader(cmd));

            }
        }



    }
}