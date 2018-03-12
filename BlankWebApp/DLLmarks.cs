using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Data;

namespace BlankWebApp
{
    public class DLLmarks
    {
        string connString = "Data Source=.\\SQLEXPRESS;Initial Catalog = studentdb; Integrated Security = True";

        public string saveMark(ATTmarks ATTmarkObj)
        {
            SqlConnection connection = new SqlConnection(connString);
            string sp = "";
            string resMsg = "";
            sp = "dbo.saveMarks";
            connection.Open();
            SqlCommand cmd = new SqlCommand(sp, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@qid", SqlDbType.Int, 10).Value = ATTmarkObj.qid;
            cmd.Parameters.Add("@math", SqlDbType.Float, 50).Value = ATTmarkObj.math;
            cmd.Parameters.Add("@science", SqlDbType.Float, 50).Value = ATTmarkObj.science;
            cmd.Parameters.Add("@english", SqlDbType.Float, 50).Value = ATTmarkObj.english;
            cmd.Parameters.Add("@social", SqlDbType.Float, 50).Value = ATTmarkObj.social;
            cmd.ExecuteNonQuery();
            resMsg = "success";
            connection.Close();
            return resMsg;
        }

        public ATTmarks getMark(int sn)
        {
            SqlConnection connection = new SqlConnection(connString);
            ATTmarks ATTmarkObj = new ATTmarks();
            string sp = "dbo.getMark";
            connection.Open();
            SqlCommand cmd = new SqlCommand(sp, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@qid", SqlDbType.Int, 10).Value = sn;
            SqlDataReader dReader = cmd.ExecuteReader();
            while (dReader.Read())
            {
                ATTmarkObj.qid = dReader.GetInt32(0);
                ATTmarkObj.math = dReader.GetDouble(1);
                ATTmarkObj.science = dReader.GetDouble(2);
                ATTmarkObj.english = dReader.GetDouble(3);
                ATTmarkObj.social = dReader.GetDouble(4);
            }
            connection.Close();
            dReader.Close();
            return ATTmarkObj;
        }

        public string deleteMark(int sn)
        {
            SqlConnection connection = new SqlConnection(connString);
            string resMsg = "";
            string sp = "dbo.deleteMark";
            connection.Open();
            SqlCommand cmd = new SqlCommand(sp, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@qid", SqlDbType.Int, 10).Value = sn;
            cmd.ExecuteNonQuery();
            resMsg = "success";
            connection.Close();
            return resMsg;
        }

        public string updateMark(ATTmarks ATTmarkObj)
        {
            SqlConnection connection = new SqlConnection(connString);
            string resMsg = "";
            string sp = "dbo.updateMark";
            connection.Open();
            SqlCommand cmd = new SqlCommand(sp, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@qid", SqlDbType.Int, 10).Value = ATTmarkObj.qid;
            cmd.Parameters.Add("@math", SqlDbType.Float, 50).Value = ATTmarkObj.math;
            cmd.Parameters.Add("@science", SqlDbType.Float, 50).Value = ATTmarkObj.science;
            cmd.Parameters.Add("@english", SqlDbType.Float, 50).Value = ATTmarkObj.english;
            cmd.Parameters.Add("@social", SqlDbType.Float, 50).Value = ATTmarkObj.social;
            cmd.ExecuteNonQuery();
            resMsg = "success";
            connection.Close();
            return resMsg;
        }
    }
}