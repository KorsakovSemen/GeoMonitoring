using MetroFramework;
using Newtonsoft.Json;
using SystAnalys_lr1.Strings;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SystAnalys_lr1
{
    public class Vertex //: ICloneable
    {
        public int x { get; set; }
        public int y { get; set; }
        public int gridNum;

        public Vertex()
        {
        }

        public Vertex(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Vertex objAsPart = obj as Vertex;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }

        public bool Equals(Vertex other)
        {
            if (other == null) return false;
            return (this.x.Equals(other.x) && this.y.Equals(other.y));
        }

        public override int GetHashCode()
        {
            var hashCode = -1577951254;
            hashCode = hashCode * -1521134295 + x.GetHashCode();
            hashCode = hashCode * -1521134295 + y.GetHashCode();
            hashCode = hashCode * -1521134295 + gridNum.GetHashCode();
            return hashCode;
        }

        //public object Clone()
        //{
        //    return this.MemberwiseClone();
        //}
    }

    public class Edge
    {
        public int v1 { get; set; }
        public int v2 { get; set; }

        public Edge()
        {
        }

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
        readonly private Pen redPen;
        readonly private Pen darkGoldPen;
        public Graphics gr;
        readonly private Font fo;
        readonly private Brush br;
        readonly private Color color;
        public int R = 3; //радиус окружности вершины

        public DrawGraph()
        {

            blackPen = new Pen(Color.Black)
            {
                Width = 1
            };
            redPen = new Pen(Color.Red)
            {
                Width = 1
            };
            darkGoldPen = new Pen(Color.DeepPink)
            {
                Width = 1
            };

            Random random = new Random();
            color = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
            fo = new Font("Arial", 15);
            br = Brushes.Black;
        }

        public void setBitmap()
        {  
            //эксепшн при загрузке левой директоории
            bitmap = new Bitmap(Main.globalMap);
            gr = Graphics.FromImage(bitmap);
        }
        // для второй формы быстрофикс
        public void setBitmap2(Image a)
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
            Graphics.FromImage(bitmap).Clear(Color.Wheat);
            bitmap = new Bitmap(Main.globalMap);
            gr = Graphics.FromImage(bitmap);

        }
        //для второй формы
        public void clearSheet2()
        {

            /*Graphics.FromImage(bitmap).Clear(Color.Wheat); *//// ТУТ ЭКСЕПШН НА МОДЕЛИНГЕ   
            bitmap = new Bitmap(DisplayEpicenters.EsheetPicture);
            gr = null;
            gr = (Graphics.FromImage(bitmap));

        }
        //
        public void drawVertex(int x, int y, string number)
        {
            gr.FillEllipse(Brushes.GreenYellow, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
            gr.DrawEllipse(blackPen, (x - R) * Main.zoom, (y - R) * Main.zoom, R * 2 * Main.zoom, R * 2 * Main.zoom);
            //     gr.DrawEllipse(blackPen, (x - R) * Main.zoom, (y - R) * Main.zoom, R * Main.zoom * R * Main.zoom * R * Main.zoom, R * Main.zoom * R * Main.zoom * R * Main.zoom);
            //   point = new PointF(x - 9, y - 9);
            // gr.DrawString(number, fo, br, point);
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
            //  gr.DrawEllipse(blackPen, (x - R) * Main.zoom, (y - R) * Main.zoom,  R * Main.zoom * R * Main.zoom, R * Main.zoom * R * Main.zoom);

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
                gr.DrawArc(pen, (V1.x - 2 * R) * Main.zoom, (V1.y - 2 * R) * Main.zoom, 2 * R, 2 * R, 90, 270);
                //  gr.DrawArc(pen, (V1.x + R), (V1.y + R), R, R, 90, 270);

                //point = new PointF(V1.x - (int)(2.75 * R), V1.y - (int)(2.75 * R));
                //gr.DrawString(((char)('a' + numberE)).ToString(), fo, br, point);
                //drawVertex(V1.x, V1.y, (E.v1 + 1).ToString());
            }
            else
            {
                gr.DrawLine(darkGoldPen, V1.x * Main.zoom, V1.y * Main.zoom, V2.x * Main.zoom, V2.y * Main.zoom);
                //gr.DrawLine(pen, V1.x + R, V1.y + R, V2.x + R, V2.y + R);
                //point = new PointF((V1.x + V2.x) / 2, (V1.y + V2.y) / 2);
                //gr.DrawString(((char)('a' + numberE)).ToString(), fo, br, point);
                drawVertex(V1.x * Main.zoom, V1.y * Main.zoom, (E.v1 + 1).ToString());
                drawVertex(V2.x * Main.zoom, V2.y * Main.zoom, (E.v2 + 1).ToString());
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
                    gr.DrawArc(pen, V[E[i].v1].x - 2 * R * Main.zoom, V[E[i].v1].y - 2 * R * Main.zoom, 2 * R * Main.zoom, 2 * R * Main.zoom, 90, 270);
                    //point = new PointF(V[E[i].v1].x - (int)(2.75 * R), V[E[i].v1].y - (int)(2.75 * R));
                    //gr.DrawString(((char)('a' + i)).ToString(), fo, br, point);
                }
                else
                {
                    //проблема с отрисовкой
                    if (E[i].v1 < V.Count && E[i].v2 < V.Count)
                    {
                        gr.DrawLine(pen, V[E[i].v1].x * Main.zoom, V[E[i].v1].y * Main.zoom, V[E[i].v2].x * Main.zoom, V[E[i].v2].y * Main.zoom);
                        //gr.DrawLine(pen, V[E[i].v1].x + R, V[E[i].v1].y + R, V[E[i].v2].x + R, V[E[i].v2].y + R);
                    }
                    //point = new PointF((V[E[i].v1].x + V[E[i].v2].x) / 2, (V[E[i].v1].y + V[E[i].v2].y) / 2);
                    //gr.DrawString(((char)('a' + i)).ToString(), fo, br, point);
                }
            }
            //рисуем вершины
            for (int i = 0; i < V.Count; i++)
            {
                if (rand != 0)
                    drawRouteVertex(V[i].x, V[i].y);
                else
                    drawVertex(V[i].x, V[i].y, (i + 1).ToString());
            }

            foreach (var stopPoints in Main.allstopPoints)
            {
                drawStopVertex(stopPoints.x, stopPoints.y);
            }
            if (Main.selectedRoute != null)
            {
                if (Main.stopPoints.ContainsKey(Main.selectedRoute))
                {
                    foreach (var stopPoints in Main.stopPoints[Main.selectedRoute])
                    {
                        drawStopRouteVertex(stopPoints.x, stopPoints.y);
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
        //public void drawRouteStopPoints(int route)
        //{
        //    foreach (var stopPoints in Main.stopPoints)
        //    {
        //        foreach(var s)
        //        //рисуем остановки
        //        if (rand != 0)
        //            drawStopRouteVertex(stopPoints.x, stopPoints.y);
        //        else
        //            drawStopVertex(stopPoints.x, stopPoints.y);

        //    }
        //}


        ////заполняет матрицу смежности
        public void fillAdjacencyMatrix(int numberV, List<Edge> E, int[,] matrix)
        {
            for (int i = 0; i < numberV; i++)
                for (int j = 0; j < numberV; j++)
                    matrix[i, j] = 0;
            for (int i = 0; i < E.Count; i++)
            {
                matrix[E[i].v1, E[i].v2] = 1;
                matrix[E[i].v2, E[i].v1] = 1;
            }
        }

        ////заполняет матрицу инцидентности
        //public void fillIncidenceMatrix(int numberV, List<Edge> E, int[,] matrix)
        //{
        //    for (int i = 0; i < numberV; i++)
        //        for (int j = 0; j < E.Count; j++)
        //            matrix[i, j] = 0;
        //    for (int i = 0; i < E.Count; i++)
        //    {
        //        matrix[E[i].v1, i] = 1;
        //        matrix[E[i].v2, i] = 1;
        //    }
        //}


    }


    //public static class EnumerableExtensions
    //{       
    //    public static Task ForEachAsync<T>(this IEnumerable<T> source, int degreeOfParallelism, Func<T, Task> body, IProgress<T> progress = null)
    //    {
    //        return Task.WhenAll(
    //            Partitioner.Create(source).GetPartitions(degreeOfParallelism)
    //                .Select(partition => Task.Run(async () => {
    //                    using (partition)
    //                        while (partition.MoveNext())
    //                        {
    //                            await body(partition.Current);
    //                            progress?.Report(partition.Current);
    //                        }
    //                }))
    //        );
    //    }
    //}

    public class Bus : ICloneable
    {
        public static int ScrollX;
        public static int ScrollY;
        public int skip = 1;
        public static string sImg = "../../Resources/bus.PNG";
        //таймер для движения
        Timer MovingTimer;
        //таймер определения эпицентров
        Timer DetectTimer;
        //массив эпицентров
        //private static List<Epicenter> Epicenters;


        /////////////////////////////////////////////////////////////////////

        public List<Epicenter> Epicenters2 { get; set; } = new List<Epicenter>();

        //////////////////////////////////////////////////////////////////////

        //сетка
        private static List<GridPart> Rectangles;
        //карта
        private static PictureBox Map;
        //маршрут движения целиком
        readonly private List<Vertex> RoutMap;
        //для создания координат
        public double angle, x, y;
        //позиция автобуса
        public int PositionAt;
        //для обратного движения по маршруту
        public bool TurnBack;
        [XmlIgnore, JsonIgnore]
        public PictureBox busPic;// { get; set; }
        //номер маршрута, по которому будет ездить автобус
        public string route;
        private double _date;
        public SerializableDictionary<int, int> grids;
        //текущий квадрат, в котором находится автобус
        public int? Locate = null;
        //все координаты для движения автобуса
        static public SerializableDictionary<string, List<Point>> AllCoordinates;
        //для того, чтобы 1 раз прибавлять к OneGridFilling
        public int? lastLocate;
        public int R = 7;
        //сколько автобусу нужно проехать в тиках
        public int TickCount_ { get; set; }
        //за сколько времени автобус нашел эпицентр
        public static int FoundTime { get; set; }
        //проверка нашел ли автобус эпицентр
        public bool EpicFounded { get; set; }
        static public int? ZoomCoef { get; set; } = 1;
        static public int small { get; set; } = 1000;
        public int passivniyTick;
        //не позволит басу двигаться на светофорах когда он должен стоять
        public static bool InstaStop { get; set; } = false;
        public int oldSize = 0;
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
            if (ZoomCoef < oldZoom)
            {
                if (ZoomCoef == 1)
                    busPic.Size = new Size(Main.sizeBus, Main.sizeBus);
                else
                    busPic.Size = new Size(oldSize - ((int)ZoomCoef), oldSize - ((int)ZoomCoef));
            }
            else
            {
                if (ZoomCoef == 1)
                    busPic.Size = new Size(Main.sizeBus, Main.sizeBus);
                else
                    busPic.Size = new Size(oldSize + ((int)ZoomCoef), oldSize + ((int)ZoomCoef));

            }
            oldZoom = (int)ZoomCoef;
        }

        public static SerializableDictionary<string, List<Point>> getAllCoordinates()
        {
            return AllCoordinates;
        }


        public static void setAllCoordinates(SerializableDictionary<string, List<Point>> A)
        {
            AllCoordinates = A;
        }

        public int? getLocate()
        {
            return Locate;
        }

        public static void setMap(PictureBox M)
        {
            Map = M;
        }

        public static void setGrid(List<GridPart> G)
        {
            Rectangles = G;
        }

        public List<GridPart> getGrids()
        {
            return Rectangles;
        }


        public double Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public Bus()
        { }
        public bool tracker { get; set; }


        public Bus(List<Vertex> m, PictureBox busPic, int PositionAt, bool Turn, string route, bool not)
        {
            tracker = not;
            oldSize = busPic.Size.Height;
            RoutMap = new List<Vertex>();
            this.RoutMap = m;
            this.grids = new SerializableDictionary<int, int>();
            for (int i = 0; i < Rectangles.Count; i++)
            {
                this.grids.Add(i, 0);
            }
            this.busPic = busPic;
            x = busPic.Left;
            y = busPic.Top;
            this.PositionAt = PositionAt;
            TurnBack = Turn;
            this.route = route;
            ScrollX = 0;
            ScrollY = 0;

        }
        //движение без графики (для моделирования)
        public void ClearAroundEpic()
        {
            foreach (var Epic in Epicenters2)
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
                    if (PositionAt < AllCoordinates[route].IndexOf(AllCoordinates[route].Last()))
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

                        //busPic.Left = AllCoordinates[route][PositionAt].X;
                        //busPic.Top = AllCoordinates[route][PositionAt].Y;
                        PositionAt++;

                    }
                    else
                    {
                        //MovingTimer.Stop();
                        //await Task.Delay(rnd.Next(1000, 10000));
                        //MovingTimer.Start();
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
            await Task.Run(() => MoveWithGraphics());
        }
        Random rnd = new Random();
        public bool skipTraffics = false;
        public int skipStops = 1;
        public int skipEnd = 5;
        //движение с графикой (для визуализации движения)
        delegate void Dpoint(Point pos);
        public void AlignBus()
        {
            //busPic.Location = new Point((AllCoordinates[route][PositionAt].X * (int)ZoomCoef) + ScrollX - busPic.Width / 2, (AllCoordinates[route][PositionAt].Y * (int)ZoomCoef) + ScrollY - busPic.Height / 2);
            //label5.Invoke(new Del((s) => label5.Text = s), "Время, за которое обнаружили загрязнение:" + (small).ToString());
            //label5.Invoke(new Del((s) => label5.Text = s), "Время, за которое обнаружили загрязнение:" + (small).ToString());

            if (PositionAt < AllCoordinates[route].Count)
            {
                busPic.Invoke(new Dpoint((pos) => busPic.Location = pos), new Point((AllCoordinates[route][PositionAt].X * (int)ZoomCoef) + ScrollX - busPic.Width / 2, (AllCoordinates[route][PositionAt].Y * (int)ZoomCoef) + ScrollY - busPic.Height / 2));
            }
            else
            {
                busPic.Invoke(new Dpoint((pos) => busPic.Location = pos), new Point((AllCoordinates[route][PositionAt-1].X * (int)ZoomCoef) + ScrollX - busPic.Width / 2, (AllCoordinates[route][PositionAt-1].Y * (int)ZoomCoef) + ScrollY - busPic.Height / 2));
            }

        }
        public Timer GetMovingTimer()
        {
            return MovingTimer;
        }
        public async void MoveWithGraphics()
        {
            try
            {
                //if (tracker == true)
                //{
                if (InstaStop == false)
                {
                    if (AllCoordinates.ContainsKey(route))
                    {
                        if (TurnBack == false)
                        {
                            if (PositionAt < AllCoordinates[route].Count)
                            {
                                if (Main.stopPoints.Count != 0 && Main.stopPoints.ContainsKey(route))
                                {
                                    if (skipStops == 0)
                                    {
                                        foreach (var sp in Main.stopPoints[route])
                                        {
                                            if (Math.Pow((double.Parse((sp.x * (int)ZoomCoef - AllCoordinates[route][PositionAt].X * (int)ZoomCoef).ToString())), 2) + Math.Pow((double.Parse(((sp.y * (int)ZoomCoef - AllCoordinates[route][PositionAt].Y * (int)ZoomCoef)).ToString())), 2) <= Main.G.R * (int)ZoomCoef * (Main.G.R * (int)ZoomCoef))
                                            {
                                                MovingTimer.Stop();
                                                await Task.Delay(rnd.Next(0, 10000));
                                                skipStops = 50;
                                                if (InstaStop == false)
                                                {
                                                    MovingTimer.Start();
                                                }
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
                                            if ((Math.Pow((double.Parse((sp.x * (int)ZoomCoef - AllCoordinates[route][PositionAt].X * (int)ZoomCoef).ToString())), 2) + Math.Pow((double.Parse(((sp.y * (int)ZoomCoef - AllCoordinates[route][PositionAt].Y * (int)ZoomCoef)).ToString())), 2) <= Main.G.R * (int)ZoomCoef * Main.G.R * (int)ZoomCoef * (Main.G.R * (int)ZoomCoef)) && sp.status != Status.RED)
                                            {
                                                skip = 100;
                                                break;
                                            }
                                            else
                                            if ((Math.Pow((double.Parse((sp.x * (int)ZoomCoef - AllCoordinates[route][PositionAt].X * (int)ZoomCoef).ToString())), 2) + Math.Pow((double.Parse(((sp.y * (int)ZoomCoef - AllCoordinates[route][PositionAt].Y * (int)ZoomCoef)).ToString())), 2) <= Main.G.R * (int)ZoomCoef * Main.G.R * (int)ZoomCoef * (Main.G.R * (int)ZoomCoef)) && sp.status == Status.RED)
                                            {
                                                skip = 100;
                                                MovingTimer.Stop();
                                                // int bal = sp.bal / 2;
                                                await Task.Delay(sp.bal / 2 * 1000);
                                             //   Console.WriteLine("After:");
                                             //   Console.WriteLine(sp.bal);
                                                if (InstaStop == false)
                                                {
                                                    MovingTimer.Start();
                                                }
                                                break;

                                            }
                                        }
                                    }
                                }
                               // busPic.Location = new Point((AllCoordinates[route][PositionAt].X * (int)ZoomCoef) + ScrollX - busPic.Width / 2, (AllCoordinates[route][PositionAt].Y * (int)ZoomCoef) + ScrollY - busPic.Height / 2);
                                busPic.Invoke(new Dpoint((pos) => busPic.Location = pos), new Point((AllCoordinates[route][PositionAt].X * (int)ZoomCoef) + ScrollX - busPic.Width / 2, (AllCoordinates[route][PositionAt].Y * (int)ZoomCoef) + ScrollY - busPic.Height / 2));
                                PositionAt++;
                            }
                            else
                            {
                                if (skipEnd == 0)
                                {
                                    MovingTimer.Stop();
                                    await Task.Delay(rnd.Next(0, 10000));                                    
                                }
                                if (InstaStop == false)
                                {
                                    MovingTimer.Start();
                                }
                                TurnBack = true;
                                PositionAt--;
                            }
                        }
                        else
                        {
                            if (PositionAt > 0)
                            {
                                if (PositionAt < AllCoordinates[route].Count)
                                {
                                    if (Main.stopPoints.Count != 0 && Main.stopPoints.ContainsKey(route))
                                    {
                                        if (skipStops == 0)
                                        {
                                            foreach (var sp in Main.stopPoints[route])
                                            {
                                                if (Math.Pow((double.Parse((sp.x * (int)ZoomCoef - AllCoordinates[route][PositionAt].X * (int)ZoomCoef).ToString())), 2) + Math.Pow((double.Parse(((sp.y * (int)ZoomCoef - AllCoordinates[route][PositionAt].Y * (int)ZoomCoef)).ToString())), 2) <= Main.G.R * (int)ZoomCoef * Main.G.R * (int)ZoomCoef * (Main.G.R * (int)ZoomCoef))
                                                {
                                                    MovingTimer.Stop();
                                                    await Task.Delay(rnd.Next(0, 10000));
                                                    skipStops = 50;
                                                    if (InstaStop == false)
                                                    {
                                                        MovingTimer.Start();
                                                    }
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
                                                if ((Math.Pow((double.Parse((sp.x * (int)ZoomCoef - AllCoordinates[route][PositionAt].X * (int)ZoomCoef).ToString())), 2) + Math.Pow((double.Parse(((sp.y * (int)ZoomCoef - AllCoordinates[route][PositionAt].Y * (int)ZoomCoef)).ToString())), 2) <= Main.G.R * (int)ZoomCoef * Main.G.R * (int)ZoomCoef * Main.G.R * (int)ZoomCoef) && sp.status != Status.RED)
                                                {
                                                    skip = 100;
                                                    break;
                                                }
                                                //else
                                                if ((Math.Pow((double.Parse((sp.x * (int)ZoomCoef - AllCoordinates[route][PositionAt].X * (int)ZoomCoef).ToString())), 2) + Math.Pow((double.Parse(((sp.y * (int)ZoomCoef - AllCoordinates[route][PositionAt].Y * (int)ZoomCoef)).ToString())), 2) <= Main.G.R * (int)ZoomCoef * Main.G.R * (int)ZoomCoef * Main.G.R * (int)ZoomCoef) && sp.status == Status.RED)
                                                {
                                                    skip = 100;
                                                    MovingTimer.Stop();
                                           
                                                    // int bal = sp.bal / 2;
                                                    await Task.Delay(sp.bal / 2 * 1000);
                                               //     Console.WriteLine("After:");
                                               //     Console.WriteLine(sp.bal);
                                                    if (InstaStop == false)
                                                    {
                                                        MovingTimer.Start();
                                                    }
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    //
                                    //  busPic.Location = new Point((AllCoordinates[route][PositionAt].X * (int)ZoomCoef) + ScrollX - busPic.Width / 2, (AllCoordinates[route][PositionAt].Y * (int)ZoomCoef) + ScrollY - busPic.Height / 2);
                                    busPic.Invoke(new Dpoint((pos) => busPic.Location = pos), new Point((AllCoordinates[route][PositionAt].X * (int)ZoomCoef) + ScrollX - busPic.Width / 2, (AllCoordinates[route][PositionAt].Y * (int)ZoomCoef) + ScrollY - busPic.Height / 2));
                                    PositionAt--;
                                }
                            }
                            else
                            {
                                if(skipEnd == 0)
                                {
                                    MovingTimer.Stop();
                                    await Task.Delay(rnd.Next(0, 10000));
                                }
                                if (InstaStop == false)
                                {
                                    MovingTimer.Start();
                                }
                                TurnBack = false;
                                PositionAt++;
                            }

                        }
                    }
                    else
                    {
                        MovingTimer.Stop();
                    };
                    // }
                }
            }
            catch { }
        }
        public string getRoute()
        {
            return route;
        }

        double GetAngle(double x2, double y2)
        {
            return Math.Atan2((x - x2), (y - y2));
        }

        public double GetDistance(double x1, double y1, double x2, double y2)
        {
            return (int)Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }
        //визуализация обмнаружения эпицентра 
        private void DetectEpicenter()
        {
            if (AllCoordinates.Any())
            {
                foreach (var EpicList in Epicenters2)
                {
                    foreach (var Sector in EpicList.GetEpicenterGrid())
                    {
                        foreach (var Square in Sector.Value)
                            if ((PositionAt < AllCoordinates[route].Count))
                                if (((AllCoordinates[route][PositionAt].X * ZoomCoef) > Square.x * ZoomCoef) && ((AllCoordinates[route][PositionAt].X * ZoomCoef) < Square.x * ZoomCoef + GridPart.width * ZoomCoef) && ((AllCoordinates[route][PositionAt].Y * ZoomCoef) > Square.y * ZoomCoef) && ((AllCoordinates[route][PositionAt].Y * ZoomCoef) < (Square.y * ZoomCoef + GridPart.height * ZoomCoef)))
                                {
                                    switch (Sector.Key)
                                    {
                                        case 1:
                                            Map.CreateGraphics().FillRectangle((Brush)Brushes.Red, (AllCoordinates[route][PositionAt].X * (int)ZoomCoef) + busPic.Width, AllCoordinates[route][PositionAt].Y * (int)ZoomCoef + busPic.Height, 3, 3);
                                            break;

                                        case 2:
                                            Map.CreateGraphics().FillRectangle((Brush)Brushes.DarkOrange, AllCoordinates[route][PositionAt].X * (int)ZoomCoef + busPic.Width, AllCoordinates[route][PositionAt].Y * (int)ZoomCoef + busPic.Height, 3, 3);
                                            break;

                                        case 3:
                                            Map.CreateGraphics().FillRectangle((Brush)Brushes.Yellow, AllCoordinates[route][PositionAt].X * (int)ZoomCoef + busPic.Width, AllCoordinates[route][PositionAt].Y * (int)ZoomCoef + busPic.Height, 3, 3);
                                            break;

                                        default:
                                            break;

                                    }
                                }
                    }
                }
            }
        }
        public async Task asDetectEpicenter2()
        {
            await Task.Run(() => DetectEpicenter2());
        }
        // обмнаружение  эпицентров через точки
        public int DetectEpicenter2()
        {
            //if (tracker == true)
            //{
            foreach (var EpicList in Epicenters2)
            {
                foreach (var Sector in EpicList.GetEpicenterGrid())
                {
                    foreach (var Square in Sector.Value)
                        if ((PositionAt < AllCoordinates[route].Count))
                            if (((AllCoordinates[route][PositionAt].X * ZoomCoef) >= Square.x * ZoomCoef) && ((AllCoordinates[route][PositionAt].X * ZoomCoef) <= Square.x * ZoomCoef + GridPart.width * ZoomCoef) && ((AllCoordinates[route][PositionAt].Y * ZoomCoef) >= Square.y * ZoomCoef) && ((AllCoordinates[route][PositionAt].Y * ZoomCoef) <= (Square.y * ZoomCoef + GridPart.height * ZoomCoef)))
                            {
                                //if (Square.check != true)
                                //{
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
                                        //Square.check = true;
                                        return 2;

                                    case 3:
                                        //Square.check = true;
                                        return 1;

                                    default:
                                        return 0;

                                }
                                //}
                            }
                }
            }

            //}

            return 0;
        }
        public int DetectEpicenterByGrid()
        {
            //if (tracker == true)
            //{
            foreach (var EpicList in Epicenters2)
            {
                foreach (var Sector in EpicList.GetEpicenterGrid())
                {
                    foreach (var Square in Sector.Value)
                    {

                        if (((Rectangles[Main.AllGridsInRoutes[route][(int)PositionAt]].x == Square.x) && (Rectangles[Main.AllGridsInRoutes[route][(int)PositionAt]].y == Square.y)))
                        {
                            //if (Square.check != true)
                            //{
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
                                    //Square.check = true;
                                    return 2;

                                case 3:
                                    //Square.check = true;
                                    return 1;

                                default:
                                    return 0;

                            }
                            //}
                        }
                    }
                }
            }

            //}

            return 0;
        }
        public async Task asyncDetectRectangle2()
        {
            await Task.Run(() => DetectRectangle2());
        }
        //определение квадрата по новому
        public void DetectRectangle2()
        {

            for (int i = 0; i < Rectangles.Count; i++)
            {
                if ((PositionAt < AllCoordinates[route].Count))

                    if (((AllCoordinates[route][PositionAt].X) > Rectangles[i].x) && ((AllCoordinates[route][PositionAt].X) < Rectangles[i].x + GridPart.width) && ((AllCoordinates[route][PositionAt].Y) > Rectangles[i].y) && ((AllCoordinates[route][PositionAt].Y) < (Rectangles[i].y + GridPart.height)))
                    {
                        //if (this.grids[i] == 0)
                        //{
                        //    this.grids[i] = 1;
                        //}
                        Locate = i;
                    }


            }

        }
        //определение квадрата по новому
        public void DetectRectangleByGrids()
        {

            //for (int i = 0; i < Rectangles.Count; i++)
            //{
            //    if ((PositionAt < AllCoordinates[route].Count))

            //        if (((AllCoordinates[route][PositionAt].X) > Rectangles[i].x) && ((AllCoordinates[route][PositionAt].X) < Rectangles[i].x + GridPart.width) && ((AllCoordinates[route][PositionAt].Y) > Rectangles[i].y) && ((AllCoordinates[route][PositionAt].Y) < (Rectangles[i].y + GridPart.height)))
            //        {
            //            //if (this.grids[i] == 0)
            //            //{
            //            //    this.grids[i] = 1;
            //            //}
            //            Locate = i;
            //        }


            //}


        }
        //определение квадрата по старому
        public void DetectRectangle()
        {
            for (int i = 0; i < Rectangles.Count; i++)
            {
                if (((busPic.Left + busPic.Width / 2) > Rectangles[i].x) && ((busPic.Left + busPic.Width / 2) < Rectangles[i].x + GridPart.width) && ((busPic.Top + busPic.Height / 2) > Rectangles[i].y) && ((busPic.Top + busPic.Height / 2) < (Rectangles[i].y + GridPart.height)))
                {
                    //if (this.grids[i] == 0)
                    //{
                    //    this.grids[i] = 1;
                    //}
                    Locate = i;
                }
            }
        }
        //(double) busPic.Top + busPic.Height / 2, (double) Rectangles[i].x + Rectangles[i].width / 2, (double) Rectangles[i].y + Rectangles[i].height / 2) < Rectangles[i].width / 2)
        private void TimerDetectProcessor(object sender, EventArgs e)
        {
            if (tracker == true)
            {
                DetectEpicenter();
            };
            if (grids != null)
            {
                DetectRectangle();
            }
        }
        private void TimerMoveProcessor(object sender, EventArgs e)
        {
            //   System.Threading.Thread.Sleep(2000);
            MoveWithGraphics();
            //        MovingTimer.Interval = 1;//rnd.Next(1, 100);
            if (skip != 0)
            {
                skip -= 1;
            }
            if (skipStops != 0)
            {
                skipStops -= 1;
            }
            if (skipEnd != 0)
                skipEnd -= 1;
        }
        public void Set()
        {

            InstaStop = false;
            MovingTimer = new Timer
            {
                Interval = 1
            };
            DetectTimer = new Timer
            {
                Interval = 1
            };
            MovingTimer.Tick += new EventHandler(TimerMoveProcessor);
            //DetectTimer.Tick += new EventHandler(TimerDetectProcessor);
            MovingTimer.Start();
            //DetectTimer.Start();
         
        }
        public void Start()
        {
            if (MovingTimer != null)
            {
                MovingTimer.Start();
                //DetectTimer.Stop();
                InstaStop = false;
            }

        }
        public void Stop()
        {
            if (MovingTimer != null)
            {
                MovingTimer.Stop();
                //DetectTimer.Stop();
                InstaStop = true;
            }

        }
        //старое движение(сейчас используется для создания координат для всех маршрутов 
        public void MoveForCoordinates()
        {
            if (RoutMap.Count >= 2)
            {
                if (TurnBack == false)
                {
                    if ((TurnBack == false) && (Math.Abs((Math.Abs(x) + Math.Abs(y)) - (Math.Abs((RoutMap[PositionAt].x) + Math.Abs(RoutMap[PositionAt].y))))) > 1)
                    {

                        x -= Math.Sin(angle);
                        y -= Math.Cos(angle);

                        busPic.Left = (int)x;
                        busPic.Top = (int)y;

                    }

                    else
                    {
                        if (PositionAt >= RoutMap.Count - 1)
                        {
                            TurnBack = true;
                            PositionAt -= 1;
                            angle = GetAngle(RoutMap[PositionAt].x, RoutMap[PositionAt].y);


                        }
                        else
                        {
                            PositionAt++;
                            angle = GetAngle(RoutMap[PositionAt].x, RoutMap[PositionAt].y);
                        }
                    }
                }
                if (TurnBack == true)
                {

                    if ((Math.Abs((Math.Abs(x) + Math.Abs(y)) - (Math.Abs(RoutMap[PositionAt].x + Math.Abs(RoutMap[PositionAt].y))))) > 1)
                    {

                        x -= Math.Sin(angle);
                        y -= Math.Cos(angle);


                        busPic.Left = (int)x;
                        busPic.Top = (int)y;
                    }
                    else
                    {
                        if (PositionAt == 0)
                        {
                            TurnBack = false;
                        }
                        else
                        {
                            PositionAt -= 1;
                            angle = GetAngle(RoutMap[PositionAt].x, RoutMap[PositionAt].y);
                        }

                    }

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
        {


        }
        public Dictionary<int, List<GridPart>> GetEpicenterGrid()
        {
            return this.EpicenterGrid;
        }
        public object Clone()
        {
            return this.MemberwiseClone();
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
                            //case 1:
                            //    foreach (var part in EpicenterGrid[3])
                            //    {
                            //        if ((part.x == PollutedSqure.x) && (part.y == PollutedSqure.y))
                            //        {
                            //            net = true;
                            //            break;
                            //        }
                            //    }
                            //    if (net == false)
                            //    {
                            //        EpicenterGrid[3].Add(new GridPart(PollutedSqure.x, PollutedSqure.y));
                            //    }
                            //    break;
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
                                        //}
                                    }
                                }
                            }
                            //BufferList.Add(new GridPart(start.x, start.y));

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
                            //BufferList.Add(new GridPart(start.x, start.y));
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
                            //BufferList.Add(new GridPart(start.x, start.y));
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
                            //BufferList.Add(new GridPart(start.x, start.y));
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
                            //BufferList.Add(new GridPart(start.x, start.y));
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
                            //BufferList.Add(new GridPart(start.x, start.y));
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
                            //BufferList.Add(new GridPart(start.x, start.y));
                        }
                        while (ScanGrid.x < RedGrid.x)
                        {
                            ScanGrid.x += GridPart.width;
                            foreach (var SearchGrid in EpicenterGrid[1])
                            {
                                if ((ScanGrid.x == SearchGrid.x) && (ScanGrid.y == SearchGrid.y))
                                {
                                    List<string> Parameters = new List<string>();
                                    Parameters = Strashno(ScanGrid, Parameters);
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
                            //BufferList.Add(new GridPart(start.x, start.y));
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
            ////////
            for (int i = 2; i < 4; i++)
            {

                List<GridPart> tesdsfdst = new List<GridPart>();
                foreach (var item in EpicenterGrid[i - 1])
                {
                    tesdsfdst.Add(new GridPart(item.x, item.y));
                }
                foreach (var itwms in tesdsfdst)
                {
                    List<string> Parameter = new List<string>();
                    Parameter = Strashno(itwms, Parameter);
                    if (Parameter.Count > 0)
                        foreach (var items in Parameter)
                        {
                            Creater(items, itwms, i);
                        }
                }
                tesdsfdst = new List<GridPart>();
                foreach (var item in EpicenterGrid[i])
                {
                    tesdsfdst.Add(new GridPart(item.x, item.y));
                }
                foreach (var itwms in tesdsfdst)
                {
                    List<string> Parameter = new List<string>();
                    Parameter = Strashno(itwms, Parameter);
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
                    //foreach(var BufGrid in OrangeBuffer)
                    //{

                    //    foreach (var EpGridList in EpicenterGrid)
                    //    {
                    //        bool net = false;
                    //        int? Red = null;
                    //        foreach (var EpGrid in EpGridList.Value)
                    //        {

                    //            if ((BufGrid.x == EpGrid.x) && (BufGrid.y == EpGrid.y))
                    //            {
                    //                //if (EpGridList.Key == 1)
                    //                //{
                    //                //    Red = EpGridList.Value.IndexOf(EpGrid);
                    //                //    net = true;
                    //                //    break;
                    //                //}

                    //                net = true;
                    //                break;


                    //            }
                    //        }
                    //        //if (Red != null)
                    //        //{
                    //        //    EpicenterGrid[1].RemoveAt((int)Red);
                    //        //    EpicenterGrid[i].Add(new GridPart(BufGrid.x, BufGrid.y));
                    //        //}
                    //        if (net == false)
                    //        {
                    //            EpicenterGrid[i].Add(new GridPart(BufGrid.x, BufGrid.y));
                    //        }


                    //    }

                    //}
                }

            }










        }
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

                Parameter = Strashno(StarterEpicPart, Parameter);
                if (Parameter.Count > 0)
                {

                    foreach (var item in Parameter)
                    {
                        if (Parameters.Contains(item))
                            Creater(item, StarterEpicPart, 1);
                    }
                }

            }

        }
        public void CreateRandomEpicenter(int SizeParam, int? StartPos)
        {
            var rand = new Random();
            EpicenterGrid = new SerializableDictionary<int, List<GridPart>>();

            EpicenterGrid.Add(1, new List<GridPart>());
            if (StartPos == null)
            {
                EpicenterGrid[1].Add(new GridPart(TheGrid[rand.Next(TheGrid.IndexOf(TheGrid.First()), TheGrid.IndexOf(TheGrid.Last()))].x, TheGrid[rand.Next(TheGrid.IndexOf(TheGrid.First()), TheGrid.IndexOf(TheGrid.Last()))].y));
            }
            else
            {
                EpicenterGrid[1].Add(new GridPart(TheGrid[(int)StartPos].x, TheGrid[(int)StartPos].y));

            }
            for (int i = 0; i < SizeParam; i++)
            {
                List<string> Parameter = new List<string>();

                GridPart StarterEpicPart = EpicenterGrid[1][rand.Next(EpicenterGrid[1].IndexOf(EpicenterGrid[1].First()), EpicenterGrid[1].IndexOf(EpicenterGrid[1].Last()))];

                Parameter = Strashno(StarterEpicPart, Parameter);
                if (Parameter.Count > 0)
                    foreach (var item in Parameter)
                    {
                        Creater(item, StarterEpicPart, 1);
                    }

            }
            for (int i = 2; i < 4; i++)
            {
                EpicenterGrid.Add(i, new List<GridPart>());
                List<GridPart> tesdsfdst = new List<GridPart>();
                foreach (var item in EpicenterGrid[i - 1])
                {
                    tesdsfdst.Add(new GridPart(item.x, item.y));
                }
                foreach (var itwms in tesdsfdst)
                {
                    List<string> Parameter = new List<string>();
                    Parameter = Strashno(itwms, Parameter);
                    if (Parameter.Count > 0)
                        foreach (var items in Parameter)
                        {
                            Creater(items, itwms, i);
                        }
                }
                tesdsfdst = new List<GridPart>();
                foreach (var item in EpicenterGrid[i])
                {
                    tesdsfdst.Add(new GridPart(item.x, item.y));
                }
                foreach (var itwms in tesdsfdst)
                {
                    List<string> Parameter = new List<string>();
                    Parameter = Strashno(itwms, Parameter);
                    if (Parameter.Count > 0)
                        foreach (var items in Parameter)
                        {
                            Creater(items, itwms, i);
                        }
                }

            }

        }
        public List<string> Strashno(GridPart EpicPart, List<string> Parameter)
        {
            if (EpicPart.x < TheGrid.Last().x)
            {
                bool net = false;
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
                bool net = false;
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
                bool net = false;
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
                bool net = false;
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
                bool net = false;
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
                bool net = false;
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
                bool net = false;
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
                bool net = false;
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
        //public void CreateRandomEpicenter2(int SizeParam, int? StartPos)
        //{
        //    var rand = new Random();
        //    EpicenterGrid2 = new List<int>();
        //    if (StartPos == null)
        //    {
        //        EpicenterGrid2.Add(TheGrid.IndexOf(TheGrid[rand.Next(TheGrid.IndexOf(TheGrid.First()), TheGrid.IndexOf(TheGrid.Last()))]));
        //    }
        //    else
        //    {
        //        EpicenterGrid2.Add(TheGrid.IndexOf(TheGrid[(int)StartPos]));

        //    }
        //    for (int i = 0; i < SizeParam; i++)
        //    {
        //        List<string> Parameter = new List<string>();

        //        int StarterEpicPart = EpicenterGrid2[rand.Next(EpicenterGrid2.IndexOf(EpicenterGrid2.First()), EpicenterGrid2.IndexOf(EpicenterGrid2.Last()))];

        //        Parameter = Strashno2(StarterEpicPart, Parameter);
        //        if (Parameter.Count > 0)
        //            foreach (var item in Parameter)
        //            {
        //                Creater2(item, StarterEpicPart, 1);
        //            }

        //    }
        //    for (int i = 2; i < 4; i++)
        //    {
        //        EpicenterGrid.Add(i, new List<GridPart>());
        //        List<GridPart> tesdsfdst = new List<GridPart>();
        //        foreach (var item in EpicenterGrid[i - 1])
        //        {
        //            tesdsfdst.Add(new GridPart(item.x, item.y));
        //        }
        //        foreach (var itwms in tesdsfdst)
        //        {
        //            List<string> Parameter = new List<string>();
        //            Parameter = Strashno(itwms, Parameter);
        //            if (Parameter.Count > 0)
        //                foreach (var items in Parameter)
        //                {
        //                    Creater(items, itwms, i);
        //                }
        //        }
        //        tesdsfdst = new List<GridPart>();
        //        foreach (var item in EpicenterGrid[i])
        //        {
        //            tesdsfdst.Add(new GridPart(item.x, item.y));
        //        }
        //        foreach (var itwms in tesdsfdst)
        //        {
        //            List<string> Parameter = new List<string>();
        //            Parameter = Strashno(itwms, Parameter);
        //            if (Parameter.Count > 0)
        //                foreach (var items in Parameter)
        //                {
        //                    Creater(items, itwms, i);
        //                }
        //        }

        //    }

        //}
        //public List<string> Strashno2(int EpicPart, List<string> Parameter)
        //{
        //    if (TheGrid[EpicPart].x < TheGrid.Last().x)
        //    {
        //        bool net = false;
        //        foreach (var part in EpicenterGrid2)
        //            if ((TheGrid[part].x == TheGrid[EpicPart].x + GridPart.width) && (TheGrid[part].y == TheGrid[EpicPart].y))
        //            {
        //                net = true;
        //                break;
        //            }

        //        if (net == false)
        //        {
        //            Parameter.Add("right");
        //        }

        //    }
        //    if ((TheGrid[EpicPart].x < TheGrid.Last().x) && (TheGrid[EpicPart].y < TheGrid.Last().y))
        //    {
        //        bool net = false;
        //        foreach (var part in EpicenterGrid2)
        //        {
        //            if ((TheGrid[part].x == TheGrid[EpicPart].x + GridPart.width) && (TheGrid[part].y == TheGrid[EpicPart].y + GridPart.height))
        //            {
        //                net = true;
        //                break;
        //            }
        //        }
        //        if (net == false)
        //        {
        //            Parameter.Add("right-down");
        //        }

        //    }
        //    if ((TheGrid[EpicPart].x < TheGrid.Last().x) && (TheGrid[EpicPart].y > TheGrid.Last().y))
        //    {
        //        bool net = false;
        //        foreach (var part in EpicenterGrid2)
        //        {
        //            if ((TheGrid[part].x == TheGrid[EpicPart].x + GridPart.width) && (TheGrid[part].y == TheGrid[EpicPart].y - GridPart.height))
        //            {
        //                net = true;
        //                break;
        //            }
        //        }
        //        if (net == false)
        //        {
        //            Parameter.Add("right-up");
        //        }

        //    }
        //    if (TheGrid[EpicPart].y < TheGrid.Last().y)
        //    {
        //        bool net = false;
        //        foreach (var part in EpicenterGrid2)
        //        {
        //            if ((TheGrid[part].x == TheGrid[EpicPart].x) && (TheGrid[part].y == TheGrid[EpicPart].y + GridPart.height))
        //            {
        //                net = true;
        //                break;
        //            }
        //        }
        //        if (net == false)
        //        {
        //            Parameter.Add("down");
        //        }
        //    }

        //    if (TheGrid[EpicPart].y > TheGrid.First().y)
        //    {
        //        bool net = false;
        //        foreach (var part in EpicenterGrid2)
        //        {
        //            if ((TheGrid[part].x == TheGrid[EpicPart].x) && (TheGrid[part].y == TheGrid[EpicPart].y - GridPart.height))
        //            {
        //                net = true;
        //                break;
        //            }
        //        }
        //        if (net == false)
        //        {
        //            Parameter.Add("up");
        //        }
        //    }
        //    if (TheGrid[EpicPart].x > TheGrid.First().x)
        //    {
        //        bool net = false;
        //        foreach (var part in EpicenterGrid2)
        //        {
        //            if ((TheGrid[part].x == TheGrid[EpicPart].x - GridPart.width) && (TheGrid[part].y == TheGrid[EpicPart].y))
        //            {
        //                net = true;
        //                break;
        //            }
        //        }
        //        if (net == false)
        //        {
        //            Parameter.Add("left");
        //        }
        //    }
        //    if ((TheGrid[EpicPart].x > TheGrid.First().x) && (TheGrid[EpicPart].y > TheGrid.Last().y))
        //    {
        //        bool net = false;
        //        foreach (var part in EpicenterGrid2)
        //        {
        //            if ((TheGrid[part].x == TheGrid[EpicPart].x - GridPart.width) && (TheGrid[part].y == TheGrid[EpicPart].y - GridPart.height))
        //            {
        //                net = true;
        //                break;
        //            }
        //        }
        //        if (net == false)
        //        {
        //            Parameter.Add("left-up");
        //        }

        //    }
        //    if ((TheGrid[EpicPart].x > TheGrid.First().x) && (TheGrid[EpicPart].y < TheGrid.Last().y))
        //    {
        //        bool net = false;
        //        foreach (var part in EpicenterGrid2)
        //        {
        //            if ((TheGrid[part].x == TheGrid[EpicPart].x - GridPart.width) && (TheGrid[part].y == TheGrid[EpicPart].y + GridPart.height))
        //            {
        //                net = true;
        //                break;
        //            }
        //        }
        //        if (net == false)
        //        {
        //            Parameter.Add("left-down");
        //        }
        //    }
        //    return Parameter;
        //}
        //public void Creater2(string Param, int StarterEpicPart, int level)
        //{
        //    switch (Param)
        //    {
        //        case "right":
        //            //EpicenterGrid2.Add(new GridPart(StarterEpicPart.x + GridPart.width, StarterEpicPart.y));
        //            //EpicenterGrid2.Add(TheGrid.IndexOf(TheGrid.FindIndex()));
        //            foreach (var grid in TheGrid)
        //            {
        //                if ((grid.x ==TheGrid[StarterEpicPart].x + GridPart.width)&&(grid.y== TheGrid[StarterEpicPart].y))
        //                {
        //                    EpicenterGrid2.Add(TheGrid.IndexOf(grid));
        //                    break;
        //                }
        //            }
        //            break;
        //        case "right-down":
        //            //EpicenterGrid2.Add(new GridPart(StarterEpicPart.x + GridPart.width, StarterEpicPart.y + GridPart.height));
        //            foreach (var grid in TheGrid)
        //            {
        //                if ((grid.x == TheGrid[StarterEpicPart].x + GridPart.width) && (grid.y == TheGrid[StarterEpicPart].y + GridPart.height))
        //                {
        //                    EpicenterGrid2.Add(TheGrid.IndexOf(grid));
        //                    break;
        //                }
        //            }
        //            break;
        //        case "right-up":
        //            //EpicenterGrid2.Add(new GridPart(StarterEpicPart.x + GridPart.width, StarterEpicPart.y - GridPart.height));
        //            foreach (var grid in TheGrid)
        //            {
        //                if ((grid.x == TheGrid[StarterEpicPart].x + GridPart.width) && (grid.y == TheGrid[StarterEpicPart].y - GridPart.height))
        //                {
        //                    EpicenterGrid2.Add(TheGrid.IndexOf(grid));
        //                    break;
        //                }
        //            }
        //            break;
        //        case "down":
        //            //EpicenterGrid2.Add(new GridPart(StarterEpicPart.x, StarterEpicPart.y + GridPart.height));
        //            foreach (var grid in TheGrid)
        //            {
        //                if ((grid.x == TheGrid[StarterEpicPart].x ) && (grid.y == TheGrid[StarterEpicPart].y + GridPart.height))
        //                {
        //                    EpicenterGrid2.Add(TheGrid.IndexOf(grid));
        //                    break;
        //                }
        //            }
        //            break;
        //        case "up":
        //            //EpicenterGrid2.Add(new GridPart(StarterEpicPart.x, StarterEpicPart.y - GridPart.height));
        //            foreach (var grid in TheGrid)
        //            {
        //                if ((grid.x == TheGrid[StarterEpicPart].x ) && (grid.y == TheGrid[StarterEpicPart].y - GridPart.height))
        //                {
        //                    EpicenterGrid2.Add(TheGrid.IndexOf(grid));
        //                    break;
        //                }
        //            }
        //            break;
        //        case "left":
        //            //EpicenterGrid2.Add(new GridPart(StarterEpicPart.x - GridPart.width, StarterEpicPart.y));
        //            foreach (var grid in TheGrid)
        //            {
        //                if ((grid.x == TheGrid[StarterEpicPart].x - GridPart.width) && (grid.y == TheGrid[StarterEpicPart].y))
        //                {
        //                    EpicenterGrid2.Add(TheGrid.IndexOf(grid));
        //                    break;
        //                }
        //            }
        //            break;
        //        case "left-down":
        //            //EpicenterGrid2.Add(new GridPart(StarterEpicPart.x - GridPart.width, StarterEpicPart.y + GridPart.height));
        //            foreach (var grid in TheGrid)
        //            {
        //                if ((grid.x == TheGrid[StarterEpicPart].x - GridPart.width) && (grid.y == TheGrid[StarterEpicPart].y + GridPart.height))
        //                {
        //                    EpicenterGrid2.Add(TheGrid.IndexOf(grid));
        //                    break;
        //                }
        //            }
        //            break;
        //        case "left-up":
        //            //EpicenterGrid2.Add(new GridPart(StarterEpicPart.x - GridPart.width, StarterEpicPart.y - GridPart.height));
        //            foreach (var grid in TheGrid)
        //            {
        //                if ((grid.x == TheGrid[StarterEpicPart].x - GridPart.width) && (grid.y == TheGrid[StarterEpicPart].y - GridPart.height))
        //                {
        //                    EpicenterGrid2.Add(TheGrid.IndexOf(grid));
        //                    break;
        //                }
        //            }
        //            break;
        //        default:
        //            break;
        //    }
        //}
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
        //    [XmlEnum("G")]
        [XmlEnum(Name = "1")]
        //   [StringValue("1")]
        GREEN = 1,
        //    [XmlEnum("Y")]
        [XmlEnum(Name = "2")]
        //     [StringValue("2")]
        YELLOW = 2,
        //    [XmlEnum("R")]
        [XmlEnum(Name = "3")]
        //     [StringValue("3")]
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
        public System.Threading.Mutex mutex;
        public int x, y;
        public int status { get; set; }
        public bool check { get; set; } = false; //todo
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