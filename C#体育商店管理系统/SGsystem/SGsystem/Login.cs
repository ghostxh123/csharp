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
    public partial class Login : Form
    {
        //变量************************************************************************************
        private DBCL.DBEntities db = new DBCL.DBEntities();



        //函数************************************************************************************
        public Login()
        {
            InitializeComponent();
        }

        private void FontPage_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string username = tbUsername.Text.Trim();
            string pwd = tbPWD.Text.Trim();
            string userlevel = "";

            if(username == "")
            {
                MessageBox.Show("你他妈没输账号！");
                return;
            }

            if(pwd == "")
            {
                MessageBox.Show("你他妈没输密码！");
                return;
            }

            if(db.users.Any(m=>m.users_workid == username) == false)
            {
                MessageBox.Show("该账号不存在!");
                return;
            }

            var model = db.users.Select(m => m);
            model = model.Where(m=>m.users_workid.IndexOf(username) >= 0);

            foreach(var item in model)
            {
                if(item.users_password != pwd)
                {
                    MessageBox.Show("密码错误!");
                    return;
                }
                else
                {
                    userlevel = item.users_level;
                }
            }

           // MessageBox.Show("登录成功！","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            Main form2 = new Main(username, userlevel);
            form2.Show();
            this.Hide();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
