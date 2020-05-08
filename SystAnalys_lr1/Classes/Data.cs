using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystAnalys_lr1.Classes
{
    public static class Data
    {
        //все массивы
        //Лист всех эпицентров
        public static List<Epicenter> Epics;
        //Лист, в котором хранится сетка
        static public List<GridPart> TheGrid { get; set; }
        //Лист, в котором хранятся автобусы
        static public List<Bus> buses;
        public static List<List<Bus>> busesPark;
        //все вершины
        static public List<Vertex> V;
        //ребра маршрутов
        static public SerializableDictionary<string, List<Edge>> routesEdge;
        //все ребра
        public static List<Edge> E;
        //массив всех маршрутов
        static public SerializableDictionary<string, List<Vertex>> routes;
        // лист номеров квадратов, в которм есть светофор
        static public List<int> TraficLightsInGrids;
        //Светофоры
        public static List<TraficLight> traficLights;
        // словарь номеров квадратов, в которм есть остановка для каждого маршрута
        public static SerializableDictionary<string, List<int>> stopPointsInGrids;
        //Остановки маршрутов
        public static List<BusStop> allstopPoints;
        public static SerializableDictionary<string, List<BusStop>> stopPoints;
        //все координаты движения автобусов
        public static SerializableDictionary<string, List<Point>> AllCoordinates;
        //все квадраты сетки, которые есть в каждом из маршрутов 
        public static SerializableDictionary<string, List<int>> AllGridsInRoutes { get; set; }
    }
}
