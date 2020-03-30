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
        }

        private void set_Click(object sender, EventArgs e)
        {

            Main.firstCrossRoads = int.Parse(textBox1.Text);
            Main.secondCrossRoads = int.Parse(textBox2.Text);
            Main.firstCrossRoadsGreenLight = int.Parse(textBox3.Text);
            Main.firstCrossRoadsRedLight = int.Parse(textBox4.Text);
      //      Main.secondCrossRoadsGreenLight = int.Parse(textBox6.Text);
      //      Main.secondCrossRoadsRedLight = int.Parse(textBox5.Text);

            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
        }
    }
}
