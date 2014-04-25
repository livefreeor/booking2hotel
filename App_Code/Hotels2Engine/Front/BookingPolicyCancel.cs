using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Hotels2thailand.DataAccess;

/// <summary>
/// Summary description for BookingPolicy
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class BookingPolicyCancel:Hotels2BaseClass
    {
        public int PolicyID { get; set; }
        public byte DayCancel { get; set; }
        public byte? HotelChargePercent { get; set; }
        public byte? BHTChargePercent { get; set; }
        public byte? HotelChargeRoomNight { get; set; }
        public byte? BHTChargeRoomNight { get; set; }

        public BookingPolicyCancel()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public List<object> GetPolicyCancel(int conditionID,DateTime dateStart)
        { 
            
            string sqlCommand = " select ppc.policy_id,day_cancel,charge_percent_hotel,charge_percent_bht,charge_room_hotel,charge_room_bht";
            sqlCommand = sqlCommand + " from tbl_product_policy pp,tbl_product_policy_cancel ppc,tbl_product_option_condition_policy pocp";
            sqlCommand = sqlCommand + " where pp.policy_id=ppc.policy_id and pp.policy_id=pocp.policy_id and pocp.condition_id="+conditionID+" and pp.type_id=1 and " + dateStart.Hotels2DateToSQlString() + "  between pp.date_start and pp.date_end and pp.status=1";
            sqlCommand = sqlCommand + " order by pocp.condition_id asc,ppc.day_cancel asc";
            using(SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlCommand,cn);
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }

            //HttpContext.Current.Response.Write("<hr>"+sqlCommand);
            //HttpContext.Current.Response.Flush();
            //DataConnect objConn = new DataConnect();
            //SqlDataReader reader = objConn.GetDataReader(sqlCommand);

            //MappingObjectCollectionFromDataReader


            //List<BookingPolicyCancel> cancels = new List<BookingPolicyCancel>();

            //while (reader.Read())
            //{
            //    cancels.Add(new BookingPolicyCancel { 
            //    PolicyID=(int)reader["policy_id"],
            //    DayCancel=(byte)reader["day_cancel"],

            //    HotelChargePercent = (byte)reader["charge_percent_hotel"],
            //    BHTChargePercent = (byte)reader["charge_percent_bht"],
            //    HotelChargeRoomNight = (byte)reader["charge_room_hotel"],
            //    BHTChargeRoomNight = (byte)reader["charge_room_bht"]
            //    });
            //}
            //objConn.Close();
            //return cancels;
        }
    }
}