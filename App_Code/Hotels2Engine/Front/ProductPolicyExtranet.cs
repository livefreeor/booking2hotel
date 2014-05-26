using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Hotels2thailand.DataAccess;
using Hotels2thailand;

/// <summary>
/// Summary description for ProductPolicyExtranet
/// </summary>
///
namespace Hotels2thailand.Front
{
    public class ProductPolicyExtranet
    {
        public int ContentID { get; set; }
        public int ConditionID { get; set; }
        public byte LangID { get; set; }
        public string Title { get; set; }
        public string ConditionTitle { get; set; }
        public string ContentTitle { get; set; }
        public string Detail { get; set; }
        public byte ConditionNameId { get; set; }
        private byte manageID=1;
        public byte ManageID {
            set { manageID = value; }
        }
        private byte CateogoryID = 29;
       

        public ProductPolicyExtranet()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public ProductPolicyExtranet(byte categoryID)
        {
            CateogoryID = categoryID;
        }
        public List<ProductPolicyExtranet> GetExtraPolicy(int ProductID, byte langID)
        {
            DataConnect objConn = new DataConnect();

            string sqlCommand = "select pocc_ex.content_id,pocc_ex.condition_id,pocc_ex.title,poct_ex.title as condition_title,pocc_ex.detail,poc_ex.condition_name_id";
            sqlCommand = sqlCommand + " from tbl_product_option po,tbl_product_option_condition_extra_net poc_ex,tbl_product_option_condition_content_extra_net  pocc_ex,tbl_product_option_condition_title_lang_extra_net poct_ex";
            sqlCommand = sqlCommand + " where po.option_id=poc_ex.option_id and poc_ex.condition_id=pocc_ex.condition_id and pocc_ex.condition_id=poct_ex.condition_id and pocc_ex.lang_id=1 and poct_ex.lang_id=1 and po.product_id=" + ProductID;
            sqlCommand = sqlCommand + " and poc_ex.status=1 and pocc_ex.status=1";
            sqlCommand = sqlCommand + " order by poc_ex.condition_id";
            //HttpContext.Current.Response.Write(sqlCommand);
            //HttpContext.Current.Response.End();
            SqlDataReader reader = objConn.GetDataReader(sqlCommand);

            List<ProductPolicyExtranet> listResult = new List<ProductPolicyExtranet>();

            string titleDisplay = string.Empty;
            string conditionTitleDisplay = string.Empty;
            string contentTitleDisplay = string.Empty;
            string detailDisplay = string.Empty;
            byte bytConditionNameId = 0;

            while (reader.Read())
            {
                titleDisplay = reader["title"].ToString();
                conditionTitleDisplay = reader["condition_title"].ToString();
                contentTitleDisplay = reader["title"].ToString();
                detailDisplay = reader["detail"].ToString();
                bytConditionNameId = (byte)reader["condition_name_id"];

                if(LangID==2)
                {
                    titleDisplay = GlobalVar.GetPolicyTitleExtranet(titleDisplay);
                    conditionTitleDisplay = GlobalVar.GetPolicyTitleExtranet(conditionTitleDisplay);
                    contentTitleDisplay = GlobalVar.GetPolicyTitleExtranet(contentTitleDisplay);
                    detailDisplay = GlobalVar.GetPolicyTitleExtranet(detailDisplay);
                }

                listResult.Add(new ProductPolicyExtranet
                {
                    ContentID = (int)reader["content_id"],
                    ConditionID = (int)reader["condition_id"],
                    LangID = langID,
                    Title = titleDisplay,
                    ConditionTitle = conditionTitleDisplay,
                    ContentTitle = contentTitleDisplay,
                    Detail = detailDisplay,
                    ConditionNameId = bytConditionNameId
                });
            }
            return listResult;
        }

        public string GetConditionPolicyList(List<ProductPolicyExtranet> policyList, int ConditionID, string promotionTitle)
        {
            string policyDisplay = string.Empty;

            //if (price.IsBreakfast > 0)
            //{
            //    policyDisplay = policyDisplay + "<li>Breakfast Included</li>";
            //}
            foreach (ProductPolicyExtranet item in policyList)
            {
                if (item.ConditionID == ConditionID)
                {

                    policyDisplay = policyDisplay + "<li>" + item.ConditionTitle + "</li>";
                    break;
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

        public string GetConditionPolicyPackage(string ConditionTitle,byte breakfastInclude)
        {
            string policyDisplay = string.Empty;

            if (breakfastInclude > 0)
            {
                if (LangID == 1)
                {
                    policyDisplay = "<li>Breakfast Included</li>";
                }
                else
                {
                    policyDisplay = "<li>รวมอาหารเช้า</li>";
                }

            }
            if(ConditionTitle=="Refundable")
            {
                policyDisplay = policyDisplay + "<li>" + ConditionTitle + "</li>";
            }

            if (!string.IsNullOrEmpty(policyDisplay))
            {
                policyDisplay = "<ul class=\"condition_list\">" + policyDisplay + "</ul>";
            }
            return policyDisplay;
            
        }

        public string GetConditionPolicyList(List<ProductPolicyExtranet> policyList, int ConditionID, string promotionTitle,byte breakfastInclude)
        {
            string policyDisplay = string.Empty;

            if (breakfastInclude > 0)
            {
                if(LangID==1)
                {
                   policyDisplay = "<li>Breakfast Included</li>";  
                }else{
                   policyDisplay = "<li>รวมอาหารเช้า</li>";
                }
               
            }
            foreach (ProductPolicyExtranet item in policyList)
            {
                if (item.ConditionID == ConditionID)
                {
                    if(item.ConditionNameId != 2)
                        policyDisplay = policyDisplay + "<li>" + item.ConditionTitle + "</li>";
                    break;
                }
            }

            if (promotionTitle != "")
            {
                //policyDisplay = policyDisplay + "<li class=\"special\">" + promotionTitle + "</li>";
                if(LangID==1)
                {
                    policyDisplay = policyDisplay + "<li class=\"special\">Special Offer</li>";
                }else{
                    policyDisplay = policyDisplay + "<li class=\"special\">ข้อเสนอพิเศษ</li>";
                }
                
            }

            if (!string.IsNullOrEmpty(policyDisplay))
            {
                policyDisplay = "<ul class=\"condition_list\">" + policyDisplay + "</ul>";
            }
            return policyDisplay;
        }
        public string GetPolicyContent(List<ProductPolicyExtranet> policyList, List<CancellationExtranet> cancellationList, int ConditionID, int PromotionID, string promotionTitle, string promotionDetail,bool isCancellation)
        {
            string result = string.Empty;
            if (PromotionID!=0)
            {
                result = result + "<img src=\"http://www.booking2hotels.com/images/ico_special_offer.jpg\">&nbsp;<strong>Promotion:</strong><br/>" + promotionTitle+"<br/>";
                result = result + Hotels2XMLContent.Hotels2XMLReaderPromotionDetailExtranet_Front(promotionDetail) + "<br/>";
            }

            byte bytConditionNameId = 0;
            foreach (ProductPolicyExtranet item in policyList)
            {
                if (item.ConditionID == ConditionID)
                {

                    result = result + "<strong>" + item.ContentTitle + "</strong> <span class=\"cancellation_content\">" + item.Detail + "</span><br>";
                    bytConditionNameId = item.ConditionNameId;
                }

            }

            if (isCancellation)
            {
                result = result + RenderCancellationPromotion(PromotionID);
            }else{
                result = result + RenderCancellation(ConditionID, cancellationList, bytConditionNameId);
            }
            //if(LangID==1)
            //{
            //result = result + "<br/><strong>Help the children: </strong><br/><span class=\"cancellation_content\">You can help the children. Every booking will be sharing to non-profit children organization in Thailand.</span>";
            //}else{
            //    result = result + "<br/><strong>ช่วยเหลือเด็กด้อยโอกาส: </strong><br/><span class=\"cancellation_content\">คุณสามารถเข้าร่วมโครงการในการช่วยเหลือเด็กยากไร้และด้อยโอกาสกับโฮเทลทูได้ ทุกๆการจองของคุณรายได้ส่วนหนึ่งเราจะนำไปร่วมสมทบทุนบริจาคให้กับมูลนิธิช่วยเหลือเด็กยากไร้ที่อยู่ในประเทศไทย</span>";
            
            //}
            
            return result;
        }
        public string GetPolicyContentPromotion(List<ProductPolicyExtranet> policyList, List<CancellationExtranet> cancellationList, int ConditionID, int PromotionID, string promotionTitle, string promotionDetail)
        {
            string result = string.Empty;

            foreach (ProductPolicyExtranet item in policyList)
            {
                if (item.ConditionID == ConditionID)
                {

                    result = result + "<strong>" + item.ContentTitle + "</strong><span class=\"cancellation_content\">" + item.Detail + "</span><br>";
                }

            }

            result = result + RenderCancellationPromotion(PromotionID);
            //result = result + "<br/><strong>Help the children: </strong><br/><span class=\"cancellation_content\">You can help the children. Every booking will be sharing to non-profit children organization in Thailand.</span>";
            //if (LangID == 1)
            //{
            //    result = result + "<br/><strong>Help the children: </strong><br/><span class=\"cancellation_content\">You can help the children. Every booking will be sharing to non-profit children organization in Thailand.</span>";
            //}
            //else
            //{
            //    result = result + "<br/><strong>ช่วยเหลือเด็กด้อยโอกาส: </strong><br/><span class=\"cancellation_content\">คุณสามารถเข้าร่วมโครงการในการช่วยเหลือเด็กยากไร้และด้อยโอกาสกับโฮเทลทูได้ ทุกๆการจองของคุณรายได้ส่วนหนึ่งเราจะนำไปร่วมสมทบทุนบริจาคให้กับมูลนิธิช่วยเหลือเด็กยากไร้ที่อยู่ในประเทศไทย</span>";

            //}
            return result;
        }

        public string RenderCancellationPromotion(int PromotionID)
        {
            string result = string.Empty;
            FrontPromotionCancel objCancel = new FrontPromotionCancel();
            IList<object> cancelList = objCancel.GetPromotionCancelByPromotionID(PromotionID);
            int lastDayCancel = 0;
            if (cancelList.Count!=0)
            {
            if (cancelList.Count == 1)
            {
                FrontPromotionCancel nonRefundCancel = (FrontPromotionCancel)cancelList[0];
                if (nonRefundCancel.DayCancel == 100)
                {
                    if(LangID==1)
                    {
                        result = result + "Non Refundable<br/>";
                    }else{
                        result = result + "ในกรณีที่ท่านไม่เช็คอินที่โรงแรมตามที่กำหนดท่านจะไม่ได้รับเงินคืน<br/>";
                    }
                    
                }
                else
                {
                    if(LangID==1)
                    {
                        result = result + Hotels2String.CancellationGenerateWording(true,nonRefundCancel.DayCancel, 0, 0, nonRefundCancel.ChangePercent, nonRefundCancel.ChangeNight) + "<br/>";
                    }else{
                        result = result + Hotels2String.CancellationGenerateWording(true, nonRefundCancel.DayCancel, 0, 0, nonRefundCancel.ChangePercent, nonRefundCancel.ChangeNight,CateogoryID, LangID)+"<br/>";
                    }
                    
                    
                }
            }
            else if (cancelList.Count > 1)
            {
                foreach (FrontPromotionCancel item in cancelList)
                {
                    if(LangID==1)
                    {
                     result = result + Hotels2String.CancellationGenerateWording(true,item.DayCancel, 0, 0, item.ChangePercent, item.ChangeNight) + "<br/>";
                    }else{
                     result = result + Hotels2String.CancellationGenerateWording(true,item.DayCancel, 0, 0, item.ChangePercent, item.ChangeNight,CateogoryID,LangID) + "<br/>";
                    }
                   
                    lastDayCancel = item.DayCancel;
                }
                result = result + "More than " + lastDayCancel + " Days prior to arrival There is a 7% administration charge<br/>";
            }
            }else{
                if (LangID == 1)
                {
                    result = result + "Non Refundable <br/>";
                }
                else
                {
                    result = result + "ในกรณีที่ท่านไม่เช็คอินที่โรงแรมตามที่กำหนดท่านจะไม่ได้รับเงินคืน <br/>";
                }
            }

            return result;
        }
        public string RenderCancellation(int ConditionID, List<CancellationExtranet> cancellationList, byte bytConditionNameID)
        {
            string cancelDisplay = "";
            string[,] ResultProductType = new string[10, 3];
            int lastDayCancel = 0;

            List<CancellationExtranet> cancalPolicy = new List<CancellationExtranet>();

            foreach (CancellationExtranet item in cancellationList)
            {
                if (item.ConditionID == ConditionID)
                {
                    cancalPolicy.Add(new CancellationExtranet
                    {
                        CancelID = item.CancelID,
                        ConditionID = item.ConditionID,
                        DateStart = item.DateStart,
                        DateEnd = item.DateEnd,
                        Title = item.Title,
                        DayCancel = item.DayCancel,
                        ChargePercent = item.ChargePercent,
                        ChargeNight = item.ChargeNight
                    });

                }

            }

            string strConditionTitle = string.Empty;
            if (bytConditionNameID == 2)
                strConditionTitle =  "(Refundable)";

            if(LangID==1)
            {
                cancelDisplay = cancelDisplay + "<br/><strong>Cancellation Policy " + strConditionTitle + "</strong><br/>";
            }else{
                cancelDisplay = cancelDisplay + "<br/><strong>เงื่อนไขการยกเลิก</strong><br/>";
            }

            
            if (cancalPolicy.Count > 0)
            {
                if (cancalPolicy.Count == 1)
                {
                    if (cancalPolicy[0].DayCancel == 100)
                    {
                        if(LangID==1)
                        {
                            cancelDisplay = cancelDisplay + "Non Refundable <br/>";
                        }else{
                            cancelDisplay = cancelDisplay + "ในกรณีที่ท่านไม่เช็คอินที่โรงแรมตามที่กำหนดท่านจะไม่ได้รับเงินคืน <br/>";
                        }
                        
                    }
                    else
                    {
                        if(LangID==1)
                        {
                            cancelDisplay = cancelDisplay + Hotels2String.CancellationGenerateWording(true, cancalPolicy[0].DayCancel, 0, 0, cancalPolicy[0].ChargePercent, cancalPolicy[0].ChargeNight, CateogoryID, LangID, manageID) + "<br/>";
                        }else{
                            cancelDisplay = cancelDisplay + Hotels2String.CancellationGenerateWording(true, cancalPolicy[0].DayCancel, 0, 0, cancalPolicy[0].ChargePercent, cancalPolicy[0].ChargeNight, CateogoryID, LangID, manageID) + "<br/>";
                        }
                        
                    }
                        if (manageID != 1)
                        {
                            if(LangID==1)
                            {
                                cancelDisplay = cancelDisplay + "More than " + lastDayCancel + " Days prior to arrival There is a 7% administration charge<br/>";
                            }else{
                                cancelDisplay = cancelDisplay + "การยกเลิกการจองมากกว่า" + lastDayCancel + "วัน ก่อนวันเช็คอินหรือใช้บริการทัวร์ ทางเวปไซต์โฮเทลทูจะเรียกเก็บค่าธรรมเนียม 7% จากยอดการจองทั้งหมด<br/>";
                            }
                            
                        }

                }
                else
                {
                    // More than 1 cancellation
                    for (int countIndex = 0; countIndex < cancalPolicy.Count; countIndex++)
                    {
                        if(LangID==1)
                        {
                            cancelDisplay = cancelDisplay + Hotels2String.CancellationGenerateWording(true, cancalPolicy[countIndex].DayCancel, 0, 0, cancalPolicy[countIndex].ChargePercent, cancalPolicy[countIndex].ChargeNight, CateogoryID, LangID, manageID) + "<br/>";
                        }else{
                            cancelDisplay = cancelDisplay + Hotels2String.CancellationGenerateWording(true, cancalPolicy[countIndex].DayCancel, 0, 0, cancalPolicy[countIndex].ChargePercent, cancalPolicy[countIndex].ChargeNight, CateogoryID, LangID, manageID) + "<br/>";
                        }
                        
                        lastDayCancel = cancalPolicy[countIndex].DayCancel;
                    }
                    if (manageID != 1)
                    {
                        if (LangID == 1)
                        {
                            cancelDisplay = cancelDisplay + "More than " + lastDayCancel + " Days prior to arrival There is a 7% administration charge<br/>";
                        }
                        else
                        {
                            cancelDisplay = cancelDisplay + "การยกเลิกการจองมากกว่า" + lastDayCancel + "วัน ก่อนวันเช็คอินหรือใช้บริการทัวร์ ทางเวปไซต์โฮเทลทูจะเรียกเก็บค่าธรรมเนียม 7% จากยอดการจองทั้งหมด<br/>";
                        }

                    }
                    
                }

            }
            else {
                if (LangID == 1)
                {
                    cancelDisplay = cancelDisplay + "Non Refundable <br/>";
                }
                else {
                    cancelDisplay = cancelDisplay + "ในกรณีที่ท่านไม่เช็คอินที่โรงแรมตามที่กำหนดท่านจะไม่ได้รับเงินคืน <br/>";
                }
                
            }
            return cancelDisplay;
 
        }

        public string GetPolicyCancellation(List<ProductPolicyExtranet> policyList, List<CancellationExtranet> cancellationList, int ConditionID, int PromotionID, string promotionTitle, string promotionDetail, bool isCancellation)
        {
            string result = string.Empty;
            if (PromotionID != 0)
            {
                result = result + "<br><strong class='headerTitle'>Promotion: </strong><span class='headerTitle'>" + promotionTitle + "</span>";
            }

            byte bytConditionNameId = 0;

            if (isCancellation)
            {
                result = result + "<span class='headerTitle'>" + RenderCancellationPromotion(PromotionID) + "</span>";
            }
            else
            {
                result = result + "<span class='headerTitle'>" + RenderCancellation(ConditionID, cancellationList, bytConditionNameId) + "</span>";
            }
            return result;
        }

    }
}