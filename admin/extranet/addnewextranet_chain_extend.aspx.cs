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
    public partial class admin_extranet_addnewextranet_chain_extend : Hotels2BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                StaffChain cStaffChain = new StaffChain();

                dropChain.DataSource = cStaffChain.GetStaffChain();
                dropChain.DataTextField = "Value";
                dropChain.DataValueField = "Key";
                dropChain.DataBind();

                ListItem item = new ListItem("Select Chain", "0");
                dropChain.Items.Insert(0, item);
            }
        }

        public void btnhotelSearch_Onclick(object sender, EventArgs e)
        {
            Product cProduct = new Product();
            string strProductCode = "\'" + txthotelCode.Text.Trim() + "\'";

            GVHoteltoExtraResult.DataSource  = cProduct.getProductCustomByHotelCode(strProductCode);
            GVHoteltoExtraResult.DataBind();
        }


        public void GVHoteltoExtraResult_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int intProductId = (int)GVHoteltoExtraResult.DataKeys[e.Row.RowIndex].Value;
                //short shrSupplierId = (short)DataBinder.Eval(e.Row.DataItem, "SupplierPrice");
                Button btnActive = e.Row.Cells[0].FindControl("btnActiveExtraNet") as Button;
                bool IsExtra = (bool)DataBinder.Eval(e.Row.DataItem, "IsExtranet");

                //if (IsExtra)
                //{
                //    btnActive.Enabled = false;
                //    btnActive.Text = "Already Extranet Partners";
                //}

            }
        }


        public void btnActiveExtraNet_Onclick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string[] Argument = btn.CommandArgument.Split(',');

            int intProductId = int.Parse(Argument[0]);
            short shrSupplierID = short.Parse(Argument[1]);

            Product cProduct = new Product();
            cProduct = cProduct.GetProductById(intProductId);

            Supplier cSupplier = new Supplier();
            cSupplier = cSupplier.getSupplierById(shrSupplierID);

            lblSupplierTitle.Text = cSupplier.SupplierTitle;
            lblProducttitle.Text = cProduct.Title;

            hdSupplierID.Value = shrSupplierID.ToString();
            hdProductId.Value = intProductId.ToString();

            StaffExtra cStaff = new StaffExtra();
            ArrayList StaffAdminUserLsit = cStaff.supplierCheckAlreadyExtra(shrSupplierID);
            int StaffAdminUserCount = StaffAdminUserLsit.Count;

            //Response.Write(StaffAdminUserCount);
            //Response.End();
            if (StaffAdminUserCount > 0)
            {
                panelListHotel.Visible = false;
                panelUserAdd.Visible = true;
                userForm.Visible = false;
            }

            if (StaffAdminUserCount == 0)
            {
                panelListHotel.Visible = false;
                panelUserAdd.Visible = true;
                userForm.Visible = false;
            }

            //if (StaffAdminUserCount > 1)
            //{
            //    Response.Write(StaffAdminUserCount);
            //    Response.End();
            //}
            


        }

       

        public void btnSave_Onclick(object sender, EventArgs e)
        {
            //Response.Write("--");
            //Response.Write(Request.Form["checked_commission"]);
            //Response.End();
            if (dropChain.SelectedValue != "0")
            {
                StaffAuthorizeExtra StaffAuthoriExtra = new StaffAuthorizeExtra();
                StaffExtra cStaff = new StaffExtra();
                StaffProduct_Extra cStaffProduct = new StaffProduct_Extra();
                StaffPageAuthorizeExtra cStaffModule = new StaffPageAuthorizeExtra();
                Product cProduct = new Product();
                Supplier cSupplier = new Supplier();
                StaffChain cStaffChain = new StaffChain();




                //ArrayList StaffAdminUserLsit = cStaff.supplierCheckAlreadyExtra(short.Parse(hdSupplierID.Value));

                //int StaffAdminUserCount = StaffAdminUserLsit.Count;
                //if (StaffAdminUserCount > 0)
                //{
                //    foreach (string ProductVal in hdProductId.Value.Split(','))
                //    {
                //        // mapping staff and Product to manage
                //        cStaffProduct.InsertStaffProduct((short)StaffAdminUserLsit[0], int.Parse(ProductVal));

                //        // Active Product To Extranet
                //        cProduct.UpdateProductToExtranet(int.Parse(ProductVal), true);
                //    }


                //    panelListHotel.Visible = false;
                //    panelUserAdd.Visible = false;
                //    panelAddSuccess.Visible = true;
                //}
                short Chain = short.Parse(dropChain.SelectedValue);
                StaffExtra cStaffEx = new StaffExtra();
                short StaffAdmin = cStaffEx.GetStaffAdmin(Chain);

                
                //if (StaffAdminUserCount == 0)
                //{
               // string UserName = "Admin@" + txtsurffix.Text;

                string SufixCurrent= cSupplier.GEtSuffixByChainId(Chain);

                
                cSupplier.UpdateSupplierSurfix(short.Parse(hdSupplierID.Value), SufixCurrent);

                    //short intStaffId = cStaff.InSertNewStaff_Extranet(txtName.Text, UserName, txtPassword.Text, txtEmail.Text);


                    int intProductId = int.Parse(hdProductId.Value);
                    // mapping staff and Product to manage
                    cStaffProduct.InsertStaffProduct(StaffAdmin, intProductId);

                    // Active Product To Extranet
                    cProduct.UpdateProductToExtranet(intProductId, true);


                    //insert staff Chain
                    cStaffChain.intSertProductChain(Chain, intProductId);


                    //foreach (string ProductVal in hdProductId.Value.Split(','))
                    //{

                    //    int intProductId = int.Parse(ProductVal);
                    //    // mapping staff and Product to manage
                    //    cStaffProduct.InsertStaffProduct(intStaffId, intProductId);

                    //    // Active Product To Extranet
                    //    cProduct.UpdateProductToExtranet(intProductId, true);


                    //    //insert staff Chain
                    //    cStaffChain.intSertProductChain(short.Parse(dropChain.SelectedValue), intProductId);
                    //}

                    // add admin user Top of user 
                    //StaffAuthoriExtra.insertStaffAuthorize(intStaffId, 1);

                    //add modul And rol for top user
                    //cStaffModule.intsertStaffModule(intStaffId, 1, 4);
                    //cStaffModule.intsertStaffModule(intStaffId, 2, 4);
                    //cStaffModule.intsertStaffModule(intStaffId, 3, 4);
                    //cStaffModule.intsertStaffModule(intStaffId, 4, 4);
                    //cStaffModule.intsertStaffModule(intStaffId, 5, 4);
                    //cStaffModule.intsertStaffModule(intStaffId, 6, 4);

                    panelListHotel.Visible = false;
                    panelUserAdd.Visible = false;
                    panelAddSuccess.Visible = true;


                    //if (!string.IsNullOrEmpty(Request.Form["checked_commission"]))
                    //{
                    //    ProductCommission cproduct = new ProductCommission();
                    //    string[] arrKeys = Request.Form["checked_commission"].Split(',');
                    //    foreach (string Key in arrKeys)
                    //    {
                    //        DateTime dDateStart = Request.Form["hd_rate_date_form_" + Key].Hotels2DateSplitYear("-");
                    //        DateTime dDateEnd = Request.Form["hd_rate_date_To_" + Key].Hotels2DateSplitYear("-");
                    //        byte bytCom = byte.Parse(Request.Form["hd_amount_" + Key]);


                    //        cproduct.Insertnewcommission(int.Parse(hdProductId.Value), short.Parse(hdSupplierID.Value), dDateStart,
                    //            dDateEnd, bytCom);
                    //    }
                    //}

                //}

                //Insert Default Holidays
                PublicholidayExtranet cPublicExtra = new PublicholidayExtranet();
                ProductsupplementExtranet cHolidaysSupplement = new ProductsupplementExtranet();

                IList<object> iListHolidays = cPublicExtra.getAllPublicHolidaysByCurrentDate();
                if (iListHolidays.Count() > 0)
                {
                    foreach (PublicholidayExtranet holidays in iListHolidays)
                    {
                        cHolidaysSupplement.InsertOptionSupplement(short.Parse(hdSupplierID.Value), int.Parse(hdProductId.Value), holidays.Title, holidays.HolidayDate);
                    }
                }

                //if (StaffAdminUserCount > 1)
                //{
                //}

                Response.Redirect("/admin/product/produt-list.aspx");
            }
            else
            {

                panelCannetInsertChain.Visible = true;
                panelUserAdd.Visible = false;
            }
            
        }

        public void btnCancel_Onclick(object sender, EventArgs e)
        {
            panelUserAdd.Visible = false;
        }

        public void btnBack_Onclick(object sender, EventArgs e)
        {
            panelUserAdd.Visible = true;
            panelCannetInsertChain.Visible = false;
        }
    }
}