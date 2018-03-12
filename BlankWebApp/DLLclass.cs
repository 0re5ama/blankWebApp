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
        string connString = "Data Source=.\\SQLEXPRESS;Initial Catalog=studentdb;Integrated Security=True";
        public string save(ATTclass ATTclassobj)
        {
            SqlConnection connection = new SqlConnection(connString);
            string sp = "";
            string msg = "";
            sp = "dbo.save";
            connection.Open();
            SqlCommand cmd = new SqlCommand(sp, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@name", SqlDbType.VarChar, 50).Value = ATTclassobj.name;
            cmd.Parameters.Add("@gender", SqlDbType.VarChar, 1).Value = ATTclassobj.gender;
            cmd.Parameters.Add("@dob", SqlDbType.Date, 50).Value = ATTclassobj.dob;
            cmd.Parameters.Add("@address", SqlDbType.VarChar, 50).Value = ATTclassobj.address;
            cmd.Parameters.Add("@percentage", SqlDbType.Float, 50).Value = ATTclassobj.percentage;
            cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            int sid = Int32.Parse(cmd.Parameters["@id"].Value.ToString());
            connection.Close();
            msg = sid.ToString();
            return msg;
        }

        public List<ATTclass> showAll()
        {
            SqlConnection connection = new SqlConnection(connString);
            string sp = "dbo.showAll";
            string strDate = "";
            string date = "";
            List <ATTclass> ATTlist = new List<ATTclass>();
            connection.Open();
            SqlCommand cmd = new SqlCommand(sp, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader d_read = cmd.ExecuteReader();
            if (d_read.HasRows)
            {
                while (d_read.Read())
                {
                    ATTclass ATTobj = new ATTclass();
                    ATTobj.id = d_read.GetInt32(0);
                    ATTobj.name = d_read.GetString(1);
                    ATTobj.gender = d_read.GetString(2);
                    strDate = d_read[3].ToString();
                    date = strDate.Remove(strDate.IndexOf(" "));
                    ATTobj.dob = date;
                    ATTobj.address = d_read.GetString(4);
                    ATTobj.percentage = d_read.GetDouble(5);
                    ATTlist.Add(ATTobj);
                }
            }
            connection.Close();
            d_read.Close();
            return ATTlist;
        }

        public ATTclass getOne(int sn)
        {
            ATTclass ATTclassobj = new ATTclass();
            SqlConnection connection = new SqlConnection(connString);
            string sp = "dbo.getOne";
            string strDate = "";
            string date = "";
            connection.Open();
            SqlCommand cmd = new SqlCommand(sp, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", SqlDbType.Int, 5).Value = sn;
            SqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                ATTclassobj.id = dataReader.GetInt32(0);
                ATTclassobj.name = dataReader.GetString(1);
                ATTclassobj.gender = dataReader.GetString(2);
                strDate = dataReader[3].ToString();
                date = strDate.Remove(strDate.IndexOf(" "));
                ATTclassobj.dob = date;
                ATTclassobj.address = dataReader.GetString(4);
                ATTclassobj.percentage = dataReader.GetDouble(5);
            }
            connection.Close();
            return ATTclassobj;
        }

        public string deleteOne(int sn)
        {
            string msg = "";
            ATTclass ATTclassobj = new ATTclass();
            SqlConnection connection = new SqlConnection(connString);
            string sp = "dbo.deleteOne";
            connection.Open();
            SqlCommand cmd = new SqlCommand(sp, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", SqlDbType.Int, 5).Value = sn;
            DLLmarks DLLmarkObj = new DLLmarks();
            string dllRes = DLLmarkObj.deleteMark(sn);
            cmd.ExecuteNonQuery();
            msg = dllRes;
            connection.Close();
            return msg;
        }

        public string update(ATTclass ATTclassobj)
        {
            SqlConnection connection = new SqlConnection(connString);
            string sp = "";
            string msg = "";
            sp = "dbo.updateRow";
            connection.Open();
            SqlCommand cmd = new SqlCommand(sp, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", SqlDbType.Int, 10).Value = ATTclassobj.id;
            cmd.Parameters.Add("@name", SqlDbType.VarChar, 50).Value = ATTclassobj.name;
            cmd.Parameters.Add("@gender", SqlDbType.VarChar, 1).Value = ATTclassobj.gender;
            cmd.Parameters.Add("@dob", SqlDbType.Date, 50).Value = ATTclassobj.dob;
            cmd.Parameters.Add("@address", SqlDbType.VarChar, 50).Value = ATTclassobj.address;
            cmd.Parameters.Add("@percentage", SqlDbType.Float, 50).Value = ATTclassobj.percentage;
            cmd.ExecuteNonQuery();
            int sid = ATTclassobj.id;
            msg = "Successfully updated with ID: " + sid;
            connection.Close();
            return msg;
        }
    }
}