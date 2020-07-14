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
using System.Xml.Serialization;

namespace SystAnalys_lr1.Classes
{
    public static class Saver
    {
        public static void SaveJSON(string save, LoadingForm loadingForm, string saveFormat)
        {
            string json = JsonConvert.SerializeObject(Data.V);
            File.WriteAllText(save + "Vertices.json", json);
            json = JsonConvert.SerializeObject(Data.E);
            File.WriteAllText(save + "Edges.json", json);
            json = JsonConvert.SerializeObject(Data.Buses);
            File.WriteAllText(save + "Buses.json", json);
            json = JsonConvert.SerializeObject(Data.AllCoordinates);
            File.WriteAllText(save + "AllCoordinates.json", json);
            json = JsonConvert.SerializeObject(Data.AllGridsInRoutes);
            File.WriteAllText(save + "AllGridsInRoutes.json", json);
            json = JsonConvert.SerializeObject(Data.StopPoints);
            File.WriteAllText(save + "StopPoints.json", json);
            json = JsonConvert.SerializeObject(Data.AllstopPoints);
            File.WriteAllText(save + "allStopPoints.json", json);
            json = JsonConvert.SerializeObject(Data.Routes);
            File.WriteAllText(save + "vertexRoutes.json", json);
            json = JsonConvert.SerializeObject(Main.Grid);
            File.WriteAllText(save + "grid.json", json);
            json = JsonConvert.SerializeObject(Data.RoutesEdge);
            File.WriteAllText(save + "edgeRoutes.json", json);
            json = JsonConvert.SerializeObject(Data.TraficLights);
            File.WriteAllText(save + "traficLights.json", json);
            json = JsonConvert.SerializeObject(Data.Staions);
            File.WriteAllText(save + "stations.json", json);
            Main.SaveF = saveFormat;
            return;
        }

        public static void SaveXML(string save, LoadingForm loadingForm, string saveFormat)
        {
            XmlSerializer serializerV = new XmlSerializer(typeof(List<Vertex>));
            XmlSerializer serializerE = new XmlSerializer(typeof(List<Edge>));

            File.Delete(save + "Vertices.xml");
            using (FileStream fileV = new FileStream(save + "Vertices.xml", FileMode.OpenOrCreate))
            {
                serializerV.Serialize(fileV, Data.V);
                Console.WriteLine("Объект сериализован");
                fileV.Close();
            }
            File.Delete(save + "Edges.xml");
            using (FileStream fileE = new FileStream(save + "Edges.xml", FileMode.OpenOrCreate))
            {
                serializerE.Serialize(fileE, Data.E);
                Console.WriteLine("Объект сериализован");
                fileE.Close();
            }
            File.Delete(save + "Buses.xml");
            XmlSerializer serializerAllBuses = new XmlSerializer(typeof(List<Bus>));
            using (FileStream fileB = new FileStream(save + "Buses.xml", FileMode.OpenOrCreate))
            {
                serializerAllBuses.Serialize(fileB, Data.Buses);
                Console.WriteLine("Объект сериализован");
            }

            File.Delete(save + "AllCoordinates.xml");
            XmlSerializer serializerAllCoor = new XmlSerializer(typeof(SerializableDictionary<string, List<Point>>));
            using (FileStream fileA = new FileStream(save + "AllCoordinates.xml", FileMode.OpenOrCreate))
            {
                serializerAllCoor.Serialize(fileA, Data.AllCoordinates);
                Console.WriteLine("Объект сериализован");
            }
            File.Delete(save + "AllGridsInRoutes.xml");
            XmlSerializer serializerAllGridsInRoutes = new XmlSerializer(typeof(SerializableDictionary<string, List<int>>));
            using (FileStream fileAG = new FileStream(save + "AllGridsInRoutes.xml", FileMode.OpenOrCreate))
            {
                serializerAllGridsInRoutes.Serialize(fileAG, Data.AllGridsInRoutes);
                Console.WriteLine("Объект сериализован");
            }
            XmlSerializer Ver = new XmlSerializer(typeof(SerializableDictionary<string, List<Vertex>>));
            XmlSerializer Edge = new XmlSerializer(typeof(SerializableDictionary<string, List<Edge>>));
            XmlSerializer stopV = new XmlSerializer(typeof(SerializableDictionary<string, List<BusStop>>));

            File.Delete(save + "vertexRoutes.xml");
            using (FileStream fileV = new FileStream(save + "vertexRoutes.xml", FileMode.OpenOrCreate))
            {
                Ver.Serialize(fileV, Data.Routes);
                Console.WriteLine("Объект сериализован");
            }
            File.Delete(save + "StopPoints.xml");
            using (FileStream fileV = new FileStream(save + "StopPoints.xml", FileMode.OpenOrCreate))
            {
                stopV.Serialize(fileV, Data.StopPoints);
                Console.WriteLine("Объект сериализован");
            }
            File.Delete(save + "allStopPoints.xml");
            using (FileStream fileV = new FileStream(save + "allStopPoints.xml", FileMode.OpenOrCreate))
            {
                stopV = new XmlSerializer(typeof(List<BusStop>));
                stopV.Serialize(fileV, Data.AllstopPoints);
                Console.WriteLine("Объект сериализован");
            }

            File.Delete(save + "edgeRoutes.xml");
            using (FileStream fileE = new FileStream(save + "edgeRoutes.xml", FileMode.OpenOrCreate))
            {
                Edge.Serialize(fileE, Data.RoutesEdge);
                Console.WriteLine("Объект сериализован");

            }
            foreach (var tl in Data.TraficLights)
            {
                tl.Stop();
            }
            File.Delete(save + "traficLights.xml");
            using (FileStream fileTL = new FileStream(save + "traficLights.xml", FileMode.OpenOrCreate))
            {
                XmlSerializer tl = new XmlSerializer(typeof(List<TraficLight>));
                tl.Serialize(fileTL, Data.TraficLights);

                Console.WriteLine("Объект сериализован");

            }
            File.Delete(save + "grid.xml");
            using (FileStream fileTL = new FileStream(save + "grid.xml", FileMode.OpenOrCreate))
            {
                XmlSerializer tl = new XmlSerializer(typeof(Classes.Grid));
                tl.Serialize(fileTL, Main.Grid);

                Console.WriteLine("Объект сериализован");

            }

            File.Delete(save + "stations.xml");
            using (FileStream fileTL = new FileStream(save + "stations.xml", FileMode.OpenOrCreate))
            {
                XmlSerializer tl = new XmlSerializer(typeof(List<Vertex>));
                tl.Serialize(fileTL, Data.Staions);

                Console.WriteLine("Объект сериализован");

            }

            Main.SaveF = saveFormat;
            return;
        }
    }
}
