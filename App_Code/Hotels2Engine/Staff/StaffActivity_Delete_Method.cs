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
    public partial class StaffActivity 
    {
        public static IList<ArrayList> ActionDeleteMethodStaff_log_MultipleRecord_FirstStep(string strQuery)
        {
            IList<ArrayList> idicOldVal = new List<ArrayList>();
            ArrayList arrObj = new ArrayList();
            
            
            StaffActivity Select = new StaffActivity();

            using (SqlConnection cn = new SqlConnection(Select.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(strQuery, cn);
                cn.Open();
                IDataReader reader = Select.ExecuteReader(cmd);

                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        object[] objItem = { reader.GetName(i), reader[i].ToString() };
                        arrObj.Add(objItem);
                        //idicOldVal.Add(reader[i].GetType().FullName, reader[i].ToString() );
                    }
                    idicOldVal.Add(arrObj);
                }

            }

            return idicOldVal;
        }
        public static IList<ArrayList> ActionDeleteMethodStaff_log_MultipleRecord_FirstStep(string TableName, string strFieldKeyName, bool? IsNot_of_KeyField,
            params object[] PrimaryKeyFieldValue)
        {
            IList<ArrayList> idicOldVal = new List<ArrayList>();
            ArrayList arrObj = new ArrayList();
            StaffActivity update = new StaffActivity();
            StringBuilder ActionCommnet = new StringBuilder();

            string KeyfieldId = strFieldKeyName.Trim().ToLower().Replace(" ", "");
            string[] arrKeyFileId = KeyfieldId.Split(',');

            StringBuilder stringCommand = new StringBuilder();
            stringCommand.Append("SELECT *");
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

            //HttpContext.Current.Response.Write(stringCommand.ToString());
            //HttpContext.Current.Response.End();
            StaffActivity Select = new StaffActivity();

            using (SqlConnection cn = new SqlConnection(Select.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(stringCommand.ToString(), cn);
                cn.Open();
                IDataReader reader = Select.ExecuteReader(cmd);

                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        object[] objItem = { reader.GetName(i), reader[i].ToString() };
                        arrObj.Add(objItem);
                        //idicOldVal.Add(reader[i].GetType().FullName, reader[i].ToString() );
                    }
                    idicOldVal.Add(arrObj);
                }

            }

            return idicOldVal;
        }


        public static int ActionDeleteMethodStaff_log_MultipleRecord_LastStep(StaffLogModule StaffModule, StaffLogActionType staffAction, StaffLogSection staffSection,
            object MainIdFocus, string TableName, IList<ArrayList> ObjFocus_OldValue, string strFieldKeyName, params object[] PrimaryKeyFieldValue)
        {
            Type DataType = PrimaryKeyFieldValue.GetType();
            StringBuilder ActionCommnet = new StringBuilder();
            string KeyfieldId = strFieldKeyName.Trim().ToLower().Replace(" ", "");

            string[] arrKeyFileId = KeyfieldId.Split(',');


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

           
            int count = 0;

            foreach (ArrayList arrItem in ObjFocus_OldValue)
            {
                ActionCommnet.Append("<" + TableName);

                if (arrKeyFileId.Length > 0 && PrimaryKeyFieldValue.Length > 0)
                {
                    for (int i = 0; i < arrKeyFileId.Length; i++)
                    {
                        if (PrimaryKeyFieldValue[i] == null)
                            ActionCommnet.Append(" " + arrKeyFileId[i] + "=\"NULL\"");
                        else
                            ActionCommnet.Append(" " + arrKeyFileId[i] + "=\"" + PrimaryKeyFieldValue[i] + "\"");
                    }
                }
                else
                {
                    if (PrimaryKeyFieldValue == null)
                        ActionCommnet.Append(" " + KeyfieldId + "=\"NULL\"");
                    else
                        ActionCommnet.Append(" " + KeyfieldId + "=\"" + PrimaryKeyFieldValue + "\"");

                }
                ActionCommnet.Append(" >" + (char)13 + (char)10);

                foreach (object[] focusItem in arrItem)
                {
                    if (focusItem[1] == null)
                    {
                        ActionCommnet.Append("<" + focusItem[0] + ">NULL</" + focusItem[0] + ">" + (char)13 + (char)10);
                    }
                    else
                    {
                        ActionCommnet.Append("<" + focusItem[0] + ">" + focusItem[1] + "</" + focusItem[0] + ">" + (char)13 + (char)10);
                    }
                    count = count + 1;
                }

                ActionCommnet.Append("</" + TableName + ">");

                cStaffActivity.InsertNewStaffActivity((int?)cStaffActivity.Product_Id, (int?)cStaffActivity.Booking_Id,
                (short?)cStaffActivity.Supplier_Id, (int?)cStaffActivity.Review_id, (byte)StaffModule, (byte)staffAction, ActionCommnet.ToString());

                ActionCommnet.Clear();
            }

            int insertResult = 1;
            return insertResult;
        }

        //============= DELETE SECTION ========================================================================================================
        //=====================================================================================================================================


        public static IList<object[]> ActionDeleteMethodStaff_log_FirstStep(string TableName, string strFieldKeyName, params object[] PrimaryKeyFieldValue)
        {
            IList<object[]> idicOldVal = new List<object[]>();

            StaffActivity update = new StaffActivity();
            StringBuilder ActionCommnet = new StringBuilder();

            string KeyfieldId = strFieldKeyName.Trim().ToLower().Replace(" ", "");
            string[] arrKeyFileId = KeyfieldId.Split(',');
           
            StringBuilder stringCommand = new StringBuilder();
            stringCommand.Append("SELECT *");
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

            //HttpContext.Current.Response.Write(stringCommand.ToString());
            //HttpContext.Current.Response.End();
            StaffActivity Select = new StaffActivity();

            using (SqlConnection cn = new SqlConnection(Select.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(stringCommand.ToString(), cn);
                cn.Open();
                IDataReader reader = Select.ExecuteReader(cmd, CommandBehavior.SingleRow);

                if (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        object[] objItem = {reader.GetName(i),reader[i].ToString()};
                        idicOldVal.Add(objItem);
                        //idicOldVal.Add(reader[i].GetType().FullName, reader[i].ToString() );
                    }
                }

            }
            
            return idicOldVal;
            

        }

        public static int ActionDeleteMethodStaff_log_LastStep(StaffLogModule StaffModule, StaffLogActionType staffAction, StaffLogSection staffSection,
            object MainIdFocus, string TableName, IList<object[]> ObjFocus_OldValue, string strFieldKeyName, params object[] PrimaryKeyFieldValue)
        {
            Type DataType = PrimaryKeyFieldValue.GetType();
            StringBuilder ActionCommnet = new StringBuilder();
            string KeyfieldId = strFieldKeyName.Trim().ToLower().Replace(" ", "");

            string[] arrKeyFileId = KeyfieldId.Split(',');

            
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

            ActionCommnet.Append("<" + TableName);

            if (arrKeyFileId.Length > 0 && PrimaryKeyFieldValue.Length > 0)
            {
                for (int i = 0; i < arrKeyFileId.Length; i++)
                {
                    if (PrimaryKeyFieldValue[i] == null)
                        ActionCommnet.Append(" " + arrKeyFileId[i] + "=\"NULL\"");
                    else
                        ActionCommnet.Append(" " + arrKeyFileId[i] + "=\"" + PrimaryKeyFieldValue[i] + "\"");
                }
            }
            else
            {
                if (PrimaryKeyFieldValue == null)
                    ActionCommnet.Append(" " + KeyfieldId + "=\"NULL\"");
                else
                    ActionCommnet.Append(" " + KeyfieldId + "=\"" + PrimaryKeyFieldValue + "\"");

            }
            ActionCommnet.Append(" >" + (char)13 + (char)10);
            int count = 0;

            foreach (object[] focusItem in ObjFocus_OldValue)
            {
                if (focusItem[1] == null)
                {
                    ActionCommnet.Append("<" + focusItem[0] + ">NULL</" + focusItem[0] + ">" + (char)13 + (char)10);
                }
                else
                {
                    ActionCommnet.Append("<" + focusItem[0] + ">" + focusItem[1] + "</" + focusItem[0] + ">" + (char)13 + (char)10);
                }
                count = count + 1;
            }

            ActionCommnet.Append("</" + TableName + ">");
                          
                    
               

            int insertResult = cStaffActivity.InsertNewStaffActivity((int?)cStaffActivity.Product_Id, (int?)cStaffActivity.Booking_Id,
                (short?)cStaffActivity.Supplier_Id, (int?)cStaffActivity.Review_id, (byte)StaffModule, (byte)staffAction, ActionCommnet.ToString());

            return insertResult;
        }
        
    }
}
