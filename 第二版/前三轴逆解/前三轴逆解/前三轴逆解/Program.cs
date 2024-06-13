using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace 前三轴逆解
{
    class Program
    {
        static void Main(string[] args)
        {
            //x,y定义到达坐标的位置，a1_,a2_,a3_定义当前旋转角，a1,a2,a3定义为最终旋转角
            double x = 0, y = 0;
            double a1_ = 0, a2_ = 0, a3_ = 0;
            double a1 = 0, a2 = 0, a3 = 0;
            Console.Write("请输入目标位置横坐标: ");
            x = double.Parse(Console.ReadLine());
            Console.Write("请输入目标位置纵坐标: ");
            y = double.Parse(Console.ReadLine());
            Console.Write("请输入当前大臂转角: ");
            a1_ = double.Parse(Console.ReadLine());
            Console.Write("请输入当前中臂转角: ");
            a2_ = double.Parse(Console.ReadLine());
            Console.Write("请输入当前小臂转角: ");
            a3_ = double.Parse(Console.ReadLine());

            //大、中、小臂长度
            double l1 = 550, l2 = 450, l3 = 600;


            //辅助变量(x1_,y1_为当前大臂末端位置，pattern为运动模式：0表示无法到达，1表示在一区内，2表示在二区内。)
            //(在pattern =1的情况下l_为中臂旋转轴心到小臂末端的距离，a_为该线段与中臂夹角；在pattern=2的情况下，l_为大臂旋转轴心到小臂末端的距离，a_为该线段与大臂夹角)
            double x1_, y1_, pattern = 0, l_, a_;
            double pi = Math.PI;
            x1_ = Math.Sin(a1_ / 180 * pi) * l1;
            y1_ = Math.Cos(a1_ / 180 * pi) * l1;
            Console.WriteLine("x1_ = " + x1_);
            Console.WriteLine("y1_ = " + y1_);

            //定义偏移量
            double pyx = -100, pyy = 0, pya;
            l3 = l3 + pyy;




            //判断目标点需要用哪种运动方案，方案一为无需移动大臂的情况，方案二为需要移动大臂的情况，方案0为无法到达,方案三为接近基座处可能发生干涉区域
            if ((x - x1_) * (x - x1_) + (y - y1_) * (y - y1_) <= (l2 + l3) * (l2 + l3))//判断条件尚未补全，有碰撞风险，偏移可能考虑不全面
            {
                pattern = 1;
                Console.Write("运动方案一");
            }
            else if (x * x + y * y <= (l1 + l2 + l3) * (l1 + l2 + l3))
            {
                pattern = 2;
                Console.Write("运动方案二");
            }
            else
            {
                pattern = 0;
                Console.Write("无法到达");
            }
            if (Math.Sqrt(x * x + y * y) <= l1 - l2 + Math.Sqrt(l3 * l3 + pyx * pyx))
            {
                pattern = 3;
                Console.Write("改为运动方案三");
            }



            //方案一的解算
            if (pattern == 1)
            {
                a1 = a1_;
                l3 = Math.Sqrt(l3 * l3 + pyx * pyx);
                l_ = Math.Sqrt((x - x1_) * (x - x1_) + (y - y1_) * (y - y1_));
                a_ = Math.Acos((l_ * l_ + l2 * l2 - l3 * l3) / (2 * l_ * l2)) / pi * 180;
                pya = Math.Asin(pyx / l3) / pi * 180;
                a3 = 180 - Math.Acos((l2 * l2 + l3 * l3 - l_ * l_) / (2 * l2 * l3)) / pi * 180;
                //判断采用左手模式还是右手模式
                //右手模式
                if (x >= x1_)
                {
                    Console.Write("右手模式");
                    a2 = Math.Asin((y - y1_) / l_) * 180 / pi + a_ - (90 - a1);
                    a2 = -a2;
                    //添加小臂偏移量
                    a3 = a3 - pya;
                }
                //左手模式
                else
                {
                    Console.Write("左手模式");
                    a2 = a_ - (90 + a1 - Math.Asin((y - y1_) / l_) * 180 / pi);
                    //添加小臂偏移量
                    a3 = a3 + pya;
                    a3 = -a3;
                }


            }

            //方案二的解算
            else if (pattern == 2)
            {
                a3 = 0;
                //辅助变量l（用于计算偏移后虚拟中小臂总长）
                double l;
                l = Math.Sqrt((l2 + l3) * (l2 + l3) + pyx * pyx);

                l_ = Math.Sqrt(x * x + y * y);
                a_ = Math.Acos((l_ * l_ + l1 * l1 - l * l) / (2 * l_ * l1)) / pi * 180;
                pya = Math.Asin(pyx / l) / pi * 180;
                a2 = 180 - Math.Acos((l1 * l1 + l * l - l_ * l_) / (2 * l1 * l)) / pi * 180;
                //判断采用左手模式还是右手模式
                //右手模式
                if (x >= 0)
                {
                    Console.Write("右手模式");
                    a1 = a_ - (90 - Math.Asin(y / l_) * 180 / pi);
                    a1 = -a1;
                    //添加中臂偏移量
                    a2 = a2 - pya;

                }
                //左手模式
                else
                {
                    Console.Write("左手模式");
                    a1 = a_ - (90 - Math.Asin(y / l_) * 180 / pi);
                    //添加中臂偏移量
                    a2 = a2 + pya;
                    a2 = -a2;
                }


            }

            //方案三的解算(其中x1_,y1_均改为了移动后大臂末端位置)
            else if (pattern == 3)
            {
                //目标点位于靠近基座中心区域
                if (Math.Abs(x) <= l3 - l2 + 2 * Math.Abs(pyx))
                {
                    a1 = 90;
                    Console.Write("左手模式");
                    l3 = Math.Sqrt(l3 * l3 + pyx * pyx);
                    x1_ = Math.Sin(a1 / 180 * pi) * l1;
                    y1_ = Math.Cos(a1 / 180 * pi) * l1;
                    l_ = Math.Sqrt((x - x1_) * (x - x1_) + (y - y1_) * (y - y1_));
                    a_ = Math.Acos((l_ * l_ + l2 * l2 - l3 * l3) / (2 * l_ * l2)) / pi * 180;
                    pya = Math.Asin(pyx / l3) / pi * 180;
                    a3 = 180 - Math.Acos((l2 * l2 + l3 * l3 - l_ * l_) / (2 * l2 * l3)) / pi * 180;
                    a2 = a_ - (90 + a1 - Math.Asin((y - y1_) / l_) * 180 / pi);
                    //添加小臂偏移量
                    a3 = a3 + pya;
                    a3 = -a3;
                }
                //目标点位于靠近基座左侧区域
                else if (x > 0)
                {
                    a1 = 0;
                    Console.Write("右手模式");
                    l3 = Math.Sqrt(l3 * l3 + pyx * pyx);
                    x1_ = Math.Sin(a1 / 180 * pi) * l1;
                    y1_ = Math.Cos(a1 / 180 * pi) * l1;
                    l_ = Math.Sqrt((x - x1_) * (x - x1_) + (y - y1_) * (y - y1_));
                    a_ = Math.Acos((l_ * l_ + l2 * l2 - l3 * l3) / (2 * l_ * l2)) / pi * 180;
                    pya = Math.Asin(pyx / l3) / pi * 180;
                    a3 = 180 - Math.Acos((l2 * l2 + l3 * l3 - l_ * l_) / (2 * l2 * l3)) / pi * 180;
                    a2 = Math.Asin((y - y1_) / l_) * 180 / pi + a_ - (90 - a1);
                    a2 = -a2;
                    //添加小臂偏移量
                    a3 = a3 - pya;
                }
                //目标点位于靠近基座右侧区域
                else if (x < 0)
                {
                    a1 = 0;
                    Console.Write("左手模式");
                    l3 = Math.Sqrt(l3 * l3 + pyx * pyx);
                    x1_ = Math.Sin(a1 / 180 * pi) * l1;
                    y1_ = Math.Cos(a1 / 180 * pi) * l1;
                    l_ = Math.Sqrt((x - x1_) * (x - x1_) + (y - y1_) * (y - y1_));
                    a_ = Math.Acos((l_ * l_ + l2 * l2 - l3 * l3) / (2 * l_ * l2)) / pi * 180;
                    pya = Math.Asin(pyx / l3) / pi * 180;
                    a3 = 180 - Math.Acos((l2 * l2 + l3 * l3 - l_ * l_) / (2 * l2 * l3)) / pi * 180;
                    a2 = a_ - (90 + a1 - Math.Asin((y - y1_) / l_) * 180 / pi);
                    //添加小臂偏移量
                    a3 = a3 + pya;
                    a3 = -a3;
                }

            }






            Console.WriteLine();
            Console.WriteLine("大臂所需旋转角度为：" + (a1 - a1_));
            Console.WriteLine("中臂所需旋转角度为：" + (a2 - a2_));
            Console.WriteLine("小臂所需旋转角度为：" + (a3 - a3_));
            Console.WriteLine("旋转后大臂角度为：" + a1);
            Console.WriteLine("旋转后中臂角度为：" + a2);
            Console.WriteLine("旋转后小臂角度为：" + a3);


            //判断三轴中最大的旋转角度
            double max = 0;
            if (Math.Abs(a1 - a1_) >= Math.Abs(a2 - a2_) && Math.Abs(a1 - a1_) >= Math.Abs(a3 - a3_))
            {
                max = Math.Abs(a1 - a1_);
            }
            else if (Math.Abs(a2 - a2_) >= Math.Abs(a1 - a1_) && Math.Abs(a2 - a2_) >= Math.Abs(a3 - a3_))
            {
                max = Math.Abs(a2 - a2_);
            }
            else if (Math.Abs(a3 - a3_) >= Math.Abs(a1 - a1_) && Math.Abs(a3 - a3_) >= Math.Abs(a2 - a2_))
            {
                max = Math.Abs(a3 - a3_);
            }

            int k = 0;
            k = (int)Math.Floor(max / 5) + 1;



            //得出各轴所需旋转角度序列表
            Console.WriteLine();
            Console.WriteLine("大臂转角序列为：");
            List<double> list1 = new List<double>();
            for (int i = 1; i <= k; i++)
            {
                list1.Add((a1_ + (a1 - a1_) / k * i));
                Console.Write((a1_ + (a1 - a1_) / k * i));
                Console.Write(" ");
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("中臂转角序列为：");
            List<double> list2 = new List<double>();
            for (int i = 1; i <= k; i++)
            {
                list2.Add((a2_ + (a2 - a2_) / k * i));
                Console.Write((a2_ + (a2 - a2_) / k * i));
                Console.Write(" ");
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("小臂转角序列为：");
            List<double> list3 = new List<double>();
            for (int i = 1; i <= k; i++)
            {
                list3.Add((a3_ + (a3 - a3_) / k * i));
                Console.Write((a3_ + (a3 - a3_) / k * i));
                Console.Write(" ");
            }



            Console.WriteLine();
            Console.WriteLine("按任意键退出...");
            Console.ReadKey(true);
        }
    }
}
