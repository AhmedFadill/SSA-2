using System.Collections.Generic;
using System.Data;

namespace SSA_2
{
    internal static class FunctionsDataBase
    {
        internal static DataTable view_table(string table, string condition = "true", string column = "*")
        {
            string query = $"SELECT {column} FROM {table} WHERE {condition}";
            return MajorDataBase.get_data(query);
        }
        internal static string get_single_data(string table, string condition, string column , bool isNumeric=false)
        {
            string query = $"SELECT {column} FROM {table} WHERE {condition}";
            return MajorDataBase.get_String(query,isNumeric);
        }
        internal static List<string> get_collection_data(string table, string column, string condition = "true")
        {
            string query = $"SELECT {column} FROM {table} WHERE {condition}";
            return MajorDataBase.get_Strings(query);
        }
        internal static bool add_element(string table, string column, string values)
        {
            string query = $"INSERT INTO {table}({column}) VALUES ({values}) ";
            return MajorDataBase.execute_query(query);
        }
        internal static bool updata_element(string table, string values, string condition)
        {
            string query = $"UPDATE {table} SET {values} WHERE {condition} ";
            return MajorDataBase.execute_query(query);
        }
        internal static bool delete_element(string table, string condition)
        {
            string query = $"DELETE FROM {table} WHERE {condition} ";
            return MajorDataBase.execute_query(query);
        }
        internal static List<KeyValuePair<string, string>> view(string table, string condition, string column)
        {
            List<KeyValuePair<string, string>> t = new List<KeyValuePair<string, string>>();
            foreach (DataRow row in MajorDataBase.get_data($"SELECT DISTINCT {column} FROM {table} WHERE {condition} ").Rows)
            {

                t.Add(new KeyValuePair<string, string>(row[0].ToString(), row[1].ToString()));
            }
            return t;
        }
        internal static List<string> query_date(string group, string lecture)
        {
            return MajorDataBase.get_Strings($"select distinct date from info where gro_id={group} and lec_id={lecture}");
        }
        internal static string warning(string rank)
        {
            return MajorDataBase.get_String($"select name from warning where rank <= {rank} order by rank desc limit 1");
        }
    }
}