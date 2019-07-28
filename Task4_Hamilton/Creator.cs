using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using Graph;

namespace Task4
{
    
    public class Creator
    {
        //Отрисовщик
        Painter painter;
        //Словарь
        object[] nameVertex = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };

        public Creator(int[,] smatrix, bool isDigraph)
        {
            //Проверка данных
            if (smatrix.GetLength(0) != smatrix.GetLength(1))
                throw new ArgumentException();
            int length = smatrix.GetLength(0);
            if (length < 1 || length > 10)
                throw new ArgumentException("Количество вершин некорректно");
            painter?.Dispose();

            //Вершины и объекты
            Graph.Vertex[] vertices = new Graph.Vertex[length];
            List<Shape> shapes = new List<Shape>();
            //Инициализация вершин
            for (int i = 0; i < length; i++)
            {
                vertices[i] = new Graph.Vertex() { Name = nameVertex[i] };
                shapes.Add(vertices[i]);
            }
            //Проведем связи (ребра, кольца)
            if (isDigraph)
            {
                //Если граф орентированный
                for (int i = 0; i < length; i++)
                    for (int j = 0; j < length; j++)
                        if (smatrix[i, j] > 0)

                            if (i != j)
                                shapes.Add(vertices[i].SetConnect(vertices[j], TypeConnect.DigraphOwn, smatrix[i, j]));
                            else
                                shapes.Add(vertices[i].SetConnect(vertices[i], TypeConnect.Diloop, smatrix[i, j]));
            }
            else
            {
                //Если граф неорентированный
                for (int i = 0; i < length; i++)
                    for (int j = 0; j < length; j++)
                        if (smatrix[i, j] > 0)

                            if (i != j)
                                shapes.Add(vertices[i].SetConnect(vertices[j], TypeConnect.Edge, smatrix[i, j]));
                            else
                                shapes.Add(vertices[i].SetConnect(vertices[i], TypeConnect.Loop, smatrix[i, j]));
            }
            shapes.RemoveAll(item => item == null);
            //Размер абстракного блока на плоскости экрана(Размер вешины)
            int size = 60;
            //Передаем все объекты которые нужно отрисовать отрисовщику, их размер и матрицу их визуального расположениея
            switch (length)
            {
                case 1:
                    painter = new Painter(shapes, size, new object[,]
                    {
                        {'A'},
                    });
                    break;
                case 2:
                    painter = new Painter(shapes, size, new object[,]
                    {
                        {'A', '0', 'B'},
                    });
                    break;
                case 3:
                    painter = new Painter(shapes, size, new object[,]
                    {
                        {'A', '0', 'B'},
                        {'0', '0', '0'},
                        {'0', 'C', '0'},
                    });
                    break;
                case 4:
                    painter = new Painter(shapes, size, new object[,]
                    {
                        {'A', '0', 'B'},
                        {'0', '0', '0'},
                        {'C', '0', 'D'},
                    });
                    break;
                case 5:
                    painter = new Painter(shapes, size, new object[,]
                    {
                        {'A', '0', '0', '0', 'B'},
                        {'0', '0', 'C', '0', '0'},
                        {'D', '0', '0', '0', 'E'},
                    });
                    break;
                case 6:
                    painter = new Painter(shapes, size, new object[,]
                    {
                        {'A', '0', '0', '0', 'B'},
                        {'0', 'C', '0', 'D', '0'},
                        {'E', '0', '0', '0', 'F'},
                    });
                    break;
                case 7:
                    painter = new Painter(shapes, size, new object[,]
                    {
                        {'0', '0', 'C', '0', '0'},
                        {'A', '0', '0', '0', 'B'},
                        {'0', 'D', '0', 'E', '0'},
                        {'F', '0', '0', '0', 'G'},
                    });
                    break;
                case 8:
                    painter = new Painter(shapes, size, new object[,]
                    {
                        {'0', '0', 'C', '0', '0'},
                        {'A', '0', '0', '0', 'B'},
                        {'0', 'D', '0', 'E', '0'},
                        {'F', '0', '0', '0', 'G'},
                        {'0', '0', 'H', '0', '0'},
                    });
                    break;
                case 9:
                    painter = new Painter(shapes, size, new object[,]
                    {
                        {'0', '0', 'C', '0', '0'},
                        {'A', '0', '0', '0', 'B'},
                        {'0', 'D', '0', 'E', '0'},
                        {'F', '0', '0', '0', 'G'},
                        {'0', 'H', '0', 'I', '0'},
                    });
                    break;
                case 10:
                    painter = new Painter(shapes, size, new object[,]
                    {
                        {'0', 'B', '0', 'C', '0'},
                        {'A', '0', '0', '0', 'D'},
                        {'0', 'E', '0', 'F', '0'},
                        {'G', '0', '0', '0', 'H'},
                        {'0', 'I', '0', 'J', '0'},
                    });
                    break;
                default:
                    break;
            }
        }
        //Отрисовка графа
        public void Show(Graphics graphics)
        {
            painter.Draw(graphics);
        }
    }
}
