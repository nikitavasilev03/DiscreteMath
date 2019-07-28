using System;
using System.Windows.Forms;

namespace Mnojestva
{
    public partial class FInputMn : Form
    {
        public string[] MN { get; private set; }
        public FInputMn()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MN = MnOperations.GetElementsFromString(textBox1.Text);
                if (MN != null && MN.Length == 1 && MN[0] == "")
                {
                    MessageBox.Show("Нет элементов!", "Ошибка!");
                    return;
                }
                MN = MnOperations.RemoveSameElements(MN, out int count);
                if (count > 0)
                    MessageBox.Show("Однинаковые элементы не допустимы. Элементов удалено - " + count, "Одинаковые элементы!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
            if (MN.Length > 25)
                MessageBox.Show("Элементов больше 25!", "Ошибка!");
            else
                DialogResult = DialogResult.OK;
            
        }
    }
}
