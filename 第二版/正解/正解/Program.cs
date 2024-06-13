using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 正解
{
    class Program
    {
        static void Main(string[] args)
        {
            //大、中、小臂长度l1、l2、l3以及末端轴间距l4、l5
            double l1 = 600, l2 = 500, l3 = 710, l4 = 68, l5 = -8;
            double pi = Math.PI;

            //定义d1,d2,d3,d4,d5
            double d1 = 198, d2 = 84, d3 = 102.5, d4 = -52.03, d5 = -108.9;

            //定义吸盘和吹嘴两个末端执行器到末端俯仰自由度的偏移量
            double xppyx = 90, xppyz = -168, czpyx = -90, czpyz = -128;


            //a1_,a2_,a3_,a4_,a5_定义为当前旋转角,h4_为升降自由度升降高度
            double a1_ = 0, a2_ = 0, a3_ = 0, h4_ = 0, a5_ = 0, a6_ = 0;
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
            


            //定义偏移量(不必修改，请勿修改！！！)
            double pyx = 0, pyy = 0, pya;
            l3 = l3 + pyy;
            l3 = Math.Sqrt(l3 * l3 + pyx * pyx);
            pya = Math.Asin(pyx / l3) / pi * 180;
            a3_ = a3_ + pya;

            //将转角转换为计算用角度
            a1_ = a1_ / 180 * pi;
            a2_ = a2_ / 180 * pi;
            a3_ = a3_ / 180 * pi;
            a5_ = a5_ / 180 * pi;
            a6_ = a6_ / 180 * pi;

            //定义大、中、小臂当前末端位置(x1_,y1_,z1_) , (x2_,y2_,z2_) , (x3_,y3_,z3_),升降轴位置（x4_,y4_,z4_）,旋转轴位置（x5_,y5_,z5_）,俯仰轴位置（x6_,y6_,z6_）
            double x1_ = 0, y1_ = 0, z1_ = 0, x2_ = 0, y2_ = 0, z2_ = 0, x3_ = 0, y3_ = 0, z3_ = 0, x4_ = 0, y4_ = 0, z4_ = 0, x5_ = 0, y5_ = 0, z5_ = 0, x6_ = 0, y6_ = 0, z6_ = 0;

            x1_ = l1 * Math.Sin(a1_);
            y1_ = l1 * Math.Cos(a1_);
            z1_ = d1;


            x2_ = x1_ + l2 * Math.Sin(a1_ + a2_);
            y2_ = y1_ + l2 * Math.Cos(a1_ + a2_);
            z2_ = d1 + d2;


            x3_ = x2_ + l3 * Math.Sin(a1_ + a2_ + a3_);
            y3_ = y2_ + l3 * Math.Cos(a1_ + a2_ + a3_);
            z3_ = d1 + d2 + d3;


            x4_ = x3_;
            y4_ = y3_;
            z4_ = z3_ + d4 + h4_;


            x5_ = x4_ + l4 * Math.Sin(a1_ + a2_ + a3_);
            y5_ = y4_ + l4 * Math.Cos(a1_ + a2_ + a3_);
            z5_ = z4_ + d5;


            x6_ = x5_ + l5 * Math.Sin(a1_ + a2_ + a3_ + a5_);
            y6_ = y5_ + l5 * Math.Cos(a1_ + a2_ + a3_ + a5_);
            z6_ = z5_;


            //定义吸盘和吹嘴坐标（xpx,xpy,xpz）,(czx,czy,czz)
            double xpx = 0, xpy = 0, xpz = 0, czx = 0, czy = 0, czz = 0;

            xpx = x6_ + Math.Cos(a6_) * xppyx * Math.Sin(a1_ + a2_ + a3_ + a5_ + pi / 2) - Math.Sin(a6_) * xppyz * Math.Sin(a1_ + a2_ + a3_ + a5_ + pi / 2);
            xpy = y6_ + Math.Cos(a6_) * xppyx * Math.Cos(a1_ + a2_ + a3_ + a5_ + pi / 2) - Math.Sin(a6_) * xppyz * Math.Cos(a1_ + a2_ + a3_ + a5_ + pi / 2);
            xpz = z6_ + Math.Sin(a6_) * xppyx + Math.Cos(a6_) * xppyz;


            czx = x6_ + Math.Cos(a6_) * czpyx * Math.Sin(a1_ + a2_ + a3_ + a5_ + pi / 2) + Math.Sin(a6_) * czpyz * Math.Sin(a1_ + a2_ + a3_ + a5_ - pi / 2);
            czy = y6_ + Math.Cos(a6_) * czpyx * Math.Cos(a1_ + a2_ + a3_ + a5_ + pi / 2) + Math.Sin(a6_) * czpyz * Math.Cos(a1_ + a2_ + a3_ + a5_ - pi / 2);
            czz = z6_ + Math.Sin(a6_) * czpyx + Math.Cos(a6_) * czpyz;



            //打印
            Console.WriteLine("当前大臂末端坐标为: " + "(" + x1_ + "," + y1_ + "," + z1_ + ")");
            Console.WriteLine("当前中臂末端坐标为: " + "(" + x2_ + "," + y2_ + "," + z2_ + ")");
            Console.WriteLine("当前小臂末端坐标为: " + "(" + x3_ + "," + y3_ + "," + z3_ + ")");
            Console.WriteLine("当前末端升降自由度坐标为: " + "(" + x4_ + "," + y4_ + "," + z4_ + ")");
            Console.WriteLine("当前末端旋转自由度坐标为: " + "(" + x5_ + "," + y5_ + "," + z5_ + ")");
            Console.WriteLine("当前末端俯仰自由度坐标为: " + "(" + x6_ + "," + y6_ + "," + z6_ + ")");
            Console.WriteLine("当前吸盘坐标为: " + "(" + xpx + "," + xpy + "," + xpz + ")");
            Console.WriteLine("当前吹嘴坐标为: " + "(" + czx + "," + czy + "," + czz + ")");


            Console.WriteLine();
            Console.WriteLine("按任意键退出...");
            Console.ReadKey(true);
        }
    }
}
