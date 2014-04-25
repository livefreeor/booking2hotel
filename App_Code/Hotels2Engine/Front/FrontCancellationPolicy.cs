using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Hotels2thailand.DataAccess;

/// <summary>
/// Summary description for FrontCancellationPolicy
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class FrontCancellationPolicy:Hotels2BaseClass
    {
        public int PolicyID { get; set; }
        public int ConditionID { get; set; }
        public byte DayCancel { get; set; }
        public byte ChargePercentBHT { get; set; }
        public byte ChargeRoomBHT { get; set; }

        private int _productID;
        private DateTime _dateCheck;
        

        public FrontCancellationPolicy()
        {
           
        }

        public FrontCancellationPolicy(int ProductID,DateTime dateCheck)
        {
            _productID = ProductID;
            _dateCheck = dateCheck;
        }

        public List<FrontCancellationPolicy> LoadCancellationPolicyByCondition()
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = "select pp.policy_id,pocp.condition_id,ppc.day_cancel,ppc.charge_percent_bht,ppc.charge_room_bht ";
                sqlCommand = sqlCommand + " from tbl_product_policy pp,tbl_product_policy_cancel ppc,tbl_product_option_condition_policy pocp";
                sqlCommand = sqlCommand + " where pp.policy_id=ppc.policy_id and pp.policy_id=pocp.policy_id and pp.product_id =" + _productID + " and pp.type_id=1 and " + _dateCheck.Hotels2DateToSQlString() + " between pp.date_start and pp.date_end and pp.status=1";
                sqlCommand = sqlCommand + " order by pocp.condition_id asc,ppc.day_cancel asc";

                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                List<FrontCancellationPolicy> result = new List<FrontCancellationPolicy>();
                while (reader.Read())
                {
                    result.Add(new FrontCancellationPolicy
                    {
                        PolicyID = (int)reader["policy_id"],
                        ConditionID = (int)reader["condition_id"],
                        DayCancel = (byte)reader["day_cancel"],
                        ChargePercentBHT = (byte)reader["charge_percent_bht"],
                        ChargeRoomBHT = (byte)reader["charge_room_bht"]
                    });
                }
                return result;
            }
            
        }

        public string RenderCancellation(List<FrontCancellationPolicy> cancellationList,int PolicyID, int ConditionID,byte CategoryID,byte LangID)
        {
            string cancelDisplay = "";
            string[,] ResultProductType = new string[10, 3];
            int lastDayCancel = 0;

            List<FrontCancellationPolicy> cancalPolicy = new List<FrontCancellationPolicy>();

            //Filter Policy of condition
            foreach (FrontCancellationPolicy item in cancellationList)
            {
                if (item.PolicyID == PolicyID && item.ConditionID == ConditionID)
                {
                    cancalPolicy.Add(new FrontCancellationPolicy
                    {
                        PolicyID = item.PolicyID,
                        ConditionID = item.ConditionID,
                        DayCancel = item.DayCancel,
                        ChargePercentBHT = item.ChargePercentBHT,
                        ChargeRoomBHT = item.ChargeRoomBHT
                    });

                }

            }

            //----
            for (int countIndex = 0; countIndex < cancalPolicy.Count; countIndex++)
            {
                if(LangID==1)
                {
                    cancelDisplay = cancelDisplay + Hotels2String.CancellationGenerateWording(false, cancalPolicy[countIndex].DayCancel, cancalPolicy[countIndex].ChargePercentBHT, cancalPolicy[countIndex].ChargeRoomBHT, 0, 0)+"<br/>";
                }else{
                    cancelDisplay = cancelDisplay + Hotels2String.CancellationGenerateWording(false, cancalPolicy[countIndex].DayCancel, cancalPolicy[countIndex].ChargePercentBHT, cancalPolicy[countIndex].ChargeRoomBHT, 0, 0,CategoryID,LangID)+"<br/>";
                }
                lastDayCancel = cancalPolicy[countIndex].DayCancel;
            }
            if (LangID == 1)
            {
                if (lastDayCancel!=1)
                {
                cancelDisplay = cancelDisplay + "More than " + lastDayCancel + " Days prior to arrival There is a 7% administration charge<br/>";
                }else{
                cancelDisplay = cancelDisplay + "More than " + lastDayCancel + " Day prior to arrival There is a 7% administration charge<br/>";
                }
                
            }
            else {
                cancelDisplay = cancelDisplay + "หากท่านยกเลิกการจองก่อน" + lastDayCancel + "วัน ท่านจะถูกเรียกเก็บเงินเท่ากับ 7%ของยอดการจอง<br/>";
            }
            
            //---Render Policy
            //for (int countIndex = 0; countIndex < cancalPolicy.Count; countIndex++)
            //{
            //    if (countIndex == 0)
            //    {
            //        //first index
            //        cancelDisplay = cancelDisplay + " +" + cancalPolicy[countIndex].DayCancel + " Days prior to arrival<br/>";
            //        if (!string.IsNullOrEmpty(cancalPolicy[countIndex].ChargePercentBHT.ToString()) && cancalPolicy[countIndex].ChargePercentBHT != 0)
            //        {
            //            cancelDisplay = cancelDisplay + "There is a " + cancalPolicy[countIndex].ChargePercentBHT + "% administration charge<br/>";
            //        }
            //        if (!string.IsNullOrEmpty(cancalPolicy[countIndex].ChargeRoomBHT.ToString()) && cancalPolicy[countIndex].ChargeRoomBHT != 0)
            //        {
            //            cancelDisplay = cancelDisplay + "There is a cancellation fee of " + cancalPolicy[countIndex].ChargeRoomBHT + " night administration charge<br/>";
            //        }

            //    }
            //    else
            //    {
            //        if ((countIndex) != cancalPolicy.Count - 1)
            //        {
            //            cancelDisplay = cancelDisplay + cancalPolicy[countIndex].DayCancel + "-" + ((int)(cancalPolicy[countIndex + 1].DayCancel) + 1) + " Days prior to arrival<br/>";
            //        }
            //        else
            //        {
            //            if (cancalPolicy[countIndex].DayCancel != 0)
            //            {
            //                cancelDisplay = cancelDisplay + " 0 - " + cancalPolicy[countIndex].DayCancel + " Days prior to arrival " + cancalPolicy[countIndex].DayCancel + "<br/>";
            //            }
            //            else
            //            {
            //                cancelDisplay = cancelDisplay + cancalPolicy[countIndex].DayCancel + " Days prior to arrival " + cancalPolicy[countIndex].DayCancel + "<br/>";
            //            }

            //        }
            //        if (!string.IsNullOrEmpty(cancalPolicy[countIndex].ChargePercentBHT.ToString()) && cancalPolicy[countIndex].ChargePercentBHT != 0)
            //        {
            //            cancelDisplay = cancelDisplay + "There is a " + cancalPolicy[countIndex].ChargePercentBHT + "% administration charge<br/>";
            //        }
            //        if (!string.IsNullOrEmpty(cancalPolicy[countIndex].ChargeRoomBHT.ToString()) && cancalPolicy[countIndex].ChargeRoomBHT != 0)
            //        {
            //            cancelDisplay = cancelDisplay + "There is a cancellation fee of " + cancalPolicy[countIndex].ChargeRoomBHT + " night administration charge<br/>";
            //        }

            //    }
            //}
            return cancelDisplay;

        }
    }
}