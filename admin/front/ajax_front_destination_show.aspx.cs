using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Front;
using System.Text;

namespace Hotels2thailand.UI
{
    public partial class admin_ajax_front_destination_show : System.Web.UI.Page
    {


        public string qProductCat
        {
            get { return Request.QueryString["pdcid"]; }
        }

        public string qLangId
        {
            get { return Request.QueryString["ln"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(this.qProductCat))
                {
                    DestinationShow cDestinationShow = new DestinationShow();
                    byte bytLangId = 1;
                    if (!string.IsNullOrEmpty(this.qLangId))
                    {
                        bytLangId = byte.Parse(this.qLangId);
                    }

                    dl_des.DataSource = cDestinationShow.GetDestinationShowPage_ByCatId(byte.Parse(qProductCat), bytLangId);
                    dl_des.DataBind();
                }

            }
        }

        public string GetFieldNameByCat(byte catId)
        {
            string FieldName = string.Empty;
            switch (catId)
            {
                case 29 :
                    FieldName = "FileName_Hotel";
                    break;
                case 32:
                    FieldName = "FileName_Golf";
                    break;
                case 34:
                    FieldName = "FileName_DayTrips";
                    break;
                case 36:
                    FieldName = "FileName_Water";
                    break;
                case 38:
                    FieldName = "FileName_Show";
                    break;
                case 39:
                    FieldName = "FileName_Heath";
                    break;
                case 40:
                    FieldName = "FileName_Spa";
                    break;

            }
            return FieldName;
        }

        public void dl_des_OnItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string FileName = (string)DataBinder.Eval(e.Item.DataItem, GetFieldNameByCat((byte.Parse(qProductCat))));
                HyperLink hyTitle = e.Item.FindControl("hyTitle") as HyperLink;
                hyTitle.NavigateUrl =  "/" + FileName;
            }
        }

        

       


       
        

    }
}