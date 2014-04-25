using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Web;
using Hotels2thailand;
using Hotels2thailand.Staffs;

/// <summary>
/// Summary description for ProductContent
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public class ProductContent : Hotels2BaseClass
    {
        
        public int ProductID { get; set; }
        public byte LanguageID { get; set; }
        public string Title { get; set; }
        public string TitleSecound { get; set; }
        public string Address { get; set; }
        public string DetailShort { get; set; }
        public string Detail { get; set; }
        public string DetailInterNet { get; set; }
        public string Direction { get; set; }
        public string _file_main;
        public string _file_information;
        public string _file_review;
        public string _file_photo;
        public string _file_why;
        public string _file_pdf;
        public bool _status;
        public string _file_contact;

        public string FileMain 
        {
            get { return _file_main; }
            set { _file_main = value; } 
        }
        public string FileInformation
        {
            get { return _file_information; }
            set { _file_information = value; }
        }
        public string FileReview
        {
            get { return _file_review; }
            set { _file_review = value; }
        }
        public string FilePhoto
        {
            get { return _file_photo; }
            set { _file_photo = value; }
        }
        public string FileWhy
        {
            get { return _file_why; }
            set { _file_why = value; }
        }
        public string FilePDF
        {
            get { return _file_pdf; }
            set { _file_pdf = value; }
        }
        public string FileContact
        {
            get { return _file_contact; }
            set { _file_contact = value; }
        }
        public bool Status
        {
            get { return _status; }
            set { _status = value; }
        }

        
        public ProductContent()
        {
            _status = true;
            
        }

        public int Insert(ProductContent content)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("INSERT INTO tbl_product_content (product_id,lang_id,title,title_second,address,detail_short,detail,detail_internet");
                query.Append(" ,direction,file_name_main,file_name_information,file_name_review,file_name_photo");
                query.Append(" ,file_name_why,file_name_pdf,file_name_contact,status)");

                query.Append(" VALUES(@product_id,@lang_id,@title,@title_second,@address,@detail_short,@detail,@detail_internet");
                query.Append(" ,@direction,@file_name_main,@file_name_information,@file_name_review,@file_name_photo");
                query.Append(" ,@file_name_why,@file_name_pdf,@file_name_contact,@status)");
                

                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = content.ProductID;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = content.LanguageID;
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = content.Title;
                cmd.Parameters.Add("@title_second", SqlDbType.NVarChar).Value = content.TitleSecound;
                cmd.Parameters.Add("@address", SqlDbType.NVarChar).Value = content.Address;
                cmd.Parameters.Add("@detail_short", SqlDbType.NVarChar).Value = content.DetailShort;
                cmd.Parameters.Add("@detail", SqlDbType.NVarChar).Value = content.Detail;
                cmd.Parameters.Add("@detail_internet", SqlDbType.NVarChar).Value = content.DetailInterNet;
                cmd.Parameters.Add("@direction", SqlDbType.NVarChar).Value = content.Direction;

                cmd.Parameters.Add("@file_name_main", SqlDbType.VarChar).Value = FilenameManage(content.FileMain, content.LanguageID);
                cmd.Parameters.Add("@file_name_information", SqlDbType.VarChar).Value = FilenameManage(content.FileMain, "information", content.LanguageID);
                cmd.Parameters.Add("@file_name_review", SqlDbType.VarChar).Value = FilenameManage(content.FileMain, "review", content.LanguageID);
                cmd.Parameters.Add("@file_name_photo", SqlDbType.VarChar).Value = FilenameManage(content.FileMain, "photo", content.LanguageID);
                cmd.Parameters.Add("@file_name_why", SqlDbType.VarChar).Value = FilenameManage(content.FileMain, "why", content.LanguageID);
                cmd.Parameters.Add("@file_name_pdf", SqlDbType.VarChar).Value = FilenameManagePDF(content.FileMain,content.LanguageID);
                cmd.Parameters.Add("@file_name_contact", SqlDbType.VarChar).Value = FilenameManage(content.FileMain, "contact", content.LanguageID);
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = content.Status;
                
                cn.Open();
               
                int ret = ExecuteNonQuery(cmd);
                //=== STAFF ACTIVITY ================================================================================================================================================================================================
                StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_detail, StaffLogActionType.Insert, StaffLogSection.Product,
                    content.ProductID, "tbl_product_content", "product_id,lang_id,title,title_second,address,detail_short,detail,detail_internet,direction,file_name_main,file_name_information,file_name_review,file_name_photo,file_name_why,file_name_pdf,file_name_contact,status",
                    "product_id,lang_id", content.ProductID, content.LanguageID);
                //===================================================================================================================================================================================================

                return ret;
                
            }
            
        }
        public bool UpdateProductContent(ProductContent content)
        {
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_content", "title,title_second,address,detail_short,detail,detail_internet,direction", "product_id,lang_id", content.ProductID, content.LanguageID);
            //============================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("UPDATE tbl_product_content SET title=@title, title_second=@title_second, address=@address, detail_short=@detail_short, detail=@detail, detail_internet=@detail_internet");

                 //file_name_main=@file_name_main, file_name_information=@file_name_information ,file_name_review=@file_name_review,file_name_photo=@file_name_photo, file_name_why=@file_name_why, file_name_pdf=@file_name_pdf, file_name_contact=@file_name_contact
                query.Append(" , direction=@direction");
                query.Append(" WHERE product_id=@product_id AND  lang_id=@lang_id");

                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = content.ProductID;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = content.LanguageID;
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = content.Title;
                cmd.Parameters.Add("@title_second", SqlDbType.NVarChar).Value = content.TitleSecound;
                cmd.Parameters.Add("@address", SqlDbType.NVarChar).Value = content.Address;
                cmd.Parameters.Add("@detail_short", SqlDbType.NVarChar).Value = content.DetailShort;
                cmd.Parameters.Add("@detail", SqlDbType.NVarChar).Value = content.Detail;
                cmd.Parameters.Add("@detail_internet", SqlDbType.NVarChar).Value = content.DetailInterNet;
                cmd.Parameters.Add("@direction", SqlDbType.NVarChar).Value = content.Direction;
                //cmd.Parameters.Add("@file_name_main", SqlDbType.VarChar).Value = FilenameManage(content.FileMain, content.LanguageID);
                //cmd.Parameters.Add("@file_name_information", SqlDbType.VarChar).Value = FilenameManage(content.FileMain, "information", content.LanguageID);
                //cmd.Parameters.Add("@file_name_review", SqlDbType.VarChar).Value = FilenameManage(content.FileMain, "review", content.LanguageID);
                //cmd.Parameters.Add("@file_name_photo", SqlDbType.VarChar).Value = FilenameManage(content.FileMain, "photo", content.LanguageID);
                //cmd.Parameters.Add("@file_name_why", SqlDbType.VarChar).Value = FilenameManage(content.FileMain, "why", content.LanguageID);
                //cmd.Parameters.Add("@file_name_pdf", SqlDbType.VarChar).Value = FilenameManagePDF(content.FileMain,content.LanguageID);
                //cmd.Parameters.Add("@file_name_contact", SqlDbType.VarChar).Value = FilenameManage(content.FileMain, "contact", content.LanguageID);
                cn.Open();
                ret = ExecuteNonQuery(cmd);
            }
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_detail, StaffLogActionType.Update, StaffLogSection.Product,
                content.ProductID, "tbl_product_content", "title,title_second,address,detail_short,detail,detail_internet,direction", arroldValue, "product_id,lang_id", content.ProductID, content.LanguageID);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);

         }

        
        public static bool UpdatePContent(int intPid, byte bytLangId, string strTitle,string strTitleSec, string strAddress,  string strDetailshort,
            string strDetail, string strDetailInterNet, string strDirection)
        {
            ProductContent cUpdate = new ProductContent
            {
                 ProductID = intPid,
                 LanguageID = bytLangId,
                 Title = strTitle,
                 TitleSecound = strTitleSec,
                 Address = strAddress,
                 DetailShort = strDetailshort,
                 Detail = strDetail,
                 DetailInterNet =  strDetailInterNet,
                 Direction = strDirection
                 //FileMain = strFileMain
            };
            return cUpdate.UpdateProductContent(cUpdate);
        }

        public List<object> GetProductContentAll()
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append(" SELECT product_id,lang_id,title,title_second,address,detail_short,detail,detail_internet");
                query.Append(" ,direction,file_name_main,file_name_information,file_name_review,file_name_photo");
                query.Append(" ,file_name_why,file_name_pdf,file_name_contact,status ");
                query.Append(" FROM tbl_product_content");

                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));

            }
            
        }

        public Dictionary<int, string> getProductsameLocation(int intProductId, byte bytLangId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT DISTINCT(pc.product_id) , pc.title");
                query.Append(" FROM tbl_product_location pl, tbl_product_content pc");
                query.Append(" WHERE pl.product_id = pc.product_id AND pc.lang_id = @lang_id AND pc.product_id <> @product_id AND");
                query.Append(" pl.location_id IN (SELECT location_id FROM tbl_product_location WHERE product_id = @product_id)");


                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytLangId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                Dictionary<int, string> iDic = new Dictionary<int, string>();
                while (reader.Read())
                {
                    iDic.Add((int)reader[0],reader[1].ToString());
                }
                return iDic;

            }
        }

        public List<object> GetProductContentByProductId(int intProductId)
        {

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append(" SELECT product_id,lang_id,title,title_second,address,detail_short,detail,detail_internet");
                query.Append(" ,direction,file_name_main,file_name_information,file_name_review,file_name_photo");
                query.Append(" ,file_name_why,file_name_pdf,file_name_contact,status");
                query.Append(" FROM tbl_product_content WHERE product_id=@product_id");

                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cn.Open();
                return  MappingObjectCollectionFromDataReader( ExecuteReader(cmd));

            }

        }

        public ArrayList GetProductContentReviewFileName(int intProductId, byte bytLangId)
        {

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append(" SELECT title, file_name_review");
                query.Append(" FROM tbl_product_content WHERE product_id=@product_id AND lang_id =@lang_id");

                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytLangId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                ArrayList arrItem = new ArrayList();
                if (reader.Read())
                {
                    arrItem.Add(reader[0]);
                    arrItem.Add(reader[1]);
                }

                return arrItem;
                
            }

        }
        public ArrayList GetProductContentFileName(int intProductId, byte bytLangId)
        {

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append(" SELECT title, file_name_main");
                query.Append(" FROM tbl_product_content WHERE product_id=@product_id AND lang_id =@lang_id");

                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytLangId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                ArrayList arrItem = new ArrayList();
                if (reader.Read())
                {
                    arrItem.Add(reader[0]);
                    arrItem.Add(reader[1]);
                }

                return arrItem;

            }

        }
        public ProductContent GetProductContentById(int intProductId, byte bytLanguageID)
        {
            
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append(" SELECT product_id,lang_id,title,title_second,address,detail_short,detail,detail_internet");
                query.Append(" ,direction,file_name_main,file_name_information,file_name_review,file_name_photo");
                query.Append(" ,file_name_why,file_name_pdf,file_name_contact,status ");
                query.Append(" FROM tbl_product_content WHERE product_id=@product_id AND lang_id=@lang_id");

                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytLanguageID;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    
                    return (ProductContent)MappingObjectFromDataReader(reader);
                }
                else
                {
                    return null;
                }
                
            }
            
        }

        public string FilenameManage(string strToReplace, string strReplaceKey, byte bytLangId)
        {
            string result = string.Empty;
            int _index = strToReplace.IndexOf(".asp");
            result = strToReplace;

            if (_index > 0)
            {
                switch (bytLangId)
                {
                    case 1:
                        result = strToReplace.Replace(".asp", "").Trim() + "_" + strReplaceKey + ".asp";
                        break;
                    case 2:
                        result = strToReplace.Replace(".asp", "").Trim() + "_" + strReplaceKey + "-th.asp";
                        break;
                }

            }

            return result;
        }

        public string FilenameManagePDF(string strToReplace, byte bytLangId)
        {
            string result = string.Empty;

            int _index = strToReplace.IndexOf(".asp");
            result = strToReplace;

            if (_index > 0)
            {
                switch (bytLangId)
                {
                    case 1:
                        result = strToReplace.Replace(".asp", "").Trim() + ".pdf";
                        break;
                    case 2:
                        result = strToReplace.Replace(".asp", "").Trim() + "-th.pdf";
                        break;
                }

            }

            return result;
        }


        public string FilenameManage(string strToReplace, byte bytLangId)
        {
            string result = string.Empty;

            int _index = strToReplace.IndexOf(".asp");

            if (_index > 0)
            {
                switch (bytLangId)
                {
                    case 1:
                        result = strToReplace;
                        break;
                    case 2:
                        result = strToReplace.Replace(".asp", "").Trim() + "-th.asp";
                        break;
                }
            }


            return result;
        }
    }
}