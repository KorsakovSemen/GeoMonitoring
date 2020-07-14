//This is a personal academic project. Dear PVS-Studio, please check it.
//PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
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
    public enum LightStatus
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
        private Timer _timerLight;
        private LightStatus _status;
        private int _tick;
        private int _bal;
        private readonly int _yellowTime = 2;

        [XmlIgnore]
        public Timer TimerLight { get => _timerLight; private set => _timerLight = value; }
        public LightStatus Status { get => _status; set => _status = value; }
        public int Tick { get => _tick; set => _tick = value; }
        public int GreenTime { get; }
        public int RedTime { get; }
        public int YellowTime => _yellowTime;
        public int Bal { get => _bal; set => _bal = value; } // остаток времени на светофоре

        public TraficLight() { }

        public TraficLight(int x, int y, int greenTime, int redTime)
        {
            X = x;
            Y = y;
            this.GreenTime = greenTime;
            this.RedTime = redTime;
        }

        public TraficLight(int x, int y, int gridNum, int greenTime, int redTime)
        {
            X = x;
            Y = y;
            this.GridNum = gridNum;
            this.GreenTime = greenTime;
            this.RedTime = redTime;
        }

        private void TimerLightProcessor(object sender, EventArgs e)
        {
            SwapLights();
            Tick += 1;
            if (Bal > 0)
                Bal -= 1;
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
                Bal = GreenTime;
                Main.G.DrawGreenVertex(X, Y);
                Status = LightStatus.GREEN;
            }
            else if (Tick == GreenTime)
            {
                Bal = YellowTime;
                Main.G.DrawYellowVertex(X, Y);
                Status = LightStatus.YELLOW;
            }
            else if (Tick == GreenTime + YellowTime)
            {
                Bal = RedTime;
                Main.G.DrawSelectedVertex(X, Y);
                Status = LightStatus.RED;
            }
            else if (Tick == GreenTime + YellowTime + RedTime)
            {
                Bal = YellowTime;
                Main.G.DrawYellowVertex(X, Y);
                Status = LightStatus.YELLOW;
            }
        }
    }
}