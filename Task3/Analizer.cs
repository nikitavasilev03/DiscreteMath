using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public static class Analizer
    {
        public static bool IsDigraph { get; set; } = true;
        private struct Incedent
        {
            public int I { get; set; }
            public int J { get; set; }
            public Incedent(int i, int j)
            {
                I = i;
                J = j;
            }
        }
        public static int[,] GetMatrixIncedents(int[,] smatr, object[] vertexs, out object[] names)
        {
            List<Incedent> inc = new List<Incedent>();
            for (int i = 0; i < smatr.GetLength(0); i++)
                for (int j = 0; j < smatr.GetLength(1); j++)
                {
                    if (smatr[i, j] != 0)
                        if (IsDigraph)
                            inc.Add(new Incedent(i, j));
                        else
                        {
                            bool f = true;
                            foreach (var item in inc)
                                if (item.J == i && item.I == j)
                                    f = false;
                            if (f)
                                inc.Add(new Incedent(i, j));
                        }
                }
            var incarr = inc.ToArray();
            int length = incarr.Length;
            int[,] matr = matr = new int[smatr.GetLength(0), length];
            names = new string[length];
            for (int i = 0; i < length; i++)
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
                if (smatr[i, i] != 0)
                    count += 2;
                for (int j = 0; j < length; j++)
                {
                    if (i != j && smatr[i, j] != 0)
                        count++;
                    if (IsDigraph)
                        if (i != j && smatr[j, i] != 0)
                            count++;
                }
                if (count > max)
                    max = count;
            } 
            return max;
        }
        public static int SvyaznostGraph(Vertex[] vs)
        {
            int c = 0;
            List<Vertex> V = Vertex.GetCloneListVertexs(vs).ToList();
            while (V.Count > 0)
            {
                Vertex y = V.First();
                List<Vertex> soed = new List<Vertex>();
                GetSoedinenie(y, soed);
                foreach (var item in soed)
                    V.Remove(item);
                c++;
            }
            return c;
        }
        public static int SvyaznostOrgraph(int[,] smatr)
        {
            int length = smatr.GetLength(0);
            int sp = 0, sm = 0, sn = 0,svyaz=0,pr=0;
            for (int i = 0; i < length; i++)
            {
                sp = 0;
                for (int j = 0; j < length; j++)
                {
                    if (i != j)
                        sp += smatr[i, j];
                }
                if (sp == 0)
                    for (int k = 0; k < length; k++)
                    {
                        sm = 0;pr = 0;
                        for (int l = 0; l < length; l++)
                        {
                            if (k != l)
                                sm += smatr[l, k];
                        }
                        if (sm == 0)
                        {
                            if (i == k) return svyaz = 0;
                            else return svyaz = 1;
                        }
                    }
            }
            for (int i = 0; i < length; i++)
            {
                sn = 0;
                for (int j = 0; j < length; j++)
                {
                    if (j != i)
                        sn += smatr[j, i];
                }
                if (sn == 0) return svyaz = 2;
            }
            for (int i = 0; i < length; i++)
            {
                pr = 0;
                for (int j = 0; j < length; j++)
                {
                    if (j != i)
                        pr += smatr[i, j];
                }
                if (pr == 0) return svyaz = 2;
            }

            return svyaz =3;
        }
        public static void GetSoedinenie(Vertex v, List<Vertex> vs)
        {
            vs.Add(v);
            foreach (var item in v.Connects)
                if (vs.IndexOf(item) == -1)
                    GetSoedinenie(item, vs);
        }
    }
}
