using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace BlankWebApp
{
    public class BLLclass
    {
        public string saveStudent(ATTclass ATTclassobj)
        {
            string msg = "";
            DLLclass DLLclassobj = new DLLclass();
            msg = DLLclassobj.save(ATTclassobj);
            return msg;
        }

        public string showAll()
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            DLLclass DLLclassobj = new DLLclass();
            List<ATTclass> ATTlist = DLLclassobj.showAll();
            string resMsg = serializer.Serialize(ATTlist);
            return resMsg;

        }

        public string getOne(int sn)
        {
            string resMsg = "";
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            DLLclass DLLclassobj = new DLLclass();
            ATTclass ATTclassobj = DLLclassobj.getOne(sn);
            resMsg = serializer.Serialize(ATTclassobj);
            return resMsg;
        }

        public string deleteOne(int sn)
        {
            string resMsg = "";
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            DLLclass DLLclassobj = new DLLclass();
            resMsg = DLLclassobj.deleteOne(sn);
            return resMsg;
        }

        public string updateStudent(ATTclass ATTclassobj)
        {
            string msg = "";
            DLLclass DLLclassobj = new DLLclass();
            msg = DLLclassobj.update(ATTclassobj);
            return msg;
        }
    }
}