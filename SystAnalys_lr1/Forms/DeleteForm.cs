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
            Main.DelType = Main.ElementConstructorType.VertexAndEdge;
            Main.GlobalDel = MainStrings.VandE;
        }

        private void TrafficLights_CheckedChanged(object sender, EventArgs e)
        {
            Main.DelType = Main.ElementConstructorType.TrafficLight;
            Main.GlobalDel = MainStrings.TrafficLights;
        }

        private void BusStops_CheckedChanged(object sender, EventArgs e)
        {
            Main.DelType = Main.ElementConstructorType.BusStops;
            Main.GlobalDel = MainStrings.busStops;
        }

        private void All_CheckedChanged(object sender, EventArgs e)
        {
            Main.DelType = Main.ElementConstructorType.All;
            Main.GlobalDel = MainStrings.all;
        }

        private void MetroButton1_Click(object sender, EventArgs e)
        {
            Main.yes = true;
            Close();
        }

        private void TheBuses_CheckedChanged(object sender, EventArgs e)
        {
            Main.DelType = Main.ElementConstructorType.TheBuses;
            Main.GlobalDel = MainStrings.theBuses;
        }

        private void DeleteForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Main.yes)
            {
                Main.DelType = Main.ElementConstructorType.None;
                Main.GlobalDel = "";
            }            
        }
    }
}
