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

namespace Mnojestva
{
    public partial class FCharacter : Form
    {
        private string[] mnA, mnB, mnU;

        public FCharacter(string[] a, string[] b, string[] u)
        {
            InitializeComponent();
            mnA = a;
            mnB = b;
            mnU = u;
        }
        private void FCharacter_Load(object sender, EventArgs e)
        {
            label14.Text = MnOperations.Print("A", mnA);
            label15.Text = MnOperations.Print("B", mnB);
            label16.Text = MnOperations.Print("U", mnU);

            //Характеристика
            label1.Text = "|A| = " + mnA.Length;
            label2.Text = "|B| = " + mnB.Length;

            //Отличия
            if (MnOperations.IsEqual(mnA, mnB))
                label3.Text = "A = B : Да";
            else
                label3.Text = "A = B : Нет";
            if (mnA.Length == mnB.Length)
                label4.Text = "A ~ B : Да";
            else
                label4.Text = "A ~ B : Нет";
            if (MnOperations.IsSubset(mnA, mnB))
                label7.Text = "A подмножество B : Да";
            else
                label7.Text = "A подмножество B : Нет";
            if (MnOperations.IsSubset(mnB, mnA))
                label8.Text = "B подмножество A : Да";
            else
                label8.Text = "B подмножество A : Нет";

            //Операции
            label5.Text = "C = A or B : " + MnOperations.Print("C", MnOperations.Or(mnA, mnB), true);
            label6.Text = "C = A and B : " + MnOperations.Print("C", MnOperations.And(mnA, mnB), true);
            label9.Text = "C = -A : " + MnOperations.Print("C", MnOperations.Addition(mnA, mnU), true);
            label10.Text = "C = -B : " + MnOperations.Print("C", MnOperations.Addition(mnB, mnU), true);
            label11.Text = "C = A \\ B : " + MnOperations.Print("C", MnOperations.Subtraction(mnA, mnB), true);
            label12.Text = "C = B \\ A : " + MnOperations.Print("C", MnOperations.Subtraction(mnB, mnA), true);
            label13.Text = "C = B ÷ A : " + MnOperations.Print("C", MnOperations.NotAnd(mnB, mnA), true);
        }

        
    }
}
