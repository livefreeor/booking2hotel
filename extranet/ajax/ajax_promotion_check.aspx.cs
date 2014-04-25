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
    public partial class admin_ajax_promotion_check : Hotels2BasePageExtra_Ajax
    {

        public string qPromotionId {
            get { return Request.QueryString["pro"]; }
        }

        public string qProGroupId
        {
            get { return Request.QueryString["pg"]; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (this.Page.IsPostBack)
            {
                //if (this.IsstaffDelete())
                //{
                if (!string.IsNullOrEmpty(this.qProGroupId) && string.IsNullOrEmpty(this.qPromotionId))
                {
                       
                    Response.Write(PromotionCheck());

                }

                if (!string.IsNullOrEmpty(this.qProGroupId) && !string.IsNullOrEmpty(this.qPromotionId))
                {
                    Response.Write(PromotionCheckEditMode());
                }
                //}
                //else
                //{
                //    Response.Write("method_invalid");
                //}
                Response.End();
            }
            
        }

        public string PromotionCheck()
        {

            string result = string.Empty;
            try
            {
                //        if (GetValueQueryString("pro") == "") {
                //    ItemVal = $("input[name='pro_item_select']:checked").val();
                //} else {
                //    ItemVal = $("#ctl00$ContentPlaceHolder1$hd_promotion_group_item").val();
                //}

                string Proitem = string.Empty;
                if (!string.IsNullOrEmpty(Request.Form["pro_item_select"]))
                {
                    Proitem = Request.Form["pro_item_select"];
                }
                else
                {
                    Proitem = Request.Form["ctl00$ContentPlaceHolder1$hd_promotion_group_item"];
                }

                byte bytDayMin = 0;

                byte bytDayAdVance =  byte.Parse(Request.Form["sel_advance_day"]);

                DateTime dDAteStart = Request.Form["hd_Stay_start"].Hotels2DateSplitYear("-");
                DateTime dDateEnd = Request.Form["hd_Stay_End"].Hotels2DateSplitYear("-");

                PromotionConditionExtranet cPromotionCon = new PromotionConditionExtranet();
                byte bytProGroup = byte.Parse(this.qProGroupId);


                switch (Proitem)
                {
                    case "6":
                    case "7":
                    case "8":
                    case "9":
                        bytDayMin = byte.Parse(Request.Form["free_night_stay"]);
                        break;
                    case "13":
                    case "14":
                        bytDayMin = byte.Parse(Request.Form["sel_consec_night"]);
                        break;
                    default:
                        bytDayMin = byte.Parse(Request.Form["sel_min_day"]);
                        break;


                }

                //Response.Write(bytProGroup);
                //Response.End();

                if (bytProGroup == 1)
                {
                    //Response.Write(bytProGroup);
                    //Response.End();
                    IList<object> ListPromotion = cPromotionCon.PromotionConditionDuplicatCheck_Advance(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                       bytDayMin, dDAteStart, dDateEnd, bytDayAdVance);

                    if (ListPromotion.Count > 0)
                    {
                        foreach (PromotionConditionExtranet con in ListPromotion)
                        {
                            result = result + con.ConditionId + ",";
                        }

                    }
                }
                else if (bytProGroup == 4)
                {
                    IList<object> ListPromotion = cPromotionCon.PromotionConditionDuplicatCheck_Hotdeal(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                       dDAteStart, dDateEnd);

                    if (ListPromotion.Count > 0)
                    {
                        foreach (PromotionConditionExtranet con in ListPromotion)
                        {
                            result = result + con.ConditionId + ",";
                        }

                    }
                }
                else
                {
                    IList<object> ListPromotion = cPromotionCon.PromotionConditionDuplicatCheck(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                      bytDayMin, dDAteStart, dDateEnd);

                    if (ListPromotion.Count > 0)
                    {
                        foreach (PromotionConditionExtranet con in ListPromotion)
                        {
                            result = result + con.ConditionId + ",";
                        }

                    }
                }


            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                Response.End();
            }

            return result.Trim().Hotels2RightCrl(1);
        }

        public string PromotionCheckEditMode()
        {

            string result = string.Empty;
            try
            {
                //        if (GetValueQueryString("pro") == "") {
                //    ItemVal = $("input[name='pro_item_select']:checked").val();
                //} else {
                //    ItemVal = $("#ctl00$ContentPlaceHolder1$hd_promotion_group_item").val();
                //}

                string Proitem = string.Empty;
                if (!string.IsNullOrEmpty(Request.Form["pro_item_select"]))
                {
                    Proitem = Request.Form["pro_item_select"];
                }
                else
                {
                    Proitem = Request.Form["ctl00$ContentPlaceHolder1$hd_promotion_group_item"];
                }

                byte bytDayMin = 0;

                byte bytDayAdVance = byte.Parse(Request.Form["sel_advance_day"]);

                DateTime dDAteStart = Request.Form["hd_Stay_start"].Hotels2DateSplitYear("-");
                DateTime dDateEnd = Request.Form["hd_Stay_End"].Hotels2DateSplitYear("-");

                PromotionConditionExtranet cPromotionCon = new PromotionConditionExtranet();
                byte bytProGroup = byte.Parse(this.qProGroupId);
                int intPromotionId = int.Parse(this.qPromotionId);

                switch (Proitem)
                {
                    case "6":
                    case "7":
                    case "8":
                    case "9":
                        bytDayMin = byte.Parse(Request.Form["free_night_stay"]);
                        break;
                    case "13":
                    case "14":
                        bytDayMin = byte.Parse(Request.Form["sel_consec_night"]);
                        break;
                    default:
                        bytDayMin = byte.Parse(Request.Form["sel_min_day"]);
                        break;


                }

                

                if (bytProGroup == 1)
                {

                    //Response.Write(this.CurrentProductActiveExtra + "<br/>" + this.CurrentSupplierId + "<br/>"
                    //   + bytDayMin + "<br/>" + dDAteStart + "<br/>" + dDateEnd + "<br/>" + bytDayAdVance + "<br/>" + intPromotionId);
                    //Response.End();
                    IList<object> ListPromotion = cPromotionCon.PromotionConditionDuplicatCheck_Advance_editMode(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                       bytDayMin, dDAteStart, dDateEnd, bytDayAdVance, intPromotionId);

                    if (ListPromotion.Count > 0)
                    {
                        Response.Write(ListPromotion.Count);
                        foreach (PromotionConditionExtranet con in ListPromotion)
                        {
                            result = result + con.ConditionId + ",";
                        }

                    }
                }
                else if (bytProGroup == 4)
                {
                    IList<object> ListPromotion = cPromotionCon.PromotionConditionDuplicatCheck_Hotdeal_editMode(this.CurrentProductActiveExtra, this.CurrentSupplierId, dDAteStart, dDateEnd, intPromotionId);

                    if (ListPromotion.Count > 0)
                    {
                        foreach (PromotionConditionExtranet con in ListPromotion)
                        {
                            result = result + con.ConditionId + ",";
                        }

                    }
                }
                else
                {
                    IList<object> ListPromotion = cPromotionCon.PromotionConditionDuplicatCheck_editMode(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                      bytDayMin, dDAteStart, dDateEnd, intPromotionId);

                    if (ListPromotion.Count > 0)
                    {
                        foreach (PromotionConditionExtranet con in ListPromotion)
                        {
                            result = result + con.ConditionId + ",";
                        }

                    }
                }


            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                Response.End();
            }

            return result.Trim().Hotels2RightCrl(1);
        }
        
    }
}