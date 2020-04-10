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
    public partial class EpicSettings : MetroForm
    {
        public EpicSettings()
        {
            InitializeComponent();


            switch (Main.EpicSizeParamSave)
            {
                case "radioEpicSmall":
                    radioEpicSmall.Checked = true;
                    break;
                case "radioEpicMedium":
                    radioEpicMedium.Checked = true;
                    break;
                case "radioEpicBig":
                    radioEpicBig.Checked = true;
                    break;
                case "radioEpicRandom":
                    radioEpicRandom.Checked = true;
                    break;
                case "radioCustom":
                    radioCustom.Checked = true;
                    textBox1.Text = Main.EpicSizeParam.ToString();
                    break;
            }
            if (Main.ExpandEpicParamet.Any())
            {
                foreach (var item in Main.ExpandEpicParamet)
                {
                    switch (item)
                    {
                        case "up":
                            up.Checked = true;
                            break;
                        case "down":
                            down.Checked = true;
                            radioEpicMedium.Enabled = true;
                            break;
                        case "right":
                            right.Checked = true;
                            radioEpicBig.Enabled = true;
                            break;
                        case "left":
                            left.Checked = true;
                            radioEpicRandom.Enabled = true;
                            break;
                    }
                }
            }

        }

        private void radioCustom_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
        }

        private void radioEpicSmall_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            textBox1.Clear();
        }

        private void radioEpicMedium_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            textBox1.Clear();
        }

        private void radioEpicBig_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            textBox1.Clear();
        }

        private void radioEpicRandom_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            textBox1.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Main.EpicSizeParam = SetEpicSize();

            Main.ExpandEpicParamet = SetExpandParam();

            this.Close();
        }
        private int SetEpicSize()
        {
            var rand = new Random();
            int EpicSizeParam = 0;
            foreach (Control control in this.groupBox2.Controls)
            {
                if (control is RadioButton)
                {
                    RadioButton radio = control as RadioButton;
                    if (radio.Checked)
                    {
                        switch (radio.Name.ToString())
                        {
                            case "radioEpicSmall":
                                EpicSizeParam = 2;
                                Main.EpicSizeParamSave = "radioEpicSmall";
                                break;
                            case "radioEpicMedium":
                                EpicSizeParam = 10;
                                Main.EpicSizeParamSave = "radioEpicMedium";
                                break;
                            case "radioEpicBig":
                                EpicSizeParam = 30;
                                Main.EpicSizeParamSave = "radioEpicBig";
                                break;
                            case "radioEpicRandom":
                                EpicSizeParam = rand.Next(2, 30);
                                Main.EpicSizeParamSave = "radioEpicRandom";
                                break;
                            case "radioCustom":
                                Main.EpicSizeParamSave = "radioCustom";
                                if (int.TryParse(textBox1.Text, out int T))
                                {
                                    EpicSizeParam = int.Parse(textBox1.Text);
                                }
                                else
                                {
                                    EpicSizeParam = rand.Next(2, 30);
                                }
                                break;
                        }
                        break;
                    }
                }
            }
            return EpicSizeParam;
        }
        private List<string> SetExpandParam()
        {
            var rand = new Random();
            List<string> Parameters = new List<string>();
            foreach (Control control in this.groupBox1.Controls)
            {
                if (control is CheckBox)
                {
                    CheckBox check = control as CheckBox;
                    if (check.Checked)
                    {
                        Parameters.Add(check.Name.ToString());
                    }
                }
            }
            return Parameters;
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
