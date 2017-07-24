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
    public partial class ChangePassword : Form
    {
        private DBCL.DBEntities db = new DBCL.DBEntities();
        string username = "";


        public ChangePassword()
        {
            InitializeComponent();
        }

        public ChangePassword(string username)
        {
            this.username = username;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "")
            {
                MessageBox.Show("输入框不能为空");
                return;
            }

            var model = db.users.FirstOrDefault(m=>m.users_workid == username);

            if(model.users_password != textBox1.Text.Trim())
            {
                MessageBox.Show("原始密码错误！");
                return;
            }

            if (textBox2.Text != textBox3.Text.Trim())
            {
                MessageBox.Show("确认密码不正确!");
                return;
            }

            model.users_password = textBox2.Text.Trim();

            db.SaveChanges();

            MessageBox.Show("修改密码成功！");

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }
    }
}
