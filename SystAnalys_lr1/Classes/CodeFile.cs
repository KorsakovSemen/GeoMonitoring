using MetroFramework;
using Newtonsoft.Json;
using SystAnalys_lr1.Strings;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SystAnalys_lr1
{
    public class Vertex //: ICloneable
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int gridNum;

        public Vertex()
        { }

        public Vertex(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is Vertex objAsPart)) return false;
            else return Equals(objAsPart);
        }

        public bool Equals(Vertex other)
        {
            if (other == null) return false;
            return (this.X.Equals(other.X) && this.Y.Equals(other.Y));
        }

        public override int GetHashCode()
        {
            var hashCode = -1577951254;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            hashCode = hashCode * -1521134295 + gridNum.GetHashCode();
            return hashCode;
        }
    }

    public class Edge
    {
        public int v1 { get; set; }
        public int v2 { get; set; }

        public Edge()
        { }

        public Edge(int v1, int v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }
    }

    public class DrawGraph
    {
        public Bitmap bitmap;
        readonly private Pen blackPen;
        readonly private Pen darkGoldPen;
        public Graphics gr;
        readonly private Color color;
        public int R = 3; //радиус окружности вершины

        public DrawGraph()
        {

            blackPen = new Pen(Color.Black)
            {
                Width = 1
            };
            darkGoldPen = new Pen(Color.MediumAquamarine)
            {
                Width = 1
            };
            _ = new Random();
            color = Color.ForestGreen;//Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
        }

        public void setBitmap()
        {
            //эксепшн при загрузке левой директоории
            bitmap = new Bitmap(Main.globalMap);
            gr = Graphics.FromImage(bitmap);
        }
        // для второй формы быстрофикс
        public void setBitmap2()
        {
            bitmap = new Bitmap(Main.globalMap);
            gr = Graphics.FromImage(bitmap);
        }
        //
        public Bitmap GetBitmap()
        {
            return bitmap;
        }

        public void clearSheet()
        {
            Graphics.FromImage(bitmap).Clear(Color.White);
            bitmap = new Bitmap(Main.globalMap);
            gr.Dispose();
            gr = Graphics.FromImage(bitmap);

        }
        //для второй формы
        public void clearSheet2()
        {
            /*Graphics.FromImage(bitmap).Clear(Color.Wheat); *//// ТУТ ЭКСЕПШН НА МОДЕЛИНГЕ   
            bitmap = new Bitmap(DisplayEpicenters.EsheetPicture);
            gr = null;
            //  gr.Dispose();
            gr = (Graphics.FromImage(bitmap));

        }
        //
        public void drawBusMove(List<Bus> buses)
        {



        }
        public void drawVertex(int x, int y)
        {
            gr.FillEllipse(Brushes.GreenYellow, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
            gr.DrawEllipse(blackPen, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
        }

        public void drawSelectedVertex(int x, int y)
        {
            gr.FillEllipse(Brushes.Red, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
            gr.DrawEllipse(blackPen, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
        }

        public void drawSelectedStopVertex(int x, int y)
        {
            gr.FillEllipse(Brushes.HotPink, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
            gr.DrawEllipse(blackPen, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
        }

        public void drawRouteVertex(int x, int y)
        {
            gr.FillEllipse(Brushes.AliceBlue, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
            gr.DrawEllipse(blackPen, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
        }
        public void drawYellowVertex(int x, int y)
        {
            gr.FillEllipse(Brushes.Yellow, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
            gr.DrawEllipse(blackPen, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
        }
        public void drawGreenVertex(int x, int y)
        {
            gr.FillEllipse(Brushes.ForestGreen, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
            gr.DrawEllipse(blackPen, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
        }
        public void drawStopVertex(int x, int y)
        {
            gr.FillEllipse(Brushes.Orange, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
            gr.DrawEllipse(blackPen, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
        }

        public void drawStopRouteVertex(int x, int y)
        {
            gr.FillEllipse(Brushes.Blue, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
            gr.DrawEllipse(blackPen, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
        }

        public void drawEdge(Vertex V1, Vertex V2, Edge E, int rand = 0)
        {
            Pen pen;
            if (rand != 0)
            {
                pen = new Pen(color)
                {
                    Width = 2 * Main.zoom
                };
            }
            else
            {
                pen = darkGoldPen;
                pen.Width = 2 * Main.zoom;
            };

            if (E.v1 == E.v2)
            {
                gr.DrawArc(pen, (V1.X - 2 * R) * Main.zoom, (V1.Y - 2 * R) * Main.zoom, 2 * R, 2 * R, 90, 270);
            }
            else
            {
                gr.DrawLine(darkGoldPen, V1.X * Main.zoom, V1.Y * Main.zoom, V2.X * Main.zoom, V2.Y * Main.zoom);
                drawVertex(V1.X * Main.zoom, V1.Y * Main.zoom);
                drawVertex(V2.X * Main.zoom, V2.Y * Main.zoom);
            }
        }

        public void drawALLGraph(List<Vertex> V, List<Edge> E, int rand = 0)
        {
            Pen pen;
            if (rand != 0)
            {
                pen = new Pen(color)
                {
                    Width = 2 * Main.zoom
                };
            }
            else
            {
                pen = darkGoldPen;
                pen.Width = 2 * Main.zoom;
            };
            //рисуем ребра
            for (int i = 0; i < E.Count; i++)
            {
                if (E[i].v1 == E[i].v2)
                {
                    gr.DrawArc(pen, V[E[i].v1].X - 2 * R * Main.zoom, V[E[i].v1].Y - 2 * R * Main.zoom, 2 * R * Main.zoom, 2 * R * Main.zoom, 90, 270);
                }
                else
                {
                    //проблема с отрисовкой
                    if (E[i].v1 < V.Count && E[i].v2 < V.Count)
                    {
                        gr.DrawLine(pen, V[E[i].v1].X * Main.zoom, V[E[i].v1].Y * Main.zoom, V[E[i].v2].X * Main.zoom, V[E[i].v2].Y * Main.zoom);
                    }
                }
            }
            //рисуем вершины
            for (int i = 0; i < V.Count; i++)
            {
                if (rand != 0)
                    drawRouteVertex(V[i].X, V[i].Y);
                else
                    drawVertex(V[i].X, V[i].Y);
            }

            foreach (var stopPoints in Main.allstopPoints)
            {
                drawStopVertex(stopPoints.X, stopPoints.Y);
            }
            if (Main.selectedRoute != null)
            {
                if (Main.stopPoints.ContainsKey(Main.selectedRoute))
                {
                    foreach (var stopPoints in Main.stopPoints[Main.selectedRoute])
                    {
                        drawStopRouteVertex(stopPoints.X, stopPoints.Y);
                    }
                }
            }

            foreach (var tl in Main.traficLights)
            {
                if (tl.status == Status.GREEN)
                {
                    Main.G.drawGreenVertex(tl.x, tl.y);
                }
                else if (tl.status == Status.YELLOW)
                {
                    Main.G.drawStopVertex(tl.x, tl.y);
                }
                else if (tl.status == Status.RED)
                {
                    Main.G.drawSelectedVertex(tl.x, tl.y);
                }
            }

        }


    }


    public class Bus : ICloneable
    {
        public static int ScrollX;
        public static int ScrollY;
        public int skip = 1;
        public static string busImg = "../../Resources/newbus.PNG";
        public static string offBusImg = "../../Resources/bus.PNG";
        //таймер для движения
        public List<Epicenter> Epicenters { get; set; } = new List<Epicenter>();
        int stopTime = 0;
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
        //за сколько времени автобус нашел эпицентр
        public static int FoundTime { get; set; }
        //проверка нашел ли автобус эпицентр
        public bool EpicFounded { get; set; }
        public int oldSize;
        static public int? ZoomCoef { get; set; } = 1;
        static public int small { get; set; } = 1000;
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
        public void setBusSize()
        {
            //if (ZoomCoef < oldZoom)
            //{
            //    if (ZoomCoef == 1)
            //        busPic.Size = new Size(Main.sizeBus, Main.sizeBus);
            //    else
            //        busPic.Size = new Size(oldSize / (int)ZoomCoef, oldSize / (int)ZoomCoef);
            //}
            //else
            //{
            //    if (ZoomCoef == 1)
            //        busPic.Size = new Size(Main.sizeBus, Main.sizeBus);
            //    else
            //        busPic.Size = new Size(oldSize * (int)ZoomCoef, oldSize * (int)ZoomCoef);

            //}
            oldZoom = (int)ZoomCoef;
        }

        public List<Point> getCoordinates()
        {
            return Coordinates;
        }


        public void setAllCoordinates(List<Point> A)
        {
            Coordinates = A;
        }

        public int? getLocate()
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

        public bool tracker { get; set; }


        public Bus(Image busPic, int PositionAt, bool Turn, string route, List<Point> Coordinates, bool not)
        {
            tracker = not;
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
        public async Task asMoveWithoutGraphics()
        {
            await Task.Run(() => MoveWithoutGraphics());
        }
        public void MoveWithoutGraphics()
        {
            if (tracker == true)
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
            if (tracker == true)
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
        public async void MoveWithGraphicsAsync()
        {
            //await Task.Run(() => MoveWithGraphics());
        }

        readonly Random rnd = new Random();
        public bool skipTraffics = false;
        public int skipStops = 1;
        public int skipEnd = 5;
        //движение с графикой (для визуализации движения)
        delegate void Dpoint(Point pos);
        public void AlignBus()
        {

            //if (PositionAt < Coordinates.Count)
            //{
            //    busPic.Invoke(new Dpoint((pos) => busPic.Location = pos), new Point((Coordinates[PositionAt].X * (int)ZoomCoef) + ScrollX - busPic.Width / 2, (Coordinates[PositionAt].Y * (int)ZoomCoef) + ScrollY - busPic.Height / 2));
            //}
            //else
            //{
            //    busPic.Invoke(new Dpoint((pos) => busPic.Location = pos), new Point((Coordinates[PositionAt - 1].X * (int)ZoomCoef) + ScrollX - busPic.Width / 2, (Coordinates[PositionAt - 1].Y * (int)ZoomCoef) + ScrollY - busPic.Height / 2));
            //}

        }
        int checkStop = 0;

        private void stopDown()
        {
            if (skip != 0)
                skip -= 1;
            if (skipStops != 0)
                skipStops -= 1;
            if (skipEnd != 0)
                skipEnd -= 1;
            if (stopTime != 0)
                stopTime -= 1;
        }
        public async Task MoveWithGraphics(Graphics G)
        {
            Bus thisBus = new Bus(busPic, PositionAt, TurnBack, route, Coordinates, tracker);
            List<Bus> buses = Main.buses;
            buses.Remove(thisBus);
            if (checkStop == 0)
            {
                if (TurnBack == false)
                {
                    if (PositionAt < Coordinates.Count - 1)
                    {
                        if (skipStops == 0)
                        {
                            if (Main.buses.Count != 0)
                            {
                                foreach (var sp in buses)
                                {
                                    if (Math.Pow((sp.Coordinates[sp.PositionAt].X - (Coordinates[PositionAt].X / (int)ZoomCoef)), 2) + Math.Pow((sp.Coordinates[sp.PositionAt].Y - (Coordinates[PositionAt].Y / (int)ZoomCoef)), 2) <= sp.R * sp.R && sp.stopTime != 0 && sp.TurnBack == TurnBack)
                                    {
                                        Console.WriteLine("Turn false");
                                        stopTime = sp.stopTime;
                                        checkStop = stopTime;
                                        skip = 100;
                                        skipStops = 50;
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
                                        stopTime = rnd.Next(0, 200);
                                        skipStops = 100;
                                        checkStop = stopTime;
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
                                    if ((Math.Pow((double.Parse((sp.x * (int)ZoomCoef - Coordinates[PositionAt].X * (int)ZoomCoef).ToString())), 2) + Math.Pow((double.Parse(((sp.y * (int)ZoomCoef - Coordinates[PositionAt].Y * (int)ZoomCoef)).ToString())), 2) <= Main.G.R * (int)ZoomCoef * Main.G.R * (int)ZoomCoef * (Main.G.R * (int)ZoomCoef)) && sp.status != Status.RED)
                                    {
                                        skip = 100;
                                        break;
                                    }
                                    else
                                    if ((Math.Pow((double.Parse((sp.x * (int)ZoomCoef - Coordinates[PositionAt].X * (int)ZoomCoef).ToString())), 2) + Math.Pow((double.Parse(((sp.y * (int)ZoomCoef - Coordinates[PositionAt].Y * (int)ZoomCoef)).ToString())), 2) <= Main.G.R * (int)ZoomCoef * Main.G.R * (int)ZoomCoef * (Main.G.R * (int)ZoomCoef)) && sp.status == Status.RED)
                                    {
                                        skip = 100;
                                        stopTime = (sp.bal * 40);
                                        checkStop = stopTime;
                                        break;

                                    }
                                }
                            }
                        }
                        G.DrawImage(busPic, Coordinates[PositionAt].X * (int)ZoomCoef - busPic.Width / 2, Coordinates[PositionAt].Y * (int)ZoomCoef - busPic.Height / 2);
                        stopDown();
                        PositionAt++;
                    }
                    else
                    {
                        stopDown();
                        TurnBack = true;
                        PositionAt--;
                    }
                }
                else
                {
                    if (PositionAt > 0)
                    {
                        if (PositionAt < Coordinates.Count)
                        {
                            if (skipStops == 0)
                            {
                                if (Main.buses.Count != 0)
                                {
                                    foreach (var sp in buses)
                                    {
                                        if (Math.Pow((sp.Coordinates[sp.PositionAt].X - (Coordinates[PositionAt].X / (int)ZoomCoef)), 2) + Math.Pow((sp.Coordinates[sp.PositionAt].Y - (Coordinates[PositionAt].Y / (int)ZoomCoef)), 2) <= sp.R * sp.R && sp.stopTime != 0 && sp.TurnBack == TurnBack)
                                        {
                                            Console.WriteLine("Turn false");
                                            stopTime = sp.stopTime;
                                            checkStop = stopTime;
                                            skip = 100;
                                            skipStops = 50;
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
                                            stopTime = rnd.Next(0, 200);
                                            skipStops = 50;
                                            checkStop = stopTime;
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
                                        if ((Math.Pow((double.Parse((sp.x * (int)ZoomCoef - Coordinates[PositionAt].X * (int)ZoomCoef).ToString())), 2) + Math.Pow((double.Parse(((sp.y * (int)ZoomCoef - Coordinates[PositionAt].Y * (int)ZoomCoef)).ToString())), 2) <= Main.G.R * (int)ZoomCoef * Main.G.R * (int)ZoomCoef * (Main.G.R * (int)ZoomCoef)) && sp.status != Status.RED)
                                        {
                                            skip = 100;
                                            break;
                                        }
                                        else
                                        if ((Math.Pow((double.Parse((sp.x * (int)ZoomCoef - Coordinates[PositionAt].X * (int)ZoomCoef).ToString())), 2) + Math.Pow((double.Parse(((sp.y * (int)ZoomCoef - Coordinates[PositionAt].Y * (int)ZoomCoef)).ToString())), 2) <= Main.G.R * (int)ZoomCoef * Main.G.R * (int)ZoomCoef * (Main.G.R * (int)ZoomCoef)) && sp.status == Status.RED)
                                        {
                                            skip = 100;
                                            stopTime = (sp.bal * 40);
                                            checkStop = stopTime;
                                            break;
                                        }
                                    }
                                }
                            }


                            G.DrawImage(busPic, Coordinates[PositionAt].X * (int)ZoomCoef - busPic.Width / 2, Coordinates[PositionAt].Y * (int)ZoomCoef - busPic.Height / 2);
                            stopDown();
                            PositionAt--;
                        }
                    }
                    else
                    {
                        stopDown();
                        TurnBack = false;
                        PositionAt++;
                    }

                }
            }
            else
            {
                if (checkStop != 0)
                    checkStop -= 1;
                G.DrawImage(busPic, Coordinates[PositionAt].X * (int)ZoomCoef - busPic.Width / 2, Coordinates[PositionAt].Y * (int)ZoomCoef - busPic.Height / 2);
            }

        }

        public string getRoute()
        {
            return route;
        }

        public async Task asDetectEpicenter2()
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
                            if (((Coordinates[PositionAt].X * ZoomCoef) >= Square.x * ZoomCoef) && ((Coordinates[PositionAt].X * ZoomCoef) <= Square.x * ZoomCoef + GridPart.width * ZoomCoef) && ((Coordinates[PositionAt].Y * ZoomCoef) >= Square.y * ZoomCoef) && ((Coordinates[PositionAt].Y * ZoomCoef) <= (Square.y * ZoomCoef + GridPart.height * ZoomCoef)))
                            {
                                switch (Sector.Key)
                                {
                                    case 1:
                                        if (Square.check == false)
                                        {
                                            Square.check = true;
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

                        if (((Main.TheGrid[Main.AllGridsInRoutes[route][(int)PositionAt]].x == Square.x) && (Main.TheGrid[Main.AllGridsInRoutes[route][(int)PositionAt]].y == Square.y)))
                        {
                            switch (Sector.Key)
                            {
                                case 1:
                                    if (Square.check == false)
                                    {
                                        Square.check = true;
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
            }

            return 0;
        }
        public async Task asyncDetectRectangle2()
        {
            await Task.Run(() => DetectRectangle2());
        }
        //определение квадрата по новому
        public void DetectRectangle2()
        {

            for (int i = 0; i < Main.TheGrid.Count; i++)
            {
                if ((PositionAt < Coordinates.Count))

                    if (((Coordinates[PositionAt].X) > Main.TheGrid[i].x) && ((Coordinates[PositionAt].X) < Main.TheGrid[i].x + GridPart.width) && ((Coordinates[PositionAt].Y) > Main.TheGrid[i].y) && ((Coordinates[PositionAt].Y) < (Main.TheGrid[i].y + GridPart.height)))
                    {
                        Locate = i;
                    }
            }

        }



    }
    public class Epicenter : ICloneable
    {
        public System.Threading.Mutex mutex = new System.Threading.Mutex();
        public SerializableDictionary<int, List<GridPart>> EpicenterGrid { get; set; }
        public List<int> EpicenterGrid2 { get; set; }
        public SerializableDictionary<int, List<GridPart>> getEpicenterGrid()
        {
            return EpicenterGrid;
        }
        public int DetectCount { get; set; }

        public List<GridPart> TheGrid { get; set; }
        public Epicenter(List<GridPart> TheGrid)
        {
            this.TheGrid = TheGrid;
            EpicenterGrid = new SerializableDictionary<int, List<GridPart>>();
        }
        public Epicenter()
        { }

        public Dictionary<int, List<GridPart>> GetEpicenterGrid()
        {
            return EpicenterGrid;
        }
        public object Clone()
        {
            return MemberwiseClone();
        }
        public void Recreate(Dictionary<string, List<GridPart>> PollutionInRoutes)
        {
            EpicenterGrid.Add(1, new List<GridPart>());
            EpicenterGrid.Add(2, new List<GridPart>());
            EpicenterGrid.Add(3, new List<GridPart>());
            List<GridPart> OrangeBuffer = new List<GridPart>();
            foreach (var Pollutedroute in PollutionInRoutes.Values)
            {
                foreach (var PollutedSqure in Pollutedroute)
                {
                    if (PollutedSqure.status != 0)
                    {
                        bool net = false;
                        switch (PollutedSqure.status)
                        {
                            case 2:
                                foreach (var part in EpicenterGrid[2])
                                {
                                    if ((part.x == PollutedSqure.x) && (part.y == PollutedSqure.y))
                                    {
                                        net = true;
                                        break;
                                    }
                                }
                                if (net == false)
                                {
                                    OrangeBuffer.Add(new GridPart(PollutedSqure.x, PollutedSqure.y));
                                }
                                break;
                            case 3:
                                foreach (var part in EpicenterGrid[1])
                                {
                                    if ((part.x == PollutedSqure.x) && (part.y == PollutedSqure.y))
                                    {
                                        net = true;
                                        break;
                                    }
                                }
                                if (net == false)
                                {
                                    EpicenterGrid[1].Add(new GridPart(PollutedSqure.x, PollutedSqure.y));
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            List<GridPart> BufferList = new List<GridPart>();
            foreach (var RedGrid in EpicenterGrid[1])
            {
                for (int i = 1; i <= 5; i++)
                {
                    GridPart ScanGrid = new GridPart(RedGrid.x, RedGrid.y - GridPart.height * i);
                    for (int j = 0; j < 4; j++)
                    {
                        while (ScanGrid.x < RedGrid.x + GridPart.width * i)
                        {
                            ScanGrid.x += GridPart.width;
                            foreach (var SearchGrid in EpicenterGrid[1])
                            {
                                if ((ScanGrid.x == SearchGrid.x) && (ScanGrid.y == SearchGrid.y))
                                {

                                    List<string> Parameters = new List<string>();
                                    GridPart bufpart = new GridPart(ScanGrid.x, ScanGrid.y);
                                    while (((bufpart.x == RedGrid.x) && (bufpart.y == RedGrid.y)) == false)
                                    {

                                        if (bufpart.x == RedGrid.x)
                                        {
                                            bufpart.y += GridPart.height;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }
                                        else
                                        if (bufpart.y == RedGrid.y)
                                        {
                                            bufpart.x -= GridPart.width;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }
                                        else
                                        {
                                            bufpart.y += GridPart.height;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                            bufpart.x -= GridPart.width;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }
                                    }
                                }
                            }
                        }
                        while (ScanGrid.y < RedGrid.y)
                        {
                            ScanGrid.y += GridPart.height;
                            foreach (var SearchGrid in EpicenterGrid[1])
                            {
                                if ((ScanGrid.x == SearchGrid.x) && (ScanGrid.y == SearchGrid.y))
                                {
                                    GridPart bufpart = new GridPart(ScanGrid.x, ScanGrid.y);
                                    while (((bufpart.x == RedGrid.x) && (bufpart.y == RedGrid.y)) == false)
                                    {

                                        if (bufpart.x == RedGrid.x)
                                        {
                                            bufpart.y += GridPart.height;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }
                                        else
                                        if (bufpart.y == RedGrid.y)
                                        {
                                            bufpart.x -= GridPart.width;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }
                                        else
                                        {
                                            bufpart.x -= GridPart.width;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                            bufpart.y += GridPart.height;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }

                                    }
                                }
                            }
                        }
                        while (ScanGrid.y < RedGrid.y + GridPart.height * i)
                        {
                            ScanGrid.y += GridPart.height;
                            foreach (var SearchGrid in EpicenterGrid[1])
                            {
                                if ((ScanGrid.x == SearchGrid.x) && (ScanGrid.y == SearchGrid.y))
                                {
                                    List<string> Parameters = new List<string>();
                                    GridPart bufpart = new GridPart(ScanGrid.x, ScanGrid.y);
                                    while (((bufpart.x == RedGrid.x) && (bufpart.y == RedGrid.y)) == false)
                                    {

                                        if (bufpart.x == RedGrid.x)
                                        {
                                            bufpart.y -= GridPart.height;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }
                                        else
                                        if (bufpart.y == RedGrid.y)
                                        {
                                            bufpart.x -= GridPart.width;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }
                                        else
                                        {
                                            bufpart.y -= GridPart.height;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                            bufpart.x -= GridPart.width;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }

                                    }
                                }
                            }
                        }
                        while (ScanGrid.x > RedGrid.x)
                        {
                            ScanGrid.x -= GridPart.width;
                            foreach (var SearchGrid in EpicenterGrid[1])
                            {
                                if ((ScanGrid.x == SearchGrid.x) && (ScanGrid.y == SearchGrid.y))
                                {
                                    GridPart bufpart = new GridPart(ScanGrid.x, ScanGrid.y);
                                    while (((bufpart.x == RedGrid.x) && (bufpart.y == RedGrid.y)) == false)
                                    {

                                        if (bufpart.x == RedGrid.x)
                                        {
                                            bufpart.y -= GridPart.height;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }
                                        else
                                        if (bufpart.y == RedGrid.y)
                                        {
                                            bufpart.x -= GridPart.width;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }
                                        else
                                        {
                                            bufpart.y -= GridPart.height;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                            bufpart.x -= GridPart.width;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }

                                    }
                                }
                            }
                        }
                        while (ScanGrid.x > RedGrid.x - GridPart.width * i)
                        {
                            ScanGrid.x -= GridPart.width;
                            foreach (var SearchGrid in EpicenterGrid[1])
                            {
                                if ((ScanGrid.x == SearchGrid.x) && (ScanGrid.y == SearchGrid.y))
                                {
                                    List<string> Parameters = new List<string>();
                                    GridPart bufpart = new GridPart(ScanGrid.x, ScanGrid.y);
                                    while (((bufpart.x == RedGrid.x) && (bufpart.y == RedGrid.y)) == false)
                                    {

                                        if (bufpart.x == RedGrid.x)
                                        {
                                            bufpart.y -= GridPart.height;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }
                                        else
                                        if (bufpart.y == RedGrid.y)
                                        {
                                            bufpart.x += GridPart.width;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }
                                        else
                                        {
                                            bufpart.y -= GridPart.height;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                            bufpart.x += GridPart.width;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }

                                    }
                                }
                            }
                        }
                        while (ScanGrid.y > RedGrid.y)
                        {
                            ScanGrid.y -= GridPart.height;
                            foreach (var SearchGrid in EpicenterGrid[1])
                            {
                                if ((ScanGrid.x == SearchGrid.x) && (ScanGrid.y == SearchGrid.y))
                                {
                                    List<string> Parameters = new List<string>();
                                    GridPart bufpart = new GridPart(ScanGrid.x, ScanGrid.y);
                                    while (((bufpart.x == RedGrid.x) && (bufpart.y == RedGrid.y)) == false)
                                    {

                                        if (bufpart.x == RedGrid.x)
                                        {
                                            bufpart.y -= GridPart.height;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }
                                        else
                                        if (bufpart.y == RedGrid.y)
                                        {
                                            bufpart.x += GridPart.width;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }
                                        else
                                        {
                                            bufpart.y -= GridPart.height;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                            bufpart.x += GridPart.width;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }

                                    }
                                }
                            }
                        }
                        while (ScanGrid.y > RedGrid.y - GridPart.width * i)
                        {
                            ScanGrid.y -= GridPart.height;
                            foreach (var SearchGrid in EpicenterGrid[1])
                            {
                                if ((ScanGrid.x == SearchGrid.x) && (ScanGrid.y == SearchGrid.y))
                                {

                                    GridPart bufpart = new GridPart(ScanGrid.x, ScanGrid.y);
                                    while (((bufpart.x == RedGrid.x) && (bufpart.y == RedGrid.y)) == false)
                                    {

                                        if (bufpart.x == RedGrid.x)
                                        {
                                            bufpart.y += GridPart.height;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }
                                        else
                                        if (bufpart.y == RedGrid.y)
                                        {
                                            bufpart.x += GridPart.width;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }
                                        else
                                        {
                                            bufpart.y += GridPart.height;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                            bufpart.x += GridPart.width;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }

                                    }
                                }
                            }
                        }
                        while (ScanGrid.x < RedGrid.x)
                        {
                            ScanGrid.x += GridPart.width;
                            foreach (var SearchGrid in EpicenterGrid[1])
                            {
                                if ((ScanGrid.x == SearchGrid.x) && (ScanGrid.y == SearchGrid.y))
                                {
                                    List<string> Parameters = new List<string>();
                                    Parameters = EpicenterGenerator(ScanGrid, Parameters);
                                    GridPart bufpart = new GridPart(ScanGrid.x, ScanGrid.y);
                                    while (((bufpart.x == RedGrid.x) && (bufpart.y == RedGrid.y)) == false)
                                    {

                                        if (bufpart.x == RedGrid.x)
                                        {
                                            bufpart.y += GridPart.height;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }
                                        else
                                        if (bufpart.y == RedGrid.y)
                                        {
                                            bufpart.x += GridPart.width;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }
                                        else
                                        {
                                            bufpart.y += GridPart.height;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                            bufpart.x += GridPart.width;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }

                                    }
                                }
                            }
                        }
                    }

                }

            }
            foreach (var BufGrid in BufferList)
            {
                bool net = false;
                foreach (var EpGrid in EpicenterGrid[1])
                {
                    if ((BufGrid.x == EpGrid.x) && (BufGrid.y == EpGrid.y))
                    {
                        net = true;
                        break;
                    }

                }
                if (net == false)
                {
                    EpicenterGrid[1].Add(new GridPart(BufGrid.x, BufGrid.y));
                }
            }
            for (int i = 2; i < 4; i++)
            {

                List<GridPart> fillEpicenter = new List<GridPart>();
                foreach (var item in EpicenterGrid[i - 1])
                {
                    fillEpicenter.Add(new GridPart(item.x, item.y));
                }
                foreach (var itwms in fillEpicenter)
                {
                    List<string> Parameter = new List<string>();
                    Parameter = EpicenterGenerator(itwms, Parameter);
                    if (Parameter.Count > 0)
                        foreach (var items in Parameter)
                        {
                            Creater(items, itwms, i);
                        }
                }
                fillEpicenter = new List<GridPart>();
                foreach (var item in EpicenterGrid[i])
                {
                    fillEpicenter.Add(new GridPart(item.x, item.y));
                }
                foreach (var itwms in fillEpicenter)
                {
                    List<string> Parameter = new List<string>();
                    Parameter = EpicenterGenerator(itwms, Parameter);
                    if (Parameter.Count > 0)
                        foreach (var items in Parameter)
                        {
                            Creater(items, itwms, i);
                        }
                }
                if (i == 2)
                {
                    foreach (var BufGrid in OrangeBuffer)
                    {
                        bool net = false;
                        foreach (var EpGrid in EpicenterGrid[i])
                        {
                            if ((BufGrid.x == EpGrid.x) && (BufGrid.y == EpGrid.y))
                            {
                                net = true;
                                break;
                            }

                        }
                        if (net == false)
                        {
                            int? Red = null;
                            foreach (var EpGrid in EpicenterGrid[1])
                            {
                                if ((BufGrid.x == EpGrid.x) && (BufGrid.y == EpGrid.y))
                                {
                                    Red = EpicenterGrid[1].IndexOf(EpGrid);
                                    break;
                                }

                            }
                            if (Red != null)
                            {
                                EpicenterGrid[1].RemoveAt((int)Red);
                                EpicenterGrid[i].Add(new GridPart(BufGrid.x, BufGrid.y));
                            }
                            else
                            {
                                EpicenterGrid[i].Add(new GridPart(BufGrid.x, BufGrid.y));
                            }

                        }
                    }
                }

            }
        }
        ////////
        public void EpicMoving(List<string> Parameters)
        {
            for (int i = 2; i < EpicenterGrid.Count + 1; i++)
            {

                EpicenterGrid[i].Clear();

            }

            //EpicenterGrid[1].RemoveAt(EpicenterGrid[1].IndexOf(EpicenterGrid[1].Last()));
        
            List<Point> ForRemove = new List<Point>();
            foreach (var gridPart in EpicenterGrid[1])
            {
                gridPart.IsMovedAway = false;
                foreach (var item in Parameters)
                {
                    if (gridPart.IsMovedAway == false)
                        switch (item)
                        {

                            case "right":
                                gridPart.x = gridPart.x + GridPart.width;
                                if (!(gridPart.x <= TheGrid.Last().x))
                                {
                                    gridPart.IsMovedAway = true;
                                    ForRemove.Add(new Point(gridPart.x, gridPart.y));
                                }
                                break;
                            //case "right-down":
                            //    break;
                            //case "right-up":
                            //    break;
                            case "down":
                                gridPart.y = gridPart.y + GridPart.height;
                                if (!(gridPart.y <= TheGrid.Last().y))
                                {
                                    gridPart.IsMovedAway = true;
                                    ForRemove.Add(new Point(gridPart.x, gridPart.y));
                                }
                                break;
                            case "up":
                                gridPart.y -= GridPart.height;
                                if (!(gridPart.y >= TheGrid.First().y))
                                {
                                    gridPart.IsMovedAway = true;
                                    ForRemove.Add(new Point(gridPart.x, gridPart.y));
                                }

                                break;
                            case "left":
                                gridPart.x = gridPart.x - GridPart.width;
                                if (!((gridPart.x >= TheGrid.First().x)))
                                {
                                    gridPart.IsMovedAway = true;
                                    ForRemove.Add(new Point(gridPart.x, gridPart.y));
                                }
                                break;
                            //case "left-down":
                            //    break;
                            //case "left-up":
                            //    break;
                            default:
                                break;
                        }
                }

            }

            foreach (var OutMovedGrid in ForRemove)
            {
                int IndexOfOutMovedGrid = 0;
                foreach (var gridPart in EpicenterGrid[1])
                {
                    if ((gridPart.x == OutMovedGrid.X) && (gridPart.y == OutMovedGrid.Y))
                    {
                        IndexOfOutMovedGrid = EpicenterGrid[1].IndexOf(gridPart);
                    }

                }
                EpicenterGrid[1].RemoveAt(IndexOfOutMovedGrid);

            }



            List<string> Parameter = new List<string>();
            //
            for (int i = 2; i < 4; i++)
            {

                // EpicenterGrid.Add(i, new List<GridPart>());
                List<GridPart> fillEpicenter = new List<GridPart>();
                foreach (var item in EpicenterGrid[i - 1])
                {
                    fillEpicenter.Add(new GridPart(item.x, item.y));
                }
                foreach (var itwms in fillEpicenter)
                {
                    Parameter = new List<string>();
                    Parameter = EpicenterGenerator(itwms, Parameter);
                    if (Parameter.Count > 0)
                        foreach (var items in Parameter)
                        {
                            Creater(items, itwms, i);
                        }
                }
                fillEpicenter = new List<GridPart>();
                foreach (var item in EpicenterGrid[i])
                {
                    fillEpicenter.Add(new GridPart(item.x, item.y));
                }
                foreach (var itwms in fillEpicenter)
                {
                    Parameter = new List<string>();
                    Parameter = EpicenterGenerator(itwms, Parameter);
                    if (Parameter.Count > 0)
                        foreach (var items in Parameter)
                        {
                            Creater(items, itwms, i);
                        }
                }

            }
            ///
        }
        ///
        public void ExpandEpic(List<string> Parameters)
        {
            for (int i = 2; i < EpicenterGrid.Count + 1; i++)
            {
                EpicenterGrid[i].Clear();
            }
            var rand = new Random();
            for (int i = 0; i < rand.Next(5, 10); i++)
            {
                List<string> Parameter = new List<string>();

                GridPart StarterEpicPart = EpicenterGrid[1][rand.Next(EpicenterGrid[1].IndexOf(EpicenterGrid[1].First()), EpicenterGrid[1].IndexOf(EpicenterGrid[1].Last()))];

                Parameter = EpicenterGenerator(StarterEpicPart, Parameter);
                if (Parameter.Count > 0)
                {

                    foreach (var item in Parameter)
                    {
                        if (Parameters.Contains(item))
                            Creater(item, StarterEpicPart, 1);
                    }
                }
            }
            ////
            for (int i = 2; i < 4; i++)
            {
                // EpicenterGrid.Add(i, new List<GridPart>());
                List<GridPart> fillEpicenter = new List<GridPart>();
                foreach (var item in EpicenterGrid[i - 1])
                {
                    fillEpicenter.Add(new GridPart(item.x, item.y));
                }
                foreach (var itwms in fillEpicenter)
                {
                    List<string> Parameter = new List<string>();
                    Parameter = EpicenterGenerator(itwms, Parameter);
                    if (Parameter.Count > 0)
                        foreach (var items in Parameter)
                        {
                            Creater(items, itwms, i);
                        }
                }
                fillEpicenter = new List<GridPart>();
                foreach (var item in EpicenterGrid[i])
                {
                    fillEpicenter.Add(new GridPart(item.x, item.y));
                }
                foreach (var itwms in fillEpicenter)
                {
                    List<string> Parameter = new List<string>();
                    Parameter = EpicenterGenerator(itwms, Parameter);
                    if (Parameter.Count > 0)
                        foreach (var items in Parameter)
                        {
                            Creater(items, itwms, i);
                        }
                }

            }
            ///
        }
        public void CreateRandomEpicenter(int SizeParam, int? StartPos)
        {
            var rand = new Random();
            EpicenterGrid = new SerializableDictionary<int, List<GridPart>>
            {
                { 1, new List<GridPart>() }
            };
            if (StartPos == null)
            {
                EpicenterGrid[1].Add(new GridPart(TheGrid[rand.Next(TheGrid.IndexOf(TheGrid.First()), TheGrid.IndexOf(TheGrid.Last()))].x, TheGrid[rand.Next(TheGrid.IndexOf(TheGrid.First()), TheGrid.IndexOf(TheGrid.Last()))].y));
            }
            else
            {
                EpicenterGrid[1].Add(new GridPart(TheGrid[(int)StartPos].x, TheGrid[(int)StartPos].y));

            }
            while (EpicenterGrid[1].Count < SizeParam)
            {
                List<string> Parameter = new List<string>();

                GridPart StarterEpicPart = EpicenterGrid[1][rand.Next(EpicenterGrid[1].IndexOf(EpicenterGrid[1].First()), EpicenterGrid[1].IndexOf(EpicenterGrid[1].Last()))];

                Parameter = EpicenterGenerator(StarterEpicPart, Parameter);
                if (Parameter.Count > 0)
                    foreach (var item in Parameter)
                    {
                        if (EpicenterGrid[1].Count < SizeParam)
                        {
                            Creater(item, StarterEpicPart, 1);
                        }

                    }
            }
            for (int i = 2; i < 4; i++)
            {
                EpicenterGrid.Add(i, new List<GridPart>());
                List<GridPart> FillEpicenter = new List<GridPart>();
                foreach (var item in EpicenterGrid[i - 1])
                {
                    FillEpicenter.Add(new GridPart(item.x, item.y));
                }
                foreach (var itwms in FillEpicenter)
                {
                    List<string> Parameter = new List<string>();
                    Parameter = EpicenterGenerator(itwms, Parameter);
                    if (Parameter.Count > 0)
                        foreach (var items in Parameter)
                        {
                            Creater(items, itwms, i);
                        }
                }
                FillEpicenter = new List<GridPart>();
                foreach (var item in EpicenterGrid[i])
                {
                    FillEpicenter.Add(new GridPart(item.x, item.y));
                }
                foreach (var itwms in FillEpicenter)
                {
                    List<string> Parameter = new List<string>();
                    Parameter = EpicenterGenerator(itwms, Parameter);
                    if (Parameter.Count > 0)
                        foreach (var items in Parameter)
                        {
                            Creater(items, itwms, i);
                        }
                }

            }

        }
        public List<string> EpicenterGenerator(GridPart EpicPart, List<string> Parameter)
        {
            bool net = false;
            if (EpicPart.x < TheGrid.Last().x)
            {
                net = false;
                for (int d = 1; d < EpicenterGrid.Count + 1; d++)
                    foreach (var part in EpicenterGrid[d])
                    {
                        if ((part.x == EpicPart.x + GridPart.width) && (part.y == EpicPart.y))
                        {
                            net = true;
                            break;
                        }
                    }
                if (net == false)
                {
                    Parameter.Add("right");
                }

            }
            if ((EpicPart.x < TheGrid.Last().x) && (EpicPart.y < TheGrid.Last().y))
            {
                net = false;
                for (int d = 1; d < EpicenterGrid.Count + 1; d++)
                    foreach (var part in EpicenterGrid[d])
                    {
                        if ((part.x == EpicPart.x + GridPart.width) && (part.y == EpicPart.y + GridPart.height))
                        {
                            net = true;
                            break;
                        }
                    }
                if (net == false)
                {
                    Parameter.Add("right-down");
                }

            }
            if ((EpicPart.x < TheGrid.Last().x) && (EpicPart.y > TheGrid.First().y))
            {
                net = false;
                for (int d = 1; d < EpicenterGrid.Count + 1; d++)
                    foreach (var part in EpicenterGrid[d])
                    {
                        if ((part.x == EpicPart.x + GridPart.width) && (part.y == EpicPart.y - GridPart.height))
                        {
                            net = true;
                            break;
                        }
                    }
                if (net == false)
                {
                    Parameter.Add("right-up");
                }

            }
            if (EpicPart.y < TheGrid.Last().y)
            {
                net = false;
                for (int d = 1; d < EpicenterGrid.Count + 1; d++)
                    foreach (var part in EpicenterGrid[d])
                    {
                        if ((part.x == EpicPart.x) && (part.y == EpicPart.y + GridPart.height))
                        {
                            net = true;
                            break;
                        }
                    }
                if (net == false)
                {
                    Parameter.Add("down");
                }
            }

            if (EpicPart.y > TheGrid.First().y)
            {
                net = false;
                for (int d = 1; d < EpicenterGrid.Count + 1; d++)
                    foreach (var part in EpicenterGrid[d])
                    {
                        if ((part.x == EpicPart.x) && (part.y == EpicPart.y - GridPart.height))
                        {
                            net = true;
                            break;
                        }
                    }
                if (net == false)
                {
                    Parameter.Add("up");
                }
            }
            if (EpicPart.x > TheGrid.First().x)
            {
                net = false;
                for (int d = 1; d < EpicenterGrid.Count + 1; d++)
                    foreach (var part in EpicenterGrid[d])
                    {
                        if ((part.x == EpicPart.x - GridPart.width) && (part.y == EpicPart.y))
                        {
                            net = true;
                            break;
                        }
                    }
                if (net == false)
                {
                    Parameter.Add("left");
                }
            }
            if ((EpicPart.x > TheGrid.First().x) && (EpicPart.y > TheGrid.First().y))
            {
                net = false;
                for (int d = 1; d < EpicenterGrid.Count + 1; d++)
                    foreach (var part in EpicenterGrid[d])
                    {
                        if ((part.x == EpicPart.x - GridPart.width) && (part.y == EpicPart.y - GridPart.height))
                        {
                            net = true;
                            break;
                        }
                    }
                if (net == false)
                {
                    Parameter.Add("left-up");
                }

            }
            if ((EpicPart.x > TheGrid.First().x) && (EpicPart.y < TheGrid.Last().y))
            {
                net = false;
                for (int d = 1; d < EpicenterGrid.Count + 1; d++)
                    foreach (var part in EpicenterGrid[d])
                    {
                        if ((part.x == EpicPart.x - GridPart.width) && (part.y == EpicPart.y + GridPart.height))
                        {
                            net = true;
                            break;
                        }
                    }
                if (net == false)
                {
                    Parameter.Add("left-down");
                }

            }
            return Parameter;
        }
        public void Creater(string Param, GridPart StarterEpicPart, int level)
        {
            switch (Param)
            {
                case "right":
                    EpicenterGrid[level].Add(new GridPart(StarterEpicPart.x + GridPart.width, StarterEpicPart.y));
                    break;
                case "right-down":
                    EpicenterGrid[level].Add(new GridPart(StarterEpicPart.x + GridPart.width, StarterEpicPart.y + GridPart.height));
                    break;
                case "right-up":
                    EpicenterGrid[level].Add(new GridPart(StarterEpicPart.x + GridPart.width, StarterEpicPart.y - GridPart.height));
                    break;
                case "down":
                    EpicenterGrid[level].Add(new GridPart(StarterEpicPart.x, StarterEpicPart.y + GridPart.height));
                    break;
                case "up":
                    EpicenterGrid[level].Add(new GridPart(StarterEpicPart.x, StarterEpicPart.y - GridPart.height));
                    break;
                case "left":
                    EpicenterGrid[level].Add(new GridPart(StarterEpicPart.x - GridPart.width, StarterEpicPart.y));
                    break;
                case "left-down":
                    EpicenterGrid[level].Add(new GridPart(StarterEpicPart.x - GridPart.width, StarterEpicPart.y + GridPart.height));
                    break;
                case "left-up":
                    EpicenterGrid[level].Add(new GridPart(StarterEpicPart.x - GridPart.width, StarterEpicPart.y - GridPart.height));
                    break;
                default:
                    break;
            }
        }

        public void DrawEpicenter(DrawGraph g, int zoom)
        {
            for (int i = 1; i < EpicenterGrid.Count + 1; i++)
            {
                for (int j = 0; j < EpicenterGrid[i].Count; j++)
                {
                    if (i == 1)
                        g.gr.FillRectangle(new SolidBrush(Color.FromArgb(80, 255, 0, 0)), new Rectangle(EpicenterGrid[i][j].x * zoom, EpicenterGrid[i][j].y * zoom, GridPart.width * zoom, GridPart.height * zoom));
                    if (i == 2)
                        g.gr.FillRectangle(new SolidBrush(Color.FromArgb(80, 255, 128, 0)), new Rectangle(EpicenterGrid[i][j].x * zoom, EpicenterGrid[i][j].y * zoom, GridPart.width * zoom, GridPart.height * zoom));
                    if (i == 3)
                        g.gr.FillRectangle(new SolidBrush(Color.FromArgb(80, 255, 255, 0)), new Rectangle(EpicenterGrid[i][j].x * zoom, EpicenterGrid[i][j].y * zoom, GridPart.width * zoom, GridPart.height * zoom));
                }
            }
        }
    }
    public class StringValueAttribute : Attribute
    {
        public string Value { get; private set; }

        public StringValueAttribute(string value)
        {
            Value = value;
        }
    }

    public enum Status
    {
        [XmlEnum(Name = "1")]
        GREEN = 1,
        [XmlEnum(Name = "2")]
        YELLOW = 2,
        [XmlEnum(Name = "3")]
        RED = 3
    }
    public class TraficLight
    {
        [XmlIgnore]
        public Timer TimerLight { get; private set; }
        public Status status { get; set; }
        public int x, y;
        public int tick, greenTime, redTime;
        public int yellowTime = 2;
        public int gridNum;
        public int bal; // остаток времени на светофоре

        public TraficLight() { }

        public TraficLight(int x, int y, int gridNum, int greenTime, int redTime)
        {
            this.x = x;
            this.y = y;
            this.gridNum = gridNum;
            this.greenTime = greenTime;
            this.redTime = redTime;
        }

        private void TimerLightProcessor(object sender, EventArgs e)
        {
            SwapLights();
            tick += 1;
            if (bal > 0)
                bal -= 1;
            if (tick == greenTime + redTime + yellowTime + yellowTime)
            {
                tick = 0;
            }
        }
        public void Stop()
        {
            if (TimerLight != null)
            {
                TimerLight.Stop();
            }

        }
        public void Start()
        {
            TimerLight.Start();
        }
        public void Set()
        {
            TimerLight = new Timer
            {
                Interval = 1000
            };
            TimerLight.Tick += new EventHandler(TimerLightProcessor);
            TimerLight.Start();
        }

        private void SwapLights()
        {
            if (tick == 0)
            {
                bal = greenTime;
                Main.G.drawGreenVertex(x, y);
                status = Status.GREEN;
            }
            else if (tick == greenTime)
            {
                bal = yellowTime;
                Main.G.drawYellowVertex(x, y);
                status = Status.YELLOW;
            }
            else if (tick == greenTime + yellowTime)
            {
                bal = redTime;
                Main.G.drawSelectedVertex(x, y);
                status = Status.RED;
            }
            else if (tick == greenTime + yellowTime + redTime)
            {
                bal = yellowTime;
                Main.G.drawYellowVertex(x, y);
                status = Status.YELLOW;
            }
        }
    }
    public class GridPart
    {
        public int x, y;
        public int status { get; set; }
        public bool check { get; set; } = false;
        public bool IsMovedAway { get; set; } = false;
        public static int width { get; set; } = 1;
        public static int height { get; set; } = 1;
        public GridPart(int x, int y)
        {
            this.x = x;
            this.y = y;
            status = 1000;
        }
        public GridPart()
        {

        }
        public void DrawPart(DrawGraph G, int zoom)
        {
            G.gr.DrawRectangle(new Pen(Color.Black, 1), x * zoom, y * zoom, width * zoom, height * zoom);
        }
        public void FillGreen(DrawGraph G, int zoom)
        {
            G.gr.FillRectangle(new SolidBrush(Color.FromArgb(20, 0, 128, 0)), new Rectangle(x * zoom, y * zoom, width * zoom, height * zoom));
        }
        public void DrawPartInRed(DrawGraph G)
        {
            G.gr.DrawRectangle(new Pen(Color.Red, 1), (x + 5) * Main.zoom, (y + 5) * Main.zoom, (width - 10) * Main.zoom, (height - 10) * Main.zoom);
        }
    }
}