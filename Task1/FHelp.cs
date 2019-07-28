using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mnojestva
{
    public partial class FHelp : Form
    {
        public FHelp()
        {
            InitializeComponent();
        }

        private void FHelp_Load(object sender, EventArgs e)
        {
            label2.Text = 
                "Универсум:\n\r" +
                "   Для задания универсума необходима нажать кнопку \"Ввести\" в группе \"Универсум\".\n\r" +
                "Множество А:\n\r" +
                "   Для задания множества А необходимо ввести элементы вручную, либо выбрать путем нажатия на элементы\n\r" +
                "универсума. При нажатии элементы окрашиваются в красный цвет и при повторном нажатии возвращают\n\r" +
                "исходный цвет. Для переноса выбраных элементов в множество необходимо нажать на кнопку\n\r" +
                "\"Распределить в A\", после чего красные элементы перенесутся в множество А.\n\r" +
                "Множество B:\n\r" +
                "   Анологично множеству А, но для переноса элементов нужно нажать \"Распределить в B\"\n\r" +
                "\n\r" +
                "Для последующих действий с множествами необходимо, что бы каждое из множеств было не пустое и содержало\n\r" +
                "до 25 элементов";
        }
    }
}
