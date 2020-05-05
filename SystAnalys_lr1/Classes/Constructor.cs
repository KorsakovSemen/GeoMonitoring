using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystAnalys_lr1.Classes
{
    public class Constructor
    {
        delegate void Del(Bitmap bmp);

        public async void AsSelect(MouseEventArgs e, List<Vertex> V, List<Edge> E, PictureBox sheet, int n = 0)
        {
            await Task.Run(() => Select(e, V, E, sheet, n));
          
        }

        public async void AsDrawEdge(MouseEventArgs e, List<Vertex> V, List<Edge> E, PictureBox sheet, int n)
        {
            await Task.Run(() => DrawEdge(e, V, E, sheet, n));
        }


        public async void AsDelete(MouseEventArgs e, List<Vertex> V, List<Edge> E, PictureBox sheet, SerializableDictionary<string, List<Edge>> routesEdgeE)
        {
            await Task.Run(() => delete(e, V, E, sheet, routesEdgeE));
            if (Main.flag)
            {
                Main.G.ClearSheet();
                Main.G.DrawALLGraph(V, E);
                sheet.Invoke(new Del((s) => sheet.Image = s), Main.G.GetBitmap());
                Main.DrawGrid();
            }
        }

        public async void AsDeleteRoute(MouseEventArgs e, List<Vertex> routeV, List<Edge> routesEdge, PictureBox sheet)
        {
            await Task.Run(() => deleteRoute(e, routeV, routesEdge, sheet));

        }

        public void AddBus(MouseEventArgs e, bool trackerCheck, bool backsideCheck, string route)
        {
            if (Main.AllCoordinates[route].Count != 0)
            {
                int pos = 0;

                if (e.Button == MouseButtons.Left)
                {
                    double min = Math.Pow((Main.AllCoordinates[route].Last().X - e.X / Main.zoom), 2) + Math.Pow((Main.AllCoordinates[route].Last().Y - e.Y / Main.zoom), 2);
                    for (int i = 0; i < Main.AllCoordinates[route].Count; i++)
                    {
                        if (Math.Pow((Main.AllCoordinates[route][i].X - e.X / Main.zoom), 2) + Math.Pow((Main.AllCoordinates[route][i].Y - e.Y / Main.zoom), 2) <= Main.G.R * Main.G.R * 500)
                        {
                            if ((Math.Pow((Main.AllCoordinates[route][i].X - e.X / Main.zoom), 2) + Math.Pow((Main.AllCoordinates[route][i].Y - e.Y / Main.zoom), 2) < min))
                            {
                                min = Math.Pow((Main.AllCoordinates[route][i].X - e.X / Main.zoom), 2) + Math.Pow((Main.AllCoordinates[route][i].Y - e.Y / Main.zoom), 2);
                                pos = i;
                            }
                        }
                    }
                }

                if (trackerCheck)
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
                                route,
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
                    Main.buses.Add(new Bus(original, pos, backsideCheck, route, Main.AllCoordinates[route], true));
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
                                route,
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
                    Main.buses.Add(new Bus(original, pos, backsideCheck, route, Main.AllCoordinates[route], false));
                };
            }
        }

        public void AddStopPointsInRoutes(MouseEventArgs e, List<Vertex> allstopPoints, PictureBox sheet, List<GridPart> gridParts, string route)
        {
            foreach (var sp in allstopPoints)
            {
                if (Math.Pow((sp.X - e.X / Main.zoom), 2) + Math.Pow((sp.Y - e.Y / Main.zoom), 2) <= Main.G.R * Main.G.R)
                {
                    foreach (var gridPart in gridParts)
                    {
                        if (((e.X > gridPart.x * Main.zoom) && (e.Y > gridPart.y * Main.zoom)) && ((e.X < gridPart.x * Main.zoom + GridPart.Width * Main.zoom) && (e.Y < gridPart.y * Main.zoom + GridPart.Height * Main.zoom)))
                        {

                            if (!Main.stopPoints[route].Contains(new Vertex(sp.X, sp.Y)))
                            {
                                if (Main.stopPoints.ContainsKey(route))
                                {
                                    Main.stopPoints[route].Add(new Vertex(sp.X, sp.Y));
                                    Main.stopPoints[route].Last().gridNum = gridParts.IndexOf(gridPart);
                                    if (Main.stopPointsInGrids.ContainsKey(route))
                                        Main.stopPointsInGrids[route].Add(gridParts.IndexOf(gridPart)); //дроп ошибки
                                    else
                                    {
                                        Main.stopPointsInGrids.Add(route, new List<int>());
                                        Main.stopPointsInGrids[route].Add(gridParts.IndexOf(gridPart));
                                    }

                                }
                                else
                                {
                                    Main.stopPoints.Add(route, new List<Vertex>());
                                    Main.stopPointsInGrids.Add(route, new List<int>());
                                    if (!Main.stopPoints[route].Contains(new Vertex(sp.X, sp.Y)))
                                    {
                                        if (Main.stopPoints.ContainsKey(route) && Main.stopPointsInGrids.ContainsKey(route))
                                        {
                                            Main.stopPoints[route].Add(new Vertex(sp.X, sp.Y));
                                            Main.stopPoints[route].Last().gridNum = gridParts.IndexOf(gridPart);
                                            Main.stopPointsInGrids[route].Add(gridParts.IndexOf(gridPart)); //дроп ошибки
                                        }
                                    }
                                }
                            }

                            Main.G.DrawStopRouteVertex(sp.X, sp.Y);
                            sheet.Image = Main.G.GetBitmap();
                            Main.DrawGrid();

                            break;
                        }
                    }

                }
            }
        }

        public void AddStopPoints(MouseEventArgs e, List<Vertex> allstopPoints, PictureBox sheet, List<GridPart> gridParts)
        {
            foreach (var gridPart in gridParts)
            {
                if (((e.X > gridPart.x * Main.zoom) && (e.Y > gridPart.y * Main.zoom)) && ((e.X < gridPart.x * Main.zoom + GridPart.Width * Main.zoom) && (e.Y < gridPart.y * Main.zoom + GridPart.Height * Main.zoom)))
                {
                    allstopPoints.Add(new Vertex(e.X / Main.zoom, e.Y / Main.zoom));
                    Main.G.DrawStopVertex(e.X / Main.zoom, e.Y / Main.zoom);
                    sheet.Image = Main.G.GetBitmap();
                    Main.DrawGrid();
                    break;
                }
            }
        }
        public void SelectRoute(MouseEventArgs e, List<Vertex> V, List<Edge> E, PictureBox sheet, Constructor c, List<int> selected, bool check)
        {
            if (!check)
            {
                c.DrawVertex(e, V, sheet);
            }

            for (int i = 0; i < V.Count; i++)
            {
                if (Math.Pow((V[i].X - e.X / Main.zoom), 2) + Math.Pow((V[i].Y - e.Y / Main.zoom), 2) <= Main.G.R * Main.G.R)
                {
                    if (selected.Count == 0)
                    {
                        selected.Add(i);
                        Main.G.DrawSelectedVertex(V[i].X, V[i].Y);
                        sheet.Image = Main.G.GetBitmap();
                        break;
                    }
                    else
                    {
                        selected.Add(i);
                        E.Add(new Edge(selected[0], selected[1]));
                        Main.G.DrawEdge(V[selected[0]], V[selected[1]], E[E.Count - 1], 1);
                        selected[0] = selected[1];
                        selected.Remove(selected[1]);
                        Main.G.ClearSheet();
                        Main.G.DrawALLGraph(V, E);
                        sheet.Image = Main.G.GetBitmap();
                        Main.DrawGrid();
                        Main.G.DrawSelectedVertex(V[i].X, V[i].Y);
                        break;
                    }

                }
            }
        }


        public void SelectRouteInRoute(MouseEventArgs e, List<Vertex> routeV, List<Edge> routesEdge, PictureBox sheet, List<int> selected)
        {
            bool select = true;
            for (int i = 0; i < Main.V.Count; i++)
            {
                if (Math.Pow((Main.V[i].X - e.X / Main.zoom), 2) + Math.Pow((Main.V[i].Y - e.Y / Main.zoom), 2) <= Main.G.R * Main.G.R)
                {
                    if (selected.Count == 0)
                    {
                        if (routesEdge.Count != 0)
                        {
                            if (routeV[routesEdge.Last().V2].X == Main.V[i].X && routeV[routesEdge.Last().V2].Y == Main.V[i].Y)
                            {
                                selected.Add(i);
                                Main.G.DrawSelectedVertex(Main.V[i].X, Main.V[i].Y);
                            }
                        }
                        else
                        {
                            if (!routeV.Contains(new Vertex(Main.V[i].X, Main.V[i].Y)))
                            {
                                routeV.Add(new Vertex(Main.V[i].X, Main.V[i].Y));
                            }
                            selected.Add(i);
                            Main.G.DrawSelectedVertex(Main.V[i].X, Main.V[i].Y);
                        }
                        break;
                    }
                    else
                    {
                        selected.Add(i);
                        foreach (var ed in Main.E)
                        {
                            if ((ed.V1 == selected[0] && ed.V2 == selected[1]) || (ed.V2 == selected[0] && ed.V1 == selected[1]))
                            {
                                routesEdge.Add(new Edge(routeV.Count - 1, routeV.Count));
                                routeV.Add(new Vertex(Main.V[i].X, Main.V[i].Y));
                                Main.G.ClearSheet();
                                Main.G.DrawALLGraph(Main.V, Main.E);
                                Main.G.DrawALLGraph(routeV, routesEdge, 1);
                                sheet.Image = Main.G.GetBitmap();
                                Main.DrawGrid();
                                select = true;
                                Main.G.DrawSelectedVertex(Main.V[i].X, Main.V[i].Y);
                                break;
                            }
                            select = false;
                        }
                        if (!select)
                        {
                            Main.G.ClearSheet();
                            Main.G.DrawALLGraph(Main.V, Main.E);
                            Main.G.DrawALLGraph(routeV, routesEdge, 1);
                            sheet.Image = Main.G.GetBitmap();
                            Main.DrawGrid();
                        }
                        if (routeV.Contains(new Vertex(Main.V[i].X, Main.V[i].Y)))
                            Main.G.DrawSelectedVertex(Main.V[i].X, Main.V[i].Y);

                    }
                    selected[0] = selected[1];
                    selected.Remove(selected[1]);

                }
            }

        }

        public void DrawEdgeInRoute(MouseEventArgs e, List<Vertex> routeV, List<Edge> routesEdge, PictureBox sheet, string route)
        {
            if (e.Button == MouseButtons.Left)
            {
                for (int i = 0; i < Main.V.Count; i++)
                {
                    if (Math.Pow((Main.V[i].X - e.X / Main.zoom), 2) + Math.Pow((Main.V[i].Y - e.Y / Main.zoom), 2) <= Main.G.R * Main.G.R)
                    {
                        if (Main.selected1 == -1)
                        {
                            if (routesEdge.Count != 0)
                            {
                                if (routeV[routesEdge.Last().V2].X == Main.V[i].X && routeV[routesEdge.Last().V2].Y == Main.V[i].Y)
                                {
                                    Main.selected1 = i;
                                    Main.G.DrawSelectedVertex(Main.V[i].X, Main.V[i].Y);
                                }
                            }
                            else
                            {
                                if (!routeV.Contains(new Vertex(Main.V[i].X, Main.V[i].Y)))
                                {
                                    routeV.Add(new Vertex(Main.V[i].X, Main.V[i].Y));
                                }
                                Main.selected1 = i;

                                Main.G.DrawSelectedVertex(Main.V[i].X, Main.V[i].Y);
                            }
                            break;
                        }
                        if (Main.selected2 == -1)
                        {
                            Main.G.DrawSelectedVertex(Main.V[i].X, Main.V[i].Y);
                            Main.selected2 = i;
                            foreach (var ed in Main.E)
                            {
                                if ((ed.V1 == Main.selected1 && ed.V2 == Main.selected2) || (ed.V2 == Main.selected1 && ed.V1 == Main.selected2))
                                {

                                    routesEdge.Add(new Edge(routeV.Count - 1, routeV.Count));
                                    routeV.Add(new Vertex(Main.V[i].X, Main.V[i].Y));                                  
                                    Main.G.DrawEdge(Main.V[Main.selected1], Main.V[Main.selected2], Main.E[Main.E.Count - 1], 1);
                                    sheet.Image = Main.G.GetBitmap();
                                    Main.selected1 = -1;
                                    Main.selected2 = -1;
                                    break;
                                }
                            }
                            //if (res == 0)
                            //{
                            //    routeV.RemoveAt(routeV.Count - 1);
                            //    //routeV.RemoveAt(routeV.Count - 1);
                            //}
                            Main.G.ClearSheet();
                            Main.G.DrawALLGraph(Main.V, Main.E);
                            Main.G.DrawALLGraph(routeV, routesEdge, 1);
                            Main.selected1 = -1;
                            Main.selected2 = -1;
                            sheet.Image = Main.G.GetBitmap();
                            Main.DrawGrid();
                            break;
                        }
                    }
                }
            }
        }

        public void DeleteBus(MouseEventArgs e, List<Vertex> routeV, List<Edge> routesEdge, PictureBox sheet, string route, int scrollX, int scrollY)
        {
            if (Main.AllCoordinates[route].Count != 0)
            {
                int? pos = null;
                double min = Math.Pow((sheet.Image.Width - (e.X / Main.zoom + scrollX)), 2) + Math.Pow((sheet.Image.Height - (e.Y / Main.zoom + scrollY)), 2);
                for (int i = 0; i < Main.buses.Count; i++)
                {
                    if (Math.Pow((Main.buses[i].Coordinates[Main.buses[i].PositionAt].X - (e.X / Main.zoom + scrollX)), 2) + Math.Pow((Main.buses[i].Coordinates[Main.buses[i].PositionAt].Y - (e.Y / Main.zoom + scrollY)), 2) <= Main.buses[i].R * Main.buses[i].R * 500)
                    {
                        if (Main.buses[i].route == route)
                        {
                            if (Math.Pow((Main.buses[i].Coordinates[Main.buses[i].PositionAt].X - (e.X / Main.zoom + scrollX)), 2) + Math.Pow((Main.buses[i].Coordinates[Main.buses[i].PositionAt].Y - (e.Y / Main.zoom + scrollY)), 2) < min)
                            {
                                min = Math.Pow((Main.buses[i].Coordinates[Main.buses[i].PositionAt].X - (e.X / Main.zoom + scrollX)), 2) + Math.Pow((Main.buses[i].Coordinates[Main.buses[i].PositionAt].Y - (e.Y / Main.zoom + scrollY)), 2);
                                pos = i;
                            }
                        }
                    }
                }
                if (pos != null)
                {
                    Main.buses.Remove(Main.buses[int.Parse(pos.ToString())]);
                }
                Main.G.ClearSheet();
                Main.G.DrawALLGraph(Main.V, Main.E);
                Main.G.DrawALLGraph(routeV, routesEdge, 1);
                sheet.Image = Main.G.GetBitmap();
                Main.DrawGrid();
            }
        }

        public void Select(MouseEventArgs e, List<Vertex> V, List<Edge> E, PictureBox sheet, int n = 0)
        {
            for (int i = 0; i < V.Count; i++)
            {
                if (Math.Pow((V[i].X - e.X / Main.zoom), 2) + Math.Pow((V[i].Y - e.Y / Main.zoom), 2) <= Main.G.R * Main.G.R)
                {
                    if (Main.selected1 != -1)
                    {
                        Main.selected1 = -1;
                        Main.G.ClearSheet();
                        if (n != 0) Main.G.DrawALLGraph(Main.V, Main.E);
                        Main.G.DrawALLGraph(V, E, n);
                        //  sheet.Image = Main.G.GetBitmap();
                        sheet.Invoke(new Del((s) => sheet.Image = s), Main.G.GetBitmap());
                        Main.DrawGrid();
                    }
                    if (Main.selected1 == -1)
                    {
                        Main.G.DrawSelectedVertex(V[i].X, V[i].Y);
                        Main.selected1 = i;
                        sheet.Invoke(new Del((s) => sheet.Image = s), Main.G.GetBitmap());
                        Main.DrawGrid();
                        //createAdjAndOut();
                        //listBoxMatrix.Items.Clear();
                        //int degree = 0;
                        //for (int j = 0; j < routeV.Count; j++)
                        //    degree += AMatrix[selected1, j];
                        //listBoxMatrix.Items.Add("Степень вершины №" + (selected1 + 1) + " равна " + degree);
                        break;
                    }
                }
            }
        }


        public void DrawVertex(MouseEventArgs e, List<Vertex> V, PictureBox sheet)
        {
            V.Add(new Vertex(e.X / Main.zoom, e.Y / Main.zoom));
            Main.G.DrawVertex(e.X / Main.zoom, e.Y / Main.zoom);
            sheet.Image = Main.G.GetBitmap();
            Main.DrawGrid();

        }

        public void DrawEdge(MouseEventArgs e, List<Vertex> V, List<Edge> E, PictureBox sheet, int n)
        {
            if (e.Button == MouseButtons.Left)
            {
                for (int i = 0; i < V.Count; i++)
                {
                    if (Math.Pow((V[i].X - e.X / Main.zoom), 2) + Math.Pow((V[i].Y - e.Y / Main.zoom), 2) <= Main.G.R * Main.G.R)
                    {
                        if (Main.selected1 == -1)
                        {
                            Main.G.DrawSelectedVertex(V[i].X, V[i].Y);
                            Main.selected1 = i;
                            sheet.Invoke(new Del((s) => sheet.Image = s), Main.G.GetBitmap());
                            break;
                        }
                        if (Main.selected2 == -1)
                        {
                            Main.G.DrawSelectedVertex(V[i].X, V[i].Y);
                            Main.selected2 = i;
                            E.Add(new Edge(Main.selected1, Main.selected2));
                            Main.G.DrawEdge(V[Main.selected1], V[Main.selected2], E[E.Count - 1], n);
                            Main.selected1 = -1;
                            Main.selected2 = -1;
                            Main.G.ClearSheet();
                            if (n != 0) Main.G.DrawALLGraph(Main.V, Main.E);
                            Main.G.DrawALLGraph(V, E, n);
                            Main.DrawGrid();
                            sheet.Invoke(new Del((s) => sheet.Image = s), Main.G.GetBitmap());
                            break;
                        }
                    }
                }
            }
        }

        public void deleteTF(MouseEventArgs e, List<Vertex> V, List<Edge> E, PictureBox sheet, SerializableDictionary<string, List<Edge>> routesEdgeE)
        {
            foreach (var tl in Main.traficLights)
            {
                if (Math.Pow((tl.x - e.X / Main.zoom), 2) + Math.Pow((tl.y - e.Y / Main.zoom), 2) <= Main.G.R * Main.G.R)
                {
                    tl.Stop();
                    Main.TraficLightsInGrids.Remove(tl.gridNum);
                    Main.traficLights.Remove(tl);
                    Main.flag = true;
                    break;
                }
            }
        }
        public void deleteVE(MouseEventArgs e, List<Vertex> V, List<Edge> E, PictureBox sheet, SerializableDictionary<string, List<Edge>> routesEdgeE)
        {
            if (!Main.flag)
            {
                foreach (var routeV in Main.routes)
                {
                    for (int i = 0; i < routeV.Value.Count; i++)
                    {
                        if (Math.Pow((routeV.Value[i].X - e.X / Main.zoom), 2) + Math.Pow((routeV.Value[i].Y - e.Y / Main.zoom), 2) <= Main.G.R * Main.G.R)
                        {
                            //foreach(var routesEdge in routesEdgeE.Values)
                            //{
                            for (int j = 0; j < routesEdgeE[routeV.Key].Count; j++)
                            {
                                if ((routesEdgeE[routeV.Key][j].V1 == i) || (routesEdgeE[routeV.Key][j].V2 == i))
                                {
                                    routesEdgeE[routeV.Key].RemoveAt(j);
                                    j--;
                                }
                                else
                                {
                                    if (routesEdgeE[routeV.Key][j].V1 > i) routesEdgeE[routeV.Key][j].V1--;
                                    if (routesEdgeE[routeV.Key][j].V2 > i) routesEdgeE[routeV.Key][j].V2--;
                                }
                            }
                            routeV.Value.RemoveAt(i);
                            Main.flag = true;
                            break;
                            //}                       
                        }

                    }
                }
            }
            //ищем, возможно было нажато ребро
            if (!Main.flag)
            {
                try
                {
                    foreach (var routeV in Main.routes)
                    {
                        for (int j = 0; j < routeV.Value.Count; j++)
                        {
                            for (int i = 0; i < routesEdgeE[routeV.Key].Count; i++)
                            {
                                if (routesEdgeE[routeV.Key][i].V1 == routesEdgeE[routeV.Key][i].V2) //если это петля
                                {
                                    if ((Math.Pow((routeV.Value[routesEdgeE[routeV.Key][i].V1].X - Main.G.R - e.X / Main.zoom), 2) + Math.Pow((routeV.Value[routesEdgeE[routeV.Key][i].V1].Y - Main.G.R - e.Y / Main.zoom), 2) <= ((Main.G.R + 2) * (Main.G.R + 2))) &&
                                        (Math.Pow((routeV.Value[routesEdgeE[routeV.Key][i].V1].X - Main.G.R - e.X / Main.zoom), 2) + Math.Pow((routeV.Value[routesEdgeE[routeV.Key][i].V1].Y - Main.G.R - e.Y / Main.zoom), 2) >= ((Main.G.R - 2) * (Main.G.R - 2))))
                                    {
                                        routesEdgeE[routeV.Key].RemoveAt(i);
                                        Main.flag = true;
                                        break;
                                    }
                                }
                                else //не петля
                                {
                                    if (((e.X / Main.zoom - routeV.Value[routesEdgeE[routeV.Key][i].V1].X) * (routeV.Value[routesEdgeE[routeV.Key][i].V2].Y - routeV.Value[routesEdgeE[routeV.Key][i].V1].Y) / (routeV.Value[routesEdgeE[routeV.Key][i].V2].X - routeV.Value[routesEdgeE[routeV.Key][i].V1].X) + routeV.Value[routesEdgeE[routeV.Key][i].V1].Y) <= (e.Y / Main.zoom + 4) &&
                                        ((e.X / Main.zoom - routeV.Value[routesEdgeE[routeV.Key][i].V1].X) * (routeV.Value[routesEdgeE[routeV.Key][i].V2].Y - routeV.Value[routesEdgeE[routeV.Key][i].V1].Y) / (routeV.Value[routesEdgeE[routeV.Key][i].V2].X - routeV.Value[routesEdgeE[routeV.Key][i].V1].X) + routeV.Value[routesEdgeE[routeV.Key][i].V1].Y) >= (e.Y / Main.zoom - 4))
                                    {
                                        if ((routeV.Value[routesEdgeE[routeV.Key][i].V1].X <= routeV.Value[routesEdgeE[routeV.Key][i].V2].X && routeV.Value[routesEdgeE[routeV.Key][i].V1].X <= e.X / Main.zoom && e.X / Main.zoom <= routeV.Value[routesEdgeE[routeV.Key][i].V2].X) ||
                                            (routeV.Value[routesEdgeE[routeV.Key][i].V1].X >= routeV.Value[routesEdgeE[routeV.Key][i].V2].X && routeV.Value[routesEdgeE[routeV.Key][i].V1].X >= e.X / Main.zoom && e.X / Main.zoom >= routeV.Value[routesEdgeE[routeV.Key][i].V2].X))
                                        {
                                            routesEdgeE[routeV.Key].RemoveAt(i);
                                            Main.flag = true;
                                            break;
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
                catch
                {

                }

            }

            for (int i = 0; i < V.Count; i++)
            {
                if (Math.Pow((V[i].X - e.X / Main.zoom), 2) + Math.Pow((V[i].Y - e.Y / Main.zoom), 2) <= Main.G.R * Main.G.R)
                {
                    for (int j = 0; j < E.Count; j++)
                    {
                        if ((E[j].V1 == i) || (E[j].V2 == i))
                        {

                            E.RemoveAt(j);
                            j--;
                        }
                        else
                        {
                            if (E[j].V1 > i) E[j].V1--;
                            if (E[j].V2 > i) E[j].V2--;
                        }
                    }
                    V.RemoveAt(i);
                    Main.flag = true;
                    break;

                }

            }

            //ищем, возможно было нажато ребро
            if (!Main.flag)
            {
                for (int i = 0; i < E.Count; i++)
                {
                    if (E[i].V1 == E[i].V2) //если это петля
                    {
                        if ((Math.Pow((V[E[i].V1].X - Main.G.R - e.X / Main.zoom), 2) + Math.Pow((V[E[i].V1].Y - Main.G.R - e.Y / Main.zoom), 2) <= ((Main.G.R + 2) * (Main.G.R + 2))) &&
                            (Math.Pow((V[E[i].V1].X - Main.G.R - e.X / Main.zoom), 2) + Math.Pow((V[E[i].V1].Y - Main.G.R - e.Y / Main.zoom), 2) >= ((Main.G.R - 2) * (Main.G.R - 2))))
                        {
                            E.RemoveAt(i);
                            Main.flag = true;
                            break;
                        }
                    }
                    else //не петля
                    {
                        try
                        {
                            if (((e.X / Main.zoom - V[E[i].V1].X) * (V[E[i].V2].Y - V[E[i].V1].Y) / (V[E[i].V2].X - V[E[i].V1].X) + V[E[i].V1].Y) <= (e.Y / Main.zoom + 4) &&
                                ((e.X / Main.zoom - V[E[i].V1].X) * (V[E[i].V2].Y - V[E[i].V1].Y) / (V[E[i].V2].X - V[E[i].V1].X) + V[E[i].V1].Y) >= (e.Y / Main.zoom - 4))
                            {
                                if ((V[E[i].V1].X <= V[E[i].V2].X && V[E[i].V1].X <= e.X / Main.zoom && e.X / Main.zoom <= V[E[i].V2].X) ||
                                    (V[E[i].V1].X >= V[E[i].V2].X && V[E[i].V1].X >= e.X / Main.zoom && e.X / Main.zoom >= V[E[i].V2].X))
                                {
                                    E.RemoveAt(i);
                                    Main.flag = true;
                                    break;
                                }
                            }
                        }
                        catch
                        {

                        }
                    }
                }
            }
        }
        public void deleteBS(MouseEventArgs e, List<Vertex> V, List<Edge> E, PictureBox sheet, SerializableDictionary<string, List<Edge>> routesEdgeE)
        {

            foreach (var sp in Main.allstopPoints)
            {
                if (Math.Pow((sp.X - e.X / Main.zoom), 2) + Math.Pow((sp.Y - e.Y / Main.zoom), 2) <= Main.G.R * Main.G.R)
                {
                    Main.allstopPoints.Remove(sp);
                    Main.flag = true;
                    break;
                }
            }


            foreach (var stop in Main.stopPoints)
            {
                foreach (var sp in stop.Value)
                {
                    if (Math.Pow((sp.X - e.X / Main.zoom), 2) + Math.Pow((sp.Y - e.Y / Main.zoom), 2) <= Main.G.R * Main.G.R)
                    {
                        Main.stopPointsInGrids[stop.Key].Remove(sp.gridNum);
                        stop.Value.Remove(sp);
                        Main.flag = true;
                        break;
                    }
                }
            }

        }
        public void delete(MouseEventArgs e, List<Vertex> V, List<Edge> E, PictureBox sheet, SerializableDictionary<string, List<Edge>> routesEdgeE)
        {
            //удалили ли что-нибудь по ЭТОМУ клику
            //ищем, возможно была нажата вершина
            Main.flag = false;

            foreach (var tl in Main.traficLights)
            {
                if (Math.Pow((tl.x - e.X / Main.zoom), 2) + Math.Pow((tl.y - e.Y / Main.zoom), 2) <= Main.G.R * Main.G.R)
                {
                    tl.Stop();
                    Main.TraficLightsInGrids.Remove(tl.gridNum);
                    Main.traficLights.Remove(tl);
                    Main.flag = true;
                    break;
                }
            }

            if (!Main.flag)
            {
                foreach (var sp in Main.allstopPoints)
                {
                    if (Math.Pow((sp.X - e.X / Main.zoom), 2) + Math.Pow((sp.Y - e.Y / Main.zoom), 2) <= Main.G.R * Main.G.R)
                    {
                        Main.allstopPoints.Remove(sp);
                        Main.flag = true;
                        break;
                    }
                }
            }


            foreach (var stop in Main.stopPoints)
            {
                foreach (var sp in stop.Value)
                {
                    if (Math.Pow((sp.X - e.X / Main.zoom), 2) + Math.Pow((sp.Y - e.Y / Main.zoom), 2) <= Main.G.R * Main.G.R)
                    {
                        Main.stopPointsInGrids[stop.Key].Remove(sp.gridNum);
                        stop.Value.Remove(sp);
                        Main.flag = true;
                        break;
                    }
                }
            }


            if (!Main.flag)
            {
                foreach (var routeV in Main.routes)
                {
                    for (int i = 0; i < routeV.Value.Count; i++)
                    {
                        if (Math.Pow((routeV.Value[i].X - e.X / Main.zoom), 2) + Math.Pow((routeV.Value[i].Y - e.Y / Main.zoom), 2) <= Main.G.R * Main.G.R)
                        {
                            //foreach(var routesEdge in routesEdgeE.Values)
                            //{
                            for (int j = 0; j < routesEdgeE[routeV.Key].Count; j++)
                            {
                                if ((routesEdgeE[routeV.Key][j].V1 == i) || (routesEdgeE[routeV.Key][j].V2 == i))
                                {
                                    routesEdgeE[routeV.Key].RemoveAt(j);
                                    j--;
                                }
                                else
                                {
                                    if (routesEdgeE[routeV.Key][j].V1 > i) routesEdgeE[routeV.Key][j].V1--;
                                    if (routesEdgeE[routeV.Key][j].V2 > i) routesEdgeE[routeV.Key][j].V2--;
                                }
                            }
                            routeV.Value.RemoveAt(i);
                            Main.flag = true;
                            break;
                            //}                       
                        }

                    }
                }
            }
            //ищем, возможно было нажато ребро
            if (!Main.flag)
            {
                try
                {
                    foreach (var routeV in Main.routes)
                    {
                        for (int j = 0; j < routeV.Value.Count; j++)
                        {
                            for (int i = 0; i < routesEdgeE[routeV.Key].Count; i++)
                            {
                                if (routesEdgeE[routeV.Key][i].V1 == routesEdgeE[routeV.Key][i].V2) //если это петля
                                {
                                    if ((Math.Pow((routeV.Value[routesEdgeE[routeV.Key][i].V1].X - Main.G.R - e.X / Main.zoom), 2) + Math.Pow((routeV.Value[routesEdgeE[routeV.Key][i].V1].Y - Main.G.R - e.Y / Main.zoom), 2) <= ((Main.G.R + 2) * (Main.G.R + 2))) &&
                                        (Math.Pow((routeV.Value[routesEdgeE[routeV.Key][i].V1].X - Main.G.R - e.X / Main.zoom), 2) + Math.Pow((routeV.Value[routesEdgeE[routeV.Key][i].V1].Y - Main.G.R - e.Y / Main.zoom), 2) >= ((Main.G.R - 2) * (Main.G.R - 2))))
                                    {
                                        routesEdgeE[routeV.Key].RemoveAt(i);
                                        Main.flag = true;
                                        break;
                                    }
                                }
                                else //не петля
                                {
                                    if (((e.X / Main.zoom - routeV.Value[routesEdgeE[routeV.Key][i].V1].X) * (routeV.Value[routesEdgeE[routeV.Key][i].V2].Y - routeV.Value[routesEdgeE[routeV.Key][i].V1].Y) / (routeV.Value[routesEdgeE[routeV.Key][i].V2].X - routeV.Value[routesEdgeE[routeV.Key][i].V1].X) + routeV.Value[routesEdgeE[routeV.Key][i].V1].Y) <= (e.Y / Main.zoom + 4) &&
                                        ((e.X / Main.zoom - routeV.Value[routesEdgeE[routeV.Key][i].V1].X) * (routeV.Value[routesEdgeE[routeV.Key][i].V2].Y - routeV.Value[routesEdgeE[routeV.Key][i].V1].Y) / (routeV.Value[routesEdgeE[routeV.Key][i].V2].X - routeV.Value[routesEdgeE[routeV.Key][i].V1].X) + routeV.Value[routesEdgeE[routeV.Key][i].V1].Y) >= (e.Y / Main.zoom - 4))
                                    {
                                        if ((routeV.Value[routesEdgeE[routeV.Key][i].V1].X <= routeV.Value[routesEdgeE[routeV.Key][i].V2].X && routeV.Value[routesEdgeE[routeV.Key][i].V1].X <= e.X / Main.zoom && e.X / Main.zoom <= routeV.Value[routesEdgeE[routeV.Key][i].V2].X) ||
                                            (routeV.Value[routesEdgeE[routeV.Key][i].V1].X >= routeV.Value[routesEdgeE[routeV.Key][i].V2].X && routeV.Value[routesEdgeE[routeV.Key][i].V1].X >= e.X / Main.zoom && e.X / Main.zoom >= routeV.Value[routesEdgeE[routeV.Key][i].V2].X))
                                        {
                                            routesEdgeE[routeV.Key].RemoveAt(i);
                                            Main.flag = true;
                                            break;
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
                catch
                {

                }

            }

            for (int i = 0; i < V.Count; i++)
            {
                if (Math.Pow((V[i].X - e.X / Main.zoom), 2) + Math.Pow((V[i].Y - e.Y / Main.zoom), 2) <= Main.G.R * Main.G.R)
                {
                    for (int j = 0; j < E.Count; j++)
                    {
                        if ((E[j].V1 == i) || (E[j].V2 == i))
                        {

                            E.RemoveAt(j);
                            j--;
                        }
                        else
                        {
                            if (E[j].V1 > i) E[j].V1--;
                            if (E[j].V2 > i) E[j].V2--;
                        }
                    }
                    V.RemoveAt(i);
                    Main.flag = true;
                    break;

                }

            }

            //ищем, возможно было нажато ребро
            if (!Main.flag)
            {
                for (int i = 0; i < E.Count; i++)
                {
                    if (E[i].V1 == E[i].V2) //если это петля
                    {
                        if ((Math.Pow((V[E[i].V1].X - Main.G.R - e.X / Main.zoom), 2) + Math.Pow((V[E[i].V1].Y - Main.G.R - e.Y / Main.zoom), 2) <= ((Main.G.R + 2) * (Main.G.R + 2))) &&
                            (Math.Pow((V[E[i].V1].X - Main.G.R - e.X / Main.zoom), 2) + Math.Pow((V[E[i].V1].Y - Main.G.R - e.Y / Main.zoom), 2) >= ((Main.G.R - 2) * (Main.G.R - 2))))
                        {
                            E.RemoveAt(i);
                            Main.flag = true;
                            break;
                        }
                    }
                    else //не петля
                    {
                        try
                        {
                            if (((e.X / Main.zoom - V[E[i].V1].X) * (V[E[i].V2].Y - V[E[i].V1].Y) / (V[E[i].V2].X - V[E[i].V1].X) + V[E[i].V1].Y) <= (e.Y / Main.zoom + 4) &&
                                ((e.X / Main.zoom - V[E[i].V1].X) * (V[E[i].V2].Y - V[E[i].V1].Y) / (V[E[i].V2].X - V[E[i].V1].X) + V[E[i].V1].Y) >= (e.Y / Main.zoom - 4))
                            {
                                if ((V[E[i].V1].X <= V[E[i].V2].X && V[E[i].V1].X <= e.X / Main.zoom && e.X / Main.zoom <= V[E[i].V2].X) ||
                                    (V[E[i].V1].X >= V[E[i].V2].X && V[E[i].V1].X >= e.X / Main.zoom && e.X / Main.zoom >= V[E[i].V2].X))
                                {
                                    E.RemoveAt(i);
                                    Main.flag = true;
                                    break;
                                }
                            }
                        }
                        catch
                        {

                        }
                    }
                }
            }
        }

        public void deleteRoute(MouseEventArgs e, List<Vertex> routeV, List<Edge> routesEdge, PictureBox sheet)
        {
            bool flag = false; //удалили ли что-нибудь по ЭТОМУ клику
                               //ищем, возможно была нажата вершина
            for (int i = 0; i < routeV.Count; i++)
            {
                if (Math.Pow((routeV[i].X - e.X / Main.zoom), 2) + Math.Pow((routeV[i].Y - e.Y / Main.zoom), 2) <= Main.G.R * Main.G.R)
                {
                    for (int j = 0; j < routesEdge.Count; j++)
                    {
                        if ((routesEdge[j].V1 == i) || (routesEdge[j].V2 == i))
                        {
                            routesEdge.RemoveAt(j);
                            j--;
                        }
                        else
                        {
                            if (routesEdge[j].V1 > i) routesEdge[j].V1--;
                            if (routesEdge[j].V2 > i) routesEdge[j].V2--;
                        }
                    }
                    routeV.RemoveAt(i);
                    flag = true;
                    //  DrawGrid();
                    break;
                }
                // DrawGrid();
            }
            //ищем, возможно было нажато ребро
            if (!flag)
            {
                for (int i = 0; i < routesEdge.Count; i++)
                {
                    if (routesEdge[i].V1 == routesEdge[i].V2) //если это петля
                    {
                        if ((Math.Pow((routeV[routesEdge[i].V1].X - Main.G.R - e.X), 2) + Math.Pow((routeV[routesEdge[i].V1].Y - Main.G.R - e.Y), 2) <= ((Main.G.R + 2) * (Main.G.R + 2))) &&
                            (Math.Pow((routeV[routesEdge[i].V1].X - Main.G.R - e.X), 2) + Math.Pow((routeV[routesEdge[i].V1].Y - Main.G.R - e.Y), 2) >= ((Main.G.R - 2) * (Main.G.R - 2))))
                        {
                            routesEdge.RemoveAt(i);
                            flag = true;
                            //   DrawGrid();
                            break;
                        }
                    }
                    else //не петля
                    {
                        try
                        {
                            if (((e.X - routeV[routesEdge[i].V1].X) * (routeV[routesEdge[i].V2].Y - routeV[routesEdge[i].V1].Y) / (routeV[routesEdge[i].V2].X - routeV[routesEdge[i].V1].X) + routeV[routesEdge[i].V1].Y) <= (e.Y + 4) &&
                                ((e.X - routeV[routesEdge[i].V1].X) * (routeV[routesEdge[i].V2].Y - routeV[routesEdge[i].V1].Y) / (routeV[routesEdge[i].V2].X - routeV[routesEdge[i].V1].X) + routeV[routesEdge[i].V1].Y) >= (e.Y - 4))
                            {
                                if ((routeV[routesEdge[i].V1].X <= routeV[routesEdge[i].V2].X && routeV[routesEdge[i].V1].X <= e.X && e.X <= routeV[routesEdge[i].V2].X) ||
                                    (routeV[routesEdge[i].V1].X >= routeV[routesEdge[i].V2].X && routeV[routesEdge[i].V1].X >= e.X && e.X >= routeV[routesEdge[i].V2].X))
                                {
                                    routesEdge.RemoveAt(i);
                                    Main.flag = true;
                                    Main.DrawGrid();
                                    break;
                                }
                            }
                        }
                        catch
                        {

                        }
                    }
                }
            }
            //если что-то было удалено, то обновляем граф на экране
            if (flag)
            {
                Main.G.ClearSheet();
                Main.G.DrawALLGraph(Main.V, Main.E);
                Main.G.DrawALLGraph(routeV, routesEdge, 1);
                sheet.Invoke(new Del((s) => sheet.Image = s), Main.G.GetBitmap());
                Main.DrawGrid();
            }
        }

    }
}
