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
    public partial class admin_productOption_product_option_weekday : Hotels2BasePage
    {
       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                Product cProduct = new Product();
                cProduct = cProduct.GetProductById(int.Parse(this.qProductId));
                Destitle.Text = cProduct.DestinationTitle;
                txthead.Text = cProduct.Title;

                dropDownSupplierListDataBind();

                ProductOptionListDataBind();

                chkOptionListDataBind();

                DropoptionShowListDataBind();

                GVSupplementListDataBind();
            }
        }

        public void dropDownSupplierListDataBind()
        {
            ProductSupplier cProductSUp = new ProductSupplier();
            dropDownSupplierList.DataSource = cProductSUp.getSupplierListByProductIdAndActiveOnly(int.Parse(this.qProductId));
            dropDownSupplierList.DataTextField = "SupplierTitle";
            dropDownSupplierList.DataValueField = "SupplierID";
            dropDownSupplierList.DataBind();
        }

        public void dropDownSupplierList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropoptionShowListDataBind();
            chkOptionListDataBind();
            ProductOptionListDataBind();

            GVSupplementListDataBind();
        }


        public void templateOpen_OnClick(object sender, EventArgs e)
        {
        }

        public void btSave_onClick(object sender, EventArgs e)
        {
            short shrSupplierId = short.Parse(dropDownSupplierList.SelectedValue);
            decimal decSun = int.Parse(txtSun.Text);
            decimal decMon = int.Parse(txtMon.Text);
            decimal decTue = int.Parse(txtTue.Text);
            decimal decWed = int.Parse(txtWed.Text);
            decimal decThu = int.Parse(txtThu.Text);
            decimal decFri = int.Parse(txtFri.Text);
            decimal decSat = int.Parse(txtSat.Text);
            DateTime dDateStart = DatePicker.GetDatetStart;
            DateTime dDateEnd = DatePicker.GetDatetEnd;


            int count = 0;
            foreach (ListItem item in chkOptionList.Items)
            {
                if (item.Selected)
                {
                    count = count + 1;
                }
            }

            if (count > 0)
            {
                
                foreach (ListItem checkeditem in chkOptionList.Items)
                {
                    if (checkeditem.Selected)
                    {
                        ProductOptionSupplementDay.insertNewWeekDay(shrSupplierId, int.Parse(checkeditem.Value), dDateStart, dDateEnd, decSun, decMon, decTue, decWed, decThu, decFri, decSat, true);
                    }
                }
            }
            else
            {
                
                int intOptionSelected = int.Parse(ProductOptionList.SelectedValue);
                if (intOptionSelected == 0)
                {
                    Option cOption = new Option();
                    
                    foreach (Option cOptionItem in cOption.GetProductOptionByCurrentSupplierANDProductId(shrSupplierId,int.Parse(this.qProductId)))
                    {
                        ProductOptionSupplementDay.insertNewWeekDay(shrSupplierId, cOptionItem.OptionID, dDateStart, dDateEnd, decSun, decMon, decTue, decWed, decThu, decFri, decSat,true);
                    }
                }
                else
                {
                    ProductOptionSupplementDay.insertNewWeekDay(shrSupplierId, intOptionSelected, dDateStart, dDateEnd, decSun, decMon, decTue, decWed, decThu, decFri, decSat, true);
                }
            }

            GVSupplementListDataBind();


        }

        public void ProductOptionListDataBind()
        {
            Option cOption = new Option();
            short shrSupplierId = short.Parse(dropDownSupplierList.SelectedValue);
            ProductOptionList.DataSource = cOption.GetProductOptionByCurrentSupplierANDProductId(shrSupplierId, int.Parse(this.qProductId));
            ProductOptionList.DataTextField = "Title";
            ProductOptionList.DataValueField = "OptionID";

            ProductOptionList.DataBind();
            ListItem NewList = new ListItem("All Option", "0");
            ProductOptionList.Items.Insert(0, NewList);
        }

        public void chkOptionListDataBind()
        {
            short shrSupplierId = short.Parse(dropDownSupplierList.SelectedValue);
            Option cOption = new Option();
            chkOptionList.DataSource = cOption.GetProductOptionByCurrentSupplierANDProductId(shrSupplierId, int.Parse(this.qProductId));
            chkOptionList.DataTextField = "Title";
            chkOptionList.DataValueField = "OptionID";
            chkOptionList.DataBind();

        }

        public void DropoptionShowListDataBind()
        {
            short shrSupplierId = short.Parse(dropDownSupplierList.SelectedValue);
            Option cOption = new Option();
            DropoptionShowList.DataSource = cOption.GetProductOptionByCurrentSupplierANDProductId(shrSupplierId, int.Parse(this.qProductId));
            DropoptionShowList.DataTextField = "Title";
            DropoptionShowList.DataValueField = "OptionID";

            DropoptionShowList.DataBind();

        }


        public void GVSupplementListDataBind()
        {
            short shrSupplierId = short.Parse(dropDownSupplierList.SelectedValue);

            int OptionIdNull = 0;
            if (DropoptionShowList.Items.Count > 0)
            {
                OptionIdNull = int.Parse(DropoptionShowList.SelectedValue);
            }

            ProductOptionSupplementDay cSupDay = new ProductOptionSupplementDay();
            GVSupplementList.DataSource = cSupDay.getWeekdayListBySupplierIdAndOptionId(shrSupplierId, OptionIdNull);
            GVSupplementList.DataBind();
        }

        public void GVSupplementList_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Hotels2thailand.UI.Controls.Control_DatepickerCalendar dDatePicker =
                    e.Row.Cells[1].FindControl("dDatePickerEdit") as Hotels2thailand.UI.Controls.Control_DatepickerCalendar;

                DateTime DateStart = (DateTime)DataBinder.Eval(e.Row.DataItem, "DateStart");
                DateTime DateEnd = (DateTime)DataBinder.Eval(e.Row.DataItem, "DateEnd");
                dDatePicker.DateStart = DateStart;
                dDatePicker.DateEnd = DateEnd;
                dDatePicker.DataBind();

                bool bolStatus = (bool)DataBinder.Eval(e.Row.DataItem, "Status");
                ImageButton disBtn = e.Row.Cells[3].FindControl("btDelete") as ImageButton;

                if (!bolStatus)
                {
                    disBtn.ImageUrl = "~/images/false.png";
                    e.Row.CssClass = "RowDisable";
                }
                else
                {
                    disBtn.ImageUrl = "~/images/true.png";
                }

            }
        }

        public void Supplement_OnClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
           
            if (btn.CommandName == "dDateEdit")
            {
                foreach (GridViewRow item in GVSupplementList.Rows)
                {
                    string[] Argument = btn.CommandArgument.Split(',');

                    int intSuppleID = int.Parse(Argument[0]);
                    int RowIndex = int.Parse(Argument[1]);

                    if (item.RowIndex == RowIndex)
                    {
                        Hotels2thailand.UI.Controls.Control_DatepickerCalendar dDatePicker =
                             GVSupplementList.Rows[item.RowIndex].Cells[1].FindControl("dDatePickerEdit") as Hotels2thailand.UI.Controls.Control_DatepickerCalendar;

                       
                        ProductOptionSupplementDay cSuppleDay = new ProductOptionSupplementDay();
                        cSuppleDay.getWeekdayById(intSuppleID);
                        cSuppleDay.DateStart = dDatePicker.GetDatetStart;
                        cSuppleDay.DateEnd = dDatePicker.GetDatetEnd;
                        cSuppleDay.Update();
                    }
                }
            }
            if (btn.CommandName == "DayAmountEdit")
            {
                foreach (GridViewRow item in GVSupplementList.Rows)
                {
                    string[] Argument = btn.CommandArgument.Split(',');

                    int intSuppleID = int.Parse(Argument[0]);
                    int RowIndex = int.Parse(Argument[1]);

                    if (item.RowIndex == RowIndex)
                    {
                        TextBox txtSun = GVSupplementList.Rows[item.RowIndex].Cells[2].FindControl("txtSun") as TextBox;
                        TextBox txtMon = GVSupplementList.Rows[item.RowIndex].Cells[2].FindControl("txtMon") as TextBox;
                        TextBox txtTue = GVSupplementList.Rows[item.RowIndex].Cells[2].FindControl("txtTue") as TextBox;
                        TextBox txtWed = GVSupplementList.Rows[item.RowIndex].Cells[2].FindControl("txtWed") as TextBox;
                        TextBox txtThu = GVSupplementList.Rows[item.RowIndex].Cells[2].FindControl("txtThu") as TextBox;
                        TextBox txtFri = GVSupplementList.Rows[item.RowIndex].Cells[2].FindControl("txtFri") as TextBox;
                        TextBox txtSat = GVSupplementList.Rows[item.RowIndex].Cells[2].FindControl("txtSat") as TextBox;
                        ProductOptionSupplementDay cSuppleDay = new ProductOptionSupplementDay();
                        cSuppleDay.getWeekdayById(intSuppleID);
                        cSuppleDay.DaySun = decimal.Parse(txtSun.Text);
                        cSuppleDay.DayMon = decimal.Parse(txtMon.Text);
                        cSuppleDay.DayTue = decimal.Parse(txtTue.Text);
                        cSuppleDay.DayWed = decimal.Parse(txtWed.Text);
                        cSuppleDay.DayThu = decimal.Parse(txtThu.Text);
                        cSuppleDay.DayFri = decimal.Parse(txtFri.Text);
                        cSuppleDay.DaySat = decimal.Parse(txtSat.Text);
                        cSuppleDay.Update();
                    }
                }
            }

            GVSupplementListDataBind();
        }
        public void DropoptionShowList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            GVSupplementListDataBind();
        }

        //public void GVSupplementList_OnRowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "del")
        //    {
        //        int SupplementdayId = int.Parse(e.CommandArgument.ToString());
        //        ProductOptionSupplementDay.DeleteSupplementDay(SupplementdayId);
        //    }

        //    GVSupplementListDataBind();
        //}

        public void btStatusUp_OnClick(object sender, EventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            //if (e.CommandName == "del")
            //{
            //    int SupplementdayId = int.Parse(e.CommandArgument.ToString());
            //    ProductOptionSupplementDay.DeleteSupplementDay(SupplementdayId);
            //}

            if (btn.CommandName == "dis")
            {
                string[] argument = btn.CommandArgument.Split(',');
                int SupId = int.Parse(argument[0]);
                bool bolstatus = bool.Parse(argument[1]);
                int Rowindex = int.Parse(argument[2]);

                ProductOptionSupplementDay cSupDay = new ProductOptionSupplementDay();
                cSupDay = cSupDay.getWeekdayById(SupId);
                if (cSupDay.Status)
                {
                    cSupDay.Status = false;
                }
                else
                {
                    cSupDay.Status = true;
                }
                cSupDay.Update();

            }

            //GVSupplementListDataBind();
            Response.Redirect(Request.Url.ToString());
        }

    }
}