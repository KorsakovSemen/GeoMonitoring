using MetroFramework.Forms;
using SystAnalys_lr1.Strings;
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
    public partial class SelectVertex : MetroForm
    {
        public SelectVertex()
        {
            InitializeComponent();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            Main.yes = true;
            Close();
        }

        private void SelectVertex_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Main.yes)
            {
                Main.DelType = Main.ElementConstructorType.None;
                Main.GlobalDel = "";
            }
        }

        private void station_CheckedChanged(object sender, EventArgs e)
        {
            Main.DelType = Main.ElementConstructorType.Station;
            Main.GlobalDel = MainStrings.station;
        }

        private void V_CheckedChanged(object sender, EventArgs e)
        {
            Main.DelType = Main.ElementConstructorType.VertexAndEdge;
            Main.GlobalDel = MainStrings.VandE;
        }

      
    }
}
