using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Production;
using Hotels2thailand.Staffs;

using Hotels2thailand.ProductOption;

/// <summary>
/// Summary description for ProductContent
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public class ProductPolicyCancellation : Hotels2BaseClass
    {
        public int PolicyId { get; set; }
        public byte DayCancel { get; set; }
        public byte HotelChargePer { get; set; }
        public byte BHTChargePer { get; set; }
        public byte HotelChargeRoom { get; set; }
        public byte BHTChargeRoom { get; set; }
        

        //=================== PRODUCT POLICY CANCEL ===========================

        public static int InsertPolicycancel(int intPolicyId, byte bytDayCancel, byte bytCPercenthotel, byte bytCpercentbht, byte bytCRoomHotel, byte bytCRoombht)
        {
            ProductPolicyCancellation cPCancel = new ProductPolicyCancellation();
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(cPCancel.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_policy_cancel(policy_id,day_cancel,charge_percent_hotel,charge_percent_bht,charge_room_hotel,charge_room_bht)VALUES(@policy_id,@day_cancel,@charge_percent_hotel,@charge_percent_bht,@charge_room_hotel,@charge_room_bht)", cn);
                cmd.Parameters.Add("@policy_id", SqlDbType.Int).Value = intPolicyId;
                cmd.Parameters.Add("@day_cancel", SqlDbType.TinyInt).Value = bytDayCancel;
                cmd.Parameters.Add("@charge_percent_hotel", SqlDbType.TinyInt).Value = bytCPercenthotel;
                cmd.Parameters.Add("@charge_percent_bht", SqlDbType.TinyInt).Value = bytCpercentbht;
                cmd.Parameters.Add("@charge_room_hotel", SqlDbType.TinyInt).Value = bytCRoomHotel;
                cmd.Parameters.Add("@charge_room_bht", SqlDbType.TinyInt).Value = bytCRoombht;
               

                //cmd.Parameters.Add("@cancel_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                //cPCancel.ExecuteNonQuery(cmd);
                //int ret = (int)cmd.Parameters["@cancel_id"].Value;
                ret = cPCancel.ExecuteNonQuery(cmd);

             }

            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_policy, StaffLogActionType.Insert, StaffLogSection.Product,
                ProductId, "tbl_product_policy_cancel", "policy_id,day_cancel,charge_percent_hotel,charge_percent_bht,charge_room_hotel,charge_room_bht",
                "policy_id,day_cancel", intPolicyId, bytDayCancel);
            //========================================================================================================================================================
            return ret;
            
        }

        public static bool DeletePolicyCancel(int intPolicyId, byte bytdayCancel)
        {
            ProductPolicyCancellation cPCancel = new ProductPolicyCancellation();
            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);


            IList<object[]> arroldValue = null;


            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            arroldValue = StaffActivity.ActionDeleteMethodStaff_log_FirstStep("tbl_product_policy_cancel", "policy_id,day_cancel", intPolicyId, bytdayCancel);
            //============================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(cPCancel.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM tbl_product_policy_cancel WHERE policy_id =@policy_id AND day_cancel=@day_cancel", cn);
                cmd.Parameters.Add("@policy_id", SqlDbType.Int).Value = intPolicyId;
                cmd.Parameters.Add("@day_cancel", SqlDbType.TinyInt).Value = bytdayCancel;
                cn.Open();
                ret = cPCancel.ExecuteNonQuery(cmd);
                

            }

            //#Staff_Activity_Log================================================================================================ STEP 2 ==
            StaffActivity.ActionDeleteMethodStaff_log_LastStep(StaffLogModule.Product_policy, StaffLogActionType.Delete, StaffLogSection.Product, ProductId,
                "tbl_product_policy_cancel", arroldValue, "policy_id,day_cancel", intPolicyId, bytdayCancel);
            //============================================================================================================================
            return (ret == 1);
        }

        public static List<object> GetPolicyCancelByPolicyId(int intPolicyId)
        {
            ProductPolicyCancellation cPCancel = new ProductPolicyCancellation();
            using (SqlConnection cn = new SqlConnection(cPCancel.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT policy_id,day_cancel,charge_percent_hotel,charge_percent_bht,charge_room_hotel,charge_room_bht FROM tbl_product_policy_cancel WHERE policy_id=@policy_id", cn);
                cmd.Parameters.Add("@policy_id", SqlDbType.Int).Value = intPolicyId;
                cn.Open();
                return cPCancel.MappingObjectCollectionFromDataReader(cPCancel.ExecuteReader(cmd));
            }
           
        }
    }
}