using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hotels2thailand.Production;

namespace Hotels2thailand.UI
{
    public partial class admin_account_sales_manage : Hotels2BasePage
    {
        public string qSalesId { get { return Request.QueryString["sal"]; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                Sales_Com_type cComType = new Sales_Com_type();
                dropComType.DataSource = cComType.getDicComType();
                dropComType.DataTextField = "Value";
                dropComType.DataValueField = "Key";
                dropComType.DataBind();

                Product_sales cSales = new Product_sales();
                gvSales.DataSource = cSales.getSaleList();
                gvSales.DataBind();

                lblSaleName.Text = "Add New Sales";

                if (!string.IsNullOrEmpty(this.qSalesId))
                {
                    byte bytSaleId = byte.Parse(this.qSalesId);
                    cSales = cSales.getSales(bytSaleId);
                    lblSaleName.Text = cSales.SaleName;
                    txtSalesName.Text = cSales.SaleName;
                    txtComval.Text = cSales.Commission.ToString("#,##0.00");
                    txtPhone.Text = cSales.Phone;
                    txtFax.Text = cSales.Fax;
                    txtmail.Text = cSales.Email;
                    txtComment.Text = cSales.Comment;
                    dropComType.SelectedValue = cSales.ComType.ToString();

                }
            }

        }



        protected void btnSaveStatus_Click(object sender, EventArgs e)
        {
            Product_sales cSales = new Product_sales {
                SaleName = txtSalesName.Text.Trim(),
                ComType = byte.Parse(dropComType.SelectedValue),
                Commission = decimal.Parse(txtComval.Text),
                Phone = txtPhone.Text,
                Fax = txtFax.Text,
                Email = txtmail.Text,
                Comment = txtComment.Text
                 
            };

            if (!string.IsNullOrEmpty(this.qSalesId))
            {
                byte bytSaleId = byte.Parse(this.qSalesId);

                cSales.SaleId = bytSaleId;
                cSales.UpdateSales(cSales);
                //Update Mode
               // Product_sales
                Response.Redirect(Request.Url.ToString());
                
            }
            else
            {
                int SaleId = cSales.InsertNewSales(cSales);
                //Insert Mode
                Response.Redirect("sale_manage.aspx?sal=" + SaleId);
            }
        }
}
}