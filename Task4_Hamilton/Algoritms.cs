using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4
{
    public class Stack
    {
        private class Node //вложенный класс, реализующий элемент стека
        {
            private object inf;
            private Node next;
            public Node(object nodeInfo)
            {
                inf = nodeInfo;
                next = null;
            }
            public Node Next
            {
                get { return next; }
                set { next = value; }
            }
            public object Inf
            {
                get { return inf; }
                set { inf = value; }
            }
        } //конец класса Node
        private Node head; //ссылка на вершину стека
        public Stack() //конструктор класса, создает пустой стек
        {
            head = null;
        }
        public void Push(object nodeInfo) // добавляет элемент в вершину стека
        {
            Node r = new Node(nodeInfo);
            r.Next = head;
            head = r;
        }
        public object Pop() //извлекает элемент из вершины стека, если он не пуст
        {
            if (head == null)
            {
                throw new Exception("Стек пуст");
            }
            else
            {
                Node r = head;
                head = r.Next;
                return r.Inf;
            }
        }
        public bool IsEmpty //определяет пуст или нет стек
        {
            get
            {
                if (head == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
    public class Queue
    {
        private class Node //вложенный класс, реализующий базовый элемент очереди
        {
            private object inf;
            private Node next;
            public Node(object nodeInfo)
            {
                inf = nodeInfo;
                next = null;
            }
            public Node Next
            {
                get { return next; }
                set { next = value; }
            }
            public object Inf
            {
                get { return inf; }
                set { inf = value; }
            }
        } //конец класса Node
        private Node head;
        private Node tail;
        public Queue()
        {
            head = null;
            tail = null;
        }
        public void Add(object nodeInfo)
        {
            Node r = new Node(nodeInfo);
            if (head == null)
            {
                head = r;
                tail = r;
            }
            else
            {
                tail.Next = r;
                tail = r;
            }
        }
        public object Take()
        {
            if (head == null)
            {
                throw new Exception("Очередь пуста.");
            }
            else
            {
                Node r = head;
                head = head.Next;
                if (head == null)
                {
                    tail = null;
                }
                return r.Inf;
            }
        }
        public bool IsEmpty
        {
            get
            {
                if (head == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }

    public class GGraph
    {
        public static List<List<object>> lists = new List<List<object>>();
        private static int gName_i;
        private static int gName_j;

        private class Node //вложенный класс для скрытия данных и алгоритмов
        {
            private int[,] array; //матрица смежности
            public int this[int i, int j] //индексатор для обращения к матрице смежности
            {
                get
                {
                    return array[i, j];
                }
                set
                {
                    array[i, j] = value;
                }
            }
            public bool this[int i] //индексатор для обращения к матрице меток
            {
                get
                {
                    return nov[i];
                }
                set
                {
                    nov[i] = value;
                }
            }
            public int Size //свойство для получения размерности матрицы смежности
            {
                get
                {
                    return array.GetLength(0);
                }
            }
            private bool[] nov; //вспомогательный массив: если i-ый элемент массива равен
                                //true, то i-ая вершина еще не просмотрена; если i-ый
                                //элемент равен false, то i-ая вершина просмотрена
            public void NovSet() //метод помечает все вершины графа как непросмотреные
            {
                for (int i = 0; i < Size; i++)
                {
                    nov[i] = true;
                }
            }
            //конструктор вложенного класса, инициализирует матрицу смежности и
            // вспомогательный массив
            public Node(int[,] a)
            {
                array = a;
                nov = new bool[a.GetLength(0)];
            }
            //реализация алгоритма обхода графа в глубину
            public void Dfs(int v)
            {
                Console.Write("{0} ", v); //просматриваем текущую вершину
                nov[v] = false; //помечаем ее как просмотренную
                                // в матрице смежности просматриваем строку с номером v
                for (int u = 0; u < Size; u++)
                {
                    //если вершины v и u смежные, к тому же вершина u не просмотрена,
                    if (array[v, u] != 0 && nov[u])
                    {
                        Dfs(u); // то рекурсивно просматриваем вершину
                    }
                }
            }
            //реализация алгоритма обхода графа в ширину
            public void Bfs(int v)
            {
                Queue q = new Queue(); // инициализируем очередь
                q.Add(v); //помещаем вершину v в очередь
                nov[v] = false; // помечаем вершину v как просмотренную
                while (!q.IsEmpty) // пока очередь не пуста
                {
                    v = Convert.ToInt32(q.Take()); //извлекаем вершину из очереди
                    Console.Write("{0} ", v); //просматриваем ее
                    for (int u = 0; u < Size; u++) //находим все вершины
                    {
                        if (array[v, u] != 0 && nov[u]) // смежные с данной и еще не просмотренные
                        {
                            q.Add(u); //помещаем их в очередь
                            nov[u] = false; //и помечаем как просмотренные
                        }
                    }
                }
            }



            public void SearchG(int v, ref int[,] a, ref Stack c) //во вложенном классе
            {
                for (int i = 0; i < a.GetLength(0); i++)
                {
                    if (a[v, i] != 0)
                    {
                        a[v, i] = 0; a[i, v] = 0;
                        SearchG(i, ref a, ref c);
                    }
                }
                c.Push(v);
            }
            public void SearchGm(int k, ref int[] St)//вложенный класс
            {
                int v = St[k - 1];
                for (int j = 0; j < array.GetLength(0); j++)
                {
                    if (array[v, j] != 0)
                    {
                        if (k == array.GetLength(0) && j == 0)
                        {
                            St[k] = j;
                            List<object> list = new List<object>();
                            foreach (int item in St)
                            {
                                list.Add(item);
                            }
                            lists.Add(list);
                        }
                        else
                        {
                            if (nov[j])
                            {
                                St[k] = j;
                                nov[j] = false;
                                SearchGm(k + 1, ref St);
                                nov[j] = true;
                            }
                        }

                    }

                }

            }

        } //конец вложенного клаcса
        private Node graph; //закрытое поле, реализующее АТД «граф»
        public GGraph(int[,] smatr) //конструктор внешнего класса
        {
            graph = new Node(smatr);
        }
        public bool GraphCheck()
        {
            bool check = true;
            int count = 0;
            int temp = 0;
            for (int i = 0; i < graph.Size; i++)
            {
                for (int j = 0; j < graph.Size; j++)
                {
                    if (graph[i, j] != 0)
                        count++;
                }
                if (count % 2 != 0)
                {
                    temp++;
                }
                count = 0;
            }
            if (temp % 2 != 0)
                check = false;
            return check;
        }
        //метод выводит матрицу смежности на консольное окно
        public void Show()
        {
            for (int i = 0; i < graph.Size; i++)
            {
                for (int j = 0; j < graph.Size; j++)
                {
                    Console.Write("{0,4}", graph[i, j]);
                }
                Console.WriteLine();
            }
        }
        public void Dfs(int v)
        {
            graph.NovSet();//помечаем все вершины графа как непросмотренные
            graph.Dfs(v); //запускаем алгоритм обхода графа в глубину
            Console.WriteLine();
        }
        public void Bfs(int v)
        {
            graph.NovSet();//помечаем все вершины графа как непросмотренные
            graph.Bfs(v); //запускаем алгоритм обхода графа в ширину
            Console.WriteLine();
        }


        public void Neighbouring()
        {
            Console.Write("Вершины не смежные с вершиной: ");
            int v = int.Parse(Console.ReadLine());
            //просматриваем строку с номером v в матрице смежности
            for (int i = 0; i < graph.Size; i++)
            {
                //если на пересечении строки v и столбца i стоит не ноль, то вершина i является
                //соседней для вершины v
                if (graph[v, i] == 0 && v != i)
                {
                    Console.Write("{0} ", i);
                }
            }
            Console.WriteLine();
        }
        public void SearchG(int start) //во внешнем классе Эйлеров
        {
            int[,] a = new int[graph.Size, graph.Size];
            for (int i = 0; i < graph.Size; i++)
            {
                for (int j = 0; j < graph.Size; j++)
                {
                    a[i, j] = graph[i, j];
                }
            }
            Stack c = new Stack();
            graph.SearchG(start, ref a, ref c);
            //while (c.Count != 0)
            while (!c.IsEmpty)
            {
                Console.Write("{0} ", (int)c.Pop() + 1);
            }
        }
        public void SearchGm()//внешний класс Гамильтонов
        {
            lists = new List<List<object>>();
            graph.NovSet();
            int[] St = new int[graph.Size + 1];
            St[0] = 0;
            graph[0] = false; //обращение к индексатору
            int start = 1;
            graph.SearchGm(start, ref St);
        }

        public int GetLength()
        {
            return graph.Size;
        }
    }
}
