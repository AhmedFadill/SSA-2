using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SQLite;

namespace SSA_2
{
    static internal class MajorDataBase
    {
        private static string DB_name = "DB.db";
        private static string connection_stirng = $"Data Source={DB_name}";
        internal static SQLiteConnection Connection()
        {
            try {
                return new SQLiteConnection("Data Source=DB.db");
            } catch {
                MessageBox.Show("Error to connect to DB");
                return null;
            }
        }
        internal static DataTable get_data(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                using(var connection = new SQLiteConnection("Data Source=DB.db"))
                {
                    connection.Open();
                    using(var command = new SQLiteCommand(query,connection)) {
                        using(var da = new SQLiteDataAdapter(command))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch(SQLiteException e)
            {
                MessageBox.Show(e.Message, "get data error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return dt;
            }
            return dt;
        }
        internal static bool execute_query(string query)
        {
            try {
                using (SQLiteConnection connection = new SQLiteConnection(connection_stirng))
                {
                    connection.Open();
                    using(var command= new SQLiteCommand(query,connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"this error is {ex.Message}", "Worng in Query", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        
        

    }
}
