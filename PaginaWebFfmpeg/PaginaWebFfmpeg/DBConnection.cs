using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Runtime.CompilerServices;
using System.Text;
using System.Data;

namespace PaginaWebFfmpeg.Controllers
{
    public class DBConnection
    {

        private static DBConnection instance = null;
        private string connString;
        MySqlConnection conn;

        private DBConnection() { }

        public static DBConnection getInstance()
        {
            if (instance == null)
            {
                DBConnection.instance = new DBConnection();
            }

            return DBConnection.instance;

        }

        public void connect(string connString)
        {
            this.connString = connString;
            this.conn = new MySqlConnection(connString);
        }

        public QueryBuilder getQueryBuilder()
        {
            return new QueryBuilder(this.conn);
        }

        public IDataReader select(QueryBuilder query)
        {

            this.conn.Open();
            var dataReader = query.comm.ExecuteReader();

            var dt = new DataTable();
            dt.Load(dataReader);
            this.conn.Close();

            return dt.CreateDataReader();

        }

        public void abm(QueryBuilder query)
        {

            this.conn.Open();
            query.comm.ExecuteNonQuery();
            this.conn.Close();

        }

        public class QueryBuilder
        {

            public MySqlConnection conn;
            public MySqlCommand comm;

            public QueryBuilder(MySqlConnection conn)
            {
                this.conn = conn;
            }

            public void setQuery(string sql)
            {
                this.comm = new MySqlCommand(sql, this.conn);
            }

            public void storeProcedure(string sql)
            {
                this.comm = new MySqlCommand(sql, this.conn);
                comm.CommandType = CommandType.StoredProcedure;
            }

            public QueryBuilder addParam(string param, int value)
            {

                this.comm.Parameters.AddWithValue(param, value);
                return this;

            }

            public QueryBuilder addParam(string param, string value)
            {

                this.comm.Parameters.AddWithValue(param, value);
                return this;

            }

            public QueryBuilder addParam(string param, bool value)
            {

                this.comm.Parameters.AddWithValue(param, value);
                return this;

            }

            public MySqlParameter addParamProcedure(string param)
            {

                var valor = this.comm.Parameters.Add("@return", MySqlDbType.Bit);
                return valor;

            }


        }

    }
}
