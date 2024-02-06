using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RYBIM.Mathematics
{
    internal static partial class MatrixFunctions
    {
        public static double[] Solve(double[,] coefficients, double[] constants)
        {
            int n = constants.Length;

            // Forward Elimination
            for (int pivot = 0; pivot < n - 1; pivot++)
            {
                for (int row = pivot + 1; row < n; row++)
                {
                    double factor = coefficients[row, pivot] / coefficients[pivot, pivot];
                    for (int col = pivot; col < n; col++)
                    {
                        coefficients[row, col] -= factor * coefficients[pivot, col];
                    }
                    constants[row] -= factor * constants[pivot];
                }
            }

            // Back Substitution
            double[] solution = new double[n];
            for (int i = n - 1; i >= 0; i--)
            {
                double sum = 0.0;
                for (int j = i + 1; j < n; j++)
                {
                    sum += coefficients[i, j] * solution[j];
                }
                solution[i] = (constants[i] - sum) / coefficients[i, i];
            }

            return solution;
        }
    }
}
