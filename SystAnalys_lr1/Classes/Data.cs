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
        //Лист всех эпицентров
        public static List<Epicenter> Epics { get; set; } = new List<Epicenter>();
        //Лист, в котором хранится сетка
        static public List<GridPart> TheGrid { get; set; } = new List<GridPart>();
        //Лист, в котором хранятся автобусы
        static public List<Bus> Buses { get; set; } = new List<Bus>();
        public static List<List<Bus>> BusesPark { get; set; } = new List<List<Bus>>();
        //все вершины
        static public List<Vertex> V { get; set; } = new List<Vertex>();
        //ребра маршрутов
        static public SerializableDictionary<string, List<Edge>> RoutesEdge { get; set; } = new SerializableDictionary<string, List<Edge>>();
        //все ребра
        public static List<Edge> E { get; set; } = new List<Edge>();
        //массив всех маршрутов
        static public SerializableDictionary<string, List<Vertex>> Routes { get; set; } = new SerializableDictionary<string, List<Vertex>>();
        // лист номеров квадратов, в которм есть светофор
        static public List<int> TraficLightsInGrids { get; set; } = new List<int>();
        //Светофоры
        public static List<TraficLight> TraficLights { get; set; } = new List<TraficLight>();
        // словарь номеров квадратов, в которм есть остановка для каждого маршрута
        public static SerializableDictionary<string, List<int>> StopPointsInGrids { get; set; } = new SerializableDictionary<string, List<int>>();
        //Остановки маршрутов
        public static List<BusStop> AllstopPoints { get; set; } = new List<BusStop>();
        public static SerializableDictionary<string, List<BusStop>> StopPoints { get; set; } = new SerializableDictionary<string, List<BusStop>>();
        //все координаты движения автобусов
        public static SerializableDictionary<string, List<Point>> AllCoordinates { get; set; } = new SerializableDictionary<string, List<Point>>();
        //все квадраты сетки, которые есть в каждом из маршрутов 
        public static SerializableDictionary<string, List<int>> AllGridsInRoutes { get; set; } = new SerializableDictionary<string, List<int>>();

        
    }
}
