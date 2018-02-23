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
    public class stud
    {
        public int id { get; set; }
        public string name { get; set; }
        public char gender { get; set; }
        public string dob { get; set; }
        public string address { get; set; }
        public float? percentage { get; set; }
    }
    public class dbhandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string obj = new StreamReader(context.Request.InputStream).ReadToEnd();
            Object reqmsg = Newtonsoft.Json.JsonConvert.DeserializeObject(obj, typeof(ATTclass));
            BLLclass BLLobj = new BLLclass();
            ATTclass ATTobj = reqmsg as ATTclass;
            string resmsg = BLLobj.saveStudent(ATTobj);
            context.Response.Write(jss.Serialize(resmsg));
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