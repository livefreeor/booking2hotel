using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Hotels2thailand.Production;
using Hotels2thailand.ProductOption;

namespace Hotels2thailand.UI
{
    public partial class admin_product_option_allotment_edit : Hotels2BasePage
    {
        //private admin_product_option_allotment allottmentPage;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                Product cProduct = new Product();
                cProduct = cProduct.GetProductById(int.Parse(this.qProductId));
                Destitle.Text = cProduct.DestinationTitle;
                txthead.Text = cProduct.Title;


                

                //Option cOption = new Option();
                //int intOptionId = int.Parse(this.qOptionId);
                //lblRoomTitle.Text = cOption.getOptionById(intOptionId).Title;

                GVAllotListDataBind();
                //ScriptManager.RegisterStartupScript(this, Page.GetType(), null, "<script>SemiHacky();</script>", false);
                
            }
        }

        public void GVAllotListDataBind()
        {
            if (!string.IsNullOrEmpty(this.qOptionId) && !string.IsNullOrEmpty(this.qSupplierId))
            {
                //allottmentPage = (admin_product_option_allotment)Context.Handler;
                
                string intOptionId = this.qOptionId.Hotels2DecryptedData().Hotels2RightCrl(1);
                Option cOption = new Option();
                GvOptionList.DataSource = cOption.getOptionListByOptionArray(intOptionId);
                GvOptionList.DataBind();

                

            }
            
        }

        public void GvOptionList_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView GvAllot = e.Row.Cells[0].FindControl("GVAllotList") as GridView;
                int intOptionId = (int)DataBinder.Eval(e.Row.DataItem, "Key");
                Allotment cAllotment = new Allotment();

                DateTime dDatestart = Request.QueryString["ds"].Hotels2DateSplitYear("-");
                DateTime dDateEnd = Request.QueryString["de"].Hotels2DateSplitYear("-");
                //Response.Write(dDatestart);
                //Response.End();
                short shrSupplierId = short.Parse(this.qSupplierId);
                GvAllot.DataSource = cAllotment.getAllotMentListByOptionId(intOptionId, shrSupplierId, dDatestart, dDateEnd);
                GvAllot.DataBind();
            }

        }

        public void GVAllotList_OnRowdataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList dropNumRoom = e.Row.Cells[2].FindControl("dropNumRoom") as DropDownList;
                DropDownList dropcutoff = e.Row.Cells[3].FindControl("dropcutoff") as DropDownList;
                RadioButtonList radioStatus = e.Row.Cells[4].FindControl("radioStatus") as RadioButtonList;
                //Image imgstatus = e.Row.Cells[4].FindControl("imgstatus") as Image;

                dropNumRoom.DataSource = this.dicGetNumberstart0(10);
                dropNumRoom.DataTextField = "Value";
                dropNumRoom.DataValueField = "Key";
                dropNumRoom.DataBind();

                dropcutoff.DataSource = this.dicGetNumberstart0(90);
                dropcutoff.DataTextField = "Value";
                dropcutoff.DataValueField = "Key";
                dropcutoff.DataBind();

                bool Status = (bool)DataBinder.Eval(e.Row.DataItem, "Status");
                if (Status)
                {
                    radioStatus.SelectedValue = "1";
                    
                }
                else
                {
                    radioStatus.SelectedValue = "0";
                    
                }
                int intNumDateCutOff = (int)DataBinder.Eval(e.Row.DataItem, "NumDateCutOff");
                int intTotalQuantity = (int)DataBinder.Eval(e.Row.DataItem, "TotalQuantity");

                dropNumRoom.SelectedValue = intTotalQuantity.ToString();
                dropcutoff.SelectedValue = intNumDateCutOff.ToString();
            }
        }

        public void btnQuickSave_OnClick(object sender, EventArgs e)
        {
            foreach (GridViewRow GvOptionRow in GvOptionList.Rows)
            {
                GridView GvChildAllot = GvOptionList.Rows[GvOptionRow.RowIndex].Cells[0].FindControl("GVAllotList") as GridView;
                foreach (GridViewRow GvchildAllotRow in GvChildAllot.Rows)
                {
                    int AllotmentID = (int)GvChildAllot.DataKeys[GvchildAllotRow.RowIndex].Value;
                    DropDownList dropNumRoom = GvChildAllot.Rows[GvchildAllotRow.RowIndex].Cells[2].FindControl("dropNumRoom") as DropDownList;
                    DropDownList dropcutoff = GvChildAllot.Rows[GvchildAllotRow.RowIndex].Cells[3].FindControl("dropcutoff") as DropDownList;
                    RadioButtonList radioStatus = GvChildAllot.Rows[GvchildAllotRow.RowIndex].Cells[4].FindControl("radioStatus") as RadioButtonList;
                    byte bytQuantity = byte.Parse(dropNumRoom.SelectedValue);
                    byte bytDateCutoff = byte.Parse(dropcutoff.SelectedValue);
                    bool Status = true;
                    if (radioStatus.SelectedValue == "0")
                    {
                        Status = false;
                    }
                    Allotment cAllotment = new Allotment();

                    cAllotment.UpdateAllotmentAndInsertNewActivityByAllotmentId(AllotmentID, 1, bytQuantity, bytDateCutoff, Status);

                    
                }
            }
            Response.Redirect(Request.Url.ToString());
        }


        public void btnSave_OnClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int intAllotmentId = int.Parse(btn.CommandArgument);
            GridViewRow GvRow = (GridViewRow)(sender as Control).Parent.Parent;
            GridViewRow GvParentRow = (GridViewRow)(sender as Control).Parent.Parent.Parent.Parent.Parent.Parent;


            int GvParentRowRowInDex = GvParentRow.RowIndex;
            int GvChildRowRowInDex = GvRow.RowIndex;

            GridView GridviewChild = GvOptionList.Rows[GvParentRowRowInDex].Cells[0].FindControl("GVAllotList") as GridView;

            DropDownList dropNumRoom = GridviewChild.Rows[GvChildRowRowInDex].Cells[2].FindControl("dropNumRoom") as DropDownList;
            DropDownList dropcutoff = GridviewChild.Rows[GvChildRowRowInDex].Cells[3].FindControl("dropcutoff") as DropDownList;
            RadioButtonList radioStatus = GridviewChild.Rows[GvChildRowRowInDex].Cells[4].FindControl("radioStatus") as RadioButtonList;

            byte bytQuantity = byte.Parse(dropNumRoom.SelectedValue);
            byte bytDateCutoff = byte.Parse(dropcutoff.SelectedValue);

            bool Status = true;
            if(radioStatus.SelectedValue == "0" )
            {
                Status = false;
            }

            Allotment cAllotment = new Allotment();

            cAllotment.UpdateAllotmentAndInsertNewActivityByAllotmentId(intAllotmentId, 1, bytQuantity, bytDateCutoff, Status);

            Response.Redirect(Request.Url.ToString());
            
        }

    }
}