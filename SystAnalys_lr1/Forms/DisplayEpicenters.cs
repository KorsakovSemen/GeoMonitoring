using MetroFramework.Controls;
using MetroFramework.Forms;
using SystAnalys_lr1.Classes;
using SystAnalys_lr1.Forms;
using SystAnalys_lr1.Strings;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using System.Windows.Forms;

namespace SystAnalys_lr1
{
    public partial class DisplayEpicenters : MetroForm
    {
        private static string s_path;
        private int _wsheet;
        private int _hsheet;
        private static int s_ezoom = 1;
        private static Image s_zoomPicture;
        private static Image s_esheetPicture;
        private static int s_eTimerTimeCounts;
        private static int s_eTimerMovingTimeCounts;
        private static int s_eTimerExpandingTimeCounts;
        private static List<Epicenter> s_copiedEpics;
        private static bool formOpen;
        private Epicenter restoredEpic;
        private DrawGraph eG;
        private readonly Main MainForm;

        public EpicSettings epSet;
        public static string Path { get => s_path; set => s_path = value; }
        public int Wsheet { get => _wsheet; set => _wsheet = value; }
        public int Hsheet { get => _hsheet; set => _hsheet = value; }
        private static int Ezoom { get => s_ezoom; set => s_ezoom = value; }
        public static Image ZoomPicture { get => s_zoomPicture; set => s_zoomPicture = value; }
        public static Image EsheetPicture { get => s_esheetPicture; set => s_esheetPicture = value; }
        private static int ETimerTimeCounts { get => s_eTimerTimeCounts; set => s_eTimerTimeCounts = value; }
        private static int ETimerMovingTimeCounts { get => s_eTimerMovingTimeCounts; set => s_eTimerMovingTimeCounts = value; }
        private static int ETimerExpandingTimeCounts { get => s_eTimerExpandingTimeCounts; set => s_eTimerExpandingTimeCounts = value; }
        private static List<Epicenter> CopiedEpics { get => s_copiedEpics; set => s_copiedEpics = value; }
        public static bool FormOpen { get => formOpen; set => formOpen = value; }
        public Epicenter RestoredEpic { get => restoredEpic; set => restoredEpic = value; }
        public DrawGraph EG { get => eG; set => eG = value; }



        public DisplayEpicenters(Main Main)
        {
            this.MainForm = Main;
            InitializeComponent();
        }

        public PictureBox Esheet = new PictureBox();
        private readonly MetroLabel label = new MetroLabel();


        private void DisplayEpicenters_Load(object sender, EventArgs e)
        {
            FormOpen = true;
            ERefreshRouts();
            this.ERouts.SelectedIndexChanged += ERouts_SelectedIndexChanged;
            this.MapPanel.AutoScroll = MainForm.mainPanel.AutoScroll;

            this.MapPanel.Dock = MainForm.mainPanel.Dock;
            this.MapPanel.Controls.Add(Esheet);


            Esheet.Dock = MainForm.GetSheet().Dock;
            Esheet.Location = new Point(0, 1);
            Esheet.Size = MainForm.GetSheet().Size;
            Esheet.Image = Main.GlobalMap;
            EsheetPicture = Main.GlobalMap;
            ZoomPicture = Main.GlobalMap;
            Wsheet = Esheet.Width;
            Hsheet = Esheet.Height;
            Esheet.AutoSize = MainForm.GetSheet().AutoSize;
            Esheet.SizeMode = MainForm.GetSheet().SizeMode;
            Esheet.Anchor = MainForm.GetSheet().Anchor;
            EG = new DrawGraph();
            EG.SetBitmap2();



            label.Text = "|";
            label.Location = new Point(EZoomBar.Width / 2 + 11, panel1.Height - 65);

            panel1.Controls.Add(ERouts);
            panel1.Controls.Add(label);

            EDrawMainEpics();
            Esheet.MouseClick += Esheet_MouseClick;

        }

        private void Esheet_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (var gridPart in Data.TheGrid)
            {
                if (((e.X > gridPart.X * Ezoom) && (e.Y > gridPart.Y * Ezoom)) && ((e.X < gridPart.X * Ezoom + GridPart.Width * Ezoom) && (e.Y < gridPart.Y * Ezoom + GridPart.Height * Ezoom)))
                {

                    ERefreshRouts();
                    Epicenter.CreateOneRandomEpicenter(Main.EpicSizeParam, Data.TheGrid.IndexOf(gridPart));
                    CopiedEpics = Epicenter.CopyEpicenter(CopiedEpics);
                    EG.ClearSheet2();

                    EDrawPollutions();
                    ETimerTimeCounts = 0;
                    break;
                }

            }

        }
        private void EZoomBar_Scroll(object sender, EventArgs e)
        {

            Esheet.Image = Main.ResizeBitmap(new Bitmap(ZoomPicture), Wsheet * EZoomBar.Value, Hsheet * EZoomBar.Value);
            ZoomPicture = Main.ResizeBitmap(new Bitmap(EsheetPicture), Wsheet * EZoomBar.Value, Hsheet * EZoomBar.Value);
            MapPanel.AutoScrollPosition = new Point(MapPanel.AutoScrollPosition.X * EZoomBar.Value, MapPanel.AutoScrollPosition.Y * EZoomBar.Value);
            Ezoom = EZoomBar.Value;

            EG.ClearSheet2();
            Esheet.Image = EG.GetBitmap();
            EDrawPollutions();
        }


        public void ERefreshRouts()
        {
            ERouts.Items.Clear();
            foreach (var r in MainForm.changeRoute.Items)
            {
                ERouts.Items.Add(r);
            };
            if (ERouts.Items.Count != 0)
                ERouts.SelectedIndex = 0;
        }
        delegate void DelBitmap(Bitmap b);
        public void EDrawEpics(List<Epicenter> Epics)
        {
            EG.ClearSheet2();
            if (Epics != null)
            {
                for (int i = 0; i < Epics.Count; i++)
                {
                    Epics[i].DrawEpicenter(EG, Ezoom);
                }

            }
            EDrawGrid();


        }
        public void EDrawMainEpics()
        {
            if (SimulatingTimer.Enabled)
            {
                SimulatingTimer.Enabled = false;
            }
            EG.ClearSheet2();

            Esheet.Invoke(new DelBitmap((b) => Esheet.Image = b), EG.GetBitmap());
            if (Data.Epics != null)
            {
                for (int i = 0; i < Data.Epics.Count; i++)
                {
                    Data.Epics[i].DrawEpicenter(EG, Ezoom);
                }

            }
            EDrawGrid();


        }
        public void EDrawGrid()
        {
            for (int i = 0; i < Data.TheGrid.Count; i++)
            {
                Data.TheGrid[i].DrawPart(EG, Ezoom);
            }
            Esheet.Invoke(new DelBitmap((b) => Esheet.Image = b), EG.GetBitmap());

        }
        private void EDrawAllPollutionsInRoutes()
        {
            for (int i = 0; i < Modeling.PollutionInRoutes.Count; i++)
            {
                for (int j = 0; j < Modeling.PollutionInRoutes[Modeling.PollutionInRoutes.ElementAt(i).Key].Count; j++)
                {
                    switch (Modeling.PollutionInRoutes[Modeling.PollutionInRoutes.ElementAt(i).Key][j].Status)
                    {
                        case 0:
                            EG.Gr.FillRectangle(new SolidBrush(Color.FromArgb(170, 0, 128, 0)), new Rectangle(Modeling.PollutionInRoutes[Modeling.PollutionInRoutes.ElementAt(i).Key][j].X * Ezoom, Modeling.PollutionInRoutes[Modeling.PollutionInRoutes.ElementAt(i).Key][j].Y * Ezoom, GridPart.Width * Ezoom, GridPart.Height * Ezoom));
                            break;

                        case 1:
                            EG.Gr.FillRectangle(new SolidBrush(Color.FromArgb(170, 255, 255, 0)), new Rectangle(Modeling.PollutionInRoutes[Modeling.PollutionInRoutes.ElementAt(i).Key][j].X * Ezoom, Modeling.PollutionInRoutes[Modeling.PollutionInRoutes.ElementAt(i).Key][j].Y * Ezoom, GridPart.Width * Ezoom, GridPart.Height * Ezoom));
                            break;


                        case 2:
                            EG.Gr.FillRectangle(new SolidBrush(Color.FromArgb(170, 255, 128, 0)), new Rectangle(Modeling.PollutionInRoutes[Modeling.PollutionInRoutes.ElementAt(i).Key][j].X * Ezoom, Modeling.PollutionInRoutes[Modeling.PollutionInRoutes.ElementAt(i).Key][j].Y * Ezoom, GridPart.Width * Ezoom, GridPart.Height * Ezoom));

                            break;
                        case 3:
                            EG.Gr.FillRectangle(new SolidBrush(Color.FromArgb(170, 255, 0, 0)), new Rectangle(Modeling.PollutionInRoutes[Modeling.PollutionInRoutes.ElementAt(i).Key][j].X * Ezoom, Modeling.PollutionInRoutes[Modeling.PollutionInRoutes.ElementAt(i).Key][j].Y * Ezoom, GridPart.Width * Ezoom, GridPart.Height * Ezoom));
                            break;
                        default:

                            break;
                    }
                }
            }
            Esheet.Image = EG.GetBitmap();
        }
        private void ERouts_SelectedIndexChanged(object sender, EventArgs e)
        {
            EDrawPollutions();
        }
        private void EDrawPollutions()
        {
            EG.ClearSheet2();
            EDrawMainEpics();
            if (ERouts.Text == MainStrings.none)
            {

                return;
            };
            if (ERouts.Text == MainStrings.network)
            {

                EDrawAllPollutionsInRoutes();
                return;
            };
            for (int i = 0; i < Modeling.PollutionInRoutes.Count; i++)
            {
                if (Modeling.PollutionInRoutes.ElementAt(i).Key == (ERouts.Text))
                {

                    for (int j = 0; j < Modeling.PollutionInRoutes[Modeling.PollutionInRoutes.ElementAt(i).Key].Count; j++)
                    {
                        switch (Modeling.PollutionInRoutes[Modeling.PollutionInRoutes.ElementAt(i).Key][j].Status)
                        {
                            case 0:
                                EG.Gr.FillRectangle(new SolidBrush(Color.FromArgb(170, 0, 128, 0)), new Rectangle(Modeling.PollutionInRoutes[Modeling.PollutionInRoutes.ElementAt(i).Key][j].X * Ezoom, Modeling.PollutionInRoutes[Modeling.PollutionInRoutes.ElementAt(i).Key][j].Y * Ezoom, GridPart.Width * Ezoom, GridPart.Height * Ezoom));
                                break;

                            case 1:
                                EG.Gr.FillRectangle(new SolidBrush(Color.FromArgb(170, 255, 255, 0)), new Rectangle(Modeling.PollutionInRoutes[Modeling.PollutionInRoutes.ElementAt(i).Key][j].X * Ezoom, Modeling.PollutionInRoutes[Modeling.PollutionInRoutes.ElementAt(i).Key][j].Y * Ezoom, GridPart.Width * Ezoom, GridPart.Height * Ezoom));
                                break;


                            case 2:
                                EG.Gr.FillRectangle(new SolidBrush(Color.FromArgb(170, 255, 128, 0)), new Rectangle(Modeling.PollutionInRoutes[Modeling.PollutionInRoutes.ElementAt(i).Key][j].X * Ezoom, Modeling.PollutionInRoutes[Modeling.PollutionInRoutes.ElementAt(i).Key][j].Y * Ezoom, GridPart.Width * Ezoom, GridPart.Height * Ezoom));

                                break;
                            case 3:
                                EG.Gr.FillRectangle(new SolidBrush(Color.FromArgb(170, 255, 0, 0)), new Rectangle(Modeling.PollutionInRoutes[Modeling.PollutionInRoutes.ElementAt(i).Key][j].X * Ezoom, Modeling.PollutionInRoutes[Modeling.PollutionInRoutes.ElementAt(i).Key][j].Y * Ezoom, GridPart.Width * Ezoom, GridPart.Height * Ezoom));
                                break;
                            default:

                                break;
                        }
                    }
                    Esheet.Image = EG.GetBitmap();
                };
            }
        }

        private void Button11_Click(object sender, EventArgs e)
        {

            epSet = new EpicSettings();
            this.StyleManager.Clone(epSet);
            epSet.ShowDialog();

        }
        public void RecReateFunction()
        {
            RestoredEpic = new Epicenter(Data.TheGrid);

            RestoredEpic.Recreate(Modeling.PollutionInRoutes);


            EG.ClearSheet2();
            for (int i = 0; i < Data.TheGrid.Count; i++)
            {
                Data.TheGrid[i].FillGreen(EG, Ezoom);
            }
            RestoredEpic.DrawEpicenter(EG, Ezoom);

            EDrawGrid();
        }
        private void DisplayEpicenters_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormOpen = false;
            EpicSettings.SavePictures = false;
            EpicSettings.ExtendedSavePictures = false;
            Esheet.Image = Main.ResizeBitmap(new Bitmap(EsheetPicture), Wsheet, Hsheet);
            Ezoom = 1;
        }
        private void metroButton2_Click(object sender, EventArgs e)
        {
            if (CopiedEpics != null)
            {
                if (SimulatingTimer.Enabled)
                {
                    SimulatingTimer.Enabled = false;
                }
                else
                {
                    SimulatingTimer.Enabled = true;
                }
            }

        }

        private void ModelingTimer_Tick(object sender, EventArgs e)
        {

            if (this.Visible)
            {
                if (EpicSettings.TurnMovingSet == true || EpicSettings.TurnExpandingSet == true)
                {
                    if (EpicSettings.TurnMovingSet == true && EpicSettings.MovingEpicParamet.Any())
                    {
                        if (ETimerMovingTimeCounts >= EpicSettings.EpicFreqMovingParam)
                        {
                            CopiedEpics.First().EpicMoving(EpicSettings.MovingEpicParamet);
                            ETimerMovingTimeCounts = 0;
                        }
                        else
                        {
                            ETimerMovingTimeCounts += 100;
                        }


                    }
                    if (EpicSettings.TurnExpandingSet == true)
                    {
                        if (ETimerExpandingTimeCounts >= EpicSettings.EpicFreqSpreadingParam)
                        {
                            CopiedEpics.First().ExpandEpic();
                            ETimerExpandingTimeCounts = 0;
                        }
                        else
                        {
                            ETimerExpandingTimeCounts += 100;
                        }
                    }
                    EDrawEpics(CopiedEpics);
                }
                GC.Collect();
                ETimerTimeCounts += 100;
                switch (System.Globalization.CultureInfo.CurrentCulture.ToString())
                {
                    case "en-US":
                        TimePastLabel.Text = "Time has passed : " + (ETimerTimeCounts / 60).ToString() + " minutes";
                        break;
                    case "ru-RU":
                        TimePastLabel.Text = "Времени прошло : " + (ETimerTimeCounts / 60).ToString() + " минут";
                        break;
                    default:
                        break;
                }

            }
            else
            {
                SimulatingTimer.Enabled = false;
            }
        }

        private void ShowOriginalButton_Click(object sender, EventArgs e)
        {
            EDrawMainEpics();
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            SimulatingTimer.Enabled = false;
            EDrawEpics(CopiedEpics);
        }


    }
}
