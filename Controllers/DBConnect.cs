using Npgsql;
using System.Data;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Sockets;

namespace myWebApp.Controllers
{
    public class DBConnect
    {
        NpgsqlConnection conn = new NpgsqlConnection();

        public DBConnect()
        {
            string conStr = $@"Server   = {"svc.gksl2.cloudtype.app"};
                             Port       = {"32590"};
                             Database = {"WebDev"};
                             UID      = {"User"};
                             password = {"qwe123"};";

            conn.ConnectionString = conStr;
        }

        internal DataTable GetDataTable_PostgreSQL(string query)
        {
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();

            DataTable dt = new DataTable();
            adapter.SelectCommand = new NpgsqlCommand(query, conn);
            adapter.SelectCommand.CommandTimeout = 60;
            adapter.Fill(dt);


            if(dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return dt;
            }
        }

    }
}
