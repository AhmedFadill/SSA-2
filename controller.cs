using ComponentFactory.Krypton.Toolkit;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Collections.Generic;
using System.Data;

namespace SSA_2
{
    internal static class controller
    {
        internal static void setStage(ref KryptonComboBox Stage)
        {
            Stage.DisplayMember = "Key";
            Stage.ValueMember = "Value";
            Stage.DataSource = FunctionsDataBase.view("CD", "true", "stage,stage_id");
        }
        internal static void setType(ref KryptonComboBox Type, ref KryptonComboBox Stage)
        {
            Type.DisplayMember = "Key";
            Type.ValueMember = "Value";
            Type.DataSource = FunctionsDataBase.view("CD", $"stage_id={Stage.SelectedValue}", "type,stage_id");
        }
        internal static void setLecture(ref KryptonComboBox Lecture, ref KryptonComboBox Type)
        {
            Lecture.DisplayMember = "Key";
            Lecture.ValueMember = "Value";
            Lecture.DataSource = FunctionsDataBase.view("CD", $"stage_id={Type.SelectedValue}", "lecture,lec_id");
        }
        internal static void setDivision(ref KryptonComboBox Division, ref KryptonComboBox Type ) {
            Division.DisplayMember = "Key";
            Division.ValueMember = "Value";
            Division.DataSource = FunctionsDataBase.view("CD", $"stage_id={Type.SelectedValue}", "division,div_id");
        }
        internal static void setGroup(ref KryptonComboBox Group, ref KryptonComboBox Division)
        {
            Group.DisplayMember = "Key";
            Group.ValueMember = "Value";
            Group.DataSource = FunctionsDataBase.view("CD", $"div_id={Division.SelectedValue}", "gro,group_id");
        }
        internal static void setDate(ref KryptonComboBox Date, ref KryptonComboBox Group, ref KryptonComboBox Lecture)
        {
            var data = FunctionsDataBase.query_date(Group.SelectedValue.ToString(), Lecture.SelectedValue.ToString());
            if(data.Count == 0) data = new List<string>() { "لا يوجد" };
            Date.DataSource= data ;
        }


        internal static bool addStudent(string name,string group_id,string card_num,string note)
        {
            name = name.Replace("'", "");
            group_id=group_id.Replace("'", "");
            card_num = card_num.Replace("'", "");
            note = note.Replace("'", "");
            return FunctionsDataBase.add_element("students", "name,group_id,card_num,note", $"'{name}','{group_id}','{card_num}','{note}'");
        }
        internal static string addStudentExcel(DataTable info,string group_id)
        {
            if (!info.Columns.Contains("الاسم") || !info.Columns.Contains("الرقم") || !info.Columns.Contains("الملاحظة")) 
            {
                return "NC3W";
            }

            string nameStudentWorngAdd = string.Empty;
            group_id = group_id.Replace("'", "");
            foreach (DataRow row in info.Rows)
            {
                string name = row["الاسم"].ToString().Replace("'", "");
                string number = row["الرقم"].ToString().Replace("'", "");
                string note = row["الملاحظة"].ToString().Replace("'", "");
                if (!FunctionsDataBase.add_element("students", "name,group_id,card_num,note", $"'{name}','{group_id}','{number}','{note}'")) { nameStudentWorngAdd += " " + name; }
            }
            return nameStudentWorngAdd;
        }
        internal static bool editStudent(string name,string group_id,string card_num,string note,string idStudent)
        {
            name = name.Replace("'", "");
            group_id = group_id.Replace("'", "");
            card_num = card_num.Replace("'", "");
            note = note.Replace("'", "");
            idStudent = idStudent.Replace("'", "");
            return FunctionsDataBase.updata_element("students", $"name = '{name}' , group_id = {group_id} , card_num = {card_num} , note = '{note}'", $"id={idStudent}");
        }
        internal static bool deleteStudent(string id)
        {
            id = id.Replace("'", "");
            return FunctionsDataBase.delete_element("students", $"id={id}");
        }


        internal static bool addAttendance(string idStudents, string idLecture, string date, string note,string isPresnet)
        {
            idStudents = idStudents.Replace("'", "");
            idLecture = idLecture.Replace("'", "");
            date = date.Replace("'", "");
            note = note.Replace("'", "");
            isPresnet = isPresnet.Replace("'", "");
            return FunctionsDataBase.add_element("attendance", "lecture_id,student_id,date,note,is_present", $"'{idStudents}','{idLecture}','{date}','{note}','{isPresnet}'");
        }
        internal static bool editAttendance(string idStudents, string idLecture, string date, string note, string isPresnet, string idAttend)
        {
            idAttend = idAttend.Replace("'", "");
            idLecture = idLecture.Replace("'", "");
            date = date.Replace("'", "");
            note = note.Replace("'", "");
            isPresnet = isPresnet.Replace("'", "");
            idAttend = idAttend.Replace("'", ""); 
            return FunctionsDataBase.updata_element("attendance", $"lecture_id = '{idLecture}' , student_id = {idStudents} , date = {date} , note = '{note}'", $"is_present={isPresnet}");
        }
        internal static bool deleteAttendance(string id)
        {
            id = id.Replace("'", "");
            return FunctionsDataBase.delete_element("attendance", $"id={id}");
        }


        internal static bool addStage(string name, string type ,bool allType =false)
        {
            name = name.Replace("'", "");
            type = type.Replace("'", "");
            if (allType) return FunctionsDataBase.add_element("stage", "name,type", $"{name},0") && FunctionsDataBase.add_element("stage", "name,type", $"{name},1"); 
            return FunctionsDataBase.add_element("stage", "name,type", $"{name},{type}");
        }
        internal static bool editStage(string name, string type, string id)
        {
            name = name.Replace("'", "");
            type = type.Replace("'", "");
            id = id.Replace("'", "");            
            return FunctionsDataBase.updata_element("stage", $"name = '{name}' , type = {type} ", $"id={id}");
        }
        internal static bool deleteStage(string id)
        {
            id = id.Replace("'", "");
            return FunctionsDataBase.delete_element("stage", $"id={id}");
        }


        internal static bool addLecture(string name, string idStage)
        {
            name = name.Replace("'", "");
            idStage = idStage.Replace("'", "");            
            return FunctionsDataBase.add_element("lectures", "name,stage_id", $"{name},{idStage}");
        }
        internal static bool editLecture(string name, string stage_id, string id)
        {
            name = name.Replace("'", "");
            stage_id = stage_id.Replace("'", "");
            id = id.Replace("'", "");
            return FunctionsDataBase.updata_element("lectures", $"name = '{name}' , stage_id = {stage_id} ", $"id={id}");
        }
        internal static bool deleteLecture(string id)
        {
            id = id.Replace("'", "");
            return FunctionsDataBase.delete_element("lectures", $"id={id}");
        }


        internal static bool addDivision(string name, string idStage)
        {
            name = name.Replace("'", "");
            idStage = idStage.Replace("'", "");
            return FunctionsDataBase.add_element("divisions", "name,stage_id", $"{name},{idStage}");
        }
        internal static bool editDivision(string name, string idStage, string id)
        {
            name = name.Replace("'", "");
            idStage = idStage.Replace("'", "");
            id = id.Replace("'", "");
            return FunctionsDataBase.updata_element("divisions", $"name = '{name}' , stage_id = {idStage} ", $"id={id}");
        }
        internal static bool deleteDivision(string id)
        {
            id = id.Replace("'", "");
            return FunctionsDataBase.delete_element("divisions", $"id={id}");
        }


        internal static bool addGroup(string name, string idDivision)
        {
            name = name.Replace("'", "");
            idDivision = idDivision.Replace("'", "");
            return FunctionsDataBase.add_element("groups", "name,div_id", $"{name},{idDivision}");
        }
        internal static bool editGroup(string name, string idDivision, string id)
        {
            name = name.Replace("'", "");
            idDivision = idDivision.Replace("'", "");
            id = id.Replace("'", "");
            return FunctionsDataBase.updata_element("groups", $"name = '{name}' , div_id = {idDivision} ", $"id={id}");
        }
        internal static bool deleteGroup(string id)
        {
            id = id.Replace("'", "");
            return FunctionsDataBase.delete_element("groups", $"id={id}");
        }


        internal static bool addWarn(string name, string rank)
        {
            name = name.Replace("'", "");
            rank = rank.Replace("'", "");
            return FunctionsDataBase.add_element("warning", "name,rank", $"'{name}',{rank}");
        }
        internal static bool editWarn(string name, string rank, string id)
        {
            name = name.Replace("'", "");
            rank = rank.Replace("'", "");
            id = id.Replace("'", "");
            return FunctionsDataBase.updata_element("warning", $"name = '{name}' , rank = {rank} ", $"id={id}");
        }
        internal static bool deleteWarn(string id)
        {
            id = id.Replace("'", "");
            return FunctionsDataBase.delete_element("warning", $"id={id}");
        }




        internal static DataTable report(string idGroup,string idLecture )
        {
            DataTable report = FunctionsDataBase.view_table("students",$"group_id={idGroup}","id,name");
            DataColumn totle = new DataColumn() { 
                DataType = typeof(string),
                AllowDBNull=false,
                ColumnName="Totle"
             }, warn = new DataColumn()
             {
                 DataType = typeof(string),
                 AllowDBNull = true,
                 ColumnName = "Warn"
             };
            report.Columns.Add(totle);
            report.Columns.Add(warn);

            foreach (DataRow row in report.Rows)
            {
                row["Totle"] = FunctionsDataBase.get_single_data("attendance", $"student_id={row["id"]} and lecture_id ={idLecture} and is_present = 0","COUNT(*)",true);
                string att =  FunctionsDataBase.warning(row["Totle"].ToString());
                row["Warn"] = att == "No data" ? "لا يوجد" : att;
            }
            

            foreach (string date in FunctionsDataBase.get_collection_data("info", "distinct date",$"gro_id={idGroup} and lec_id={idLecture}"))
            {
                DataColumn column = new DataColumn()
                {
                    DataType = typeof(string),
                    AllowDBNull = true,
                    ColumnName = date
                };
                report.Columns.Add(column);
            }
            foreach(DataRow row in report.Rows)
            {
                foreach (DataColumn column in report.Columns)
                {
                    if (column.ColumnName == "id" || column.ColumnName == "name" || column.ColumnName == "Totle" || column.ColumnName == "Warn") continue;
                    string att = FunctionsDataBase.get_single_data("info", $"stu_id={row["id"]} and lec_id ={idLecture} and date = '{column.ColumnName}'", "P");
                    row[column.ColumnName] =  att == "No data" ?"لا يوجد":att;

                }
            }
            
            return report;
        }




    }
}
