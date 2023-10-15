using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace _2._6
{
    public partial class Form2 : Form
    {
        string strCon = @"Data Source=redRUM\REDRUM;Initial Catalog=bai2_6;Integrated Security=True";
        SqlConnection sqlCon = null;
        public Form2()
        {
            InitializeComponent();
         
            if (sqlCon == null)
            {
                sqlCon = new SqlConnection(strCon);
            }
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();

            }
        }
       
       
        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text ==""|| textBox2.Text=="" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" )
            {
                MessageBox.Show("Vui long nhap day du thong tin"); 
            }
            else {
                SqlCommand sqlcmd = new SqlCommand();

                sqlcmd.CommandType = CommandType.Text;
                string MaSV = textBox1.Text.Trim();
               

                // Kiểm tra xem giá trị đã tồn tại chưa
                string queryCheckExistence = $"SELECT COUNT(*) FROM SINHVIEN WHERE MaSV = '{MaSV}'";
                SqlCommand checkExistenceCmd = new SqlCommand(queryCheckExistence, sqlCon);
                int count = (int)checkExistenceCmd.ExecuteScalar();

                if (count == 0)
                {
                    string HoSV = textBox2.Text.Trim();
                    string TenLot = textBox4.Text.Trim();
                    string TenSV = textBox3.Text.Trim();
                    string GioiTinh = comboBox1.Text;
                    string MaLop = textBox5.Text.Trim();
                    DateTime ngSinh = dateTimePicker1.Value;
                    sqlcmd.CommandText = $"insert into SINHVIEN(MASV,HoSV,TenLot,TenSV,NgSinh,GioiTinh,MaLop) values('{MaSV}',N'{HoSV}',N'{TenLot}',N'{TenSV}','{ngSinh}','{GioiTinh}','{MaLop}')";
                    sqlcmd.Connection = sqlCon;

                    int kq = sqlcmd.ExecuteNonQuery();
                    if (kq > 0)
                    {

                        MessageBox.Show("Them du lieu thanh cong");
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";

                    }
                    else
                    {
                        MessageBox.Show("Them du lieu khong thanh cong");
                    }
                }
                else
                {
                    
                    MessageBox.Show("Giá trị MaSV đã tồn tại trong bảng SINHVIEN.");
                }
                
                
            }

        }
    }
}
