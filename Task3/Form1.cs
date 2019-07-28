using System;
using System.Drawing;
using System.Windows.Forms;
using Graph;

namespace Task3
{
    public partial class Form1 : Form
    {
        Graphics graphics;
        Creator creator;
        MatrixTextbox inputMatrix, matrixInced;
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
            matrixInced = new MatrixTextbox(textBox3, textBox2, textBox1);
            matrixInced.ReadOnly = true;
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
                MatrixTextboxN matrix = new MatrixTextboxN(tLine, tColumn, tItem);
                matrix.Create(nV, nV);
                matrix.SetEventTextChange();
                inputMatrix = matrix;
            } 
            else if (type == DialogResult.Yes)
            {
                isDigraph = true;
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
            try
            {
                Clear();
                Analizer.IsDigraph = isDigraph;
                int[,] smatr = inputMatrix.Int32Matrix();
                int[,] inc = Analizer.GetMatrixIncedents(smatr, nV, out object[] namesInc);
                matrixInced.Create(nV, namesInc, MatrixTextbox.TypeToObjects(inc));
                foreach (var item in Analizer.GetListIncedents(smatr, nV))
                    listBox1.Items.Add(item);

                creator = new Creator(smatr, isDigraph);
                creator.Show(graphics);

                listBox4.Items.Add("Количество вершин: " + nV.Length);
                if (isDigraph)
                    listBox4.Items.Add("Количество дуг: " + Analizer.GetEdgeCount(smatr));
                else
                    listBox4.Items.Add("Количество ребер: " + Analizer.GetEdgeCount(smatr));
                listBox4.Items.Add("Количество петель: " + Analizer.GetLoopCount(smatr));
                listBox4.Items.Add("Максимальная степень: " + Analizer.GetMaxVertexPower(smatr));
                if (isDigraph)
                    switch (Analizer.SvyaznostOrgraph(smatr))
                    {
                        case 0:
                            listBox4.Items.Add("Категория связности: Несвязный");
                            break;
                        case 1:
                            listBox4.Items.Add("Категория связности: Слабосвязный");
                            break;
                        case 2:
                            listBox4.Items.Add("Категория связности: Односвязный");
                            break;
                        case 3:
                            listBox4.Items.Add("Категория связности: Связный");
                            break;
                    }
                else
                    if (Analizer.SvyaznostGraph(Vertex.GetVertexs(smatr)) > 1)
                        listBox4.Items.Add("Категория связности: Несвязный");
                    else
                        listBox4.Items.Add("Категория связности: Связный");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Clear()
        {
            panel1.Refresh();
            listBox1.Items.Clear();
            listBox4.Items.Clear();
            matrixInced.Dispose();
            Refresh();
        }
        
    }
}
