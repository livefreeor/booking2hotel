using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Front;
using Hotels2thailand.Booking;
using Hotels2thailand.Production;
using Hotels2thailand;
using Hotels2thailand.ProductOption;

namespace Hotels2thailand.UI
{
    public partial class admin_ajax_room_manage_save : Hotels2BasePageExtra_Ajax
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.IsPostBack)
            {
                if (this.IsStaffAdd())
                {
                    Response.Write(RoomTypeINsert());
                }
                else
                {
                    Response.Write("method_invalid");
                }
                
                Response.Flush();
            }
        }

        public string RoomTypeINsert()
        {
            string result = "False";
            try
            {
                string MealFor = "(for adult)";
                if (Request.Form["chk_adult_child"] == "1")
                    MealFor = "(for children)";
                int OptionId = 0;
                string OptionTitle = Request.Form["option_title"];
                byte OptionCat = byte.Parse(Request.Form["dropOptionCAt"]);
                string OptionDetail = Request.Form["room_detail"];
                byte bytSize = 0;
                if (!string.IsNullOrEmpty(Request.Form["txt_size"]))
                    bytSize = byte.Parse(Request.Form["txt_size"]);

                byte bytPri = byte.Parse(Request.Form["txt_priority"]);

                if (OptionCat == 58)
                    OptionTitle = OptionTitle + MealFor;
                    

                Option cOption = new Option
                {
                    Title = OptionTitle,
                    Status = true,
                    ProductID = this.CurrentProductActiveExtra,
                    CategoryID = OptionCat,
                    Size = bytSize,
                    Priority = bytPri
                };

                OptionId = cOption.InsertOption(cOption);
                cOption.insertOptionMappingSupplier_ExtraNet(this.CurrentProductActiveExtra, OptionId, this.CurrentSupplierId);
                ProductOptionContent cOptionContent = new ProductOptionContent();
                cOptionContent.InsertOptionContentExtra(this.CurrentProductActiveExtra, OptionId, 1, OptionTitle, OptionDetail);


               
                if (!string.IsNullOrEmpty(Request.Form["amenresult"]))
                {
                    foreach(string facKey in Request.Form["amenresult"].Split(','))
                    {
                        ProductOptionFacility cFac = new ProductOptionFacility
                        {
                            OptionId = OptionId,
                            LangId = 1,
                            Title = Request.Form["txt_amen_" + facKey]
                        };
                        cFac.InsertNewOptionFacility(cFac);
                    }
                    
                    
                }
                result = "True";
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }


            return result;
        }
        

        
    }
}