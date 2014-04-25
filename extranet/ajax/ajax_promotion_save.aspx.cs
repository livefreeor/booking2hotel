using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Staffs;
using Hotels2thailand.ProductOption;
using Hotels2thailand.Production;
using System.Text;


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_promotion_save : Hotels2BasePageExtra_Ajax
    {
        public string qPromotionGroup
        {
            get { return Request.QueryString["pg"]; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (this.Page.IsPostBack)
            {
                if (this.IsStaffAdd())
                {
                    if (!string.IsNullOrEmpty(this.qPromotionGroup))
                    {
                        //Response.Write(Request.Form["ctl00$ContentPlaceHolder1$hd_country_selected"]);
                        Response.Write(PromotionSave());
                       //ctl00$ContentPlaceHolder1$country_selected
                    }
                }
                else
                {
                    Response.Write("method_invalid");
                }
                Response.End();
            }
            
        }

        public string PromotionSave()
        {
            string result = "False";
            try
            {
                PromotionExxtranet cProExtra = new PromotionExxtranet();

                PromotionDayuseExtranet cProDateUse = new PromotionDayuseExtranet();
                PromotionBenefitExtranet cProbenefit = new PromotionBenefitExtranet();
                PromotionConditionExtranet cProcondition = new PromotionConditionExtranet();
                PromotioncancellExtranet cPromotionCancel = new PromotioncancellExtranet();


                
                byte bytPromotionGroup = byte.Parse(this.qPromotionGroup);

                string strPromotionTitle = Request.Form["hd_promotion_title"].Replace(" <span class=\"setting_val\">", "").Replace("</span>", ""); 

                bool IsCancellation = false;

                if (Request.Form["radiocancel"] == "1")
                    IsCancellation = true;

                byte bytPromotionCat = 1;
                int intProductId = this.CurrentProductActiveExtra;
                short shrSuplierId = this.CurrentSupplierId;
                string PromotionTitle = strPromotionTitle;

                DateTime dDateBookingstart = Request.Form["hd_booking_start"].Hotels2DateSplitYear("-");
                DateTime dDateBookingEnd = Request.Form["hd_booking_End"].Hotels2DateSplitYear("-");

                DateTime dDateStayStart = Request.Form["hd_Stay_start"].Hotels2DateSplitYear("-");
                DateTime dDateStayEnd = Request.Form["hd_Stay_End"].Hotels2DateSplitYear("-");

                bool IsSun = true;
                bool IsMon = true;
                bool IsTue = true;
                bool IsWed = true;
                bool IsThu = true;
                bool IsFri = true;
                bool IsSat = true;
                if (Request.Form["radioweekDay"] == "1")
                {
                    string[] DayOfWeek = Request.Form["check_dayofWeek"].Split(',');

                    if (DayOfWeek.SingleOrDefault(d => d == "0") == null) { IsSun = false; }
                    if (DayOfWeek.SingleOrDefault(d => d == "1") == null) { IsMon = false; }
                    if (DayOfWeek.SingleOrDefault(d => d == "2") == null) { IsTue = false; }
                    if (DayOfWeek.SingleOrDefault(d => d == "3") == null) { IsWed = false; }
                    if (DayOfWeek.SingleOrDefault(d => d == "4") == null) { IsThu = false; }
                    if (DayOfWeek.SingleOrDefault(d => d == "5") == null) { IsFri = false; }
                    if (DayOfWeek.SingleOrDefault(d => d == "6") == null) { IsSat = false; }

                    //Response.Write(IsSun.ToString() + IsMon + IsTue + IsWed + IsThu + IsFri + IsSat);
                    //Response.End();
                }

                
                byte bytDayMin = 1;
                //Default ProAdvande;
                short bytAdvanceDay = 0;
                //Default Pro Last minute;
                short shrLastminuteDay = 999;
                //byte bytWithinDay = 1;
                byte bytHolidayAll = 1;
                bool bolIsLastMinute = false;
                bool bolIsWordwide = true;
                if (Request.Form["radioholiday"] == "1")
                    bytHolidayAll = 0;
                byte bytMaxSet = 100;
                // Promotion Is Abf 
                byte bytIsABF = 1;
                decimal decABFCharge = 0;

                short shrProItem = short.Parse(Request.Form["pro_item_select"]);

               
                

                decimal TotalCharge = 0;
                byte bytStartDiscountnight = 1;
                byte numDiscountnight = 1;
                byte PromotionType = 1;
                byte byteFreeNightStay = 0;
                byte byteFreeNightPay = 0;
                string benefit = string.Empty;
                byte bytBenefitFac1 = 0;
                byte bytBenefitFac2 = 1;
                // cProExtra.InsertPromotion();

                bytMaxSet = byte.Parse(Request.Form["limit_book"]);
                bytDayMin = byte.Parse(Request.Form["sel_min_day"]);
               
                // all Promotion use  benefit
                benefit = GenPromotionDetailXML();
                //Response.Write(benefit);
                //Response.End();
                

                switch (bytPromotionGroup)
                {
                    // Early Bird
                    case 1:
                        bytAdvanceDay = short.Parse(Request.Form["sel_advance_day"]);

                        switch (shrProItem)
                        {
                            case 1:
                            case 3:
                                TotalCharge = decimal.Parse(Request.Form["dis_percent"]);
                                PromotionType = 2;
                                break;
                            case 2:
                            case 4:
                                TotalCharge = decimal.Parse(Request.Form["dis_baht"]);
                                PromotionType = 6;
                                break;

                            case 5:
                                
                                PromotionType = 6;
                                break;

                        }
                        break;
                    // Free Night
                    case 2:

                        byteFreeNightStay = byte.Parse(Request.Form["free_night_stay"]);
                        byteFreeNightPay = byte.Parse(Request.Form["free_night_pay"]);

                        bytDayMin = byteFreeNightStay;
                        bytStartDiscountnight = (byte)(byteFreeNightPay + 1);
                        numDiscountnight = (byte)(byteFreeNightStay - byteFreeNightPay);
                        PromotionType = 1;

                        switch (shrProItem)
                        {
                            case 6:

                                break;
                            case 7:
                                bytIsABF = 3;
                                decABFCharge = decimal.Parse(Request.Form["com_abf"]);

                                break;
                            //case 8:
                            //    bytIsABF = 3;
                            //    decABFCharge = decimal.Parse(Request.Form["com_abf"]);
                            //    benefit = GenPromotionDetailXML();
                            //    break;

                            //case 9:
                            //    benefit = GenPromotionDetailXML();
                            //    break;

                        }
                        break;
                    // MiniMum Night
                    case 3:
                        switch (shrProItem)
                        {
                            case 11:
                                PromotionType = 2;
                                TotalCharge = decimal.Parse(Request.Form["dis_percent"]);
                                break;
                            case 12:
                                PromotionType = 6;
                                TotalCharge = decimal.Parse(Request.Form["dis_baht"]);
                                break;
                            case 13:
                                bytDayMin = byte.Parse(Request.Form["sel_consec_night"]);
                                TotalCharge = decimal.Parse(Request.Form["dis_percent"]);
                                numDiscountnight = byte.Parse(Request.Form["sel_consec_night"]);
                                PromotionType = 3;
                                break;
                            case 14:
                                bytDayMin = byte.Parse(Request.Form["sel_consec_night"]);
                                TotalCharge = decimal.Parse(Request.Form["dis_baht"]);
                                numDiscountnight = byte.Parse(Request.Form["sel_consec_night"]);
                                PromotionType = 4;
                                break;
                            case 19:
                                PromotionType = 6;
                                break;
                            case 20:
                                PromotionType = 6;
                                break;
                        }
                        break;
                    // Discount
                    case 4:
                        switch (shrProItem)
                        {
                            case 15:
                                PromotionType = 2;
                                TotalCharge = decimal.Parse(Request.Form["dis_percent"]);
                                break;
                            case 16:
                                PromotionType = 6;
                                TotalCharge = decimal.Parse(Request.Form["dis_baht"]);
                                break;
                            //case 17:
                            //    PromotionType = 2;
                            //    TotalCharge = decimal.Parse(Request.Form["dis_percent"]);
                            //    benefit = GenPromotionDetailXML();
                            //    break;
                            //case 18:
                            //    PromotionType = 6;
                            //    benefit = GenPromotionDetailXML();
                            //    TotalCharge = decimal.Parse(Request.Form["dis_baht"]);
                            //    break;
                        }
                        break;
                    // LastMinute
                    case 5:
                        shrLastminuteDay = short.Parse(Request.Form["sel_within_day"]);
                        switch (shrProItem)
                        {
                            case 21:
                                PromotionType = 2;
                                TotalCharge = decimal.Parse(Request.Form["dis_percent"]);
                                break;
                            case 22:
                                PromotionType = 6;
                                TotalCharge = decimal.Parse(Request.Form["dis_baht"]);
                                break;
                            case 23:
                                PromotionType = 2;
                                TotalCharge = decimal.Parse(Request.Form["dis_percent"]);
                                break;
                            case 24:
                                PromotionType = 6;
                                TotalCharge = decimal.Parse(Request.Form["dis_baht"]);
                                break;
                            //case 17:
                            //    PromotionType = 2;
                            //    TotalCharge = decimal.Parse(Request.Form["dis_percent"]);
                            //    benefit = GenPromotionDetailXML();
                            //    break;
                            //case 18:
                            //    PromotionType = 6;
                            //    benefit = GenPromotionDetailXML();
                            //    TotalCharge = decimal.Parse(Request.Form["dis_baht"]);
                            //    break;
                        }
                        break;
                }

                
                int intPromotionId = 0;
                byte bytScore = 0;
                byte bytScoreBenefit = 0;

                try
                {
                    string[] conditionList = Request.Form["checkbox_condition_check"].Split(',');
                    PromotionScore cPromotionScore = new PromotionScore();
                    bytScore = cPromotionScore.GetScore(intProductId, dDateStayStart, dDateStayEnd, PromotionType, bytIsABF
                        , decABFCharge, TotalCharge, conditionList, byteFreeNightStay, byteFreeNightPay, bytDayMin);

                   bytScoreBenefit = cPromotionScore.GetScoreBenefit(benefit);

                }
                catch (Exception ex)
                {
                    Response.Write("INSERT promotion Score error : " + ex.Message);
                    Response.End();
                }


                try
                {

                    if (!string.IsNullOrEmpty(Request.Form["ctl00$ContentPlaceHolder1$hd_country_selected"]))
                        bolIsWordwide =  false;
                    //insert PromotionMain
                    intPromotionId = cProExtra.InsertPromotion(bytPromotionCat, intProductId, shrSuplierId, PromotionTitle, dDateBookingstart,
                        dDateBookingEnd, IsMon, IsTue, IsWed, IsThu, IsFri, IsSat, IsSun, bytDayMin, bytAdvanceDay, bytHolidayAll, bytMaxSet,
                        bytIsABF, decABFCharge, shrProItem, IsCancellation, bolIsLastMinute, bytScore, bytScoreBenefit, shrLastminuteDay, bolIsWordwide);
                    result = "True";
                }
                catch (Exception ex)
                {
                    Response.Write("INSERT promotion Main error : " + ex.Message);
                    Response.End();
                }

                try
                {
                    // insert PromotionContent 
                    PromotionContentExtranet cProcontent = new PromotionContentExtranet { PromotionId = intPromotionId, langId = 1, Title = PromotionTitle, Detail = benefit };
                    cProcontent.InsertContentLang(cProcontent, intProductId);
                    result = "True";
                }
                catch (Exception ex)
                {
                    Response.Write("INSERT promotion Content error : " + ex.Message);
                    Response.End();
                }

                try
                {
                    // Insert PromotionCondition
                    string[] conditionList = Request.Form["checkbox_condition_check"].Split(',');

                    foreach (string condition in conditionList)
                    {
                        cProcondition.InsertMappingConditionPromotionId(intPromotionId, int.Parse(condition), intProductId);
                    }

                    result = "True";
                }
                catch (Exception ex)
                {
                    Response.Write("INSERT promotion Condition Mapping error : " + ex.Message);
                    Response.End();
                }


                try
                {
                    //insert DateUse Promotion extranet 
                    cProDateUse.intsertdateUse(intProductId, intPromotionId, dDateStayStart, dDateStayEnd);
                    result = "True";
                }
                catch (Exception ex)
                {
                    Response.Write("INSERT insert DateUse Promotion extranet : " + ex.Message);
                    Response.End();
                }

                try
                {
                    if (PromotionType == 1 || PromotionType == 3 || PromotionType == 4)
                    {
                        bytBenefitFac1 = 1;
                        bytBenefitFac2 = 0;
                    }
                    else
                    {
                        bytBenefitFac1 = 0;
                        bytBenefitFac2 = 1;
                    }
                       
                    // iNsert Benefit Promotion 
                    cProbenefit.InsertBenefit(intPromotionId, intProductId, bytStartDiscountnight, numDiscountnight, PromotionType, TotalCharge, 1, bytBenefitFac1, bytBenefitFac2);
                    result = "True";
                }
                catch (Exception ex)
                {
                    Response.Write("INSERT Insert Benefit Promotion : " + ex.Message);
                    Response.End();
                }

                // Insert Cancelltion Pro
                if (IsCancellation)
                {
                    try
                    {
                        string[] CancellationList = Request.Form["cencel_list_Checked"].Split(',');

                        foreach (string cancel in CancellationList)
                        {
                            cPromotionCancel.InsertNewCancelRule(intProductId, intPromotionId, byte.Parse(Request.Form["drop_daycancel_" + cancel]),
                                byte.Parse(Request.Form["txt_per_charge_" + cancel]),byte.Parse(Request.Form["txt_day_charge_" + cancel]));
                        }
                        //// iNsert Benefit Promotion 
                        //cProbenefit.InsertBenefit(intPromotionId, intProductId, bytStartDiscountnight, numDiscountnight, PromotionType, TotalCharge, 1);
                        result = "True";
                    }
                    catch (Exception ex)
                    {
                        Response.Write("Insert Cancelltion Pro error: " + ex.Message);
                        Response.End();
                    }
                }


                // Insert Country
                if(!string.IsNullOrEmpty(Request.Form["ctl00$ContentPlaceHolder1$hd_country_selected"]))
                {
                    try
                    {
                        PromotionCountry cPromotionCountry = new PromotionCountry();
                        string[] arrCountry = Request.Form["ctl00$ContentPlaceHolder1$hd_country_selected"].Split(',');
                        if(arrCountry.Count() > 0)
                        {
                            foreach(string country in arrCountry)
                            {
                                cPromotionCountry.InsertPromotionCounty(intProductId, intPromotionId, short.Parse(country));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("Insert Country Targeting: " + ex.Message);
                        Response.End();
                    }
                }
                
            }
            catch (Exception ex)
            {
                Response.Write("Insert main error : " + ex.Message);
                Response.End();
            }


            //

            return result;
        }

        public string GenPromotionDetailXML()
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(Request.Form["benefitList"]))
            {
                string[] benefitVal = Request.Form["benefitList"].Split(',');

                
                result = result + "<PromotionShow>";
                result = result + "<head>Special Benefit</head>";
                result = result + "<List>";

                foreach (string benefit in benefitVal)
                {
                    result = result + "<item>" + Request.Form["hd_benefit_" + benefit].ToString().Hotels2SPcharacter_remove() + "</item>";
                }

                result = result + "</List>";
                result = result + "</PromotionShow>";

            }
            return result;
        }
    }
}