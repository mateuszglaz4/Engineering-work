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
using Microsoft.Data.SqlClient;

namespace JazdyProbne
{
    public partial class rental : Form
    {
        public rental()
        {
            InitializeComponent();
            carload();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-JLI1ULJ; Initial Catalog= carrental; User ID=sa; Password=admin123;");
        SqlCommand cmd;
        SqlDataReader dr;
        string proid;
        string sql;
        bool Mode = true;
        string id;

        public void carload()
        {
            cmd = new SqlCommand("select * from carreg", con);
            con.Open();
            dr = cmd.ExecuteReader();

            while(dr.Read())
            {
                txtcarid.Items.Add(dr["regno"].ToString());
            }
            con.Close();
        }



        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void rental_Load(object sender, EventArgs e)
        {

        }

        private void txtcarid_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd = new SqlCommand("select * from carreg where regno ='" + txtcarid.Text + "'  ", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if(dr.Read())
            {
                string aval;
                aval =(dr["available"].ToString());

                label9.Text = aval;

                if(aval == "No")
                {

                    txtcustid.Enabled = false;
                    txtfee.Enabled = false;
                    txtdate.Enabled = false;
                    txtdue.Enabled = false;
                }
            }
            else
            {
                label9.Text = "Car not available";
                
            }
            con.Close();
        }

        private void txtcustid_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyValue == 13)
            {
                cmd = new SqlCommand("select * from customer where custid ='" + txtcustid.Text + "'  ", con);
                con.Open();
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    txtcustname.Text = dr["custname"].ToString();
                }
                else
                    MessageBox.Show("Customer ID not found");
                }
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string carid = txtcarid.SelectedItem.ToString();
            string custid = txtcustid.Text;
            string custname = txtcustname.Text;
            string fee = txtfee.Text;



            
                sql = "insert into carreg(regno,make,model,available)values(@regno,@make,@model,@available)";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@regno", regno);
                cmd.Parameters.AddWithValue("@make", make);
                cmd.Parameters.AddWithValue("@model", model);
                cmd.Parameters.AddWithValue("@available", aval);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Added");
            }
    }
}
