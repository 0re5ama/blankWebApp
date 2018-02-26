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
            string data_from_req = new StreamReader(context.Request.InputStream).ReadToEnd();
            requestdata json_from_req = jss.Deserialize<requestdata>(data_from_req);
            string action = json_from_req.action;
            switch (action)
            {
                case "saveStudent":
                    BLLclass BLLobj = new BLLclass();
                    ATTclass ATTobj = jss.Deserialize<ATTclass>(json_from_req.data) as ATTclass;
                    string resmsg = BLLobj.saveStudent(ATTobj);
                    context.Response.ContentType = "text/plain";
                    context.Response.Write(resmsg);
                    break;
                case "showAllStudent":
                    BLLclass BLLobj2 = new BLLclass();
                    string resData = BLLobj2.showAll();
                    context.Response.ContentType = "application/json";
                    context.Response.Write(resData);
                    break;
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