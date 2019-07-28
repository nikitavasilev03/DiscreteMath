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
    public partial class Form1 : Form
    {
        Button[] btnsU, btnsA, btnsB;
        string[] U, A, B;
        public Form1()
        {
            InitializeComponent();
            HelpButtonClicked += Form1_HelpButtonClicked;
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void button5_Click(object sender, EventArgs e)
        {
            ResetButtonsMN(btnsU);
            ResetButtonsMN(btnsA); btnsA = null;
            ResetButtonsMN(btnsB); btnsB = null;
            FInputMn fInputU = new FInputMn();
            if (fInputU.ShowDialog(this) == DialogResult.OK)
            {
                U = fInputU.MN;
                btnsU = CreateButtonsMN(U, button2);
                foreach (var item in btnsU)
                    item.MouseUp += buttonColor_Click;
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            if (btnsA != null && btnsB != null)
            {
                FCharacter fCharacter = new FCharacter(A, B, U);
                fCharacter.ShowDialog(this);
            }
            else
                MessageBox.Show("Множества не заполнены");
        }
        private void button10_Click(object sender, EventArgs e)
        {
            if (U == null)
                return;
            ResetButtonsMN(btnsB);
            List<string> ibtnsList = new List<string>();
            foreach (var item in btnsU)
                if (item.BackColor == Color.Red)
                    ibtnsList.Add(item.Text);
            B = ibtnsList.ToArray();
            if (B.Length == 0)
            {
                B = null;
                return;
            }
            btnsB = CreateButtonsMN(B, button4);
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if (U == null)
                return;
            ResetButtonsMN(btnsA);
            List<string> ibtnsList = new List<string>();
            foreach (var item in btnsU)
                if (item.BackColor == Color.Red)
                    ibtnsList.Add(item.Text);
            A = ibtnsList.ToArray();
            if (A.Length == 0)
            {
                A = null;
                return;
            }
            btnsA = CreateButtonsMN(A, button3);
        }
        private void button8_Click(object sender, EventArgs e)
        {
            ResetButtonsMN(btnsA); btnsA = null;
            FInputMn fInputA = new FInputMn();
            if (fInputA.ShowDialog(this) == DialogResult.OK)
            {
                A = fInputA.MN;
                A = MnOperations.MnToUniverse(A, U, out int x);
                if (x > 0)
                    MessageBox.Show("Некоторые элементы не содержит универсум. Элементов удалено - " + x, "Элементы не схожи с универсумом!");
                if (A.Length == 0)
                {
                    A = null;
                    return;
                }
                btnsA = CreateButtonsMN(A, button3);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ResetButtonsMN(btnsB); btnsB = null;
            FInputMn fInputB = new FInputMn();
            if (fInputB.ShowDialog(this) == DialogResult.OK)
            {
                B = fInputB.MN;
                B = MnOperations.MnToUniverse(B, U, out int x);
                if (x > 0)
                    MessageBox.Show("Некоторые элементы не содержит универсум. Элементов удалено - " + x, "Элементы не схожи с универсумом!");
                if (B.Length == 0)
                {
                    B = null;
                    return;
                }
                btnsB = CreateButtonsMN(B, button4);
            }
        }
        private void Form1_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            FHelp fHelp = new FHelp();
            fHelp.ShowDialog(this);
        }
        private void buttonColor_Click(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;
            if (button.BackColor == Color.Red)
                button.BackColor = button2.BackColor;
            else
                button.BackColor = Color.Red;
        }

        private void ResetButtonsMN(Button[] buttons)
        {
            if (buttons != null)
                foreach (var item in buttons)
                    item.Dispose();
            Refresh();
        }
        private Button[] CreateButtonsMN(string[] mn, Button example)
        {
            Button[] buttons = new Button[mn.Length];
            for (int i = 0; i < mn.Length; i++)
            {
                Button btn = new Button();
                btn.Top = example.Top;
                btn.Left = example.Left + (example.Width * i + 10);
                btn.Height = example.Height;
                btn.Width = example.Width;
                btn.Parent = example.Parent;
                btn.BackColor = example.BackColor;
                btn.Text = mn[i];
                buttons[i] = btn;
            }
            return buttons;
        }
        private string[] StrsOutButtonsText(Button[] buttons)
        {
            string[] str = new string[buttons.Length];
            for (int i = 0; i < str.Length; i++)
                str[i] = buttons[i].Text;
            return str;
        }
        private void ButtonsTextOutStrs(string[] strs, Button[] buttons)
        {
            for (int i = 0; i < strs.Length; i++)
                buttons[i].Text = strs[i];
        }
    }
}
