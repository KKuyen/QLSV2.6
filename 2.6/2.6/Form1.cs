using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace _2._6
{
    public partial class Form1 : Form
    {
        string strCon = @"Data Source=redRUM\REDRUM;Initial Catalog=bai2_6;Integrated Security=True";
        SqlConnection sqlCon = null;
        int ktra = 0;
        public Form1()
        {
            InitializeComponent();
            Load();
            button3.Enabled = false;
            button2.Enabled = false;


        }
        public void Load()
        {
            if (sqlCon == null)
            {
                sqlCon = new SqlConnection(strCon);
            }
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();

            }
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.CommandType = CommandType.Text;
            sqlcmd.CommandText = "select * from SINHVIEN";
            sqlcmd.Connection = sqlCon;
            SqlDataReader reader = sqlcmd.ExecuteReader();
            listView1.Items.Clear();
            while (reader.Read())
            {

                string MaSV = reader.GetString(0);
                string TenSV = reader.GetString(1);
                string TenLot = reader.GetString(2);
                string HoSV = reader.GetString(3);
                string NgSinh = reader.GetDateTime(4).ToString();
                string GioiTinh = reader.GetString(5);
                string MaLop = reader.GetString(6);
               
                ListViewItem lvi = new ListViewItem(MaSV);

                lvi.SubItems.Add(HoSV);
                lvi.SubItems.Add(TenLot);
                lvi.SubItems.Add(TenSV);
                lvi.SubItems.Add(NgSinh);
                lvi.SubItems.Add(GioiTinh);

                lvi.SubItems.Add(MaLop);
               
                listView1.Items.Add(lvi);

            }
            reader.Close();
        }    

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
        
            button3.Enabled = true;
            button2.Enabled = true;
            ListViewItem lvi = listView1.SelectedItems[0];
            label10.Text = lvi.SubItems[0].Text;
            textBox2.Text = lvi.SubItems[1].Text;
            textBox3.Text = lvi.SubItems[2].Text;
            textBox4.Text = lvi.SubItems[3].Text;
            comboBox1.Text = lvi.SubItems[5].Text;
            textBox5.Text = lvi.SubItems[6].Text;
         string day=lvi.SubItems[4].Text;
            string[] daysp=day.Split(' ');
            string realday=daysp[0];
            string[] rdsp = realday.Split('/');
           string dt = $"{rdsp[0]}-{rdsp[1]}-{rdsp[2]}";
            DateTime dt2 = DateTime.Parse(dt);
            dateTimePicker1.Value= dt2;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
            Load();
          
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {


           if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("Vui long nhap du thong tin");
            }
            else
            {
                if (sqlCon == null)
                {
                    sqlCon = new SqlConnection(strCon);
                }
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();

                }
                SqlCommand sqlcmd = new SqlCommand();

                sqlcmd.CommandType = CommandType.Text;




                string MaSV= label10.Text;
                    string HoSV = textBox2.Text.Trim();
                    string TenLot = textBox3.Text.Trim();
                    string TenSV = textBox4.Text.Trim();
                    string GioiTinh = comboBox1.Text;
                    string MaLop = textBox5.Text.Trim();
                    DateTime ngSinh = dateTimePicker1.Value;
                    sqlcmd.CommandText = $"update SINHVIEN set HoSV =N'{HoSV}',TenSV=N'{TenSV}',TenLot=N'{TenLot}',GioiTinh='{GioiTinh}',NgSinh='{ngSinh}',MaLop='{MaLop}' where MaSV='{MaSV}'";
                    sqlcmd.Connection = sqlCon;

                    int kq = sqlcmd.ExecuteNonQuery();
                    if (kq > 0)
                    {

                        MessageBox.Show("Sua du lieu thanh cong");
                    Load();
                       

                    }
                    else
                    {
                        MessageBox.Show("Sua du lieu khong thanh cong");
                    }
                }
              
            }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            ktra++;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (sqlCon == null)
            {
                sqlCon = new SqlConnection(strCon);
            }
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();

            }
            SqlCommand sqlcmd = new SqlCommand();

            sqlcmd.CommandType = CommandType.Text;
            string MaSV = label10.Text;
            sqlcmd.CommandText = $"delete from SINHVIEN where MaSV={MaSV}";
            sqlcmd.Connection = sqlCon;
            int kq = sqlcmd.ExecuteNonQuery();
            if (kq > 0)
            {

                MessageBox.Show("Xoa du lieu thanh cong");
                button3.Enabled=false;
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                comboBox1.Text = "";
                label10.Text = "";
               

                Load();


            }
        }
    }
}