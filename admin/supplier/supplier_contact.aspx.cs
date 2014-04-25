using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Suppliers;

namespace Hotels2thailand.UI
{
    public partial class admin_supplier_supplier_contact : Hotels2BasePage
    {
       

        protected void Page_Load(object sender, EventArgs e)
        {
            //chkBoxSupplier.Attributes.Add("onclick", "javascript:fnCheckUnCheck('" + chkBoxSupplier.ClientID + "');");
            //Button1.Attributes.Add("onkeydown", "javascript:abc('onclick');");
            if (!this.Page.IsPostBack)
            {
                GvDepartmentDataBind();
                DropDepDataBind();
                dropPhontCatDataBind();

                dropcatInsertDataBind();

                panelContactDetailDataBind();

                

            }
        }

        public void DropDepDataBind()
        {
            SupplierContact cSupplierContact = new SupplierContact();
            DropDep.DataSource = cSupplierContact.getdicDepartment();
            DropDep.DataTextField = "Value";
            DropDep.DataValueField = "Key";
            DropDep.DataBind();
        }

        public void dropPhontCatDataBind()
        {
            dropPhontCat.DataSource = SupplierContact.getPhoneCat();
            dropPhontCat.DataTextField = "Value";
            dropPhontCat.DataValueField = "Key";
            dropPhontCat.DataBind();
        }

        


        //================= LEFT PAN ==============================
        //============== INSERT BOX =======================


        public void btnSave_Onclick(object sender, EventArgs e)
        {
            short shrSupplierId = short.Parse(this.qSupplierId);
            byte bytDepId = byte.Parse(dropcatInsert.SelectedValue);
            int InsertContact = SupplierContact.InsertStaffContact(shrSupplierId, bytDepId, txtTitleName.Text, string.Empty, true);

            //Response.Redirect(Request.Url.ToString());
            
            GvDepartmentDataBind();
            
            
        }

        public void  dropcatInsertDataBind()
        {
            SupplierContact cSupplierContact = new SupplierContact();
            dropcatInsert.DataSource = cSupplierContact.getdicDepartment();
            dropcatInsert.DataTextField = "Value";
            dropcatInsert.DataValueField = "Key";
            dropcatInsert.DataBind();
            
        }
        //============== CONTACT LIST BOX =======================
        public void GvDepartmentDataBind()
        {
            SupplierContact cSupplierContact = new SupplierContact();
            GvDepartment.DataSource = cSupplierContact.getdicDepartmentHaveRecord(short.Parse(this.qSupplierId));
            GvDepartment.DataBind();
        }

        public void GvDepartment_OnRowdataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                byte bytDepId = (byte)GvDepartment.DataKeys[e.Row.RowIndex].Value;
                GridView GvChild = e.Row.Cells[0].FindControl("GVContactList") as GridView;
                SupplierContact cSupplierContact = new SupplierContact();
                GvChild.DataSource = cSupplierContact.getSupplierStaffContactListByDepId(short.Parse(this.qSupplierId),bytDepId);
                GvChild.DataBind();
            }
        }

        public void GVContactList_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                bool bolStatus = (bool)DataBinder.Eval(e.Row.DataItem, "status");

                HyperLink Hypertaff = e.Row.Cells[0].FindControl("lstaff") as HyperLink;
                if (!bolStatus)
                {
                    Hypertaff.CssClass = "contactStaffDis";
                }
            }
        }
       //========================================================

       // RIGHT PAN
       //====================== PANEL CONTACT DETAIL ==============

        public void panelContactDetailDataBind()
        {
            if (!string.IsNullOrEmpty(this.qcontactId))
            {
                txteditLocal.Text = "2";

                int intstaffId = int.Parse(this.qcontactId);
                SupplierContact cSupplierContact = new SupplierContact();


                lblHeadtitle.Text = cSupplierContact.getSupplierStaffContactbyId(intstaffId).title;
                DropDep.SelectedValue = cSupplierContact.getSupplierStaffContactbyId(intstaffId).department_id.ToString();
                txtTitle.Text = cSupplierContact.getSupplierStaffContactbyId(intstaffId).title;
                txtcomment.Text = cSupplierContact.getSupplierStaffContactbyId(intstaffId).comment;

                if (cSupplierContact.getSupplierStaffContactbyId(intstaffId).status)
                {
                    radiocontactenable.Checked = true;
                    radiocontactDisable.Checked = false;
                }
                else
                {
                    radiocontactDisable.Checked = true;
                    radiocontactenable.Checked = false;
                }

                GVPhoneListDataBind();
                GvEmailDataBind();
            }
            else
            {
                screenBlock.Visible = true;
                panelContactInsert.Visible = false;
            }
        }
        public void GVPhoneListDataBind()
        {
            int intstaffId = int.Parse(this.qcontactId);
            GVPhoneList.DataSource = SupplierContact.GetListPhoneListByStaffContactId(intstaffId);
            GVPhoneList.DataBind();
        }

        public void GVPhoneList_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPhone = e.Row.Cells[0].FindControl("lablePhone") as Label;

                int intStaffId = (int)DataBinder.Eval(e.Row.DataItem, "staff_id");
                byte bytCatId = (byte)DataBinder.Eval(e.Row.DataItem, "cat_id");
                string strPhone = (string)DataBinder.Eval(e.Row.DataItem, "phone_number");
                string strcountryCode = (string)DataBinder.Eval(e.Row.DataItem, "code_country");
                string strlocalcode = (string)DataBinder.Eval(e.Row.DataItem, "code_local");
                bool bolStatus = (bool)DataBinder.Eval(e.Row.DataItem, "status");

                string CatTitle = SupplierContact.GetCatTitlebyCatId(bytCatId);

                string strPhoneShowformat = CatTitle + "&nbsp;:&nbsp;(" + strcountryCode + ")" + strlocalcode + "-" + strPhone;
                lblPhone.Text = strPhoneShowformat;

                DropDownList dropPhontCatEdit = e.Row.Cells[0].FindControl("dropPhontCatEdit") as DropDownList;
                dropPhontCatEdit.DataSource = SupplierContact.getPhoneCat();
                dropPhontCatEdit.DataTextField = "Value";
                dropPhontCatEdit.DataValueField = "Key";
                dropPhontCatEdit.DataBind();
                dropPhontCatEdit.SelectedValue = bytCatId.ToString();

                TextBox txtCountryCode = e.Row.Cells[0].FindControl("txteditCountryCodeEdit") as TextBox;
                txtCountryCode.Text = strcountryCode;
                TextBox txtLocalCode = e.Row.Cells[0].FindControl("txteditLocalEdit") as TextBox;
                txtLocalCode.Text = strlocalcode;
                TextBox txtPhone = e.Row.Cells[0].FindControl("txtPhoneEdit") as TextBox;
                txtPhone.Text = strPhone;

                Button btnDisable = e.Row.Cells[0].FindControl("btnDisable") as Button;

                if (!bolStatus)
                {
                    btnDisable.Text = "Enable";
                    lblPhone.CssClass = "headPhoneCatDis";
                }
                else
                {
                    lblPhone.CssClass = "headPhoneCat";
                }

            }
        }

        public void btnSavephone_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.qcontactId))
            {
                int intstaffId = int.Parse(this.qcontactId);
                byte bytPhoneCat = byte.Parse(dropPhontCat.SelectedValue);
                string strCountryCode = txteditCountryCode.Text;
                string strLocalCode = txteditLocal.Text;
                string strPhone = txtPhone.Text;

                int insertPhone = SupplierContact.InsertStaffPhone(bytPhoneCat, intstaffId, strCountryCode, strLocalCode, strPhone, true);
            }

            GVPhoneListDataBind();
        }

        public void GvEmailDataBind()
        {
            int intstaffId = int.Parse(this.qcontactId);
            GvEmail.DataSource = SupplierContact.GetListEmailByStaffContactId(intstaffId);
            GvEmail.DataBind();
        }

        public void GvEmail_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblEmail = e.Row.Cells[0].FindControl("LabelEmail") as Label;
                string strEmail = (string)DataBinder.Eval(e.Row.DataItem, "email");
                bool bolStatus = (bool)DataBinder.Eval(e.Row.DataItem, "status");
                lblEmail.Text = strEmail;

                Button btnEmailsaveeditDis = e.Row.Cells[0].FindControl("btnEmailsaveeditDis") as Button;
                if (!bolStatus)
                {
                    btnEmailsaveeditDis.Text = "Enable";
                    lblEmail.CssClass = "headPhoneCatDis";
                }
                else
                {
                    lblEmail.CssClass = "headPhoneCat";
                }
            }
        }

        public void btnEmailsave_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.qcontactId))
            {
                int intstaffId = int.Parse(this.qcontactId);
                string strEmail = txteditemail.Text;

                int intInsertEmail = SupplierContact.InsertStaffEmail(intstaffId, strEmail, true);
            }

            GvEmailDataBind();
        }

        public void dropPhontCat_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropPhontCat.SelectedValue == "1")
            {
                txteditLocal.Text = "2";
            }
            else if (dropPhontCat.SelectedValue == "2")
            {
                txteditLocal.Text = "81";
            }
            else if (dropPhontCat.SelectedValue == "3")
            {
                txteditLocal.Text = "2";
            }
        }

        public void btnContactsave_Onclick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.qcontactId))
            {
                int intstaffId = int.Parse(this.qcontactId);
                byte DepId= byte.Parse(DropDep.SelectedValue);

                bool bolStatus = true;
                if (radiocontactDisable.Checked)
                {
                    bolStatus = false;
                }



                SupplierContact.UpdateStaffContact(intstaffId, DepId, txtTitle.Text, txtcomment.Text, bolStatus);
            }
            Response.Redirect(Request.Url.ToString());
        }

        public void btnSavephoneEdit_OnClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string[] argument = btn.CommandArgument.Split(',');
            int PhoneId = int.Parse(argument[0]);
            int RowIndex = int.Parse(argument[1]);

            if (btn.CommandName == "phoneEdit")
            {
                foreach (GridViewRow item in GVPhoneList.Rows)
                {
                    if (RowIndex == item.RowIndex)
                    {
                        DropDownList dropPhontCatEdit = GVPhoneList.Rows[item.RowIndex].Cells[0].FindControl("dropPhontCatEdit") as DropDownList;
                        TextBox txtCountryCode = GVPhoneList.Rows[item.RowIndex].Cells[0].FindControl("txteditCountryCodeEdit") as TextBox;
                        TextBox txtLocalCode = GVPhoneList.Rows[item.RowIndex].Cells[0].FindControl("txteditLocalEdit") as TextBox;
                        TextBox txtPhone = GVPhoneList.Rows[item.RowIndex].Cells[0].FindControl("txtPhoneEdit") as TextBox;

                        bool update = SupplierContact.UpdateStaffPhone(PhoneId, byte.Parse(dropPhontCatEdit.SelectedValue), txtCountryCode.Text, txtLocalCode.Text, txtPhone.Text);
                    }
                }
            }

            if (btn.CommandName == "phoneDis")
            {
                foreach (GridViewRow item in GVPhoneList.Rows)
                {
                    if (RowIndex == item.RowIndex)
                    {
                        SupplierContact cSupplierContact = new SupplierContact();
                        if (cSupplierContact.GetStaffPhonebyId(PhoneId).status)
                        {
                            SupplierContact.UpdateStaffPhoneStatus(PhoneId, false);

                        }
                        else
                        {
                            SupplierContact.UpdateStaffPhoneStatus(PhoneId, true);
                        }
                    }
                }
            }

            if (btn.CommandName == "PhoneDel")
            {
                foreach (GridViewRow item in GVPhoneList.Rows)
                {
                    if (RowIndex == item.RowIndex)
                    {
                        SupplierContact.DelStaffContactPhone(PhoneId);
                    }
                }
            }

            GVPhoneListDataBind();

        }

        public void btnEmailsaveedit_OnClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string[] argument = btn.CommandArgument.Split(',');
            int EmailId = int.Parse(argument[0]);
            int RowIndex = int.Parse(argument[1]);

            if (btn.CommandName == "EmailEdit")
            {
                foreach (GridViewRow item in GvEmail.Rows)
                {
                    if (RowIndex == item.RowIndex)
                    {
                        TextBox txteditemailedit = GvEmail.Rows[item.RowIndex].Cells[0].FindControl("txteditemailedit") as TextBox;
                        SupplierContact.UpdateStaffEmail(EmailId, txteditemailedit.Text);
                    }
                }
            }

            if (btn.CommandName == "EmailDis")
            {
                foreach (GridViewRow item in GvEmail.Rows)
                {
                    if (RowIndex == item.RowIndex)
                    {
                        SupplierContact cSupplierContact = new SupplierContact();
                        if (cSupplierContact.GetStaffEmailById(EmailId).status)
                        {
                            SupplierContact.UpdateStaffEmailStatus(EmailId, false);
                        }
                        else
                        {
                            SupplierContact.UpdateStaffEmailStatus(EmailId, true);
                        }
                    }
                }
            }

            if (btn.CommandName == "Emaildel")
            {
                foreach (GridViewRow item in GvEmail.Rows)
                {
                    if (RowIndex == item.RowIndex)
                    {
                        SupplierContact.DelStaffEmail(EmailId);
                    }
                }
            }

            GvEmailDataBind();
        }
    }
}