using MetroFramework.Forms;
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
    public partial class Main : MetroForm
    {

        //функция, которая создает все координаты для одного маршрута
        private void CreateOneRouteCoordinates(string i)
        {
            if (routes[i].Count >= 2)
            {
                AllCoordinates[i] = new List<Point>();
                AllGridsInRoutes[i] = new List<int>();
                AllCoordinates[i].AddRange(getPoints(routes[i], i));

            }
            Bus.SetScrollX(mainPanel.AutoScrollPosition.X);
            Bus.SetScrollY(mainPanel.AutoScrollPosition.Y);


            foreach (var bus in buses)
            {
                bus.SetAllCoordinates(AllCoordinates[bus.GetRoute()]);
            }

        }
        private async void AsyncCreateAllCoordinates()
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
            foreach (var gridpart in TheGrid)
            {
                if ((points.Last().X > gridpart.x) && ((points.Last().X) < gridpart.x + GridPart.Width) && ((points.Last().Y) > gridpart.y) && ((points.Last().Y) < (gridpart.y + GridPart.Height)))
                {

                    Locate = TheGrid.IndexOf(gridpart);
                }
            }
            for (int k = 0; k < TheGrid.Count; k++)
            {
                if (Locate == k)
                {
                    if (LastLocate != Locate)
                    {
                        AllGridsInRoutes[route].Add(k);

                        LastLocate = Locate;
                    }
                }
            }
       
        }

    


        //функция, которая создает все координаты для всех маршрутов
        private void CreateAllCoordinates()
        {

            AllCoordinates = new SerializableDictionary<string, List<Point>>();
            AllGridsInRoutes = new SerializableDictionary<string, List<int>>();
            for (int i = 0; i < routes.Count; i++)
            {
                AllCoordinates.Add(routes.ElementAt(i).Key, new List<Point>());
                AllGridsInRoutes.Add(routes.ElementAt(i).Key, new List<int>());
                if (routes.ElementAt(i).Value.Count >= 2)
                {
                    AllCoordinates[AllCoordinates.ElementAt(i).Key].AddRange(getPoints(routes.ElementAt(i).Value, routes.ElementAt(i).Key));
                }
                Bus.SetScrollX(mainPanel.AutoScrollPosition.X);
                Bus.SetScrollY(mainPanel.AutoScrollPosition.Y);

            }

            foreach (var bus in buses)
            {
                bus.SetAllCoordinates(AllCoordinates[bus.GetRoute()]);
            }



        }

    }
}
