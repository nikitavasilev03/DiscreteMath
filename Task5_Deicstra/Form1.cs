using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Maker;

namespace Task5
{
    public partial class Form1 : Form
    {
        Graphics graphics;
        Creator creator;
        MatrixTextbox inputMatrix;
        object[] nameVertex = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
        object[] nV;
        bool isDigraph;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            graphics = panel1.CreateGraphics();
            inputMatrix = new MatrixTextbox(tLine, tColumn, tItem);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int count = (int)numericUpDown1.Value;
            nV = new object[count];
            for (int i = 0; i < nV.Length; i++)
                nV[i] = nameVertex[i];
            DialogSwitch dialog = new DialogSwitch();
            var type = dialog.ShowDialog(this);
            inputMatrix.Dispose();
            if (type == DialogResult.OK)
            {
                isDigraph = false;
                //Матрица тексбоксов NxN, гдe a(i, j) = a(j, i)
                MatrixTextboxN matrix = new MatrixTextboxN(tLine, tColumn, tItem);
                matrix.Create(nV, nV);
                matrix.SetEventTextChange();
                inputMatrix = matrix;
            }
            else if (type == DialogResult.Yes)
            {
                isDigraph = true;
                //Матрица тексбоксов NxN
                MatrixTextbox matrix = new MatrixTextbox(tLine, tColumn, tItem);
                matrix.Create(nV, nV);
                inputMatrix = matrix;
            }
            else
                return;
            Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Зачистка
            Clear();
            
            int[,] smatr = inputMatrix.Int32Matrix();

            //Список вершин
            Vertex[] vs = Vertex.GetVertexs(smatr);
            for(int iii = 0; iii < nV.Length; iii++)
            {
                listBox1.Items.Add("Для вершины " + nV[iii]);
                Vertex[] vin = Vertex.GetVertexs(smatr);
                Algoritms.Deikctra(vin, vin[iii], inputMatrix.Int32Matrix());
                for (int i = 0; i < nV.Length; i++)
                {
                    vin[i].Name = nV[i];
                }
                foreach (var v in vin)
                {
                    listBox1.Items.Add(v + " : Расстояние = " + ((v.D == Algoritms.NULL_PATH) ? 0 : v.D) + " : Путь-> " + Armake.GetAsString(v.PathTo.ToArray()));
                }
                listBox1.Items.Add("");
            }
            
            
            creator = new Creator(smatr, isDigraph);
            creator.Show(graphics);
        }

        private void Clear()
        {
            panel1.Refresh();
            listBox1.Items.Clear();
            Refresh();
        }
        private object Find(Vertex[] vs, Vertex v)
        {
            foreach (var item in vs)
                if (item == v)
                    return item;
            return null;
        }

    }
}
