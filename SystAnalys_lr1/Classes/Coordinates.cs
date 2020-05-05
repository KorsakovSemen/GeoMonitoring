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

        //функция, которая создает все координаты для одного маршрута
        public void CreateOneRouteCoordinates(string i)
        {
            if (Main.routes[i].Count >= 2)
            {
                Main.AllCoordinates[i] = new List<Point>();
                Main.AllGridsInRoutes[i] = new List<int>();
                Main.AllCoordinates[i].AddRange(getPoints(Main.routes[i], i));

            }
            Bus.SetScrollX(Main.scrollX);
            Bus.SetScrollY(Main.scrollY);


            foreach (var bus in Main.buses)
            {
                bus.SetAllCoordinates(Main.AllCoordinates[bus.GetRoute()]);
            }

        }
        public async void AsyncCreateAllCoordinates()
        {
            await Task.Run(() =>
            {
                CreateAllCoordinates();
            });
        }

        public List<Point> getPoints(List<Vertex> routeVertexes, string route)
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
                    int quantity = (int)GetDistance(p1.X, p1.Y, p2.X, p2.Y);
                    for (double j = 0; j < quantity; j++)
                    {
                        y = slope == 0 ? 0 : ydiff * (j / quantity);
                        x = slope == 0 ? xdiff * (j / quantity) : y / slope;
                        points.Add(new Point((int)Math.Round(x) + p1.X, (int)Math.Round(y) + p1.Y));
                        if (RectCheckCount == 10)
                        {
                            RectCheckCount = 0;
                            getOneRouteGrids(points, route);
                        }
                        else
                        {
                            RectCheckCount++;
                        }

                    }
                    points.Add(p2);
                    getOneRouteGrids(points, route);
                }
            }
            return points;
        }
        public void getOneRouteGrids(List<Point> points,string route)
        {
            int Locate = 0;
            int LastLocate = 0;
            foreach (var gridpart in Main.TheGrid)
            {
                if ((points.Last().X > gridpart.x) && ((points.Last().X) < gridpart.x + GridPart.Width) && ((points.Last().Y) > gridpart.y) && ((points.Last().Y) < (gridpart.y + GridPart.Height)))
                {

                    Locate = Main.TheGrid.IndexOf(gridpart);
                }
            }
            for (int k = 0; k < Main.TheGrid.Count; k++)
            {
                if (Locate == k)
                {
                    if (LastLocate != Locate)
                    {
                        Main.AllGridsInRoutes[route].Add(k);

                        LastLocate = Locate;
                    }
                }
            }
       
        }
    


        //функция, которая создает все координаты для всех маршрутов
        public void CreateAllCoordinates()
        {

            Main.AllCoordinates = new SerializableDictionary<string, List<Point>>();
            Main.AllGridsInRoutes = new SerializableDictionary<string, List<int>>();
            for (int i = 0; i < Main.routes.Count; i++)
            {
                Main.AllCoordinates.Add(Main.routes.ElementAt(i).Key, new List<Point>());
                Main.AllGridsInRoutes.Add(Main.routes.ElementAt(i).Key, new List<int>());
                if (Main.routes.ElementAt(i).Value.Count >= 2)
                {
                    Main.AllCoordinates[Main.AllCoordinates.ElementAt(i).Key].AddRange(getPoints(Main.routes.ElementAt(i).Value, Main.routes.ElementAt(i).Key));
                }
                Bus.SetScrollX(Main.scrollX);
                Bus.SetScrollY(Main.scrollY);

            }

            foreach (var bus in Main.buses)
            {
                bus.SetAllCoordinates(Main.AllCoordinates[bus.GetRoute()]);
            }



        }

    }
}
