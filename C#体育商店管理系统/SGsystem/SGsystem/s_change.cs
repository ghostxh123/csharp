using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SGsystem.share;

namespace SGsystem
{
    public partial class s_change : Form
    {
        private DBCL.DBEntities db = new DBCL.DBEntities();
        int id;
        string sname, sunit, sother;

        decimal dprice, dnumber;
        public s_change()
        {
            InitializeComponent();
        }
        public s_change(int oid, string oname, decimal oprice, decimal onumber, string ounit, string oother)
        {
            InitializeComponent();
            id = oid;
            sname = oname;
            dprice = oprice;
            dnumber = onumber;
            sunit = ounit;
            sother = oother;
        }

        private void s_change_Load(object sender, EventArgs e)
        {

            name.Text = sname;
            price.Text = Convert.ToString(dprice);
            number.Text = Convert.ToString(Convert.ToInt32(dnumber));
            unit.Text = sunit;
            other.Text = sother;
        }
        //退出事件
        public event Action CloseWindow;

        private void save_Click(object sender, EventArgs e)
        {
           
            if (name.Text.Trim() == "")
            {
                MessageBox.Show("名称不能为空");
                return;
            }

            //判断数量是否为空，是否为正整数
            if (number.Text.Trim() == "")
            {
                MessageBox.Show("数量不能为空");
                return;
            }

            if (Validator.IsNumStr(number.Text.Trim()) == false)
            {
                MessageBox.Show("数量不是正整数或超出10位");
                return;
            }
            //判断单位是否为空，是否与数据库该商品单位相同，不相同则返回报错
            if (unit.Text.Trim() == "")
            {
                MessageBox.Show("单位不能为空");
                return;
            }
            string dname, sdunit;
            dname = name.Text.Trim();
            sdunit = unit.Text.Trim();
            try
            {
                var w_model = db.warehouses.FirstOrDefault(m => m.warehouse_name == dname);
                string dunit = "";
                dunit = w_model.warehouse_unit;
                if (dunit != "")
                {
                    if (dunit != sunit)
                    {
                        MessageBox.Show("单位错误应为:" + dunit);
                        return;
                    }
                }
            }
            catch { }
            //判断单价是否为空，是否为保留两位小数，或整数
            if (price.Text.Trim() =="")
            {
                MessageBox.Show("单价不能为空");
                return;
            }
            string sdprice = price.Text.Trim();
            if (Validator.IsNumber2(sdprice) == false)
            {
                MessageBox.Show("单价必须是正整数或两位以内小数");
                return;
            }
            sname = name.Text.Trim();
            dnumber = Convert.ToDecimal(number.Text.Trim());
            dprice = Convert.ToDecimal(price.Text.Trim());
            sunit = unit.Text.Trim();
            sother = other.Text.Trim();
            try
            {
                var model = db.warehouses.FirstOrDefault(m => m.warehouse_id == id);
                model.warehouse_name = sname;
                model.warehouse_number = dnumber;
                model.warehouse_price = dprice;
                model.warehouse_unit = sunit;
                model.warehouse_others = sother;
                
            }
            catch
            {
                MessageBox.Show("更改失败");
                return;
            }
            try 
            { 
                db.SaveChanges(); 
            }
            catch
            {
                MessageBox.Show("更改失败");
                return;
            }
            MessageBox.Show("更改成功");
            
            Action handler = CloseWindow;
            if (handler != null) handler();
            this.Close();
            
        }

        private void s_close_Click(object sender, EventArgs e)
        {
            Action handler = CloseWindow;
            if (handler != null) handler();
            this.Close();
        }
    }
}