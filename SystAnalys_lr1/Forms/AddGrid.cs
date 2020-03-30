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
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Main.g.left = int.Parse(up.Text);
            Main.g.up = int.Parse(left.Text);
            Main.g.right = int.Parse(down.Text);
            Main.g.down = int.Parse(right.Text);
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
            //  Main.buses.Add(new Bus(Main.routes[int.Parse(this.textBox1.Text)], new PictureBox(), 0, false, new List<Vertex>(),int.Parse(this.textBox1.Text), true));

            this.Close();
        }
    }
}
