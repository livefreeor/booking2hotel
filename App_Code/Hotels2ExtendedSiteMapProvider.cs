using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;

/// <summary>
/// Summary description for Hotels2SiteMapXMLOverride
/// </summary>
/// 
namespace Configuration
{

    public class ExtendedSiteMapProvider : XmlSiteMapProvider
    {

        public override void Initialize(string name, NameValueCollection attributes)
        {

            base.Initialize(name, attributes);

            this.SiteMapResolve += SmartSiteMapProvider_SiteMapResolve;

        }

        static SiteMapNode SmartSiteMapProvider_SiteMapResolve(object sender, SiteMapResolveEventArgs e)
        {

            if ((SiteMap.CurrentNode == null)) return null;

            SiteMapNode temp = SiteMap.CurrentNode.Clone(true);

            SiteMapNode tempNode = temp;

            while (tempNode != null)
            {

                string qs = GetQueryString(tempNode, e.Context);

                if (qs != null)
                {
                    //HttpContext.Current.Response.Write(tempNode["queryStringToInclude"]);
                    tempNode.Url += qs;

                }

                tempNode = tempNode.ParentNode;

            }

            return temp;

        }

        private static string GetQueryString(SiteMapNode node, HttpContext context)
        {

            if (node["queryStringToInclude"] == null) return null;

            NameValueCollection values = new NameValueCollection();

            string[] vars = node["queryStringToInclude"].Split(Convert.ToChar(","));

            foreach (string s in vars)
            {

                string var = s.Trim();

                if (context.Request.QueryString[var] == null) continue;

                values.Add(var, context.Request.QueryString[var]);

            }

            if (values.Count == 0) return null;

            return NameValueCollectionToString(values);

        }

        private static string NameValueCollectionToString(NameValueCollection col)
        {

            string[] parts = new string[col.Count];

            string[] keys = col.AllKeys;

            for (int i = 0; i <= keys.Length - 1; i++)
            {

                parts[i] = keys[i] + "=" + col[keys[i]];

            }

            return "?" + string.Join("&", parts);

        }

    }

}
