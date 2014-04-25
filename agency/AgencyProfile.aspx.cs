using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Hotels2thailand;
using Hotels2thailand.Production;

public partial class agency_AgencyProfile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["aid"] != null)
            {
                lblHeader.Text = "Edit agency profile";
                LoadAgency(Convert.ToInt32(Request.QueryString["aid"].ToString()));
            }
            else
            {
                lblHeader.Text = "Add agency profile";
                rbt1.Checked = true;
            }
        }
    }

    private void LoadAgency(int intAgencyID)
    {
        B2bAgency cB2bAgency = new B2bAgency();
        cB2bAgency = cB2bAgency.GetB2bAgencyProfile(intAgencyID);
        txtAgencyname.Text = cB2bAgency.agency_name;
        txtContactname.Text = cB2bAgency.contact_name;
        txtAddress.Text = cB2bAgency.address;
        txtPhone.Text = cB2bAgency.phone;
        txtEmail.Text = cB2bAgency.email;
        txtCommission.Text = cB2bAgency.commission.ToString();
        txtUsername.Text = cB2bAgency.username;
        txtPassword.Text = cB2bAgency.password;
        hdAgencyID.Value = cB2bAgency.agency_id.ToString();
        if (cB2bAgency.status == true)
        {
            rbt1.Checked = true;
        }
        else
        {
            rbt0.Checked = true;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtAgencyname.Text.Trim() != "" && txtContactname.Text.Trim() != "" && txtAddress.Text.Trim() != "" && txtPhone.Text.Trim() != "" && txtEmail.Text.Trim() != "" && txtUsername.Text.Trim() != "" && txtPassword.Text.Trim() != "")
        {
            bool boolStatus = true;
            if (rbt0.Checked == true)
            {
                boolStatus = false;
            }
           
            B2bAgency cB2bAgency = new B2bAgency();
            int intAgencyID = cB2bAgency.CheckUsernameAvailability(txtUsername.Text.Trim());
            if (hdAgencyID.Value == "")
            {
                if (intAgencyID == 0)
                {
                    intAgencyID = cB2bAgency.InsertB2bAgencyProfile(txtAgencyname.Text.Trim(), txtContactname.Text.Trim(), txtAddress.Text.Trim(), txtPhone.Text.Trim(), txtEmail.Text.Trim(), Convert.ToDecimal(txtCommission.Text.Trim()), txtUsername.Text.Trim(), txtPassword.Text.Trim(), boolStatus);
                    if (intAgencyID > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Save complete.'); window.location = 'AgencyList.aspx';", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Can not save data.');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('The username " + txtUsername.Text.Trim() + " is not available.');", true);
                }
            }
            else
            {
                if (intAgencyID == Convert.ToInt32(hdAgencyID.Value) || intAgencyID == 0)
                {
                    if (cB2bAgency.UpdateB2bAgencyProfile(Convert.ToInt32(hdAgencyID.Value), txtAgencyname.Text.Trim(), txtContactname.Text.Trim(), txtAddress.Text.Trim(), txtPhone.Text.Trim(), txtEmail.Text.Trim(), Convert.ToDecimal(txtCommission.Text.Trim()), txtUsername.Text.Trim(), txtPassword.Text.Trim(), boolStatus))
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Save complete.'); window.location = 'AgencyList.aspx';", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Can not save data.');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('The username " + txtUsername.Text.Trim() + " is not available.');", true);
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Please fill in all required information.');", true);
        }

    }
}