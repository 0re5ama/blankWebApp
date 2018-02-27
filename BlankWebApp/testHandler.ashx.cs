using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlankWebApp
{
    /// <summary>
    /// Summary description for testHandler
    /// </summary>
    public class testHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if(context.Request.HttpMethod == "GET")
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("This is GET");
            }
            else if(context.Request.HttpMethod == "POST")
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("This is POST");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}