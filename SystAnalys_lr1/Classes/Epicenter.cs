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
        public static void CreateOneRandomEpicenter(int EpicSizeParam, int? StartPos)
        {           
            Data.Epics = new List<Epicenter>
            {
                new Epicenter(Data.TheGrid)
            };
            Data.Epics.First().CreateRandomEpicenter(EpicSizeParam, StartPos);
        }
        public SerializableDictionary<int, List<GridPart>> EpicenterGrid { get; set; }
        public Point StartPositon;
       
        public int DetectCount { get; set; }

        public List<GridPart> TheGrid { get; set; }

        public Epicenter(List<GridPart> TheGrid)
        {
            this.TheGrid = TheGrid;
            EpicenterGrid = new SerializableDictionary<int, List<GridPart>>();
        }

        public Epicenter()
        { }

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
                                    if ((part.x == PollutedSqure.x) && (part.y == PollutedSqure.y))
                                    {
                                        net = true;
                                        break;
                                    }
                                }
                                if (net == false)
                                {
                                    OrangeBuffer.Add(new GridPart(PollutedSqure.x, PollutedSqure.y));
                                }
                                break;
                            case 3:
                                foreach (var part in EpicenterGrid[1])
                                {
                                    if ((part.x == PollutedSqure.x) && (part.y == PollutedSqure.y))
                                    {
                                        net = true;
                                        break;
                                    }
                                }
                                if (net == false)
                                {
                                    EpicenterGrid[1].Add(new GridPart(PollutedSqure.x, PollutedSqure.y));
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
                    GridPart ScanGrid = new GridPart(RedGrid.x, RedGrid.y - GridPart.Height * i);
                    for (int j = 0; j < 4; j++)
                    {
                        while (ScanGrid.x < RedGrid.x + GridPart.Width * i)
                        {
                            ScanGrid.x += GridPart.Width;
                            foreach (var SearchGrid in EpicenterGrid[1])
                            {
                                if ((ScanGrid.x == SearchGrid.x) && (ScanGrid.y == SearchGrid.y))
                                {

                                    List<string> Parameters = new List<string>();
                                    GridPart bufpart = new GridPart(ScanGrid.x, ScanGrid.y);
                                    while (((bufpart.x == RedGrid.x) && (bufpart.y == RedGrid.y)) == false)
                                    {

                                        if (bufpart.x == RedGrid.x)
                                        {
                                            bufpart.y += GridPart.Height;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }
                                        else
                                        if (bufpart.y == RedGrid.y)
                                        {
                                            bufpart.x -= GridPart.Width;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }
                                        else
                                        {
                                            bufpart.y += GridPart.Height;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                            bufpart.x -= GridPart.Width;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }
                                    }
                                }
                            }
                        }
                        while (ScanGrid.y < RedGrid.y)
                        {
                            ScanGrid.y += GridPart.Height;
                            foreach (var SearchGrid in EpicenterGrid[1])
                            {
                                if ((ScanGrid.x == SearchGrid.x) && (ScanGrid.y == SearchGrid.y))
                                {
                                    GridPart bufpart = new GridPart(ScanGrid.x, ScanGrid.y);
                                    while (((bufpart.x == RedGrid.x) && (bufpart.y == RedGrid.y)) == false)
                                    {

                                        if (bufpart.x == RedGrid.x)
                                        {
                                            bufpart.y += GridPart.Height;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }
                                        else
                                        if (bufpart.y == RedGrid.y)
                                        {
                                            bufpart.x -= GridPart.Width;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }
                                        else
                                        {
                                            bufpart.x -= GridPart.Width;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                            bufpart.y += GridPart.Height;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }

                                    }
                                }
                            }
                        }
                        while (ScanGrid.y < RedGrid.y + GridPart.Height * i)
                        {
                            ScanGrid.y += GridPart.Height;
                            foreach (var SearchGrid in EpicenterGrid[1])
                            {
                                if ((ScanGrid.x == SearchGrid.x) && (ScanGrid.y == SearchGrid.y))
                                {
                                    List<string> Parameters = new List<string>();
                                    GridPart bufpart = new GridPart(ScanGrid.x, ScanGrid.y);
                                    while (((bufpart.x == RedGrid.x) && (bufpart.y == RedGrid.y)) == false)
                                    {

                                        if (bufpart.x == RedGrid.x)
                                        {
                                            bufpart.y -= GridPart.Height;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }
                                        else
                                        if (bufpart.y == RedGrid.y)
                                        {
                                            bufpart.x -= GridPart.Width;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }
                                        else
                                        {
                                            bufpart.y -= GridPart.Height;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                            bufpart.x -= GridPart.Width;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }

                                    }
                                }
                            }
                        }
                        while (ScanGrid.x > RedGrid.x)
                        {
                            ScanGrid.x -= GridPart.Width;
                            foreach (var SearchGrid in EpicenterGrid[1])
                            {
                                if ((ScanGrid.x == SearchGrid.x) && (ScanGrid.y == SearchGrid.y))
                                {
                                    GridPart bufpart = new GridPart(ScanGrid.x, ScanGrid.y);
                                    while (((bufpart.x == RedGrid.x) && (bufpart.y == RedGrid.y)) == false)
                                    {

                                        if (bufpart.x == RedGrid.x)
                                        {
                                            bufpart.y -= GridPart.Height;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }
                                        else
                                        if (bufpart.y == RedGrid.y)
                                        {
                                            bufpart.x -= GridPart.Width;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }
                                        else
                                        {
                                            bufpart.y -= GridPart.Height;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                            bufpart.x -= GridPart.Width;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }

                                    }
                                }
                            }
                        }
                        while (ScanGrid.x > RedGrid.x - GridPart.Width * i)
                        {
                            ScanGrid.x -= GridPart.Width;
                            foreach (var SearchGrid in EpicenterGrid[1])
                            {
                                if ((ScanGrid.x == SearchGrid.x) && (ScanGrid.y == SearchGrid.y))
                                {
                                    List<string> Parameters = new List<string>();
                                    GridPart bufpart = new GridPart(ScanGrid.x, ScanGrid.y);
                                    while (((bufpart.x == RedGrid.x) && (bufpart.y == RedGrid.y)) == false)
                                    {

                                        if (bufpart.x == RedGrid.x)
                                        {
                                            bufpart.y -= GridPart.Height;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }
                                        else
                                        if (bufpart.y == RedGrid.y)
                                        {
                                            bufpart.x += GridPart.Width;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }
                                        else
                                        {
                                            bufpart.y -= GridPart.Height;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                            bufpart.x += GridPart.Width;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }

                                    }
                                }
                            }
                        }
                        while (ScanGrid.y > RedGrid.y)
                        {
                            ScanGrid.y -= GridPart.Height;
                            foreach (var SearchGrid in EpicenterGrid[1])
                            {
                                if ((ScanGrid.x == SearchGrid.x) && (ScanGrid.y == SearchGrid.y))
                                {
                                    List<string> Parameters = new List<string>();
                                    GridPart bufpart = new GridPart(ScanGrid.x, ScanGrid.y);
                                    while (((bufpart.x == RedGrid.x) && (bufpart.y == RedGrid.y)) == false)
                                    {

                                        if (bufpart.x == RedGrid.x)
                                        {
                                            bufpart.y -= GridPart.Height;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }
                                        else
                                        if (bufpart.y == RedGrid.y)
                                        {
                                            bufpart.x += GridPart.Width;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }
                                        else
                                        {
                                            bufpart.y -= GridPart.Height;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                            bufpart.x += GridPart.Width;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }

                                    }
                                }
                            }
                        }
                        while (ScanGrid.y > RedGrid.y - GridPart.Width * i)
                        {
                            ScanGrid.y -= GridPart.Height;
                            foreach (var SearchGrid in EpicenterGrid[1])
                            {
                                if ((ScanGrid.x == SearchGrid.x) && (ScanGrid.y == SearchGrid.y))
                                {

                                    GridPart bufpart = new GridPart(ScanGrid.x, ScanGrid.y);
                                    while (((bufpart.x == RedGrid.x) && (bufpart.y == RedGrid.y)) == false)
                                    {

                                        if (bufpart.x == RedGrid.x)
                                        {
                                            bufpart.y += GridPart.Height;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }
                                        else
                                        if (bufpart.y == RedGrid.y)
                                        {
                                            bufpart.x += GridPart.Width;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }
                                        else
                                        {
                                            bufpart.y += GridPart.Height;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                            bufpart.x += GridPart.Width;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }

                                    }
                                }
                            }
                        }
                        while (ScanGrid.x < RedGrid.x)
                        {
                            ScanGrid.x += GridPart.Width;
                            foreach (var SearchGrid in EpicenterGrid[1])
                            {
                                if ((ScanGrid.x == SearchGrid.x) && (ScanGrid.y == SearchGrid.y))
                                {
                                    List<string> Parameters = new List<string>();
                                    Parameters = EpicenterGenerator(ScanGrid, Parameters);
                                    GridPart bufpart = new GridPart(ScanGrid.x, ScanGrid.y);
                                    while (((bufpart.x == RedGrid.x) && (bufpart.y == RedGrid.y)) == false)
                                    {

                                        if (bufpart.x == RedGrid.x)
                                        {
                                            bufpart.y += GridPart.Height;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }
                                        else
                                        if (bufpart.y == RedGrid.y)
                                        {
                                            bufpart.x += GridPart.Width;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                        }
                                        else
                                        {
                                            bufpart.y += GridPart.Height;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
                                            bufpart.x += GridPart.Width;
                                            BufferList.Add(new GridPart(bufpart.x, bufpart.y));
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
                    if ((BufGrid.x == EpGrid.x) && (BufGrid.y == EpGrid.y))
                    {
                        net = true;
                        break;
                    }

                }
                if (net == false)
                {
                    EpicenterGrid[1].Add(new GridPart(BufGrid.x, BufGrid.y));
                }
            }
            for (int i = 2; i < 4; i++)
            {

                List<GridPart> fillEpicenter = new List<GridPart>();
                foreach (var item in EpicenterGrid[i - 1])
                {
                    fillEpicenter.Add(new GridPart(item.x, item.y));
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
                    fillEpicenter.Add(new GridPart(item.x, item.y));
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
                            if ((BufGrid.x == EpGrid.x) && (BufGrid.y == EpGrid.y))
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
                                if ((BufGrid.x == EpGrid.x) && (BufGrid.y == EpGrid.y))
                                {
                                    Red = EpicenterGrid[1].IndexOf(EpGrid);
                                    break;
                                }

                            }
                            if (Red != null)
                            {
                                EpicenterGrid[1].RemoveAt((int)Red);
                                EpicenterGrid[i].Add(new GridPart(BufGrid.x, BufGrid.y));
                            }
                            else
                            {
                                EpicenterGrid[i].Add(new GridPart(BufGrid.x, BufGrid.y));
                            }

                        }
                    }
                }

            }
        }
        ////////
        public void EpicMoving(List<string> Parameters)
        {
            for (int i = 2; i < EpicenterGrid.Count + 1; i++)
            {

                EpicenterGrid[i].Clear();

            }

            //EpicenterGrid[1].RemoveAt(EpicenterGrid[1].IndexOf(EpicenterGrid[1].Last()));

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
                                gridPart.x += GridPart.Width;
                                if (!(gridPart.x <= TheGrid.Last().x))
                                {
                                    gridPart.IsMovedAway = true;
                                    ForRemove.Add(new Point(gridPart.x, gridPart.y));
                                }
                                break;
                            //case "right-down":
                            //    break;
                            //case "right-up":
                            //    break;
                            case "down":
                                gridPart.y += GridPart.Height;
                                if (!(gridPart.y <= TheGrid.Last().y))
                                {
                                    gridPart.IsMovedAway = true;
                                    ForRemove.Add(new Point(gridPart.x, gridPart.y));
                                }
                                break;
                            case "up":
                                gridPart.y -= GridPart.Height;
                                if (!(gridPart.y >= TheGrid.First().y))
                                {
                                    gridPart.IsMovedAway = true;
                                    ForRemove.Add(new Point(gridPart.x, gridPart.y));
                                }

                                break;
                            case "left":
                                gridPart.x -= GridPart.Width;
                                if (!((gridPart.x >= TheGrid.First().x)))
                                {
                                    gridPart.IsMovedAway = true;
                                    ForRemove.Add(new Point(gridPart.x, gridPart.y));
                                }
                                break;
                            //case "left-down":
                            //    break;
                            //case "left-up":
                            //    break;
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
                    if ((gridPart.x == OutMovedGrid.X) && (gridPart.y == OutMovedGrid.Y))
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
            //
            for (int i = 2; i < 4; i++)
            {

                // EpicenterGrid.Add(i, new List<GridPart>());
                List<GridPart> fillEpicenter = new List<GridPart>();
                foreach (var item in EpicenterGrid[i - 1])
                {
                    fillEpicenter.Add(new GridPart(item.x, item.y));
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
                    fillEpicenter.Add(new GridPart(item.x, item.y));
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
            ///
        }
        //
        public List<int> NewExpandCount { get; set; } = new List<int>();
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
                //GridPart StarterEpicPart = EpicenterGrid[1][rand.Next(EpicenterGrid[1].IndexOf(EpicenterGrid[1].First()), EpicenterGrid[1].IndexOf(EpicenterGrid[1].Last()))];
                GridPart StarterEpicPart = new GridPart(StartPositon.X, StartPositon.Y);
                Parameter = EpicenterGenerator(StarterEpicPart, Parameter);
                if (Parameter.Count > 0)
                {
                    NewExpandCount.Clear();
                    foreach (var item in Parameter)
                    {
                        //if (Parameters.Contains(item))
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
                            //  NewExpandCount.Add(EpicenterGrid[1].IndexOf(EpicenterGrid[1].Last()));
                        }

                    }             
                }
            }
            ////
            for (int i = 2; i < 4; i++)
            {
                // EpicenterGrid.Add(i, new List<GridPart>());
                List<GridPart> fillEpicenter = new List<GridPart>();
                foreach (var item in EpicenterGrid[i - 1])
                {
                    fillEpicenter.Add(new GridPart(item.x, item.y));
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
                    fillEpicenter.Add(new GridPart(item.x, item.y));
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
            ///
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
                EpicenterGrid[1].Add(new GridPart(TheGrid[rand.Next(TheGrid.IndexOf(TheGrid.First()), TheGrid.IndexOf(TheGrid.Last()))].x, TheGrid[rand.Next(TheGrid.IndexOf(TheGrid.First()), TheGrid.IndexOf(TheGrid.Last()))].y));
                StartPositon = new Point(EpicenterGrid[1].First().x, EpicenterGrid[1].First().y);
            }
            else
            {
                EpicenterGrid[1].Add(new GridPart(TheGrid[(int)StartPos].x, TheGrid[(int)StartPos].y));
                StartPositon = new Point(EpicenterGrid[1].First().x, EpicenterGrid[1].First().y);
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
                    FillEpicenter.Add(new GridPart(item.x, item.y));
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
                    FillEpicenter.Add(new GridPart(item.x, item.y));
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
            bool net;
            if (EpicPart.x < TheGrid.Last().x)
            {
                net = false;
                for (int d = 1; d < EpicenterGrid.Count + 1; d++)
                    foreach (var part in EpicenterGrid[d])
                    {
                        if ((part.x == EpicPart.x + GridPart.Width) && (part.y == EpicPart.y))
                        {
                            net = true;
                            break;
                        }
                    }
                if (net == false)
                {
                    Parameter.Add("right");
                }

            }
            if ((EpicPart.x < TheGrid.Last().x) && (EpicPart.y < TheGrid.Last().y))
            {
                net = false;
                for (int d = 1; d < EpicenterGrid.Count + 1; d++)
                    foreach (var part in EpicenterGrid[d])
                    {
                        if ((part.x == EpicPart.x + GridPart.Width) && (part.y == EpicPart.y + GridPart.Height))
                        {
                            net = true;
                            break;
                        }
                    }
                if (net == false)
                {
                    Parameter.Add("right-down");
                }

            }
            if ((EpicPart.x < TheGrid.Last().x) && (EpicPart.y > TheGrid.First().y))
            {
                net = false;
                for (int d = 1; d < EpicenterGrid.Count + 1; d++)
                    foreach (var part in EpicenterGrid[d])
                    {
                        if ((part.x == EpicPart.x + GridPart.Width) && (part.y == EpicPart.y - GridPart.Height))
                        {
                            net = true;
                            break;
                        }
                    }
                if (net == false)
                {
                    Parameter.Add("right-up");
                }

            }
            if (EpicPart.y < TheGrid.Last().y)
            {
                net = false;
                for (int d = 1; d < EpicenterGrid.Count + 1; d++)
                    foreach (var part in EpicenterGrid[d])
                    {
                        if ((part.x == EpicPart.x) && (part.y == EpicPart.y + GridPart.Height))
                        {
                            net = true;
                            break;
                        }
                    }
                if (net == false)
                {
                    Parameter.Add("down");
                }
            }

            if (EpicPart.y > TheGrid.First().y)
            {
                net = false;
                for (int d = 1; d < EpicenterGrid.Count + 1; d++)
                    foreach (var part in EpicenterGrid[d])
                    {
                        if ((part.x == EpicPart.x) && (part.y == EpicPart.y - GridPart.Height))
                        {
                            net = true;
                            break;
                        }
                    }
                if (net == false)
                {
                    Parameter.Add("up");
                }
            }
            if (EpicPart.x > TheGrid.First().x)
            {
                net = false;
                for (int d = 1; d < EpicenterGrid.Count + 1; d++)
                    foreach (var part in EpicenterGrid[d])
                    {
                        if ((part.x == EpicPart.x - GridPart.Width) && (part.y == EpicPart.y))
                        {
                            net = true;
                            break;
                        }
                    }
                if (net == false)
                {
                    Parameter.Add("left");
                }
            }
            if ((EpicPart.x > TheGrid.First().x) && (EpicPart.y > TheGrid.First().y))
            {
                net = false;
                for (int d = 1; d < EpicenterGrid.Count + 1; d++)
                    foreach (var part in EpicenterGrid[d])
                    {
                        if ((part.x == EpicPart.x - GridPart.Width) && (part.y == EpicPart.y - GridPart.Height))
                        {
                            net = true;
                            break;
                        }
                    }
                if (net == false)
                {
                    Parameter.Add("left-up");
                }

            }
            if ((EpicPart.x > TheGrid.First().x) && (EpicPart.y < TheGrid.Last().y))
            {
                net = false;
                for (int d = 1; d < EpicenterGrid.Count + 1; d++)
                    foreach (var part in EpicenterGrid[d])
                    {
                        if ((part.x == EpicPart.x - GridPart.Width) && (part.y == EpicPart.y + GridPart.Height))
                        {
                            net = true;
                            break;
                        }
                    }
                if (net == false)
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
                    EpicenterGrid[level].Add(new GridPart(StarterEpicPart.x + GridPart.Width, StarterEpicPart.y));
                    break;
                case "right-down":
                    EpicenterGrid[level].Add(new GridPart(StarterEpicPart.x + GridPart.Width, StarterEpicPart.y + GridPart.Height));
                    break;
                case "right-up":
                    EpicenterGrid[level].Add(new GridPart(StarterEpicPart.x + GridPart.Width, StarterEpicPart.y - GridPart.Height));
                    break;
                case "down":
                    EpicenterGrid[level].Add(new GridPart(StarterEpicPart.x, StarterEpicPart.y + GridPart.Height));
                    break;
                case "up":
                    EpicenterGrid[level].Add(new GridPart(StarterEpicPart.x, StarterEpicPart.y - GridPart.Height));
                    break;
                case "left":
                    EpicenterGrid[level].Add(new GridPart(StarterEpicPart.x - GridPart.Width, StarterEpicPart.y));
                    break;
                case "left-down":
                    EpicenterGrid[level].Add(new GridPart(StarterEpicPart.x - GridPart.Width, StarterEpicPart.y + GridPart.Height));
                    break;
                case "left-up":
                    EpicenterGrid[level].Add(new GridPart(StarterEpicPart.x - GridPart.Width, StarterEpicPart.y - GridPart.Height));
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
                        g.gr.FillRectangle(new SolidBrush(Color.FromArgb(80, 255, 0, 0)), new Rectangle(EpicenterGrid[i][j].x * zoom, EpicenterGrid[i][j].y * zoom, GridPart.Width * zoom, GridPart.Height * zoom));
                    if (i == 2)
                        g.gr.FillRectangle(new SolidBrush(Color.FromArgb(80, 255, 128, 0)), new Rectangle(EpicenterGrid[i][j].x * zoom, EpicenterGrid[i][j].y * zoom, GridPart.Width * zoom, GridPart.Height * zoom));
                    if (i == 3)
                        g.gr.FillRectangle(new SolidBrush(Color.FromArgb(80, 255, 255, 0)), new Rectangle(EpicenterGrid[i][j].x * zoom, EpicenterGrid[i][j].y * zoom, GridPart.Width * zoom, GridPart.Height * zoom));
                }
            }
        }
    }
}
