using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JazdyProbne
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            rental r = new rental();
            r.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            carreg c = new carreg();
            c.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            customer c = new customer();
            c.Show();
        }
    }
}
