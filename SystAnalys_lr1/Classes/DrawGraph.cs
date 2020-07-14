//This is a personal academic project. Dear PVS-Studio, please check it.
//PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystAnalys_lr1.Classes
{
    public class DrawGraph
    {
        public Bitmap Bitmap { get => _bitmap; set => _bitmap = value; }
        public Graphics Gr { get => _gr; set => _gr = value; }
        public int R { get => _r; set => _r = value; }

        private readonly Pen blackPen;
        private readonly Pen darkGoldPen;
        private readonly Color color;
        private Bitmap _bitmap;
        private Graphics _gr;
        private int _r = 3;

        public DrawGraph()
        {
            blackPen = new Pen(Color.Black)
            {
                Width = 1
            };
            darkGoldPen = new Pen(Color.IndianRed);
            _ = new Random();
            color = Color.DarkBlue;
        }

        public void SetBitmap()
        {
            Bitmap = new Bitmap(Main.GlobalMap);
            Gr = Graphics.FromImage(Bitmap);
        }

        public Bitmap GetBitmap()
        {
            return Bitmap;
        }

        public void ClearSheet()
        {
            Graphics.FromImage(Bitmap).Clear(Color.White);
            Bitmap = new Bitmap(Main.GlobalMap);
            Gr.Dispose();
            Gr = Graphics.FromImage(Bitmap);

        }

        public void ClearSheet2()
        {
            if (!Main.Ep.IsDisposed)
            {
                Bitmap = new Bitmap(DisplayEpicenters.ZoomPicture);
            }

            Gr = null;
            Gr = Graphics.FromImage(Bitmap);

        }

        public void DrawStation(int x, int y, int r, Brush b)
        {
            Gr.FillEllipse(Brushes.Red, (x - r), (y - r), 4 * r, 4 * r);
            Gr.FillEllipse(b, (x - 30 * r), (y - 30 * r), 60 * r, 60 * r);
        }

        public void DrawVertex(int x, int y)
        {
            Gr.FillEllipse(Brushes.GreenYellow, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
            Gr.DrawEllipse(blackPen, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
        }

        public void DrawSelectedVertex(int x, int y)
        {
            Gr.FillEllipse(Brushes.Red, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
            Gr.DrawEllipse(blackPen, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
        }

        public void DrawSelectedStopVertex(int x, int y)
        {
            Gr.FillEllipse(Brushes.HotPink, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
            Gr.DrawEllipse(blackPen, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
        }

        public void DrawRouteVertex(int x, int y)
        {
            Gr.FillEllipse(Brushes.AliceBlue, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
            Gr.DrawEllipse(blackPen, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
        }
        public void DrawYellowVertex(int x, int y)
        {
            Gr.FillEllipse(Brushes.Yellow, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
            Gr.DrawEllipse(blackPen, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
        }
        public void DrawGreenVertex(int x, int y)
        {
            Gr.FillEllipse(Brushes.ForestGreen, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
            Gr.DrawEllipse(blackPen, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
        }
        public void DrawStopVertex(int x, int y)
        {
            Gr.FillEllipse(Brushes.Orange, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
            Gr.DrawEllipse(blackPen, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
        }

        public void DrawStopRouteVertex(int x, int y)
        {
            Gr.FillEllipse(Brushes.SkyBlue, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
            Gr.DrawEllipse(blackPen, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
        }

        public void DrawEdge(Vertex V1, Vertex V2, Edge E, int rand = 0)
        {
            Pen pen;
            if (rand != 0)
            {
                pen = new Pen(color)
                {
                    Width = 2 * Main.zoom
                };
            }
            else
            {
                pen = darkGoldPen;
                pen.Width = 2 * Main.zoom;
            };

            if (E.V1 == E.V2)
            {
                Gr.DrawArc(pen, (V1.X - 2 * R) * Main.zoom, (V1.Y - 2 * R) * Main.zoom, 2 * R * Main.zoom, 2 * R * Main.zoom, 90, 270);
            }
            else
            {
                Gr.DrawLine(darkGoldPen, V1.X * Main.zoom, V1.Y * Main.zoom, V2.X * Main.zoom, V2.Y * Main.zoom);
                DrawVertex(V1.X * Main.zoom, V1.Y * Main.zoom);
                DrawVertex(V2.X * Main.zoom, V2.Y * Main.zoom);
            }
        }

        public void DrawALLGraph(List<Vertex> V, List<Edge> E, int rand = 0)
        {
            Pen pen;
            if (rand != 0)
            {
                pen = new Pen(color)
                {
                    Width = 2 * Main.zoom
                };
                
            }
            else
            {
                pen = darkGoldPen;
                pen.Width = 2 * Main.zoom;
                foreach (var s in Data.Staions)
                {
                    DrawStation(s.X * Main.zoom, s.Y * Main.zoom, 2 * Main.zoom, new SolidBrush(Color.FromArgb(128, 178, 34, 34)));
                }
            };
            for (int i = 0; i < E.Count; i++)
            {
                if (E[i].V1 == E[i].V2)
                {
                    Gr.DrawArc(pen, (V[E[i].V1].X - 2 * R) * Main.zoom, (V[E[i].V1].Y - 2 * R) * Main.zoom, 2 * R * Main.zoom, 2 * R * Main.zoom, 90, 270);
                }
                else
                {
                    if (E[i].V1 < V.Count && E[i].V2 < V.Count)
                    {
                        Gr.DrawLine(pen, V[E[i].V1].X * Main.zoom, V[E[i].V1].Y * Main.zoom, V[E[i].V2].X * Main.zoom, V[E[i].V2].Y * Main.zoom);
                    }
                }
            }
          
            DrawStopPoints();
            for (int i = 0; i < V.Count; i++)
            {
                if (rand != 0)
                    DrawRouteVertex(V[i].X, V[i].Y);
                else
                    DrawVertex(V[i].X, V[i].Y);
            }

            DrawTrafficLights();

        }

        public void DrawTrafficLights()
        {
            foreach (var tl in Data.TraficLights)
            {
                if (tl.Status == LightStatus.GREEN)
                {
                    Main.G.DrawGreenVertex(tl.X, tl.Y);
                }
                else if (tl.Status == LightStatus.YELLOW)
                {
                    Main.G.DrawYellowVertex(tl.X, tl.Y);
                }
                else if (tl.Status == LightStatus.RED)
                {
                    Main.G.DrawSelectedVertex(tl.X, tl.Y);
                }
            }
        }

        public void DrawStopPoints()
        {
            foreach (var stopPoints in Data.AllstopPoints)
            {
                DrawStopVertex(stopPoints.X, stopPoints.Y);
            }
            if (Main.SelectedRoute != null)
            {
                if (Data.StopPoints.ContainsKey(Main.SelectedRoute))
                {
                    foreach (var stopPoints in Data.StopPoints[Main.SelectedRoute])
                    {
                        DrawStopRouteVertex(stopPoints.X, stopPoints.Y);
                    }
                }
            }
        }

    }

}
