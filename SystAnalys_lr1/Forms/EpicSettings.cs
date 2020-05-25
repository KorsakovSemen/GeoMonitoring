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

        public static string EpicSizeParamSave { get => s_epicSizeParamSave; set => s_epicSizeParamSave = value; }
        public static List<string> MovingEpicParamet { get => s_movingEpicParamet; set => s_movingEpicParamet = value; }
        public static string EpicFreqMovingSave { get => s_epicFreqMovingSave; set => s_epicFreqMovingSave = value; }
        public static int EpicFreqMovingParam { get => s_epicFreqMovingParam; set => s_epicFreqMovingParam = value; }
        public static string EpicFreqSpreadingSave { get => s_epicFreqSpreadingSave; set => s_epicFreqSpreadingSave = value; }
        public static int EpicFreqSpreadingParam { get => s_epicFreqSpreadingParam; set => s_epicFreqSpreadingParam = value; }
        public static string EpicPhaseSavingSave { get => s_epicPhaseSavingSave; set => s_epicPhaseSavingSave = value; }
        public static int EpicPhaseSavingParam { get => s_epicPhaseSavingParam; set => s_epicPhaseSavingParam = value; }
        public static bool SavePictures { get => s_savePictures; set => s_savePictures = value; }
        public static bool ExtendedSavePictures { get => s_extendedSavePictures; set => s_extendedSavePictures = value; }
        public static bool TurnMovingSet = false;
        public static bool TurnExpandingSet = false;

        private static string s_epicSizeParamSave = "radioEpicMedium";
        private static List<string> s_movingEpicParamet;
        private static string s_epicFreqMovingSave = null;
        private static int s_epicFreqMovingParam = 0;
        private static string s_epicFreqSpreadingSave = null;
        private static int s_epicFreqSpreadingParam = 0;
        private static string s_epicPhaseSavingSave = null;
        private static int s_epicPhaseSavingParam = 1;
        private static bool s_savePictures = false;
        private static bool s_extendedSavePictures = false;

        public EpicSettings()
        {
            InitializeComponent();
            if (DisplayEpicenters.FormOpen == false)
            {
                SavingPictures.Checked = false;
                SavingPictures.Enabled = false;
                extendedSavePictures.Enabled = false;

            }
            else
            {
                SavingPictures.Checked = SavePictures;
                extendedSavePictures.Checked = ExtendedSavePictures;
            }
            if (extendedSavePictures.Checked == true)
            {
                phasesSettingsBox.Enabled = true;
            }
            else
            {
                phasesSettingsBox.Enabled = false;
            }
            TurnMoving.Checked = TurnMovingSet;

            if (TurnMoving.Checked == true)
            {

                directionBox.Enabled = true;
                frequencyBox.Enabled = true;
            }
            else
            {
                directionBox.Enabled = false;
                frequencyBox.Enabled = false;
            }

            TurnSpreading.Checked = TurnExpandingSet;
            if (TurnSpreading.Checked == true)
            {
                SpredFreq.Enabled = true;
            }
            else
            {
                SpredFreq.Enabled = false;
            }

            switch (EpicPhaseSavingSave)
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

            switch (EpicSizeParamSave)
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

            switch (EpicFreqMovingSave)
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
                    break;
                case "RadioFreqCustom":
                    RadioFreqCustom.Checked = true;
                    metroTextBox1.Text = EpicFreqMovingParam.ToString();
                    break;
            }


            if (MovingEpicParamet.Any())
            {
                foreach (var item in MovingEpicParamet)
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

            switch (EpicFreqSpreadingSave)
            {
                case "RadioFreqSpred100":
                    RadioFreqSpred100.Checked = true;
                    break;
                case "RadioFreqSpred500":
                    RadioFreqSpred500.Checked = true;
                    break;
                case "RadioFreqSpred1000":
                    RadioFreqSpred1000.Checked = true;
                    break;
                case "RadioFreqSpred2500":
                    RadioFreqSpred2500.Checked = true;
                    break;
                case "RadioFreqSpred5000":
                    RadioFreqSpred5000.Checked = true;
                    break;
                case "RadioFreqSpred10000":
                    RadioFreqSpred10000.Checked = true;
                    break;
                case "RadioFreqSpredRandom":
                    break;
                case "RadioFreqSpredCustom":
                    RadioFreqSpredCustom.Checked = true;
                    metroTextBox3.Text = EpicFreqMovingParam.ToString();
                    break;
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
            EpicPhaseSavingParam = SetPhaseSavingSize();
            Main.EpicSizeParam = SetEpicSize();
            EpicFreqMovingParam = SetFreqMovingSize();
            EpicFreqSpreadingParam = SetFreqSpreadingSize();
            MovingEpicParamet = SetExpandParam();
            SavePictures = SavingPictures.Checked;
            ExtendedSavePictures = extendedSavePictures.Checked;
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
                                EpicSizeParamSave = "radioEpicSmall";
                                break;
                            case "radioEpicMedium":
                                EpicSizeParam = 25;
                                EpicSizeParamSave = "radioEpicMedium";
                                break;
                            case "radioEpicBig":
                                EpicSizeParam = 50;
                                EpicSizeParamSave = "radioEpicBig";
                                break;
                            case "radioEpicRandom":
                                EpicSizeParam = rand.Next(8, 100);
                                EpicSizeParamSave = "radioEpicRandom";
                                break;
                            case "radioCustom":
                                EpicSizeParamSave = "radioCustom";
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

        private int SetFreqMovingSize()
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
                                EpicFreqMovingSave = "RadioFreq100";
                                break;
                            case "RadioFreq500":
                                FreqSpreadParam = 500;
                                EpicFreqMovingSave = "RadioFreq500";
                                break;
                            case "RadioFreq1000":
                                FreqSpreadParam = 1000;
                                EpicFreqMovingSave = "RadioFreq1000";
                                break;
                            case "RadioFreq2500":
                                FreqSpreadParam = 2500;
                                EpicFreqMovingSave = "RadioFreq2500";
                                break;
                            case "RadioFreq5000":
                                FreqSpreadParam = 5000;
                                EpicFreqMovingSave = "RadioFreq5000";
                                break;
                            case "RadioFreq10000":
                                FreqSpreadParam = 10000;
                                EpicFreqMovingSave = "RadioFreq10000";
                                break;
                            case "RadioFreqRandom":
                                EpicFreqMovingSave = "RadioFreqRandom";
                                break;
                            case "RadioFreqCustom":
                                EpicFreqMovingSave = "RadioFreqCustom";
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

        private int SetFreqSpreadingSize()
        {
            var rand = new Random();
            int FreqSpreadParam = 0;
            foreach (Control control in this.SpredFreq.Controls)
            {
                if (control is RadioButton)
                {
                    RadioButton radio = control as RadioButton;
                    if (radio.Checked)
                    {
                        switch (radio.Name.ToString())
                        {
                            case "RadioFreqSpred100":
                                FreqSpreadParam = 100;
                                EpicFreqSpreadingSave = "RadioFreqSpred100";
                                break;
                            case "RadioFreqSpred500":
                                FreqSpreadParam = 500;
                                EpicFreqSpreadingSave = "RadioFreqSpred500";
                                break;
                            case "RadioFreqSpred1000":
                                FreqSpreadParam = 1000;
                                EpicFreqSpreadingSave = "RadioFreqSpred1000";
                                break;
                            case "RadioFreqSpred2500":
                                FreqSpreadParam = 2500;
                                EpicFreqSpreadingSave = "RadioFreqSpred2500";
                                break;
                            case "RadioFreqSpred5000":
                                FreqSpreadParam = 5000;
                                EpicFreqSpreadingSave = "RadioFreqSpred5000";
                                break;
                            case "RadioFreqSpred10000":
                                FreqSpreadParam = 10000;
                                EpicFreqSpreadingSave = "RadioFreqSpred10000";
                                break;
                            case "RadioFreqSpredRandom":
                                EpicFreqSpreadingSave = "RadioFreqSpredRandom";
                                break;
                            case "RadioFreqSpredCustom":
                                EpicFreqSpreadingSave = "RadioFreqSpredCustom";
                                if (int.TryParse(metroTextBox3.Text, out int T))
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
                                EpicPhaseSavingSave = "radioPhase2";
                                break;
                            case "radioPhase3":
                                PhasesSizeParam = 3;
                                EpicPhaseSavingSave = "radioPhase3";
                                break;
                            case "radioPhase5":
                                PhasesSizeParam = 5;
                                EpicPhaseSavingSave = "radioPhase5";
                                break;
                            case "radioPhase10":
                                PhasesSizeParam = 10;
                                EpicPhaseSavingSave = "radioPhase10";
                                break;
                            case "radioPhaseCustom":
                                EpicPhaseSavingSave = "radioPhaseCustom";
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
            if (SavingPictures.Checked == false)
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
            if (TurnMoving.Checked == true)
            {
                TurnMovingSet = true;
                directionBox.Enabled = true;
                frequencyBox.Enabled = true;
            }
            else
            {
                TurnMovingSet = false;
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

        private void EpicSettings_Load(object sender, EventArgs e)
        {

        }

        private void metroCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (TurnSpreading.Checked == true)
            {
                TurnExpandingSet = true;
                SpredFreq.Enabled = true;

            }
            else
            {
                TurnExpandingSet = false;
                SpredFreq.Enabled = false;

            }
        }
    }
}
