using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gauss2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            label1.Text = "Jordan Gauss Elimination";
            button1.Text = "Wprowadź dane ręcznie";
            button2.Text = "Losuj dane";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 userData = new Form1();
            userData.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 autoData = new Form3();
            autoData.Show();
        }
    }
}
