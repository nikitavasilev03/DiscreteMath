using System.Collections.Generic;
using System.Linq;

namespace Mnojestva
{
    public static class MnOperations
    {
        public static string MnToStr(string[] mn)
        {
            string s = "";
            foreach (var item in mn)
                s += item + ", ";
            if (s.Length > 1)
                return s.Remove(s.Length - 2, 2);
            else
                return s;
        }
        public static string[] GetElementsFromString(string text, char c = ',')
        {
            return text.Replace(" ", "").Split(c);
        }
        public static string[] RemoveSameElements(string[] str)
        {
            List<string> list = str.ToList();
            for (int i = 0; i < list.Count; i++)
                for (int j = 0; j < list.Count; j++)
                    if (list[i] == list[j] && i != j)
                        list.RemoveAt(j);
            return list.ToArray();
        }
        public static string[] RemoveSameElements(string[] str, out int count)
        {
            List<string> list = str.ToList();
            count = 0;
            for (int i = 0; i < list.Count; i++)
                for (int j = 0; j < list.Count; j++)
                    if (list[i] == list[j] && i != j)
                    {
                        list.RemoveAt(j);
                        count++;
                    }
            return list.ToArray();
        }
        public static bool IsEqual(string[] mnA, string[] mnB)
        {
            List<string> listA = mnA.ToList();
            List<string> listB = mnB.ToList();
            for (int i = 0; i < listA.Count; i++)
            {
                if (listB.IndexOf(listA[i]) != -1)
                {
                    listB.Remove(listA[i]);
                    listA.RemoveAt(i);
                    i--;
                }
            }
            if (listA.Count == 0 && listB.Count == 0)
                return true;
            else
                return false;
        }
        public static bool IsSubset(string[] mnA, string[] mnB)
        {
            if (mnA.Intersect(mnB).ToArray().Length == mnA.Length)
                return true;
            else
                return false;
        }
        public static string[] Or(string[] mnA, string[] mnB)
        {
            return mnA.Union(mnB).ToArray();
        }
        public static string[] And(string[] mnA, string[] mnB)
        {
            return mnA.Intersect(mnB).ToArray();
        }
        public static string[] Addition(string[] mnA, string[] mnU)
        {
            List<string> listU = mnU.ToList();
            foreach (var item in mnA)
                listU.Remove(item);
            return listU.ToArray();
        }
        public static string[] Subtraction(string[] mnA, string[] mnB)
        {
            List<string> listA = mnA.ToList();
            List<string> listB = mnB.ToList();
            for (int i = 0; i < listA.Count; i++)
            {
                if (listB.IndexOf(listA[i]) != -1)
                {
                    listA.RemoveAt(i);
                    i--;
                }
            }
            return listA.ToArray();
        }
        public static string[] NotAnd(string[] mnA, string[] mnB)
        {
            return Subtraction(mnA, mnB).Union(Subtraction(mnB, mnA)).ToArray();
        }
        public static string Print(string name, string[] mn, bool printLength = false)
        {
            if (printLength)
                return $"{name} = " + "{" + MnToStr(mn) + "} " + $"|{name}| = " + mn.Length;
            else
                return $"{name} = " + "{" + MnToStr(mn) + "}";
        }
        public static string[] MnToUniverse(string[] mn, string[] universe)
        {
            List<string> listMn = mn.ToList();
            List<string> listU = universe.ToList();
            for (int i = 0; i < listMn.Count; i++)
                if (listU.IndexOf(listMn[i]) == -1)
                {
                    listMn.RemoveAt(i);
                    i--;
                }
            return listMn.ToArray();
        }
        public static string[] MnToUniverse(string[] mn, string[] universe, out int count)
        {
            count = 0;
            List<string> listMn = mn.ToList();
            List<string> listU = universe.ToList();
            for (int i = 0; i < listMn.Count; i++)
                if (listU.IndexOf(listMn[i]) == -1)
                {
                    listMn.RemoveAt(i);
                    i--;
                    count++;
                }
            return listMn.ToArray();
        }
    }
}
