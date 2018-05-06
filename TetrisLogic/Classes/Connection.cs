using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisInterfaces;

namespace TetrisLogic.Classes
{
    internal class Connection
    {
        public Connection(SqlConnectionStringBuilder conn)
        {
            if (conn != null)
            {
                _conn = conn;
            }
        }

        public bool SaveGamePoint(BoardPoint[,] field, int[,] curFigBody, int[,] nextFigBody, int level,
            int burnedLines, int score)
        {
            bool result = true;


            return result;
        }


        public SqlDataAdapter GetDataAdapter(SqlConnection conn)
        {
            SqlCommand cmd = conn.CreateCommand();
            //cmd.CommandText = "AddSavePoint";  
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd); 
          
            adapter.InsertCommand = new SqlCommand("EXEC AddSavePoint null, @level, @burnLine, @score", conn);
            adapter.InsertCommand.Parameters.Add("@job_desc", SqlDbType.NVarChar, 50, "job_desc");
            adapter.InsertCommand.Parameters.Add("@min_lvl", SqlDbType.TinyInt, 0, "min_lvl");
            adapter.InsertCommand.Parameters.Add("@max_lvl", SqlDbType.TinyInt, 0, "max_lvl");

            return adapter;
        }

        private readonly SqlConnectionStringBuilder _conn;

    }
}
