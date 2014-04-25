using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
using Hotels2thailand.Suppliers;
using Hotels2thailand;
using Hotels2thailand.Staffs;
namespace Hotels2thailand.UI
{
    public partial class ajax_product_extranet_list : System.Web.UI.Page
    {
        public string qProductListType { get { return Request.QueryString["type"]; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.IsPostBack)
            {
                try
                {

                    Response.Write(SupplierAndProductExtranetList());
                }
                catch(Exception ex)
                {
                    Response.Write(ex.Message + "<br/>" + ex.StackTrace);
                }
               Response.End();

            }
        }



        public string SupplierAndProductExtranetList()
        {
            StaffSessionAuthorize cStaff = new StaffSessionAuthorize();
            string StaffCAt = cStaff.HotelsSessionItem;
            
            short shrDestinationId = short.Parse(Request.Form["ctl00$ContentPlaceHolder1$dropDestination"]);
            byte bytStatusProcess = byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$dropStatusProcess"]);
            bool bolStatus = bool.Parse(Request.Form["ctl00$ContentPlaceHolder1$dropStatus"]);
            ProductListAdmin cProductList = new ProductListAdmin();

            IList<object> iProductList = new List<object>();
            
                switch (this.qProductListType)
                {
                    case "normal":
                        iProductList = cProductList.getProductListProcedure(29, shrDestinationId, bytStatusProcess, bolStatus);
                        break;
                    case "bht":
                        iProductList = cProductList.getProductListBhtManage();
                        break;
                    case "bhtb2b":
                        iProductList = cProductList.getProductListBhtManageB2b();
                        break;
                    case "hotel":
                        iProductList = cProductList.getProductListHotelManage();
                        break;
                    case "flat":
                        iProductList = cProductList.getProductListComFlatrate();
                        break;
                    case "monthly":
                        iProductList = cProductList.getProductListComMonthly();
                        break;
                    case "step":
                        iProductList = cProductList.getProductListComStep();
                        break;
                    case "hoff":
                        iProductList = cProductList.getProductListHotelManage_Offline();
                        break;
                    case "hon":
                        iProductList = cProductList.getProductListHotelManage_Online();
                        break;
                }
            

            


            StringBuilder result = new StringBuilder();


            result.Append("<table cellpadding=\"0\" cellspacing=\"1\" width=\"100%\" style=\"background-color:#d8dfea;\" >");
            result.Append("<tr style=\"background-color:#3f5d9d;height:25px;color:#ffffff;text-align:center;\"><th width=\"5%\">Gateway</th><th width=\"5%\">No.</th>");
            if (StaffCAt != "12")
                result.Append("<th width=\"10%\">Code</th>");

            result.Append("<th width=\"55%\">Hotel Name</th><th width=\"10%\">Manage</th></tr>");


            //result.Append("<img src=\"http://www.booking2hotels.com/images/supplier.png\" />"+sup.SupplierTitle + "</td></tr>");
            string Rowcolor = "";
            int count = 0;
            short StatusTmp = 0;

            
            foreach (ProductListAdmin product in iProductList)
            {
                count = count + 1;
                Rowcolor = "#eceff5";
                if (count % 2 == 0)
                    Rowcolor = "#ffffff";
                //string URL = string.Empty;
                //string DesFolderName = product.DesFolderName;
                //string FileName = product.ProductFileName;
                //string FolderResult = DesFolderName + "-" + Hotels2thailand.Front.Utility.GetProductType(29)[0, 3];

                //URL = "http://www.hotels2thailand.com/" + FolderResult + "/" + FileName.Trim();

                string UrlExtranet = "/extranet/mainextra.aspx?pid=" + product.ProductId + "&supid=" + product.SupplierId;
                string UrlManage = "/admin/extranet/extranetManage.aspx?pid=" + product.ProductId + "&supid=" + product.SupplierId;
                string UrlProductManage = "/admin/product/product.aspx?pid=" + product.ProductId + "&supid=" + product.SupplierId + "&pdcid=29";

                if (StatusTmp != product.StatusId)
                {
                    result.Append("<tr style=\"background-color:#f2f2f2;\"><td colspan=\"10\" align=\"center\" style=\"font-size:14px;font-weight:bold;color:#9b0b0b;\">" + product.StatusTitle + "</td></tr>");
                }

                result.Append("<tr id=\"product_row_" + product.ProductId + "\" title=\""+product.ProductId+"\" class=\"product_row\" onmouseover=\"changein(this);\" onmouseout=\"changeout(this);\" style=\"height:25px;background-color:" + Rowcolor + ";\">");
                result.Append("<td align=\"center\" >" + Hotels2String.GetImgBank(product.GateWayId, product.ManageID, product.BookingType) + "</td>");
                result.Append("<td align=\"center\">" + count + "</td>");
                if (StaffCAt != "12")
                result.Append("<td align=\"center\"><a href=\"" + UrlProductManage + "\" target=\"_Blank\" >" + product.ProductCode + "</a></td>");

                result.Append("<td style=\"text-align:left;\">&nbsp;&nbsp;<a href=\"" + product.WebsiteName + "\" target=\"_Blank\" >" + product.Producttitle + "</a></td>");

                result.Append("<td align=\"center\"><a href=\"" + UrlExtranet + "\" target=\"_Blank\" >Manage</a></td>");
                //result.Append("<td id=\"_book_" + product.ProductId + "\" title=\"1\" align=\"center\"></td>");
                //result.Append("<td id=\"_map_" + product.ProductId + "\" title=\"2\" align=\"center\"></td>");
                //result.Append("<td id=\"_review_" + product.ProductId + "\" title=\"3\" align=\"center\"></td>");
                //result.Append("<td id=\"_review_write_" + product.ProductId + "\" title=\"4\" align=\"center\"></td>");
                //result.Append("<td id=\"_thankyou_" + product.ProductId + "\" title=\"5\" align=\"center\"></td>");
                result.Append("</tr>");
                StatusTmp = product.StatusId;
            }




            result.Append("");
            result.Append("</table>");


            return result.ToString();
        }




    }
}