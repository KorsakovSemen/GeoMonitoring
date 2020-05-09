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
        public Bitmap bitmap;
        readonly private Pen blackPen;
        readonly private Pen darkGoldPen;
        public Graphics gr;
        readonly private Color color;
        public int R = 3; //радиус окружности вершины

        public DrawGraph()
        {

            blackPen = new Pen(Color.Black)
            {
                Width = 1
            };
            darkGoldPen = new Pen(Color.MediumAquamarine)
            {
                Width = 1
            };
            _ = new Random();
            color = Color.ForestGreen;//Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
        }

        public void SetBitmap()
        {
            //эксепшн при загрузке левой директоории
            bitmap = new Bitmap(Main.GlobalMap);
            gr = Graphics.FromImage(bitmap);
        }
        // для второй формы быстрофикс
        public void SetBitmap2()
        {
            bitmap = new Bitmap(Main.GlobalMap);
            gr = Graphics.FromImage(bitmap);
        }
        //
        public Bitmap GetBitmap()
        {
            return bitmap;
        }

        public void ClearSheet()
        {
            Graphics.FromImage(bitmap).Clear(Color.White);
            bitmap = new Bitmap(Main.GlobalMap);
            gr.Dispose();
            gr = Graphics.FromImage(bitmap);

        }
        //для второй формы
        public void ClearSheet2()
        {
            /*Graphics.FromImage(bitmap).Clear(Color.Wheat); *//// ТУТ ЭКСЕПШН НА МОДЕЛИНГЕ   
            if (!Main.Ep.IsDisposed) {
                bitmap = new Bitmap(DisplayEpicenters.EsheetPicture);
            }
       
            gr = null;
            //  gr.Dispose();
            gr = Graphics.FromImage(bitmap);

        }

        public void DrawVertex(int x, int y)
        {
            gr.FillEllipse(Brushes.GreenYellow, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
            gr.DrawEllipse(blackPen, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
        }

        public void DrawSelectedVertex(int x, int y)
        {
            gr.FillEllipse(Brushes.Red, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
            gr.DrawEllipse(blackPen, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
        }

        public void DrawSelectedStopVertex(int x, int y)
        {
            gr.FillEllipse(Brushes.HotPink, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
            gr.DrawEllipse(blackPen, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
        }

        public void DrawRouteVertex(int x, int y)
        {
            gr.FillEllipse(Brushes.AliceBlue, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
            gr.DrawEllipse(blackPen, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
        }
        public void DrawYellowVertex(int x, int y)
        {
            gr.FillEllipse(Brushes.Yellow, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
            gr.DrawEllipse(blackPen, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
        }
        public void DrawGreenVertex(int x, int y)
        {
            gr.FillEllipse(Brushes.ForestGreen, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
            gr.DrawEllipse(blackPen, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
        }
        public void DrawStopVertex(int x, int y)
        {
            gr.FillEllipse(Brushes.Orange, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
            gr.DrawEllipse(blackPen, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
        }

        public void DrawStopRouteVertex(int x, int y)
        {
            gr.FillEllipse(Brushes.SkyBlue, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
            gr.DrawEllipse(blackPen, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
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
                gr.DrawArc(pen, (V1.X - 2 * R) * Main.zoom, (V1.Y - 2 * R) * Main.zoom, 2 * R * Main.zoom, 2 * R * Main.zoom, 90, 270);
            }
            else
            {
                gr.DrawLine(darkGoldPen, V1.X * Main.zoom, V1.Y * Main.zoom, V2.X * Main.zoom, V2.Y * Main.zoom);
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
            };
            //рисуем ребра
            for (int i = 0; i < E.Count; i++)
            {
                if (E[i].V1 == E[i].V2)
                {
                    gr.DrawArc(pen, (V[E[i].V1].X - 2 * R) * Main.zoom, (V[E[i].V1].Y - 2 * R) * Main.zoom, 2 * R * Main.zoom, 2 * R * Main.zoom, 90, 270);
                }
                else
                {
                    //проблема с отрисовкой
                    if (E[i].V1 < V.Count && E[i].V2 < V.Count)
                    {
                        gr.DrawLine(pen, V[E[i].V1].X * Main.zoom, V[E[i].V1].Y * Main.zoom, V[E[i].V2].X * Main.zoom, V[E[i].V2].Y * Main.zoom);
                    }
                }
            }
            DrawStopPoints();
            //рисуем вершины
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
                if (tl.Status == Status.GREEN)
                {
                    Main.G.DrawGreenVertex(tl.X, tl.Y);
                }
                else if (tl.Status == Status.YELLOW)
                {
                    Main.G.DrawYellowVertex(tl.X, tl.Y);
                }
                else if (tl.Status == Status.RED)
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
