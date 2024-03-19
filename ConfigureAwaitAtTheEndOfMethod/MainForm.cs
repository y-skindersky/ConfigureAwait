using System;
using System.Windows.Forms;

namespace ConfigureAwaitAtTheEndOfMethod
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.RunBothCases();
        }
    }
}
