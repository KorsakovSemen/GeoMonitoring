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
            GridPart.Height = sheet.Height / Main.g.gridHeight;
            GridPart.Width = sheet.Width / Main.g.gridWidth;
            Data.TheGrid = new List<GridPart>();
            for (int i = Main.g.left; i < sheet.Height - Main.g.right; i += sheet.Height / Main.g.gridHeight)
            {
                for (int j = Main.g.up; j < sheet.Width - Main.g.down; j += sheet.Width / Main.g.gridWidth)
                {
                    Data.TheGrid.Add(new GridPart(j, i));
                }
            }
            DrawGrid(sheet);

        }
    }
}
