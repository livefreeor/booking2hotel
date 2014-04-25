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
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using Hotels2thailand.Booking;
using System.Data.SqlClient;
using Hotels2thailand.DataAccess;




/// <summary>
/// Summary description for Hotels2Stringformat
/// </summary>
/// 
namespace Hotels2thailand
{
    public static class Hotels2XMLContent 
    {
        //public static Dictionary<string, string> TEstGen()
        //{ 
        //    DataAccess.DataConnect dd = new DataConnect();
        //    Dictionary<string,string> ff = new Dictionary<string,string>();
        //    using(SqlConnection cn = new SqlConnection(dd.ConnectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("SELECT booking_item_id, condition_detail FROM tbl_booking_item WHERE booking_product_id = 118831",cn);
                
        //        cn.Open();
        //        IDataReader reader = dd.ExecuteReader(cmd);
        //        while(reader.Read())
        //        {
        //            ff.Add(reader[0].ToString() + "," + "TEST",reader[1].ToString());
        //        }
        //        return ff;
        //    }
            
        //}


        public static string Hotels2XMlReader(this string XmlSource)
        {
            

            StringBuilder strXML = new StringBuilder();
            XmlDocument doc = new XmlDocument();
            int find = XmlSource.IndexOf("</contents>");

            //if (find > 0 )
            //{
                //    try
                //    {
                doc.LoadXml(XmlSource);
                if (doc.HasChildNodes && doc.ChildNodes[0].Name == "contents")
                {

                    XmlNode root = doc.DocumentElement;
                    if (root.Name == "contents")
                    {
                        //XmlNodeList list = root.ChildNodes.
                        //int ParentCount = root.ChildNodes.Count;

                        //for (int countGen = 1; countGen >= ParentCount; countGen++)
                        //{

                        //}
                        strXML.Append("<div id=\"detail_main\">");

                        foreach (XmlNode ParentNodeStyle in root)
                        {
                            if (ParentNodeStyle.Name == "content")
                            {
                                string styleType = ParentNodeStyle.Attributes["style"].Value;
                                switch (styleType)
                                {
                                    case "1":
                                        int childStyle1Count = ParentNodeStyle.ChildNodes.Count;
                                        if (childStyle1Count == 2)
                                        {
                                            XmlNode Head_node = ParentNodeStyle.ChildNodes[0];

                                            //HttpContext.Current.Response.Write(ParentNodeStyle.ChildNodes[0].InnerText);
                                            //HttpContext.Current.Response.End();
                                            strXML.Append("<div class=\"style\">");
                                            if (Head_node.Name == "head")
                                            {

                                                strXML.Append("<p class=\"header\">");
                                                strXML.Append(ParentNodeStyle.ChildNodes[0].InnerText);
                                                strXML.Append("</p>");
                                            }

                                            XmlNode List_node = ParentNodeStyle.ChildNodes[1];

                                            strXML.Append("<ul>");

                                            if (List_node.Name == "List")
                                            {
                                                foreach (XmlNode NodeListItem in List_node)
                                                {
                                                    strXML.Append("<li>");
                                                    strXML.Append(NodeListItem.InnerText);
                                                    strXML.Append("</li>");
                                                }
                                            }

                                            strXML.Append("</ul>");
                                            strXML.Append("</div>");
                                        }

                                        break;
                                    case "2":
                                        int childStyle2Count = ParentNodeStyle.ChildNodes.Count;
                                        if (childStyle2Count == 2)
                                        {
                                            XmlNode Head_node = ParentNodeStyle.ChildNodes[0];
                                            strXML.Append("<div class=\"style\">");
                                            if (Head_node.Name == "head")
                                            {
                                                strXML.Append("<p class=\"header\">");
                                                strXML.Append(ParentNodeStyle.ChildNodes[0].InnerText);
                                                strXML.Append("</p>");
                                            }

                                            XmlNode ParagraphNode = ParentNodeStyle.ChildNodes[1];
                                            if (ParagraphNode.Name == "paragraph")
                                            {
                                                foreach (XmlNode NodeP_Item in ParagraphNode)
                                                {
                                                    strXML.Append("<p>");
                                                    strXML.Append(NodeP_Item.InnerText);
                                                    strXML.Append("</p>");

                                                }
                                            }
                                            strXML.Append("</div>");
                                        }

                                        break;
                                    case "3":
                                        strXML.Append("<div class=\"style\">");
                                        foreach (XmlNode ParagrahNode in ParentNodeStyle)
                                        {
                                            if (ParagrahNode.Name == "paragraph")
                                            {
                                                int childStyle3Count = ParagrahNode.ChildNodes.Count;
                                                if (childStyle3Count == 2)
                                                {
                                                    XmlNode Head_node = ParagrahNode.ChildNodes[0];
                                                    //strXML.Append("<div class=\"XMLstyle_Pr\" style=\"font-size:12px; width:100%; margin:10px 0px 0px 0px; padding:0px;\">");

                                                    if (Head_node.Name == "head")
                                                    {
                                                        strXML.Append("<p class=\"header\">");
                                                        strXML.Append(Head_node.InnerText);
                                                        strXML.Append("</p>");
                                                    }

                                                    XmlNode P_Node = ParagrahNode.ChildNodes[1];
                                                    if (P_Node.Name == "p")
                                                    {
                                                        strXML.Append("<p>");
                                                        strXML.Append(P_Node.InnerText);
                                                        strXML.Append("</p>");
                                                    }

                                                    //strXML.Append("</div>");
                                                }
                                            }
                                        }
                                        strXML.Append("</div>");

                                        break;
                                }

                            }
                        }
                        strXML.Append("</div>");
                    }
                }
            //}
            //else
            //{
            //    strXML.Append("error");
            //}
            //        else
            //        {
            //            strXML.Append(XmlSource);
            //        }
            //    }
            //    catch
            //    {
            //        strXML.Append(XmlSource);
            //    }
            //}
            //else
            //{
            //    strXML.Append(XmlSource);
            //}
            return strXML.ToString();
            //XmlNode root = doc.DocumentElement;


        }


        // Display for Detail information -PM Page
        public static string Hotels2XMlReader_IgnoreError(this string XmlSource)
        {
            StringBuilder strXML = new StringBuilder();
            XmlDocument doc = new XmlDocument();
            int find = XmlSource.IndexOf("</contents>");

            if (find > 0)
            {
                try
                {
                    doc.LoadXml(XmlSource);
                    if (doc.HasChildNodes && doc.ChildNodes[0].Name == "contents")
                    {

                        XmlNode root = doc.DocumentElement;
                        if (root.Name == "contents")
                        {
                            //XmlNodeList list = root.ChildNodes.
                            //int ParentCount = root.ChildNodes.Count;

                            //for (int countGen = 1; countGen >= ParentCount; countGen++)
                            //{

                            //}
                            strXML.Append("<div id=\"detail_main\">");

                            foreach (XmlNode ParentNodeStyle in root)
                            {
                                if (ParentNodeStyle.Name == "content")
                                {
                                    string styleType = ParentNodeStyle.Attributes["style"].Value;
                                    switch (styleType)
                                    {
                                        case "1":
                                            int childStyle1Count = ParentNodeStyle.ChildNodes.Count;
                                            if (childStyle1Count == 2)
                                            {
                                                XmlNode Head_node = ParentNodeStyle.ChildNodes[0];

                                                //HttpContext.Current.Response.Write(ParentNodeStyle.ChildNodes[0].InnerText);
                                                //HttpContext.Current.Response.End();
                                                strXML.Append("<div class=\"style\">");
                                                if (Head_node.Name == "head")
                                                {

                                                    strXML.Append("<p class=\"header\">");
                                                    strXML.Append(ParentNodeStyle.ChildNodes[0].InnerText);
                                                    strXML.Append("</p>");
                                                }

                                                XmlNode List_node = ParentNodeStyle.ChildNodes[1];

                                                strXML.Append("<ul>");

                                                if (List_node.Name == "List")
                                                {
                                                    foreach (XmlNode NodeListItem in List_node)
                                                    {
                                                        strXML.Append("<li>");
                                                        strXML.Append(NodeListItem.InnerText);
                                                        strXML.Append("</li>");
                                                    }
                                                }

                                                strXML.Append("</ul>");
                                                strXML.Append("</div>");
                                            }

                                            break;
                                        case "2":
                                            int childStyle2Count = ParentNodeStyle.ChildNodes.Count;
                                            if (childStyle2Count == 2)
                                            {
                                                XmlNode Head_node = ParentNodeStyle.ChildNodes[0];
                                                strXML.Append("<div class=\"style\">");
                                                if (Head_node.Name == "head")
                                                {
                                                    strXML.Append("<p class=\"header\">");
                                                    strXML.Append(ParentNodeStyle.ChildNodes[0].InnerText);
                                                    strXML.Append("</p>");
                                                }

                                                XmlNode ParagraphNode = ParentNodeStyle.ChildNodes[1];
                                                if (ParagraphNode.Name == "paragraph")
                                                {
                                                    foreach (XmlNode NodeP_Item in ParagraphNode)
                                                    {
                                                        strXML.Append("<p>");
                                                        strXML.Append(NodeP_Item.InnerText);
                                                        strXML.Append("</p>");

                                                    }
                                                }
                                                strXML.Append("</div>");
                                            }

                                            break;
                                        case "3":
                                            strXML.Append("<div class=\"style\">");
                                            foreach (XmlNode ParagrahNode in ParentNodeStyle)
                                            {
                                                if (ParagrahNode.Name == "paragraph")
                                                {
                                                    int childStyle3Count = ParagrahNode.ChildNodes.Count;
                                                    if (childStyle3Count == 2)
                                                    {
                                                        XmlNode Head_node = ParagrahNode.ChildNodes[0];
                                                        //strXML.Append("<div class=\"XMLstyle_Pr\" style=\"font-size:12px; width:100%; margin:10px 0px 0px 0px; padding:0px;\">");

                                                        if (Head_node.Name == "head")
                                                        {
                                                            strXML.Append("<p class=\"header\">");
                                                            strXML.Append(Head_node.InnerText);
                                                            strXML.Append("</p>");
                                                        }

                                                        XmlNode P_Node = ParagrahNode.ChildNodes[1];
                                                        if (P_Node.Name == "p")
                                                        {
                                                            strXML.Append("<p>");
                                                            strXML.Append(P_Node.InnerText);
                                                            strXML.Append("</p>");
                                                        }

                                                        //strXML.Append("</div>");
                                                    }
                                                }
                                            }
                                            strXML.Append("</div>");

                                            break;
                                    }

                                }
                            }
                            strXML.Append("</div>");
                        }
                    }
                    else
                    {
                        strXML.Append(XmlSource);
                    }
                }
                catch
                {
                    strXML.Append(XmlSource);
                }
            }
            else
            {
                strXML.Append(XmlSource);
            }
            
            
            
            return strXML.ToString();
            //XmlNode root = doc.DocumentElement;
            
           
        }

        public static string Hotels2XMLReaderPromotionDetailExtranet_Front(this string XmlSource)
        {
            StringBuilder strXML = new StringBuilder();
            ////ArrayList arrList = new ArrayList();

            //XmlDocument Maindoc = new XmlDocument();
            //Maindoc.LoadXml(XmlSource);
            if (!string.IsNullOrEmpty(XmlSource))
            {

                XmlDocument doc = new XmlDocument();
                //HttpContext.Current.Response.Write(PromotionDetailNode.InnerXml);
                //HttpContext.Current.Response.Flush();
                doc.LoadXml(XmlSource);


                XmlNode root = doc.DocumentElement;

                if (root.Name == "PromotionShow")
                {
                    //HttpContext.Current.Response.Write("55");
                    //HttpContext.Current.Response.End();

                    //strXML.Append("<div class=\"XML_div_style1\" style=\"width:100%; margin:10px 0px 0px 0px; padding:0px;\">");
                    if (root.ChildNodes.Count == 2)
                    {
                        XmlNode Head_node = root.ChildNodes[0];
                        if (Head_node.Name == "head")
                        {
                            strXML.Append("<strong>");
                            strXML.Append(root.ChildNodes[0].InnerText);
                            strXML.Append("</strong>");
                        }

                        XmlNode List_node = root.ChildNodes[1];
                        strXML.Append("<ul class=\"list_promotion_detail\">");
                        if (List_node.Name == "List")
                        {
                            foreach (XmlNode NodeListItem in List_node)
                            {
                                strXML.Append("<li>");
                                //arrList.Add(NodeListItem.InnerText);
                                strXML.Append(NodeListItem.InnerText);
                                strXML.Append("</li>");
                            }
                        }

                        strXML.Append("</ul>");
                        //strXML.Append("</div>");
                    }

                }
            }

            return strXML.ToString();
        }

        public static ArrayList Hotels2XMLReaderPromotionDetailExtranet(this string XmlSource)
        {
            ArrayList arrList = new ArrayList();

            //XmlDocument Maindoc = new XmlDocument();
            //Maindoc.LoadXml(XmlSource);
            if (!string.IsNullOrEmpty(XmlSource))
            {

                XmlDocument doc = new XmlDocument();
                //HttpContext.Current.Response.Write(PromotionDetailNode.InnerXml);
                //HttpContext.Current.Response.Flush();
                doc.LoadXml(XmlSource);


                XmlNode root = doc.DocumentElement;

                if (root.Name == "PromotionShow")
                {
                    //HttpContext.Current.Response.Write("55");
                    //HttpContext.Current.Response.End();

                    //strXML.Append("<div class=\"XML_div_style1\" style=\"width:100%; margin:10px 0px 0px 0px; padding:0px;\">");
                    if (root.ChildNodes.Count == 2)
                    {
                        XmlNode Head_node = root.ChildNodes[0];
                        //if (Head_node.Name == "head")
                        //{
                        //    //strXML.Append("<h1 class=\"XMlStyle_Head\" style=\"font-size:11px; width:100%; margin:0px; padding:0px;\">");
                        //    //strXML.Append(root.ChildNodes[0].InnerText);
                        //    //strXML.Append("</h1>");
                        //}

                        XmlNode List_node = root.ChildNodes[1];
                        //strXML.Append("<ul style=\"width:100%; margin:0px 0px 0px 0px; padding:0px 0px 0px 5px;\">");
                        if (List_node.Name == "List")
                        {
                            foreach (XmlNode NodeListItem in List_node)
                            {
                                //strXML.Append("<li style=\"width:100%; margin:0px 0px 0px 0px; padding:0px; list-style-position:inside\">");
                                arrList.Add(NodeListItem.InnerText);
                                //strXML.Append(NodeListItem.InnerText);
                                //strXML.Append("</li>");
                            }
                        }

                        //strXML.Append("</ul>");
                        //strXML.Append("</div>");
                    }

                }
            }

            return arrList;
        }

        public static string Hotels2XMlReaderPomotionDetail(this string XmlSource)
        {
            StringBuilder strXML = new StringBuilder();
            if (XmlSource.IndexOf("<?xml version=\"1.0\" encoding=\"utf-8\"?>") == -1)
            {
                strXML.Append(XmlSource);
            }
            else
            {
                XmlDocument Maindoc = new XmlDocument();
                Maindoc.LoadXml(XmlSource);

                
                XmlNode Mainroot = Maindoc.DocumentElement;
                if (Mainroot.Name == "Promotions" && !string.IsNullOrEmpty(Mainroot.SelectSingleNode("Promotion").InnerText))
                {
                    XmlNode ChildNode = Mainroot.SelectSingleNode("Promotion");

                    strXML.Append("<p class=\"xml_promotion_title\" style=\"margin:0px;padding:0px;\">" + ChildNode.SelectSingleNode("TitleLang").InnerText + "</p>");
                    XmlNode PromotionDetailNode = ChildNode.SelectSingleNode("DetailLang");
                    if (PromotionDetailNode.HasChildNodes && PromotionDetailNode.ChildNodes[0].Name == "PromotionShow")
                    {
                        XmlDocument doc = new XmlDocument();
                        //HttpContext.Current.Response.Write(PromotionDetailNode.InnerXml);
                        //HttpContext.Current.Response.Flush();
                        doc.LoadXml(PromotionDetailNode.InnerXml);


                        XmlNode root = doc.DocumentElement;

                        if (root.Name == "PromotionShow")
                        {
                            strXML.Append("<div class=\"XML_div_style1\" style=\"width:100%; margin:10px 0px 0px 0px; padding:0px;\">");
                            if (root.ChildNodes.Count == 2)
                            {
                                XmlNode Head_node = root.ChildNodes[0];
                                if (Head_node.Name == "head")
                                {
                                    strXML.Append("<strong>");
                                    strXML.Append(root.ChildNodes[0].InnerText);
                                    strXML.Append("</strong>");
                                }

                                XmlNode List_node = root.ChildNodes[1];
                                strXML.Append("<ul style=\"width:100%; margin:0px 0px 0px 0px; padding:0px 0px 0px 5px;\">");
                                if (List_node.Name == "List")
                                {
                                    foreach (XmlNode NodeListItem in List_node)
                                    {
                                        strXML.Append("<li style=\"width:100%; margin:0px 0px 0px 0px; padding:0px; list-style-position:inside\">");
                                        strXML.Append(NodeListItem.InnerText);
                                        strXML.Append("</li>");
                                    }
                                }

                                strXML.Append("</ul>");
                                strXML.Append("</div>");
                            }

                        }
                        else
                        {
                            strXML.Append("<div style=\"font-size:11px;margin:0px;padding:0px;\">");
                            strXML.Append(PromotionDetailNode.InnerText);
                            strXML.Append("</div>");

                        }
                    }
                    else
                    {
                        strXML.Append("<div style=\"font-size:11px;margin:0px;padding:0px;\">");
                        strXML.Append(PromotionDetailNode.InnerText);
                        strXML.Append("</div>");

                    }
                }
            }
            

            

            return strXML.ToString();
                                    
        }
        // Diaplay Policy in Voucher 
        public static string Hotels2XMlReader_PolicyAndCancellation(IList<object> dBookingIteMList, int intBookingProductId, bool IsExtranet, byte bytBookingLang)
        {

            StringBuilder strXML = new StringBuilder();
            string topic = string.Empty;
            string topicCanCel = string.Empty;
            BookingProductDisplay cBookingProduct = new BookingProductDisplay();
            cBookingProduct = cBookingProduct.getBookingProductDisplayByBookingProductId(intBookingProductId);
           

            switch (bytBookingLang)
            {
                case 1:
                    topic = "Policy";
                    topicCanCel = "Cancellation Policy";
                    break;
                case 2:
                    topic = "เงื่อนไขการจอง";
                    topicCanCel = "เงื่อนไขการยกเลิก";
                    break;
            }
            strXML.Append("<div class=\"policy\">");
            strXML.Append("<b class=\"head\">" + topic + ": </b><div class=\"clear-all\"></div>");

            foreach (BookingItemDisplay XmlSource in dBookingIteMList)
            {
                if (XmlSource.ConditionDetail.IndexOf("<?xml version=\"1.0\" encoding=\"utf-8\"?>") == -1)
                {
                    strXML.Append(XmlSource.ConditionDetail);
                }
                else
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(XmlSource.ConditionDetail.Hotels2SPcharacter_removeONe());
                    XmlNode root = doc.DocumentElement;
                    strXML.Append("<b>" + XmlSource.OptionTitle + "</b>");

                    strXML.Append("<ul class=\"subPolicy\">");

                    if (root.Name == "Policies")
                    {
                        foreach (XmlNode PolicyNode in root)
                        {
                            if (PolicyNode.Name == "Policy")
                            {
                                if (PolicyNode.SelectSingleNode("TypeID").InnerText != "1")
                                {
                                    strXML.Append("<li>");

                                    XmlDocument docdetail = new XmlDocument();
                                    string strDetail = string.Empty;
                                    int find = PolicyNode.SelectSingleNode("PolicyDetailDisplay").InnerXml.IndexOf("</contents>");

                                    if (find > -1)
                                        strDetail = PolicyNode.SelectSingleNode("PolicyDetailDisplay").InnerXml.Hotels2XMlReader();

                                    else
                                        strDetail = PolicyNode.SelectSingleNode("PolicyDetailDisplay").InnerText;

                                    strXML.Append("<span class=\"check\">" + PolicyNode.SelectSingleNode("PolicyTitleDisplay").InnerText + ": </span>" + strDetail);
                                    strXML.Append("</li>");
                                }
                                else
                                {
                                    XmlNode CancellationNode = PolicyNode.SelectSingleNode("Cancellations");
                                    strXML.Append("<li>");
                                    strXML.Append("<span class=\"check\">" + topicCanCel + "</span>");
                                    strXML.Append("<ul class=\"cancellation\">");


                                    string DayCancel = "0";
                                    string bhtNCharge = "0";
                                    string bhtPerCharge = "0";
                                    string hotelNCharge = "0";
                                    string hotelPerCharge = "0";

                                    int nodeCancelCount = CancellationNode.ChildNodes.Count;
                                    int rowCount = 1;
                                    int DayCancelTemp = 0;
                                    foreach (XmlNode XmlCancellItemList in CancellationNode.ChildNodes)
                                    {
                                        if (XmlCancellItemList.Name == "Cancellation")
                                        {
                                            if (!string.IsNullOrEmpty(XmlCancellItemList.SelectSingleNode("DayCancel").InnerText))
                                                DayCancel = XmlCancellItemList.SelectSingleNode("DayCancel").InnerText;

                                            if (!string.IsNullOrEmpty(XmlCancellItemList.SelectSingleNode("BhtChargeRoom").InnerText))
                                                bhtNCharge = XmlCancellItemList.SelectSingleNode("BhtChargeRoom").InnerText;

                                            if (!string.IsNullOrEmpty(XmlCancellItemList.SelectSingleNode("BhtChargePercent").InnerText))
                                                bhtPerCharge = XmlCancellItemList.SelectSingleNode("BhtChargePercent").InnerText;

                                            if (!string.IsNullOrEmpty(XmlCancellItemList.SelectSingleNode("HotelChargeRoom").InnerText))
                                                hotelNCharge = XmlCancellItemList.SelectSingleNode("HotelChargeRoom").InnerText;

                                            if (!string.IsNullOrEmpty(XmlCancellItemList.SelectSingleNode("HotelChargePercent").InnerText))
                                                hotelPerCharge = XmlCancellItemList.SelectSingleNode("HotelChargePercent").InnerText;

                                            strXML.Append("<li>");


                                            strXML.Append(Hotels2String.CancellationGenerateWording(IsExtranet, byte.Parse(DayCancel), byte.Parse(bhtPerCharge), byte.Parse(bhtNCharge), byte.Parse(hotelPerCharge), byte.Parse(hotelNCharge), cBookingProduct.ProductCategory, bytBookingLang));


                                            strXML.Append("</li>");
                                        }

                                        if (intBookingProductId <= 132159)
                                        {
                                            if (rowCount == 1)
                                                DayCancelTemp = int.Parse(DayCancel);
                                        }

                                        if (intBookingProductId > 132159)
                                        {
                                            if (rowCount == nodeCancelCount)
                                                DayCancelTemp = int.Parse(DayCancel);
                                        }

                                        rowCount = rowCount + 1;


                                    }



                                    strXML.Append("<li>");
                                    switch (bytBookingLang)
                                    {
                                        case 1:
                                            strXML.Append("More than " + DayCancelTemp + " days prior to arrival, there is a 7% charged of administration fee.");
                                            break;
                                        case 2:

                                            if (cBookingProduct.ProductCategory == 29)
                                                strXML.Append("การยกเลิกการจองมากกว่า " + DayCancelTemp + " วัน ก่อนวันเช็คอินทางเวปไซต์โฮเทลทูจะเรียกเก็บค่าธรรมเนียม 7% จากยอดการจองทั้งหมด");
                                            else
                                                strXML.Append("การยกเลิกการจองมากกว่า " + DayCancelTemp + " วัน ก่อนวันเช็คอินหรือใช้บริการทัวร์ ทางเวปไซต์โฮเทลทูจะเรียกเก็บค่าธรรมเนียม 7% จากยอดการจองทั้งหมด");

                                            break;
                                    }

                                    strXML.Append("</li>");

                                    strXML.Append("</ul>");

                                }

                            }
                        }
                    }


                    strXML.Append("</ul>");
                }
                    
                
                
            }

            strXML.Append("</div>");
            return strXML.ToString();
        }


        

        private static string TxtCancellation()
        {
            StringBuilder txt = new StringBuilder();
            txt.Append("No charge is made to your credit card until your booking is final, that is, when you submitted the payment either online or by fax and we e-mailed you the hotel voucher. In order to secure your room reservation you must complete and submit your credit card details via our Secure Online Credit Card Form or alternatively you may want to print it out and fax it to us duly completed and signed (Our fax no.: +66 (0)2 930 6514). ");
            return txt.ToString();
        }

     

        private static string TxtDaycancelGen(string strDayCancel, string strPreviousCancel , int AllList, int Index )
        {
            StringBuilder Result = new StringBuilder();
            int AllIndex = AllList - 1;

            int DayCancel = int.Parse(strDayCancel);
            int PreviousCancel = int.Parse(strPreviousCancel);
            //HttpContext.Current.Response.Write(PreviousCancel);
            //HttpContext.Current.Response.End();
           

            if (DayCancel == 0)
            {
                Result.Append("No Show Days prior to arrival ");
            }
            else
            {
                if (Index == 0)
                {
                    Result.Append("+" + strDayCancel + " Days prior to arrival ");
                    //Result.Append(strDayCancel + " - 1 Days prior to arrival");
                }
                else
                {
                    Result.Append(DayCancel + " - " + (PreviousCancel - DayCancel) + " Days prior to arrival ");
                }
            }
            return Result.ToString();
        }


        //public static string Hotels2TableStyleGen(int Style)
        //{

        //}
       

    }
}