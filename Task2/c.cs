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
            foreach (var item in tMatrix)
                item.KeyDown += Form1_KeyDown;
            CreateTable(tLine2, tColumn2, tItem2, ref tLines2, ref tColumns2, ref tMatrix2, (int)numericUpDown2.Value);
            foreach (var item in tMatrix2)
                item.KeyDown += Form1_KeyDown1;

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox t = (TextBox)sender;
            int di = 0, dj = 0;
            if (FindInMatrix(t, tMatrix, out int i, out int j))
            {
                switch (e.KeyCode)
                {
                    case Keys.Down: di = 1; dj = 0; break;
                    case Keys.Up: di = -1; dj = 0; break;
                    case Keys.Left: di = 0; dj = -1; break;
                    case Keys.Right: di = 0; dj = 1; break;
                }
            }
            if (i + di < tMatrix.GetLength(0) && i + di >= 0 && j + dj < tMatrix.GetLength(1) && j + dj >= 0)
                tMatrix[i + di, j + dj].Focus();
        }
        private void Form1_KeyDown1(object sender, KeyEventArgs e)
        {
            TextBox t = (TextBox)sender;
            int di = 0, dj = 0;
            if (FindInMatrix(t, tMatrix2, out int i, out int j))
            {
                switch (e.KeyCode)
                {
                    case Keys.Down: di = 1; dj = 0; break;
                    case Keys.Up: di = -1; dj = 0; break;
                    case Keys.Left: di = 0; dj = -1; break;
                    case Keys.Right: di = 0; dj = 1; break;
                }
            }
            if (i + di < tMatrix2.GetLength(0) && i + di >= 0 && j + dj < tMatrix2.GetLength(1) && j + dj >= 0)
                tMatrix2[i + di, j + dj].Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {   
            try
            {
                listBox1.Items.Clear();
                int[,] A = GetFromTableIntMatrix(tMatrix, 1, (int)numericUpDown1.Value);
                int[,] B = GetFromTableIntMatrix(tMatrix2, 1, (int)numericUpDown1.Value);

                bool fRazreshimost = Analyzer.Razreshimost(A);
                bool fAssociativnost = Analyzer.Associativnost(A);
                bool fE = Analyzer.GetE(A, out int elem);
                bool fComunitotivnost = Analyzer.Comunitotivnost(A);
                bool fIdempotentnost = Analyzer.Idempotentnost(A);

                bool fRazreshimost2 = Analyzer.Razreshimost(B);
                bool fAssociativnost2 = Analyzer.Associativnost(B);
                bool fE2 = Analyzer.GetE(B, out int elem2);
                bool fComunitotivnost2 = Analyzer.Comunitotivnost(B);
                bool fIdempotentnost2 = Analyzer.Idempotentnost(B);
                //Опреция 1
                listBox1.Items.Add("Операция 1:");
                listBox1.Items.Add("Разрешимость = " + fRazreshimost);
                listBox1.Items.Add("Ассоциативность = " + fAssociativnost);
                if (fE)
                    listBox1.Items.Add("Нейтральный элемент = " + elem);
                else
                    listBox1.Items.Add("Нет нейтрального элемента");
                listBox1.Items.Add("Коммутативность = " + fComunitotivnost);
                listBox1.Items.Add("Идемпотентность = " + fIdempotentnost);
                listBox1.Items.Add("Обратимость = " + (fRazreshimost && fE));

                listBox1.Items.Add("");//определение группоидов
                listBox1.Items.Add("Полугруппа = " + fAssociativnost);
                listBox1.Items.Add("Квазигруппа = " + fRazreshimost);
                listBox1.Items.Add("Моноид = " + (fAssociativnost && fE));
                listBox1.Items.Add("Лупа = " + (fRazreshimost && fE));
                listBox1.Items.Add("Группа = " + (fRazreshimost && fAssociativnost && fE));
                listBox1.Items.Add("Абелева группа = " + (fRazreshimost && fComunitotivnost && fAssociativnost && fE));
                //Опреция 2
                listBox1.Items.Add("");
                listBox1.Items.Add("Операция 2:");
                listBox1.Items.Add("Разрешимость = " + fRazreshimost2);
                listBox1.Items.Add("Ассоциативность = " + fAssociativnost2);
                if (fE2)
                    listBox1.Items.Add("Нейтральный элемент = " + elem2);
                else
                    listBox1.Items.Add("Нет нейтрального элемента");
                listBox1.Items.Add("Коммутативность = " + fComunitotivnost2);
                listBox1.Items.Add("Идемпотентность = " + fIdempotentnost2);
                listBox1.Items.Add("Обратимость = " + (fRazreshimost2 && fE2));

                listBox1.Items.Add("");//определение группоидов
                listBox1.Items.Add("Полугруппа = " + fAssociativnost2);
                listBox1.Items.Add("Квазигруппа = " + fRazreshimost2);
                listBox1.Items.Add("Моноид = " + (fAssociativnost2 && fE2));
                listBox1.Items.Add("Лупа = " + (fRazreshimost2 && fE2));
                listBox1.Items.Add("Группа = " + (fRazreshimost2 && fAssociativnost2 && fE2));
                listBox1.Items.Add("Абелева группа = " + (fRazreshimost2 && fComunitotivnost2 && fAssociativnost2 && fE2));

                listBox1.Items.Add("");
                listBox1.Items.Add(
                    "Решетка = " +
                    (
                        (fAssociativnost && fAssociativnost2) &&
                        (fComunitotivnost && fComunitotivnost2) &&
                        (fIdempotentnost && fIdempotentnost2) &&
                        Analyzer.Pogloshenie(A, B)
                    )
                );
                listBox1.Items.Add(
                    "Дистрибутивность = " +
                    ( 
                        Analyzer.Distributivnost(A, B)
                    )
                );
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Замкнутость = false");
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
