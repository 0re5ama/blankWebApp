using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }
}