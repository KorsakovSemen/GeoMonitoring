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

        public static bool TurnSpreadingSet = false;
        public EpicSettings()
        {
            InitializeComponent();
            if (DisplayEpicenters.FormOpen == false)
            {
                SavePictures.Checked = false;
                SavePictures.Enabled = false;
                extendedSavePictures.Enabled = false;

            }
            else
            {
                SavePictures.Checked = Main.SavePictures;
                extendedSavePictures.Checked = Main.extendedSavePictures;
            }
            if (extendedSavePictures.Checked == true)
            {
                phasesSettingsBox.Enabled = true;
            }
            else
            {
                phasesSettingsBox.Enabled = false;
            }
            TurnSpreading.Checked = TurnSpreadingSet;
            //
            if (TurnSpreading.Checked == true)
            {
             
                directionBox.Enabled = true;
                frequencyBox.Enabled = true;
            }
            else
            {            
                directionBox.Enabled = false;
                frequencyBox.Enabled = false;
            }
            ///
            switch (Main.EpicPhaseSavingSave)
            {
                case "radioPhase2":
                    radioPhase2.Checked = true;
                break;
                case "radioPhase3":
                    radioPhase3.Checked = true;
                break;
                case "radioPhase5":
                    radioPhase5.Checked = true;
                break;
                case "radioPhase10":
                    radioPhase10.Checked = true;
                break;
                case "radioPhaseCustom":
                    radioPhaseCustom.Checked = true;
                    metroTextBox2.Text = Main.EpicSizeParam.ToString();
                break;
            }
            //
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
            //
            switch (Main.EpicFreqSpreadSave)
            {
                case "RadioFreq100":
                    RadioFreq100.Checked = true;
                    break;
                case "RadioFreq500":
                    RadioFreq500.Checked = true;
                    break;
                case "RadioFreq1000":
                    RadioFreq1000.Checked = true;
                    break;
                case "RadioFreq2500":
                    RadioFreq2500.Checked = true;
                    break;
                case "RadioFreq5000":
                    RadioFreq5000.Checked = true;
                    break;
                case "RadioFreq10000":
                    RadioFreq10000.Checked = true;
                    break;
                case "RadioFreqRandom":
                  //  RadioFreqRandom.Checked = true;
                    break;
                case "RadioFreqCustom":
                    RadioFreqCustom.Checked = true;
                    metroTextBox1.Text = Main.EpicFreqSpreadParam.ToString();
                    break;
            }
            //
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
            Main.EpicPhaseSavingParam = SetPhaseSavingSize();
            Main.EpicSizeParam = SetEpicSize();
            Main.EpicFreqSpreadParam = SetFreqSpreadSize();
            Main.ExpandEpicParamet = SetExpandParam();
            Main.SavePictures = SavePictures.Checked;
            Main.extendedSavePictures = extendedSavePictures.Checked;
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
                                EpicSizeParam = 8;
                                Main.EpicSizeParamSave = "radioEpicSmall";
                                break;
                            case "radioEpicMedium":
                                EpicSizeParam = 25;
                                Main.EpicSizeParamSave = "radioEpicMedium";
                                break;
                            case "radioEpicBig":
                                EpicSizeParam = 50;
                                Main.EpicSizeParamSave = "radioEpicBig";
                                break;
                            case "radioEpicRandom":
                                EpicSizeParam = rand.Next(8, 100);
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
                                    EpicSizeParam = rand.Next(8, 100);
                                }
                                break;
                        }
                        break;
                    }
                }
            }
            return EpicSizeParam;
        }
        /// 
        private int SetFreqSpreadSize()
        {
            var rand = new Random();
            int FreqSpreadParam = 0;
            foreach (Control control in this.frequencyBox.Controls)
            {
                if (control is RadioButton)
                {
                    RadioButton radio = control as RadioButton;
                    if (radio.Checked)
                    {
                        switch (radio.Name.ToString())
                        {
                            case "RadioFreq100":
                                FreqSpreadParam = 100;
                                Main.EpicFreqSpreadSave = "RadioFreq100";
                                break;
                            case "RadioFreq500":
                                FreqSpreadParam = 500;
                                Main.EpicFreqSpreadSave = "RadioFreq500";
                                break;
                            case "RadioFreq1000":
                                FreqSpreadParam = 1000;
                                Main.EpicFreqSpreadSave = "RadioFreq1000";
                                break;
                            case "RadioFreq2500":
                                FreqSpreadParam = 2500;
                                Main.EpicFreqSpreadSave = "RadioFreq2500";
                                break;
                            case "RadioFreq5000":
                                FreqSpreadParam = 5000;
                                Main.EpicFreqSpreadSave = "RadioFreq5000";
                                break;
                            case "RadioFreq10000":
                                FreqSpreadParam = 10000;
                                Main.EpicFreqSpreadSave = "RadioFreq10000";
                                break;
                            case "RadioFreqRandom":
                                //FreqSpreadParam = rand.Next(8, 100);
                                Main.EpicFreqSpreadSave = "RadioFreqRandom";
                                break;
                            case "RadioFreqCustom":
                                Main.EpicFreqSpreadSave = "RadioFreqCustom";
                                if (int.TryParse(metroTextBox1.Text, out int T))
                                {
                                    FreqSpreadParam = int.Parse(metroTextBox1.Text);
                                }
                                else
                                {
                                    FreqSpreadParam = rand.Next(8, 100);
                                }
                                break;
                        }
                        break;
                    }
                }
            }
            return FreqSpreadParam;
        }
        private int SetPhaseSavingSize()
        {
       
            int PhasesSizeParam = 0;
            foreach (Control control in this.phasesSettingsBox.Controls)
            {
                if (control is RadioButton)
                {
                    RadioButton radio = control as RadioButton;
                    if (radio.Checked)
                    {
                        switch (radio.Name.ToString())
                        {
                            case "radioPhase2":
                                PhasesSizeParam = 2;
                                Main.EpicPhaseSavingSave = "radioPhase2";
                                break;
                            case "radioPhase3":
                                PhasesSizeParam = 3;
                                Main.EpicPhaseSavingSave = "radioPhase3";
                                break;
                            case "radioPhase5":
                                PhasesSizeParam = 5;
                                Main.EpicPhaseSavingSave = "radioPhase5";
                                break;
                            case "radioPhase10":
                                PhasesSizeParam = 10;
                                Main.EpicPhaseSavingSave = "radioPhase10";
                                break;                      
                            case "radioPhaseCustom":
                                Main.EpicPhaseSavingSave = "radioPhaseCustom";
                                if (int.TryParse(metroTextBox2.Text, out int T))
                                {
                                    PhasesSizeParam = int.Parse(metroTextBox2.Text);
                                }
                                else
                                {
                                    PhasesSizeParam = 1;
                                }
                                break;
                        }
                        break;
                    }
                }
            }
            return PhasesSizeParam;
        }


        /// 


        private List<string> SetExpandParam()
        {
            var rand = new Random();
            List<string> Parameters = new List<string>();
            foreach (Control control in this.directionBox.Controls)
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
        private void SavePictures_CheckedChanged(object sender, EventArgs e)
        {
            if (SavePictures.Checked == false)
            {
                extendedSavePictures.Checked = false;
                extendedSavePictures.Enabled = false;
            }
            else
            {
                extendedSavePictures.Enabled = true;
            }
        }

        private void TurnSpreading_CheckedChanged(object sender, EventArgs e)
        {
            if (TurnSpreading.Checked == true)
            {
                TurnSpreadingSet = true;
                directionBox.Enabled = true;
                frequencyBox.Enabled = true;
            }
            else
            {
                TurnSpreadingSet = false;
                directionBox.Enabled = false;
                frequencyBox.Enabled = false;
            }
        }

        private void metroTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void extendedSavePictures_CheckedChanged(object sender, EventArgs e)
        {
            if (extendedSavePictures.Checked == true)
            {         
                    phasesSettingsBox.Enabled = true;
            }
            else
            {
                phasesSettingsBox.Enabled = false;
            }
        }

        private void metroTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

       
    }
}
