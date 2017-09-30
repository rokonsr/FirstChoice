using System.Configuration;
using System.Data.SqlClient;

namespace FirstChoiceApp.Gateway
{
    public class DbConnection
    {
        public SqlConnection Connection()
        {
            string conStr = ConfigurationManager.ConnectionStrings["FcConnection"].ConnectionString;

            SqlConnection con = new SqlConnection(conStr);
            con.Open();
            return con;
        }
    }
}