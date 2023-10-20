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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace LibraryManagementSystem
{
    public partial class IssueBooks : Form
    {
        public IssueBooks()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void IssueBooks_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection("data source=DESKTOP-H1RFGLB\\SQLEXPRESS; database=LMS; integrated security=True"))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("select bName from NewBook", con))
                using (SqlDataReader Sdr = cmd.ExecuteReader())
                {
                    while (Sdr.Read())
                    {
                        comboBox1.Items.Add(Sdr.GetString(0));
                    }
                } // Close the reader here (it will also close when exiting the using block)
            } // Close the connection here

        }

        int count;
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtEnrollment.Text != "")
            {
                String eid = txtEnrollment.Text;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "data source=DESKTOP-H1RFGLB\\SQLEXPRESS; database=LMS;integrated security=True";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "select * from New_Student where Student_enrollment='" + eid + "'";
                SqlDataAdapter DA = new SqlDataAdapter(cmd); // Initialize the adapter with the command
                DataSet DS = new DataSet();
                DA.Fill(DS);

                cmd.CommandText = "select count(Student_enrollment) from IRBook where Student_enrollment='" + eid + "' and Book_return_date is null";
                SqlDataAdapter DA1 = new SqlDataAdapter(cmd); // Initialize another adapter with the same command
                DataSet DS1 = new DataSet();
                DA1.Fill(DS1);

                count = int.Parse(DS1.Tables[0].Rows[0][0].ToString());

                if (DS.Tables[0].Rows.Count != 0)
                {
                    txtName.Text = DS.Tables[0].Rows[0][0].ToString();
                    txtDepartment.Text = DS.Tables[0].Rows[0][2].ToString();
                    txtSemester.Text = DS.Tables[0].Rows[0][3].ToString();
                    txtContact.Text = DS.Tables[0].Rows[0][4].ToString();
                    txtEmail.Text = DS.Tables[0].Rows[0][5].ToString();
                }
                else
                {
                    txtName.Clear();
                    txtDepartment.Clear();
                    txtSemester.Clear();
                    txtContact.Clear();
                    MessageBox.Show("Invalid Enrollment Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnIssueBook_Click(object sender, EventArgs e)
        {
            
            if (txtName.Text != " ")
            {
               
                if ((comboBox1.SelectedIndex == 0) || (count <= 2))
                {

                    String enroll = txtEnrollment.Text;
                    String sname = txtName.Text;
                    String sdep = txtDepartment.Text;
                    String sem = txtSemester.Text;
                    Int64 contact = Int64.Parse(txtContact.Text);
                    String email = txtEmail.Text;
                    String bookname = comboBox1.Text;
                    String bookIssueDate = dateTimePicker.Text;

                    String eid = txtEnrollment.Text;
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = "data source =DESKTOP-H1RFGLB\\SQLEXPRESS; database=LMS;integrated security=True";
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();
                    cmd.CommandText = "insert into IRBook(Student_enrollment,Student_name,Student_department,Student_semester,Student_contact,Student_email,Book_name,Book_issue_date) values('" + enroll + "','" + sname + "','" + sdep + "','" + sem + "','" + contact + "','" + email + "','" + bookname + "','" + bookIssueDate + "')";
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Book Issued", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else
                {

                    MessageBox.Show("Select Book. OR Maximum number of books has been issued", "No Book selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Enter vaild Enrollment no", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtEnrollmentNumber_TextChanged(object sender, EventArgs e)
        {
            if (txtEnrollment.Text == "")
            {
                txtName.Clear();
                txtDepartment.Clear();
                txtSemester.Clear();
                txtContact.Clear();
                txtEmail.Clear();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtEnrollment.Text = "";

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.Close();
            }
        }
    }
}