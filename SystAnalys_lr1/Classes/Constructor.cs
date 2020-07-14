//This is a personal academic project. Dear PVS-Studio, please check it.
//PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using MetroFramework;
using SystAnalys_lr1.Strings;
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

        public bool CheckV(MouseEventArgs e, bool check)
        {
            for (int i = 0; i < Data.V.Count; i++)
            {
                if (Math.Pow((Data.V[i].X - e.X / Main.zoom), 2) + Math.Pow((Data.V[i].Y - e.Y / Main.zoom), 2) <= Main.G.R * Main.G.R)
                {
                    return true;
                }
            }
            return false;
        }


        public void MapUpdate(PictureBox sheet)
        {
            sheet.Invoke(new Del((s) => sheet.Image = s), Main.G.GetBitmap());
            GridCreator.DrawGrid(sheet);
        }

        public void MapUpdateNetwork(PictureBox sheet, List<Vertex> V, List<Edge> E)
        {
            Main.G.ClearSheet();
            Main.G.DrawALLGraph(V, E);
            sheet.Invoke(new Del((s) => sheet.Image = s), Main.G.GetBitmap());
            GridCreator.DrawGrid(sheet);
        }

        public void MapUpdateRoute(PictureBox sheet, List<Vertex> routeV, List<Edge> routeE)
        {
            Main.G.ClearSheet();
            Main.G.DrawALLGraph(Data.V, Data.E);
            Main.G.DrawALLGraph(routeV, routeE, 1);
            sheet.Invoke(new Del((s) => sheet.Image = s), Main.G.GetBitmap());
            GridCreator.DrawGrid(sheet);
        }

        delegate void Del(Bitmap bmp);

        public async void AsSelect(MouseEventArgs e, List<Vertex> V, List<Edge> E, PictureBox sheet, int n = 0)
        {
            await Task.Run(() => Select(e, V, E, sheet, n));

        }

        public async void AsDrawEdge(MouseEventArgs e, List<Vertex> V, List<Edge> E, PictureBox sheet)
        {
            await Task.Run(() => DrawEdge(e, V, E, sheet));
        }


        public async void AsDelete(MouseEventArgs e, List<Vertex> V, List<Edge> E, PictureBox sheet, SerializableDictionary<string, List<Edge>> routesEdgeE)
        {
            await Task.Run(() => Delete(e, V, E, sheet, routesEdgeE));
            if (Main.Flag)
            {
                Main.G.ClearSheet();
                Main.G.DrawALLGraph(V, E);
                sheet.Invoke(new Del((s) => sheet.Image = s), Main.G.GetBitmap());
                GridCreator.DrawGrid(sheet);
            }
        }

        public async void AsDeleteRoute(MouseEventArgs e, List<Vertex> routeV, List<Edge> routesEdge, PictureBox sheet)
        {
            await Task.Run(() => DeleteRoute(e, routeV, routesEdge, sheet));

        }

        public void CreateBus(MouseEventArgs e, bool trackerCheck, bool backsideCheck, string route)
        {
            if (Data.AllCoordinates[route].Count != 0)
            {
                int pos = 0;

                if (e.Button == MouseButtons.Left)
                {
                    double min = Math.Pow((Data.AllCoordinates[route].Last().X - e.X / Main.zoom), 2) + Math.Pow((Data.AllCoordinates[route].Last().Y - e.Y / Main.zoom), 2);
                    for (int i = 0; i < Data.AllCoordinates[route].Count; i++)
                    {
                        if (Math.Pow((Data.AllCoordinates[route][i].X - e.X / Main.zoom), 2) + Math.Pow((Data.AllCoordinates[route][i].Y - e.Y / Main.zoom), 2) <= Main.G.R * Main.G.R * 500)
                        {
                            if ((Math.Pow((Data.AllCoordinates[route][i].X - e.X / Main.zoom), 2) + Math.Pow((Data.AllCoordinates[route][i].Y - e.Y / Main.zoom), 2) < min))
                            {
                                min = Math.Pow((Data.AllCoordinates[route][i].X - e.X / Main.zoom), 2) + Math.Pow((Data.AllCoordinates[route][i].Y - e.Y / Main.zoom), 2);
                                pos = i;
                            }
                        }
                    }
                }

                if (trackerCheck)
                {
                    Rectangle rect = new Rectangle(0, 0, 200, 100);
                    Bitmap busPic = new Bitmap(Bus.BusImg);
                    busPic = new Bitmap(busPic, new Size(15, 15));
                    Bitmap num = new Bitmap(busPic.Height, busPic.Width);
                    using (Graphics gr = Graphics.FromImage(num))
                    {
                        using (Font font = new Font("Segoe UI", 8))
                        {
                            gr.FillRectangle(Brushes.Transparent, rect);

                            gr.DrawString(
                                route,
                                font,
                                Brushes.Black,
                                rect,
                                StringFormat.GenericTypographic
                                );
                        }
                    }

                    Bitmap original = new Bitmap(Math.Max(busPic.Width, num.Width), Math.Max(busPic.Height, num.Height) * 2);
                    using (Graphics graphics = Graphics.FromImage(original))
                    {

                        graphics.DrawImage(busPic, 0, 0);
                        graphics.DrawImage(num, 0, 15);
                        graphics.Dispose();

                    }
                    Data.Buses.Add(new Bus(original, pos, backsideCheck, route, Data.AllCoordinates[route], true));
                }
                else
                {
                    Rectangle rect = new Rectangle(0, 0, 200, 100);
                    Bitmap busPic = new Bitmap(Bus.OffBusImg);
                    busPic = new Bitmap(busPic, new Size(15, 15));
                    Bitmap num = new Bitmap(busPic.Height, busPic.Width);
                    using (Graphics gr = Graphics.FromImage(num))
                    {
                        using (Font font = new Font("Segoe UI", 8))
                        {
                            gr.FillRectangle(Brushes.Transparent, rect);

                            gr.DrawString(
                                route,
                                font,
                                Brushes.Black,
                                rect,
                                StringFormat.GenericTypographic
                                );
                        }
                    }

                    Bitmap original = new Bitmap(Math.Max(busPic.Width, num.Width), Math.Max(busPic.Height, num.Height) * 2);
                    using (Graphics graphics = Graphics.FromImage(original))
                    {

                        graphics.DrawImage(busPic, 0, 0);
                        graphics.DrawImage(num, 0, 15);
                        graphics.Dispose();

                    }
                    Data.Buses.Add(new Bus(original, pos, backsideCheck, route, Data.AllCoordinates[route], false));
                };
            }
        }

        public void AddBus(MouseEventArgs e, bool trackerCheck, bool backsideCheck, string route)
        {
            if (Data.AllCoordinates.ContainsKey(route))
            {
                CreateBus(e, trackerCheck, backsideCheck, route);
            }
            else
            {
                try
                {
                    Coordinates c = new Coordinates();
                    c.CreateOneRouteCoordinates(route);
                    CreateBus(e, trackerCheck, backsideCheck, route);
                }
                catch
                {
                    throw new Exception();
                }
            }
        }


        public void AddStopPointInRoutes(PictureBox sheet, List<GridPart> gridParts)
        {
            bool br = false;
            foreach (var stopPoints in Data.StopPoints)
            {
                foreach (var sp in stopPoints.Value)
                {
                    foreach (var gridPart in gridParts)
                    {
                        if (((sp.X > gridPart.X * Main.zoom) && (sp.Y > gridPart.Y * Main.zoom)) && ((sp.X < gridPart.X * Main.zoom + GridPart.Width * Main.zoom) && (sp.Y < gridPart.Y * Main.zoom + GridPart.Height * Main.zoom)))
                        {
                            if (!Data.StopPointsInGrids.ContainsKey(stopPoints.Key))
                            {
                                Data.StopPoints[stopPoints.Key].Last().GridNum = gridParts.IndexOf(gridPart);
                                Data.StopPointsInGrids.Add(stopPoints.Key, new List<int>());
                                Data.StopPointsInGrids[stopPoints.Key].Add(gridParts.IndexOf(gridPart));

                                break;
                            }

                            if (!Data.StopPointsInGrids[stopPoints.Key].Contains(gridParts.IndexOf(gridPart)) )
                            {
                                Data.StopPoints[stopPoints.Key].Last().GridNum = gridParts.IndexOf(gridPart);
                                Data.StopPointsInGrids[stopPoints.Key].Add(gridParts.IndexOf(gridPart));
                            }
                            else
                            {
                                Data.StopPoints[stopPoints.Key].Last().GridNum = gridParts.IndexOf(gridPart);
                            }

                            break;


                        }
                    }
                }
            
            }

        }
    

    public void AddStopPointsInRoute(MouseEventArgs e, List<BusStop> allstopPoints, PictureBox sheet, List<GridPart> gridParts, string route)
    {
        foreach (var sp in allstopPoints)
        {
            if (Math.Pow((sp.X - e.X / Main.zoom), 2) + Math.Pow((sp.Y - e.Y / Main.zoom), 2) <= Main.G.R * Main.G.R)
            {
                if (Data.StopPoints.ContainsKey(route))
                {
                    if (!Data.StopPoints[route].Contains(new Vertex(sp.X, sp.Y)))
                    {
                        if (Data.StopPoints.ContainsKey(route))
                        {
                            Data.StopPoints[route].Add(new BusStop(sp.X, sp.Y));
                        }
                        else
                        {
                            Data.StopPoints.Add(route, new List<BusStop>());
                            if (!Data.StopPoints[route].Contains(new Vertex(sp.X, sp.Y)))
                            {
                                if (Data.StopPoints.ContainsKey(route) && Data.StopPointsInGrids.ContainsKey(route))
                                {
                                    Data.StopPoints[route].Add(new BusStop(sp.X, sp.Y));
                                }
                            }
                        }
                    }

                    Main.G.DrawStopRouteVertex(sp.X, sp.Y);
                    MapUpdate(sheet);

                    break;
                }
                else
                {
                    Data.StopPoints.Add(route, new List<BusStop>());
                    if (!Data.StopPoints[route].Contains(new Vertex(sp.X, sp.Y)))
                    {
                        if (Data.StopPoints.ContainsKey(route))
                        {
                            Data.StopPoints[route].Add(new BusStop(sp.X, sp.Y));
                        }
                        else
                        {
                            Data.StopPoints.Add(route, new List<BusStop>());
                            if (!Data.StopPoints[route].Contains(new Vertex(sp.X, sp.Y)))
                            {
                                if (Data.StopPoints.ContainsKey(route) && Data.StopPointsInGrids.ContainsKey(route))
                                {
                                    Data.StopPoints[route].Add(new BusStop(sp.X, sp.Y));
                                }
                            }
                        }
                    }

                    Main.G.DrawStopRouteVertex(sp.X, sp.Y);
                    MapUpdate(sheet);

                    break;
                }
            }
        }
    }


    public void AddStopPoints(MouseEventArgs e, List<BusStop> allstopPoints, PictureBox sheet, List<GridPart> gridParts)
    {
        allstopPoints.Add(new BusStop(e.X / Main.zoom, e.Y / Main.zoom));
        Main.G.DrawStopVertex(e.X / Main.zoom, e.Y / Main.zoom);
        MapUpdate(sheet);
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
                    MapUpdate(sheet);
                    break;
                }
                else
                {
                    selected.Add(i);
                    E.Add(new Edge(selected[0], selected[1]));
                    Main.G.DrawEdge(V[selected[0]], V[selected[1]], E[E.Count - 1], 1);
                    selected[0] = selected[1];
                    selected.Remove(selected[1]);
                    MapUpdateNetwork(sheet, V, E);
                    Main.G.DrawSelectedVertex(V[i].X, V[i].Y);
                    break;
                }

            }
        }


    }


    public void SelectRouteInRoute(MouseEventArgs e, List<Vertex> routeV, List<Edge> routesEdge, PictureBox sheet, List<int> selected)
    {
        bool select = true;
        for (int i = 0; i < Data.V.Count; i++)
        {
            if (Math.Pow((Data.V[i].X - e.X / Main.zoom), 2) + Math.Pow((Data.V[i].Y - e.Y / Main.zoom), 2) <= Main.G.R * Main.G.R)
            {
                if (selected.Count == 0)
                {
                    if (routesEdge.Count != 0)
                    {
                        if (routeV[routesEdge.Last().V2].X == Data.V[i].X && routeV[routesEdge.Last().V2].Y == Data.V[i].Y)
                        {
                            selected.Add(i);
                            Main.G.DrawSelectedVertex(Data.V[i].X, Data.V[i].Y);
                        }
                    }
                    else
                    {
                        if (!routeV.Contains(new Vertex(Data.V[i].X, Data.V[i].Y)))
                        {
                            routeV.Add(new Vertex(Data.V[i].X, Data.V[i].Y));
                        }
                        selected.Add(i);
                        Main.G.DrawSelectedVertex(Data.V[i].X, Data.V[i].Y);
                    }
                    break;
                }
                else
                {
                    selected.Add(i);
                    foreach (var ed in Data.E)
                    {
                        if ((ed.V1 == selected[0] && ed.V2 == selected[1]) || (ed.V2 == selected[0] && ed.V1 == selected[1]))
                        {
                            routesEdge.Add(new Edge(routeV.Count - 1, routeV.Count));
                            routeV.Add(new Vertex(Data.V[i].X, Data.V[i].Y));
                            MapUpdateRoute(sheet, routeV, routesEdge);
                            select = true;
                            Main.G.DrawSelectedVertex(Data.V[i].X, Data.V[i].Y);
                            break;
                        }
                        select = false;
                    }
                    if (!select)
                    {
                        MapUpdateNetwork(sheet, Data.V, Data.E);
                    }
                    if (routeV.Contains(new Vertex(Data.V[i].X, Data.V[i].Y)))
                        Main.G.DrawSelectedVertex(Data.V[i].X, Data.V[i].Y);

                }
                selected[0] = selected[1];
                selected.Remove(selected[1]);

            }
        }
        MapUpdate(sheet);


    }

    public void DrawEdgeInRoute(MouseEventArgs e, List<Vertex> routeV, List<Edge> routesEdge, PictureBox sheet, string route)
    {
        if (e.Button == MouseButtons.Left)
        {
            for (int i = 0; i < Data.V.Count; i++)
            {
                if (Math.Pow((Data.V[i].X - e.X / Main.zoom), 2) + Math.Pow((Data.V[i].Y - e.Y / Main.zoom), 2) <= Main.G.R * Main.G.R)
                {
                    if (Main.Selected1 == -1)
                    {
                        if (routesEdge.Count != 0)
                        {
                            if (routeV[routesEdge.Last().V2].X == Data.V[i].X && routeV[routesEdge.Last().V2].Y == Data.V[i].Y)
                            {
                                Main.Selected1 = i;
                                Main.G.DrawSelectedVertex(Data.V[i].X, Data.V[i].Y);
                            }
                        }
                        else
                        {
                            if (!routeV.Contains(new Vertex(Data.V[i].X, Data.V[i].Y)))
                            {
                                routeV.Add(new Vertex(Data.V[i].X, Data.V[i].Y));
                            }
                            Main.Selected1 = i;
                            Main.G.DrawSelectedVertex(Data.V[i].X, Data.V[i].Y);
                        }


                        break;
                    }
                    if (Main.Selected2 == -1)
                    {
                        Main.G.DrawSelectedVertex(Data.V[i].X, Data.V[i].Y);
                        Main.Selected2 = i;
                        foreach (var ed in Data.E)
                        {
                            if ((ed.V1 == Main.Selected1 && ed.V2 == Main.Selected2) || (ed.V2 == Main.Selected1 && ed.V1 == Main.Selected2))
                            {
                                routesEdge.Add(new Edge(routeV.Count - 1, routeV.Count));
                                routeV.Add(new Vertex(Data.V[i].X, Data.V[i].Y));
                                Main.G.DrawEdge(Data.V[Main.Selected1], Data.V[Main.Selected2], Data.E[Data.E.Count - 1], 1);
                                sheet.Image = Main.G.GetBitmap();
                                Main.Selected1 = -1;
                                Main.Selected2 = -1;
                                break;
                            }
                        }
                        MapUpdateRoute(sheet, routeV, routesEdge);
                        Main.Selected1 = -1;
                        Main.Selected2 = -1;
                        break;
                    }
                }
            }
            MapUpdate(sheet);
        }

    }

    public void DeleteBus(MouseEventArgs e, List<Vertex> routeV, List<Edge> routesEdge, PictureBox sheet, string route, int scrollX, int scrollY)
    {
        if (Data.AllCoordinates[route].Count != 0)
        {
            int? pos = null;
            double min = Math.Pow((sheet.Image.Width - (e.X / Main.zoom + scrollX)), 2) + Math.Pow((sheet.Image.Height - (e.Y / Main.zoom + scrollY)), 2);
            for (int i = 0; i < Data.Buses.Count; i++)
            {
                if (Math.Pow((Data.Buses[i].Coordinates[Data.Buses[i].PositionAt].X - (e.X / Main.zoom + scrollX)), 2) + Math.Pow((Data.Buses[i].Coordinates[Data.Buses[i].PositionAt].Y - (e.Y / Main.zoom + scrollY)), 2) <= Data.Buses[i].R * Data.Buses[i].R * 500)
                {
                    if (Data.Buses[i].Route == route)
                    {
                        if (Math.Pow((Data.Buses[i].Coordinates[Data.Buses[i].PositionAt].X - (e.X / Main.zoom + scrollX)), 2) + Math.Pow((Data.Buses[i].Coordinates[Data.Buses[i].PositionAt].Y - (e.Y / Main.zoom + scrollY)), 2) < min)
                        {
                            min = Math.Pow((Data.Buses[i].Coordinates[Data.Buses[i].PositionAt].X - (e.X / Main.zoom + scrollX)), 2) + Math.Pow((Data.Buses[i].Coordinates[Data.Buses[i].PositionAt].Y - (e.Y / Main.zoom + scrollY)), 2);
                            pos = i;
                        }
                    }
                }
            }
            if (pos != null)
            {
                Data.Buses.Remove(Data.Buses[int.Parse(pos.ToString())]);
            }
            MapUpdateRoute(sheet, routeV, routesEdge);
        }
    }

    public void AddGridPart(List<TraficLight> traficLights, List<GridPart> gridParts)
    {
        foreach (var tf in traficLights)
        {
            foreach (var gridPart in gridParts)
            {
                if (((tf.X > gridPart.X * Main.zoom) && (tf.Y > gridPart.Y * Main.zoom)) && ((tf.X < gridPart.X * Main.zoom + GridPart.Width * Main.zoom) && (tf.Y < gridPart.Y * Main.zoom + GridPart.Height * Main.zoom)))
                {
                    if (!Data.TraficLightsInGrids.Contains(gridParts.IndexOf(gridPart)))
                    {
                        Data.TraficLightsInGrids.Add(gridParts.IndexOf(gridPart));
                        tf.GridNum = gridParts.IndexOf(gridPart);
                        break;
                    }
                }
            }
        }
    }

    public void FirstTrafficLight(MouseEventArgs e, List<TraficLight> traficLights, PictureBox sheet, List<GridPart> gridParts)
    {
        traficLights.Add(new TraficLight(e.X / Main.zoom, e.Y / Main.zoom, Main.FirstCrossRoadsGreenLight, Main.FirstCrossRoadsRedLight));
        Main.G.DrawGreenVertex(e.X / Main.zoom, e.Y / Main.zoom);
        Main.FirstCrossRoads -= 1;
        sheet.Image = Main.G.GetBitmap();
        GridCreator.DrawGrid(sheet);
    }

    public void SecondTrafficLight(MouseEventArgs e, List<TraficLight> traficLights, PictureBox sheet, List<GridPart> gridParts)
    {
        traficLights.Add(new TraficLight(e.X / Main.zoom, e.Y / Main.zoom, Main.FirstCrossRoadsRedLight, Main.FirstCrossRoadsGreenLight));
        traficLights.Last().Tick = Main.FirstCrossRoadsRedLight + 2;
        traficLights.Last().Status = LightStatus.RED;
        Main.G.DrawSelectedVertex(e.X / Main.zoom, e.Y / Main.zoom);
        Main.SecondCrossRoads -= 1;
    }

    public void Select(MouseEventArgs e, List<Vertex> V, List<Edge> E, PictureBox sheet, int n = 0)
    {
        for (int i = 0; i < V.Count; i++)
        {
            if (Math.Pow((V[i].X - e.X / Main.zoom), 2) + Math.Pow((V[i].Y - e.Y / Main.zoom), 2) <= Main.G.R * Main.G.R)
            {
                if (Main.Selected1 != -1)
                {
                    Main.Selected1 = -1;
                    if (n != 0) MapUpdateRoute(sheet, V, E);
                    else MapUpdateNetwork(sheet, V, E);
                }
                if (Main.Selected1 == -1)
                {
                    Main.G.DrawSelectedVertex(V[i].X, V[i].Y);
                    Main.Selected1 = i;
                    MapUpdate(sheet);
                    break;
                }
            }
        }
    }


    public void DrawVertex(MouseEventArgs e, List<Vertex> V, PictureBox sheet)
    {
        V.Add(new Vertex(e.X / Main.zoom, e.Y / Main.zoom));
        Main.G.DrawVertex(e.X / Main.zoom, e.Y / Main.zoom);
        MapUpdate(sheet);
    }

    public void DrawEdge(MouseEventArgs e, List<Vertex> V, List<Edge> E, PictureBox sheet)
    {
        if (e.Button == MouseButtons.Left)
        {
            for (int i = 0; i < V.Count; i++)
            {
                if (Math.Pow((V[i].X - e.X / Main.zoom), 2) + Math.Pow((V[i].Y - e.Y / Main.zoom), 2) <= Main.G.R * Main.G.R)
                {
                    if (Main.Selected1 == -1)
                    {
                        Main.G.DrawSelectedVertex(V[i].X, V[i].Y);
                        Main.Selected1 = i;
                        sheet.Invoke(new Del((s) => sheet.Image = s), Main.G.GetBitmap());
                        break;
                    }
                    if (Main.Selected2 == -1)
                    {
                        Main.G.DrawSelectedVertex(V[i].X, V[i].Y);
                        Main.Selected2 = i;
                        E.Add(new Edge(Main.Selected1, Main.Selected2));
                        Main.G.DrawEdge(V[Main.Selected1], V[Main.Selected2], E[E.Count - 1]);
                        Main.Selected1 = -1;
                        Main.Selected2 = -1;
                        MapUpdateNetwork(sheet, V, E);
                        break;
                    }
                }
            }
        }

    }

    public void DeleteTF(MouseEventArgs e, List<Vertex> V, List<Edge> E, PictureBox sheet, SerializableDictionary<string, List<Edge>> routesEdgeE)
    {
        foreach (var tl in Data.TraficLights)
        {
            if (Math.Pow((tl.X - e.X / Main.zoom), 2) + Math.Pow((tl.Y - e.Y / Main.zoom), 2) <= Main.G.R * Main.G.R)
            {
                tl.Stop();
                Data.TraficLightsInGrids.Remove(tl.GridNum);
                Data.TraficLights.Remove(tl);
                Main.Flag = true;
                break;
            }
        }
    }
    public void DeleteVE(MouseEventArgs e, List<Vertex> V, List<Edge> E, PictureBox sheet, SerializableDictionary<string, List<Edge>> routesEdgeE)
    {
        if (!Main.Flag)
        {
            foreach (var routeV in Data.Routes)
            {
                for (int i = 0; i < routeV.Value.Count; i++)
                {
                    if (Math.Pow((routeV.Value[i].X - e.X / Main.zoom), 2) + Math.Pow((routeV.Value[i].Y - e.Y / Main.zoom), 2) <= Main.G.R * Main.G.R)
                    {
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
                        Main.Flag = true;
                        break;
                    }

                }
            }
        }
        if (!Main.Flag)
        {
            try
            {
                foreach (var routeV in Data.Routes)
                {
                    for (int j = 0; j < routeV.Value.Count; j++)
                    {
                        for (int i = 0; i < routesEdgeE[routeV.Key].Count; i++)
                        {
                            if (routesEdgeE[routeV.Key][i].V1 == routesEdgeE[routeV.Key][i].V2)
                            {
                                if ((Math.Pow((routeV.Value[routesEdgeE[routeV.Key][i].V1].X - Main.G.R - e.X / Main.zoom), 2) + Math.Pow((routeV.Value[routesEdgeE[routeV.Key][i].V1].Y - Main.G.R - e.Y / Main.zoom), 2) <= ((Main.G.R + 2) * (Main.G.R + 2))) &&
                                    (Math.Pow((routeV.Value[routesEdgeE[routeV.Key][i].V1].X - Main.G.R - e.X / Main.zoom), 2) + Math.Pow((routeV.Value[routesEdgeE[routeV.Key][i].V1].Y - Main.G.R - e.Y / Main.zoom), 2) >= ((Main.G.R - 2) * (Main.G.R - 2))))
                                {
                                    routesEdgeE[routeV.Key].RemoveAt(i);
                                    Main.Flag = true;
                                    break;
                                }
                            }
                            else
                            {
                                if (((e.X / Main.zoom - routeV.Value[routesEdgeE[routeV.Key][i].V1].X) * (routeV.Value[routesEdgeE[routeV.Key][i].V2].Y - routeV.Value[routesEdgeE[routeV.Key][i].V1].Y) / (routeV.Value[routesEdgeE[routeV.Key][i].V2].X - routeV.Value[routesEdgeE[routeV.Key][i].V1].X) + routeV.Value[routesEdgeE[routeV.Key][i].V1].Y) <= (e.Y / Main.zoom + 4) &&
                                    ((e.X / Main.zoom - routeV.Value[routesEdgeE[routeV.Key][i].V1].X) * (routeV.Value[routesEdgeE[routeV.Key][i].V2].Y - routeV.Value[routesEdgeE[routeV.Key][i].V1].Y) / (routeV.Value[routesEdgeE[routeV.Key][i].V2].X - routeV.Value[routesEdgeE[routeV.Key][i].V1].X) + routeV.Value[routesEdgeE[routeV.Key][i].V1].Y) >= (e.Y / Main.zoom - 4))
                                {
                                    if ((routeV.Value[routesEdgeE[routeV.Key][i].V1].X <= routeV.Value[routesEdgeE[routeV.Key][i].V2].X && routeV.Value[routesEdgeE[routeV.Key][i].V1].X <= e.X / Main.zoom && e.X / Main.zoom <= routeV.Value[routesEdgeE[routeV.Key][i].V2].X) ||
                                        (routeV.Value[routesEdgeE[routeV.Key][i].V1].X >= routeV.Value[routesEdgeE[routeV.Key][i].V2].X && routeV.Value[routesEdgeE[routeV.Key][i].V1].X >= e.X / Main.zoom && e.X / Main.zoom >= routeV.Value[routesEdgeE[routeV.Key][i].V2].X))
                                    {
                                        routesEdgeE[routeV.Key].RemoveAt(i);
                                        Main.Flag = true;
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
                Main.Flag = true;
                break;

            }

        }

        if (!Main.Flag)
        {
            for (int i = 0; i < E.Count; i++)
            {
                if (E[i].V1 == E[i].V2)
                {
                    if ((Math.Pow((V[E[i].V1].X - Main.G.R - e.X / Main.zoom), 2) + Math.Pow((V[E[i].V1].Y - Main.G.R - e.Y / Main.zoom), 2) <= ((Main.G.R + 2) * (Main.G.R + 2))) &&
                        (Math.Pow((V[E[i].V1].X - Main.G.R - e.X / Main.zoom), 2) + Math.Pow((V[E[i].V1].Y - Main.G.R - e.Y / Main.zoom), 2) >= ((Main.G.R - 2) * (Main.G.R - 2))))
                    {
                        E.RemoveAt(i);
                        Main.Flag = true;
                        break;
                    }
                }
                else
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
                                Main.Flag = true;
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


    public void DeleteStopsOnRoute(MouseEventArgs e, List<Vertex> routeV, PictureBox sheet, string route)
    {
        bool flag = false;

        foreach (var stopRoute in Data.StopPoints[route])
        {
            if (Math.Pow((stopRoute.X - e.X / Main.zoom), 2) + Math.Pow((stopRoute.Y - e.Y / Main.zoom), 2) <= Main.G.R * Main.G.R)
            {
                Data.StopPointsInGrids[route].Remove(stopRoute.GridNum);
                Data.StopPoints[route].Remove(stopRoute);
                flag = true;
                break;
            }

        }
        if (flag)
        {
            MapUpdateRoute(sheet, routeV, Data.RoutesEdge[route]);
        }
    }

    public void DeleteVandE(MouseEventArgs e, List<Vertex> routeV, List<Edge> routesEdge, PictureBox sheet)
    {
        bool flag = false;

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
                break;
            }
        }
        if (!flag)
        {
            for (int i = 0; i < routesEdge.Count; i++)
            {
                if (routesEdge[i].V1 == routesEdge[i].V2)
                {
                    if ((Math.Pow((routeV[routesEdge[i].V1].X - Main.G.R - e.X / Main.zoom), 2) + Math.Pow((routeV[routesEdge[i].V1].Y - Main.G.R - e.Y / Main.zoom), 2) <= ((Main.G.R + 2) * (Main.G.R + 2))) &&
                        (Math.Pow((routeV[routesEdge[i].V1].X - Main.G.R - e.X / Main.zoom), 2) + Math.Pow((routeV[routesEdge[i].V1].Y - Main.G.R - e.Y / Main.zoom), 2) >= ((Main.G.R - 2) * (Main.G.R - 2))))
                    {
                        routesEdge.RemoveAt(i);
                        flag = true;
                        break;
                    }
                }
                else
                {
                    try
                    {
                        if (((e.X / Main.zoom - routeV[routesEdge[i].V1].X) * (routeV[routesEdge[i].V2].Y - routeV[routesEdge[i].V1].Y) / (routeV[routesEdge[i].V2].X - routeV[routesEdge[i].V1].X) + routeV[routesEdge[i].V1].Y) <= (e.Y / Main.zoom + 4) &&
                            ((e.X / Main.zoom - routeV[routesEdge[i].V1].X) * (routeV[routesEdge[i].V2].Y - routeV[routesEdge[i].V1].Y) / (routeV[routesEdge[i].V2].X - routeV[routesEdge[i].V1].X) + routeV[routesEdge[i].V1].Y) >= (e.Y / Main.zoom - 4))
                        {
                            if ((routeV[routesEdge[i].V1].X <= routeV[routesEdge[i].V2].X && routeV[routesEdge[i].V1].X <= e.X / Main.zoom && e.X / Main.zoom <= routeV[routesEdge[i].V2].X) ||
                                (routeV[routesEdge[i].V1].X >= routeV[routesEdge[i].V2].X && routeV[routesEdge[i].V1].X >= e.X / Main.zoom && e.X / Main.zoom >= routeV[routesEdge[i].V2].X))
                            {
                                routesEdge.RemoveAt(i);
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
            MapUpdateRoute(sheet, routeV, routesEdge);
        }
    }
    public void DeleteTFOnRoute(MouseEventArgs e, List<Vertex> routeV, List<Edge> routesEdge, PictureBox sheet, List<TraficLight> traficLights)
    {
        bool flag = false;

        for (var i = 0; i < traficLights.Count; i++)
        {
            if (Math.Pow((traficLights[i].X - e.X / Main.zoom), 2) + Math.Pow((traficLights[i].Y - e.Y / Main.zoom), 2) <= Main.G.R * Main.G.R)
            {
                Data.TraficLightsInGrids.RemoveAt(i);
                traficLights.RemoveAt(i);
                flag = true;
                break;
            }

        }
        if (flag)
        {
            MapUpdateRoute(sheet, routeV, routesEdge);
        }
    }

    public void DeleteOnRoute(MouseEventArgs e, List<Vertex> routeV, List<Edge> routesEdge, PictureBox sheet, string route)
    {
        bool flag = false;

        foreach (var stopRoute in Data.StopPoints[route])
        {
            if (Math.Pow((stopRoute.X - e.X / Main.zoom), 2) + Math.Pow((stopRoute.Y - e.Y / Main.zoom), 2) <= Main.G.R * Main.G.R)
            {
                Data.StopPointsInGrids[route].Remove(stopRoute.GridNum);
                Data.StopPoints[route].Remove(stopRoute);
                flag = true;
                break;
            }
        }

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
                break;
            }
        }
        if (!flag)
        {
            for (int i = 0; i < routesEdge.Count; i++)
            {
                if (routesEdge[i].V1 == routesEdge[i].V2)
                {
                    if ((Math.Pow((routeV[routesEdge[i].V1].X - Main.G.R - e.X / Main.zoom), 2) + Math.Pow((routeV[routesEdge[i].V1].Y - Main.G.R - e.Y / Main.zoom), 2) <= ((Main.G.R + 2) * (Main.G.R + 2))) &&
                        (Math.Pow((routeV[routesEdge[i].V1].X - Main.G.R - e.X / Main.zoom), 2) + Math.Pow((routeV[routesEdge[i].V1].Y - Main.G.R - e.Y / Main.zoom), 2) >= ((Main.G.R - 2) * (Main.G.R - 2))))
                    {
                        routesEdge.RemoveAt(i);
                        flag = true;
                        break;
                    }
                }
                else
                {
                    try
                    {
                        if (((e.X / Main.zoom - routeV[routesEdge[i].V1].X) * (routeV[routesEdge[i].V2].Y - routeV[routesEdge[i].V1].Y) / (routeV[routesEdge[i].V2].X - routeV[routesEdge[i].V1].X) + routeV[routesEdge[i].V1].Y) <= (e.Y / Main.zoom + 4) &&
                            ((e.X / Main.zoom - routeV[routesEdge[i].V1].X) * (routeV[routesEdge[i].V2].Y - routeV[routesEdge[i].V1].Y) / (routeV[routesEdge[i].V2].X - routeV[routesEdge[i].V1].X) + routeV[routesEdge[i].V1].Y) >= (e.Y / Main.zoom - 4))
                        {
                            if ((routeV[routesEdge[i].V1].X <= routeV[routesEdge[i].V2].X && routeV[routesEdge[i].V1].X <= e.X / Main.zoom && e.X / Main.zoom <= routeV[routesEdge[i].V2].X) ||
                                (routeV[routesEdge[i].V1].X >= routeV[routesEdge[i].V2].X && routeV[routesEdge[i].V1].X >= e.X / Main.zoom && e.X / Main.zoom >= routeV[routesEdge[i].V2].X))
                            {
                                routesEdge.RemoveAt(i);
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
            MapUpdateRoute(sheet, routeV, routesEdge);
        }
    }

    public void DeleteBS(MouseEventArgs e, List<Vertex> V, List<Edge> E, PictureBox sheet, SerializableDictionary<string, List<Edge>> routesEdgeE)
    {
        foreach (var sp in Data.AllstopPoints)
        {
            if (Math.Pow((sp.X - e.X / Main.zoom), 2) + Math.Pow((sp.Y - e.Y / Main.zoom), 2) <= Main.G.R * Main.G.R)
            {
                Data.AllstopPoints.Remove(sp);
                Main.Flag = true;
                break;
            }
        }


        foreach (var stop in Data.StopPoints)
        {
            foreach (var sp in stop.Value)
            {
                if (Math.Pow((sp.X - e.X / Main.zoom), 2) + Math.Pow((sp.Y - e.Y / Main.zoom), 2) <= Main.G.R * Main.G.R)
                {
                    Data.StopPointsInGrids[stop.Key].Remove(sp.GridNum);
                    stop.Value.Remove(sp);
                    Main.Flag = true;
                    break;
                }
            }
        }

    }
    public void Delete(MouseEventArgs e, List<Vertex> V, List<Edge> E, PictureBox sheet, SerializableDictionary<string, List<Edge>> routesEdgeE)
    {
        Main.Flag = false;

        foreach (var tl in Data.TraficLights)
        {
            if (Math.Pow((tl.X - e.X / Main.zoom), 2) + Math.Pow((tl.Y - e.Y / Main.zoom), 2) <= Main.G.R * Main.G.R)
            {
                tl.Stop();
                Data.TraficLightsInGrids.Remove(tl.GridNum);
                Data.TraficLights.Remove(tl);
                Main.Flag = true;
                break;
            }
        }

        if (!Main.Flag)
        {
            foreach (var sp in Data.AllstopPoints)
            {
                if (Math.Pow((sp.X - e.X / Main.zoom), 2) + Math.Pow((sp.Y - e.Y / Main.zoom), 2) <= Main.G.R * Main.G.R)
                {
                    Data.AllstopPoints.Remove(sp);
                    Main.Flag = true;
                    break;
                }
            }
        }


        foreach (var stop in Data.StopPoints)
        {
            foreach (var sp in stop.Value)
            {
                if (Math.Pow((sp.X - e.X / Main.zoom), 2) + Math.Pow((sp.Y - e.Y / Main.zoom), 2) <= Main.G.R * Main.G.R)
                {
                    Data.StopPointsInGrids[stop.Key].Remove(sp.GridNum);
                    stop.Value.Remove(sp);
                    Main.Flag = true;
                    break;
                }
            }
        }


        if (!Main.Flag)
        {
            foreach (var routeV in Data.Routes)
            {
                for (int i = 0; i < routeV.Value.Count; i++)
                {
                    if (Math.Pow((routeV.Value[i].X - e.X / Main.zoom), 2) + Math.Pow((routeV.Value[i].Y - e.Y / Main.zoom), 2) <= Main.G.R * Main.G.R)
                    {
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
                        Main.Flag = true;
                        break;
                    }

                }
            }
        }
        if (!Main.Flag)
        {
            try
            {
                foreach (var routeV in Data.Routes)
                {
                    for (int j = 0; j < routeV.Value.Count; j++)
                    {
                        for (int i = 0; i < routesEdgeE[routeV.Key].Count; i++)
                        {
                            if (routesEdgeE[routeV.Key][i].V1 == routesEdgeE[routeV.Key][i].V2)
                            {
                                if ((Math.Pow((routeV.Value[routesEdgeE[routeV.Key][i].V1].X - Main.G.R - e.X / Main.zoom), 2) + Math.Pow((routeV.Value[routesEdgeE[routeV.Key][i].V1].Y - Main.G.R - e.Y / Main.zoom), 2) <= ((Main.G.R + 2) * (Main.G.R + 2))) &&
                                    (Math.Pow((routeV.Value[routesEdgeE[routeV.Key][i].V1].X - Main.G.R - e.X / Main.zoom), 2) + Math.Pow((routeV.Value[routesEdgeE[routeV.Key][i].V1].Y - Main.G.R - e.Y / Main.zoom), 2) >= ((Main.G.R - 2) * (Main.G.R - 2))))
                                {
                                    routesEdgeE[routeV.Key].RemoveAt(i);
                                    Main.Flag = true;
                                    break;
                                }
                            }
                            else
                            {
                                if (((e.X / Main.zoom - routeV.Value[routesEdgeE[routeV.Key][i].V1].X) * (routeV.Value[routesEdgeE[routeV.Key][i].V2].Y - routeV.Value[routesEdgeE[routeV.Key][i].V1].Y) / (routeV.Value[routesEdgeE[routeV.Key][i].V2].X - routeV.Value[routesEdgeE[routeV.Key][i].V1].X) + routeV.Value[routesEdgeE[routeV.Key][i].V1].Y) <= (e.Y / Main.zoom + 4) &&
                                    ((e.X / Main.zoom - routeV.Value[routesEdgeE[routeV.Key][i].V1].X) * (routeV.Value[routesEdgeE[routeV.Key][i].V2].Y - routeV.Value[routesEdgeE[routeV.Key][i].V1].Y) / (routeV.Value[routesEdgeE[routeV.Key][i].V2].X - routeV.Value[routesEdgeE[routeV.Key][i].V1].X) + routeV.Value[routesEdgeE[routeV.Key][i].V1].Y) >= (e.Y / Main.zoom - 4))
                                {
                                    if ((routeV.Value[routesEdgeE[routeV.Key][i].V1].X <= routeV.Value[routesEdgeE[routeV.Key][i].V2].X && routeV.Value[routesEdgeE[routeV.Key][i].V1].X <= e.X / Main.zoom && e.X / Main.zoom <= routeV.Value[routesEdgeE[routeV.Key][i].V2].X) ||
                                        (routeV.Value[routesEdgeE[routeV.Key][i].V1].X >= routeV.Value[routesEdgeE[routeV.Key][i].V2].X && routeV.Value[routesEdgeE[routeV.Key][i].V1].X >= e.X / Main.zoom && e.X / Main.zoom >= routeV.Value[routesEdgeE[routeV.Key][i].V2].X))
                                    {
                                        routesEdgeE[routeV.Key].RemoveAt(i);
                                        Main.Flag = true;
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
                Main.Flag = true;
                break;

            }

        }
        if (!Main.Flag)
        {
            for (int i = 0; i < E.Count; i++)
            {
                if (E[i].V1 == E[i].V2)
                {
                    if ((Math.Pow((V[E[i].V1].X - Main.G.R - e.X / Main.zoom), 2) + Math.Pow((V[E[i].V1].Y - Main.G.R - e.Y / Main.zoom), 2) <= ((Main.G.R + 2) * (Main.G.R + 2))) &&
                        (Math.Pow((V[E[i].V1].X - Main.G.R - e.X / Main.zoom), 2) + Math.Pow((V[E[i].V1].Y - Main.G.R - e.Y / Main.zoom), 2) >= ((Main.G.R - 2) * (Main.G.R - 2))))
                    {
                        E.RemoveAt(i);
                        Main.Flag = true;
                        break;
                    }
                }
                else
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
                                Main.Flag = true;
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

    public void DeleteRoute(MouseEventArgs e, List<Vertex> routeV, List<Edge> routesEdge, PictureBox sheet)
    {
        bool flag = false;
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
                break;
            }
        }
        if (!flag)
        {
            for (int i = 0; i < routesEdge.Count; i++)
            {
                if (routesEdge[i].V1 == routesEdge[i].V2)
                {
                    if ((Math.Pow((routeV[routesEdge[i].V1].X - Main.G.R - e.X), 2) + Math.Pow((routeV[routesEdge[i].V1].Y - Main.G.R - e.Y), 2) <= ((Main.G.R + 2) * (Main.G.R + 2))) &&
                        (Math.Pow((routeV[routesEdge[i].V1].X - Main.G.R - e.X), 2) + Math.Pow((routeV[routesEdge[i].V1].Y - Main.G.R - e.Y), 2) >= ((Main.G.R - 2) * (Main.G.R - 2))))
                    {
                        routesEdge.RemoveAt(i);
                        flag = true;
                        break;
                    }
                }
                else
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
                                Main.Flag = true;
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
        if (flag)
        {
            MapUpdateRoute(sheet, routeV, routesEdge);
        }
    }

}


}
