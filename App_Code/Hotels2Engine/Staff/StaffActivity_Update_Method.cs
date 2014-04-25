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

        //============= UPDATE SECTION ========================================================================================================
        //=====================================================================================================================================
        public static IList<ArrayList> ActionUpdateMethodStaff_log_MultipleRecord_FirstStep(string strQuery)
        {
            StaffActivity update = new StaffActivity();
            StringBuilder ActionCommnet = new StringBuilder();
            
            IList<ArrayList> IListObj = new List<ArrayList>();
            ArrayList arrObj = new ArrayList();
            
            StaffActivity Insert = new StaffActivity();

            using (SqlConnection cn = new SqlConnection(Insert.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(strQuery, cn);
                cn.Open();
                IDataReader reader = Insert.ExecuteReader(cmd);

                while (reader.Read())
                {

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        arrObj.Add(reader[i]);
                    }

                    IListObj.Add(arrObj);
                }

            }
                    
            return IListObj;
        }

        public static IList<ArrayList> ActionUpdateMethodStaff_log_MultipleRecord_FirstStep(string TableName, string Filed_focus, string strFieldKeyName,
            bool? IsNot_of_KeyField, params object[] PrimaryKeyFieldValue)
        {
            StaffActivity update = new StaffActivity();
            StringBuilder ActionCommnet = new StringBuilder();
            string KeyfieldId = strFieldKeyName.Trim().ToLower().Replace(" ", "");

            string[] arrKeyFileId = KeyfieldId.Split(',');


            IList<ArrayList> IListObj = new List<ArrayList>();
            ArrayList arrObj = new ArrayList();
            string focus = Filed_focus.ToLower().Trim().Replace(" ", "");

            if (!string.IsNullOrEmpty(focus))
            {

                if (focus.Hotels2CharFindLast(',') == false)
                {
                    string[] arrfocus = focus.Split(',');

                    if (arrfocus.Length - focus.Hotels2CharFind(',') == 1)
                    {
                        StringBuilder stringCommand = new StringBuilder();
                        stringCommand.Append("SELECT");

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

                        StaffActivity Insert = new StaffActivity();

                        using (SqlConnection cn = new SqlConnection(Insert.ConnectionString))
                        {
                            SqlCommand cmd = new SqlCommand(stringCommand.ToString(), cn);
                            cn.Open();
                            IDataReader reader = Insert.ExecuteReader(cmd);
                            
                           while(reader.Read())
                            {

                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    arrObj.Add(reader[i]);
                                }

                                IListObj.Add(arrObj);
                            }

                        }
                    }
                }
            }


            return IListObj;
        }


        public static int ActionUpdateMethodStaff_log_MultipleRecord_Laststep(StaffLogModule StaffModule, StaffLogActionType staffAction, StaffLogSection staffSection,
            object MainIdFocus, string TableName, string strQuery, IList<ArrayList> ObjFocus_OldValue, string strFieldKeyName, params object[] PrimaryKeyFieldValue)
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


            StaffActivity Insert = new StaffActivity();
            using (SqlConnection cn = new SqlConnection(Insert.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(strQuery, cn);
                cn.Open();
                IDataReader reader = Insert.ExecuteReader(cmd);

                IList<ArrayList> IListObj = new List<ArrayList>();
                ArrayList arrObj = new ArrayList();
                ArrayList arrObjname = new ArrayList();
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        arrObj.Add(reader[i]);
                        if (i == 0)
                        {
                            arrObjname.Add(reader.GetName(i));
                        }
                    }

                    IListObj.Add(arrObj);
                }

                int insertResult = 0;
                int count = IListObj.Count();
                for (int iList = 0; iList < count; iList++)
                {
                    ActionCommnet.Append("<" + TableName);

                    if (arrKeyFileId.Length > 0 && PrimaryKeyFieldValue.Length > 0)
                    {
                        for (int j = 0; j < arrKeyFileId.Length; j++)
                        {
                            //ActionCommnet.Append(" " + arrKeyFileId[i] + " = " + PrimaryKeyFieldValue[i]);
                            if (PrimaryKeyFieldValue[j] == null)
                                ActionCommnet.Append(" " + arrKeyFileId[j] + "=\"NULL\"");
                            else
                                ActionCommnet.Append(" " + arrKeyFileId[j] + "=\"" + PrimaryKeyFieldValue[j] + "\"");
                        }
                    }
                    else
                    {
                        //ActionCommnet.Append(" " + KeyfieldId + " = " + PrimaryKeyFieldValue);
                        if (PrimaryKeyFieldValue == null)
                            ActionCommnet.Append(" " + KeyfieldId + "=\"NULL\"");
                        else
                            ActionCommnet.Append(" " + KeyfieldId + "=\"" + PrimaryKeyFieldValue + "\"");

                    }

                    ActionCommnet.Append(" >" + (char)13 + (char)10);

                    int fieldCount = 0;
                    foreach (ArrayList focusItem in IListObj)
                    {
                        ActionCommnet.Append("<" + arrObjname[fieldCount] + ">" + ObjFocus_OldValue[iList][fieldCount] + "#/#" + focusItem[fieldCount] + "</" + arrObjname[fieldCount] + ">" + (char)13 + (char)10);
                        fieldCount = fieldCount + 1;
                    }

                    ActionCommnet.Append("</" + TableName + ">");

                    insertResult = cStaffActivity.InsertNewStaffActivity((int?)cStaffActivity.Product_Id, (int?)cStaffActivity.Booking_Id,
                   (short?)cStaffActivity.Supplier_Id, (int?)cStaffActivity.Review_id, (byte)StaffModule, (byte)staffAction, ActionCommnet.ToString());

                    ActionCommnet.Clear();
                }
               

                return insertResult;
            }
        }

        public static int ActionUpdateMethodStaff_log_MultipleRecord_Laststep(StaffLogModule StaffModule, StaffLogActionType staffAction, StaffLogSection staffSection,
            object MainIdFocus, string TableName, string Filed_focus, ArrayList ObjFocus_OldValue, string strFieldKeyName, bool? IsNot_of_KeyField, params object[] PrimaryKeyFieldValue)
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
                        if (arrKeyFileId.Length > 0)
                        {
                            for (int i = 0; i < arrKeyFileId.Length; i++)
                            {
                                stringCommand.Append(" " + arrKeyFileId[i] + ",");
                            }

                        }

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

                        StaffActivity Insert = new StaffActivity();

                        using (SqlConnection cn = new SqlConnection(Insert.ConnectionString))
                        {
                            SqlCommand cmd = new SqlCommand(stringCommand.ToString(), cn);
                            cn.Open();
                            IDataReader reader = Insert.ExecuteReader(cmd);

                            while (reader.Read())
                            {
                                ActionCommnet.Append("<" + TableName);

                                if (arrKeyFileId.Length > 0 && PrimaryKeyFieldValue.Length > 0)
                                {
                                    for (int i = 0; i < arrKeyFileId.Length; i++)
                                    {
                                        //ActionCommnet.Append(" " + arrKeyFileId[i] + " = " + PrimaryKeyFieldValue[i]);
                                        if (PrimaryKeyFieldValue[i] == null)
                                            ActionCommnet.Append(" " + arrKeyFileId[i] + "=\"NULL\"");
                                        else
                                            ActionCommnet.Append(" " + arrKeyFileId[i] + " =\"" + PrimaryKeyFieldValue[i] + "\"");
                                    }
                                }
                                else
                                {
                                    //ActionCommnet.Append(" " + KeyfieldId + " = " + PrimaryKeyFieldValue);
                                    if (PrimaryKeyFieldValue == null)
                                        ActionCommnet.Append(" " + KeyfieldId + "=\"NULL\"");
                                    else
                                        ActionCommnet.Append(" " + KeyfieldId + " =\"" + PrimaryKeyFieldValue + "\"");

                                }
                                ActionCommnet.Append(" >" + (char)13 + (char)10);
                                int count = 0;
                                foreach (string focusItem in arrfocus)
                                {
                                    ActionCommnet.Append("<" + focusItem + ">" + ObjFocus_OldValue[count] + "#/#" + reader[focusItem] + "</" + focusItem + ">" + (char)13 + (char)10);
                                    count = count + 1;
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

        
        public static ArrayList ActionUpdateMethodStaff_log_FirstStep(params object[] ObjFocus_OldValue)
        {
            ArrayList arrObj = new ArrayList();
            foreach (object item in ObjFocus_OldValue)
            {
                arrObj.Add(item);
            }
            return arrObj;
        }

        public static ArrayList ActionUpdateMethodStaff_log_FirstStep(string TableName, string Filed_focus, string strFieldKeyName, params object[] PrimaryKeyFieldValue)
        {
            StaffActivity update = new StaffActivity();
            StringBuilder ActionCommnet = new StringBuilder();
            string KeyfieldId = strFieldKeyName.Trim().ToLower().Replace(" ", "");

            string[] arrKeyFileId = KeyfieldId.Split(',');
            ArrayList arrObj = new ArrayList();
            string focus = Filed_focus.ToLower().Trim().Replace(" ", "");

            if (!string.IsNullOrEmpty(focus))
            {

                if (focus.Hotels2CharFindLast(',') == false)
                {
                    string[] arrfocus = focus.Split(',');

                    if (arrfocus.Length - focus.Hotels2CharFind(',') == 1)
                    {
                        StringBuilder stringCommand = new StringBuilder();
                        stringCommand.Append("SELECT");
                        
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

                        StaffActivity Insert = new StaffActivity();
                        
                        using (SqlConnection cn = new SqlConnection(Insert.ConnectionString))
                        {
                            SqlCommand cmd = new SqlCommand(stringCommand.ToString(), cn);
                            cn.Open();
                            IDataReader reader = Insert.ExecuteReader(cmd, CommandBehavior.SingleRow);

                            if (reader.Read())
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    arrObj.Add(reader[i]);
                                }
                            }

                        }
                    }
                }
            }


            return arrObj;
            

            
        }

        
        public static int ActionUpdateMethodStaff_log_Laststep(StaffLogModule StaffModule, StaffLogActionType staffAction, StaffLogSection staffSection,
            object MainIdFocus, string TableName, string Filed_focus, ArrayList ObjFocus_OldValue, string strFieldKeyName, params object[] PrimaryKeyFieldValue)
        {
            Type DataType = PrimaryKeyFieldValue.GetType();
            StringBuilder ActionCommnet = new StringBuilder();
            string KeyfieldId = strFieldKeyName.Trim().ToLower().Replace(" ", "");

            string[] arrKeyFileId = KeyfieldId.Split(',');

            string focus = Filed_focus.ToLower().Trim().Replace(" ", "");;
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
                        if (arrKeyFileId.Length > 0)
                        {
                            for (int i = 0; i < arrKeyFileId.Length; i++)
                            {
                                stringCommand.Append(" " + arrKeyFileId[i] + ",");
                            }

                        }

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

                        StaffActivity Insert = new StaffActivity();

                        using (SqlConnection cn = new SqlConnection(Insert.ConnectionString))
                        {
                            SqlCommand cmd = new SqlCommand(stringCommand.ToString(), cn);
                            cn.Open();
                            IDataReader reader = Insert.ExecuteReader(cmd, CommandBehavior.SingleRow);

                            if (reader.Read())
                            {
                                ActionCommnet.Append("<" + TableName);

                                if (arrKeyFileId.Length > 0 && PrimaryKeyFieldValue.Length > 0)
                                {
                                    for (int i = 0; i < arrKeyFileId.Length; i++)
                                    {
                                        //ActionCommnet.Append(" " + arrKeyFileId[i] + " = " + PrimaryKeyFieldValue[i]);
                                        if (PrimaryKeyFieldValue[i] == null)
                                            ActionCommnet.Append(" " + arrKeyFileId[i] + "=\"NULL\"");
                                        else
                                            ActionCommnet.Append(" " + arrKeyFileId[i] + " =\"" + PrimaryKeyFieldValue[i] + "\"");
                                    }
                                }
                                else
                                {
                                    //ActionCommnet.Append(" " + KeyfieldId + " = " + PrimaryKeyFieldValue);
                                    if (PrimaryKeyFieldValue == null)
                                        ActionCommnet.Append(" " + KeyfieldId + "=\"NULL\"");
                                    else
                                        ActionCommnet.Append(" " + KeyfieldId + " =\"" + PrimaryKeyFieldValue + "\"");

                                }
                                ActionCommnet.Append(" >" + (char)13 + (char)10);
                                int count = 0;
                                foreach (string focusItem in arrfocus)
                                {
                                    ActionCommnet.Append("<" + focusItem + ">" + ObjFocus_OldValue[count] + "#/#" + reader[focusItem] + "</" + focusItem + ">" + (char)13 + (char)10);
                                    count = count + 1;
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
