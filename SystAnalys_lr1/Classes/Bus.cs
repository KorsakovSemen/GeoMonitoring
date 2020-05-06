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
    enum Skips
    {
    }

    public class Bus : ICloneable
    {
        delegate void Dpoint(Point pos);

        public static string busImg { get; set; } = "../../Resources/newbus.PNG";
        public static string offBusImg { get; set; } = "../../Resources/bus.PNG";

        public static int ScrollX { get; set; }
        public static int ScrollY { get; set; }

        public List<Epicenter> Epicenters { get; set; } = new List<Epicenter>();

        public bool skipTraffics = false;
        public int skipStops = 1;
        public int skipEnd = 5;
        public int skip { get; set; } = 1;

        private readonly Random rnd = new Random();
       

        int checkStop = 0;
        int checkStoppedBus = 0;

        readonly int stopTime = 0;
        public double angle { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        //позиция автобуса
        public int PositionAt { get; set; }
        //для обратного движения по маршруту
        public bool TurnBack { get; set; }
        [XmlIgnore, JsonIgnore]
        public Image busPic { get; set; }
        //номер маршрута, по которому будет ездить автобус
        public string route { get; set; }
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

        public bool EpicFounded { get; set; }
        static public int? ZoomCoef { get; set; } = 1;
        public bool Tracker { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        ~Bus()
        { }

        public Bus()
        { }

        public Bus(Image busPic, int PositionAt, bool Turn, string route, List<Point> Coordinates, bool not)
        {
            Tracker = not;
            this.busPic = busPic;
            this.PositionAt = PositionAt;
            TurnBack = Turn;
            this.route = route;
            this.Coordinates = Coordinates;
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
                                    if ((Math.Pow((double.Parse((sp.X * (int)ZoomCoef - Coordinates[PositionAt].X * (int)ZoomCoef).ToString())), 2) + Math.Pow((double.Parse(((sp.Y * (int)ZoomCoef - Coordinates[PositionAt].Y * (int)ZoomCoef)).ToString())), 2) <= Main.G.R * (int)ZoomCoef * Main.G.R * (int)ZoomCoef * (Main.G.R * (int)ZoomCoef)) && sp.Status != Status.RED)
                                    {
                                        skip = 100;
                                        checkStoppedBus = 100;
                                        break;
                                    }
                                    else
                                    if ((Math.Pow((double.Parse((sp.X * (int)ZoomCoef - Coordinates[PositionAt].X * (int)ZoomCoef).ToString())), 2) + Math.Pow((double.Parse(((sp.Y * (int)ZoomCoef - Coordinates[PositionAt].Y * (int)ZoomCoef)).ToString())), 2) <= Main.G.R * (int)ZoomCoef * Main.G.R * (int)ZoomCoef * (Main.G.R * (int)ZoomCoef)) && sp.Status == Status.RED)
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
                                        if ((Math.Pow((double.Parse((sp.X * (int)ZoomCoef - Coordinates[PositionAt].X * (int)ZoomCoef).ToString())), 2) + Math.Pow((double.Parse(((sp.Y * (int)ZoomCoef - Coordinates[PositionAt].Y * (int)ZoomCoef)).ToString())), 2) <= Main.G.R * (int)ZoomCoef * Main.G.R * (int)ZoomCoef * (Main.G.R * (int)ZoomCoef)) && sp.Status != Status.RED)
                                        {
                                            skip = 100;
                                            checkStoppedBus = 100;
                                            break;
                                        }
                                        else
                                        if ((Math.Pow((double.Parse((sp.X * (int)ZoomCoef - Coordinates[PositionAt].X * (int)ZoomCoef).ToString())), 2) + Math.Pow((double.Parse(((sp.Y * (int)ZoomCoef - Coordinates[PositionAt].Y * (int)ZoomCoef)).ToString())), 2) <= Main.G.R * (int)ZoomCoef * Main.G.R * (int)ZoomCoef * (Main.G.R * (int)ZoomCoef)) && sp.Status == Status.RED)
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

        public async Task AsDetectEpicenter()
        {
            await Task.Run(() => DetectEpicenter());
        }
        // обмнаружение  эпицентров через точки
        public int DetectEpicenter()
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


        public async Task AsyncDetectRectangle()
        {
            await Task.Run(() => DetectRectangle());
        }
        //определение квадрата по новому
        public void DetectRectangle()
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
