using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4
{
    public struct Arc
    {
        public int I { get; set; }
        public int J { get; set; }
        public Arc(int i, int j)
        {
            I = i;
            J = j;
        }
    }
    public struct Vertex
    {
        public object Name { get; set; }
        public List<Vertex> Connects { get; set; }
        public Vertex(object name)
        {
            Name = name;
            Connects = new List<Vertex>();
        }
        public override string ToString()
        {
            return Name.ToString();
        }
        
    }
    public static class Analizer
    {
        
        public static bool IsDigraph { get; set; } = true;
          
        public static Vertex[] GetVertex(int[,] smatr, object[] names = null)
        {
            List<Vertex> vs = new List<Vertex>();
            int length = smatr.GetLength(0);
            for (int i = 0; i < length; i++)
            {
                if (names == null)
                    vs.Add(new Vertex(i));
                else
                    vs.Add(new Vertex(names[i]));
            }
            Vertex[] arVs = vs.ToArray();
            for (int i = 0; i < length; i++)
                for (int j = 0; j < length; j++)
                    if (smatr[i, j] != 0)
                        arVs[i].Connects.Add(arVs[j]);
            return arVs;
        }
        public static Arc[] GetArcs(int[,] smatr)
        {
            List<Arc> inc = new List<Arc>();
            for (int i = 0; i < smatr.GetLength(0); i++)
                for (int j = 0; j < smatr.GetLength(1); j++)
                {
                    if (smatr[i, j] != 0)
                        inc.Add(new Arc(i, j));
                }
            return inc.ToArray();
        }
        public static int[,] GetMatrixIncedents(int[,] smatr, object[] vertexs, out object[] names)
        {
            var incarr = GetArcs(smatr);
            int[,] matr = new int[smatr.GetLength(0), incarr.Length];
            names = new string[incarr.Length];
            for (int i = 0; i < incarr.Length; i++)
            {
                if (incarr[i].I == incarr[i].J)
                    matr[incarr[i].I, i] = 2;
                else
                {
                    matr[incarr[i].J, i] = 1;
                    if (IsDigraph)
                        matr[incarr[i].I, i] = -1;
                    else
                        matr[incarr[i].I, i] = 1;
                }
                names[i] = vertexs[incarr[i].I].ToString() + vertexs[incarr[i].J].ToString();
            }
            return matr;
        }
        public static string[] GetListIncedents(int[,] smatr, object[] vertexs)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < smatr.GetLength(0); i++)
            {
                string s = string.Empty;
                for (int j = 0; j < smatr.GetLength(1); j++)
                    if (smatr[i, j] != 0)
                        s += vertexs[j].ToString() + ", ";
                if (s == string.Empty)
                    list.Add(vertexs[i].ToString() + ": ");
                else
                    list.Add(vertexs[i].ToString() + ": " + s.Remove(s.Length - 2, 2));
            }
            return list.ToArray();
        }
        public static int GetEdgeCount(int[,] smatr)
        {
            int length = smatr.GetLength(0);
            int count = 0;
            if (IsDigraph)
            {
                for (int i = 0; i < length; i++)
                    for (int j = 0; j < length; j++)
                        if (i != j && smatr[i, j] != 0)
                            count++;
            }
            else
            {
                for (int i = 0; i < length; i++)
                    for (int j = i; j < length; j++)
                        if (i != j && smatr[i, j] != 0)
                            count++;
            }
            return count;
        }
        public static int GetLoopCount(int[,] smatr)
        {
            int length = smatr.GetLength(0);
            int count = 0;
            for (int i = 0; i < length; i++)
                if (smatr[i, i] != 0)
                    count++;
            return count;
        }
        public static int GetMaxVertexPower(int[,] smatr)
        {
            int length = smatr.GetLength(0);
            int count = 0;
            int max = 0;
            for (int i = 0; i < length; i++)
            {
                count = 0;
                for (int j = 0; j < length; j++)
                    if (i != j && smatr[i, j] != 0)
                        count++;
                if (count > max)
                    max = count;
            } 
            return max;
        }
        //Я **** даже разбираться в этом не буду
        public static string GetStrSvyaznost(int[,] mas) //Определяет связность матрицы. На вход требует матрицу смежности
        {
            int n = mas.GetLength(0);
            bool flag = true;
            int[,] masDost = new int[n, n]; //Матрица достижимости

            int[,] mas2 = new int[n, n]; //Матрица в квадрате
            int[,] mas3 = new int[n, n]; //Матрица в кубе
            int[,] mas4 = new int[n, n]; //Матрица в 4 степени
            //Они нужны для построения матрицы достижимости

            //Возведение матрицы в квадрат
            for (int i = 0; i < mas2.GetLength(0); i++)
            {
                for (int k = 0; k < mas2.GetLength(1); k++)
                {
                    for (int j = 0; j < mas.GetLength(1); j++)
                    {
                        mas2[i, k] += mas[j, k] * mas[i, j];
                    }

                }

            }

            //Возведение матрицы в куб
            for (int i = 0; i < mas3.GetLength(0); i++)
            {
                for (int k = 0; k < mas3.GetLength(1); k++)
                {
                    for (int j = 0; j < mas2.GetLength(1); j++)
                    {
                        mas3[i, k] += mas2[j, k] * mas[i, j];
                    }

                }

            }

            //Возведение матрицы в 4 степень
            for (int i = 0; i < mas4.GetLength(0); i++)
            {
                for (int k = 0; k < mas4.GetLength(1); k++)
                {
                    for (int j = 0; j < mas3.GetLength(1); j++)
                    {
                        mas4[i, k] += mas3[j, k] * mas[i, j];
                    }

                }

            }

            //Заполнение матрицы достижимости наложением всех 4 матриц друг на друга
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (mas[i, j] != 0 || mas2[i, j] != 0 || mas3[i, j] != 0 || mas4[i, j] != 0)
                    {
                        masDost[i, j] = 1;
                    }
                }
            }




            //Проверка на сильную связность
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (masDost[i, j] == 0 && i != j)
                    {
                        flag = false;
                        i = n; //Выход из проверки, если нашли хоть одну пару элементов несоответствующую текущей связности
                        break;
                    }
                }
            }

            if (flag)
                return "Связный";

            flag = true;

            //Проверка на односторонную связность
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (masDost[i, j] == 0 && masDost[j, i] == 0 && i != j)
                    {
                        flag = false;
                        i = n;//Выход из проверки, если нашли хоть одну пару элементов несоответствующую текущей связности
                        break;
                    }
                }
            }

            if (flag)
                return "Односторонне связный";

            flag = true;

            //Невероятный костыль, который вносит в матрицу смежности все дуги противоположные имеющимся. Эта матрица нужна будет для создания матрицы достижимости для проверки на слабую связность
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (mas[i, j] == 1)
                        mas[j, i] = 1;
                }
            }
            //Обнуляем все матрицы
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    mas2[i, j] = 0;
                    mas3[i, j] = 0;
                    mas4[i, j] = 0;
                    masDost[i, j] = 0;
                }
            }

            //В этом участке кода ма создаем матрицу достижимости для ненаправленного графа, чтобы проверить наш направленный граф на слабосвязность
            //Возведение матрицы в квадрат
            for (int i = 0; i < mas2.GetLength(0); i++)
            {
                for (int k = 0; k < mas2.GetLength(1); k++)
                {
                    for (int j = 0; j < mas.GetLength(1); j++)
                    {
                        mas2[i, k] += mas[j, k] * mas[i, j];
                    }

                }

            }

            //Возведение матрицы в куб
            for (int i = 0; i < mas3.GetLength(0); i++)
            {
                for (int k = 0; k < mas3.GetLength(1); k++)
                {
                    for (int j = 0; j < mas2.GetLength(1); j++)
                    {
                        mas3[i, k] += mas2[j, k] * mas[i, j];
                    }

                }

            }

            //Возведение матрицы в 4 степень
            for (int i = 0; i < mas4.GetLength(0); i++)
            {
                for (int k = 0; k < mas4.GetLength(1); k++)
                {
                    for (int j = 0; j < mas3.GetLength(1); j++)
                    {
                        mas4[i, k] += mas3[j, k] * mas[i, j];
                    }

                }

            }


            //Заполнение матрицы достижимости наложением всех 4 матриц друг на друга
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (mas[i, j] != 0 || mas2[i, j] != 0 || mas3[i, j] != 0 || mas4[i, j] != 0)
                    {
                        masDost[i, j] = 1;
                    }
                }
            }
            //Конец, костыля. Вот тут заканчивается создание этой матрицы

            //Проверка на слабую связность
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (masDost[i, j] == 0 && i != j)
                    {
                        flag = false;
                        i = n; //Выход из проверки, если нашли хоть одну пару элементов несоответствующую текущей связности
                        break;
                    }
                }
            }

            if (flag)
                return "Слабо связный";

            return "Не связный";

        }
    }
}
