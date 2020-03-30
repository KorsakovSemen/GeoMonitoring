using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystAnalys_lr1
{
    class AllRotations
    {
        static List<Point> AllRotationsPoints = new List<Point>();
        static List<Vertex> route23 = new List<Vertex>();
        static List<Vertex> route7 = new List<Vertex>();
        static List<Vertex> route62 = new List<Vertex>();
        static List<Vertex> route43 = new List<Vertex>();
        static List<Vertex> route20 = new List<Vertex>();
    
        static List<Vertex> route107 = new List<Vertex>();
        static List<Vertex> stop = new List<Vertex>();


        public static List<Vertex> GetRoute20()
        {
            route20.Add(new Vertex(AllRotationsPoints[51].X, AllRotationsPoints[51].Y));
            route20.Add(new Vertex(AllRotationsPoints[52].X, AllRotationsPoints[52].Y));
            route20.Add(route23[10]);
            route20.Add(route23[9]);
            route20.Add(route23[8]);
            route20.Add(route23[4]);
            route20.Add(route23[5]);
            route20.Add(route23[6]);
            route20.Add(route23[13]);
            route20.Add(route23[14]);
            route20.Add(route23[15]);
            route20.Add(route23[16]);
            route20.Add(route23[17]);
            route20.Add(route23[18]);
            route20.Add(route23[19]);
            route20.Add(route23[20]);
            route20.Add(route23[21]);
            route20.Add(route23[22]);
            route20.Add(route7[2]);
            route20.Add(route7[1]);
            route20.Add(route62[16]);
            route20.Add(route7[17]);
            route20.Add(route7[18]);
            route20.Add(route7[19]);
            route20.Add(route7[20]);
            route20.Add(route7[21]);
            route20.Add(route7[22]);
            return route20;
        }

        public static List<Vertex> GetRoute43()
        {
            route43.Add(route23[0]);
            route43.Add(route23[1]);
            route43.Add(route23[2]);
            route43.Add(route23[3]);
            route43.Add(route23[4]);
            route43.Add(route23[8]);
            route43.Add(route23[9]);
            route43.Add(route23[10]);
            route43.Add(route23[11]);
            route43.Add(route23[12]);
            route43.Add(route23[13]);
            route43.Add(route23[14]);
            route43.Add(route23[15]);
            route43.Add(route23[16]);
            route43.Add(route23[17]);
            route43.Add(route23[18]);
            route43.Add(route23[19]);
            route43.Add(route23[20]);
            route43.Add(route23[21]);
            route43.Add(route23[22]);
            route43.Add(route23[23]);
            route43.Add(route23[24]);
            route43.Add(new Vertex(AllRotationsPoints[48].X, AllRotationsPoints[48].Y));
            route43.Add(route7[17]);
            route43.Add(route7[18]);
            route43.Add(route7[19]);
            route43.Add(route7[20]);
            route43.Add(new Vertex(AllRotationsPoints[49].X, AllRotationsPoints[49].Y));
            route43.Add(route7[20]);
            route43.Add(route23[34]);
            route43.Add(route23[35]);
            return route43;
        }

        public static List<Vertex> GetRoute107()
        {
       
            
            return route107;
        }


        public static List<Vertex> GetRoute62()
        {
            route62.Add(route23[35]);
            route62.Add(route23[34]);
            route62.Add(route23[33]);
            route62.Add(route23[32]);
            route62.Add(route23[31]);
            route62.Add(route23[30]);
            route62.Add(route23[29]);
            route62.Add(route23[28]);
            route62.Add(route23[27]);
            route62.Add(route23[26]);
            route62.Add(route23[25]);
            route62.Add(route23[24]);
            route62.Add(route23[23]);
            route62.Add(route23[22]);
            route62.Add(route7[2]);
            route62.Add(route7[1]);
            route62.Add(new Vertex(AllRotationsPoints[50].X, AllRotationsPoints[50].Y));
            route62.Add(route7[17]);
            route62.Add(route23[33]);
            route62.Add(route23[34]);
            route62.Add(route62[0]);
            return route62;    
        }      

        private static Vertex CallRotationPoint(List<Point> AllRotationsPoints, int x, int y)
        {
            return new Vertex(AllRotationsPoints[AllRotationsPoints.IndexOf(new Point(x, y))].X, AllRotationsPoints[AllRotationsPoints.IndexOf(new Point(x, y))].Y);
        }


        public static List<Vertex> GetStop()
        {
            //stop.Add(new Vertex(AllRotationsPoints[0].X, AllRotationsPoints[0].Y));
            //stop.Add(new Vertex(AllRotationsPoints[AllRotationsPoints.IndexOf(new Point(164, 620))].X, AllRotationsPoints[AllRotationsPoints.IndexOf(new Point(164, 620))].Y));
            //stop.Add(new Vertex(AllRotationsPoints[0].X, AllRotationsPoints[0].Y));
            //stop.Add(new Vertex(AllRotationsPoints[AllRotationsPoints.IndexOf(new Point(179, 563))].X, AllRotationsPoints[AllRotationsPoints.IndexOf(new Point(179, 563))].Y));
            //stop.Add(new Vertex(AllRotationsPoints[0].X, AllRotationsPoints[0].Y));
            //stop.Add(new Vertex(AllRotationsPoints[AllRotationsPoints.IndexOf(new Point(213, 484))].X, AllRotationsPoints[AllRotationsPoints.IndexOf(new Point(213, 484))].Y));
            //stop.Add(route7[1]);
            //stop.Add(new Vertex(AllRotationsPoints[0].X, AllRotationsPoints[0].Y));
            //stop.Add(route7[3]);
            ////stop.Add(new Vertex(AllRotationsPoints[0].X, AllRotationsPoints[0].Y));
            ////stop.Add(route7[5]);
            ////stop.Add(new Vertex(AllRotationsPoints[0].X, AllRotationsPoints[0].Y));
            ////stop.Add(route7[7]);
            ////stop.Add(new Vertex(AllRotationsPoints[0].X, AllRotationsPoints[0].Y));
            ////stop.Add(route7[9]);
            //stop.Add(new Vertex(AllRotationsPoints[0].X, AllRotationsPoints[0].Y));
            //stop.Add(new Vertex(AllRotationsPoints[16].X, AllRotationsPoints[16].Y));
            //stop.Add(new Vertex(AllRotationsPoints[0].X, AllRotationsPoints[0].Y));
            //stop.Add(new Vertex(AllRotationsPoints[18].X, AllRotationsPoints[18].Y));
            ////stop.Add(new Vertex(AllRotationsPoints[0].X, AllRotationsPoints[0].Y));
            ////stop.Add(new Vertex(AllRotationsPoints[20].X, AllRotationsPoints[20].Y));

            return stop;
        }

        public static List<Vertex> GetRoute7()
        {
            route7.Add(new Vertex(AllRotationsPoints[0].X, AllRotationsPoints[0].Y));
            route7.Add(new Vertex(AllRotationsPoints[1].X, AllRotationsPoints[1].Y));
            route7.Add(new Vertex(AllRotationsPoints[2].X, AllRotationsPoints[2].Y));
            route7.Add(new Vertex(AllRotationsPoints[3].X, AllRotationsPoints[3].Y));
            route7.Add(new Vertex(AllRotationsPoints[4].X, AllRotationsPoints[4].Y));
            route7.Add(new Vertex(AllRotationsPoints[5].X, AllRotationsPoints[5].Y));
            route7.Add(new Vertex(AllRotationsPoints[6].X, AllRotationsPoints[6].Y));
            route7.Add(new Vertex(AllRotationsPoints[7].X, AllRotationsPoints[7].Y));
            route7.Add(new Vertex(AllRotationsPoints[8].X, AllRotationsPoints[8].Y));
            route7.Add(new Vertex(AllRotationsPoints[9].X, AllRotationsPoints[9].Y));
            route7.Add(new Vertex(AllRotationsPoints[10].X, AllRotationsPoints[10].Y));
            route7.Add(new Vertex(AllRotationsPoints[11].X, AllRotationsPoints[11].Y));
            route7.Add(new Vertex(AllRotationsPoints[12].X, AllRotationsPoints[12].Y));
            route7.Add(new Vertex(AllRotationsPoints[13].X, AllRotationsPoints[13].Y));
            route7.Add(new Vertex(AllRotationsPoints[14].X, AllRotationsPoints[14].Y));
            route7.Add(new Vertex(AllRotationsPoints[15].X, AllRotationsPoints[15].Y));
            route7.Add(new Vertex(AllRotationsPoints[16].X, AllRotationsPoints[16].Y));
            route7.Add(new Vertex(AllRotationsPoints[17].X, AllRotationsPoints[17].Y));
            route7.Add(new Vertex(AllRotationsPoints[18].X, AllRotationsPoints[18].Y));
            route7.Add(new Vertex(AllRotationsPoints[19].X, AllRotationsPoints[19].Y));
            route7.Add(new Vertex(AllRotationsPoints[20].X, AllRotationsPoints[20].Y));
            route7.Add(new Vertex(AllRotationsPoints[21].X, AllRotationsPoints[21].Y));
            route7.Add(new Vertex(AllRotationsPoints[22].X, AllRotationsPoints[22].Y));



            return route7;
        }

        public static List<Vertex> GetRoute23()
        {
            route23.Add(new Vertex(AllRotationsPoints[23].X, AllRotationsPoints[23].Y));
            route23.Add(new Vertex(AllRotationsPoints[24].X, AllRotationsPoints[24].Y));
            route23.Add(new Vertex(AllRotationsPoints[25].X, AllRotationsPoints[25].Y));
            route23.Add(new Vertex(AllRotationsPoints[26].X, AllRotationsPoints[26].Y));
            route23.Add(new Vertex(AllRotationsPoints[27].X, AllRotationsPoints[27].Y));
            route23.Add(new Vertex(AllRotationsPoints[28].X, AllRotationsPoints[28].Y));
            route23.Add(new Vertex(AllRotationsPoints[29].X, AllRotationsPoints[29].Y));
            route23.Add(new Vertex(AllRotationsPoints[30].X, AllRotationsPoints[30].Y));
            route23.Add(new Vertex(AllRotationsPoints[31].X, AllRotationsPoints[31].Y));
            route23.Add(new Vertex(AllRotationsPoints[32].X, AllRotationsPoints[32].Y));
            route23.Add(new Vertex(AllRotationsPoints[33].X, AllRotationsPoints[33].Y));
            route23.Add(new Vertex(AllRotationsPoints[34].X, AllRotationsPoints[34].Y));
            route23.Add(new Vertex(AllRotationsPoints[35].X, AllRotationsPoints[35].Y));
            route23.Add(new Vertex(AllRotationsPoints[29].X, AllRotationsPoints[29].Y));
            route23.Add(new Vertex(AllRotationsPoints[36].X, AllRotationsPoints[36].Y));
            route23.Add(new Vertex(AllRotationsPoints[37].X, AllRotationsPoints[37].Y));
            route23.Add(new Vertex(AllRotationsPoints[38].X, AllRotationsPoints[38].Y));
            route23.Add(new Vertex(AllRotationsPoints[39].X, AllRotationsPoints[39].Y));
            route23.Add(new Vertex(AllRotationsPoints[40].X, AllRotationsPoints[40].Y));
            route23.Add(new Vertex(AllRotationsPoints[41].X, AllRotationsPoints[41].Y));
            route23.Add(new Vertex(AllRotationsPoints[42].X, AllRotationsPoints[42].Y));
            route23.Add(new Vertex(AllRotationsPoints[43].X, AllRotationsPoints[43].Y));
            route23.Add(new Vertex(AllRotationsPoints[44].X, AllRotationsPoints[44].Y));
            route23.Add(route7[3]);
            route23.Add(route7[4]);
            route23.Add(route7[5]);
            route23.Add(route7[6]);
            route23.Add(route7[7]);
            route23.Add(route7[8]);
            route23.Add(route7[9]);
            route23.Add(route7[10]);
            route23.Add(route7[11]);
            route23.Add(route7[16]);
            route23.Add(new Vertex(AllRotationsPoints[45].X, AllRotationsPoints[45].Y));
            route23.Add(new Vertex(AllRotationsPoints[46].X, AllRotationsPoints[46].Y));
            route23.Add(new Vertex(AllRotationsPoints[47].X, AllRotationsPoints[47].Y));


            return route23;
        }

        public static List<Point> GetAllRotationsPoints()
        {
            AllRotationsPoints.Add(new Point(1113, 520));
            AllRotationsPoints.Add(new Point(1083, 526));
            AllRotationsPoints.Add(new Point(1000, 563));
            AllRotationsPoints.Add(new Point(1022, 630));
            AllRotationsPoints.Add(new Point(1147, 612));
            AllRotationsPoints.Add(new Point(1190, 704));
            AllRotationsPoints.Add(new Point(1223, 712));
            AllRotationsPoints.Add(new Point(1278, 673));
            AllRotationsPoints.Add(new Point(1323, 743));
            AllRotationsPoints.Add(new Point(1425, 665));
            AllRotationsPoints.Add(new Point(1419, 638));
            AllRotationsPoints.Add(new Point(1413, 600));
            AllRotationsPoints.Add(new Point(1358, 538));
            AllRotationsPoints.Add(new Point(1416, 503));
            AllRotationsPoints.Add(new Point(1383, 468));
            AllRotationsPoints.Add(new Point(1337, 505));
            AllRotationsPoints.Add(new Point(1318, 481));
            AllRotationsPoints.Add(new Point(1233, 480));

            AllRotationsPoints.Add(new Point(1237, 380));
            AllRotationsPoints.Add(new Point(1295, 380));
            AllRotationsPoints.Add(new Point(1319, 403));
            AllRotationsPoints.Add(new Point(1414, 347));
            AllRotationsPoints.Add(new Point(1459, 347));

            AllRotationsPoints.Add(new Point(436, 386));
            AllRotationsPoints.Add(new Point(484, 398));
            AllRotationsPoints.Add(new Point(481, 410));
            AllRotationsPoints.Add(new Point(593, 447));
            AllRotationsPoints.Add(new Point(616, 462));
            AllRotationsPoints.Add(new Point(603, 486));
            AllRotationsPoints.Add(new Point(642, 513));
            AllRotationsPoints.Add(new Point(648, 443));
            AllRotationsPoints.Add(new Point(632, 441));
            AllRotationsPoints.Add(new Point(640, 420));
            AllRotationsPoints.Add(new Point(717, 424));
            AllRotationsPoints.Add(new Point(716, 481));
            AllRotationsPoints.Add(new Point(668, 531));
           // AllRotationsPoints.Add(new Point(642, 513));
            AllRotationsPoints.Add(new Point(618, 551));
            AllRotationsPoints.Add(new Point(640, 571));
            AllRotationsPoints.Add(new Point(668, 556));
            AllRotationsPoints.Add(new Point(784, 655));
            AllRotationsPoints.Add(new Point(843, 673));
            AllRotationsPoints.Add(new Point(983, 661));
            AllRotationsPoints.Add(new Point(985, 637));
            AllRotationsPoints.Add(new Point(982, 603));
            AllRotationsPoints.Add(new Point(1007, 593));
            AllRotationsPoints.Add(new Point(1345, 489));
            AllRotationsPoints.Add(new Point(1348, 447));
            AllRotationsPoints.Add(new Point(1416, 442));
            AllRotationsPoints.Add(new Point(1216, 485));
            AllRotationsPoints.Add(new Point(1384, 376));
            AllRotationsPoints.Add(new Point(1172, 486));

            AllRotationsPoints.Add(new Point(633, 0));//51
            AllRotationsPoints.Add(new Point(718, 96));
            return AllRotationsPoints;
        }

    }
}