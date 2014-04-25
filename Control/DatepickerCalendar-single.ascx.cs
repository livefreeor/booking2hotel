using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hotels2thailand.UI.Controls
{
    public partial class Control_DatepickerCalendar_single : System.Web.UI.UserControl
    {

        private DateTime _date_start = DateTime.Now;
        public DateTime DateStart
        {
            get { return _date_start; }
            set { _date_start = value; }
        }

        //private DateTime _date_end = DateTime.Now.AddDays(1);
        //public DateTime DateEnd
        //{
        //    get { return _date_end; }
        //    set { _date_end = value; }
        //}


        public DateTime GetDatetStart
        {
            get
            {

                return txtDateStart.Text.Hotels2DateSplit("/");
            }
        }

        //public DateTime GetDatetEnd
        //{
        //    get
        //    {
        //        return txtDateEnd.Text.Hotels2DateSplit("/");
        //    }
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, Page.GetType(), txtDateStart.ClientID.ToString(), "<script>DuplicatDatFormat('" + txtDateStart.ClientID + "','" + txtDateStartReal.ClientID + "');</script>", false);
            //this.Page.ClientScript.RegisterStartupScript(Page.GetType(), "1", "<script>DuplicatDatFormat('ctl00_ContentPlaceHolder1_controlDatepicker_txtDateStart','ctl00_ContentPlaceHolder1_controlDatepicker_txtDateStartReal');</script>", false);
            //this.Page.ClientScript.RegisterStartupScript(Page.GetType(), "2", "<script>DuplicatDatFormat('ctl00_ContentPlaceHolder1_controlDatepicker_txtDateEnd','ctl00_ContentPlaceHolder1_controlDatepicker_txtDateEndReal');</script>", false);

            txtDateStart.Attributes.Add("onchange", "javascript:DuplicatDatFormat('" + txtDateStart.ClientID + "','" + txtDateStartReal.ClientID + "');");
            if (!this.IsPostBack)
            {
                //txtDateStart.Attributes.Add("onchange", "javascript:setValue('" + txtDateStart.ClientID + "');");
                txtDateStart.Text = this.DateStart.ToString("MM/dd/yyyy");
                //txtDateEnd.Text = this.DateEnd.ToString("MM/dd/yyyy");

            }
        }

        public override void DataBind()
        {
            txtDateStart.Text = this.DateStart.ToString("MM/dd/yyyy");
            //txtDateEnd.Text = this.DateEnd.ToString("MM/dd/yyyy");
        }
        
    }
}