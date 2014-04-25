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
    public partial class admin_ajax_allotment_save_insertbox : Hotels2BasePageExtra_Ajax
    {
        public string qRoomCustom
        {
            get { return Request.QueryString["custom"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (this.Page.IsPostBack)
            {
                if (this.IsStaffAdd())
                {

                    Response.Write(btnAllotmentSave());
                    //Response.Write("HELLO");
                }
                else
                {
                    Response.Write("method_invalid");
                }
                Response.End();
                
            }
        }

        public string btnAllotmentSave()
        {
            string result = "False";
            try
            {
                Option cOption = new Option();
                Staff cStaff = this.CurrentStaffobj;
                
                string chkRoom = Request.Form["ctl00$ContentPlaceHolder1$dropOptionInsert"];
                int intRoomSelect = int.Parse(Request.Form["ctl00$ContentPlaceHolder1$dropOptionInsert"]);

                string RoomCheckList = string.Empty;
                

                Allotment cAllotment = new Allotment(this.CurrentProductActiveExtra);

                short shrSupplierId = this.CurrentSupplierId;
                DateTime dDateStart = Request.Form["hd_insert_date_start"].Hotels2DateSplitYear("-");
                DateTime dDateEnd = Request.Form["hd_insert_date_end"].Hotels2DateSplitYear("-");

                byte bytCutoff = byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$dropCutoff"]);
                byte bytAllot = byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$dropAllot"]);

                
                short shrstaffSupplier = this.CurrentSupplierId;

                string RoomresultDisplay = "";
                if (!string.IsNullOrEmpty(this.qRoomCustom))
                {
                    RoomresultDisplay = this.qRoomCustom.Trim();
                    foreach (string strRoomItem in this.qRoomCustom.Trim().Hotels2RightCrl(1).Split(','))
                    {
                        cAllotment.InsertNewallotandUpdateBydateRange(shrSupplierId, int.Parse(strRoomItem), dDateStart, dDateEnd, bytCutoff, cStaff.Cat_Id, bytAllot, true);
                    }
                }
                else
                {
                    if (intRoomSelect == 0)
                    {
                        List<object> ListRoom = cOption.GetProductOptionByProductId_RoomOnlyExtranet(this.CurrentProductActiveExtra, shrstaffSupplier);
                        foreach (Option OptionItem in ListRoom)
                        {
                            cAllotment.InsertNewallotandUpdateBydateRange(shrSupplierId, OptionItem.OptionID, dDateStart, dDateEnd, bytCutoff, cStaff.Cat_Id, bytAllot, true);
                        }
                    }
                    else
                    {
                        cOption = cOption.getOptionById(intRoomSelect);
                        cAllotment.InsertNewallotandUpdateBydateRange(shrSupplierId, intRoomSelect, dDateStart, dDateEnd, bytCutoff, cStaff.Cat_Id, bytAllot, true);
                        //RoomresultDisplay = cOption.OptionID + ",";
                    }
                }

                
                result = "True";
            }
            catch (Exception ex)
            {
                Response.Write("error#1#: Insert All Allot" + ex.Message);
                Response.End();
            }

            return result;
            

        }
        
    }
}