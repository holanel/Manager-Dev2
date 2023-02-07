using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Vyvojovy_pomocnik.Classes
{
    class DB
    {

        protected readonly static string Server_DB1 = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\janho\\Desktop\\Manager Dev\\Vyvojovy_pomocnik\\Manager_dev.mdf\";Integrated Security=True";

        protected readonly static string Server_DB = "Vyvojovy_pomocnik";
        
        protected readonly static string Server_Name = "Manager_dev";

        protected readonly static string Server_User = "sysdba";

        protected readonly static string Server_Password = "password";


        private static SqlConnection _connection;
        public static SqlConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    SqlConnection connection = new SqlConnection(Server_DB1);
                    //SqlConnection connection = new SqlConnection("server=" + Server_DB + ";database=" + Server_Name + ";user=" + Server_User + ";password=" + Server_Password);
                    connection.Open();

                    _connection = connection;
                }
                return _connection;
            }
        }


        public static Boolean QueryVoid(Dictionary<object, object> slovnik, String sql)
        {
            try
            {
                using (SqlCommand command = new SqlCommand(sql, Connection))
                {
                    if (slovnik != null)
                    {
                        foreach (KeyValuePair<object, object> kvp in slovnik)
                        {
                            command.Parameters.AddWithValue((string)kvp.Key, kvp.Value);
                        }
                    }
                    command.ExecuteNonQuery();

                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public static List<object> Query(String sql)
        {
            List<object> pole1 = new List<object>();

            try
            {
                using (SqlCommand command = new SqlCommand(sql, Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                pole1.Add(reader.GetValue(i));
                            }
                        }
                    }
                }

                return pole1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
