using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5
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
    public class Vertex : ICloneable
    {
        public object Name { get; set; }
        public List<Vertex> Connects { get; set; }
        public int D { get; set; }
        public bool F { get; set; } = false;
        public List<Vertex> PathTo { get; set; } = new List<Vertex>();

        public Vertex(object name)
        {
            Name = name;
            Connects = new List<Vertex>();
        }
        public override string ToString()
        {
            return Name.ToString();
        }

        public static bool operator ==(Vertex v1, Vertex v2)
        {
            return v1.Name == v2.Name;
        }
        public static bool operator !=(Vertex v1, Vertex v2)
        {
            return v1.Name != v2.Name;
        }
        public override bool Equals(object obj)
        {
            if (obj is Vertex)
            {
                if (base.Equals(obj))
                    return true;
                Vertex v = (Vertex)obj;
                if (v.Name == Name)
                    return true;
                return false;
            }
            else
                return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public object Clone()
        {
            return MemberwiseClone();
        }
        public static Vertex[] GetVertexs(int[,] smatr, object[] names = null)
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
        public static Vertex[] GetCloneListVertexs(Vertex[] vs)
        {
            Vertex[] outvs = new Vertex[vs.Length];
            for (int i = 0; i < outvs.Length; i++)
                outvs[i] = (Vertex)vs[i].Clone();
            return outvs;
        }
    }
}
