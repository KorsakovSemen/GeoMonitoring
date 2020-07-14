//This is a personal academic project. Dear PVS-Studio, please check it.
//PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using MetroFramework;
using MetroFramework.Controls;
using MetroFramework.Forms;
using Newtonsoft.Json;
using SystAnalys_lr1.Classes;
using SystAnalys_lr1.Forms;
using SystAnalys_lr1.Strings;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Serialization;

namespace SystAnalys_lr1
{
    public partial class Main : MetroForm
    {
        public enum ElementConstructorType
        {
            None,
            TrafficLight,
            BusStops,
            VertexAndEdge,
            All,
            TheBuses,
            Station
        }

        delegate void Del(string text);
        delegate void DelInt(int text);
        delegate void DelBool(bool text);
        delegate void DelBmp(Bitmap bmp);

        private ToolStripButton deleteBus;
        private ToolStripButton deleteRoute;
        private ToolStripButton delAllBusesOnRoute;


        PictureBox AnimationBox;
        Graphics AnimationGraphics;
        Bitmap AnimationBitmap;
        Coordinates coordinates;
        Report report;
        CrossroadsSettings crossSettings;
        AddRoute addR;
        AddGrid addG;
        public EpicSettings EpSet { get; set; }
        LoadingForm loadingForm;
        readonly Constructor c = new Constructor();

        public static string Average { get; set; }
        public static ElementConstructorType DelType { get; set; }

        public static string SelectedRoute { get; set; }
        public static int FirstCrossRoads { get; set; } = 0;
        public static int SecondCrossRoads { get; set; } = 0;
        public static int FirstCrossRoadsGreenLight { get; set; } = 0;
        public static int FirstCrossRoadsRedLight { get; set; } = 0;


        string savepath;
        public static Classes.Grid Grid { get; set; }
        public static DrawGraph G { get; set; }

        public static int Selected1 { get; set; } //выбранные вершины, для соединения линиями
        public static int Selected2 { get; set; }

        bool lang = false;

        Image saveImage;
        readonly Random rnd = new Random();

        //вторая форма
        static public DisplayEpicenters Ep { get; set; }

        int wsheet;
        int hsheet;
        static public Image GlobalMap { get; set; }
        static public int zoom, scrollX, scrollY;
        public static bool yes;

        static public int ReportCount { get; set; }
        int oldChart;

        static public string GlobalDel { get; set; } = "All";
        public static int EpicSizeParam { get; set; } = 25;
        public static string SaveF { get; set; } = "xml";
        List<int> selected = new List<int>();
        static public int RefreshLights { get; set; } = 0;
        public static bool Flag { get; set; } = false;
        public static bool TFCheck { get => tFCheck; set => tFCheck = value; }

        public Panel GetMainPanel()
        {
            return panelSettings;
        }

        public PictureBox GetSheet()
        {
            return sheet;
        }

        public Main()
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.Language))
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo(Properties.Settings.Default.Language);
                System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo(Properties.Settings.Default.Language);
            }
            InitializeComponent();
            InitializeElements();
            LoadSettings();
            AnimationSettings();
        }


        private void Panel6_MouseWheel(object sender, MouseEventArgs e)
        {
            Bus.ScrollX = mainPanel.AutoScrollPosition.X;
            Bus.ScrollY = mainPanel.AutoScrollPosition.Y;
        }
        private void Panel6_Scroll(object sender, ScrollEventArgs e)
        {
            Bus.ScrollX = mainPanel.AutoScrollPosition.X;
            Bus.ScrollY = mainPanel.AutoScrollPosition.Y;
        }

        private void Panel6_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.ScaleTransform(10, 10);
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            AddRoute f = new AddRoute
            {
                Owner = this
            };
            f.ShowDialog();
            Ep.ERefreshRouts();
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            G.ClearSheet();
            G.DrawALLGraph(Data.V, Data.E);
            sheet.Image = G.GetBitmap();
            Selected1 = -1;
            GridCreator.DrawGrid(sheet);
        }

        private void AddBus_Click(object sender, EventArgs e)
        {
            addBus.Enabled = false;
            deleteBus.Enabled = true;
            selectButton.Enabled = true;
            drawVertexButton.Enabled = false;
            drawEdgeButton.Enabled = true;
            deleteButton.Enabled = true;
            allBusSettings.Enabled = true;
            stopPointButton.Enabled = true;
            trafficLightLabel.Visible = false;
            selectRoute.Enabled = true;
            addTraficLight.Enabled = false;
            sheet.Image = G.GetBitmap();
            Selected1 = -1;
            selected = new List<int>();
            GridCreator.DrawGrid(sheet);
        }

        private void ButtonOn()
        {
            changeRoute.Invoke(new DelBool((s) => changeRoute.Enabled = s), true);
            optimize.Invoke(new DelBool((s) => optimize.Enabled = s), true);
            launchBuses.Invoke(new DelBool((s) => launchBuses.Enabled = s), true);
            stopBuses.Invoke(new DelBool((s) => launchBuses.Enabled = s), true);
            pollutionOptions.Invoke(new DelBool((s) => pollutionOptions.Enabled = s), true);
            toolStripMenu.Invoke((System.Action)(() =>
            {
                saveButton.Enabled = true;
                loadButton.Enabled = true;
            }));
        }

        private void ButtonOff()
        {
            changeRoute.Invoke(new DelBool((s) => changeRoute.Enabled = s), false);
            optimize.Invoke(new DelBool((s) => optimize.Enabled = s), false);
            launchBuses.Invoke(new DelBool((s) => launchBuses.Enabled = s), false);
            stopBuses.Invoke(new DelBool((s) => launchBuses.Enabled = s), false);
            pollutionOptions.Invoke(new DelBool((s) => pollutionOptions.Enabled = s), false);
            toolStripMenu.Invoke((System.Action)(() =>
            {
                saveButton.Enabled = false;
                loadButton.Enabled = false;
                selectButton.Enabled = false;
                drawVertexButton.Enabled = false;
                drawEdgeButton.Enabled = false;
                deleteButton.Enabled = false;
                deleteALLButton.Enabled = false;
                deleteRoute.Enabled = false;
                addBus.Enabled = false;
                deleteBus.Enabled = false;
                delAllBusesOnRoute.Enabled = false;
                stopPointButton.Enabled = false;
                addTraficLight.Enabled = false;
                selectRoute.Enabled = false;
            }));

        }

        private void ConstructorPressButton()
        {
            sheet.Image = G.GetBitmap();
            stopPointButton.Enabled = true;
            selected = new List<int>();
            trafficLightLabel.Visible = false;
            GridCreator.DrawGrid(sheet);
            CheckBusesOnRoute();
        }

        private void DelBus()
        {
            deleteBus.Enabled = false;
            addBus.Enabled = true;
            selectButton.Enabled = true;
            drawVertexButton.Enabled = false;
            drawEdgeButton.Enabled = true;
            deleteButton.Enabled = true;
            allBusSettings.Enabled = false;
            addTraficLight.Enabled = false;
            selectRoute.Enabled = true;
            Selected1 = -1;
            c.MapUpdateRoute(sheet, Data.Routes[changeRoute.Text], Data.RoutesEdge[changeRoute.Text]);
            ConstructorPressButton();
        }

        public static Image ResizeBitmap(Image sourceBMP, int width, int height)
        {

            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(sourceBMP, 0, 0, width, height);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            }
            return result;

        }

        public MetroTrackBar GetTrackBar()
        {
            return zoomBar;
        }

        public static string NormalizePath(string path)
        {
            return Path.GetFullPath(new Uri(path).LocalPath)
                       .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                       .ToUpperInvariant();
        }

        private void SaveTool()
        {
            try
            {
                if (sheet.Image != null)
                {
                    string date = DateTime.Now.ToShortDateString() + ":-:" + DateTime.Now.ToLocalTime();
                    if (savepath != null)
                    {
                        if (!File.Exists(savepath + "/Map.png"))
                            saveImage.Save(savepath + "/Map.png", System.Drawing.Imaging.ImageFormat.Png);
                        SaveRoutes(SaveF, savepath + @"\");
                        BringToFront();
                        SavedVisible();
                    }
                    else
                    {

                        using (var dialog = new FolderBrowserDialog())
                        {
                            dialog.SelectedPath = System.Windows.Forms.Application.StartupPath;
                            if (dialog.ShowDialog() == DialogResult.OK)
                            {
                                string path = dialog.SelectedPath;
                                File.WriteAllText("../../SaveConfig/save.txt", string.Empty);
                                savepath = path + @"\" + string.Format("{0}_{1}_{2}_{3}_{4}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, DateTime.Now.Hour, DateTime.Now.Minute);
                                Directory.CreateDirectory(savepath);
                                saveImage.Save(savepath + "/Map.png", System.Drawing.Imaging.ImageFormat.Png);
                                SaveRoutes(SaveF, savepath + @"\");
                                using (StreamWriter fileV = new StreamWriter("../../SaveConfig/save.txt"))
                                {
                                    fileV.WriteLine(savepath.ToString());
                                }
                                BringToFront();
                                SavedVisible();

                            }
                        }
                    }
                    config.Text = MainStrings.config + savepath;
                    stopPointButton.Enabled = true;
                }

            }
            catch (Exception exc)
            {
                StackTrace stackTrace = new StackTrace(exc, true);
                if (stackTrace.FrameCount > 0)
                {
                    MetroMessageBox.Show(this, $"{exc.StackTrace}", MainStrings.error, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SavedVisible();
            SaveTool();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            yes = false;
            DeleteForm df = new DeleteForm();
            StyleManager.Clone(df);
            if (changeRoute.Text == MainStrings.network)
            {
                df.theBuses.Enabled = false;
                allBusSettings.Enabled = false;
                addBus.Enabled = false;
                deleteBus.Enabled = false;
                drawEdgeButton.Enabled = true;
                selectButton.Enabled = true;
                drawVertexButton.Enabled = true;
                deleteButton.Enabled = false;
                addTraficLight.Enabled = true;
                CheckBuses();
            };
            if (changeRoute.SelectedIndex > 1)
            {
                df.theBuses.Enabled = true;
                allBusSettings.Enabled = false;
                addBus.Enabled = true;
                deleteBus.Enabled = true;
                drawEdgeButton.Enabled = true;
                selectButton.Enabled = true;
                drawVertexButton.Enabled = false;
                deleteButton.Enabled = false;
                addTraficLight.Enabled = false;
                G.DrawALLGraph(Data.Routes[(changeRoute.Text)], Data.RoutesEdge[(changeRoute.Text)], 1);
                CheckBusesOnRoute();
            }
            df.ShowDialog();
            trafficLightLabel.Text = GlobalDel;
            selectRoute.Enabled = true;
            stopPointButton.Enabled = true;
            ConstructorPressButton();
            trafficLightLabel.Visible = true;
            if (DelType == ElementConstructorType.None)
            {
                deleteButton.Enabled = true;
                trafficLightLabel.Visible = false;
            }

        }


        private void DrawVertexButton_Click(object sender, EventArgs e)
        {
            SelectVertex selectVertex = new SelectVertex();
            StyleManager.Clone(selectVertex);
            deleteBus.Enabled = false;
            addBus.Enabled = false;
            allBusSettings.Enabled = false;
            drawVertexButton.Enabled = false;
            selectButton.Enabled = true;
            drawEdgeButton.Enabled = true;
            deleteButton.Enabled = true;
            selectRoute.Enabled = true;
            stopPointButton.Enabled = true;
            trafficLightLabel.Visible = false;
            addTraficLight.Enabled = true;
            G.ClearSheet();
            G.DrawALLGraph(Data.V, Data.E);
            ConstructorPressButton();
            selectVertex.ShowDialog();
            trafficLightLabel.Text = GlobalDel;
            selectRoute.Enabled = true;
            stopPointButton.Enabled = true;
            ConstructorPressButton();
            trafficLightLabel.Visible = true;
            if (DelType == ElementConstructorType.None)
            {
                drawVertexButton.Enabled = true;
                trafficLightLabel.Visible = false;
            }

        }

        private void DeleteRoute_Click(object sender, EventArgs e)
        {
            try
            {
                LoadingForm loadingForm = new LoadingForm();
                loadingForm.loading.Value = 0;
                loadingForm.loading.Maximum = 100;
                allBusSettings.Enabled = false;
                addBus.Enabled = true;
                selectButton.Enabled = true;
                drawVertexButton.Enabled = true;
                drawEdgeButton.Enabled = true;
                deleteButton.Enabled = true;
                string message = MainStrings.deleteGraph;
                string caption = MainStrings.delete;
                var MBSave = MetroMessageBox.Show(this, message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (Data.Routes != null && Data.RoutesEdge != null && Data.E != null && Data.V != null && Data.Buses != null)
                {
                    if (MBSave == DialogResult.Yes && changeRoute.Text != "" && changeRoute.Text != MainStrings.network)
                    {
                        loadingForm.Show();
                        List<Bus> busTest = new List<Bus>();
                        loadingForm.loading.Value = 20;
                        foreach (var b in Data.Buses)
                        {
                            if (b.Route == changeRoute.Text)
                            {
                                busTest.Add(b);
                            };
                        };
                        loadingForm.loading.Value = 40;
                        foreach (var b in busTest)
                        {
                            Data.Buses.Remove(b);
                        }

                        Data.Routes.Remove(changeRoute.Text);
                        Data.RoutesEdge.Remove(changeRoute.Text);
                        Data.AllCoordinates.Remove(changeRoute.Text);
                        AddInComboBox();
                        changeRoute.Text = changeRoute.Items[0].ToString();
                        G.ClearSheet();
                        G.DrawALLGraph(Data.V, Data.E);
                        sheet.Image = G.GetBitmap();
                        GridCreator.DrawGrid(sheet);
                        loadingForm.loading.Value = 60;
                    }
                    else
                    {
                        loadingForm.close = true;
                        loadingForm.Close();
                    }
                    if (MBSave == DialogResult.Yes && changeRoute.Text == MainStrings.network)
                    {
                        loadingForm = new LoadingForm();
                        loadingForm.Show();
                        loadingForm.loading.Value = 20;
                        Data.Buses.Clear();
                        addBus.Enabled = false;
                        deleteBus.Enabled = false;
                        loadingForm.loading.Value = 40;
                        Data.V.Clear();
                        Data.E.Clear();
                        addTraficLight.Enabled = true;
                        Data.Routes.Clear();
                        Data.RoutesEdge.Clear();
                        AddInComboBox();
                        Data.AllCoordinates.Clear();
                        G.ClearSheet();
                        G.DrawALLGraph(Data.V, Data.E);
                        ConstructorPressButton();
                        loadingForm.loading.Value = 60;
                    }
                    else
                    {
                        loadingForm.close = true;
                        loadingForm.Close();
                    }
                }
                if (changeRoute.Text == MainStrings.network)
                {
                    deleteBus.Enabled = false;
                    addBus.Enabled = false;
                    drawVertexButton.Enabled = true;
                    addTraficLight.Enabled = true;
                };
                loadingForm.loading.Value = 70;
                trafficLightLabel.Visible = false;
                selectRoute.Enabled = true;
                selected = new List<int>();
                stopPointButton.Enabled = true;
                Ep.ERefreshRouts();
                loadingForm.loading.Value = 85;
                loadingForm.loading.Value = 100;
                loadingForm.close = true;
                loadingForm.Close();
                BringToFront();
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

        private void NewModelToolStripMenuItem_Click(object sender, EventArgs e)
        {

            OpenFileDialog fb = new OpenFileDialog
            {
                FilterIndex = 1,
                Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png"
            };
            if (fb.ShowDialog() == DialogResult.OK)
            {
                timer.Stop();
                savepath = null;
                if (Ep != null)
                {
                    Ep.EG = new DrawGraph();
                    Ep.Close();

                }
                config.Text = MainStrings.config;
                DeleteAll();
                G.Bitmap = null;


                sheet.Image = Image.FromFile(fb.FileName);
                saveImage = sheet.Image;
                zoomBar.Value = 1;
                wsheet = sheet.Width;
                hsheet = sheet.Height;
                GlobalMap = sheet.Image;
                G.SetBitmap();
                GridCreator.CreateGrid(sheet);
                AnimationBitmap = new Bitmap(sheet.Width, sheet.Height);
                AnimationBox.Image = AnimationBitmap;

                Modeling.CreatePollutionInRoutes();
                AddInComboBox();
                Ep = new DisplayEpicenters(this);
                StyleManager.Clone(Ep);
                Ep.Show();

                openEpicFormToolStripMenuItem.Enabled = true;
                addRouteToolStripMenuItem.Enabled = true;
                createGridToolStripMenuItem.Enabled = true;
                matrix.MatrixCreate();
                BringToFront();
                timer.Dispose();
                timer.Start();
                MetroMessageBox.Show(this, "", MainStrings.done, MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        private void TextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }


        private void DeleteALLButton_Click(object sender, EventArgs e)
        {
            try
            {
                int index = changeRoute.SelectedIndex;
                string message = MainStrings.clearGraph;
                string caption = MainStrings.delete;
                yes = false;
                DeleteForm df = new DeleteForm();
                StyleManager.Clone(df);
                df.All.Text = MainStrings.clearAll.Trim(new Char[] { ':' });
                df.deleteTypes.Text = MainStrings.clearAll;
                df.ShowDialog();

                DialogResult MBSave = DialogResult.No;
                if (yes)
                    MBSave = MetroMessageBox.Show(this, message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                else
                    return;
                switch (DelType)
                {
                    case ElementConstructorType.All:
                        if (MBSave == DialogResult.Yes && changeRoute.Text != MainStrings.network)
                        {
                            LoadingVisible();
                            List<Bus> busTest = new List<Bus>();
                            Data.Routes.Remove(changeRoute.Text);
                            Data.RoutesEdge.Remove(changeRoute.Text);
                            Data.AllCoordinates.Remove(changeRoute.Text);
                            Data.StopPoints.Remove(changeRoute.Text);
                            Data.StopPointsInGrids.Remove(changeRoute.Text);

                            foreach (var b in Data.Buses)
                            {
                                if (b.Route == changeRoute.Text)
                                {
                                    busTest.Add(b);
                                };
                            };

                            foreach (var b in busTest)
                            {
                                Data.Buses.Remove(b);
                            }

                            G.ClearSheet();
                            G.DrawALLGraph(Data.V, Data.E);
                            sheet.Image = G.GetBitmap();
                            AnimationBitmap = new Bitmap(sheet.Width, sheet.Height);
                            AnimationBox.Image = AnimationBitmap;

                        }
                        if (MBSave == DialogResult.Yes && changeRoute.Text == MainStrings.network)
                        {
                            LoadingVisible();
                            Data.Buses.Clear();
                            Data.Routes.Keys.ToList().ForEach(x => Data.Routes[x] = new List<Vertex>());
                            Data.RoutesEdge.Keys.ToList().ForEach(x => Data.RoutesEdge[x] = new List<Edge>());
                            Data.AllCoordinates.Clear();
                            Data.V.Clear();
                            Data.E.Clear();
                            G.ClearSheet();
                            G.DrawALLGraph(Data.V, Data.E);
                            sheet.Image = G.GetBitmap();
                            AnimationBitmap = new Bitmap(sheet.Width, sheet.Height);
                            AnimationBox.Image = AnimationBitmap;
                        };
                        break;
                    case ElementConstructorType.VertexAndEdge:
                        if (MBSave == DialogResult.Yes && changeRoute.Text != MainStrings.network)
                        {
                            LoadingVisible();
                            List<Bus> busTest = new List<Bus>();
                            Data.Routes[changeRoute.Text].Clear();
                            Data.RoutesEdge[changeRoute.Text].Clear();
                            foreach (var b in Data.Buses)
                            {
                                if (b.Route == changeRoute.Text)
                                {
                                    busTest.Add(b);
                                };
                            };
                            foreach (var b in busTest)
                            {
                                Data.Buses.Remove(b);
                            }
                            Data.AllCoordinates[changeRoute.Text].Clear();
                            G.ClearSheet();
                            G.DrawALLGraph(Data.V, Data.E);
                            sheet.Image = G.GetBitmap();
                            AnimationBitmap = new Bitmap(sheet.Width, sheet.Height);
                            AnimationBox.Image = AnimationBitmap;

                        }
                        if (MBSave == DialogResult.Yes && changeRoute.Text == MainStrings.network)
                        {
                            LoadingVisible();
                            Data.Buses.Clear();
                            Data.Routes.Keys.ToList().ForEach(x => Data.Routes[x] = new List<Vertex>());
                            Data.RoutesEdge.Keys.ToList().ForEach(x => Data.RoutesEdge[x] = new List<Edge>());
                            Data.AllCoordinates.Clear();
                            G.ClearSheet();
                            G.DrawALLGraph(Data.V, Data.E);
                            sheet.Image = G.GetBitmap();
                            AnimationBitmap = new Bitmap(sheet.Width, sheet.Height);
                            AnimationBox.Image = AnimationBitmap;
                        };
                        break;
                    case ElementConstructorType.BusStops:
                        if (MBSave == DialogResult.Yes && changeRoute.Text != MainStrings.network)
                        {
                            LoadingVisible();
                            Data.StopPoints[changeRoute.Text].Clear();
                            Data.StopPointsInGrids[changeRoute.Text].Clear();
                        }
                        if (MBSave == DialogResult.Yes && changeRoute.Text == MainStrings.network)
                        {
                            LoadingVisible();
                            Data.AllstopPoints.Clear();
                            Data.StopPoints.Clear();
                            Data.StopPointsInGrids.Clear();
                        }
                        break;
                    case ElementConstructorType.TrafficLight:
                        if (MBSave == DialogResult.Yes)
                        {
                            LoadingVisible();
                            foreach (var tf in Data.TraficLights)
                                tf.Stop();
                            Data.TraficLights.Clear();
                            Data.TraficLightsInGrids.Clear();
                        }
                        break;
                    case ElementConstructorType.TheBuses:
                        if (MBSave == DialogResult.Yes)
                        {
                            LoadingVisible();
                            DelAllBus();
                        }
                        break;
                }

                AddInComboBox();
                G.ClearSheet();
                sheet.Image = G.GetBitmap();
                if (Data.V != null)
                    G.DrawALLGraph(Data.V, Data.E);
                GridCreator.DrawGrid(sheet);
                selectRoute.Enabled = true;
                trafficLightLabel.Visible = false;
                selected = new List<int>();
                stopPointButton.Enabled = true;
                if (!Ep.IsDisposed)
                {
                    Ep.ERefreshRouts();
                }
                changeRoute.SelectedIndex = index;
                if (changeRoute.Text == MainStrings.network)
                {
                    selectRoute.Enabled = true;
                    deleteBus.Enabled = true;
                    allBusSettings.Enabled = false;
                    drawEdgeButton.Enabled = true;
                    selectButton.Enabled = true;
                    drawVertexButton.Enabled = true;
                    deleteButton.Enabled = true;
                    deleteALLButton.Enabled = true;
                    deleteRoute.Enabled = true;
                    addBus.Enabled = false;
                    deleteBus.Enabled = false;
                    stopPointButton.Enabled = true;
                    addTraficLight.Enabled = true;
                    CheckBuses();
                };
                if (changeRoute.SelectedIndex > 1)
                {
                    selectRoute.Enabled = true;
                    deleteBus.Enabled = true;
                    allBusSettings.Enabled = false;
                    drawEdgeButton.Enabled = true;
                    selectButton.Enabled = true;
                    drawVertexButton.Enabled = false;
                    deleteButton.Enabled = true;
                    deleteALLButton.Enabled = true;
                    deleteRoute.Enabled = true;
                    addBus.Enabled = true;
                    deleteBus.Enabled = true;
                    stopPointButton.Enabled = true;
                    addTraficLight.Enabled = true;
                    if (Data.Routes.ContainsKey(changeRoute.Text))
                        G.DrawALLGraph(Data.Routes[changeRoute.Text], Data.RoutesEdge[changeRoute.Text], 1);
                    CheckBusesOnRoute();
                }
                BringToFront();

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


        private void SelectButton_Click(object sender, EventArgs e)
        {
            if (changeRoute.Text == MainStrings.network)
            {
                addBus.Enabled = false;
                allBusSettings.Enabled = false;
                selectButton.Enabled = false;
                drawVertexButton.Enabled = true;
                drawEdgeButton.Enabled = true;
                addTraficLight.Enabled = true;
                CheckBuses();
            };
            if (changeRoute.SelectedIndex > 1)
            {
                addBus.Enabled = true;
                deleteBus.Enabled = true;
                allBusSettings.Enabled = false;
                selectButton.Enabled = false;
                drawVertexButton.Enabled = false;
                drawEdgeButton.Enabled = true;
                addTraficLight.Enabled = false;
                CheckBusesOnRoute();
            }
            trafficLightLabel.Visible = false;
            selectRoute.Enabled = true;
            stopPointButton.Enabled = true;
            Selected1 = -1;
            ConstructorPressButton();
        }

        public int? GetKeyByValue(int? value)
        {
            foreach (var recordOfDictionary in Optimization.PercentMean)
            {
                if (recordOfDictionary.Value.Equals(value))
                    return recordOfDictionary.Key;
            }
            return null;
        }


        private async void Optimize_ClickAsync(object sender, EventArgs e)
        {
            if (Data.Buses != null && optText.Text != "" && speed.Text != "" && Data.Buses.Count != 0 && int.Parse(optText.Text) > 0 && int.Parse(speed.Text) > 0)
            {
                c.AddStopPointInRoutes(sheet, Data.TheGrid);
                c.AddGridPart(Data.TraficLights, Data.TheGrid);
                report.Hide();
                coordinates.CreateAllCoordinates();
                Optimization.WithoutSensorsBuses = new List<int>();
                Optimization.CountWithoutSensors = Data.Buses.Where((bus) => bus.Tracker == true).Count();
                var busesparkreturn = Data.BusesPark;
                bool check = false;
                foreach (var bus in Data.Buses)
                {
                    if (bus.Tracker == true)
                    {
                        check = true;
                        break;
                    }
                }
                if (check)
                {
                    timer.Stop();
                    foreach (var tl in Data.TraficLights)
                        tl.TimerLight.Interval = 1;
                    Optimization.OptiCount = int.Parse(optText.Text);
                    Optimization.OptiSpeed = int.Parse(speed.Text);

                    ButtonOff();

                    matrix.MatrixCreate();
                    if (speed.Text != "" && int.TryParse(speed.Text, out int sp))
                    {
                        if (int.Parse(speed.Text) / 20 == 0)
                            Modeling.T = 1;
                        else
                            Modeling.T = int.Parse(speed.Text) / 20;
                    }
                    var style = msmMain.Style;

                    if (msmMain.Style == (MetroFramework.MetroColorStyle)Convert.ToInt32(13))
                        msmMain.Style = (MetroFramework.MetroColorStyle)Convert.ToInt32(14);
                    else
                        msmMain.Style = (MetroFramework.MetroColorStyle)Convert.ToInt32(13);

                    loadingForm = new LoadingForm
                    {
                        Theme = msmMain.Theme,
                        Style = msmMain.Style
                    };

                    if (!Ep.IsDisposed)
                    {
                        StyleManager.Clone(Ep);
                        Ep.Refresh();
                    }

                    if (EpicSettings.SavePictures == true)
                    {
                        Ep.Hide();
                        Directory.CreateDirectory(Optimization.PathOpt + "/Epics");
                    }

                    loadingForm.Show();
                    loadingForm.Refresh();

                    Application.OpenForms["LoadingForm"].Focus();

                    await Task.Run(() =>
                    {
                        Optimization.Opt(matrix, loadingForm);
                    });

                    if (Average == "Found")
                    {
                        mean.Text = MainStrings.average + " " + (Optimization.Min / 60 == 0 ? (Optimization.Min + " " + MainStrings.sec).ToString() : (Optimization.Min / 60 + " " + MainStrings.minute).ToString()) + " " + " - " + MainStrings.countSensors + ": " + Optimization.Result[0];
                    }
                    else
                    {
                        mean.Text = MainStrings.average + " " + MainStrings.notFound;
                    }

                    loadingForm.close = true;
                    loadingForm.Close();
                    loadingForm.Dispose();

                    matrix.MatrixCreate();
                    Optimization.ResMatrix(results);
                    Data.BusesPark = busesparkreturn;

                    msmMain.Style = style;

                    if (!Ep.IsDisposed)
                    {
                        StyleManager.Clone(Ep);
                        Ep.Refresh();
                    }
                    BringToFront();
                    timer.Start();
                    ButtonOn();
                    if (ReportCount == 0)
                        oldChart = (int)Optimization.PercentMean.Keys.Sum();
                    Optimization.ResChart(oldChart, report, StyleManager);

                    foreach (var tl in Data.TraficLights)
                        tl.TimerLight.Interval = 1000;
                    report.Show();
                    ConstructorOnNetwork();
                }

            }
        }

        public bool GetSavePictruesCheckBox()
        {
            return EpicSettings.SavePictures;
        }

        private void LoadFromToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                if (!Directory.Exists(savepath))
                {
                    dialog.SelectedPath = Path.GetFullPath("../../Configs/");
                }
                else
                {
                    dialog.SelectedPath = Path.GetFullPath(savepath);
                }
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(dialog.SelectedPath))
                        {
                            ToolStripMenuButtonOn();

                            LoadRoutes(dialog.SelectedPath + @"\");
                            LoadingVisible();
                            savepath = dialog.SelectedPath;

                            File.WriteAllText("../../SaveConfig/save.txt", string.Empty);
                            using (StreamWriter fileV = new StreamWriter("../../SaveConfig/save.txt"))
                            {
                                fileV.WriteLine(savepath.ToString());
                            }

                        }

                    }
                    catch (Exception exc)
                    {
                        StackTrace stackTrace = new StackTrace(exc, true);
                        if (stackTrace.FrameCount > 0)
                        {
                            MetroMessageBox.Show(this, $"{exc.StackTrace}", MainStrings.error, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        savepath = "";
                    }
                }
            }
        }
        public void ToolStripMenuButtonOn()
        {
            openEpicFormToolStripMenuItem.Enabled = true;
            addRouteToolStripMenuItem.Enabled = true;
            createGridToolStripMenuItem.Enabled = true;
        }

        public FolderBrowserDialog NewBasePath(FolderBrowserDialog dialog)
        {
            if (!Directory.Exists(savepath))
            {
                dialog.SelectedPath = Path.GetFullPath("../../Configs/");
            }
            else
            {
                dialog.SelectedPath = Path.GetFullPath(savepath);
            }
            return dialog;

        }

        private void LoadTool()
        {
            if (savepath != null && savepath.Length > 2 && Directory.Exists(savepath))
            {
                try
                {
                    ToolStripMenuButtonOn();
                    LoadRoutes(savepath + @"\");
                }
                catch (Exception exc)
                {
                    try
                    {
                        using (var dialog = new FolderBrowserDialog())
                        {
                            if (!Directory.Exists(savepath))
                            {
                                dialog.SelectedPath = Path.GetFullPath("../../Configs/");
                            }
                            else
                            {
                                dialog.SelectedPath = Path.GetFullPath(savepath);
                            }

                            if (dialog.ShowDialog() == DialogResult.OK)
                            {
                                if (!string.IsNullOrWhiteSpace(dialog.SelectedPath))
                                {
                                    ToolStripMenuButtonOn();

                                    LoadRoutes(dialog.SelectedPath + @"\");

                                    File.WriteAllText("../../SaveConfig/save.txt", string.Empty);
                                    savepath = dialog.SelectedPath;
                                    using (StreamWriter fileV = new StreamWriter("../../SaveConfig/save.txt"))
                                    {
                                        fileV.WriteLine(savepath.ToString());
                                    }

                                }
                            }
                        }
                    }
                    catch
                    {
                        StackTrace stackTrace = new StackTrace(exc, true);
                        if (stackTrace.FrameCount > 0)
                        {
                            MetroMessageBox.Show(this, $"{exc.StackTrace}", MainStrings.error, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        savepath = "";
                    }
                }
            }
            else
            {
                using (var dialog = new FolderBrowserDialog())
                {
                    if (!Directory.Exists(savepath))
                    {
                        dialog.SelectedPath = Path.GetFullPath("../../Configs/");
                    }
                    else
                    {
                        dialog.SelectedPath = Path.GetFullPath(savepath);
                    }
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        if (!string.IsNullOrWhiteSpace(dialog.SelectedPath))
                        {
                            ToolStripMenuButtonOn();

                            LoadRoutes(dialog.SelectedPath + @"\");
                            savepath = dialog.SelectedPath;
                            File.WriteAllText("../../SaveConfig/save.txt", string.Empty);

                            using (StreamWriter fileV = new StreamWriter("../../SaveConfig/save.txt"))
                            {
                                fileV.WriteLine(savepath);
                            }

                        }

                    }
                }
            }
            changeRoute.Text = MainStrings.network;
        }

        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadTool();
        }

        private void DrawEdgeButton_Click(object sender, EventArgs e)
        {
            G.ClearSheet();
            G.DrawALLGraph(Data.V, Data.E);
            if (changeRoute.Text == MainStrings.network)
            {
                allBusSettings.Enabled = false;
                addBus.Enabled = false;
                deleteBus.Enabled = false;
                drawEdgeButton.Enabled = false;
                selectButton.Enabled = true;
                drawVertexButton.Enabled = true;
                addTraficLight.Enabled = true;
                deleteButton.Enabled = true;
                CheckBuses();
            };
            if (changeRoute.SelectedIndex > 1)
            {
                allBusSettings.Enabled = false;
                addBus.Enabled = true;
                deleteBus.Enabled = true;
                drawEdgeButton.Enabled = false;
                selectButton.Enabled = true;
                drawVertexButton.Enabled = false;
                deleteButton.Enabled = true;
                addTraficLight.Enabled = false;
                G.DrawALLGraph(Data.Routes[changeRoute.Text], Data.RoutesEdge[changeRoute.Text], 1);
                CheckBusesOnRoute();
            }
            selectRoute.Enabled = true;
            trafficLightLabel.Visible = false;
            delAllBusesOnRoute.Enabled = true;
            stopPointButton.Enabled = true;
            sheet.Image = G.GetBitmap();
            Selected1 = -1;
            Selected2 = -1;
            selected = new List<int>();
            GridCreator.DrawGrid(sheet);
        }

        private void SaveRoutes(string saveFormat = "xml", string save = "../../Configs/")
        {
            try
            {
                if (saveFormat == "xml")
                {
                    Saver.SaveXML(save, loadingForm, saveFormat);
                }
                if (saveFormat == "json")
                {
                    Saver.SaveJSON(save, loadingForm, saveFormat);
                }

            }
            catch (Exception exc)
            {
                StackTrace stackTrace = new StackTrace(exc, true);
                if (stackTrace.FrameCount > 0)
                {
                    MetroMessageBox.Show(this, $"{exc.StackTrace}", MainStrings.error, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }

        private void DeleteAll()
        {
            if (Ep != null)
            {
                if (!Ep.IsDisposed)
                {
                    Ep.EG.ClearSheet2();
                    Ep.Dispose();
                    Ep.Close();
                }
            }

            foreach (var bus in Data.Buses)
            {
                bus.Coordinates.Clear();
                bus.Coordinates.TrimExcess();
                bus.BusPic.Dispose();
            }
            Data.Buses.Clear();


            foreach (var tl in Data.TraficLights)
            {
                tl.Stop();
            }

            Data.Staions.Clear();
            Data.Routes.Clear();
            Data.RoutesEdge.Clear();
            changeRoute.Items.Clear();
            Data.AllCoordinates.Clear();

            Data.AllstopPoints.Clear();
            Data.StopPoints.Clear();
            Data.StopPointsInGrids.Clear();

            Data.TraficLights.Clear();
            Data.TraficLights.TrimExcess();
            Data.TraficLightsInGrids.Clear();

            Data.V.Clear();
            Data.E.Clear();

            Data.Epics.Clear();
            AnimationClear();
        }

        private void LoadRoutes(string load)
        {
            try
            {
                DeleteAll();
                Loader.Load(load, loadingForm, sheet, timer);

                LoadOptions(load);

                matrix.MatrixCreate();
                BringToFront();

            }
            catch (Exception exc)
            {
                StackTrace stackTrace = new StackTrace(exc, true);
                if (stackTrace.FrameCount > 0)
                {
                    MetroMessageBox.Show(this, $"{exc.StackTrace}", MainStrings.error, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    throw new Exception();
                }

            }
        }

        private void LoadOptions(string load)
        {

            DisplayEpicenters.Path = load;
            sheet.Image = Image.FromFile(load + "/Map.png");
            saveImage = sheet.Image;
            zoomBar.Value = 1;
            wsheet = sheet.Width;
            hsheet = sheet.Height;
            ZoomHelper();
            GlobalMap = sheet.Image;
            G.SetBitmap();
            config.Text = MainStrings.config + load;
            openEpicFormToolStripMenuItem.Enabled = true;
            GridCreator.CreateGrid(sheet);
            Modeling.CreatePollutionInRoutes();
            ConstructorOnNetwork();
            AddInComboBox();
            G.ClearSheet();
            G.DrawALLGraph(Data.V, Data.E);
            sheet.Image = G.GetBitmap();
            GridCreator.DrawGrid(sheet);
            if (Ep != null)
                Ep.Close();
            Ep = new DisplayEpicenters(this);
            StyleManager.Clone(Ep);
            Ep.Show();

            if (Data.AllCoordinates.Count != 0)
                coordinates.CreateAllCoordinates();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            matrix.MatrixCreate();
        }


        public double GetDistance(double x1, double y1, double x2, double y2)
        {
            return (int)Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }


        private void AddTrafficLight(MouseEventArgs e)
        {
            if (FirstCrossRoads > 0 || SecondCrossRoads > 0)
            {
                if (FirstCrossRoads > 0)
                {
                    trafficLightLabel.Visible = true;
                    c.FirstTrafficLight(e, Data.TraficLights, sheet, Data.TheGrid);
                    trafficLightLabel.Text = MainStrings.putTrafficLights1 + " " + FirstCrossRoads.ToString();
                    if (FirstCrossRoads == 0)
                        trafficLightLabel.Text = MainStrings.putTrafficLights2 + " " + SecondCrossRoads.ToString();
                    return;
                }
                if (FirstCrossRoads <= 0 && SecondCrossRoads > 0)
                {
                    trafficLightLabel.Text = MainStrings.putTrafficLights2 + " " + SecondCrossRoads.ToString();
                    c.SecondTrafficLight(e, Data.TraficLights, sheet, Data.TheGrid);
                    trafficLightLabel.Text = MainStrings.putTrafficLights2 + " " + (SecondCrossRoads - 1).ToString();
                }
            }
            if (FirstCrossRoads <= 0 && SecondCrossRoads <= 0)
            {
                trafficLightLabel.Visible = false;
                Data.TraficLights.ForEach((tl) =>
                {
                    tl.Set();
                    tl.Start();
                });
                SelectedRoute = null;
                selectRoute.Enabled = true;
                deleteBus.Enabled = true;
                allBusSettings.Enabled = false;
                drawEdgeButton.Enabled = true;
                selectButton.Enabled = true;
                drawVertexButton.Enabled = true;
                deleteButton.Enabled = true;
                deleteALLButton.Enabled = true;
                deleteRoute.Enabled = true;
                addBus.Enabled = false;
                deleteBus.Enabled = false;
                stopPointButton.Enabled = true;
                addTraficLight.Enabled = true;
            }
        }


        private void Sheet_MouseClick_1(object sender, MouseEventArgs e)
        {
            if (changeRoute.Text == MainStrings.network)
            {
                if (selectRoute.Enabled == false)
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        c.MapUpdateNetwork(sheet, Data.V, Data.E);
                        selected = new List<int>();
                        return;
                    }
                    bool check = false;
                    check = c.CheckV(e, check);
                    c.SelectRoute(e, Data.V, Data.E, sheet, c, selected, check);
                }
                if (addTraficLight.Enabled == false)
                {
                    AddTrafficLight(e);
                    return;
                }
                if (stopPointButton.Enabled == false)
                {
                    c.AddStopPoints(e, Data.AllstopPoints, sheet, Data.TheGrid);
                    return;
                }
                if (selectButton.Enabled == false)
                {
                    c.Select(e, Data.V, Data.E, sheet, 0);
                    return;
                }
                if (drawVertexButton.Enabled == false)
                {
                    switch (DelType)
                    {
                        case ElementConstructorType.Station:
                            G.DrawStation(e.X, e.Y, 2, new SolidBrush(Color.FromArgb(128, 178, 34, 34)));
                            Data.Staions.Add(new Vertex(e.X, e.Y));
                            break;
                        case ElementConstructorType.VertexAndEdge:
                            c.DrawVertex(e, Data.V, sheet);
                            break;
                    }
                    return;
                }
                if (drawEdgeButton.Enabled == false)
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        c.MapUpdateNetwork(sheet, Data.V, Data.E);
                        Selected1 = -1;
                        Selected2 = -1;
                        return;
                    }
                    c.DrawEdge(e, Data.V, Data.E, sheet);
                    return;
                }
                if (deleteButton.Enabled == false)
                {
                    switch (DelType)
                    {
                        case ElementConstructorType.All:
                            c.AsDelete(e, Data.V, Data.E, sheet, Data.RoutesEdge);
                            break;
                        case ElementConstructorType.BusStops:
                            c.DeleteBS(e, Data.V, Data.E, sheet, Data.RoutesEdge);
                            break;
                        case ElementConstructorType.TrafficLight:
                            c.DeleteTF(e, Data.V, Data.E, sheet, Data.RoutesEdge);
                            break;
                        case ElementConstructorType.VertexAndEdge:
                            c.DeleteVE(e, Data.V, Data.E, sheet, Data.RoutesEdge);
                            break;
                    }
                    c.MapUpdateNetwork(sheet, Data.V, Data.E);
                }
                return;
            }

            if (changeRoute.SelectedIndex > 1)
            {
                List<Vertex> routeV = Data.Routes[changeRoute.Text];
                if (stopPointButton.Enabled == false)
                {
                    c.AddStopPointsInRoute(e, Data.AllstopPoints, sheet, Data.TheGrid, changeRoute.Text);
                    return;
                }
                if (selectButton.Enabled == false)
                {
                    c.Select(e, routeV, Data.RoutesEdge[changeRoute.Text], sheet, 1);
                    return;
                }
                if (selectRoute.Enabled == false)
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        c.MapUpdateRoute(sheet, routeV, Data.RoutesEdge[changeRoute.Text]);
                        selected = new List<int>();
                        return;
                    }
                    c.SelectRouteInRoute(e, routeV, Data.RoutesEdge[changeRoute.Text], sheet, selected);
                    return;
                }
                if (addBus.Enabled == false)
                {
                    try
                    {
                        c.AddBus(e, trackerCheck.Checked, backsideCheck.Checked, changeRoute.Text);
                    }
                    catch
                    {
                        MetroMessageBox.Show(this, MainStrings.bus);
                    }

                }
                if (deleteBus.Enabled == false)
                {
                    c.DeleteBus(e, routeV, Data.RoutesEdge[changeRoute.Text], sheet, changeRoute.Text, mainPanel.AutoScrollPosition.X, mainPanel.AutoScrollPosition.Y);
                    return;
                }
                if (drawEdgeButton.Enabled == false)
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        c.MapUpdateRoute(sheet, routeV, Data.RoutesEdge[changeRoute.Text]);
                        Selected1 = -1;
                        Selected2 = -1;
                        return;
                    }
                    c.DrawEdgeInRoute(e, routeV, Data.RoutesEdge[changeRoute.Text], sheet, changeRoute.Text);
                    return;
                }
                if (deleteButton.Enabled == false)
                {
                    switch (DelType)
                    {
                        case ElementConstructorType.All:
                            c.DeleteOnRoute(e, routeV, Data.RoutesEdge[changeRoute.Text], sheet, changeRoute.Text);
                            break;
                        case ElementConstructorType.BusStops:
                            c.DeleteStopsOnRoute(e, routeV, sheet, changeRoute.Text);
                            break;
                        case ElementConstructorType.TrafficLight:
                            c.DeleteTFOnRoute(e, routeV, Data.RoutesEdge[changeRoute.Text], sheet, Data.TraficLights);
                            break;
                        case ElementConstructorType.VertexAndEdge:
                            c.DeleteVandE(e, routeV, Data.RoutesEdge[changeRoute.Text], sheet);
                            break;
                        case ElementConstructorType.TheBuses:
                            c.DeleteBus(e, routeV, Data.RoutesEdge[changeRoute.Text], sheet, changeRoute.Text, mainPanel.AutoScrollPosition.X, mainPanel.AutoScrollPosition.Y);
                            break;
                    }
                    if (Flag)
                    {
                        c.MapUpdateRoute(sheet, routeV, Data.RoutesEdge[changeRoute.Text]);
                    }
                }
                coordinates.CreateOneRouteCoordinates(changeRoute.Text);
                return;
            }
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            if (G.Bitmap != null)
            {
                AddGrid f = new AddGrid
                {
                    Owner = this
                };
                f.ShowDialog();
                c.MapUpdate(sheet);
                GridCreator.DrawGrid(sheet);
                if (!Ep.IsDisposed)
                {
                    Ep.EDrawGrid();
                }

            }
        }

        private void GridButton_Click(object sender, EventArgs e)
        {
            if (changeRoute.Text == MainStrings.network)
            {
                selectRoute.Enabled = false;
                deleteBus.Enabled = false;
                addBus.Enabled = false;
                selectButton.Enabled = true;
                drawVertexButton.Enabled = true;
                drawEdgeButton.Enabled = true;
                deleteButton.Enabled = true;
                allBusSettings.Enabled = false;
                selectRoute.Enabled = true;
                addTraficLight.Enabled = true;
                CheckBuses();
            };
            if (changeRoute.SelectedIndex > 1)
            {
                selectRoute.Enabled = true;
                deleteBus.Enabled = true;
                addBus.Enabled = true;
                selectButton.Enabled = true;
                drawVertexButton.Enabled = false;
                drawEdgeButton.Enabled = true;
                deleteButton.Enabled = true;
                selectRoute.Enabled = true;
                allBusSettings.Enabled = false;
                addTraficLight.Enabled = false;
                CheckBusesOnRoute();
            }
            selected = new List<int>();
            selectRoute.Enabled = true;
            trafficLightLabel.Visible = false;
            delAllBusesOnRoute.Enabled = true;
            stopPointButton.Enabled = false;
        }
        private void DelAllBus()
        {
            CheckBuses();
            if (changeRoute.Text == MainStrings.network)
            {
                selectRoute.Enabled = false;
                deleteBus.Enabled = false;
                addBus.Enabled = false;
                selectButton.Enabled = true;
                selectRoute.Enabled = true;
                drawVertexButton.Enabled = true;
                drawEdgeButton.Enabled = true;
                deleteButton.Enabled = true;
                selectRoute.Enabled = true;
                allBusSettings.Enabled = false;
                stopPointButton.Enabled = true;
                addTraficLight.Enabled = true;
                Data.Buses.Clear();
                delAllBusesOnRoute.Enabled = false;

            };
            CheckBusesOnRoute();
            if (changeRoute.SelectedIndex > 1)
            {
                selectRoute.Enabled = true;
                deleteBus.Enabled = true;
                addBus.Enabled = true;
                selectButton.Enabled = true;
                drawVertexButton.Enabled = false;
                drawEdgeButton.Enabled = true;
                deleteButton.Enabled = true;
                selectRoute.Enabled = true;
                allBusSettings.Enabled = false;
                stopPointButton.Enabled = true;
                List<Bus> b = new List<Bus>();
                foreach (var bus in Data.Buses)
                {
                    if (bus.Route == (changeRoute.Text))
                    {
                        b.Add(bus);
                    }
                }
                //Parallel.ForEach(b, (bus) =>
                foreach(var bus in b)
                {
                    foreach (var B in Data.Buses)
                    {
                        if (B == bus)
                        {
                            Data.Buses.Remove(bus);
                            break;
                        }
                    }
                }//);
                b.Clear();
                delAllBusesOnRoute.Enabled = true;

            }
            trafficLightLabel.Visible = false;
            selected = new List<int>();
            AnimationBitmap = new Bitmap(sheet.Width, sheet.Height);
            AnimationBox.Image = AnimationBitmap;
        }

        private void CheckBuses()
        {
            if (Data.Buses.Count != 0 && changeRoute.Text == MainStrings.network)
            {
                delAllBusesOnRoute.Enabled = true;
            }
        }

        private void CheckBusesOnRoute()
        {
            foreach (var bus in Data.Buses)
            {
                if (bus.Route == (changeRoute.Text))
                {
                    delAllBusesOnRoute.Enabled = true;
                    break;
                }
            }
        }

        private void JSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sheet.Image != null)
            {
                using (var dialog = new FolderBrowserDialog())
                {
                    if (!Directory.Exists(savepath))
                    {
                        dialog.SelectedPath = Path.GetFullPath("../../Configs/");
                    }
                    else
                    {
                        dialog.SelectedPath = Path.GetFullPath(savepath);
                    }
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        SavedVisible();
                        string path = dialog.SelectedPath;
                        savepath = dialog.SelectedPath + @"\" + string.Format("{0}_{1}_{2}_{3}_{4}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, DateTime.Now.Hour, DateTime.Now.Minute);
                        File.WriteAllText("../../SaveConfig/save.txt", string.Empty);
                        using (StreamWriter fileV = new StreamWriter("../../SaveConfig/save.txt"))
                        {
                            fileV.WriteLine(savepath.ToString());
                        }
                        if (!Directory.Exists(savepath))
                        {
                            Directory.CreateDirectory(savepath);
                            SaveRoutes("json", savepath + @"\");
                            saveImage.Save(savepath + "/Map.png", System.Drawing.Imaging.ImageFormat.Png);
                        }
                        else
                        {
                            savepath += rnd.Next(0, 100).ToString();
                            Directory.CreateDirectory(savepath);
                            SaveRoutes("json", savepath + @"\");
                            saveImage.Save(savepath + "/Map.png", System.Drawing.Imaging.ImageFormat.Png);
                        }
                    }
                    config.Text = MainStrings.config + savepath;
                }
            }
            BringToFront();

        }

        private static bool tFCheck = false;

        private void AddTraficLight_Click(object sender, EventArgs e)
        {
            if (changeRoute.Text == MainStrings.network)
            {
                crossSettings = new CrossroadsSettings();
                this.StyleManager.Clone(crossSettings);
                crossSettings.ShowDialog();
                if (FirstCrossRoads != 0 && SecondCrossRoads != 0)
                {
                    selectRoute.Enabled = false;
                    deleteBus.Enabled = false;
                    addBus.Enabled = false;
                    selectButton.Enabled = true;
                    selectRoute.Enabled = true;
                    drawVertexButton.Enabled = true;
                    drawEdgeButton.Enabled = true;
                    deleteButton.Enabled = true;
                    allBusSettings.Enabled = false;
                    delAllBusesOnRoute.Enabled = false;
                    trafficLightLabel.Visible = true;
                    trafficLightLabel.Text = MainStrings.putTrafficLights1 + " " + FirstCrossRoads.ToString();
                    selected = new List<int>();
                    stopPointButton.Enabled = true;
                    addTraficLight.Enabled = false;
                    selectRoute.Enabled = true;

                }
                sheet.Image = G.GetBitmap();
                Selected1 = -1;
                GridCreator.DrawGrid(sheet);

            }
        }
        private void XMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sheet.Image != null)
            {
                using (var dialog = new FolderBrowserDialog())
                {
                    if (!Directory.Exists(savepath))
                    {
                        dialog.SelectedPath = Path.GetFullPath("../../Configs/");
                    }
                    else
                    {
                        dialog.SelectedPath = Path.GetFullPath(savepath);
                    }
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        SavedVisible();
                        string path = dialog.SelectedPath;
                        savepath = dialog.SelectedPath + @"\" + string.Format("{0}_{1}_{2}_{3}_{4}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, DateTime.Now.Hour, DateTime.Now.Minute);
                        File.WriteAllText("../../SaveConfig/save.txt", string.Empty);
                        using (StreamWriter fileV = new StreamWriter("../../SaveConfig/save.txt"))
                        {
                            fileV.WriteLine(savepath.ToString());
                        }
                        if (!Directory.Exists(savepath))
                        {
                            Directory.CreateDirectory(savepath);
                            SaveRoutes("xml", savepath + @"\");
                            saveImage.Save(savepath + "/Map.png", System.Drawing.Imaging.ImageFormat.Png);
                        }
                        else
                        {
                            savepath += rnd.Next(0, 100).ToString();
                            Directory.CreateDirectory(savepath);
                            SaveRoutes("xml", savepath + @"\");
                            saveImage.Save(savepath + "/Map.png", System.Drawing.Imaging.ImageFormat.Png);
                        }
                    }
                    config.Text = MainStrings.config + savepath;
                }
            }
            BringToFront();
        }

        private void SelectRoute_Click(object sender, EventArgs e)
        {
            if (changeRoute.Text == MainStrings.network)
            {
                selectRoute.Enabled = false;
                deleteBus.Enabled = false;
                addBus.Enabled = false;
                addTraficLight.Enabled = true;
                stopPointButton.Enabled = true;
                selectButton.Enabled = true;
                drawVertexButton.Enabled = true;
                drawEdgeButton.Enabled = true;
                deleteButton.Enabled = true;
                allBusSettings.Enabled = false;
                delAllBusesOnRoute.Enabled = false;
                addTraficLight.Enabled = true;
                CheckBuses();
            };
            if (changeRoute.SelectedIndex > 1)
            {
                selectRoute.Enabled = false;
                deleteBus.Enabled = true;
                addBus.Enabled = true;
                selectButton.Enabled = true;
                addTraficLight.Enabled = false;
                stopPointButton.Enabled = true;
                drawVertexButton.Enabled = false;
                drawEdgeButton.Enabled = true;
                deleteButton.Enabled = true;
                allBusSettings.Enabled = false;
                delAllBusesOnRoute.Enabled = true;
                CheckBusesOnRoute();
            }
            trafficLightLabel.Visible = false;
            stopPointButton.Enabled = true;
            sheet.Image = G.GetBitmap();
            Selected1 = -1;
            GridCreator.DrawGrid(sheet);
        }


        private void OpenEpicFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Ep.IsDisposed)
            {
                Ep.Close();

            }
            Ep = new DisplayEpicenters(this);
            StyleManager.Clone(Ep);
            Ep.Show();

        }

        private void AddRouteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addR = new AddRoute();
            this.StyleManager.Clone(addR);
            addR.ShowDialog();
            addR.Dispose();
            if (!Ep.IsDisposed)
            {
                Ep.ERefreshRouts();
            }
            if (addR.textBox1.Text != "")
            {
                if (!Data.Routes.ContainsKey(this.addR.textBox1.Text))
                {
                    Data.Routes.Add((this.addR.textBox1.Text), new List<Vertex>());
                    Data.RoutesEdge.Add((this.addR.textBox1.Text), new List<Edge>());
                    changeRoute.Items.Add(addR.textBox1.Text);
                    Data.StopPoints.Add((this.addR.textBox1.Text), new List<BusStop>());
                    changeRoute.SelectedIndex = changeRoute.Items.IndexOf(addR.textBox1.Text);
                }
            }
        }

        private void RunTrafficLightsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Data.TraficLights.ForEach((tl) =>
            {
                tl.Start();
            });
        }

        private void Themes_CheckedChanged(object sender, EventArgs e)
        {
            if (themes.Checked == true)
            {
                msmMain.Theme = MetroFramework.MetroThemeStyle.Dark;
                toolStripMenu.BackColor = Color.FromArgb(17, 17, 17);
                toolStripMenu.ForeColor = Color.FromArgb(153, 153, 153);
                selectButton.Image = new Bitmap(("../../Resources/newcursor_bt.png"));
                drawVertexButton.Image = new Bitmap(("../../Resources/CIRCLE_BT_.png"));
                drawEdgeButton.Image = new Bitmap(("../../Resources/line_new_bt.png"));
                selectRoute.Image = new Bitmap(("../../Resources/line-chart_bt.png"));
                addTraficLight.Image = new Bitmap("../../Resources/traffic-light.png");


            }
            else
            {
                msmMain.Theme = MetroFramework.MetroThemeStyle.Light;
                toolStripMenu.BackColor = Color.FromArgb(255, 255, 255);
                toolStripMenu.ForeColor = Color.FromArgb(0, 0, 0);
                selectButton.Image = new Bitmap(("../../Resources/newcursor.png"));
                drawVertexButton.Image = new Bitmap(("../../Resources/CIRCLE_WT.png"));
                drawEdgeButton.Image = new Bitmap(("../../Resources/new_line__.png"));
                selectRoute.Image = new Bitmap(("../../Resources/line-chart.png"));
                addTraficLight.Image = new Bitmap("../../Resources/traffic-light_.png");

            }
            using (StreamWriter fileV = new StreamWriter("../../SaveConfig/theme.txt"))
            {
                fileV.WriteLine(msmMain.Theme);
            }

            this.StyleManager.Clone(Ep);
            if (Ep != null)
            {
                Ep.Refresh();

            }
        }


        private void ChangeTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            msmMain.Style = (MetroFramework.MetroColorStyle)Convert.ToInt32(changeTheme.Items.IndexOf(changeTheme.Text));
            StyleManager.Clone(Ep);
            using (StreamWriter fileV = new StreamWriter("../../SaveConfig/style.txt"))
            {
                fileV.WriteLine(Convert.ToInt32(changeTheme.Items.IndexOf(changeTheme.Text)));
            }
            if (Ep != null)
            {
                Ep.Refresh();
            }

        }

        private void LaunchBuses_Click(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void StopBuses_Click(object sender, EventArgs e)
        {
            timer.Stop();
        }

        private async void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Language = changeLanguage.SelectedValue.ToString();
            Properties.Settings.Default.Save();
            await Task.Delay(1000);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            changeLanguage.DataSource = new System.Globalization.CultureInfo[]{
                 System.Globalization.CultureInfo.GetCultureInfo("ru-RU"),
                 System.Globalization.CultureInfo.GetCultureInfo("en-US")
            };

            changeLanguage.DisplayMember = "NativeName";
            changeLanguage.ValueMember = "Name";

            if (!string.IsNullOrEmpty(Properties.Settings.Default.Language))
            {
                changeLanguage.SelectedValue = Properties.Settings.Default.Language;
            }
            lang = true;
        }

        private void MetroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lang == true)
            {
                hint.Visible = true;
                hint.Text = MainStrings.hint;
                Application.Restart();

            }
        }

        private void ZoomHelper()
        {
            sheet.Image = ResizeBitmap(new Bitmap(saveImage), wsheet * zoomBar.Value, hsheet * zoomBar.Value);
            GlobalMap = sheet.Image;
            mainPanel.AutoScrollPosition = new Point(scrollX * zoomBar.Value, scrollY * zoomBar.Value);
            scrollX = mainPanel.AutoScrollPosition.X;
            scrollY = mainPanel.AutoScrollPosition.Y;
            zoom = zoomBar.Value;
            Bus.ScrollX = mainPanel.AutoScrollPosition.X;
            Bus.ScrollY = mainPanel.AutoScrollPosition.Y;
            Bus.ZoomCoef = zoomBar.Value;
        }

        private void MetroTrackBar1_ScrollAsync(object sender, ScrollEventArgs e)
        {
            if (sheet.Image != null && saveImage != null)
            {
                ZoomHelper();

                G.ClearSheet();
                if (changeRoute.SelectedIndex > 1)
                {
                    G.DrawALLGraph(Data.V, Data.E);
                    G.DrawALLGraph(Data.Routes[(changeRoute.Text)], Data.RoutesEdge[(changeRoute.Text)], 1);
                }
                else if (changeRoute.Text == MainStrings.none)
                {
                    G.ClearSheet();
                }
                else
                {
                    G.DrawALLGraph(Data.V, Data.E);
                }

                if (timer.Enabled == false)
                {
                    AnimationBitmap.Dispose();
                    AnimationBitmap = new Bitmap(sheet.Width, sheet.Height);
                    AnimationBitmap.MakeTransparent();
                    AnimationGraphics.Dispose();
                    AnimationGraphics = Graphics.FromImage(AnimationBitmap);
                    foreach (var bus in Data.Buses)
                    {

                        AnimationGraphics.DrawImage(bus.BusPic, bus.Coordinates[bus.PositionAt].X * zoomBar.Value - bus.BusPic.Width / 2, bus.Coordinates[bus.PositionAt].Y * zoomBar.Value - bus.BusPic.Height / 2);
                    }
                    AnimationBox.Image = AnimationBitmap;
                }
                sheet.Image = G.GetBitmap();
                GridCreator.DrawGrid(sheet);


            }
        }


        private void BusSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
        }

        private void OptText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void Speed_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void MetroButton1_Click_1(object sender, EventArgs e)
        {
            coordinates.AsyncCreateAllCoordinates();
        }


        private void MetroButton2_Click(object sender, EventArgs e)
        {
            EpSet = new EpicSettings();
            StyleManager.Clone(EpSet);
            EpSet.ShowDialog();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (Data.Buses.Any())
            {
                AnimationBitmap = new Bitmap(sheet.Width, sheet.Height);
                AnimationBitmap.MakeTransparent();

                AnimationGraphics = Graphics.FromImage(AnimationBitmap);
                foreach (var bus in Data.Buses)
                {
                    bus.MoveWithGraphics(AnimationGraphics);
                    AnimationBox.Image = AnimationBitmap;
                    if (Data.Buses.Count <= 20)
                    {
                        GC.Collect();
                    }

                }
            }
        }


        private void ReportTool_Click(object sender, EventArgs e)
        {
            report.Show();
            report.BringToFront();
        }

        private void StopBuses_Click_1(object sender, EventArgs e)
        {
            timer.Stop();
        }

        private void LaunchBuses_Click_1(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void ChangeRoute_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (changeRoute.Text == MainStrings.none)
            {
                SelectedRoute = null;
                deleteBus.Enabled = false;
                allBusSettings.Enabled = false;
                selectButton.Enabled = false;
                drawVertexButton.Enabled = false;
                drawEdgeButton.Enabled = false;
                deleteButton.Enabled = false;
                deleteALLButton.Enabled = false;
                addTraficLight.Enabled = false;
                stopPointButton.Enabled = false;
                deleteRoute.Enabled = false;
                addBus.Enabled = false;
                selectRoute.Enabled = false;
                delAllBusesOnRoute.Enabled = false;
                G.ClearSheet();
                G.DrawStopPoints();
                G.DrawTrafficLights();
                sheet.Image = G.GetBitmap();
                trafficLightLabel.Visible = false;
                selected = new List<int>();
                return;
            };
            if (changeRoute.Text == MainStrings.network)
            {
                SelectedRoute = null;
                selectRoute.Enabled = true;
                deleteBus.Enabled = true;
                allBusSettings.Enabled = false;
                drawEdgeButton.Enabled = true;
                selectButton.Enabled = true;
                drawVertexButton.Enabled = true;
                deleteButton.Enabled = true;
                deleteALLButton.Enabled = true;
                deleteRoute.Enabled = true;
                addBus.Enabled = false;
                deleteBus.Enabled = false;
                stopPointButton.Enabled = true;
                addTraficLight.Enabled = true;
                CheckBuses();
                G.ClearSheet();
                G.DrawALLGraph(Data.V, Data.E);
                trafficLightLabel.Visible = false;
                sheet.Image = G.GetBitmap();
                GridCreator.DrawGrid(sheet);
                Console.WriteLine(MainStrings.network);
                selected = new List<int>();
                return;
            };
            for (int i = 0; i < Data.Routes.Count; i++)
            {
                if (Data.Routes.ElementAt(i).Key == (changeRoute.Text))
                {
                    SelectedRoute = (changeRoute.Text);
                    selectRoute.Enabled = true;
                    selectButton.Enabled = true;
                    deleteBus.Enabled = true;
                    allBusSettings.Enabled = false;
                    drawVertexButton.Enabled = false;
                    drawEdgeButton.Enabled = true;
                    deleteButton.Enabled = true;
                    deleteALLButton.Enabled = true;
                    deleteRoute.Enabled = true;
                    addBus.Enabled = true;
                    stopPointButton.Enabled = true;
                    trafficLightLabel.Visible = false;
                    addTraficLight.Enabled = false;
                    CheckBusesOnRoute();
                    G.ClearSheet();
                    G.DrawALLGraph(Data.V, Data.E);
                    G.DrawALLGraph(Data.Routes[(changeRoute.Text)], Data.RoutesEdge[(changeRoute.Text)], 1);
                    sheet.Image = G.GetBitmap();
                    GridCreator.DrawGrid(sheet);
                    selected = new List<int>();
                    return;
                };
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            clearButton.Enabled = false;
            openEpicFormToolStripMenuItem.Enabled = false;
            addRouteToolStripMenuItem.Enabled = false;
            createGridToolStripMenuItem.Enabled = false;
            savepath = null;
            sheet.Image = null;
            DeleteAll();
            config.Text = MainStrings.config;
            if (G.GetBitmap() != null)
            {
                G.ClearSheet();
                G.ClearSheet2();
                G = new DrawGraph();
                GlobalMap.Dispose();

            }
            changeRoute.Items.Add(MainStrings.none);
            changeRoute.Items.Clear();
            changeRoute.Text = "";
            zoomBar.Value = 1;
            ConstructorOffNetwork();
            File.WriteAllText("../../SaveConfig/save.txt", string.Empty);
            BringToFront();
            timer.Stop();
            timer.Dispose();
            AnimationBox.Image = null;
            AnimationGraphics.Dispose();
            AnimationBitmap.Dispose();
            clearButton.Enabled = true;
            Refresh();
        }
        private void CreateGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (G.Bitmap != null)
            {
                addG = new AddGrid();
                StyleManager.Clone(addG);
                addG.ShowDialog();
                G.ClearSheet();
                if (!Ep.IsDisposed)
                {
                    Ep.EG.ClearSheet2();
                }
                G.DrawALLGraph(Data.V, Data.E);
                GridCreator.CreateGrid(sheet);

                sheet.Image = G.GetBitmap();
                GridCreator.DrawGrid(sheet);
                if (!Ep.IsDisposed)
                {
                    Ep.EDrawGrid();
                }

                coordinates.CreateAllCoordinates();
                Modeling.CreatePollutionInRoutes();
            }
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            if (G.Bitmap != null)
            {
                EpicSettings f = new EpicSettings
                {
                    Owner = this
                };
                f.ShowDialog();
            }
        }

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
            EpicSettings.MovingEpicParamet = new List<string>();
            KeyPreview = true;
            deleteBus = new ToolStripButton();
            deleteRoute = new ToolStripButton();
            delAllBusesOnRoute = new ToolStripButton();
            report = new Report();
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

        private async void SavedVisible()
        {
            saved.Visible = true;
            loadingSpinner.Visible = true;
            await Task.Delay(2500);
            saved.Visible = false;
            loadingSpinner.Visible = false;
        }

        private async void LoadingVisible()
        {
            loadingSpinner.Visible = true;
            await Task.Delay(2500);
            loadingSpinner.Visible = false;
        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                LoadingVisible();
                LoadTool();
                Application.OpenForms["Main"].Focus();
                e.SuppressKeyPress = true;
            }
            if (e.Control && e.KeyCode == Keys.S)
            {
                Application.OpenForms["Main"].Focus();
                e.SuppressKeyPress = true;
                try
                {
                    if (sheet.Image != null)
                    {
                        string date = DateTime.Now.ToShortDateString() + ":-:" + DateTime.Now.ToLocalTime();
                        if (savepath != null)
                        {
                            if (!File.Exists(savepath + "/Map.png"))
                                saveImage.Save(savepath + "/Map.png", System.Drawing.Imaging.ImageFormat.Png);
                            SaveRoutes(SaveF, savepath + @"\");
                            BringToFront();
                            SaveTool();
                        }
                        else
                        {

                            using (var dialog = new FolderBrowserDialog())
                            {
                                dialog.SelectedPath = System.Windows.Forms.Application.StartupPath;
                                if (dialog.ShowDialog() == DialogResult.OK)
                                {
                                    string path = dialog.SelectedPath;
                                    File.WriteAllText("../../SaveConfig/save.txt", string.Empty);
                                    savepath = path + @"\" + string.Format("{0}_{1}_{2}_{3}_{4}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, DateTime.Now.Hour, DateTime.Now.Minute);
                                    Directory.CreateDirectory(savepath);
                                    saveImage.Save(savepath + "/Map.png", System.Drawing.Imaging.ImageFormat.Png);
                                    SaveRoutes(SaveF, savepath + @"\");
                                    using (StreamWriter fileV = new StreamWriter("../../SaveConfig/save.txt"))
                                    {
                                        fileV.WriteLine(savepath.ToString());
                                    }
                                    BringToFront();
                                    SaveTool();
                                }
                            }
                        }
                        config.Text = MainStrings.config + savepath;
                        stopPointButton.Enabled = true;
                    }

                }
                catch (Exception exc)
                {
                    StackTrace stackTrace = new StackTrace(exc, true);
                    if (stackTrace.FrameCount > 0)
                    {
                        MetroMessageBox.Show(this, $"{exc.StackTrace}", MainStrings.error, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void LoadSettings()
        {
            if (File.Exists("../../SaveConfig/save.txt"))
            {
                using (FileStream fstream = File.OpenRead("../../SaveConfig/save.txt"))
                {
                    byte[] array = new byte[fstream.Length];
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
                    fstream.Read(array, 0, array.Length);
                    text = System.Text.Encoding.Default.GetString(array).Replace(Environment.NewLine, "");
                    msmMain.Style = (MetroFramework.MetroColorStyle)Convert.ToInt32(text);
                    Console.WriteLine($"Текст из файла: {savepath}");

                }
                changeTheme.SelectedIndex = Convert.ToInt32(text);
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
            Optimization.CountWithoutSensors = Data.Buses.Count;
            hint.Visible = false;
            report.ch.Titles.Add(MainStrings.report);
            report.ch.Series[ReportCount].LegendText = "1";
            foreach(var sp in Data.Routes)
            {
                if (!Data.StopPointsInGrids.ContainsKey(sp.Key))
                    Data.StopPointsInGrids.Add(sp.Key, new List<int>());
            }
        }

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
        public void AnimationClear()
        {
            if (AnimationGraphics != null)
            {
                AnimationGraphics.Dispose();
            }

            AnimationBitmap = new Bitmap(sheet.Width, sheet.Height);
            AnimationGraphics = Graphics.FromImage(AnimationBitmap);
            if (AnimationBox != null)
            {
                AnimationBox.Image = AnimationBitmap;
            }

        }
    }
}
