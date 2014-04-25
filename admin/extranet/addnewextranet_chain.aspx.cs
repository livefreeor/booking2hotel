using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
using Hotels2thailand.Suppliers;
using Hotels2thailand.Staffs;

namespace Hotels2thailand.UI
{
    public partial class admin_extranet_addnewextranet_chain : Hotels2BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void btnhotelSearch_Onclick(object sender, EventArgs e)
        {
            Product cProduct = new Product();
            string strProductCode = txthotelCode.Text.Trim();
            string[] arrProductCode = strProductCode.Split(',');
            if (arrProductCode.Count() > 1)
            {
                strProductCode = string.Empty;
                foreach (string code in arrProductCode)
                {
                    strProductCode = strProductCode + "\'" + code + "\',";
                }

                strProductCode = strProductCode.Hotels2RightCrl(1);
            }
            else
            {
               strProductCode =  "\'" + txthotelCode.Text.Trim() + "\'";
            }

            IList<object> iListProduct = cProduct.getProductCustomByHotelCode(strProductCode);

            GVHoteltoExtraResult.DataSource = iListProduct;
            GVHoteltoExtraResult.DataBind();

            //int count = 0;
            //foreach (Product product in iListProduct)
            //{
            //    if (product.IsExtranet)
            //        count = count + 1;
            //}
            //if (count > 0)
            //    panelCannotAdd.Visible = true;
            //else
                panelUserAdd.Visible = true;  

        }


        public void GVHoteltoExtraResult_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int intProductId = (int)GVHoteltoExtraResult.DataKeys[e.Row.RowIndex].Value;
                //short shrSupplierId = (short)DataBinder.Eval(e.Row.DataItem, "SupplierPrice");
                Button btnActive = e.Row.Cells[0].FindControl("btnActiveExtraNet") as Button;
                bool IsExtra = (bool)DataBinder.Eval(e.Row.DataItem, "IsExtranet");
                btnActive.Visible = false;
                //if (IsExtra)
                //{
                //    //btnActive.Enabled = false;
                //    //btnActive.Text = "Already Extranet Partners";
                //}
                //else
                //{
                //    btnActive.Visible = false;
                //}

            }
        }



        public void btnSave_Onclick(object sender, EventArgs e)
        {
            //Response.Write("--");
            //Response.Write(Request.Form["checked_commission"]);
            //Response.End();

            StaffAuthorizeExtra StaffAuthoriExtra = new StaffAuthorizeExtra();
            StaffExtra cStaff = new StaffExtra();
            StaffProduct_Extra cStaffProduct = new StaffProduct_Extra();
            StaffPageAuthorizeExtra cStaffModule = new StaffPageAuthorizeExtra();
            Product cProduct = new Product();
            Supplier cSupplier = new Supplier();
            StaffChain cStaffChain = new StaffChain();
            PublicholidayExtranet cPublicExtra = new PublicholidayExtranet();
            ProductsupplementExtranet cHolidaysSupplement = new ProductsupplementExtranet();
            //ArrayList StaffAdminUserLsit = cStaff.supplierCheckAlreadyExtra(short.Parse(hdSupplierID.Value));
            string UserName = "Admin@" + txtsurffix.Text;
            short intStaffId = cStaff.InSertNewStaff_Extranet(txtName.Text, UserName, txtPassword.Text, txtEmail.Text);
            short intChain = cStaffChain.insertChain(txtChain.Text);
            foreach (GridViewRow GVRow in GVHoteltoExtraResult.Rows)
            {
                if (GVRow.RowType == DataControlRowType.DataRow)
                {

                    HiddenField hdSupplierId = (HiddenField)GVHoteltoExtraResult.Rows[GVRow.RowIndex].Cells[1].FindControl("hdSupplierID");
                    int intProductId = (int)GVHoteltoExtraResult.DataKeys[GVRow.RowIndex].Value;
                   

                    //Insert Chain
                    cStaffChain.intSertProductChain(intChain, intProductId);

                    //Update Suffix To Supplier
                    cSupplier.UpdateSupplierSurfix(short.Parse(hdSupplierId.Value), txtsurffix.Text);

                    // mapping staff and Product to manage
                    cStaffProduct.InsertStaffProduct(intStaffId, intProductId);

                    // Active Product To Extranet
                    cProduct.UpdateProductToExtranet(intProductId, true);


                    //Insert Product Commission
                    //if (!string.IsNullOrEmpty(Request.Form["checked_commission"]))
                    //{
                    //    ProductCommission cproduct = new ProductCommission();
                    //    string[] arrKeys = Request.Form["checked_commission"].Split(',');
                    //    foreach (string Key in arrKeys)
                    //    {
                    //        DateTime dDateStart = Request.Form["hd_rate_date_form_" + Key].Hotels2DateSplitYear("-");
                    //        DateTime dDateEnd = Request.Form["hd_rate_date_To_" + Key].Hotels2DateSplitYear("-");
                    //        byte bytCom = byte.Parse(Request.Form["hd_amount_" + Key]);


                    //        cproduct.Insertnewcommission(intProductId, short.Parse(hdSupplierId.Value), dDateStart,
                    //            dDateEnd, bytCom);
                    //    }
                    //}


                    //Insert Default Holidays
                    IList<object> iListHolidays = cPublicExtra.getAllPublicHolidaysByCurrentDate();
                    if (iListHolidays.Count() > 0)
                    {
                        foreach (PublicholidayExtranet holidays in iListHolidays)
                        {
                            cHolidaysSupplement.InsertOptionSupplement(short.Parse(hdSupplierId.Value), int.Parse(hdSupplierId.Value), holidays.Title, holidays.HolidayDate);
                        }
                    }

                }
            }

            // add admin user Top of user 
            StaffAuthoriExtra.insertStaffAuthorize(intStaffId, 1);

            //add modul And rol for top user
            cStaffModule.intsertStaffModule(intStaffId, 1, 4);
            cStaffModule.intsertStaffModule(intStaffId, 2, 4);
            cStaffModule.intsertStaffModule(intStaffId, 3, 4);
            cStaffModule.intsertStaffModule(intStaffId, 4, 4);
            cStaffModule.intsertStaffModule(intStaffId, 5, 4);
            cStaffModule.intsertStaffModule(intStaffId, 6, 4);
            cStaffModule.intsertStaffModule(intStaffId, 7, 4);
            cStaffModule.intsertStaffModule(intStaffId, 8, 4);

            panelListHotel.Visible = false;
            panelUserAdd.Visible = false;
            panelAddSuccess.Visible = true;
            
        }

        public void btnCancel_Onclick(object sender, EventArgs e)
        {
            panelUserAdd.Visible = false;
        }
    }
}