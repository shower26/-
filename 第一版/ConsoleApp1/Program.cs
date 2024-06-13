using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //x,y定义到达坐标的位置，a1_,a2_,a3_定义当前旋转角，a1,a2,a3定义为最终旋转角
            double x = 800, y = 300;
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
            double l1=550,l2=450,l3=600;


            //辅助变量(x_,y_为目标点在以大臂末端为原点的坐标系中的位置)
            double x_, y_,l_,a_;
            double pi = Math.PI;

            //定义偏移量
            double pyx = 0, pyy = 0, pya;
            l3 = l3 + pyy;
            l3 = Math.Sqrt(l3 * l3 + pyx * pyx);
            pya = Math.Asin(pyx / l3) / pi * 180;
            Console.WriteLine(l3);
            Console.WriteLine(pya);

            if (x * x + y * y > 1600 * 1600 | y < 0)
            {
                Console.WriteLine("无法到达");
            }
            else
            {
                //判断目标点所属区域
                if (x >= -1500 & x <= -900)
                {
                    if (y >= 0 & y <= 800)
                    {
                        a1 = -70;
                        x_ = l1 * Math.Cos((90-a1)/180*pi);
                        x_ = x - x_;
                        y_ = l1 * Math.Sin((90 - a1) / 180 * pi);
                        y_ = y - y_;
                        l_ = Math.Sqrt(x_ * x_ + y_ * y_);
                        a_ = Math.Acos((l_ * l_ + l2 * l2 - l3 * l3) / (2 * l_ * l2))/pi*180;
                        a2 = Math.Asin(y_ / l_) * 180 / pi + a_ - (90 + a1);//注意这里的a1不是图上标的a1,90+a1才是图上真正的a1
                        a3 = 180 - Math.Acos((l2 * l2 + l3 * l3 - l_ * l_) / (2 * l2 * l3))/pi*180;
                        a3 = -a3;
                    }
                    else if (y > 800 & y <= 1600)
                    {
                        a1 = -45;
                        x_ = l1 * Math.Cos((90 - a1) / 180 * pi);
                        x_ = x - x_;
                        y_ = l1 * Math.Sin((90 - a1) / 180 * pi);
                        y_ = y - y_;
                        l_ = Math.Sqrt(x_ * x_ + y_ * y_);
                        a_ = Math.Acos((l_ * l_ + l2 * l2 - l3 * l3) / (2 * l_ * l2)) / pi * 180;
                        a2 = Math.Asin(y_ / l_) * 180 / pi + a_ - (90 + a1);
                        a3 = 180 - Math.Acos((l2 * l2 + l3 * l3 - l_ * l_) / (2 * l2 * l3)) / pi * 180;
                        a3 = -a3;
                    }
                    else
                    {
                        Console.WriteLine("无法到达");
                    }
                }

                else if (x > -900 & x <= -300)
                {
                    if (y >= 0 & y <= 400)
                    {
                        a1 = 0;
                        x_ = l1 * Math.Cos((90 - a1) / 180 * pi);
                        x_ = x - x_;
                        y_ = l1 * Math.Sin((90 - a1) / 180 * pi);
                        y_ = y - y_;
                        l_ = Math.Sqrt(x_ * x_ + y_ * y_);
                        a_ = Math.Acos((l_ * l_ + l2 * l2 - l3 * l3) / (2 * l_ * l2)) / pi * 180;
                        a2 = Math.Asin(y_ / l_) * 180 / pi + a_ - (90 + a1);
                        a3 = 180 - Math.Acos((l2 * l2 + l3 * l3 - l_ * l_) / (2 * l2 * l3)) / pi * 180;
                        a3 = -a3;
                    }
                    else if (y > 400 & y <= 800)
                    {
                        a1 = 10;
                        x_ = l1 * Math.Cos((90 - a1) / 180 * pi);
                        x_ = x - x_;
                        y_ = l1 * Math.Sin((90 - a1) / 180 * pi);
                        y_ = y - y_;
                        l_ = Math.Sqrt(x_ * x_ + y_ * y_);
                        a_ = Math.Acos((l_ * l_ + l2 * l2 - l3 * l3) / (2 * l_ * l2)) / pi * 180;
                        a2 = Math.Asin(y_ / l_) * 180 / pi + a_ - (90 + a1);
                        a3 = 180 - Math.Acos((l2 * l2 + l3 * l3 - l_ * l_) / (2 * l2 * l3)) / pi * 180;
                        a3 = -a3;
                    }
                    else if (y > 800 & y <= 1600)
                    {
                        //这一段有问题（三角函数值计算错误），调用了对称的算法（大臂为45度的情况取反）
                        x = -x;
                        a1 = 45;
                        x_ = l1 * Math.Cos((90 - a1) / 180 * pi);
                        x_ = x - x_;
                        y_ = l1 * Math.Sin((90 - a1) / 180 * pi);
                        y_ = y - y_;
                        l_ = Math.Sqrt(x_ * x_ + y_ * y_);
                        Console.WriteLine(x_);
                        Console.WriteLine(y_);
                        Console.WriteLine(l_);
                        a_ = Math.Acos((l_ * l_ + l2 * l2 - l3 * l3) / (2 * l_ * l2)) / pi * 180;
                        a2 = Math.Acos(x_ / l_) * 180 / pi + a_ - (90 - a1);
                        a3 = 180 - Math.Acos((l2 * l2 + l3 * l3 - l_ * l_) / (2 * l2 * l3)) / pi * 180;
                        a1 = -a1;
                        a3 = -a3;

                    }
                    else
                    {
                        Console.WriteLine("无法到达");
                    }


                }



                else if (x > -300 & x <= 300)
                {
                    if (y >= 0 & y <= 400)
                    {
                        a1 = 90;
                        x_ = l1 * Math.Cos((90 - a1) / 180 * pi);
                        x_ = x - x_;
                        y_ = l1 * Math.Sin((90 - a1) / 180 * pi);
                        y_ = y - y_;
                        l_ = Math.Sqrt(x_ * x_ + y_ * y_);
                        a_ = Math.Acos((l_ * l_ + l2 * l2 - l3 * l3) / (2 * l_ * l2)) / pi * 180;
                        a2 = Math.Asin(y_ / l_) * 180 / pi + a_ - (90 + a1);
                        a3 = 180 - Math.Acos((l2 * l2 + l3 * l3 - l_ * l_) / (2 * l2 * l3)) / pi * 180;
                        a3 = -a3;
                    }
                    else if (y > 400 & y <= 800)
                    {
                        a1 = 70;
                        x_ = l1 * Math.Cos((90 - a1) / 180 * pi);
                        x_ = x - x_;
                        y_ = l1 * Math.Sin((90 - a1) / 180 * pi);
                        y_ = y - y_;
                        l_ = Math.Sqrt(x_ * x_ + y_ * y_);
                        a_ = Math.Acos((l_ * l_ + l2 * l2 - l3 * l3) / (2 * l_ * l2)) / pi * 180;
                        a2 = Math.Asin(y_ / l_) * 180 / pi + a_ - (90 + a1);
                        a3 = 180 - Math.Acos((l2 * l2 + l3 * l3 - l_ * l_) / (2 * l2 * l3)) / pi * 180;
                        a3 = -a3;
                    }
                    else if (y > 800 & y <= 1200)
                    {
                        a1 = 40;
                        x_ = l1 * Math.Cos((90 - a1) / 180 * pi);
                        x_ = x - x_;
                        y_ = l1 * Math.Sin((90 - a1) / 180 * pi);
                        y_ = y - y_;
                        l_ = Math.Sqrt(x_ * x_ + y_ * y_);
                        a_ = Math.Acos((l_ * l_ + l2 * l2 - l3 * l3) / (2 * l_ * l2)) / pi * 180;
                        a2 = Math.Asin(y_ / l_) * 180 / pi + a_ - (90 + a1);
                        a3 = 180 - Math.Acos((l2 * l2 + l3 * l3 - l_ * l_) / (2 * l2 * l3)) / pi * 180;
                        a3 = -a3;
                    }
                    else if (y > 1200 & y <= 1600)
                    {
                        a1 = 0;
                        x_ = l1 * Math.Cos((90 - a1) / 180 * pi);
                        x_ = x - x_;
                        y_ = l1 * Math.Sin((90 - a1) / 180 * pi);
                        y_ = y - y_;
                        l_ = Math.Sqrt(x_ * x_ + y_ * y_);
                        Console.WriteLine(x_);
                        Console.WriteLine(y_);
                        Console.WriteLine(l_);
                        a_ = Math.Acos((l_ * l_ + l2 * l2 - l3 * l3) / (2 * l_ * l2)) / pi * 180;
                        a2 = Math.Asin(y_ / l_) * 180 / pi + a_ - (90 - a1);
                        a3 = 180 - Math.Acos((l2 * l2 + l3 * l3 - l_ * l_) / (2 * l2 * l3)) / pi * 180;
                        a2 = -a2;
                        if (x > -300 & x <= 0)
                        {
                            a3 = -a3;
                            a2 = -a2;
                        }
                    }
                    else
                    {
                        Console.WriteLine("无法到达");
                    }


                }



                else if (x > 300 & x <= 900)
                {
                    if (y >= 0 & y <= 400)
                    {
                        a1 = 0;
                        x_ = l1 * Math.Cos((90 - a1) / 180 * pi);
                        x_ = x - x_;
                        y_ = l1 * Math.Sin((90 - a1) / 180 * pi);
                        y_ = y - y_;
                        l_ = Math.Sqrt(x_ * x_ + y_ * y_);
                        a_ = Math.Acos((l_ * l_ + l2 * l2 - l3 * l3) / (2 * l_ * l2)) / pi * 180;
                        a2 = Math.Asin(y_ / l_) * 180 / pi + a_ - (90 - a1);
                        a3 = 180 - Math.Acos((l2 * l2 + l3 * l3 - l_ * l_) / (2 * l2 * l3)) / pi * 180;
                        a2 = -a2;
                    }
                    else if (y > 400 & y <= 800)
                    {
                        a1 = -10;
                        x_ = l1 * Math.Cos((90 - a1) / 180 * pi);
                        x_ = x - x_;
                        y_ = l1 * Math.Sin((90 - a1) / 180 * pi);
                        y_ = y - y_;
                        l_ = Math.Sqrt(x_ * x_ + y_ * y_);
                        Console.WriteLine(x_);
                        Console.WriteLine(y_);
                        Console.WriteLine(l_);
                        a_ = Math.Acos((l_ * l_ + l2 * l2 - l3 * l3) / (2 * l_ * l2)) / pi * 180;
                        a2 = Math.Asin(y_ / l_) * 180 / pi + a_ - (90 - a1);
                        a3 = 180 - Math.Acos((l2 * l2 + l3 * l3 - l_ * l_) / (2 * l2 * l3)) / pi * 180;
                        a2 = -a2;
                    }
                    else if (y > 800 & y <= 1600)
                    {
                        a1 = 45;
                        x_ = l1 * Math.Cos((90 - a1) / 180 * pi);
                        x_ = x - x_;
                        y_ = l1 * Math.Sin((90 - a1) / 180 * pi);
                        y_ = y - y_;
                        l_ = Math.Sqrt(x_ * x_ + y_ * y_);
                        Console.WriteLine(x_);
                        Console.WriteLine(y_);
                        Console.WriteLine(l_);
                        a_ = Math.Acos((l_ * l_ + l2 * l2 - l3 * l3) / (2 * l_ * l2)) / pi * 180;
                        a2 = Math.Acos(x_ / l_) * 180 / pi + a_ - (90 - a1);
                        a3 = 180 - Math.Acos((l2 * l2 + l3 * l3 - l_ * l_) / (2 * l2 * l3)) / pi * 180;
                        a2 = -a2;
                    }
                    else
                    {
                        Console.WriteLine("无法到达");
                    }

                }

                else if (x > 900 & x <= 1500)
                {
                    if (y >= 0 & y <= 800)
                    {
                        a1 = 70;
                        x_ = l1 * Math.Cos((90 - a1) / 180 * pi);
                        x_ = x - x_;
                        y_ = l1 * Math.Sin((90 - a1) / 180 * pi);
                        y_ = y - y_;
                        l_ = Math.Sqrt(x_ * x_ + y_ * y_);
                        Console.WriteLine(x_);
                        Console.WriteLine(y_);
                        Console.WriteLine(l_);
                        a_ = Math.Acos((l_ * l_ + l2 * l2 - l3 * l3) / (2 * l_ * l2)) / pi * 180;
                        a2 = Math.Asin(y_ / l_) * 180 / pi + a_ - (90 - a1);
                        a3 = 180 - Math.Acos((l2 * l2 + l3 * l3 - l_ * l_) / (2 * l2 * l3)) / pi * 180;
                        a2 = -a2;
                    }
                    else if (y > 800 & y <= 1600)
                    {
                        a1 = 45;
                        x_ = l1 * Math.Cos((90 - a1) / 180 * pi);
                        x_ = x - x_;
                        y_ = l1 * Math.Sin((90 - a1) / 180 * pi);
                        y_ = y - y_;
                        l_ = Math.Sqrt(x_ * x_ + y_ * y_);
                        Console.WriteLine(x_);
                        Console.WriteLine(y_);
                        Console.WriteLine(l_);
                        a_ = Math.Acos((l_ * l_ + l2 * l2 - l3 * l3) / (2 * l_ * l2)) / pi * 180;
                        a2 = Math.Asin(y_ / l_) * 180 / pi + a_ - (90 - a1);
                        a3 = 180 - Math.Acos((l2 * l2 + l3 * l3 - l_ * l_) / (2 * l2 * l3)) / pi * 180;
                        a2 = -a2;
                    }
                    else
                    {
                        Console.WriteLine("无法到达");
                    }



                }


                else
                {
                    Console.WriteLine("无法到达");
                }
            }

            //添加小臂角度偏移量
            a3 = a3 - pya;
           

            Console.WriteLine("大臂所需旋转角度为：" + (a1 - a1_) );
            Console.WriteLine("中臂所需旋转角度为：" + (a2 - a2_));
            Console.WriteLine("小臂所需旋转角度为：" + (a3 - a3_));
            Console.WriteLine("旋转后大臂角度为：" + a1);
            Console.WriteLine("旋转后中臂角度为：" + a2);
            Console.WriteLine("旋转后小臂角度为：" + a3);


            
            //判断三轴中最大的旋转角度
            double max = 0;
            if( Math.Abs(a1 - a1_) >= Math.Abs(a2 - a2_) && Math.Abs(a1 - a1_) >= Math.Abs(a3 - a3_) )
            {
                max = Math.Abs(a1 - a1_);
            }
            else if( Math.Abs(a2 - a2_) >= Math.Abs(a1 - a1_) && Math.Abs(a2 - a2_) >= Math.Abs(a3 - a3_))
            {
                max = Math.Abs(a2 - a2_);
            }
            else if (Math.Abs(a3 - a3_) >= Math.Abs(a1 - a1_) && Math.Abs(a3 - a3_) >= Math.Abs(a2 - a2_))
            {
                max = Math.Abs(a3 - a3_);
            }
  
            int k = 0;
            k = (int)Math.Floor(max/5)+1;



            //得出各轴所需旋转角度序列表
            Console.WriteLine();
            Console.WriteLine("大臂转角序列为：");
            List<double> list1 = new List<double>();
            for (int i = 1; i<=k; i++)
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
