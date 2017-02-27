using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class MainForm : Form
    {

        Variables variables = Program.Variables;

        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WebForm webForm = new WebForm();
            webForm.Show();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
           MessageBox.Show(variables.access_token);
        }
    }
}
