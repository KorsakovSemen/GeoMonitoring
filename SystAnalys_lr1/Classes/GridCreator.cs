//This is a personal academic project. Dear PVS-Studio, please check it.
//PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystAnalys_lr1.Classes
{
    public static class GridCreator
    {
        //отрисовать всю сетку
        static public void DrawGrid(PictureBox sheet)
        {
            for (int i = 0; i < Data.TheGrid.Count; i++)
            {
                Data.TheGrid[i].DrawPart(Main.G, Main.zoom);
            }
            sheet.Image = Main.G.GetBitmap();
        }

        //создать сетку
        static public void CreateGrid(PictureBox sheet)
        {
            GridPart.Height = sheet.Height / Main.Grid.GridHeight;
            GridPart.Width = sheet.Width / Main.Grid.GridWidth;
            Data.TheGrid = new List<GridPart>();
            for (int i = Main.Grid.Left; i < sheet.Height - Main.Grid.Right; i += sheet.Height / Main.Grid.GridHeight)
            {
                for (int j = Main.Grid.Up; j < sheet.Width - Main.Grid.Down; j += sheet.Width / Main.Grid.GridWidth)
                {
                    Data.TheGrid.Add(new GridPart(j, i));
                }
            }
            DrawGrid(sheet);

        }
    }
}
