using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Staffs;

namespace Hotels2thailand.UI
{
    public partial class extranet_addnewstaff : Hotels2BasePageExtra
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                ChkProductList();


                Suppliers.Supplier cSupplier = new Suppliers.Supplier();
                cSupplier = cSupplier.getSupplierById(this.CurrentSupplierId);
                txtSurfixSup.Text = cSupplier.SuffixUserExtra;

                hdsurfix_supplier.Value = cSupplier.SuffixUserExtra.ToString();
            }
        }

        public void ChkProductList()
        {
            StaffProduct_Extra cStaffProduct = new StaffProduct_Extra();
            if (string.IsNullOrEmpty(this.qProductId))
            {
                chkListProduct.DataSource = cStaffProduct.getProductByStaffId(this.CurrentStaffId);
            }
            else
            {
                chkListProduct.DataSource = cStaffProduct.getProductExtraByProductId(int.Parse(this.qProductId));
            }

            chkListProduct.DataBind();
            
        }

        public void btnAddNewStaff_Onclick(object sender, EventArgs e)
        {

            StaffAuthorizeExtra StaffAuthoriExtra = new StaffAuthorizeExtra();
            StaffProduct_Extra cStaffProduct = new StaffProduct_Extra();
            StaffPageAuthorizeExtra cStaffModule = new StaffPageAuthorizeExtra();
            StaffExtra cStaff = new StaffExtra();
            //Product cProduct = new Product();


            string Username = txtUsername.Text.Trim() + "@" + hdsurfix_supplier.Value.Trim();
            // Insert staff
            short intStaffId = cStaff.InSertNewStaff_Extranet(txtFullName.Text, Username, txtPassword.Text, txtEmails.Text);
            


            // mapping staff and Product to manage
            foreach (ListItem item in chkListProduct.Items)
            {
                if(item.Selected)
                    cStaffProduct.InsertStaffProduct(intStaffId, int.Parse(item.Value));
            }

            // add admin user Top of user 
            StaffAuthoriExtra.insertStaffAuthorize(intStaffId, 2);

            //add module and method rateControl
            if (chkRateControl.Checked)
                cStaffModule.intsertStaffModule(intStaffId, 2, byte.Parse(dropMethodRateControl.SelectedValue));

            //add module and method Package
            if (chkPackage.Checked)
                cStaffModule.intsertStaffModule(intStaffId, 8, byte.Parse(dropMethodPackageControl.SelectedValue));

            //add module and method member
            if (chkMember.Checked)
                cStaffModule.intsertStaffModule(intStaffId, 9, byte.Parse(dropMethodPackageControl.SelectedValue));

            //add module and method Promotion
            if (chkPromotion.Checked)
                cStaffModule.intsertStaffModule(intStaffId, 3, byte.Parse(dropMethodPromotion.SelectedValue));
            //add module and method allotment
            if (chkAllotment.Checked)
                cStaffModule.intsertStaffModule(intStaffId, 4, byte.Parse(dropMethodAllotment.SelectedValue));

            //add module and method ordercenter
            if(chkOrdercenter.Checked)
                cStaffModule.intsertStaffModule(intStaffId, 5, byte.Parse(dropMethodOrdercenter.SelectedValue));

            //add module and method REview
            if (chkReview.Checked)
                cStaffModule.intsertStaffModule(intStaffId, 7, byte.Parse(dropMethodreview.SelectedValue));


            //add module and method Report
            if (checkReport.Checked)
                cStaffModule.intsertStaffModule(intStaffId, 6, byte.Parse(dropMethodReport.SelectedValue));

            //add module and method Newsletter
            if (checkNews.Checked)
                cStaffModule.intsertStaffModule(intStaffId, 10, byte.Parse(dropMethodNews.SelectedValue));

            string strAppendQuery = this.AppendCurrentQueryString();
            if (Request.QueryString.Count == 0)
            {
                

                if (!string.IsNullOrEmpty(this.qProductId) && !string.IsNullOrEmpty(this.qSupplierId))
                {
                    strAppendQuery = "?" + strAppendQuery.Hotels2LeftClr(1);
                    Response.Redirect(this.BaseModultPath_BlueHouse + "staffmanage/stafflist.aspx" + strAppendQuery);
                }
                else
                {
                    Response.Redirect(this.BaseModultPath + "staffmanage/stafflist.aspx" + strAppendQuery);
                }
            }

            if (Request.QueryString.Count > 0)
            {
                strAppendQuery = "?" + strAppendQuery.Hotels2LeftClr(1);

                if (!string.IsNullOrEmpty(this.qProductId) && !string.IsNullOrEmpty(this.qSupplierId))
                {
                    Response.Redirect(this.BaseModultPath_BlueHouse + "staffmanage/stafflist.aspx" + strAppendQuery);
                }
                else
                {
                    Response.Redirect(this.BaseModultPath + "staffmanage/stafflist.aspx" + strAppendQuery);
                }
            }

            //Response.Redirect(this.BaseModultPath + "stafflist.aspx" + this.AppendCurrentQueryString());
        }
    }
}