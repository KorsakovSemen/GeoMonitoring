using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GradientPanelDemo
{
    class GradientPanel : Panel
    {
        public Color ColorTop { get; set; }
        public Color ColorBottom { get; set; }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Construct a path gradient brush based on an array of points.
            PointF[] ptsF = {
       new PointF(0, 0),
       new PointF(160, 0),
       new PointF(160, 200),
       new PointF(80, 150),
       new PointF(0, 200)};

            PathGradientBrush pBrush = new PathGradientBrush(ptsF);

            // An array of five points was used to construct the path gradient
            // brush. Set the color of each point in that array.
            Color[] colors = {
       Color.FromArgb(255, 255, 0, 0),  // (0, 0) red
       Color.FromArgb(255, 0, 255, 0),  // (160, 0) green
       Color.FromArgb(255, 0, 255, 0),  // (160, 200) green
       Color.FromArgb(255, 0, 0, 255),  // (80, 150) blue
       Color.FromArgb(255, 255, 0, 0)}; // (0, 200) red

            pBrush.SurroundColors = colors;

            // Set the center color to white.
            pBrush.CenterColor = Color.White;

            e.Graphics.FillRectangle(pBrush, new Rectangle(0, 0, 160, 200));
        }
    }
}
