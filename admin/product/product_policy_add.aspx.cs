using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
namespace Hotels2thailand.UI
{
    public partial class admin_product_product_policy_add : Hotels2BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.Page.IsPostBack)
            {
                Product cProduct = new Product();
                cProduct = cProduct.GetProductById(int.Parse(this.qProductId));
                Destitle.Text = cProduct.DestinationTitle;
                txthead.Text = cProduct.Title;

                dropPolicyCatDataBind();
                dropPolicyTypeDataBind();

                dropCancelDataBind();

                panelProductPolicyCancel.Visible = false;

                

                PageDataBind();
                //check panel Cancellation Visible
                if (dropPolicyType.SelectedValue == "1")
                {
                    panelProductPolicyCancel.Visible = true;

                }
            }
            else
            {
                PolicyContentLang.DataBind();
            }
        }

        public void PageDataBind()
        {
            if (!string.IsNullOrEmpty(this.qPolicyId))
            {
                ProductPolicyAdmin cProductPolicy = new ProductPolicyAdmin();
                cProductPolicy = cProductPolicy.GetPolicyByID(int.Parse(this.qPolicyId));

                txtTitle.Text = cProductPolicy.Title;
                dDatePicker.DateStart = cProductPolicy.DateStart;
                dDatePicker.DateEnd = cProductPolicy.DateEnd;
                dDatePicker.DataBind();

                if (cProductPolicy.Status)
                {
                    radioStatusTrue.Checked = true;
                    radioStatusFalse.Checked = false;
                }
                else
                {
                    radioStatusTrue.Checked = false;
                    radioStatusFalse.Checked = true;
                }

                dropPolicyCat.SelectedValue = cProductPolicy.PolicyCat.ToString();
                dropPolicyType.SelectedValue = cProductPolicy.PolicyType.ToString();

                if (cProductPolicy.PolicyType == 1)
                {
                    panelProductPolicyCancel.Visible = true;
                    panelProductPolicyContentLang.Visible = false;
                }
                else
                {
                    panelProductPolicyCancel.Visible = false;
                    panelProductPolicyContentLang.Visible = true;
                }

                GVCancelPolicyListDataBind();
            }
            else
            {
                
                panelProductPolicyContentLang.Visible = false;
                panelProductPolicyCancel.Visible = false;
            }
        }

        public void GVCancelPolicyList_onRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                byte dayCancel = (byte)DataBinder.Eval(e.Row.DataItem, "DayCancel");
                Label lbldayCancel = e.Row.Cells[1].FindControl("lblDayCancel") as Label;

                if (dayCancel == 250)
                    lbldayCancel.Text = "<p style=\"font-weight:bold;color:red;margin:0px;padding:0px;\">none refund<p>"; 
            }
        }
        public void dropPolicyCatDataBind()
        {
            dropPolicyCat.DataSource = ProductPolicyAdmin.getPolicyCategoryall();
            dropPolicyCat.DataTextField = "Value";
            dropPolicyCat.DataValueField = "Key";
            dropPolicyCat.DataBind();
        }

        

        public void dropPolicyTypeDataBind()
        {
            dropPolicyType.DataSource = ProductPolicyAdmin.getPolicyType();
            dropPolicyType.DataTextField = "Value";
            dropPolicyType.DataValueField = "Key";
            dropPolicyType.DataBind();
        }

        public void dropPolicyType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PolicyContentLang.DataBind();
            if (dropPolicyType.SelectedValue == "1")
            {
                panelProductPolicyCancel.Visible = true;
            }
            else
            {
                panelProductPolicyCancel.Visible = false;
            }
            
        }

        public void dropCancelDataBind()
        {
            dropCancel.DataSource = this.dicGetNumberstart0(120);
            dropCancel.DataTextField ="Value";
            dropCancel.DataValueField="Key";
            dropCancel.DataBind();

            //incase Non-refund 
            ListItem ItemNoRefund = new ListItem("non refund", "250");
            dropCancel.Items.Insert(0, ItemNoRefund);
        }

        

        public void GVCancelPolicyListDataBind()
        {
            if (!string.IsNullOrEmpty(this.qPolicyId))
            {
                int intPolicyId = int.Parse(this.qPolicyId);
                GVCancelPolicyList.DataSource = ProductPolicyCancellation.GetPolicyCancelByPolicyId(intPolicyId);
                GVCancelPolicyList.DataBind();
            }

            
            
        }

        public void linkBtDel_OnClick(object sender, EventArgs e)
        {
            LinkButton btnDel = (LinkButton)sender;
            string[] Param = btnDel.CommandArgument.Split(',');
            int PolicyId = int.Parse(Param[0]);
            byte DayCancel = byte.Parse(Param[1]);

            if (btnDel.CommandName == "cancelDel")
            {
                
                bool intDel = ProductPolicyCancellation.DeletePolicyCancel(PolicyId, DayCancel);
            }

            GVCancelPolicyListDataBind();
        }

        public void btnCancelSave_OnClick(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(this.qPolicyId))
            {
                int intPolicyId = int.Parse(this.qPolicyId);
                byte bytDayCancel = byte.Parse(dropCancel.SelectedValue);
                byte bytPercentHotel = byte.Parse(txtPercentHotel.Text);
                byte bytPercentBHt = byte.Parse(txtPercentBHt.Text);
                byte bytRoomHotel = byte.Parse(txtRoomHotel.Text);
                byte bytRoomBht = byte.Parse(txtRoomBht.Text);

                int insert = ProductPolicyCancellation.InsertPolicycancel(intPolicyId, bytDayCancel, bytPercentHotel, bytPercentBHt, bytRoomHotel, bytRoomBht);

            }
            GVCancelPolicyListDataBind();
        }

        public void tbnInfSave_Onclick(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(this.qPolicyId))
            {
                ProductPolicyAdmin cInsert = new ProductPolicyAdmin
                {
                    PolicyCat = byte.Parse(dropPolicyCat.SelectedValue),
                    PolicyType = byte.Parse(dropPolicyType.SelectedValue),
                    ProductID = int.Parse(this.qProductId),
                    Title = txtTitle.Text,
                    DateStart = dDatePicker.GetDatetStart,
                    DateEnd = dDatePicker.GetDatetEnd,
                    Status = true

                };

                int inPolicyId = cInsert.Insert(cInsert);
                Response.Redirect(Request.Url.ToString() + "&polid=" + inPolicyId);
            }
            else
            {
                int intPolicyId = int.Parse(this.qPolicyId);
                ProductPolicyAdmin cProductPolicy = new ProductPolicyAdmin();
                cProductPolicy.GetPolicyByID(intPolicyId);
                cProductPolicy.PolicyCat = byte.Parse(dropPolicyCat.SelectedValue);
                cProductPolicy.PolicyType = byte.Parse(dropPolicyType.SelectedValue);
                cProductPolicy.Title = txtTitle.Text;
                cProductPolicy.DateStart = dDatePicker.GetDatetStart;
                cProductPolicy.DateEnd = dDatePicker.GetDatetEnd;

                if (radioStatusTrue.Checked)
                {
                    cProductPolicy.Status = true;
                }

                if (radioStatusFalse.Checked)
                {
                    cProductPolicy.Status = false;
                }

                cProductPolicy.Update();
                Response.Redirect(Request.Url.ToString());
            }

            
        }
    }
}