using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SystAnalys_lr1.Classes
{

    public class Bus : ICloneable
    {
        public static int ScrollX;
        public static int ScrollY;
        public int skip = 1;
        public static string busImg = "../../Resources/newbus.PNG";
        public static string offBusImg = "../../Resources/bus.PNG";
        //таймер для движения
        public List<Epicenter> Epicenters { get; set; } = new List<Epicenter>();

        readonly int stopTime = 0;
        //сетка
        //для создания координат
        public double angle, x, y;
        //позиция автобуса
        public int PositionAt;
        //для обратного движения по маршруту
        public bool TurnBack;
        [XmlIgnore, JsonIgnore]

        public Image busPic;
        //номер маршрута, по которому будет ездить автобус
        public string route;
        //текущий квадрат, в котором находится автобус
        private int? Locate = null;
        //все координаты для движения автобуса
        public List<Point> Coordinates;
        //для того, чтобы 1 раз прибавлять к OneGridFilling
        public int R = 7;
        //сколько автобусу нужно проехать в тиках
        public int TickCount_ { get; set; }
        //все время, которое проехал автобус
        public int AllTickCount { get; set; }
        //за сколько времени автобус нашел эпицентр
        public static int FoundTime { get; set; }
        //проверка нашел ли автобус эпицентр
        public bool EpicFounded { get; set; }
        public int oldSize;
        static public int? ZoomCoef { get; set; } = 1;
        static public int Small { get; set; } = 1000;
        //не позволит басу двигаться на светофорах когда он должен стоять
        public static bool InstaStop { get; set; } = false;
        public static void SetScrollX(int x)
        {
            ScrollX = x;
        }
        public int GetScrollY()
        {
            return ScrollY;
        }
        public int GetScrollX()
        {
            return ScrollX;
        }
        public static void SetScrollY(int y)
        {
            ScrollY = y;
        }

        int oldZoom = (int)ZoomCoef;
        public void SetBusSize()
        {
            oldZoom = (int)ZoomCoef;
        }

        public List<Point> GetCoordinates()
        {
            return Coordinates;
        }


        public void SetAllCoordinates(List<Point> A)
        {
            Coordinates = A;
        }

        public int? GetLocate()
        {
            return Locate;
        }


        public object Clone()
        {
            return MemberwiseClone();
        }

        ~Bus()
        {
        }

        public Bus()
        { }

        public bool Tracker { get; set; }


        public Bus(Image busPic, int PositionAt, bool Turn, string route, List<Point> Coordinates, bool not)
        {
            Tracker = not;
            oldSize = busPic.Size.Height;
            this.busPic = busPic;
            this.PositionAt = PositionAt;
            TurnBack = Turn;
            this.route = route;
            oldSize = busPic.Height;
            this.Coordinates = Coordinates;
        }
        //движение без графики (для моделирования)
        public void ClearAroundEpic()
        {
            foreach (var Epic in Epicenters)
            {
                for (int i = 2; i < Epic.EpicenterGrid.Count + 1; i++)
                {
                    Epic.EpicenterGrid[i] = new List<GridPart>();
                }
            }
        }
        public async Task AsMoveWithoutGraphics()
        {
            await Task.Run(() => MoveWithoutGraphics());
        }
        public void MoveWithoutGraphics()
        {
            if (Tracker == true)
            {
                if (TurnBack == false)
                {
                    if (PositionAt < Coordinates.IndexOf(Coordinates.Last()))
                    {
                        PositionAt++;
                    }
                    else
                    {
                        TurnBack = true;
                        PositionAt--;
                    }
                }
                else
                {
                    if (PositionAt > 0)
                    {
                        PositionAt--;
                    }
                    else
                    {

                        TurnBack = false;
                        PositionAt++;
                    }

                }
            }
        }
        public void MoveWithoutGraphicsByGrids()
        {
            if (Tracker == true)
            {
                if (TurnBack == false)
                {

                    if (PositionAt < Main.AllGridsInRoutes[route].Count - 1)
                    {
                        PositionAt++;

                    }
                    else
                    {
                        TurnBack = true;
                        PositionAt--;
                    }
                }
                else
                {
                    if (PositionAt > 0)
                    {
                        PositionAt--;
                    }
                    else
                    {
                        TurnBack = false;
                        PositionAt++;

                    }

                }
            }
        }

        readonly Random rnd = new Random();
        public bool skipTraffics = false;
        public int skipStops = 1;
        public int skipEnd = 5;
        //движение с графикой (для визуализации движения)
        delegate void Dpoint(Point pos);
        public void AlignBus()
        {

        }
        int checkStop = 0;
        int checkStoppedBus = 0;
        private void StopDown()
        {
            if (skip != 0)
                skip -= 1;
            if (skipStops != 0)
                skipStops -= 1;
            if (skipEnd != 0)
                skipEnd -= 1;
            if (checkStop != 0)
                checkStop -= 1;
            if (checkStoppedBus != 0)
                checkStoppedBus -= 1;
        }
        public void MoveWithGraphics(Graphics G)
        {
            Bus thisBus = new Bus(busPic, PositionAt, TurnBack, route, Coordinates, Tracker);
            List<Bus> buses = Main.buses;
            buses.Remove(thisBus);
            if (checkStop == 0)
            {
                if (TurnBack == false)
                {
                    if (PositionAt < Coordinates.Count - 1)
                    {
                        if (checkStoppedBus == 0)
                        {
                            if (Main.buses.Count != 0)
                            {
                                foreach (var sp in buses)
                                {
                                    if (Math.Pow((sp.Coordinates[sp.PositionAt].X * (int)ZoomCoef - (Coordinates[PositionAt].X * (int)ZoomCoef)), 2) + Math.Pow((sp.Coordinates[sp.PositionAt].Y * (int)ZoomCoef - (Coordinates[PositionAt].Y * (int)ZoomCoef)), 2) <= sp.R * sp.R && sp.checkStop != 0 && sp.TurnBack == TurnBack)
                                    {
                                        Console.WriteLine("Turn false");
                                        checkStop = sp.checkStop;
                                        skip = 100;
                                        skipStops = rnd.Next(0, 50);
                                        break;
                                    }
                                }
                            }
                        }
                        if (Main.stopPoints.Count != 0 && Main.stopPoints.ContainsKey(route))
                        {
                            if (skipStops == 0)
                            {
                                foreach (var sp in Main.stopPoints[route])
                                {
                                    if (Math.Pow((double.Parse((sp.X * (int)ZoomCoef - Coordinates[PositionAt].X * (int)ZoomCoef).ToString())), 2) + Math.Pow((double.Parse(((sp.Y * (int)ZoomCoef - Coordinates[PositionAt].Y * (int)ZoomCoef)).ToString())), 2) <= Main.G.R * (int)ZoomCoef * (Main.G.R * (int)ZoomCoef))
                                    {
                                        checkStop = rnd.Next(0, 200);
                                        skipStops = 250 - checkStop;
                                        checkStoppedBus = checkStop + 100;
                                        break;
                                    }
                                }
                            }
                        }
                        if (Main.traficLights.Count != 0)
                        {
                            if (skip == 0)
                            {
                                foreach (var sp in Main.traficLights)
                                {
                                    if ((Math.Pow((double.Parse((sp.x * (int)ZoomCoef - Coordinates[PositionAt].X * (int)ZoomCoef).ToString())), 2) + Math.Pow((double.Parse(((sp.y * (int)ZoomCoef - Coordinates[PositionAt].Y * (int)ZoomCoef)).ToString())), 2) <= Main.G.R * (int)ZoomCoef * Main.G.R * (int)ZoomCoef * (Main.G.R * (int)ZoomCoef)) && sp.Status != Status.RED)
                                    {
                                        skip = 100;
                                        checkStoppedBus = 100;
                                        break;
                                    }
                                    else
                                    if ((Math.Pow((double.Parse((sp.x * (int)ZoomCoef - Coordinates[PositionAt].X * (int)ZoomCoef).ToString())), 2) + Math.Pow((double.Parse(((sp.y * (int)ZoomCoef - Coordinates[PositionAt].Y * (int)ZoomCoef)).ToString())), 2) <= Main.G.R * (int)ZoomCoef * Main.G.R * (int)ZoomCoef * (Main.G.R * (int)ZoomCoef)) && sp.Status == Status.RED)
                                    {
                                        if (sp.bal == 0)
                                        {
                                            skip = (sp.bal + 2) * 30 + 100;
                                            checkStop = 80;
                                        }
                                        else
                                        {
                                            skip = (sp.bal + 2) * 30 + 100;
                                            checkStop = ((sp.bal + 2) * 30);
                                        }
                                        checkStoppedBus = 100;
                                        break;

                                    }
                                }
                            }
                        }
                        G.DrawImage(busPic, Coordinates[PositionAt].X * (int)ZoomCoef - busPic.Width / 2, Coordinates[PositionAt].Y * (int)ZoomCoef - busPic.Height / 2);
                        StopDown();
                        PositionAt++;
                    }
                    else
                    {
                        StopDown();
                        skipEnd = rnd.Next(0, 200);
                        G.DrawImage(busPic, Coordinates[PositionAt].X * (int)ZoomCoef - busPic.Width / 2, Coordinates[PositionAt].Y * (int)ZoomCoef - busPic.Height / 2);
                        if (skipEnd == 0)
                        {
                            TurnBack = true;
                            PositionAt--;
                        }
                    }
                }
                else
                {
                    if (PositionAt > 0)
                    {
                        //for (int i = Main.routesEdge[thisBus.route].Count; i > 1; i--)
                        //{
                        //    if (Main.routesEdge[thisBus.route][i].v1 != Main.routesEdge[thisBus.route][i - 1].v1 - 1)
                        //    {
                        //        stopDown();
                        //        skipEnd = rnd.Next(0, 200);
                        //        G.DrawImage(busPic, Coordinates[PositionAt].X * (int)ZoomCoef - busPic.Width / 2, Coordinates[PositionAt].Y * (int)ZoomCoef - busPic.Height / 2);
                        //        if (skipEnd == 0)
                        //        {
                        //            TurnBack = false;
                        //            PositionAt++;
                        //        }
                        //        break;
                        //    }
                        //}
                        if (PositionAt < Coordinates.Count)
                        {
                            //if (skipStops == 0)
                            //{
                            if (checkStoppedBus == 0)
                            {
                                if (Main.buses.Count != 0)
                                {
                                    foreach (var sp in buses)
                                    {
                                        if (Math.Pow((sp.Coordinates[sp.PositionAt].X * (int)ZoomCoef - (Coordinates[PositionAt].X * (int)ZoomCoef)), 2) + Math.Pow((sp.Coordinates[sp.PositionAt].Y * (int)ZoomCoef - (Coordinates[PositionAt].Y * (int)ZoomCoef)), 2) <= sp.R * sp.R && sp.checkStop != 0 && sp.TurnBack == TurnBack)
                                        {
                                            Console.WriteLine("Turn true");
                                            checkStop = sp.checkStop;
                                            skip = 100;
                                            skipStops = rnd.Next(200, 250);
                                            break;
                                        }
                                    }
                                }
                            }
                            // }
                            if (Main.stopPoints.Count != 0 && Main.stopPoints.ContainsKey(route))
                            {
                                if (skipStops == 0)
                                {
                                    foreach (var sp in Main.stopPoints[route])
                                    {
                                        if (Math.Pow((double.Parse((sp.X * (int)ZoomCoef - Coordinates[PositionAt].X * (int)ZoomCoef).ToString())), 2) + Math.Pow((double.Parse(((sp.Y * (int)ZoomCoef - Coordinates[PositionAt].Y * (int)ZoomCoef)).ToString())), 2) <= Main.G.R * (int)ZoomCoef * (Main.G.R * (int)ZoomCoef))
                                        {
                                            checkStop = rnd.Next(0, 200);
                                            skipStops = 250 - checkStop;
                                            checkStoppedBus = checkStop + 100;
                                            break;
                                        }
                                    }
                                }
                            }
                            if (Main.traficLights.Count != 0)
                            {
                                if (skip == 0)
                                {
                                    foreach (var sp in Main.traficLights)
                                    {
                                        if ((Math.Pow((double.Parse((sp.x * (int)ZoomCoef - Coordinates[PositionAt].X * (int)ZoomCoef).ToString())), 2) + Math.Pow((double.Parse(((sp.y * (int)ZoomCoef - Coordinates[PositionAt].Y * (int)ZoomCoef)).ToString())), 2) <= Main.G.R * (int)ZoomCoef * Main.G.R * (int)ZoomCoef * (Main.G.R * (int)ZoomCoef)) && sp.Status != Status.RED)
                                        {
                                            skip = 100;
                                            checkStoppedBus = 100;
                                            break;
                                        }
                                        else
                                        if ((Math.Pow((double.Parse((sp.x * (int)ZoomCoef - Coordinates[PositionAt].X * (int)ZoomCoef).ToString())), 2) + Math.Pow((double.Parse(((sp.y * (int)ZoomCoef - Coordinates[PositionAt].Y * (int)ZoomCoef)).ToString())), 2) <= Main.G.R * (int)ZoomCoef * Main.G.R * (int)ZoomCoef * (Main.G.R * (int)ZoomCoef)) && sp.Status == Status.RED)
                                        {
                                            if (sp.bal == 0)
                                            {
                                                skip = (sp.bal + 2) * 30 + 100;
                                                checkStop = 80;
                                            }
                                            else
                                            {
                                                skip = (sp.bal + 2) * 30 + 100;
                                                checkStop = ((sp.bal + 2) * 30);
                                            }
                                            checkStoppedBus = 100;
                                            break;
                                        }
                                    }
                                }
                            }

                            G.DrawImage(busPic, Coordinates[PositionAt].X * (int)ZoomCoef - busPic.Width / 2, Coordinates[PositionAt].Y * (int)ZoomCoef - busPic.Height / 2);
                            StopDown();
                            PositionAt--;
                        }
                    }
                    else
                    {
                        StopDown();
                        skipEnd = rnd.Next(0, 200);
                        G.DrawImage(busPic, Coordinates[PositionAt].X * (int)ZoomCoef - busPic.Width / 2, Coordinates[PositionAt].Y * (int)ZoomCoef - busPic.Height / 2);
                        if (skipEnd == 0)
                        {
                            TurnBack = false;
                            PositionAt++;
                        }

                    }

                }
            }
            else
            {
                StopDown();
                G.DrawImage(busPic, Coordinates[PositionAt].X * (int)ZoomCoef - busPic.Width / 2, Coordinates[PositionAt].Y * (int)ZoomCoef - busPic.Height / 2);
            }

        }

        public string GetRoute()
        {
            return route;
        }

        public async Task AsDetectEpicenter2()
        {
            await Task.Run(() => DetectEpicenter2());
        }
        // обмнаружение  эпицентров через точки
        public int DetectEpicenter2()
        {
            foreach (var EpicList in Epicenters)
            {
                foreach (var Sector in EpicList.GetEpicenterGrid())
                {
                    foreach (var Square in Sector.Value)
                        if ((PositionAt < Coordinates.Count))
                            if (((Coordinates[PositionAt].X * ZoomCoef) >= Square.x * ZoomCoef) && ((Coordinates[PositionAt].X * ZoomCoef) <= Square.x * ZoomCoef + GridPart.Width * ZoomCoef) && ((Coordinates[PositionAt].Y * ZoomCoef) >= Square.y * ZoomCoef) && ((Coordinates[PositionAt].Y * ZoomCoef) <= (Square.y * ZoomCoef + GridPart.Height * ZoomCoef)))
                            {
                                switch (Sector.Key)
                                {
                                    case 1:
                                        if (Square.Check == false)
                                        {
                                            Square.Check = true;
                                            EpicList.DetectCount++;
                                        }
                                        return 3;
                                    case 2:
                                        return 2;
                                    case 3:
                                        return 1;
                                    default:
                                        return 0;

                                }
                            }
                }
            }

            return 0;
        }
        public int DetectEpicenterByGrid()
        {
            foreach (var EpicList in Epicenters)
            {
                foreach (var Sector in EpicList.GetEpicenterGrid())
                {
                    foreach (var Square in Sector.Value)
                    {


                        if (((Main.TheGrid[Main.AllGridsInRoutes[route][(int)PositionAt]].x + GridPart.Width == Square.x) && (Main.TheGrid[Main.AllGridsInRoutes[route][(int)PositionAt]].y == Square.y)))
                        {
                            CheckEpic(Sector, Square, EpicList);
                        }
                        if (((Main.TheGrid[Main.AllGridsInRoutes[route][(int)PositionAt]].x - GridPart.Width == Square.x) && (Main.TheGrid[Main.AllGridsInRoutes[route][(int)PositionAt]].y == Square.y)))
                        {
                            CheckEpic(Sector, Square, EpicList);
                        }
                        //if (((Main.TheGrid[Main.AllGridsInRoutes[route][(int)PositionAt]].x == Square.x) && (Main.TheGrid[Main.AllGridsInRoutes[route][(int)PositionAt]].y + GridPart.Height == Square.y)))
                        //{
                        //    CheckEpic(Sector, Square, EpicList);
                        //}
                        //if (((Main.TheGrid[Main.AllGridsInRoutes[route][(int)PositionAt]].x == Square.x) && (Main.TheGrid[Main.AllGridsInRoutes[route][(int)PositionAt]].y - GridPart.Height == Square.y)))
                        //{
                        //    CheckEpic(Sector, Square, EpicList);
                        //}
                        //if (((Main.TheGrid[Main.AllGridsInRoutes[route][(int)PositionAt]].x - GridPart.Width == Square.x) && (Main.TheGrid[Main.AllGridsInRoutes[route][(int)PositionAt]].y - GridPart.Height == Square.y)))
                        //{
                        //    CheckEpic(Sector, Square, EpicList);
                        //}
                        //if (((Main.TheGrid[Main.AllGridsInRoutes[route][(int)PositionAt]].x + GridPart.Width == Square.x) && (Main.TheGrid[Main.AllGridsInRoutes[route][(int)PositionAt]].y + GridPart.Height == Square.y)))
                        //{
                        //    CheckEpic(Sector, Square, EpicList);
                        //}
                        //if (((Main.TheGrid[Main.AllGridsInRoutes[route][(int)PositionAt]].x + GridPart.Width == Square.x) && (Main.TheGrid[Main.AllGridsInRoutes[route][(int)PositionAt]].y - GridPart.Height == Square.y)))
                        //{
                        //    CheckEpic(Sector, Square, EpicList);
                        //}
                        //if (((Main.TheGrid[Main.AllGridsInRoutes[route][(int)PositionAt]].x - GridPart.Width == Square.x) && (Main.TheGrid[Main.AllGridsInRoutes[route][(int)PositionAt]].y + GridPart.Height == Square.y)))
                        //{
                        //    CheckEpic(Sector, Square, EpicList);
                        //}
                        if (((Main.TheGrid[Main.AllGridsInRoutes[route][(int)PositionAt]].x == Square.x) && (Main.TheGrid[Main.AllGridsInRoutes[route][(int)PositionAt]].y == Square.y)))
                        {
                            return CheckEpic(Sector, Square, EpicList);
                        }
                    }
                }
            }

            return 0;
        }
        private int CheckEpic(KeyValuePair<int, List<GridPart>> Sector, GridPart Square, Epicenter EpicList)
        {
            switch (Sector.Key)
            {
                case 1:
                    if (Square.Check == false)
                    {
                        Square.Check = true;
                        EpicList.DetectCount++;
                    }
                    return 3;
                case 2:
                    return 2;
                case 3:
                    return 1;

                default:
                    return 0;

            }
        }


        public async Task AsyncDetectRectangle2()
        {
            await Task.Run(() => DetectRectangle2());
        }
        //определение квадрата по новому
        public void DetectRectangle2()
        {

            for (int i = 0; i < Main.TheGrid.Count; i++)
            {
                if ((PositionAt < Coordinates.Count))

                    if (((Coordinates[PositionAt].X) > Main.TheGrid[i].x) && ((Coordinates[PositionAt].X) < Main.TheGrid[i].x + GridPart.Width) && ((Coordinates[PositionAt].Y) > Main.TheGrid[i].y) && ((Coordinates[PositionAt].Y) < (Main.TheGrid[i].y + GridPart.Height)))
                    {
                        Locate = i;
                    }
            }

        }



    }
}
