//This is a personal academic project. Dear PVS-Studio, please check it.
//PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using SystAnalys_lr1.Forms;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystAnalys_lr1.Classes;

namespace SystAnalys_lr1.Classes
{
    static class Modeling
    {

        private static Dictionary<string, List<GridPart>> s_pollutionInRoutes;
        private static List<Epicenter> epList;
        private static int s_t;
        private static List<int?> s_resultFromModeling = new List<int?>();

        public static Dictionary<string, List<GridPart>> PollutionInRoutes { get => s_pollutionInRoutes; set => s_pollutionInRoutes = value; }
        public static int T { get => s_t; set => s_t = value; }
        public static List<int?> ResultFromModeling { get => s_resultFromModeling; set => s_resultFromModeling = value; }
        public static List<Epicenter> EpList { get => epList; set => epList = value; } 


        public static async void GetEpicentersAsync(List<Epicenter> epList, int i)
        {
            foreach (var EpicList in Data.Epics)
            {
                await GetTask(epList, EpicList, i);
            };
        }

        public static Task GetTask(List<Epicenter> epList, Epicenter EpicList, int i)
        {
            return Task.Run(() =>
            {
                epList.Add(new Epicenter(Data.TheGrid));
                foreach (var Sector in EpicList.EpicenterGrid)
                {
                    epList[i].EpicenterGrid.Add(Sector.Key, new List<GridPart>());
                    epList[i].StartPositon = EpicList.StartPositon;
                    epList[i].NewExpandCount = new List<int>();
                    foreach (var Square in Sector.Value)
                    {
                        epList[i].EpicenterGrid[Sector.Key].Add(new GridPart(Square.X, Square.Y));
                    }
                }
                i++;
            });
        }


        public static void CreatePollutionInRoutes()
        {

            PollutionInRoutes = new Dictionary<string, List<GridPart>>();
            for (int i = 0; i < Data.AllCoordinates.Count; i++)
            {
                PollutionInRoutes.Add(Data.AllCoordinates.ElementAt(i).Key, new List<GridPart>());
                foreach (var Grid in Data.TheGrid)
                {
                    PollutionInRoutes[PollutionInRoutes.ElementAt(i).Key].Add(new GridPart(Grid.X, Grid.Y));
                }
            }

        }


        public static void StartModeling(string SavePath, int Cicle, int ModelNum)
        {
            EpList = new List<Epicenter>();
            int i = 0;

            ConcurrentQueue<Bus> cqBus = new ConcurrentQueue<Bus>();
            Data.Buses.ForEach((b) => cqBus.Enqueue((Bus)b.Clone()));
            GetEpicentersAsync(EpList, i);

            int small = 10000;
            int old = small;
            int FoundTime = small + 1;

            int ExpandTimer = 0;
            int MovingTimer = 0;
            i = 1;
            int PhaseSizeSelect()
            {
                if (EpicSettings.ExtendedSavePictures == false)
                {
                    return 1;
                }
                else
                {
                    return EpicSettings.EpicPhaseSavingParam;
                }
            }

            
            int MaxEpicItnerValue;
            int MaxEpicItnerCycleValue;
            if (EpicSettings.TurnExpandingSet == false && EpicSettings.TurnMovingSet == false)
            {
                MaxEpicItnerValue = 1;
                MaxEpicItnerCycleValue = T / PhaseSizeSelect() / MaxEpicItnerValue;
            }
            else
            {
                if (EpicSettings.EpicFreqMovingParam < EpicSettings.EpicFreqSpreadingParam)
                {
                    MaxEpicItnerValue = EpicSettings.EpicFreqMovingParam / 20;
                    MaxEpicItnerCycleValue = T / PhaseSizeSelect() / MaxEpicItnerValue;
                    if (MaxEpicItnerCycleValue == 0)
                    {
                        MaxEpicItnerCycleValue = 1;
                    }
                }
                else
                {
                    MaxEpicItnerValue = EpicSettings.EpicFreqSpreadingParam / 20;
                    MaxEpicItnerCycleValue = T / PhaseSizeSelect() / MaxEpicItnerValue;
                    if (MaxEpicItnerCycleValue == 0)
                    {
                        MaxEpicItnerCycleValue = 1;
                    }
                }
            }
            bool EpicFounded = false;
            for (int j = PhaseSizeSelect(); j > 0; j--)
            {
                CreatePollutionInRoutes();

                if (j == PhaseSizeSelect())
                {
                    if ((EpicSettings.SavePictures == true) && (EpicSettings.ExtendedSavePictures == true))
                    {
                        Directory.CreateDirectory(SavePath + "/Epics" + "/" + (Cicle + 1).ToString() + "/" + (ModelNum + 1).ToString() + "/" + 0.ToString());
                        lock (Main.Ep.Esheet)
                        {
                            Main.Ep.EDrawEpics(EpList);
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

                for (int k = 0; k < MaxEpicItnerCycleValue; k++)
                {
                    if (T / PhaseSizeSelect() / MaxEpicItnerValue != 0)
                    {

                        if (EpicSettings.TurnExpandingSet == true)
                        {

                            ExpandTimer += MaxEpicItnerValue;
                        }

                        if (EpicSettings.TurnMovingSet == true)
                        {
                            MovingTimer += MaxEpicItnerValue;
                        }
                        if (EpicSettings.TurnMovingSet == true)
                        {
                            if (MovingTimer >= ((EpicSettings.EpicFreqMovingParam / 20)))
                            {
                                lock (EpList)
                                {
                                    MoveEpics(EpList);
                                }
                                MovingTimer = 0;
                            }
                        }
                        if (EpicSettings.TurnExpandingSet == true)
                        {
                            if (ExpandTimer >= ((EpicSettings.EpicFreqSpreadingParam / 20)))
                            {
                                lock (EpList)
                                {
                                    ExpandEpics(EpList);
                                }
                                ExpandTimer = 0;
                            }
                        }
                    }
                    Parallel.ForEach(cqBus, new ParallelOptions { MaxDegreeOfParallelism = 5 }, (bus) =>
                    {
                        bus.Epicenters = EpList;

                        bus.TickCount_ = 0;
                        if (bus.Skips.SkipTrafficLights > 0)
                            bus.Skips.SkipTrafficLights -= 1;
                        if (bus.Tracker == true)
                        {
                            while (bus.TickCount_ < (T / PhaseSizeSelect() / (MaxEpicItnerCycleValue)))
                            {
                                bus.MoveWithoutGraphicsByGrids();

                                if (Data.TraficLightsInGrids.Contains(Data.AllGridsInRoutes[bus.GetRoute()][(int)bus.PositionAt]))
                                {
                                    if (bus.Skips.SkipTrafficLights == 0)
                                    {
                                        foreach (var sp in Data.TraficLights)
                                        {
                                            if (sp.Status != LightStatus.RED)
                                            {
                                                bus.Skips.SkipTrafficLights = sp.GreenTime;
                                                break;
                                            }
                                            if (sp.Status == LightStatus.GREEN || sp.Status == LightStatus.YELLOW)
                                            {
                                                bus.TickCount_ += sp.Bal;
                                                bus.AllTickCount += sp.Bal;
                                                bus.Skips.SkipTrafficLights = sp.GreenTime;
                                                break;

                                            }
                                        }
                                    }
                                }

                                if ((Data.StopPointsInGrids.ContainsKey(bus.GetRoute())) && (Data.StopPointsInGrids[bus.GetRoute()].Contains(Data.AllGridsInRoutes[bus.GetRoute()][(int)bus.PositionAt])))
                                {
                                    Random rnd = new Random();
                                    int timeboost = rnd.Next(0, 3);
                                    bus.TickCount_ += timeboost;
                                    bus.AllTickCount += timeboost;
                                }


                                if (bus.StopAtStationByGrid == true)
                                {
                                    Random rnd = new Random();
                                    int timeboost = rnd.Next(0, 3);
                                    bus.TickCount_ += timeboost;
                                    bus.AllTickCount += timeboost;
                                    bus.StopAtStationByGrid = false;
                                }

                                PollutionInRoutes[bus.GetRoute()][Data.AllGridsInRoutes[bus.GetRoute()][(int)bus.PositionAt]].Status = bus.DetectEpicenterByGrid();
                                Parallel.ForEach(bus.Epicenters, new ParallelOptions { MaxDegreeOfParallelism = 2 }, (Epic, state) =>
                                    {
                                        if (Epic.DetectCount >= Epic.EpicenterGrid[1].Count / 2)
                                        {
                                            if (EpicFounded == false)
                                            {
                                                EpicFounded = true;
                                                if (EpicFounded == true)
                                                {
                                                    FoundTime = bus.AllTickCount;
                                                    if (small > FoundTime)
                                                    {
                                                        small = FoundTime;
                                                    }
                                                }
                                            }
                                            state.Break();
                                        }
                                    });
                                bus.TickCount_++;
                                bus.AllTickCount++;
                            }
                        }

                    });
                }
                if ((EpicSettings.SavePictures == true) && (EpicSettings.ExtendedSavePictures == true))
                {
                    Directory.CreateDirectory(SavePath + "/Epics" + "/" + (Cicle + 1).ToString() + "/" + (ModelNum + 1).ToString() + "/" + i.ToString());
                    lock (Main.Ep.Esheet)
                    {
                        Main.Ep.EDrawEpics(EpList);
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
                    ResultFromModeling.Add(small * 20); 
                }
                else
                {
                    ResultFromModeling.Add(small * 20);
                }
            }

        }

        private static void MoveEpics(List<Epicenter> Epics)
        {
            if (EpicSettings.MovingEpicParamet.Count > 0)
            {
                Epics.First().EpicMoving(EpicSettings.MovingEpicParamet);
            }
        }

        private static void ExpandEpics(List<Epicenter> Epics)
        {
            Epics.First().ExpandEpic();
        }

    }

}
