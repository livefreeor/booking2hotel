using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Hotels2thailand;
using Hotels2thailand.Booking;
using Hotels2thailand.Production;


public partial class agency_SearchProduct : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["Agency_ID"] != null && Session["Agency_ID"].ToString() != "" && Session["Agency_ID"].ToString() != "0") || (Request.QueryString["Staff"] != null && Request.QueryString["Staff"].ToString() == "BHT"))
        {
            if (!IsPostBack)
            {
                hdCatID.Value = "29";
                getDestination(29);
                if (Session["Agency_ID"] != null)
                {
                    hdAgencyID.Value = Session["Agency_ID"].ToString();
                }
                if (Request.QueryString["aid"] != null)
                {
                    hdAgencyID.Value = Request.QueryString["aid"].ToString();
                    Session["Agency_ID"] = Request.QueryString["aid"].ToString();
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Session timeout. Please login again.');", true);
            Response.Redirect("Default.aspx");
        }
    }
    private void getDestination(int intProductCatID)
    {
        Destination cDestination = new Destination();
        Dictionary<string, string> dicDestination = cDestination.GetB2BDestinationByProductCatID(intProductCatID);
        ddlDesination.DataSource = dicDestination;
        ddlDesination.DataValueField = "Key";
        ddlDesination.DataTextField = "Value";
        ddlDesination.DataBind();
        ddlDesination.Items.Insert(0, new ListItem("All Destination", "0"));
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Staff"] != null && Request.QueryString["Staff"].ToString() == "BHT")
        {
            Response.Redirect("ProductList.aspx?CatID=" + hdCatID.Value + "&DesID=" + ddlDesination.Value + "&Staff=BHT");            
        }
        else
        {
            Response.Redirect("ProductList.aspx?CatID=" + hdCatID.Value + "&DesID=" + ddlDesination.Value);
        }
    }
    protected void rbtHotel_CheckedChanged(object sender, EventArgs e)
    {
        hdCatID.Value = "29";
        getDestination(29);
    }
    protected void rbtShow_CheckedChanged(object sender, EventArgs e)
    {
        hdCatID.Value = "38";
        getDestination(38);
    }
    protected void rbtDayTrip_CheckedChanged(object sender, EventArgs e)
    {
        hdCatID.Value = "34";
        getDestination(34);
    }
    protected void rbtWaterAct_CheckedChanged(object sender, EventArgs e)
    {
        hdCatID.Value = "36";
        getDestination(36);
    }
}