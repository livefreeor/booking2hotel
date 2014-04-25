using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand;
using Hotels2thailand.ProductOption;
using System.Text;
using System.Collections;

namespace Hotels2thailand.UI.Controls
{
    public partial class Control_CalendarCustomControl : System.Web.UI.UserControl
    {

        public void Page_Load(object sender, EventArgs e)
        {
            dropmonthDataBind();
            dropyearDataBind();
            lbldateshow.Text = lbldateshowDataBind();
        }

        public void dropmonthDataBind()
        {

            dropmonth.DataSource = Hotels2thailand.Hotels2DateTime.GetMonthList();
            dropmonth.DataTextField = "Value";
            dropmonth.DataValueField = "Key";
            dropmonth.DataBind();
        }

        public void dropyearDataBind()
        {
            dropyear.DataSource = Hotels2thailand.Hotels2DateTime.GetYearList();
            dropyear.DataTextField = "Value";
            dropyear.DataValueField = "Key";
            dropyear.DataBind();
        }

        public string lbldateshowDataBind()
        {
            StringBuilder Result = new StringBuilder();
            Result.Append("<table width=\"100%\">");
            Result.Append("<tr>");
            Result.Append("<td>Sun</td>");
            Result.Append("<td>Mon</td>");
            Result.Append("<td>Tue</td>");
            Result.Append("<td>Wed</td>");
            Result.Append("<td>Thu</td>");
            Result.Append("<td>Fri</td>");
            Result.Append("<td>Sat</td>");
            
            Result.Append("</tr>");

            for (int i = 0; i < 5; i++)
            {
                Result.Append("<tr>");
                for (int j = 0; j < 7; j++)
                {
                    string ii = DateTime.Now.DayOfWeek.ToString();
                    string ff = DateTime.Now.Day.ToString();
                    string yy = DateTime.Now.Date.ToString();
                    
                    Result.Append("<td>" + DateTime.DaysInMonth(2010, 10) +"---"+ ii +"****"+ ff +"///"+yy+ "</td>");
                }
                Result.Append("</tr>");
            }

            Result.Append("</table>");

            return Result.ToString();
        }

        public void btnToday_OnClick(object sender, EventArgs e)
        {

        }

        public void btnprevious_OnClick(object sender, EventArgs e)
        {

        }

        public void btnnext_OnClick(object sender, EventArgs e)
        {

        }
    }
}