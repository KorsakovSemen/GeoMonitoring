using MetroFramework.Controls;
using MetroFramework.Forms;
using SystAnalys_lr1.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystAnalys_lr1
{
    public partial class DisplayEpicenters : MetroForm
    {
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
        private Panel MapPanel = new Panel();
 

        private void DisplayEpicenters_Load(object sender, EventArgs e)
        {
            panel1.Size = MainForm.GetMainPanel().Size;
            panel1.Left = MainForm.GetMainPanel().Left;
            panel1.Top = MainForm.GetMainPanel().Top;

            ERefreshRouts();
            this.ERouts.SelectedIndexChanged += ERouts_SelectedIndexChanged;
            // Esheet.Dock = DockStyle.Right;
            ////
            this.MapPanel.Location = MainForm.GetMapPanel().Location;
            this.MapPanel.MaximumSize = MainForm.GetMapPanel().MaximumSize;
            this.MapPanel.Size = MainForm.GetMapPanel().Size;
            this.MapPanel.AutoScroll = MainForm.GetMapPanel().AutoScroll;
            this.MapPanel.BorderStyle = MainForm.GetMapPanel().BorderStyle;
            this.MapPanel.AutoSizeMode = MainForm.GetMapPanel().AutoSizeMode;
            this.MapPanel.Dock = DockStyle.Fill;

            //this.MapPanel.Dock = MainForm.GetMapPanel().Dock;
            ////
            this.Width = MainForm.Width;
            this.Height = MainForm.Height;
            ////
            Esheet.Dock = MainForm.GetSheet().Dock;
            Esheet.Width = MainForm.GetSheet().Width;
            Esheet.Height = MainForm.GetSheet().Height;
            Esheet.Image = Main.globalMap;
            EsheetPicture = Main.globalMap;
            wsheet = Esheet.Width;
            hsheet = Esheet.Height;
            Esheet.SizeMode = PictureBoxSizeMode.AutoSize;
            EG = new DrawGraph();
            EG.setBitmap2(Esheet.Image);
            ///
            //EZoomBar = MainForm.GetTrackBar();
            EZoomBar.StyleManager = MainForm.GetTrackBar().StyleManager;
            EZoomBar.Size = MainForm.GetTrackBar().Size;
            EZoomBar.LargeChange = MainForm.GetTrackBar().LargeChange;
            EZoomBar.Value = MainForm.GetTrackBar().Value;
            EZoomBar.Maximum = MainForm.GetTrackBar().Maximum;
            EZoomBar.Minimum = MainForm.GetTrackBar().Minimum;
            EZoomBar.Left = MainForm.GetTrackBar().Left ;  
            EZoomBar.Top = MainForm.GetTrackBar().Top  ;
            EZoomBar.Scroll += EZoomBar_Scroll;
            //
            this.panel1.Controls.Add(ERouts);
            this.panel1.Controls.Add(EZoomBar);
            this.Controls.Add(MapPanel);
            
            this.MapPanel.Controls.Add(Esheet);
            //EDrawGrid();
            EDrawEpics();
            Esheet.MouseClick += Esheet_MouseClick;
        }

        private void Esheet_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (var gridPart in MainForm.GetTheGrid())
            {
                if (((e.X > gridPart.x * Ezoom) && (e.Y > gridPart.y * Ezoom)) && ((e.X < gridPart.x * Ezoom + GridPart.width * Ezoom) && (e.Y < gridPart.y * Ezoom + GridPart.height * Ezoom)))
                {

                    ERefreshRouts();
                    MainForm.CreateOneRandomEpicenter(Main.EpicSizeParam, MainForm.GetTheGrid().IndexOf(gridPart));
                    //Bus.setEpicenters(MainForm.GetEpicenters());
                    EG.clearSheet2();
                    //Esheet.Image = EG.GetBitmap();
                    //EDrawGrid();
                    //EDrawEpics();
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

            EG.clearSheet2();
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
            ERouts.SelectedIndex = 0;
        }
        delegate void DelBitmap(Bitmap b);
        public void EDrawEpics()
        {

            EG.clearSheet2();
            Esheet.Invoke(new DelBitmap((b) => Esheet.Image = b), EG.GetBitmap()); //ошибка при закрытии формы
            // Esheet.Image = EG.GetBitmap();
            for (int i = 0; i < MainForm.GetEpicenters().Count; i++)
            {
                MainForm.GetEpicenters()[i].DrawEpicenter(EG, Ezoom);
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

        private void button1_Click(object sender, EventArgs e)
        {

            ERefreshRouts();
            MainForm.CreateOneRandomEpicenter(Main.EpicSizeParam, null);
            //Bus.setEpicenters(MainForm.GetEpicenters());
            EG.clearSheet2();
            //Esheet.Image = EG.GetBitmap();
            //EDrawGrid();
            //EDrawEpics();
            EDrawPollutions();
        }
        private void EDrawAllPollutionsInRoutes()
        {
            for (int i = 0; i < MainForm.GetPollutionInRoutes().Count; i++)
            {
                for (int j = 0; j < MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key].Count; j++)
                {
                    switch (MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key][j].status)
                    {
                        case 0:
                            EG.gr.FillRectangle(new SolidBrush(Color.FromArgb(170, 0, 128, 0)), new Rectangle(MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key][j].x * Ezoom, MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key][j].y * Ezoom, GridPart.width * Ezoom, GridPart.height * Ezoom));
                            break;

                        case 1:
                            EG.gr.FillRectangle(new SolidBrush(Color.FromArgb(170, 255, 255, 0)), new Rectangle(MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key][j].x * Ezoom, MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key][j].y * Ezoom, GridPart.width * Ezoom, GridPart.height * Ezoom));
                            break;


                        case 2:
                            EG.gr.FillRectangle(new SolidBrush(Color.FromArgb(170, 255, 128, 0)), new Rectangle(MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key][j].x * Ezoom, MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key][j].y * Ezoom, GridPart.width * Ezoom, GridPart.height * Ezoom));

                            break;
                        case 3:
                            EG.gr.FillRectangle(new SolidBrush(Color.FromArgb(170, 255, 0, 0)), new Rectangle(MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key][j].x * Ezoom, MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key][j].y * Ezoom, GridPart.width * Ezoom, GridPart.height * Ezoom));
                            break;
                        default:
                            //EG.gr.FillRectangle(new SolidBrush(Color.FromArgb(170, 220, 220, 220)), new Rectangle(MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key][j].x, MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key][j].y, GridPart.width, GridPart.height));
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
            EG.clearSheet2();
            EDrawEpics();
            if (ERouts.Text == "None")
            {

                return;
            };
            if (ERouts.Text == "All")
            {

                EDrawAllPollutionsInRoutes();
                return;
            };
            for (int i = 0; i < MainForm.GetPollutionInRoutes().Count; i++)
            {
                if (MainForm.GetPollutionInRoutes().ElementAt(i).Key == int.Parse(ERouts.Text))
                {

                    for (int j = 0; j < MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key].Count; j++)
                    {
                        switch (MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key][j].status)
                        {
                            case 0:
                                EG.gr.FillRectangle(new SolidBrush(Color.FromArgb(170, 0, 128, 0)), new Rectangle(MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key][j].x * Ezoom, MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key][j].y * Ezoom, GridPart.width * Ezoom, GridPart.height * Ezoom));
                                break;

                            case 1:
                                EG.gr.FillRectangle(new SolidBrush(Color.FromArgb(170, 255, 255, 0)), new Rectangle(MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key][j].x * Ezoom, MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key][j].y * Ezoom, GridPart.width * Ezoom, GridPart.height * Ezoom));
                                break;


                            case 2:
                                EG.gr.FillRectangle(new SolidBrush(Color.FromArgb(170, 255, 128, 0)), new Rectangle(MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key][j].x * Ezoom, MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key][j].y * Ezoom, GridPart.width * Ezoom, GridPart.height * Ezoom));

                                break;
                            case 3:
                                EG.gr.FillRectangle(new SolidBrush(Color.FromArgb(170, 255, 0, 0)), new Rectangle(MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key][j].x * Ezoom, MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key][j].y * Ezoom, GridPart.width * Ezoom, GridPart.height * Ezoom));
                                break;
                            default:
                                //EG.gr.FillRectangle(new SolidBrush(Color.FromArgb(170, 220, 220, 220)), new Rectangle(MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key][j].x, MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key][j].y, GridPart.width, GridPart.height));
                                break;
                        }
                    }
                    Esheet.Image = EG.GetBitmap();
                };
            }
        }
        int ti = 1;
        int tj = 0;
        private void RISUY()
        {
            EG.clearSheet2();
            EDrawEpics();
            foreach (var item2 in MainForm.getGifList()[ti])
            {
                switch (item2.status)
                {
                    case 0:
                        EG.gr.FillRectangle(new SolidBrush(Color.FromArgb(170, 0, 128, 0)), new Rectangle(item2.x * Ezoom, item2.y * Ezoom, GridPart.width * Ezoom, GridPart.height * Ezoom));
                        break;
                    case 1:
                        EG.gr.FillRectangle(new SolidBrush(Color.FromArgb(170, 255, 255, 0)), new Rectangle(item2.x * Ezoom, item2.y * Ezoom, GridPart.width * Ezoom, GridPart.height * Ezoom));
                        break;

                    case 2:
                        EG.gr.FillRectangle(new SolidBrush(Color.FromArgb(170, 255, 128, 0)), new Rectangle(item2.x * Ezoom, item2.y * Ezoom, GridPart.width * Ezoom, GridPart.height * Ezoom));

                        break;
                    case 3:
                        EG.gr.FillRectangle(new SolidBrush(Color.FromArgb(170, 255, 0, 0)), new Rectangle(item2.x * Ezoom, item2.y * Ezoom, GridPart.width * Ezoom, GridPart.height * Ezoom));
                        break;
                    default:
                        //EG.gr.FillRectangle(new SolidBrush(Color.FromArgb(170, 220, 220, 220)), new Rectangle(MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key][j].x, MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key][j].y, GridPart.width, GridPart.height));
                        break;
                }

            }
            Esheet.Image = EG.GetBitmap();
            if (ti < MainForm.getGifList().Count)
            {
                ti++;
            }
            else
            {
                ti = 1;
            }
            Console.WriteLine(ti.ToString());

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (MainForm.getGifList()[ti][tj].status)
            {
                case 0:
                    EG.gr.FillRectangle(new SolidBrush(Color.FromArgb(170, 0, 128, 0)), new Rectangle(MainForm.getGifList()[ti][tj].x * Ezoom, MainForm.getGifList()[ti][tj].y * Ezoom, GridPart.width * Ezoom, GridPart.height * Ezoom));
                    break;

                case 1:
                    EG.gr.FillRectangle(new SolidBrush(Color.FromArgb(170, 255, 255, 0)), new Rectangle(MainForm.getGifList()[ti][tj].x * Ezoom, MainForm.getGifList()[ti][tj].y * Ezoom, GridPart.width * Ezoom, GridPart.height * Ezoom));
                    break;


                case 2:
                    EG.gr.FillRectangle(new SolidBrush(Color.FromArgb(170, 255, 128, 0)), new Rectangle(MainForm.getGifList()[ti][tj].x * Ezoom, MainForm.getGifList()[ti][tj].y * Ezoom, GridPart.width * Ezoom, GridPart.height * Ezoom));

                    break;
                case 3:
                    EG.gr.FillRectangle(new SolidBrush(Color.FromArgb(170, 255, 0, 0)), new Rectangle(MainForm.getGifList()[ti][tj].x * Ezoom, MainForm.getGifList()[ti][tj].y * Ezoom, GridPart.width * Ezoom, GridPart.height * Ezoom));
                    break;
                default:
                    //EG.gr.FillRectangle(new SolidBrush(Color.FromArgb(170, 220, 220, 220)), new Rectangle(MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key][j].x, MainForm.GetPollutionInRoutes()[MainForm.GetPollutionInRoutes().ElementAt(i).Key][j].y, GridPart.width, GridPart.height));
                    break;
            }
            Esheet.Image = EG.GetBitmap();

            if (tj + 1 >= MainForm.getGifList()[1].Count)
            {
                tj = 0;
                if (ti + 1 >= MainForm.getGifList().Count)
                {
                    ti = 1;
                    EG.clearSheet2();
                }
                else
                {
                    ti++;
                    EG.clearSheet2();
                }
            }
            else
            {
                tj++;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //timer1.Interval = 25;
            //timer1.Start();
            RISUY();
        }



        private void button3_Click_1(object sender, EventArgs e)
        {
            MainForm.GetEpicenters().First().ExpandEpic(Main.ExpandEpicParamet);
            EDrawEpics();
        }
        public EpicSettings epSet;
        private void button11_Click(object sender, EventArgs e)
        {

            epSet = new EpicSettings();
            this.StyleManager.Clone(epSet);
            epSet.ShowDialog();
            //G.clearSheet();
            //sheet.Image = G.GetBitmap();
            //DrawGrid();

            //Ep.EDrawGrid();

        }
        public void RecReateFunction()
        {
            restoredEpic = new Epicenter(MainForm.GetTheGrid());

            restoredEpic.Recreate(MainForm.GetPollutionInRoutes());


            EG.clearSheet2();
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
            EDrawEpics();
        }
    }
}
