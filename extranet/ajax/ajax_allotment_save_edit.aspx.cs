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
    public partial class admin_ajax_allotment_save_edit : Hotels2BasePageExtra_Ajax
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (this.Page.IsPostBack)
            {
                if (this.IsStaffEdit())
                {
                    Response.Write(UpdateAndInsertAllot());
                }
                else
                {
                    Response.Write("method_invalid");
                }
                Response.End();
                
            }
        }

        public string UpdateAndInsertAllot()
        {
            string result = "False";
            //Response.Write(Request.Form["chk_to_insert"] + "<br/>");
            //Response.Write(Request.Form["chk_to_update"]);
            //Response.End();
            try
            {
                if (!string.IsNullOrEmpty(Request.Form["chk_to_update"]))
                {
                    
                    
                    string[] AllotToUpdate = Request.Form["chk_to_update"].Split(',');

                    if (AllotToUpdate.Count() > 0)
                    {
                        foreach (string allot in AllotToUpdate)
                        {
                            int intAllotId = int.Parse(allot);
                            byte bytRoomAllot = byte.Parse(Request.Form["room_allot_" + allot]);
                            byte bytcutOff = byte.Parse(Request.Form["cutoff_allot_" + allot]);

                            bool bolStatus = true;

                            if (Request.Form["radio_status_" + allot] == "0")
                            {
                                bolStatus = false;
                            }

                            Staff cStaff = this.CurrentStaffobj;
                            Allotment cAllotment = new Allotment(this.CurrentProductActiveExtra);

                            cAllotment.UpdateAllotmentAndInsertNewActivityByAllotmentId(intAllotId, cStaff.Cat_Id, bytRoomAllot, bytcutOff, bolStatus);

                        }
                    }
                    result = "True";
                }
                
            }
            catch (Exception ex)
            {
                Response.Write("error#1#: Updateallot " + ex.Message);
                Response.End();
            }

                try
                {
                if (!string.IsNullOrEmpty(Request.Form["chk_to_insert"]))
                {

                    

                    string[] AllotToInsert = Request.Form["chk_to_insert"].Split(',');
                    if (AllotToInsert.Count() > 0)
                    {
                        foreach (string allot in AllotToInsert)
                        {
                            
                            int intOPtionId = int.Parse(allot.Split('%')[0]);
                            DateTime DateAllot = allot.Split('%')[1].Hotels2DateSplitYear("-");
                            byte bytRoomAllot = byte.Parse(Request.Form["room_allot_" + allot]);
                            byte bytcutOff = byte.Parse(Request.Form["cutoff_allot_" + allot]);

                            bool bolStatus = true;

                            if (Request.Form["radio_status_" + allot] == "0")
                            {
                                bolStatus = false;
                            }

                            Staff cStaff = this.CurrentStaffobj;
                            Allotment cAllotment = new Allotment(this.CurrentProductActiveExtra);

                            cAllotment.InsertNewallotandUpdateBydateAllot(this.CurrentSupplierId, intOPtionId, DateAllot, bytcutOff, cStaff.Cat_Id, bytRoomAllot, bolStatus);
                        }
                    }
                    result = "True";
                }
             
                
            }
            catch (Exception ex)
            {
                Response.Write("error#2#: Insert allot blank " + ex.Message);
                Response.End();
            }

                
           

            return result.ToString();

        }
    }
}