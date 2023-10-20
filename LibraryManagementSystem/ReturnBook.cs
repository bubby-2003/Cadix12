using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagementSystem
{
    public partial class ReturnBook : Form
    {
        public ReturnBook()
        {
            InitializeComponent();
        }

        private void btnSearchStudent_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "data source = DESKTOP-H1RFGLB\\SQLEXPRESS; database = LMS;integrated security=True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "select * from IRBook where Student_enrollment='"+txtEnterEnroll.Text+"' and book_return_date IS NULL";

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count != 0)
            {

                dataGridView1.DataSource = ds.Tables[0];
            }
            else
            {

                MessageBox.Show("Invalid ID or No Book Issued", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
    }

        private void ReturnBook_Load(object sender, EventArgs e)
        {
            panel1.Visible = false;
            txtEnterEnroll.Clear();
        }
        
        
        String bname;
        String bdate;
     
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            panel1.Visible = true;
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
               
                bname = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                bdate = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
            txtBookName.Text = bname;
            txtBookIssueDate.Text = bdate;
        }
        private void btnReturn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "data source = DESKTOP-H1RFGLB\\SQLEXPRESS; database = LMS;integrated security=True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            con.Open();

            cmd.CommandText = "update IRBook set Book_return_date='" + dateTimePicker.Text + "'where Student_enrollment='" + txtEnterEnroll.Text + "'";
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Return Succesful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            con.Close();
            ReturnBook_Load(this, null);
        }

        private void txtEnterEnroll_TextChanged(object sender, EventArgs e)
        {
            if (txtEnterEnroll.Text == "")
            {
                panel1.Visible = false; dataGridView1.DataSource = null;
            }
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtEnterEnroll.Clear();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        
    }
    }
}
