using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using NPOI;
using NPOI.HSSF.UserModel;
using NPOI.OOXML;
using NPOI.OpenXml4Net;
using NPOI.OpenXmlFormats;
using NPOI.SS.UserModel;





namespace 正逆解验证
{
    class Program
    {
        public static void DataTableToExcel(DataTable dt, string path)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            //创建工作表 
            ISheet sheet = string.IsNullOrEmpty(dt.TableName) ? workbook.CreateSheet("Sheet1") : workbook.CreateSheet(dt.TableName);
            #region 标题
            //在工作表中添加一行
            IRow row = sheet.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                //在行中添加一列
                ICell cell = row.CreateCell(i);
                //设置列的内容	 
                cell.SetCellValue(dt.Columns[i].ColumnName);
            }
            #endregion
            #region 填充数据
            for (int i = 1; i <= dt.Rows.Count; i++)//遍历DataTable行
            {
                DataRow dataRow = dt.Rows[i - 1];
                row = sheet.CreateRow(i);//在工作表中添加一行

                for (int j = 0; j < dt.Columns.Count; j++)//遍历DataTable列
                {
                    ICell cell = row.CreateCell(j);//在行中添加一列
                    cell.SetCellValue(dataRow[j].ToString());//设置列的内容	 
                }
            }
            #endregion
            #region 输出Excel
            MemoryStream ms = new MemoryStream();
            workbook.Write(ms);
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                byte[] bArr = ms.ToArray();
                fs.Write(bArr, 0, bArr.Length);
                fs.Flush();
            }
            #endregion
        }

        public static List<double> Zhengjie(double a1_, double a2_, double a3_,double h4_, double a5_, double a6_)
        {
            //大、中、小臂长度l1、l2、l3以及末端轴间距l4、l5
            double l1 = 600, l2 = 500, l3 = 710, l4 = 68, l5 = -8;
            double pi = Math.PI;

            //定义d1,d2,d3,d4,d5
            double d1 = 198, d2 = 84, d3 = 102.5, d4 = -52.03, d5 = -108.9;

            //定义吸盘和吹嘴两个末端执行器到末端俯仰自由度的偏移量
            double xppyx = 90, xppyz = -168, czpyx = -90, czpyz = -128;


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


            List<double> coordinate = new List<double>();
            coordinate.Add(x3_);
            coordinate.Add(y3_);
            coordinate.Add(z3_);
            coordinate.Add(xpx);
            coordinate.Add(xpy);
            coordinate.Add(xpz);
            coordinate.Add(czx);
            coordinate.Add(czy);
            coordinate.Add(czz);
            return coordinate;
        }


        public static List<double> Nijie(double x, double y,int mode, double a1_, double a2_, double a3_, double h4_, double a5_, double a6_)
        {
            //x,y定义到达坐标的位置，（mode为1则采用吸盘，为2则采用吹嘴），a1_,a2_,a3_,h4_(高度),a5_,a6_定义当前旋转角，a1,a2,a3,h4(高度),a5,a6定义为最终旋转角
            double a1 = 0, a2 = 0, a3 = 0, h4 = 0, a5 = 0, a6 = 0;


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
                if (Math.Sqrt(x * x + y * y) <= (l1 + l2 + l3))
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
                if (Math.Abs(x) <= Math.Sqrt(l3 * l3 + pyx * pyx) - l2 )
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
                    //特殊点位判断！！！！！！
                    if ( Double.IsNaN(a_) )
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

            List<double> anglelist = new List<double>();
            anglelist.Add(a1);
            anglelist.Add(a2);
            anglelist.Add(a3);
            anglelist.Add(h4);
            anglelist.Add(a5);
            anglelist.Add(a6);
            return anglelist;

        }






        static void Main(string[] args)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("当前大臂转角");
            dt.Columns.Add("当前中臂转角");
            dt.Columns.Add("当前小臂转角");
            dt.Columns.Add("当前升降高度");
            dt.Columns.Add("当前旋转角度");
            dt.Columns.Add("当前俯仰角度");
            dt.Columns.Add("目标点x坐标");
            dt.Columns.Add("目标点y坐标");
            dt.Columns.Add("运动模式");
            dt.Columns.Add("运动后大臂转角");
            dt.Columns.Add("运动后中臂转角");
            dt.Columns.Add("运动后小臂转角");
            dt.Columns.Add("运动后升降高度");
            dt.Columns.Add("运动后旋转角度");
            dt.Columns.Add("运动后俯仰角度");
            dt.Columns.Add("运动后实际末端x坐标");
            dt.Columns.Add("运动后实际末端y坐标");
            dt.Columns.Add("运动后实际末端z坐标");
            dt.Columns.Add("精确度");

            Random random = new Random();



            for (int k = 0; k <= 60000; k++)
            {
                double x = 0, y = 0, a1_ = 0, a2_ = 0, a3_ = 0, h4_ = 0, a5_ = 0, a6_ = 0;
                int mode = 0;
                List<double> anglelist = new List<double>();
                List<double> coordinate = new List<double>();


                x = random.Next(-1000, 1001); // 生成1到100之间的随机整数
                y = random.Next(200, 1601);
                mode = random.Next(0, 3);
                a1_ = random.Next(-90, 91);
                a2_ = random.Next(-180, 181);
                a3_ = random.Next(-180, 181);
                h4_ = random.Next(-40, 1);
                a5_ = random.Next(-180, 181);
                a6_ = random.Next(-90, 91);


                anglelist = Nijie(x, y, mode, a1_, a2_, a3_, h4_, a5_, a6_);
                coordinate = Zhengjie(anglelist[0], anglelist[1], anglelist[2], anglelist[3], anglelist[4], anglelist[5]);



                DataRow dr = null;
                dr = dt.NewRow();
                dr["当前大臂转角"] = a1_;
                dr["当前中臂转角"] = a2_;
                dr["当前小臂转角"] = a3_;
                dr["当前升降高度"] = h4_;
                dr["当前旋转角度"] = a5_;
                dr["当前俯仰角度"] = a6_;
                dr["目标点x坐标"] = x;
                dr["目标点y坐标"] = y;
                dr["运动模式"] = mode;
                dr["运动后大臂转角"] = anglelist[0];
                dr["运动后中臂转角"] = anglelist[1];
                dr["运动后小臂转角"] = anglelist[2];
                dr["运动后升降高度"] = anglelist[3];
                dr["运动后旋转角度"] = anglelist[4];
                dr["运动后俯仰角度"] = anglelist[5];


                double accuracy = 0;
                if (mode == 0)
                {
                    accuracy = Math.Sqrt((x - coordinate[0]) * (x - coordinate[0]) + (y - coordinate[1]) * (y - coordinate[1]));
                    dr["运动后实际末端x坐标"] = coordinate[0];
                    dr["运动后实际末端y坐标"] = coordinate[1];
                    dr["运动后实际末端z坐标"] = coordinate[2];
                }
                else if (mode == 1)
                {
                    accuracy = Math.Sqrt((x - coordinate[3]) * (x - coordinate[3]) + (y - coordinate[4]) * (y - coordinate[4]));
                    dr["运动后实际末端x坐标"] = coordinate[3];
                    dr["运动后实际末端y坐标"] = coordinate[4];
                    dr["运动后实际末端z坐标"] = coordinate[5];
                }
                else if (mode == 2)
                {
                    accuracy = Math.Sqrt((x - coordinate[6]) * (x - coordinate[6]) + (y - coordinate[7]) * (y - coordinate[7])) - 30;
                    dr["运动后实际末端x坐标"] = coordinate[6];
                    dr["运动后实际末端y坐标"] = coordinate[7];
                    dr["运动后实际末端z坐标"] = coordinate[8];
                }
                
   
                
                dr["精确度"] = accuracy;
                dt.Rows.Add(dr);


                Console.WriteLine(k);
            }





            string filename = DateTime.Now.ToString("验证结果改（缩减后）") +  ".xls";
            DataTableToExcel(dt, "D:\\测试\\" + filename);


            Console.WriteLine();
            Console.WriteLine("按任意键退出...");
            Console.ReadKey(true);
        }
    }
}
