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
    public class Epicenter : ICloneable
    {
        public SerializableDictionary<int, List<GridPart>> EpicenterGrid { get; set; }
        private Point startPositon;
        public int DetectCount { get; set; }
        public List<GridPart> TheGrid { get; set; }
        public Epicenter(List<GridPart> TheGrid)
        {
            this.TheGrid = TheGrid;
            EpicenterGrid = new SerializableDictionary<int, List<GridPart>>();
        }
        public Epicenter()
        { }
        public static void CreateOneRandomEpicenter(int EpicSizeParam, int? StartPos)
        {
            Data.Epics = new List<Epicenter>
            {
                new Epicenter(Data.TheGrid)
            };
            Data.Epics.First().CreateRandomEpicenter(EpicSizeParam, StartPos);
        }
        public static List<Epicenter> CopyEpicenter(List<Epicenter> CopiedEpics)
        {
            CopiedEpics = new List<Epicenter>();
            int i = 0;
            Parallel.ForEach(Data.Epics, (EpicList) =>
            {
                CopiedEpics.Add(new Epicenter(Data.TheGrid));
                foreach (var Sector in EpicList.EpicenterGrid)
                {
                    CopiedEpics[i].EpicenterGrid.Add(Sector.Key, new List<GridPart>());
                    CopiedEpics[i].StartPositon = EpicList.StartPositon;
                    CopiedEpics[i].NewExpandCount = new List<int>();
                    foreach (var Square in Sector.Value)
                    {
                        CopiedEpics[i].EpicenterGrid[Sector.Key].Add(new GridPart(Square.X, Square.Y));
                    }
                }
                i++;
            });
            return CopiedEpics;
        }
        public object Clone()
        {
            return MemberwiseClone();
        }
        public void Recreate(Dictionary<string, List<GridPart>> PollutionInRoutes)
        {
            EpicenterGrid.Add(1, new List<GridPart>());
            EpicenterGrid.Add(2, new List<GridPart>());
            EpicenterGrid.Add(3, new List<GridPart>());
            List<GridPart> OrangeBuffer = new List<GridPart>();
            foreach (var Pollutedroute in PollutionInRoutes.Values)
            {
                foreach (var PollutedSqure in Pollutedroute)
                {
                    if (PollutedSqure.Status != 0)
                    {
                        bool net = false;
                        switch (PollutedSqure.Status)
                        {
                            case 2:
                                foreach (var part in EpicenterGrid[2])
                                {
                                    if ((part.X == PollutedSqure.X) && (part.Y == PollutedSqure.Y))
                                    {
                                        net = true;
                                        break;
                                    }
                                }
                                if (net == false)
                                {
                                    OrangeBuffer.Add(new GridPart(PollutedSqure.X, PollutedSqure.Y));
                                }
                                break;
                            case 3:
                                foreach (var part in EpicenterGrid[1])
                                {
                                    if ((part.X == PollutedSqure.X) && (part.Y == PollutedSqure.Y))
                                    {
                                        net = true;
                                        break;
                                    }
                                }
                                if (net == false)
                                {
                                    EpicenterGrid[1].Add(new GridPart(PollutedSqure.X, PollutedSqure.Y));
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            List<GridPart> BufferList = new List<GridPart>();
            foreach (var RedGrid in EpicenterGrid[1])
            {
                for (int i = 1; i <= 5; i++)
                {
                    GridPart ScanGrid = new GridPart(RedGrid.X, RedGrid.Y - GridPart.Height * i);
                    for (int j = 0; j < 4; j++)
                    {
                        while (ScanGrid.X < RedGrid.X + GridPart.Width * i)
                        {
                            ScanGrid.X += GridPart.Width;
                            foreach (var SearchGrid in EpicenterGrid[1])
                            {
                                if ((ScanGrid.X == SearchGrid.X) && (ScanGrid.Y == SearchGrid.Y))
                                {
                                    List<string> Parameters = new List<string>();
                                    GridPart bufpart = new GridPart(ScanGrid.X, ScanGrid.Y);
                                    while (((bufpart.X == RedGrid.X) && (bufpart.Y == RedGrid.Y)) == false)
                                    {
                                        if (bufpart.X == RedGrid.X)
                                        {
                                            bufpart.Y += GridPart.Height;
                                            BufferList.Add(new GridPart(bufpart.X, bufpart.Y));
                                        }
                                        else
                                        if (bufpart.Y == RedGrid.Y)
                                        {
                                            bufpart.X -= GridPart.Width;
                                            BufferList.Add(new GridPart(bufpart.X, bufpart.Y));
                                        }
                                        else
                                        {
                                            bufpart.Y += GridPart.Height;
                                            BufferList.Add(new GridPart(bufpart.X, bufpart.Y));
                                            bufpart.X -= GridPart.Width;
                                            BufferList.Add(new GridPart(bufpart.X, bufpart.Y));
                                        }
                                    }
                                }
                            }
                        }
                        while (ScanGrid.Y < RedGrid.Y)
                        {
                            ScanGrid.Y += GridPart.Height;
                            foreach (var SearchGrid in EpicenterGrid[1])
                            {
                                if ((ScanGrid.X == SearchGrid.X) && (ScanGrid.Y == SearchGrid.Y))
                                {
                                    GridPart bufpart = new GridPart(ScanGrid.X, ScanGrid.Y);
                                    while (((bufpart.X == RedGrid.X) && (bufpart.Y == RedGrid.Y)) == false)
                                    {
                                        if (bufpart.X == RedGrid.X)
                                        {
                                            bufpart.Y += GridPart.Height;
                                            BufferList.Add(new GridPart(bufpart.X, bufpart.Y));
                                        }
                                        else
                                        if (bufpart.Y == RedGrid.Y)
                                        {
                                            bufpart.X -= GridPart.Width;
                                            BufferList.Add(new GridPart(bufpart.X, bufpart.Y));
                                        }
                                        else
                                        {
                                            bufpart.X -= GridPart.Width;
                                            BufferList.Add(new GridPart(bufpart.X, bufpart.Y));
                                            bufpart.Y += GridPart.Height;
                                            BufferList.Add(new GridPart(bufpart.X, bufpart.Y));
                                        }
                                    }
                                }
                            }
                        }
                        while (ScanGrid.Y < RedGrid.Y + GridPart.Height * i)
                        {
                            ScanGrid.Y += GridPart.Height;
                            foreach (var SearchGrid in EpicenterGrid[1])
                            {
                                if ((ScanGrid.X == SearchGrid.X) && (ScanGrid.Y == SearchGrid.Y))
                                {
                                    List<string> Parameters = new List<string>();
                                    GridPart bufpart = new GridPart(ScanGrid.X, ScanGrid.Y);
                                    while (((bufpart.X == RedGrid.X) && (bufpart.Y == RedGrid.Y)) == false)
                                    {

                                        if (bufpart.X == RedGrid.X)
                                        {
                                            bufpart.Y -= GridPart.Height;
                                            BufferList.Add(new GridPart(bufpart.X, bufpart.Y));
                                        }
                                        else
                                        if (bufpart.Y == RedGrid.Y)
                                        {
                                            bufpart.X -= GridPart.Width;
                                            BufferList.Add(new GridPart(bufpart.X, bufpart.Y));
                                        }
                                        else
                                        {
                                            bufpart.Y -= GridPart.Height;
                                            BufferList.Add(new GridPart(bufpart.X, bufpart.Y));
                                            bufpart.X -= GridPart.Width;
                                            BufferList.Add(new GridPart(bufpart.X, bufpart.Y));
                                        }
                                    }
                                }
                            }
                        }
                        while (ScanGrid.X > RedGrid.X)
                        {
                            ScanGrid.X -= GridPart.Width;
                            foreach (var SearchGrid in EpicenterGrid[1])
                            {
                                if ((ScanGrid.X == SearchGrid.X) && (ScanGrid.Y == SearchGrid.Y))
                                {
                                    GridPart bufpart = new GridPart(ScanGrid.X, ScanGrid.Y);
                                    while (((bufpart.X == RedGrid.X) && (bufpart.Y == RedGrid.Y)) == false)
                                    {
                                        if (bufpart.X == RedGrid.X)
                                        {
                                            bufpart.Y -= GridPart.Height;
                                            BufferList.Add(new GridPart(bufpart.X, bufpart.Y));
                                        }
                                        else
                                        if (bufpart.Y == RedGrid.Y)
                                        {
                                            bufpart.X -= GridPart.Width;
                                            BufferList.Add(new GridPart(bufpart.X, bufpart.Y));
                                        }
                                        else
                                        {
                                            bufpart.Y -= GridPart.Height;
                                            BufferList.Add(new GridPart(bufpart.X, bufpart.Y));
                                            bufpart.X -= GridPart.Width;
                                            BufferList.Add(new GridPart(bufpart.X, bufpart.Y));
                                        }
                                    }
                                }
                            }
                        }
                        while (ScanGrid.X > RedGrid.X - GridPart.Width * i)
                        {
                            ScanGrid.X -= GridPart.Width;
                            foreach (var SearchGrid in EpicenterGrid[1])
                            {
                                if ((ScanGrid.X == SearchGrid.X) && (ScanGrid.Y == SearchGrid.Y))
                                {
                                    List<string> Parameters = new List<string>();
                                    GridPart bufpart = new GridPart(ScanGrid.X, ScanGrid.Y);
                                    while (((bufpart.X == RedGrid.X) && (bufpart.Y == RedGrid.Y)) == false)
                                    {

                                        if (bufpart.X == RedGrid.X)
                                        {
                                            bufpart.Y -= GridPart.Height;
                                            BufferList.Add(new GridPart(bufpart.X, bufpart.Y));
                                        }
                                        else
                                        if (bufpart.Y == RedGrid.Y)
                                        {
                                            bufpart.X += GridPart.Width;
                                            BufferList.Add(new GridPart(bufpart.X, bufpart.Y));
                                        }
                                        else
                                        {
                                            bufpart.Y -= GridPart.Height;
                                            BufferList.Add(new GridPart(bufpart.X, bufpart.Y));
                                            bufpart.X += GridPart.Width;
                                            BufferList.Add(new GridPart(bufpart.X, bufpart.Y));
                                        }
                                    }
                                }
                            }
                        }
                        while (ScanGrid.Y > RedGrid.Y)
                        {
                            ScanGrid.Y -= GridPart.Height;
                            foreach (var SearchGrid in EpicenterGrid[1])
                            {
                                if ((ScanGrid.X == SearchGrid.X) && (ScanGrid.Y == SearchGrid.Y))
                                {
                                    List<string> Parameters = new List<string>();
                                    GridPart bufpart = new GridPart(ScanGrid.X, ScanGrid.Y);
                                    while (((bufpart.X == RedGrid.X) && (bufpart.Y == RedGrid.Y)) == false)
                                    {
                                        if (bufpart.X == RedGrid.X)
                                        {
                                            bufpart.Y -= GridPart.Height;
                                            BufferList.Add(new GridPart(bufpart.X, bufpart.Y));
                                        }
                                        else
                                        if (bufpart.Y == RedGrid.Y)
                                        {
                                            bufpart.X += GridPart.Width;
                                            BufferList.Add(new GridPart(bufpart.X, bufpart.Y));
                                        }
                                        else
                                        {
                                            bufpart.Y -= GridPart.Height;
                                            BufferList.Add(new GridPart(bufpart.X, bufpart.Y));
                                            bufpart.X += GridPart.Width;
                                            BufferList.Add(new GridPart(bufpart.X, bufpart.Y));
                                        }
                                    }
                                }
                            }
                        }
                        while (ScanGrid.Y > RedGrid.Y - GridPart.Width * i)
                        {
                            ScanGrid.Y -= GridPart.Height;
                            foreach (var SearchGrid in EpicenterGrid[1])
                            {
                                if ((ScanGrid.X == SearchGrid.X) && (ScanGrid.Y == SearchGrid.Y))
                                {
                                    GridPart bufpart = new GridPart(ScanGrid.X, ScanGrid.Y);
                                    while (((bufpart.X == RedGrid.X) && (bufpart.Y == RedGrid.Y)) == false)
                                    {
                                        if (bufpart.X == RedGrid.X)
                                        {
                                            bufpart.Y += GridPart.Height;
                                            BufferList.Add(new GridPart(bufpart.X, bufpart.Y));
                                        }
                                        else
                                        if (bufpart.Y == RedGrid.Y)
                                        {
                                            bufpart.X += GridPart.Width;
                                            BufferList.Add(new GridPart(bufpart.X, bufpart.Y));
                                        }
                                        else
                                        {
                                            bufpart.Y += GridPart.Height;
                                            BufferList.Add(new GridPart(bufpart.X, bufpart.Y));
                                            bufpart.X += GridPart.Width;
                                            BufferList.Add(new GridPart(bufpart.X, bufpart.Y));
                                        }
                                    }
                                }
                            }
                        }
                        while (ScanGrid.X < RedGrid.X)
                        {
                            ScanGrid.X += GridPart.Width;
                            foreach (var SearchGrid in EpicenterGrid[1])
                            {
                                if ((ScanGrid.X == SearchGrid.X) && (ScanGrid.Y == SearchGrid.Y))
                                {
                                    List<string> Parameters = new List<string>();
                                    Parameters = EpicenterGenerator(ScanGrid, Parameters);
                                    GridPart bufpart = new GridPart(ScanGrid.X, ScanGrid.Y);
                                    while (((bufpart.X == RedGrid.X) && (bufpart.Y == RedGrid.Y)) == false)
                                    {

                                        if (bufpart.X == RedGrid.X)
                                        {
                                            bufpart.Y += GridPart.Height;
                                            BufferList.Add(new GridPart(bufpart.X, bufpart.Y));
                                        }
                                        else
                                        if (bufpart.Y == RedGrid.Y)
                                        {
                                            bufpart.X += GridPart.Width;
                                            BufferList.Add(new GridPart(bufpart.X, bufpart.Y));
                                        }
                                        else
                                        {
                                            bufpart.Y += GridPart.Height;
                                            BufferList.Add(new GridPart(bufpart.X, bufpart.Y));
                                            bufpart.X += GridPart.Width;
                                            BufferList.Add(new GridPart(bufpart.X, bufpart.Y));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            foreach (var BufGrid in BufferList)
            {
                bool net = false;
                foreach (var EpGrid in EpicenterGrid[1])
                {
                    if ((BufGrid.X == EpGrid.X) && (BufGrid.Y == EpGrid.Y))
                    {
                        net = true;
                        break;
                    }
                }
                if (net == false)
                {
                    EpicenterGrid[1].Add(new GridPart(BufGrid.X, BufGrid.Y));
                }
            }
            for (int i = 2; i < 4; i++)
            {
                List<GridPart> fillEpicenter = new List<GridPart>();
                foreach (var item in EpicenterGrid[i - 1])
                {
                    fillEpicenter.Add(new GridPart(item.X, item.Y));
                }
                foreach (var itwms in fillEpicenter)
                {
                    List<string> Parameter = new List<string>();
                    Parameter = EpicenterGenerator(itwms, Parameter);
                    if (Parameter.Count > 0)
                        foreach (var items in Parameter)
                        {
                            Creater(items, itwms, i);
                        }
                }
                fillEpicenter = new List<GridPart>();
                foreach (var item in EpicenterGrid[i])
                {
                    fillEpicenter.Add(new GridPart(item.X, item.Y));
                }
                foreach (var itwms in fillEpicenter)
                {
                    List<string> Parameter = new List<string>();
                    Parameter = EpicenterGenerator(itwms, Parameter);
                    if (Parameter.Count > 0)
                        foreach (var items in Parameter)
                        {
                            Creater(items, itwms, i);
                        }
                }
                if (i == 2)
                {
                    foreach (var BufGrid in OrangeBuffer)
                    {
                        bool net = false;
                        foreach (var EpGrid in EpicenterGrid[i])
                        {
                            if ((BufGrid.X == EpGrid.X) && (BufGrid.Y == EpGrid.Y))
                            {
                                net = true;
                                break;
                            }
                        }
                        if (net == false)
                        {
                            int? Red = null;
                            foreach (var EpGrid in EpicenterGrid[1])
                            {
                                if ((BufGrid.X == EpGrid.X) && (BufGrid.Y == EpGrid.Y))
                                {
                                    Red = EpicenterGrid[1].IndexOf(EpGrid);
                                    break;
                                }
                            }
                            if (Red != null)
                            {
                                EpicenterGrid[1].RemoveAt((int)Red);
                                EpicenterGrid[i].Add(new GridPart(BufGrid.X, BufGrid.Y));
                            }
                            else
                            {
                                EpicenterGrid[i].Add(new GridPart(BufGrid.X, BufGrid.Y));
                            }
                        }
                    }
                }
            }
        }
        public void EpicMoving(List<string> Parameters)
        {
            for (int i = 2; i < EpicenterGrid.Count + 1; i++)
            {
                EpicenterGrid[i].Clear();
            }
            List<Point> ForRemove = new List<Point>();
            foreach (var gridPart in EpicenterGrid[1])
            {
                gridPart.IsMovedAway = false;
                foreach (var item in Parameters)
                {
                    if (gridPart.IsMovedAway == false)
                        switch (item)
                        {
                            case "right":
                                gridPart.X += GridPart.Width;
                                if (!(gridPart.X <= TheGrid.Last().X))
                                {
                                    gridPart.IsMovedAway = true;
                                    ForRemove.Add(new Point(gridPart.X, gridPart.Y));
                                }
                                break;
                            case "down":
                                gridPart.Y += GridPart.Height;
                                if (!(gridPart.Y <= TheGrid.Last().Y))
                                {
                                    gridPart.IsMovedAway = true;
                                    ForRemove.Add(new Point(gridPart.X, gridPart.Y));
                                }
                                break;
                            case "up":
                                gridPart.Y -= GridPart.Height;
                                if (!(gridPart.Y >= TheGrid.First().Y))
                                {
                                    gridPart.IsMovedAway = true;
                                    ForRemove.Add(new Point(gridPart.X, gridPart.Y));
                                }
                                break;
                            case "left":
                                gridPart.X -= GridPart.Width;
                                if (!((gridPart.X >= TheGrid.First().X)))
                                {
                                    gridPart.IsMovedAway = true;
                                    ForRemove.Add(new Point(gridPart.X, gridPart.Y));
                                }
                                break;
                            default:
                                break;
                        }
                }
            }
            foreach (var OutMovedGrid in ForRemove)
            {
                int IndexOfOutMovedGrid = 0;
                foreach (var gridPart in EpicenterGrid[1])
                {
                    if ((gridPart.X == OutMovedGrid.X) && (gridPart.Y == OutMovedGrid.Y))
                    {
                        IndexOfOutMovedGrid = EpicenterGrid[1].IndexOf(gridPart);
                        if (NewExpandCount.Any())
                        {
                            NewExpandCount.Remove(NewExpandCount.Last());
                        }
                        break;
                    }
                }
                EpicenterGrid[1].RemoveAt(IndexOfOutMovedGrid);
            }
            List<string> Parameter;
            for (int i = 2; i < 4; i++)
            {
                List<GridPart> fillEpicenter = new List<GridPart>();
                foreach (var item in EpicenterGrid[i - 1])
                {
                    fillEpicenter.Add(new GridPart(item.X, item.Y));
                }
                foreach (var itwms in fillEpicenter)
                {
                    Parameter = new List<string>();
                    Parameter = EpicenterGenerator(itwms, Parameter);
                    if (Parameter.Count > 0)
                        foreach (var items in Parameter)
                        {
                            Creater(items, itwms, i);
                        }
                }
                fillEpicenter = new List<GridPart>();
                foreach (var item in EpicenterGrid[i])
                {
                    fillEpicenter.Add(new GridPart(item.X, item.Y));
                }
                foreach (var itwms in fillEpicenter)
                {
                    Parameter = new List<string>();
                    Parameter = EpicenterGenerator(itwms, Parameter);
                    if (Parameter.Count > 0)
                        foreach (var items in Parameter)
                        {
                            Creater(items, itwms, i);
                        }
                }
            }
        }
        public List<int> NewExpandCount { get; set; } = new List<int>();
        public Point StartPositon { get => startPositon; set => startPositon = value; }

        public void ExpandEpic()
        {
            for (int i = 2; i < EpicenterGrid.Count + 1; i++)
            {
                EpicenterGrid[i].Clear();
            }
            var rand = new Random();

            for (int i = 0; i < 3; i++)
            {
                List<string> Parameter = new List<string>();
                GridPart StarterEpicPart = new GridPart(StartPositon.X, StartPositon.Y);
                Parameter = EpicenterGenerator(StarterEpicPart, Parameter);
                if (Parameter.Count > 0)
                {
                    NewExpandCount.Clear();
                    foreach (var item in Parameter)
                    {
                        Creater(item, StarterEpicPart, 1);
                        NewExpandCount.Add(EpicenterGrid[1].IndexOf(EpicenterGrid[1].Last()));
                    }
                }
                else
                {
                    if (NewExpandCount.Any())
                    {
                        StarterEpicPart = EpicenterGrid[1][rand.Next(NewExpandCount.First(), NewExpandCount.Last())];
                        Parameter = EpicenterGenerator(StarterEpicPart, Parameter);
                        foreach (var item in Parameter)
                        {
                            Creater(item, StarterEpicPart, 1);
                            NewExpandCount.Add(EpicenterGrid[1].IndexOf(EpicenterGrid[1].Last()));
                        }
                    }
                    else
                    {
                        StarterEpicPart = EpicenterGrid[1][rand.Next(EpicenterGrid[1].Count / 8, EpicenterGrid[1].IndexOf(EpicenterGrid[1].Last()))];
                        Parameter = EpicenterGenerator(StarterEpicPart, Parameter);
                        foreach (var item in Parameter)
                        {
                            Creater(item, StarterEpicPart, 1);
                        }
                    }
                }
            }
            for (int i = 2; i < 4; i++)
            {
                List<GridPart> fillEpicenter = new List<GridPart>();
                foreach (var item in EpicenterGrid[i - 1])
                {
                    fillEpicenter.Add(new GridPart(item.X, item.Y));
                }
                foreach (var itwms in fillEpicenter)
                {
                    List<string> Parameter = new List<string>();
                    Parameter = EpicenterGenerator(itwms, Parameter);
                    if (Parameter.Count > 0)
                        foreach (var items in Parameter)
                        {
                            Creater(items, itwms, i);
                        }
                }
                fillEpicenter = new List<GridPart>();
                foreach (var item in EpicenterGrid[i])
                {
                    fillEpicenter.Add(new GridPart(item.X, item.Y));
                }
                foreach (var itwms in fillEpicenter)
                {
                    List<string> Parameter = new List<string>();
                    Parameter = EpicenterGenerator(itwms, Parameter);
                    if (Parameter.Count > 0)
                        foreach (var items in Parameter)
                        {
                            Creater(items, itwms, i);
                        }
                }
            }
        }
        public void CreateRandomEpicenter(int SizeParam, int? StartPos)
        {
            var rand = new Random();
            EpicenterGrid = new SerializableDictionary<int, List<GridPart>>
            {
                { 1, new List<GridPart>() }
            };
            if (StartPos == null)
            {
                EpicenterGrid[1].Add(new GridPart(TheGrid[rand.Next(TheGrid.IndexOf(TheGrid.First()), TheGrid.IndexOf(TheGrid.Last()))].X, TheGrid[rand.Next(TheGrid.IndexOf(TheGrid.First()), TheGrid.IndexOf(TheGrid.Last()))].Y));
                StartPositon = new Point(EpicenterGrid[1].First().X, EpicenterGrid[1].First().Y);
            }
            else
            {
                EpicenterGrid[1].Add(new GridPart(TheGrid[(int)StartPos].X, TheGrid[(int)StartPos].Y));
                StartPositon = new Point(EpicenterGrid[1].First().X, EpicenterGrid[1].First().Y);
            }
            while (EpicenterGrid[1].Count < SizeParam)
            {
                List<string> Parameter = new List<string>();
                GridPart StarterEpicPart = EpicenterGrid[1][rand.Next(EpicenterGrid[1].IndexOf(EpicenterGrid[1].First()), EpicenterGrid[1].IndexOf(EpicenterGrid[1].Last()))];
                Parameter = EpicenterGenerator(StarterEpicPart, Parameter);
                if (Parameter.Count > 0)
                    foreach (var item in Parameter)
                    {
                        if (EpicenterGrid[1].Count < SizeParam)
                        {
                            Creater(item, StarterEpicPart, 1);
                        }
                    }
            }
            for (int i = 2; i < 4; i++)
            {
                EpicenterGrid.Add(i, new List<GridPart>());
                List<GridPart> FillEpicenter = new List<GridPart>();
                foreach (var item in EpicenterGrid[i - 1])
                {
                    FillEpicenter.Add(new GridPart(item.X, item.Y));
                }
                foreach (var itwms in FillEpicenter)
                {
                    List<string> Parameter = new List<string>();
                    Parameter = EpicenterGenerator(itwms, Parameter);
                    if (Parameter.Count > 0)
                        foreach (var items in Parameter)
                        {
                            Creater(items, itwms, i);
                        }
                }
                FillEpicenter = new List<GridPart>();
                foreach (var item in EpicenterGrid[i])
                {
                    FillEpicenter.Add(new GridPart(item.X, item.Y));
                }
                foreach (var itwms in FillEpicenter)
                {
                    List<string> Parameter = new List<string>();
                    Parameter = EpicenterGenerator(itwms, Parameter);
                    if (Parameter.Count > 0)
                        foreach (var items in Parameter)
                        {
                            Creater(items, itwms, i);
                        }
                }
            }
        }
        public List<string> EpicenterGenerator(GridPart EpicPart, List<string> Parameter)
        {
            bool EmptyCellCheck;
            if (EpicPart.X < TheGrid.Last().X)
            {
                EmptyCellCheck = false;
                for (int d = 1; d < EpicenterGrid.Count + 1; d++)
                    foreach (var part in EpicenterGrid[d])
                    {
                        if ((part.X == EpicPart.X + GridPart.Width) && (part.Y == EpicPart.Y))
                        {
                            EmptyCellCheck = true;
                            break;
                        }
                    }
                if (EmptyCellCheck == false)
                {
                    Parameter.Add("right");
                }

            }
            if ((EpicPart.X < TheGrid.Last().X) && (EpicPart.Y < TheGrid.Last().Y))
            {
                EmptyCellCheck = false;
                for (int d = 1; d < EpicenterGrid.Count + 1; d++)
                    foreach (var part in EpicenterGrid[d])
                    {
                        if ((part.X == EpicPart.X + GridPart.Width) && (part.Y == EpicPart.Y + GridPart.Height))
                        {
                            EmptyCellCheck = true;
                            break;
                        }
                    }
                if (EmptyCellCheck == false)
                {
                    Parameter.Add("right-down");
                }

            }
            if ((EpicPart.X < TheGrid.Last().X) && (EpicPart.Y > TheGrid.First().Y))
            {
                EmptyCellCheck = false;
                for (int d = 1; d < EpicenterGrid.Count + 1; d++)
                    foreach (var part in EpicenterGrid[d])
                    {
                        if ((part.X == EpicPart.X + GridPart.Width) && (part.Y == EpicPart.Y - GridPart.Height))
                        {
                            EmptyCellCheck = true;
                            break;
                        }
                    }
                if (EmptyCellCheck == false)
                {
                    Parameter.Add("right-up");
                }
            }
            if (EpicPart.Y < TheGrid.Last().Y)
            {
                EmptyCellCheck = false;
                for (int d = 1; d < EpicenterGrid.Count + 1; d++)
                    foreach (var part in EpicenterGrid[d])
                    {
                        if ((part.X == EpicPart.X) && (part.Y == EpicPart.Y + GridPart.Height))
                        {
                            EmptyCellCheck = true;
                            break;
                        }
                    }
                if (EmptyCellCheck == false)
                {
                    Parameter.Add("down");
                }
            }
            if (EpicPart.Y > TheGrid.First().Y)
            {
                EmptyCellCheck = false;
                for (int d = 1; d < EpicenterGrid.Count + 1; d++)
                    foreach (var part in EpicenterGrid[d])
                    {
                        if ((part.X == EpicPart.X) && (part.Y == EpicPart.Y - GridPart.Height))
                        {
                            EmptyCellCheck = true;
                            break;
                        }
                    }
                if (EmptyCellCheck == false)
                {
                    Parameter.Add("up");
                }
            }
            if (EpicPart.X > TheGrid.First().X)
            {
                EmptyCellCheck = false;
                for (int d = 1; d < EpicenterGrid.Count + 1; d++)
                    foreach (var part in EpicenterGrid[d])
                    {
                        if ((part.X == EpicPart.X - GridPart.Width) && (part.Y == EpicPart.Y))
                        {
                            EmptyCellCheck = true;
                            break;
                        }
                    }
                if (EmptyCellCheck == false)
                {
                    Parameter.Add("left");
                }
            }
            if ((EpicPart.X > TheGrid.First().X) && (EpicPart.Y > TheGrid.First().Y))
            {
                EmptyCellCheck = false;
                for (int d = 1; d < EpicenterGrid.Count + 1; d++)
                    foreach (var part in EpicenterGrid[d])
                    {
                        if ((part.X == EpicPart.X - GridPart.Width) && (part.Y == EpicPart.Y - GridPart.Height))
                        {
                            EmptyCellCheck = true;
                            break;
                        }
                    }
                if (EmptyCellCheck == false)
                {
                    Parameter.Add("left-up");
                }
            }
            if ((EpicPart.X > TheGrid.First().X) && (EpicPart.Y < TheGrid.Last().Y))
            {
                EmptyCellCheck = false;
                for (int d = 1; d < EpicenterGrid.Count + 1; d++)
                    foreach (var part in EpicenterGrid[d])
                    {
                        if ((part.X == EpicPart.X - GridPart.Width) && (part.Y == EpicPart.Y + GridPart.Height))
                        {
                            EmptyCellCheck = true;
                            break;
                        }
                    }
                if (EmptyCellCheck == false)
                {
                    Parameter.Add("left-down");
                }
            }
            return Parameter;
        }
        public void Creater(string Param, GridPart StarterEpicPart, int level)
        {
            switch (Param)
            {
                case "right":
                    EpicenterGrid[level].Add(new GridPart(StarterEpicPart.X + GridPart.Width, StarterEpicPart.Y));
                    break;
                case "right-down":
                    EpicenterGrid[level].Add(new GridPart(StarterEpicPart.X + GridPart.Width, StarterEpicPart.Y + GridPart.Height));
                    break;
                case "right-up":
                    EpicenterGrid[level].Add(new GridPart(StarterEpicPart.X + GridPart.Width, StarterEpicPart.Y - GridPart.Height));
                    break;
                case "down":
                    EpicenterGrid[level].Add(new GridPart(StarterEpicPart.X, StarterEpicPart.Y + GridPart.Height));
                    break;
                case "up":
                    EpicenterGrid[level].Add(new GridPart(StarterEpicPart.X, StarterEpicPart.Y - GridPart.Height));
                    break;
                case "left":
                    EpicenterGrid[level].Add(new GridPart(StarterEpicPart.X - GridPart.Width, StarterEpicPart.Y));
                    break;
                case "left-down":
                    EpicenterGrid[level].Add(new GridPart(StarterEpicPart.X - GridPart.Width, StarterEpicPart.Y + GridPart.Height));
                    break;
                case "left-up":
                    EpicenterGrid[level].Add(new GridPart(StarterEpicPart.X - GridPart.Width, StarterEpicPart.Y - GridPart.Height));
                    break;
                default:
                    break;
            }
        }
        public void DrawEpicenter(DrawGraph g, int zoom)
        {
            for (int i = 1; i < EpicenterGrid.Count + 1; i++)
            {
                for (int j = 0; j < EpicenterGrid[i].Count; j++)
                {
                    if (i == 1)
                        g.Gr.FillRectangle(new SolidBrush(Color.FromArgb(80, 255, 0, 0)), new Rectangle(EpicenterGrid[i][j].X * zoom, EpicenterGrid[i][j].Y * zoom, GridPart.Width * zoom, GridPart.Height * zoom));
                    if (i == 2)
                        g.Gr.FillRectangle(new SolidBrush(Color.FromArgb(80, 255, 128, 0)), new Rectangle(EpicenterGrid[i][j].X * zoom, EpicenterGrid[i][j].Y * zoom, GridPart.Width * zoom, GridPart.Height * zoom));
                    if (i == 3)
                        g.Gr.FillRectangle(new SolidBrush(Color.FromArgb(80, 255, 255, 0)), new Rectangle(EpicenterGrid[i][j].X * zoom, EpicenterGrid[i][j].Y * zoom, GridPart.Width * zoom, GridPart.Height * zoom));
                }
            }
        }
    }
}
