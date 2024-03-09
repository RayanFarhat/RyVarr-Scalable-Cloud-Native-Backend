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
            double[] x = new double[n];
            double[,] augmentedMatrix = new double[n, n + 1];

            // Create augmented matrix [A | b]
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    augmentedMatrix[i, j] = coefficients[i, j];
                }
                augmentedMatrix[i, n] = constants[i];
            }

            // Perform Gaussian elimination
            for (int i = 0; i < n; i++)
            {
                // Find pivot for column i
                int maxRow = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (Math.Abs(augmentedMatrix[j, i]) > Math.Abs(augmentedMatrix[maxRow, i]))
                    {
                        maxRow = j;
                    }
                }

                // Swap current row with the row containing the pivot
                for (int k = i; k <= n; k++)
                {
                    double temp = augmentedMatrix[i, k];
                    augmentedMatrix[i, k] = augmentedMatrix[maxRow, k];
                    augmentedMatrix[maxRow, k] = temp;
                }

                // Make all elements below the pivot zero in current column
                for (int j = i + 1; j < n; j++)
                {
                    double factor = augmentedMatrix[j, i] / augmentedMatrix[i, i];
                    for (int k = i; k <= n; k++)
                    {
                        augmentedMatrix[j, k] -= factor * augmentedMatrix[i, k];
                    }
                }
            }

            // Back substitution
            for (int i = n - 1; i >= 0; i--)
            {
                x[i] = augmentedMatrix[i, n];
                for (int j = i + 1; j < n; j++)
                {
                    x[i] -= augmentedMatrix[i, j] * x[j];
                }
                x[i] /= augmentedMatrix[i, i];
            }

            return x;
        }
    }
}
