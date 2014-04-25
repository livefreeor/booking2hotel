using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Linq;
using System.Web.Security;
using System.Security.Cryptography;
using System.Reflection;


using Hotels2thailand.DataAccess;


/// <summary>
/// Summary description for Hotels2BaseClass
/// Static Method
/// </summary>
/// 
namespace Hotels2thailand
{
    public class Hotels2BaseClass :DataConnect
    {
        //public int Hotels2DataContextInserting(object objDataContext, object objToInsert)
        //{
        //    System.Data.Linq.Table<object> ObjTableToInsert = new System.Data.Linq.Table<tbl_staff>();
        //    ObjTableToInsert.InsertOnSubmit(objToInsert);
        //    (objDataContext as LinqStaffDataContext).SubmitChanges();
        //    return 1;
        //}
        

        

        
        protected object MappingObjectFromDataContext(object fromObj)
        {
            Type fromObjectType = fromObj.GetType();
            Type toObjectType = this.GetType();
            int count = 0;
            foreach (PropertyInfo toProperty in
                toObjectType.GetProperties())
            {
                if (toProperty.CanRead)
                {
                    //string propertyNameFrom = fromObjectType.GetProperties()[count].Name;
                    //PropertyInfo fromProperty = fromObjectType.GetProperty(propertyNameFrom);
                    PropertyInfo fromProperty = fromObjectType.GetProperties()[count];
                    if (toProperty.CanWrite)
                    {
                        object fromValue = fromProperty.GetValue(fromObj, null);
                        toProperty.SetValue(this, fromValue, null);
                    }
                }
                count = count + 1;
            }
            return this;
        }


        protected List<object> MappingObjectFromDataContextCollection(IQueryable IqObjectDatacontext)
        {
            List<object> objList = new List<object>();

            foreach (object objDatacontext in IqObjectDatacontext)
            {
                Type fromObjectType = objDatacontext.GetType();
                Type toObjectType = this.GetType();
                

                int count = 0;
                object obj = Activator.CreateInstance(Type.GetType(this.GetType().FullName.ToString()));
                
                foreach (PropertyInfo toProperty in toObjectType.GetProperties())
                {
                    if (toProperty.CanRead)
                    {
                        PropertyInfo fromProperty = fromObjectType.GetProperties()[count];

                        if (toProperty.CanWrite)
                        {
                            object fromValue = fromProperty.GetValue(objDatacontext, null);
                            obj.GetType().GetProperties()[count].SetValue(obj, fromValue, null);

                        }
                    }
                    count = count + 1;
                }
                objList.Add(obj);
            }

            return objList;
        }

        protected object MappingObjectFromDataReader(IDataReader reader, object ClassTOMap)
        {
            //Type fromObjectType = fromObj.GetType();
            Type toObjectType = ClassTOMap.GetType();
            int count = 0;
            Object newObjectToReturn = Activator.CreateInstance(ClassTOMap.GetType());
            //object obj = Activator.CreateInstance(Type.GetType(this.GetType().FullName.ToString()));
            int FieldCount = reader.FieldCount;
            foreach (PropertyInfo toProperty in toObjectType.GetProperties())
            {
                if (toProperty.CanRead)
                {
                    if (toProperty.CanWrite)
                    {
                        if (count < FieldCount)
                        {
                            if (reader[count] != DBNull.Value)
                            {
                                toProperty.SetValue(newObjectToReturn, reader[count], null);
                            }
                        }
                    }
                }
                count = count + 1;
            }
            return newObjectToReturn;

        }

        protected object MappingObjectFromDataReader(IDataReader reader)
        {
            //Type fromObjectType = fromObj.GetType();
            Type toObjectType = this.GetType();
            int count = 0;
            Object newObjectToReturn = Activator.CreateInstance(this.GetType());
            //object obj = Activator.CreateInstance(Type.GetType(this.GetType().FullName.ToString()));

            int FieldCount = reader.FieldCount;
            foreach (PropertyInfo toProperty in toObjectType.GetProperties())
            {
               
                if (toProperty.CanRead)
                {
                    if (toProperty.CanWrite)
                    {
                        if (count < FieldCount)
                        {
                            if (reader[count] != DBNull.Value)
                            {
                               // HttpContext.Current.Response.Write(toProperty.Name + "<br/>");
                                //HttpContext.Current.Response.Flush();
                                toProperty.SetValue(newObjectToReturn, reader[count], null);
                            }
                        }
                    }
                }
                count = count + 1;
            }
            return newObjectToReturn;

        }

        protected List<object> MappingObjectCollectionFromDataReader(IDataReader reader, object ClassTOMap)
        {
            List<object> ListObject = new List<object>();
            while (reader.Read())
            {
                ListObject.Add(MappingObjectFromDataReader(reader, ClassTOMap));
            }
            return ListObject;
        }

        protected List<object> MappingObjectCollectionFromDataReader(IDataReader reader)
        {
            List<object> ListObject = new List<object>();
            while (reader.Read())
            {
                ListObject.Add(MappingObjectFromDataReader(reader));
            }
            return ListObject;
        }


        

        
    }
}
