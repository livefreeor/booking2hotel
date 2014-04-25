using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
using Hotels2thailand.ProductOption;
using Hotels2thailand.Reviews;

namespace Hotels2thailand.UI
{
    public partial class admin_product_picture_generate : Hotels2BasePage
    {
         protected void Page_Load(object sender, EventArgs e)
         {
             if (!this.Page.IsPostBack)
             {

                 Product cProduct = new Product();
                 cProduct = cProduct.GetProductById(int.Parse(this.qProductId));
                 txthead.Text = cProduct.ProductCode + "&nbsp;::&nbsp;" + cProduct.Title;

                 CheckHotelPrepare();
            }
         }
         private string MainSourcePath = "../../hotels2thailand_images_source/";

        
         private string _hotelfolder = string.Empty;
         public string HotelFolder
         {
             get { return _hotelfolder; }
             set { _hotelfolder = value; }
         }
         public void CheckHotelPrepare()
         {
             Product cProduct = new Product();
             cProduct = cProduct.GetProductById(int.Parse(this.qProductId));
             //========================== STEP 1 
             bool IsOk = false;
             string strDes = cProduct.GetProductById(int.Parse(this.qProductId)).DestinationFolderName;
             if (string.IsNullOrEmpty(strDes))
             {
                 IsOk = false;
             }
             else
             {
                 IsOk = true;
                 DirectoryInfo dirSourcePath_Destination = new DirectoryInfo(HttpContext.Current.Server.MapPath(MainSourcePath + strDes));
                 if (!dirSourcePath_Destination.Exists)
                 {
                     dirSourcePath_Destination.Create();
                 }
             }

             if(IsOk)
                 IsOk = true;


             if (IsOk)
                 imgDesCheck.ImageUrl = "~/images/true_b.png";
             else
                 imgDesCheck.ImageUrl = "~/images/false_b.png";

             //---------------------------------------------END STEP 1-------------
             //========================== STEP 2
             bool IsOk2 = false;

             ProductLocation cProductLocation = new ProductLocation();
             cProductLocation = cProductLocation.getTopOneLactionInProduct(int.Parse(this.qProductId));
             string PathUpdate = string.Empty;
             string strLocationCheck = string.Empty;
             if (cProductLocation == null)
             {
                 IsOk2 = false;

             }
             else
             {
                 strLocationCheck = cProductLocation.getDefaultLocationPath(int.Parse(this.qProductId));
                
                if (string.IsNullOrEmpty(strLocationCheck))
                {
                    //Response.Write("HELLO");
                    //Response.End();
                    cProductLocation.InsertDefaultLocationPath(int.Parse(this.qProductId), cProductLocation.LocationID);
                }

                IsOk2 = true;
                strLocationCheck = cProductLocation.getDefaultLocationPath(int.Parse(this.qProductId));
                DirectoryInfo dirSourcePath_HotelItem = new DirectoryInfo(HttpContext.Current.Server.MapPath(MainSourcePath + strDes + "/" + strLocationCheck));
                if (!dirSourcePath_HotelItem.Exists)
                {
                   
                    dirSourcePath_HotelItem.Create();
                }
                
                PathUpdate = MainSourcePath + strDes + "/" + strLocationCheck;

              }

             
             string strHotelFolder  = (cProduct.ProductCode + "_" + cProduct.Title).Trim().ToLower().Replace(" ", "_");

             if (IsOk2)
             {
                 imgLocCheck.ImageUrl = "~/images/true_b.png";
                 lblPathUpLoad.Text = PathUpdate;

                 lblHotelFolder.Text = strHotelFolder;
                 //
             }
             else
                 imgLocCheck.ImageUrl = "~/images/false_b.png";
             //---------------------------------------------END STEP 2-------------
             DirectoryInfo dirSourceHotelFolder = new DirectoryInfo(Server.MapPath(MainSourcePath + strDes + "/" + strLocationCheck + "/" + strHotelFolder));
             if(dirSourceHotelFolder.Exists)
                 imgHotelCheck.ImageUrl = "~/images/true_b.png";
                 
             else
                 imgHotelCheck.ImageUrl = "~/images/false_b.png";


             DirectoryInfo ProductPicFolder = new DirectoryInfo(Server.MapPath(MainSourcePath + strDes + "/" + strLocationCheck + "/" + strHotelFolder + "/Product"));
             if (ProductPicFolder.Exists)
             {
                 int PicCount = ProductPicFolder.GetFiles().Count();
                 if (PicCount > 0)
                 {
                     imgProductCheck.ImageUrl = "~/images/true_b.png";
                     panelgProduct.Visible = true;
                     gProductLink.NavigateUrl = gProductLink.NavigateUrl + "?path=" + (MainSourcePath + strDes + "/" + strLocationCheck + "/" + strHotelFolder + "/Product").Hotels2EncryptedData() + this.AppendCurrentQueryString();
                 }
             }
             else
                 imgProductCheck.ImageUrl = "~/images/false_b.png";

             DirectoryInfo OptionPicFolder = new DirectoryInfo(Server.MapPath(MainSourcePath + strDes + "/" + strLocationCheck + "/" + strHotelFolder + "/Option"));
             if (OptionPicFolder.Exists)
             {
                 int FolderPicCount = OptionPicFolder.GetDirectories().Count();
                 if (FolderPicCount > 0)
                 {
                     Option cOption = new Option();
                     StringBuilder result = new StringBuilder();
                     result.Append("<div class=\"room_check_item\">");
                     foreach (Option optionItem in cOption.GetProductOptionByProductId_RoomOnly(int.Parse(this.qProductId)))
                     {
                         int Count = 0;
                         foreach (DirectoryInfo folrderName in OptionPicFolder.GetDirectories())
                         {
                             if (optionItem.Title.Trim().ToLower().Replace(" ", "-") == folrderName.Name.Trim().ToLower().Replace(" ", "-"))
                             {
                                 Count = Count + 1;
                             }
                         }
                         if (Count > 0)
                             result.Append("<p>" + optionItem.Title + "<img src=\"../../images/true.png\"/></p>");
                         else
                             result.Append("<p>" + optionItem.Title + "<img src=\"../../images/false.png\"/></p>");
                     }

                     result.Append("</div>");

                     lblRoomList.Text = result.ToString();

                     int picCount = 0;
                     foreach (DirectoryInfo folrderName in OptionPicFolder.GetDirectories())
                     {
                         DirectoryInfo OptionPicFolderSub = new DirectoryInfo(Server.MapPath(MainSourcePath + strDes + "/" + strLocationCheck + "/" + strHotelFolder + "/Option/" + folrderName.Name));
                         foreach (FileInfo fileItem in OptionPicFolderSub.GetFiles())
                         {
                             picCount = picCount + 1;
                         }
                     }





                     if (picCount == 0)
                     {
                         imgOptionCheck.ImageUrl = "~/images/false_b.png";
                         panelgOption.Visible = false;
                     }
                     else
                     {
                         imgOptionCheck.ImageUrl = "~/images/true_b.png";
                         panelgOption.Visible = true;
                         gOptionLink.NavigateUrl = gOptionLink.NavigateUrl + "?path=" + (MainSourcePath + strDes + "/" + strLocationCheck + "/" + strHotelFolder + "/Option").Hotels2EncryptedData() + this.AppendCurrentQueryString();
                     }
                 }
             }
             else
                 imgOptionCheck.ImageUrl = "~/images/false_b.png";

             DirectoryInfo ConstructionPic = new DirectoryInfo(Server.MapPath(MainSourcePath + strDes + "/" + strLocationCheck + "/" + strHotelFolder + "/Construction"));
             if (ConstructionPic.Exists)
             {
                 int FolderPicCount = ConstructionPic.GetDirectories().Count();
                 if (FolderPicCount > 0)
                 {
                     int picCount = 0;
                     foreach (DirectoryInfo folrderName in ConstructionPic.GetDirectories())
                     {
                         DirectoryInfo ConstructionPicSub = new DirectoryInfo(Server.MapPath(MainSourcePath + strDes + "/" + strLocationCheck + "/" + strHotelFolder + "/Construction/" + folrderName.Name));
                         foreach (FileInfo fileItem in ConstructionPicSub.GetFiles())
                         {
                             picCount = picCount + 1;
                         }
                     }

                     if (picCount == 0)
                     {
                         imgConstructionCheck.ImageUrl = "~/images/false_b.png";
                         panelgConstruction.Visible = false;
                     }
                     else
                     {
                         imgConstructionCheck.ImageUrl = "~/images/true_b.png";
                         panelgConstruction.Visible = true;
                         gConlink.NavigateUrl = gConlink.NavigateUrl + "?path=" + (MainSourcePath + strDes + "/" + strLocationCheck + "/" + strHotelFolder + "/Construction").Hotels2EncryptedData() + this.AppendCurrentQueryString();
                     }
                     
                 }
             }
             else
                 imgConstructionCheck.ImageUrl = "~/images/false_b.png";
         }

            public void btnCheck_OnClick(object sender, EventArgs e)
             {
                 CheckHotelPrepare();
                 Response.Redirect(Request.Url.ToString());
             }

        

             public void btnCheck2_OnClick(object sender, EventArgs e)
             {
                 CheckHotelPrepare();
                 Response.Redirect(Request.Url.ToString());
             }

             
         
         }

    



         

}