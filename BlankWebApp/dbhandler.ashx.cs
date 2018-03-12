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
        public string mark { get; set; }
    }
    public class dbhandler : IHttpHandler
    {
        JavaScriptSerializer jss = new JavaScriptSerializer();
        public void ProcessRequest(HttpContext context)
        {
            string action = "";
            string studRes = "";
            string markRes = "";
            string resMsg = "";
            string data = "";
            string mark = "";
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
                mark = json_from_req.mark;
            }
            switch (action)
            {
                case "saveStudent":
                    resMsg = save(data);
                    context.Response.ContentType = "text/plain";
                    context.Response.Write(resMsg);
                    break;
                case "saveMark":
                    resMsg = saveMark(data);
                    context.Response.ContentType = "text/plain";
                    context.Response.Write(resMsg);
                    break;
                case "showAllStudent":
                    resMsg = showAll();
                    context.Response.ContentType = "application/json";
                    context.Response.Write(resMsg);
                    break;
                case "getOne":
                    studRes = getOne(data);
                    markRes = getMark(data);
                    string studResMod = studRes.Substring(0, studRes.Length - 1);
                    string markResMod = markRes.Remove(0, 1);
                    resMsg = studResMod + ", " + markResMod;
                    context.Response.ContentType = "application/json";
                    context.Response.Write(resMsg);
                    break;
                case "deleteOne":
                    studRes = deleteOne(data);
                    resMsg = studRes;
                    context.Response.ContentType = "text/plain";
                    context.Response.Write(resMsg);
                    break;
                case "updateStudent":
                    studRes = updateRow(data);
                    markRes = updateMark(mark);
                    if(markRes == "success")
                    {
                        resMsg = studRes;
                    }
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

        public string saveMark(string mark)
        {
            BLLmarks BLLmarkObj = new BLLmarks();
            ATTmarks ATTmarkObj = jss.Deserialize<ATTmarks>(mark) as ATTmarks;
            string msg = BLLmarkObj.saveMark(ATTmarkObj);
            return msg;
        }

        public string getMark(string data)
        {
            int id = int.Parse(data);
            BLLmarks BLLmarkObj = new BLLmarks();
            string msg = BLLmarkObj.getMark(id);
            return msg;
        }

        public string deleteMark(string data)
        {
            int id = int.Parse(data);
            BLLmarks BLLmarkObj = new BLLmarks();
            string msg = BLLmarkObj.deleteMark(id);
            return msg;
        }

        public string updateMark(string mark)
        {
            BLLmarks BLLmarkObj = new BLLmarks();
            ATTmarks ATTmarkObj = jss.Deserialize<ATTmarks>(mark) as ATTmarks;
            string msg = BLLmarkObj.updateMark(ATTmarkObj);
            return msg;
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