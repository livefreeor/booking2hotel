using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Text;
using Hotels2thailand.Booking;
using System.Data;
using System.Data.SqlClient;
using Hotels2thailand.Staffs;

/// <summary>
/// Summary description for BookingActivity
/// </summary>
/// 
namespace Hotels2thailand.Booking
{
    public class Gateway : Hotels2BaseClass
    {
        public byte GateWayId { get; set; }
        public string Title { get; set; }
        public string MerchantId { get; set; }
        public string TerminalId { get; set; }
        public string UrlReturn { get; set; }
        public string UrlThankyou { get; set; }
        public byte TimeStart { get; set; }
        public byte TimeEnd { get; set; }
        public bool DayMon { get; set; }
        public bool DayTue { get; set; }
        public bool DayWed { get; set; }
        public bool DayThu { get; set; }
        public bool DayFri { get; set; }
        public bool DaySat { get; set; }
        public bool DaySun { get; set; }
        public bool GatWayActive { get; set; }
        public bool Status { get; set; }

        public Dictionary<byte, string> getGateWayList()
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                Dictionary<byte, string> dicGateWay = new Dictionary<byte, string>();
                SqlCommand cmd = new SqlCommand("SELECT gateway_id,title FROM tbl_gateway WHERE gateway_id IN (3,6)", cn);
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                while (reader.Read())
                {
                    dicGateWay.Add((byte)reader[0], reader[1].ToString());
                }
                return dicGateWay;
            }
        }

        public Dictionary<byte, string> getGateWayListProduct()
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                Dictionary<byte, string> dicGateWay = new Dictionary<byte, string>();
                SqlCommand cmd = new SqlCommand("SELECT gateway_id,title FROM tbl_gateway  WHERE status =1 ", cn);
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                while (reader.Read())
                {
                    dicGateWay.Add((byte)reader[0], reader[1].ToString());
                }
                return dicGateWay;
            }
        }

        public List<object> GetGateWayAllByStatus()
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_gateway WHERE status = 1 ORDER BY status DESC , gateway_active DESC", cn);
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }


        public bool UpdateGateWayStatus(byte bytGatWayId,  bool bolStatus)
        {
            int ret = 0;

            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_gateway", "status", "gateway_id", bytGatWayId);
            //============================================================================================================================
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_gateway SET  status=@status WHERE gateway_id=@gateway_id", cn);

                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = bolStatus;
                cmd.Parameters.Add("@gateway_id", SqlDbType.TinyInt).Value = bytGatWayId;
                cn.Open();
                ret = ExecuteNonQuery(cmd);


            }

            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Booking, StaffLogActionType.Update, StaffLogSection.NULL, null,
                "tbl_gateway", "status", arroldValue, "gateway_id", bytGatWayId);
            //==================================================================================================================== COMPLETED ========

            return (ret == 1);
        }
        public bool UpdateGateWayActive(byte bytGatWayId,  bool Active)
        {
            int ret = 0;

            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_gateway", "gateway_active", "gateway_id", bytGatWayId);
            //============================================================================================================================
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_gateway SET  gateway_active=@gateway_active WHERE gateway_id=@gateway_id", cn);
                
                cmd.Parameters.Add("@gateway_active", SqlDbType.Bit).Value = Active;
                
                cmd.Parameters.Add("@gateway_id", SqlDbType.TinyInt).Value = bytGatWayId;
                cn.Open();
                ret = ExecuteNonQuery(cmd);


            }

            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Booking, StaffLogActionType.Update, StaffLogSection.NULL, null,
                "tbl_gateway", "gateway_active", arroldValue, "gateway_id", bytGatWayId);
            //==================================================================================================================== COMPLETED ========

            return (ret == 1);
        }

        public bool UpdateGateWayByGateWayId(byte bytGatWayId, byte bytTimeStart, byte bytTimeEnd, bool bolDayMon, bool bolDayTue, bool bolDayWed,
            bool bolDayThu, bool bolDayFri, bool bolDaySat, bool bolDaySun)
        {
            int ret = 0;

             //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_gateway","time_start,time_end,day_mon,day_tue,day_wed,day_thu,day_fri,day_sat,day_sun","gateway_id",bytGatWayId);
            //============================================================================================================================
            using(SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_gateway SET time_start=@time_start, time_end=@time_end, day_mon=@day_mon, day_tue=@day_tue, day_wed=@day_wed, day_thu=@day_thu, day_fri=@day_fri, day_sat=@day_sat, day_sun=@day_sun WHERE gateway_id=@gateway_id",cn);
                cmd.Parameters.Add("@time_start",SqlDbType.TinyInt).Value = bytTimeStart;
                cmd.Parameters.Add("@time_end",SqlDbType.TinyInt).Value = bytTimeEnd;
                cmd.Parameters.Add("@day_mon", SqlDbType.Bit).Value = bolDayMon;
                cmd.Parameters.Add("@day_tue", SqlDbType.Bit).Value = bolDayTue;
                cmd.Parameters.Add("@day_wed", SqlDbType.Bit).Value = bolDayWed;
                cmd.Parameters.Add("@day_thu", SqlDbType.Bit).Value = bolDayThu;
                cmd.Parameters.Add("@day_fri", SqlDbType.Bit).Value = bolDayFri;
                cmd.Parameters.Add("@day_sat", SqlDbType.Bit).Value = bolDaySat;
                cmd.Parameters.Add("@day_sun", SqlDbType.Bit).Value = bolDaySun;
               
                cmd.Parameters.Add("@gateway_id",SqlDbType.TinyInt).Value = bytGatWayId;
                cn.Open();
                ret = ExecuteNonQuery(cmd);

               
            }

            //#Staff_Activity_Log================================================================================================ STEP 2 ============
                    StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Booking, StaffLogActionType.Update, StaffLogSection.NULL, null,
                        "tbl_gateway", "time_start,time_end,day_mon,day_tue,day_wed,day_thu,day_fri,day_sat,day_sun", arroldValue, "gateway_id", bytGatWayId);
            //==================================================================================================================== COMPLETED ========

                    return (ret == 1);
        }
    }
}
