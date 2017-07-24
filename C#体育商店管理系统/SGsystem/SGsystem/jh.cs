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

using System.Runtime.InteropServices;
namespace SGsystem
{
    public partial class jh : Form
    {
        string chushi_id,chushi_fid;
        bool power;
        bool panxiu = false;
        private DBCL.DBEntities db = new DBCL.DBEntities();
        public jh()
        {
            InitializeComponent();
        }
        public jh(string purchase_id,bool power)
        {
            chushi_id = purchase_id;
            this.power = power;
            
            InitializeComponent();

        }
        //父窗体控件挡住子窗体
        [DllImport("user32")]
        public static extern int SetParent(int hWndChild, int hWndNewParent);

        public event Action CloseWindow;
        private string set_housan(int number1)
        {
            string s = "";
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
        private string pay_getid()
        {
            string xuhao = "", id = "", san = "", housan = "";
            int hs = 0, h = 0;
            string xh3 = "";
            DBCL.DBEntities dbc = new DBCL.DBEntities();
            var model = dbc.pays.Select(m => m);
            try
            {
                foreach (var item in model)
                {
                    xuhao = item.pay_id;
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
            id = "FK" + data + san + xh3;
            return id;
        }
        private void jh_Load(object sender, EventArgs e)
        {
            
            var model = db.purchases.FirstOrDefault(m => m.purchase_id == chushi_id);
            j_id.Text = chushi_id;
            j_workid.Text = model.purchase_worker_id;
            j_data.Text = model.purchase_data;
            j_realsum.Text = Convert.ToString(model.purchase_real_price);
            j_sum.Text = Convert.ToString(model.purchase_whole_price);
            j_source.Text = model.purchase_source;

            var d_model = db.purchases.Select(m => m);
            d_model = d_model.Where(m => m.purchase_id == chushi_id);
            foreach (var item in d_model)
            {
                j_d.Rows.Add(item.id, item.purchase_name, Convert.ToInt32(item.purchase_number), item.purchase_unit, item.purchase_price, item.purchase_others,item.purchase_date,item.purchase_size,item.purchase_code);
            }

            if(power == false)
            {
                j_submit.Enabled = false;
                jh_delete.Enabled = false;
            }
        }
        //仓库的更改
        private bool c_setnumber(string fuhao, string name, decimal number)
        {
            var model = db.warehouses.FirstOrDefault(m => m.warehouse_name == name);
            try
            {
                if (model == null)
                {
                    MessageBox.Show(name+"在数据库中不存在!");
                    return false;
                }
                if (fuhao == "-")
                {
                    model.warehouse_number -= number;
                    return true;
                }
                else if (fuhao == "+")
                {
                    model.warehouse_number += number;
                    return true;
                }
                else
                {
                    MessageBox.Show("仓库修改失败！");
                    return false;
                }
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
                xh = 1;
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



        //删除付款单***********************************************************
        private bool pay_delete(string pay_id)
        {

            var model = db.pays.FirstOrDefault(m => m.pay_id == pay_id);
            try
            {
                
                if (model.pay_id != null)
                { chushi_fid = model.pay_id; }
                else { chushi_fid = pay_getid(); }
                db.pays.Remove(model);
            }
            catch
            {
                MessageBox.Show("删除付款单失败");
                return false;

            }
            return true;
        }

        //总资金的加减*********************************************************
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
            }
            catch { return false; }

        }

        //进货单获取总价*******************************************************

        //删除进货单
        private bool j_delete()
        {
            //删除进货单
            string id = "", d = "OK";
            id = chushi_id;
            var model = db.purchases.Select(m => m);
            model = model.Where(m => m.purchase_id == id);
            try
            {
                var pay_item = db.pays.FirstOrDefault(m => m.pay_purchase_id == id);
                pay_delete(pay_item.pay_id);
               
            }
            catch
            {
                DialogResult drs = MessageBox.Show("未找到相应的付款单是否继续", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                d = drs.ToString();
                if (d != "OK")
                { return false; }
            }
            try 
            {
                var p_item = db.purchases.FirstOrDefault(m => m.purchase_id == id);
                z_setmoney("+", Convert.ToDecimal(p_item.purchase_real_price));
            }
            catch { }
            
            foreach (var item in model)
            {
                if (item == null)
                {
                    MessageBox.Show("该角色在数据库中不存在!");
                    return false;
                }
                //删除对应付款单
                try
                {
                   
                    try
                    {
                        //仓库修改 减去增加的
                        c_setnumber("-", item.purchase_name, Convert.ToDecimal(item.purchase_number));
                    }
                    catch
                    {
                        DialogResult drs = MessageBox.Show("仓库未找到相应的物品是否继续", "shangchu", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        d = drs.ToString();
                        if (d != "OK")
                        { return false; }
                    }
                    db.purchases.Remove(item);
                }
                catch
                {
                    MessageBox.Show("删除数据失败");
                    return false;
                }
            }
            try
            {
                db.SaveChanges();
            }
            catch
            {
                MessageBox.Show("删除存入失败");
                return false;

            }
            return true;
        }

        
        //提交表单*************************************************************
        private bool jh_tijiao()
        {
            //变量*************************************************************
            int length = j_d.Rows.Count;
            int k = 0;
            string id = "", workid = "", data = "", sum = "", realsum = "", source = "";
            string name = "", unit = "", price = "", other = "", date = "", size = "", code = "";
            decimal dnumber = 0, dprice = 0, whole_price = 0, real_price = 0;
            //校验
            if (length == 1)
            {
                MessageBox.Show("请输入数据");
                return false;
            }
            for (int i = 0; i < length - 1; i++)
            {
                if (j_d.Rows[i].Cells[1].Value == null  && j_d.Rows[i].Cells[3].Value == null)
                {
                    continue;
                }
                k = i + 1;
                //判断名称不为空，数据库是否存在，如存在则继续，不存在
                if (j_d.Rows[i].Cells[1].Value == null)
                {
                    MessageBox.Show("第" + k + "行名称不能为空");
                    return false;
                }

                //判断数量是否为空，是否为正整数
                if (j_d.Rows[i].Cells[2].Value == null)
                {
                    MessageBox.Show("第" + k + "行数量不能为空");
                    return false;
                }

                if (Validator.IsNumStr(Convert.ToString(j_d.Rows[i].Cells[2].Value)) == false)
                {
                    MessageBox.Show("第" + k + "行数量不是正整数或超出10位");
                    return false;
                }
                //判断单位是否为空，是否与数据库该商品单位相同，不相同则返回报错
                if (j_d.Rows[i].Cells[3].Value == null)
                {
                    MessageBox.Show("第" + k + "行单位不能为空");
                    return false;
                }
                name = Convert.ToString(j_d.Rows[i].Cells[1].Value);
                unit = Convert.ToString(j_d.Rows[i].Cells[3].Value);
                var w_model = db.warehouses.Select(m => m);
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
                        return false;
                    }
                }
                //判断单价是否为空，是否为保留两位小数，或整数
                if (j_d.Rows[i].Cells[4].Value == null)
                {
                    MessageBox.Show("第" + k + "行单价不能为空");
                    return false;
                }
                price = Convert.ToString(j_d.Rows[i].Cells[4].Value);
                if (Validator.IsNumber2(price) == false)
                {
                    MessageBox.Show("第" + k + "行单价必须是正整数或两位以内小数");
                    return false;
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
                return false;
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
            for (int i = 0; i < length - 1; i++)
            {
                if (j_d.Rows[i].Cells[1].Value == null && j_d.Rows[i].Cells[3].Value == null)
                {
                    continue;
                }
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
                other = Convert.ToString(j_d.Rows[i].Cells[5].Value);
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
                try
                {
                    db.purchases.Add(model);
                    //修改仓库库存*****************************************************
                    //判断仓库是否存在该物品 如果存在更新数据，如果不存在就插入数据
                    if (db.warehouses.Any(m => m.warehouse_name == name) == true)
                    {
                        //更新仓库数量
                        if (c_setnumber("+", name, dnumber) == false) { return false; };
                    }
                    else
                    {
                        //插入新物品
                        if (w_setnew(name, dnumber, unit) == false) { return false; };
                    }
                    try { db.SaveChanges(); }
                    catch
                    {
                        MessageBox.Show("更新失败!");
                        return false;
                    }
                }
                catch
                {
                    MessageBox.Show("更新失败!");
                    return false;
                }
            }
            try
            { //增加付款单
                if (pay_setnew(id, workid, data, real_price) == false) { return false; };
                //修改资金总额
                if (z_setmoney("-", real_price) == false) { return false; };
                db.SaveChanges();
            }
            catch
            {
                MessageBox.Show("更新失败!");
                return false;
            }
           

            return true;
        }
        //新增付款单***********************************************************
        private bool pay_setnew(string purchase_id, string work_id, string data, decimal number)
        {
            DBCL.pay model = new DBCL.pay();
            model.pay_id = chushi_fid;
            model.pay_purchase_id = purchase_id;
            model.pay_worker_id = work_id;
            model.pay_data = data;
            model.pay_number = number;
            model.pay_type = "默认";
            try
            {
                db.pays.Add(model);
            }
            catch
            {
                MessageBox.Show("添加付款单失败!");
                return false;
            }
            return true;
        }

        private void j_submit_Click(object sender, EventArgs e)
        {
            try 
            {
                //变量*************************************************************
                int length = j_d.Rows.Count;
                int k = 0;
                string name = "", unit = "", price = "";

                //校验
                if (length == 1)
                {
                    MessageBox.Show("请输入数据");
                    return;
                }
                for (int i = 0; i < length - 1; i++)
                {
                    if (j_d.Rows[i].Cells[1].Value == null && j_d.Rows[i].Cells[3].Value == null)
                    {
                        continue;
                    }
                    k = i + 1;
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
                    var w_model = db.warehouses.Select(m => m);
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
            }
            catch { return; }
            try
            {
                if (j_delete() == false)
                {
                    return;
                }
                if (jh_tijiao() == false)
                {
                    return;
                }
                panxiu = false;
            }
            catch
            {
                MessageBox.Show("更新失败");
            }
            MessageBox.Show("更新成功");
            try
            {
                //************************************************************************************************************************************
                Action handler = CloseWindow;
                if (handler != null) handler();
                this.Close();
            }
            catch { }
        }

        private void j_d_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            panxiu = true;
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

        private void jh_delete_Click(object sender, EventArgs e)
        {
            if (panxiu == true)
            {
                string d = "";
                DialogResult dr = MessageBox.Show("是否保存", "shangchu", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                d = dr.ToString();
                if (d == "OK")
                {
                    try
                    {
                        if (j_delete() == false)
                        {
                            return;
                        }
                        if (jh_tijiao() == false)
                        {
                            return;
                        }
                        panxiu = false;
                    }
                    catch
                    {
                        MessageBox.Show("更新失败");
                    }
                }
                else 
                {
                    
                }
            }
            string a = "";
            DialogResult b = MessageBox.Show("是否删除", "shangchu", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            a = b.ToString();
            if (a == "OK")
            {
                int length = 0;
                List<int> j_d_del = new List<int>();
                foreach (DataGridViewCell c in j_d.SelectedCells)
                {
                    length = j_d_del.Count;
                    bool panc = false;
                    for (int i = 0; i < length; i++)
                    {
                        if (j_d_del[i] == c.RowIndex)
                        {
                            panc = true;
                        }
                    }
                    if (panc == false)
                    {
                        j_d_del.Add(c.RowIndex);
                        for (int j = 0; j < 6; j++)
                        {
                            j_d.Rows[c.RowIndex].Cells[j].Value = null;
                        }
                    }
                }
                bool panquankong = true;
                foreach (DataGridViewRow c in j_d.Rows)
                {
                    if (c.Cells[0].Value != null)
                    {
                        panquankong = false;
                    }
                }

                if (panquankong == true)
                {
                    try
                    {
                        if (j_delete() == false)
                        {
                            return;
                        }
                        panxiu = false;
                    }

                    catch
                    {
                        MessageBox.Show("更新失败");
                    }
                }
                else
                {
                    try
                    {
                        if (j_delete() == false)
                        {
                            return;
                        }
                        if (jh_tijiao() == false)
                        {
                            return;
                        }
                        panxiu = false;
                    }
                    catch
                    {
                        MessageBox.Show("更新失败");
                    }
                }
            }
            else
            {
                return;
            }
            try
            {
                Action handler = CloseWindow;
                if (handler != null) handler();
                this.Close();
            }
            catch { }
        }

        private void j_data_CloseUp(object sender, EventArgs e)
        {
            panxiu = true;
        }

        private void j_realsum_KeyPress(object sender, KeyPressEventArgs e)
        {
            panxiu = true;
        }

        private void j_source_KeyPress(object sender, KeyPressEventArgs e)
        {
            panxiu = true;
        }

        private void j_reset_Click(object sender, EventArgs e)
        {
            Action handler = CloseWindow;
            if (handler != null) handler();
            this.Close();
        }
    }
}
