using System;
using System.Collections.Generic;

using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.ProductOption;
using Hotels2thailand.Production;

namespace Hotels2thailand.UI
{
    public partial class ajax_product_option_holidays_insert : System.Web.UI.Page
    {
        public string qProductId
        {
            get { return Request.QueryString["pid"]; }
        }

        public string qSupplierId
        {
            get { return Request.QueryString["supid"]; }
        }

        public string qTitle
        {
            get { return Request.QueryString["t"]; }
        }
        
        public string qSupdate
        {
            get { return Request.QueryString["d"]; }
        }
        public string qSupAmount
        {
            get { return Request.QueryString["total"]; }
        }
        public string qInsertType
        {
            get { return Request.QueryString["type"]; }
        }

        public string qholidayIdList
        {
            get { return Request.QueryString["hol"]; }
        }
        public string qOptionList
        {
            get { return Request.QueryString["oid"]; }
        }


        public short getCurrentSupplier
        {
            get
            {
                if (string.IsNullOrEmpty(this.qSupplierId))
                {
                    Product cProduct = new Product();
                    cProduct = cProduct.GetProductById(int.Parse(this.qProductId));
                    return cProduct.SupplierPrice; 
                }
                else
                {
                    return short.Parse(this.qSupplierId);
                }
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.IsPostBack)
            {
                
                if (!string.IsNullOrEmpty(this.qProductId))
                {
                    if (this.qInsertType == "single")
                    {
                        //Response.Write(Request.Url.ToString());
                        Response.Write(InsertSupplementSingle());
                    }

                    if (this.qInsertType == "template")
                    {
                        //Response.Write(Request.Url.ToString());
                        Response.Write(InsertSupplementTemplate());
                        //Response.Write(decimal.Parse(Request.Form["txtAmount_" + 7 + ""]));
                    }

                    Response.End();

                }
            }
            
            
        }


        public string InsertSupplementSingle()
        {
            string strResult = "false";
            short shrSupplierID = short.Parse(this.qSupplierId);
            //DateTime dDateSup = this.qSupdate.Hotels2DateSplitYear("-");
            decimal Amount = decimal.Parse(this.qSupAmount);
            string[] arrOptionId = this.qOptionList.Hotels2RightCrl(1).Split(';');

            byte bytDateType = byte.Parse(Request.Form["issingleDateCheck"]);
            DateTime dDateSup = Request.Form["hd_txtDateStart"].Hotels2DateSplitYear("-");
            DateTime dDateSupStart = Request.Form["hd_txtDateStart_01"].Hotels2DateSplitYear("-");
            DateTime dDateSupEnd = Request.Form["hd_txtDateEnd_01"].Hotels2DateSplitYear("-");
            //Response.Write(arrOptionId.Length);
            //Response.End();
            if (arrOptionId.Length > 0)
            {
                ProductOptionSupplementDate cSup = new ProductOptionSupplementDate();
                foreach (string option in arrOptionId)
                {
                    if (bytDateType == 1)
                    {
                        cSup.InsertOptionSupplement(shrSupplierID, int.Parse(option), this.qTitle, dDateSup, Amount);
                    }

                    if (bytDateType == 2)
                    {
                        int intdateDiff = dDateSupEnd.Subtract(dDateSupStart).Days;
                        for (int i = 0; i <= intdateDiff; i++)
                        {
                            DateTime DateTimeCurrent = dDateSupStart.AddDays(i);
                            cSup.InsertOptionSupplement(shrSupplierID, int.Parse(option), this.qTitle, DateTimeCurrent, Amount);
                        }
                    }
                    
                    
                    strResult = "true";
                }

            }

            return strResult;

        }

        public string InsertSupplementTemplate()
        {
            
            string strResult = "false";
            short shrSupplierID = short.Parse(this.qSupplierId);

            //decimal Amount = decimal.Parse(Request.Form["txtAmount_" + ]);
            string[] arrOptionId = this.qOptionList.Hotels2RightCrl(1).Split(';');
            string[] arrHolidayId = this.qholidayIdList.Hotels2RightCrl(1).Split(';');

            if (arrHolidayId.Length > 0)
            {
                byte bytHolidayId = 0;
                foreach (string holid in arrHolidayId)
                {
                    bytHolidayId = byte.Parse(holid);
                    ProductOptioPublicHolidays cPublicHolidays = new ProductOptioPublicHolidays();
                    cPublicHolidays = cPublicHolidays.getPublicHolidaybyId(bytHolidayId);
                    if (arrOptionId.Length > 0)
                    {
                        ProductOptionSupplementDate cSup = new ProductOptionSupplementDate();
                        foreach (string option in arrOptionId)
                        {
                            cSup.InsertOptionSupplement(shrSupplierID, int.Parse(option), cPublicHolidays.Title, cPublicHolidays.HolidayDate, decimal.Parse(Request.Form["txtAmount_" + bytHolidayId + ""]));
                            strResult = "true";
                        }

                    }
                }
            }

            return strResult;

        }
        
    }
}