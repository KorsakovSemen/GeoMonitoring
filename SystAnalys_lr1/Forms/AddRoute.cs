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

namespace SystAnalys_lr1
{
    public partial class AddRoute : MetroForm
    {
        public AddRoute()
        {
            InitializeComponent();
            textBox1.Validated += textBox1_Validated;
            errorProvider1.SetIconAlignment(textBox1, ErrorIconAlignment.MiddleRight);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "")
            {
                if (!Main.routes.ContainsKey(int.Parse(this.textBox1.Text)))
                {
                    Main.routes.Add(int.Parse(this.textBox1.Text), new List<Vertex>());
                    Main.routesEdge.Add(int.Parse(this.textBox1.Text), new List<Edge>());
                }
            }         

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void textBox1_Validated(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(textBox1.Text))
                errorProvider1.SetError(textBox1, "Заполните поле!");
            else
            {
                errorProvider1.SetError(textBox1, string.Empty);
                Close();
            }
            //if (string.IsNullOrWhiteSpace((sender as TextBox).Text))
            //    errorProvider1.SetError(textBox1, "Заполните поле!");

        }

     
    }
}
