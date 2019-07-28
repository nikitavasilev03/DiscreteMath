using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task5
{
    public class MatrixTextboxN : MatrixTextbox
    {
        public MatrixTextboxN(TextBox nLine, TextBox nColumn, TextBox nItem) : base(nLine, nColumn, nItem)
        {

        }
        public void SetEventTextChange()
        {
            foreach (var item in items)
                item.TextChanged += Item_TextChanged;
        }

        private void Item_TextChanged(object sender, EventArgs e)
        {
            FindInMatrix(sender, out int i, out int j);
            items[j, i].Text = items[i, j].Text;
        }
    }
}
