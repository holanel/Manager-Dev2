using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vyvojovy_pomocnik.Classes;

namespace Vyvojovy_pomocnik
{
    #region Pocet_radku
    public class Counter
    {
        protected readonly static string Server_DB1 = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"|DataDirectory|Manager_dev.mdf\";Integrated Security=True";

        protected readonly static string Server_DB = "(LocalDB)\\MSSQLLocalDB";

        protected readonly static string Server_Name = "Manager_Dev";

        protected readonly static string Server_User = "sysdba";

        protected readonly static string Server_Password = "password";

        public static int CountRow(int id_table)
        {
            int pocet = 0;

            try
            {
                String connectionString = Server_DB1;
                //String connectionString = "server=" + Server_DB + ";database=" + Server_Name + ";user=" + Server_User + ";password=" + Server_Password;
                using (SqlConnection connection2 = new SqlConnection(connectionString))
                {
                    connection2.Open();
                    String sql2 = string.Empty;

                    if (id_table == 1)
                    {
                        sql2 = "SELECT COUNT(*) AS pocet FROM Projekty WHERE kos='FALSE'";
                    }
                    else if (id_table == 2)
                    {
                        sql2 = "SELECT COUNT(*) AS pocet FROM Ukoly WHERE kos='FALSE'";
                    }
                    else if (id_table == 3)
                    {
                        sql2 = "SELECT COUNT(*) AS pocet FROM Projekty WHERE kos='TRUE'";
                    }
                    else if (id_table == 4)
                    {
                        sql2 = "SELECT COUNT(*) AS pocet FROM Ukoly WHERE kos='TRUE'";
                    }
                    else if (id_table == 5)
                    {
                        sql2 = "SELECT COUNT(*) AS pocet FROM Problemy WHERE kos='FALSE'";
                    }
                    else if (id_table == 6)
                    {
                        sql2 = "SELECT COUNT(*) AS pocet FROM Bugy WHERE kos='FALSE'";
                    }
                    else if (id_table == 7)
                    {
                        sql2 = "SELECT COUNT(*) AS pocet FROM Problemy WHERE kos='TRUE'";
                    }
                    else if (id_table == 8)
                    {
                        sql2 = "SELECT COUNT(*) AS pocet FROM Bugy WHERE kos='TRUE'";
                    }
                    else
                    {
                        return 0;
                    }

                    using (SqlCommand command2 = new SqlCommand(sql2, connection2))
                    {
                        using (SqlDataReader reader2 = command2.ExecuteReader())
                        {
                            while (reader2.Read())
                            {
                                pocet = reader2.GetInt32(0);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                return pocet;
            }


            return pocet;
        }
                            

        #endregion Pocet_radku
    }
}
