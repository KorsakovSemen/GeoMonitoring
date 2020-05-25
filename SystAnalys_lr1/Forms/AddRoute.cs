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



        private void textBox1_Validated(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(textBox1.Text))
                errorProvider1.SetError(textBox1, "Заполните поле!");
            else
            {
                errorProvider1.SetError(textBox1, string.Empty);
                Close();
            }
        }

    }
}
