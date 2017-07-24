using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aptech.UI;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using SGsystem.share;

namespace SGsystem
{
    public partial class Main : Form
    {
        //变量************************************************************************************
        private DBCL.DBEntities db = new DBCL.DBEntities();
        public string username;
        public string users_level;
        private List<int> y_array = new List<int>();

        public bool power1 = false;
        public bool power4 = false;
        public bool power6 = false;
        public bool power9 = false;
        public bool power12 = false;

        //
        public Main()
        {
            InitializeComponent();
        }

        public Main(string username, string userlevel)
        {
            InitializeComponent();
            this.username = username;
            this.users_level = userlevel;
        }


        //函数************************************************************************************

        //辅助函数*********************************************************************

        //将权限编码转化为权限名称*********************************************
        private string getpname(string code)
        {
            string pname = "";
            DBCL.DBEntities dbc = new DBCL.DBEntities();
            var p2_model = dbc.permissions.Select(m => m);
            p2_model = p2_model.Where(m => m.permission_code.IndexOf(code) >= 0);
            foreach (var p_item in p2_model)
            {
                pname = p_item.permission_na;
            }
            return pname;
        }

        //将权限名称转化为权限编码*********************************************
        private string getpcode(string pname)
        {
            string code = "";
            DBCL.DBEntities dbc = new DBCL.DBEntities();
            var p2_model = dbc.permissions.Select(m => m);
            p2_model = p2_model.Where(m => m.permission_na.IndexOf(pname) >= 0);
            foreach (var p_item in p2_model)
            {
                code = p_item.permission_code;
            }
            return code;
        }

        //后三位转换
        private string set_housan(int  number1 )
        {
            string s="";
            int number;
            number = number1 + 1;
            if (number < 10)
            {
                s = "0" + "0" + Convert.ToString(number);
            }
            else if (number < 100)
            {
                s = "0" + Convert.ToString(number);
            }
            else
            {
                s = Convert.ToString(number);
            }
            return s;

        }
        //获取进货单的id*******************************************************
        private string j_getid()
        {
            string xuhao = "", id = "",san="",housan="";
            string data = DateTime.Today.ToString("yyyyMMdd");
            int hs=0,h=0;
            string xh3;
            try
            {
                var model = db.purchases.Select(m => m);
                foreach (var item in model)
                {
                    xuhao = item.purchase_id;
                    string da=xuhao.Substring(2, 8);
                    if (da.Equals(data) || da == data)
                    {
                        housan = xuhao.Substring(xuhao.Length - 3, 3);
                        hs = Convert.ToInt32(housan);
                        if (hs > h)
                        {
                            h = hs;
                        }
                    }
                    
                }
            }
            catch { }
           

            if (xuhao == "")
            {
                xh3 = "001";
            }
            else
            {
              
                xh3 = set_housan(h);
            }
           
            id = "JH" + data+san + xh3;
            return id;
        }

        //获取付款单的id*******************************************************
        private string pay_getid()
        {
            string data = DateTime.Today.ToString("yyyyMMdd");
            string xuhao = "", id = "", san = "", housan = "";
            int hs = 0, h = 0;
            string xh3="";
            var model = db.pays.Select(m => m);
            try
            {
                foreach (var item in model)
                {
                    xuhao = item.pay_id;
                    string da = xuhao.Substring(2, 8);
                    if (da.Equals(data) || da == data)
                    {
                        housan = xuhao.Substring(xuhao.Length - 3, 3);
                        hs = Convert.ToInt32(housan);
                        if (hs > h)
                        {
                            h = hs;
                        }
                    }
                    
                }
            }
            catch { }
            

            if (xuhao == "")
            {
                xh3 = "001";
            }
            else
            {
                xh3 = set_housan(h);
            }
           
            
            id = "FK" + data+san + xh3;
            return id;
        }
        
       //获取收款单的id 
        private string shou_getid()
        {
            string data = DateTime.Today.ToString("yyyyMMdd");
            string xuhao = "", id = "", san = "", housan = "";
            int hs = 0, h = 0;
            string xh3 = "";
            var model = db.recipts.Select(m => m);
            try
            {
                foreach (var item in model)
                {
                    xuhao = item.receipt_id;
                     string da = xuhao.Substring(2, 8);
                     if (da.Equals(data) || da == data)
                     {
                         housan = xuhao.Substring(xuhao.Length - 3, 3);
                         hs = Convert.ToInt32(housan);
                         if (hs > h)
                         {
                             h = hs;
                         }
                     }
                }
            }
            catch { }


            if (xuhao == "")
            {
                xh3 = "001";
            }
            else
            {
                xh3 = set_housan(h);
            }

            
            id = "SK" + data + san + xh3;
            return id;
        }
        //获取仓库退货单的id***************************************************
        private string ct_getid()
        {
            string xuhao = "", id = "", san = "", housan = "";
            int hs = 0, h = 0;
            string xh3;
            DBCL.DBEntities dbc = new DBCL.DBEntities();
            try
            {
                var model = dbc.TGReturns.Select(m => m);
                foreach (var item in model)
                {
                    xuhao = item.TGReturns_id;
                    housan = xuhao.Substring(xuhao.Length - 3, 3);
                    hs = Convert.ToInt32(housan);
                    if (hs > h)
                    {
                        h = hs;
                    }
                }
            }
            catch { }
            

            if (xuhao == "")
            {
                xh3 = "001";
            }
            else
            {
                xh3 = set_housan(h);
            }
            string data = DateTime.Today.ToString("yyyyMMdd");
            id = "TG" + data + san + xh3;
            return id;
        }

        //仓库对应名称的物品增减数量*******************************************
        private bool c_setnumber(string fuhao, string name, decimal number)
        {
          
            var model = db.warehouses.FirstOrDefault(m => m.warehouse_name == name);
            try
            {
                if (model == null)
                {
                    MessageBox.Show("该物品单在数据库中不存在!");
                    return false;
                }
                if (fuhao == "-")
                {
                    model.warehouse_number =model.warehouse_number- number;
                    return true;
                }
                else if (fuhao == "+")
                {
                    model.warehouse_number =model.warehouse_number+ number;
                    return true;
                }
                else
                {
                    MessageBox.Show("仓库修改失败！");
                    return false;
                }
                //try { db.SaveChanges(); }
                //catch { }
            }
            catch
            { return false; }
           
        }

        //仓库增加新物品*******************************************************
        private bool w_setnew(string name, decimal dnumber, string unit)
        {
            int xh = -1;
            var wi_model = db.warehouses.Select(m => m);
            foreach (var item in wi_model)
            {
                xh = item.warehouse_id;
            }
            //如果仓库为空自动设为1；
            if (xh == -1)
            {
                xh = 0;
            }
            DBCL.warehouse w_model = new DBCL.warehouse();
            w_model.warehouse_id = xh + 1;
            w_model.warehouse_name = name;
            w_model.warehouse_number = dnumber;
            w_model.warehouse_unit = unit;
            try
            {
                db.warehouses.Add(w_model);
            }
            catch
            {
                MessageBox.Show("仓库货物添加失败!");
                return false;
            }
            return true;
        }

        //新增付款单***********************************************************
        private bool pay_setnew(string purchase_id, string work_id, string data, decimal number)
        {
            string xh = pay_getid();
            DBCL.pay model = new DBCL.pay();
            model.pay_id = xh;
            model.pay_purchase_id = purchase_id;
            model.pay_worker_id = work_id;
            model.pay_data = data;
            model.pay_number = number;
            try
            {
                db.pays.Add(model);
            }
            catch
            {
                MessageBox.Show("添加付款单失败!");
                return false;
            }
            try
            {
                db.SaveChanges();
            }
            catch
            {
                MessageBox.Show("添加付款单失败!");
                return false;
            }
            return true;
        }
        //新增收款单
        private bool shou_setnew(string purchase_id, string work_id, string data, decimal number)
        {
            string xh = pay_getid();
            DBCL.pay model = new DBCL.pay();
            model.pay_id = xh;
            model.pay_purchase_id = purchase_id;
            model.pay_worker_id = work_id;
            model.pay_data = data;
            model.pay_number = number;
            try
            {
                db.pays.Add(model);
            }
            catch
            {
                MessageBox.Show("添加付款单失败!");
                return false;
            }
            try
            {
                db.SaveChanges();
            }
            catch
            {
                MessageBox.Show("添加付款单失败!");
                return false;
            }
            return true;
        }
        //删除付款单***********************************************************
        private bool pay_delete(string pay_id)
        {

            var model = db.pays.FirstOrDefault(m => m.pay_id == pay_id);
            try
            {

                db.pays.Remove(model);
            }
            catch
            {
                MessageBox.Show("删除付款单失败");
                return false;

            }
            return true;
        }

        //总资金的加减***************************************************************************************************
        private bool z_setmoney(string fuhao, decimal number)
        {
            var model = db.funds.First(m => m.funds_id == 1);
            try
            {
                if (fuhao == "-")
                {
                    model.funds_number -= number;
                }
                else if (fuhao == "+")
                {
                    model.funds_number += number;
                }
                else
                {
                    MessageBox.Show("资金修改失败！");
                    return false;
                }
                return true;
                try { db.SaveChanges(); }
                catch { }
            }
            catch { return false; }
           
            
        }


        //父窗体控件挡住子窗体
        [DllImport("user32")]
        public static extern int SetParent(int hWndChild, int hWndNewParent);

        //权限设置的辅助函数*******************************************************************************************

        //设置界面的权限显示
        private void role_set(char[] codea)
        {
            if (codea[0] == '1') { p1.Checked = true; } else { p1.Checked = false; };//复选框设为被选中
            if (codea[1] == '1') { p2.Checked = true; } else { p2.Checked = false; };
            if (codea[2] == '1') { p3.Checked = true; } else { p3.Checked = false; };
            if (codea[3] == '1') { p4.Checked = true; } else { p4.Checked = false; };
            if (codea[4] == '1') { p5.Checked = true; } else { p5.Checked = false; };
            if (codea[5] == '1') { p6.Checked = true; } else { p6.Checked = false; };
            if (codea[6] == '1') { p7.Checked = true; } else { p7.Checked = false; };
            if (codea[7] == '1') { p8.Checked = true; } else { p8.Checked = false; };
            if (codea[8] == '1') { p9.Checked = true; } else { p9.Checked = false; };
            if (codea[9] == '1') { p10.Checked = true; } else { p10.Checked = false; };
            if (codea[10] == '1') { p11.Checked = true; } else { p11.Checked = false; };
            if (codea[11] == '1') { p12.Checked = true; } else { p12.Checked = false; };
            if (codea[12] == '1') { p13.Checked = true; } else { p13.Checked = false; };
            if (codea[13] == '1') { p14.Checked = true; } else { p14.Checked = false; };
            if (codea[14] == '1') { p15.Checked = true; } else { p15.Checked = false; };
            if (codea[15] == '1') { p16.Checked = true; } else { p16.Checked = false; };
            if (codea[16] == '1') { p17.Checked = true; } else { p17.Checked = false; };
            if (codea[17] == '1') { p18.Checked = true; } else { p18.Checked = false; };
            if (codea[18] == '1') { p19.Checked = true; } else { p19.Checked = false; };
            if (codea[19] == '1') { p20.Checked = true; } else { p20.Checked = false; };
            if (codea[20] == '1') { p21.Checked = true; } else { p21.Checked = false; };
            if (codea[21] == '1') { p22.Checked = true; } else { p22.Checked = false; };
            if (codea[22] == '1') { p23.Checked = true; } else { p23.Checked = false; };
            if (codea[23] == '1') { p24.Checked = true; } else { p24.Checked = false; };
        }

        //清空权限
        private void role_setnull ()
        {
            p1.Checked = false; //复选框设为被选中
            p2.Checked = false; 
            p3.Checked = false; 
            p4.Checked = false; 
            p5.Checked = false; 
            p6.Checked = false; 
            p7.Checked = false; 
            p8.Checked = false; 
            p9.Checked = false; 
            p10.Checked = false; 
            p11.Checked = false; 
            p12.Checked = false; 
            p13.Checked = false; 
            p14.Checked = false; 
            p15.Checked = false; 
            p16.Checked = false; 
            p17.Checked = false; 
            p18.Checked = false; 
            p19.Checked = false; 
            p20.Checked = false; 
            p21.Checked = false; 
            p22.Checked = false; 
            p23.Checked = false; 
            p24.Checked = false; 
        }
        private void nrole_setnull()
        {
            n1.Checked = false; //复选框设为不被选中
            n2.Checked = false;
            n3.Checked = false;
            n4.Checked = false;
            n5.Checked = false;
            n6.Checked = false;
            n7.Checked = false;
            n8.Checked = false;
            n9.Checked = false;
            n10.Checked = false;
            n11.Checked = false;
            n12.Checked = false;
            n13.Checked = false;
            n14.Checked = false;
            n15.Checked = false;
            n16.Checked = false;
            n17.Checked = false;
            n18.Checked = false;
            n19.Checked = false;
            n20.Checked = false;
            n21.Checked = false;
            n22.Checked = false;
            n23.Checked = false;
            n24.Checked = false;
        }
        //获取员工的权限
        private char[] role_get()
        {
            string code = "000000000000000000000000";
            char[] codea = code.ToCharArray();
            if (p1.Checked == true) { codea[0] = '1'; };//设置编码
            if (p2.Checked == true) { codea[1] = '1'; };
            if (p3.Checked == true) { codea[2] = '1'; };
            if (p4.Checked == true) { codea[3] = '1'; };
            if (p5.Checked == true) { codea[4] = '1'; };
            if (p6.Checked == true) { codea[5] = '1'; };
            if (p7.Checked == true) { codea[6] = '1'; };
            if (p8.Checked == true) { codea[7] = '1'; };
            if (p9.Checked == true) { codea[8] = '1'; };
            if (p10.Checked == true) { codea[9] = '1'; };
            if (p11.Checked == true) { codea[10] = '1'; };
            if (p12.Checked == true) { codea[11] = '1'; };
            if (p13.Checked == true) { codea[12] = '1'; };
            if (p14.Checked == true) { codea[13] = '1'; };
            if (p15.Checked == true) { codea[14] = '1'; };
            if (p16.Checked == true) { codea[15] = '1'; };
            if (p17.Checked == true) { codea[16] = '1'; };
            if (p18.Checked == true) { codea[17] = '1'; };
            if (p19.Checked == true) { codea[18] = '1'; };
            if (p20.Checked == true) { codea[19] = '1'; };
            if (p21.Checked == true) { codea[20] = '1'; };
            if (p22.Checked == true) { codea[21] = '1'; };
            if (p23.Checked == true) { codea[22] = '1'; };
            if (p24.Checked == true) { codea[23] = '1'; };
            return codea;
        }

        //获取新员工的权限
        private char[] nrole_get()
        {
            string code = "000000000000000000000000";
            char[] codea = code.ToCharArray();
            if (n1.Checked == true) { codea[0] = '1'; };//设置编码
            if (n2.Checked == true) { codea[1] = '1'; };
            if (n3.Checked == true) { codea[2] = '1'; };
            if (n4.Checked == true) { codea[3] = '1'; };
            if (n5.Checked == true) { codea[4] = '1'; };
            if (n6.Checked == true) { codea[5] = '1'; };
            if (n7.Checked == true) { codea[6] = '1'; };
            if (n8.Checked == true) { codea[7] = '1'; };
            if (n9.Checked == true) { codea[8] = '1'; };
            if (n10.Checked == true) { codea[9] = '1'; };
            if (n11.Checked == true) { codea[10] = '1'; };
            if (n12.Checked == true) { codea[11] = '1'; };
            if (n13.Checked == true) { codea[12] = '1'; };
            if (n14.Checked == true) { codea[13] = '1'; };
            if (n15.Checked == true) { codea[14] = '1'; };
            if (n16.Checked == true) { codea[15] = '1'; };
            if (n17.Checked == true) { codea[16] = '1'; };
            if (n18.Checked == true) { codea[17] = '1'; };
            if (n19.Checked == true) { codea[18] = '1'; };
            if (n20.Checked == true) { codea[19] = '1'; };
            if (n21.Checked == true) { codea[20] = '1'; };
            if (n22.Checked == true) { codea[21] = '1'; };
            if (n23.Checked == true) { codea[22] = '1'; };
            if (n24.Checked == true) { codea[23] = '1'; };
            return codea;
        }




        //SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS
        //MainForm加载时，隐藏所有标签页
        private void Form2_Load(object sender, EventArgs e)
        {
            //第一队列
            page_j.Parent = null;
            page_jh.Parent = null;
            page_ct.Parent = null;
            page_cth.Parent = null;
            page_c.Parent = null;
            新建销售.Parent = null;
            销售记录.Parent = null;
            新建客户退货.Parent = null;
            客户退货记录.Parent = null;

            //第二队列
            新建收入.Parent = null;
            收入记录.Parent = null;
            新建支出.Parent = null;
            支出记录.Parent = null;

            //第三队列
            page_ny.Parent = null;
            y.Parent = null;
            page_level.Parent = null;

            //间接队列
            page_s.Parent = null;
            page_newlevel.Parent = null;
        }

        //标题栏功能
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 关于我们ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show();
        }

        private void 使用手册ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UseNote usenote = new UseNote();
            usenote.Show();
        }

        private void 个人中心ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Center center = new Center(username);
            center.Show();
        }

        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePassword cp = new ChangePassword(username);
            cp.ShowDialog();
        }

        //sideBar左侧菜单栏，判断对应的权限，显示对应的功能栏
        private void sideBar1_Load(object sender, EventArgs e)
        {
            int i0 = 0;
            int i1 = 0;
            int i2 = 0;
            int i3 = 0;

            for (int i = 0; i < 14; i++)
            {
                if (users_level[i] == '1')
                {
                    sideBar1.AddGroup("商品详情");
                    break;
                }
            }
            for (int i = 14; i < 20; i++)
            {
                if (users_level[i] == '1')
                {
                    sideBar1.AddGroup("资金详情");
                    break;
                }
            }
            for (int i = 20; i < 24; i++)
            {
                if (users_level[i] == '1')
                {
                    sideBar1.AddGroup("员工详情");
                    break;
                }
            }

            if (users_level[0] == '0')
            {

            }
            else
            {
                sideBar1.Groups[i0].Items.Add("新建进货", i1);
                i1++;
            }

            if (users_level[1] == '0')
            {
                power1 = false;
            }
            else
            {
                power1 = true;
            }

            if (users_level[2] == '0')
            {

            }
            else
            {
                sideBar1.Groups[i0].Items.Add("进货记录", i1);
                i1++;
            }

            if (users_level[3] == '0')
            {

            }
            else
            {
                sideBar1.Groups[i0].Items.Add("新建仓库退货", i1);
                i1++;
            }

            if (users_level[4] == '0')
            {
                power4 = false;
            }
            else
            {
                power4 = true;
            }

            if (users_level[5] == '0')
            {

            }
            else
            {
                sideBar1.Groups[i0].Items.Add("仓库退货记录", i1);
                i1++;
            }

            if (users_level[6] == '0')
            {

            }
            else
            {
                power6 = true;
            }

            if (users_level[7] == '0')
            {

            }
            else
            {
                sideBar1.Groups[i0].Items.Add("查询库存情况", i1);
                i1++;
            }

            if (users_level[8] == '0')
            {

            }
            else
            {
                sideBar1.Groups[i0].Items.Add("新建销售", i1);
                i1++;
            }

            if (users_level[9] == '0')
            {

            }
            else
            {
                power9 = true;
            }

            if (users_level[10] == '0')
            {

            }
            else
            {
                sideBar1.Groups[i0].Items.Add("销售记录", i1);
                i1++;
            }

            if (users_level[11] == '0')
            {

            }
            else
            {
                sideBar1.Groups[i0].Items.Add("新建客户退货", i1);
                i1++;
            }

            if (users_level[12] == '0')
            {

            }
            else
            {
                power12 = true;
            }

            if (users_level[13] == '0')
            {

            }
            else
            {
                sideBar1.Groups[i0].Items.Add("客户退货记录", i1);
                i1++;
            }

            if (sideBar1.Groups[i0].Items.Count != 0)
            {
                i0++;
            }

            i2 = i1;

            if (users_level[14] == '0')
            {

            }
            else
            {
                sideBar1.Groups[i0].Items.Add("新建收入", i2);
                i2++;
            }

            if (users_level[15] == '0')
            {

            }
            else
            {

            }

            if (users_level[16] == '0')
            {

            }
            else
            {
                sideBar1.Groups[i0].Items.Add("收入记录", i2);
                i2++;
            }

            if (users_level[17] == '0')
            {

            }
            else
            {
                sideBar1.Groups[i0].Items.Add("新建支出", i2);
                i2++;
            }

            if (users_level[18] == '0')
            {

            }
            else
            {

            }

            if (users_level[19] == '0')
            {

            }
            else
            {
                sideBar1.Groups[i0].Items.Add("支出记录", i2);
                i2++;
            }

            if (sideBar1.Groups[i0].Items.Count != 0)
            {
                i0++;
            }

            i3 = i2;

            if (users_level[20] == '0')
            {

            }
            else
            {
                sideBar1.Groups[i0].Items.Add("新增员工", i3);
                i3++;
            }

            if (users_level[21] == '0')
            {
                role_save.Enabled = false;
            }
            else
            {
                role_save.Enabled = true;
            }

            if (users_level[22] == '0')
            {

            }
            else
            {
                sideBar1.Groups[i0].Items.Add("员工信息", i3);
                i3++;
            }

            if (users_level[23] == '0')
            {

            }
            else
            {
                sideBar1.Groups[i0].Items.Add("管理角色", i3);
                i3++;
            }
        }

        //判断sideBar点击按钮，打开对应的标签页******************************************起始状态********************************************
        private void sideBar1_ItemClick(SbItemEventArgs e)
        {
           
            if (e.Item.Text == "新建进货")
            {
                page_j.Parent = tabControl1;
                tabControl1.SelectTab(page_j);
                j_id.Text = j_getid();
                j_workid.Text = username;
            }
            else if (e.Item.Text == "新增员工")
            {
                ny_d_level.Items.Clear();
                DBCL.DBEntities dbc = new DBCL.DBEntities();
                var model = dbc.permissions.Select(m => m);
                foreach (var item in model)
                {
                    ny_d_level.Items.Add(item.permission_na);
                }
                page_ny.Parent = tabControl1;
                tabControl1.SelectTab(page_ny);
            }
            else if (e.Item.Text == "管理角色")
            {
                role_select.Items.Clear();
                DBCL.DBEntities dbc = new DBCL.DBEntities();
                var model = dbc.permissions.Select(m => m);
                foreach (var item in model)
                {
                    role_select.Items.Add(item.permission_na);

                }
                page_level.Parent = tabControl1;
                tabControl1.SelectTab(page_level);
            }
            else if (e.Item.Text == "新增权限")
            {
                page_newlevel.Parent = tabControl1;
                tabControl1.SelectTab(page_newlevel);
            }
            else if(e.Item.Text == "员工信息")
            {
                //初始化表格***************************************************

                y.Parent = tabControl1;
                tabControl1.SelectTab(y);
                y_d.Rows.Clear();
                y_d_sex.Items.Clear();
                y_co_sex.Items.Clear();
                y_d_level.Items.Clear();
                y_co_level.Items.Clear();

                y_d_workid.ReadOnly = true;
                DBCL.DBEntities dbc = new DBCL.DBEntities();
                var p_model = dbc.permissions.Select(m => m);
                foreach (var p_item in p_model)
                {
                    y_d_level.Items.Add((string)p_item.permission_na);
                };
                y_d_sex.Items.Add("男");
                y_d_sex.Items.Add("女");
                //上方下拉框初始化***********************************************
                foreach (var p_item in p_model)
                {
                    y_co_level.Items.Add((string)p_item.permission_na);
                };
                y_co_sex.Items.Add("男");
                y_co_sex.Items.Add("女");
                //表格赋值*****************************************************
                y_d.Rows.Clear();
                var model = dbc.users.Select(m => m);
                foreach (var item in model)
                {
                    string code = "", pname = "";
                    code = (string)item.users_level;
                    pname = getpname(code);
                    y_d.Rows.Add((string)item.users_workid, (string)item.users_name, (string)item.users_sex, (string)item.users_id, pname);
                }
            }
            else if (e.Item.Text == "进货记录")
            {
                page_jh.Parent = tabControl1;
                tabControl1.SelectTab(page_jh);
                jh_sd.Rows.Clear();
                DBCL.DBEntities db_j = new DBCL.DBEntities();
                var jh_model = db_j.purchases.Select(m => m);
                if (jh_model == null)
                {
                    MessageBox.Show("暂无数据");
                }
                if (username != "2017000")
                {

                    jh_model = jh_model.Where(m => m.purchase_worker_id == username);
                }

                jh_sd.Rows.Clear();
                List<string> tg = new List<string>();
                bool pantg = false;
                foreach (var item in jh_model)
                {
                    for (int i = 0; i < tg.Count; i++)
                    {
                        if (tg[i] == item.purchase_id)
                        {
                            pantg = true;
                        }
                    }
                    if (pantg == false)
                    {
                        jh_sd.Rows.Add(item.purchase_id, item.purchase_data);
                        tg.Add(item.purchase_id);
                    }
                    pantg = false;
                }

                tg.Clear();
            }
            else if (e.Item.Text == "查询库存情况")
            {
                page_c.Parent = tabControl1;
                tabControl1.SelectTab(page_c);
                DBCL.DBEntities db_k = new DBCL.DBEntities();
                var c_model = db_k.warehouses.Select(m => m);
                c_d.Rows.Clear();
                foreach (var item in c_model)
                {
                    if (item.warehouse_name == "")
                    { continue; }
                    c_d.Rows.Add(item.warehouse_id, item.warehouse_name, item.warehouse_price, Convert.ToInt32(item.warehouse_number), item.warehouse_unit,item.warehouse_others);
                }
            }
            else if (e.Item.Text == "新建仓库退货")
            {
                page_ct.Parent = tabControl1;
                tabControl1.SelectTab(page_ct);
                ct_id.Text = ct_getid();
                ct_workid.Text = username;

            }
            else if (e.Item.Text == "仓库退货记录")
            {
                page_cth.Parent = tabControl1;
                tabControl1.SelectTab(page_cth);
                cth_d.Rows.Clear();
                DBCL.DBEntities db_cth = new DBCL.DBEntities();
                var cth_model = db_cth.TGReturns.Select(m => m);
                if (cth_model == null)
                {
                    MessageBox.Show("暂无数据");
                }
                if (username != "2017000")
                {

                    cth_model = cth_model.Where(m => m.TGReturns_worker_id == username);
                }
                cth_d.Rows.Clear();
                List<string> tg = new List<string>();
                bool pantg = false;
                foreach (var item in cth_model)
                {
                    for (int i = 0; i < tg.Count; i++)
                    {
                        if (tg[i] == item.TGReturns_id)
                        {
                            pantg = true;
                        }
                    }
                    if (pantg == false)
                    {
                        cth_d.Rows.Add(item.TGReturns_id, item.TGReturns_data);
                        tg.Add(item.TGReturns_id);
                    }
                    pantg = false;
                }

                tg.Clear();
            }
            else if (e.Item.Text == "新建销售")
            {
                //显示“新建销售”标签页
                新建销售.Parent = tabControl1;
                tabControl1.SelectTab("新建销售");

                //自动生成表单号，日期

                var model = db.sales.Select(m => m);
                string tb1 = "";

                foreach (var item in model)
                {
                    tb1 = item.sales_id.ToString();
                }

                string tb11 = tb1.Substring(2, 8);
                string tb12 = tb1.Substring(10, 3);
                int num = Convert.ToInt32(tb12);

                if (tb11 == DateTime.Now.ToString("yyyyMMdd"))
                {
                    num++;
                }
                else
                {
                    tb11 = DateTime.Now.ToString("yyyyMMdd");
                    num = 1;
                }

                if (num.ToString().Length == 1)
                {
                    textBox1.Text = "XS" + tb11 + "00" + num;
                }
                else if (num.ToString().Length == 2)
                {
                    textBox1.Text = "XS" + tb11 + "0" + num;
                }
                else
                {
                    textBox1.Text = "XS" + tb11 + num;
                }

                textBox2.Text = "";
                textBox3.Text = DateTime.Now.ToString("yyyy-MM-dd");
                textBox4.Text = "";
                dataGridView2.Rows.Clear();
            }
            else if (e.Item.Text == "销售记录")
            {
                //显示“销售记录”标签页
                销售记录.Parent = tabControl1;
                tabControl1.SelectTab("销售记录");

                if(power9 == false)
                {
                    button2.Enabled = false;
                }

                label3.Text = DateTime.Now.ToString("yyyy-MM-dd");
                label5.Text = username;
                dataGridView1.Rows.Clear();

                //查看是否是超级管理员，显示不同的结果
                if (users_level[23] == '1')
                {
                    var model = db.sales.Select(m => m);
                    int line = 0;
                    string lastname = "XS20161111001";

                    foreach (var item in model)
                    {
                        if (lastname != item.sales_id)
                        {
                            lastname = item.sales_id;
                            dataGridView1.Rows.Add();
                            dataGridView1.Rows[line].Cells[0].Value = item.sales_id;
                            dataGridView1.Rows[line].Cells[1].Value = item.sales_data;
                            line++;
                        }
                    }
                }
                else
                {
                    var model = db.sales.Select(m => m);
                    model = model.Where(m => m.sales_worker_id == username);
                    int line = 0;
                    string lastname = "XS20161111001";

                    foreach (var item in model)
                    {
                        if (lastname != item.sales_id)
                        {
                            lastname = item.sales_id;
                            dataGridView1.Rows.Add();
                            dataGridView1.Rows[line].Cells[0].Value = item.sales_id;
                            dataGridView1.Rows[line].Cells[1].Value = item.sales_data;
                            line++;
                        }
                    }
                }
            }
            else if (e.Item.Text == "新建客户退货")
            {
                //显示“新建销售”标签页
                新建客户退货.Parent = tabControl1;
                tabControl1.SelectTab("新建客户退货");

                //自动生成表单号，日期

                var model = db.TKReturns.Select(m => m);
                string tb1 = "";

                foreach (var item in model)
                {
                    tb1 = item.TKReturns_id.ToString();
                }

                string tb11 = tb1.Substring(2, 8);
                string tb12 = tb1.Substring(10, 3);
                int num = Convert.ToInt32(tb12);

                if (tb11 == DateTime.Now.ToString("yyyyMMdd"))
                {
                    num++;
                }
                else
                {
                    tb11 = DateTime.Now.ToString("yyyyMMdd");
                    num = 1;
                }

                if (num.ToString().Length == 1)
                {
                    textBox5.Text = "TK" + tb11 + "00" + num;
                }
                else if (num.ToString().Length == 2)
                {
                    textBox5.Text = "TK" + tb11 + "0" + num;
                }
                else
                {
                    textBox5.Text = "TK" + tb11 + num;
                }

                textBox6.Text = "";
                textBox7.Text = DateTime.Now.ToString("yyyy-MM-dd");
                textBox8.Text = username;
                dataGridView3.Rows.Clear();
            }
            else if(e.Item.Text == "客户退货记录")
            {
                //显示“客户退货记录”标签页
                客户退货记录.Parent = tabControl1;
                tabControl1.SelectTab("客户退货记录");

                if(power12 == false)
                {
                    button7.Enabled = false;
                }

                label8.Text = DateTime.Now.ToString("yyyy-MM-dd");
                label6.Text = username;
                dataGridView4.Rows.Clear();

                //查看是否是超级管理员，显示不同的结果
                if (users_level[23] == '1')
                {
                    var model = db.TKReturns.Select(m => m);
                    int line = 0;
                    string lastname = "TK00000000000";

                    foreach (var item in model)
                    {
                        if (lastname != item.TKReturns_id)
                        {
                            lastname = item.TKReturns_id;
                            dataGridView4.Rows.Add();
                            dataGridView4.Rows[line].Cells[0].Value = item.TKReturns_id;
                            dataGridView4.Rows[line].Cells[1].Value = item.TKReturns_data;
                            line++;
                        }
                    }
                }
                else
                {
                    var model = db.TKReturns.Select(m => m);
                    model = model.Where(m => m.TKReturns_worker_id == username);
                    int line = 0;
                    string lastname = "TK00000000000";

                    foreach (var item in model)
                    {
                        if (lastname != item.TKReturns_id)
                        {
                            lastname = item.TKReturns_id;
                            dataGridView4.Rows.Add();
                            dataGridView4.Rows[line].Cells[0].Value = item.TKReturns_id;
                            dataGridView4.Rows[line].Cells[1].Value = item.TKReturns_data;
                            line++;
                        }
                    }
                }
            }
            else if(e.Item.Text == "新建收入")
            {
                //显示“新建收入”标签页
                新建收入.Parent = tabControl1;
                tabControl1.SelectTab("新建收入");

                //自动生成表单号，日期

                var model = db.recipts.Select(m => m);
                string tb1 = "";

                foreach (var item in model)
                {
                    tb1 = item.receipt_id.ToString();
                }

                string tb11 = tb1.Substring(2, 8);
                string tb12 = tb1.Substring(10, 3);
                int num = Convert.ToInt32(tb12);

                if (tb11 == DateTime.Now.ToString("yyyyMMdd"))
                {
                    num++;
                }
                else
                {
                    tb11 = DateTime.Now.ToString("yyyyMMdd");
                    num = 1;
                }

                if (num.ToString().Length == 1)
                {
                    textBox12.Text = "SK" + tb11 + "00" + num;
                }
                else if (num.ToString().Length == 2)
                {
                    textBox12.Text = "SK" + tb11 + "0" + num;
                }
                else
                {
                    textBox12.Text = "SK" + tb11 + num;
                }
                
                textBox10.Text = DateTime.Now.ToString("yyyy-MM-dd");
                textBox9.Text = username;
                textBox13.Text = "";
                textBox14.Text = "";
                textBox15.Text = "";
            }
            else if(e.Item.Text == "收入记录")
            {
                //显示“销售记录”标签页
                收入记录.Parent = tabControl1;
                tabControl1.SelectTab("收入记录");

                label26.Text = DateTime.Now.ToString("yyyy-MM-dd");
                label19.Text = username;
                dataGridView5.Rows.Clear();

                //查看是否是超级管理员，显示不同的结果
                if (users_level[23] == '1')
                {
                    var model = db.recipts.Select(m => m);
                    int line = 0;

                    foreach (var item in model)
                    {
                        dataGridView5.Rows.Add();
                        dataGridView5.Rows[line].Cells[0].Value = item.receipt_id;
                        dataGridView5.Rows[line].Cells[1].Value = item.receipt_sales_id;
                        dataGridView5.Rows[line].Cells[2].Value = item.receipt_worker_id;
                        dataGridView5.Rows[line].Cells[3].Value = item.receipt_data;
                        dataGridView5.Rows[line].Cells[4].Value = item.receipt_number;
                        dataGridView5.Rows[line].Cells[5].Value = item.receipt_type;
                        dataGridView5.Rows[line].Cells[6].Value = item.receipt_others;
                        line++;
                    }
                }
                else
                {
                    var model = db.recipts.Select(m => m);
                    model = model.Where(m => m.receipt_worker_id == username);
                    int line = 0;

                    foreach (var item in model)
                    {
                        dataGridView5.Rows.Add();
                        dataGridView5.Rows[line].Cells[0].Value = item.receipt_id;
                        dataGridView5.Rows[line].Cells[1].Value = item.receipt_sales_id;
                        dataGridView5.Rows[line].Cells[2].Value = item.receipt_worker_id;
                        dataGridView5.Rows[line].Cells[3].Value = item.receipt_data;
                        dataGridView5.Rows[line].Cells[4].Value = item.receipt_number;
                        dataGridView5.Rows[line].Cells[5].Value = item.receipt_type;
                        dataGridView5.Rows[line].Cells[6].Value = item.receipt_others;
                        line++;
                    }
                }
            }
            else if(e.Item.Text == "新建支出")
            {
                //显示“新建支出”标签页
                新建支出.Parent = tabControl1;
                tabControl1.SelectTab("新建支出");

                //自动生成表单号，日期

                var model = db.pays.Select(m => m);
                string tb1 = "";

                foreach (var item in model)
                {
                    tb1 = item.pay_id.ToString();
                }

                string tb11 = tb1.Substring(2, 8);
                string tb12 = tb1.Substring(10, 3);
                int num = Convert.ToInt32(tb12);

                if (tb11 == DateTime.Now.ToString("yyyyMMdd"))
                {
                    num++;
                }
                else
                {
                    tb11 = DateTime.Now.ToString("yyyyMMdd");
                    num = 1;
                }

                if (num.ToString().Length == 1)
                {
                    textBox20.Text = "FK" + tb11 + "00" + num;
                }
                else if (num.ToString().Length == 2)
                {
                    textBox20.Text = "FK" + tb11 + "0" + num;
                }
                else
                {
                    textBox20.Text = "FK" + tb11 + num;
                }

                textBox19.Text = DateTime.Now.ToString("yyyy-MM-dd");
                textBox18.Text = username;
                textBox17.Text = "";
                textBox16.Text = "";
                textBox11.Text = "";
            }
            else if (e.Item.Text == "支出记录")
            {
                //显示“支出记录”标签页
                支出记录.Parent = tabControl1;
                tabControl1.SelectTab("支出记录");

                label36.Text = DateTime.Now.ToString("yyyy-MM-dd");
                label34.Text = username;
                dataGridView6.Rows.Clear();

                //查看是否是超级管理员，显示不同的结果
                if (users_level[23] == '1')
                {
                    var model = db.pays.Select(m => m);
                    int line = 0;

                    foreach (var item in model)
                    {
                        dataGridView6.Rows.Add();
                        dataGridView6.Rows[line].Cells[0].Value = item.pay_id;
                        dataGridView6.Rows[line].Cells[1].Value = item.pay_purchase_id;
                        dataGridView6.Rows[line].Cells[2].Value = item.pay_worker_id;
                        dataGridView6.Rows[line].Cells[3].Value = item.pay_data;
                        dataGridView6.Rows[line].Cells[4].Value = item.pay_number;
                        dataGridView6.Rows[line].Cells[5].Value = item.pay_type;
                        dataGridView6.Rows[line].Cells[6].Value = item.pay_others;
                        line++;
                    }
                }
                else
                {
                    var model = db.pays.Select(m => m);
                    model = model.Where(m => m.pay_worker_id == username);
                    int line = 0;

                    foreach (var item in model)
                    {
                        dataGridView6.Rows.Add();
                        dataGridView6.Rows[line].Cells[0].Value = item.pay_id;
                        dataGridView6.Rows[line].Cells[1].Value = item.pay_purchase_id;
                        dataGridView6.Rows[line].Cells[2].Value = item.pay_worker_id;
                        dataGridView6.Rows[line].Cells[3].Value = item.pay_data;
                        dataGridView6.Rows[line].Cells[4].Value = item.pay_number;
                        dataGridView6.Rows[line].Cells[5].Value = item.pay_type;
                        dataGridView6.Rows[line].Cells[6].Value = item.pay_others;
                        line++;
                    }
                }
            }
        }

        //右侧标签页，双击关闭对应标签页
        private void tabControl1_DoubleClick(object sender, EventArgs e)
        {
            tabControl1.SelectedTab.Parent = null;
        }


        //各个分标签页的功能╭(′▽`)╭(′▽`)╯╭(′▽`)╭(′▽`)╯╭(′▽`)╭(′▽`)╯╭(′▽`)╭(′▽`)╯╭(′▽`)╭(′▽`)╯╭(′▽`)╭(′▽`)╯╭(′▽`)╭(′▽`)╯
        //╭(′▽`)╭(′▽`)╯╭(′▽`)╭(′▽`)╯╭(′▽`)╭(′▽`)╯╭(′▽`)╭(′▽`)╯╭(′▽`)╭(′▽`)╯╭(′▽`)╭(′▽`)╯╭(′▽`)╭(′▽`)╯╭(′▽`)╭(′▽`)╯
        //╭(′▽`)╭(′▽`)╯╭(′▽`)╭(′▽`)╯╭(′▽`)╭(′▽`)╯╭(′▽`)╭(′▽`)╯╭(′▽`)╭(′▽`)╯╭(′▽`)╭(′▽`)╯╭(′▽`)╭(′▽`)╯╭(′▽`)╭(′▽`)╯

        //新建销售************************************************************************************************************

        //关闭『新建销售』标签页
        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            dataGridView2.Rows.Clear();
            新建销售.Parent = null;
        }

        //计算总额
        private void dataGridView2_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {

            for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
            {
                if (dataGridView2.Rows[i].Cells[1].Value == null || dataGridView2.Rows[i].Cells[2].Value == null)
                {
                    return;
                }
            }

            for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
            {
                if (!Regex.IsMatch(dataGridView2.Rows[i].Cells[1].Value.ToString(), @"^[+-]?(?!0\d)\d+(\.[0-9]+)?$"))
                {
                    return;
                }

                if (!Regex.IsMatch(dataGridView2.Rows[i].Cells[2].Value.ToString(), @"^[+-]?(?!0\d)\d+(\.[0-9]+)?$"))
                {
                    return;
                }

                if (dataGridView2.Rows[i].Cells[4].Value != null)
                {
                    if (!Regex.IsMatch(dataGridView2.Rows[i].Cells[1].Value.ToString(), @"^[+-]?(?!0\d)\d+(\.[0-9]+)?$"))
                    {
                        return;
                    }
                }
            }

            double result = 0;

            for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
            {
                if (dataGridView2.Rows[i].Cells[4].Value == null)
                {
                    result = result + Convert.ToDouble(dataGridView2.Rows[i].Cells[1].Value) * Convert.ToDouble(dataGridView2.Rows[i].Cells[2].Value);
                }
                else
                {
                    result = result + Convert.ToDouble(dataGridView2.Rows[i].Cells[1].Value) * Convert.ToDouble(dataGridView2.Rows[i].Cells[4].Value);
                }
            }

            textBox4.Text = Convert.ToString(result);
        }

        //提交销售单
        private void button4_Click(object sender, EventArgs e)
        {
            //验证销售单格式
            if (textBox2.Text.Trim() == "")
            {
                MessageBox.Show("销售对象不能为空！");
                return;
            }

            for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView2.Columns.Count - 1; j++)
                {
                    if (j == 4)
                    {
                        continue;
                    }

                    if (dataGridView2.Rows[i].Cells[j].Value == null)
                    {
                        MessageBox.Show("第" + (i + 1) + "行" + (j + 1) + "列处不能为空！");
                        return;
                    }
                }
            }

            for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
            {
                if (!Regex.IsMatch(dataGridView2.Rows[i].Cells[1].Value.ToString(), @"^[+-]?(?!0\d)\d+(\.[0-9]+)?$"))
                {
                    MessageBox.Show("数量必须为数字！");
                    return;
                }

                if (!Regex.IsMatch(dataGridView2.Rows[i].Cells[2].Value.ToString(), @"^[+-]?(?!0\d)\d+(\.[0-9]+)?$"))
                {
                    MessageBox.Show("单价必须为数字！");
                    return;
                }

                if (!Regex.IsMatch(dataGridView2.Rows[i].Cells[3].Value.ToString(), "^[\u4e00-\u9fa5]$"))
                {
                    MessageBox.Show("单位必须为汉字！");
                    return;
                }

                if (dataGridView2.Rows[i].Cells[4].Value != null)
                {
                    if (!Regex.IsMatch(dataGridView2.Rows[i].Cells[1].Value.ToString(), @"^[+-]?(?!0\d)\d+(\.[0-9]+)?$"))
                    {
                        MessageBox.Show("折扣价必须为数字！");
                        return;
                    }
                }
            }

            //判断仓库里是否有对应的物品
            for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
            {
                string name = dataGridView2.Rows[i].Cells[0].Value.ToString().Trim();
                string number = dataGridView2.Rows[i].Cells[1].Value.ToString().Trim();

                if (db.warehouses.Any(m => m.warehouse_name == name) == false)
                {
                    MessageBox.Show("仓库中不存在" + name + "商品！");
                    return;
                }
                else
                {
                    var mo = db.warehouses.Select(m => m);
                    mo = mo.Where(m => m.warehouse_name.IndexOf(name) >= 0);

                    foreach (var item in mo)
                    {
                        if (item.warehouse_number < Convert.ToDecimal(number))
                        {
                            MessageBox.Show("仓库中的" + name + "剩余量不足！");
                            return;
                        }
                    }
                }
            }

            //如验证无误，则存入数据库
            DBCL.sale model = new DBCL.sale();
            string sales_id = "";
            string sales_whereabouts = "";
            string sales_worker_id = "";
            string sales_data = "";
            string sales_name = "";
            string sales_nuit = "";
            string sales_number = "";
            string sales_price = "";
            string sales_real_price = "";
            string sales_others = "";
            string sales_whole_price = "";

            sales_id = textBox1.Text.Trim();
            sales_whereabouts = textBox2.Text.Trim();
            sales_worker_id = username;
            sales_data = textBox3.Text.Trim();
            sales_whole_price = textBox4.Text.Trim();

            for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
            {
                sales_name = dataGridView2.Rows[i].Cells[0].Value.ToString().Trim();
                sales_number = dataGridView2.Rows[i].Cells[1].Value.ToString().Trim();
                sales_price = dataGridView2.Rows[i].Cells[2].Value.ToString().Trim();
                sales_nuit = dataGridView2.Rows[i].Cells[3].Value.ToString().Trim();


                if (dataGridView2.Rows[i].Cells[4].Value == null)
                {
                    sales_real_price = sales_price;
                }
                else
                {
                    sales_real_price = dataGridView2.Rows[i].Cells[4].Value.ToString().Trim();
                }

                if (dataGridView2.Rows[i].Cells[5].Value == null)
                {
                    sales_others = "";
                }
                else
                {
                    sales_others = dataGridView2.Rows[i].Cells[5].Value.ToString().Trim();
                }

                //向销售表中添加的信息
                model.sales_id = sales_id;
                model.sales_whereabouts = sales_whereabouts;
                model.sales_worker_id = sales_worker_id;
                model.sales_data = sales_data;
                model.sales_name = sales_name;
                model.sales_unit = sales_nuit;
                model.sales_number = Convert.ToDecimal(sales_number);
                model.sales_price = Convert.ToDecimal(sales_price);
                model.sales_real_price = Convert.ToDecimal(sales_real_price);
                model.sales_others = sales_others;
                model.sales_whole_price = Convert.ToDecimal(sales_whole_price);

                //仓库表中修改的信息
                var model2 = db.warehouses.FirstOrDefault(m => m.warehouse_name == sales_name);
                model2.warehouse_number = model2.warehouse_number - Convert.ToDecimal(sales_number);

                try
                {
                    db.sales.Add(model);
                    db.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("销售失败!");
                    return;
                }
            }

            //添加收入单
            var model3 = db.recipts.Select(m => m);
            string x1 = "";

            foreach (var item in model3)
            {
                x1 = item.receipt_id.ToString();
            }

            string x11 = x1.Substring(2, 8);
            string x12 = x1.Substring(10, 3);
            int x0 = Convert.ToInt32(x12);

            if (x11 == DateTime.Now.ToString("yyyyMMdd"))
            {
                x0++;
            }
            else
            {
                x11 = DateTime.Now.ToString("yyyyMMdd");
                x0 = 1;
            }

            DBCL.recipt model4 = new DBCL.recipt();
            string receipt_id = "";

            if (x0.ToString().Length == 1)
            {
                receipt_id = "SK" + x11 + "00" + x0;
            }
            else if (x0.ToString().Length == 2)
            {
                receipt_id = "SK" + x11 + "0" + x0;
            }
            else
            {
                receipt_id = "SK" + x11 + x0;
            }

            string receipt_sales_id = sales_id; ;
            string receipt_worker_id = username;
            string receipt_data = x11;
            string receipt_number = sales_whole_price;
            string receipt_type = "现金";
            string receipt_others = "无";

            model4.receipt_id = receipt_id;
            model4.receipt_sales_id = sales_id;
            model4.receipt_worker_id = username;
            model4.receipt_data = receipt_data;
            model4.receipt_number = Convert.ToDecimal(receipt_number);
            model4.receipt_type = receipt_type;
            model4.receipt_others = receipt_others;

            try
            {
                db.recipts.Add(model4);
                db.SaveChanges();
            }
            catch
            {
                MessageBox.Show("销售失败！");
                return;
            }

            //总资金修改
            var model5 = db.funds.FirstOrDefault(m => m.funds_id == 1);
            model5.funds_number = model5.funds_number + Convert.ToDecimal(sales_whole_price);

            try
            {
                db.SaveChanges();
            }
            catch
            {
                MessageBox.Show("销售失败！");
                return;
            }

            MessageBox.Show("销售成功！");
            textBox1.Text = "";

            var model6 = db.sales.Select(m => m);
            string tb1 = "";

            //更新标签页内容
            foreach (var item in model6)
            {
                tb1 = item.sales_id.ToString();
            }

            string tb11 = tb1.Substring(2, 8);
            string tb12 = tb1.Substring(10, 3);
            int num = Convert.ToInt32(tb12);

            if (tb11 == DateTime.Now.ToString("yyyyMMdd"))
            {
                num++;
            }
            else
            {
                tb11 = DateTime.Now.ToString("yyyyMMdd");
                num = 1;
            }

            if (num.ToString().Length == 1)
            {
                textBox1.Text = "XS" + tb11 + "00" + num;
            }
            else if (num.ToString().Length == 2)
            {
                textBox1.Text = "XS" + tb11 + "0" + num;
            }
            else
            {
                textBox1.Text = "XS" + tb11 + num;
            }

            textBox2.Text = "";
            textBox3.Text = DateTime.Now.ToString("yyyy-MM-dd");
            textBox4.Text = "";
            dataGridView2.Rows.Clear();
        }

        //销售记录************************************************************************************************************

        //关闭『销售记录』标签页
        private void button1_Click(object sender, EventArgs e)
        {
            销售记录.Parent = null;
        }

        //删除销售记录中的某几行数据
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("确认删除？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                {
                    return;
                }

                foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                {
                    string sales_id = r.Cells[0].Value.ToString();
                    var model = db.sales.Select(m => m);
                    model = model.Where(m => m.sales_id == sales_id);
                    string money = "";

                    foreach (var item in model)
                    {
                        string sales_name = item.sales_name;
                        string sales_number = Convert.ToString(item.sales_number);
                        money = Convert.ToString(item.sales_whole_price);

                        //修改库存量
                        var model2 = db.warehouses.FirstOrDefault(m => m.warehouse_name == sales_name);
                        model2.warehouse_number = model2.warehouse_number + Convert.ToDecimal(sales_number);

                        db.sales.Remove(item);
                    }

                    //减少总资金
                    var model3 = db.funds.FirstOrDefault(m => m.funds_id == 1);
                    model3.funds_number = model3.funds_number - Convert.ToDecimal(money);

                    //删除收入记录
                    var model4 = db.recipts.FirstOrDefault(m => m.receipt_sales_id == sales_id);
                    db.recipts.Remove(model4);

                    try
                    {
                        db.SaveChanges();
                    }
                    catch
                    {
                        MessageBox.Show("删除失败！");
                        return;
                    }
                }

                MessageBox.Show("删除成功！");

            }
            else
            {
                MessageBox.Show("请选择要删除的数据行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            dataGridView1.Rows.Clear();

            //重新加载表单内容
            if (users_level[23] == '1')
            {
                var model = db.sales.Select(m => m);
                int line = 0;
                string lastname = "XS20161111001";

                foreach (var item in model)
                {
                    if (lastname != item.sales_id)
                    {
                        lastname = item.sales_id;
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[line].Cells[0].Value = item.sales_id;
                        dataGridView1.Rows[line].Cells[1].Value = item.sales_data;
                        line++;
                    }
                }
            }
            else
            {
                var model = db.sales.Select(m => m);
                model = model.Where(m => m.sales_worker_id == username);
                int line = 0;
                string lastname = "XS20161111001";

                foreach (var item in model)
                {
                    if (lastname != item.sales_id)
                    {
                        lastname = item.sales_id;
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[line].Cells[0].Value = item.sales_id;
                        dataGridView1.Rows[line].Cells[1].Value = item.sales_data;
                        line++;
                    }
                }
            }
        }

        //查看选中行的销售单详情
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1)
                {
                    return;
                }
                else
                {
                    string current_sales_id = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

                    DetailOfSales dos = new DetailOfSales(username, current_sales_id, power9);
                    dos.ShowDialog();
                }
            }
            catch { }
           
        }

        //销售退货************************************************************************************************************

        //关闭『新建客户退货』标签页
        private void button3_Click(object sender, EventArgs e)
        {
            新建客户退货.Parent = null;
        }

        //保存到数据库
        private void button6_Click(object sender, EventArgs e)
        {
            //检验标签格式是否正确
            if (textBox5.Text.Trim() == null)
            {
                MessageBox.Show("收货单号不能为空！");
                return;
            }

            for (int i = 0; i < dataGridView3.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView3.Columns.Count - 1; j++)
                {
                    if (dataGridView3.Rows[i].Cells[j].Value == null)
                    {
                        MessageBox.Show("第" + (i + 1) + "行" + (j + 1) + "列处不能为空！");
                        return;
                    }
                }
            }

            for (int i = 0; i < dataGridView3.Rows.Count - 1; i++)
            {
                if (!Regex.IsMatch(dataGridView3.Rows[i].Cells[1].Value.ToString(), @"^[+-]?(?!0\d)\d+(\.[0-9]+)?$"))
                {
                    MessageBox.Show("数量必须为数字！");
                    return;
                }

                if (!Regex.IsMatch(dataGridView3.Rows[i].Cells[2].Value.ToString(), @"^[+-]?(?!0\d)\d+(\.[0-9]+)?$"))
                {
                    MessageBox.Show("单价必须为数字！");
                    return;
                }

                if (!Regex.IsMatch(dataGridView3.Rows[i].Cells[3].Value.ToString(), "^[\u4e00-\u9fa5]$"))
                {
                    MessageBox.Show("单位必须为汉字！");
                    return;
                }
            }

            //验证是否有此进货单
            var model = db.sales.Where(m => m.sales_id == textBox6.Text.Trim());

            if (model.Count() == 0)
            {
                MessageBox.Show("此销售单不存在，请重新输入！");
                return;
            }

            //验证表单内容是否正确
            for (int i = 0; i < dataGridView3.Rows.Count - 1; i++)
            {
                bool flag = false;

                foreach (var item in model)
                {
                    if(dataGridView3.Rows[i].Cells[0].Value.ToString() == item.sales_name)
                    {
                        flag = true;
                        break;
                    }
                }

                if(flag == false)
                {
                    MessageBox.Show("退货表单信息有误！");
                    return;
                }
            }

            //向数据库写入数据
            DBCL.TKReturn model2 = new DBCL.TKReturn();
            string TKReturns_id = "";
            string TKReturns_salesid = "";
            string TKReturns_worker_id = "";
            string TKReturns_data = "";
            string TKReturns_name = "";
            string TKReturns_unit = "";
            string TKReturns_price = "";
            string TKReturns_number = "";
            string TKReturns_others = "";

            TKReturns_id = textBox5.Text.Trim();
            TKReturns_salesid = textBox6.Text.Trim();
            TKReturns_worker_id = username;
            TKReturns_data = textBox7.Text.Trim();

            for (int i = 0; i < dataGridView3.Rows.Count - 1; i++)
            {
                TKReturns_name = dataGridView3.Rows[i].Cells[0].Value.ToString().Trim();
                TKReturns_number = dataGridView3.Rows[i].Cells[1].Value.ToString().Trim();
                TKReturns_price = dataGridView3.Rows[i].Cells[2].Value.ToString().Trim();
                TKReturns_unit = dataGridView3.Rows[i].Cells[3].Value.ToString().Trim();

                if (dataGridView3.Rows[i].Cells[4].Value == null)
                {
                    TKReturns_others = "";
                }
                else
                {
                    TKReturns_others = dataGridView3.Rows[i].Cells[4].Value.ToString().Trim();
                }

                model2.TKReturns_id = TKReturns_id;
                model2.TKReturns_salesid = TKReturns_salesid;
                model2.TKReturns_worker_id = TKReturns_worker_id;
                model2.TKReturns_data = TKReturns_data;
                model2.TKReturns_name = TKReturns_name;
                model2.TKReturns_number = Convert.ToDecimal(TKReturns_number);
                model2.TKReturns_price = Convert.ToDecimal(TKReturns_price);
                model2.TKReturns_unit = TKReturns_unit;
                model2.TKReturns_others = TKReturns_others;

                //更改仓库货物数量
                var model3 = db.warehouses.FirstOrDefault(m => m.warehouse_name == TKReturns_name);
                model3.warehouse_number = model3.warehouse_number + Convert.ToDecimal(TKReturns_number);

                //保存到客户进货单
                try
                {
                    db.TKReturns.Add(model2);
                    db.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("失败！");
                    return;
                }
            }

            //添加支出单
            var model4 = db.TKReturns.Select(m=>m);
            string x1 = "";

            foreach(var item in model4)
            {
                x1 = item.TKReturns_id;
            }

            string x11 = x1.Substring(2, 8);
            string x12 = x1.Substring(10, 3);
            int x0 = Convert.ToInt32(x12);

            if (x11 == DateTime.Now.ToString("yyyyMMdd"))
            {
                x0++;
            }
            else
            {
                x11 = DateTime.Now.ToString("yyyyMMdd");
                x0 = 1;
            }

            DBCL.pay model5 = new DBCL.pay();
            string pay_id = "";

            if (x0.ToString().Length == 1)
            {
                pay_id = "FK" + x11 + "00" + x0;
            }
            else if (x0.ToString().Length == 2)
            {
                pay_id = "FK" + x11 + "0" + x0;
            }
            else
            {
                pay_id = "FK" + x11 + x0;
            }

            string pay_purchase_id = TKReturns_id;
            string pay_worker_id = username;
            string pay_data = x11;

            //退款总价
            string pay_number = "";
            double money = 0;

            for(int i = 0;i<dataGridView3.Rows.Count - 1;i++)
            {
                money = money + Convert.ToDouble(dataGridView3.Rows[i].Cells[1].Value.ToString()) * Convert.ToDouble(dataGridView3.Rows[i].Cells[2].Value.ToString());
            }

            pay_number = Convert.ToString(money);

            string pay_type = "现金";
            string pay_others = "无";

            model5.pay_id = pay_id;
            model5.pay_purchase_id = pay_purchase_id;
            model5.pay_worker_id = pay_worker_id;
            model5.pay_data = pay_data;
            model5.pay_number = Convert.ToDecimal(pay_number);
            model5.pay_type = pay_type;
            model5.pay_others = pay_others;

            try
            {
                db.pays.Add(model5);
                db.SaveChanges();
            }
            catch
            {
                MessageBox.Show("退货失败！");
                return;
            }


            //修改总金额
            var model6 = db.funds.FirstOrDefault(m => m.funds_id == 1);
            model6.funds_number = model6.funds_number - Convert.ToDecimal(pay_number);
            

            try
            {
                db.SaveChanges();
            }
            catch
            {
                MessageBox.Show("退货失败！");
                return;
            }

            MessageBox.Show("退货成功！");

            //重新加载页面

            var model7 = db.TKReturns.Select(m => m);
            string tb1 = "";

            foreach (var item in model7)
            {
                tb1 = item.TKReturns_id.ToString();
            }

            string tb11 = tb1.Substring(2, 8);
            string tb12 = tb1.Substring(10, 3);
            int num = Convert.ToInt32(tb12);

            if (tb11 == DateTime.Now.ToString("yyyyMMdd"))
            {
                num++;
            }
            else
            {
                tb11 = DateTime.Now.ToString("yyyyMMdd");
                num = 1;
            }

            if (num.ToString().Length == 1)
            {
                textBox5.Text = "TK" + tb11 + "00" + num;
            }
            else if (num.ToString().Length == 2)
            {
                textBox5.Text = "TK" + tb11 + "0" + num;
            }
            else
            {
                textBox5.Text = "TK" + tb11 + num;
            }

            textBox6.Text = "";
            textBox7.Text = DateTime.Now.ToString("yyyy-MM-dd");
            textBox8.Text = username;
            dataGridView3.Rows.Clear();
        }

        //客户退货记录********************************************************************************************************

        //关闭『客户退货记录』标签页
        private void button8_Click(object sender, EventArgs e)
        {
            客户退货记录.Parent = null;
        }

        //查看选中行的退货单详情
        private void dataGridView4_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex == -1)
            {
                return;
            }
            else
            {
                string current_TK_id = dataGridView4.Rows[e.RowIndex].Cells[0].Value.ToString();

                DetailOfTK dot = new DetailOfTK(username,current_TK_id,power12);
                dot.ShowDialog();
            }
        }

        //删除退货单的几行数据
        private void button7_Click(object sender, EventArgs e)
        {
            if (dataGridView4.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("确认删除？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                {
                    return;
                }

                foreach (DataGridViewRow r in dataGridView4.SelectedRows)
                {
                    string TKReturns_id = r.Cells[0].Value.ToString();
                    var model = db.TKReturns.Select(m => m);
                    model = model.Where(m => m.TKReturns_id == TKReturns_id);
                    double money = 0;

                    foreach (var item in model)
                    {
                        string TKReturns_name = item.TKReturns_name;
                        string TKReturns_number = Convert.ToString(item.TKReturns_number);
                        money = money + Convert.ToDouble(item.TKReturns_number) * Convert.ToDouble(item.TKReturns_price);

                        //库存量减少
                        var model2 = db.warehouses.FirstOrDefault(m => m.warehouse_name == TKReturns_name);
                        model2.warehouse_number = model2.warehouse_number - Convert.ToDecimal(TKReturns_number);

                        db.TKReturns.Remove(item);
                    }

                    //增加总资金
                    var model3 = db.funds.FirstOrDefault(m => m.funds_id == 1);
                    model3.funds_number = model3.funds_number + Convert.ToDecimal(money);

                    //删除支出记录
                    var model4 = db.pays.FirstOrDefault(m => m.pay_purchase_id == TKReturns_id);
                    db.pays.Remove(model4);

                    try
                    {
                        db.SaveChanges();
                    }
                    catch
                    {
                        MessageBox.Show("删除失败！");
                        return;
                    }
                }

                MessageBox.Show("删除成功！");

            }
            else
            {
                MessageBox.Show("请选择要删除的数据行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //重新加载表单内容
            dataGridView4.Rows.Clear();
            
            if (users_level[23] == '1')
            {
                var model = db.TKReturns.Select(m => m);
                int line = 0;
                string lastname = "TK00000000000";

                foreach (var item in model)
                {
                    if (lastname != item.TKReturns_id)
                    {
                        lastname = item.TKReturns_id;
                        dataGridView4.Rows.Add();
                        dataGridView4.Rows[line].Cells[0].Value = item.TKReturns_id;
                        dataGridView4.Rows[line].Cells[1].Value = item.TKReturns_data;
                        line++;
                    }
                }
            }
            else
            {
                var model = db.TKReturns.Select(m => m);
                model = model.Where(m => m.TKReturns_worker_id == username);
                int line = 0;
                string lastname = "TK00000000000";

                foreach (var item in model)
                {
                    if (lastname != item.TKReturns_id)
                    {
                        lastname = item.TKReturns_id;
                        dataGridView4.Rows.Add();
                        dataGridView4.Rows[line].Cells[0].Value = item.TKReturns_id;
                        dataGridView4.Rows[line].Cells[1].Value = item.TKReturns_data;
                        line++;
                    }
                }
            }
        }

        //新建收入************************************************************************************************************

        //关闭『新建收入』标签页
        private void button9_Click(object sender, EventArgs e)
        {
            新建收入.Parent = null;
        }

        //将新数据保存到数据库
        private void button10_Click(object sender, EventArgs e)
        {
            string receipt_id = "";
            string receipt_sales_id = "";
            string receipt_worker_id = "";
            string receipt_data = "";
            string receipt_number = "";
            string receipt_type = "";
            string receipt_others = "";

            if(textBox13.Text.Trim() == "")
            {
                MessageBox.Show("数目不能为空","提示",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            if (textBox14.Text.Trim() == "")
            {
                MessageBox.Show("收款方式不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (textBox15.Text.Trim() == "")
            {
                MessageBox.Show("备注不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            receipt_id = textBox12.Text.Trim();
            receipt_sales_id = "无(非销售收入)";
            receipt_worker_id = textBox9.Text.Trim();
            receipt_data = DateTime.Now.ToString("yyyyMMdd");
            receipt_number = textBox13.Text.Trim();
            receipt_type = textBox14.Text.Trim();
            receipt_others = textBox15.Text.Trim();

            DBCL.recipt re = new DBCL.recipt();

            re.receipt_id = receipt_id;
            re.receipt_sales_id = receipt_sales_id;
            re.receipt_worker_id = receipt_worker_id;
            re.receipt_data = receipt_data;
            re.receipt_number = Convert.ToDecimal(receipt_number);
            re.receipt_type = receipt_type;
            re.receipt_others = receipt_others;

            //总金额增加

            var model = db.funds.FirstOrDefault(m => m.funds_id == 1);
            model.funds_number = model.funds_number + Convert.ToDecimal(receipt_number);

            try
            {
                db.recipts.Add(re);
                db.SaveChanges();
            }
            catch
            {
                MessageBox.Show("添加失败！");
                return;
            }

            MessageBox.Show("添加成功！");

            //刷新界面

            var model2 = db.recipts.Select(m => m);
            string tb1 = "";

            foreach (var item in model2)
            {
                tb1 = item.receipt_id.ToString();
            }

            string tb11 = tb1.Substring(2, 8);
            string tb12 = tb1.Substring(10, 3);
            int num = Convert.ToInt32(tb12);

            if (tb11 == DateTime.Now.ToString("yyyyMMdd"))
            {
                num++;
            }
            else
            {
                tb11 = DateTime.Now.ToString("yyyyMMdd");
                num = 1;
            }

            if (num.ToString().Length == 1)
            {
                textBox12.Text = "SK" + tb11 + "00" + num;
            }
            else if (num.ToString().Length == 2)
            {
                textBox12.Text = "SK" + tb11 + "0" + num;
            }
            else
            {
                textBox12.Text = "SK" + tb11 + num;
            }

            textBox10.Text = DateTime.Now.ToString("yyyy-MM-dd");
            textBox9.Text = username;
            textBox13.Text = "";
            textBox14.Text = "";
            textBox15.Text = "";
        }

        //收入记录************************************************************************************************************

        //关闭『收入记录』标签页
        private void button12_Click(object sender, EventArgs e)
        {
            收入记录.Parent = null;
        }

        //新建支出************************************************************************************************************

        //关闭『新建支出』标签页
        private void button11_Click(object sender, EventArgs e)
        {
            新建支出.Parent = null;
        }

        //将新数据保存到数据库
        private void button13_Click(object sender, EventArgs e)
        {
            string pay_id = "";
            string pay_purchase_id = "";
            string pay_worker_id = "";
            string pay_data = "";
            string pay_number = "";
            string pay_type = "";
            string pay_others = "";

            if (textBox17.Text.Trim() == "")
            {
                MessageBox.Show("数目不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (textBox16.Text.Trim() == "")
            {
                MessageBox.Show("收款方式不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (textBox11.Text.Trim() == "")
            {
                MessageBox.Show("备注不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            pay_id = textBox20.Text.Trim();
            pay_purchase_id = "无(非退款支出)";
            pay_worker_id = textBox18.Text.Trim();
            pay_data = DateTime.Now.ToString("yyyyMMdd");
            pay_number = textBox17.Text.Trim();
            pay_type = textBox16.Text.Trim();
            pay_others = textBox11.Text.Trim();

            DBCL.pay pa = new DBCL.pay();

            pa.pay_id = pay_id;
            pa.pay_purchase_id = pay_purchase_id;
            pa.pay_worker_id = pay_worker_id;
            pa.pay_data = pay_data;
            pa.pay_number = Convert.ToDecimal(pay_number);
            pa.pay_type = pay_type;
            pa.pay_others = pay_others;

            //总金额增加

            var model = db.funds.FirstOrDefault(m => m.funds_id == 1);
            model.funds_number = model.funds_number - Convert.ToDecimal(pay_number);

            try
            {
                db.pays.Add(pa);
                db.SaveChanges();
            }
            catch
            {
                MessageBox.Show("添加失败！");
                return;
            }

            MessageBox.Show("添加成功！");


            //重载标签页
            新建支出.Parent = tabControl1;
            tabControl1.SelectTab("新建支出");
            
            var model2 = db.pays.Select(m => m);
            string tb1 = "";

            foreach (var item in model2)
            {
                tb1 = item.pay_id.ToString();
            }

            string tb11 = tb1.Substring(2, 8);
            string tb12 = tb1.Substring(10, 3);
            int num = Convert.ToInt32(tb12);

            if (tb11 == DateTime.Now.ToString("yyyyMMdd"))
            {
                num++;
            }
            else
            {
                tb11 = DateTime.Now.ToString("yyyyMMdd");
                num = 1;
            }

            if (num.ToString().Length == 1)
            {
                textBox20.Text = "FK" + tb11 + "00" + num;
            }
            else if (num.ToString().Length == 2)
            {
                textBox20.Text = "FK" + tb11 + "0" + num;
            }
            else
            {
                textBox20.Text = "FK" + tb11 + num;
            }

            textBox19.Text = DateTime.Now.ToString("yyyy-MM-dd");
            textBox18.Text = username;
            textBox17.Text = "";
            textBox16.Text = "";
            textBox11.Text = "";
        }

        //支出记录************************************************************************************************************

        //关闭『支出记录』标签页
        private void button14_Click(object sender, EventArgs e)
        {
            支出记录.Parent = null;
        }

        
        //祖浩然

        //新建进货************************************************************************************************************

        //进货单提交
        private void j_submit_Click(object sender, EventArgs e)
        {
            //变量*************************************************************
            int length = j_d.Rows.Count;
            int k = 0;
            string id = "", workid = "", data = "", sum = "", realsum = "", source = "";
            string name = "", unit = "", price = "", other = "",date="",size="",code="";
            decimal dnumber = 0, dprice = 0, whole_price = 0, real_price = 0;
            //校验
            if (length == 1)
            {
                MessageBox.Show("请输入数据");
                return;
            }
            for (int i = 0; i < length - 1; i++)
            {
                k = i + 1;
                if (j_d.Rows[i].Cells[1].Value == null && j_d.Rows[i].Cells[2].Value == null && j_d.Rows[i].Cells[3].Value == null && j_d.Rows[i].Cells[4].Value == null)
                { continue; }
                //判断名称不为空，数据库是否存在，如存在则继续，不存在

                if (j_d.Rows[i].Cells[1].Value == null)
                {
                    MessageBox.Show("第" + k + "行名称不能为空");
                    return;
                }

                //判断数量是否为空，是否为正整数
                if (j_d.Rows[i].Cells[2].Value == null)
                {
                    MessageBox.Show("第" + k + "行数量不能为空");
                    return;
                }

                if (Validator.IsNumStr(Convert.ToString(j_d.Rows[i].Cells[2].Value)) == false)
                {
                    MessageBox.Show("第" + k + "行数量不是正整数或超出10位");
                    return;
                }
                //判断单位是否为空，是否与数据库该商品单位相同，不相同则返回报错
                if (j_d.Rows[i].Cells[3].Value == null)
                {
                    MessageBox.Show("第" + k + "行单位不能为空");
                    return;
                }
                name = Convert.ToString(j_d.Rows[i].Cells[1].Value);
                unit = Convert.ToString(j_d.Rows[i].Cells[3].Value);
                DBCL.DBEntities dbc = new DBCL.DBEntities();
                var w_model = dbc.warehouses.Select(m => m);
                w_model = w_model.Where(m => m.warehouse_name == name);
                string dunit = "";
                foreach (var item in w_model)
                {
                    dunit = item.warehouse_unit;
                }
                if (dunit != "")
                {
                    if (dunit != unit)
                    {
                        MessageBox.Show("第" + k + "行单位错误应为:" + dunit);
                        return;
                    }
                }


                //判断单价是否为空，是否为保留两位小数，或整数
                if (j_d.Rows[i].Cells[4].Value == null)
                {
                    MessageBox.Show("第" + k + "行单价不能为空");
                    return;
                }
                price = Convert.ToString(j_d.Rows[i].Cells[4].Value);
                if (Validator.IsNumber2(price) == false)
                {
                    MessageBox.Show("第" + k + "行单价必须是正整数或两位以内小数");
                    return;
                }
            }
            //判断折扣价是否为空，若为空则赋值为实际价格
            if (j_realsum.Text.Trim() == "")
            {
                j_realsum.Text = j_sum.Text.Trim();
            }
            //判断供货商是否为空，提示并返回
            if (j_source.Text.Trim() == "")
            {
                MessageBox.Show("请输入供货商");
                return;
            }
            //获取变量
            id = j_id.Text.Trim();
            workid = j_workid.Text.Trim();
            data = j_data.Text.Trim();
            sum = j_sum.Text.Trim();
            realsum = j_realsum.Text.Trim();
            source = j_source.Text.Trim();

            //存入数据库
            DBCL.purchase model = new DBCL.purchase();
            try
            {
                //增加付款单
                if (pay_setnew(id, workid, data,Convert.ToDecimal( realsum)) == false) 
                {  
                    MessageBox.Show("付款单添加失败!");
                    return; 
                };
                //修改资金总额
                if (z_setmoney("-", real_price) == false)
                {
                    MessageBox.Show("余额不足");
                    return; 
                };
                
            }
            catch
            {
                MessageBox.Show("付款单添加失败!");
                return;
            }
            for (int i = 0; i < length-1; i++)
            {
                if (j_d.Rows[i].Cells[1].Value == null && j_d.Rows[i].Cells[2].Value == null && j_d.Rows[i].Cells[3].Value == null && j_d.Rows[i].Cells[4].Value == null)
                { continue; }
                //获取表格变量
                name = Convert.ToString(j_d.Rows[i].Cells[1].Value);
                dnumber = Convert.ToDecimal(j_d.Rows[i].Cells[2].Value);
                unit = Convert.ToString(j_d.Rows[i].Cells[3].Value);
                dprice = Convert.ToDecimal(j_d.Rows[i].Cells[4].Value);
                other = Convert.ToString(j_d.Rows[i].Cells[5].Value);
                date = Convert.ToString(j_d.Rows[i].Cells[6].Value);
                size = Convert.ToString(j_d.Rows[i].Cells[7].Value);
                code = Convert.ToString(j_d.Rows[i].Cells[8].Value);
                whole_price = Convert.ToDecimal(sum);
                real_price = Convert.ToDecimal(realsum);
                model.purchase_id = id;
                model.purchase_worker_id = workid;
                model.purchase_data = data;
                model.purchase_name = name;
                model.purchase_number = dnumber;
                model.purchase_unit = unit;
                model.purchase_price = dprice;
                model.purchase_whole_price = whole_price;
                model.purchase_real_price = real_price;
                model.purchase_source = source;
                model.purchase_others = other;
                model.purchase_date = date;
                model.purchase_size = size;
                model.purchase_code = code;
                try
                {
                    db.purchases.Add(model);
                    //修改仓库库存*****************************************************
                    //判断仓库是否存在该物品 如果存在更新数据，如果不存在就插入数据
                    if (db.warehouses.Any(m => m.warehouse_name == name) == true)
                    {
                        //更新仓库数量
                        if (c_setnumber("+", name, dnumber) == false) { return; };
                    }
                    else
                    {
                        //插入新物品
                        if (w_setnew(name, dnumber, unit) == false) { return; };
                    }
                    try
                    {
                        db.SaveChanges();
                    }
                    catch
                    {
                        MessageBox.Show("添加失败!");
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("添加失败!");
                    return;
                }
                

            }
           
            MessageBox.Show("添加成功!");
            

            j_id.Text = j_getid();
            j_workid.Text = username;
            j_d.Rows.Clear();
            j_sum.Text = "";
            j_realsum.Text = "";
            j_source.Text = "";
        }

        //进货单重置
        private void j_reset_Click(object sender, EventArgs e)
        {
            j_id.Text = j_getid();
            j_workid.Text = username;
            j_d.Rows.Clear();
            j_sum.Text = "";
            j_realsum.Text = "";
            j_source.Text = "";
        }
        
        //进货单获取总价
        private void j_d_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int length = j_d.Rows.Count;//数据总行数
            decimal sum = 0;
            for (int i = 0; i < length - 1; i++)
            {
                decimal price = 0, number = 0;
                string snumber = "", sprice = "";
                if (j_d.Rows[i].Cells[2].Value != null)
                {
                    snumber = Convert.ToString(j_d.Rows[i].Cells[2].Value);
                    if (Validator.IsNumStr(snumber) == true)
                    {
                        number = Convert.ToDecimal(snumber);
                    }
                    else
                    {
                        number = 0;
                    }
                }
                if (j_d.Rows[i].Cells[4].Value != null)
                {
                    sprice = Convert.ToString(j_d.Rows[i].Cells[4].Value);
                    if (Validator.IsNumber(sprice) == true)
                    {
                        price = Convert.ToDecimal(sprice);
                    }
                    else
                    {
                        price = 0;
                    }
                }
                sum += (number * price);
            }
            j_sum.Text = Convert.ToString(sum);
        }
        
        //进货记录************************************************************************************************************

        //删除历史进货
        private void jh_delete_Click(object sender, EventArgs e)
        {
            //变量 
            string d = "", id = "";
            int length = 0;
            DialogResult dr = MessageBox.Show("是否删除", "删除", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            d = dr.ToString();
            if (d == "OK")
            {
                //将选中的行添加到动态数值内
                List<int> jh_sd_del = new List<int>();
                foreach (DataGridViewCell c in jh_sd.SelectedCells)
                {
                    length = jh_sd_del.Count;
                    bool panc = false;
                    for (int i = 0; i < length; i++)
                    {
                        if (jh_sd_del[i] == c.RowIndex)
                        {
                            panc = true;
                        }
                    }
                    if (panc == false)
                    {
                        jh_sd_del.Add(c.RowIndex);

                        //删除进货单
                        id = Convert.ToString(jh_sd.Rows[c.RowIndex].Cells[0].Value);
                        //总资金
                        var zmodel = db.purchases.FirstOrDefault(m => m.purchase_id==id);
                        try { if (z_setmoney("+", Convert.ToDecimal(zmodel.purchase_real_price)) == false) { }; }
                        catch { }
                        var model = db.purchases.Select(m => m);
                        model=model.Where(m =>m.purchase_id.IndexOf(id) >=0);
                        if (model == null)
                        {
                            MessageBox.Show("该角色在数据库中不存在!");
                            return;
                        }
                        //删除对应付款单
                        try
                        {
                            var pay_model = db.pays.FirstOrDefault(m => m.pay_purchase_id == id);
                            pay_delete(pay_model.pay_id);
                        }
                        catch
                        {
                            DialogResult drs = MessageBox.Show("未找到相应的付款单是否继续", "删除", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                            d = drs.ToString();
                            if (d != "OK")
                            { return; }
                        }
                        foreach (var item in model)
                        {
                            try
                            {
                                //仓库修改 减去增加的
                                c_setnumber("-", item.purchase_name, Convert.ToDecimal(item.purchase_number));
                            }
                            catch
                            {

                            }
                            db.purchases.Remove(item);
                        }
                    }

                }

                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("删除失败!");
                    return;
                }
                MessageBox.Show("删除成功!");
                try 
                {
                    jh_sd.Rows.Clear();
                    var jh_model = db.purchases.Select(m => m);
                    if (jh_model == null)
                    {
                        MessageBox.Show("暂无数据");
                    }
                    if (username != "2017000")
                    {

                        jh_model = jh_model.Where(m => m.purchase_worker_id == username);
                    }
                    jh_sd.Rows.Clear();
                    List<string> tg = new List<string>();
                    bool pantg = false;
                    foreach (var item in jh_model)
                    {
                        for (int i = 0; i < tg.Count; i++)
                        {
                            if (tg[i] == item.purchase_id)
                            {
                                pantg = true;
                            }
                        }
                        if (pantg == false)
                        {
                            jh_sd.Rows.Add(item.purchase_id, item.purchase_data);
                            tg.Add(item.purchase_id);
                        }
                        pantg = false;
                    }

                    tg.Clear();
                }
                catch { }
               
            }
            else
            { return; }
        }

        //关闭『进货记录』标签页
        private void jh_close_Click(object sender, EventArgs e)
        {
            page_jh.Parent = null;
        }

        void sub_CloseWindow()
        {
            //改变父窗体控件内容
            jh_sd.Rows.Clear();
            DBCL.DBEntities db_j = new DBCL.DBEntities();
            var jh_model = db_j.purchases.Select(m => m);
            if (jh_model == null)
            {
                MessageBox.Show("暂无数据");
            }
            if (username != "2017000")
            {

                jh_model = jh_model.Where(m => m.purchase_worker_id == username);
            }

            jh_sd.Rows.Clear();
            List<string> tg = new List<string>();
            bool pantg = false;
            foreach (var item in jh_model)
            {
                for (int i = 0; i < tg.Count; i++)
                {
                    if (tg[i] == item.purchase_id)
                    {
                        pantg = true;
                    }
                }
                if (pantg == false)
                {
                    jh_sd.Rows.Add(item.purchase_id, item.purchase_data);
                    tg.Add(item.purchase_id);
                }
                pantg = false;
            }

            tg.Clear();

        }
        //历史进货单双击进行编辑
       
        private void jh_sd_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try 
            {
                if (e.RowIndex == -1)
                {
                    return;
                }
               
                int row = jh_sd.SelectedCells[0].RowIndex;
                string chushiid = Convert.ToString(jh_sd.Rows[row].Cells[0].Value);
                jh jh_f = new jh(chushiid, power1);
                jh_f.CloseWindow += new Action(sub_CloseWindow); 
                jh_f.MdiParent = this;
                jh_f.Show();
                SetParent((int)jh_f.Handle, (int)this.Handle);
            }
            catch { }
            
        }
        //新建仓库退货************************************************************************************************************

        //提交仓库退货单
        private void ct_submit_Click(object sender, EventArgs e)
        {
            //变量*************************************************************
            int length = ct_d.Rows.Count;
            int k = 0;
            string id = "", workid = "", data = "", sum = "", source = "";
            string name = "", unit = "", price = "", other = "";
            decimal dnumber = 0, dprice = 0, whole_price = 0;
            //校验
            if (length == 1)
            {
                MessageBox.Show("请输入数据");
                return;
            }
            for (int i = 0; i < length - 1; i++)
            {
                k = i + 1;

                if (ct_d.Rows[i].Cells[1].Value == null && ct_d.Rows[i].Cells[2].Value == null && ct_d.Rows[i].Cells[3].Value == null && ct_d.Rows[i].Cells[4].Value == null)
                { continue; }
                //判断名称不为空，数据库是否存在，如存在则继续，不存在
                if (ct_d.Rows[i].Cells[1].Value == null)
                {
                    MessageBox.Show("第" + k + "行名称不能为空");
                    return;
                }
                name = Convert.ToString(ct_d.Rows[i].Cells[1].Value);
                DBCL.DBEntities dbc = new DBCL.DBEntities();
                var w_model = dbc.warehouses.FirstOrDefault(m => m.warehouse_name == name);
                string dunit = "";
                if (w_model == null)
                {
                    MessageBox.Show("第" + k + "行名称仓库不存在");
                    return;
                }
                dunit = w_model.warehouse_unit;
                //判断数量是否为空，是否为正整数
                if (ct_d.Rows[i].Cells[2].Value == null)
                {
                    MessageBox.Show("第" + k + "行数量不能为空");
                    return;
                }
                decimal shuliang=Convert.ToDecimal(ct_d.Rows[i].Cells[2].Value);
                if(w_model.warehouse_number-shuliang <0)
                {
                    MessageBox.Show("第" + k + "行库存不足");
                    return;
                }
                if (Validator.IsNumStr(Convert.ToString(ct_d.Rows[i].Cells[2].Value)) == false)
                {
                    MessageBox.Show("第" + k + "行数量不是正整数或超出10位" );
                    return;
                }
                //判断单位是否为空，是否与数据库该商品单位相同，不相同则返回报错
                if (ct_d.Rows[i].Cells[3].Value == null)
                {
                    MessageBox.Show("第" + k + "行单位不能为空");
                    return;
                }
                name = Convert.ToString(ct_d.Rows[i].Cells[1].Value);
                unit = Convert.ToString(ct_d.Rows[i].Cells[3].Value);

                if (dunit != "")
                {
                    if (dunit != unit)
                    {
                        MessageBox.Show("第" + k + "行单位错误应为:" + dunit);
                        return;
                    }
                }


                //判断单价是否为空，是否为保留两位小数，或整数
                if (ct_d.Rows[i].Cells[4].Value == null)
                {
                    MessageBox.Show("第" + k + "行单价不能为空");
                    return;
                }
                price = Convert.ToString(ct_d.Rows[i].Cells[4].Value);
                if (Validator.IsNumber2(price) == false)
                {
                    MessageBox.Show("第" + k + "行单价必须是正整数或两位以内小数");
                    return;
                }
            }

            //判断供货商是否为空，提示并返回
            if (ct_source.Text.Trim() == "")
            {
                MessageBox.Show("请输入供货商");
                return;
            }
            //获取变量
            id = ct_id.Text.Trim();
            workid = ct_workid.Text.Trim();
            data = ct_data.Text.Trim();
            sum = ct_sum.Text.Trim();
            source = ct_source.Text.Trim();

            //存入数据库
            DBCL.TGReturn model = new DBCL.TGReturn();
            for (int i = 0; i < length - 1; i++)
            {
                if (ct_d.Rows[i].Cells[1].Value == null && ct_d.Rows[i].Cells[2].Value == null && ct_d.Rows[i].Cells[3].Value == null && ct_d.Rows[i].Cells[4].Value == null)
                { continue; }
                //获取表格变量
                name = Convert.ToString(ct_d.Rows[i].Cells[1].Value);
                dnumber = Convert.ToDecimal(ct_d.Rows[i].Cells[2].Value);
                unit = Convert.ToString(ct_d.Rows[i].Cells[3].Value);
                dprice = Convert.ToDecimal(ct_d.Rows[i].Cells[4].Value);
                other = Convert.ToString(ct_d.Rows[i].Cells[5].Value);
                whole_price = Convert.ToDecimal(sum);
                model.TGReturns_id = id;
                model.TGReturns_worker_id = workid;
                model.TGReturns_data = data;
                model.TGReturns_name = name;
                model.TGReturns_number = dnumber;
                model.TGreturns_unit = unit;
                model.TGReturns_price = dprice;
                model.TGReturns_source = source;
                model.TGReturns_others = other;
                try
                {
                    db.TGReturns.Add(model);
                    //修改仓库库存*****************************************************
                    //判断仓库是否存在该物品 如果存在更新数据，如果不存在就插入数据
                    if (db.warehouses.Any(m => m.warehouse_name == name) == true)
                    {
                        //更新仓库数量
                        if (c_setnumber("-", name, dnumber) == false) { return; };
                    }
                    else
                    {
                        MessageBox.Show("商品错误");
                        return;
                    }

                    //修改资金总额
                    if (z_setmoney("+", whole_price) == false) { return; };
                    db.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("添加失败!");
                    return;
                }

            }
            MessageBox.Show("退货单添加成功!");



            ct_id.Text = ct_getid();
            ct_workid.Text = username;
            ct_d.Rows.Clear();
            ct_sum.Text = "";
            ct_source.Text = "";
        }

        //仓库退货记录************************************************************************************************************

        //删除对应退货单
        private void cth_delete_Click(object sender, EventArgs e)
        {
            //变量 
            string d = "", id = "";
            int length = 0;
            DialogResult dr = MessageBox.Show("是否删除", "shanchu", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            d = dr.ToString();
            if (d == "OK")
            {
                //将选中的行添加到动态数值内
                List<int> cth_d_del = new List<int>();
                foreach (DataGridViewCell c in cth_d.SelectedCells)
                {
                    length = cth_d_del.Count;
                    bool panc = false;
                    for (int i = 0; i < length; i++)
                    {
                        if (cth_d_del[i] == c.RowIndex)
                        {
                            panc = true;
                        }
                    }
                    if (panc == false)
                    {
                        cth_d_del.Add(c.RowIndex);

                        //删除进货单
                        id = Convert.ToString(cth_d.Rows[c.RowIndex].Cells[0].Value);

                        var pmodel = db.TGReturns.FirstOrDefault(m => m.TGReturns_id == id);
                        if (pmodel == null)
                        {
                            MessageBox.Show("该角色在数据库中不存在!");
                            return;
                        }
                        if (d != "OK")
                        {
                            DialogResult drs = MessageBox.Show("资金是否进行相应修改", "删除", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                            d = drs.ToString();
                            if (d == "OK")
                            {
                                if (z_setmoney("-", Convert.ToDecimal(pmodel.TGReturns_number * pmodel.TGReturns_price)) == false)
                                { return; };
                            }
                        }
                        //总资金修改 减少的
                        var model = db.TGReturns.Select(m => m);
                        model = model.Where(m => m.TGReturns_id == id);
                        foreach (var item in model)
                        {
                            try
                            {
                                //仓库修改 增加的
                                c_setnumber("+", item.TGReturns_name, Convert.ToDecimal(item.TGReturns_number));
                            }
                            catch
                            {
                                DialogResult drs = MessageBox.Show("仓库未找到相应的物品是否继续", "删除", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                                d = drs.ToString();
                                if (d != "OK")
                                { return; }
                            }
                            db.TGReturns.Remove(item);
                        }
                    }
                   
                }

                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("删除失败!");
                    return;
                }
                MessageBox.Show("删除成功!");
                cth_d.Rows.Clear();
                var jh_model = db.TGReturns.Select(m => m);
                if (jh_model == null)
                {
                    MessageBox.Show("暂无数据");
                }
                if (username != "2017000")
                {

                    jh_model = jh_model.Where(m => m.TGReturns_worker_id == username);
                }
                cth_d.Rows.Clear();
                List<string> tg = new List<string>();
                bool pantg = false;
                foreach (var item in jh_model)
                {
                    for (int i = 0; i < tg.Count; i++)
                    {
                        if (tg[i] == item.TGReturns_id)
                        {
                            pantg = true;
                        }
                    }
                    if (pantg == false)
                    {
                        cth_d.Rows.Add(item.TGReturns_id, item.TGReturns_data);
                        tg.Add(item.TGReturns_id);
                    }
                    pantg = false;

                }
                tg.Clear();
            }
            else
            { return; }
        }


        void c_sub_CloseWindow()
        {
            DBCL.DBEntities db_c = new DBCL.DBEntities();
            //改变父窗体控件内容
            cth_d.Rows.Clear();
            var cth_model = db_c.TGReturns.Select(m => m);
            if (cth_model == null)
            {
                MessageBox.Show("暂无数据");
            }
            if (username != "2017000")
            {

                cth_model = cth_model.Where(m => m.TGReturns_worker_id == username);
            }
            cth_d.Rows.Clear();
            List<string> tg = new List<string>();
            bool pantg = false;
            foreach (var item in cth_model)
            {
                for (int i = 0; i < tg.Count; i++)
                {
                    if (tg[i] == item.TGReturns_id)
                    {
                        pantg = true;
                    }
                }
                if (pantg == false)
                {
                    cth_d.Rows.Add(item.TGReturns_id, item.TGReturns_data);
                    tg.Add(item.TGReturns_id);
                }
                pantg = false;
            }

            tg.Clear();
            
        }
        //仓库退货历史双击选中
        private void cth_d_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try 
            {
                if (e.RowIndex == -1)
                {
                    return;
                }
                
                int row = cth_d.SelectedCells[0].RowIndex;
                string chushiid = Convert.ToString(cth_d.Rows[row].Cells[0].Value);
                cth cth_f = new cth(chushiid, power4);//********************************************************************************************
                cth_f.MdiParent = this;
                cth_f.CloseWindow += new Action(c_sub_CloseWindow); 
                cth_f.Show();
                SetParent((int)cth_f.Handle, (int)this.Handle);
            }
            catch { }
           
        }

        //仓库退货单总和
        private void ct_d_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int length = ct_d.Rows.Count;//数据总行数
            decimal sum = 0;
            for (int i = 0; i < length - 1; i++)
            {
                decimal price = 0, number = 0;
                string snumber = "", sprice = "";
                if (ct_d.Rows[i].Cells[2].Value != null)
                {
                    snumber = Convert.ToString(ct_d.Rows[i].Cells[2].Value);
                    if (Validator.IsNumStr(snumber) == true)
                    {
                        number = Convert.ToDecimal(snumber);
                    }
                    else
                    {
                        number = 0;
                    }
                }
                if (ct_d.Rows[i].Cells[4].Value != null)
                {
                    sprice = Convert.ToString(ct_d.Rows[i].Cells[4].Value);
                    if (Validator.IsNumber(sprice) == true)
                    {
                        price = Convert.ToDecimal(sprice);
                    }
                    else
                    {
                        price = 0;
                    }
                }
                sum += (number * price);
            }
            ct_sum.Text = Convert.ToString(sum);
        }
        //关闭标签页
        private void cth_close_Click(object sender, EventArgs e)
        {
            page_cth.Parent = null;
        }

        //查看库存情况************************************************************************************************************

        //查看预警线
        private void c_yjselect_Click(object sender, EventArgs e)
        {
            int number = 999999;
            if (c_number.Text.Trim() != "")
            {
                if (Validator.IsNumStr(c_number.Text.Trim()) == false)
                {
                    MessageBox.Show("请填入正整数");
                    return;
                }
                number = Convert.ToInt32(c_number.Text.Trim());
            }
            DBCL.DBEntities dbc = new DBCL.DBEntities();
            var model = dbc.warehouses.Select(m => m);
            model = model.Where(m => m.warehouse_number <= number);
            c_d.Rows.Clear();
            foreach (var item in model)
            {
                if (item.warehouse_name == "")
                { continue; }
                c_d.Rows.Add(item.warehouse_id, item.warehouse_name, item.warehouse_price, Convert.ToInt32(item.warehouse_number), item.warehouse_unit,item.warehouse_others);
            }
        }

        //综合查询
        private void c_zhselect_Click(object sender, EventArgs e)
        {
            page_s.Parent = tabControl1;
            tabControl1.SelectTab(page_s);
            if(power6 == false)
            {
                s_delete.Enabled = false;
            }
            DBCL.DBEntities dbc = new DBCL.DBEntities();
            var model = dbc.warehouses.Select(m => m);
            s_d.Rows.Clear();
            foreach (var item in model)
            {
                if (item.warehouse_name == "")
                { continue; }
                s_d.Rows.Add(item.warehouse_id, item.warehouse_name, item.warehouse_price, Convert.ToInt32(item.warehouse_number), item.warehouse_unit,item.warehouse_others);
            }
        }
        //删除
        private void s_delete_Click(object sender, EventArgs e)
        {
            //变量 
            string d = "";
            int length = 0;
            DialogResult dr = MessageBox.Show("是否删除", "删除", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            d = dr.ToString();
           
            if (d == "OK")
            {
               
                //将选中的行添加到动态数值内
                List<int> s_d_del = new List<int>();
                foreach (DataGridViewCell c in s_d.SelectedCells)
                {
                    length = s_d_del.Count;
                    bool panc = false;
                    for (int i = 0; i < length; i++)
                    {
                        if (s_d_del[i] == c.RowIndex)
                        {
                            panc = true;
                        }
                        
                    }
                    if (panc == false)
                    {
                        s_d_del.Add(c.RowIndex);
                        //进行删除
                       
                        int hang=Convert.ToInt32(s_d.Rows[c.RowIndex].Cells[0].Value);
                        var s_model = db.warehouses.FirstOrDefault(m => m.warehouse_id == hang);
                        db.warehouses.Remove(s_model);
                    }
                    try
                    {
                        db.SaveChanges();
                    }
                    catch
                    {
                        MessageBox.Show("删除失败");
                        return;
                    }
                }
                
                s_d.Rows.Clear();
                var model = db.warehouses.Select(m => m);
                foreach (var item in model)
                {
                    if (item.warehouse_name == "")
                    { continue; }
                    s_d.Rows.Add(item.warehouse_id, item.warehouse_name, item.warehouse_price, Convert.ToInt32(item.warehouse_number), item.warehouse_unit,item.warehouse_others);
                }
            }
        }


        void s_sub_CloseWindow()
        {
            DBCL.DBEntities db_s = new DBCL.DBEntities();
            s_d.Rows.Clear();
            //改变父窗体控件内容
         
            var sa_model = db_s.warehouses.Select(m => m);
            foreach (var sa_item in sa_model)
            {
                if (sa_item.warehouse_name == "")
                { continue; }
                s_d.Rows.Add(sa_item.warehouse_id, sa_item.warehouse_name, sa_item.warehouse_price, sa_item.warehouse_number, sa_item.warehouse_unit,sa_item.warehouse_others);
            }

        }
        //查看商品详情
        private void s_d_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1)
                {
                    return;
                }
                
                if (power6 == false)
                {
                    return;
                }

                int row = s_d.SelectedCells[0].RowIndex;
                s_change s_c = new s_change(Convert.ToInt32(s_d.Rows[row].Cells[0].Value), Convert.ToString(s_d.Rows[row].Cells[1].Value), Convert.ToDecimal(s_d.Rows[row].Cells[2].Value), Convert.ToDecimal(s_d.Rows[row].Cells[3].Value), Convert.ToString(s_d.Rows[row].Cells[4].Value), Convert.ToString(s_d.Rows[row].Cells[5].Value));

                s_c.MdiParent = this;
                s_c.CloseWindow += new Action(s_sub_CloseWindow); 
                s_c.Show();
                SetParent((int)s_c.Handle, (int)this.Handle);
            }
            catch { }
            
        }
        //商品查询
        private void s_select_Click(object sender, EventArgs e)
        {
            DBCL.DBEntities db_sa = new DBCL.DBEntities();
            var model = db_sa.warehouses.Select(m => m);
            if (s_id.Text.Trim() != "")
            {
                
                string unit = s_id.Text.Trim();
                model = model.Where(m => m.warehouse_unit.IndexOf(unit)>=0);
            }
            if (s_name.Text.Trim() != "")
            {
                string name = s_name.Text.Trim();
                model = model.Where(m => m.warehouse_name.IndexOf(name) >= 0);
            }

            if (s_numbermin.Text.Trim() != "")
            {
                if (Validator.IsNumStr(s_numbermin.Text.Trim()) == false)
                {
                    MessageBox.Show("请填入正整数");
                    return;
                }
                decimal numbermin = Convert.ToDecimal(s_numbermin.Text.Trim());
                model = model.Where(m => m.warehouse_number >= numbermin);
            }
            if (s_numbermax.Text.Trim() != "")
            {
                if (Validator.IsNumStr(s_numbermax.Text.Trim()) == false)
                {
                    MessageBox.Show("请填入正整数");
                    return;
                }
                decimal numbermax = Convert.ToDecimal(s_numbermax.Text.Trim());
                model = model.Where(m => m.warehouse_number <= numbermax);
            }
            
            if(s_numbermin.Text.Trim() != ""&&s_numbermax.Text.Trim() != "")
            {
                if(Convert.ToInt32(s_numbermax.Text.Trim())<Convert.ToInt32(s_numbermin.Text.Trim()))
                {
                    string n = s_numbermax.Text.Trim();
                    s_numbermax.Text = s_numbermin.Text.Trim();
                    s_numbermin.Text = n;
                }
            }
            model = model.Where(m => m.warehouse_name != "");
            s_d.Rows.Clear();
            try
            {
                foreach (var item in model)
                {
                    s_d.Rows.Add(item.warehouse_id, item.warehouse_name, Convert.ToString(item.warehouse_price), Convert.ToString(item.warehouse_number), item.warehouse_unit, item.warehouse_others);
                }
            }
            catch
            {
                MessageBox.Show("无查询结果");
            }
        }

        
        
        //新增员工************************************************************************************************************

        //添加新员工
        private void ny_save_Click(object sender, EventArgs e)
        {
            string ny_strwordid, ny_strname, ny_strid, ny_strsex, ny_strlevelna, ny_strlevel = "";
            int length = ny_d.Rows.Count;
            //校验不能为空*****************************************************
            for (int i = 0; i < length - 1; i++)
            {
                int k = i + 1;//输出为第几行
                if (ny_d.Rows[i].Cells[0].Value == null)
                {
                    MessageBox.Show("第" + k + "行员工号不能为空");
                    return;
                }
                if (ny_d.Rows[i].Cells[1].Value == null)
                {
                    MessageBox.Show("第" + k + "行员工姓名不能为空");
                    return;
                }
                if (ny_d.Rows[i].Cells[2].Value == null)
                {
                    MessageBox.Show("第" + k + "行员工性别不能为空");
                    return;
                }
                if (ny_d.Rows[i].Cells[3].Value == null)
                {
                    MessageBox.Show("第" + k + "行员工身份证号不能为空");
                    return;
                }

                if (ny_d.Rows[i].Cells[4].Value == null)
                {
                    MessageBox.Show("第" + k + "行员工权限不能为空");
                    return;
                }
            }
            //判断工号和身份证号是否重复***************************************
            for (int i = 0; i < length - 1; i++)
            {
                ny_strwordid = (string)ny_d.Rows[i].Cells[0].Value;
                ny_strid = (string)ny_d.Rows[i].Cells[3].Value;
                int k = i + 1;//输出为第几行
                if (db.users.Any(m => m.users_workid == ny_strwordid) == true)
                {
                    MessageBox.Show("第" + k + "行员工号重复!");
                    return;
                }
                if (db.users.Any(m => m.users_id == ny_strid) == true)
                {
                    MessageBox.Show("第" + k + "行员工身份证号重复!");
                    return;
                }
            }
            
            for (int i = 0; i < length - 1; i++)
            {
                DBCL.user model = new DBCL.user();
                ny_strwordid = (string)ny_d.Rows[i].Cells[0].Value;
                ny_strname = (string)ny_d.Rows[i].Cells[1].Value;
                ny_strsex = (string)ny_d.Rows[i].Cells[2].Value;
                ny_strid = (string)ny_d.Rows[i].Cells[3].Value;
                ny_strlevelna = (string)ny_d.Rows[i].Cells[4].Value;
                var p_model = db.permissions.Select(m => m);
                p_model = p_model.Where(m => m.permission_na.IndexOf(ny_strlevelna) >= 0);
                foreach (var item in p_model)
                {
                    ny_strlevel = item.permission_code;
                }

                model.users_workid = ny_strwordid;
                model.users_name = ny_strname;
                model.users_sex = ny_strsex;
                model.users_id = ny_strid;
                model.users_level = ny_strlevel;
                model.users_password = ny_strwordid;
                try
                {
                    db.users.Add(model);
                    db.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("添加失败!");
                    return;
                }
            }
            
            MessageBox.Show("添加成功!");
            ny_d.Rows.Clear();
        }

        private void ny_gave_Click(object sender, EventArgs e)
        {
            page_ny.Parent = null;
        }

        //员工信息************************************************************************************************************

        //搜索
        private void y_select_Click(object sender, EventArgs e)
        {
            string workid, name, id, sex = "", level = "";
            //赋值*************************************************************
            workid = y_t_workid.Text;
            name = y_t_name.Text;
            id = y_t_id.Text;
            if (string.IsNullOrEmpty(y_co_sex.Text) == false)
            {
                sex = (string)y_co_sex.SelectedItem.ToString();
            }
            if (string.IsNullOrEmpty(y_co_level.Text) == false)
            {
                level = (string)y_co_level.SelectedItem.ToString();
            }
            //数据库查询**********************************************************
            DBCL.DBEntities dbc = new DBCL.DBEntities();
            var model = dbc.users.Select(m => m);
            if (workid != "")
            {
                model = model.Where(m => m.users_workid.IndexOf(workid) >= 0);
            }
            if (name != "")
            {
                model = model.Where(m => m.users_name.IndexOf(name) >= 0);
            }
            if (id != "")
            {
                model = model.Where(m => m.users_id.IndexOf(id) >= 0);
            }
            if (sex != "")
            {
                model = model.Where(m => m.users_sex.IndexOf(sex) >= 0);
            }
            if (level != "")
            {
                string code = getpcode(level);
                model = model.Where(m => m.users_level.IndexOf(code) >= 0);
            }
            y_d.Rows.Clear();
            foreach (var item in model)
            {
                string code = "", pname = "";
                code = (string)item.users_level;
                pname = getpname(code);
                y_d.Rows.Add((string)item.users_workid, (string)item.users_name, (string)item.users_sex, (string)item.users_id, pname);
            }
        }

        //保存
        private void y_save_Click(object sender, EventArgs e)
        {
            while (y_array.Count != 0)
            {
                int arrayid = y_array.Last();

                // MessageBox.Show("" + arrayid);
                string workid = "", name = "", sex = "", id = "", level = "";
                int k = arrayid + 1;//输出为第几行
                if (y_d.Rows[arrayid].Cells[0].Value == null)
                {
                    MessageBox.Show("第" + k + "行员工号不能为空");
                    return;
                }
                if (y_d.Rows[arrayid].Cells[1].Value == null)
                {
                    MessageBox.Show("第" + k + "行员工姓名不能为空");
                    return;
                }
                if (y_d.Rows[arrayid].Cells[2].Value == null)
                {
                    MessageBox.Show("第" + k + "行员工性别不能为空");
                    return;
                }
                if (y_d.Rows[arrayid].Cells[3].Value == null)
                {
                    MessageBox.Show("第" + k + "行员工身份证号不能为空");
                    return;
                }

                if (y_d.Rows[arrayid].Cells[4].Value == null)
                {
                    MessageBox.Show("第" + k + "行员工权限不能为空");
                    return;
                }
                workid = (string)y_d.Rows[arrayid].Cells[0].Value;
                id = (string)y_d.Rows[arrayid].Cells[3].Value;
                string firstid = "";
                var id_model = db.users.Select(m => m);
                id_model = id_model.Where(m => m.users_workid == workid);
                foreach (var item in id_model)
                {
                    firstid = item.users_id;
                }
                if (db.users.Any(m => m.users_id == firstid) != true)
                {
                    // MessageBox.Show(firstid +" 变后 "+id );
                    if (db.users.Any(m => m.users_id == id) == true)
                    {
                        MessageBox.Show("第" + k + "行员工身份证号重复!");
                        return;
                    }
                }


                var model = db.users.FirstOrDefault(m => m.users_workid == workid);
                if (model == null)
                {
                    MessageBox.Show("该角色在数据库中不存在!");
                    return;
                }
                name = y_d.Rows[arrayid].Cells[1].Value.ToString();
                sex = y_d.Rows[arrayid].Cells[2].Value.ToString();
                id = y_d.Rows[arrayid].Cells[3].Value.ToString();
                level = y_d.Rows[arrayid].Cells[4].Value.ToString();
                model.users_name = name;
                model.users_sex = sex;
                model.users_id = id;
                model.users_level = getpcode(level);
                //MessageBox.Show(name + " sex" + sex + "id" + id + "lev" + getpcode(level));

                try
                {

                    db.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("编辑失败!");
                    return;
                }

                y_array.Remove(y_array.Last());
            }
            MessageBox.Show("保存成功");
        }

        //删除
        private void y_delete_Click(object sender, EventArgs e)
        {
            string d = "", id = "";
            int length = 0;
            DialogResult dr = MessageBox.Show("是否删除", "删除", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            d = dr.ToString();
            if (d == "OK")
            {
                //将选中的行添加到动态数值内
                List<int> y_darray = new List<int>();
                foreach (DataGridViewCell c in y_d.SelectedCells)
                {
                    length = y_darray.Count;
                    bool panc = false;
                    for (int i = 0; i < length; i++)
                    {
                        if (y_darray[i] == c.RowIndex)
                        {
                            panc = true;
                        }
                    }
                    if (panc == false)
                    {
                        y_darray.Add(c.RowIndex);

                        id = Convert.ToString(y_d.Rows[c.RowIndex].Cells[0].Value);
                        var model = db.users.FirstOrDefault(m => m.users_workid == id);
                        if (model == null)
                        {
                            MessageBox.Show("该角色在数据库中不存在!");
                            return;
                        }
                        db.users.Remove(model);

                    }
                }
                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("删除失败!");
                    return;
                }
                y_d.Rows.Clear();

                var y_rsmodel = db.users.Select(m => m);
                foreach (var item in y_rsmodel)
                {
                    string code = "", pname = "";
                    code = (string)item.users_level;
                    pname = getpname(code);
                    y_d.Rows.Add((string)item.users_workid, (string)item.users_name, (string)item.users_sex, (string)item.users_id, pname);

                }

                MessageBox.Show("删除成功!");
               
            }
        }

        //员工信息的哪行被修改
        private void y_d_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            bool pan = false;

            for (int i = 0; i < y_array.Count; i++)
            {
                if (y_array[i] == y_d.SelectedCells[0].RowIndex)
                { pan = true; }
            }
            if (pan == false)
            {
                y_array.Add(y_d.SelectedCells[0].RowIndex);
            }
        }
        //角色管理************************************************************************************************************

        //保存修改
        private void role_save_Click(object sender, EventArgs e)
        {
            string name, code = "";
            char[] codea = { '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0' };
            codea = role_get();
            for (int i = 0; i < codea.Length; i++)
            {
                code += codea[i];
            }
            DBCL.permission model = new DBCL.permission();
            model.permission_code = code;

            if (role_select.SelectedItem != null)
            {
                name = role_select.SelectedItem.ToString();//获取所选角色的名字
                var mode = db.permissions.FirstOrDefault(m => m.permission_na == name);
                if (mode == null)
                {
                    MessageBox.Show("该角色在数据库中不存在!");
                    return;
                }
                mode.permission_code = code;
                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("编辑失败!");
                    return;
                }
                MessageBox.Show("修改成功!");
            }
            else
            {
                MessageBox.Show("输入角色名称");
                return;
            }
        }

        //删除角色
        private void role_delete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("是否删除", "删除", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            string d = dr.ToString();
            if (d == "OK")
            {
                string name = "";
                if (role_select.SelectedItem != null)
                {
                    name = role_select.SelectedItem.ToString();//获取所选角色的名字
                    var mode = db.permissions.FirstOrDefault(m => m.permission_na == name);
                    if (mode == null)
                    {
                        MessageBox.Show("该角色在数据库中不存在!");
                        return;
                    }
                    db.permissions.Remove(mode);
                    try
                    {
                        db.SaveChanges();
                        role_select.Items.Remove(name);

                    }
                    catch
                    {
                        MessageBox.Show("编辑失败!");
                        return;
                    }
                    MessageBox.Show("修改成功!");
                    role_setnull();
                }
            }
            else
            { return; }

        }

        //新增角色
        private void role_add_Click(object sender, EventArgs e)
        {
            page_newlevel.Parent = tabControl1;
            tabControl1.SelectTab(page_newlevel);
        }

        //新增角色************************************************************************************************************

        //新增角色
        private void n_yes_Click(object sender, EventArgs e)
        {
            string strrole_new, name, code = "";
            char[] codea = { '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0' };
            strrole_new = role_new.Text.Trim();
            codea = nrole_get();
            for (int i = 0; i < codea.Length; i++)
            {
                code += codea[i];
            }
            DBCL.permission model = new DBCL.permission();
            model.permission_code = code;
            name = role_new.Text.Trim();
            model.permission_na = name;
            try
            {
                db.permissions.Add(model);
                db.SaveChanges();
            }
            catch
            {
                MessageBox.Show("添加失败!");
                return;
            }
            role_select.Items.Add(name);//将其添加至combobox
            MessageBox.Show("添加成功!");
            role_new.Text = "";
            nrole_setnull();
        }
        //角色管理下拉触发
        private void role_select_SelectedIndexChanged(object sender, EventArgs e)
        {

            string strrole_select, code = "000000000000000000000000";
            char[] codea;
            strrole_select = (string)role_select.SelectedItem.ToString();
            var model = db.permissions.Select(m => m);
            model = model.Select(m => m);
            model = model.Where(m => m.permission_na.IndexOf(strrole_select) >= 0);
            foreach (var item in model)
            {
                code = item.permission_code;
            }
            codea = code.ToCharArray();
            role_set(codea);
        }

        
        //关闭此页
        private void n_no_Click(object sender, EventArgs e)
        {
            page_newlevel.Parent = null;
        }

        private void s_close_Click(object sender, EventArgs e)
        {
            page_s.Parent = null;
        }

        private void ct_resert_Click(object sender, EventArgs e)
        {
            ct_id.Text = ct_getid();
            ct_workid.Text = username;
            ct_d.Rows.Clear();
            ct_data.Value = DateTime.Now;
            ct_source.Text = "";
            ct_sum.Text = "";
        }

    }
    
}
