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
        public static void saveJSON(string save, LoadingForm loadingForm, string saveFormat)
        {
            loadingForm.loading.Value = 10;
            string json = JsonConvert.SerializeObject(Data.V);
            File.WriteAllText(save + "Vertices.json", json);
            loadingForm.loading.Value = 20;
            json = JsonConvert.SerializeObject(Data.E);
            File.WriteAllText(save + "Edges.json", json);
            loadingForm.loading.Value = 30;
            json = JsonConvert.SerializeObject(Data.buses);
            File.WriteAllText(save + "Buses.json", json);
            loadingForm.loading.Value = 50;
            //AsyncCreateAllCoordinates();
            json = JsonConvert.SerializeObject(Data.AllCoordinates);
            File.WriteAllText(save + "AllCoordinates.json", json);
            loadingForm.loading.Value = 60;
            json = JsonConvert.SerializeObject(Data.AllGridsInRoutes);
            File.WriteAllText(save + "AllGridsInRoutes.json", json);
            loadingForm.loading.Value = 70;
            json = JsonConvert.SerializeObject(Data.stopPoints);
            File.WriteAllText(save + "StopPoints.json", json);
            json = JsonConvert.SerializeObject(Data.allstopPoints);
            File.WriteAllText(save + "allStopPoints.json", json);
            loadingForm.loading.Value = 80;
            json = JsonConvert.SerializeObject(Data.routes);
            File.WriteAllText(save + "vertexRoutes.json", json);
            json = JsonConvert.SerializeObject(Main.g);
            File.WriteAllText(save + "grid.json", json);
            loadingForm.loading.Value = 90;
            json = JsonConvert.SerializeObject(Data.routesEdge);
            File.WriteAllText(save + "edgeRoutes.json", json);
            json = JsonConvert.SerializeObject(Data.traficLights);
            File.WriteAllText(save + "traficLights.json", json);
            Main.saveF = saveFormat;
            loadingForm.loading.Value = 100;
            loadingForm.close = true;
            loadingForm.Close();
            return;
        }

        public static void saveXML(string save, LoadingForm loadingForm, string saveFormat)
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
            loadingForm.loading.Value = 10;
            File.Delete(save + "Edges.xml");
            using (FileStream fileE = new FileStream(save + "Edges.xml", FileMode.OpenOrCreate))
            {
                serializerE.Serialize(fileE, Data.E);
                Console.WriteLine("Объект сериализован");
                fileE.Close();
            }
            loadingForm.loading.Value = 20;
            File.Delete(save + "Buses.xml");
            XmlSerializer serializerAllBuses = new XmlSerializer(typeof(List<Bus>));
            using (FileStream fileB = new FileStream(save + "Buses.xml", FileMode.OpenOrCreate))
            {
                serializerAllBuses.Serialize(fileB, Data.buses);
                Console.WriteLine("Объект сериализован");
            }
            loadingForm.loading.Value = 30;

            //AsyncCreateAllCoordinates();
            File.Delete(save + "AllCoordinates.xml");
            XmlSerializer serializerAllCoor = new XmlSerializer(typeof(SerializableDictionary<string, List<Point>>));
            using (FileStream fileA = new FileStream(save + "AllCoordinates.xml", FileMode.OpenOrCreate))
            {
                serializerAllCoor.Serialize(fileA, Data.AllCoordinates);
                Console.WriteLine("Объект сериализован");
            }
            loadingForm.loading.Value = 50;
            File.Delete(save + "AllGridsInRoutes.xml");
            XmlSerializer serializerAllGridsInRoutes = new XmlSerializer(typeof(SerializableDictionary<string, List<int>>));
            using (FileStream fileAG = new FileStream(save + "AllGridsInRoutes.xml", FileMode.OpenOrCreate))
            {
                serializerAllGridsInRoutes.Serialize(fileAG, Data.AllGridsInRoutes);
                Console.WriteLine("Объект сериализован");
            }
            loadingForm.loading.Value = 60;
            XmlSerializer Ver = new XmlSerializer(typeof(SerializableDictionary<string, List<Vertex>>));
            XmlSerializer Edge = new XmlSerializer(typeof(SerializableDictionary<string, List<Edge>>));
            XmlSerializer stopV = new XmlSerializer(typeof(SerializableDictionary<string, List<BusStop>>));

            File.Delete(save + "vertexRoutes.xml");
            using (FileStream fileV = new FileStream(save + "vertexRoutes.xml", FileMode.OpenOrCreate))
            {
                Ver.Serialize(fileV, Data.routes);
                Console.WriteLine("Объект сериализован");
            }
            loadingForm.loading.Value = 70;
            File.Delete(save + "StopPoints.xml");
            using (FileStream fileV = new FileStream(save + "StopPoints.xml", FileMode.OpenOrCreate))
            {               
                stopV.Serialize(fileV, Data.stopPoints);
                Console.WriteLine("Объект сериализован");
            }
            File.Delete(save + "allStopPoints.xml");
            using (FileStream fileV = new FileStream(save + "allStopPoints.xml", FileMode.OpenOrCreate))
            {
                stopV = new XmlSerializer(typeof(List<BusStop>));
                stopV.Serialize(fileV, Data.allstopPoints);
                Console.WriteLine("Объект сериализован");
            }
            loadingForm.loading.Value = 80;

            File.Delete(save + "edgeRoutes.xml");
            using (FileStream fileE = new FileStream(save + "edgeRoutes.xml", FileMode.OpenOrCreate))
            {
                Edge.Serialize(fileE, Data.routesEdge);
                Console.WriteLine("Объект сериализован");

            }
            foreach (var tl in Data.traficLights)
            {
                tl.Stop();
            }
            File.Delete(save + "traficLights.xml");
            using (FileStream fileTL = new FileStream(save + "traficLights.xml", FileMode.OpenOrCreate))
            {
                XmlSerializer tl = new XmlSerializer(typeof(List<TraficLight>));
                tl.Serialize(fileTL, Data.traficLights);

                Console.WriteLine("Объект сериализован");

            }
            File.Delete(save + "grid.xml");
            using (FileStream fileTL = new FileStream(save + "grid.xml", FileMode.OpenOrCreate))
            {
                XmlSerializer tl = new XmlSerializer(typeof(Classes.Grid));
                tl.Serialize(fileTL, Main.g);

                Console.WriteLine("Объект сериализован");

            }

            Main.saveF = saveFormat;
            loadingForm.loading.Value = 90;
            loadingForm.loading.Value = 100;
            loadingForm.close = true;
            loadingForm.Close();
            return;
        }
    }
}
