using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Caching;
using System.IO;
using System.Collections;
using System.Collections.Specialized;
using Hotels2thailand.Production;
using System.Text;
using System.Text.RegularExpressions;
using Hotels2thailand.Front;
/// <summary>
/// Summary description for Hotels2FolderAndPath
/// </summary>
/// 
namespace Hotels2thailand
{
    public class Hotels2FolderAndPath
    {
        //default folder of All Picture in Hotels2thailand
        //private string _default_picture_folder_path = "/picture/hotels/";

        public Hotels2FolderAndPath()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static string GetPicturePath(int intProductId)
        {
            Product cProduct = new Product();
            cProduct = cProduct.GetProductById(intProductId);
            int GetPath = PicturefolderAndPathGenerate(intProductId);

            string DestinationFolderName = cProduct.DestinationFolderName;
            string FolderProductCat = Utility.GetProductType(cProduct.ProductCategoryID)[0, 3];

            StringBuilder cPicturePath = new StringBuilder("/pictures/" + FolderProductCat + "/");

            
            ProductLocation cProductLocation = new ProductLocation();
            string strLocationFolderName = cProductLocation.getDefaultLocationPath(intProductId);
            cPicturePath.Append(DestinationFolderName.ToLower().Replace(" ","-"));
            cPicturePath.Append("/");

            // If Product Cat == Hotel Do Something.. 
            //if (cProduct.ProductCategoryID == 29)
            //{
            //    cPicturePath.Append(strLocationFolderName.ToLower().Replace(" ", "-"));
            //    cPicturePath.Append("/");
            //}
            
            return cPicturePath.ToString();
        }

        public static string GeneratePictureFileName(string strtitleName, string strTypeName, string strNumber)
        {

            StringBuilder strFilenameGen = new StringBuilder();
            string NewProductName = Regex.Replace(strtitleName.Trim(), "[.'\\-,]", "");
            NewProductName = Regex.Replace(NewProductName, "[\\s/]", "_").ToLower();
            NewProductName = NewProductName.Replace("__", "_");
            NewProductName = NewProductName.Replace("___", "_");
            NewProductName = NewProductName.Replace("____", "_");
           
            //string NewCatName = Regex.Replace(strCatName, "[\\s]", "_");

            strFilenameGen.Append(NewProductName);
            strFilenameGen.Append("_");
            //strFilenameGen.Append(NewCatName);
            //strFilenameGen.Append("_");
            strFilenameGen.Append(strTypeName);
            strFilenameGen.Append("_");
            strFilenameGen.Append(strNumber);
            strFilenameGen.Append(".jpg");
            return strFilenameGen.ToString();
        }

        public static int PicturefolderAndPathGenerate(int intProductId)
        {
            // Prodec Success;
           int ErrorType = 0;
           string shrDesId = string.Empty;
           string strLocationFolderName = string.Empty;
           Product cProduct = new Product();
           cProduct = cProduct.GetProductById(intProductId);

           string FolderProductCat = Utility.GetProductType(cProduct.ProductCategoryID)[0, 3];
           string _default_picture_folder_path = "../../pictures/" + FolderProductCat + "/";

           
           if (!string.IsNullOrEmpty(cProduct.DestinationFolderName))
           { 
               shrDesId = cProduct.GetProductById(intProductId).DestinationFolderName.ToLower().Replace(" ", "-");
               DirectoryInfo PicturePath_Des = new DirectoryInfo(HttpContext.Current.Server.MapPath(_default_picture_folder_path) + shrDesId);
               if (!PicturePath_Des.Exists)
               {
                   PicturePath_Des.Create();
               }
           }
           else
           {
               // error : Destination Folder Name Are Empty!! Please Insert
               ErrorType = 102;
               return ErrorType;
           }

           if (cProduct.ProductCategoryID == 29)
           {
               ProductLocation cProductLocation = new ProductLocation();
               //HttpContext.Current.Response.Write(cProductLocation.getTopOneLactionInProductAndLocation(23, 2).LocationFolderName);
               //HttpContext.Current.Response.End();

               if (cProductLocation.getTopOneLactionInProduct(intProductId) == null)
               {
                   // error : There are no record Location in this product, Please Insert Lacation before
                   ErrorType = 103;
                   return ErrorType;
               }
               else
               {
                   //HttpContext.Current.Response.Write("test");
                   //HttpContext.Current.Response.End();
                   if (cProductLocation.getTopOneLactionInProduct(intProductId) != null)
                   {

                       if (string.IsNullOrEmpty(cProductLocation.getDefaultLocationPath(intProductId)))
                       {
                           //HttpContext.Current.Response.Write(cProductLocation.getDefaultLocationPath(intProductId));
                           //HttpContext.Current.Response.End();

                           cProductLocation.InsertDefaultLocationPath(intProductId, cProductLocation.getTopOneLactionInProduct(intProductId).LocationID);
                       }

                       if (!string.IsNullOrEmpty(cProductLocation.getDefaultLocationPath(intProductId)))
                       {
                           strLocationFolderName = cProductLocation.getDefaultLocationPath(intProductId).ToLower().Replace(" ", "-");
                           //HttpContext.Current.Response.Write(strLocationFolderName);
                           //HttpContext.Current.Response.End();
                           DirectoryInfo PicturePath_Location = new DirectoryInfo(HttpContext.Current.Server.MapPath(_default_picture_folder_path) + shrDesId + "/" + strLocationFolderName);
                           if (!PicturePath_Location.Exists)
                           {
                               PicturePath_Location.Create();
                           }
                       }

                   }
                   else
                   {

                       // error : Location Folder Name Are Empty!! Please Insert
                       ErrorType = 104;
                       return ErrorType;
                   }

               }
           }
           return ErrorType;
           
        }
       
    }
}