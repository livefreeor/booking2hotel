using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
using Hotels2thailand.ProductOption;

namespace Hotels2thailand.UI
{
    public partial class admin_product_construction : Hotels2BasePage
    {
        

         protected void Page_Load(object sender, EventArgs e)
         {
             if (!this.Page.IsPostBack)
             {
                 radioIsTimeCheckNo.Attributes.Add("onclick", "javascript:DisableDiv('DivTimeService');");
                 radioIsTimeCheckYes.Attributes.Add("onclick", "javascript:EnableDiv('DivTimeService');");

                 Product cProduct = new Product();
                 cProduct = cProduct.GetProductById(int.Parse(this.qProductId));
                 Destitle.Text = cProduct.DestinationTitle;
                 txthead.Text = cProduct.Title;

                 
                 //lnkOptionCreate.NavigateUrl = "option_add.aspx?pdcid=" + this.qProductCat + "&pid=" + this.qProductId;
                 dropcatInsertDataBind();
                 GVConstructionDataBind();
                 galaDetailBinding();

                 if (string.IsNullOrEmpty(this.qConstructionId))
                 {
                     screenBlock.Visible = true;
                 }
             }
         }

         public void GVConstructionDataBind()
         {
             ProductConstructionCategory cConCat = new ProductConstructionCategory();
             GvConScat.DataSource = cConCat.ConstructionCategoryByHaveConstructionrecord(int.Parse(this.qProductId));
             GvConScat.DataBind();
         }

         public void GvConScat_OnRowDataBound(object sender, GridViewRowEventArgs e)
         {
             if (e.Row.RowType == DataControlRowType.DataRow)
             {
                 GridView GVChild = e.Row.Cells[0].FindControl("gvChildConstruction") as GridView;

                 
                 byte bytConCat = (byte)DataBinder.Eval(e.Row.DataItem,"Key");
                 ProductConstruction cProductConstruction = new ProductConstruction();

                 GVChild.DataSource = cProductConstruction.GetConstructionByCatIdAndProductId(bytConCat, int.Parse(this.qProductId));
                 GVChild.DataBind();
             }
         }

        

         protected void gvChildConstruction_OnRowDataBound(object sender, GridViewRowEventArgs e)
         {
             if (e.Row.RowType == DataControlRowType.DataRow)
             {
                 Button btnDis = e.Row.Cells[0].FindControl("tbnDis") as Button;
                 HyperLink lnkTitle = e.Row.Cells[0].FindControl("lOption") as HyperLink;
                 bool bolStatus = (bool)DataBinder.Eval(e.Row.DataItem, "Status");
                 if (bolStatus == false)
                 {
                     lnkTitle.CssClass = "product_announcement_left_Gv_item_dis";
                     btnDis.Text = "Enable";
                 }

             }
         }


         public void dropcatInsertDataBind()
         {
             ProductConstructionCategory cProductConcat = new ProductConstructionCategory();
             dropcatInsert.DataSource = cProductConcat.ConstructionCategory();
             dropcatInsert.DataTextField = "Value";
             dropcatInsert.DataValueField = "Key";
             dropcatInsert.DataBind();
         }
         public void btnSave_Onclick(object sender, EventArgs e)
         {
             ProductConstruction cProductConStruction = new ProductConstruction
             {
                 ProductID = int.Parse(this.qProductId),
                 CategoryID = byte.Parse(dropcatInsert.SelectedValue),
                 Title = txtTitle.Text
             };

             cProductConStruction.Insert(cProductConStruction);

             txtTitle.Text = string.Empty;
             GVConstructionDataBind();
         }

         public void galaBtn_Cilck(object sender, EventArgs e)
         {
             Button btn = (Button)sender;
              if (btn.CommandName == "ancSave")
             {
                 string[] Argument = btn.CommandArgument.Split(',');
                 int intConStruc = int.Parse(Argument[0]);
                 //int RowIndex = int.Parse(Argument[1]);
                 //GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent.Parent;
                 GridViewRow GVParentIndex = (GridViewRow)(sender as Control).Parent.Parent.Parent.Parent.Parent.Parent;
                 GridViewRow GvChildInDex = (GridViewRow)(sender as Control).Parent.Parent;

                 int ParentIndex = GVParentIndex.RowIndex;
                 int ChildIndex = GvChildInDex.RowIndex;

                 //Response.Write(ChildIndex);
                 //Response.End();

                 GridView GridviewChild = GvConScat.Rows[ParentIndex].Cells[0].FindControl("gvChildConstruction") as GridView;

                 TextBox txttitle = GridviewChild.Rows[ChildIndex].Cells[0].FindControl("txtTitle") as TextBox;
                 ProductConstruction cProductConstruction = new ProductConstruction();
                 cProductConstruction.GetConstructionByID(intConStruc);
                 cProductConstruction.Title = txttitle.Text;
                 cProductConstruction.Update();

                 //string[] Argument = btn.CommandArgument.Split(',');
                 //int intConStruc = int.Parse(Argument[0]);
                 //int RowIndex = int.Parse(Argument[1]);
                 //foreach (GridViewRow itemcat in GvConScat.Rows)
                 //{
                 //    GridView GridviewChild = GvConScat.Rows[itemcat.RowIndex].Cells[0].FindControl("gvChildConstruction") as GridView;
                 //    foreach (GridViewRow item in GridviewChild.Rows)
                 //    {
                 //        if (item.RowType == DataControlRowType.DataRow)
                 //        {
                 //            if (item.RowIndex == RowIndex)
                 //            {
                 //                GridViewRow gvMasterRow = (GridViewRow)GridviewChild.Parent.Parent.Parent;
                 //                int ParentRowIndex = gvMasterRow.RowIndex;
                 //                Response.Write(ParentRowIndex);
                 //                TextBox txttitle = GridviewChild.Rows[item.RowIndex].Cells[0].FindControl("txtTitle") as TextBox;
                 //                ProductConstruction cProductConstruction = new ProductConstruction();
                 //                cProductConstruction.GetConstructionByID(intConStruc);
                 //                cProductConstruction.Title = txttitle.Text;
                 //                cProductConstruction.Update();

                 //            }
                 //        }
                 //    }
                 //}
             }

             if (btn.CommandName == "ancDis")
             {
                 string[] Argument = btn.CommandArgument.Split(',');
                 int intConStruc = int.Parse(Argument[0]);
                 int RowIndex = int.Parse(Argument[1]);

                 ProductConstruction cProductConstruction = new ProductConstruction();
                 cProductConstruction.GetConstructionByID(intConStruc);

                 if (cProductConstruction.Status)
                 {
                     cProductConstruction.Status = false;
                     cProductConstruction.Update();
                 }
                 else
                 {
                     cProductConstruction.Status = true;
                     cProductConstruction.Update();
                 }
             }
             Content_Lang_box.DataBind();
             GVConstructionDataBind();
             //Response.Redirect(Request.Url.ToString());
         }

        //--------------------------------------------------- Right 
         public void dropConCatDataBind()
         {
             ProductConstructionCategory  cProductConcat = new ProductConstructionCategory();
             dropConCat.DataSource = cProductConcat.ConstructionCategory();
             dropConCat.DataTextField = "Value";
             dropConCat.DataValueField = "Key";
             dropConCat.DataBind(); 
         }
         public void galaDetailBinding()
         {
             dropConCatDataBind();

             if (!string.IsNullOrEmpty(this.qConstructionId))
             {
                 drpHrsStart.DataSource = this.dicGetTimEHrs(23);
                 drpHrsStart.DataTextField = "Value";
                 drpHrsStart.DataValueField = "Key";
                 drpHrsStart.DataBind();

                 //drpMinsStart.DataSource = this.dicGetNumber(60);
                 //drpMinsStart.DataTextField = "Value";
                 //drpMinsStart.DataValueField = "Key";
                 //drpMinsStart.DataBind();

                 drpHrsEnd.DataSource = this.dicGetTimEHrs(23);
                 drpHrsEnd.DataTextField = "Value";
                 drpHrsEnd.DataValueField = "Key";
                 drpHrsEnd.DataBind();

                 //drpMinsEnd.DataSource = this.dicGetNumber(60);
                 //drpMinsEnd.DataTextField = "Value";
                 //drpMinsEnd.DataValueField = "Key";
                 //drpMinsEnd.DataBind();

                 ProductConstruction cConstruction = new ProductConstruction();
                 cConstruction.GetConstructionByID(int.Parse(this.qConstructionId));
                 lblHeadtitle.Text = cConstruction.Title;
                 dropConCat.SelectedValue = cConstruction.CategoryID.ToString();

                 if (cConstruction.TimeOpen == null || cConstruction.TimeClose == null)
                 {
                     radioIsTimeCheckNo.Checked = true;
                     radioIsTimeCheckYes.Checked = false;

                     drpHrsStart.SelectedValue = DateTime.Now.Hour.ToString();
                     drpMinsStart.SelectedValue = DateTime.Now.Minute.ToString();

                     drpHrsEnd.SelectedValue = DateTime.Now.Hour.ToString();
                     drpMinsEnd.SelectedValue = DateTime.Now.Minute.ToString();
                 }
                 else 
                 {
                     radioIsTimeCheckNo.Checked = false;
                     radioIsTimeCheckYes.Checked = true;
                     this.Page.ClientScript.RegisterStartupScript(Page.GetType(), null, "<script>showDiv('DivTimeService');</script>", false);
                     DateTime dDateOpen = (DateTime)cConstruction.TimeOpen;
                     DateTime dDateClose = (DateTime)cConstruction.TimeClose;

                     drpHrsStart.SelectedValue = dDateOpen.Hour.ToString();
                     drpMinsStart.SelectedValue = dDateOpen.Minute.ToString();

                     drpHrsEnd.SelectedValue = dDateClose.Hour.ToString();
                     drpMinsEnd.SelectedValue = dDateClose.Minute.ToString();
                 }
                 
             }
         }

         public void btnConSave_OnClick(object sender, EventArgs e)
         {
             if (radioIsTimeCheckNo.Checked)
             {
                ProductConstruction cProductConstruction = new ProductConstruction();
                cProductConstruction.GetConstructionByID(int.Parse(this.qConstructionId));
                 cProductConstruction.CategoryID = byte.Parse(dropConCat.SelectedValue);
                 cProductConstruction.TimeOpen = null;
                 cProductConstruction.TimeClose = null;
                 cProductConstruction.Update();
             }

             if (radioIsTimeCheckYes.Checked)
             {
                 DateTime dTimeStart = new DateTime(1900, 09, 09, int.Parse(drpHrsStart.SelectedValue), int.Parse(drpMinsStart.SelectedValue), 0);
                 DateTime dTimeEnd = new DateTime(1900, 09, 09, int.Parse(drpHrsEnd.SelectedValue), int.Parse(drpMinsEnd.SelectedValue), 0);

                 ProductConstruction cProductConstruction = new ProductConstruction();
                 cProductConstruction.GetConstructionByID(int.Parse(this.qConstructionId));
                 cProductConstruction.CategoryID = byte.Parse(dropConCat.SelectedValue);
                 cProductConstruction.TimeOpen = dTimeStart;
                 cProductConstruction.TimeClose = dTimeEnd;
                 cProductConstruction.Update();
             }
             
            Response.Redirect(Request.UrlReferrer.ToString());

         }
    }
}