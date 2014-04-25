using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Staffs;
using Hotels2thailand.UI;


namespace Hotels2thailand.UI.Controls
{
    public partial class Control_StaffAddEditBox : System.Web.UI.UserControl
    {
        private byte _DropCatSelectdValue;
        public byte DropDoenCatSelecedValue
        {
            get
            {
                DropDownList DropCatFromFv = FVStaffAddEdit.FindControl("dropCatId") as DropDownList;
                _DropCatSelectdValue = Convert.ToByte(DropCatFromFv.SelectedValue);
                return _DropCatSelectdValue;
            }
        }

        //private string _type_staff;
        //public string StaffTypeCheck
        //{
        //    get 
        //    { 
        //        if(!string.IsNullOrEmpty(this.QueryString))
        //        {
        //            Staff clStaff = new Staff();
        //            clStaff.getStaffById(this.shrQueryString);
        //            _type_staff = clStaff.CatTitle;

        //            if (clStaff.Cat_Id == 6)
        //            {
        //                _type_staff = clStaff.Suppliertitle;
        //            }
        //        }
        //        return _type_staff;
        //    }
        //}

        public string QueryString
        {
            get { return Request.QueryString["sid"]; }
        }

        public short shrQueryString
        {
            get { return Convert.ToInt16(this.QueryString); }
        }

        public event EventHandler DataSaved;
        

        protected void FVStaffAddEdit_DataBound(object sender, EventArgs e)
        {
            if (FVStaffAddEdit.CurrentMode == FormViewMode.ReadOnly)
            {
                if ((this.Page as Hotels2BasePage).SessionId != 2 && (this.Page as Hotels2BasePage).SessionId != 7)
                {
                    LinkButton linkButtonInsert = (LinkButton)FVStaffAddEdit.FindControl("NewButton");
                    LinkButton linkButtonEdit = (LinkButton)FVStaffAddEdit.FindControl("EditButton");

                    linkButtonInsert.Visible = false;
                    linkButtonEdit.Visible = false;
                }

                //Hotels2BasePage dd = new Hotels2BasePage();
                //if(dd.Session)
                //Finding Control Label in Formveiw : LastAccess
                Label lblLastaccess = (Label)FVStaffAddEdit.FindControl("LastAccessLabel");

                //Finding Control Label in Formveiw : StaffId
                Label lblStaffId = (Label)FVStaffAddEdit.FindControl("Staff_IdLabel");

                //Finding Control Label in FormView : Password
                Label lblPassWord = (Label)FVStaffAddEdit.FindControl("PassWordLabel");
                lblPassWord.Text = "*******";

                //convert Display To dateTime FormatString
                lblLastaccess.Text = lblLastaccess.Text.Hotels2LongDateTimeFormat();

                //Convert Display from StaffId to Picture
                string strPicfilePath = "../../images_staffs/StaffPic" + lblStaffId.Text.ToLower() + ".gif";

                if ((this.Page as Hotels2BasePage).fileExist(Server.MapPath(strPicfilePath)))
                {
                    lblStaffId.Text = "<img src=\"" + strPicfilePath + "\" class=\"picStaff\"/>";
                }
                else
                {
                    lblStaffId.Text = "<img src=\"../../images_staffs/nopicture.gif\"/>";
                }
            }
         }

        protected void FVStaffAddEdit_ItemInserted(object sender, FormViewInsertedEventArgs e)
        {
            if (DataSaved != null)
            {
                DataSaved(this, new EventArgs());
            }

            //Server.Transfer("staffmanage.aspx");
           //Response.Redirect("staffmanage.aspx");
        }

        protected void btReset_Onclick(object sender, EventArgs e)
        {
            
            TextBox NewPW = (TextBox)FVStaffAddEdit.FindControl("txtNewPW");
            Staff.updateStaffPassWord(this.shrQueryString, NewPW.Text);

            Response.Redirect("staffmanage.aspx?sid=" + this.shrQueryString);
        }

        protected void FVStaffAddEdit_ItemInserting(object sender, FormViewInsertEventArgs e)
        {
            // declare SupplierId value for bluehousestaff
            short shrSupplierIdByBHTStaff = 43;

            //declare StaffCat value for Partner Staff
            byte bytSatffCatByPartnerStaff = 6;

            if (this.DropDoenCatSelecedValue != bytSatffCatByPartnerStaff)
            {
              e.Values["Supplier_Id"] = shrSupplierIdByBHTStaff;
            }
         }


        protected void UpdateCancelButton_Click(object sender, EventArgs e)
        {
            if (FVStaffAddEdit.CurrentMode == FormViewMode.Insert && string.IsNullOrEmpty(this.QueryString))
            {
                Response.Redirect("staffmanage.aspx");
            }
        }


        protected void FVStaffAddEdit_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {
            //Response.Write(e.OldValues["Status"]);
            //Response.End();
            e.NewValues["Status"] = e.OldValues["Status"];



        }

        protected void ObjStaffAddEdit_ItemInserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            IDictionary paramsFromPage = e.InputParameters;

            string Cat_Id = e.InputParameters[0].ToString();
            string Title = e.InputParameters[1].ToString();
            string UserName = e.InputParameters[2].ToString();
            string PassWord = e.InputParameters[3].ToString();
            string Supplier_Id = e.InputParameters[4].ToString();
            
            paramsFromPage.Clear();
            paramsFromPage.Add("bytCat_Id", Cat_Id);
            paramsFromPage.Add("strTitle", Title);
            paramsFromPage.Add("strUsername", UserName);
            paramsFromPage.Add("strPassword", PassWord);
            paramsFromPage.Add("shrSupplier_id", Supplier_Id);
            
        }

        protected void ObjStaffAddEdit_ItemUpdating(object sender, ObjectDataSourceMethodEventArgs e)
        {
            IDictionary paramsFromPage = e.InputParameters;

            string Cat_Id = e.InputParameters[0].ToString();

            string Title = e.InputParameters[1].ToString();
            string UserName = e.InputParameters[2].ToString();
            //string PassWord = e.InputParameters[3].ToString();
            string Staff_Id = e.InputParameters[4].ToString();
            //Response.Write(Staff_Id);
            //Response.End();
            //bool bolStatus = bool.Parse(e.InputParameters[3].ToString());

            //byte Supplier_Id = byte.Parse(e.InputParameters[4].ToString());

            paramsFromPage.Clear();
            paramsFromPage.Add("shrStaff_Id", Staff_Id);
            paramsFromPage.Add("bytCat_Id", Cat_Id);
            //paramsFromPage.Add("shrSupplier_id", "");
            paramsFromPage.Add("strTitle", Title);
            paramsFromPage.Add("strUsername", UserName);
            //paramsFromPage.Add("strPassWord", PassWord);
            //paramsFromPage.Add("bolStatus", true);
            
         }

        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (string.IsNullOrEmpty(this.QueryString))
                {
                    FVStaffAddEdit.DefaultMode = FormViewMode.Insert;
                    FVStaffAddEdit.Visible = true;
                }
            }
        }

        public void FormViewBind()
        {
            FVStaffAddEdit.DataBind();
        }

        public void ButtonAddnewStaff()
        {
            //FVStaffAddEdit.Visible = true;
            //FVStaffAddEdit.ChangeMode(FormViewMode.Insert);
        }
}
}