using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hotels2thailand.UI.Controls
{
    public partial class Control_DatepickerCalendar : System.Web.UI.UserControl
    {

        private DateTime _date_start = DateTime.Now;
        public DateTime DateStart
        {
            get { return _date_start; }
            set { _date_start = value; }
        }

        private DateTime _date_end = DateTime.Now.AddDays(1);
        public DateTime DateEnd
        {
            get { return _date_end; }
            set { _date_end = value; }
        }

        //private TextBox _text_start = string.Empty;
        //public TextBox TextStart
        //{
        //    get { return _text_start.Text.Hotels2DateSplit("/"); }
        //    set { }
        //}
        //public Control_DatepickerCalendar()
        //{
        //    //_date_start = txtDateStart.Text.Hotels2DateSplit("/");
        //    //_date_end = txtDateEnd.Text.Hotels2DateSplit("/");
        //}
        public DateTime GetDatetStart
        {
            get
            {

                return txtDateStart.Text.Hotels2DateSplit("/");
            }
        }

        public DateTime GetDatetEnd
        {
            get
            {
                return txtDateEnd.Text.Hotels2DateSplit("/");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, Page.GetType(), txtDateStart.ClientID.ToString(), "<script>DuplicatDatFormat('" + txtDateStart.ClientID + "','" + txtDateStartReal.ClientID + "');</script>", false);
            ScriptManager.RegisterStartupScript(this, Page.GetType(), txtDateEnd.ClientID.ToString(), "<script>DuplicatDatFormat('" + txtDateEnd.ClientID + "','" + txtDateEndReal.ClientID + "');</script>", false);
            //this.Page.ClientScript.RegisterStartupScript(Page.GetType(), txtDateStart.ClientID.ToString(), "<script>DuplicatDatFormat('" + txtDateStart.ClientID + "','" + txtDateStartReal.ClientID + "');</script>", false);
            //this.Page.ClientScript.RegisterStartupScript(Page.GetType(), txtDateEnd.ClientID.ToString(), "<script>DuplicatDatFormat('" + txtDateEnd.ClientID + "','" + txtDateEndReal.ClientID + "');</script>", false);

            txtDateStart.Attributes.Add("onchange", "javascript:DuplicatDatFormat('" + txtDateStart.ClientID + "','" + txtDateStartReal.ClientID + "');");
            txtDateEnd.Attributes.Add("onchange", "javascript:DuplicatDatFormat('" + txtDateEnd.ClientID + "','" + txtDateEndReal.ClientID + "');");

            if (!this.IsPostBack)
            {
                txtDateStart.Text = this.DateStart.ToString("MM/dd/yyyy");
                txtDateEnd.Text = this.DateEnd.ToString("MM/dd/yyyy");

               
            }
        }

        public override void DataBind()
        {
           
            //Response.Write("ddddd");
            //Response.End();
            txtDateStart.Text = this.DateStart.ToString("MM/dd/yyyy");
            txtDateEnd.Text = this.DateEnd.ToString("MM/dd/yyyy");
        }

        //public DateTime getDateStrat(object sender, EventArgs e)
        //{

        //    this.DateStart = this.GetDatetStartFromTextBox;
        //    Response.Write(this.DateStart);
        //    Response.End();
        //}

        //public void txtDateStart_OnTextChanged(object sender, EventArgs e)
        //{

        //    txtDateStart.Text = txtDateStart.Text.Hotels2DateSplit("/").ToString("MMMM d, yyyy");
        //}
        
    }
}