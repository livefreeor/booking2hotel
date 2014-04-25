using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class test_paging : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_genPag_Click(object sender, EventArgs e)
    {

        int total = int.Parse(txtPagetotal.Text);
        int intPageNum = int.Parse(txtCurrentPage.Text);



        StringBuilder result = new StringBuilder();



        

        result.Append("<div id=\"page_num\">");
        result.Append("<table>");
        result.Append("<tr>");
        result.Append("<td><span id=\"previous\">Previous</span></td>");
        result.Append("<td>");
        result.Append("<ul class=\"ul_page\">");
        if (total > 5)
        {
            //[] arrPage = new int[]{};
           // int count = 0;
            ArrayList arrPage = new ArrayList();
            for (int iPre = (intPageNum-2); iPre <= (intPageNum + 2); iPre++)
            {
              

                if (iPre > 0)
                {
                    arrPage.Add(iPre);
                   
                }
                   
            }
            arrPage[0] = 4;

            //for (int i = 0; i < arrPage.Count(); i++)
            //{
            //    result.Append("<li><a href=\"" + arrPage[i] + "\">" + arrPage[i] + "</a></li>");
            //}
                

            
            //result.Append("<li class=\"except\">...</li>");
            //result.Append("<li><a href=\"" + total + "\">" + total + "</a></li>");
        }
        else
        {

            for (int i = 1; i <= total; i++)
            {
                if (intPageNum == i)
                    result.Append("<li><a class=\"page_active\" href=\"" + i + "\">" + i + "</a></li>");
                else
                    result.Append("<li><a href=\"" + i + "\">" + i + "</a></li>");

            }

            //    result.Append("<li><a href=\"1\">1</a></li>");
            //result.Append("<li><a href=\"2\">2</a></li>");
            //result.Append("<li class=\"except\">...</li>");
            //result.Append("<li><a href=\"6\">6</a></li>");
        }

        result.Append("</ul>");
        result.Append("</td>");
        result.Append("<td id=\"next\">Next</td>");
        result.Append("</tr>");
        result.Append("</table>");
        result.Append("</div>");

        lblREsult.Text =  result.ToString();
    }
}