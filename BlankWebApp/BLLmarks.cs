using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace BlankWebApp
{
    public class BLLmarks
    {
        public string saveMark(ATTmarks ATTmarkObj)
        {
            DLLmarks DLLmarkObj = new DLLmarks();
            string resMsg = DLLmarkObj.saveMark(ATTmarkObj);
            return resMsg;
        }

        public string getMark(int sn)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            DLLmarks DLLmarkObj = new DLLmarks();
            ATTmarks ATTmarkObj = DLLmarkObj.getMark(sn);
            string resMsg = serializer.Serialize(ATTmarkObj);
            return resMsg;
        }

        public string deleteMark(int sn)
        {
            DLLmarks DLLmarkObj = new DLLmarks();
            string resMsg = DLLmarkObj.deleteMark(sn);
            return resMsg;
        }

        public string updateMark(ATTmarks ATTmarkObj)
        {
            DLLmarks DLLmarkObj = new DLLmarks();
            string resMsg = DLLmarkObj.updateMark(ATTmarkObj);
            return resMsg;
        }
    }
}