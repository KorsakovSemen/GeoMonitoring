using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SystAnalys_lr1
{
    public partial class Main : MetroForm
    {
        private async void AsyncGridMatrix()
        {
            await Task.Run(() => GridMatrix());
        }

        private void GridMatrix()
        {
            //    dataGridView3.Rows.Clear();
            //    dataGridView3.Columns.Clear();
            // dataGridView3.Refresh();
            //dataGridView3.Invoke(new DelInt((s) => dataGridView3.ColumnCount = s), AllGridFilling.Count());
            //dataGridView3.Invoke(new DelInt((s) => dataGridView3.RowCount = s), TheGrid.Count());
            //for (int i = 0; i < AllGridFilling.Count(); i++)
            //{
            //    dataGridView3.Invoke(new Del((s) => dataGridView3.Columns[i].HeaderText = s), "Пак данных N" + i.ToString() + " за " + TimeForAllGridFilling[i].ToString() + " секунд");

            //}
            //for (int i = 0; i < TheGrid.Count; ++i)
            //{
            //    dataGridView3.Invoke(new Del((s) => dataGridView3.Rows[i].HeaderCell.Value = s), "Квадрат - " + i.ToString());

            //}
            ////  dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ////  dataGridView3.RowHeadersWidth = 100;
            //for (int i = 0; i < AllGridFilling.Count(); i++)
            //{

            //    for (int j = 0; j < AllGridFilling[i].Count(); j++)
            //    {
            //        dataGridView3.Invoke(new Del((s) => dataGridView3.Rows[j].Cells[i].Value = s), AllGridFilling[i][j].ToString());
            //    }
            //}
        }

        //функция, которая создает/очищает контейнер для AllGridFilling
        //private void CreateAllOneGrids()
        //{
        //    OneGridFilling = new Dictionary<int, int>();
        //    OneRouteGridFilling = new Dictionary<int, Dictionary<int, int>>();
        //    for (int i = 0; i < TheGrid.Count; i++)
        //    {
        //        OneGridFilling.Add(i, 0);
        //        OneRouteGridFilling.Add(i, new Dictionary<int, int>());
        //        for (int j = 0; j < routes.Count; j++)

        //            OneRouteGridFilling[i].Add(routes.ElementAt(j).Key, 0);
        //    }
        //}

        //потом
        private void GridMatrixByRoute()
        {
            //dataGridView2.Rows.Clear();
            //dataGridView2.Columns.Clear();
            //dataGridView2.Refresh();
            //dataGridView2.ColumnCount = AllRouteGridFilling.Count();
            //dataGridView2.RowCount = TheGrid.Count();
            //for (int i = 0; i < AllRouteGridFilling.Count(); i++)
            //{
            //    dataGridView2.Columns[i].HeaderText = "Пак данных - " + i.ToString();

            //}
            //for (int i = 0; i < TheGrid.Count; ++i)
            //{
            //    dataGridView2.Rows[i].HeaderCell.Value = "Квадрат - " + i.ToString(); ;

            //}
            //dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //dataGridView2.RowHeadersWidth = 100;
            //for (int i = 0; i < AllRouteGridFilling.Count(); i++)
            //{

            //    for (int j = 0; j < AllRouteGridFilling[i].Count(); j++)
            //    {

            //        dataGridView2.Rows[j].Cells[i].Value = AllRouteGridFilling[i][j][7].ToString();

            //    }
            //}
        }
        //функция подсчитывает количество пустых квадратов 
        //private void EmptyGridCounts()
        //{
        //    EmptyGridCount = new Dictionary<int, int>();
        //    for (int i = 0; i < TheGrid.Count; i++)
        //    {
        //        EmptyGridCount.Add(i, 0);
        //    }


        //    for (int i = 0; i < AllGridFilling.Last().Count; i++)
        //    {
        //        if (AllGridFilling.Last()[i] == 0)
        //        {
        //            EmptyGridCount[i] = 1;
        //        }
        //    }

        //    //label6.Text = "Количество пустых секторов : " + EmptyGridCount.Skip(1).Sum(ix => ix.Value);
        //  //  label6.Invoke(new Del((s) => label6.Text = s), "Количество пустых секторов : " + EmptyGridCount.Skip(1).Sum(ix => ix.Value));
        //}
        public class Grid
        {
            public Grid() { }
            public Grid(int l, int u, int r, int d, int gW, int gH)
            {
                left = l;
                up = u;
                right = r;
                down = d;
                gridWidth = gW;
                gridHeight = gH;
            }
            public int left;
            public int up;
            public int right;
            public int down;
            public int gridWidth;
            public int gridHeight;
        }
    //    public static int left = 90, up = 300, right = 100, down = 300, gridWidth = 80 , gridHeight = 40;
        //создать сетку
        public void CreateGrid()
        {
            GridPart.height = sheet.Height / g.gridHeight;
            GridPart.width =  sheet.Width / g.gridWidth;
            TheGrid = new List<GridPart>();
            for (int i = g.left; i < sheet.Height - g.right; i += sheet.Height / g.gridHeight)
            {
                for (int j = g.up; j < sheet.Width - g.down; j += sheet.Width / g.gridWidth)
                {
                    TheGrid.Add(new GridPart(j, i));
                }
            }
            DrawGrid();
            
        }
       
        //функция рисует пустые квадраты
        //private void DrawEmptyGrid()
        //{
        //    for (int i = 0; i < EmptyGridCount.Count; i++)
        //    {
        //        if (EmptyGridCount[i] == 1)
        //        {
        //            TheGrid[i].DrawPartInRed(G);
        //        }
        //    }
        //    sheet.Image = G.GetBitmap();
        //}

    }
}
