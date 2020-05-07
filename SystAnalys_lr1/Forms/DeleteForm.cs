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
    public partial class DeleteForm : MetroForm
    {
        public DeleteForm()
        {
            InitializeComponent();
        }

        private void MetroRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Main.delType = Main.DeleteType.VertexAndEdge;
            Main.globalDel = MainStrings.VandE;
        }

        private void TrafficLights_CheckedChanged(object sender, EventArgs e)
        {
            Main.delType = Main.DeleteType.TrafficLight;
            Main.globalDel = MainStrings.TrafficLights;
        }

        private void BusStops_CheckedChanged(object sender, EventArgs e)
        {
            Main.delType = Main.DeleteType.BusStops;
            Main.globalDel = MainStrings.busStops;
        }

        private void All_CheckedChanged(object sender, EventArgs e)
        {
            Main.delType = Main.DeleteType.All;
            Main.globalDel = MainStrings.all;
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            Main.yes = true;
            Close();
        }

        private void theBuses_CheckedChanged(object sender, EventArgs e)
        {
            Main.delType = Main.DeleteType.TheBuses;
            Main.globalDel = MainStrings.theBuses;
        }

        private void DeleteForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Main.yes)
            {
                Main.delType = Main.DeleteType.None;
                Main.globalDel = "";
            }            
        }
    }
}
