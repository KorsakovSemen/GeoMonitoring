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
        public int Tick { get; set; } 
        public int GreenTime { get; }
        public int RedTime { get; }
        public int YellowTime { get; } = 2;
        public int bal { get; set; } // остаток времени на светофоре

        public TraficLight() { }

        public TraficLight(int x, int y, int gridNum, int greenTime, int redTime)
        {
            X = x;
            Y = y;
            this.gridNum = gridNum;
            this.GreenTime = greenTime;
            this.RedTime = redTime;
        }

        private void TimerLightProcessor(object sender, EventArgs e)
        {
            SwapLights();
            Tick += 1;
            if (bal > 0)
                bal -= 1;
            if (Tick == GreenTime + RedTime + YellowTime + YellowTime)
            {
                Tick = 0;
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
            if (Tick == 0)
            {
                bal = GreenTime;
                Main.G.DrawGreenVertex(X, Y);
                Status = Status.GREEN;
            }
            else if (Tick == GreenTime)
            {
                bal = YellowTime;
                Main.G.DrawYellowVertex(X, Y);
                Status = Status.YELLOW;
            }
            else if (Tick == GreenTime + YellowTime)
            {
                bal = RedTime;
                Main.G.DrawSelectedVertex(X, Y);
                Status = Status.RED;
            }
            else if (Tick == GreenTime + YellowTime + RedTime)
            {
                bal = YellowTime;
                Main.G.DrawYellowVertex(X, Y);
                Status = Status.YELLOW;
            }
        }
    }
}