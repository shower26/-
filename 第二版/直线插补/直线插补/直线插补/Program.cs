using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 直线插补
{
    class Program
    {
        static void Main(string[] args)
        {
            //x,y定义到达坐标的位置；x_,y_定义当前坐标的位置
            double x = 0, y = 0, x_ = 0, y_ = 0;
            Console.Write("请输入目标位置横坐标: ");
            x = double.Parse(Console.ReadLine());
            Console.Write("请输入目标位置纵坐标: ");
            y = double.Parse(Console.ReadLine());
            Console.Write("请输入当前位置横坐标: ");
            x_ = double.Parse(Console.ReadLine());
            Console.Write("请输入当前位置纵坐标: ");
            y_ = double.Parse(Console.ReadLine());

            //定义distance为当前位置与目标位置间距离
            double distance = 0;
            distance = Math.Sqrt((x - x_) * (x - x_) + (y - y_) * (y - y_));

            //定义step为目标点序列元素个数,accuracy为精度
            int step = 0;
            double accuracy = 2;
            step = (int)Math.Floor(distance / accuracy) + 1;




            //得出目标点插补序列表
            Console.WriteLine();
            Console.WriteLine("横坐标序列为：");
            List<double> xlist = new List<double>();
            for (int i = 1; i <= step; i++)
            {
                xlist.Add((x_ + (x - x_) / step * i));
                Console.Write((x_ + (x - x_) / step * i));
                Console.Write(" ");
            }
            Console.WriteLine();

            Console.WriteLine();
            Console.WriteLine("纵坐标序列为：");
            List<double> ylist = new List<double>();
            for (int i = 1; i <= step; i++)
            {
                ylist.Add((y_ + (y - y_) / step * i));
                Console.Write((y_ + (y - y_) / step * i));
                Console.Write(" ");
            }
            Console.WriteLine();






            Console.WriteLine();
            Console.WriteLine("按任意键退出...");
            Console.ReadKey(true);


        }
    }
}
