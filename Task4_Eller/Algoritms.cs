using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4
{
    public static class Algoritms
    {
        public static Vertex[] Eller(Vertex[] vs, Vertex start)
        {
            foreach (var item in vs)
                if (item.Connects.Count % 2 == 1)
                    throw new ArgumentException("Присутствуют вершины с нечетной степенью");
            Stack<Vertex> stack = new Stack<Vertex>();
            Stack<Vertex> res = new Stack<Vertex>();
            stack.Push(start);
            Vertex v, u;
            while (stack.Count != 0)
            {
                v = stack.Peek();
                if (v.Connects?.Count != 0)
                {
                    u = v.Connects.First();
                    stack.Push(u);
                    v.Connects.Remove(u);
                    u.Connects.Remove(v);
                }
                else
                {
                    v = stack.Pop();
                    res.Push(v);
                }
            }
            return res.ToArray();
        } 
    }
}
