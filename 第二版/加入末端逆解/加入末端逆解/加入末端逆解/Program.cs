using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 逆解
{
    class Program
    {
        static void Main(string[] args)
        {
            //x,y定义到达坐标的位置，（mode为1则采用吸盘，为2则采用吹嘴），a1_,a2_,a3_,h4_(高度),a5_,a6_定义当前旋转角，a1,a2,a3,h4(高度),a5,a6定义为最终旋转角
            double x = 0, y = 0;
            double a1_ = 0, a2_ = 0, a3_ = 0, h4_ = 0, a5_ = 0, a6_ = 0;
            double a1 = 0, a2 = 0, a3 = 0, h4 = 0, a5 = 0, a6 = 0;
            int mode = 0;
            Console.Write("请输入目标位置横坐标: ");
            x = double.Parse(Console.ReadLine());
            Console.Write("请输入目标位置纵坐标: ");
            y = double.Parse(Console.ReadLine());
            Console.Write("请输入处理模式（1或2）: ");
            mode = int.Parse(Console.ReadLine());
            Console.Write("请输入当前大臂转角: ");
            a1_ = double.Parse(Console.ReadLine());
            Console.Write("请输入当前中臂转角: ");
            a2_ = double.Parse(Console.ReadLine());
            Console.Write("请输入当前小臂转角: ");
            a3_ = double.Parse(Console.ReadLine());
            Console.Write("请输入当前末端升降自由度高度: ");
            h4_ = double.Parse(Console.ReadLine());
            Console.Write("请输入当前末端旋转自由度转角: ");
            a5_ = double.Parse(Console.ReadLine());
            Console.Write("请输入当前末端俯仰自由度转角: ");
            a6_ = double.Parse(Console.ReadLine());
            Console.WriteLine();

            //大、中、小臂长度l1、l2、l3以及末端轴间距l4、l5
            double l1 = 600, l2 = 500, l3 = 710, l4 = 68, l5 = -8;

            //定义d1,d2,d3,d4,d5
            double d1 = 198, d2 = 84, d3 = 102.5, d4 = -52.03, d5 = -108.9;

            //定义吸盘和吹嘴两个末端执行器到末端俯仰自由度的偏移量
            double xppyx = 90, xppyz = -168, czpyx = -90, czpyz = -128;

            //辅助变量(x1_,y1_为当前大臂末端位置，pattern为运动模式：0表示无法到达，1表示在一区内，2表示在二区内。)
            //(在pattern =1的情况下l_为中臂旋转轴心到小臂末端的距离，a_为该线段与中臂夹角；在pattern=2的情况下，l_为大臂旋转轴心到小臂末端的距离，a_为该线段与大臂夹角)
            double x1_, y1_, pattern = 0, l_, a_;
            double pi = Math.PI;
            x1_ = Math.Sin(a1_ / 180 * pi) * l1;
            y1_ = Math.Cos(a1_ / 180 * pi) * l1;
            //Console.WriteLine("x1_ = " + x1_);
            //Console.WriteLine("y1_ = " + y1_);

            //定义偏移量
            double pyx = 0, pyy = 0, pya;

            //定义产线所在z平面的高度
            double cxz = 25.57;


            //判断采用吸盘还是吹嘴
            //采用吸盘
            if (mode == 1)
            {
                double height = 8;
                h4 = height - 30;//height指希望吸盘底面到产线间的距离；30指升降自由度位于零点时吸盘底面到产线的距离
                if( Math.Sqrt(x * x + y * y) <= (l1 + l2 + l3) )
                {
                    pyx = xppyx;
                    pyy = l4 + l5;
                    a5 = 0;
                    a6 = 0;
                }
                else
                {
                    pyx = -l5;
                    pyy = l4 + xppyx;
                    a5 = -90;
                    a6 = 0;
                }
            }
            //采用吹嘴
            else if (mode == 2)
            {
                double height = 30;//height指希望吹嘴底面到产线间的距离；
                h4 = cxz - d1 - d2 - d3 - d4 - d5 - (czpyx + czpyz) / Math.Sqrt(2) + height;
                pyx = -l5;
                pyy = l4 + (czpyx - czpyz) / Math.Sqrt(2) + height;
                a5 = -90;
                a6 = 45;
            }










            //得出真实l3
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
                a1 = a1_;
                a2 = a2_;
                a3 = a3_;
                h4 = h4_;
                a5 = a5_;
                a6 = a6_;

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
                if (Math.Abs(x) <= Math.Sqrt(l3 * l3 + pyx * pyx) - l2 )//可根据实际情况缩减
                {
                    a1 = 90;
                    Console.Write("左手模式");
                    //避免吸盘与基座碰撞
                    if (mode == 1)
                    {
                        pyx = -xppyx;
                        a5 = 180;
                    }

                    l3 = Math.Sqrt(l3 * l3 + pyx * pyx);
                    x1_ = Math.Sin(a1 / 180 * pi) * l1;
                    y1_ = Math.Cos(a1 / 180 * pi) * l1;
                    l_ = Math.Sqrt((x - x1_) * (x - x1_) + (y - y1_) * (y - y1_));
                    a_ = Math.Acos((l_ * l_ + l2 * l2 - l3 * l3) / (2 * l_ * l2)) / pi * 180;
                    //特殊位置判断
                    if (Double.IsNaN(a_))
                    {
                        a1 = 45;
                        l3 = Math.Sqrt(l3 * l3 + pyx * pyx);
                        x1_ = Math.Sin(a1 / 180 * pi) * l1;
                        y1_ = Math.Cos(a1 / 180 * pi) * l1;
                        l_ = Math.Sqrt((x - x1_) * (x - x1_) + (y - y1_) * (y - y1_));
                        a_ = Math.Acos((l_ * l_ + l2 * l2 - l3 * l3) / (2 * l_ * l2)) / pi * 180;
                    }
                    pya = Math.Asin(pyx / l3) / pi * 180;
                    a3 = 180 - Math.Acos((l2 * l2 + l3 * l3 - l_ * l_) / (2 * l2 * l3)) / pi * 180;
                    a2 = a_ - (90 + a1 - Math.Asin((y - y1_) / l_) * 180 / pi);
                    //添加小臂偏移量
                    a3 = a3 + pya;
                    a3 = -a3;
                    //不可达位置判断
                    if (Double.IsNaN(a_))
                    {
                        Console.Write("无法到达");
                        a1 = a1_;
                        a2 = a2_;
                        a3 = a3_;
                        h4 = h4_;
                        a5 = a5_;
                        a6 = a6_;
                    }
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
            Console.WriteLine("升降自由度所需升降高度为：" + (h4 - h4_));
            Console.WriteLine("旋转自由度旋转角度为：" + (a5 - a5_));
            Console.WriteLine("俯仰自由度所需旋转角度为：" + (a6 - a6_));
            Console.WriteLine("运动后大臂角度为：" + a1);
            Console.WriteLine("运动后中臂角度为：" + a2);
            Console.WriteLine("运动后小臂角度为：" + a3);
            Console.WriteLine("运动后升降自由度高度为：" + h4);
            Console.WriteLine("运动后旋转自由度角度为：" + a5);
            Console.WriteLine("运动后俯仰自由度角度为：" + a6);


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
            Console.WriteLine();
            Console.WriteLine("升降自由度高度序列为：");
            List<double> list4 = new List<double>();
            for (int i = 1; i <= k; i++)
            {
                list4.Add((h4_ + (h4 - h4_) / k * i));
                Console.Write((h4_ + (h4 - h4_) / k * i));
                Console.Write(" ");
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("旋转自由度转角序列为：");
            List<double> list5 = new List<double>();
            for (int i = 1; i <= k; i++)
            {
                list3.Add((a5_ + (a5 - a5_) / k * i));
                Console.Write((a5_ + (a5 - a5_) / k * i));
                Console.Write(" ");
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("俯仰自由度转角序列为：");
            List<double> list6 = new List<double>();
            for (int i = 1; i <= k; i++)
            {
                list3.Add((a6_ + (a6 - a6_) / k * i));
                Console.Write((a6_ + (a6 - a6_) / k * i));
                Console.Write(" ");
            }



            Console.WriteLine();
            Console.WriteLine("按任意键退出...");
            Console.ReadKey(true);
        }
    }
}
