using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Front;
using Hotels2thailand;
using Hotels2thailand.Production;
using Hotels2thailand.DataAccess;
public partial class vtest_BookingAddNewItemInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //double temp = (100.00/3)-(int)(100.00/3);
            int productID = int.Parse(Request.Form["hotel_id"]);
            DateTime dateStart = Hotels2thailand.Hotels2DateTime.Hotels2DateSplitYear(Request.Form["date_start"], "-");
            DateTime dateEnd = Hotels2thailand.Hotels2DateTime.Hotels2DateSplitYear(Request.Form["date_end"], "-");

            string ConditionSelect = Request.Form["ddPrice"];
            string ExtraSelect = Request.Form["ddPriceExtra"];

            string selectOption = string.Empty;

            string[] condition = ConditionSelect.Split(',');

            int countSelect = ConditionSelect.Count();
            int[] conditionID = new int[countSelect];
            int[] optionID = new int[countSelect];
            int[] quantity = new int[countSelect];
            int[] promotionID = new int[countSelect];

            int promotionTmp = 0;
            for (int conditionCount = 0; conditionCount < condition.Count(); conditionCount++)
            {
                string[] item = condition[conditionCount].Split('_');
                for (int itemCount = 0; itemCount < item.Count(); itemCount++)
                {
                    conditionID[conditionCount] = Int32.Parse(item[0]);
                    optionID[conditionCount] = Int32.Parse(item[1]);
                    promotionID[conditionCount] = Int32.Parse(item[2]);
                    quantity[conditionCount] = Int32.Parse(item[6]);
                }
                selectOption = selectOption + "<input type=\"hidden\" name=\"room_" + conditionID[conditionCount] + "_" + optionID[conditionCount] + "_" + promotionID[conditionCount] + "\" value=\"" + quantity[conditionCount] + "\">\n";
                if (promotionID[conditionCount] != promotionTmp)
                {
                    promotionTmp = promotionID[conditionCount];
                }

            }

            //Booking Information

            decimal priceTotal = 0;

            // CalculatePrice price-------------------------------------------------------------------------------
            DataConnect objConnCat = new DataConnect();
            string sqlCommand = "select cat_id from tbl_product where product_id=" + productID;
            byte categoryID = (byte)objConnCat.ExecuteScalar(sqlCommand);

            ProductPrice priceProduct = new ProductPrice(productID,categoryID, dateStart, dateEnd);
            priceProduct.LoadPrice();

            PriceSupplement priceSupplement = new PriceSupplement();
            priceSupplement.LoadPriceSupplementByProductID(productID);

            decimal priceCalculate = 0;

            for (int conditionCount = 0; conditionCount < conditionID.Count(); conditionCount++)
            {
                //Response.Write(quantity[conditionCount]+"<br>");
                if (quantity[conditionCount] != 0)
                {
                    priceCalculate = priceProduct.CalculateAll(conditionID[conditionCount], optionID[conditionCount], promotionID[conditionCount]).Price;
                    priceTotal = priceTotal + (priceSupplement.GetPriceSupplement(dateStart, priceCalculate, conditionID[conditionCount], quantity[conditionCount]));
                }

            }


            bool checkTransfer = false;
            List<PriceBase> ExtraOptionList = priceProduct.RateBase(2);

            String[] extraOption = new String[2];

            foreach (PriceBase extraItem in ExtraOptionList)
            {
                if (Request.Form["ddPriceExtra_" + extraItem.ConditionID] != null)
                {
                    extraOption = Request.Form["ddPriceExtra_" + extraItem.ConditionID].Split('_');
                    if (int.Parse(extraOption[6]) != 0)
                    {
                        if (extraItem.OptionCategoryID == 43 || extraItem.OptionCategoryID == 44)
                        {
                            checkTransfer = true;
                        }
                        selectOption = selectOption + "<input type=\"hidden\" name=\"ddPriceExtra_" + extraItem.ConditionID + "\" value=\"" + int.Parse(extraOption[6]) + "\">\n";
                        priceTotal = priceTotal + extraItem.Rate;
                    }
                }
            }

            if (!checkTransfer)
            {
                List<PriceBase> PriceList = new List<PriceBase>();
                priceProduct.GetTransferFromOtherProduct(productID);
                PriceList = priceProduct.RateBase(3);
                foreach (PriceBase extraItem in PriceList)
                {
                    if (Request.Form["ddPriceExtra_" + extraItem.ConditionID + "_" + extraItem.OptionID] != null)
                    {
                        extraOption = Request.Form["ddPriceExtra_" + extraItem.ConditionID + "_" + extraItem.OptionID].Split('_');
                        if (int.Parse(extraOption[6]) != 0)
                        {
                            selectOption = selectOption + "<input type=\"hidden\" name=\"ddPriceExtra_" + extraItem.ConditionID + "\" value=\"" + int.Parse(extraOption[6]) + "\">\n";
                            priceTotal = priceTotal + extraItem.Rate * int.Parse(extraOption[6]);
                            checkTransfer = true;
                        }
                    }
                }
            }
            //Gala
            GalaDinner gala = new GalaDinner(productID, dateStart, dateEnd);
            List<GalaDinner> galaList = gala.GetGala();

            int numNightRoom = dateEnd.Subtract(dateStart).Days;
            int numNightGala = 0;

            DateTime dateCheck;
            //DateTime dateGalaCheck;

            foreach (GalaDinner item in galaList)
            {

                if (item.DefaultGala == 0)
                {
                    // Select Only day
                    if (item.RequireAdult)
                    {
                        priceTotal = priceTotal + item.Rate;
                    }
                    if (item.RequireChild)
                    {
                        priceTotal = priceTotal + item.Rate;
                    }

                }
                else
                {
                    numNightGala = item.DateUseEnd.Subtract(dateStart).Days;
                    // Select all day
                    if (item.RequireAdult)
                    {
                        //for (int countNightGala = 0; countNightGala < numNightGala;countNightGala++ )
                        //{
                        //dateGalaCheck = item.DateUseEnd.AddDays(countNightGala);
                        for (int countNight = 0; countNight < numNightRoom; countNight++)
                        {
                            dateCheck = dateStart.AddDays(countNight);
                            if (dateCheck.CompareTo(item.DateUseStart) >= 0 && item.DateUseEnd.CompareTo(dateCheck) >= 0)
                            {
                                priceTotal = priceTotal + item.Rate;
                            }
                        }
                        //}
                    }
                    if (item.RequireChild)
                    {
                        for (int countNight = 0; countNight < numNightRoom; countNight++)
                        {
                            dateCheck = dateStart.AddDays(countNight);
                            if (dateCheck.CompareTo(item.DateUseStart) >= 0 && item.DateUseEnd.CompareTo(dateCheck) >= 0)
                            {
                                priceTotal = priceTotal + item.Rate;
                            }
                        }
                    }

                }
            }
            //
            //Response.Write(priceTotal.ToString());
            //lblTotalPrice.Text = priceTotal.ToString();
            //lblCheckIn.Text = dateStart.ToString("dddd,MMMM dd, yyyy");
            //lblCheckOut.Text = dateEnd.ToString("dddd,MMMM dd, yyyy");
            //lblDescription.Text = dateEnd.CompareTo(dateStart).ToString() + " Nights";

            //Country country = new Country();
            //lblCountry.Text = DropdownUtility.CountryList("country", country.GetCountryAll());


            //Guess Information
            string RequireInfo = "";

            switch (categoryID)
            {
                case 29:
                    RequireInfo = RequireInfo + "<table width=\"760\"><tr><td>Requirement</td></tr><tr><td>\n";
                    for (int productCount = 0; productCount < conditionID.Count(); productCount++)
                    {
                        if (quantity[productCount] > 0)
                        {

                            if (quantity[productCount] == 1)
                            {
                                RequireInfo = RequireInfo + "<table>\n";
                                RequireInfo = RequireInfo + "<tr><td colspan=\"3\">Room:</td></tr>\n";
                                RequireInfo = RequireInfo + "<tr><td>Max</td><td>Bed Type</td><td>Smoke</td><td>Floor</td></tr>\n";
                                RequireInfo = RequireInfo + "<tr><td>2 guests</td><td>" + DropdownUtility.BedType("bed_type_" + conditionID[productCount] + "_1") + "</td><td>" + DropdownUtility.SmokeType("smoke_type_" + conditionID[productCount] + "_1") + "</td><td>" + DropdownUtility.FloorType("floor_type_" + conditionID[productCount] + "_1") + "</td></tr>\n";
                                RequireInfo = RequireInfo + "<tr><td>Special Request: </td><td colspan=\"2\"><textarea name=\"sp_require_" + conditionID[productCount] + "_1\" style=\"width:300px;height:50px;\"></textarea></td></tr>\n";
                                RequireInfo = RequireInfo + "</table>\n";
                            }
                            else
                            {
                                for (int roomCount = 1; roomCount <= quantity[productCount]; roomCount++)
                                {
                                    RequireInfo = RequireInfo + "<table>\n";
                                    RequireInfo = RequireInfo + "<tr><td colspan=\"3\">Room:" + roomCount + "</td></tr>\n";
                                    RequireInfo = RequireInfo + "<tr><td>Max</td><td>Bed Type</td><td>Smoke</td><td>Floor</td></tr>\n";
                                    RequireInfo = RequireInfo + "<tr><td>2 guests</td><td>" + DropdownUtility.BedType("bed_type_" + conditionID[productCount] + "_" + roomCount) + "</td><td>" + DropdownUtility.SmokeType("smoke_type_" + conditionID[productCount] + "_" + roomCount) + "</td><td>" + DropdownUtility.FloorType("floor_type_" + conditionID[productCount] + "_" + roomCount) + "</td></tr>\n";
                                    RequireInfo = RequireInfo + "<tr><td>Special Request: </td><td colspan=\"2\"><textarea name=\"sp_require_" + conditionID[productCount] + "_" + roomCount + "\" style=\"width:300px;height:50px;\"></textarea></td></tr>\n";
                                    RequireInfo = RequireInfo + "</table>\n";
                                }
                            }
                        }
                    }
                    break;
                case 39:

                    break;
                case 40:

                    break;
            }


            RequireInfo = RequireInfo + "<input type=\"hidden\" name=\"booking_product_id\" value=\"" + Request.Form["booking_product_id"] + "\">\n";
            RequireInfo = RequireInfo + "<input type=\"hidden\" name=\"sid\" value=\"" + Request.Form["sid"] + "\">\n";
            RequireInfo = RequireInfo + "<input type=\"hidden\" name=\"hotel_id\" value=\"" + productID + "\">\n";
            RequireInfo = RequireInfo + "<input type=\"hidden\" name=\"date_start\" value=\"" + Request.Form["date_start"] + "\">\n";
            RequireInfo = RequireInfo + "<input type=\"hidden\" name=\"date_end\" value=\"" + Request.Form["date_end"] + "\">\n";


            RequireInfo = RequireInfo + selectOption + "<input type=\"hidden\" name=\"sum_price\" value=\"" + priceTotal + "\"></td></tr></table>\n";
            lblGuest.Text = RequireInfo;
        }
    }
}