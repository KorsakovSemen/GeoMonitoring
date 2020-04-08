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
            if (routes[i].Count > 2)
            {
                AllCoordinates[i] = new List<Point>();
                AllGridsInRoutes[i] = new List<int>();
                PictureBox bus = new PictureBox();
                bus.Location = new System.Drawing.Point(routes[i][0].x, routes[i][0].y);

                bus.Size = new System.Drawing.Size(25, 25);

               Bus onebus = new Bus(routes[i], bus, 0, false, i, false);


                while (onebus.TurnBack == false)
                {
                    onebus.MoveForCoordinates();
                    onebus.DetectRectangle();
                    if (onebus.grids != null)
                    {
                        for (int k = 0; k < TheGrid.Count; k++)
                        {
                            if (onebus.getLocate() == k)
                            {
                                if (onebus.lastLocate != onebus.Locate)
                                {

                                    AllGridsInRoutes[i].Add(k);
                                    onebus.lastLocate = onebus.Locate;
                                }
                            }
                        }
                    }
                    AllCoordinates[i].Add(new Point((int)onebus.x, (int)onebus.y));
                }
                onebus = new Bus();
            }
            Bus.SetScrollX(mainPanel.AutoScrollPosition.X);
            Bus.SetScrollY(mainPanel.AutoScrollPosition.Y);
            CreatePollutionInRoutes();

        }
        private async void AsyncCreateAllCoordinates()
        {
            //label9.Visible = true;
            //label9.Text = "ТИШЕ ТИШЕ ТИШЕ";


    
            ////?/
            //     buttonOff();
            await Task.Run(() =>
            {
      //          await buttonOff();
             //   Console.WriteLine("Buttonoff finish");
                CreateAllCoordinates();
                Bus.AllCoordinates = AllCoordinates;
        //        await buttonOn();
            //   Console.WriteLine("Buttonon finish");

            });

       
            //  buttonOn();
            //label9.Text = "Закрывай";
            //label9.Visible = false;
          //  MessageBox.Show("Готово");

        }
        //Mutex mutex = new Mutex();
        //функция, которая создает все координаты для каждого маршрута
        private void CreateAllCoordinates()
        {
            //    label9.Text = "ТИШЕ ТИШЕ ТИШЕ";
            //  progressBar1.Visible = true;
            //try { 
            
         
            AllCoordinates = new SerializableDictionary<string, List<Point>>();
            AllGridsInRoutes = new SerializableDictionary<string, List<int>>();
            for (int i = 0; i < routes.Count; i++)
            {
                AllCoordinates.Add(routes.ElementAt(i).Key, new List<Point>());
                AllGridsInRoutes.Add(routes.ElementAt(i).Key, new List<int>());
                if (routes.ElementAt(i).Value.Count >= 2)
                {
                    PictureBox bus = new PictureBox();
                    bus.Location = new System.Drawing.Point(routes.ElementAt(i).Value[0].x, routes.ElementAt(i).Value[0].y);

                    bus.Size = new System.Drawing.Size(1, 1);
                    Bus onebus = new Bus(routes[routes.ElementAt(i).Key], bus, 0, false, routes.ElementAt(i).Key, false);

                  
                    while (onebus.TurnBack == false)
                    {
                        onebus.MoveForCoordinates();
                        onebus.DetectRectangle();
                        if (onebus.grids != null)
                        {
                            //Parallel.For(0, TheGrid.Count, (k) =>
                            //{
                            for (int k = 0; k < TheGrid.Count; k++)
                            {                           
                                if (onebus.getLocate() == k)
                                {
                                    if (onebus.lastLocate != onebus.Locate)
                                    {
                                        AllGridsInRoutes[routes.ElementAt(i).Key].Add(k);
                                        onebus.lastLocate = onebus.Locate;
                                    }
                                }
                            }
                            //});
                        }
                        //lock (AllCoordinates)
                        //{
                        //  buses.Last().mutex.WaitOne();
                        //await Task.Delay(10);
                        AllCoordinates[AllCoordinates.ElementAt(i).Key].Add(new Point((int)onebus.x, (int)onebus.y));//ошипка                   

                     //   buses.Last().mutex.ReleaseMutex();
                        //}


                    }
                    //onebus.Stop();
                    onebus = new Bus();

                }
                Bus.SetScrollX(mainPanel.AutoScrollPosition.X);
                Bus.SetScrollY(mainPanel.AutoScrollPosition.Y);
                //CreatePollutionInRoutes();
            }
            //}
            //    catch
            //    {
            //       // MessageBox.Show("быстрый быстрый");
            //    }.
            //  label9.Text = "Закрывай";
            //foreach (var traflight in traficLights)
            //{
            //    traflight.Start();
            //}
            //foreach (var bus in buses)
            //{
            //    bus.Start();
            //}
            
            //buttonOn();
            //Bus.AllCoordinates = AllCoordinates;
        }

    }
}
