using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pet___Beyonds
{
    internal class DBcon
    {
        private SqlConnection con = new SqlConnection(@"Data Source=NISA\sqlexpress;Initial Catalog=""OOP2 project database file"";Integrated Security=True");
        public SqlConnection GetCon()
        { return con; }
        public void Opencon()
        {
            if (con.State == ConnectionState.Closed)
            { con.Open(); }
        }
        public void Closecon()
        {
            if (con.State == ConnectionState.Open)
            { con.Close(); }
        }
    }
}
