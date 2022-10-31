using System.Diagnostics;
using Game;

namespace Rabochiy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            this.Visible = false;
            f3.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            this.Visible = false;
            f2.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 f2 = new Form4();
            this.Visible = false;
            f2.ShowDialog();
        }
    }
}