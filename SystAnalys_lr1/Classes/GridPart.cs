//This is a personal academic project. Dear PVS-Studio, please check it.
//PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
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
        private int _x;
        private int _y;
        private int _status;
        private bool _check = false;
        private bool _isMovedAway = false;
        private static int s_width = 1;
        private static int s_height = 1;

        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }
        public int Status { get => _status; set => _status = value; }
        public bool Check { get => _check; set => _check = value; }
        public bool IsMovedAway { get => _isMovedAway; set => _isMovedAway = value; }
        public static int Width { get => s_width; set => s_width = value; }
        public static int Height { get => s_height; set => s_height = value; }

        public GridPart(int x, int y)
        {
            this.X = x;
            this.Y = y;
            Status = 1000;
        }

        public GridPart()
        { }

        public void DrawPart(DrawGraph G, int zoom)
        {
            G.Gr.DrawRectangle(new Pen(Color.Black, 1), X * zoom, Y * zoom, Width * zoom, Height * zoom);
        }

        public void FillGreen(DrawGraph G, int zoom)
        {
            G.Gr.FillRectangle(new SolidBrush(Color.FromArgb(20, 0, 128, 0)), new Rectangle(X * zoom, Y * zoom, Width * zoom, Height * zoom));
        }

        public void DrawPartInRed(DrawGraph G)
        {
            G.Gr.DrawRectangle(new Pen(Color.Red, 1), (X + 5) * Main.zoom, (Y + 5) * Main.zoom, (Width - 10) * Main.zoom, (Height - 10) * Main.zoom);
        }

    }
}
