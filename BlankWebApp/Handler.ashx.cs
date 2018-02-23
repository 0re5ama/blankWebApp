using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.IO;

namespace BlankWebApp
{
    /// <summary>
    /// Summary description for Handler1
    /// </summary>
    public class person
    {
        public Int32 id { get; set; }
        public string name { get; set; }
        public Int32 age { get; set; }
    }
    public class Handler1 : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<person> ps = new List<person>();
            person p1 = new person();
            p1.name = context.Request.QueryString["name"];
            p1.age = Int32.Parse(context.Request.QueryString["age"]);
            p1.id = 1;
            ps.Add(p1);
            context.Response.ContentType = "application/json";
            var json = js.Serialize(ps);
            context.Response.Write(json);
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