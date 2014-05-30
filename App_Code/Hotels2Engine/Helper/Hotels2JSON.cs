using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using System.Web.Script.Serialization;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


/// <summary>
/// Summary description for Hotels2Cookie
/// </summary>
/// 

//namespace Hotels2thailand
//{
//    public static class Hotels2JSON
//    {
    
//        public static string HotelsToJSON(this object obj)
//        {

//            string body = JsonConvert.SerializeObject(obj, Formatting.Indented);
//            return body;
//        }

//        public static string HotelsToJSON(this object obj, int recursionDepth)
//        {
//            JavaScriptSerializer serializer = new JavaScriptSerializer();
//            serializer.RecursionLimit = recursionDepth;
//            return serializer.Serialize(obj);
//        }
    

//        //using ExtensionMethods;

//        //...

//        //List<Person> people = new List<Person>{
//        //                   new Person{ID = 1, FirstName = "Scott", LastName = "Gurthie"},
//        //                   new Person{ID = 2, FirstName = "Bill", LastName = "Gates"}
//        //                   };


//        //string jsonString = people.ToJSON();

//    }
//}


namespace Hotels2thailand
{
    public static class Hotels2JSON
    {

        public static string HotelsToJSON(this object obj)
        {


            string body = JsonConvert.SerializeObject(obj, Formatting.Indented);
            return body;
        }

        public static string HotelsToJSON(this object obj, int recursionDepth)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.RecursionLimit = recursionDepth;
            return serializer.Serialize(obj);
        }

        public static dynamic Hotels2GetJson(string strJson)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            return serializer.Deserialize<dynamic>(strJson);
        }

        //public 

        public static object[] Hotel2ParseToArrayVal(object obj)
        {
            if (obj.GetType().Name == "Object[]")
                return (object[])obj;
            else
            {
                object[] data = new object[1] { obj };

                return data;
            }
        }


        /// <summary>
        /// function by Json.Net 
        /// return Jboject
        /// JToken is the base class for JObject, JArray, JProperty, JValue, etc. You can use the Children<T>() method to get a filtered list of a JToken's children that are of a certain type, for example JObject. Each JObject has a collection of JProperty objects, which can be accessed via the Properties() method. For each JProperty, you can get its Name. (Of course you can also get the Value if desired, which is another JToken.)
        /// 
        /// JArray array = JArray.Parse(json);

        //foreach (JObject content in array.Children<JObject>())
        //{
        //    foreach (JProperty prop in content.Properties())
        //    {
        //        Console.WriteLine(prop.Name);
        //    }
        //}
        /// </summary>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static JObject Hotels2GetJson2(string strJson)
        {
            //DataTable dt = null;
            //dt = JsonConvert.DeserializeObject<DataTable>(someParameter);

            JObject jObject = JObject.Parse(strJson);
            return jObject;
            // JToken jUser = jObject["CheckboxConditionList"];
            //dt = JsonConvert.DeserializeObject<DataTable>(someParameter);
            //string json = JsonConvert.SerializeObject(someParameter);
            //JObject jObject = JObject.Parse(someParameter);



            //Response.Write(cTierControl.InsertOrUpdateTierConditionReduce());
            //Response.End();

        }


    }
}