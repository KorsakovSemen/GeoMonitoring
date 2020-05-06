using MetroFramework;
using Newtonsoft.Json;
using SystAnalys_lr1.Classes;
using SystAnalys_lr1.Strings;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SystAnalys_lr1
{   
    public enum Status
    {
        [XmlEnum(Name = "1")]
        GREEN = 1,
        [XmlEnum(Name = "2")]
        YELLOW = 2,
        [XmlEnum(Name = "3")]
        RED = 3
    }
    public class TraficLight : Vertex
    {
        [XmlIgnore]
        public Timer TimerLight { get; private set; }
        public Status Status { get; set; }
        public int tick, greenTime, redTime;
        public int yellowTime = 2;
        public int bal; // остаток времени на светофоре

        public TraficLight() { }

        public TraficLight(int x, int y, int gridNum, int greenTime, int redTime)
        {
            this.X = x;
            this.Y = y;
            this.gridNum = gridNum;
            this.greenTime = greenTime;
            this.redTime = redTime;
        }

        private void TimerLightProcessor(object sender, EventArgs e)
        {
            SwapLights();
            tick += 1;
            if (bal > 0)
                bal -= 1;
            if (tick == greenTime + redTime + yellowTime + yellowTime)
            {
                tick = 0;
            }
        }
        public void Stop()
        {
            if (TimerLight != null)
            {
                TimerLight.Stop();
            }

        }
        public void Start()
        {
            TimerLight.Start();
        }
        public void Set()
        {
            TimerLight = new Timer
            {
                Interval = 1000
            };
            TimerLight.Tick += new EventHandler(TimerLightProcessor);
            TimerLight.Start();
        }

        private void SwapLights()
        {
            if (tick == 0)
            {
                bal = greenTime;
                Main.G.DrawGreenVertex(X, Y);
                Status = Status.GREEN;
            }
            else if (tick == greenTime)
            {
                bal = yellowTime;
                Main.G.DrawYellowVertex(X, Y);
                Status = Status.YELLOW;
            }
            else if (tick == greenTime + yellowTime)
            {
                bal = redTime;
                Main.G.DrawSelectedVertex(X, Y);
                Status = Status.RED;
            }
            else if (tick == greenTime + yellowTime + redTime)
            {
                bal = yellowTime;
                Main.G.DrawYellowVertex(X, Y);
                Status = Status.YELLOW;
            }
        }
    }
}