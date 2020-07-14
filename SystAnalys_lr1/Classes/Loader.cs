//This is a personal academic project. Dear PVS-Studio, please check it.
//PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
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
                    Main.Grid = (Classes.Grid)deserializerV.Deserialize(reader);
                    Main.Grid.GridHeight = 40;
                    Main.Grid.GridWidth = 80;
                }
            }

            if (File.Exists(load + "grid.json"))
            {
                using (StreamReader reader = new StreamReader(load + "grid.json"))
                {
                    Main.Grid = JsonConvert.DeserializeObject<Classes.Grid>(reader.ReadToEnd());
                    Main.Grid.GridHeight = 40;
                    Main.Grid.GridWidth = 80;
                }
            }
            
            if (File.Exists(load + "StopPoints.xml"))
            {
                using (StreamReader reader = new StreamReader(load + "StopPoints.xml"))
                {
                    XmlSerializer deserializerV = new XmlSerializer(typeof(SerializableDictionary<string, List<BusStop>>));
                    Data.StopPoints = (SerializableDictionary<string, List<BusStop>>)deserializerV.Deserialize(reader);
                    foreach (var sp in Data.StopPoints.Values)
                    {
                        foreach (var s in sp)
                            if (!Data.AllstopPoints.Contains(s))
                                Data.AllstopPoints.Add(s);
                    }
                    Data.StopPointsInGrids = new SerializableDictionary<string, List<int>>();
                    foreach (var StopList in Data.StopPoints)
                    {
                        Data.StopPointsInGrids.Add(StopList.Key, new List<int>());
                        foreach (var vertex in StopList.Value)
                        {
                            Data.StopPointsInGrids[StopList.Key].Add(vertex.GridNum);
                        }

                    }
                }
            }

            if (File.Exists(load + "allStopPoints.xml"))
            {
                using (StreamReader reader = new StreamReader(load + "allStopPoints.xml"))
                {
                    XmlSerializer deserializerV = new XmlSerializer(typeof(List<BusStop>));
                    Data.AllstopPoints = (List<BusStop>)deserializerV.Deserialize(reader);

                }
            }

            if (File.Exists(load + "StopPoints.json"))
            {
                using (StreamReader reader = new StreamReader(load + "StopPoints.json"))
                {
                    Data.StopPoints = JsonConvert.DeserializeObject<SerializableDictionary<string, List<BusStop>>>(File.ReadAllText(load + "StopPoints.json"));
                    foreach (var sp in Data.StopPoints.Values)
                    {
                        foreach (var s in sp)
                            if (!Data.AllstopPoints.Contains(s))
                                Data.AllstopPoints.Add(s);
                    }
                    Data.StopPointsInGrids = new SerializableDictionary<string, List<int>>();
                    foreach (var StopList in Data.StopPoints)
                    {
                        Data.StopPointsInGrids.Add(StopList.Key, new List<int>());
                        foreach (var vertex in StopList.Value)
                        {
                            Data.StopPointsInGrids[StopList.Key].Add(vertex.GridNum);
                        }

                    }

                }
            }

            if (File.Exists(load + "allStopPoints.json"))
            {
                using (StreamReader reader = new StreamReader(load + "allStopPoints.json"))
                {
                    Data.AllstopPoints = JsonConvert.DeserializeObject<List<BusStop>>(File.ReadAllText(load + "allStopPoints.json"));
                }
            }            

            if (File.Exists(load + "traficLights.xml"))
            {
                XmlSerializer tl = new XmlSerializer(typeof(List<TraficLight>));
                using (StreamReader reader = new StreamReader(load + "traficLights.xml"))
                    Data.TraficLights = (List<TraficLight>)tl.Deserialize(reader);
                Data.TraficLightsInGrids = new List<int>();
                foreach (var item in Data.TraficLights)
                {
                    Data.TraficLightsInGrids.Add(item.GridNum);
                }
                foreach (var tll in Data.TraficLights)
                {
                    tll.Set();
                    tll.Start();
                }
                sheet.Image = Main.G.GetBitmap();
            }
            if (File.Exists(load + "traficLights.json"))
            {
                using (StreamReader reader = new StreamReader(load + "traficLights.json"))
                {
                    Data.TraficLights = JsonConvert.DeserializeObject<List<TraficLight>>(reader.ReadToEnd());
                    Data.TraficLightsInGrids = new List<int>();
                    foreach (var item in Data.TraficLights)
                    {
                        Data.TraficLightsInGrids.Add(item.GridNum);
                    }
                    foreach (var tll in Data.TraficLights)
                    {
                        tll.Set();
                        tll.Start();
                    }
                    sheet.Image = Main.G.GetBitmap();
                }
            }

            if (Data.Buses != null)
            {
                Data.Buses.Clear();
            }

            if (File.Exists(load + "Buses.json"))
            {
                using (StreamReader reader = new StreamReader(load + "Buses.json"))
                {
                    Data.Buses = JsonConvert.DeserializeObject<List<Bus>>(reader.ReadToEnd());
                }
            }

            XmlSerializer deserializerAllBuses = new XmlSerializer(typeof(List<Bus>));
            if (File.Exists(load + "Buses.xml"))
            {
                using (StreamReader reader = new StreamReader(load + "Buses.xml"))
                {
                    Data.Buses = (List<Bus>)deserializerAllBuses.Deserialize(reader);
                }
            }
            foreach (var tl in Data.TraficLights)
            {
                tl.Start();
            }
            Image num;
            Bitmap original = new Bitmap(1, 1);
            foreach (var x in Data.Buses)
            {

                if (x.Tracker == true)
                {
                    Rectangle rect = new Rectangle(0, 0, 200, 100);
                    x.BusPic = new Bitmap(Bus.BusImg);
                    x.BusPic = new Bitmap(x.BusPic, new Size(15, 15));
                    num = new Bitmap(x.BusPic.Height, x.BusPic.Width);
                    using (Graphics gr = Graphics.FromImage(num))
                    {
                        using (Font font = new Font("Segoe UI", 8))
                        {
                            var textBounds = gr.VisibleClipBounds;
                            textBounds.Inflate(-1, -1);
                            gr.FillRectangle(Brushes.Transparent, rect);

                            gr.DrawString(
                                x.Route.ToString(),
                                font,
                                Brushes.Black, 
                                rect, 
                                StringFormat.GenericTypographic
                                
                                );
                        }
                    }

                    original = new Bitmap(Math.Max(x.BusPic.Width, num.Width), Math.Max(x.BusPic.Height, num.Height) * 2); 
                    using (Graphics graphics = Graphics.FromImage(original))
                    {
                        graphics.DrawImage(x.BusPic, 0, 0);
                        graphics.DrawImage(num, 0, 15);
                        graphics.Dispose();
                    }
                }
                else
                {
                    Rectangle rect = new Rectangle(0, 0, 200, 100);
                    x.BusPic = new Bitmap(Bus.OffBusImg);
                    x.BusPic = new Bitmap(x.BusPic, new Size(15, 15));
                    num = new Bitmap(x.BusPic.Height, x.BusPic.Width);
                    using (Graphics gr = Graphics.FromImage(num))
                    {
                        using (Font font = new Font("Segoe UI", 8))
                        {
                            gr.FillRectangle(Brushes.Transparent, rect);

                            gr.DrawString(
                                x.Route.ToString(),
                                font,
                                Brushes.Black, 
                                rect, 
                                StringFormat.GenericTypographic
                                );
                        }
                    }

                    original = new Bitmap(Math.Max(x.BusPic.Width, num.Width), Math.Max(x.BusPic.Height, num.Height) * 2);
                    using (Graphics graphics = Graphics.FromImage(original))
                    {

                        graphics.DrawImage(x.BusPic, 0, 0);
                        graphics.DrawImage(num, 0, 15);
                        graphics.Dispose();

                    }
                }


                x.BusPic = original;

                x.Skips.SkipTrafficLights = 5;
                x.Skips.SkipStops = 5;
                x.Skips.SkipEnd = 5;

            }
           
            timer.Start();
            
            XmlSerializer ver = new XmlSerializer(typeof(List<Vertex>));
            XmlSerializer ed = new XmlSerializer(typeof(List<Edge>));


            XmlSerializer Ver = new XmlSerializer(typeof(SerializableDictionary<string, List<Vertex>>));
            XmlSerializer Edge = new XmlSerializer(typeof(SerializableDictionary<string, List<Edge>>));

            if (File.Exists(load + "vertexRoutes.xml"))
            {
                using (StreamReader reader = new StreamReader(load + "vertexRoutes.xml"))
                    Data.Routes = (SerializableDictionary<string, List<Vertex>>)Ver.Deserialize(reader);
            }

            if (File.Exists(load + "vertexRoutes.json"))
            {
                using (StreamReader reader = new StreamReader(load + "vertexRoutes.json"))
                {
                    Data.Routes = JsonConvert.DeserializeObject<SerializableDictionary<string, List<Vertex>>>(reader.ReadToEnd());
                }
            }
            if (File.Exists(load + "edgeRoutes.xml"))
            {
                using (StreamReader reader = new StreamReader(load + "edgeRoutes.xml"))
                    Data.RoutesEdge = (SerializableDictionary<string, List<Edge>>)Edge.Deserialize(reader);
                Main.SaveF = "xml";
            }

            if (File.Exists(load + "edgeRoutes.json"))
            {
                using (StreamReader reader = new StreamReader(load + "edgeRoutes.json"))
                {
                    Data.RoutesEdge = JsonConvert.DeserializeObject<SerializableDictionary<string, List<Edge>>>(reader.ReadToEnd());
                }
                Main.SaveF = "json";
            }
            XmlSerializer deserializerAllCoor = new XmlSerializer(typeof(SerializableDictionary<string, List<Point>>));
            if (File.Exists(load + "AllCoordinates.xml"))
            {
                using (StreamReader reader = new StreamReader(load + "AllCoordinates.xml"))
                    Data.AllCoordinates = (SerializableDictionary<string, List<Point>>)deserializerAllCoor.Deserialize(reader);
            }

            if (File.Exists(load + "AllCoordinates.json"))
            {
                using (StreamReader reader = new StreamReader(load + "AllCoordinates.json"))
                {
                    Data.AllCoordinates = JsonConvert.DeserializeObject<SerializableDictionary<string, List<Point>>>(reader.ReadToEnd());
                }
            }
            XmlSerializer deserializerAllGridsInRoutes = new XmlSerializer(typeof(SerializableDictionary<string, List<int>>));
            if (File.Exists(load + "AllGridsInRoutes.xml"))
            {
                using (StreamReader reader = new StreamReader(load + "AllGridsInRoutes.xml"))
                    Data.AllGridsInRoutes = (SerializableDictionary<string, List<int>>)deserializerAllGridsInRoutes.Deserialize(reader);
            }

            if (File.Exists(load + "AllGridsInRoutes.json"))
            {
                using (StreamReader reader = new StreamReader(load + "AllGridsInRoutes.json"))
                {
                    Data.AllGridsInRoutes = JsonConvert.DeserializeObject<SerializableDictionary<string, List<int>>>(reader.ReadToEnd());
                }
            }

            XmlSerializer stations = new XmlSerializer(typeof(List<Vertex>));
            if (File.Exists(load + "stations.xml"))
            {
                using (StreamReader reader = new StreamReader(load + "stations.xml"))
                    Data.Staions = (List<Vertex>)stations.Deserialize(reader);
            }

            if (File.Exists(load + "stations.json"))
            {
                using (StreamReader reader = new StreamReader(load + "stations.json"))
                {
                    Data.Staions = JsonConvert.DeserializeObject<List<Vertex>>(reader.ReadToEnd());
                }
            }
        }
    }
}
