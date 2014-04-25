using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using Hotels2thailand;
using Hotels2thailand.Production;

public partial class agency_AgencyList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SetDetail();
    }

    private void SetDetail()
    {
        B2bAgency cB2bAgency = new B2bAgency();
        lblHeader.Text = "Agency";
        //gvAgency.DataSource = cB2bAgency.GetAgencyList();
        //gvAgency.DataBind();
        IList<object> iListAgency = null;
        iListAgency = cB2bAgency.GetAgencyList();
        if (iListAgency.Count() > 0)
        {
            int iNo = 1;
            StringBuilder cStringBuilder = new StringBuilder();
            cStringBuilder.Append("<table style='width:100%' class='tbDetail' cellspacing='0'>");
            cStringBuilder.Append("<tr>");
            cStringBuilder.Append("<th style='text-align:center; width:80px;'>");
            cStringBuilder.Append("No.");
            cStringBuilder.Append("</th>");
            cStringBuilder.Append("<th style='text-align:left;'>");
            cStringBuilder.Append("Agency Name");
            cStringBuilder.Append("</th>");
            cStringBuilder.Append("<th  style='text-align:center; width:80px;'>");
            cStringBuilder.Append("Status");
            cStringBuilder.Append("</th>");
            cStringBuilder.Append("<th  style='text-align:center; width:80px;'>");
            cStringBuilder.Append("");
            cStringBuilder.Append("</th>");
            cStringBuilder.Append("</tr>");
            foreach (B2bAgency agrncy in iListAgency)
            {
                cStringBuilder.Append("<tr>");
                cStringBuilder.Append("<td style='text-align:center'>");
                cStringBuilder.Append(iNo);
                cStringBuilder.Append("</td>");
                cStringBuilder.Append("<td>");
                if (agrncy.status == true)
                {
                    cStringBuilder.Append("<a href='SearchProduct.aspx?Staff=BHT&aid=" + agrncy.agency_id + "' target='_Blank'>" + agrncy.agency_name + "</a>");
                }
                else
                {
                    cStringBuilder.Append(agrncy.agency_name);                
                }
                cStringBuilder.Append("</td>");
                cStringBuilder.Append("<td style='text-align:center'>");
                if (agrncy.status == true)
                {
                    cStringBuilder.Append("Enable");
                }
                else
                {
                    cStringBuilder.Append("Disable");
                }
                cStringBuilder.Append("</td>");
                cStringBuilder.Append("<td style='text-align:center'>");
                cStringBuilder.Append("<a href='AgencyProfile.aspx?aid=" + agrncy.agency_id + "' >Edit</a>");
                cStringBuilder.Append("<td>");
                cStringBuilder.Append("</tr>");
                iNo = iNo + 1;
            }
            cStringBuilder.Append("</table>");
            divDetail.InnerHtml = cStringBuilder.ToString();
        }
    }
}