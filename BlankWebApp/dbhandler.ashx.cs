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
        JavaScriptSerializer jss = new JavaScriptSerializer();
        public void ProcessRequest(HttpContext context)
        {
            string action = "";
            string resMsg = "";
            string data = "";
            if(context.Request.HttpMethod == "GET")
            {
                action = context.Request["action"].ToString();
                data = context.Request["id"].ToString();
            }
            else if(context.Request.HttpMethod == "POST")
            {
                string data_from_req = new StreamReader(context.Request.InputStream).ReadToEnd();
                requestdata json_from_req = jss.Deserialize<requestdata>(data_from_req);
                action = json_from_req.action;
                data = json_from_req.data;
            }
            switch (action)
            {
                case "saveStudent":
                    resMsg = save(data);
                    context.Response.ContentType = "text/plain";
                    context.Response.Write(resMsg);
                    break;
                case "showAllStudent":
                    resMsg = showAll();
                    context.Response.ContentType = "application/json";
                    context.Response.Write(resMsg);
                    break;
                case "getOne":
                    resMsg = getOne(data);
                    context.Response.ContentType = "application/json";
                    context.Response.Write(resMsg);
                    break;
                case "deleteOne":
                    resMsg = deleteOne(data);
                    context.Response.ContentType = "text/plain";
                    context.Response.Write(resMsg);
                    break;
                case "updateStudent":
                    resMsg = updateRow(data);
                    context.Response.ContentType = "text/plain";
                    context.Response.Write(resMsg);
                    break;
            }
        }

        public string save(string data)
        {
            string resMsg = "";
            BLLclass BLLobj = new BLLclass();
            ATTclass ATTobj = jss.Deserialize<ATTclass>(data) as ATTclass;
            resMsg = BLLobj.saveStudent(ATTobj);
            return resMsg;
        }

        public string showAll()
        {
            BLLclass BLLobj = new BLLclass();
            string resData = BLLobj.showAll();
            return resData;
        }

        public string getOne(string data)
        {
            int id = int.Parse(data);
            BLLclass BLLobj = new BLLclass();
            string singleDetail = BLLobj.getOne(id);
            return singleDetail;
        }

        public string deleteOne(string data)
        {
            int id = int.Parse(data);
            BLLclass BLLobj = new BLLclass();
            string delMsg = BLLobj.deleteOne(id);
            return delMsg;
        }

        public string updateRow(string data)
        {
            BLLclass BLLobj = new BLLclass();
            ATTclass ATTobj = jss.Deserialize<ATTclass>(data) as ATTclass;
            string updMsg = BLLobj.updateStudent(ATTobj);
            return updMsg;
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