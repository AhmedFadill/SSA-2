using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace SSA_2
{
    static internal class MajorDataBase
    {
        private static string connection_stirng = "Data Source=DB.db";
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
            catch (Exception ex)
            {
                MessageBox.Show($"this error is {ex}", "wow", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        
        

    }
}
