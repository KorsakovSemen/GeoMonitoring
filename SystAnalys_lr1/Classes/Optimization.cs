using SystAnalys_lr1.Forms;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystAnalys_lr1.Classes
{
    static class Optimization
    {
        static public Dictionary<string, List<GridPart>> PollutionInRoutes;
        static public int T;
        static public List<int?> ResultFromModeling = new List<int?>();

        public static void CreatePollutionInRoutes()
        {
            PollutionInRoutes = new Dictionary<string, List<GridPart>>();
            for (int i = 0; i < Main.AllCoordinates.Count; i++)
            {
                PollutionInRoutes.Add(Main.AllCoordinates.ElementAt(i).Key, new List<GridPart>());
                foreach (var Grid in Main.TheGrid)
                {
                    PollutionInRoutes[PollutionInRoutes.ElementAt(i).Key].Add(new GridPart(Grid.x, Grid.y));
                }
            }
        }






        public static void Modeling(string SavePath, int Cicle, int ModelNum)
        {
            List<Epicenter> epList = new List<Epicenter>();
            int i = 0;
            ConcurrentQueue<Bus> cqBus = new ConcurrentQueue<Bus>();
            Main.buses.ForEach((b) => cqBus.Enqueue((Bus)b.Clone()));
            foreach (var EpicList in Main.Epics)
            {

                epList.Add(new Epicenter(Main.TheGrid));
                foreach (var Sector in EpicList.GetEpicenterGrid())
                {
                    epList[i].EpicenterGrid.Add(Sector.Key, new List<GridPart>());
                    epList[i].StartPositon = EpicList.StartPositon;
                    epList[i].NewExpandCount = new List<int>();
                    foreach (var Square in Sector.Value)
                    {
                        epList[i].EpicenterGrid[Sector.Key].Add(new GridPart(Square.x, Square.y));
                    }
                }
                i++;
            }
            CreatePollutionInRoutes();
            int small = 10000;
            int old = small;
            int FoundTime = small + 1;

            int ExpandTimer = 0;
            int MovingTimer = 0;
            i = 1;
            int PhaseSizeSelect()
            {
                if (Main.extendedSavePictures == false)
                {
                    return 1;
                }
                else
                {
                    return Main.EpicPhaseSavingParam;
                }

            }
            foreach (var bus in cqBus)
            {
                bus.AllTickCount = 0;

            }
            bool EpicFounded = false;
            for (int j = PhaseSizeSelect(); j > 0; j--)
            {
                ////
                if (j == PhaseSizeSelect())
                {
                    if ((Main.SavePictures == true) && (Main.extendedSavePictures == true))
                    {
                        Directory.CreateDirectory(SavePath + "/Epics" + "/" + (Cicle + 1).ToString() + "/" + (ModelNum + 1).ToString() + "/" + 0.ToString());
                        lock (Main.Ep.Esheet)
                        {
                            Main.Ep.EDrawEpics(epList);
                        }
                        lock (Main.Ep.Esheet)
                        {
                            using (System.Drawing.Image img = (Image)Main.Ep.Esheet.Image.Clone())
                            {
                                img.Save(SavePath + "/Epics" + "/" + (Cicle + 1).ToString() + "/" + (ModelNum + 1).ToString() + "/" + 0.ToString() + "/" + 0.ToString() + "_nat" + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                            }
                        }
                    }
                }
                ////

                foreach (var bus in cqBus)
                {
                    bus.Epicenters = epList;

                    bus.TickCount_ = 0;
                    if (bus.skip > 0)
                        bus.skip -= 1;
                    if (bus.Tracker == true)
                    {
                        while (bus.TickCount_ < (T / PhaseSizeSelect()))
                        {
                            bus.MoveWithoutGraphicsByGrids();
                            if (EpicSettings.TurnMovingSet == true)
                            {
                                if (MovingTimer >= ((Main.EpicFreqMovingParam / 20) * cqBus.Count))
                                {
                                    lock (epList)
                                    {
                                        MoveEpics(epList);
                                    }
                                    MovingTimer = 0;
                                }
                            }
                            if (EpicSettings.TurnSpreadingSet == true)
                            {
                                if (ExpandTimer >= ((Main.EpicFreqSpreadingParam / 20) * cqBus.Count))
                                {
                                    lock (epList)
                                    {
                                        ExpandEpics(epList);
                                    }
                                    ExpandTimer = 0;
                                }
                            }
                            if (Main.TraficLightsInGrids.Contains(Main.AllGridsInRoutes[bus.GetRoute()][(int)bus.PositionAt])) //ошибка с выходом за пределы тушто нужно "вот эту" разкоментить
                            {
                                if (bus.skip == 0)
                                {
                                    foreach (var sp in Main.traficLights)
                                    {
                                        if (sp.Status != Status.RED)
                                        {
                                            bus.skip = sp.greenTime;
                                            break;
                                        }
                                        if (sp.Status == Status.RED)
                                        {
                                            bus.TickCount_ = bus.TickCount_ + sp.bal;
                                            bus.AllTickCount = bus.AllTickCount + sp.bal;
                                            bus.skip = sp.greenTime;
                                            break;

                                        }
                                    }
                                }
                            }

                            if ((Main.stopPointsInGrids.ContainsKey(bus.GetRoute())) && (Main.stopPointsInGrids[bus.GetRoute()].Contains(Main.AllGridsInRoutes[bus.GetRoute()][(int)bus.PositionAt])))
                            {
                                Random rnd = new Random();
                                int timeboost = rnd.Next(0, 3);
                                bus.TickCount_ = bus.TickCount_ + timeboost;
                                bus.AllTickCount = bus.AllTickCount + timeboost;
                            }

                            PollutionInRoutes[bus.GetRoute()][Main.AllGridsInRoutes[bus.GetRoute()][(int)bus.PositionAt]].Status = bus.DetectEpicenterByGrid(); // ошибка

                            foreach (var Epic in bus.Epicenters)
                            {
                                if (Epic.DetectCount >= Epic.getEpicenterGrid()[1].Count / 2)
                                {
                                    if (EpicFounded == false)
                                    {
                                        EpicFounded = true;
                                        if (EpicFounded == true)
                                        {
                                            //  FoundTime = (T - (bus.TickCount_ * j));
                                            FoundTime = bus.AllTickCount;
                                            if (small > FoundTime)
                                            {
                                                small = FoundTime;
                                            }
                                        }
                                    }
                                    break;
                                }
                            }
                            //  bus.TickCount_--;
                            bus.TickCount_++;
                            bus.AllTickCount++;
                            if (EpicSettings.TurnSpreadingSet == true)
                            {
                                ExpandTimer++;
                            }
                            if (EpicSettings.TurnMovingSet == true)
                            {
                                MovingTimer++;
                            }
                        }
                    }
                }

                if ((Main.SavePictures == true) && (Main.extendedSavePictures == true))
                {
                    Directory.CreateDirectory(SavePath + "/Epics" + "/" + (Cicle + 1).ToString() + "/" + (ModelNum + 1).ToString() + "/" + i.ToString());
                    lock (Main.Ep.Esheet)
                    {
                        Main.Ep.EDrawEpics(epList);
                    }
                    lock (Main.Ep.Esheet)
                    {
                        using (System.Drawing.Image img = (Image)Main.Ep.Esheet.Image.Clone())
                        {
                            img.Save(SavePath + "/Epics" + "/" + (Cicle + 1).ToString() + "/" + (ModelNum + 1).ToString() + "/" + i.ToString() + "/" + i.ToString() + "_nat" + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                        }
                    }

                    lock (Main.Ep.Esheet)
                    {
                        Main.Ep.RecReateFunction();
                    }
                    lock (Main.Ep.Esheet)
                    {
                        using (System.Drawing.Image img = (Image)Main.Ep.Esheet.Image.Clone())
                        {
                            img.Save(SavePath + "/Epics" + "/" + (Cicle + 1).ToString() + "/" + (ModelNum + 1).ToString() + "/" + i.ToString() + "/" + i.ToString() + "_re" + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                        }
                    }
                    i++;
                }

            }
            if (small == old)
                ResultFromModeling.Add(null);
            else
            {
                if (small == 0)
                {
                    small += 1;
                    ResultFromModeling.Add(small * 20); // small в мин или секах
                }
                else
                {
                    ResultFromModeling.Add(small * 20);
                }
            }

        }
        //
        private static void MoveEpics(List<Epicenter> Epics)
        {
            if (Main.MovingEpicParamet.Count > 0)
            {
                Epics.First().EpicMoving(Main.MovingEpicParamet);
            }
        }
        //
        private static void ExpandEpics(List<Epicenter> Epics)
        {
            Epics.First().ExpandEpic();
        }
        //
    }

}
