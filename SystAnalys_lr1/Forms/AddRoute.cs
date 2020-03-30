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
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (!Main.routes.ContainsKey(int.Parse(this.textBox1.Text)))
            {
                Main.routes.Add(int.Parse(this.textBox1.Text), new List<Vertex>());
                Main.routesEdge.Add(int.Parse(this.textBox1.Text), new List<Edge>());
            }

            this.Close();
        }
    }
}
