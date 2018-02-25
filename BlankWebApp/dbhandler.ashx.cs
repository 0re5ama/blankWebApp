using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.IO;

namespace BlankWebApp
{
    /// <summary>
    /// Summary description for dbhandler
    /// </summary>
    
    public class requestdata
    {
        public string action { get; set; }
        public string data { get; set; }
    }
    public class dbhandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string data_from_req = new StreamReader(context.Request.InputStream).ReadToEnd();}
            BLLclass BLLobj = new BLLclass();
            ATTclass ATTobj = reqmsg as ATTclass;
            // string resmsg = BLLobj.saveStudent(ATTobj);
            context.Response.Write(jss.Serialize("false"));
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