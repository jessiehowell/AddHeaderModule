using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Web;

namespace AddHeaderModule
{
    public class AddHeader : IHttpModule
    {
        public void Dispose()
        {
        
        }

        public void Init(HttpApplication httpContext)
        {
            httpContext.PostAuthenticateRequest += new EventHandler(OnPostAuthenticateRequest);
        }

        public void OnPostAuthenticateRequest(Object source, EventArgs e)
        {
            HttpApplication httpApp = (HttpApplication)source;
            HttpRequest httpReq = httpApp.Context.Request;
            NameValueCollection customHeaders = ConfigurationManager.AppSettings;
            foreach (string headerName in customHeaders)
            {
                string headerValue = customHeaders[headerName];
                if (headerValue.StartsWith("{") && headerValue.EndsWith("}"))
                {
                    headerValue = httpReq.ServerVariables.Get(headerValue.TrimStart('{').TrimEnd('}'));
                }
                //NameValueCollection newHeader = new NameValueCollection();
                try
                {
                    //newHeader.Set(headerName, headerValue);
                    httpReq.Headers.Set(headerName, headerValue);
                }
                catch (Exception ex)
                {
                    //newHeader.Set("X-Exception-Thrown", ex.Source);
                    httpReq.Headers.Set("X-Exception-Thrown", ex.Source);
                }
            }
        }
    }
}