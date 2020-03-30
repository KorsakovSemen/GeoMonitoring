using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystAnalys_lr1
{
    public partial class Main : MetroForm
    {      
        public List<Bus> BubbleSortEx(List<Bus> buses)
        {
            var temp = new Bus();
            for (int i = 0; i < buses.Count - 1; i++)
            {
                for (int j = 0; j < buses.Count - i - 1; j++)
                {
                    if (buses[j + 1].route == buses[j].route && buses[j + 1].tracker == true && buses[j].tracker == false)
                    {
                        temp = buses[j + 1];
                        buses[j + 1] = buses[j];
                        buses[j] = temp;
                    };
                }
            }
            return buses;
        }

        SortedDictionary<int, List<Vertex>> routesSorted;

        private void Matrix()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            buses.Sort((a1, b1) => a1.route.CompareTo(b1.route));

            SplitBuses();

            routesSorted  = new SortedDictionary<int, List<Vertex>>(routes); 
           
            foreach(var x in routes)
            {
                if (x.Value.Count == 0) {
                    routesSorted.Remove(x.Key);
                }
            }

            int parkSize = 0;

            foreach (var x in busesPark)
            {
                parkSize = Math.Max(parkSize, x.Count);
            }

            int[,] myArr = new int[routesSorted.Count, parkSize];

            dataGridView1.RowCount = routesSorted.Count;
            dataGridView1.ColumnCount = parkSize + 1;

            for (int i = 1; i < parkSize; i++)
            {
                dataGridView1.Columns[i - 1].HeaderText = i.ToString();
                if (i + 1 == parkSize)
                {
                    dataGridView1.Columns[i].HeaderText = parkSize.ToString();
                }
            }

            dataGridView1.Columns[parkSize].HeaderText = "Total";

            for (int i = 0; i < routesSorted.Count; ++i)
            {
                //if (routesSorted.ElementAt(i).Value.Count != 0)
                //{
                    dataGridView1.Rows[i].HeaderCell.Value = routesSorted.ElementAt(i).Key.ToString();
             //   }
            }

            int total, res;
            res = 0;
            for (int i = 0; i < busesPark.Count; i++)
            {
                //if(routesSorted.Values.Count != 0)
               // {
                    total = 0;
                    for (int j = 0; j < parkSize + 1; j++)
                    {
                        if (j < busesPark[i].Count)
                        {
                            if (busesPark[i][j].tracker == true)//getRoute() == routesSorted.ElementAt(i).Key)
                            {
                                myArr[i, j] = 1;
                                total++;
                            }
                            else
                            {
                                myArr[i, j] = 0;
                            }
                            dataGridView1.Rows[i].Cells[j].Value = myArr[i, j];
                        }
                        else
                        {
                            dataGridView1.Rows[i].Cells[j].Value = 0;
                        }
                        dataGridView1.Rows[i].Cells[parkSize].Value = total;

                    }
                    res += total;
               // }               
            }

            label8.Text = "С датчиками:" + res.ToString() + "  Без: " + (buses.Count - res).ToString() + " Всего: " + buses.Count.ToString();
        }
    }
}
