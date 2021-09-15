using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Калькулятор : Form
    {
        public Калькулятор()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "DEC") checkBox1.Enabled = true;
            else checkBox1.Enabled = false;
            checkBox1.Checked = false;
            numericUpDown1.Value = 3;
            стираниеРезультата();
        }

        private void label_symbol_Click(object sender, EventArgs e)
        {

        }

        private void Калькулятор_Load(object sender, EventArgs e)
        {
            radioButton_add.Checked = true;
            comboBox1.SelectedItem = "DEC";
        }

        private void radioButton_add_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_add.Checked == true) label_symbol.Text = "+";
            progressBar1.Value = 1;
            стираниеРезультата();
        }

        private void radioButton_sub_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_sub.Checked == true) label_symbol.Text = "-";
            progressBar1.Value = 2;
            стираниеРезультата();
        }

        private void radioButton_mult_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_mult.Checked == true) label_symbol.Text = "*";
            progressBar1.Value = 3;
            стираниеРезультата();
        }

        private void radioButton_div_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_div.Checked == true) label_symbol.Text = "/";
            progressBar1.Value = 4;
            стираниеРезультата();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true) numericUpDown1.Enabled = true;
            else numericUpDown1.Enabled = false;
        }

        private void button_result_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "") textBox1.Text = "1";
            if (textBox2.Text == "") textBox2.Text = "1";

            char[] c = textBox1.Text.ToCharArray();
            int a = c.Length;
            int d = 0;
            for (int i = 0; i < a; i++)
                if (c[i] == ',') d = d + 1;
            if (d == 2) MessageBox.Show("Забагато ком");

            double x1 = Convert.ToDouble(textBox1.Text);
            double x2 = Double.Parse(textBox2.Text);
            string symbol = label_symbol.Text;
            double y;
            switch (symbol)
            {
                case "+":
                    y = x1 + x2;
                    break;
                case "-":
                    y = x1 - x2;
                    break;
                case "*":
                    y = x1 * x2;
                    break;
                case "/":
                    y = x1 / x2;
                    break;
                case "^":
                    y = Math.Pow(x1, x2);
                    break;

                default:
                    y = 0;
                    break;
            }

            int precision = (int)numericUpDown1.Value;
            y = Math.Round(y, precision);

            symbol = Convert.ToString(comboBox1.SelectedItem);
            string result;
            switch (symbol)
            {
                case "HEX":
                    result = Convert.ToString((int)y, 16);
                    result = result.ToUpper();
                    break;
                case "DEC":
                    result = Convert.ToString(y);
                    break;
                case "OCT":
                    result = Convert.ToString((int)y, 8);
                    break;
                case "BIN":
                    result = Convert.ToString((int)y, 2);
                    break;
                default:
                    MessageBox.Show("Така система числення не запрограмована", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox_result.Text = "???";
                    return;
                    //break;
            }

            textBox_result.Text = result;
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            //TextBox textBox = (TextBox)sender;
            //textBox.SelectAll();
            (sender as TextBox).SelectAll();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char[] c = textBox1.Text.ToCharArray();
            int a = c.Length;
            int d = 0;

            //if (c[0] == ',')
            //textBox1.Text = "0,";

            //for (int i = 0; i < a; i++)
            //if (c[i] == ',') d = d + 1;

            //if (d == 1)
            //{
            //if (e.KeyChar == ',')
            //{
            //e.Handled = true;
            //}
            //}
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true;
            }
        }

        private void стираниеРезультата()
        {
            textBox_result.Text = "???";
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char[] c2 = textBox2.Text.ToCharArray();
            int a2 = c2.Length;
            int d2 = 0;

            if (c2[0] == ',')
                textBox2.Text = "0,";

            for (int i = 0; i < a2; i++)
                if (c2[i] == ',') d2 = d2 + 1;

            if (d2 == 1)
            {
                if (e.KeyChar == ',')
                {
                    e.Handled = true;
                }
            }

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true;
            }
        }

        private void textBox2_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.Clear();    //Clear if any old value is there in Clipboard        
            Clipboard.SetText(textBox_result.Text); //Copy text to Clipboard
            string strClip = Clipboard.GetText(); //Get text from Clipboard

            MessageBox.Show("Результат \n" + strClip + "\n скопійовано \n\n тепер можете нажати Ctrl+V \n і вставити текст кудись", "Копія у буфер обміну", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
    }
}
