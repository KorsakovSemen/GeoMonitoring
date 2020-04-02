using MetroFramework.Forms;
using System;
using System.Windows.Forms;

namespace SystAnalys_lr1.Forms
{
    public partial class CrossroadsSettings : MetroForm
    {
        public CrossroadsSettings()
        {
            InitializeComponent();
            textBox1.Validated += textBox1_Validated;
            textBox2.Validated += textBox2_Validated;
            textBox3.Validated += textBox3_Validated;
            textBox4.Validated += textBox4_Validated;
            errorProvider1.SetIconAlignment(textBox1, ErrorIconAlignment.MiddleRight);
            errorProvider2.SetIconAlignment(textBox2, ErrorIconAlignment.MiddleRight);
            errorProvider3.SetIconAlignment(textBox3, ErrorIconAlignment.MiddleRight);
            errorProvider4.SetIconAlignment(textBox4, ErrorIconAlignment.MiddleRight);
        }

        private void set_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "")
                Main.firstCrossRoads = int.Parse(textBox1.Text);
            else
                errorProvider1.SetError(textBox1, "Заполните поле!");
            if (textBox2.Text != "")
                Main.secondCrossRoads = int.Parse(textBox2.Text);
            else
                errorProvider1.SetError(textBox2, "Заполните поле!");
            if (textBox3.Text != "")
                Main.firstCrossRoadsGreenLight = int.Parse(textBox3.Text);
            else
                errorProvider1.SetError(textBox3, "Заполните поле!");
            if (textBox4.Text != "")
                Main.firstCrossRoadsRedLight = int.Parse(textBox4.Text);
            else
                errorProvider1.SetError(textBox4, "Заполните поле!");
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "")
                Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void textBox1_Validated(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                errorProvider1.SetError(textBox1, "Заполните поле!");
            else
            {
                errorProvider1.SetError(textBox1, string.Empty);
            }
        }

        private void textBox2_Validated(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
                errorProvider2.SetError(textBox2, "Заполните поле!");
            else
            {
                errorProvider2.SetError(textBox2, string.Empty);
            }
        }

        private void textBox3_Validated(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
                errorProvider3.SetError(textBox3, "Заполните поле!");
            else
            {
                errorProvider3.SetError(textBox3, string.Empty);
            }
        }

        private void textBox4_Validated(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
                errorProvider4.SetError(textBox4, "Заполните поле!");
            else
            {
                errorProvider4.SetError(textBox4, string.Empty);
            }
        }
    }
}
