using Npgsql;
using System;
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
            string conStr = $@"Server = {"svc.gksl2.cloudtype.app"};
                             Port     = {"32590"};
                             Database = {"WebDev"};
                             UID      = {"User"};
                             password = {"qwe123"};";

            conn.ConnectionString = conStr;
        }

        public DataTable GetDataTable_PostgreSQL(string query)
        {
            try
            {
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();
                DataTable dt = new DataTable();

                adapter.SelectCommand = new NpgsqlCommand(query, conn); //DB 연결
                adapter.SelectCommand.CommandTimeout = 60;  //해당 시간 지나면 연결 불량으로 체크
                adapter.Fill(dt);   //데이터 불러오기

                if (dt.Rows.Count != 0)
                {
                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetDataTable_MySQL 오류\n{ex.Message}");
                throw;
            }
        }

        public void Excute_PostgreSQL(string query)
        {
            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);

            try
            {
                //query문 실행
                cmd.CommandTimeout = 60;
                cmd.Connection.Open();  //DB 연결
                cmd.ExecuteNonQuery();  //Query 작동
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetDataTable_MySQL 오류\n{ex.Message}");
                throw;
            }
            finally
            {
                cmd.Connection.Close(); //DB 연결 해제
            }
        }
    }
}
