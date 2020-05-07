using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystAnalys_lr1.Forms
{
    public partial class AddGrid : MetroForm
    {
        public AddGrid()
        {
            InitializeComponent();
            up.Validated += up_Validated;
            down.Validated += down_Validated;
            left.Validated += left_Validated;
            right.Validated += right_Validated;
            errorProvider1.SetIconAlignment(up, ErrorIconAlignment.MiddleRight);
            errorProvider2.SetIconAlignment(down, ErrorIconAlignment.MiddleRight);
            errorProvider3.SetIconAlignment(left, ErrorIconAlignment.MiddleRight);
            errorProvider4.SetIconAlignment(right, ErrorIconAlignment.MiddleRight);
            errorProvider1.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider2.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider3.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider4.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (up.Text != "")
                 Main.g.left = int.Parse(up.Text);
            else
                errorProvider1.SetError(up, "Заполните поле!");
            if (left.Text != "")
                Main.g.up = int.Parse(left.Text);
            else
                errorProvider1.SetError(left, "Заполните поле!");
            if (down.Text != "")
                Main.g.right = int.Parse(down.Text);
            else
                errorProvider1.SetError(down, "Заполните поле!");
            if (right.Text != "")
                Main.g.down = int.Parse(right.Text);
            else
                errorProvider1.SetError(right, "Заполните поле!");
            if (w.Text != "")
                Main.g.gridWidth = int.Parse(w.Text);
            else
                Main.g.gridWidth = 80;
            if (h.Text != "")
                Main.g.gridHeight = int.Parse(h.Text);
            else
                Main.g.gridHeight = 40;
            //GridPart.height = int.Parse(textBox1.Text) / 40;
            //GridPart.width = int.Parse(textBox2.Text) / 80;
            //  Data.buses.Add(new Bus(Data.routes[int.Parse(this.textBox1.Text)], new PictureBox(), 0, false, new List<Vertex>(),int.Parse(this.textBox1.Text), true));
            if(up.Text != "" && left.Text != "" && right.Text != "" && down.Text != "")
                Close();
        }

        private void up_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void left_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void w_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void right_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void down_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void h_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void up_Validated(object sender, EventArgs e)
        {
            if (up.Text == "")
                errorProvider1.SetError(up, "Заполните поле!");
            else
            {
                errorProvider1.SetError(up, string.Empty);
            }
        }

        private void left_Validated(object sender, EventArgs e)
        {
            if (left.Text == "")
                errorProvider3.SetError(left, "Заполните поле!");
            else
            {
                errorProvider3.SetError(left, string.Empty);
            }
        }

        private void right_Validated(object sender, EventArgs e)
        {
            if (right.Text == "")
                errorProvider4.SetError(right, "Заполните поле!");
            else
            {
                errorProvider4.SetError(right, string.Empty);
            }
        }

        private void down_Validated(object sender, EventArgs e)
        {
            if (down.Text == "")
                errorProvider2.SetError(down, "Заполните поле!");
            else
            {
                errorProvider2.SetError(down, string.Empty);
            }
        }
    }
}
