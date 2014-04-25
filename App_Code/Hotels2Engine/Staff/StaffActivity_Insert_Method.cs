using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Staff;

/// <summary>
/// Summary description for StaffActivity
/// </summary>
/// 
namespace Hotels2thailand.Staffs
{
    public partial class StaffActivity : Hotels2BaseClass
    {

        // declare Property for Staff class!
        public int Activity_Id { get; set; }
        public short Staff_Id { get; set; }

        private Nullable<int> _product_id;
        private Nullable<int> _booking_id;
        private Nullable<short> _supplier_id;
        private Nullable<int> _review_id;

        public Nullable<int> Product_Id 
        {
            get { return _product_id; }

            set { _product_id = value; }
        }
        public Nullable<int> Booking_Id
        {
            get { return _booking_id; }

            set { _booking_id = value; }
        }

        public Nullable<short> Supplier_Id
        {
            get { return _supplier_id; }

            set { _supplier_id = value; }
        }

        public Nullable<int> Review_id
        {
            get { return _review_id; }

            set { _review_id = value; }
        }

        public byte Cat_Id { get; set; }
        public byte TypeID { get; set; }
        public string PageAction { get; set; }
        public string IP { get; set; }
        public DateTime DateActivity { get; set; }
        public string Comment { get; set; }


        private string _cat_title = string.Empty;
        
       // Constructor set Defualt Values
        public StaffActivity()
        {
            _product_id = null;
            _booking_id = null;
            _supplier_id = null;
            _review_id = null;
        }



        public int InsertNewStaffActivity(int? intProductId, int? intbookingId, short? shrSupplierId, int? intReviewId, byte bytCatId, byte bytTypeId, string Comment)
        {
            StaffSessionAuthorize cStaffId = new StaffSessionAuthorize();

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder Query = new StringBuilder();
                Query.Append("INSERT INTO tbl_staff_activity(staff_id,product_id,booking_id,supplier_id,review_id,cat_id,type_id,page,ip_address,date_activity,comment)");
                Query.Append(" VALUES(@staff_id,@product_id,@booking_id,@supplier_id,@review_id,@cat_id,@type_id,@page,@ip_address,@date_activity,@comment)");
                

                SqlCommand cmd = new SqlCommand(Query.ToString(), cn);
                cmd.Parameters.Add("@staff_id", SqlDbType.SmallInt).Value = cStaffId.CurrentStaffId;

                if (intProductId == null)
                    cmd.Parameters.AddWithValue("@product_id", DBNull.Value);
                    
                else
                    cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;


                if (intbookingId == null)
                    cmd.Parameters.AddWithValue("@booking_id", DBNull.Value);
                else
                    cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = intbookingId;


                if (shrSupplierId == null)
                    cmd.Parameters.AddWithValue("@supplier_id", DBNull.Value);
                else
                    cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;

                if (intReviewId == null)
                    cmd.Parameters.AddWithValue("@review_id", DBNull.Value);
                else
                    cmd.Parameters.Add("@review_id", SqlDbType.Int).Value = intReviewId;

                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytCatId;
                cmd.Parameters.Add("@type_id", SqlDbType.TinyInt).Value = bytTypeId;
                cmd.Parameters.Add("@page", SqlDbType.VarChar).Value = HttpContext.Current.Request.Url.ToString();
                cmd.Parameters.Add("@ip_address", SqlDbType.VarChar).Value = HttpContext.Current.Request.UserHostAddress;
                cmd.Parameters.Add("@date_activity", SqlDbType.SmallDateTime).Value = DateTime.Now.Hotels2ThaiDateTime();
                cmd.Parameters.Add("@comment", SqlDbType.NVarChar).Value = Comment;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return ret;
            }
        }


        //============= INSERT SECTION ========================================================================================================
        //=====================================================================================================================================
        public static int ActionInsertMethodStaff_log_MultipleRecord(StaffLogModule StaffModule, StaffLogActionType staffAction, StaffLogSection staffSection,
            object MainIdFocus, string TableName, string Filed_focus, string strFieldKeyName, bool? IsNot_of_KeyField, params object[] PrimaryKeyFieldValue)
        {
            Type DataType = PrimaryKeyFieldValue.GetType();
            StringBuilder ActionCommnet = new StringBuilder();
            string KeyfieldId = strFieldKeyName.Trim().ToLower().Replace(" ", "");

            string[] arrKeyFileId = KeyfieldId.Split(',');

            string focus = Filed_focus.ToLower().Trim().Replace(" ", "");

            StaffActivity cStaffActivity = new StaffActivity();
            switch (staffSection)
            {
                case StaffLogSection.Product:
                    cStaffActivity.Product_Id = (int)MainIdFocus;
                    break;
                case StaffLogSection.Booking:
                    cStaffActivity.Booking_Id = (int)MainIdFocus;
                    break;
                case StaffLogSection.Supplier:
                    cStaffActivity.Supplier_Id = (short)MainIdFocus;
                    break;
                case StaffLogSection.F_Review:
                    cStaffActivity.Review_id = (int)MainIdFocus;
                    break;
            }


            if (!string.IsNullOrEmpty(focus))
            {

                if (focus.Hotels2CharFindLast(',') == false)
                {
                    string[] arrfocus = focus.Split(',');

                    if (arrfocus.Length - focus.Hotels2CharFind(',') == 1)
                    {
                        StringBuilder stringCommand = new StringBuilder();
                        stringCommand.Append("SELECT");
                        //if (arrKeyFileId.Length > 0)
                        //{

                        //    for (int i = 0; i < arrKeyFileId.Length; i++)
                        //    {
                        //        stringCommand.Append(" " + arrKeyFileId[i] + ",");

                        //    }

                        //}

                        stringCommand.Append(" " + focus + "");
                        stringCommand.Append(" FROM " + TableName + " WHERE ");

                        if (arrKeyFileId.Length > 0 && PrimaryKeyFieldValue.Length > 0)
                        {
                            for (int i = 0; i < arrKeyFileId.Length; i++)
                            {


                                if (i != arrKeyFileId.Length - 1 && i != PrimaryKeyFieldValue.Length - 1)
                                {
                                    if (PrimaryKeyFieldValue[i] == null)
                                    {

                                        
                                        if (IsNot_of_KeyField != null && (bool)IsNot_of_KeyField)
                                            stringCommand.Append(" " + arrKeyFileId[i] + " IS NULL AND ");
                                        else
                                            stringCommand.Append(" " + arrKeyFileId[i] + " IS NOT NULL AND ");
                                        
                                    }
                                    else
                                    {
                                        if (PrimaryKeyFieldValue[i].ToString().Hotels2CharFind(',') > 0)
                                        {
                                            if (IsNot_of_KeyField != null && (bool)IsNot_of_KeyField)
                                                stringCommand.Append(" " + arrKeyFileId[i] + " IN ( " + PrimaryKeyFieldValue[i] + " ) AND ");
                                            else
                                                stringCommand.Append(" " + arrKeyFileId[i] + " NOT IN ( " + PrimaryKeyFieldValue[i] + " ) AND ");
                                        }
                                        else
                                        {
                                            stringCommand.Append(" " + arrKeyFileId[i] + " = " + PrimaryKeyFieldValue[i] + " AND ");
                                        }
                                        
                                    }

                                }
                                else
                                {
                                    if (PrimaryKeyFieldValue[i] == null)
                                    {
                                        stringCommand.Append(" " + arrKeyFileId[i] + " IS NULL");
                                    }
                                    else
                                    {
                                        stringCommand.Append(" " + arrKeyFileId[i] + " = " + PrimaryKeyFieldValue[i]);
                                    }
                                }

                            }
                        }
                        else
                        {
                            if (PrimaryKeyFieldValue == null)
                                stringCommand.Append(" " + KeyfieldId + " IS NULL");
                            else
                                stringCommand.Append(" " + KeyfieldId + " = " + PrimaryKeyFieldValue);

                        }

                        //string Query = "SELECT " + fieldId + " ," + Filed_focus + " FROM " + TableName + " WHERE " + fieldId + " = " + PrimaryKeyFieldId;
                        //HttpContext.Current.Response.Write(stringCommand.ToString());
                        //HttpContext.Current.Response.End();
                        StaffActivity Insert = new StaffActivity();

                        //IDataReader Result = Insert.SelectMethod(Query, fieldId, PrimaryKeyFieldId);
                        //SelectMethod(string sqlcommand, string Id, object objId)
                        ArrayList arrSTringItem = new ArrayList();
                        using (SqlConnection cn = new SqlConnection(Insert.ConnectionString))
                        {
                            
                            SqlCommand cmd = new SqlCommand(stringCommand.ToString(), cn);
                            //cmd.Parameters.AddWithValue(fieldId, PrimaryKeyFieldId);
                            cn.Open();
                            IDataReader reader = Insert.ExecuteReader(cmd);

                            while (reader.Read())
                            {
                                //int Count = Result.FieldCount;
                                ActionCommnet.Append("<" + TableName);

                                if (arrKeyFileId.Length > 0 && PrimaryKeyFieldValue.Length > 0)
                                {
                                    for (int i = 0; i < arrKeyFileId.Length; i++)
                                    {
                                        //ActionCommnet.Append(" " + arrKeyFileId[i] + " = " + PrimaryKeyFieldValue[i]);
                                        if (PrimaryKeyFieldValue[i] == null)
                                            ActionCommnet.Append(" " + arrKeyFileId[i] + "=\"NULL\"");
                                        else
                                            ActionCommnet.Append(" " + arrKeyFileId[i] + "=\"" + PrimaryKeyFieldValue[i] +"\"");

                                    }
                                }
                                else
                                {
                                    // ActionCommnet.Append(" " + KeyfieldId + " = " + PrimaryKeyFieldValue);
                                    if (PrimaryKeyFieldValue == null)
                                        ActionCommnet.Append(" " + KeyfieldId + "=\"NULL\"");
                                    else
                                        ActionCommnet.Append(" " + KeyfieldId + "=\"" + PrimaryKeyFieldValue+"\"");

                                }
                                ActionCommnet.Append(" >" + (char)13 + (char)10);
                                foreach (string focusItem in arrfocus)
                                {

                                    string FocusItemreader = "\"" + focusItem + "\"";
                                    //HttpContext.Current.Response.Write(reader[focusItem]);
                                    //HttpContext.Current.Response.End();
                                    ActionCommnet.Append("<" + focusItem + ">" + reader[focusItem] + "</" + focusItem + ">" + (char)13 + (char)10);
                                }

                                ActionCommnet.Append("</" + TableName + ">");

                                arrSTringItem.Add(ActionCommnet.ToString());
                            }

                        }
                        
                        foreach (string item in arrSTringItem)
                        {
                            cStaffActivity.InsertNewStaffActivity((int?)cStaffActivity.Product_Id, (int?)cStaffActivity.Booking_Id,
                            (short?)cStaffActivity.Supplier_Id, (int?)cStaffActivity.Review_id, (byte)StaffModule, (byte)staffAction, ActionCommnet.ToString());
                        }
                    }
                }

            }

            int insertResult = 1;
            return insertResult;
        }


        public static int ActionInsertMethodStaff_log(StaffLogModule StaffModule, StaffLogActionType staffAction, StaffLogSection staffSection, 
            object MainIdFocus, string TableName, string Filed_focus,string strFieldKeyName, params object[] PrimaryKeyFieldValue)
        {
            Type DataType = PrimaryKeyFieldValue.GetType();
            StringBuilder ActionCommnet = new StringBuilder();
            string KeyfieldId = strFieldKeyName.Trim().ToLower().Replace(" ", "");

            string[] arrKeyFileId = KeyfieldId.Split(',');

            string focus = Filed_focus.ToLower().Trim().Replace(" ", "");
            
            StaffActivity cStaffActivity = new StaffActivity();
            switch (staffSection)
            {
                case StaffLogSection.Product :
                    cStaffActivity.Product_Id = (int)MainIdFocus;
                    break;
                case StaffLogSection.Booking:
                    cStaffActivity.Booking_Id = (int)MainIdFocus;
                    break;
                case StaffLogSection.Supplier:
                    cStaffActivity.Supplier_Id = (short)MainIdFocus;
                    break;
                case StaffLogSection.F_Review:
                    cStaffActivity.Review_id = (int)MainIdFocus;
                    break;
            }


            if (!string.IsNullOrEmpty(focus))
            {
                
                if (focus.Hotels2CharFindLast(',') == false)
                {
                    string[] arrfocus = focus.Split(',');

                    if (arrfocus.Length - focus.Hotels2CharFind(',') == 1)
                    {
                        StringBuilder stringCommand = new StringBuilder();
                        stringCommand.Append("SELECT");
                        //if (arrKeyFileId.Length > 0)
                        //{

                        //    for (int i = 0; i < arrKeyFileId.Length; i++)
                        //    {
                        //        stringCommand.Append(" " + arrKeyFileId[i] + ",");

                        //    }

                        //}

                        stringCommand.Append(" " + focus + "");
                        stringCommand.Append(" FROM " + TableName + " WHERE ");

                        if (arrKeyFileId.Length > 0 && PrimaryKeyFieldValue.Length > 0)
                        {
                            for (int i = 0; i < arrKeyFileId.Length; i++)
                            {
                                
                                    
                                if (i != arrKeyFileId.Length - 1 && i != PrimaryKeyFieldValue.Length - 1)
                                {
                                    if (PrimaryKeyFieldValue[i] == null)
                                        stringCommand.Append(" " + arrKeyFileId[i] + " IS NULL AND ");
                                    else
                                        stringCommand.Append(" " + arrKeyFileId[i] + " = " + PrimaryKeyFieldValue[i] + " AND ");

                                }
                                else
                                {
                                    if (PrimaryKeyFieldValue[i] == null)
                                        stringCommand.Append(" " + arrKeyFileId[i] + " IS NULL");
                                    else
                                        stringCommand.Append(" " + arrKeyFileId[i] + " = " + PrimaryKeyFieldValue[i]);
                                }
                                
                            }
                        }
                        else
                        {
                            if (PrimaryKeyFieldValue == null)
                                stringCommand.Append(" " + KeyfieldId + " IS NULL");
                            else
                                stringCommand.Append(" " + KeyfieldId + " = " + PrimaryKeyFieldValue);
                            
                        }
                      
                        //string Query = "SELECT " + fieldId + " ," + Filed_focus + " FROM " + TableName + " WHERE " + fieldId + " = " + PrimaryKeyFieldId;
                        //HttpContext.Current.Response.Write(stringCommand.ToString());
                        //HttpContext.Current.Response.End();
                        StaffActivity Insert = new StaffActivity();

                        //IDataReader Result = Insert.SelectMethod(Query, fieldId, PrimaryKeyFieldId);
                        //SelectMethod(string sqlcommand, string Id, object objId)
                        using (SqlConnection cn = new SqlConnection(Insert.ConnectionString))
                        {
                            SqlCommand cmd = new SqlCommand(stringCommand.ToString(), cn);
                            //cmd.Parameters.AddWithValue(fieldId, PrimaryKeyFieldId);
                            cn.Open();
                            IDataReader reader = Insert.ExecuteReader(cmd, CommandBehavior.SingleRow);

                            if (reader.Read())
                            {
                                //int Count = Result.FieldCount;
                                ActionCommnet.Append("<" + TableName );

                                if (arrKeyFileId.Length > 0 && PrimaryKeyFieldValue.Length > 0)
                                {
                                    for (int i = 0; i < arrKeyFileId.Length; i++)
                                    {
                                        //ActionCommnet.Append(" " + arrKeyFileId[i] + " = " + PrimaryKeyFieldValue[i]);
                                        if (PrimaryKeyFieldValue[i] == null)
                                            ActionCommnet.Append(" " + arrKeyFileId[i] + "=\"NULL\"");
                                        else
                                            ActionCommnet.Append(" " + arrKeyFileId[i] + "=\"" + PrimaryKeyFieldValue[i] + "\"");

                                    }
                                }
                                else
                                {
                                   // ActionCommnet.Append(" " + KeyfieldId + " = " + PrimaryKeyFieldValue);
                                    if (PrimaryKeyFieldValue == null)
                                        ActionCommnet.Append(" " + KeyfieldId + "=\"NULL\"");
                                    else
                                        ActionCommnet.Append(" " + KeyfieldId + "=\"" + PrimaryKeyFieldValue + "\"");
                                    
                                }
                                ActionCommnet.Append(" >" + (char)13 + (char)10);
                                foreach (string focusItem in arrfocus)
                                {

                                    string FocusItemreader = "\"" + focusItem + "\"";
                                    //HttpContext.Current.Response.Write(reader[focusItem]);
                                    //HttpContext.Current.Response.End();
                                    ActionCommnet.Append("<" + focusItem + ">" + reader[focusItem] + "</" + focusItem + ">" + (char)13 + (char)10);
                                }

                                ActionCommnet.Append("</" + TableName + ">");
                            }
                            
                        }
                        
                        
                    }
                }

            }

            int insertResult = cStaffActivity.InsertNewStaffActivity((int?)cStaffActivity.Product_Id, (int?)cStaffActivity.Booking_Id,
                (short?)cStaffActivity.Supplier_Id, (int?)cStaffActivity.Review_id, (byte)StaffModule, (byte)staffAction, ActionCommnet.ToString());
            return insertResult;
            
        }

    }
}
