using MetroFramework;
using MetroFramework.Forms;
using SystAnalys_lr1.Classes;
using SystAnalys_lr1.Forms;
using SystAnalys_lr1.Strings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystAnalys_lr1
{
    public partial class Main : MetroForm
    {
        private void AddInComboBox()
        {
            changeRoute.Items.Clear();
            changeRoute.Items.Add(MainStrings.none);
            changeRoute.Items.Add(MainStrings.network);
            foreach (var r in Data.Routes)
            {
                changeRoute.Items.Add(r.Key);
            };
            changeRoute.Text = MainStrings.network;
        }


        private void ConstructorOnNetwork()
        {
            addTraficLight.Enabled = true;
            delAllBusesOnRoute.Enabled = false;
            stopPointButton.Enabled = true;
            allBusSettings.Enabled = false;
            drawEdgeButton.Enabled = true;
            selectButton.Enabled = true;
            drawVertexButton.Enabled = true;
            deleteButton.Enabled = true;
            deleteALLButton.Enabled = true;
            deleteRoute.Enabled = true;
            addBus.Enabled = false;
            deleteBus.Enabled = true;
            selectRoute.Enabled = true;
        }

        private void ConstructorOffNetwork()
        {
            addTraficLight.Enabled = false;
            delAllBusesOnRoute.Enabled = false;
            stopPointButton.Enabled = false;
            allBusSettings.Enabled = false;
            drawEdgeButton.Enabled = false;
            selectButton.Enabled = false;
            drawVertexButton.Enabled = false;
            deleteButton.Enabled = false;
            deleteALLButton.Enabled = false;
            deleteRoute.Enabled = false;
            addBus.Enabled = false;
            deleteBus.Enabled = false;
            selectRoute.Enabled = false;
        }

        private void InitializeElements()
        {
            MovingEpicParamet = new List<string>();
            timer.Interval = BusStop.StopTime / 10;
            r = new Report();
            loadingForm = new LoadingForm();
            ReportCount = 0;
            coordinates = new Coordinates();
            Grid = new Classes.Grid(0, 0, 0, 0, 80, 40);
            G = new DrawGraph();

            StyleManager = msmMain;
            ConstructorOffNetwork();

            zoom = 1;
            scrollX = 0;
            scrollY = 0;

        }

        private void LoadSettings()
        {
            if (File.Exists("../../SaveConfig/save.txt"))
            {
                using (FileStream fstream = File.OpenRead("../../SaveConfig/save.txt"))
                {
                    byte[] array = new byte[fstream.Length];
                    // асинхронное чтение файла
                    fstream.Read(array, 0, array.Length);
                    savepath = System.Text.Encoding.Default.GetString(array);
                    try
                    {
                        if (savepath != null && savepath.Length > 2 && Directory.Exists(savepath))
                        {
                            savepath = Path.GetFullPath(savepath);
                            try
                            {
                                if (Directory.Exists(savepath))
                                {
                                    Console.WriteLine(savepath);
                                    LoadRoutes(savepath + @"\");
                                }
                            }
                            catch (Exception exc)
                            {
                                StackTrace stackTrace = new StackTrace(exc, true);
                                if (stackTrace.FrameCount > 0)
                                {
                                    BringToFront();
                                    MetroMessageBox.Show(this, $"{exc.StackTrace}", MainStrings.error, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                        }
                    }
                    catch
                    {
                        BringToFront();
                        MetroMessageBox.Show(this, MainStrings.errorPath, MainStrings.error, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    Console.WriteLine($"Текст из файла: {savepath}");

                }
            }
            else
            {
                File.Create("../../SaveConfig/save.txt");
            }
            if (File.Exists("../../SaveConfig/theme.txt"))
            {
                using (FileStream fstream = File.OpenRead("../../SaveConfig/theme.txt"))
                {
                    byte[] array = new byte[fstream.Length];
                    // асинхронное чтение файла
                    fstream.Read(array, 0, array.Length);
                    if (System.Text.Encoding.Default.GetString(array) == "Dark\r\n")
                    {
                        msmMain.Theme = MetroThemeStyle.Dark;
                        toolStripMenu.BackColor = Color.FromArgb(17, 17, 17);
                        toolStripMenu.ForeColor = Color.FromArgb(153, 153, 153);
                        fstream.Close();
                        themes.Checked = true;
                    }
                    if (System.Text.Encoding.Default.GetString(array) == "Light\r\n")
                    {
                        msmMain.Theme = MetroThemeStyle.Light;
                        toolStripMenu.BackColor = Color.FromArgb(255, 255, 255);
                        toolStripMenu.ForeColor = Color.FromArgb(0, 0, 0);
                        fstream.Close();
                        themes.Checked = false;
                    }
                    if (System.Text.Encoding.Default.GetString(array) == "Default\r\n")
                    {
                        msmMain.Theme = MetroThemeStyle.Default;
                        toolStripMenu.BackColor = Color.FromArgb(255, 255, 255);
                        toolStripMenu.ForeColor = Color.FromArgb(0, 0, 0);
                        fstream.Close();
                        themes.Checked = false;
                    }
                    Console.WriteLine($"Текст из файла: {savepath}");

                }
            }
            else
            {
                File.Create("../../SaveConfig/theme.txt");
            }
            string text = "Default";
            if (File.Exists("../../SaveConfig/style.txt"))
            {
                using (FileStream fstream = File.OpenRead("../../SaveConfig/style.txt"))
                {
                    byte[] array = new byte[fstream.Length];
                    // асинхронное чтение файла
                    fstream.Read(array, 0, array.Length);
                    text = System.Text.Encoding.Default.GetString(array).Replace(Environment.NewLine, "");
                    msmMain.Style = (MetroFramework.MetroColorStyle)Convert.ToInt32(text);
                    Console.WriteLine($"Текст из файла: {savepath}");

                }
                changeTheme.SelectedIndex = Convert.ToInt32(text);
                //       changeTheme.Text = text;
            }
            else
            {
                File.Create("../../SaveConfig/style_text.txt");
            }
            if (sheet.Image == null)
            {
                addRouteToolStripMenuItem.Enabled = false;
                openEpicFormToolStripMenuItem.Enabled = false;
                createGridToolStripMenuItem.Enabled = false;
            }

            mainPanel.MaximumSize = new System.Drawing.Size(sheet.Width, sheet.Height);
            mainPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            mainPanel.MouseWheel += new System.Windows.Forms.MouseEventHandler(Panel6_MouseWheel);
            Optimization.countWithoutSensors = Data.Buses.Count;
            matrix.MatrixCreate();
            hint.Visible = false;
            r.ch.Titles.Add(MainStrings.report);
            r.ch.Series[ReportCount].LegendText = "1";
        }
        //class jopa
        public void AnimationSettings()
        {

            AnimationBitmap = new Bitmap(sheet.Width, sheet.Height);
            AnimationBitmap.MakeTransparent();
            AnimationBox = new PictureBox
            {
                Image = AnimationBitmap
            };
            AnimationGraphics = Graphics.FromImage(AnimationBitmap);
            sheet.Controls.Add(AnimationBox);
            AnimationBox.SizeMode = sheet.SizeMode;
            AnimationBox.Location = new Point(0, 0);
            AnimationBox.BackColor = Color.Transparent;
            AnimationBox.Size = sheet.Size;
            AnimationBox.MouseClick += Sheet_MouseClick_1;
        }

    }
}
