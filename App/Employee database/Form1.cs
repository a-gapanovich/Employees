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
using System.Globalization;

namespace Employee_database
{
    public partial class Employees : Form
    {
        public Employees()
        {
            InitializeComponent();
        }
        DataBase d = new DataBase();
     
        public void Filling_dataGridView()
        {
            if (d.dt.Rows != null)
            {
                d.dt.Clear();
            }
            d.cmd.CommandText = "select * from MainList";
            d.cmd.Connection = d.con;
            d.dr = d.cmd.ExecuteReader();
            d.dt.Load(d.dr);
            dataGridView1.DataSource = d.dt;
            d.dr.Close();
        }

        public void Sort()
        {
            if (d.dt.Rows != null)
            {
                d.dt.Clear();
            }
            d.cmd.CommandText = "select * from MainList where Position = '" + tbFilt.Text + "'";
            d.cmd.Connection = d.con;
            d.dr = d.cmd.ExecuteReader();
            d.dt.Load(d.dr);
            dataGridView1.DataSource = d.dt;
            d.dr.Close();
        }
        public int counter()
        {
            int cpt;
            d.cmd.CommandText = "select count(Name) from MainList where Name = '" + tbName.Text + "' and Surname = '" + tbSurname.Text + "' and Was_born = '" + tbYear.Text + "' and Position = '" + tbPosition.Text + "'";
            d.cmd.Connection = d.con;
            cpt = Convert.ToInt32(d.cmd.ExecuteScalar());
            return cpt;
        }
        public bool Add_Employee()
        {
            if (counter() == 0)
            {
               
                d.cmd.CommandText = "insert into MainList values ('" + tbName.Text + "','" + tbSurname.Text + "','" + tbPosition.Text + "','" + tbYear.Text + "','" + tbSalary.Text.Replace(',', '.') + "')";
                d.cmd.Connection = d.con;
                d.cmd.ExecuteNonQuery();
                return true;
                
            }
            return false;
        }

        public bool Del_Employee()
        {
            if (counter() != 0)
            {
                d.cmd.CommandText = "delete from MainList where Name = '" + tbName.Text + "' and Surname = '" + tbSurname.Text + "' and Was_born = '" + tbYear.Text + "' and Position = '" + tbPosition.Text + "'";
                d.cmd.Connection = d.con;
                d.cmd.ExecuteNonQuery();
                return true;
            }
            return false;
        }

        public void VIDER(Control f)
        {
            foreach (Control ct in f.Controls)
            {
                if (ct.GetType() == typeof(TextBox))
                {
                    ct.Text = "";
                }
                if (ct.Controls.Count != 0)
                {
                    VIDER(ct);
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            d.CONNECTER();
            Filling_dataGridView();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to exit?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                d.DECONNECTER();
                this.Close();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Clear entered data?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                VIDER(this);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (tbName.Text == "" || tbSurname.Text == "" || tbPosition.Text == "" || tbYear.Text == "" || tbSalary.Text == "")
            {
                MessageBox.Show("Please fill in all fields!");
                return;
            }
            if (Add_Employee() == true)
            {
                MessageBox.Show("Employee added successfully!");
                Filling_dataGridView();
            }
            else
            {
                MessageBox.Show("Error! Such an employee is already working!");
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (tbName.Text == "" || tbSurname.Text == "" || tbPosition.Text == "" || tbYear.Text == "" )
            {
                MessageBox.Show("Please fill in all fields, except for <Salary>!");
                return;
            }
            if (Del_Employee() == true)
            {
                MessageBox.Show("Employee deleted successfully!");
                Filling_dataGridView();
            }
            else
            {
                MessageBox.Show("Error! Such an employee does not exist!");
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void tbYear_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Filling_dataGridView();
        }

        private void btnFilt_Click(object sender, EventArgs e)
        {
            Sort();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
        }
    }
}
