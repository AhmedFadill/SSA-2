using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using ZXing;

namespace SSA_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            login.id = "1";
        }
        //Variables
        static public SqlConnection sqlcon = DB_Functions.Connection();
        private FilterInfoCollection filterInfoCollection;
        private VideoCaptureDevice device;
        string test, infoid;
        bool isedit;



        //Method
        void Load_data_specific()
        {
            DataTable data;
            data = DB_Functions.Load_data("SELECT [studentID] ,[Name],[cardNum],[notS],[PA],[notA] FROM SA  where [teacherID] = " + login.id
                + "and [Date]='" + kryptonDateTimePicker1.Value + "'  " + "and infID = " + login.id);
            if (data.Rows.Count > 0)
            {
                isedit = true;
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    data.Rows[i]["PA"] = data.Rows[i][4].ToString() == "حاضر";
                    data.Rows[i]["notA"] = data.Rows[i][5];
                }
            }
            else
            {
                isedit = false;
                data = DB_Functions.Load_data("select [id]  as studentID,[Name],[cardNum],[Note] as 'notS' from [students] where  [studyInformationID] = " + infoid);
                if (data != null)
                {
                    for (int i = 1; i < data.Columns.Count; i++)
                        data.Columns[i].ReadOnly = true;
                    data.Columns.Add("PA", typeof(bool));
                    data.Columns.Add("notA", typeof(string));
                }
            }
            table_mainscreen.DataSource = data;
            table_mainscreen.Columns["studentID"].Visible = false;
            table_mainscreen.Columns["Name"].Width = 400;
            table_mainscreen.Columns["Name"].HeaderText = "اسم الطالب";
            table_mainscreen.Columns["cardNum"].HeaderText = "رقم البطاقة";
            table_mainscreen.Columns["notS"].HeaderText = "الملومات الاضافية";
            if (table_mainscreen.Columns.Contains("PA") == true)
            {
                table_mainscreen.Columns["PA"].HeaderText = "الغياب";
                table_mainscreen.Columns["notA"].HeaderText = "ملاحظة";
                table_mainscreen.Columns["PA"].ReadOnly = false;
            }
            for (int i = 2; i < table_mainscreen.ColumnCount - 1; i++)
            {
                table_mainscreen.Columns[i].Width = 200;
            }
            

        }
        private void start()
        {
            device = new VideoCaptureDevice(filterInfoCollection[cbocamera.SelectedIndex].MonikerString);
            device.NewFrame += VideoCaptureDevice_NewFrame;
            device.Start();
        }
        private string Check(string card_number)
        {
            for (int i = 0; i < table_mainscreen.Rows.Count; i++)
            {
                if (table_mainscreen.Rows[i].Cells[2].Value.ToString() == card_number)
                {
                    table_mainscreen.Rows[i].Cells[4].Value = CheckState.Checked;
                    Console.Beep(500, 750);
                    return table_mainscreen.Rows[i].Cells[1].Value.ToString();
                }
            }
            Console.Beep(1000, 1000);
            return "البطاقة غير معرفة";
        }
        private void VideoCaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {

            Bitmap bmp = (Bitmap)eventArgs.Frame.Clone();
            BarcodeReader reader = new BarcodeReader();
            var result = reader.Decode(bmp);
            if (result != null)
            {
                if (test != result.Text)
                {
                    name_student.Invoke(new MethodInvoker(delegate ()
                    {
                        name_student.Text = Check(result.Text);
                        test = result.Text;
                    }));

                }
            }
            camera_barcode.Image = bmp;
        }


        

        void set(bool Stage = false, bool Type = false, bool Division = false, bool Groups = false, bool Subjects = false)
        {

            if (Stage) DB_Functions.SetComboBox(ref kryptonComboBoxStage, DB_Functions.Load_data("select [Sta] from [IS] where [teaID]=" + login.id + " group by [Sta];"));
            if (Type) DB_Functions.SetComboBox(ref kryptonComboBoxType, DB_Functions.Load_data("select [Typ] from [IS] where [teaID]=" + login.id + " and [Sta]='" + kryptonComboBoxStage.Text + "' group by [Typ];"));
            if (Division) DB_Functions.SetComboBox(ref kryptonComboBoxDiv, DB_Functions.Load_data("select [Div] from [IS] where [teaID]= " + login.id + " and [Sta]='" + kryptonComboBoxStage.Text + "'" + " and [Typ]='" + kryptonComboBoxType.Text + "'" + "  group by [Div];"));
            if (Groups) DB_Functions.SetComboBox(ref kryptonComboBoxGro, DB_Functions.Load_data("select [Gro] from [IS] where [teaID]= " + login.id + " and [Sta]='" + kryptonComboBoxStage.Text + "'" + " and [Typ]='" + kryptonComboBoxType.Text + "'" + " and [Div]='" + kryptonComboBoxDiv.Text + "'" + "  group by [Gro];"));
            if (Subjects) DB_Functions.SetComboBox(ref kryptonComboBoxLes, DB_Functions.Load_data("select distinct [lessonName] from [IS] where [teaID]=" + login.id + " and [Sta]='" + kryptonComboBoxStage.Text + "'" + " and [Typ]='" + kryptonComboBoxType.Text + "'" + " and [Div]='" + kryptonComboBoxDiv.Text + "' and [Gro]='" + kryptonComboBoxGro.Text + "'"));
        }


        //Event
        private void SideBarControll(int num)
        {
            panelMainScreem.BackColor = num == 1 ? Color.FromArgb(235, 239, 255) : Color.Transparent;
            panelShow.BackColor = num == 2 ? Color.FromArgb(235, 239, 255) : Color.Transparent;
            panelListStudent.BackColor = num == 3 ? Color.FromArgb(235, 239, 255) : Color.Transparent;
            panelShowReport.BackColor = num == 4 ? Color.FromArgb(235, 239, 255) : Color.Transparent;

            labelMainScreen.ForeColor = num == 1 ? Color.FromArgb(100, 119, 219) : Color.Black;
            labelShow.ForeColor = num == 2 ? Color.FromArgb(100, 119, 219) : Color.Black;
            labelListStudent.ForeColor = num == 3 ? Color.FromArgb(100, 119, 219) : Color.Black;
            labelShowReport.ForeColor = num == 4 ? Color.FromArgb(100, 119, 219) : Color.Black;

            table_mainscreen.Visible = num==1? true: false;
            filteer_mainscreen.Visible= num==1? true: false;

            show1.Visible = num==2? true: false;

            control1.Visible = num==3? true: false;

            reports1.Visible = num==4? true: false;
        }

        private void panelMainScreem_Click(object sender, EventArgs e)
        {
            SideBarControll(1);
        }

        private void panelShow_Click(object sender, EventArgs e)
        {
            SideBarControll(2);
        }

        private void panelListStudent_Click(object sender, EventArgs e)
        {
            SideBarControll(3);
        }

        private void panelShowReport_Click(object sender, EventArgs e)
        {
            SideBarControll(4);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void kryptonDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            camera_off.Visible = false;
            camera_on.Visible = true;
            panel7.Visible = true;
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            cbocamera.Items.Clear();
            foreach (FilterInfo item in filterInfoCollection)
            {
                cbocamera.Items.Add(item.Name);
            }
            cbocamera.SelectedIndex = 0;
            start();
        }

        private void camera_on_Click(object sender, EventArgs e)
        {
            camera_off.Visible = true;
            camera_on.Visible = false;
            panel7.Visible = false;
        }

        private void kryptonPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            set(Stage:true);
        }

        private void kryptonComboBoxStage_TextChanged(object sender, EventArgs e)
        {
            set(Type:true);
        }

        private void kryptonComboBoxType_TextChanged(object sender, EventArgs e)
        {
            set(Division:true);
        }

        private void kryptonComboBoxDiv_TextChanged(object sender, EventArgs e)
        {
            set(Groups:true);
        }

        private void kryptonComboBoxGro_TextChanged(object sender, EventArgs e)
        {
            set(Subjects:true);
            DataTable data = DB_Functions.Load_data("SELECT id FROM [info] where Sta='" + kryptonComboBoxStage.Text + "'" + " and [Typ]='" + kryptonComboBoxType.Text + "'" + " and [Div]='" + kryptonComboBoxDiv.Text + "'" + " and [Gro]='" + kryptonComboBoxGro.Text + "'");
            if (data.Rows.Count > 0) infoid = data.Rows[0][0].ToString();
            Load_data_specific();
        }

        private void kryptonComboBoxLes_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
