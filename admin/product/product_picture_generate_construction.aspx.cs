using System;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
using Hotels2thailand.ProductOption;

namespace Hotels2thailand.UI
{
    public partial class admin_product_picture_generate_construction : Hotels2BasePage
    {
         protected void Page_Load(object sender, EventArgs e)
         {
             if (!this.Page.IsPostBack)
             {
                 Product cProduct = new Product();
                 cProduct = cProduct.GetProductById(int.Parse(this.qProductId));
                 txthead.Text = cProduct.ProductCode + "&nbsp;::&nbsp;" + cProduct.Title;


                 string Path = Request.QueryString["path"].Hotels2DecryptedData();
                 DirectoryInfo ProductPicFolder = new DirectoryInfo(Server.MapPath(Path));
                 int NumDir = ProductPicFolder.GetDirectories().Count();
                 int Count = 0;

                 if (NumDir > 0)
                 {
                     
                     //Option cOption = new Option();
                     ProductConstruction cConstruction = new ProductConstruction();
                     List<object> ILcConstruction = cConstruction.GetConstructionByProductID(int.Parse(this.qProductId));


                     //List<object> ILcOption = cOption.GetProductOptionByProductId_RoomOnly(int.Parse(this.qProductId));


                     StringBuilder Result = new StringBuilder();
                     Result.Append("<div class=\"mainItem\">");
                     //foreach (ProductConstruction ConItem in ILcConstruction)
                     //{
                     
                         foreach (DirectoryInfo dirItem in ProductPicFolder.GetDirectories())
                         {
                             //if (ConItem.Title.Trim().ToLower().Replace(" ", "-") == dirItem.Name.Trim().ToLower().Replace(" ", "-"))
                             //{
                             Result.Append("<p class=\"mainItem_Title\">" + dirItem.Name + "</p>");

                                 Result.Append("<div class=\"gpicGroup\">");
                                 DirectoryInfo OptionFile = new DirectoryInfo(Server.MapPath(Path + "/" + dirItem.Name));
                                 Result.Append("<div id=\"gpicListOverView\" class=\"pic_box_gen\">");
                                 foreach (FileInfo fileItem in OptionFile.GetFiles())
                                 {

                                     if (Regex.IsMatch(fileItem.Name, "^overview_small."))
                                     {
                                         Result.Append("<div class=\"pic_item_gen\">");
                                         Result.Append("<img src=\"" + Path + "/" + dirItem.Name + "/" + fileItem.Name + "\" title=\"overview_small\" />");
                                         Result.Append("</div>");

                                         Count = Count + 1;
                                     }

                                     if (Regex.IsMatch(fileItem.Name, "^large."))
                                     {
                                         Result.Append("<div class=\"pic_item_gen\">");
                                         Result.Append("<img src=\"" + Path + "/" + dirItem.Name + "/" + fileItem.Name + "\" title=\"large\"  />");
                                         Result.Append("</div>");

                                         Count = Count + 1;
                                     }

                                     if (Regex.IsMatch(fileItem.Name, "^thumb."))
                                     {
                                         Result.Append("<div class=\"pic_item_gen\">");
                                         Result.Append("<img src=\"" + Path + "/" + dirItem.Name + "/" + fileItem.Name + "\" title=\"thumb\"  />");
                                         Result.Append("</div>");

                                         Count = Count + 1;
                                     }

                                 }
                                 Result.Append("<div style=\"clear:both\"></div>");
                                 Result.Append("</div>");


                                 Result.Append("</div>");
                             //}
                         }
                    //}
                     Result.Append("</div>");
                     OptionPic.Text = Result.ToString();
                 }
                 else
                 {
                     
                     panelEmpty.Visible = true;
                     panelinsert.Visible = false;
                 }

                 if (Count == 0)
                 {
                     panelEmpty.Visible = true;
                     panelinsert.Visible = false;
                 }

            }
         }


         public void GenPic_ONclick(object sender, EventArgs e)
         {
             string Path = Request.QueryString["path"].Hotels2DecryptedData();
             DirectoryInfo ProductPicFolder = new DirectoryInfo(Server.MapPath(Path));
             int NumPic = ProductPicFolder.GetFiles().Count();
             int intProductId = int.Parse(this.qProductId);
             int Process = Hotels2thailand.Hotels2FolderAndPath.PicturefolderAndPathGenerate(intProductId);
             //Response.Write(Process);
             //Response.End();
             if (Process == 102)
             {
                 // error : Destination Folder Name Are Empty!! Please Insert
                 Response.Redirect("~/admin/hotels2exception.aspx");
                 return;
             }
             if (Process == 103)
             {
                 // error : There are no record Location in this product, Please Insert Lacation before
                 Response.Redirect("~/admin/hotels2exception.aspx");
                 return;
             }
             if (Process == 104)
             {
                 // error : Location Folder Name Are Empty!! Please Insert
                 Response.Redirect("~/admin/hotels2exception.aspx");
                 return;
             }
             if (Process == 0)
             {
                Product cProduct = new Product();
                cProduct = cProduct.GetProductById(int.Parse(this.qProductId));

                
                 // Product Module
                string typeTitle = string.Empty;
                string Cat_Title = ProductPicCategory.getCatTitleById(3);

               
                ProductPic cProductPic = new ProductPic();
                ProductPicName cProductPicContent = new ProductPicName();

                string PicFileName = string.Empty;
                string PicFileNamePath = string.Empty;

                string PicTitle = string.Empty;
                int PicId = 0;
                

                //Option cOption = new Option();
                //List<object> ILcOption = cOption.GetProductOptionByProductId_RoomOnly(int.Parse(this.qProductId));
                ProductConstruction cConstruction = new ProductConstruction();
                List<object> ILcConstruction = cConstruction.GetConstructionByProductID(int.Parse(this.qProductId));

                //if (ILcConstruction.Count > 0)
                //{
                    StringBuilder Result = new StringBuilder();
                    Result.Append("<div class=\"mainItem\">");
                    //foreach (ProductConstruction ConItem in ILcConstruction)
                    //{
                        foreach (DirectoryInfo dirItem in ProductPicFolder.GetDirectories())
                        {
                            //if (ConItem.Title.Trim().ToLower().Replace(" ", "-") == dirItem.Name.Trim().ToLower().Replace(" ", "-"))
                            //{
                            Result.Append("<p class=\"mainItem_Title\">" + dirItem.Name + "</p>");

                                Result.Append("<div class=\"gpicGroup\">");
                                DirectoryInfo OptionFile = new DirectoryInfo(Server.MapPath(Path + "/" + dirItem.Name));
                                Result.Append("<div id=\"gpicListOverView\" class=\"pic_box_gen\">");

                                int countoverview = 1;
                                int countlarge = 1;
                                int countThumb = 1;
                                int ConstructionId = 0;

                                ConstructionId = cConstruction.InsertProductConstruction(intProductId, 1, dirItem.Name, DateTime.Now, DateTime.Now, true);
                                cConstruction.InsertConStructionContent(ConstructionId, 1, dirItem.Name, dirItem.Name);

                                foreach (FileInfo fileItem in OptionFile.GetFiles())
                                {

                                    if (Regex.IsMatch(fileItem.Name, "^feature_hotel."))
                                    {
                                        typeTitle = ProductPicType.getTypeTitleById(2);
                                        PicTitle = Cat_Title + "-" + cProduct.Title + "-" + dirItem.Name + "-" + typeTitle;

                                        PicFileName = Hotels2thailand.Hotels2FolderAndPath.GeneratePictureFileName(cProduct.Title + "_" + dirItem.Name  , typeTitle, countoverview.ToString());
                                        PicFileNamePath = Hotels2thailand.Hotels2FolderAndPath.GetPicturePath(intProductId);

                                        PicId = cProductPic.InsertNewPicConstruction(3, 4, intProductId, ConstructionId, PicFileNamePath + PicFileName, PicTitle, 1);
                                        cProductPicContent.insertNewProductPicContent(PicId, 1, dirItem.Name);
                                        fileItem.MoveTo(Server.MapPath("../.." + PicFileNamePath + PicFileName));

                                        countoverview = countoverview + 1;

                                    }

                                    if (Regex.IsMatch(fileItem.Name, "^large."))
                                    {
                                        typeTitle = ProductPicType.getTypeTitleById(7);
                                        PicTitle = Cat_Title + "-" + cProduct.Title + "-" + dirItem.Name + "-" + typeTitle;

                                        PicFileName = Hotels2thailand.Hotels2FolderAndPath.GeneratePictureFileName(cProduct.Title + "_" + dirItem.Name, typeTitle, countlarge.ToString());
                                        PicFileNamePath = Hotels2thailand.Hotels2FolderAndPath.GetPicturePath(intProductId);

                                        PicId = cProductPic.InsertNewPicConstruction(3, 7, intProductId, ConstructionId, PicFileNamePath + PicFileName, PicTitle, 1);
                                        cProductPicContent.insertNewProductPicContent(PicId, 1, dirItem.Name);
                                        fileItem.MoveTo(Server.MapPath("../.." + PicFileNamePath + PicFileName));

                                        countlarge = countlarge + 1;
                                    }

                                    if (Regex.IsMatch(fileItem.Name, "^thumb."))
                                    {
                                        typeTitle = ProductPicType.getTypeTitleById(8);
                                        PicTitle = Cat_Title + "-" + cProduct.Title + "-" + dirItem.Name + "-" + typeTitle;

                                        PicFileName = Hotels2thailand.Hotels2FolderAndPath.GeneratePictureFileName(cProduct.Title + "_" + dirItem.Name, typeTitle, countThumb.ToString());
                                        PicFileNamePath = Hotels2thailand.Hotels2FolderAndPath.GetPicturePath(intProductId);

                                        PicId = cProductPic.InsertNewPicConstruction(3, 8, intProductId, ConstructionId, PicFileNamePath + PicFileName, PicTitle, 1);
                                        //cProductPicContent.insertNewProductPicContent(PicId, 1, OptionItem.Title);
                                        fileItem.MoveTo(Server.MapPath("../.." + PicFileNamePath + PicFileName));

                                        countThumb = countThumb + 1;
                                    }

                                }
                                Result.Append("<div style=\"clear:both\"></div>");
                                Result.Append("</div>");


                                Result.Append("</div>");
                                
                        }



                    //}


                    Result.Append("</div>");
                    OptionPic.Text = Result.ToString();
                //}
                
   
             }

             Response.Redirect(Request.Url.ToString());
             Response.End();
         }
        

         

        

        

         
    }
}