using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Staffs;
using Hotels2thailand.ProductOption;
using Hotels2thailand.Production;

namespace Hotels2thailand.UI
{
    
    public partial class extranet_package_package_manage_edit : Hotels2BasePageExtra
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
               

                if (!string.IsNullOrEmpty(this.qOptionId))
                {
                    int intOptionId = int.Parse(this.qOptionId);


                    PackageDetailDataBind();

                    PackageContentDetailDataBind(intOptionId);
                    dropConditiontitleDataBind(intOptionId);


                    //ConditionDataBind();
                    
                    //GetRoomDetail(intOptionId);

                    
                    //int intConditionId = int.Parse(conditionTitle.SelectedValue);
                    
                    //ConditionPriceDataBind(intConditionId);

                   

                }
                else
                {
                   //GetCancellationDefault();
                }

                
               
                
            }
        }

       
        public void dropConditiontitleDataBind(int intOptionId)
        {
          
            ProductConditionExtra cConditionExtra = new ProductConditionExtra();

            conditionTitle.DataSource = cConditionExtra.getConditionListByOptionId(intOptionId, 1);
            conditionTitle.DataTextField = "TitleConditionPackage";
            conditionTitle.DataValueField = "ConditionId";
            conditionTitle.DataBind();

        }


        public void PackageDetailDataBind()
        {
            
            dropNight.DataSource = this.dicGetNumber(10);
            dropNight.DataTextField = "Value";
            dropNight.DataValueField = "Key";
            dropNight.DataBind();

            drop_adult_child.DataSource = this.dicGetNumber(10);
            drop_adult_child.DataTextField = "Value";
            drop_adult_child.DataValueField = "Key";
            drop_adult_child.DataBind();
            drop_adult_child.SelectedValue = "1";

            ListItem iItem = new ListItem("Day Trip", "1");
            dropNight.Items.Insert(0, iItem);

            //drop_breakfast.Items.RemoveAt(0);
            ListItem newList = new ListItem("Room only", "0");
            //drop_breakfast.Items.Insert(0, newList);

        }


        public void PackageContentDetailDataBind(int intOptionId)
        {
            ProductOptionContent cOptionContent = new ProductOptionContent();
           cOptionContent =  cOptionContent.GetProductOptionContentById(intOptionId,1);
           txt_package_detail.Text = cOptionContent.Detail;
        }

        public void GetCancellationDefault()
        {
            string ListItem = string.Empty;

            ListItem = ListItem + "<div class=\"cancel_list_item\" id=\"cancel_list_item_1\">";


            ListItem = ListItem + "<input type=\"checkbox\" checked=\"checked\" value=\"1\" name=\"cencel_list_Checked\" style=\"display:none;\" />";

            ListItem = ListItem + "<table cellpadding=\"5\" cellspacing=\"1\" width=\"100%\" bgcolor=\"#d8dfea\" border=\"0\" style=\"text-align:center; margin:0px 0px 0px 0px;\">";
            ListItem = ListItem + "<tr style=\"background-color:#ffffff;\" >";
            ListItem = ListItem + "<td align=\"center\" style=\"margin:2px 0px 2px 0px; padding:2px 0px 2px 0px;\" width=\"30%\">";
            ListItem = ListItem + "<select id=\"drop_daycancel_1\" class=\"Extra_Drop\" name=\"drop_daycancel_1\" >";

            for (int i = 0; i <= 120; i++)
            {
                string Ischecked = string.Empty;
                

                if (i == 0)
                {
                    ListItem = ListItem + "<option value=\"" + i + "\" selected=\"selected\" >no-show</option>";
                }
                else
                {
                    ListItem = ListItem + "<option value=\"" + i + "\"  >" + i + "</option>";
                }
            }

            ListItem = ListItem + "</select>";
            ListItem = ListItem + "</td>";
            ListItem = ListItem + "<td align=\"center\" style=\"margin:2px 0px 2px 0px; padding:2px 0px 2px 0px;\" width=\"30%\">";
            ListItem = ListItem + "<input type=\"text\" id=\"txt_day_charge_1\" maxlength=\"2\" value=\"0\" style=\"width:20px;\" name=\"txt_day_charge_1\" class=\"Extra_textbox\"  />";
            ListItem = ListItem + "</td>";
            ListItem = ListItem + "<td align=\"center\" style=\"margin:2px 0px 2px 0px; padding:2px 0px 2px 0px;\" width=\"30%\">";
            ListItem = ListItem + "<input type=\"text\" id=\"txt_per_charge_1\" maxlength=\"3\" value=\"0\" style=\"width:22px;\" name=\"txt_per_charge_1\" class=\"Extra_textbox\" />";
            ListItem = ListItem + "</td>";
            ListItem = ListItem + "<td width=\"10%\"></td>";
            ListItem = ListItem + "</tr>";
            ListItem = ListItem + "</table>";
            ListItem = ListItem + "</div>";

            ltCancelDefault.Text = ListItem;



        }


        public string GenPackageDetailXML()
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(Request.Form["packagelist"]))
            {
                string[] benefitVal = Request.Form["packagelist"].Split(',');


                result = result + "<PromotionShow>";
                result = result + "<head>Special Benefit</head>";
                result = result + "<List>";

                foreach (string benefit in benefitVal)
                {
                    result = result + "<item>" + Request.Form["package_" + benefit].ToString().Hotels2SPcharacter_remove() + "</item>";
                }

                result = result + "</List>";
                result = result + "</PromotionShow>";

            }
            return result;
        }



        
    }
}