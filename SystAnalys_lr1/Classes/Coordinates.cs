//This is a personal academic project. Dear PVS-Studio, please check it.
//PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using MetroFramework.Forms;
using SystAnalys_lr1.Classes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystAnalys_lr1
{
    public class Coordinates
    {
        public double GetDistance(double x1, double y1, double x2, double y2)
        {
            return (int)Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }

        public void CreateOneRouteCoordinates(string i)
        {
            if (Data.Routes[i].Count >= 2)
            {
                Data.AllCoordinates[i] = new List<Point>();
                Data.AllGridsInRoutes[i] = new List<int>();
                Data.AllCoordinates[i].AddRange(GetPoints(Data.Routes[i], i));

            }
            Bus.ScrollX = Main.scrollX;
            Bus.ScrollY = Main.scrollY;

            try
            {
                foreach (var bus in Data.Buses)
                {
                    bus.Coordinates = Data.AllCoordinates[bus.GetRoute()];
                }
            }
            catch (Exception)
            {

                CreateAllCoordinates();
                foreach (var bus in Data.Buses)
                {
                    bus.Coordinates = Data.AllCoordinates[bus.GetRoute()];
                }
            }
         

        }
        public async void AsyncCreateAllCoordinates()
        {
            await Task.Run(() =>
            {
                CreateAllCoordinates();
            });
        }

        public List<Point> GetPoints(List<Vertex> routeVertexes, string route)
        {
            var points = new List<Point>();
            int RectCheckCount = 0;
     
            if (routeVertexes.Count > 1)
            {
                for (int i = 0; i < routeVertexes.Count-1; i++)
                {
                    Point p1 = new Point(routeVertexes[i].X, routeVertexes[i].Y);
                    Point p2 = new Point(routeVertexes[i + 1].X, routeVertexes[i + 1].Y);
                    int ydiff = p2.Y - p1.Y, xdiff = p2.X - p1.X;
                    double slope = (double)(p2.Y - p1.Y) / (p2.X - p1.X);
                    double x, y;
                    int quantity = (int)GetDistance(p1.X, p1.Y, p2.X, p2.Y)*2;
                    for (double j = 0; j < quantity; j++)
                    {
                        y = slope == 0 ? 0 : ydiff * (j / quantity);
                        x = slope == 0 ? xdiff * (j / quantity) : y / slope;
                        points.Add(new Point((int)Math.Round(x) + p1.X, (int)Math.Round(y) + p1.Y));
                        if (RectCheckCount == 10)
                        {
                            RectCheckCount = 0;
                            GetOneRouteGrids(points, route);
                        }
                        else
                        {
                            RectCheckCount++;
                        }

                    }
                    points.Add(p2);
                    GetOneRouteGrids(points, route);
                }
            }
            return points;
        }
        public void GetOneRouteGrids(List<Point> points,string route)
        {
            int Locate = 0;
            int LastLocate = 0;
            foreach (var gridpart in Data.TheGrid)
            {
                if ((points.Last().X > gridpart.X) && ((points.Last().X) < gridpart.X + GridPart.Width) && ((points.Last().Y) > gridpart.Y) && ((points.Last().Y) < (gridpart.Y + GridPart.Height)))
                {

                    Locate = Data.TheGrid.IndexOf(gridpart);
                }
            }
            for (int k = 0; k < Data.TheGrid.Count; k++)
            {
                if (Locate == k)
                {
                    if (LastLocate != Locate)
                    {
                        Data.AllGridsInRoutes[route].Add(k);

                        LastLocate = Locate;
                    }
                }
            }       
        }    


        //функция, которая создает все координаты для всех маршрутов
        public void CreateAllCoordinates()
        {
            Data.AllCoordinates = new SerializableDictionary<string, List<Point>>();
            Data.AllGridsInRoutes = new SerializableDictionary<string, List<int>>();
            for (int i = 0; i < Data.Routes.Count; i++)
            {
                Data.AllCoordinates.Add(Data.Routes.ElementAt(i).Key, new List<Point>());
                Data.AllGridsInRoutes.Add(Data.Routes.ElementAt(i).Key, new List<int>());
                if (Data.Routes.ElementAt(i).Value.Count >= 2)
                {
                    Data.AllCoordinates[Data.AllCoordinates.ElementAt(i).Key].AddRange(GetPoints(Data.Routes.ElementAt(i).Value, Data.Routes.ElementAt(i).Key));
                }
                Bus.ScrollX = Main.scrollX;
                Bus.ScrollY = Main.scrollY;

            }

            foreach (var bus in Data.Buses)
            {
                bus.Coordinates = Data.AllCoordinates[bus.GetRoute()];
            }


        }

    }
}
