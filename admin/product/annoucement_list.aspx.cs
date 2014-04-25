using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;

namespace Hotels2thailand.UI
{
    public partial class admin_product_annoucement_list : Hotels2BasePage
    {
         protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                GridAnnoucementDataBind();
                if (!string.IsNullOrEmpty(this.qAnnc_Id))
                {
                    int intAnncId = int.Parse(this.qAnnc_Id);
                    Annoucement cAnnc = new Annoucement();
                    cAnnc = cAnnc.getAnnouncementById(intAnncId);

                    dDatePicker.DateStart = cAnnc.DateStart;
                    dDatePicker.DateEnd = cAnnc.DateEnd;
                    dDatePicker.DataBind();
                }
                else
                {
                    screenBlock.Visible = true;
                }
            }
        }

         public void GridAnnoucementDataBind()
         {
             Annoucement cAnnc = new Annoucement();
             GridAnnoucement.DataSource = cAnnc.getAnnounceMentListByProductId(int.Parse(this.qProductId));
             GridAnnoucement.DataBind();
         }

         public void GridAnnoucement_OnRowDataBound(object sender, GridViewRowEventArgs e)
         {
             if (e.Row.RowType == DataControlRowType.DataRow)
             {
                 Button btnDis = e.Row.Cells[0].FindControl("tbnDis") as Button;
                 HyperLink lnkTitle = e.Row.Cells[0].FindControl("lnkTitle") as HyperLink;
                 bool bolStatus = (bool)DataBinder.Eval(e.Row.DataItem, "Status");
                 if (bolStatus == false)
                 {
                     lnkTitle.CssClass = "product_announcement_left_Gv_item_dis";
                     btnDis.Text = "Enable";
                 }

             }
         }

         public void btnSave_OnClick(object sender, EventArgs e)
         {
            int ReturnVal =  Annoucement.InsertAnnounceMentFirstStep(int.Parse(this.qProductId),txtTitle.Text);

            GridAnnoucementDataBind();
            controlLang.DataBind();
         }

         public void SaveDateRange_OnClick(object sender, EventArgs e)
         {
             if (!string.IsNullOrEmpty(this.qAnnc_Id))
             {
                 Annoucement.UpdateDateRange(int.Parse(this.qAnnc_Id), dDatePicker.GetDatetStart, dDatePicker.GetDatetEnd);
             }

             Response.Redirect(Request.UrlReferrer.ToString());
         }

         public void AnncBtn_Cilck(object sender, EventArgs e)
         {
             Button btn = (Button)sender;

             if (btn.CommandName == "ancSave")
             {
                 string[] Argument = btn.CommandArgument.Split(',');
                 int intAnncId = int.Parse(Argument[0]);
                 int RowIndex = int.Parse(Argument[1]);

                 foreach (GridViewRow item in GridAnnoucement.Rows)
                 {
                     if (item.RowType == DataControlRowType.DataRow)
                     {
                         if (item.RowIndex == RowIndex)
                         {
                             TextBox txttitle = GridAnnoucement.Rows[item.RowIndex].Cells[0].FindControl("txtTitle") as TextBox;
                             Annoucement.UpdateTitle(intAnncId, txttitle.Text);
                         }
                     }
                 }
             }

             if (btn.CommandName == "ancDis")
             {
                 string[] Argument = btn.CommandArgument.Split(',');
                 int intAnncId = int.Parse(Argument[0]);
                 int RowIndex = int.Parse(Argument[1]);

                 Annoucement cAnn = new Annoucement();
                 cAnn.getAnnouncementById(intAnncId);
                 if (cAnn.Status)
                 {
                     Annoucement.UpdateStatus(intAnncId, false);
                 }
                 else
                 {
                     Annoucement.UpdateStatus(intAnncId, true);
                 }
             }

             Response.Redirect(Request.UrlReferrer.ToString());
         }
    }
}