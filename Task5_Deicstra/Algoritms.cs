using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5
{
    public static class Algoritms
    {
        public const int NULL_PATH =  2000000000;

        public static bool IsDigraph { get; set; } = true;
        public static void Deikctra(Vertex[] vs, Vertex s, int[,] smatr)
        {
            int length = smatr.GetLength(0);
            for (int i = 0; i < length; i++)
                for (int j = 0; j < length; j++)
                    if (smatr[i, j] == 0)
                        smatr[i, j] = NULL_PATH;
            foreach (var v in vs)
            {
                v.D = smatr[(int)s.Name, (int)v.Name];
                v.PathTo.Add(s);
            }
            s.F = true;
            while (GetNFVertex(vs).Count > 0)
            {
                Vertex u = GetMinRV(GetNFVertex(vs).ToArray());
                if (u.PathTo.IndexOf(u) == -1)
                    u.PathTo.Add(u);
                u.F = true;
                foreach (var v in GetNFVertex(u.Connects.ToArray()))
                {
                    int d = u.D + smatr[(int)u.Name, (int)v.Name];
                    if (d < v.D)
                    {
                        v.D = d;
                        v.PathTo.Clear();
                        v.PathTo.AddRange(u.PathTo);
                        v.PathTo.Add(v);
                    }
                }
            }  
        }
        public static List<Vertex> GetNFVertex(Vertex[] vs)
        {
            List<Vertex> vt = new List<Vertex>();
            foreach (var v in vs)
                if (!v.F)
                    vt.Add(v);
            return vt;
        }
        public static Vertex GetMinRV(Vertex[] vs)
        {
            Vertex vr = vs[0];
            foreach (var v in vs)
                if (v.D < vr.D)
                    vr = v;
            return vr;
        }
    }
}
