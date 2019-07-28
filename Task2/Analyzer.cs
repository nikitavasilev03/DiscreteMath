using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public static class Analyzer
    {
        public static bool Razreshimost(int[,] A)
        {
            int m = A.GetLength(0);
            for (int i = 0; i < m; i++)
                for (int j = 0; j < m; j++)
                    for (int k = 0; k < m; k++)
                    {
                        if (A[i, j] == A[i, k] && j != k)
                            return false;
                        if (A[j, i] == A[k, i] && j != k)
                            return false;
                    }
            return true;
        }
        public static bool Associativnost(int[,] A)
        {
            int m = A.GetLength(0);
            for (int i = 0; i < m; i++)
                for (int j = 0; j < m; j++)
                    for (int k = 0; k < m; k++)
                        if (A[i, A[j, k] - 1] != A[A[i, j] - 1, k])
                            return false;
            return true;
        }
        public static bool GetE(int[,] A, out int elem)
        {
            int m = A.GetLength(0);
            elem = 0;
            int l = 0;
            int[] test1 = new int[m];
            for (int i = 0; i < m; i++)
                test1[i] = 0;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (A[i, j] == j + 1)
                        l++;
                }
                if (l == m)
                    test1[i] = 1;
                l = 0;
            }
            for (int j = 0; j < m; j++)
            {
                if (test1[j] == 1)
                    for (int i = 0; i < m; i++)
                        if (A[i, j] == i + 1)
                            l++;
                if (l == m)
                    elem = A[j, j];
                l = 0;
            }
            if (elem > 0)
                return true;
            else
            {
                elem = 0;
                return false;
            }       
        }
        public static bool Comunitotivnost(int[,] A)
        {
            int m = A.GetLength(0);
            for (int i = 0; i < m; i++)
                for (int j = 0; j < m; j++)
                    if (A[i, j] != A[j, i])
                        return false;
            return true;
        }
        public static bool Idempotentnost(int[,] A)
        {
            int length = A.GetLength(0);
            for (int i = 0; i < length; i++)
                if ((i + 1) != A[i, i])
                    return false;
            return true;
        }
        public static bool Pogloshenie(int[,] A, int[,] B)
        {
            int length = A.GetLength(0);
            for (int i = 0; i < length; i++)
                for (int j = 0; j < length; j++)
                    if ((i+1) == A[i, B[i, j]-1] && (i+1) == B[i, A[i, j]-1])
                        return true;
            return false;
        }
        public static bool Distributivnost(int[,] A, int[,] B)
        {
            int length = A.GetLength(0);
            for (int i = 0; i < length; i++)
                for (int j = 0; j < length; j++)
                    for (int k = 0; k < length; k++)
                        if (B[i, A[j, k] - 1] == A[B[i, j] - 1, B[i, k] - 1] &&
                            A[i, B[j, k] - 1] == B[A[i, j] - 1, A[i, k] - 1])
                            return true;
            return false;
        }
    }
}
