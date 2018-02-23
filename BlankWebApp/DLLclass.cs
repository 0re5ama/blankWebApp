using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;

namespace BlankWebApp
{
    public class DLLclass
    {
        public string save(ATTclass ATTclassobj)
        {
            string sp = "";
            string connstring = "";
            string msg = "";
            connstring = "Data Source =.\\SQLEXPRESS; Initial Catalog = studentdb; Integrated Security = True";
            SqlConnection connection;
            connection = new SqlConnection(connstring);
            sp = "dbo.save";
            connection.Open();
            SqlCommand cmd = new SqlCommand(sp, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@name", SqlDbType.VarChar, 50).Value = ATTclassobj.name;
            cmd.Parameters.Add("@gender", SqlDbType.Char, 1).Value = ATTclassobj.gender;
            cmd.Parameters.Add("@dob", SqlDbType.Date, 50).Value = ATTclassobj.dob;
            cmd.Parameters.Add("@address", SqlDbType.VarChar, 50).Value = ATTclassobj.address;
            cmd.Parameters.Add("@percentage", SqlDbType.Float, 50).Value = ATTclassobj.percentage;
            cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Output;
            int eid = Int32.Parse(cmd.Parameters["@id"].Value.ToString());
            cmd.ExecuteNonQuery();
            msg = "Saved successfully with id: " + eid;
            connection.Close();
            return msg;
        }
    }
}