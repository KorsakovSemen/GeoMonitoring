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
        public static bool FormOpen;
        private Epicenter restoredEpic;
        public static string path { get; set; }
        public int wsheet { get; set; }
        public int hsheet { get; set; }
        private static int Ezoom { get; set; } = 1;
        public static Image EsheetPicture { get; set; }
        public DrawGraph EG;
        private Main MainForm;
        public DisplayEpicenters(Main Main)
        {
            this.MainForm = Main;
            InitializeComponent();

        }

        private MetroTrackBar EZoomBar = new MetroTrackBar();
        public PictureBox Esheet = new PictureBox();
        private MetroFramework.Controls.MetroPanel MapPanel = new MetroFramework.Controls.MetroPanel();
 

        private void DisplayEpicenters_Load(object sender, EventArgs e)
        {
            FormOpen = true;
            this.MinimumSize = MainForm.MinimumSize;
            this.MaximumSize = MainForm.MaximumSize;
            //MainForm.GetSavePictruesCheckBox().Enabled = true;
            panel1.Size = MainForm.GetMainPanel().Size;
            panel1.Left = MainForm.GetMainPanel().Left;
            panel1.Top = MainForm.GetMainPanel().Top;
            panel1.Dock = MainForm.GetMainPanel().Dock;
            ERefreshRouts();
            this.ERouts.SelectedIndexChanged += ERouts_SelectedIndexChanged;
            // Esheet.Dock = DockStyle.Right;
            ////
            this.MapPanel.Dock = MainForm.GetMapPanel().Dock;
            this.MapPanel.AutoSize = MainForm.GetMapPanel().AutoSize;
            this.MapPanel.Location = MainForm.GetMapPanel().Location;
           
            this.MapPanel.AutoScroll = MainForm.GetMapPanel().AutoScroll;
            this.MapPanel.BorderStyle = MainForm.GetMapPanel().BorderStyle;
            this.MapPanel.AutoSizeMode = MainForm.GetMapPanel().AutoSizeMode;
           
            //this.MapPanel.MaximumSize = MainForm.GetMapPanel().MaximumSize;
            //this.MapPanel.Size = MainForm.GetMapPanel().Size;

            //this.MapPanel.Dock = MainForm.GetMapPanel().Dock;
            ////
            this.Width = MainForm.Width;
            this.Height = MainForm.Height;
            ////
            this.Controls.Add(MapPanel);
            this.MapPanel.Controls.Add(Esheet);
            Esheet.Dock = MainForm.GetSheet().Dock;

            Esheet.Location = new Point(panel1.Width, 1);
            Esheet.Size = MainForm.GetSheet().Size;
            Esheet.Image = Main.globalMap;
            EsheetPicture = Main.globalMap;
            wsheet = Esheet.Width;
            hsheet = Esheet.Height;
            Esheet.AutoSize = MainForm.GetSheet().AutoSize;
            Esheet.SizeMode = MainForm.GetSheet().SizeMode;
            Esheet.Anchor = MainForm.GetSheet().Anchor;
            EG = new DrawGraph();
            EG.SetBitmap2();
            ///
            //EZoomBar = MainForm.GetTrackBar();
            EZoomBar.StyleManager = MainForm.GetTrackBar().StyleManager;
            EZoomBar.Size = MainForm.GetTrackBar().Size;
            EZoomBar.LargeChange = MainForm.GetTrackBar().LargeChange;
            EZoomBar.Value = MainForm.GetTrackBar().Value;
            EZoomBar.Maximum = MainForm.GetTrackBar().Maximum;
            EZoomBar.Minimum = MainForm.GetTrackBar().Minimum;
            EZoomBar.Left = MainForm.GetTrackBar().Left;  
            EZoomBar.Top = MainForm.GetTrackBar().Top;
            EZoomBar.Scroll += EZoomBar_Scroll;
            //
            this.panel1.Controls.Add(ERouts);
            this.panel1.Controls.Add(EZoomBar);



            //EDrawGrid();
            EDrawMAinEpics();
            Esheet.MouseClick += Esheet_MouseClick;
        }

        private void Esheet_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (var gridPart in MainForm.GetTheGrid())
            {
                if (((e.X > gridPart.x * Ezoom) && (e.Y > gridPart.y * Ezoom)) && ((e.X < gridPart.x * Ezoom + GridPart.Width * Ezoom) && (e.Y < gridPart.y * Ezoom + GridPart.Height * Ezoom)))
                {

                    ERefreshRouts();
                    MainForm.CreateOneRandomEpicenter(Main.EpicSizeParam, MainForm.GetTheGrid().IndexOf(gridPart));

                    EG.ClearSheet2();
             
                    EDrawPollutions();
                    break;
                }

            }

        }
        private void EZoomBar_Scroll(object sender, EventArgs e)
        {

            Esheet.Image = Main.ResizeBitmap(new Bitmap(EsheetPicture), wsheet * EZoomBar.Value, hsheet * EZoomBar.Value);
            EsheetPicture = Main.ResizeBitmap(new Bitmap(EsheetPicture), wsheet * EZoomBar.Value, hsheet * EZoomBar.Value);
            EsheetPicture = Main.ResizeBitmap(new Bitmap(EsheetPicture), wsheet * EZoomBar.Value, hsheet * EZoomBar.Value);
            MapPanel.AutoScrollPosition = new Point(MapPanel.AutoScrollPosition.X * EZoomBar.Value, MapPanel.AutoScrollPosition.Y * EZoomBar.Value);
            Ezoom = EZoomBar.Value;

            EG.ClearSheet2();
            Esheet.Image = EG.GetBitmap();
            EDrawPollutions();
        }


        public void ERefreshRouts()
        {
            ERouts.Items.Clear();
            foreach (var r in MainForm.GetcomboBox1().Items)
            {
                ERouts.Items.Add(r);
            };
            if(ERouts.Items.Count != 0)
                ERouts.SelectedIndex = 0;
        }
        delegate void DelBitmap(Bitmap b);
        public void EDrawEpics(System.Collections.Generic.List<Epicenter>Epics)
        {
            EG.ClearSheet2();
            Esheet.Invoke(new DelBitmap((b) => Esheet.Image = b), EG.GetBitmap()); //ошибка при закрытии формы
            // Esheet.Image = EG.GetBitmap();
            if (Epics != null)
            {
                for (int i = 0; i < Epics.Count; i++)
                {
                    Epics[i].DrawEpicenter(EG, Ezoom);
                }

            }
            EDrawGrid();


        }
        public void EDrawMAinEpics()
        {
            EG.ClearSheet2();
           
            Esheet.Invoke(new DelBitmap((b) => Esheet.Image = b), EG.GetBitmap()); //ошибка при закрытии формы
            // Esheet.Image = EG.GetBitmap();
            if (MainForm.GetEpicenters() != null)
            {
                for (int i = 0; i < MainForm.GetEpicenters().Count; i++)
                {
                    MainForm.GetEpicenters()[i].DrawEpicenter(EG, Ezoom);
                }

            }
            EDrawGrid();


        }
        public void EDrawGrid()
        {
            //EG.clearSheet();
            for (int i = 0; i < MainForm.GetTheGrid().Count; i++)
            {
                MainForm.GetTheGrid()[i].DrawPart(EG, Ezoom);
            }
            Esheet.Invoke(new DelBitmap((b) => Esheet.Image = b), EG.GetBitmap());
            // Esheet.Image = EG.GetBitmap();

        }

        private void EDrawAllPollutionsInRoutes()
        {
            for (int i = 0; i < MainForm.GetPollutionInRoutes().Count; i++)
            {
                for (int j = 0; j < MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key].Count; j++)
                {
                    switch (MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key][j].Status)
                    {
                        case 0:
                            EG.gr.FillRectangle(new SolidBrush(Color.FromArgb(170, 0, 128, 0)), new Rectangle(MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key][j].x * Ezoom, MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key][j].y * Ezoom, GridPart.Width * Ezoom, GridPart.Height * Ezoom));
                            break;

                        case 1:
                            EG.gr.FillRectangle(new SolidBrush(Color.FromArgb(170, 255, 255, 0)), new Rectangle(MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key][j].x * Ezoom, MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key][j].y * Ezoom, GridPart.Width * Ezoom, GridPart.Height * Ezoom));
                            break;


                        case 2:
                            EG.gr.FillRectangle(new SolidBrush(Color.FromArgb(170, 255, 128, 0)), new Rectangle(MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key][j].x * Ezoom, MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key][j].y * Ezoom, GridPart.Width * Ezoom, GridPart.Height * Ezoom));

                            break;
                        case 3:
                            EG.gr.FillRectangle(new SolidBrush(Color.FromArgb(170, 255, 0, 0)), new Rectangle(MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key][j].x * Ezoom, MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key][j].y * Ezoom, GridPart.Width * Ezoom, GridPart.Height * Ezoom));
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
            EDrawMAinEpics();
            if (ERouts.Text == MainStrings.none)
            {

                return;
            };
            if (ERouts.Text == MainStrings.network)
            {

                EDrawAllPollutionsInRoutes();
                return;
            };
            for (int i = 0; i < MainForm.GetPollutionInRoutes().Count; i++)
            {
                if (MainForm.GetPollutionInRoutes().ElementAt(i).Key == (ERouts.Text))
                {

                    for (int j = 0; j < MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key].Count; j++)
                    {
                        switch (MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key][j].Status)
                        {
                            case 0:
                                EG.gr.FillRectangle(new SolidBrush(Color.FromArgb(170, 0, 128, 0)), new Rectangle(MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key][j].x * Ezoom, MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key][j].y * Ezoom, GridPart.Width * Ezoom, GridPart.Height * Ezoom));
                                break;

                            case 1:
                                EG.gr.FillRectangle(new SolidBrush(Color.FromArgb(170, 255, 255, 0)), new Rectangle(MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key][j].x * Ezoom, MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key][j].y * Ezoom, GridPart.Width * Ezoom, GridPart.Height * Ezoom));
                                break;


                            case 2:
                                EG.gr.FillRectangle(new SolidBrush(Color.FromArgb(170, 255, 128, 0)), new Rectangle(MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key][j].x * Ezoom, MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key][j].y * Ezoom, GridPart.Width * Ezoom, GridPart.Height * Ezoom));

                                break;
                            case 3:
                                EG.gr.FillRectangle(new SolidBrush(Color.FromArgb(170, 255, 0, 0)), new Rectangle(MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key][j].x * Ezoom, MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key][j].y * Ezoom, GridPart.Width * Ezoom, GridPart.Height * Ezoom));
                                break;
                            default:
                           
                                break;
                        }
                    }
                    Esheet.Image = EG.GetBitmap();
                };
            }
        }


        public EpicSettings epSet;
        private void button11_Click(object sender, EventArgs e)
        {

            epSet = new EpicSettings();
            this.StyleManager.Clone(epSet);
            epSet.ShowDialog();
    
        }
        public void RecReateFunction()
        {
            restoredEpic = new Epicenter(MainForm.GetTheGrid());

            restoredEpic.Recreate(MainForm.GetPollutionInRoutes());


            EG.ClearSheet2();
            //Esheet.Invoke(new DelBitmap((b) => Esheet.Image = b), EG.GetBitmap());
            // Esheet.Image = EG.GetBitmap();
            for (int i = 0; i < MainForm.GetTheGrid().Count; i++)
            {
                MainForm.GetTheGrid()[i].FillGreen(EG, Ezoom);
            }
            //Esheet.Invoke(new DelBitmap((b) => Esheet.Image = b), EG.GetBitmap());
            restoredEpic.DrawEpicenter(EG, Ezoom);

            EDrawGrid();
        }
        private void metroButton1_Click(object sender, EventArgs e)
        {
            RecReateFunction();

        }
        public Image EsheetPic()
        {
            return Esheet.Image;
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            EDrawMAinEpics();
        }

        private void DisplayEpicenters_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormOpen = false;
            Main.SavePictures = false;
            Main.extendedSavePictures = false;
        }

        private void metroButton2_Click_1(object sender, EventArgs e)
        {
            List<string>  ssss = new List<string>();
            //ssss.Add("up");
            ssss.Add("right");
        //     ssss.Add("down");
            MainForm.GetEpicenters().First().EpicMoving(ssss);
            //MainForm.GetEpicenters().First().EpicMoving(ssss);
            //MainForm.GetEpicenters().First().EpicMoving(ssss);
            //MainForm.GetEpicenters().First().ExpandEpic(ssss);
            EDrawEpics(MainForm.GetEpicenters());
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
        
     
            MainForm.GetEpicenters().First().ExpandEpic();
            EDrawEpics(MainForm.GetEpicenters());
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            MainForm.CreateOneRandomEpicenter(Main.EpicSizeParam,null);
        }
    }
}
