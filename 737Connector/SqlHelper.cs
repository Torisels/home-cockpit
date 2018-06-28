using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _737Connector
{

    class SqlHelper
    {

        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        public SqlHelper()
        {
            SetConnection();
        }
        private void SetConnection()
        {
            
            sql_con = new SQLiteConnection
                ("Data Source = db.db; Version = 3; ");
            sql_con.Open();

            string sql = "SELECT * FROM Events";
            var cmd = new SQLiteCommand(sql,sql_con);
            var r = cmd.ExecuteReader();
            while (r.Read())
            {
                Console.WriteLine(r["Id"]);
            }
        }
    }
}
