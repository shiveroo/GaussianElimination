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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox3.ScrollBars = ScrollBars.Vertical;
            textBox4.ScrollBars = ScrollBars.Vertical;
            button1.Text = "Oblicz";
            label6.Text = " ";
            label10.Text = " ";
            label11.Text = " ";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            gaussJordan();
        }

        public double GetRandomNumber(double minimum, double maximum, Random random)
        {
            
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        private void gaussJordan()
        {
            label6.Text = "Uruchomiono";
            label6.Refresh();

            int error = 1;
            int n = 0;
            double mmin = 0, mmax = 0, xmin = 0, xmax = 0;

            DateTime dt = DateTime.Now;

            try
            {
                error = 0;

                n = int.Parse(textBox2.Text);
                mmin = double.Parse(textBox5.Text);
                mmax = double.Parse(textBox6.Text);
                xmin = double.Parse(textBox7.Text);
                xmax = double.Parse(textBox8.Text);
            }
            catch
            {
                error = 1;
                System.Windows.Forms.MessageBox.Show("Error");
                label6.Text = "Error";
            }
      
            if (n < 2)
            {
                System.Windows.Forms.MessageBox.Show("Podaj poprawną wielkość macierzy");
            }



            //Printing results
            //for (int i = 0; i < n; i++)
            //{
            //    for (int j = 0; j < n + 1; j++)
            //    {
            //        textBox1.Text += rows[i][j].ToString() + " ";
            //    }
            //    textBox1.AppendText(Environment.NewLine);
            //}          

            if (error == 0)
            {
                double[][] rows = new double[n][];
                double[] x_dok = new double[n];
                double[] b_dok = new double[n];

                Random rnd = new Random();

                for (int i = 0; i < n; i++)
                {
                    rows[i] = new double[n + 1];

                    //x_dok[i] = GetRandomNumber(0.0001 , 100000, rnd);
                    x_dok[i] = GetRandomNumber(xmin, xmax, rnd);

                    for (int j = 0; j < n + 1; j++)
                    {
                        rows[i][j] = GetRandomNumber(1000, 100000, rnd);
                        x_dok[i] = GetRandomNumber(mmin, mmax, rnd);
                    }
                }

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        b_dok[i] += rows[i][j] * x_dok[j];
                    }
                }

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n + 1; j++)
                    {
                        if (j == n)
                        {
                            rows[i][j] = b_dok[i];
                            textBox1.Text += "X_dok " + (i + 1) + ": " + x_dok[i].ToString();
                            textBox1.AppendText(Environment.NewLine);
                        }
                    }
                }

                int length = rows[0].Length;
                for (int i = 0; i < rows.Length - 1; i++)
                {
                    for (int j = i; j < rows.Length; j++)
                    {
                        TimeSpan ts = DateTime.Now - dt;
                        label4.Text = ts.TotalMilliseconds.ToString() + " ms";
                        label4.Refresh();

                        double[] d = new double[length];
                        for (int x = 0; x < length; x++)
                        {
                            if (i == j && rows[j][i] == 0)
                            {
                                bool changed = false;
                                for (int z = rows.Length - 1; z > i; z--)
                                {
                                    if (rows[z][i] != 0)
                                    {
                                        double[] temp = new double[length];
                                        temp = rows[z];
                                        rows[z] = rows[j];
                                        rows[j] = temp;
                                        changed = true;
                                    }
                                }
                                if (!changed)
                                {
                                    textBox1.Text += "Brak rozwiązań\r\n";
                                    return;
                                }
                            }
                            if (rows[j][i] != 0)
                            {
                                d[x] = rows[j][x] / rows[j][i];
                            }
                            else
                            {
                                d[x] = rows[j][x];
                            }
                        }
                        rows[j] = d;
                    }

                    for (int y = i + 1; y < rows.Length; y++)
                    {
                        double[] f = new double[length];
                        for (int g = 0; g < length; g++)
                        {
                            if (rows[y][i] != 0)
                            {
                                f[g] = rows[y][g] - rows[i][g];
                            }
                            else
                            {
                                f[g] = rows[y][g];
                            }
                        }
                        rows[y] = f;
                    }
                }

                double val = 0;
                int k = length - 2;
                double[] result = new double[rows.Length];
                for (int i = rows.Length - 1; i >= 0; i--)
                {
                    val = rows[i][length - 1];
                    for (int x = length - 2; x > k; x--)
                    {
                        val -= rows[i][x] * result[x];
                    }
                    result[i] = val / rows[i][i];
                    if (result[i].ToString() == "NaN" || result[i].ToString().Contains("Infinity"))
                    {
                        textBox1.Text += "No Solution Found!\n";
                        return;
                    }
                    k--;
                }

                double min = 1000000000000000;
                double max = -10;

                for (int i = 0; i < result.Length; i++)
                {                   
                    double blad = Math.Abs(x_dok[i] - result[i]);

                    if(blad < min)
                    {
                        min = blad;
                    }

                    if(blad > max)
                    {
                        max = blad;
                    }

                    textBox3.Text += string.Format("X{0} = {1}\r\n", i + 1, Math.Round(result[i], 15));
                    textBox4.Text += blad.ToString();
                    textBox4.AppendText(Environment.NewLine);

                    label10.Text = "Błąd min.: " + min;
                    label11.Text = "Błąd max.: " + max;
                }

                label6.Text = "Koniec";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
