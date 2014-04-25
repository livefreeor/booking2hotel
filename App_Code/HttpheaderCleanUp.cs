using System;
using System.Web;

/// <summary>
/// Summary description for HttpheaderCleanUp
/// </summary>
/// 
namespace Hotels2thailand
{
    public class HttpheaderCleanUp:IHttpModule
    {
        public HttpheaderCleanUp()
        {
            
        }

        public void Init(HttpApplication context)
        {
            
            
            context.PreSendRequestHeaders += OnPreSendRequestHeaders;
        }

        void OnPreSendRequestHeaders(object sender, EventArgs e)
        {
            HttpResponse response = HttpContext.Current.Response;
            response.Headers.Remove("Server");
            response.Headers.Remove("ETag");
        }

        public void Dispose()
        {
        }
    }
}
