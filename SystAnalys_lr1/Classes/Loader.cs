using Newtonsoft.Json;
using SystAnalys_lr1.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SystAnalys_lr1.Classes
{
    public static class Loader
    {
        public static void Load(string load, LoadingForm loadingForm, PictureBox sheet, Timer timer)
        {
            if (File.Exists(load + "Vertices.xml"))
            {
                using (StreamReader reader = new StreamReader(load + "Vertices.xml"))
                {
                    XmlSerializer deserializerV = new XmlSerializer(typeof(List<Vertex>));
                    Data.V = (List<Vertex>)deserializerV.Deserialize(reader);
                }
            }

            if (File.Exists(load + "Vertices.json"))
            {
                using (StreamReader reader = new StreamReader(load + "Vertices.json"))
                {
                    Data.V = JsonConvert.DeserializeObject<List<Vertex>>(File.ReadAllText(load + "Vertices.json"));
                }
            }
            loadingForm.loading.Value = 10;

            if (File.Exists(load + "Edges.xml"))
            {
                using (StreamReader reader = new StreamReader(load + "Edges.xml"))
                {
                    XmlSerializer deserializerE = new XmlSerializer(typeof(List<Edge>));
                    Data.E = (List<Edge>)deserializerE.Deserialize(reader);
                }
            }

            if (File.Exists(load + "Edges.json"))
            {
                using (StreamReader reader = new StreamReader(load + "Edges.json"))
                {
                    Data.E = JsonConvert.DeserializeObject<List<Edge>>(reader.ReadToEnd());
                }
            }

            if (File.Exists(load + "grid.xml"))
            {
                using (StreamReader reader = new StreamReader(load + "grid.xml"))
                {
                    XmlSerializer deserializerV = new XmlSerializer(typeof(Classes.Grid));
                    Main.g = (Classes.Grid)deserializerV.Deserialize(reader);
                    Main.g.gridHeight = 40;
                    Main.g.gridWidth = 80;
                }
            }

            if (File.Exists(load + "grid.json"))
            {
                using (StreamReader reader = new StreamReader(load + "grid.json"))
                {
                    Main.g = JsonConvert.DeserializeObject<Classes.Grid>(reader.ReadToEnd());
                    Main.g.gridHeight = 40;
                    Main.g.gridWidth = 80;
                }
            }

            loadingForm.loading.Value = 20;

            if (File.Exists(load + "StopPoints.xml"))
            {
                using (StreamReader reader = new StreamReader(load + "StopPoints.xml"))
                {
                    XmlSerializer deserializerV = new XmlSerializer(typeof(SerializableDictionary<string, List<Vertex>>));
                    Data.stopPoints = (SerializableDictionary<string, List<BusStop>>)deserializerV.Deserialize(reader);
                    foreach (var sp in Data.stopPoints.Values)
                    {
                        foreach (var s in sp)
                            if (!Data.allstopPoints.Contains(s))
                                Data.allstopPoints.Add(s);
                    }
                    Data.stopPointsInGrids = new SerializableDictionary<string, List<int>>();
                    foreach (var StopList in Data.stopPoints)
                    {
                        Data.stopPointsInGrids.Add(StopList.Key, new List<int>());
                        foreach (var vertex in StopList.Value)
                        {
                            Data.stopPointsInGrids[StopList.Key].Add(vertex.gridNum);
                        }

                    }
                }
            }

            if (File.Exists(load + "allStopPoints.xml"))
            {
                using (StreamReader reader = new StreamReader(load + "allStopPoints.xml"))
                {
                    XmlSerializer deserializerV = new XmlSerializer(typeof(List<Vertex>));
                    Data.allstopPoints = (List<BusStop>)deserializerV.Deserialize(reader);

                }
            }

            if (File.Exists(load + "StopPoints.json"))
            {
                using (StreamReader reader = new StreamReader(load + "StopPoints.json"))
                {
                    Data.stopPoints = JsonConvert.DeserializeObject<SerializableDictionary<string, List<BusStop>>>(File.ReadAllText(load + "StopPoints.json"));
                    foreach (var sp in Data.stopPoints.Values)
                    {
                        foreach (var s in sp)
                            if (!Data.allstopPoints.Contains(s))
                                Data.allstopPoints.Add(s);
                    }
                    Data.stopPointsInGrids = new SerializableDictionary<string, List<int>>();
                    foreach (var StopList in Data.stopPoints)
                    {
                        Data.stopPointsInGrids.Add(StopList.Key, new List<int>());
                        foreach (var vertex in StopList.Value)
                        {
                            Data.stopPointsInGrids[StopList.Key].Add(vertex.gridNum);
                        }

                    }

                }
            }

            if (File.Exists(load + "allStopPoints.json"))
            {
                using (StreamReader reader = new StreamReader(load + "allStopPoints.json"))
                {
                    Data.allstopPoints = JsonConvert.DeserializeObject<List<BusStop>>(File.ReadAllText(load + "allStopPoints.json"));
                }
            }

            loadingForm.loading.Value = 30;


            if (File.Exists(load + "Data.traficLights.xml"))
            {
                XmlSerializer tl = new XmlSerializer(typeof(List<TraficLight>));
                using (StreamReader reader = new StreamReader(load + "Data.traficLights.xml"))
                    Data.traficLights = (List<TraficLight>)tl.Deserialize(reader);
                Data.TraficLightsInGrids = new List<int>();
                foreach (var item in Data.traficLights)
                {
                    Data.TraficLightsInGrids.Add(item.gridNum);
                }
                foreach (var tll in Data.traficLights)
                {
                    tll.Set();
                    tll.Start();
                }
                sheet.Image = Main.G.GetBitmap();
            }
            if (File.Exists(load + "Data.traficLights.json"))
            {
                using (StreamReader reader = new StreamReader(load + "Data.traficLights.json"))
                {
                    Data.traficLights = JsonConvert.DeserializeObject<List<TraficLight>>(reader.ReadToEnd());
                    Data.TraficLightsInGrids = new List<int>();
                    foreach (var item in Data.traficLights)
                    {
                        Data.TraficLightsInGrids.Add(item.gridNum);
                    }
                    foreach (var tll in Data.traficLights)
                    {
                        tll.Set();
                        tll.Start();
                    }
                    sheet.Image = Main.G.GetBitmap();
                }
            }
            loadingForm.loading.Value = 40;

            if (Data.buses != null)
            {
                Data.buses.Clear();
            }

            if (File.Exists(load + "Buses.json"))
            {
                using (StreamReader reader = new StreamReader(load + "Buses.json"))
                {
                    Data.buses = JsonConvert.DeserializeObject<List<Bus>>(reader.ReadToEnd());
                }
            }

            XmlSerializer deserializerAllBuses = new XmlSerializer(typeof(List<Bus>));
            if (File.Exists(load + "Buses.xml"))
            {
                using (StreamReader reader = new StreamReader(load + "Buses.xml"))
                {
                    Data.buses = (List<Bus>)deserializerAllBuses.Deserialize(reader);
                }
            }
            loadingForm.loading.Value = 60;
            foreach (var tl in Data.traficLights)
            {
                tl.Start();
            }
            Image num;
            Bitmap original = new Bitmap(1, 1);
            foreach (var x in Data.buses)
            {

                if (x.Tracker == true)
                {
                    Rectangle rect = new Rectangle(0, 0, 200, 100);
                    x.BusPic = new Bitmap(Bus.BusImg);
                    x.BusPic = new Bitmap(x.BusPic, new Size(15, 15));
                    num = new Bitmap(x.BusPic.Height, x.BusPic.Width);
                    using (Graphics gr = Graphics.FromImage(num))
                    {
                        using (Font font = new Font("Arial", 10))
                        {
                            // Заливаем фон нужным цветом.
                            gr.FillRectangle(Brushes.Transparent, rect);

                            // Выводим текст.
                            gr.DrawString(
                                x.Route.ToString(),
                                font,
                                Brushes.Black, // цвет текста
                                rect, // текст будет вписан в указанный прямоугольник
                                StringFormat.GenericTypographic
                                );
                        }
                    }

                    original = new Bitmap(Math.Max(x.BusPic.Width, num.Width), Math.Max(x.BusPic.Height, num.Height) * 2); //load the image file
                    using (Graphics graphics = Graphics.FromImage(original))
                    {

                        graphics.DrawImage(x.BusPic, 0, 0);
                        graphics.DrawImage(num, 0, 15);
                        graphics.Dispose();

                    }
                    //  bitmap = new Bitmap(original, new Size(15, 15));
                }
                else
                {
                    Rectangle rect = new Rectangle(0, 0, 200, 100);
                    x.BusPic = new Bitmap(Bus.OffBusImg);
                    x.BusPic = new Bitmap(x.BusPic, new Size(15, 15));
                    num = new Bitmap(x.BusPic.Height, x.BusPic.Width);
                    using (Graphics gr = Graphics.FromImage(num))
                    {
                        using (Font font = new Font("Arial", 10))
                        {
                            // Заливаем фон нужным цветом.
                            gr.FillRectangle(Brushes.Transparent, rect);

                            // Выводим текст.
                            gr.DrawString(
                                x.Route.ToString(),
                                font,
                                Brushes.Black, // цвет текста
                                rect, // текст будет вписан в указанный прямоугольник
                                StringFormat.GenericTypographic
                                );
                        }
                    }

                    original = new Bitmap(Math.Max(x.BusPic.Width, num.Width), Math.Max(x.BusPic.Height, num.Height) * 2); //load the image file
                    using (Graphics graphics = Graphics.FromImage(original))
                    {

                        graphics.DrawImage(x.BusPic, 0, 0);
                        graphics.DrawImage(num, 0, 15);
                        graphics.Dispose();

                    }
                }


                x.BusPic = original;//res;// new Bitmap(res, new Size(1000, 1000));

                x.Skip = 5;
                x.skipStops = 5;
                x.skipEnd = 5;

            }
            //
            timer.Start();
            //
            XmlSerializer ver = new XmlSerializer(typeof(List<Vertex>));
            XmlSerializer ed = new XmlSerializer(typeof(List<Edge>));


            XmlSerializer Ver = new XmlSerializer(typeof(SerializableDictionary<string, List<Vertex>>));
            XmlSerializer Edge = new XmlSerializer(typeof(SerializableDictionary<string, List<Edge>>));

            if (File.Exists(load + "vertexRoutes.xml"))
            {
                using (StreamReader reader = new StreamReader(load + "vertexRoutes.xml"))
                    Data.routes = (SerializableDictionary<string, List<Vertex>>)Ver.Deserialize(reader);
            }

            if (File.Exists(load + "vertexRoutes.json"))
            {
                using (StreamReader reader = new StreamReader(load + "vertexRoutes.json"))
                {
                    Data.routes = JsonConvert.DeserializeObject<SerializableDictionary<string, List<Vertex>>>(reader.ReadToEnd());
                }
            }
            loadingForm.loading.Value = 80;
            if (File.Exists(load + "edgeRoutes.xml"))
            {
                using (StreamReader reader = new StreamReader(load + "edgeRoutes.xml"))
                    Data.routesEdge = (SerializableDictionary<string, List<Edge>>)Edge.Deserialize(reader);
                Main.saveF = "xml";
            }

            if (File.Exists(load + "edgeRoutes.json"))
            {
                using (StreamReader reader = new StreamReader(load + "edgeRoutes.json"))
                {
                    Data.routesEdge = JsonConvert.DeserializeObject<SerializableDictionary<string, List<Edge>>>(reader.ReadToEnd());
                }
                Main.saveF = "json";
            }
            XmlSerializer deserializerAllCoor = new XmlSerializer(typeof(SerializableDictionary<string, List<Point>>));
            if (File.Exists(load + "Data.AllCoordinates.xml"))
            {
                using (StreamReader reader = new StreamReader(load + "Data.AllCoordinates.xml"))
                    Data.AllCoordinates = (SerializableDictionary<string, List<Point>>)deserializerAllCoor.Deserialize(reader);
            }

            if (File.Exists(load + "Data.AllCoordinates.json"))
            {
                using (StreamReader reader = new StreamReader(load + "Data.AllCoordinates.json"))
                {
                    Data.AllCoordinates = JsonConvert.DeserializeObject<SerializableDictionary<string, List<Point>>>(reader.ReadToEnd());
                }
            }
            XmlSerializer deserializerAllGridsInRoutes = new XmlSerializer(typeof(SerializableDictionary<string, List<int>>));
            if (File.Exists(load + "Data.AllGridsInRoutes.xml"))
            {
                using (StreamReader reader = new StreamReader(load + "Data.AllGridsInRoutes.xml"))
                    Data.AllGridsInRoutes = (SerializableDictionary<string, List<int>>)deserializerAllGridsInRoutes.Deserialize(reader);
            }

            if (File.Exists(load + "Data.AllGridsInRoutes.json"))
            {
                using (StreamReader reader = new StreamReader(load + "Data.AllGridsInRoutes.json"))
                {
                    Data.AllGridsInRoutes = JsonConvert.DeserializeObject<SerializableDictionary<string, List<int>>>(reader.ReadToEnd());
                }
            }
            loadingForm.loading.Value = 90;
        }
    }
}
