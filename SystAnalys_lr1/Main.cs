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
        public enum deleteType
        {
            None,
            TrafficLight,
            BusStops,
            VertexAndEdge,
            All,
            TheBuses
        }

        PictureBox AnimationBox;
        Graphics AnimationGraphics;
        Bitmap AnimationBitmap;

        public static deleteType delType;
        int tracbarX, tracbarY;
        public static string selectedRoute;
        public static int firstCrossRoads = 0;
        public static int firstCrossRoadsGreenLight = 0;
        public static int firstCrossRoadsRedLight = 0;
        public static int secondCrossRoads = 0;
        public static string EpicSizeParamSave = "radioEpicMedium";
        
        public static List<string> MovingEpicParamet;
        
        public static string EpicFreqMovingSave = null;
        public static int EpicFreqMovingParam = 0;
        
        public static string EpicFreqSpreadingSave = null;
        public static int EpicFreqSpreadingParam = 0;
        
        public static string EpicPhaseSavingSave = null;
        public static int EpicPhaseSavingParam = 1;
        
        string savepath;
       
        public static bool SavePictures = false;        
        
        public static bool extendedSavePictures = false;
        
        public static Grid g;
        //Лист всех эпицентров
        List<Epicenter> Epics;
        //Лист, в котором хранится сетка
        static public List<GridPart> TheGrid;
        public static DrawGraph G;
        //Лист, в котором хранятся автобусы
        static public List<Bus> buses;
        static List<List<Bus>> busesPark;
        public static int selected1; //выбранные вершины, для соединения линиями
        public static int selected2;
        //массив всех маршрутов
        static public SerializableDictionary<string, List<Vertex>> routes;
        //статичный размер басса
        public static int sizeBus;
        //очистить данные
        //все вершины
        static public List<Vertex> V;
        //ребра маршрутов
        static public SerializableDictionary<string, List<Edge>> routesEdge;
        //все ребра
        public static List<Edge> E;
        //для AllGridFilling, чтобы отображать время за которое моделилось движение
        List<int> TimeForAllGridFilling;
        //потом
        List<Dictionary<string, Dictionary<string, int>>> AllRouteGridFilling;
        //уровень загрязнения в координатах
        Dictionary<string, List<GridPart>> PollutionInRoutes;

        // лист номеров квадратов, в которм есть светофор
        static public List<int> TraficLightsInGrids;
        // словарь номеров квадратов, в которм есть остановка для каждого маршрута
        public static List<Vertex> allstopPoints;
        public static SerializableDictionary<string, List<int>> stopPointsInGrids;
        //Остановки маршрутов
        public static SerializableDictionary<string, List<Vertex>> stopPoints;

        //Светофоры
        public static List<TraficLight> traficLights;
        bool lang = false;
        //все координаты движения автобусов
        private SerializableDictionary<string, List<Point>> AllCoordinates;
        //все квадраты сетки, которые есть в каждом из маршрутов 
        public static SerializableDictionary<string, List<int>> AllGridsInRoutes { get; set; }
        Image saveImage;
        Random rnd = new Random();
        static public List<SerializableDictionary<int, Vertex>> routePoints;
        static public SerializableDictionary<int, List<Edge>> edgePoints;
        //вторая форма
        DisplayEpicenters Ep;
        int countWithoutSensors;
        int wsheet;
        int hsheet;
        static public Image globalMap;
        static public int zoom, scrollX, scrollY;
        Report r;

        int T;
        List<int?> ResultFromModeling = new List<int?>();

        int rCount;
        int iCh;
        int oldChart;

        private void addInComboBox()
        {
            changeRoute.Items.Clear();
            changeRoute.Items.Add(MainStrings.none);
            changeRoute.Items.Add(MainStrings.network);
            foreach (var r in routes)
            {
                changeRoute.Items.Add(r.Key);
            };
            changeRoute.Text = MainStrings.network;
        }

        private void InitializeElements()
        {
            tracbarX = metroTrackBar1.Location.X;
            tracbarY = metroTrackBar1.Location.Y;
            MovingEpicParamet = new List<string>();
            r =  new Report();
            iCh = 0;
            rCount = 0;
            g = new Grid(0, 0, 0, 0, 80, 40);
            routePoints = new List<SerializableDictionary<int, Vertex>>();
            edgePoints = new SerializableDictionary<int, List<Edge>>();
            AllCoordinates = new SerializableDictionary<string, List<Point>>();
            AllGridsInRoutes = new SerializableDictionary<string, List<int>>();
            stopPointsInGrids = new SerializableDictionary<string, List<int>>();
            TraficLightsInGrids = new List<int>();
            allstopPoints = new List<Vertex>();
            stopPoints = new SerializableDictionary<string, List<Vertex>>();
            StyleManager = msmMain;
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
            _instance = this;
            G = new DrawGraph();
            E = new List<Edge>();
            V = new List<Vertex>();

            zoom = 1;
            scrollX = 0;
            scrollY = 0;

            routesEdge = new SerializableDictionary<string, List<Edge>>();
            AllRouteGridFilling = new List<Dictionary<string, Dictionary<string, int>>>();
            TimeForAllGridFilling = new List<int>();
            traficLights = new List<TraficLight>();
            routes = new SerializableDictionary<string, List<Vertex>>();
            buses = new List<Bus>();
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
                        if (savepath != "")
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
                                    //loading.Visible = false;
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
            mainPanel.MouseWheel += new System.Windows.Forms.MouseEventHandler(panel6_MouseWheel);
            countWithoutSensors = buses.Count;
            Matrix();
            hint.Visible = false;
            r.ch.Titles.Add(MainStrings.report);
            r.ch.Series[rCount].LegendText = "1";
        }

        public void AnimationSettings()
        {

            AnimationBitmap = new Bitmap(sheet.Width, sheet.Height);
            AnimationBitmap.MakeTransparent();
            AnimationBox = new PictureBox();
            AnimationBox.Image = AnimationBitmap;
            AnimationGraphics = Graphics.FromImage(AnimationBitmap);
            sheet.Controls.Add(AnimationBox);
            AnimationBox.SizeMode = sheet.SizeMode;
            AnimationBox.Location = new Point(0, 0);
            AnimationBox.BackColor = Color.Transparent;
            AnimationBox.Size = sheet.Size;
            AnimationBox.MouseClick += sheet_MouseClick_1;
        }

        private static Main _instance;

        public Main()
        {
            // Если в настройках есть язык, устанавлияем его для текущего потока, в котором выполняется приложение.
            if (!string.IsNullOrEmpty(Properties.Settings.Default.Language))
            {
                // ВАЖНО: Устанавливать язык нужно до создания элементов формы!
                // Это можно сделать глобально, в рамках приложения в классе Program (см. файл Program.cs).
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo(Properties.Settings.Default.Language);
                System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo(Properties.Settings.Default.Language);
            }

            InitializeComponent();
            InitializeElements();
            LoadSettings();
            AnimationSettings();


        }
        //функция возвращает массив координат маршрутов (для 2 формы)
        public SerializableDictionary<string, List<Point>> GetAllCoordinates()
        {
            return AllCoordinates;
        }
        //функция возвращает массив загрязнений по маршрутам (для 2 формы)
        public Dictionary<string, List<GridPart>> GetPollutionInRoutes()
        {
            return PollutionInRoutes;
        }
        //функция возвращает эпицентры (для 2 формы)
        public List<Epicenter> GetEpicenters()
        {
            return Epics;
        }
        //функция возвращает сетку (для 2 формы)
        public List<GridPart> GetTheGrid()
        {
            return TheGrid;
        }
        //функция возвращает комбобох1 (для 2 формы)
        public ComboBox GetcomboBox1()
        {
            return changeRoute;
        }

        delegate void Del(string text);
        delegate void DelInt(int text);
        delegate void DelBool(bool text);
        //функция, которая моделирует движение
        int small = 10000;
        private void ExpandEpics(List<Epicenter> Epics)
        {
            Epics.First().ExpandEpic();
        }
        private void MoveEpics(List<Epicenter> Epics)
        {
            if (MovingEpicParamet.Count > 0)
            {
                Epics.First().EpicMoving(MovingEpicParamet);
            }
        }
        private void Modeling(string SavePath, int Cicle, int ModelNum)
        {
            List<Epicenter> epList = new List<Epicenter>();
            int i = 0;
            ConcurrentQueue<Bus> cqBus = new ConcurrentQueue<Bus>();
            buses.ForEach((b) => cqBus.Enqueue((Bus)b.Clone()));
            foreach (var EpicList in Epics)
            {

                epList.Add(new Epicenter(TheGrid));
                foreach (var Sector in EpicList.GetEpicenterGrid())
                {
                    epList[i].EpicenterGrid.Add(Sector.Key, new List<GridPart>());
                    epList[i].StartPositon = EpicList.StartPositon;
                    epList[i].NewExpandCount = new List<int>();
                    foreach (var Square in Sector.Value)
                    {
                        epList[i].EpicenterGrid[Sector.Key].Add(new GridPart(Square.x, Square.y));
                    }
                }
                i++;
            }
            CreatePollutionInRoutes();
            int small = 10000;
            int old = small;
            int FoundTime = small + 1;

            int ExpandTimer = 0;
            int MovingTimer = 0;
            i = 1;
            int PhaseSizeSelect()
            {
                if (extendedSavePictures == false)
                {
                    return 1;
                }
                else
                {
                    return EpicPhaseSavingParam;
                }

            }
            foreach (var bus in cqBus)
            {
                bus.AllTickCount = 0;

            }
            bool EpicFounded = false;
            for (int j = PhaseSizeSelect(); j > 0; j--)
            {
                ////
                if (j == PhaseSizeSelect())
                {
                    if ((SavePictures == true) && (extendedSavePictures == true))
                    {
                        Directory.CreateDirectory(SavePath + "/Epics" + "/" + (Cicle + 1).ToString() + "/" + (ModelNum + 1).ToString() + "/" + 0.ToString());
                        lock (Ep.Esheet)
                        {
                            Ep.EDrawEpics(epList);
                        }
                        lock (Ep.Esheet)
                        {
                            using (System.Drawing.Image img = (Image)Ep.Esheet.Image.Clone())
                            {
                                img.Save(SavePath + "/Epics" + "/" + (Cicle + 1).ToString() + "/" + (ModelNum + 1).ToString() + "/" + 0.ToString() + "/" + 0.ToString() + "_nat" + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                            }
                        }
                    }
                }
                ////

                foreach (var bus in cqBus)
                {
                    bus.Epicenters = epList;

                    bus.TickCount_ = 0;
                    if (bus.skip > 0)
                        bus.skip -= 1;
                    if (bus.Tracker == true)
                    {
                        while (bus.TickCount_ < (T / PhaseSizeSelect()))
                        {
                            bus.MoveWithoutGraphicsByGrids();
                            if (EpicSettings.TurnMovingSet == true)
                            {
                                if (MovingTimer >= ((EpicFreqMovingParam / 20) * cqBus.Count))
                                {
                                    lock (epList)
                                    {
                                        MoveEpics(epList);
                                    }
                                    MovingTimer = 0;
                                }
                            }
                            if (EpicSettings.TurnSpreadingSet == true)
                            {
                                if (ExpandTimer >= ((EpicFreqSpreadingParam / 20) * cqBus.Count))
                                {
                                    lock (epList)
                                    {
                                        ExpandEpics(epList);
                                    }
                                    ExpandTimer = 0;
                                }
                            }
                            if (TraficLightsInGrids.Contains(AllGridsInRoutes[bus.GetRoute()][(int)bus.PositionAt])) //ошибка с выходом за пределы тушто нужно "вот эту" разкоментить
                            {
                                if (bus.skip == 0)
                                {
                                    foreach (var sp in traficLights)
                                    {
                                        if (sp.Status != Status.RED)
                                        {
                                            bus.skip = sp.greenTime;
                                            break;
                                        }
                                        if (sp.Status == Status.RED)
                                        {
                                            bus.TickCount_ = bus.TickCount_ + sp.bal;
                                            bus.AllTickCount = bus.AllTickCount + sp.bal;
                                            bus.skip = sp.greenTime;
                                            break;

                                        }
                                    }
                                }
                            }

                            if ((stopPointsInGrids.ContainsKey(bus.GetRoute())) && (stopPointsInGrids[bus.GetRoute()].Contains(AllGridsInRoutes[bus.GetRoute()][(int)bus.PositionAt])))
                            {
                                int timeboost = rnd.Next(0, 3);
                                bus.TickCount_ = bus.TickCount_ + timeboost;
                                bus.AllTickCount = bus.AllTickCount + timeboost;
                            }

                            PollutionInRoutes[bus.GetRoute()][AllGridsInRoutes[bus.GetRoute()][(int)bus.PositionAt]].Status = bus.DetectEpicenterByGrid(); // ошибка

                            foreach (var Epic in bus.Epicenters)
                            {
                                if (Epic.DetectCount >= Epic.getEpicenterGrid()[1].Count / 2)
                                {
                                    if (EpicFounded == false)
                                    {
                                        EpicFounded = true;
                                        if (EpicFounded == true)
                                        {
                                            //  FoundTime = (T - (bus.TickCount_ * j));
                                            FoundTime = bus.AllTickCount;
                                            if (small > FoundTime)
                                            {
                                                small = FoundTime;
                                            }
                                        }
                                    }
                                    break;
                                }
                            }
                            //  bus.TickCount_--;
                            bus.TickCount_++;
                            bus.AllTickCount++;
                            if (EpicSettings.TurnSpreadingSet == true)
                            {
                                ExpandTimer++;
                            }
                            if (EpicSettings.TurnMovingSet == true)
                            {
                                MovingTimer++;
                            }
                        }
                    }
                }

                if ((SavePictures == true) && (extendedSavePictures == true))
                {
                    Directory.CreateDirectory(SavePath + "/Epics" + "/" + (Cicle + 1).ToString() + "/" + (ModelNum + 1).ToString() + "/" + i.ToString());
                    lock (Ep.Esheet)
                    {
                        Ep.EDrawEpics(epList);
                    }
                    lock (Ep.Esheet)
                    {
                        using (System.Drawing.Image img = (Image)Ep.Esheet.Image.Clone())
                        {
                            img.Save(SavePath + "/Epics" + "/" + (Cicle + 1).ToString() + "/" + (ModelNum + 1).ToString() + "/" + i.ToString() + "/" + i.ToString() + "_nat" + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                        }
                    }

                    lock (Ep.Esheet)
                    {
                        Ep.RecReateFunction();
                    }
                    lock (Ep.Esheet)
                    {
                        using (System.Drawing.Image img = (Image)Ep.Esheet.Image.Clone())
                        {
                            img.Save(SavePath + "/Epics" + "/" + (Cicle + 1).ToString() + "/" + (ModelNum + 1).ToString() + "/" + i.ToString() + "/" + i.ToString() + "_re" + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                        }
                    }
                    i++;
                }

            }
            if (small == old)
                ResultFromModeling.Add(null);
            else
            {
                if (small == 0)
                {
                    small += 1;
                    ResultFromModeling.Add(small * 20); // small в мин или секах
                }
                else
                {
                    ResultFromModeling.Add(small * 20);
                }
            }

        }
        delegate void DelBmp(Bitmap bmp);
        //функция создает 1 случайный эпицентр в пределах сетки
        public void CreateOneRandomEpicenter(int EpicSizeParam, int? StartPos)
        {
            var rand = new Random();
            Epics = new List<Epicenter>
            {
                new Epicenter(TheGrid)
            };
            Epics.First().CreateRandomEpicenter(EpicSizeParam, StartPos);
        }
        //функция создает массив загрязнений в маршруте
        private void CreatePollutionInRoutes()
        {
            PollutionInRoutes = new Dictionary<string, List<GridPart>>();
            for (int i = 0; i < AllCoordinates.Count; i++)
            {
                PollutionInRoutes.Add(AllCoordinates.ElementAt(i).Key, new List<GridPart>());
                foreach (var Grid in TheGrid)
                {
                    PollutionInRoutes[PollutionInRoutes.ElementAt(i).Key].Add(new GridPart(Grid.x, Grid.y));
                }
            }
        }
        private void CreateGridRoutes()
        {
            PollutionInRoutes = new Dictionary<string, List<GridPart>>();
            for (int i = 0; i < AllCoordinates.Count; i++)
            {
                PollutionInRoutes.Add(AllCoordinates.ElementAt(i).Key, new List<GridPart>());
                foreach (var Grid in TheGrid)
                {
                    PollutionInRoutes[PollutionInRoutes.ElementAt(i).Key].Add(new GridPart(Grid.x, Grid.y));
                }
            }
        }

        private void panel6_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {

            Bus.SetScrollX(mainPanel.AutoScrollPosition.X);
            Bus.SetScrollY(mainPanel.AutoScrollPosition.Y);
        }
        private void panel6_Scroll(object sender, ScrollEventArgs e)
        {

            Bus.SetScrollX(mainPanel.AutoScrollPosition.X);
            Bus.SetScrollY(mainPanel.AutoScrollPosition.Y);

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.ScaleTransform(10, 10);
        }

        public Panel GetMainPanel()
        {
            return panel4;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AddRoute f = new AddRoute
            {
                Owner = this
            };
            f.ShowDialog();
            Ep.ERefreshRouts();
        }

        //в textBox1 можно вводить только цифры
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }

        }

        //возвращает картинку карты(нужно для 2 формы)
        public PictureBox GetSheet()
        {
            return sheet;
        }
        public Panel GetMapPanel()
        {
            return mainPanel;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            G.ClearSheet();
            G.DrawALLGraph(V, E);
            sheet.Image = G.GetBitmap();
            selected1 = -1;
            DrawGrid();
        }

        private void addBus_Click(object sender, EventArgs e)
        {
            addBus.Enabled = false;
            deleteBus.Enabled = true;
            selectButton.Enabled = true;
            drawVertexButton.Enabled = false;
            drawEdgeButton.Enabled = true;
            deleteButton.Enabled = true;
            allBusSettings.Enabled = true;
            sheet.Image = G.GetBitmap();
            selected1 = -1;
            selectRoute.Enabled = true;
            label12.Visible = false;
            selected = new List<int>();
            addTraficLight.Enabled = false;
            DrawGrid();
        }

        private void comboBox1_SelectedIndexChangedAsync(object sender, EventArgs e)
        {
            if (changeRoute.Text == MainStrings.none)
            {
                selectedRoute = null;
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
                sheet.Image = G.GetBitmap();
                label12.Visible = false;
                selected = new List<int>();
                return;
            };
            if (changeRoute.Text == MainStrings.network)
            {
                selectedRoute = null;
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
                checkBuses();
                G.ClearSheet();
                G.DrawALLGraph(V, E);
                label12.Visible = false;
                sheet.Image = G.GetBitmap();
                DrawGrid();
                Console.WriteLine(MainStrings.network);
                selected = new List<int>();
                return;
            };
            for (int i = 0; i < routes.Count; i++)
            {
                if (routes.ElementAt(i).Key == (changeRoute.Text))
                {
                    selectedRoute = (changeRoute.Text);
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
                    label12.Visible = false;
                    addTraficLight.Enabled = false;
                    checkBusesOnRoute();
                    G.ClearSheet();
                    G.DrawALLGraph(V, E);
                    G.DrawALLGraph(routes[(changeRoute.Text)], routesEdge[(changeRoute.Text)], 1);
                    sheet.Image = G.GetBitmap();
                    DrawGrid();
                    selected = new List<int>();
                    return;
                };
            }
        }
        private void buttonOn()
        {
            changeRoute.Invoke(new DelBool((s) => changeRoute.Enabled = s), true);
            button8.Invoke(new DelBool((s) => button8.Enabled = s), true);
            optimize.Invoke(new DelBool((s) => optimize.Enabled = s), true);
            createCoordinates.Invoke(new DelBool((s) => createCoordinates.Enabled = s), true);
            launchBuses.Invoke(new DelBool((s) => launchBuses.Enabled = s), true);
            stopBuses.Invoke(new DelBool((s) => launchBuses.Enabled = s), true);
            metroButton2.Invoke(new DelBool((s) => metroButton2.Enabled = s), true);
            toolStripMenu.Invoke((System.Action)(() =>
            {
                saveButton.Enabled = true;
                loadButton.Enabled = true;
            }));
        }
        private void buttonOff()
        {
            changeRoute.Invoke(new DelBool((s) => changeRoute.Enabled = s), false);
            button8.Invoke(new DelBool((s) => button8.Enabled = s), false);
            optimize.Invoke(new DelBool((s) => optimize.Enabled = s), false);
            createCoordinates.Invoke(new DelBool((s) => createCoordinates.Enabled = s), false);
            launchBuses.Invoke(new DelBool((s) => launchBuses.Enabled = s), false);
            stopBuses.Invoke(new DelBool((s) => launchBuses.Enabled = s), false);
            metroButton2.Invoke(new DelBool((s) => metroButton2.Enabled = s), false);
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
        private void delBus()
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
            G.ClearSheet();
            G.DrawALLGraph(V, E);
            G.DrawALLGraph(routes[(changeRoute.Text)], routesEdge[(changeRoute.Text)], 1);
            sheet.Image = G.GetBitmap();
            selected1 = -1;
            stopPointButton.Enabled = true;
            selected = new List<int>();
            label12.Visible = false;
            DrawGrid();
            checkBusesOnRoute();
        }

        private void deleteBus_Click(object sender, EventArgs e)
        {

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
            return metroTrackBar1;
        }

        private static Image Zoom(Image img, Size size)
        {
            Bitmap bmp = new Bitmap(img, size);
            Graphics g = Graphics.FromImage(bmp);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            return bmp;
        }

        public static string NormalizePath(string path)
        {
            return Path.GetFullPath(new Uri(path).LocalPath)
                       .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                       .ToUpperInvariant();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
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
                        SaveRoutes(saveF, savepath + @"\");
                        BringToFront();
                        MetroMessageBox.Show(this, "", MainStrings.done, MessageBoxButtons.OK, MessageBoxIcon.Question);
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
                                SaveRoutes(saveF, savepath + @"\");
                                using (StreamWriter fileV = new StreamWriter("../../SaveConfig/save.txt"))
                                {
                                    fileV.WriteLine(savepath.ToString());
                                }
                                BringToFront();
                                MetroMessageBox.Show(this, "", MainStrings.done, MessageBoxButtons.OK, MessageBoxIcon.Question);

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
        static public string globalDel = "All";
        private void deleteButton_Click(object sender, EventArgs e)
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
                checkBuses();
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
                G.DrawALLGraph(routes[(changeRoute.Text)], routesEdge[(changeRoute.Text)], 1);
                checkBusesOnRoute();
            }
            df.ShowDialog();
            label12.Text = globalDel;
            label12.Visible = true;
            selected = new List<int>();
            selectRoute.Enabled = true;
            stopPointButton.Enabled = true;
            sheet.Image = G.GetBitmap();
            DrawGrid();
            if (delType == deleteType.None)
                deleteButton.Enabled = true;

        }


        private void drawVertexButton_Click(object sender, EventArgs e)
        {
            deleteBus.Enabled = false;
            addBus.Enabled = false;
            allBusSettings.Enabled = false;
            drawVertexButton.Enabled = false;
            Console.WriteLine("DrawVert");
            selectButton.Enabled = true;
            drawEdgeButton.Enabled = true;
            deleteButton.Enabled = true;
            G.ClearSheet();
            G.DrawALLGraph(V, E);
            selectRoute.Enabled = true;
            sheet.Image = G.GetBitmap();
            stopPointButton.Enabled = true;
            label12.Visible = false;
            addTraficLight.Enabled = true;
            DrawGrid();
            selected = new List<int>();
        }

        private void deleteRoute_Click(object sender, EventArgs e)
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
                if (routes != null && routesEdge != null && E != null && V != null && buses != null)
                {
                    if (MBSave == DialogResult.Yes && changeRoute.Text != "" && changeRoute.Text != MainStrings.network)
                    {
                        loadingForm.Show();
                        List<Bus> busTest = new List<Bus>();
                        loadingForm.loading.Value = 20;
                        foreach (var b in buses)
                        {
                            if (b.route == changeRoute.Text)
                            {
                                // mainPanel.Controls.Remove(b.busPic);
                                busTest.Add(b);
                            };
                        };
                        loadingForm.loading.Value = 40;
                        foreach (var b in busTest)
                        {
                            buses.Remove(b);
                        }

                        routes.Remove(changeRoute.Text);
                        routesEdge.Remove(changeRoute.Text);
                        AllCoordinates.Remove(changeRoute.Text);
                        addInComboBox();
                        changeRoute.Text = changeRoute.Items[0].ToString();
                        G.ClearSheet();
                        G.DrawALLGraph(V, E);
                        sheet.Image = G.GetBitmap();
                        DrawGrid();
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
                        buses.Clear();
                        addBus.Enabled = false;
                        deleteBus.Enabled = false;
                        loadingForm.loading.Value = 40;
                        V.Clear();
                        E.Clear();
                        addTraficLight.Enabled = true;
                        routes.Clear();
                        routesEdge.Clear();
                        addInComboBox();
                        AllCoordinates.Clear();
                        G.ClearSheet();
                        G.DrawALLGraph(V, E);
                        sheet.Image = G.GetBitmap();
                        DrawGrid();
                        checkBuses();
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
                label12.Visible = false;
                selectRoute.Enabled = true;
                selected = new List<int>();
                stopPointButton.Enabled = true;
                Ep.ERefreshRouts();

                BarabanAfterOpti();
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

        private void newModelToolStripMenuItem_Click(object sender, EventArgs e)
        {

            OpenFileDialog fb = new OpenFileDialog
            {
                FilterIndex = 1,
                Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png"
            };
            if (fb.ShowDialog() == DialogResult.OK)
            {
                savepath = null;
                if (Ep != null)
                {
                    Ep.EG = new DrawGraph();
                    Ep.Close();
                }
                buses.Clear();
                config.Text = MainStrings.config;
                foreach (var tl in traficLights)
                {
                    tl.Stop();
                }
                TraficLightsInGrids.Clear();
                stopPointsInGrids.Clear();
                V.Clear();
                E.Clear();
                G.bitmap = null;
                if (G.bitmap != null)
                {
                    ZoomHelper();
                    G.ClearSheet();
                    G.ClearSheet2();
                }
                routes.Clear();
                routesEdge.Clear();
                changeRoute.Items.Clear();
                AllCoordinates.Clear();
                allstopPoints.Clear();
                stopPoints.Clear();
                traficLights.Clear();
                sheet.Image = Image.FromFile(fb.FileName);
                saveImage = sheet.Image;
                metroTrackBar1.Value = 1;
                wsheet = sheet.Width;
                hsheet = sheet.Height;
                globalMap = sheet.Image;
                G.SetBitmap();
                CreateGrid();
                CreatePollutionInRoutes();
                addInComboBox();
                Ep = new DisplayEpicenters(this);
                StyleManager.Clone(Ep);
                Ep.Show();

                openEpicFormToolStripMenuItem.Enabled = true;
                addRouteToolStripMenuItem.Enabled = true;
                createGridToolStripMenuItem.Enabled = true;
                Matrix();
                BringToFront();
                timer1.Dispose();
                timer1.Start();
                MetroMessageBox.Show(this, "", MainStrings.done, MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        public static bool yes;
        private void deleteALLButton_Click(object sender, EventArgs e)
        {
            try
            {
                yes = false;
                DeleteForm df = new DeleteForm();
                StyleManager.Clone(df);
                df.VandE.Enabled = false;
                df.All.Text = MainStrings.graphClear;
                df.ShowDialog();
                LoadingForm loadingForm = new LoadingForm();
                loadingForm.loading.Value = 0;
                loadingForm.loading.Maximum = 100;
                addBus.Enabled = true;
                drawVertexButton.Enabled = false;
                allBusSettings.Enabled = false;
                selectButton.Enabled = true;
                drawEdgeButton.Enabled = true;
                deleteButton.Enabled = true;
                string message = MainStrings.clearGraph;
                string caption = MainStrings.delete;
                DialogResult MBSave = DialogResult.No;
                if (yes)
                    MBSave = MetroMessageBox.Show(this, message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                else
                    return;
                switch (delType)
                {
                    case deleteType.All:
                        if (MBSave == DialogResult.Yes && changeRoute.Text != MainStrings.network)
                        {
                            loadingForm.Show();
                            routes[changeRoute.Text].Clear();
                            routesEdge[changeRoute.Text].Clear();
                            loadingForm.loading.Value = 20;
                            List<Bus> busTest = new List<Bus>();
                            foreach (var b in buses)
                            {
                                if (b.route == changeRoute.Text)
                                {
                                    busTest.Add(b);
                                };
                            };
                            loadingForm.loading.Value = 40;
                            foreach (var b in busTest)
                            {
                                buses.Remove(b);
                            }
                            loadingForm.loading.Value = 50;
                            AllCoordinates[changeRoute.Text].Clear();
                            G.ClearSheet();
                            G.DrawALLGraph(V, E);
                            sheet.Image = G.GetBitmap();
                            DrawGrid();

                        }
                        if (MBSave == DialogResult.Yes && changeRoute.Text == MainStrings.network)
                        {
                            loadingForm.Show();
                            loadingForm.loading.Value = 20;
                            buses.Clear();
                            routes.Keys.ToList().ForEach(x => routes[x] = new List<Vertex>());
                            routesEdge.Keys.ToList().ForEach(x => routesEdge[x] = new List<Edge>());
                            loadingForm.loading.Value = 40;
                            AllCoordinates.Clear();
                            G.ClearSheet();
                            sheet.Image = G.GetBitmap();
                            DrawGrid();
                            loadingForm.loading.Value = 50;
                        };
                        break;
                    case deleteType.BusStops:
                        if (MBSave == DialogResult.Yes && changeRoute.Text != MainStrings.network)
                        {
                            loadingForm.Show();
                            loadingForm.loading.Value = 20;
                            stopPoints[changeRoute.Text].Clear();
                            stopPointsInGrids[changeRoute.Text].Clear();
                            loadingForm.loading.Value = 40;
                            G.ClearSheet();
                            sheet.Image = G.GetBitmap();
                            DrawGrid();
                            loadingForm.loading.Value = 50;
                        }
                        if (MBSave == DialogResult.Yes && changeRoute.Text == MainStrings.network)
                        {
                            loadingForm.Show();
                            loadingForm.loading.Value = 20;
                            allstopPoints.Clear();
                            stopPoints.Clear();
                            stopPointsInGrids.Clear();
                            loadingForm.loading.Value = 40;
                            G.ClearSheet();
                            sheet.Image = G.GetBitmap();
                            DrawGrid();
                            loadingForm.loading.Value = 50;
                        }
                        break;
                    case deleteType.TrafficLight:
                        if (MBSave == DialogResult.Yes)
                        {
                            loadingForm.Show();
                            loadingForm.loading.Value = 20;
                            foreach (var tf in traficLights)
                                tf.Stop();
                            traficLights.Clear();
                            TraficLightsInGrids.Clear();
                            loadingForm.loading.Value = 40;
                            G.ClearSheet();
                            sheet.Image = G.GetBitmap();
                            DrawGrid();
                            loadingForm.loading.Value = 50;
                        }
                        break;
                    case deleteType.TheBuses:
                        if (MBSave == DialogResult.Yes)
                        {
                            loadingForm.Show();
                            loadingForm.loading.Value = 20;
                            delAllBus();
                            loadingForm.loading.Value = 40;
                            loadingForm.loading.Value = 50;
                        }
                        break;
                }


                if (changeRoute.Text == MainStrings.network)
                {
                    deleteBus.Enabled = false;
                    addBus.Enabled = false;
                    drawVertexButton.Enabled = true;
                    addTraficLight.Enabled = true;
                };
                G.DrawALLGraph(V, E);
                selectRoute.Enabled = true;
                loadingForm.loading.Value = 80;
                label12.Visible = false;
                selected = new List<int>();
                stopPointButton.Enabled = true;
                if (!Ep.IsDisposed)
                {
                    Ep.ERefreshRouts();
                }

                BarabanAfterOpti();
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
                    //loading.Visible = false;
                }
            }

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            if (changeRoute.Text == MainStrings.network)
            {
                addBus.Enabled = false;
                allBusSettings.Enabled = false;
                selectButton.Enabled = false;
                drawVertexButton.Enabled = true;
                drawEdgeButton.Enabled = true;
                addTraficLight.Enabled = true;
                checkBuses();
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
                checkBusesOnRoute();
            }
            label12.Visible = false;
            selectRoute.Enabled = true;
            stopPointButton.Enabled = true;
            sheet.Image = G.GetBitmap();
            selected1 = -1;
            selected = new List<int>();
            DrawGrid();
        }
        private List<Bus> SplitBuses()
        {
            busesPark = new List<List<Bus>>();

            List<Bus> b = new List<Bus>();

            for (int i = 0; i < buses.Count; i++)
            {
                if (i == buses.Count - 1)
                {
                    if (b.Count == 0)
                    {
                        busesPark.Add(new List<Bus>() { buses[i] });
                    }
                    else
                    {
                        b.Add(buses[i]);
                        b = BubbleSortEx(b);
                        busesPark.Add(new List<Bus>(b));
                        b.Clear();
                    }
                }
                else
                {
                    if (buses[i].route == buses[i + 1].route)
                    {
                        b.Add(buses[i]);
                    }
                    else
                    {
                        if (b.Count == 0)
                        {
                            busesPark.Add(new List<Bus>() { buses[i] });
                        }
                        else
                        {
                            b.Add(buses[i]);
                            b = BubbleSortEx(b);
                            busesPark.Add(new List<Bus>(b));
                            b.Clear();
                        }
                    }
                }
            };
            return b;
        }
        public bool NextBool()
        {
            // as simple as possible
            return rnd.Next(0, 2) == 1;
        }
        private void BarabanAfterOpti()
        {
            Random rnd = new Random();
            foreach (var b in buses)
            {
                int r = rnd.Next(0, AllCoordinates[b.route].Count - 1);
                b.PositionAt = r;
            };
        }
        private void Baraban()
        {

            foreach (var bp in busesPark)
            {
                var tot = (AllGridsInRoutes[bp.First().route].Count - 1) / bp.Count;
                if (tot == 0 || tot == 1)
                {
                    foreach (var b in buses)
                    {
                        if (b.route == bp.First().route)
                        {
                            int r = rnd.Next(0, AllGridsInRoutes[bp.First().route].Count - 1);
                            b.PositionAt = r;
                        }

                    };
                }
                else
                {
                    List<int> array = new List<int>();
                    int i = 0;
                    while (i < AllGridsInRoutes[bp.First().route].Count - 1)
                    {
                        array.Add(i);
                        i += tot;
                    }
                    if (array.Count != 0)
                    {
                        foreach (var b in buses)
                        {

                            if (b.route == bp.First().route)
                            {
                                int r = rnd.Next(0, array.Count - 1);
                                b.PositionAt = array[r];
                                array.RemoveAt(r);
                            }

                        };
                    }
                }
            }
        }

        List<int> withoutSensorsBuses = new List<int>();
        private void offBuses(int proc = 0)
        {
            int countSensors = 0;
            int tot = 0;
            SplitBuses();
            foreach (var b in busesPark)
            {
                double razm = Math.Round(b.Count - b.Count * 0.01 * proc);
                double limit = Math.Round(b.Count - razm, 0);
                foreach (var bus in b)
                {
                    if (0 != limit)
                    {
                        countSensors += 1;
                        bus.Tracker = false;
                        limit = limit - 1;
                    }
                    else
                    {
                        break;
                    };
                };
                for (var i = 0; i < b.Count; i++)
                {
                    buses[tot] = b[i];
                    tot += 1;
                }
            };
            countWithoutSensors -= countSensors;
            if (withoutSensorsBuses.Count == 4)
            {
                countWithoutSensors = 1;
            }
            if (withoutSensorsBuses.Count != 5)
            {
                withoutSensorsBuses.Add(countWithoutSensors);
            }
        }

        SerializableDictionary<int, int?> percentMean;
        public int? GetKeyByValue(int? value)
        {
            foreach (var recordOfDictionary in percentMean)
            {
                if (recordOfDictionary.Value.Equals(value))
                    return recordOfDictionary.Key;
            }
            return null;
        }

        LoadingForm loadingForm = new LoadingForm();
        string pathOpt;
        private async void Opt()
        {
            loadingForm = new LoadingForm();
            buttonOff();
            pathOpt = "../../Results/" + string.Format("{0}_{1}_{2}_{3}_{4}_{5}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            Directory.CreateDirectory(pathOpt);

            if (SavePictures == true)
            {
                Ep.Hide();
                Directory.CreateDirectory(pathOpt + "/Epics");
            }
            loadingForm.Theme = msmMain.Theme;
            loadingForm.Style = msmMain.Style;
            Matrix();
            percentMean = new SerializableDictionary<int, int?>();

            List<Bus> optimizeBuses = new List<Bus>();
            buses.ForEach((b) => optimizeBuses.Add(
                (Bus)b.Clone()
            ));
            if (speed.Text != "" && int.TryParse(speed.Text, out int sp))
            {
                if (int.Parse(speed.Text) / 20 == 0)
                    T = 1;
                else
                    T = int.Parse(speed.Text) / 20;
            }
            int old = small;
            var style = msmMain.Style;
            if (msmMain.Style == (MetroFramework.MetroColorStyle)Convert.ToInt32(13))
                msmMain.Style = (MetroFramework.MetroColorStyle)Convert.ToInt32(14);
            else
                msmMain.Style = (MetroFramework.MetroColorStyle)Convert.ToInt32(13);
            loadingForm.Show();
            StyleManager.Clone(Ep);
            if (Ep != null)
            {
                Ep.Refresh();
            }
            var busesparkreturn = busesPark;

            await Task.Run(() =>
            {
                if (changeProcent.Text != "" && int.TryParse(speed.Text, out int ch) && changeProcent.Text != "")
                {
                    offBuses(int.Parse(changeProcent.Text));
                };
                int ciclTotal = 5;

                loadingForm.loading.Invoke(new DelInt((s) => loadingForm.loading.Maximum = s), ciclTotal * int.Parse(optText.Text));
                for (int cicl = 0; cicl < ciclTotal; cicl++)
                {
                    offBuses(cicl * 10);
                    if (cicl == ciclTotal - 1)
                        buses[rnd.Next(0, buses.Count)].Tracker = true;
                    List<int?> mas = new List<int?>();
                    Baraban();
                    if (SavePictures == true)
                    {
                        Directory.CreateDirectory(pathOpt + "/Epics" + "/" + (cicl + 1).ToString());
                    }
                    for (int i = 0; i < int.Parse(optText.Text); i++)
                    {
                        if (SavePictures == true)
                        {
                            Directory.CreateDirectory(pathOpt + "/Epics" + "/" + (cicl + 1).ToString() + "/" + (i + 1).ToString());
                        }
                        CreateOneRandomEpicenter(EpicSizeParam, null);
                        Modeling(pathOpt, cicl, i);
                        if ((SavePictures == true) && (!extendedSavePictures == true))
                        {

                            lock (Ep.Esheet)
                            {
                                Ep.EDrawEpics(Epics);
                            }
                            lock (Ep.Esheet)
                            {
                                using (System.Drawing.Image img = (Image)Ep.Esheet.Image.Clone())
                                {
                                    img.Save(pathOpt + "/Epics" + "/" + (cicl + 1).ToString() + "/" + (i + 1).ToString() + "/" + i.ToString() + "_nat" + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                                }
                            }

                            lock (Ep.Esheet)
                            {
                                Ep.RecReateFunction();
                            }
                            lock (Ep.Esheet)
                            {
                                using (System.Drawing.Image img = (Image)Ep.Esheet.Image.Clone())
                                {
                                    img.Save(pathOpt + "/Epics" + "/" + (cicl + 1).ToString() + "/" + (i + 1).ToString() + "/" + i.ToString() + "_re" + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                                }
                            }

                        }

                        loadingForm.loading.Invoke(new DelInt((s) => loadingForm.loading.Value = s), loadingForm.loading.Value + 1);
                        small = 10000;
                    }

                    int total = ResultFromModeling.Sum(x => Convert.ToInt32(x));
                    int count = 0;
                    foreach (var m in ResultFromModeling)
                    {
                        if (m != null)
                        {
                            count += 1;
                        }
                    }

                    if (total < 0 || count < ResultFromModeling.Count / 2)
                    {
                        //   mean.Invoke(new Del((s) => mean.Text = s), MainStrings.average + MainStrings.none + "\n" + MainStrings.procentSuc + " " + count * 100.00 / (int.Parse(optText.Text)) + "\n" + MainStrings.procentFailed + " " + ((ResultFromModeling.Count - count) * 100.00 / (int.Parse(optText.Text))));
                        if (!percentMean.ContainsKey(withoutSensorsBuses.Last()))
                            percentMean.Add(withoutSensorsBuses.Last(), null);
                        //  mean.Invoke(new Del((s) => mean.Text = s), MainStrings.average + MainStrings.none);
                    }
                    else
                    {
                        //  mean.Invoke(new Del((s) => mean.Text = s), MainStrings.average + " " + (total / ResultFromModeling.Count).ToString()
                        //      + "\n" + MainStrings.procentSuc + " " + ResultFromModeling.Count * 100.00 / (int.Parse(optText.Text)) + "\n" + MainStrings.procentFailed + " " + ((ResultFromModeling.Count - count) * 100.00 / (int.Parse(optText.Text))));
                        if (!percentMean.ContainsKey(withoutSensorsBuses.Last()))
                        {
                            if (count != 0)
                                percentMean.Add(withoutSensorsBuses.Last(), total / count);
                            else
                                percentMean.Add(withoutSensorsBuses.Last(), -1);
                        }
                        //   mean.Invoke(new Del((s) => mean.Text = s), MainStrings.average + " " + (Convert.ToDouble(total / ResultFromModeling.Count).ToString()));
                    }
                    ;
                    using (StreamWriter fileV = new StreamWriter(pathOpt + @"\" + withoutSensorsBuses.Last() + "_buses" + ".txt"))
                    {
                        fileV.WriteLine(MainStrings.sensorsDown + ": " + (cicl * 10).ToString());
                        fileV.WriteLine(MainStrings.countBuses + ": " + (withoutSensorsBuses.Last()).ToString());
                        fileV.WriteLine(MainStrings.numIter + ": " + optText.Text);
                        fileV.WriteLine(MainStrings.distance + ": " + speed.Text + " " + MainStrings.sec + " (" + (int.Parse(speed.Text) / 60 == 0 ? ">1" + MainStrings.minute : int.Parse(speed.Text) / 60 + " " + MainStrings.minute) + ")");
                        fileV.WriteLine(MainStrings.found + ": " + (from num in ResultFromModeling where (num != null) select num).Count());
                        if (count == 0)
                        {
                            fileV.WriteLine(MainStrings.none);
                        }
                        else
                        {
                            fileV.WriteLine(MainStrings.average + " " + (total / count / 60 == 0 ? (total / count + " " + MainStrings.sec).ToString() : (total / count / 60 + " " + MainStrings.minute + " " + total / count % 60 + " " + MainStrings.sec).ToString())
                                + "\n" + MainStrings.procentSuc + " " + (count * 100.00 / int.Parse(optText.Text)) + "\n" + MainStrings.procentFailed + " " + (((ResultFromModeling.Count - count) * 100.00 / int.Parse(optText.Text))).ToString());
                        }
                        fileV.WriteLine(MainStrings.cycle + " " + cicl.ToString());
                        for (int i = 0; i < ResultFromModeling.Count; i++)
                            if (ResultFromModeling[i] != null)
                            {
                                fileV.WriteLine(i.ToString() + " : " + (ResultFromModeling[i] / 60 == 0 ? (ResultFromModeling[i] + " " + MainStrings.sec).ToString() : (ResultFromModeling[i] / 60 + " " + MainStrings.minute + " " + ResultFromModeling[i] % 60 + " " + MainStrings.sec).ToString()));
                            }
                            else
                            {
                                fileV.WriteLine(i.ToString() + " : " + MainStrings.notFound);
                            }

                        Console.WriteLine("Объект сериализован");
                    }
                    ResultFromModeling = new List<int?>();
                }

            });
            var res = percentMean.Where(s => s.Value.Equals(percentMean.Min(v => v.Value))).Select(s => s.Key).ToList();
            var min = percentMean.Min(v => v.Value);
            var result = percentMean.Where(s => s.Value.Equals(min)).Select(s => s.Key).ToList();
            result.Sort();
            if (res.Count != 0 && min != 0 && min != null)
                mean.Text = MainStrings.average + " " + (min / 60 == 0 ? (min + " " + MainStrings.sec).ToString() : (min / 60 + " " + MainStrings.minute).ToString()) + " " + " - " + MainStrings.countSensors + ": " + result[0];
            else
            {
                mean.Text = MainStrings.average + " " + MainStrings.notFound;
            }

            using (StreamWriter fileV = new StreamWriter(pathOpt + "/Average.txt"))
            {
                fileV.WriteLine(mean.Text != MainStrings.average + " " + MainStrings.notFound ? MainStrings.average + " " + (min / 60 == 0 ? (min + " " + MainStrings.sec).ToString() : (min / 60 + " " + MainStrings.minute).ToString()) + " - " + MainStrings.countSensors + ": " + result[0] : MainStrings.notFound);
            }
            Matrix();
            resMatrix();
            msmMain.Style = style;
            // MetroMessageBox.Show(this, "", MainStrings.done, MessageBoxButtons.OK, MessageBoxIcon.Question);
            loadingForm.close = true;
            loadingForm.Close();
            loadingForm.Dispose();
            BringToFront();
            if (!Ep.IsDisposed)
            {
                Ep.Show();
            }
            BarabanAfterOpti();
            timer1.Start();
            buttonOn();
            busesPark = busesparkreturn;
            buses = optimizeBuses;


            resChart();

        }

        public static int EpicSizeParam = 25;

        private void optimize_Click(object sender, EventArgs e)
        {
            if (optText.Text != "" && speed.Text != "" && buses.Count != 0 && int.Parse(optText.Text) > 0 && int.Parse(speed.Text) > 0 && buses != null)
            {
                withoutSensorsBuses = new List<int>();
                countWithoutSensors = buses.Count;
                bool check = false;
                foreach (var bus in buses)
                {
                    if (bus.Tracker == true)
                    {
                        check = true;
                        break;
                    }
                }
                if (check)
                {
                    //foreach (var bus in buses)
                    //    bus.Stop();
                    timer1.Stop();
                    foreach (var tl in traficLights)
                        tl.TimerLight.Interval = 1;
                    Opt();
                    foreach (var tl in traficLights)
                        tl.TimerLight.Interval = 1000;
                }
                else
                {
                    return;
                }

            }
        }
        public void resMatrix()
        {
            results.Rows.Clear();
            results.Refresh();
            results.RowCount = 5;
            int i = 0;
            foreach (var pm in percentMean)
            {
                results.Rows[i].HeaderCell.Value = pm.Key.ToString();
                if (pm.Value != 0)
                    results.Rows[i].Cells[0].Value = (pm.Value / 60).ToString();
                else
                    results.Rows[i].Cells[0].Value = MainStrings.notFound;
                i += 1;
            }
        }
        public void resChart()
        {
            if (oldChart == (int)percentMean.Keys.Sum())
            {
                int iCh = 0;
                StyleManager.Clone(r);
                if (rCount != 0)
                    r.ch.Series.Add(rCount.ToString());
                r.ch.Series[rCount].LegendText = rCount.ToString();
                foreach (var pm in percentMean)
                {
                    if (pm.Value == null)
                    {
                        r.ch.Series[rCount].Points.AddY(0);
                    }
                    else
                    {
                        r.ch.Series[rCount].Points.AddY(pm.Value / 60 != 0 ? (double)pm.Value / 60 : (double)pm.Value);
                    }
                    if (rCount == 0)
                        r.ch.ChartAreas[rCount].AxisX.CustomLabels.Add(new CustomLabel(iCh, iCh + 2, pm.Key.ToString(), 0, LabelMarkStyle.LineSideMark));
                    iCh++;
                }
                r.ch.SaveImage(pathOpt + "/" + MainStrings.chart + ".jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
                r.TopMost = true;
                r.Show();
                r.BringToFront();

                rCount += 1;
            }
            else
            {
                try
                {
                    r.ch.Legends.Clear();
                    rCount = 0;
                    // r.ch.Series[rCount].LegendText = rCount.ToString();
                    foreach (var series in r.ch.Series)
                    {
                        series.Points.Clear();
                    }
                    oldChart = (int)percentMean.Keys.Sum();
                    int iCh = 0;
                    StyleManager.Clone(r);
                    if (rCount != 0)
                        r.ch.Series.Add(rCount.ToString());
                    r.ch.Series[rCount].LegendText = rCount.ToString();
                    foreach (var pm in percentMean)
                    {
                        if (pm.Value == null)
                        {
                            r.ch.Series[rCount].Points.AddY(0);
                        }
                        else
                        {
                            r.ch.Series[rCount].Points.AddY(pm.Value / 60 != 0 ? (double)pm.Value / 60 : (double)pm.Value);
                        }
                        if (rCount == 0)
                            r.ch.ChartAreas[rCount].AxisX.CustomLabels.Add(new CustomLabel(iCh, iCh + 2, pm.Key.ToString(), 0, LabelMarkStyle.LineSideMark));
                        iCh++;
                    }
                    r.ch.SaveImage(pathOpt + "/" + MainStrings.chart + ".jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    r.TopMost = true;
                    r.Show();
                    r.BringToFront();

                    rCount += 1;
                }
                catch { }
            }

        }
        public bool GetSavePictruesCheckBox()
        {
            return SavePictures;
        }
        private void loadFromToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                if (!Directory.Exists(savepath))
                {
                    dialog.SelectedPath = System.Windows.Forms.Application.StartupPath;
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
                            openEpicFormToolStripMenuItem.Enabled = true;
                            addRouteToolStripMenuItem.Enabled = true;
                            createGridToolStripMenuItem.Enabled = true;
                            foreach (var tl in traficLights)
                            {
                                tl.Stop();
                                tl.TimerLight.Dispose();
                            }
                            traficLights.Clear();
                            traficLights.TrimExcess();
                            TraficLightsInGrids.Clear();
                            stopPoints.Clear();
                            allstopPoints.Clear();
                            allstopPoints.TrimExcess();
                            LoadRoutes(dialog.SelectedPath + @"\");
                            savepath = dialog.SelectedPath;
                            File.WriteAllText("../../SaveConfig/save.txt", string.Empty);
                            using (StreamWriter fileV = new StreamWriter("../../SaveConfig/save.txt"))
                            {
                                fileV.WriteLine(savepath.ToString());
                            }
                            BringToFront();
                            MetroMessageBox.Show(this, MainStrings.done, "", MessageBoxButtons.OK, MessageBoxIcon.Question);
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
            BringToFront();
        }
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (savepath != null)
            {
                try
                {
                    openEpicFormToolStripMenuItem.Enabled = true;
                    addRouteToolStripMenuItem.Enabled = true;
                    createGridToolStripMenuItem.Enabled = true;
                    //
                    if (!Ep.IsDisposed)
                    {
                        Ep.EG.ClearSheet2();
                        Ep.Dispose();
                        Ep.Close();
                    }
                    foreach (var bus in buses)
                    {
                        bus.GetCoordinates().Clear();
                        bus.GetCoordinates().TrimExcess();
                        bus.busPic.Dispose();
                        //   mainPanel.Controls.Remove(bus.busPic);
                    }
                    buses.Clear();

                    config.Text = MainStrings.config;
                    foreach (var tl in traficLights)
                    {
                        tl.Stop();
                        tl.TimerLight.Dispose();
                    }
                    TraficLightsInGrids.Clear();
                    stopPointsInGrids.Clear();
                    V.Clear();
                    E.Clear();
                    if (G.bitmap != null)
                    {
                        ZoomHelper();
                        G.ClearSheet();
                        G.ClearSheet2();
                    }
                    routes.Clear();
                    routesEdge.Clear();
                    changeRoute.Items.Clear();
                    AllCoordinates.Clear();
                    allstopPoints.Clear();
                    stopPoints.Clear();
                    traficLights.Clear();
                    traficLights.TrimExcess();
                    LoadRoutes(savepath + @"\");
                    saveImage = sheet.Image;
                    metroTrackBar1.Value = 1;
                    wsheet = sheet.Width;
                    hsheet = sheet.Height;
                    globalMap = sheet.Image;
                    G.SetBitmap();
                    CreateGrid();
                    CreatePollutionInRoutes();
                    addInComboBox();
                    DrawGrid();
                    Matrix();
                    BringToFront();
                    //
                    Console.WriteLine("Memory used before collection:       {0:N0}",
                       GC.GetTotalMemory(false));
                    // Collect all generations of memory.
                    GC.Collect();
                    Console.WriteLine("Memory used after full collection:   {0:N0}",
                                      GC.GetTotalMemory(true));
                }
                catch (Exception exc)
                {
                    try
                    {
                        using (var dialog = new FolderBrowserDialog())
                        {
                            if (!Directory.Exists(savepath))
                            {
                                dialog.SelectedPath = System.Windows.Forms.Application.StartupPath;
                            }
                            else
                            {
                                dialog.SelectedPath = Path.GetFullPath(savepath);
                            }
                            if (dialog.ShowDialog() == DialogResult.OK)
                            {
                                if (!string.IsNullOrWhiteSpace(dialog.SelectedPath))
                                {
                                    foreach (var tl in traficLights)
                                    {
                                        tl.Stop();
                                    }
                                    TraficLightsInGrids.Clear();
                                    stopPoints.Clear();
                                    allstopPoints.Clear();
                                    LoadRoutes(dialog.SelectedPath + @"\");
                                    openEpicFormToolStripMenuItem.Enabled = true;
                                    addRouteToolStripMenuItem.Enabled = true;
                                    createGridToolStripMenuItem.Enabled = true;
                                    savepath = dialog.SelectedPath;
                                    File.WriteAllText("../../SaveConfig/save.txt", string.Empty);
                                    using (StreamWriter fileV = new StreamWriter("../../SaveConfig/save.txt"))
                                    {
                                        fileV.WriteLine(savepath.ToString());
                                    }
                                    BringToFront();
                                    MetroMessageBox.Show(this, MainStrings.done, "", MessageBoxButtons.OK, MessageBoxIcon.Question);
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
                        dialog.SelectedPath = System.Windows.Forms.Application.StartupPath;
                    }
                    else
                    {
                        dialog.SelectedPath = Path.GetFullPath(savepath);
                    }
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        if (!string.IsNullOrWhiteSpace(dialog.SelectedPath))
                        {
                            foreach (var tl in traficLights)
                            {
                                tl.Stop();
                            }
                            TraficLightsInGrids.Clear();
                            stopPoints.Clear();
                            allstopPoints.Clear();
                            LoadRoutes(dialog.SelectedPath + @"\");
                            openEpicFormToolStripMenuItem.Enabled = true;
                            addRouteToolStripMenuItem.Enabled = true;
                            createGridToolStripMenuItem.Enabled = true;
                            savepath = dialog.SelectedPath;
                            File.WriteAllText("../../SaveConfig/save.txt", string.Empty);
                            using (StreamWriter fileV = new StreamWriter("../../SaveConfig/save.txt"))
                            {
                                fileV.WriteLine(savepath.ToString());
                            }
                            BringToFront();
                            MetroMessageBox.Show(this, MainStrings.done, "", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        }

                    }
                }
            }
            BringToFront();
            changeRoute.Text = MainStrings.network;
        }

        private void drawEdgeButton_Click(object sender, EventArgs e)
        {
            G.ClearSheet();
            G.DrawALLGraph(V, E);
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
                checkBuses();
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
                G.DrawALLGraph(routes[changeRoute.Text], routesEdge[changeRoute.Text], 1);
                checkBusesOnRoute();
            }
            selectRoute.Enabled = true;
            label12.Visible = false;
            delAllBusesOnRoute.Enabled = true;
            stopPointButton.Enabled = true;
            sheet.Image = G.GetBitmap();
            selected1 = -1;
            selected2 = -1;
            selected = new List<int>();
            DrawGrid();
        }

        string saveF = "xml";
        private void SaveRoutes(string saveFormat = "xml", string save = "../../Data/")
        {
            try
            {
                loadingForm = new LoadingForm();
                loadingForm.loading.Value = 0;
                loadingForm.Show();
                loadingForm.close = false;
                loadingForm.loading.Maximum = 100;
                if (saveFormat == "xml")
                {
                    XmlSerializer serializerV = new XmlSerializer(typeof(List<Vertex>));
                    XmlSerializer serializerE = new XmlSerializer(typeof(List<Edge>));

                    File.Delete(save + "Vertices.xml");
                    using (FileStream fileV = new FileStream(save + "Vertices.xml", FileMode.OpenOrCreate))
                    {
                        serializerV.Serialize(fileV, V);
                        Console.WriteLine("Объект сериализован");
                        fileV.Close();
                    }
                    loadingForm.loading.Value = 10;
                    File.Delete(save + "Edges.xml");
                    using (FileStream fileE = new FileStream(save + "Edges.xml", FileMode.OpenOrCreate))
                    {
                        serializerE.Serialize(fileE, E);
                        Console.WriteLine("Объект сериализован");
                        fileE.Close();
                    }
                    loadingForm.loading.Value = 20;
                    File.Delete(save + "Buses.xml");
                    XmlSerializer serializerAllBuses = new XmlSerializer(typeof(List<Bus>));
                    using (FileStream fileB = new FileStream(save + "Buses.xml", FileMode.OpenOrCreate))
                    {
                        serializerAllBuses.Serialize(fileB, buses);
                        Console.WriteLine("Объект сериализован");
                    }
                    loadingForm.loading.Value = 30;

                    //AsyncCreateAllCoordinates();
                    File.Delete(save + "AllCoordinates.xml");
                    XmlSerializer serializerAllCoor = new XmlSerializer(typeof(SerializableDictionary<string, List<Point>>));
                    using (FileStream fileA = new FileStream(save + "AllCoordinates.xml", FileMode.OpenOrCreate))
                    {
                        serializerAllCoor.Serialize(fileA, AllCoordinates);
                        Console.WriteLine("Объект сериализован");
                    }
                    loadingForm.loading.Value = 50;
                    File.Delete(save + "AllGridsInRoutes.xml");
                    XmlSerializer serializerAllGridsInRoutes = new XmlSerializer(typeof(SerializableDictionary<string, List<int>>));
                    using (FileStream fileAG = new FileStream(save + "AllGridsInRoutes.xml", FileMode.OpenOrCreate))
                    {
                        serializerAllGridsInRoutes.Serialize(fileAG, AllGridsInRoutes);
                        Console.WriteLine("Объект сериализован");
                    }
                    loadingForm.loading.Value = 60;
                    XmlSerializer Ver = new XmlSerializer(typeof(SerializableDictionary<string, List<Vertex>>));
                    XmlSerializer Edge = new XmlSerializer(typeof(SerializableDictionary<string, List<Edge>>));

                    File.Delete(save + "vertexRoutes.xml");
                    using (FileStream fileV = new FileStream(save + "vertexRoutes.xml", FileMode.OpenOrCreate))
                    {
                        Ver.Serialize(fileV, routes);
                        Console.WriteLine("Объект сериализован");
                    }
                    loadingForm.loading.Value = 70;
                    File.Delete(save + "StopPoints.xml");
                    using (FileStream fileV = new FileStream(save + "StopPoints.xml", FileMode.OpenOrCreate))
                    {
                        Ver.Serialize(fileV, stopPoints);
                        Console.WriteLine("Объект сериализован");
                    }
                    File.Delete(save + "allStopPoints.xml");
                    using (FileStream fileV = new FileStream(save + "allStopPoints.xml", FileMode.OpenOrCreate))
                    {
                        XmlSerializer V = new XmlSerializer(typeof(List<Vertex>));
                        V.Serialize(fileV, allstopPoints);
                        Console.WriteLine("Объект сериализован");
                    }
                    loadingForm.loading.Value = 80;

                    File.Delete(save + "edgeRoutes.xml");
                    using (FileStream fileE = new FileStream(save + "edgeRoutes.xml", FileMode.OpenOrCreate))
                    {
                        Edge.Serialize(fileE, routesEdge);
                        Console.WriteLine("Объект сериализован");

                    }
                    foreach (var tl in traficLights)
                    {
                        tl.Stop();
                    }
                    File.Delete(save + "traficLights.xml");
                    using (FileStream fileTL = new FileStream(save + "traficLights.xml", FileMode.OpenOrCreate))
                    {
                        XmlSerializer tl = new XmlSerializer(typeof(List<TraficLight>));
                        tl.Serialize(fileTL, traficLights);

                        Console.WriteLine("Объект сериализован");

                    }
                    File.Delete(save + "grid.xml");
                    using (FileStream fileTL = new FileStream(save + "grid.xml", FileMode.OpenOrCreate))
                    {
                        XmlSerializer tl = new XmlSerializer(typeof(Grid));
                        tl.Serialize(fileTL, g);

                        Console.WriteLine("Объект сериализован");

                    }
                    loadingForm.loading.Value = 90;
                    saveF = saveFormat;
                    loadingForm.loading.Value = 100;
                    loadingForm.close = true;
                    loadingForm.Close();
                    return;
                }
                if (saveFormat == "json")
                {
                    loadingForm.loading.Value = 10;
                    string json = JsonConvert.SerializeObject(V);
                    File.WriteAllText(save + "Vertices.json", json);
                    loadingForm.loading.Value = 20;
                    json = JsonConvert.SerializeObject(E);
                    File.WriteAllText(save + "Edges.json", json);
                    loadingForm.loading.Value = 30;
                    json = JsonConvert.SerializeObject(buses);
                    File.WriteAllText(save + "Buses.json", json);
                    loadingForm.loading.Value = 50;
                    //AsyncCreateAllCoordinates();
                    json = JsonConvert.SerializeObject(AllCoordinates);
                    File.WriteAllText(save + "AllCoordinates.json", json);
                    loadingForm.loading.Value = 60;
                    json = JsonConvert.SerializeObject(AllGridsInRoutes);
                    File.WriteAllText(save + "AllGridsInRoutes.json", json);
                    loadingForm.loading.Value = 70;
                    json = JsonConvert.SerializeObject(stopPoints);
                    File.WriteAllText(save + "StopPoints.json", json);
                    json = JsonConvert.SerializeObject(allstopPoints);
                    File.WriteAllText(save + "allStopPoints.json", json);
                    loadingForm.loading.Value = 80;
                    json = JsonConvert.SerializeObject(routes);
                    File.WriteAllText(save + "vertexRoutes.json", json);
                    json = JsonConvert.SerializeObject(g);
                    File.WriteAllText(save + "grid.json", json);
                    loadingForm.loading.Value = 90;
                    json = JsonConvert.SerializeObject(routesEdge);
                    File.WriteAllText(save + "edgeRoutes.json", json);
                    json = JsonConvert.SerializeObject(traficLights);
                    File.WriteAllText(save + "traficLights.json", json);
                    saveF = saveFormat;
                    loadingForm.loading.Value = 100;
                    loadingForm.close = true;
                    loadingForm.Close();
                    return;
                }

            }
            catch (Exception exc)
            {
                StackTrace stackTrace = new StackTrace(exc, true);
                if (stackTrace.FrameCount > 0)
                {
                    MetroMessageBox.Show(this, $"{exc.StackTrace}", MainStrings.error, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    loadingForm.close = true;
                    loadingForm.Close();
                }

            }
        }

        private void Load_Click(object sender, EventArgs e)
        {
            LoadRoutes();
        }

        public List<string> extensionsFiles(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            var res = dir.GetFiles();
            List<string> extensions = new List<string>();
            foreach (var r in res)
                extensions.Add(r.Extension);
            return extensions;
        }

        private void deleteAll()
        {
            TraficLightsInGrids.Clear();
            allstopPoints.Clear();
            stopPoints.Clear();
            stopPointsInGrids.Clear();
            V.Clear();
            E.Clear();
            routes.Clear();
            routesEdge.Clear();

        }

        private void LoadRoutes(string load = "../../Data/")
        {
            try
            {
                deleteAll();
                DisplayEpicenters.path = load;
                sheet.Image = Image.FromFile(load + "/Map.png");
                saveImage = sheet.Image;
                metroTrackBar1.Value = 1;
                wsheet = sheet.Width;
                hsheet = sheet.Height;
                //ZoomHelper(); //дроп ошибки если загружать конфиг в первый раз
                loadingForm = new LoadingForm
                {
                    close = false
                };
                loadingForm.Show();
                loadingForm.loading.Value = 0;
                routePoints.Clear();
                globalMap = sheet.Image;
                G.SetBitmap();
                config.Text = MainStrings.config + load;
                if (File.Exists(load + "Vertices.xml"))
                {
                    using (StreamReader reader = new StreamReader(load + "Vertices.xml"))
                    {
                        XmlSerializer deserializerV = new XmlSerializer(typeof(List<Vertex>));
                        V = (List<Vertex>)deserializerV.Deserialize(reader);
                    }
                }

                if (File.Exists(load + "Vertices.json"))
                {
                    using (StreamReader reader = new StreamReader(load + "Vertices.json"))
                    {
                        V = JsonConvert.DeserializeObject<List<Vertex>>(File.ReadAllText(load + "Vertices.json"));
                    }
                }
                loadingForm.loading.Value = 10;

                if (File.Exists(load + "Edges.xml"))
                {
                    using (StreamReader reader = new StreamReader(load + "Edges.xml"))
                    {
                        XmlSerializer deserializerE = new XmlSerializer(typeof(List<Edge>));
                        E = (List<Edge>)deserializerE.Deserialize(reader);
                    }
                }

                if (File.Exists(load + "Edges.json"))
                {
                    using (StreamReader reader = new StreamReader(load + "Edges.json"))
                    {
                        E = JsonConvert.DeserializeObject<List<Edge>>(reader.ReadToEnd());
                    }
                }

                if (File.Exists(load + "grid.xml"))
                {
                    using (StreamReader reader = new StreamReader(load + "grid.xml"))
                    {
                        XmlSerializer deserializerV = new XmlSerializer(typeof(Grid));
                        g = (Grid)deserializerV.Deserialize(reader);
                        g.gridHeight = 40;
                        g.gridWidth = 80;
                    }
                }

                if (File.Exists(load + "grid.json"))
                {
                    using (StreamReader reader = new StreamReader(load + "grid.json"))
                    {
                        g = JsonConvert.DeserializeObject<Grid>(reader.ReadToEnd());
                        g.gridHeight = 40;
                        g.gridWidth = 80;
                    }
                }

                loadingForm.loading.Value = 20;

                if (File.Exists(load + "StopPoints.xml"))
                {
                    using (StreamReader reader = new StreamReader(load + "StopPoints.xml"))
                    {
                        XmlSerializer deserializerV = new XmlSerializer(typeof(SerializableDictionary<string, List<Vertex>>));
                        stopPoints = (SerializableDictionary<string, List<Vertex>>)deserializerV.Deserialize(reader);
                        foreach (var sp in stopPoints.Values)
                        {
                            foreach (var s in sp)
                                if (!allstopPoints.Contains(s))
                                    allstopPoints.Add(s);
                        }
                        stopPointsInGrids = new SerializableDictionary<string, List<int>>();
                        foreach (var StopList in stopPoints)
                        {
                            stopPointsInGrids.Add(StopList.Key, new List<int>());
                            foreach (var vertex in StopList.Value)
                            {
                                stopPointsInGrids[StopList.Key].Add(vertex.gridNum);
                            }

                        }
                    }
                }

                if (File.Exists(load + "allStopPoints.xml"))
                {
                    using (StreamReader reader = new StreamReader(load + "allStopPoints.xml"))
                    {
                        XmlSerializer deserializerV = new XmlSerializer(typeof(List<Vertex>));
                        allstopPoints = (List<Vertex>)deserializerV.Deserialize(reader);

                    }
                }

                if (File.Exists(load + "StopPoints.json"))
                {
                    using (StreamReader reader = new StreamReader(load + "StopPoints.json"))
                    {
                        stopPoints = JsonConvert.DeserializeObject<SerializableDictionary<string, List<Vertex>>>(File.ReadAllText(load + "StopPoints.json"));
                        foreach (var sp in stopPoints.Values)
                        {
                            foreach (var s in sp)
                                if (!allstopPoints.Contains(s))
                                    allstopPoints.Add(s);
                        }
                        stopPointsInGrids = new SerializableDictionary<string, List<int>>();
                        foreach (var StopList in stopPoints)
                        {
                            stopPointsInGrids.Add(StopList.Key, new List<int>());
                            foreach (var vertex in StopList.Value)
                            {
                                stopPointsInGrids[StopList.Key].Add(vertex.gridNum);
                            }

                        }

                    }
                }

                if (File.Exists(load + "allStopPoints.json"))
                {
                    using (StreamReader reader = new StreamReader(load + "allStopPoints.json"))
                    {
                        allstopPoints = JsonConvert.DeserializeObject<List<Vertex>>(File.ReadAllText(load + "allStopPoints.json"));
                    }
                }

                loadingForm.loading.Value = 30;


                if (File.Exists(load + "traficLights.xml"))
                {
                    XmlSerializer tl = new XmlSerializer(typeof(List<TraficLight>));
                    using (StreamReader reader = new StreamReader(load + "traficLights.xml"))
                        traficLights = (List<TraficLight>)tl.Deserialize(reader);
                    TraficLightsInGrids = new List<int>();
                    foreach (var item in traficLights)
                    {
                        TraficLightsInGrids.Add(item.gridNum);
                    }
                    foreach (var tll in traficLights)
                    {
                        tll.Set();
                        tll.Start();
                    }
                    sheet.Image = G.GetBitmap();
                }
                if (File.Exists(load + "traficLights.json"))
                {
                    using (StreamReader reader = new StreamReader(load + "traficLights.json"))
                    {
                        traficLights = JsonConvert.DeserializeObject<List<TraficLight>>(reader.ReadToEnd());
                        TraficLightsInGrids = new List<int>();
                        foreach (var item in traficLights)
                        {
                            TraficLightsInGrids.Add(item.gridNum);
                        }
                        foreach (var tll in traficLights)
                        {
                            tll.Set();
                            tll.Start();
                        }
                        sheet.Image = G.GetBitmap();
                    }
                }
                loadingForm.loading.Value = 40;

                if (buses != null)
                {
                    buses.Clear();
                }

                if (File.Exists(load + "Buses.json"))
                {
                    using (StreamReader reader = new StreamReader(load + "Buses.json"))
                    {
                        buses = JsonConvert.DeserializeObject<List<Bus>>(reader.ReadToEnd());
                    }
                }

                XmlSerializer deserializerAllBuses = new XmlSerializer(typeof(List<Bus>));
                if (File.Exists(load + "Buses.xml"))
                {
                    using (StreamReader reader = new StreamReader(load + "Buses.xml"))
                    {
                        buses = (List<Bus>)deserializerAllBuses.Deserialize(reader);
                    }
                }
                loadingForm.loading.Value = 60;
                foreach (var tl in traficLights)
                {
                    tl.Start();
                }
                Image num;
                Bitmap original = new Bitmap(1, 1);
                foreach (var x in buses)
                {

                    if (x.Tracker == true)
                    {
                        Rectangle rect = new Rectangle(0, 0, 200, 100);
                        x.busPic = new Bitmap(Bus.busImg);
                        x.busPic = new Bitmap(x.busPic, new Size(15, 15));
                        num = new Bitmap(x.busPic.Height, x.busPic.Width);
                        using (Graphics gr = Graphics.FromImage(num))
                        {
                            using (Font font = new Font("Arial", 10))
                            {
                                // Заливаем фон нужным цветом.
                                gr.FillRectangle(Brushes.Transparent, rect);

                                // Выводим текст.
                                gr.DrawString(
                                    x.route.ToString(),
                                    font,
                                    Brushes.Black, // цвет текста
                                    rect, // текст будет вписан в указанный прямоугольник
                                    StringFormat.GenericTypographic
                                    );
                            }
                        }

                        original = new Bitmap(Math.Max(x.busPic.Width, num.Width), Math.Max(x.busPic.Height, num.Height) * 2); //load the image file
                        using (Graphics graphics = Graphics.FromImage(original))
                        {

                            graphics.DrawImage(x.busPic, 0, 0);
                            graphics.DrawImage(num, 0, 15);
                            graphics.Dispose();

                        }
                        //  bitmap = new Bitmap(original, new Size(15, 15));
                    }
                    else
                    {
                        Rectangle rect = new Rectangle(0, 0, 200, 100);
                        x.busPic = new Bitmap(Bus.offBusImg);
                        x.busPic = new Bitmap(x.busPic, new Size(15, 15));
                        num = new Bitmap(x.busPic.Height, x.busPic.Width);
                        using (Graphics gr = Graphics.FromImage(num))
                        {
                            using (Font font = new Font("Arial", 10))
                            {
                                // Заливаем фон нужным цветом.
                                gr.FillRectangle(Brushes.Transparent, rect);

                                // Выводим текст.
                                gr.DrawString(
                                    x.route.ToString(),
                                    font,
                                    Brushes.Black, // цвет текста
                                    rect, // текст будет вписан в указанный прямоугольник
                                    StringFormat.GenericTypographic
                                    );
                            }
                        }

                        original = new Bitmap(Math.Max(x.busPic.Width, num.Width), Math.Max(x.busPic.Height, num.Height) * 2); //load the image file
                        using (Graphics graphics = Graphics.FromImage(original))
                        {

                            graphics.DrawImage(x.busPic, 0, 0);
                            graphics.DrawImage(num, 0, 15);
                            graphics.Dispose();

                        }
                    }


                    x.busPic = original;//res;// new Bitmap(res, new Size(1000, 1000));

                    x.skip = 5;
                    x.skipStops = 5;
                    x.skipEnd = 5;

                }
                //
                timer1.Start();
                //
                XmlSerializer ver = new XmlSerializer(typeof(List<Vertex>));
                XmlSerializer ed = new XmlSerializer(typeof(List<Edge>));


                XmlSerializer Ver = new XmlSerializer(typeof(SerializableDictionary<string, List<Vertex>>));
                XmlSerializer Edge = new XmlSerializer(typeof(SerializableDictionary<string, List<Edge>>));

                if (File.Exists(load + "vertexRoutes.xml"))
                {
                    using (StreamReader reader = new StreamReader(load + "vertexRoutes.xml"))
                        routes = (SerializableDictionary<string, List<Vertex>>)Ver.Deserialize(reader);
                }

                if (File.Exists(load + "vertexRoutes.json"))
                {
                    using (StreamReader reader = new StreamReader(load + "vertexRoutes.json"))
                    {
                        routes = JsonConvert.DeserializeObject<SerializableDictionary<string, List<Vertex>>>(reader.ReadToEnd());
                    }
                }
                loadingForm.loading.Value = 80;
                if (File.Exists(load + "edgeRoutes.xml"))
                {
                    using (StreamReader reader = new StreamReader(load + "edgeRoutes.xml"))
                        routesEdge = (SerializableDictionary<string, List<Edge>>)Edge.Deserialize(reader);
                    saveF = "xml";
                }

                if (File.Exists(load + "edgeRoutes.json"))
                {
                    using (StreamReader reader = new StreamReader(load + "edgeRoutes.json"))
                    {
                        routesEdge = JsonConvert.DeserializeObject<SerializableDictionary<string, List<Edge>>>(reader.ReadToEnd());
                    }
                    saveF = "json";
                }
                XmlSerializer deserializerAllCoor = new XmlSerializer(typeof(SerializableDictionary<string, List<Point>>));
                if (File.Exists(load + "AllCoordinates.xml"))
                {
                    using (StreamReader reader = new StreamReader(load + "AllCoordinates.xml"))
                        AllCoordinates = (SerializableDictionary<string, List<Point>>)deserializerAllCoor.Deserialize(reader);
                }

                if (File.Exists(load + "AllCoordinates.json"))
                {
                    using (StreamReader reader = new StreamReader(load + "AllCoordinates.json"))
                    {
                        AllCoordinates = JsonConvert.DeserializeObject<SerializableDictionary<string, List<Point>>>(reader.ReadToEnd());
                    }
                }
                XmlSerializer deserializerAllGridsInRoutes = new XmlSerializer(typeof(SerializableDictionary<string, List<int>>));
                if (File.Exists(load + "AllGridsInRoutes.xml"))
                {
                    using (StreamReader reader = new StreamReader(load + "AllGridsInRoutes.xml"))
                        AllGridsInRoutes = (SerializableDictionary<string, List<int>>)deserializerAllGridsInRoutes.Deserialize(reader);
                }

                if (File.Exists(load + "AllGridsInRoutes.json"))
                {
                    using (StreamReader reader = new StreamReader(load + "AllGridsInRoutes.json"))
                    {
                        AllGridsInRoutes = JsonConvert.DeserializeObject<SerializableDictionary<string, List<int>>>(reader.ReadToEnd());
                    }
                }
                // loadingForm.loading.Value = 50;
                loadingForm.loading.Value = 90;
                openEpicFormToolStripMenuItem.Enabled = true;
                CreateGrid();
                CreatePollutionInRoutes();
                CreateOneRandomEpicenter(EpicSizeParam, null);

                addInComboBox();
                G.ClearSheet();
                G.DrawALLGraph(V, E);
                sheet.Image = G.GetBitmap();
                DrawGrid();
                if (Ep != null)
                    Ep.Close();
                Ep = new DisplayEpicenters(this);
                StyleManager.Clone(Ep);
                Ep.Show();
                loadingForm.loading.Value = 100;
                loadingForm.close = true;
                loadingForm.Close();

                if (AllCoordinates.Count != 0)
                {
                    CreateAllCoordinates();
                }
                GC.Collect(GC.MaxGeneration);
            }
            catch (Exception exc)
            {
                StackTrace stackTrace = new StackTrace(exc, true);
                if (stackTrace.FrameCount > 0)
                {
                    MetroMessageBox.Show(this, $"{exc.StackTrace}", MainStrings.error, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    loadingForm.close = true;
                    loadingForm.Close();
                }

            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Matrix();
        }


        public double GetDistance(double x1, double y1, double x2, double y2)
        {
            return (int)Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }

        List<int> selected = new List<int>();

        Constructor c = new Constructor();

        static public int refreshLights = 0;

        public static bool flag = false;

        private async void asyncCheckV(MouseEventArgs e, bool check)
        {
            await Task.Run(() => CheckV(e, check));
        }

        private bool CheckV(MouseEventArgs e, bool check)
        {
            for (int i = 0; i < V.Count; i++)
            {
                if (Math.Pow((V[i].X - e.X / zoom), 2) + Math.Pow((V[i].Y - e.Y / zoom), 2) <= G.R * G.R)
                {
                    return true;
                }
            }
            return false;
        }

        private void sheet_MouseClick_1(object sender, MouseEventArgs e)
        {
            if (changeRoute.Text == MainStrings.network)
            {
                if (selectRoute.Enabled == false)
                {
                    bool check = false;
                    check = CheckV(e, check);
                    c.SelectRoute(e, V, E, G, sheet, c, selected, check);
                }
                if (addTraficLight.Enabled == false)
                {
                    if (firstCrossRoads > 0 || secondCrossRoads > 0)
                    {
                        if (firstCrossRoads > 0)
                        {
                            label12.Visible = true;
                            label12.Text = MainStrings.putTrafficLights1 + " " + firstCrossRoads.ToString();
                            foreach (var gridPart in GetTheGrid())
                            {
                                if (((e.X > gridPart.x * zoom) && (e.Y > gridPart.y * zoom)) && ((e.X < gridPart.x * zoom + GridPart.Width * zoom) && (e.Y < gridPart.y * zoom + GridPart.Height * zoom)))
                                {
                                    traficLights.Add(new TraficLight(e.X / zoom, e.Y / zoom, GetTheGrid().IndexOf(gridPart), firstCrossRoadsGreenLight, firstCrossRoadsRedLight));
                                    TraficLightsInGrids.Add(GetTheGrid().IndexOf(gridPart));
                                    G.DrawGreenVertex(e.X / zoom, e.Y / zoom);
                                    firstCrossRoads -= 1;
                                    if (firstCrossRoads == 0)
                                        label12.Text = MainStrings.putTrafficLights2 + " " + secondCrossRoads.ToString();
                                    sheet.Image = G.GetBitmap();
                                    DrawGrid();
                                    break;
                                }
                            }
                            return;
                        }
                        if (firstCrossRoads <= 0 && secondCrossRoads > 0)
                        {
                            label12.Text = MainStrings.putTrafficLights2 + " " + secondCrossRoads.ToString();

                            foreach (var gridPart in GetTheGrid())
                            {
                                if (((e.X > gridPart.x * zoom) && (e.Y > gridPart.y * zoom)) && ((e.X < gridPart.x * zoom + GridPart.Width * zoom) && (e.Y < gridPart.y * zoom + GridPart.Height * zoom)))
                                {
                                    traficLights.Add(new TraficLight(e.X / zoom, e.Y / zoom, GetTheGrid().IndexOf(gridPart), firstCrossRoadsRedLight, firstCrossRoadsGreenLight));
                                    TraficLightsInGrids.Add(GetTheGrid().IndexOf(gridPart));
                                    traficLights.Last().tick = firstCrossRoadsRedLight + 2;
                                    traficLights.Last().Status = Status.RED;
                                    label12.Text = MainStrings.putTrafficLights2 + " " + (secondCrossRoads - 1).ToString();
                                    G.DrawSelectedVertex(e.X / zoom, e.Y / zoom);
                                    sheet.Image = G.GetBitmap();
                                    DrawGrid();
                                    secondCrossRoads -= 1;
                                    break;
                                }

                            }
                        }
                    }
                    if (firstCrossRoads <= 0 && secondCrossRoads <= 0)
                    {
                        label12.Visible = false;
                        traficLights.ForEach((tl) =>
                        {
                            tl.Set();
                            tl.Start();
                        });
                        selectedRoute = null;
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
                if (stopPointButton.Enabled == false)
                {
                    foreach (var gridPart in GetTheGrid())
                    {
                        if (((e.X > gridPart.x * zoom) && (e.Y > gridPart.y * zoom)) && ((e.X < gridPart.x * zoom + GridPart.Width * zoom) && (e.Y < gridPart.y * zoom + GridPart.Height * zoom)))
                        {
                            allstopPoints.Add(new Vertex(e.X / zoom, e.Y / zoom));
                            G.DrawStopVertex(e.X / zoom, e.Y / zoom);
                            sheet.Image = G.GetBitmap();
                            DrawGrid();
                        }
                    }
                }
                if (selectButton.Enabled == false)
                {
                    c.Select(e, V, E, G, sheet, 0);
                }
                //нажата кнопка "рисовать вершину"
                if (drawVertexButton.Enabled == false)
                {
                    c.DrawVertex(e, V, G, sheet);
                }
                //нажата кнопка "рисовать ребро"
                if (drawEdgeButton.Enabled == false)
                {
                    c.AsDrawEdge(e, V, E, G, sheet, 0);
                }
                //нажата кнопка "удалить элемент"
                if (deleteButton.Enabled == false)
                {
                    switch (delType)
                    {
                        case deleteType.All:
                            c.AsDelete(e, V, E, sheet, G, routesEdge);
                            break;
                        case deleteType.BusStops:
                            c.deleteBS(e, V, E, sheet, G, routesEdge);
                            break;
                        case deleteType.TrafficLight:
                            c.deleteTF(e, V, E, sheet, G, routesEdge);
                            break;
                        case deleteType.VertexAndEdge:
                            c.deleteVE(e, V, E, sheet, G, routesEdge);
                            break;
                    }
                    if (flag)
                    {
                        G.ClearSheet();
                        G.DrawALLGraph(V, E);
                        sheet.Image = G.GetBitmap();
                        DrawGrid();
                    }
                }
                return;
            }

            if (changeRoute.SelectedIndex > 1)
            {
                List<Vertex> routeV = routes[changeRoute.Text];
                if (stopPointButton.Enabled == false)
                {
                    foreach (var sp in allstopPoints)
                    {
                        if (Math.Pow((sp.X - e.X / zoom), 2) + Math.Pow((sp.Y - e.Y / zoom), 2) <= G.R * G.R)
                        {
                            foreach (var gridPart in GetTheGrid())
                            {
                                if (((e.X > gridPart.x * zoom) && (e.Y > gridPart.y * zoom)) && ((e.X < gridPart.x * zoom + GridPart.Width * zoom) && (e.Y < gridPart.y * zoom + GridPart.Height * zoom)))
                                {

                                    if (!stopPoints[changeRoute.Text].Contains(new Vertex(sp.X, sp.Y)))
                                    {
                                        if (stopPoints.ContainsKey(changeRoute.Text))
                                        {
                                            stopPoints[changeRoute.Text].Add(new Vertex(sp.X, sp.Y));
                                            stopPoints[changeRoute.Text].Last().gridNum = GetTheGrid().IndexOf(gridPart);
                                            if (stopPointsInGrids.ContainsKey(changeRoute.Text))
                                                stopPointsInGrids[changeRoute.Text].Add(GetTheGrid().IndexOf(gridPart)); //дроп ошибки
                                            else
                                            {
                                                stopPointsInGrids.Add(changeRoute.Text, new List<int>());
                                                stopPointsInGrids[changeRoute.Text].Add(GetTheGrid().IndexOf(gridPart));
                                            }

                                        }
                                        else
                                        {
                                            stopPoints.Add(changeRoute.Text, new List<Vertex>());
                                            stopPointsInGrids.Add(changeRoute.Text, new List<int>());
                                            if (!stopPoints[changeRoute.Text].Contains(new Vertex(sp.X, sp.Y)))
                                            {
                                                if (stopPoints.ContainsKey(changeRoute.Text) && stopPointsInGrids.ContainsKey(changeRoute.Text))
                                                {
                                                    stopPoints[changeRoute.Text].Add(new Vertex(sp.X, sp.Y));
                                                    stopPoints[changeRoute.Text].Last().gridNum = GetTheGrid().IndexOf(gridPart);
                                                    stopPointsInGrids[changeRoute.Text].Add(GetTheGrid().IndexOf(gridPart)); //дроп ошибки
                                                }
                                            }
                                        }
                                    }

                                    G.DrawStopRouteVertex(sp.X, sp.Y);
                                    sheet.Image = G.GetBitmap();
                                    DrawGrid();

                                    break;
                                }
                            }

                        }
                    }
                }
                //нажата кнопка "выбрать вершину", ищем степень вершины
                if (selectButton.Enabled == false)
                {
                    c.Select(e, routeV, routesEdge[changeRoute.Text], G, sheet, 1);
                }
                if (selectRoute.Enabled == false)
                {
                    c.SelectRouteInRoute(e, routeV, routesEdge[changeRoute.Text], G, sheet, selected);

                }
                //нажата кнопка addBus
                if (addBus.Enabled == false)
                {
                    try
                    {
                        if (AllCoordinates[changeRoute.Text].Count != 0)
                        {
                            int pos = 0;

                            if (e.Button == MouseButtons.Left)
                            {
                                double min = Math.Pow((AllCoordinates[changeRoute.Text].Last().X - e.X / zoom), 2) + Math.Pow((AllCoordinates[changeRoute.Text].Last().Y - e.Y / zoom), 2);
                                for (int i = 0; i < AllCoordinates[changeRoute.Text].Count; i++)
                                {
                                    if (Math.Pow((AllCoordinates[changeRoute.Text][i].X - e.X / zoom), 2) + Math.Pow((AllCoordinates[changeRoute.Text][i].Y - e.Y / zoom), 2) <= G.R * G.R * 500)
                                    {
                                        if ((Math.Pow((AllCoordinates[changeRoute.Text][i].X - e.X / zoom), 2) + Math.Pow((AllCoordinates[changeRoute.Text][i].Y - e.Y / zoom), 2) < min))
                                        {
                                            min = Math.Pow((AllCoordinates[changeRoute.Text][i].X - e.X / zoom), 2) + Math.Pow((AllCoordinates[changeRoute.Text][i].Y - e.Y / zoom), 2);
                                            pos = i;
                                        }
                                    }
                                }
                            }

                            if (trackerCheck.Checked == true)
                            {
                                Rectangle rect = new Rectangle(0, 0, 200, 100);
                                Bitmap busPic = new Bitmap(Bus.busImg);
                                busPic = new Bitmap(busPic, new Size(15, 15));
                                Bitmap num = new Bitmap(busPic.Height, busPic.Width);
                                using (Graphics gr = Graphics.FromImage(num))
                                {
                                    using (Font font = new Font("Arial", 10))
                                    {
                                        // Заливаем фон нужным цветом.
                                        gr.FillRectangle(Brushes.Transparent, rect);

                                        // Выводим текст.
                                        gr.DrawString(
                                            changeRoute.Text,
                                            font,
                                            Brushes.Black, // цвет текста
                                            rect, // текст будет вписан в указанный прямоугольник
                                            StringFormat.GenericTypographic
                                            );
                                    }
                                }

                                Bitmap original = new Bitmap(Math.Max(busPic.Width, num.Width), Math.Max(busPic.Height, num.Height) * 2); //load the image file
                                using (Graphics graphics = Graphics.FromImage(original))
                                {

                                    graphics.DrawImage(busPic, 0, 0);
                                    graphics.DrawImage(num, 0, 15);
                                    graphics.Dispose();

                                }
                                buses.Add(new Bus(original, pos, backsideCheck.Checked, changeRoute.Text, AllCoordinates[changeRoute.Text], true));
                            }
                            else
                            {
                                Rectangle rect = new Rectangle(0, 0, 200, 100);
                                Bitmap busPic = new Bitmap(Bus.offBusImg);
                                busPic = new Bitmap(busPic, new Size(15, 15));
                                Bitmap num = new Bitmap(busPic.Height, busPic.Width);
                                using (Graphics gr = Graphics.FromImage(num))
                                {
                                    using (Font font = new Font("Arial", 10))
                                    {
                                        // Заливаем фон нужным цветом.
                                        gr.FillRectangle(Brushes.Transparent, rect);

                                        // Выводим текст.
                                        gr.DrawString(
                                            changeRoute.Text,
                                            font,
                                            Brushes.Black, // цвет текста
                                            rect, // текст будет вписан в указанный прямоугольник
                                            StringFormat.GenericTypographic
                                            );
                                    }
                                }

                                Bitmap original = new Bitmap(Math.Max(busPic.Width, num.Width), Math.Max(busPic.Height, num.Height) * 2); //load the image file
                                using (Graphics graphics = Graphics.FromImage(original))
                                {

                                    graphics.DrawImage(busPic, 0, 0);
                                    graphics.DrawImage(num, 0, 15);
                                    graphics.Dispose();

                                }
                                buses.Add(new Bus(original, pos, backsideCheck.Checked, changeRoute.Text, AllCoordinates[changeRoute.Text], false));
                            };
                        }
                    }
                    catch
                    {
                        MetroMessageBox.Show(this, MainStrings.error);
                    }


                }
                if (deleteBus.Enabled == false)
                {
                    if (AllCoordinates[changeRoute.Text].Count != 0)
                    {
                        int? pos = null;
                        double min = Math.Pow((sheet.Image.Width - (e.X / zoom + mainPanel.AutoScrollPosition.X)), 2) + Math.Pow((sheet.Image.Height - (e.Y / zoom + mainPanel.AutoScrollPosition.Y)), 2);
                        for (int i = 0; i < buses.Count; i++)
                        {
                            if (Math.Pow((buses[i].Coordinates[buses[i].PositionAt].X - (e.X / zoom + mainPanel.AutoScrollPosition.X)), 2) + Math.Pow((buses[i].Coordinates[buses[i].PositionAt].Y - (e.Y / zoom + mainPanel.AutoScrollPosition.Y)), 2) <= buses[i].R * buses[i].R * 500)
                            {
                                if (buses[i].route == changeRoute.Text)
                                {
                                    if (Math.Pow((buses[i].Coordinates[buses[i].PositionAt].X - (e.X / zoom + mainPanel.AutoScrollPosition.X)), 2) + Math.Pow((buses[i].Coordinates[buses[i].PositionAt].Y - (e.Y / zoom + mainPanel.AutoScrollPosition.Y)), 2) < min)
                                    {
                                        min = Math.Pow((buses[i].Coordinates[buses[i].PositionAt].X - (e.X / zoom + mainPanel.AutoScrollPosition.X)), 2) + Math.Pow((buses[i].Coordinates[buses[i].PositionAt].Y - (e.Y / zoom + mainPanel.AutoScrollPosition.Y)), 2);
                                        pos = i;
                                    }

                                }
                            }
                        }
                        if (pos != null)
                        {
                            buses.Remove(buses[int.Parse(pos.ToString())]);
                        }
                        G.ClearSheet();
                        G.DrawALLGraph(V, E);
                        G.DrawALLGraph(routeV, routesEdge[changeRoute.Text], 1);
                        sheet.Image = G.GetBitmap();
                        DrawGrid();
                    }
                }

                //нажата кнопка "рисовать ребро"
                if (drawEdgeButton.Enabled == false)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        for (int i = 0; i < V.Count; i++)
                        {
                            if (Math.Pow((V[i].X - e.X / Main.zoom), 2) + Math.Pow((V[i].Y - e.Y / Main.zoom), 2) <= G.R * G.R)
                            {
                                if (selected1 == -1)
                                {
                                    if (routesEdge[changeRoute.Text].Count != 0)
                                    {
                                        if (routeV[routesEdge[changeRoute.Text].Last().V2].X == V[i].X && routeV[routesEdge[changeRoute.Text].Last().V2].Y == V[i].Y)
                                        {
                                            selected1 = i;
                                            G.DrawSelectedVertex(V[i].X, V[i].Y);
                                        }
                                    }
                                    else
                                    {
                                        if (!routeV.Contains(new Vertex(V[i].X, V[i].Y)))
                                        {
                                            routeV.Add(new Vertex(V[i].X, V[i].Y));
                                        }
                                        selected1 = i;

                                        G.DrawSelectedVertex(V[i].X, V[i].Y);
                                    }
                                    break;
                                }
                                if (selected2 == -1)
                                {
                                    G.DrawSelectedVertex(V[i].X, V[i].Y);
                                    selected2 = i;
                                    foreach (var ed in E)
                                    {
                                        if ((ed.V1 == selected1 && ed.V2 == selected2) || (ed.V2 == selected1 && ed.V1 == selected2))
                                        {

                                            // if (!routeV.Contains(new Vertex(V[i].x, V[i].y))) {
                                            routesEdge[changeRoute.Text].Add(new Edge(routeV.Count - 1, routeV.Count));
                                            routeV.Add(new Vertex(V[i].X, V[i].Y));
                                            //  routesEdge[int.Parse(changeRoute.Text)].Add(new Edge(routeV.Count - (routeV.Count / 2 + 1), routeV.Count - (routeV.Count / 2)));                                    
                                            G.DrawEdge(V[selected1], V[selected2], E[E.Count - 1], 1);
                                            sheet.Image = G.GetBitmap();
                                            selected1 = -1;
                                            selected2 = -1;
                                            break;
                                        }
                                    }
                                    //if (res == 0)
                                    //{
                                    //    routeV.RemoveAt(routeV.Count - 1);
                                    //    //routeV.RemoveAt(routeV.Count - 1);
                                    //}
                                    G.ClearSheet();
                                    G.DrawALLGraph(V, E);
                                    G.DrawALLGraph(routeV, routesEdge[changeRoute.Text], 1);
                                    selected1 = -1;
                                    selected2 = -1;
                                    sheet.Image = G.GetBitmap();
                                    DrawGrid();
                                    break;
                                }
                            }
                        }
                    }
                }
                //нажата кнопка "удалить элемент"
                if (deleteButton.Enabled == false)
                {
                    switch (delType)
                    {
                        case deleteType.All:
                            deleteOnRoute(e, routeV);
                            break;
                        case deleteType.BusStops:
                            deleteStopsOnRoute(e, routeV);
                            break;
                        case deleteType.TrafficLight:
                            deleteTFOnRoute(e, routeV);
                            break;
                        case deleteType.VertexAndEdge:
                            deleteVandE(e, routeV);
                            break;
                        case deleteType.TheBuses:
                            delBus();
                            break;
                    }
                }
                //   

                DrawGrid();
                CreateOneRouteCoordinates((changeRoute.Text));
                //Bus.AllCoordinates = AllCoordinates;
                return;
            }
        }
        private void deleteStopsOnRoute(MouseEventArgs e, List<Vertex> routeV)
        {
            bool flag = false; //удалили ли что-нибудь по ЭТОМУ клику
                               //ищем, возможно была нажата вершина


            foreach (var stopRoute in stopPoints[changeRoute.Text])
            {
                if (Math.Pow((stopRoute.X - e.X / zoom), 2) + Math.Pow((stopRoute.Y - e.Y / zoom), 2) <= G.R * G.R)
                {
                    stopPointsInGrids[changeRoute.Text].Remove(stopRoute.gridNum);
                    stopPoints[changeRoute.Text].Remove(stopRoute);
                    flag = true;
                    break;
                }

            }
            if (flag)
            {
                G.ClearSheet();
                G.DrawALLGraph(V, E);
                G.DrawALLGraph(routeV, routesEdge[(changeRoute.Text)], 1);
                sheet.Image = G.GetBitmap();
                DrawGrid();
            }
        }
        private void deleteVandE(MouseEventArgs e, List<Vertex> routeV)
        {
            bool flag = false; //удалили ли что-нибудь по ЭТОМУ клику

            for (int i = 0; i < routeV.Count; i++)
            {
                if (Math.Pow((routeV[i].X - e.X / zoom), 2) + Math.Pow((routeV[i].Y - e.Y / zoom), 2) <= G.R * G.R)
                {
                    for (int j = 0; j < routesEdge[changeRoute.Text].Count; j++)
                    {
                        if ((routesEdge[changeRoute.Text][j].V1 == i) || (routesEdge[changeRoute.Text][j].V2 == i))
                        {
                            routesEdge[changeRoute.Text].RemoveAt(j);
                            j--;
                        }
                        else
                        {
                            if (routesEdge[changeRoute.Text][j].V1 > i) routesEdge[changeRoute.Text][j].V1--;
                            if (routesEdge[changeRoute.Text][j].V2 > i) routesEdge[changeRoute.Text][j].V2--;
                        }
                    }
                    routeV.RemoveAt(i);
                    flag = true;
                    break;
                }
            }
            //ищем, возможно было нажато ребро
            if (!flag)
            {
                for (int i = 0; i < routesEdge[changeRoute.Text].Count; i++)
                {
                    if (routesEdge[changeRoute.Text][i].V1 == routesEdge[changeRoute.Text][i].V2) //если это петля
                    {
                        if ((Math.Pow((routeV[routesEdge[changeRoute.Text][i].V1].X - G.R - e.X / zoom), 2) + Math.Pow((routeV[routesEdge[changeRoute.Text][i].V1].Y - G.R - e.Y / zoom), 2) <= ((G.R + 2) * (G.R + 2))) &&
                            (Math.Pow((routeV[routesEdge[changeRoute.Text][i].V1].X - G.R - e.X / zoom), 2) + Math.Pow((routeV[routesEdge[changeRoute.Text][i].V1].Y - G.R - e.Y / zoom), 2) >= ((G.R - 2) * (G.R - 2))))
                        {
                            routesEdge[changeRoute.Text].RemoveAt(i);
                            flag = true;
                            break;
                        }
                    }
                    else //не петля
                    {
                        try
                        {
                            if (((e.X / zoom - routeV[routesEdge[changeRoute.Text][i].V1].X) * (routeV[routesEdge[(changeRoute.Text)][i].V2].Y - routeV[routesEdge[changeRoute.Text][i].V1].Y) / (routeV[routesEdge[(changeRoute.Text)][i].V2].X - routeV[routesEdge[(changeRoute.Text)][i].V1].X) + routeV[routesEdge[(changeRoute.Text)][i].V1].Y) <= (e.Y / zoom + 4) &&
                                ((e.X / zoom - routeV[routesEdge[(changeRoute.Text)][i].V1].X) * (routeV[routesEdge[(changeRoute.Text)][i].V2].Y - routeV[routesEdge[(changeRoute.Text)][i].V1].Y) / (routeV[routesEdge[(changeRoute.Text)][i].V2].X - routeV[routesEdge[(changeRoute.Text)][i].V1].X) + routeV[routesEdge[(changeRoute.Text)][i].V1].Y) >= (e.Y / zoom - 4))
                            {
                                if ((routeV[routesEdge[(changeRoute.Text)][i].V1].X <= routeV[routesEdge[(changeRoute.Text)][i].V2].X && routeV[routesEdge[(changeRoute.Text)][i].V1].X <= e.X / zoom && e.X / zoom <= routeV[routesEdge[(changeRoute.Text)][i].V2].X) ||
                                    (routeV[routesEdge[(changeRoute.Text)][i].V1].X >= routeV[routesEdge[(changeRoute.Text)][i].V2].X && routeV[routesEdge[(changeRoute.Text)][i].V1].X >= e.X / zoom && e.X / zoom >= routeV[routesEdge[(changeRoute.Text)][i].V2].X))
                                {
                                    routesEdge[(changeRoute.Text)].RemoveAt(i);
                                    flag = true;
                                    break;
                                }
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Ребро не удаляется");
                        }
                    }

                }
            }
            if (flag)
            {
                G.ClearSheet();
                G.DrawALLGraph(V, E);
                G.DrawALLGraph(routeV, routesEdge[(changeRoute.Text)], 1);
                sheet.Image = G.GetBitmap();
                DrawGrid();
            }
        }
        private void deleteTFOnRoute(MouseEventArgs e, List<Vertex> routeV)
        {
            bool flag = false; //удалили ли что-нибудь по ЭТОМУ клику

            for (var i = 0; i < traficLights.Count; i++)
            {
                if (Math.Pow((traficLights[i].x - e.X / zoom), 2) + Math.Pow((traficLights[i].y - e.Y / zoom), 2) <= G.R * G.R)
                {
                    TraficLightsInGrids.RemoveAt(i);
                    traficLights.RemoveAt(i);
                    flag = true;
                    break;
                }

            }
            if (flag)
            {
                G.ClearSheet();
                G.DrawALLGraph(V, E);
                G.DrawALLGraph(routeV, routesEdge[(changeRoute.Text)], 1);
                sheet.Image = G.GetBitmap();
                DrawGrid();
            }
        }

        private void deleteOnRoute(MouseEventArgs e, List<Vertex> routeV)
        {
            bool flag = false; //удалили ли что-нибудь по ЭТОМУ клику
                               //ищем, возможно была нажата вершина


            foreach (var stopRoute in stopPoints[changeRoute.Text])
            {
                if (Math.Pow((stopRoute.X - e.X / zoom), 2) + Math.Pow((stopRoute.Y - e.Y / zoom), 2) <= G.R * G.R)
                {
                    stopPointsInGrids[changeRoute.Text].Remove(stopRoute.gridNum);
                    stopPoints[changeRoute.Text].Remove(stopRoute);
                    flag = true;
                    break;
                }

            }


            for (int i = 0; i < routeV.Count; i++)
            {
                if (Math.Pow((routeV[i].X - e.X / zoom), 2) + Math.Pow((routeV[i].Y - e.Y / zoom), 2) <= G.R * G.R)
                {
                    for (int j = 0; j < routesEdge[changeRoute.Text].Count; j++)
                    {
                        if ((routesEdge[changeRoute.Text][j].V1 == i) || (routesEdge[changeRoute.Text][j].V2 == i))
                        {
                            routesEdge[changeRoute.Text].RemoveAt(j);
                            j--;
                        }
                        else
                        {
                            if (routesEdge[changeRoute.Text][j].V1 > i) routesEdge[changeRoute.Text][j].V1--;
                            if (routesEdge[changeRoute.Text][j].V2 > i) routesEdge[changeRoute.Text][j].V2--;
                        }
                    }
                    routeV.RemoveAt(i);
                    flag = true;
                    break;
                }
            }
            //ищем, возможно было нажато ребро
            if (!flag)
            {
                for (int i = 0; i < routesEdge[changeRoute.Text].Count; i++)
                {
                    if (routesEdge[changeRoute.Text][i].V1 == routesEdge[changeRoute.Text][i].V2) //если это петля
                    {
                        if ((Math.Pow((routeV[routesEdge[changeRoute.Text][i].V1].X - G.R - e.X / zoom), 2) + Math.Pow((routeV[routesEdge[changeRoute.Text][i].V1].Y - G.R - e.Y / zoom), 2) <= ((G.R + 2) * (G.R + 2))) &&
                            (Math.Pow((routeV[routesEdge[changeRoute.Text][i].V1].X - G.R - e.X / zoom), 2) + Math.Pow((routeV[routesEdge[changeRoute.Text][i].V1].Y - G.R - e.Y / zoom), 2) >= ((G.R - 2) * (G.R - 2))))
                        {
                            routesEdge[changeRoute.Text].RemoveAt(i);
                            flag = true;
                            break;
                        }
                    }
                    else //не петля
                    {
                        try
                        {
                            if (((e.X / zoom - routeV[routesEdge[changeRoute.Text][i].V1].X) * (routeV[routesEdge[(changeRoute.Text)][i].V2].Y - routeV[routesEdge[changeRoute.Text][i].V1].Y) / (routeV[routesEdge[(changeRoute.Text)][i].V2].X - routeV[routesEdge[(changeRoute.Text)][i].V1].X) + routeV[routesEdge[(changeRoute.Text)][i].V1].Y) <= (e.Y / zoom + 4) &&
                                ((e.X / zoom - routeV[routesEdge[(changeRoute.Text)][i].V1].X) * (routeV[routesEdge[(changeRoute.Text)][i].V2].Y - routeV[routesEdge[(changeRoute.Text)][i].V1].Y) / (routeV[routesEdge[(changeRoute.Text)][i].V2].X - routeV[routesEdge[(changeRoute.Text)][i].V1].X) + routeV[routesEdge[(changeRoute.Text)][i].V1].Y) >= (e.Y / zoom - 4))
                            {
                                if ((routeV[routesEdge[(changeRoute.Text)][i].V1].X <= routeV[routesEdge[(changeRoute.Text)][i].V2].X && routeV[routesEdge[(changeRoute.Text)][i].V1].X <= e.X / zoom && e.X / zoom <= routeV[routesEdge[(changeRoute.Text)][i].V2].X) ||
                                    (routeV[routesEdge[(changeRoute.Text)][i].V1].X >= routeV[routesEdge[(changeRoute.Text)][i].V2].X && routeV[routesEdge[(changeRoute.Text)][i].V1].X >= e.X / zoom && e.X / zoom >= routeV[routesEdge[(changeRoute.Text)][i].V2].X))
                                {
                                    routesEdge[(changeRoute.Text)].RemoveAt(i);
                                    flag = true;
                                    break;
                                }
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Ребро не удаляется");
                        }
                    }

                }
            }
            //если что-то было удалено, то обновляем граф на экране
            if (flag)
            {
                G.ClearSheet();
                G.DrawALLGraph(V, E);
                G.DrawALLGraph(routeV, routesEdge[(changeRoute.Text)], 1);
                sheet.Image = G.GetBitmap();
                DrawGrid();
            }
        }
        private void button9_Click(object sender, EventArgs e)
        {
            if (G.bitmap != null)
            {
                AddGrid f = new AddGrid
                {
                    Owner = this
                };
                f.ShowDialog();
                G.ClearSheet();
                sheet.Image = G.GetBitmap();
                DrawGrid();
                if (!Ep.IsDisposed)
                {
                    Ep.EDrawGrid();
                }

            }
        }

        private void gridButton_Click(object sender, EventArgs e)
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
                checkBuses();
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
                checkBusesOnRoute();
            }
            selected = new List<int>();
            selectRoute.Enabled = true;
            label12.Visible = false;
            delAllBusesOnRoute.Enabled = true;
            stopPointButton.Enabled = false;
        }
        private void delAllBus()
        {
            checkBuses();
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
                buses.Clear();
                delAllBusesOnRoute.Enabled = false;
            };
            checkBusesOnRoute();
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
                foreach (var bus in buses)
                {
                    if (bus.route == (changeRoute.Text))
                    {
                        b.Add(bus);
                    }
                }
                Parallel.ForEach(b, (bus) =>
                {
                    foreach (var B in buses)
                    {
                        if (B == bus)
                        {
                            buses.Remove(bus);
                            break;
                        }
                    }
                });
                b.Clear();
                delAllBusesOnRoute.Enabled = true;

            }
            label12.Visible = false;
            selected = new List<int>();

        }
        private void delAllBusesOnRoute_Click(object sender, EventArgs e)
        {


        }
        private void checkBuses()
        {
            if (buses.Count != 0 && changeRoute.Text == MainStrings.network)
            {
                delAllBusesOnRoute.Enabled = true;
            }
        }
        private void checkBusesOnRoute()
        {
            foreach (var bus in buses)
            {
                if (bus.route == (changeRoute.Text))
                {
                    delAllBusesOnRoute.Enabled = true;
                    break;
                }
            }
        }


        private void jSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = savepath;
            if (sheet.Image != null)
            {
                using (var dialog = new FolderBrowserDialog())
                {
                    if (!Directory.Exists(savepath))
                    {
                        dialog.SelectedPath = System.Windows.Forms.Application.StartupPath;
                    }
                    else
                    {
                        dialog.SelectedPath = Path.GetFullPath(savepath);
                    }
                    // dialog.SelectedPath = Path.GetFullPath(savepath); //System.Windows.Forms.Application.StartupPath;
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        path = dialog.SelectedPath;
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
                            savepath = savepath + rnd.Next(0, 100).ToString();
                            Directory.CreateDirectory(savepath);
                            SaveRoutes("json", savepath + @"\");
                            saveImage.Save(savepath + "/Map.png", System.Drawing.Imaging.ImageFormat.Png);
                        }
                        MetroMessageBox.Show(this, MainStrings.done, "", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    }
                    config.Text = MainStrings.config + savepath;
                }
            }
            BringToFront();

        }
        CrossroadsSettings crossSettings;
        private void addTraficLight_Click(object sender, EventArgs e)
        {
            if (changeRoute.Text == MainStrings.network)
            {
                crossSettings = new CrossroadsSettings();
                this.StyleManager.Clone(crossSettings);
                crossSettings.ShowDialog();
                if (firstCrossRoads != 0 && secondCrossRoads != 0)
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
                    label12.Visible = true;
                    label12.Text = MainStrings.putTrafficLights1 + " " + firstCrossRoads.ToString();
                    selected = new List<int>();
                    stopPointButton.Enabled = true;
                    addTraficLight.Enabled = false;
                    selectRoute.Enabled = true;
                }
                sheet.Image = G.GetBitmap();
                selected1 = -1;
                DrawGrid();

            }

        }
        private void xMLToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (sheet.Image != null)
            {
                using (var dialog = new FolderBrowserDialog())
                {
                    if (!Directory.Exists(savepath))
                    {
                        dialog.SelectedPath = System.Windows.Forms.Application.StartupPath;
                    }
                    else
                    {
                        dialog.SelectedPath = Path.GetFullPath(savepath);
                    }
                    //dialog.SelectedPath = Path.GetFullPath(savepath); //System.Windows.Forms.Application.StartupPath;
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
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
                            savepath = savepath + rnd.Next(0, 100).ToString();
                            Directory.CreateDirectory(savepath);
                            SaveRoutes("xml", savepath + @"\");
                            saveImage.Save(savepath + "/Map.png", System.Drawing.Imaging.ImageFormat.Png);
                        }
                        MetroMessageBox.Show(this, MainStrings.done, "", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    }
                    config.Text = MainStrings.config + savepath;
                }
            }
            BringToFront();
        }

        private void selectRoute_Click(object sender, EventArgs e)
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
                checkBuses();
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
                checkBusesOnRoute();
            }
            label12.Visible = false;
            stopPointButton.Enabled = true;
            sheet.Image = G.GetBitmap();
            selected1 = -1;
            DrawGrid();
        }


        private void openEpicFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Ep.IsDisposed)
            {
                Ep.Close();

            }
            Ep = new DisplayEpicenters(this);
            StyleManager.Clone(Ep);
            Ep.Show();

        }
        AddRoute addR;
        private void addRouteToolStripMenuItem_Click(object sender, EventArgs e)
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
                if (!routes.ContainsKey(this.addR.textBox1.Text))
                {
                    routes.Add((this.addR.textBox1.Text), new List<Vertex>());
                    routesEdge.Add((this.addR.textBox1.Text), new List<Edge>());
                    changeRoute.Items.Add(addR.textBox1.Text);
                    stopPoints.Add((this.addR.textBox1.Text), new List<Vertex>());
                    changeRoute.SelectedIndex = changeRoute.Items.IndexOf(addR.textBox1.Text);
                }
            }
        }

        private void runTrafficLightsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            traficLights.ForEach((tl) =>
            {
                tl.Start();
            });
        }

        private void themes_CheckedChanged(object sender, EventArgs e)
        {
            if (themes.Checked == true)
            {
                msmMain.Theme = MetroFramework.MetroThemeStyle.Dark;
                toolStripMenu.BackColor = Color.FromArgb(17, 17, 17);
                toolStripMenu.ForeColor = Color.FromArgb(153, 153, 153);
                selectButton.Image = new Bitmap(("../../Resources/newcursor_bt.png"));
                drawVertexButton.Image = new Bitmap(("../../Resources/circle_bt.png"));
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
                drawVertexButton.Image = new Bitmap(("../../Resources/circle1.png"));
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


        private void changeTheme_SelectedIndexChanged(object sender, EventArgs e)
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

        private void launchBuses_Click(object sender, EventArgs e)
        {
            //BarabanAfterOpti();
            //foreach (var bus in buses)
            //{

            //    bus.Start();
            //}
            timer1.Start();
        }

        private void stopBuses_Click(object sender, EventArgs e)
        {
            //foreach (var bus in buses)
            //{
            //    bus.Stop();
            //}
            timer1.Stop();
        }



        private async void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Language = metroComboBox1.SelectedValue.ToString();
            Properties.Settings.Default.Save();
            await Task.Delay(1000);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            // Заносим список поддерживаемых языков.
            metroComboBox1.DataSource = new System.Globalization.CultureInfo[]{
                 System.Globalization.CultureInfo.GetCultureInfo("ru-RU"),
                 System.Globalization.CultureInfo.GetCultureInfo("en-US")
            };

            // Каждый элемент списка comboBox1 будет являться экземпляром класса CultureInfo.

            metroComboBox1.DisplayMember = "NativeName"; // <= System.Globalization.CultureInfo.GetCultureInfo("ru-RU").NativeName
            metroComboBox1.ValueMember = "Name"; // <= System.Globalization.CultureInfo.GetCultureInfo("ru-RU").Name

            // Если в настройках есть язык, выбираем его в списке.
            if (!string.IsNullOrEmpty(Properties.Settings.Default.Language))
            {
                metroComboBox1.SelectedValue = Properties.Settings.Default.Language;
            }
            lang = true;
        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lang == true)
            {
                hint.Visible = true;
                hint.Text = MainStrings.hint;
            }
        }

        private void ZoomHelper()
        {
            sheet.Image = ResizeBitmap(new Bitmap(saveImage), wsheet * metroTrackBar1.Value, hsheet * metroTrackBar1.Value);
            //AnimationBox.Image = ResizeBitmap(new Bitmap(AnimationBitmap), wsheet * metroTrackBar1.Value, hsheet * metroTrackBar1.Value);
            globalMap = sheet.Image;
            mainPanel.AutoScrollPosition = new Point(scrollX * metroTrackBar1.Value, scrollY * metroTrackBar1.Value);
            scrollX = mainPanel.AutoScrollPosition.X;
            scrollY = mainPanel.AutoScrollPosition.Y;
            zoom = metroTrackBar1.Value;
            Bus.SetScrollX(mainPanel.AutoScrollPosition.X);
            Bus.SetScrollY(mainPanel.AutoScrollPosition.Y);
            Bus.ZoomCoef = metroTrackBar1.Value;
        }
        private void metroTrackBar1_ScrollAsync(object sender, ScrollEventArgs e)
        {
            //try
            //{
            if (sheet.Image != null && saveImage != null)
            {
                ZoomHelper();

                G.ClearSheet();
                if (changeRoute.SelectedIndex > 1)
                {
                    G.DrawALLGraph(V, E);
                    G.DrawALLGraph(routes[(changeRoute.Text)], routesEdge[(changeRoute.Text)], 1);
                }
                else if (changeRoute.Text == MainStrings.none)
                {
                    G.ClearSheet();
                }
                else
                {
                    G.DrawALLGraph(V, E);
                }

                if (timer1.Enabled == false)
                {
                    AnimationBitmap.Dispose();
                    AnimationBitmap = new Bitmap(sheet.Width, sheet.Height);
                    AnimationBitmap.MakeTransparent();
                    AnimationGraphics.Dispose();
                    AnimationGraphics = Graphics.FromImage(AnimationBitmap);
                    foreach (var bus in buses)
                    {

                        AnimationGraphics.DrawImage(bus.busPic, bus.Coordinates[bus.PositionAt].X * metroTrackBar1.Value - bus.busPic.Width / 2, bus.Coordinates[bus.PositionAt].Y * metroTrackBar1.Value - bus.busPic.Height / 2);
                    }
                    AnimationBox.Image = AnimationBitmap;
                    //AnimationBox.Update();
                }
                // AnimationBox.Update();
                sheet.Image = G.GetBitmap();
                // CreateGrid();
                DrawGrid();


            }
            //}
            //catch//(OutOfMemoryException ex)
            //{
            //    Console.WriteLine("ex");
            //}
        }
        AddGrid addG;

        private void busSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
        }

        private void optText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void speed_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void metroButton1_Click_1(object sender, EventArgs e)
        {
            AsyncCreateAllCoordinates();
        }

        //
        public EpicSettings epSet;
        private void metroButton2_Click(object sender, EventArgs e)
        {
            epSet = new EpicSettings();
            StyleManager.Clone(epSet);
            epSet.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //AnimationBitmap.Dispose();
            AnimationBitmap = new Bitmap(sheet.Width, sheet.Height);
            AnimationBitmap.MakeTransparent();
            // AnimationGraphics.Dispose();
            AnimationGraphics = Graphics.FromImage(AnimationBitmap);
            foreach (var bus in buses)
            {
                bus.MoveWithGraphics(AnimationGraphics);
            }
            AnimationBox.Image = AnimationBitmap;
            //  AnimationBox.Update();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            timer1.Start();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void reportTool_Click(object sender, EventArgs e)
        {
            r.Show();
            r.BringToFront();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {

            openEpicFormToolStripMenuItem.Enabled = false;
            addRouteToolStripMenuItem.Enabled = false;
            createGridToolStripMenuItem.Enabled = false;
            savepath = null; //мб лучше это оствить при клире
            sheet.Image = null;
            if (Ep != null)
            {
                Ep.EG.ClearSheet2();
                Ep.Close();
            }
            buses.Clear();
            buses.TrimExcess();
            config.Text = MainStrings.config;
            foreach (var tl in traficLights)
            {
                tl.Stop();
                tl.TimerLight.Dispose();
            }
            TraficLightsInGrids.Clear();
            stopPointsInGrids.Clear();
            V.Clear();
            E.Clear();
            if (G.GetBitmap() != null)
            {
                G.ClearSheet();
                G.ClearSheet2();
                G = new DrawGraph();
                globalMap.Dispose();

            }
            routes.Clear();
            routesEdge.Clear();
            changeRoute.Items.Clear();
            changeRoute.Items.Add(MainStrings.none);
            changeRoute.Items.Clear();
            changeRoute.Text = "";
            AllCoordinates.Clear();
            allstopPoints.Clear();
            stopPoints.Clear();
            traficLights.Clear();
            traficLights.TrimExcess();
            metroTrackBar1.Value = 1;
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
            File.WriteAllText("../../SaveConfig/save.txt", string.Empty);
            BringToFront();
            Matrix();
            TheGrid = new List<GridPart>();
            TheGrid.TrimExcess();
            timer1.Stop();
            timer1.Dispose();
            AnimationBox.Image = null;
            AnimationGraphics.Dispose();
            AnimationBitmap.Dispose();
            Refresh();
        }





        private void createGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (G.bitmap != null)
            {
                addG = new AddGrid();
                this.StyleManager.Clone(addG);
                addG.ShowDialog();
                G.ClearSheet();
                if (!Ep.IsDisposed)
                {
                    Ep.EG.ClearSheet2();
                }
                G.DrawALLGraph(V, E);
                CreateGrid();

                sheet.Image = G.GetBitmap();
                DrawGrid();
                if (!Ep.IsDisposed)
                {
                    Ep.EDrawGrid();
                }

                CreateAllCoordinates();
                CreatePollutionInRoutes();
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (G.bitmap != null)
            {
                EpicSettings f = new EpicSettings
                {
                    Owner = this
                };
                f.ShowDialog();
            }
        }


    }
}
