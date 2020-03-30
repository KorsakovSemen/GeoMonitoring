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
    public partial class TrafficLightSettings : MetroForm
    {
        public TrafficLightSettings()
        {
            InitializeComponent();
        }
        private int SetCrossroads()
        {
            int crossRoads = 0;
            foreach (Control control in this.traficLightsOptions.Controls)
            {
                if (control is RadioButton)
                {
                    RadioButton radio = control as RadioButton;
                    if (radio.Checked)
                    {
                        switch (radio.Name.ToString())
                        {
                            case "2 светофора":
                                crossRoads = 2;
                                Main.crossRoadsParamSave = "twoTrafficLights";
                                break;
                            case "Т-образный перекресток (3 светофора)":
                                crossRoads = 3;
                                Main.crossRoadsParamSave = "threeTrafficLights";
                                break;
                            case "Обычный перекресток(4 светофора)":
                                crossRoads = 4;
                                Main.crossRoadsParamSave = "fourTrafficLights";
                                break;
                            case "Свое значение":
                                Main.crossRoadsParamSave = "customTrafficLights";
                                if (int.TryParse(textBox1.Text, out int test))
                                {
                                    crossRoads = int.Parse(textBox1.Text);
                                }
                                break;
                        }
                        break;
                    }
                }
            }
            return crossRoads;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Main.EpicSizeParam = SetCrossroads();
            CrossroadsSettings f = new CrossroadsSettings();
            f.ShowDialog();
            this.Close();
        }

        private void twoTrafficLights_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
        }

        private void threeTrafficLights_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
        }

        private void fourTrafficLights_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
        }

        private void customTrafficLights_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
        }
    }
}
