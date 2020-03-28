using MOPELABA3.Extension;
using System;

namespace MOPELABA3
{
	class Program
	{
		static void Main(string[] args)
		{

            DirtyWork factor = new DirtyWork();

            Console.WriteLine("Done by Dmytro Boychenko IB-83");
            Console.WriteLine();

            Console.WriteLine($"Xmin1 - {factor.Xmin1}");
            Console.WriteLine($"Xmin2 - {factor.Xmin2}");
            Console.WriteLine($"Xmin3 - {factor.Xmin3}");
            Console.WriteLine();

            Console.WriteLine($"Xmax1 - {factor.Xmax1}");
            Console.WriteLine($"Xmax2 - {factor.Xmax2}");
            Console.WriteLine($"Xmax3 - {factor.Xmax3}");
            Console.WriteLine();

            factor.MessageMatrix("matrix Y", factor.MatrixY);
            factor.MessageMatrix("matrix X", factor.MatrixX);

            factor.Message("Average of Columns in Matrix Y", factor.AvgColumnsY);

            factor.Message("Mx ", factor.Mx);

            Console.WriteLine($"My: {factor.My}");
            for (int i = 0; i < 50; i++)
                Console.Write("~");
            Console.WriteLine();

            factor.Message("Ax", factor.Ax);

            factor.MessageMatrix("Axx: ", factor.Axx);
            factor.ShowMatrix(factor.Axx);

            factor.Message("Bx", factor.Bx);

            Console.WriteLine(@"Уравнение регресии: {0} + {1} * x1 + {2} * x2 + {3} * x3", factor.Bx[0], factor.Bx[1], factor.Bx[2], factor.Bx[3]);
            for (int i = 0; i < 115; i++)
                Console.Write("~");
            Console.WriteLine();

            factor.Message("Y normalized", factor.Y_Norm);

            factor.Message("Sx", factor.Dispresion);

            Console.WriteLine($"Our Gp: {factor.Gp}");
            Console.WriteLine($"Проверка на дисперсию за критерием Кохрана Gp < Gt: {factor.Check_Koxran}");
            for (int i = 0; i < 50; i++)
                Console.Write("~");
            Console.WriteLine();

            Console.WriteLine($"S2B: {factor.S2B}");
            Console.WriteLine($"S2Bs: {factor.S2Bs}");
            Console.WriteLine($"SBs: {factor.SBs}");
            for (int i = 0; i < 50; i++)
                Console.Write("~");
            Console.WriteLine();
            factor.Message("Ti", factor.beta_x);

            factor.Message("Y/\\", factor.Y_Xor);

            Console.WriteLine("S2Ad: " + factor.S2ad);
            Console.WriteLine("Fp: " + factor.Fp);
            Console.WriteLine("Ft: " + factor.Ft);
            Console.WriteLine("Адекватность при q = 0.05 " + factor.Check_Fisher);

            Console.ReadKey();
        }
	}
}
