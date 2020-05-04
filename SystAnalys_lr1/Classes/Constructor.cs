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

        public async void AsSelect(MouseEventArgs e, List<Vertex> V, List<Edge> E, DrawGraph G, PictureBox sheet, int n = 0)
        {
            await Task.Run(() => Select(e, V, E, G, sheet, n));
        }

        public async void AsDrawEdge(MouseEventArgs e, List<Vertex> V, List<Edge> E, DrawGraph G, PictureBox sheet, int n)
        {
            await Task.Run(() => DrawEdge(e, V, E, G, sheet, n));
        }


        public async void AsDelete(MouseEventArgs e, List<Vertex> V, List<Edge> E, PictureBox sheet, DrawGraph G, SerializableDictionary<string, List<Edge>> routesEdgeE)
        {
            await Task.Run(() => delete(e, V, E, sheet, G, routesEdgeE));
            if (Main.flag)
            {
                G.ClearSheet();
                G.DrawALLGraph(V, E);
                sheet.Invoke(new Del((s) => sheet.Image = s), G.GetBitmap());
                Main.DrawGrid();
            }
        }

        public async void AsDeleteRoute(MouseEventArgs e, List<Vertex> routeV, List<Edge> routesEdge, PictureBox sheet, DrawGraph G)
        {
            await Task.Run(() => deleteRoute(e, routeV, routesEdge, sheet, G));

        }
        public void AddStopPoints(MouseEventArgs e, List<Vertex> allstopPoints, PictureBox sheet, DrawGraph G, List<GridPart> gridParts)
        {
            foreach (var gridPart in gridParts)
            {
                if (((e.X > gridPart.x * Main.zoom) && (e.Y > gridPart.y * Main.zoom)) && ((e.X < gridPart.x * Main.zoom + GridPart.Width * Main.zoom) && (e.Y < gridPart.y * Main.zoom + GridPart.Height * Main.zoom)))
                {
                    allstopPoints.Add(new Vertex(e.X / Main.zoom, e.Y / Main.zoom));
                    G.DrawStopVertex(e.X / Main.zoom, e.Y / Main.zoom);
                    sheet.Image = G.GetBitmap();
                    Main.DrawGrid();
                    break;
                }
            }
        }
        public void SelectRoute(MouseEventArgs e, List<Vertex> V, List<Edge> E, DrawGraph G, PictureBox sheet, Constructor c, List<int> selected, bool check)
        {
            if (!check)
            {
                c.DrawVertex(e, V, G, sheet);
            }

            for (int i = 0; i < V.Count; i++)
            {
                if (Math.Pow((V[i].X - e.X / Main.zoom), 2) + Math.Pow((V[i].Y - e.Y / Main.zoom), 2) <= G.R * G.R)
                {
                    if (selected.Count == 0)
                    {
                        selected.Add(i);
                        G.DrawSelectedVertex(V[i].X, V[i].Y);
                        sheet.Image = G.GetBitmap();
                        break;
                    }
                    else
                    {
                        selected.Add(i);
                        E.Add(new Edge(selected[0], selected[1]));
                        G.DrawEdge(V[selected[0]], V[selected[1]], E[E.Count - 1], 1);
                        selected[0] = selected[1];
                        selected.Remove(selected[1]);
                        G.ClearSheet();
                        G.DrawALLGraph(V, E);
                        sheet.Image = G.GetBitmap();
                        Main.DrawGrid();
                        G.DrawSelectedVertex(V[i].X, V[i].Y);
                        break;
                    }

                }
            }
        }


        public void SelectRouteInRoute(MouseEventArgs e, List<Vertex> routeV, List<Edge> routesEdge, DrawGraph G, PictureBox sheet, List<int> selected)
        {
            bool select = true;
            for (int i = 0; i < Main.V.Count; i++)
            {
                if (Math.Pow((Main.V[i].X - e.X / Main.zoom), 2) + Math.Pow((Main.V[i].Y - e.Y / Main.zoom), 2) <= G.R * G.R)
                {
                    if (selected.Count == 0)
                    {
                        if (routesEdge.Count != 0)
                        {
                            if (routeV[routesEdge.Last().V2].X == Main.V[i].X && routeV[routesEdge.Last().V2].Y == Main.V[i].Y)
                            {
                                selected.Add(i);
                                G.DrawSelectedVertex(Main.V[i].X, Main.V[i].Y);
                            }
                        }
                        else
                        {
                            if (!routeV.Contains(new Vertex(Main.V[i].X, Main.V[i].Y)))
                            {
                                routeV.Add(new Vertex(Main.V[i].X, Main.V[i].Y));
                            }
                            selected.Add(i);
                            G.DrawSelectedVertex(Main.V[i].X, Main.V[i].Y);
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
                                G.ClearSheet();
                                G.DrawALLGraph(Main.V, Main.E);
                                G.DrawALLGraph(routeV, routesEdge, 1);
                                sheet.Image = G.GetBitmap();
                                Main.DrawGrid();
                                select = true;
                                G.DrawSelectedVertex(Main.V[i].X, Main.V[i].Y);
                                break;
                            }
                            select = false;
                        }
                        if (!select)
                        {
                            G.ClearSheet();
                            G.DrawALLGraph(Main.V, Main.E);
                            G.DrawALLGraph(routeV, routesEdge, 1);
                            sheet.Image = G.GetBitmap();
                            Main.DrawGrid();
                        }
                        if (routeV.Contains(new Vertex(Main.V[i].X, Main.V[i].Y)))
                            G.DrawSelectedVertex(Main.V[i].X, Main.V[i].Y);

                    }
                    selected[0] = selected[1];
                    selected.Remove(selected[1]);

                }
            }

        }

        public void Select(MouseEventArgs e, List<Vertex> V, List<Edge> E, DrawGraph G, PictureBox sheet, int n = 0)
        {
            for (int i = 0; i < V.Count; i++)
            {
                if (Math.Pow((V[i].X - e.X / Main.zoom), 2) + Math.Pow((V[i].Y - e.Y / Main.zoom), 2) <= G.R * G.R)
                {
                    if (Main.selected1 != -1)
                    {
                        Main.selected1 = -1;
                        G.ClearSheet();
                        if (n != 0) G.DrawALLGraph(Main.V, Main.E);
                        G.DrawALLGraph(V, E, n);
                        //  sheet.Image = G.GetBitmap();
                        sheet.Invoke(new Del((s) => sheet.Image = s), G.GetBitmap());
                        Main.DrawGrid();
                    }
                    if (Main.selected1 == -1)
                    {
                        G.DrawSelectedVertex(V[i].X, V[i].Y);
                        Main.selected1 = i;
                        sheet.Invoke(new Del((s) => sheet.Image = s), G.GetBitmap());
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


        public void DrawVertex(MouseEventArgs e, List<Vertex> V, DrawGraph G, PictureBox sheet)
        {
            V.Add(new Vertex(e.X / Main.zoom, e.Y / Main.zoom));
            G.DrawVertex(e.X / Main.zoom, e.Y / Main.zoom);
            sheet.Image = G.GetBitmap();
            Main.DrawGrid();

        }

        public void DrawEdge(MouseEventArgs e, List<Vertex> V, List<Edge> E, DrawGraph G, PictureBox sheet, int n)
        {
            if (e.Button == MouseButtons.Left)
            {
                for (int i = 0; i < V.Count; i++)
                {
                    if (Math.Pow((V[i].X - e.X / Main.zoom), 2) + Math.Pow((V[i].Y - e.Y / Main.zoom), 2) <= G.R * G.R)
                    {
                        if (Main.selected1 == -1)
                        {
                            G.DrawSelectedVertex(V[i].X, V[i].Y);
                            Main.selected1 = i;
                            sheet.Invoke(new Del((s) => sheet.Image = s), G.GetBitmap());
                            break;
                        }
                        if (Main.selected2 == -1)
                        {
                            G.DrawSelectedVertex(V[i].X, V[i].Y);
                            Main.selected2 = i;
                            E.Add(new Edge(Main.selected1, Main.selected2));
                            G.DrawEdge(V[Main.selected1], V[Main.selected2], E[E.Count - 1], n);
                            Main.selected1 = -1;
                            Main.selected2 = -1;
                            G.ClearSheet();
                            if (n != 0) G.DrawALLGraph(Main.V, Main.E);
                            G.DrawALLGraph(V, E, n);
                            Main.DrawGrid();
                            sheet.Invoke(new Del((s) => sheet.Image = s), G.GetBitmap());
                            break;
                        }
                    }
                }
            }
        }

        public void deleteTF(MouseEventArgs e, List<Vertex> V, List<Edge> E, PictureBox sheet, DrawGraph G, SerializableDictionary<string, List<Edge>> routesEdgeE)
        {
            foreach (var tl in Main.traficLights)
            {
                if (Math.Pow((tl.x - e.X / Main.zoom), 2) + Math.Pow((tl.y - e.Y / Main.zoom), 2) <= G.R * G.R)
                {
                    tl.Stop();
                    Main.TraficLightsInGrids.Remove(tl.gridNum);
                    Main.traficLights.Remove(tl);
                    Main.flag = true;
                    break;
                }
            }
        }
        public void deleteVE(MouseEventArgs e, List<Vertex> V, List<Edge> E, PictureBox sheet, DrawGraph G, SerializableDictionary<string, List<Edge>> routesEdgeE)
        {
            if (!Main.flag)
            {
                foreach (var routeV in Main.routes)
                {
                    for (int i = 0; i < routeV.Value.Count; i++)
                    {
                        if (Math.Pow((routeV.Value[i].X - e.X / Main.zoom), 2) + Math.Pow((routeV.Value[i].Y - e.Y / Main.zoom), 2) <= G.R * G.R)
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
                                    if ((Math.Pow((routeV.Value[routesEdgeE[routeV.Key][i].V1].X - G.R - e.X / Main.zoom), 2) + Math.Pow((routeV.Value[routesEdgeE[routeV.Key][i].V1].Y - G.R - e.Y / Main.zoom), 2) <= ((G.R + 2) * (G.R + 2))) &&
                                        (Math.Pow((routeV.Value[routesEdgeE[routeV.Key][i].V1].X - G.R - e.X / Main.zoom), 2) + Math.Pow((routeV.Value[routesEdgeE[routeV.Key][i].V1].Y - G.R - e.Y / Main.zoom), 2) >= ((G.R - 2) * (G.R - 2))))
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
                if (Math.Pow((V[i].X - e.X / Main.zoom), 2) + Math.Pow((V[i].Y - e.Y / Main.zoom), 2) <= G.R * G.R)
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
                        if ((Math.Pow((V[E[i].V1].X - G.R - e.X / Main.zoom), 2) + Math.Pow((V[E[i].V1].Y - G.R - e.Y / Main.zoom), 2) <= ((G.R + 2) * (G.R + 2))) &&
                            (Math.Pow((V[E[i].V1].X - G.R - e.X / Main.zoom), 2) + Math.Pow((V[E[i].V1].Y - G.R - e.Y / Main.zoom), 2) >= ((G.R - 2) * (G.R - 2))))
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
        public void deleteBS(MouseEventArgs e, List<Vertex> V, List<Edge> E, PictureBox sheet, DrawGraph G, SerializableDictionary<string, List<Edge>> routesEdgeE)
        {

            foreach (var sp in Main.allstopPoints)
            {
                if (Math.Pow((sp.X - e.X / Main.zoom), 2) + Math.Pow((sp.Y - e.Y / Main.zoom), 2) <= G.R * G.R)
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
                    if (Math.Pow((sp.X - e.X / Main.zoom), 2) + Math.Pow((sp.Y - e.Y / Main.zoom), 2) <= G.R * G.R)
                    {
                        Main.stopPointsInGrids[stop.Key].Remove(sp.gridNum);
                        stop.Value.Remove(sp);
                        Main.flag = true;
                        break;
                    }
                }
            }

        }
        public void delete(MouseEventArgs e, List<Vertex> V, List<Edge> E, PictureBox sheet, DrawGraph G, SerializableDictionary<string, List<Edge>> routesEdgeE)
        {
            //удалили ли что-нибудь по ЭТОМУ клику
            //ищем, возможно была нажата вершина
            Main.flag = false;

            foreach (var tl in Main.traficLights)
            {
                if (Math.Pow((tl.x - e.X / Main.zoom), 2) + Math.Pow((tl.y - e.Y / Main.zoom), 2) <= G.R * G.R)
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
                    if (Math.Pow((sp.X - e.X / Main.zoom), 2) + Math.Pow((sp.Y - e.Y / Main.zoom), 2) <= G.R * G.R)
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
                    if (Math.Pow((sp.X - e.X / Main.zoom), 2) + Math.Pow((sp.Y - e.Y / Main.zoom), 2) <= G.R * G.R)
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
                        if (Math.Pow((routeV.Value[i].X - e.X / Main.zoom), 2) + Math.Pow((routeV.Value[i].Y - e.Y / Main.zoom), 2) <= G.R * G.R)
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
                                    if ((Math.Pow((routeV.Value[routesEdgeE[routeV.Key][i].V1].X - G.R - e.X / Main.zoom), 2) + Math.Pow((routeV.Value[routesEdgeE[routeV.Key][i].V1].Y - G.R - e.Y / Main.zoom), 2) <= ((G.R + 2) * (G.R + 2))) &&
                                        (Math.Pow((routeV.Value[routesEdgeE[routeV.Key][i].V1].X - G.R - e.X / Main.zoom), 2) + Math.Pow((routeV.Value[routesEdgeE[routeV.Key][i].V1].Y - G.R - e.Y / Main.zoom), 2) >= ((G.R - 2) * (G.R - 2))))
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
                if (Math.Pow((V[i].X - e.X / Main.zoom), 2) + Math.Pow((V[i].Y - e.Y / Main.zoom), 2) <= G.R * G.R)
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
                        if ((Math.Pow((V[E[i].V1].X - G.R - e.X / Main.zoom), 2) + Math.Pow((V[E[i].V1].Y - G.R - e.Y / Main.zoom), 2) <= ((G.R + 2) * (G.R + 2))) &&
                            (Math.Pow((V[E[i].V1].X - G.R - e.X / Main.zoom), 2) + Math.Pow((V[E[i].V1].Y - G.R - e.Y / Main.zoom), 2) >= ((G.R - 2) * (G.R - 2))))
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

        public void deleteRoute(MouseEventArgs e, List<Vertex> routeV, List<Edge> routesEdge, PictureBox sheet, DrawGraph G)
        {
            bool flag = false; //удалили ли что-нибудь по ЭТОМУ клику
                               //ищем, возможно была нажата вершина
            for (int i = 0; i < routeV.Count; i++)
            {
                if (Math.Pow((routeV[i].X - e.X / Main.zoom), 2) + Math.Pow((routeV[i].Y - e.Y / Main.zoom), 2) <= G.R * G.R)
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
                        if ((Math.Pow((routeV[routesEdge[i].V1].X - G.R - e.X), 2) + Math.Pow((routeV[routesEdge[i].V1].Y - G.R - e.Y), 2) <= ((G.R + 2) * (G.R + 2))) &&
                            (Math.Pow((routeV[routesEdge[i].V1].X - G.R - e.X), 2) + Math.Pow((routeV[routesEdge[i].V1].Y - G.R - e.Y), 2) >= ((G.R - 2) * (G.R - 2))))
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
                G.ClearSheet();
                G.DrawALLGraph(Main.V, Main.E);
                G.DrawALLGraph(routeV, routesEdge, 1);
                sheet.Invoke(new Del((s) => sheet.Image = s), G.GetBitmap());
                Main.DrawGrid();
            }
        }

    }
}
