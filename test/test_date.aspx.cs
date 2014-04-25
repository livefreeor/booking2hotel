using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.IO;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Hotels2thailand.Production;
using Hotels2thailand.PDF;
using Hotels2thailand;

using EvoPdf.HtmlToPdf;


public partial class test_date : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                Response.Write(DateTime.Now.Hotels2ThaiDateTime());
                Response.End();
                DateTime dDateStart = new DateTime(2011, 11, 28);

                int dateDiff = dDateStart.Subtract(DateTime.Now.Date).Days;
                Response.Write(dateDiff);
                Response.End();
            }
            //ctrlDemoLinksBox.LoadDemo("GettingStarted");
        }

        public void btnPdfHotel_Onclick(object sender, EventArgs e)
        {
            byte bytCat = byte.Parse(radioCat.SelectedValue);
            PdfGenerateCore cPDF = new PdfGenerateCore();
            cPDF.GenrateReviewAllProduct(bytCat);
        }
    }

