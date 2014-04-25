using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EvoPdf.HtmlToPdf;
using Hotels2thailand.Front;
using Hotels2thailand.Production;

/// <summary>
/// Summary description for PdfGenerateCore
/// </summary>
/// 
namespace Hotels2thailand.PDF
{
    public class PdfGenerateCore
    {
        public PdfGenerateCore()
        {

        }


        public void GenrateReviewAllProduct(byte bytProductCat)
        {
            //bool IsCompleted = false;
            Hotels2thailand.Production.Destination cDestination = new Production.Destination();
            foreach (KeyValuePair<string, string> des in cDestination.GetDestinationAll())
            {
                Hotels2thailand.Production.Product cProduct = new Production.Product();
                if (cProduct.GetProductByDestionIdAndCatId(short.Parse(des.Key), bytProductCat).Count > 0)
                {
                    foreach (int ProductId in cProduct.GetProductByDestionIdAndCatId(short.Parse(des.Key), bytProductCat))
                    {
                        PdfGenerateByProductID(ProductId, bytProductCat);
                    }
                }
            }

        }

        public void PdfGenerateByProductID(int intProductID, byte bytProductCat)
        {
            ProductContent cProductContent = new ProductContent();
            Destination cDestination = new Destination();
            string FolderName = cDestination.GetDestinationFolderNameByProcutId(intProductID);
            string FolderResult = FolderName + "-" + Utility.GetProductType(bytProductCat)[0, 3];

            foreach(ProductContent item in cProductContent.GetProductContentByProductId(intProductID))
            {
                if(!string.IsNullOrEmpty(item.FileMain))
                {
                    HttpContext.Current.Response.Write(GenPdf(FolderResult, FolderResult, item.FilePDF, item.FileMain) + "<br/>");
                    HttpContext.Current.Response.Flush();
                }
            }
            
        }

        private string GenPdf(string Url, string Path, string strfilename, string stringHotelFilename)
        {
            string fileName = strfilename;
            string MainSite = "http://www.hotels2thailand.com/";
            string urlToConvert = MainSite + Url.Trim() + "/" + stringHotelFilename;

            //string fileName = strfilename.Hotels2RightCrl(3)+ "pdf";
            // Create the PDF converter. Optionally the HTML viewer width can be specified as parameter
            // The default HTML viewer width is 1024 pixels.
            PdfConverter pdfConverter = new PdfConverter();

            // set the license key - required
            //DEmo LicenseKey
           // pdfConverter.LicenseKey = "ORIJGQoKGQkZCxcJGQoIFwgLFwAAAAA=";

            //Price LicenseKey
            pdfConverter.LicenseKey = "p4yWh5SUh5GRh5WJl4eUlomWlYmenp6e";

            // set the converter options - optional
            pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4;
            pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal;
            pdfConverter.PdfDocumentOptions.PdfPageOrientation = PdfPageOrientation.Portrait;

            byte[] pdfBytes = pdfConverter.GetPdfBytesFromUrl(urlToConvert);

            DirectoryInfo folder = new DirectoryInfo(HttpContext.Current.Server.MapPath("/" + Path));
            if (!folder.Exists)
            {
                folder.Create();
            }

            FileInfo des = new FileInfo(HttpContext.Current.Server.MapPath("/" + Path + "/" + fileName));

            FileStream pdfWriter = des.Open(FileMode.Append, FileAccess.Write);
            pdfWriter.Write(pdfBytes, 0, pdfBytes.Length);
            pdfWriter.Close();

            return urlToConvert;
        }
    }
}