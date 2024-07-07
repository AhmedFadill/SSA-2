using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSA_2
{
    internal static class FunctionsDataBase 
    {
        internal static DataTable view_table(string table,string condition="true" , string column = "*")
        {
            string query = $"SELECT {column} FROM {table} WHERE {condition}";
            return MajorDataBase.get_data(query);
        }
        internal static bool add_element(string table,string column , string values)
        {
            string query = $"INSERT INTO {table}({column}) VALUES ({values}) ";
            return MajorDataBase.execute_query(query);
        }
        internal static bool updata_element(string table, string values,string condition)
        {
            string query = $"UPDATE {table} SET {values} WHERE {condition} ";
            return MajorDataBase.execute_query(query);
        }
    }
}