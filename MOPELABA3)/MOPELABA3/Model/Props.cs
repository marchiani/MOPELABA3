using System;
using System.Collections.Generic;
using System.Text;

namespace MOPELABA3.Model
{
	public class Props
	{
        public virtual int Xmin1 { get; set; } = 20;
        public int Xmin2 { get; set; } = -20;
        public int Xmin3 { get; set; } = 70;
        public int Xmax1 { get; set; } = 70;
        public int Xmax2 { get; set; } = 40;
        public int Xmax3 { get; set; } = 80;
        
        protected double x_min_avg = (20 - 20 + 70) / 3;
        protected double x_max_avg = (70 + 40 + 80) / 3;

        public double y_max { get; set; }
        public double y_min { get; set; }
        protected double[,] Matrix_default;
        public int[,] MatrixX { get; protected set; }
        public double[,] MatrixY { get; set; }
        protected int n { get; set; }
        protected int m { get; set; }
        protected double[,] DividedMatrix { get; set; }
        //must be underneath each b[i]
        protected double[,] b0 { get; set; }
        protected double[,] b1 { get; set; }
        protected double[,] b2 { get; set; }
        protected double[,] b3 { get; set; }

        public double My { get; protected set; }
        public double[] Mx { get; protected set; }
        public double[] AvgColumnsY { get; protected set; }
        public double[] Ax { get; protected set; }
        public double[,] Axx { get; protected set; }
        public double[] Bx { get; protected set; }
        public double[] Y_Norm { get; protected set; }
        public double[] Dispresion { get; set; }
        public double Gp { get; protected set; }
        public double Gt { get; protected set; }
        public double F1 { get; protected set; }
        public double F2 { get; protected set; }
        public bool Check_Koxran { get; protected set; }
        public double S2B { get; protected set; }
        public double S2Bs { get; protected set; }
        public double SBs { get; protected set; }
        public double[] BetaXArr { get; protected set; }
        public double[] beta_x { get; protected set; }
        public double[] Y_Xor { get; protected set; }
        public bool Check_Fisher { get; protected set; }
        public double Fp { get; protected set; }
        public double Ft { get; protected set; }
        public double S2ad { get; protected set; }


    }
}
