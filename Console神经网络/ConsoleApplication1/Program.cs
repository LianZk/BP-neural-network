//主调用程序  
using System;

namespace BpANNet
{
    /// <summary>  
    /// Class1 的摘要说明。  
    /// </summary>  
    class Class1
    {
        /// <summary>  
        /// 
        /// 应用程序的主入口点。  
        /// </summary>  
        [STAThread]
        static void Main(string[] args)
        {
            //p1为输入层
            //t1为教师层
            //  double[,] p1 = new double[,] { { 166, 93 }, {136,84 },{ 85, 105 },{ 126, 109 }, { 209,210  }, { 212, 212 }, { 224, 211 }, { 208, 206 }, { 227, 2315 }, { 205, 332 }, { 205, 332 }, { 239, 329 }, { 212, 311 }, { 238, 277 }, { 147, 319 }, { 177, 329 }, { 302, 153}, { 301, 156 }, { 293, 141}, { 245, 193 } };
            //  double[,] t1 = new double[,] { { 100,100 }, { 100,100 }, { 100,100 }, { 100,100 }, { 200,200 }, { 200, 200 }, { 200, 200 }, { 200, 200 }, { 200, 300 }, { 200, 300 }, { 200, 300 }, { 200, 300 }, { 300, 300 }, { 300, 300 }, { 300, 300 }, { 300, 300 }, { 300, 320 }, { 300, 320 }, { 300, 320 }, { 300, 320 } };
            double[,] p1 = new double[,] { { 126, 109 }, { 136, 84 }, { 85, 105 }, { 209, 210 }, { 212, 212 }, { 224, 211 }, { 208, 206 }, { 208, 210 }, { 206, 210 }, { 227, 315 }, { 205, 332 }, { 205, 332 }, { 212, 311 }, { 238, 277 }, { 147, 319 }, { 302, 153 }, { 301, 156 }, { 293, 141 } };
            double[,] t1 = new double[,] { { 100, 100 }, { 100, 100 }, { 100, 100 }, { 200, 200 }, { 200, 200 }, { 200, 200 }, { 200, 200 }, { 200, 200 }, { 200, 200 }, { 200, 300 }, { 200, 300 }, { 200, 300 }, { 300, 300 }, { 300, 300 }, { 300, 300 }, { 300, 200 }, { 300, 200 }, { 300, 200 } };

            BpNet bp = new BpNet(p1, t1);
            int study = 0;
            do
            {
                study++;
                bp.train(p1, t1);
                //       bp.rate=0.95-(0.95-0.3)*study/50000;  
                //        Console.Write("第 "+ study+"次学习： ");  
                //        Console.WriteLine(" 均方差为 "+bp.e);  

            } while (bp.e > 0.001 && study < 50000);
            Console.Write("第 " + study + "次学习： ");
            Console.WriteLine(" 均方差为 " + bp.e);
            bp.saveMatrix(bp.w, "w.txt");
            bp.saveMatrix(bp.v, "v.txt");
            bp.saveMatrix(bp.b1, "b1.txt");
            bp.saveMatrix(bp.b2, "b2.txt");
            bp.saveParas("para.txt");

            pretect(); //开始预测新样本  
        }

        public static void pretect()
        {
            Console.WriteLine("预测开始...");
            BpNet bp = new BpNet();
            bp.readParas("para.txt");
            bp.initial();
            bp.readMatrixW(bp.w, "w.txt");
            bp.readMatrixW(bp.v, "v.txt");
            bp.readMatrixB(bp.b1, "b1.txt");
            bp.readMatrixB(bp.b2, "b2.txt");

          //  double[,] p2 = new double[,] { { 85, 105 }, { 314, 309 } };
            double[,] p2 = new double[,] { { 126,109  }, { 213, 215}, { 155, 334 } };
            int aa = bp.inNum;
            int bb = bp.outNum;
            int cc = p2.GetLength(0);
            double[] p21 = new double[aa];
            double[] t2 = new double[bb];
            for (int n = 0; n < cc; n++)
            {
                for (int i = 0; i < aa; i++)
                {
                    p21[i] = p2[n, i];
                }
                t2 = bp.sim(p21);

                for (int i = 0; i < t2.Length; i++)
                {
                    Console.WriteLine("预测数据" + n.ToString() + "： " + t2[i] + " " );
                }
                Console.WriteLine("\n");

            }

            Console.ReadLine();
        }
    }
}