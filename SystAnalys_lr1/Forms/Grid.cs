using MetroFramework.Forms;
using Newtonsoft.Json;
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

        //отрисовать всю сетку
        static public void DrawGrid()
        {
            for (int i = 0; i < TheGrid.Count; i++)
            {
                TheGrid[i].DrawPart(G, zoom);
            }
            _instance.Invoke(new DelBmp((s) => _instance.sheet.Image = s), G.GetBitmap());
        }

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
            [XmlIgnore, JsonIgnore]
            public int gridWidth;
            [XmlIgnore, JsonIgnore]
            public int gridHeight;
        }
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

    }
}
