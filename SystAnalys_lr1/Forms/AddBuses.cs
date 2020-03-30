//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace SystAnalys_lr1
//{
//    public partial class Main : Form
//    {

//        private void AddBuses()
//        {

//            Bus7_1 = new Bus(route7, pictureBus7_1, 65, false, stop, 7);
//            Bus7_1.Start();

//            Bus7_2 = new Bus(route7, pictureBus7_2, 155, true, stop, 7);
//            Bus7_2.Start();
//            Bus7_3 = new Bus(route7, pictureBox7_3, 1000, false, stop, 7);
//            Bus7_3.Start();
//            Bus7_4 = new Bus(route7, pictureBox1, 343, stop, 7);
//            Bus7_4.Start();
//            Bus7_5 = new Bus(route7, pictureBox2, 546, true, stop, 7);
//            Bus7_5.Start();
//            Bus7_6 = new Bus(route7, pictureBox3, 123, stop, 7);
//            Bus7_6.Start();
//            Bus7_7 = new Bus(route7, pictureBox4, 321, stop, 7);
//            Bus7_7.Start();
//            Bus7_8 = new Bus(route7, pictureBox6, 456, true, stop, 7);
//            Bus7_8.Start();
//            Bus7_9 = new Bus(route7, pictureBox8, 164, stop, 7);
//            Bus7_9.Start();
//            Bus7_10 = new Bus(route7, pictureBox9, 1123, stop, 7);
//            Bus7_10.Start();
//            Bus7_11 = new Bus(route7, pictureBox10, 567, stop, 7);
//            Bus7_11.Start();
//            Bus7_12 = new Bus(route7, pictureBox11, 876, true, stop, 7);
//            Bus7_12.Start();
//            Bus7_13 = new Bus(route7, pictureBox12, 789, stop, 7);
//            Bus7_13.Start();
//            Bus7_14 = new Bus(route7, pictureBox13, 999, stop, 7);
//            Bus7_14.Start();
//            Bus7_15 = new Bus(route7, pictureBox14, 777, stop, 7);
//            Bus7_15.Start();
//            Bus7_16 = new Bus(route7, pictureBox7, 888, true, stop, 7);
//            Bus7_16.Start();

//            Bus23_1 = new Bus(route23, pictureBus23_1, 1, stop, 23);
//            Bus23_1.Start();
//            Bus23_2 = new Bus(route23, pictureBus23_2, 333, stop, 23);
//            Bus23_2.Start();

//            Bus62_1 = new Bus(route62, pictureBus62_1, 444, stop, 62);
//            Bus62_1.Start();


//            Bus_43 = new Bus(route43, pictureBox16, 0, stop, 43);
//            Bus_43.Start();
//            Bus_20 = new Bus(route20, pictureBox17, 0, stop, 20);
//            Bus_20.Start();



//            ////Bus7_1 = new Bus(route7, pictureBus7_1, 100, stop, 7);
//            ////Bus7_1.Start();
//            //Bus7_1 = new Bus(route7, pictureBus7_1, 65, false, stop, 7);
//            ////Bus7_1.Start();
//            //Bus7_2 = new Bus(route7, pictureBus7_2, 155, true, stop, 7);
//            ////Bus7_2.Start();
//            //Bus7_3 = new Bus(route7, pictureBox7_3, 1000, false, stop, 7);
//            ////Bus7_3.Start();
//            //Bus7_4 = new Bus(route7, pictureBox1, 343, stop, 7);
//            ////Bus7_4.Start();
//            //Bus7_5 = new Bus(route7, pictureBox2, 546, true, stop, 7);
//            ////Bus7_5.Start();
//            //Bus7_6 = new Bus(route7, pictureBox3, 123, stop, 7);
//            ////Bus7_6.Start();
//            //Bus7_7 = new Bus(route7, pictureBox4, 321, stop, 7);
//            ////Bus7_7.Start();
//            //Bus7_8 = new Bus(route7, pictureBox6, 456, true, stop, 7);
//            ////Bus7_8.Start();
//            //Bus7_9 = new Bus(route7, pictureBox8, 164, stop, 7);
//            ////Bus7_9.Start();
//            //Bus7_10 = new Bus(route7, pictureBox9, 1123, stop, 7);
//            ////Bus7_10.Start();
//            //Bus7_11 = new Bus(route7, pictureBox10, 567, stop, 7);
//            ////Bus7_11.Start();
//            //Bus7_12 = new Bus(route7, pictureBox11, 876, true, stop, 7);
//            ////Bus7_12.Start();
//            //Bus7_13 = new Bus(route7, pictureBox12, 789, stop, 7);
//            ////Bus7_13.Start();
//            //Bus7_14 = new Bus(route7, pictureBox13, 999, stop, 7);
//            ////Bus7_14.Start();
//            //Bus7_15 = new Bus(route7, pictureBox14, 777, stop, 7);
//            ////Bus7_15.Start();
//            //Bus7_16 = new Bus(route7, pictureBox7, 888, true, stop, 7);
//            ////Bus7_16.Start();

//            //Bus23_1 = new Bus(route23, pictureBus23_1, 1, stop, 23);
//            ////Bus23_1.Start();
//            //Bus23_2 = new Bus(route23, pictureBus23_2, 333, stop, 23);
//            ////Bus23_2.Start();

//            //Bus62_1 = new Bus(route62, pictureBus62_1, 444, stop, 62);
//            ////Bus62_1.Start();


//            //Bus_43 = new Bus(route43, pictureBox16, 0, stop, 43);
//            ////Bus_43.Start();
//            //Bus_20 = new Bus(route20, pictureBox17, 0, stop, 20);
//            ////Bus_20.Start();
//            park7 = new List<Bus>() { Bus7_1, Bus7_2, Bus7_3, Bus7_4, Bus7_5, Bus7_6, Bus7_7, Bus7_8, Bus7_9, Bus7_10, Bus7_11, Bus7_12, Bus7_13, Bus7_14, Bus7_15, Bus7_16 };
//            park23 = new List<Bus>() { Bus23_1, Bus23_2 };
//            park62 = new List<Bus>() { Bus62_1 };

//            park20 = new List<Bus>() { Bus_20 };
//            park43 = new List<Bus>() { Bus_43 };
//            //park107 = new List<Bus>() { Bus_107 };

//            buses = new List<Bus>() { Bus7_1, Bus7_2, Bus7_3, Bus62_1, Bus23_2, Bus7_4, Bus7_5, Bus7_6, Bus7_7, Bus7_8, Bus7_9,
//                Bus7_10, Bus7_11, Bus7_12, Bus7_13, Bus7_14, Bus7_15, Bus7_16, Bus23_1,  Bus_20, Bus_43};
//            busesPark = new List<List<Bus>>() { park7, park23, park62, park20, park43 };

//        }
//    }
//}
