//This is a personal academic project. Dear PVS-Studio, please check it.
//PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using MetroFramework;
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
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SystAnalys_lr1.Classes
{
    static class Optimization
    {
        private static int? min;
        private static List<int> result;
        private static string mean;
        private static string s_pathOpt;
        private static SerializableDictionary<int, int?> s_percentMean;
        private static int s_small = 10000;
        private static int s_countWithoutSensors;
        private static List<int> s_withoutSensorsBuses = new List<int>();
        private static int s_optiSpeed;
        private static int s_optiCount;

        public static string PathOpt { get => s_pathOpt; set => s_pathOpt = value; }
        public static SerializableDictionary<int, int?> PercentMean { get => s_percentMean; set => s_percentMean = value; }
        private static int Small { get => s_small; set => s_small = value; }
        public static int CountWithoutSensors { get => s_countWithoutSensors; set => s_countWithoutSensors = value; }
        public static List<int> WithoutSensorsBuses { get => s_withoutSensorsBuses; set => s_withoutSensorsBuses = value; }
        delegate void DelInt(int text);
        static Random rnd = new Random();
        public static int OptiSpeed { get => s_optiSpeed; set => s_optiSpeed = value; }
        public static int OptiCount { get => s_optiCount; set => s_optiCount = value; }
        public static string Mean { get => mean; set => mean = value; }
        public static int? Min { get => min; set => min = value; }
        public static List<int> Result { get => result; set => result = value; }


        public static void ResMatrix(MetroGrid results)
        {
            results.Rows.Clear();
            results.Refresh();
            results.RowCount = 5;
            int i = 0;
            foreach (var pm in PercentMean)
            {
                results.Rows[i].HeaderCell.Value = pm.Key.ToString();
                if (pm.Value != 0)
                    results.Rows[i].Cells[0].Value = (pm.Value / 60).ToString();
                else
                    results.Rows[i].Cells[0].Value = MainStrings.notFound;
                i += 1;
            }
        }

        public static void ResChart(int oldChart, Report r, MetroStyleManager StyleManager)
        {
            bool changeText = false;
            if (oldChart != PercentMean.Keys.Sum())
            {
                r.ch.Legends.Clear();
                Main.ReportCount = 0;
                foreach (var series in r.ch.Series)
                {
                    series.Points.Clear();
                }
                oldChart = PercentMean.Keys.Sum();
                changeText = true;
            }
            int iCh = 0;
            StyleManager.Clone(r);
            if (Main.ReportCount != 0)
                r.ch.Series.Add(Main.ReportCount.ToString());
            r.ch.Series[Main.ReportCount].LegendText = Main.ReportCount.ToString();
            foreach (var pm in PercentMean)
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
            r.ch.SaveImage(PathOpt + "/" + MainStrings.chart + ".jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
            r.TopMost = true;
            r.Show();
            r.BringToFront();

            Main.ReportCount += 1;

        }


        public static void Opt(MatrixControl matrixControl, LoadingForm loadingForm)
        {

            PathOpt = "../../Results/" + string.Format("{0}_{1}_{2}_{3}_{4}_{5}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            Directory.CreateDirectory(PathOpt);
            Directory.CreateDirectory(PathOpt + "/Matrices");
            FileStream fs;
            StreamWriter streamWriter;
            PercentMean = new SerializableDictionary<int, int?>();

            List<Bus> optimizeBuses = new List<Bus>();
            Data.Buses.ForEach((b) => optimizeBuses.Add(
                (Bus)b.Clone()
            ));
            int old = Small;


            int ciclTotal = 5;

            loadingForm.loading.Invoke(new DelInt((s) => loadingForm.loading.Maximum = s), ciclTotal * OptiCount);
            for (int cicl = 0; cicl < ciclTotal; cicl++)
            {
                fs = File.Create(PathOpt + "/Matrices/" + cicl + "_Matrix.txt");
                streamWriter = new StreamWriter(fs);
                OffBuses(matrixControl, cicl * 10);
                if (cicl == ciclTotal - 1)
                    Data.Buses[rnd.Next(0, Data.Buses.Count)].Tracker = true;
                List<int?> mas = new List<int?>();
                ShuffleBuses();
                if (EpicSettings.SavePictures == true)
                {
                    Directory.CreateDirectory(PathOpt + "/Epics" + "/" + (cicl + 1).ToString());
                }
                for (int i = 0; i < OptiCount; i++)
                {
                    if (EpicSettings.SavePictures == true)
                    {
                        Directory.CreateDirectory(PathOpt + "/Epics" + "/" + (cicl + 1).ToString() + "/" + (i + 1).ToString());
                    }
                    Epicenter.CreateOneRandomEpicenter(Main.EpicSizeParam, null);
                    Modeling.StartModeling(PathOpt, cicl, i);
                    if ((EpicSettings.SavePictures == true) && (!EpicSettings.ExtendedSavePictures == true))
                    {

                        lock (Main.Ep.Esheet)
                        {
                            Main.Ep.EDrawEpics(Data.Epics);
                        }
                        lock (Main.Ep.Esheet)
                        {
                            using (System.Drawing.Image img = (Image)Main.Ep.Esheet.Image.Clone())
                            {
                                img.Save(PathOpt + "/Epics" + "/" + (cicl + 1).ToString() + "/" + (i + 1).ToString() + "/" + i.ToString() + "_nat" + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
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
                                img.Save(PathOpt + "/Epics" + "/" + (cicl + 1).ToString() + "/" + (i + 1).ToString() + "/" + i.ToString() + "_re" + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                            }
                        }

                    }

                    loadingForm.loading.Invoke(new DelInt((s) => loadingForm.loading.Value = s), loadingForm.loading.Value + 1);
                    Small = 10000;
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
                    if (!PercentMean.ContainsKey(WithoutSensorsBuses.Last()))
                        PercentMean.Add(WithoutSensorsBuses.Last(), null);
                }
                else
                {
                    if (!PercentMean.ContainsKey(WithoutSensorsBuses.Last()))
                    {
                        if (count != 0)
                            PercentMean.Add(WithoutSensorsBuses.Last(), total / count);
                        else
                            PercentMean.Add(WithoutSensorsBuses.Last(), -1);
                    }
                };
                using (StreamWriter fileV = new StreamWriter(PathOpt + @"\" + WithoutSensorsBuses.Last() + "_buses" + ".txt"))
                {
                    fileV.WriteLine(MainStrings.sensorsDown + ": " + (cicl * 10).ToString());
                    fileV.WriteLine(MainStrings.countBuses + ": " + (WithoutSensorsBuses.Last()).ToString());
                    fileV.WriteLine(MainStrings.numIter + ": " + OptiCount);
                    fileV.WriteLine(MainStrings.distance + ": " + OptiSpeed.ToString() + " " + MainStrings.sec + " (" + (OptiSpeed <= 60 ? ">1" + MainStrings.minute : OptiSpeed / 60 + " " + MainStrings.minute) + ")");
                    fileV.WriteLine(MainStrings.found + ": " + (from num in Modeling.ResultFromModeling where (num != null) select num).Count());
                    if (count == 0)
                    {
                        fileV.WriteLine(MainStrings.none);
                    }
                    else
                    {
                        fileV.WriteLine(MainStrings.average + " " + (total / count / 60 < 60 ? (total / count + " " + MainStrings.sec).ToString() : (total / count / 60 + " " + MainStrings.minute + " " + total / count % 60 + " " + MainStrings.sec).ToString())
                                      + "\n" + MainStrings.procentSuc + " " + (count * 100.00 / OptiCount) + "\n" + MainStrings.procentFailed + " " + ((Modeling.ResultFromModeling.Count - count) * 100.00 / OptiCount).ToString());
                    }
                    fileV.WriteLine(MainStrings.cycle + " " + cicl.ToString());
                    for (int i = 0; i < Modeling.ResultFromModeling.Count; i++)
                        if (Modeling.ResultFromModeling[i] != null)
                        {
                            var ts = TimeSpan.FromSeconds((double)Modeling.ResultFromModeling[i]);
                            fileV.WriteLine(i.ToString() + ": {0} д. {1} ч. {2} м. {3} с. {4} мс.", ts.Days, ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
                        }
                        else
                        {
                            fileV.WriteLine(i.ToString() + " : " + MainStrings.notFound);
                        }

                    Console.WriteLine("Объект сериализован");
                }
                Modeling.ResultFromModeling = new List<int?>();

                matrixControl.MatrixCreate(false);
                SaveMatrix(matrixControl, streamWriter);
            }

            var res = PercentMean.Where(s => s.Value.Equals(PercentMean.Min(v => v.Value))).Select(s => s.Key).ToList();
            Min = PercentMean.Min(v => v.Value);
            Result = PercentMean.Where(s => s.Value.Equals(Min)).Select(s => s.Key).ToList();
            Result.Sort();
            if (res.Count != 0 && Min != 0 && Min != null)
                Mean = "Found";
            else
            {
                Mean = null;
            }

            using (StreamWriter fileV = new StreamWriter(PathOpt + "/Average.txt"))
            {
                fileV.WriteLine(Mean != MainStrings.average + " " + MainStrings.notFound ? MainStrings.average + " " + (Min / 60 == 0 ? (Min + " " + MainStrings.sec).ToString() : (Min / 60 + " " + MainStrings.minute).ToString()) + " - " + MainStrings.countSensors + ": " + Result[0] : MainStrings.notFound);
            }


            Main.Average = Mean;
            Data.Buses = optimizeBuses;

        }

        private static void SaveMatrix(MatrixControl matrixControl, StreamWriter streamWriter)
        {

            try
            {
                int parkSize = matrixControl.parkSize;
                List<int> col_n = new List<int>();
                streamWriter.Write("\t");
                for (int i = 1; i < parkSize; i++)
                {
                    streamWriter.Write(i.ToString() + "\t");
                    if (i + 1 == parkSize)
                    {
                        streamWriter.Write(parkSize.ToString() + "\t");
                        streamWriter.Write("Total");
                    }
                }
                streamWriter.Write(" \r\n");
                foreach (DataGridViewColumn col in matrixControl.matrixGrid.Columns)
                    if (col.Visible)
                    {
                        col_n.Add(col.Index);
                    }
                int x = matrixControl.matrixGrid.RowCount;
                if (matrixControl.matrixGrid.AllowUserToAddRows) x--;

                for (int i = 0; i < x; i++)
                {

                    streamWriter.Write(matrixControl.busesPark[i].Last().Route.ToString() + "\t");
                    for (int y = 0; y < col_n.Count; y++)
                    {
                        streamWriter.Write(matrixControl.matrixGrid[col_n[y], i].Value + "\t");
                    }
                    streamWriter.Write(" \r\n");
                }
                streamWriter.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }
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
            CountWithoutSensors -= countSensors;
            if (WithoutSensorsBuses.Count == 4)
            {
                CountWithoutSensors = 1;
            }
            if (WithoutSensorsBuses.Count != 5)
            {
                WithoutSensorsBuses.Add(CountWithoutSensors);
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
                            if (Data.AllGridsInRoutes[bp.First().Route].Count - 1 >= 0)
                            {
                                int r = rnd.Next(0, Data.AllGridsInRoutes[bp.First().Route].Count - 1);
                                b.PositionAt = r;
                            }
                            else
                            {
                                b.PositionAt = rnd.Next(Data.AllGridsInRoutes[bp.First().Route].Count);
                            }

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
