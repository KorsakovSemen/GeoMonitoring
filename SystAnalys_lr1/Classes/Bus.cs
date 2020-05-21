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
    public struct Skips
    {
        public bool skipTraffics;
        public int skipStops;
        public int skipEnd;
        public int skipTrafficLights;

        public Skips(bool skipTraffics, int skipStops, int skipEnd, int skipTrafficLights)
        {
            this.skipTraffics = skipTraffics;
            this.skipStops = skipStops;
            this.skipEnd = skipEnd;
            this.skipTrafficLights = skipTrafficLights;
        }
    }

    struct CheckStops
    {
        public int checkStop;
        public int checkStoppedBus;
        public CheckStops(int checkStop, int checkStoppedBus)
        {
            this.checkStop = checkStop;
            this.checkStoppedBus = checkStoppedBus;
        }
    }

    public class Bus : ICloneable
    {
        delegate void Dpoint(Point pos);

        public Skips Skips;
        CheckStops CheckStops;

        public static string BusImg { get; set; } = "../../Resources/newbus.PNG";
        public static string OffBusImg { get; set; } = "../../Resources/bus.PNG";

        public static int ScrollX { get; set; }
        public static int ScrollY { get; set; }

        public List<Epicenter> Epicenters { get; set; } = new List<Epicenter>();

        private readonly Random rnd = new Random();

        public int PositionAt { get; set; }
        public bool TurnBack { get; set; }

        public bool StopAtStationByGrid { get; set; } = false;
        [XmlIgnore, JsonIgnore]
        public Image BusPic { get; set; }
        //номер маршрута, по которому будет ездить автобус
        public string Route { get; set; }
        //текущий квадрат, в котором находится автобус
        private int? Locate = null;
        //все координаты для движения автобуса
        public List<Point> Coordinates { get; set; }
        //для того, чтобы 1 раз прибавлять к OneGridFilling
        public int R { get; set; } = 7;
        //сколько автобусу нужно проехать в тиках
        public int TickCount_ { get; set; }
        //все время, которое проехал автобус
        public int AllTickCount { get; set; }
        //за сколько времени автобус нашел эпицентр
        public static int FoundTime { get; set; }
        bool stopOnBusStop = false;

        public bool EpicFounded { get; set; }
        static public int? ZoomCoef { get; set; } = 1;
        public bool Tracker { get; set; }

        int speed;
        int changeSpeed;

        public object Clone()
        {
            return MemberwiseClone();
        }

        ~Bus()
        { }

        public Bus()
        { }

        public Bus(Image BusPic, int PositionAt, bool Turn, string Route, List<Point> Coordinates, bool not)
        {
            Tracker = not;
            this.BusPic = BusPic;
            this.PositionAt = PositionAt;
            TurnBack = Turn;
            this.Route = Route;
            this.Coordinates = Coordinates;
            Skips = new Skips(false, 1, 5, 1);
            CheckStops = new CheckStops(0, 0);
            changeSpeed = rnd.Next(20, 50);
        }

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


        public void MoveWithoutGraphicsByGrids()
        {
            if (Tracker == true)
            {
                if (TurnBack == false)
                {
                    if (PositionAt < Data.AllGridsInRoutes[Route].Count - 1)
                    {
                        PositionAt++;
                    }
                    else
                    {
                        TurnBack = true;
                        PositionAt--;
                        StopAtStationByGrid = true;
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
                        StopAtStationByGrid = true;
                    }
                }
            }
        }

        private void StopDown()
        {
            if (changeSpeed != 0)
                changeSpeed -= 1;

            if (SlowDown > 3)
            {
                changeSpeed = rnd.Next(BusStop.StopTime / 10, 70);
                speed = 1;
            }
            if (changeSpeed == 0)
            {
                changeSpeed = rnd.Next(BusStop.StopTime / 10, 70);
                speed = rnd.Next(1, 3);
            }
            if (Skips.skipTrafficLights != 0)
                Skips.skipTrafficLights -= 1;
            if (Skips.skipStops != 0)
                Skips.skipStops -= 1;
            if (Skips.skipEnd != 0)
                Skips.skipEnd -= 1;
            if (CheckStops.checkStop != 0)
                CheckStops.checkStop -= 1;
            if (CheckStops.checkStoppedBus != 0)
                CheckStops.checkStoppedBus -= 1;
        }

        public void TurnBackOn()
        {
            if (PositionAt - speed > 0)
                PositionAt -= speed;
            else
                PositionAt--;
        }

        public void TurnBackOff()
        {
            if (PositionAt + speed < Coordinates.Count - 1)
                PositionAt += speed;
            else
                PositionAt++;
        }

        public void CheckBus(Bus bus)
        {
            CheckStops.checkStop = bus.CheckStops.checkStop;
            Skips.skipTrafficLights = BusStop.StopTime / 2;
            Skips.skipStops = rnd.Next(BusStop.StopTime, BusStop.StopTime + 50);
        }

        public void CheckStop()
        {
            CheckStops.checkStop = rnd.Next(0, BusStop.StopTime);
            Skips.skipStops = BusStop.StopTime + 50 - CheckStops.checkStop;
            CheckStops.checkStoppedBus = CheckStops.checkStop + 100;
            stopOnBusStop = true;
        }

        public void CheckTrafficLight()
        {
            foreach (var sp in Data.TraficLights)
            {
                if ((Math.Pow((double.Parse((sp.X * (int)ZoomCoef - Coordinates[PositionAt].X * (int)ZoomCoef).ToString())), 2) + Math.Pow((double.Parse(((sp.Y * (int)ZoomCoef - Coordinates[PositionAt].Y * (int)ZoomCoef)).ToString())), 2) <= Main.G.R * (int)ZoomCoef * Main.G.R * (int)ZoomCoef * (Main.G.R * (int)ZoomCoef)) && sp.Status != LightStatus.RED)
                {
                    Skips.skipTrafficLights = BusStop.StopTime / 2;
                    CheckStops.checkStoppedBus = BusStop.StopTime / 2;
                    break;
                }
                else
                if ((Math.Pow((double.Parse((sp.X * (int)ZoomCoef - Coordinates[PositionAt].X * (int)ZoomCoef).ToString())), 2) + Math.Pow((double.Parse(((sp.Y * (int)ZoomCoef - Coordinates[PositionAt].Y * (int)ZoomCoef)).ToString())), 2) <= Main.G.R * (int)ZoomCoef * Main.G.R * (int)ZoomCoef * (Main.G.R * (int)ZoomCoef)) && sp.Status == LightStatus.RED)
                {
                    if (sp.Bal == 0)
                    {
                        Skips.skipTrafficLights = (sp.Bal + 2) * (BusStop.StopTime / 10 + 10) + (BusStop.StopTime / 10 * 4);
                        CheckStops.checkStop = 80;
                    }
                    else
                    {
                        Skips.skipTrafficLights = (sp.Bal + 2) * (BusStop.StopTime / 10 + 10) + (BusStop.StopTime / 10 * 4);
                        CheckStops.checkStop = (sp.Bal + 2) * (BusStop.StopTime / 10 + 10);
                    }
                    CheckStops.checkStoppedBus = BusStop.StopTime / 2;
                    break;

                }
            }
        }

        private int SlowDown { get; set; }

        public void MoveWithGraphics(Graphics G)
        {
            Bus thisBus = new Bus(BusPic, PositionAt, TurnBack, Route, Coordinates, Tracker);
            List<Bus> buses = Data.Buses;
            buses.Remove(thisBus);
            if (CheckStops.checkStop == 0)
            {
                if (TurnBack == false)
                {
                    if (PositionAt < Coordinates.Count - 1)
                    {
                        if (Data.Buses.Count != 0)
                        {
                            foreach (var bus in buses)
                            {
                                if (Math.Pow((bus.Coordinates[bus.PositionAt].X * (int)ZoomCoef - (Coordinates[PositionAt].X * (int)ZoomCoef)), 2) + Math.Pow((bus.Coordinates[bus.PositionAt].Y * (int)ZoomCoef - (Coordinates[PositionAt].Y * (int)ZoomCoef)), 2) <= bus.R * bus.R && bus.TurnBack == TurnBack && !bus.stopOnBusStop && bus.CheckStops.checkStop != 0)
                                {
                                    CheckBus(bus);
                                    break;
                                }
                                if (Math.Pow((bus.Coordinates[bus.PositionAt].X * (int)ZoomCoef - (Coordinates[PositionAt].X * (int)ZoomCoef)), 2) + Math.Pow((bus.Coordinates[bus.PositionAt].Y * (int)ZoomCoef - (Coordinates[PositionAt].Y * (int)ZoomCoef)), 2) <= bus.R * bus.R)
                                {
                                    if (bus.TurnBack == TurnBack && bus.PositionAt > PositionAt)
                                    {
                                        SlowDown += 1;
                                        break;
                                    }
                                    else
                                    {
                                        if (SlowDown != 0)
                                            SlowDown -= 1;
                                    }
                                }

                            }
                        }
                        if (Data.StopPoints.Count != 0 && Data.StopPoints.ContainsKey(Route))
                        {
                            if (Skips.skipStops == 0)
                            {
                                foreach (var sp in Data.StopPoints[Route])
                                {
                                    if (Math.Pow((double.Parse((sp.X * (int)ZoomCoef - Coordinates[PositionAt].X * (int)ZoomCoef).ToString())), 2) + Math.Pow((double.Parse(((sp.Y * (int)ZoomCoef - Coordinates[PositionAt].Y * (int)ZoomCoef)).ToString())), 2) <= Main.G.R * (int)ZoomCoef * (Main.G.R * (int)ZoomCoef))
                                    {
                                        CheckStop();
                                        break;
                                    }
                                }
                            }
                        }
                        if (Data.TraficLights.Count != 0)
                        {
                            if (Skips.skipTrafficLights == 0)
                            {
                                CheckTrafficLight();
                            }
                        }
                        G.DrawImage(BusPic, Coordinates[PositionAt].X * (int)ZoomCoef - BusPic.Width / 2, Coordinates[PositionAt].Y * (int)ZoomCoef - BusPic.Height / 2);
                        StopDown();
                        TurnBackOff();

                    }
                    else
                    {
                        StopDown();
                        Skips.skipEnd = rnd.Next(0, BusStop.StopTime * 3);
                        G.DrawImage(BusPic, Coordinates[PositionAt].X * (int)ZoomCoef - BusPic.Width / 2, Coordinates[PositionAt].Y * (int)ZoomCoef - BusPic.Height / 2);
                        if (Skips.skipEnd == 0)
                        {
                            TurnBack = true;
                            TurnBackOn();

                        }
                    }
                }
                else
                {
                    if (PositionAt > 0)
                    {
                        if (PositionAt < Coordinates.Count)
                        {
                            if (CheckStops.checkStoppedBus == 0)
                            {
                                if (Data.Buses.Count != 0)
                                {
                                    foreach (var bus in buses)
                                    {
                                        if (Math.Pow((bus.Coordinates[bus.PositionAt].X * (int)ZoomCoef - (Coordinates[PositionAt].X * (int)ZoomCoef)), 2) + Math.Pow((bus.Coordinates[bus.PositionAt].Y * (int)ZoomCoef - (Coordinates[PositionAt].Y * (int)ZoomCoef)), 2) <= bus.R * bus.R && bus.TurnBack == TurnBack && !bus.stopOnBusStop && bus.CheckStops.checkStop != 0)
                                        {
                                            CheckBus(bus);
                                            break;
                                        }
                                        if (Math.Pow((bus.Coordinates[bus.PositionAt].X * (int)ZoomCoef - (Coordinates[PositionAt].X * (int)ZoomCoef)), 2) + Math.Pow((bus.Coordinates[bus.PositionAt].Y * (int)ZoomCoef - (Coordinates[PositionAt].Y * (int)ZoomCoef)), 2) <= bus.R * bus.R)
                                        {
                                            if (bus.TurnBack == TurnBack && bus.PositionAt < PositionAt)
                                            {
                                                SlowDown += 1;
                                                break;
                                            }
                                            else
                                            {
                                                if (SlowDown != 0)
                                                    SlowDown -= 1;
                                            }
                                        }

                                    }
                                }
                            }
                            if (Data.StopPoints.Count != 0 && Data.StopPoints.ContainsKey(Route))
                            {
                                if (Skips.skipStops == 0)
                                {
                                    foreach (var sp in Data.StopPoints[Route])
                                    {
                                        if (Math.Pow((double.Parse((sp.X * (int)ZoomCoef - Coordinates[PositionAt].X * (int)ZoomCoef).ToString())), 2) + Math.Pow((double.Parse(((sp.Y * (int)ZoomCoef - Coordinates[PositionAt].Y * (int)ZoomCoef)).ToString())), 2) <= Main.G.R * (int)ZoomCoef * (Main.G.R * (int)ZoomCoef))
                                        {
                                            CheckStop();
                                            break;
                                        }
                                    }
                                }
                            }
                            if (Data.TraficLights.Count != 0)
                            {
                                if (Skips.skipTrafficLights == 0)
                                {
                                    CheckTrafficLight();
                                }
                            }

                            G.DrawImage(BusPic, Coordinates[PositionAt].X * (int)ZoomCoef - BusPic.Width / 2, Coordinates[PositionAt].Y * (int)ZoomCoef - BusPic.Height / 2);
                            StopDown();
                            TurnBackOn();
                        }
                    }
                    else
                    {
                        StopDown();
                        Skips.skipEnd = rnd.Next(0, BusStop.StopTime * 3);
                        G.DrawImage(BusPic, Coordinates[PositionAt].X * (int)ZoomCoef - BusPic.Width / 2, Coordinates[PositionAt].Y * (int)ZoomCoef - BusPic.Height / 2);
                        if (Skips.skipEnd == 0)
                        {
                            TurnBack = false;
                            TurnBackOff();
                        }

                    }

                }
            }
            else
            {
                StopDown();
                G.DrawImage(BusPic, Coordinates[PositionAt].X * (int)ZoomCoef - BusPic.Width / 2, Coordinates[PositionAt].Y * (int)ZoomCoef - BusPic.Height / 2);
            }

        }

        public string GetRoute()
        {
            return Route;
        }

        public async Task AsDetectEpicenter()
        {
            await Task.Run(() => DetectEpicenter());
        }

        // обмнаружение  эпицентров через точки
        public int DetectEpicenter()
        {
            foreach (var EpicList in Epicenters)
            {
                foreach (var Sector in EpicList.EpicenterGrid)
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
               foreach (var Sector in EpicList.EpicenterGrid)
                {
                    foreach (var Square in Sector.Value)
                    {

                        //if (((Data.TheGrid[Data.AllGridsInRoutes[Route][(int)PositionAt]].x == Square.x) && (Data.TheGrid[Data.AllGridsInRoutes[Route][(int)PositionAt]].y + GridPart.Height == Square.y)))
                        //{
                        //    CheckEpic(Sector, Square, EpicList);
                        //}

                        //if (((Main.TheGrid[Main.AllGridsInRoutes[route][(int)PositionAt]].x == Square.x) && (Main.TheGrid[Main.AllGridsInRoutes[route][(int)PositionAt]].y - GridPart.Height == Square.y)))
                        //{
                        //    CheckEpic(Sector, Square, EpicList);
                        //}

                        if (((Data.TheGrid[Data.AllGridsInRoutes[Route][(int)PositionAt]].x - GridPart.Width == Square.x) && (Data.TheGrid[Data.AllGridsInRoutes[Route][(int)PositionAt]].y - GridPart.Height == Square.y)))
                        {
                            CheckEpic(Sector, Square, EpicList);
                        }

                        //был (вроде норм влияет)
                        //if (((Data.TheGrid[Data.AllGridsInRoutes[Route][(int)PositionAt]].x + GridPart.Width == Square.x) && (Data.TheGrid[Data.AllGridsInRoutes[Route][(int)PositionAt]].y - GridPart.Height == Square.y)))
                        //{
                        //    CheckEpic(Sector, Square, EpicList);
                        //}
                        //был (плюс минус влияет)
                        //if (((Data.TheGrid[Data.AllGridsInRoutes[Route][(int)PositionAt]].x + GridPart.Width == Square.x) && (Data.TheGrid[Data.AllGridsInRoutes[Route][(int)PositionAt]].y == Square.y)))
                        //{
                        //    CheckEpic(Sector, Square, EpicList);
                        //}
                        //был (плюс минус влияет)
                        //if (((Data.TheGrid[Data.AllGridsInRoutes[Route][(int)PositionAt]].x - GridPart.Width == Square.x) && (Data.TheGrid[Data.AllGridsInRoutes[Route][(int)PositionAt]].y == Square.y)))
                        //{
                        //    CheckEpic(Sector, Square, EpicList);
                        //}


                        if (((Data.TheGrid[Data.AllGridsInRoutes[Route][(int)PositionAt]].x + GridPart.Width == Square.x) && (Data.TheGrid[Data.AllGridsInRoutes[Route][(int)PositionAt]].y + GridPart.Height == Square.y)))
                        {
                            CheckEpic(Sector, Square, EpicList);
                        }

                        if (((Data.TheGrid[Data.AllGridsInRoutes[Route][(int)PositionAt]].x - GridPart.Width == Square.x) && (Data.TheGrid[Data.AllGridsInRoutes[Route][(int)PositionAt]].y + GridPart.Height == Square.y)))
                        {
                            CheckEpic(Sector, Square, EpicList);
                        }

                        if (((Data.TheGrid[Data.AllGridsInRoutes[Route][(int)PositionAt]].x == Square.x) && (Data.TheGrid[Data.AllGridsInRoutes[Route][(int)PositionAt]].y == Square.y)))
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


        public async Task AsyncDetectRectangle()
        {
            await Task.Run(() => DetectRectangle());
        }
        //определение квадрата по новому
        public void DetectRectangle()
        {

            for (int i = 0; i < Data.TheGrid.Count; i++)
            {
                if ((PositionAt < Coordinates.Count))

                    if (((Coordinates[PositionAt].X) > Data.TheGrid[i].x) && ((Coordinates[PositionAt].X) < Data.TheGrid[i].x + GridPart.Width) && ((Coordinates[PositionAt].Y) > Data.TheGrid[i].y) && ((Coordinates[PositionAt].Y) < (Data.TheGrid[i].y + GridPart.Height)))
                    {
                        Locate = i;
                    }
            }

        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
