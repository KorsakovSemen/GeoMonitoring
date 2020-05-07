using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystAnalys_lr1.Classes
{
    public class GridPart
    {
        public int x, y;

        public int Status { get; set; }

        public bool Check { get; set; } = false;

        public bool IsMovedAway { get; set; } = false;

        public static int Width { get; set; } = 1;

        public static int Height { get; set; } = 1;

        public GridPart(int x, int y)
        {
            this.x = x;
            this.y = y;
            Status = 1000;
        }

        public GridPart()
        { }

        public void DrawPart(DrawGraph G, int zoom)
        {
            G.gr.DrawRectangle(new Pen(Color.Black, 1), x * zoom, y * zoom, Width * zoom, Height * zoom);
        }

        public void FillGreen(DrawGraph G, int zoom)
        {
            G.gr.FillRectangle(new SolidBrush(Color.FromArgb(20, 0, 128, 0)), new Rectangle(x * zoom, y * zoom, Width * zoom, Height * zoom));
        }

        public void DrawPartInRed(DrawGraph G)
        {
            G.gr.DrawRectangle(new Pen(Color.Red, 1), (x + 5) * Main.zoom, (y + 5) * Main.zoom, (Width - 10) * Main.zoom, (Height - 10) * Main.zoom);
        }

    }
}
