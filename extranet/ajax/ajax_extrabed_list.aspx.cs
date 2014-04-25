using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.ProductOption;
using System.Text;
using Hotels2thailand.Production;


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_extrabed_list : Hotels2BasePageExtra_Ajax
    {
        public string qoptionCat
        {
            get { return Request.QueryString["opcat"]; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.IsPostBack)
            {
                //Response.Write("HELLO");
                Response.Write(getPricePeriodList());
                Response.End();
                
                
            }
        }

        
        public string  getPricePeriodList()
        {
            string result = string.Empty;
            try
            {
                //byte bytOptionCat = byte.Parse(this.qoptionCat);
                //Option cOption = new Option();
                //cOption = cOption.GetProductOptionTop1_Extrnet(this.CurrentProductActiveExtra, 39, this.CurrentSupplierId);
                if (!string.IsNullOrEmpty(Request.Form["drop_option"]))
                {
                    int intOptionId = int.Parse(Request.Form["drop_option"]);
                    Option cOption = new Option();
                    cOption = cOption.getOptionById(intOptionId);
                    string strtxtHead = "";
                    switch (cOption.CategoryID)
                    {
                        case 39:
                            strtxtHead = "Extra Bed";
                            break;
                        case 44:
                            strtxtHead = "Transfer";
                            break;
                    }
                    
                    ProductConditionExtra cConditionExtra = new ProductConditionExtra();
                    cConditionExtra = cConditionExtra.getTopConditionByOptionId(intOptionId);

                    if (cConditionExtra != null)
                    {
                        int intCondition = cConditionExtra.ConditionId;

                        result = result + "<h4><img id=\"Image4\" src=\"/images/content.png\" /> Current " + strtxtHead + "</h4>";
                        if (cOption.CategoryID == 39)
                        {
                            if (cConditionExtra.Breakfast > 0)
                                result = result + "<p>Breakfast : <input type=\"checkbox\" name=\"check_ABF\" id=\"check_ABF\"  checked=\"checked\" />";
                            else
                                result = result + "<p>Breakfast : <input type=\"checkbox\" name=\"check_ABF\" id=\"check_ABF\" />";
                        

                        result = result + "<input type=\"button\" class=\"Extra_Button_small_green\" id=\"abfUpdate\" onclick=\"updateAbf('" + intCondition + "','');return false;\" value=\"Update\"  /></p>";

                        }
                        result = result + "<table  class=\"tbl_acknow\" cellspacing=\"2\" >";
                        result = result + "<tr class=\"header_field\" ><th>Date From</th><th>Date To</th><th>Price</th><th>Update</th><th>Bin</th></tr>";

                        PoductPriceExtra cExtraPrice = new PoductPriceExtra();

                        List<object> listprice = cExtraPrice.getPriceExtrabyCurrentDate(intCondition);

                        int index = 0;
                        int Total = listprice.Count();
                        if (Total > 0)
                        {
                            decimal decPrice  = 0;
                            string PriceSplit = string.Empty;
                            DateTime endTemp = DateTime.Now;
                            foreach (PoductPriceExtra price in listprice)
                            {
                               
                                if (decPrice != price.Price) 
                                {
                                    
                                    if (index != 0)
                                    {
                                        result = result.Replace("##tempend##", endTemp.ToString("dd-MMM-yyy")).Replace("##tempendParam##", endTemp.ToString("yyyy-MM-dd"));
                                    }

                                    result = result + "<tr bgcolor=\"#ffffff\" align=\"center\">";
                                    result = result + "<td>" + price.DatePrice.ToString("dd-MMM-yyy") + "<input type=\"hidden\" name=\"hd_extrabed_date_From_" + index + "\" value=\"" + price.DatePrice.ToString("yyyy-MM-dd") + "\" />" + "<input type=\"checkbox\" style=\"display:none;\" checked=\"checked\" name=\"extrabed_checked_list\" value=\""+ index + "\"  /></td>";

                                    result = result + "<td>##tempend##<input type=\"hidden\" name=\"hd_extrabed_date_To_" + index + "\" value=\"##tempendParam##\" /></td>";
                                    result = result + "<td><input type=\"text\" class=\"Extra_textbox_yellow\" value=\"" + price.Price.ToString("#,0") + "\" name=\"price_" + index + "\" /></td>";
                                    result = result + "<td><input type=\"button\" class=\"Extra_Button_small_green\" onclick=\"RateUpdate('" + index + "','" + price.DatePrice.ToString("yyyy-MM-dd") + "','##tempendParam##', '" + intCondition + "');return false;\" value=\"Save\" /></td>";
                                    result = result + "<td><img src=\"/images_extra/bin.png\" onclick=\"DarkmanPopUpComfirm(400,'Are you sure to delete' ,'delExtra(" + index + "," + intCondition + ")');return false;\"  style=\"cursor:pointer;\"  /></td>";
                                    
                                    result = result + "</tr>";


                                    decPrice = price.Price;
                                }
                                endTemp = price.DatePrice;

                                index = index + 1;
                            }

                            result = result.Replace("##tempend##", endTemp.ToString("dd-MMM-yyy")).Replace("##tempendParam##", endTemp.ToString("yyyy-MM-dd"));
                            
                        }
                        else
                        {
                            result = "<div class=\"box_empty\">";

                            result = result + "<p><label>No Rate " + strtxtHead + "</label> Please insert new one</p>";
                            
                            result = result + "</div>";
                            
                        }
                    }
                    else
                    {
                        result = "<div class=\"box_empty\">";

                        result = result + "<p><label>No Rate " + strtxtHead + "</label> Please insert new one</p>";

                        result = result + "</div>";
                    }

                }
                else
                {
                    //result = "<div class=\"box_empty\">";

                    //result = result + "<p>Sorry! System have problem . please contact Hotels2thailand team</p>";

                    //result = result + "</div>";
                }
            }
            catch (Exception ex)
            {
                result = "error: " + ex.Message;
            }

            result = result + "</table>";
            
            return result;
        }
    }
}