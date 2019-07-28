using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task2
{
    public partial class Form1 : Form
    {
        TextBox[] tLines, tLines2;
        TextBox[] tColumns, tColumns2;
        TextBox[,] tMatrix, tMatrix2;
        public Form1()
        {
            InitializeComponent();
        }
 
        private void Form1_Load(object sender, EventArgs e)
        {
            //tLines = null; tColumn = null; tMatrix = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateTable(tLine, tColumn, tItem, ref tLines, ref tColumns, ref tMatrix, (int)numericUpDown1.Value);
            for (int i = 0; i < tMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < tMatrix.GetLength(1); j++)
                {
                    tMatrix[i, j].KeyPress += Form1_KeyPress;
                }
            }
            CreateTable(tLine2, tColumn2, tItem2, ref tLines2, ref tColumns2, ref tMatrix2, (int)numericUpDown2.Value);
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox t = (TextBox)sender;
            if (FindInMatrix(t, tMatrix, out int i, out int j))
        }

        private void button2_Click(object sender, EventArgs e)
        {   
            try
            {
                listBox1.Items.Clear();
                int[,] A = GetFromTableIntMatrix(tMatrix, 1, (int)numericUpDown1.Value);

                bool fRazreshimost = Analyzer.Razreshimost(A);
                bool fAssociativnost = Analyzer.Associativnost(A);
                bool fE = Analyzer.GetE(A, out int elem);
                bool fComunitotivnost = Analyzer.Comunitotivnost(A);

                if (fRazreshimost)
                    listBox1.Items.Add("Разрешимость");
                else
                    listBox1.Items.Add("Неразрешимость");
                if (fAssociativnost)
                    listBox1.Items.Add("Ассоциативность");
                else
                    listBox1.Items.Add("Не ассоциативность");
                if (fE)
                    listBox1.Items.Add("Нейтральный элемент - " + elem);
                else
                    listBox1.Items.Add("Нет нейтрального элемента");
                if (fComunitotivnost)
                    listBox1.Items.Add("Коммутативность");
                else
                    listBox1.Items.Add("Не коммутативность");

                listBox1.Items.Add("");//определение группоидов
                if (fAssociativnost)
                    listBox1.Items.Add("Полугруппа");
                else listBox1.Items.Add("Не является Полуруппой");

                if (fRazreshimost)
                    listBox1.Items.Add("Квазигруппа");
                else listBox1.Items.Add("Не является Квазигруппой");

                if (fAssociativnost && fE)
                    listBox1.Items.Add("Моноид");
                else listBox1.Items.Add("Не является Моноидом");

                if (fRazreshimost && fE)
                    listBox1.Items.Add("Лупа");
                else listBox1.Items.Add("Не является Лупой");

                if (fRazreshimost && fAssociativnost && fE)
                    listBox1.Items.Add("Группа");
                else listBox1.Items.Add("Не является Группой");

                if (fRazreshimost && fComunitotivnost && fAssociativnost && fE)
                    listBox1.Items.Add("Абелева группа");
                else listBox1.Items.Add("Не является Абелевой группой");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CreateTable(TextBox tLine, TextBox tColumn, TextBox tItem, 
             ref TextBox[] tLines, ref TextBox[] tColumns, ref TextBox[,] tMatrix, int length)
        {
            DeleteTable(tLines, tColumns, tMatrix);
            tColumns = new TextBox[length];
            for (int i = 0; i < length; i++)
            {
                tColumns[i] = new TextBox();
                tColumns[i].Left = tColumn.Left + i * tColumn.Width + 7;
                tColumns[i].Top = tColumn.Top;
                tColumns[i].Width = tColumn.Width;
                tColumns[i].Height = tColumn.Height;
                tColumns[i].BackColor = tColumn.BackColor;
                tColumns[i].Font = tColumn.Font;
                tColumns[i].TextAlign = tColumn.TextAlign;
                tColumns[i].Parent = tColumn.Parent;
                tColumns[i].Enabled = false;
                tColumns[i].Text = (i + 1).ToString();
            }
            tLines = new TextBox[length];
            for (int i = 0; i < length; i++)
            {
                tLines[i] = new TextBox();
                tLines[i].Left = tLine.Left;
                tLines[i].Top = tLine.Top + i * tLine.Height + 7;
                tLines[i].Width = tLine.Width;
                tLines[i].Height = tLine.Height;
                tLines[i].BackColor = tLine.BackColor;
                tLines[i].Font = tLine.Font;
                tLines[i].TextAlign = tLine.TextAlign;
                tLines[i].Parent = tLine.Parent;
                tLines[i].Enabled = false;
                tLines[i].Text = (i + 1).ToString();
            }
            tMatrix = new TextBox[length, length];
            for (int i = 0; i < length; i++)
                for (int j = 0; j < length; j++)
                {
                    tMatrix[i, j] = new TextBox();
                    tMatrix[i, j].Left = tItem.Left + j * tItem.Width + 7;
                    tMatrix[i, j].Top = tItem.Top + i * tItem.Height + 7;
                    tMatrix[i, j].Width = tItem.Width;
                    tMatrix[i, j].Height = tItem.Height;
                    tMatrix[i, j].BackColor = tItem.BackColor;
                    tMatrix[i, j].Font = tItem.Font;
                    tMatrix[i, j].TextAlign = tItem.TextAlign;
                    tMatrix[i, j].Parent = tItem.Parent;
                    
                }
        }
        private void DeleteTable(TextBox[] tLines, TextBox[] tColumns, TextBox[,] tMatrix)
        {
            if (tLines != null)
                foreach (var item in tLines)
                    item.Dispose();
            if (tColumns != null)
                foreach (var item in tColumns)
                    item.Dispose();
            if (tMatrix != null)
                foreach (var item in tMatrix)
                    item.Dispose();
            Refresh();
        }
        private int[,] GetFromTableIntMatrix(TextBox[,] tMatrix, int min, int max)
        {
            int[,] matrix = new int[tMatrix.GetLength(0), tMatrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (tMatrix[i, j].Text == "")
                        matrix[i, j] = min;
                    else
                    {
                        matrix[i, j] = Convert.ToInt32(tMatrix[i, j].Text);
                        if (matrix[i, j] < min || matrix[i, j] > max)
                            throw new ArgumentOutOfRangeException($"Числа должны быть заданы от {min} от {max}!");
                    }
                }
            return matrix;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown1.Value = numericUpDown2.Value;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown2.Value = numericUpDown1.Value;
        }
        private bool FindInMatrix(object item, object[,] matrix, out int i, out int j)
        {
            i = 0; j = 0;
            for (i = 0; i < matrix.GetLength(0); i++)
            {
                for (j = 0; j < matrix.GetLength(1); j++)
                {
                    if (item.Equals(matrix[i, j]))
                        return true;
                }
            }
            return false;
        }
    }
}
