using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task5
{
    public class MatrixTextbox
    {
        private TextBox nLine, nColumn, nItem;
        protected TextBox[,] items;
        private List<Control> controls = new List<Control>();
        public bool ReadOnly { get; set; } = false;
        public bool Enabled { get; set; } = true;
        
        public MatrixTextbox(TextBox nLine, TextBox nColumn, TextBox nItem)
        {
            this.nLine = nLine;
            this.nColumn = nColumn;
            this.nItem = nItem;
        }
        public void Create(object[] linesName, object[] columnsName, object[,] itemsName = null)
        {
            Dispose();
            TextBox[] lines = new TextBox[linesName.Length];
            TextBox[] columns = new TextBox[columnsName.Length];
            TextBox[,] items = new TextBox[lines.Length, columns.Length];
            Create(lines, columns, items);
            for (int i = 0; i < lines.Length; i++)
                lines[i].Text = linesName[i].ToString();
            for (int i = 0; i < columns.Length; i++)
                columns[i].Text = columnsName[i].ToString();
            if (itemsName != null)
            {
                for (int i = 0; i < items.GetLength(0); i++)
                    for (int j = 0; j < items.GetLength(1); j++)
                        items[i, j].Text = itemsName[i, j].ToString();
            }
            this.items = items;
        }
        public void Create(int countLines, int countColumns, int startIndex = 0)
        {
            Dispose();
            TextBox[] lines = new TextBox[countLines];
            TextBox[] columns = new TextBox[countColumns];
            TextBox[,] items = new TextBox[countLines, countColumns];
            Create(lines, columns, items);
            for (int i = 0; i < lines.Length; i++)
                lines[i].Text = (i + startIndex).ToString();
            for (int i = 0; i < columns.Length; i++)
                columns[i].Text = (i + startIndex).ToString();
            this.items = items;
        }
        private void Create(TextBox[] lines, TextBox[] columns, TextBox[,] items)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = new TextBox();
                lines[i].Left = nLine.Left;
                lines[i].Top = nLine.Top + i * nLine.Height + 7;
                lines[i].Width = nLine.Width;
                lines[i].Height = nLine.Height;
                lines[i].BackColor = nLine.BackColor;
                lines[i].Font = nLine.Font;
                lines[i].TextAlign = nLine.TextAlign;
                lines[i].Parent = nLine.Parent;
                lines[i].Enabled = false;              
                controls.Add(lines[i]);
            }

            for (int i = 0; i < columns.Length; i++)
            {
                columns[i] = new TextBox();
                columns[i].Left = nColumn.Left + i * nColumn.Width + 7;
                columns[i].Top = nColumn.Top;
                columns[i].Width = nColumn.Width;
                columns[i].Height = nColumn.Height;
                columns[i].BackColor = nColumn.BackColor;
                columns[i].Font = nColumn.Font;
                columns[i].TextAlign = nColumn.TextAlign;
                columns[i].Parent = nColumn.Parent;
                columns[i].Enabled = false;
                controls.Add(columns[i]);
            }

            for (int i = 0; i < items.GetLength(0); i++)
                for (int j = 0; j < items.GetLength(1); j++)
                {
                    items[i, j] = new TextBox();
                    items[i, j].Left = nItem.Left + j * nItem.Width + 7;
                    items[i, j].Top = nItem.Top + i * nItem.Height + 7;
                    items[i, j].Width = nItem.Width;
                    items[i, j].Height = nItem.Height;
                    items[i, j].BackColor = nItem.BackColor;
                    items[i, j].Font = nItem.Font;
                    items[i, j].TextAlign = nItem.TextAlign;
                    items[i, j].Parent = nItem.Parent;
                    items[i, j].ReadOnly = ReadOnly;
                    items[i, j].Enabled = Enabled;
                    items[i, j].KeyDown += Event_KeyDown;
                    controls.Add(items[i, j]);
                }
        }
        public void Dispose()
        {
            foreach (var item in controls)
                item.Dispose();
            controls.Clear();
        }
        private void Event_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox t = (TextBox)sender;
            int di = 0, dj = 0;
            if (FindInMatrix(t, out int i, out int j))
            {
                switch (e.KeyCode)
                {
                    case Keys.Down: di = 1; dj = 0; break;
                    case Keys.Up: di = -1; dj = 0; break;
                    case Keys.Left: di = 0; dj = -1; break;
                    case Keys.Right: di = 0; dj = 1; break;
                }
            }
            if (i + di < items.GetLength(0) && i + di >= 0 && j + dj < items.GetLength(1) && j + dj >= 0)
                items[i + di, j + dj].Focus();

        }
        protected bool FindInMatrix(object item, out int i, out int j)
        {
            i = 0; j = 0;
            for (i = 0; i < items.GetLength(0); i++)
                for (j = 0; j < items.GetLength(1); j++)
                    if (item.Equals(items[i, j]))
                        return true;
            return false;
        }
        public int[,] Int32Matrix()
        {
            int[,] matrix = new int[items.GetLength(0), items.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    if (items[i, j].Text == "")
                        matrix[i, j] = 0;
                    else
                        matrix[i, j] = Convert.ToInt32(items[i, j].Text);
            return matrix;
        }
        public object[,] ObjectMatrix()
        {
            object[,] matrix = new object[items.GetLength(0), items.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    if (items[i, j].Text == "")
                        matrix[i, j] = 0;
                    else
                        matrix[i, j] = items[i, j].Text;
            return matrix;
        }
        public string[,] StringMatrix()
        {
            string[,] matrix = new string[items.GetLength(0), items.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    matrix[i, j] = items[i, j].Text;
            return matrix;
        }
        public static object[] TypeToObjects<T>(T[] items)
        {
            object[] objs = new object[items.Length];
            for (int i = 0; i < objs.Length; i++)
                objs[i] = items[i];
            return objs;
        }
        public static object[,] TypeToObjects<T>(T[,] items)
        {
            object[,] objs = new object[items.GetLength(0), items.GetLength(1)];
            for (int i = 0; i < objs.GetLength(0); i++)
                for (int j = 0; j < objs.GetLength(1); j++)
                    objs[i, j] = items[i, j];
            return objs;
        }
    }
}
