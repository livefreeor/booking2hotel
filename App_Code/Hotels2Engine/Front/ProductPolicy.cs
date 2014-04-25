using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Hotels2thailand.DataAccess;
using Hotels2thailand;
/// <summary>
/// Summary description for ProductPolicy
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class ProductPolicy:Hotels2BaseClass
    {
        public int PolicyID { get; set; }
        public int ConditionID { get; set; }
        public byte PolicyCategory { get; set; }
        public byte PolicyType { get; set; }
        public string PolicyTypeTitle { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        private byte _langID = 1;

        private DateTime dateCheck;
        public byte LangID
        {
            set { _langID = value; }
        }

        public DateTime DateCheck
        {
            set { dateCheck = value; }
        }

        public ProductPolicy()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public List<ProductPolicy> GetProductPolicy(int ProductID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                //string sqlCommand = "select pp.policy_id,pp.cat_id,pp.type_id,pct.title,ppt.title as type_title,pct.detail,pp.date_start,pp.date_end,poc.condition_id";
                //sqlCommand = sqlCommand + " from tbl_product_policy pp,tbl_product_policy_content pct,tbl_product_option_condition_policy pocp,tbl_product_option po,tbl_product_option_condition poc,tbl_product_policy_type ppt";
                //sqlCommand = sqlCommand + " where pp.policy_id=pct.policy_id";
                //sqlCommand = sqlCommand + " and pp.type_id=ppt.type_id";
                //sqlCommand = sqlCommand + " and po.option_id=poc.option_id";
                //sqlCommand = sqlCommand + " and pocp.policy_id=pp.policy_id";
                //sqlCommand = sqlCommand + " and pocp.condition_id=poc.condition_id";



                //if (dateCheck != null)
                //{
                //    sqlCommand = sqlCommand + " and (" + dateCheck.Hotels2DateToSQlString() + " between pp.date_start and pp.date_end)";
                //}
                //sqlCommand = sqlCommand + " and pct.lang_id=1";
                //sqlCommand = sqlCommand + " and po.product_id=" + ProductID;


                string sqlCommand = "select temp.policy_id,ppc.title,temp.cat_id,temp.type_id,temp.title as type_title,temp.date_start,temp.date_end,temp.condition_id,ppc.detail,temp.condition_id";
                sqlCommand = sqlCommand + " from";
                sqlCommand = sqlCommand + " (";
                sqlCommand = sqlCommand + " select pp.policy_id,pp.cat_id,pp.type_id,ppt.title,pp.date_start,pp.date_end,poc.condition_id";
                sqlCommand = sqlCommand + " from tbl_product_policy pp,tbl_product_option_condition_policy pocp,tbl_product_option po,tbl_product_option_condition poc,tbl_product_policy_type ppt";
                sqlCommand = sqlCommand + " where";
                sqlCommand = sqlCommand + " po.option_id=poc.option_id";
                sqlCommand = sqlCommand + " and pocp.policy_id=pp.policy_id";
                sqlCommand = sqlCommand + " and pocp.condition_id=poc.condition_id";
                sqlCommand = sqlCommand + " and pp.type_id=ppt.type_id";
                if (dateCheck != null)
                {
                    sqlCommand = sqlCommand + " and " + dateCheck.Hotels2DateToSQlString() + " between pp.date_start and pp.date_end";
                }
                sqlCommand = sqlCommand + " and po.product_id=" + ProductID;
                sqlCommand = sqlCommand + " ) as temp left outer join tbl_product_policy_content ppc on ppc.policy_id=temp.policy_id";
                sqlCommand = sqlCommand + " and ppc.lang_id=" + _langID;

                //HttpContext.Current.Response.Write(sqlCommand);
                //HttpContext.Current.Response.End();
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<ProductPolicy> Items = new List<ProductPolicy>();
                string policyTypeTitle = string.Empty;

                while (reader.Read())
                {
                    if(_langID==1)
                    {
                    policyTypeTitle=reader["type_title"].ToString();
                    }else{
                        switch ((byte)reader["type_id"])
                        {
                            case 1:
                                policyTypeTitle = "เงื่อนไขการยกเลิก";
                                break;
                            case 2:
                                policyTypeTitle = "เช็คอิน";
                                break;
                            case 3:
                                policyTypeTitle = "เช็คเอ้าท์";
                                break;
                            case 4:
                                policyTypeTitle = "เด็กและเตียงเสริม";
                                break;
                            case 5:
                                policyTypeTitle = "สัตว์เลี้ยง";
                                break;
                            case 6:
                                policyTypeTitle = "ราคาเด็ก";
                                break;
                            case 7:
                                policyTypeTitle = "อื่นๆ";
                                break;
                            case 8:
                                policyTypeTitle = "เวลาแสดงและรถรับส่ง";
                                break;
                            case 9:
                                policyTypeTitle = "เวลาการทำสปา";
                                break;
                        }
                    
                    }

                    Items.Add(new ProductPolicy
                    {
                        PolicyID = (int)reader["policy_id"],
                        ConditionID = (int)reader["condition_id"],
                        PolicyCategory = (byte)reader["cat_id"],
                        PolicyType = (byte)reader["type_id"],
                        PolicyTypeTitle = policyTypeTitle,
                        Title = reader["title"].ToString(),
                        Detail = reader["detail"].ToString(),
                        DateStart = (DateTime)reader["date_start"],
                        DateEnd = (DateTime)reader["date_end"]
                    });
                }
                return Items;
            }
            
        }

        public string GetConditionPolicyList(List<ProductPolicy> policyList, int ConditionID, string promotionTitle, List<FrontCancellationPolicy> cancellationList)
        {
            string policyDisplay = string.Empty;

            //if (price.IsBreakfast > 0)
            //{
            //    policyDisplay = policyDisplay + "<li>Breakfast Included</li>";
            //}
            foreach (ProductPolicy item in policyList)
            {
                if (item.ConditionID == ConditionID)
                {

                    if (item.PolicyCategory == 3)
                    {
                        policyDisplay = policyDisplay + "<li>" + item.Title + "</li>";
                    }
                    else
                    {
                        if (item.PolicyType == 1 || item.PolicyType == 7 || item.PolicyType == 8 || item.PolicyType == 9)
                        {
                            if (item.PolicyType == 1)
                            {
                                if (RefundableAccept(item.PolicyID, item.ConditionID, cancellationList) != "")
                                {
                                    policyDisplay = policyDisplay + "<li>Refundable</li>";
                                }
                                else {
                                    policyDisplay = policyDisplay + "<li>Non-refundable</li>";
                                }
                                
                            }
                            else
                            {
                                policyDisplay = policyDisplay + "<li>" + item.Title + "</li>";
                            }

                        }
                    }


                }
            }

            if (promotionTitle != "")
            {
                policyDisplay = policyDisplay + "<li class=\"special\">" + promotionTitle + "</li>";
            }

            if (!string.IsNullOrEmpty(policyDisplay))
            {
                policyDisplay = "<ul class=\"condition_list\">" + policyDisplay + "</ul>";
            }
            return policyDisplay;
        }
        public string GetConditionPolicyList(List<ProductPolicy> policyList, int ConditionID, string promotionTitle, List<FrontCancellationPolicy> cancellationList, byte breakfastInclude)
        {
            string policyDisplay = string.Empty;

            if (breakfastInclude > 0)
            {
                if(_langID==1)
                {
                policyDisplay = "<li>Breakfast Included</li>";
                }else{
                policyDisplay = "<li>รวมอาหารเช้า</li>";
                }
                
            }
            foreach (ProductPolicy item in policyList)
            {
                if (item.ConditionID == ConditionID)
                {

                    if (item.PolicyCategory == 3)
                    {
                        policyDisplay = policyDisplay + "<li>" + item.Title + "</li>";
                    }
                    else
                    {
                        if (item.PolicyType == 1 || item.PolicyType == 7 || item.PolicyType == 8 || item.PolicyType == 9)
                        {
                            if (item.PolicyType == 1)
                            {
                                if (RefundableAccept(item.PolicyID, item.ConditionID, cancellationList) != "")
                                {
                                    if(_langID==1)
                                    {
                                        policyDisplay = policyDisplay + "<li>Refundable</li>";
                                    }else{
                                        policyDisplay = policyDisplay + "<li>สามารถคืนเงินได้</li>";
                                    }
                                }
                                else
                                {
                                    if(_langID==1)
                                    {
                                        policyDisplay = policyDisplay + "<li>Non-refundable</li>";
                                    }else{
                                        policyDisplay = policyDisplay + "<li>ไม่สามารถคืนเงินได้</li>";
                                    }
                                    
                                }

                            }
                            else
                            {
                                policyDisplay = policyDisplay + "<li>" + item.Title + "</li>";
                            }

                        }
                    }


                }
            }

            if (promotionTitle != "")
            {
                //policyDisplay = policyDisplay + "<li class=\"special\">" + promotionTitle + "</li>";

                policyDisplay = policyDisplay + "<li class=\"special\">Special Offer</li>";
            }

            if (!string.IsNullOrEmpty(policyDisplay))
            {
                policyDisplay = "<ul class=\"condition_list\">" + policyDisplay + "</ul>";
            }
            return policyDisplay;
        }
        public string GetPolicyContent(List<ProductPolicy> policyList,List<FrontCancellationPolicy> cancellationList, int ConditionID)
        {

            string result = string.Empty;
            //if (price.IsBreakfast > 0)
            //{
            //    result = result + "<strong>Breakfast: Included</strong><br>";
            //}
            foreach (ProductPolicy item in policyList)
            {
                if (item.ConditionID == ConditionID)
                {

                    if (item.PolicyType != 2 && item.PolicyType != 3)
                    {
                        if (item.PolicyCategory == 3)
                        {

                            result = result + "<strong>" + item.PolicyTypeTitle + "Private</strong><span class=\"cancellation_content\">" + item.Detail + "</span><br>";
                        }
                        else
                        {

                            if (item.PolicyType == 1)
                            {
                                if (RefundableAccept(item.PolicyID, item.ConditionID,cancellationList) != "")
                                {
                                    FrontCancellationPolicy cancelPolicy = new FrontCancellationPolicy();
                                    
                                    result = result + "<br/><strong>" + item.PolicyTypeTitle + " :</strong><br/><span class=\"cancellation_content\">" + cancelPolicy.RenderCancellation(cancellationList, item.PolicyID, item.ConditionID,32,_langID) + "</span><br/>";
                                }
                                else
                                {
                                    if(_langID==1)
                                    {
                                        result = result + "<br/><strong>" + item.PolicyTypeTitle + " :</strong><br/><span class=\"cancellation_content\">Non-refundable</span><br/><br/>";
                                    
                                    }else{
                                        result = result + "<br/><strong>" + item.PolicyTypeTitle + " :</strong><br/><span class=\"cancellation_content\">ไม่สามารถคืนเงินได้</span><br/><br/>";
                                    }
                                    
                                }
                            }
                            else
                            {
                                result = result + "<strong>" + item.PolicyTypeTitle + ": </strong><span class=\"cancellation_content\">" + item.Detail + "</span><br>";
                            }


                            //result = result + "<strong>" + item.PolicyTypeTitle + item + "</strong>" + item.Detail + "<br>";
                        }
                    }
                }

            }
            if(_langID==1)
            {
            result = result + "<strong>Help the children: </strong><br/><span class=\"cancellation_content\">You can help the children,everybooking booking to shared non-profit childen organization in Thailand</span>";
            }else{
                result = result + "<strong>ช่วยเหลือเด็กด้อยโอกาส: </strong><br/><span class=\"cancellation_content\">คุณสามารถเข้าร่วมโครงการในการช่วยเหลือเด็กยากไร้และด้อยโอกาสกับโฮเทลทูได้ ทุกๆการจองของคุณรายได้ส่วนหนึ่งเราจะนำไปร่วมสมทบทุนบริจาคให้กับมูลนิธิช่วยเหลือเด็กยากไร้ที่อยู่ในประเทศไทย</span>";
            
            }
            
            return result;
        }

        public string RefundableAccept(int PolicyID, int ConditionID, List<FrontCancellationPolicy> cancellationList)
        {
            string cancelDisplay = "Refundable";
            if(_langID==1)
            {
            cancelDisplay = "สามารถคืนเงินได้";
            }
            string[,] ResultProductType = new string[10, 3];

            List<FrontCancellationPolicy> cancalPolicy = new List<FrontCancellationPolicy>();

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

            foreach (FrontCancellationPolicy cancel in cancalPolicy)
            {
                if (cancel.DayCancel == 250)
                {
                    cancelDisplay = "";
                    break;
                }
            }


            return cancelDisplay;

        }
    }
}