using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SystAnalys_lr1.Strings;
using MetroFramework;

namespace SystAnalys_lr1.Classes
{
    public partial class MatrixControl : MetroFramework.Controls.MetroUserControl
    {
        public MatrixControl()
        {
            InitializeComponent();
        }

        public List<List<Bus>> busesPark;
        public SortedDictionary<string, List<Vertex>> routesSorted;
        public int parkSize;

        public List<Bus> SplitBuses()
        {
            busesPark = new List<List<Bus>>();

            List<Bus> b = new List<Bus>();

            for (int i = 0; i < Main.buses.Count; i++)
            {
                if (i == Main.buses.Count - 1)
                {
                    if (b.Count == 0)
                    {
                        busesPark.Add(new List<Bus>() { Main.buses[i] });
                    }
                    else
                    {
                        b.Add(Main.buses[i]);
                        b = BubbleSortEx(b);
                        busesPark.Add(new List<Bus>(b));
                        b.Clear();
                    }
                }
                else
                {
                    if (Main.buses[i].Route == Main.buses[i + 1].Route)
                    {
                        b.Add(Main.buses[i]);
                    }
                    else
                    {
                        if (b.Count == 0)
                        {
                            busesPark.Add(new List<Bus>() { Main.buses[i] });
                        }
                        else
                        {
                            b.Add(Main.buses[i]);
                            b = BubbleSortEx(b);
                            busesPark.Add(new List<Bus>(b));
                            b.Clear();
                        }
                    }
                }
            };
            return b;
        }


        public List<Bus> BubbleSortEx(List<Bus> buses)
        {
            var temp = new Bus();
            for (int i = 0; i < buses.Count - 1; i++)
            {
                for (int j = 0; j < buses.Count - i - 1; j++)
                {
                    if (buses[j + 1].Route == buses[j].Route && buses[j + 1].Tracker == true && buses[j].Tracker == false)
                    {
                        temp = buses[j + 1];
                        buses[j + 1] = buses[j];
                        buses[j] = temp;
                    };
                }
            }
            return buses;
        }


        public void MatrixCreate()
        {
            if (parkSize + 1 < 655)
            {
                matrixGrid.Rows.Clear();
                matrixGrid.Refresh();

                Main.buses.Sort((a1, b1) => a1.Route.CompareTo(b1.Route));

                SplitBuses();

                routesSorted = new SortedDictionary<string, List<Vertex>>(Main.routes);

                foreach (var x in Main.routes)
                {
                    if (x.Value.Count == 0)
                    {
                        routesSorted.Remove(x.Key);
                    }
                }

                parkSize = 0;

                foreach (var x in busesPark)
                {
                    parkSize = Math.Max(parkSize, x.Count);
                }

                int[,] myArr = new int[routesSorted.Count, parkSize];
                if (routesSorted.Count == 0)
                    matrixGrid.RowCount = 1;
                else
                    matrixGrid.RowCount = routesSorted.Count;

                matrixGrid.ColumnCount = parkSize + 1;

                for (int i = 1; i < parkSize; i++)
                {
                    matrixGrid.Columns[i - 1].HeaderText = i.ToString();
                    if (i + 1 == parkSize)
                    {
                        matrixGrid.Columns[i].HeaderText = parkSize.ToString();
                        matrixGrid.Columns[i].FillWeight = 1;
                    }
                }

                matrixGrid.Columns[parkSize].HeaderText = "Total";
                try
                {
                    for (int i = 0; i < busesPark.Count; ++i)
                    {
                        if (busesPark[i].Count != 0)
                        {
                            matrixGrid.Rows[i].HeaderCell.Value = busesPark[i].Last().Route.ToString();
                        }
                    }

                    int total, res;
                    res = 0;
                    for (int i = 0; i < busesPark.Count; i++)
                    {
                        total = 0;
                        for (int j = 0; j < parkSize + 1; j++)
                        {
                            if (j < busesPark[i].Count)
                            {
                                if (busesPark[i][j].Tracker == true)
                                {
                                    myArr[i, j] = 1;
                                    total++;
                                }
                                else
                                {
                                    myArr[i, j] = 0;
                                }
                                matrixGrid.Rows[i].Cells[j].Value = myArr[i, j];
                            }
                            else
                            {
                                matrixGrid.Rows[i].Cells[j].Value = 0;
                            }
                            matrixGrid.Rows[i].Cells[parkSize].Value = total;

                        }
                        res += total;
                    }

                    result.Text = MainStrings.matrixFirst + res.ToString() + " " + MainStrings.matrixSecond + (Main.buses.Count - res).ToString();
                    count.Text = MainStrings.matrixThird + Main.buses.Count.ToString();
                }
                catch
                {
                    MetroMessageBox.Show(this, MainStrings.Matrix, MainStrings.error, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MetroMessageBox.Show(this, MainStrings.Matrix, MainStrings.error, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void MatrixControl_Load(object sender, EventArgs e)
        {
            metroButton1.Text = MainStrings.create;
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            MatrixCreate();
        }
    }
}
