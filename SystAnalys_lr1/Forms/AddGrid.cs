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
            errorProvider1.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (up.Text != "" && 0 <= int.Parse(up.Text) && int.Parse(up.Text) <= 400)
                 Main.Grid.Left = int.Parse(up.Text);
            else
                errorProvider1.SetError(up, "Заполните поле!");
            if (left.Text != "" && 0 <= int.Parse(left.Text) && int.Parse(left.Text) <= 800)
                Main.Grid.Up = int.Parse(left.Text);
            else
                errorProvider1.SetError(left, "Заполните поле!");
            if (down.Text != "" && 0 <= int.Parse(down.Text) && int.Parse(down.Text) <= 400)
                Main.Grid.Right = int.Parse(down.Text);
            else
                errorProvider1.SetError(down, "Заполните поле!"); 
            if (right.Text != "" && 0 <= int.Parse(right.Text) && int.Parse(right.Text) <= 800)
                Main.Grid.Down = int.Parse(right.Text);
            else
                errorProvider1.SetError(right, "Заполните поле!");
            if (w.Text != "")
                Main.Grid.GridWidth = int.Parse(w.Text);
            else
                Main.Grid.GridWidth = 80;
            if (h.Text != "")
                Main.Grid.GridHeight = int.Parse(h.Text);
            else
                Main.Grid.GridHeight = 40;
            if(up.Text != "" && left.Text != "" && right.Text != "" && down.Text != "" && 0 <= int.Parse(up.Text) && int.Parse(up.Text) <= 400 && 0 <= int.Parse(left.Text) && int.Parse(left.Text) <= 800 && 0 <= int.Parse(down.Text) && int.Parse(down.Text) <= 400 && 0 <= int.Parse(right.Text) && int.Parse(right.Text) <= 800)
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
                errorProvider1.SetError(left, "Заполните поле!");
            else
            {
                errorProvider1.SetError(left, string.Empty);
            }
        }

        private void right_Validated(object sender, EventArgs e)
        {
            if (right.Text == "")
                errorProvider1.SetError(right, "Заполните поле!");
            else
            {
                errorProvider1.SetError(right, string.Empty);
            }
        }

        private void down_Validated(object sender, EventArgs e)
        {
            if (down.Text == "")
                errorProvider1.SetError(down, "Заполните поле!");
            else
            {
                errorProvider1.SetError(down, string.Empty);
            }
        }
    }
}
