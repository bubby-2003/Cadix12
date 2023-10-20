using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagementSystem
{
    public partial class ViewStudentInformation : Form
    {
        public ViewStudentInformation()
        {
            InitializeComponent();
        }

        // Define the bid variable as a string
        string bid;

        Int64 rowid;

        private void ViewStudentInformation_Load(object sender, EventArgs e)
        {
            panel4.Visible = false;

            SqlConnection con = new SqlConnection();
            con.ConnectionString = "data source=DESKTOP-H1RFGLB\\SQLEXPRESS; database=LMS; integrated security=True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "select * from New_Student";
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DataSet DS = new DataSet();
            DA.Fill(DS);

            dataGridView1.DataSource = DS.Tables[0];
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ensure that a valid row is clicked
            {
                // Extract the values from the DataGridView and populate the panel
                string name = dataGridView1.Rows[e.RowIndex].Cells["Student_name"].Value.ToString();
                string enrollment = dataGridView1.Rows[e.RowIndex].Cells["Student_enrollment"].Value.ToString();
                string department = dataGridView1.Rows[e.RowIndex].Cells["Student_department"].Value.ToString();
                string semester = dataGridView1.Rows[e.RowIndex].Cells["Student_semester"].Value.ToString();
                string contact = dataGridView1.Rows[e.RowIndex].Cells["Student_contact"].Value.ToString();
                string email = dataGridView1.Rows[e.RowIndex].Cells["Student_email"].Value.ToString();

                textName.Text = name;
                textEnrollment.Text = enrollment;
                textDepartment.Text = department;
                textSemester.Text = semester;
                textContact.Text = contact;
                textEmail.Text = email;

                // Make the panel visible
                panel4.Visible = true;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string sname = textName.Text;
            string enroll = textEnrollment.Text;
            string dept = textDepartment.Text;
            string sem = textSemester.Text;
            Int64 contact = Int64.Parse(textContact.Text);
            string email = textEmail.Text;

            if (MessageBox.Show("Data will be Updated. Confirm?", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                using (SqlConnection con = new SqlConnection("data source=DESKTOP-H1RFGLB\\SQLEXPRESS; database=LMS; integrated security=True"))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE New_Student SET Student_name = @name, Student_department = @dept, Student_semester = @sem, Student_contact = @contact, Student_email = @email WHERE Student_enrollment = @enrollment", con))
                    {
                        cmd.Parameters.AddWithValue("@name", sname);
                        cmd.Parameters.AddWithValue("@enrollment", enroll);
                        cmd.Parameters.AddWithValue("@dept", dept);
                        cmd.Parameters.AddWithValue("@sem", sem);
                        cmd.Parameters.AddWithValue("@contact", contact);
                        cmd.Parameters.AddWithValue("@email", email);

                        try
                        {
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Data Updated Successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("No records were updated.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            // Hide panel4
            panel4.Visible = false;

            // Clear text boxes
            textName.Text = "";
            textEnrollment.Text = "";
            textDepartment.Text = "";
            textSemester.Text = "";
            textContact.Text = "";
            textEmail.Text = "";

            // Reload data in the DataGridView
            ReloadDataGridViewData();
        }

        private void ReloadDataGridViewData()
        {
            using (SqlConnection con = new SqlConnection("data source=DESKTOP-H1RFGLB\\SQLEXPRESS; database=LMS; integrated security=True"))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM New_Student", con))
                {
                    SqlDataAdapter DA = new SqlDataAdapter(cmd);
                    DataSet DS = new DataSet();
                    DA.Fill(DS);
                    dataGridView1.DataSource = DS.Tables[0];
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textEnrollment.Text))
            {
                MessageBox.Show("No student selected for deletion.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this student's record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Perform the deletion
                DeleteStudentRecord(textEnrollment.Text);

                // Hide panel4 and clear text boxes
                panel4.Visible = false;
                textName.Text = "";
                textEnrollment.Text = "";
                textDepartment.Text = "";
                textSemester.Text = "";
                textContact.Text = "";
                textEmail.Text = "";

                // Refresh the DataGridView data
                ReloadDataGridViewData();
            }
        }

        private void DeleteStudentRecord(string enrollment)
        {
            using (SqlConnection con = new SqlConnection("data source=DESKTOP-H1RFGLB\\SQLEXPRESS; database=LMS; integrated security=True"))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("DELETE FROM New_Student WHERE Student_enrollment = @enrollment", con))
                {
                    cmd.Parameters.AddWithValue("@enrollment", enrollment);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;

            // Clear text boxes
            textName.Text = "";
            textEnrollment.Text = "";
            textDepartment.Text = "";
            textSemester.Text = "";
            textContact.Text = "";
            textEmail.Text = "";
        }
    }
}
