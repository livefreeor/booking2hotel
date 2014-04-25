using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using System.Web.Script.Serialization;
using System.Text;

using Newtonsoft.Json;


/// <summary>
/// Summary description for Hotels2Cookie
/// </summary>
/// 

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
    

        //using ExtensionMethods;

        //...

        //List<Person> people = new List<Person>{
        //                   new Person{ID = 1, FirstName = "Scott", LastName = "Gurthie"},
        //                   new Person{ID = 2, FirstName = "Bill", LastName = "Gates"}
        //                   };


        //string jsonString = people.ToJSON();

    }
}