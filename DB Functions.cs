using ComponentFactory.Krypton.Toolkit;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace SSA_2
{
    static internal class DB_Functions
    {
        static SqlConnection sqlcon = Connection();

        static public SqlConnection Connection()
        {
            try
            {
                string[] info_con = File.ReadAllLines(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Connection.txt"));
                sqlcon = new SqlConnection(@"Data Source=" + info_con[0] + ";Initial Catalog=" + info_con[1] + ";Integrated Security=True");
                try
                {
                    sqlcon.Open();
                    sqlcon.Close();
                }
                catch
                {
                    MessageBox.Show("يرجى التاكد من اسم السيرفر و قاعدة البيانات", "ملف الاتصال غير موجود", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
            }
            catch
            {
                MessageBox.Show("يرجى التاكد من ان ملف الاتصال موجود في مكانه الصحيح", "ملف الاتصال غير موجود", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlcon;
        }            
        static public DataTable Load_data(string query)
        {
            try
            {
                DataTable dt = new DataTable();
                sqlcon.Open();
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                sqlcon.Close();
                return dt;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                MessageBox.Show("يوجد خطا في تحميل بيانات" , "فشل في تحميل البيانات",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return null;
            }

        }
        static public void Execute(string query)
        {
            try
            {
            sqlcon.Open();
            SqlCommand command = new SqlCommand(query, sqlcon);
            command.ExecuteNonQuery();
            sqlcon.Close();
            } catch(Exception e) {
                MessageBox.Show(e.Message);
                MessageBox.Show("يوجد خطا في اضافة على البيانات" , "فشل في اضافة البيانات", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        static public bool Check(string query)
        {
            try
            {
                sqlcon.Open();
                SqlCommand command = new SqlCommand(query, sqlcon);
                SqlDataReader reader = command.ExecuteReader();
                bool c=reader.Read();
                sqlcon.Close();
                return c;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                MessageBox.Show("يوجد خطا في اضافة على البيانات" , "فشل في اضافة البيانات", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        static internal void SetComboBox(ref KryptonComboBox t, DataTable data)
        {
            t.Items.Clear();
            if (data.Rows.Count == 0) { t.Text = "لاتوجد بيانات"; return; }
            foreach (DataRow row in data.Rows)
            {
                t.Items.Add(row[0].ToString());
            }
            if (t.Items.Count > 0) t.Text = t.Items[t.Items.Count - 1].ToString();

        }





    }
}
