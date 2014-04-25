using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Hotels2thailand.Staffs;

/// <summary>
/// Summary description for ReviewWebsite
/// </summary>
/// 
namespace Hotels2thailand.Reviews
{
    public class ReviewWebsite : Hotels2BaseClass
    {
        
        public int reviewId { get; set; }
        public int CustomerId { get; set; }
        public byte site_interaction_speed { get; set; }
        public string site_interaction_speed_suggest { get; set; }
        public byte site_enquiry_response { get; set; }
        public string site_enquiry_response_suggest { get; set; }
        public byte site_problem_solution { get; set; }
        public string site_problem_solution_suggest { get; set; }
        public byte site_hotel_information { get; set; }
        public string site_hotel_information_suggest { get; set; }
        public byte find_us_id { get; set; }
        public string find_us_other { get; set; }
        public string enhance_other { get; set; }
        public string problem_hotel_found_suggest { get; set; }
        public string problem_other_suggest { get; set; }
        public string product_destination_suggest { get; set; }
        public string product_other_suggest { get; set; }
        public string visit_other_suggest { get; set; }
        public short visit_num_day { get; set; }
        public byte often_visit_id { get; set; }
        public bool book_before { get; set; }
        public bool rebook { get; set; }
        public string rebook_reason { get; set; }
        public string comment { get; set; }
        public DateTime date_submit { get; set; }
        public bool status { get; set; }

        public byte Current_StaffLangId
        {
            get
            {
                StaffSessionAuthorize cStaffAuthorize = new StaffSessionAuthorize();
                return cStaffAuthorize.CurrentLangId;
            }
        }

        public string OftenTitle
        {
            get { return this.GetOftentitle(this.often_visit_id); }
        }
        public string FindUstitle
        {
            get { return this.GetFindUstitle(this.find_us_id); }
        }

        public IDictionary<byte, string> ReviewVisitList
        {
            get { return this.GetVisitList(this.reviewId); }
        }
        public IDictionary<byte, string> ReviewEnhance
        {
            get { return this.GetEnhach(this.reviewId); }
        }
        public IDictionary<byte, string> ReviewEnhanceProduct
        {
            get { return this.GetEnhachProduct(this.reviewId); }
        }
        public IDictionary<byte, string> ReviewProblem
        {
            get { return this.GetProblem(this.reviewId); }
        }

        public ReviewWebsite()
        {
            
        }

        public string GetOftentitle(byte often_visit_id)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT srfn.title FROM tbl_site_review_often srf, tbl_site_review_often_name srfn WHERE srf.often_visit_id = srfn.often_visit_id AND srfn.lang_di=@lang_id AND srf.often_visit_id= @often_visit_id", cn);
                cmd.Parameters.Add("@often_visit_id", SqlDbType.TinyInt).Value = often_visit_id;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = this.Current_StaffLangId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return reader[0].ToString();
                else
                    return string.Empty;     
            }
        }

        public string GetFindUstitle(byte find_us_id)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT srfn.title FROM tbl_site_review_find_us srf, tbl_site_review_find_us_name srfn WHERE srf.find_us_id = srfn.find_us_id AND srfn.lang_di=@lang_id AND srf.find_us_id= @find_us_id", cn);
                cmd.Parameters.Add("@find_us_id", SqlDbType.TinyInt).Value = find_us_id;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = this.Current_StaffLangId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return reader[0].ToString();
                else
                    return string.Empty;    
            }
        }

        public IDictionary<byte, string> GetVisitList(int intReviewId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                IDictionary<byte, string> Result = new Dictionary<byte, string>();
                StringBuilder query = new StringBuilder();
                query.Append("SELECT vr.visit_id , vrn.title");
                query.Append(" FROM tbl_visit_review_site vm, tbl_site_review_visit vr, tbl_site_review_visit_name vrn");
                query.Append(" WHERE vm.visit_id = vr.visit_id AND vr.visit_id = vrn.visit_id AND vrn.lang_id = @lang_id AND vm.review_id = @review_id");
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@review_id", SqlDbType.Int).Value = intReviewId;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = this.Current_StaffLangId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                while (reader.Read())
                {
                    Result.Add((byte)reader[0], reader[0].ToString());
                }
                return Result;
            }
        }

        public IDictionary<byte, string> GetEnhach(int intReviewId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                IDictionary<byte, string> Result = new Dictionary<byte, string>();
                StringBuilder query = new StringBuilder();
                query.Append("SELECT vr.enhance_id , vrn.title");
                query.Append(" FROM tbl_enhance_site_review vm, tbl_site_review_enhance vr, tbl_site_review_enhance_name vrn");
                query.Append(" WHERE vm.enhance_id = vr.enhance_id AND vr.enhance_id = vrn.enhance_id AND vrn.lang_id =  @lang_id AND vm.review_id = @review_id");
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@review_id", SqlDbType.Int).Value = intReviewId;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = this.Current_StaffLangId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                while (reader.Read())
                {
                    Result.Add((byte)reader[0], reader[0].ToString());
                }
                return Result;
            }
        }

        public IDictionary<byte, string> GetEnhachProduct(int intReviewId)
        {

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                IDictionary<byte, string> Result = new Dictionary<byte, string>();
                StringBuilder query = new StringBuilder();
                query.Append("SELECT vr.product_enh_id , vrn.title");
                query.Append(" FROM tbl_enhance_product_site_review vm, tbl_site_review_enhance_product vr, tbl_site_review_enhance_product_name vrn");
                query.Append(" WHERE vm.product_enh_id = vr.product_enh_id AND vr.product_enh_id = vrn.product_enh_id AND vrn.lang_id =  @lang_id AND vm.review_id = @review_id");
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@review_id", SqlDbType.Int).Value = intReviewId;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = this.Current_StaffLangId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                while (reader.Read())
                {
                    Result.Add((byte)reader[0], reader[0].ToString());
                }
                return Result;
            }
            
        }

        public IDictionary<byte, string> GetProblem(int intReviewId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                IDictionary<byte, string> Result = new Dictionary<byte, string>();
                StringBuilder query = new StringBuilder();
                query.Append("SELECT vr.problem_id , vrn.title");
                query.Append(" FROM tbl_problem_review_site vm, tbl_site_review_problem vr, tbl_site_review_problem_name vrn");
                query.Append(" WHERE vm.problem_id = vr.problem_id AND vr.problem_id = vrn.problem_id AND vrn.lang_id = @lang_id AND vm.review_id = @review_id");
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@review_id", SqlDbType.Int).Value = intReviewId;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = this.Current_StaffLangId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                while (reader.Read())
                {
                    Result.Add((byte)reader[0], reader[0].ToString());
                }
                return Result;
            }
        }

        public List<object[]> getReviewListALLCustomUnread()
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT rs.review_id, cs.full_name, rs.date_submit, rs.rebook FROM tbl_site_review rs, tbl_customer cs WHERE cs.cus_id = rs.cus_id AND rs.status = 1 ORDER BY rs.date_submit DESC,rs.status , cs.full_name ", cn);
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                List<object[]> objList = new List<object[]>();
                while (reader.Read())
                {
                    object[] item = { reader[0], reader[1], reader[2], reader[3] };
                    objList.Add(item);
                }
                return objList;

            }
        }

        public List<object[]> getReviewListALLCustomByDateRang(DateTime dDateStart, DateTime dDateEnd)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT rs.review_id, cs.full_name, rs.date_submit, rs.rebook  FROM tbl_site_review rs, tbl_customer cs WHERE cs.cus_id = rs.cus_id AND rs.date_submit BETWEEN @date_start AND @date_end  ORDER BY rs.status , cs.full_name", cn);
                cmd.Parameters.Add("@date_start",SqlDbType.SmallDateTime).Value = dDateStart;
                cmd.Parameters.Add("@date_end", SqlDbType.SmallDateTime).Value = dDateEnd;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                List<object[]> objList = new List<object[]>();
                    while (reader.Read())
                    {
                        object[] item = { reader[0], reader[1], reader[2], reader[3] };
                        objList.Add(item);
                    }
                return objList;
            }
        }

        public ReviewWebsite getReviewListById(int intReview_id)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_site_review WHERE review_id = @review_id", cn);
                cmd.Parameters.Add("@review_id", SqlDbType.Int).Value = intReview_id;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (ReviewWebsite)MappingObjectFromDataReader(reader);
                else
                    return null;

            }
        }


        public int InsertNewReviewSite(int cus_id, byte site_interaction_speed, string site_interaction_speed_suggest, byte site_enquiry_response, string site_enquiry_response_suggest,
            byte site_problem_solution, string site_problem_solution_suggest, byte site_hotel_information, string site_hotel_information_suggest, byte find_us_id, string find_us_other,
            string enhance_other, string problem_hotel_found_suggest, string problem_other_suggest, string product_destination_suggest, string product_other_suggest, string visit_other_suggest
            , short visit_num_day, byte often_visit_id, bool? book_before, bool? rebook, string rebook_reason, string comment)
        {
            using(SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("INSERT INTO tbl_site_review (cus_id, site_interaction_speed, site_interaction_speed_suggest, site_enquiry_response,");
                query.Append(" site_enquiry_response_suggest, site_problem_solution, site_problem_solution_suggest, site_hotel_information, site_hotel_information_suggest,");
                query.Append(" find_us_id, find_us_other,enhance_other, problem_hotel_found_suggest, problem_other_suggest,");
                query.Append(" product_destination_suggest, product_other_suggest,visit_other_suggest,visit_num_day,");
                query.Append(" often_visit_id,book_before,rebook,rebook_reason,comment,date_submit)");

                query.Append(" VALUES(@cus_id,@site_interaction_speed,@site_interaction_speed_suggest,@site_enquiry_response,");
                query.Append(" @site_enquiry_response_suggest,@site_problem_solution,@site_problem_solution_suggest,@site_hotel_information,@site_hotel_information_suggest,");
                query.Append(" @find_us_id,@find_us_other,@enhance_other,@problem_hotel_found_suggest,@problem_other_suggest,");
                query.Append(" @product_destination_suggest,@product_other_suggest,@visit_other_suggest,@visit_num_day,");
                query.Append(" @often_visit_id,@book_before,@rebook,@rebook_reason,@comment,@date_submit); SET @review_id=SCOPE_IDENTITY()");

                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@cus_id", SqlDbType.Int).Value = cus_id;


                if (site_interaction_speed != 0)
                    cmd.Parameters.Add("@site_interaction_speed", SqlDbType.TinyInt).Value = site_interaction_speed;
                else
                    cmd.Parameters.AddWithValue("@site_interaction_speed", DBNull.Value);

                if (!string.IsNullOrEmpty(site_interaction_speed_suggest))
                    cmd.Parameters.Add("@site_interaction_speed_suggest", SqlDbType.Text).Value = site_interaction_speed_suggest;
                else
                    cmd.Parameters.AddWithValue("@site_interaction_speed_suggest", DBNull.Value);


                if (site_enquiry_response != 0)
                    cmd.Parameters.Add("@site_enquiry_response", SqlDbType.TinyInt).Value = site_enquiry_response;
                else
                    cmd.Parameters.AddWithValue("@site_enquiry_response", DBNull.Value);

                if (!string.IsNullOrEmpty(site_enquiry_response_suggest))
                    cmd.Parameters.Add("@site_enquiry_response_suggest", SqlDbType.Text).Value = site_enquiry_response_suggest;
                else
                    cmd.Parameters.AddWithValue("@site_enquiry_response_suggest", DBNull.Value);

                if (site_problem_solution != 0)
                    cmd.Parameters.Add("@site_problem_solution", SqlDbType.TinyInt).Value = site_problem_solution;
                else
                    cmd.Parameters.AddWithValue("@site_problem_solution", DBNull.Value);

                if (!string.IsNullOrEmpty(site_problem_solution_suggest))
                    cmd.Parameters.Add("@site_problem_solution_suggest", SqlDbType.Text).Value = site_problem_solution_suggest;
                else
                    cmd.Parameters.AddWithValue("@site_problem_solution_suggest", DBNull.Value);

                if (site_hotel_information != 0)
                    cmd.Parameters.Add("@site_hotel_information", SqlDbType.TinyInt).Value = site_hotel_information;
                else
                    cmd.Parameters.AddWithValue("@site_hotel_information", DBNull.Value);

                if (!string.IsNullOrEmpty(site_hotel_information_suggest))
                    cmd.Parameters.Add("@site_hotel_information_suggest", SqlDbType.Text).Value = site_hotel_information_suggest;
                else
                    cmd.Parameters.AddWithValue("@site_hotel_information_suggest", DBNull.Value);


                if (find_us_id != 0)
                    cmd.Parameters.Add("@find_us_id", SqlDbType.TinyInt).Value = find_us_id;
                else
                    cmd.Parameters.AddWithValue("@find_us_id", DBNull.Value);

                if (!string.IsNullOrEmpty(find_us_other))
                    cmd.Parameters.Add("@find_us_other", SqlDbType.NVarChar).Value = find_us_other;
                else
                    cmd.Parameters.AddWithValue("@find_us_other", DBNull.Value);

                if (!string.IsNullOrEmpty(enhance_other))
                    cmd.Parameters.Add("@enhance_other", SqlDbType.NVarChar).Value = enhance_other;
                else
                    cmd.Parameters.AddWithValue("@enhance_other", DBNull.Value);

                if (!string.IsNullOrEmpty(problem_hotel_found_suggest))
                    cmd.Parameters.Add("@problem_hotel_found_suggest", SqlDbType.Text).Value = problem_hotel_found_suggest;
                else
                    cmd.Parameters.AddWithValue("@problem_hotel_found_suggest", DBNull.Value);

                if (!string.IsNullOrEmpty(problem_other_suggest))
                    cmd.Parameters.Add("@problem_other_suggest", SqlDbType.Text).Value = problem_other_suggest;
                else
                    cmd.Parameters.AddWithValue("@problem_other_suggest", DBNull.Value);

                if (!string.IsNullOrEmpty(product_destination_suggest))
                    cmd.Parameters.Add("@product_destination_suggest", SqlDbType.NVarChar).Value = product_destination_suggest;
                else
                    cmd.Parameters.AddWithValue("@product_destination_suggest", DBNull.Value);


                if (!string.IsNullOrEmpty(product_other_suggest))
                    cmd.Parameters.Add("@product_other_suggest", SqlDbType.NVarChar).Value = product_other_suggest;
                else
                    cmd.Parameters.AddWithValue("@product_other_suggest", DBNull.Value);

                if (!string.IsNullOrEmpty(visit_other_suggest))
                    cmd.Parameters.Add("@visit_other_suggest", SqlDbType.NVarChar).Value = visit_other_suggest;
                else
                    cmd.Parameters.AddWithValue("@visit_other_suggest", DBNull.Value);


                if (visit_num_day != 0)
                    cmd.Parameters.Add("@visit_num_day", SqlDbType.SmallInt).Value = visit_num_day;
                else
                    cmd.Parameters.AddWithValue("@visit_num_day", DBNull.Value);
                
                if (often_visit_id != 0)
                    cmd.Parameters.Add("@often_visit_id", SqlDbType.TinyInt).Value = often_visit_id;
                else
                    cmd.Parameters.AddWithValue("@often_visit_id", DBNull.Value);

                if (book_before != null)
                    cmd.Parameters.Add("@book_before", SqlDbType.Bit).Value = book_before;
                else
                    cmd.Parameters.AddWithValue("@book_before", DBNull.Value);

                if (rebook != null)
                    cmd.Parameters.Add("@rebook", SqlDbType.Bit).Value = rebook;
                else
                    cmd.Parameters.AddWithValue("@rebook", DBNull.Value);

                if (!string.IsNullOrEmpty(rebook_reason))
                    cmd.Parameters.Add("@rebook_reason", SqlDbType.Text).Value = rebook_reason;
                else
                    cmd.Parameters.AddWithValue("@rebook_reason", DBNull.Value);

                if (!string.IsNullOrEmpty(comment))
                    cmd.Parameters.Add("@comment", SqlDbType.Text).Value = comment;
                else
                    cmd.Parameters.AddWithValue("@comment", DBNull.Value);
                


                cmd.Parameters.Add("@date_submit", SqlDbType.SmallDateTime).Value = DateTime.Now.Hotels2ThaiDateTime();
                
                cmd.Parameters.Add("@review_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                ExecuteNonQuery(cmd);
                int ret = (int)cmd.Parameters["@review_id"].Value;
                return ret;
            }

        }

        public bool CloseReviewSite(int intReviewId)
        {
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_site_review", "status", "review_id", intReviewId);
            //============================================================================================================================
            int ret = 0;
            //tbl_site_review
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_site_review SET status=@status WHERE review_id=@review_id", cn);
                cmd.Parameters.Add("@review_id", SqlDbType.Int).Value = intReviewId;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = true;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
               
            }

            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Review, StaffLogActionType.Update, StaffLogSection.F_Review, intReviewId, "tbl_site_review", "status", arroldValue, "review_id", intReviewId);
            //==================================================================================================================== COMPLETED ========

            return (ret == 1);
        }
        //=========================== 
        public int InsertReviewVisit(int intREviewid, byte bytVisitId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_visit_review_site (review_id,visit_id)VALUES (@review_id,@visit_id)",cn);
                cmd.Parameters.Add("@review_id", SqlDbType.Int).Value = intREviewid;
                cmd.Parameters.Add("@visit_id", SqlDbType.TinyInt).Value = bytVisitId;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return ret;
            }
        }

        public IDictionary<byte,string> SelectReviewVisit(byte LangId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT rs.visit_id, rsn.title FROM tbl_site_review_visit rs, tbl_site_review_visit_name rsn WHERE rs.visit_id = rsn.visit_id AND rsn.lang_id = @langId AND rs.visit_id <> 1", cn);
                cmd.Parameters.Add("@langId", SqlDbType.TinyInt).Value = LangId;
                cn.Open();
                IDictionary<byte, string> Idic = new Dictionary<byte, string>();
                
                IDataReader reader = ExecuteReader(cmd);
                while (reader.Read())
                {
                    Idic.Add((byte)reader[0], reader[1].ToString());
                }
                return Idic;
            }
        }

        
        public IDictionary<byte, string> SelectReviewOften(byte LangId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT rs.often_visit_id, rsn.title FROM tbl_site_review_often rs, tbl_site_review_often_name rsn WHERE rs.often_visit_id = rsn.often_visit_id AND rsn.lang_id = @langId AND rs.often_visit_id <> 1", cn);
                cmd.Parameters.Add("@langId", SqlDbType.TinyInt).Value = LangId;
                cn.Open();
                IDictionary<byte, string> Idic = new Dictionary<byte, string>();

                IDataReader reader = ExecuteReader(cmd);
                while (reader.Read())
                {
                    Idic.Add((byte)reader[0], reader[1].ToString());
                }
                return Idic;
            }
        }

        public IDictionary<byte, string> SelectReviewFindUs(byte LangId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT rs.find_us_id, rsn.title FROM tbl_site_review_find_us rs, tbl_site_review_find_us_name rsn WHERE rs.find_us_id = rsn.find_us_id AND rsn.lang_id = @langId AND rs.find_us_id <> 1", cn);
                cmd.Parameters.Add("@langId", SqlDbType.TinyInt).Value = LangId;
                cn.Open();
                IDictionary<byte, string> Idic = new Dictionary<byte, string>();

                IDataReader reader = ExecuteReader(cmd);
                while (reader.Read())
                {
                    Idic.Add((byte)reader[0], reader[1].ToString());
                }
                return Idic;
            }
        }


        public int InsertReviewenhances(int intREviewid, byte bytVisitId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_enhance_site_review (review_id,enhance_id)VALUES (@review_id,@enhance_id)", cn);
                cmd.Parameters.Add("@review_id", SqlDbType.Int).Value = intREviewid;
                cmd.Parameters.Add("@enhance_id", SqlDbType.TinyInt).Value = bytVisitId;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return ret;
            }
        }
        public IDictionary<byte, string> SelectReviewenhances(byte LangId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT rs.enhance_id, rsn.title FROM tbl_site_review_enhance rs, tbl_site_review_enhance_name rsn WHERE rs.enhance_id = rsn.enhance_id AND rsn.lang_id = 1 AND rs.enhance_id <> 1", cn);
                cmd.Parameters.Add("@langId", SqlDbType.TinyInt).Value = LangId;
                cn.Open();
                IDictionary<byte, string> Idic = new Dictionary<byte, string>();

                IDataReader reader = ExecuteReader(cmd);
                while (reader.Read())
                {
                    Idic.Add((byte)reader[0], reader[1].ToString());
                }
                return Idic;
            }
        }


        public int InsertReviewenhancesProduct(int intREviewid, byte bytVisitId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_enhance_product_site_review (review_id,product_enh_id)VALUES (@review_id,@product_enh_id)", cn);
                cmd.Parameters.Add("@review_id", SqlDbType.Int).Value = intREviewid;
                cmd.Parameters.Add("@product_enh_id", SqlDbType.TinyInt).Value = bytVisitId;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return ret;
            }
        }
        public IDictionary<byte, string> SelectReviewenhancesProduct(byte LangId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT rs.product_enh_id, rsn.title FROM tbl_site_review_enhance_product rs, tbl_site_review_enhance_product_name rsn WHERE rs.product_enh_id = rsn.product_enh_id AND rsn.lang_id = 1 AND rs.product_enh_id <> 1", cn);
                cmd.Parameters.Add("@langId", SqlDbType.TinyInt).Value = LangId;
                cn.Open();
                IDictionary<byte, string> Idic = new Dictionary<byte, string>();

                IDataReader reader = ExecuteReader(cmd);
                while (reader.Read())
                {
                    Idic.Add((byte)reader[0], reader[1].ToString());
                }
                return Idic;
            }
        }

        public int InsertReviewenProblem(int intREviewid, byte bytVisitId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_problem_review_site (review_id,problem_id)VALUES (@review_id,@problem_id)", cn);
                cmd.Parameters.Add("@review_id", SqlDbType.Int).Value = intREviewid;
                cmd.Parameters.Add("@problem_id", SqlDbType.TinyInt).Value = bytVisitId;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return ret;
            }
        }
        public IDictionary<byte, string> SelectReviewProblem(byte LangId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT rs.problem_id, rsn.title FROM tbl_site_review_problem rs, tbl_site_review_problem_name rsn WHERE rs.problem_id = rsn.problem_id AND rsn.lang_id = 1 AND rs.problem_id <> 1", cn);
                cmd.Parameters.Add("@langId", SqlDbType.TinyInt).Value = LangId;
                cn.Open();
                IDictionary<byte, string> Idic = new Dictionary<byte, string>();

                IDataReader reader = ExecuteReader(cmd);
                while (reader.Read())
                {
                    Idic.Add((byte)reader[0], reader[1].ToString());
                }
                return Idic;
            }
        }
    }
}