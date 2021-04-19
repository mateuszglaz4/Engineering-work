using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace JazdyProbne
{
    public partial class carreg : Form
    {
        public carreg()
        {
            InitializeComponent();
            Autono();
            load();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-JLI1ULJ; Initial Catalog= carrental; User ID=sa; Password=admin123;");
        SqlCommand cmd;
        SqlDataReader dr;
        string proid;
        string sql;
        bool Mode = true;
        string id;



        public void Autono()
        {
            sql = "select regno from carreg order by regno desc";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();

            if(dr.Read())
            {
                int id = int.Parse(dr[0].ToString()) + 1;
                proid = id.ToString("00000");
            }
            else if(Convert.IsDBNull(dr))
            {
                proid = ("00000");
            }
            else
            {
                proid = ("00000");
            }

            txtregno.Text = proid.ToString();

            con.Close();
        }
       
        public void load()
        {
            sql = "select * from carreg";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();
            dataGridView1.Rows.Clear();

            while(dr.Read())
            {
                dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3]);
            }
            con.Close();
        }


        public void getid(String id)
        {
            sql = "select * from carreg where regno = '" + id + " ' ";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();

            while(dr.Read())
            {
                txtregno.Text = dr[0].ToString();
                txtmake.Text = dr[1].ToString();
                txtmodel.Text = dr[2].ToString();
                txtavl.Text = dr[3].ToString();
            }
            con.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void carreg2_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string regno = txtregno.Text;
            string make = txtmake.Text;
            string model = txtmodel.Text;
            string aval = txtavl.SelectedItem.ToString();

            id = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            if(Mode == true)
            {
                sql = "insert into carreg(regno,make,model,available)values(@regno,@make,@model,@available)";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@regno", regno);
                cmd.Parameters.AddWithValue("@make", make);
                cmd.Parameters.AddWithValue("@model", model);
                cmd.Parameters.AddWithValue("@available", aval);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Added");

                txtmake.Clear();
                txtmodel.Clear();
                
                txtmake.Focus();
                
            }
            else
            {
                sql = "update carreg set make= @make, model=@model, available = @available where regno = @regno";
                con.Open();
                cmd = new SqlCommand(sql, con);
               
                cmd.Parameters.AddWithValue("@make", make);
                cmd.Parameters.AddWithValue("@model", model);
                cmd.Parameters.AddWithValue("@available", aval);
                cmd.Parameters.AddWithValue("@regno", id);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Updated");
                txtregno.Enabled = true;
                Mode = true;

                txtmake.Clear();
                txtmodel.Clear();
                
                txtmake.Focus();
                
            }

            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex ==dataGridView1.Columns["edit"].Index && e.RowIndex >= 0)
            {
                Mode = false;
                txtregno.Enabled = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                getid(id);

            }
            else if (e.ColumnIndex == dataGridView1.Columns["delete"].Index && e.RowIndex >= 0)
            {
                Mode = false;

                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                sql = "delete from carreg where regno = @id";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Deleted");
                con.Close();
                
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            load();
            Autono();
            txtmake.Clear();
            txtmodel.Clear();
           
            txtmake.Focus();
        }
    }
}
