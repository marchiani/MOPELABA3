using MOPELABA3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOPELABA3.Extension
{
	class DirtyWork : Props
	{
        public DirtyWork(int m = 4, int n = 4)
        {
            this.n = n;
            this.m = m;
            y_max = 200 + x_max_avg;
            y_min = 200 + x_min_avg;

            Matrix_default = new double[,]
            {
                {1,-1,-1,-1},
                {1,-1, 1, 1},
                {1, 1,-1, 1},
                {1, 1, 1,-1}
            };
            //задали матрицу согласно методичке

            MatrixX = new int[,]
            {
                {Xmin1, Xmin2, Xmin3},
                {Xmin1, Xmax2, Xmax3},
                {Xmax1, Xmin2, Xmax3},
                {Xmax1, Xmax2, Xmin3}
            };

            AvgColumnsY = new double[n];
            MatrixY = new double[n, m];
            Mx = new double[3];
            Ax = new double[3];
            Axx = new double[3, 3];

            GenertateMatrix(MatrixY);
            GetAveregeColumnsOfMatrixY();
            GetMxs();
            GetMy();
            GetAxs();

            Bx = new double[4];

            DividedMatrix = new double[,]
            {
                {1,Mx[0],Mx[1],Mx[2]},
                {Mx[0],Axx[0,0],Axx[0,1],Axx[0,2]},
                {Mx[1],Axx[0,1],Axx[1,1],Axx[1,2]},
                {Mx[2],Axx[0,2],Axx[1,2],Axx[2,2]},
            };

            b0 = new double[,]
            {
                {My, Mx[0], Mx[1], Mx[2]},
                {Ax[0], Axx[0, 0], Axx[0, 1], Axx[0, 2]},
                {Ax[1], Axx[0, 1], Axx[1, 1], Axx[1, 2]},
                {Ax[2], Axx[0, 2], Axx[1, 2], Axx[2, 2]},
            };

            b1 = new double[,]
            {
                {1, My, Mx[1], Mx[2]},
                {Mx[0], Axx[0, 0], Axx[0, 1], Axx[0, 2]},
                {Mx[1], Axx[0, 1], Axx[1, 1], Axx[1, 2]},
                {Mx[2], Axx[0, 2], Axx[1, 2], Axx[2, 2]},
            };

            b2 = new double[,]
            {
                {1,Mx[0], My, Mx[2]},
                {Mx[0], Axx[0, 0], Axx[0, 1], Axx[0, 2]},
                {Mx[1], Axx[0, 1], Axx[1, 1], Axx[1, 2]},
                {Mx[2], Axx[0, 2], Axx[1, 2], Axx[2, 2]},
            };

            b3 = new double[,]
            {
                {1, Mx[0], Mx[1], My},
                {Mx[0], Axx[0, 0], Axx[0, 1], Axx[0, 2]},
                {Mx[1], Axx[0, 1], Axx[1, 1], Axx[1, 2]},
                {Mx[2], Axx[0, 2], Axx[1, 2], Axx[2, 2]},
            };

            GetBx();

            Y_Norm = new double[4];
            Y_Norm[0] = Bx[0] + Bx[1] * Xmin1 + Bx[2] * Xmin2 + Bx[3] * Xmin3;
            Y_Norm[1] = Bx[0] + Bx[1] * Xmin1 + Bx[2] * Xmax2 + Bx[3] * Xmax3;
            Y_Norm[2] = Bx[0] + Bx[1] * Xmax1 + Bx[2] * Xmin2 + Bx[3] * Xmax3;
            Y_Norm[3] = Bx[0] + Bx[1] * Xmax1 + Bx[2] * Xmax2 + Bx[3] * Xmin3;

            Dispresion = new double[4];
            Dispresion[0] = (Math.Pow(MatrixY[0, 0] - AvgColumnsY[0], 2) + Math.Pow(MatrixY[0, 1] - AvgColumnsY[0], 2) +
                     Math.Pow(MatrixY[0, 2] - AvgColumnsY[0], 2)) / 3;

            Dispresion[1] = (Math.Pow(MatrixY[1, 0] - AvgColumnsY[1], 2) + Math.Pow(MatrixY[1, 1] - AvgColumnsY[1], 2) +
                     Math.Pow(MatrixY[2, 2] - AvgColumnsY[1], 2)) / 3;

            Dispresion[2] = (Math.Pow(MatrixY[2, 0] - AvgColumnsY[2], 2) + Math.Pow(MatrixY[2, 1] - AvgColumnsY[2], 2) +
                     Math.Pow(MatrixY[2, 2] - AvgColumnsY[2], 2)) / 3;

            Dispresion[3] = (Math.Pow(MatrixY[3, 0] - AvgColumnsY[3], 2) + Math.Pow(MatrixY[3, 1] - AvgColumnsY[3], 2) +
                     Math.Pow(MatrixY[3, 2] - AvgColumnsY[3], 2)) / 3;


            Gp = Dispresion.Max() / Dispresion.Sum();
            F1 = m - 1;
            F2 = n;
            Gt = 0.7679;
            //Kak v priemere
            Check_Koxran = Gp <= Gt;
            S2B = Dispresion.Sum() / n;
            S2Bs = S2B / (n * m);
            SBs = Math.Sqrt(S2Bs);

            beta_x = new double[4];
            BetaX();

            int f3 = (m - 1) * n;
            double tabl = 2.306;
            for (int i = 0; i < beta_x.Length; i++)
            {
                if (beta_x[i] < tabl)
                    beta_x[i] = 0;
                if ((int)beta_x[i] == 0)
                {
                    Bx[i] = 0;
                }
            }

            GetYXor();
            GetFisher();
        }

        private void GenertateMatrix(double[,] matrix)
        {
            Random random = new Random();
            for (int i = 0; i < MatrixY.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < MatrixY.GetUpperBound(1) + 1; j++)
                {
                    MatrixY[i, j] = random.Next((int)y_min, (int)y_max);
                }
            }
        }

        public string ShowMatrix(double[,] matrix)
        {
            string empty = string.Empty;
            for (int i = 0; i < matrix.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < matrix.GetUpperBound(1) + 1; j++)
                {
                    empty += $"{matrix[i, j]} ";
                }
                empty += "\n";
            }

            return empty;
        }
        public string ShowMatrix(int[,] matrix)
        {
            string empty = string.Empty;
            for (int i = 0; i < matrix.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < matrix.GetUpperBound(1) + 1; j++)
                {
                    empty += $"{matrix[i, j]} ";
                }
                empty += "\n";
            }

            return empty;
        }

        public void GetAveregeColumnsOfMatrixY()
        {
            double avg = 0;

            for (int i = 0; i < MatrixY.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < MatrixY.GetUpperBound(1) + 1; j++)
                {
                    avg += MatrixY[i, j];
                }

                AvgColumnsY[i] = avg / 4;

                avg = 0;
            }
        }

        private void GetMxs()
        {
            Mx[0] = (Xmin1 + Xmin1 + Xmax1 + Xmax1) / 4;
            Mx[1] = (Xmin2 + Xmin2 + Xmax2 + Xmax2) / 4;
            Mx[2] = (Xmin3 + Xmin3 + Xmax3 + Xmax3) / 4;
        }

        private void GetMy()
        {
            for (int i = 0; i < AvgColumnsY.Length; i++)
            {
                My += AvgColumnsY[i];
            }
            My /= 4;
        }

        private void GetAxs()
        {
            Ax[0] = (Xmin1 * AvgColumnsY[0] + Xmin1 * AvgColumnsY[1] + Xmax1 * AvgColumnsY[2] + Xmax1 * AvgColumnsY[3]) / n;
            Ax[1] = (Xmin2 * AvgColumnsY[0] + Xmax2 * AvgColumnsY[1] + Xmin2 * AvgColumnsY[2] + Xmax2 * AvgColumnsY[3]) / n;
            Ax[2] = (Xmin3 * AvgColumnsY[0] + Xmax3 * AvgColumnsY[1] + Xmax3 * AvgColumnsY[2] + Xmin3 * AvgColumnsY[3]) / n;

            Axx[0, 0] = (Xmin1 * 2 + Xmin1 * 2 + Xmax1 * 2 + Xmax1 * 2) / n;
            Axx[1, 1] = (Xmin2 * 2 + Xmin2 * 2 + Xmax2 * 2 + Xmax2 * 2) / n;
            Axx[2, 2] = (Xmin3 * 2 + Xmin3 * 2 + Xmax3 * 2 + Xmax3 * 2) / n;

            Axx[0, 1] = Axx[1, 0] = (Xmin1 * Xmin2 + Xmin1 * Xmax2 + Xmax1 * Xmin2 + Xmax1 * Xmax2) / n;
            Axx[0, 2] = Axx[2, 0] = (Xmin1 * Xmin3 + Xmin1 * Xmax3 + Xmax1 * Xmax3 + Xmax1 * Xmin3) / n;
            Axx[1, 2] = Axx[2, 1] = (Xmin2 * Xmin3 + Xmax2 * Xmax3 + Xmin2 * Xmax3 + Xmax2 * Xmin3) / n;
        }

        private void GetBx()
        {
            for (int i = 0; i < Bx.Length; i += 4)
            {
                Bx[i] = Determinate(b0) / Determinate(DividedMatrix);
                Bx[i + 1] = Determinate(b1) / Determinate(DividedMatrix);
                Bx[i + 2] = Determinate(b2) / Determinate(DividedMatrix);
                Bx[i + 3] = Determinate(b3) / Determinate(DividedMatrix);
            }
        }

        private double Determinate(double[,] matrix)
        {
            if (matrix.Length == 4)
            {
                return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
            }
            double sign = 1, result = 0;

            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                double[,] minr = GetMinor(matrix, i);
                result += sign * matrix[0, i] * Determinate(minr);
                sign = -sign;
            }

            return result;
        }

        private double[,] GetMinor(double[,] matrix, int n)
        {
            double[,] result = new double[matrix.GetLength(0) - 1, matrix.GetLength(0) - 1];

            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                for (int j = 0, col = 0; j < matrix.GetLength(1); j++)
                {
                    if (j == n)
                        continue;
                    result[i - 1, col] = matrix[i, j];
                    col++;
                }
            }
            return result;
        }

        private void BetaX()
        {
            BetaXArr = new double[4];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < Matrix_default.GetLength(0); j++)
                {
                    BetaXArr[i] += (AvgColumnsY[j] * Matrix_default[i, j]);
                }
                BetaXArr[i] /= 4.0;
                beta_x[i] = Math.Abs(BetaXArr[i]) / SBs;
            }
        }

        #region Y/\
        private void GetYXor()
        {
            Y_Xor = new double[n];
            Y_Xor[0] = Bx[0] + Bx[1] * Xmin1 + Bx[2] * Xmin2 + Bx[3] * Xmin3;
            Y_Xor[1] = Bx[0] + Bx[1] * Xmin1 + Bx[2] * Xmax2 + Bx[3] * Xmax3;
            Y_Xor[2] = Bx[0] + Bx[1] * Xmax1 + Bx[2] * Xmin2 + Bx[3] * Xmax3;
            Y_Xor[3] = Bx[0] + Bx[1] * Xmax1 + Bx[2] * Xmax2 + Bx[3] * Xmin3;
        }
        #endregion

        private void GetFisher()
        {
            int d = 1;
            int f4 = n - d;
            double sum = 0;

            for (int i = 0; i < AvgColumnsY.Length; i++)
            {
                sum += (Y_Xor[i] - AvgColumnsY[i]) * (Y_Xor[i] - AvgColumnsY[i]);
            }

            S2ad = (double)m / (n - d) * sum;

            Fp = S2ad / S2B;
            Ft = 4.1;

            if (Fp > Ft)
            {
                Check_Fisher = true;
            }
        }

        public void Message(string message, double[] array)
        {
            Console.WriteLine($"{message}:");

            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine("[{0}]: {1:f2}", i, array[i]);
            }
            Console.WriteLine();
            for (int i = 0; i < 50; i++)
                Console.Write("~");

            Console.WriteLine();
        }

        public void MessageMatrix(string message, double[,] matrix)
        {
            Console.WriteLine($"{message}:");
            Console.WriteLine(ShowMatrix(matrix));
            for (int i = 0; i < 50; i++)
                Console.Write("~");
            Console.WriteLine();
        }
        public void MessageMatrix(string message, int[,] matrix)
        {
            Console.WriteLine($"{message}:");
            Console.WriteLine(ShowMatrix(matrix));
            for (int i = 0; i < 50; i++)
                Console.Write("~");
            Console.WriteLine();
        }
    }
}
