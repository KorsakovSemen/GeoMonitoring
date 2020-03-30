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
        private void CreateOneRouteCoordinates(int i)
        {
            if (routes[i].Count > 2)
            {
                AllCoordinates[i] = new List<Point>();
                AllGridsInRoutes[i] = new List<int>();
                PictureBox bus = new PictureBox();
                bus.Location = new System.Drawing.Point(routes[i][0].x, routes[i][0].y);

                bus.Size = new System.Drawing.Size(25, 25);

                buses.Add(new Bus(routes[i], bus, 0, false, i, false));


                while (buses.Last().TurnBack == false)
                {
                    buses.Last().MoveForCoordinates();
                    buses.Last().DetectRectangle();
                    if (buses.Last().grids != null)
                    {
                        for (int k = 0; k < TheGrid.Count; k++)
                        {
                            if (buses.Last().getLocate() == k)
                            {
                                if (buses.Last().lastLocate != buses.Last().Locate)
                                {

                                    AllGridsInRoutes[i].Add(k);
                                    buses.Last().lastLocate = buses.Last().Locate;
                                }
                            }
                        }
                    }
                    AllCoordinates[i].Add(new Point((int)buses.Last().x, (int)buses.Last().y));
                }
                buses.Remove(buses.Last());
            }
            Bus.SetScrollX(mainPanel.AutoScrollPosition.X);
            Bus.SetScrollY(mainPanel.AutoScrollPosition.Y);
            CreatePollutionInRoutes();

        }
        private async void AsyncCreateAllCoordinates()
        {
            label9.Visible = true;
            label9.Text = "ТИШЕ ТИШЕ ТИШЕ";
            await Task.Run(() =>
            {
                buttonOff();
                CreateAllCoordinates();
                buttonOn();
            });
            label9.Text = "Закрывай";
            label9.Visible = false;
        }
        Mutex mutex = new Mutex();
        //функция, которая создает все координаты для каждого маршрута
        private void CreateAllCoordinates()
        {
        //    label9.Text = "ТИШЕ ТИШЕ ТИШЕ";
            //  progressBar1.Visible = true;
            //try { 
            AllCoordinates = new SerializableDictionary<int, List<Point>>();
            AllGridsInRoutes = new SerializableDictionary<int, List<int>>();
            for (int i = 0; i < routes.Count; i++)
            {
                AllCoordinates.Add(routes.ElementAt(i).Key, new List<Point>());
                AllGridsInRoutes.Add(routes.ElementAt(i).Key, new List<int>());
                if (routes.ElementAt(i).Value.Count != 0)
                {
                    PictureBox bus = new PictureBox();
                    bus.Location = new System.Drawing.Point(routes.ElementAt(i).Value[0].x, routes.ElementAt(i).Value[0].y);

                    bus.Size = new System.Drawing.Size(1, 1);
                    buses.Add(new Bus(routes[routes.ElementAt(i).Key], bus, 0, false, routes.ElementAt(i).Key, false));


                    while (buses.Last().TurnBack == false)
                    {
                        buses.Last().MoveForCoordinates();
                        buses.Last().DetectRectangle();
                        if (buses.Last().grids != null)
                        {
                            Parallel.For(0, TheGrid.Count, (k) =>
                            {
                                if (buses.Last().getLocate() == k)
                                {
                                    if (buses.Last().lastLocate != buses.Last().Locate)
                                    {
                                        AllGridsInRoutes[AllCoordinates.ElementAt(i).Key].Add(k);
                                        buses.Last().lastLocate = buses.Last().Locate;
                                    }
                                }
                            });
                        }
                        //lock (AllCoordinates)
                        //{
                        //  buses.Last().mutex.WaitOne();
                        //await Task.Delay(10);
                        AllCoordinates[AllCoordinates.ElementAt(i).Key].Add(new Point((int)buses.Last().x, (int)buses.Last().y));//ошипка                   

                     //   buses.Last().mutex.ReleaseMutex();
                        //}


                    }
                    buses.Remove(buses.Last());

                }
                Bus.SetScrollX(mainPanel.AutoScrollPosition.X);
                Bus.SetScrollY(mainPanel.AutoScrollPosition.Y);
                CreatePollutionInRoutes();
            }
            //}
            //    catch
            //    {
            //       // MessageBox.Show("быстрый быстрый");
            //    }.
          //  label9.Text = "Закрывай";
        }

    }
}
