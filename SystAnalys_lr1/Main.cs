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
using System.Xml.Serialization;

namespace SystAnalys_lr1
{
    public partial class Main : MetroForm
    {
        int tracbarX, tracbarY;
        public static string selectedRoute;
        public static int firstCrossRoads = 0;
        public static int firstCrossRoadsGreenLight = 0;
        public static int firstCrossRoadsRedLight = 0;
        public static int secondCrossRoads = 0;
        public static string EpicSizeParamSave = "radioEpicMedium";
        string savepath;

        public static Grid g;
        //Лист всех эпицентров
        List<Epicenter> Epics;
        //Лист, в котором хранится сетка
        static List<GridPart> TheGrid;
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
            tracbarX = metroTrackBar1.Location.X;
            tracbarY = metroTrackBar1.Location.Y;
            ExpandEpicParamet = new List<string>();
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
                                    loading.Visible = false;
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
                    msmMain.Style = (MetroFramework.MetroColorStyle)Convert.ToInt32(changeTheme.Items.IndexOf(text));
                    Console.WriteLine($"Текст из файла: {savepath}");

                }
            }
            else
            {
                File.Create("../../SaveConfig/style.txt");
            }
            changeTheme.Text = text;
            if (sheet.Image == null)
            {
                addRouteToolStripMenuItem.Enabled = false;
                openEpicFormToolStripMenuItem.Enabled = false;
                createGridToolStripMenuItem.Enabled = false;
            }
            mainPanel.MaximumSize = new System.Drawing.Size(sheet.Width, sheet.Height);
            mainPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            mainPanel.MouseWheel += new System.Windows.Forms.MouseEventHandler(panel6_MouseWheel);
            if (buses.Count != 0)
            {
                sizeBus = buses.Last().busPic.Width;
            }
            else
            {
                sizeBus = 15;
            }
            countWithoutSensors = buses.Count;
            Matrix();
            timer2.Interval = 1000;
            timer2.Start();
            hint.Visible = false;
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
            if (ExpandEpicParamet.Count > 0)
            {
                Epics.First().ExpandEpic(ExpandEpicParamet);
            }
        }
        int T;
        List<int?> ResultFromModeling = new List<int?>();
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
            bool EpicFounded = false;
            int ExpandTimer = 0;
            Epics = epList;
            i = 1;
            foreach (var bus in buses)
            {
                bus.Epicenters = epList;
            }
            for (int j = 3; j > 0; j--)
            {

                foreach (var bus in cqBus)
                {
                    bus.Stop();
                    //bus.Epicenters.Clear();
                    bus.Epicenters = epList;
                    bus.TickCount_ = T / 3;
                    if (bus.skip > 0)
                        bus.skip -= 1;
                    if (bus.tracker == true)
                    {
                        while (bus.TickCount_ > 0)
                        {
                            bus.MoveWithoutGraphicsByGrids();
                            if (ExpandTimer == 150)
                            {
                                lock (epList)
                                {
                                    ExpandEpics(epList);
                                }
                                ExpandTimer = 0;
                            }
                            if (TraficLightsInGrids.Contains(AllGridsInRoutes[bus.getRoute()][(int)bus.PositionAt])) //ошибка с выходом за пределы
                                                                                                                     //тушто нужно "вот эту" разкоментить
                            {
                                if (bus.skip == 0)
                                {
                                    foreach (var sp in traficLights)
                                    {
                                        if (sp.status != Status.RED)
                                        {
                                            bus.skip = sp.greenTime;
                                            break;
                                        }
                                        if (sp.status == Status.RED)
                                        {
                                            bus.TickCount_ = bus.TickCount_ - sp.bal;
                                            bus.skip = sp.greenTime;
                                            break;

                                        }
                                    }
                                }
                            }

                            if ((stopPointsInGrids.ContainsKey(bus.getRoute())) && (stopPointsInGrids[bus.getRoute()].Contains(AllGridsInRoutes[bus.getRoute()][(int)bus.PositionAt])))
                            {
                                bus.TickCount_ = bus.TickCount_ - rnd.Next(0, 3);
                            }

                            PollutionInRoutes[bus.getRoute()][AllGridsInRoutes[bus.getRoute()][(int)bus.PositionAt]].status = bus.DetectEpicenterByGrid(); // ошибка

                            foreach (var Epic in bus.Epicenters)
                            {
                                if (Epic.DetectCount >= Epic.getEpicenterGrid()[1].Count / 5)
                                {
                                    if (EpicFounded == false)
                                    {
                                        EpicFounded = true;
                                        if (EpicFounded == true)
                                        {
                                            FoundTime = (T / 2 - bus.TickCount_) / 5;
                                            if (small > FoundTime)
                                            {
                                                small = FoundTime;
                                            }
                                        }
                                    }
                                }
                            }
                            bus.TickCount_--;
                            ExpandTimer++;

                        }
                    }
                }

                if ((SavePictures.Checked) && (extendedSavePictures.Checked))
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
                    ResultFromModeling.Add(small * 25);
                }
                else
                {
                    ResultFromModeling.Add(small * 25);
                }
            }
        }
        delegate void DelBmp(Bitmap bmp);
        //отрисовать всю сетку
        static public void DrawGrid()
        {
            for (int i = 0; i < TheGrid.Count; i++)
            {
                TheGrid[i].DrawPart(G, Main.zoom);
            }
            _instance.Invoke(new DelBmp((s) => _instance.sheet.Image = s), G.GetBitmap());
        }
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

        //старый таймер
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (TheGrid != null)
            {
                sheet.Image = G.GetBitmap();
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
            return this.panel4;
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
            G.clearSheet();
            G.drawALLGraph(V, E);
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
                G.clearSheet();
                label12.Visible = false;
                DrawGrid();
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
                G.clearSheet();
                G.drawALLGraph(V, E);
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
                    G.clearSheet();
                    G.drawALLGraph(V, E);
                    G.drawALLGraph(routes[(changeRoute.Text)], routesEdge[(changeRoute.Text)], 1);
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

        private void deleteBus_Click(object sender, EventArgs e)
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
            G.clearSheet();
            G.drawALLGraph(V, E);
            G.drawALLGraph(routes[(changeRoute.Text)], routesEdge[(changeRoute.Text)], 1);
            sheet.Image = G.GetBitmap();
            selected1 = -1;
            stopPointButton.Enabled = true;
            selected = new List<int>();
            label12.Visible = false;
            DrawGrid();
            checkBusesOnRoute();
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

        private void deleteButton_Click(object sender, EventArgs e)
        {
            G.clearSheet();
            G.drawALLGraph(V, E);
            if (changeRoute.Text == MainStrings.network)
            {
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
                allBusSettings.Enabled = false;
                addBus.Enabled = true;
                deleteBus.Enabled = true;
                drawEdgeButton.Enabled = true;
                selectButton.Enabled = true;
                drawVertexButton.Enabled = false;
                deleteButton.Enabled = false;
                addTraficLight.Enabled = false;
                G.drawALLGraph(routes[(changeRoute.Text)], routesEdge[(changeRoute.Text)], 1);
                checkBusesOnRoute();
            }
            label12.Visible = false;
            selected = new List<int>();
            selectRoute.Enabled = true;
            stopPointButton.Enabled = true;
            sheet.Image = G.GetBitmap();
            DrawGrid();
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
            G.clearSheet();
            G.drawALLGraph(V, E);
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
                            b.Stop();
                            mainPanel.Controls.Remove(b.busPic);
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
                    G.clearSheet();
                    G.drawALLGraph(V, E);
                    sheet.Image = G.GetBitmap();
                    DrawGrid();
                    loadingForm.loading.Value = 60;
                    // SaveRoutes(saveF, savepath + @"\");

                }
                else
                {
                    loadingForm.close = true;
                    loadingForm.Close();
                }
                if (MBSave == DialogResult.Yes && changeRoute.Text == MainStrings.network)
                {
                    //DirectoryInfo dirInfo = new DirectoryInfo(savepath);
                    //foreach (FileInfo file in dirInfo.GetFiles())
                    //{
                    //    if (Path.GetExtension(file.FullName) != ".png")
                    //    {
                    //        file.Delete();
                    //    }
                    //}
                    loadingForm.Show();
                    loadingForm.loading.Value = 20;
                    foreach (var b in buses)
                    {
                        b.Stop();
                        mainPanel.Controls.Remove(b.busPic);
                    }
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
                    G.clearSheet();
                    G.drawALLGraph(V, E);
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
            foreach (var bus in buses)
            {
                bus.Start();
            }
            loadingForm.loading.Value = 100;
            loadingForm.close = true;
            loadingForm.Close();
            BringToFront();
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
                    Ep.EG.clearSheet2();
                    Ep.Close();
                }
                foreach (var bus in buses)
                {
                    bus.Stop();
                    mainPanel.Controls.Remove(bus.busPic);
                }
                buses.Clear();
                config.Text = MainStrings.config;
                foreach (var tl in traficLights)
                {
                    tl.Stop();
                }
                TraficLightsInGrids.Clear();
                V.Clear();
                E.Clear();
                if (G.bitmap != null)
                {
                    ZoomHelper();
                    G.clearSheet();
                    G.clearSheet2();
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
                saveImage = sheet.Image;
                G.setBitmap();
                CreateGrid();
                CreatePollutionInRoutes();
                Bus.setGrid(TheGrid);
                Bus.setAllCoordinates(AllCoordinates);
                addInComboBox();
                Ep = new DisplayEpicenters(this);
                StyleManager.Clone(Ep);
                Ep.Show();
                DrawGrid();
                openEpicFormToolStripMenuItem.Enabled = true;
                addRouteToolStripMenuItem.Enabled = true;
                createGridToolStripMenuItem.Enabled = true;
                Matrix();
                BringToFront();
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


        private void deleteALLButton_Click(object sender, EventArgs e)
        {
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
            var MBSave = MetroMessageBox.Show(this, message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (routes != null || routesEdge != null || E != null || V != null || buses != null)
            {
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
                            b.Stop();
                            mainPanel.Controls.Remove(b.busPic);
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
                    //   SaveRoutes();
                    G.clearSheet();
                    G.drawALLGraph(V, E);
                    sheet.Image = G.GetBitmap();
                    DrawGrid();

                }
                if (MBSave == DialogResult.Yes && changeRoute.Text == MainStrings.network)
                {
                    //V.Clear();
                    //E.Clear();
                    loadingForm.Show();
                    foreach (var b in buses)
                    {
                        b.Stop();
                        mainPanel.Controls.Remove(b.busPic);
                    };
                    loadingForm.loading.Value = 20;
                    buses.Clear();
                    routes.Keys.ToList().ForEach(x => routes[x] = new List<Vertex>());
                    routesEdge.Keys.ToList().ForEach(x => routesEdge[x] = new List<Edge>());
                    loadingForm.loading.Value = 40;
                    AllCoordinates.Clear();
                    G.clearSheet();
                    sheet.Image = G.GetBitmap();
                    DrawGrid();
                    loadingForm.loading.Value = 50;
                };
            }
            if (changeRoute.Text == MainStrings.network)
            {
                deleteBus.Enabled = false;
                addBus.Enabled = false;
                drawVertexButton.Enabled = true;
                addTraficLight.Enabled = true;
            };
            G.drawALLGraph(V, E);
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
            foreach (var bus in buses)
            {
                bus.Start();
            }
            loadingForm.loading.Value = 100;
            loadingForm.close = true;
            loadingForm.Close();
            BringToFront();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            foreach (var item in buses)
            {
                item.Stop();
            }
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


        public class AutoClosingMessageBox
        {
            System.Threading.Timer _timeoutTimer;
            string _caption;
            AutoClosingMessageBox(string text, string caption, int timeout)
            {
                _caption = caption;
                _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
                    null, timeout, System.Threading.Timeout.Infinite);
                using (_timeoutTimer)
                    MessageBox.Show(text, caption);
            }
            public static void Show(string text, string caption, int timeout)
            {
                new AutoClosingMessageBox(text, caption, timeout);
            }
            void OnTimerElapsed(object state)
            {
                IntPtr mbWnd = FindWindow("#32770", _caption); // lpClassName is #32770 for MessageBox
                if (mbWnd != IntPtr.Zero)
                    SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                _timeoutTimer.Dispose();
            }
            const int WM_CLOSE = 0x0010;
            [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
            static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
            [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
            static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
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
                        bus.tracker = false;
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
            countWithoutSensors -= countSensors;//(int)(buses.Count * 0.01 * proc);
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
        private async void Opt()
        {
            loadingForm = new LoadingForm();
            buttonOff();
            string path = "../../Results/" + string.Format("{0}_{1}_{2}_{3}_{4}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, DateTime.Now.Hour, DateTime.Now.Minute);
            if (SavePictures.Checked)
            {
                Ep.Hide();

                Directory.CreateDirectory(path + "/Epics");
            }
            loadingForm.Theme = msmMain.Theme;
            loadingForm.Style = msmMain.Style;
            Matrix();
            percentMean = new SerializableDictionary<int, int?>();

            Directory.CreateDirectory(path);
            int? sum = null;
            List<Bus> optimizeBuses = new List<Bus>();
            buses.ForEach((b) => optimizeBuses.Add(
                (Bus)b.Clone()
            ));
            if (speed.Text != "" && int.TryParse(speed.Text, out int sp))
            {
                T = int.Parse(speed.Text) / 20;
            }
            loadingForm.Show();
            int old = small;
            var style = msmMain.Style;
            SavePictures.Enabled = false;
            if (msmMain.Style == (MetroFramework.MetroColorStyle)Convert.ToInt32(changeTheme.Items.IndexOf("Red")))
                msmMain.Style = (MetroFramework.MetroColorStyle)Convert.ToInt32(changeTheme.Items.IndexOf("Yellow"));
            else
                msmMain.Style = (MetroFramework.MetroColorStyle)Convert.ToInt32(changeTheme.Items.IndexOf("Red"));
            var busesparkreturn = busesPark;

            await Task.Run(() =>
            {
                if (changeProcent.Text != "" && int.TryParse(speed.Text, out int ch) && changeProcent.Text != "")
                {
                    offBuses(int.Parse(changeProcent.Text));
                };
                int ciclTotal = 5;
                int totalW = 0;
                if (T < 20)
                    totalW = 1;
                else
                    totalW = T / 20;
                loadingForm.loading.Invoke(new DelInt((s) => loadingForm.loading.Maximum = s), ciclTotal * int.Parse(optText.Text));
                for (int cicl = 0; cicl < ciclTotal; cicl++)
                {
                    offBuses(cicl * 10);
                    if (cicl == ciclTotal - 1)
                        buses[rnd.Next(0, buses.Count)].tracker = true;
                    List<int?> mas = new List<int?>();
                    Baraban();
                    if (SavePictures.Checked)
                    {
                        Directory.CreateDirectory(path + "/Epics" + "/" + (cicl + 1).ToString());
                    }
                    for (int i = 0; i < int.Parse(optText.Text); i++)
                    {
                        if (SavePictures.Checked)
                        {
                            Directory.CreateDirectory(path + "/Epics" + "/" + (cicl + 1).ToString() + "/" + (i + 1).ToString());
                        }
                        CreateOneRandomEpicenter(EpicSizeParam, null);
                        Modeling(path, cicl, i);
                        if ((SavePictures.Checked) && (!extendedSavePictures.Checked))
                        {

                            lock (Ep.Esheet)
                            {
                                Ep.EDrawEpics(Epics);
                            }
                            lock (Ep.Esheet)
                            {
                                using (System.Drawing.Image img = (Image)Ep.Esheet.Image.Clone())
                                {
                                    img.Save(path + "/Epics" + "/" + (cicl + 1).ToString() + "/" + (i + 1).ToString() + "/" + i.ToString() + "_nat" + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
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
                                    img.Save(path + "/Epics" + "/" + (cicl + 1).ToString() + "/" + (i + 1).ToString() + "/" + i.ToString() + "_re" + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                                }
                            }
                            i++;
                        }

                        loadingForm.loading.Invoke(new DelInt((s) => loadingForm.loading.Value = s), loadingForm.loading.Value + 1);
                        small = 10000;
                    }

                    int total = ResultFromModeling.Sum(x => Convert.ToInt32(x));
                    sum = total;
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
                        mean.Invoke(new Del((s) => mean.Text = s), MainStrings.average + MainStrings.none + "\n" + MainStrings.procentSuc + " " + count * 100.00 / (int.Parse(optText.Text)) + "\n" + MainStrings.procentFailed + " " + ((ResultFromModeling.Count - count) * 100.00 / (int.Parse(optText.Text))));
                        percentMean.Add(withoutSensorsBuses.Last(), null);
                        mean.Invoke(new Del((s) => mean.Text = s), MainStrings.average + MainStrings.none);
                    }
                    else
                    {
                        mean.Invoke(new Del((s) => mean.Text = s), MainStrings.average + " " + (total / ResultFromModeling.Count).ToString()
                            + "\n" + MainStrings.procentSuc + " " + ResultFromModeling.Count * 100.00 / (int.Parse(optText.Text)) + "\n" + MainStrings.procentFailed + " " + ((ResultFromModeling.Count - count) * 100.00 / (int.Parse(optText.Text))));
                        percentMean.Add(withoutSensorsBuses.Last(), total / ResultFromModeling.Count);
                        mean.Invoke(new Del((s) => mean.Text = s), MainStrings.average + " " + (Convert.ToDouble(total / ResultFromModeling.Count).ToString()));
                    }
;
                    using (StreamWriter fileV = new StreamWriter(path + @"\" + cicl.ToString() + "0%" + ".txt"))
                    {
                        fileV.WriteLine(MainStrings.sensorsDown + ": " + (cicl * 10).ToString());
                        fileV.WriteLine(MainStrings.countBuses + ": " + (withoutSensorsBuses.Last()).ToString());
                        fileV.WriteLine(MainStrings.numIter + ": " + optText.Text);
                        fileV.WriteLine(MainStrings.distance + ": " + speed.Text);
                        fileV.WriteLine(MainStrings.found + ": " + (from num in ResultFromModeling where (num != null) select num).Count());
                        fileV.WriteLine(MainStrings.average + " " + (total / ResultFromModeling.Count).ToString()
                            + "\n" + MainStrings.procentSuc + " " + (count * 100.00 / int.Parse(optText.Text)) + "\n" + MainStrings.procentFailed + " " + (((ResultFromModeling.Count - count) * 100.00 / int.Parse(optText.Text))).ToString());
                        fileV.WriteLine(MainStrings.cycle + " " + cicl.ToString());
                        for (int i = 0; i < ResultFromModeling.Count; i++)
                            if (ResultFromModeling[i] != null)
                            {
                                fileV.WriteLine(i.ToString() + " : " + ResultFromModeling[i].ToString());
                            }
                            else
                            {
                                fileV.WriteLine(i.ToString() + " : " + MainStrings.none);
                            }

                        Console.WriteLine("Объект сериализован");
                    }
                    ResultFromModeling = new List<int?>();
                }
                //busesPark = busesparkreturn; //невозможно взяимодействовать с басами после опти из-за этой строчки

                MetroMessageBox.Show(this, "", MainStrings.done, MessageBoxButtons.OK, MessageBoxIcon.Question);
            });
            var res = percentMean.Where(s => s.Value.Equals(percentMean.Min(v => v.Value))).Select(s => s.Key).ToList();
            var min = percentMean.Min(v => v.Value);
            if (res.Count != 0 && min != 0 && min != null)
                mean.Text = MainStrings.average + " " + min + " - " + MainStrings.countSensors + ": " + GetKeyByValue(percentMean.Min(v => v.Value));
            else
            {
                mean.Text = MainStrings.none;
            }

            using (StreamWriter fileV = new StreamWriter(path + "/Average.txt"))
            {
                fileV.WriteLine(sum != 0 ? MainStrings.average + " " + min + " - " + MainStrings.countSensors + ": " + GetKeyByValue(percentMean.Min(v => v.Value)) : "Null");
            }
            Matrix();
            resMatrix();

            msmMain.Style = style;
            SavePictures.Enabled = true;
            if (!Ep.IsDisposed)
            {
                Ep.Show();
                SavePictures.Enabled = true;
            }
            BarabanAfterOpti();
            foreach (var bus in buses)
            {
                bus.Start();
            }
            BringToFront();
            loadingForm.close = true;
            loadingForm.Close();
            buttonOn();
            busesPark = busesparkreturn;
            buses = optimizeBuses;
        }

        public static int EpicSizeParam = 10;
        public static List<string> ExpandEpicParamet;
        private void optimize_Click(object sender, EventArgs e)
        {
            if (optText.Text != "" && speed.Text != "" && buses.Count != 0 && int.Parse(optText.Text) > 0 && int.Parse(speed.Text) > 0 && buses != null)
            {
                withoutSensorsBuses = new List<int>();
                countWithoutSensors = buses.Count;
                bool check = false;
                foreach (var bus in buses)
                {
                    if (bus.tracker == true)
                    {
                        check = true;
                        break;
                    }
                }
                if (check)
                {
                    foreach (var bus in buses)
                        bus.Stop();
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
                    results.Rows[i].Cells[0].Value = pm.Value.ToString();
                i += 1;
            }
        }
        public MetroCheckBox GetSavePictruesCheckBox()
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
                            foreach (var tl in traficLights)
                            {
                                tl.Stop();
                            }
                            TraficLightsInGrids.Clear();
                            stopPoints.Clear();
                            allstopPoints.Clear();
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
                        addRouteToolStripMenuItem.Enabled = true;
                        createGridToolStripMenuItem.Enabled = true;
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
                    LoadRoutes(savepath + @"\");
                }
                catch (Exception exc)
                {
                    try
                    {
                        MetroMessageBox.Show(this, MainStrings.chooseWay, MainStrings.acrossThePath, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                                    savepath = dialog.SelectedPath;
                                    File.WriteAllText("../../SaveConfig/save.txt", string.Empty);
                                    using (StreamWriter fileV = new StreamWriter("../../SaveConfig/save.txt"))
                                    {
                                        fileV.WriteLine(savepath.ToString());
                                    }
                                    BringToFront();
                                    MetroMessageBox.Show(this, MainStrings.done, "", MessageBoxButtons.OK, MessageBoxIcon.Question);
                                }
                                addRouteToolStripMenuItem.Enabled = true;
                                createGridToolStripMenuItem.Enabled = true;
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
                            savepath = dialog.SelectedPath;
                            File.WriteAllText("../../SaveConfig/save.txt", string.Empty);
                            using (StreamWriter fileV = new StreamWriter("../../SaveConfig/save.txt"))
                            {
                                fileV.WriteLine(savepath.ToString());
                            }
                            addRouteToolStripMenuItem.Enabled = true;
                            createGridToolStripMenuItem.Enabled = true;
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
            G.clearSheet();
            G.drawALLGraph(V, E);
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
                G.drawALLGraph(routes[changeRoute.Text], routesEdge[changeRoute.Text], 1);
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


        List<SerializableDictionary<string, Edge>> routePointsEdge = new List<SerializableDictionary<string, Edge>>();
        private void Routes(int num, List<Vertex> route, List<Edge> edges)
        {
            SerializableDictionary<int, Vertex> rp = new SerializableDictionary<int, Vertex>();
            SerializableDictionary<int, Edge> ep = new SerializableDictionary<int, Edge>();
            foreach (var v in route)
            {
                for (int i = 0; i < V.Count; i++)
                {
                    if (v.X == V[i].X && v.Y == V[i].Y)
                    {
                        rp[i] = v;
                        break;
                    };


                }
            };
            routePoints.Add(rp);
        }



        private void Save_Click(object sender, EventArgs e)
        {
            SaveRoutes();
        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }
        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
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

                    File.Delete(save + "Vertexes.xml");
                    using (FileStream fileV = new FileStream(save + "Vertexes.xml", FileMode.OpenOrCreate))
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
                    File.WriteAllText(save + "Vertex.json", json);
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

        private Bitmap MakeGray(Bitmap bmp)
        {
            // Задаём формат Пикселя.
            PixelFormat pxf = PixelFormat.Format24bppRgb;

            // Получаем данные картинки.
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            //Блокируем набор данных изображения в памяти
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, pxf);

            // Получаем адрес первой линии.
            IntPtr ptr = bmpData.Scan0;

            // Задаём массив из Byte и помещаем в него надор данных.
            // int numBytes = bmp.Width * bmp.Height * 3; 
            //На 3 умножаем - поскольку RGB цвет кодируется 3-мя байтами
            //Либо используем вместо Width - Stride
            int numBytes = bmpData.Stride * bmp.Height;
            int widthBytes = bmpData.Stride;
            byte[] rgbValues = new byte[numBytes];

            // Копируем значения в массив.
            Marshal.Copy(ptr, rgbValues, 0, numBytes);

            // Перебираем пикселы по 3 байта на каждый и меняем значения
            for (int counter = 0; counter < rgbValues.Length; counter += 3)
            {

                int value = rgbValues[counter] + rgbValues[counter + 1] + rgbValues[counter + 2];
                byte color_b = 0;

                color_b = Convert.ToByte(value / 3);


                rgbValues[counter] = color_b;
                rgbValues[counter + 1] = color_b;
                rgbValues[counter + 2] = color_b;

            }
            // Копируем набор данных обратно в изображение
            Marshal.Copy(rgbValues, 0, ptr, numBytes);

            // Разблокируем набор данных изображения в памяти.
            bmp.UnlockBits(bmpData);

            return bmp;
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
                ZoomHelper();
                loadingForm = new LoadingForm
                {
                    close = false
                };
                loadingForm.Show();
                loadingForm.loading.Value = 0;
                routePoints.Clear();
                globalMap = sheet.Image;
                G.setBitmap();
                config.Text = MainStrings.config + load;
                if (File.Exists(load + "Vertexes.xml"))
                {
                    using (StreamReader reader = new StreamReader(load + "Vertexes.xml"))
                    {
                        XmlSerializer deserializerV = new XmlSerializer(typeof(List<Vertex>));
                        V = (List<Vertex>)deserializerV.Deserialize(reader);
                    }
                }

                if (File.Exists(load + "Vertex.json"))
                {
                    using (StreamReader reader = new StreamReader(load + "Vertex.json"))
                    {
                        V = JsonConvert.DeserializeObject<List<Vertex>>(File.ReadAllText(load + "Vertex.json"));
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

                if (File.Exists(load + "StopPoints.json"))
                {
                    using (StreamReader reader = new StreamReader(load + "StopPoints.json"))
                    {
                        stopPoints = JsonConvert.DeserializeObject<SerializableDictionary<string, List<Vertex>>>(File.ReadAllText(load + "StopPoints.json"));
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
                loadingForm.loading.Value = 30;

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

                if (AllCoordinates.Count == 0)
                    CreateAllCoordinates();


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
                loadingForm.loading.Value = 50;
                if (buses != null)
                {
                    foreach (var x in buses)
                    {
                        x.Stop();
                        mainPanel.Controls.Remove(x.busPic);
                    }
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


                foreach (var x in buses)
                {
                    x.busPic = new PictureBox
                    {
                        Location = new System.Drawing.Point(int.Parse((x.x + mainPanel.AutoScrollPosition.X).ToString()), int.Parse((x.y + mainPanel.AutoScrollPosition.Y).ToString())),
                        Size = new System.Drawing.Size(15, 15)
                    };
                    Bitmap bitmap = new Bitmap(Bus.sImg); //load the image file
                    using (Graphics graphics = Graphics.FromImage(bitmap))
                    {
                        using (Font arialFont = new Font("Arial", 300))
                        {
                            graphics.DrawString(x.route.ToString(), arialFont, Brushes.Black, new Point(10, 10));
                        }
                    }
                    x.skip = 5;
                    x.skipStops = 5;
                    x.skipEnd = 5;
                    x.busPic.Image = bitmap;
                    if (x.tracker == false)
                    {
                        x.busPic.Image = MakeGray(bitmap);
                    };
                    x.busPic.SizeMode = PictureBoxSizeMode.StretchImage;
                    mainPanel.Controls.Add(x.busPic);
                    x.busPic.BringToFront();
                    x.Set();

                }

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
                loadingForm.loading.Value = 90;
                openEpicFormToolStripMenuItem.Enabled = true;
                CreateGrid();
                CreatePollutionInRoutes();
                CreateOneRandomEpicenter(EpicSizeParam, null);
                Bus.setGrid(TheGrid);
                Bus.setAllCoordinates(AllCoordinates);

                addInComboBox();
                G.clearSheet();
                G.drawALLGraph(V, E);
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
            }
            catch (Exception exc)
            {
                loadingForm.close = true;
                loadingForm.Close();
                throw exc;
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
        private void sheet_MouseClick_1(object sender, MouseEventArgs e)
        {
            if (changeRoute.Text == MainStrings.network)
            {
                if (selectRoute.Enabled == false)
                {
                    for (int i = 0; i < V.Count; i++)
                    {
                        if (Math.Pow((V[i].X - e.X / zoom), 2) + Math.Pow((V[i].Y - e.Y / zoom), 2) <= G.R * G.R)
                        {
                            if (selected.Count == 0)
                            {
                                selected.Add(i);
                                G.drawSelectedVertex(V[i].X, V[i].Y);
                                sheet.Image = G.GetBitmap();
                                break;
                            }
                            else
                            {
                                selected.Add(i);
                                E.Add(new Edge(selected[0], selected[1]));
                                G.drawEdge(V[selected[0]], V[selected[1]], E[E.Count - 1], 1);
                                selected[0] = selected[1];
                                selected.Remove(selected[1]);
                                G.clearSheet();
                                G.drawALLGraph(V, E);
                                sheet.Image = G.GetBitmap();
                                DrawGrid();
                                G.drawSelectedVertex(V[i].X, V[i].Y);
                                break;
                            }

                        }
                    }
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
                                if (((e.X > gridPart.x * zoom) && (e.Y > gridPart.y * zoom)) && ((e.X < gridPart.x * zoom + GridPart.width * zoom) && (e.Y < gridPart.y * zoom + GridPart.height * zoom)))
                                {
                                    traficLights.Add(new TraficLight(e.X / zoom, e.Y / zoom, GetTheGrid().IndexOf(gridPart), firstCrossRoadsGreenLight, firstCrossRoadsRedLight));
                                    TraficLightsInGrids.Add(GetTheGrid().IndexOf(gridPart));
                                    G.drawGreenVertex(e.X / zoom, e.Y / zoom);
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
                                if (((e.X > gridPart.x * zoom) && (e.Y > gridPart.y * zoom)) && ((e.X < gridPart.x * zoom + GridPart.width * zoom) && (e.Y < gridPart.y * zoom + GridPart.height * zoom)))
                                {
                                    traficLights.Add(new TraficLight(e.X / zoom, e.Y / zoom, GetTheGrid().IndexOf(gridPart), firstCrossRoadsRedLight, firstCrossRoadsGreenLight));
                                    TraficLightsInGrids.Add(GetTheGrid().IndexOf(gridPart));
                                    traficLights.Last().tick = firstCrossRoadsRedLight + 2;
                                    traficLights.Last().status = Status.RED;
                                    label12.Text = MainStrings.putTrafficLights2 + " " + (secondCrossRoads - 1).ToString();
                                    G.drawSelectedVertex(e.X / zoom, e.Y / zoom);
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
                        loading.Enabled = true;
                        loading.Value = 0;
                        loading.Maximum = traficLights.Count;
                        traficLights.ForEach((tl) =>
                        {
                            loading.Value += 1;
                            tl.Set();
                            tl.Start();
                        });
                        //AsyncCreateAllCoordinates()();
                        loading.Enabled = false;
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
                        if (((e.X > gridPart.x * zoom) && (e.Y > gridPart.y * zoom)) && ((e.X < gridPart.x * zoom + GridPart.width * zoom) && (e.Y < gridPart.y * zoom + GridPart.height * zoom)))
                        {
                            allstopPoints.Add(new Vertex(e.X / zoom, e.Y / zoom));
                            G.drawStopVertex(e.X / zoom, e.Y / zoom);
                            sheet.Image = G.GetBitmap();
                            DrawGrid();
                        }
                    }
                }
                if (selectButton.Enabled == false)
                {
                    c.asSelect(e, V, E, G, sheet, 0);
                }
                //нажата кнопка "рисовать вершину"
                if (drawVertexButton.Enabled == false)
                {
                    c.drawVertex(e, V, G, sheet);
                }
                //нажата кнопка "рисовать ребро"
                if (drawEdgeButton.Enabled == false)
                {
                    c.asDrawEdge(e, V, E, G, sheet, 0);
                }
                //нажата кнопка "удалить элемент"
                if (deleteButton.Enabled == false)
                {
                    c.asDelete(e, V, E, sheet, G, routesEdge);
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
                                if (((e.X > gridPart.x * zoom) && (e.Y > gridPart.y * zoom)) && ((e.X < gridPart.x * zoom + GridPart.width * zoom) && (e.Y < gridPart.y * zoom + GridPart.height * zoom)))
                                {
                                    if (stopPoints.ContainsKey(changeRoute.Text))
                                    {
                                        stopPoints[changeRoute.Text].Add(new Vertex(sp.X, sp.Y));
                                        stopPoints[changeRoute.Text].Last().gridNum = GetTheGrid().IndexOf(gridPart);
                                        stopPointsInGrids[changeRoute.Text].Add(GetTheGrid().IndexOf(gridPart));
                                    }
                                    else
                                    {
                                        stopPoints.Add(changeRoute.Text, new List<Vertex>());
                                        stopPointsInGrids.Add(changeRoute.Text, new List<int>());
                                        stopPoints[changeRoute.Text].Add(new Vertex(sp.X, sp.Y));
                                        stopPoints[changeRoute.Text].Last().gridNum = GetTheGrid().IndexOf(gridPart);
                                        stopPointsInGrids[changeRoute.Text].Add(GetTheGrid().IndexOf(gridPart));
                                    }
                                    G.drawStopRouteVertex(sp.X, sp.Y);
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
                    c.asSelect(e, routeV, routesEdge[changeRoute.Text], G, sheet, 1);
                }
                if (selectRoute.Enabled == false)
                {
                    for (int i = 0; i < V.Count; i++)
                    {
                        if (Math.Pow((V[i].X - e.X / zoom), 2) + Math.Pow((V[i].Y - e.Y / zoom), 2) <= G.R * G.R)
                        {
                            if (selected.Count == 0)
                            {
                                selected.Add(i);
                                if (!routeV.Contains(new Vertex(V[i].X, V[i].Y)))
                                    routeV.Add(new Vertex(V[i].X, V[i].Y));
                                G.drawSelectedVertex(V[i].X, V[i].Y);
                                break;
                            }
                            else
                            {
                                selected.Add(i);
                                foreach (var ed in E)
                                {
                                    if ((ed.v1 == selected[0] && ed.v2 == selected[1]) || (ed.v2 == selected[0] && ed.v1 == selected[1]))
                                    {
                                        routesEdge[changeRoute.Text].Add(new Edge(routeV.Count - 1, routeV.Count));
                                        routeV.Add(new Vertex(V[i].X, V[i].Y));
                                        G.clearSheet();
                                        G.drawALLGraph(V, E);
                                        G.drawALLGraph(routeV, routesEdge[changeRoute.Text], 1);
                                        sheet.Image = G.GetBitmap();
                                        DrawGrid();
                                        G.drawSelectedVertex(V[i].X, V[i].Y);
                                        break;
                                    }
                                }
                            }
                            selected[0] = selected[1];
                            selected.Remove(selected[1]);

                        }
                    }


                }
                //нажата кнопка addBus
                if (addBus.Enabled == false)
                {
                    try
                    {
                        if (AllCoordinates[changeRoute.Text].Count != 0)
                        {
                            if (buses.Count != 0)
                                sizeBus = buses.Last().busPic.Width;
                            PictureBox busPic = new PictureBox
                            {
                                Location = new System.Drawing.Point(e.X + mainPanel.AutoScrollPosition.X, e.Y + mainPanel.AutoScrollPosition.Y)
                            };
                            if (busSize.Text != "")
                                busPic.Size = new System.Drawing.Size(int.Parse(busSize.Text), int.Parse(busSize.Text));
                            else
                                busPic.Size = new System.Drawing.Size(sizeBus, sizeBus);
                            busPic.Image = new Bitmap("../../Resources/shkolnyy-avtobus.png");
                            busPic.SizeMode = PictureBoxSizeMode.StretchImage;
                            mainPanel.Controls.Add(busPic);
                            busPic.BringToFront();

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
                                using (Graphics graphics = Graphics.FromImage(busPic.Image))
                                {
                                    using (Font arialFont = new Font("Arial", 300))
                                    {
                                        graphics.DrawString(changeRoute.Text, arialFont, Brushes.Black, new Point(10, 10));
                                    }
                                }
                                buses.Add(new Bus(busPic, pos, backsideCheck.Checked, changeRoute.Text, true));
                            }
                            else
                            {
                                busPic.Image = MakeGray(new Bitmap("../../Resources/shkolnyy-avtobus.png"));
                                using (Graphics graphics = Graphics.FromImage(busPic.Image))
                                {
                                    using (Font arialFont = new Font("Arial", 300))
                                    {
                                        graphics.DrawString(changeRoute.Text, arialFont, Brushes.Black, new Point(10, 10));
                                    }
                                }
                                buses.Add(new Bus(busPic, pos, backsideCheck.Checked, changeRoute.Text, false));
                            };
                            buses.Last().Set();
                        }
                    }
                    catch
                    {
                        MetroMessageBox.Show(this, MainStrings.bus, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                            if (Math.Pow((buses[i].busPic.Left - (e.X / zoom + mainPanel.AutoScrollPosition.X)), 2) + Math.Pow((buses[i].busPic.Top - (e.Y / zoom + mainPanel.AutoScrollPosition.Y)), 2) <= buses[i].R * buses[i].R * 500)
                            {
                                if (buses[i].route == changeRoute.Text)
                                {
                                    if (Math.Pow((buses[i].busPic.Left - (e.X / zoom + mainPanel.AutoScrollPosition.X)), 2) + Math.Pow((buses[i].busPic.Top - (e.Y / zoom + mainPanel.AutoScrollPosition.Y)), 2) < min)
                                    {
                                        min = Math.Pow((buses[i].busPic.Left - (e.X / zoom + mainPanel.AutoScrollPosition.X)), 2) + Math.Pow((buses[i].busPic.Top - (e.Y / zoom + mainPanel.AutoScrollPosition.Y)), 2);
                                        pos = i;
                                    }

                                }
                            }
                        }
                        if (pos != null)
                        {
                            buses[int.Parse(pos.ToString())].Stop();
                            mainPanel.Controls.Remove(buses[int.Parse(pos.ToString())].busPic);
                            buses.Remove(buses[int.Parse(pos.ToString())]);
                        }
                        G.clearSheet();
                        G.drawALLGraph(V, E);
                        G.drawALLGraph(routeV, routesEdge[changeRoute.Text], 1);
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
                                    G.drawSelectedVertex(V[i].X, V[i].Y);
                                    if (!routeV.Contains(new Vertex(V[i].X, V[i].Y)))
                                        routeV.Add(new Vertex(V[i].X, V[i].Y));
                                    selected1 = i;
                                    sheet.Image = G.GetBitmap();
                                    break;
                                }
                                if (selected2 == -1)
                                {
                                    G.drawSelectedVertex(V[i].X, V[i].Y);
                                    selected2 = i;
                                    foreach (var ed in E)
                                    {
                                        if ((ed.v1 == selected1 && ed.v2 == selected2) || (ed.v2 == selected1 && ed.v1 == selected2))
                                        {

                                            // if (!routeV.Contains(new Vertex(V[i].x, V[i].y))) {
                                            routesEdge[changeRoute.Text].Add(new Edge(routeV.Count - 1, routeV.Count));
                                            routeV.Add(new Vertex(V[i].X, V[i].Y));
                                            //  routesEdge[int.Parse(changeRoute.Text)].Add(new Edge(routeV.Count - (routeV.Count / 2 + 1), routeV.Count - (routeV.Count / 2)));                                    
                                            G.drawEdge(V[selected1], V[selected2], E[E.Count - 1], 1);
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
                                    G.clearSheet();
                                    G.drawALLGraph(V, E);
                                    G.drawALLGraph(routeV, routesEdge[changeRoute.Text], 1);
                                    selected1 = -1;
                                    selected2 = -1;
                                    sheet.Image = G.GetBitmap();
                                    DrawGrid();
                                    break;
                                }
                            }
                        }
                    }
                    //if (e.Button == MouseButtons.Right)
                    //{
                    //    if ((selected1 != -1) &&
                    //        (Math.Pow((routeV[selected1].x - e.X), 2) + Math.Pow((routeV[selected1].y - e.Y), 2) <= G.R * G.R))
                    //    {
                    //        G.drawVertex(routeV[selected1].x, routeV[selected1].y, (selected1 + 1).ToString());
                    //        selected1 = -1;
                    //        sheet.Image = G.GetBitmap();
                    //    }
                    //}
                }
                //нажата кнопка "удалить элемент"
                if (deleteButton.Enabled == false)
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
                    for (int i = 0; i < routeV.Count; i++)
                    {
                        if (Math.Pow((routeV[i].X - e.X / zoom), 2) + Math.Pow((routeV[i].Y - e.Y / zoom), 2) <= G.R * G.R)
                        {
                            for (int j = 0; j < routesEdge[changeRoute.Text].Count; j++)
                            {
                                if ((routesEdge[changeRoute.Text][j].v1 == i) || (routesEdge[changeRoute.Text][j].v2 == i))
                                {
                                    routesEdge[changeRoute.Text].RemoveAt(j);
                                    j--;
                                }
                                else
                                {
                                    if (routesEdge[changeRoute.Text][j].v1 > i) routesEdge[changeRoute.Text][j].v1--;
                                    if (routesEdge[changeRoute.Text][j].v2 > i) routesEdge[changeRoute.Text][j].v2--;
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
                            if (routesEdge[changeRoute.Text][i].v1 == routesEdge[changeRoute.Text][i].v2) //если это петля
                            {
                                if ((Math.Pow((routeV[routesEdge[changeRoute.Text][i].v1].X - G.R - e.X / zoom), 2) + Math.Pow((routeV[routesEdge[changeRoute.Text][i].v1].Y - G.R - e.Y / zoom), 2) <= ((G.R + 2) * (G.R + 2))) &&
                                    (Math.Pow((routeV[routesEdge[changeRoute.Text][i].v1].X - G.R - e.X / zoom), 2) + Math.Pow((routeV[routesEdge[changeRoute.Text][i].v1].Y - G.R - e.Y / zoom), 2) >= ((G.R - 2) * (G.R - 2))))
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
                                    if (((e.X / zoom - routeV[routesEdge[changeRoute.Text][i].v1].X) * (routeV[routesEdge[(changeRoute.Text)][i].v2].Y - routeV[routesEdge[changeRoute.Text][i].v1].Y) / (routeV[routesEdge[(changeRoute.Text)][i].v2].X - routeV[routesEdge[(changeRoute.Text)][i].v1].X) + routeV[routesEdge[(changeRoute.Text)][i].v1].Y) <= (e.Y / zoom + 4) &&
                                        ((e.X / zoom - routeV[routesEdge[(changeRoute.Text)][i].v1].X) * (routeV[routesEdge[(changeRoute.Text)][i].v2].Y - routeV[routesEdge[(changeRoute.Text)][i].v1].Y) / (routeV[routesEdge[(changeRoute.Text)][i].v2].X - routeV[routesEdge[(changeRoute.Text)][i].v1].X) + routeV[routesEdge[(changeRoute.Text)][i].v1].Y) >= (e.Y / zoom - 4))
                                    {
                                        if ((routeV[routesEdge[(changeRoute.Text)][i].v1].X <= routeV[routesEdge[(changeRoute.Text)][i].v2].X && routeV[routesEdge[(changeRoute.Text)][i].v1].X <= e.X / zoom && e.X / zoom <= routeV[routesEdge[(changeRoute.Text)][i].v2].X) ||
                                            (routeV[routesEdge[(changeRoute.Text)][i].v1].X >= routeV[routesEdge[(changeRoute.Text)][i].v2].X && routeV[routesEdge[(changeRoute.Text)][i].v1].X >= e.X / zoom && e.X / zoom >= routeV[routesEdge[(changeRoute.Text)][i].v2].X))
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
                        G.clearSheet();
                        G.drawALLGraph(V, E);
                        G.drawALLGraph(routeV, routesEdge[(changeRoute.Text)], 1);
                        sheet.Image = G.GetBitmap();
                        DrawGrid();
                    }

                }
                //   

                DrawGrid();
                CreateOneRouteCoordinates((changeRoute.Text));
                Bus.AllCoordinates = AllCoordinates;
                return;
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
                G.clearSheet();
                sheet.Image = G.GetBitmap();
                DrawGrid();
                if(!Ep.IsDisposed)
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

        private void delAllBusesOnRoute_Click(object sender, EventArgs e)
        {
            var MBSave = MetroMessageBox.Show(this, MainStrings.deleteBus, MainStrings.delete, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (MBSave == DialogResult.Yes)
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
                    foreach (var bus in buses)
                    {
                        bus.Stop();
                        mainPanel.Controls.Remove(bus.busPic);
                    }
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
                            bus.Stop();
                            mainPanel.Controls.Remove(bus.busPic);
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
                if (firstCrossRoads != 0 && secondCrossRoads != 0)
                {
                    label12.Visible = true;
                    label12.Text = MainStrings.putTrafficLights1 + " " + firstCrossRoads.ToString();
                }
                selected = new List<int>();
                stopPointButton.Enabled = true;
                addTraficLight.Enabled = false;
                selectRoute.Enabled = true;
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
            }
            else
            {
                msmMain.Theme = MetroFramework.MetroThemeStyle.Light;
                toolStripMenu.BackColor = Color.FromArgb(255, 255, 255);
                toolStripMenu.ForeColor = Color.FromArgb(0, 0, 0);
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
                fileV.WriteLine(msmMain.Style);
            }
            if (Ep != null)
            {
                Ep.Refresh();
            }
        }

        private void launchBuses_Click(object sender, EventArgs e)
        {
            BarabanAfterOpti();
            foreach (var bus in buses)
            {

                bus.Start();
            }
        }

        private void stopBuses_Click(object sender, EventArgs e)
        {
            foreach (var bus in buses)
            {
                bus.Stop();
            }
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

        private void metroButton1_Click(object sender, EventArgs e)
        {

        }
        private void ZoomHelper()
        {
            sheet.Image = ResizeBitmap(new Bitmap(saveImage), wsheet * metroTrackBar1.Value, hsheet * metroTrackBar1.Value);
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

                G.clearSheet();
                if (changeRoute.SelectedIndex > 1)
                {
                    G.drawALLGraph(V, E);
                    G.drawALLGraph(routes[(changeRoute.Text)], routesEdge[(changeRoute.Text)], 1);
                }
                else if (changeRoute.Text == MainStrings.none)
                {
                    G.clearSheet();
                }
                else
                {
                    G.drawALLGraph(V, E);
                }

                sheet.Image = G.GetBitmap();
                DrawGrid();
                Console.WriteLine("Done");
                foreach (var bus in buses)
                {
                    bus.AlignBus();
                }
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

        private void metroButton1_Click_2(object sender, EventArgs e)
        {
            foreach (var bus in buses)
            {
                bus.PositionAt = 1;
                bus.Stop();
            }
            CreateAllCoordinates();
            BarabanAfterOpti();
            foreach (var bus in buses)
            {

                bus.Start();
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

        private void createGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (G.bitmap != null)
            {
                addG = new AddGrid();
                this.StyleManager.Clone(addG);
                addG.ShowDialog();
                G.clearSheet();
                if (!Ep.IsDisposed)
                {
                    Ep.EG.clearSheet2();
                }
                G.drawALLGraph(V, E);
                CreateGrid();
                sheet.Image = G.GetBitmap();
                DrawGrid();
                if (!Ep.IsDisposed)
                {
                    Ep.EDrawGrid();
                }
                CreatePollutionInRoutes();
                CreateAllCoordinates();
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
