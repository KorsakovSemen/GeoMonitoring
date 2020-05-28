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
            textBox3.Validated += textBox3_Validated;
            textBox4.Validated += textBox4_Validated;
            errorProvider3.SetIconAlignment(textBox3, ErrorIconAlignment.MiddleRight);
            errorProvider4.SetIconAlignment(textBox4, ErrorIconAlignment.MiddleRight);
        }

        private void set_Click(object sender, EventArgs e)
        {
            if ((textBox3.Text == "" || textBox4.Text == "") && (Xbutton.Checked == false || Tbutton.Checked == false))
            {
                Main.TFCheck = true;
                Close();
            }
            if (textBox3.Text != "")
                Main.FirstCrossRoadsGreenLight = int.Parse(textBox3.Text);
            else
                errorProvider3.SetError(textBox3, "Заполните поле!");
            if (textBox4.Text != "")
                Main.FirstCrossRoadsRedLight = int.Parse(textBox4.Text);
            else
                errorProvider4.SetError(textBox4, "Заполните поле!");
            if (textBox3.Text != "" && textBox4.Text != "" && (Xbutton.Checked == true || Tbutton.Checked == true))
            {
                Close();
            }
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

        private void Tbutton_CheckedChanged(object sender, EventArgs e)
        {
            Main.FirstCrossRoads = 1;
            Main.SecondCrossRoads = 2;
        }

        private void Xbutton_CheckedChanged(object sender, EventArgs e)
        {
            Main.FirstCrossRoads = 2;
            Main.SecondCrossRoads = 2;
        }

        private void CrossroadsSettings_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
