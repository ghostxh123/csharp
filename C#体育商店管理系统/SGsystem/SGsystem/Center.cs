using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGsystem
{
    public partial class Center : Form
    {
        string username = "";

        public Center()
        {
            InitializeComponent();
        }
        public Center(string username)
        {
            this.username = username;
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChangePassword cp = new ChangePassword(username);
            cp.Show();
        }
    }
}
