using MetroFramework.Components;
using MetroFramework.Controls;
using SystAnalys_lr1.Forms;
using SystAnalys_lr1.Strings;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace SystAnalys_lr1.Classes
{
    static class Optimization
    {
        static public string pathOpt;
        static public SerializableDictionary<int, int?> percentMean;
        static int small = 10000;
        static public int countWithoutSensors;
        static public List<int> withoutSensorsBuses = new List<int>();
        delegate void DelInt(int text);
        static Random rnd = new Random();
        static string mean;
        public static int OptiSpeed;
        public static int OptiCount;

        //class opt
        public static void ResMatrix(MetroGrid results)
        {
            results.Rows.Clear();
            results.Refresh();
            results.RowCount = 5;
            int i = 0;
            foreach (var pm in Optimization.percentMean)
            {
                results.Rows[i].HeaderCell.Value = pm.Key.ToString();
                if (pm.Value != 0)
                    results.Rows[i].Cells[0].Value = (pm.Value / 60).ToString();
                else
                    results.Rows[i].Cells[0].Value = MainStrings.notFound;
                i += 1;
            }
        }
        //class opt
        public static void ResChart(int oldChart, Report r, MetroStyleManager StyleManager)
        {
            bool changeText = false;
            if (oldChart != percentMean.Keys.Sum())
            {
                r.ch.Legends.Clear();
                Main.ReportCount = 0;
                foreach (var series in r.ch.Series)
                {
                    series.Points.Clear();
                }
                oldChart = percentMean.Keys.Sum();
                changeText = true;
            }
            int iCh = 0;
            StyleManager.Clone(r);
            if (Main.ReportCount != 0)
                r.ch.Series.Add(Main.ReportCount.ToString());
            r.ch.Series[Main.ReportCount].LegendText = Main.ReportCount.ToString();
            foreach (var pm in percentMean)
            {
                if (pm.Value == null)
                {
                    r.ch.Series[Main.ReportCount].Points.AddY(0);
                }
                else
                {
                    r.ch.Series[Main.ReportCount].Points.AddY(pm.Value / 60 != 0 ? (double)pm.Value / 60 : (double)pm.Value);
                }
                if (!changeText)
                    r.ch.ChartAreas[0].AxisX.CustomLabels.Add(new CustomLabel(iCh, iCh + 2, pm.Key.ToString(), 0, LabelMarkStyle.LineSideMark));
                else
                    r.ch.ChartAreas[0].AxisX.CustomLabels[iCh].Text = pm.Key.ToString();
                iCh++;
            }
            r.ch.SaveImage(pathOpt + "/" + MainStrings.chart + ".jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
            r.TopMost = true;
            r.Show();
            r.BringToFront();

            Main.ReportCount += 1;
           
        }

        public static void Opt(MatrixControl matrixControl1, LoadingForm loadingForm)
        {
            pathOpt = "../../Results/" + string.Format("{0}_{1}_{2}_{3}_{4}_{5}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            Directory.CreateDirectory(pathOpt);
            percentMean = new SerializableDictionary<int, int?>();

            List<Bus> optimizeBuses = new List<Bus>();
            Data.Buses.ForEach((b) => optimizeBuses.Add(
                (Bus)b.Clone()
            ));
            int old = small;


            int ciclTotal = 5;

            loadingForm.loading.Invoke(new DelInt((s) => loadingForm.loading.Maximum = s), ciclTotal * OptiCount);
            for (int cicl = 0; cicl < ciclTotal; cicl++)
            {
                OffBuses(matrixControl1, cicl * 10);
                if (cicl == ciclTotal - 1)
                    Data.Buses[rnd.Next(0, Data.Buses.Count)].Tracker = true;
                List<int?> mas = new List<int?>();
                ShuffleBuses();
                if (Main.SavePictures == true)
                {
                    Directory.CreateDirectory(pathOpt + "/Epics" + "/" + (cicl + 1).ToString());
                }
                for (int i = 0; i < OptiCount; i++)
                {
                    if (Main.SavePictures == true)
                    {
                        Directory.CreateDirectory(pathOpt + "/Epics" + "/" + (cicl + 1).ToString() + "/" + (i + 1).ToString());
                    }
                    Epicenter.CreateOneRandomEpicenter(Main.EpicSizeParam, null);
                    Modeling.StartModeling(pathOpt, cicl, i);
                    if ((Main.SavePictures == true) && (!Main.ExtendedSavePictures == true))
                    {

                        lock (Main.Ep.Esheet)
                        {
                            Main.Ep.EDrawEpics(Data.Epics);
                        }
                        lock (Main.Ep.Esheet)
                        {
                            using (System.Drawing.Image img = (Image)Main.Ep.Esheet.Image.Clone())
                            {
                                img.Save(pathOpt + "/Epics" + "/" + (cicl + 1).ToString() + "/" + (i + 1).ToString() + "/" + i.ToString() + "_nat" + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
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
                                img.Save(pathOpt + "/Epics" + "/" + (cicl + 1).ToString() + "/" + (i + 1).ToString() + "/" + i.ToString() + "_re" + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                            }
                        }

                    }

                    loadingForm.loading.Invoke(new DelInt((s) => loadingForm.loading.Value = s), loadingForm.loading.Value + 1);
                    small = 10000;
                }

                int total = Modeling.ResultFromModeling.Sum(x => Convert.ToInt32(x));
                int count = 0;
                foreach (var m in Modeling.ResultFromModeling)
                {
                    if (m != null)
                    {
                        count += 1;
                    }
                }

                if (total < 0 || count < Modeling.ResultFromModeling.Count / 2)
                {
                    if (!percentMean.ContainsKey(withoutSensorsBuses.Last()))
                        percentMean.Add(withoutSensorsBuses.Last(), null);
                }
                else
                {
                    if (!percentMean.ContainsKey(withoutSensorsBuses.Last()))
                    {
                        if (count != 0)
                            percentMean.Add(withoutSensorsBuses.Last(), total / count);
                        else
                            percentMean.Add(withoutSensorsBuses.Last(), -1);
                    }
                };

                using (StreamWriter fileV = new StreamWriter(pathOpt + @"\" + withoutSensorsBuses.Last() + "_buses" + ".txt"))
                {
                    fileV.WriteLine(MainStrings.sensorsDown + ": " + (cicl * 10).ToString());
                    fileV.WriteLine(MainStrings.countBuses + ": " + (withoutSensorsBuses.Last()).ToString());
                    fileV.WriteLine(MainStrings.numIter + ": " + OptiCount);
                    fileV.WriteLine(MainStrings.distance + ": " + OptiSpeed.ToString() + " " + MainStrings.sec + " (" + (OptiSpeed / 60 == 0 ? ">1" + MainStrings.minute : OptiSpeed / 60 + " " + MainStrings.minute) + ")");
                    fileV.WriteLine(MainStrings.found + ": " + (from num in Modeling.ResultFromModeling where (num != null) select num).Count());
                    if (count == 0)
                    {
                        fileV.WriteLine(MainStrings.none);
                    }
                    else
                    {
                        fileV.WriteLine(MainStrings.average + " " + (total / count / 60 == 0 ? (total / count + " " + MainStrings.sec).ToString() : (total / count / 60 + " " + MainStrings.minute + " " + total / count % 60 + " " + MainStrings.sec).ToString())
                            + "\n" + MainStrings.procentSuc + " " + (count * 100.00 / OptiCount) + "\n" + MainStrings.procentFailed + " " + (((Modeling.ResultFromModeling.Count - count) * 100.00 / OptiCount)).ToString());
                    }
                    fileV.WriteLine(MainStrings.cycle + " " + cicl.ToString());
                    for (int i = 0; i < Modeling.ResultFromModeling.Count; i++)
                        if (Modeling.ResultFromModeling[i] != null)
                        {
                            fileV.WriteLine(i.ToString() + " : " + (Modeling.ResultFromModeling[i] / 60 == 0 ? (Modeling.ResultFromModeling[i] + " " + MainStrings.sec).ToString() : (Modeling.ResultFromModeling[i] / 60 + " " + MainStrings.minute + " " + Modeling.ResultFromModeling[i] % 60 + " " + MainStrings.sec).ToString()));
                        }
                        else
                        {
                            fileV.WriteLine(i.ToString() + " : " + MainStrings.notFound);
                        }

                    Console.WriteLine("Объект сериализован");
                }
                Modeling.ResultFromModeling = new List<int?>();
            }

            var res = percentMean.Where(s => s.Value.Equals(percentMean.Min(v => v.Value))).Select(s => s.Key).ToList();
            var min = percentMean.Min(v => v.Value);
            var result = percentMean.Where(s => s.Value.Equals(min)).Select(s => s.Key).ToList();
            result.Sort();
            if (res.Count != 0 && min != 0 && min != null)
                mean = MainStrings.average + " " + (min / 60 == 0 ? (min + " " + MainStrings.sec).ToString() : (min / 60 + " " + MainStrings.minute).ToString()) + " " + " - " + MainStrings.countSensors + ": " + result[0];
            else
            {
                mean = MainStrings.average + " " + MainStrings.notFound;
            }

            using (StreamWriter fileV = new StreamWriter(pathOpt + "/Average.txt"))
            {
                fileV.WriteLine(mean != MainStrings.average + " " + MainStrings.notFound ? MainStrings.average + " " + (min / 60 == 0 ? (min + " " + MainStrings.sec).ToString() : (min / 60 + " " + MainStrings.minute).ToString()) + " - " + MainStrings.countSensors + ": " + result[0] : MainStrings.notFound);
            }

            
            Main.Average = mean;
            //BarabanAfterOpti();
            Data.Buses = optimizeBuses;
        }

        private static void OffBuses(MatrixControl matrixControl1, int proc = 0)
        {
            int countSensors = 0;
            int tot = 0;
            matrixControl1.SplitBuses();
            Data.BusesPark = matrixControl1.busesPark;
            foreach (var b in Data.BusesPark)
            {
                var BusesParkWithSensors = b.Where((bus) => bus.Tracker == true);
                double razm = Math.Round(b.Count - b.Count * 0.01 * proc);
                double limit = Math.Round(b.Count - razm, 0);
                foreach (var bus in BusesParkWithSensors)
                {
                    if (0 != limit)
                    {
                        countSensors += 1;
                        bus.Tracker = false;
                        limit = limit - 1;
                    }
                    else
                    {
                        break;
                    };
                };
                for (var i = 0; i < b.Count; i++)
                {
                    Data.Buses[tot] = b[i];
                    tot += 1;
                }
            };
            countWithoutSensors -= countSensors;
            if (withoutSensorsBuses.Count == 4)
            {
                countWithoutSensors = 1;
            }
            if (withoutSensorsBuses.Count != 5)
            {
                withoutSensorsBuses.Add(countWithoutSensors);
            }
        }
        private static void ShuffleBuses()
        {
            foreach (var bp in Data.BusesPark)
            {
                var tot = (Data.AllGridsInRoutes[bp.First().Route].Count - 1) / bp.Count;
                if (tot == 0 || tot == 1)
                {
                    foreach (var b in Data.Buses)
                    {
                        if (b.Route == bp.First().Route)
                        {
                            int r = rnd.Next(0, Data.AllGridsInRoutes[bp.First().Route].Count - 1);
                            b.PositionAt = r;
                        }

                    };
                }
                else
                {
                    List<int> array = new List<int>();
                    int i = 0;
                    while (i < Data.AllGridsInRoutes[bp.First().Route].Count - 1)
                    {
                        array.Add(i);
                        i += tot;
                    }
                    if (array.Count != 0)
                    {
                        foreach (var b in Data.Buses)
                        {

                            if (b.Route == bp.First().Route)
                            {
                                int r = rnd.Next(0, array.Count - 1);
                                b.PositionAt = array[r];
                                array.RemoveAt(r);
                            }

                        };
                    }
                }
            }
        }
        private static void ShuffleBusesAfterOptimization()
        {
            Random rnd = new Random();
            foreach (var b in Data.Buses)
            {
                int r = rnd.Next(0, Data.AllCoordinates[b.Route].Count - 1);
                b.PositionAt = r;
            };
        }
    }
}
