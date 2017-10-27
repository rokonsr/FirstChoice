using System.Configuration;
using System.Data.SqlClient;

namespace FirstChoiceApp.Gateway
{
    public class DbConnection
    {
        public string Connection()
        {
            return ConfigurationManager.ConnectionStrings["FcConnection"].ConnectionString;
        }
    } 
}