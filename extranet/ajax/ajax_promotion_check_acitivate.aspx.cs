using System;
using System.Collections;
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
    public partial class admin_ajax_promotion_check_acitivate : Hotels2BasePageExtra_Ajax
    {


        public string qPromotionID
        {
            get { return Request.QueryString["pro"]; }
        }

        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (this.IsStaffEdit())
                {
                    if (!string.IsNullOrEmpty(this.qPromotionID))
                    {
                        Response.Write(PromotionCheck());

                    }
                }
                else
                {
                    Response.Write("method_invalid");
                }
                Response.End();
            }
            
        }

        public string PromotionCheck()
        {

            string result = "True";
            
            try
            {
                int intPromotionId = int.Parse(this.qPromotionID);

                PromotionExxtranet cProExtra = new PromotionExxtranet();
                ArrayList arrPromotion = cProExtra.GetPromotionCheckById(intPromotionId);
                string Proitem = arrPromotion[6].ToString();


                byte bytDayMin = (byte)arrPromotion[7];

                short bytDayAdVance = (short)arrPromotion[8];

                DateTime dDAteStart = (DateTime)arrPromotion[3];
                DateTime dDateEnd = (DateTime)arrPromotion[4];

                PromotionConditionExtranet cPromotionCon = new PromotionConditionExtranet();
                byte bytProGroup = (byte)arrPromotion[5];



                if (bytProGroup == 1)
                {

                    IList<object> ListPromotion = cPromotionCon.PromotionConditionDuplicatCheck_Advance(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                       bytDayMin, dDAteStart, dDateEnd, bytDayAdVance);

                    if (ListPromotion.Count > 0)
                    {
                        result = "False";
                        //foreach (PromotionConditionExtranet con in ListPromotion)
                        //{
                        //    result = result + con.ConditionId + ",";
                        //}

                    }
                }
                else if (bytProGroup == 4)
                {
                    IList<object> ListPromotion = cPromotionCon.PromotionConditionDuplicatCheck_Hotdeal(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                       dDAteStart, dDateEnd);

                    if (ListPromotion.Count > 0)
                    {
                        result = "False";
                        //foreach (PromotionConditionExtranet con in ListPromotion)
                        //{
                        //    result = result + con.ConditionId + ",";
                        //}

                    }
                 }
                else
                {
                    IList<object> ListPromotion = cPromotionCon.PromotionConditionDuplicatCheck(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                      bytDayMin, dDAteStart, dDateEnd);

                    if (ListPromotion.Count > 0)
                    {
                        result = "False";
                        //foreach (PromotionConditionExtranet con in ListPromotion)
                        //{
                        //    result = result + con.ConditionId + ",";
                        //}

                    }
                }


                
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                Response.End();
            }

            return result;
        }


        
    }
}