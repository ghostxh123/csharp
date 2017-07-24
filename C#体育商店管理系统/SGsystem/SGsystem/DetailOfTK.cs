using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Text.RegularExpressions;

namespace SGsystem
{
    public partial class DetailOfTK : Form
    {
        //变量******************************************************************************************************
        private DBCL.DBEntities db = new DBCL.DBEntities();
        string username = "";
        string current_TK_id = "";
        bool ifChanged = false;
        bool power;
        
        //函数******************************************************************************************************
        public DetailOfTK()
        {
            InitializeComponent();
        }

        public DetailOfTK(string username,string current_TK_id,bool power)
        {
            InitializeComponent();
            this.username = username;
            this.current_TK_id = current_TK_id;
            this.power = power;
        }

        //界面加载
        private void DetailOfTK_Load(object sender, EventArgs e)
        {
            if(power == false)
            {
                button2.Enabled = false;
                button3.Enabled = false;
            }

            textBox4.Text = username;
            dataGridView1.Rows.Clear();

            //给datagridview赋值
            var model = db.TKReturns.Select(m => m);
            model = model.Where(m => m.TKReturns_id == current_TK_id);
            int line = 0;

            foreach (var item in model)
            {
                textBox1.Text = item.TKReturns_id;
                textBox2.Text = item.TKReturns_salesid;
                textBox3.Text = item.TKReturns_data;

                dataGridView1.Rows.Add();
                dataGridView1.Rows[line].Cells[0].Value = item.TKReturns_name;
                dataGridView1.Rows[line].Cells[1].Value = item.TKReturns_number;
                dataGridView1.Rows[line].Cells[2].Value = item.TKReturns_price;
                dataGridView1.Rows[line].Cells[3].Value = item.TKReturns_unit;
                dataGridView1.Rows[line].Cells[4].Value = item.TKReturns_others;

                line++;
            }

            ifChanged = false;
        }

        //关闭界面
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //判断数据是否发生改变
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            ifChanged = true;
        }
        
        //删除数据
        private void button2_Click(object sender, EventArgs e)
        {
            //判断表单是否已更新，若未更新，执行删除，否则，执行更新
            if (ifChanged == true)
            {
                if (MessageBox.Show("表单已修改，是否要更新？（保存后才可删除）", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                {
                    return;
                }
                else
                {
                    //删除原有表单
                    string TKReturns_id = textBox1.Text.ToString();

                    var model = db.TKReturns.Select(m => m);
                    model = model.Where(m => m.TKReturns_id == TKReturns_id);

                    foreach (var item in model)
                    {
                        //减少仓库中的货物数量
                        var model2 = db.warehouses.FirstOrDefault(m => m.warehouse_name == item.TKReturns_name);
                        model2.warehouse_number = model2.warehouse_number - Convert.ToDecimal(item.TKReturns_number);

                        //增加总资金
                        var model3 = db.funds.FirstOrDefault(m => m.funds_id == 1);
                        model3.funds_number = model3.funds_number - Convert.ToDecimal(item.TKReturns_number) * Convert.ToDecimal(item.TKReturns_price);

                        //对应收入单销售额减少
                        var model4 = db.pays.FirstOrDefault(m => m.pay_purchase_id == TKReturns_id);
                        model4.pay_number = model4.pay_number - Convert.ToDecimal(item.TKReturns_number) * Convert.ToDecimal(item.TKReturns_price);

                        //删除销售单中对应单号的所有数据

                        db.TKReturns.Remove(item);
                    }

                    //验证新单格式
                    if (textBox2.Text.Trim() == "")
                    {
                        MessageBox.Show("销售单号不能为空！");
                        return;
                    }

                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j < dataGridView1.Columns.Count - 1; j++)
                        {
                            if (dataGridView1.Rows[i].Cells[j].Value == null)
                            {
                                MessageBox.Show("第" + (i + 1) + "行" + (j + 1) + "列处不能为空！");
                                return;
                            }
                        }
                    }

                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        if (!Regex.IsMatch(dataGridView1.Rows[i].Cells[1].Value.ToString(), @"^[+-]?(?!0\d)\d+(\.[0-9]+)?$"))
                        {
                            MessageBox.Show("数量必须为数字！");
                            return;
                        }

                        if (!Regex.IsMatch(dataGridView1.Rows[i].Cells[2].Value.ToString(), @"^[+-]?(?!0\d)\d+(\.[0-9]+)?$"))
                        {
                            MessageBox.Show("单价必须为数字！");
                            return;
                        }

                        if (!Regex.IsMatch(dataGridView1.Rows[i].Cells[3].Value.ToString(), "^[\u4e00-\u9fa5]$"))
                        {
                            MessageBox.Show("单位必须为汉字！");
                            return;
                        }
                    }

                    //验证是否有此退货单
                    var model5 = db.TKReturns.Where(m => m.TKReturns_id == textBox1.Text.Trim());

                    if (model5.Count() == 0)
                    {
                        MessageBox.Show("此销售单不存在，请重新输入！");
                        return;
                    }

                    //验证表单内容是否正确
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        bool flag = false;

                        foreach (var item in model5)
                        {
                            if (dataGridView1.Rows[i].Cells[0].Value.ToString() == item.TKReturns_name)
                            {
                                flag = true;
                                break;
                            }
                        }

                        if (flag == false)
                        {
                            MessageBox.Show("退货表单信息有误！");
                            return;
                        }
                    }

                    //写入数据库
                    DBCL.TKReturn model6 = new DBCL.TKReturn();
                    string TKReturns_salesid = "";
                    string TKReturns_worker_id = "";
                    string TKReturns_data = "";
                    string TKReturns_name = "";
                    string TKReturns_unit = "";
                    string TKReturns_price = "";
                    string TKReturns_number = "";
                    string TKReturns_others = "";

                    TKReturns_salesid = textBox2.Text.Trim();
                    TKReturns_worker_id = username;
                    TKReturns_data = textBox3.Text.Trim();

                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        TKReturns_name = dataGridView1.Rows[i].Cells[0].Value.ToString().Trim();
                        TKReturns_number = dataGridView1.Rows[i].Cells[1].Value.ToString().Trim();
                        TKReturns_price = dataGridView1.Rows[i].Cells[2].Value.ToString().Trim();
                        TKReturns_unit = dataGridView1.Rows[i].Cells[3].Value.ToString().Trim();

                        if (dataGridView1.Rows[i].Cells[4].Value == null)
                        {
                            TKReturns_others = "";
                        }
                        else
                        {
                            TKReturns_others = dataGridView1.Rows[i].Cells[4].Value.ToString().Trim();
                        }

                        model6.TKReturns_id = TKReturns_id;
                        model6.TKReturns_salesid = TKReturns_salesid;
                        model6.TKReturns_worker_id = TKReturns_worker_id;
                        model6.TKReturns_data = TKReturns_data;
                        model6.TKReturns_name = TKReturns_name;
                        model6.TKReturns_number = Convert.ToDecimal(TKReturns_number);
                        model6.TKReturns_price = Convert.ToDecimal(TKReturns_price);
                        model6.TKReturns_unit = TKReturns_unit;
                        model6.TKReturns_others = TKReturns_others;

                        //保存到客户进货单
                        try
                        {
                            db.TKReturns.Add(model6);
                            db.SaveChanges();
                        }
                        catch
                        {
                            MessageBox.Show("失败！");
                            return;
                        }

                        //更改仓库货物数量
                        var model7 = db.warehouses.FirstOrDefault(m => m.warehouse_name == TKReturns_name);
                        model7.warehouse_number = model7.warehouse_number + Convert.ToDecimal(TKReturns_number);
                    }

                    //修改支出表
                    var model8 = db.pays.FirstOrDefault(m => m.pay_purchase_id == TKReturns_id);
                    double money = 0;

                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        money = money + Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value.ToString());
                    }

                    model8.pay_number = Convert.ToDecimal(money);


                    //修改总金额
                    var model9 = db.funds.FirstOrDefault(m => m.funds_id == 1);
                    model9.funds_number = model9.funds_number - Convert.ToDecimal(money);

                    try
                    {
                        db.SaveChanges();
                    }
                    catch
                    {
                        MessageBox.Show("更新失败！");
                        return;
                    }

                    MessageBox.Show("更新成功！");
                    //********************************************************************************************
                    //显示新表
                    textBox4.Text = username;
                    dataGridView1.Rows.Clear();

                    var model10 = db.TKReturns.Select(m => m);
                    model10 = model10.Where(m => m.TKReturns_id == current_TK_id);
                    int line = 0;

                    foreach (var item in model)
                    {
                        textBox1.Text = item.TKReturns_id;
                        textBox2.Text = item.TKReturns_salesid;
                        textBox3.Text = item.TKReturns_data;

                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[line].Cells[0].Value = item.TKReturns_name;
                        dataGridView1.Rows[line].Cells[1].Value = item.TKReturns_number;
                        dataGridView1.Rows[line].Cells[2].Value = item.TKReturns_price;
                        dataGridView1.Rows[line].Cells[3].Value = item.TKReturns_unit;
                        dataGridView1.Rows[line].Cells[4].Value = item.TKReturns_others;

                        line++;
                    }

                    ifChanged = false;
                }
            }
            else
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    if (MessageBox.Show("确认删除？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                    {
                        return;
                    }

                    string TKReturns_id = textBox1.Text.ToString();

                    foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                    {
                        string TKReturns_name = r.Cells[0].Value.ToString();
                        string TKReturns_number = r.Cells[1].Value.ToString();
                        string TKReturns_price = r.Cells[2].Value.ToString();

                        //删除销售单中对应的数据
                        var model = db.TKReturns.Select(m => m);
                        model = model.Where(m => m.TKReturns_id == TKReturns_id);
                        model = model.Where(m => m.TKReturns_name == TKReturns_name);

                        foreach (var item in model)
                        {
                            db.TKReturns.Remove(item);
                        }

                        //减少仓库中的货物数量
                        var model2 = db.warehouses.FirstOrDefault(m => m.warehouse_name == TKReturns_name);
                        model2.warehouse_number = model2.warehouse_number - Convert.ToDecimal(TKReturns_number);

                        //增加总资金
                        var model3 = db.funds.FirstOrDefault(m => m.funds_id == 1);
                        model3.funds_number = model3.funds_number - Convert.ToDecimal(TKReturns_number) * Convert.ToDecimal(TKReturns_price);

                        //对应支出单数额减少
                        var model4 = db.pays.FirstOrDefault(m => m.pay_purchase_id == TKReturns_id);
                        model4.pay_number = model4.pay_number - Convert.ToDecimal(TKReturns_number) * Convert.ToDecimal(TKReturns_price);

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

                    MessageBox.Show("删除成功！");
                }
                else
                {
                    MessageBox.Show("请选择要删除的数据行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //重新加载页面内容

                dataGridView1.Rows.Clear();
                this.textBox4.Text = username;

                var model5 = db.TKReturns.Select(m => m);
                model5 = model5.Where(m => m.TKReturns_id == current_TK_id);
                int line = 0;

                foreach (var item in model5)
                {
                    textBox1.Text = item.TKReturns_id;
                    textBox2.Text = item.TKReturns_salesid;
                    textBox3.Text = item.TKReturns_data;

                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[line].Cells[0].Value = item.TKReturns_name;
                    dataGridView1.Rows[line].Cells[1].Value = item.TKReturns_number;
                    dataGridView1.Rows[line].Cells[2].Value = item.TKReturns_price;
                    dataGridView1.Rows[line].Cells[3].Value = item.TKReturns_unit;
                    dataGridView1.Rows[line].Cells[4].Value = item.TKReturns_others;

                    line++;
                }

                ifChanged = false;
            }
        }

        //更新数据
        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定更新？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }

            //删除原有表单
            string TKReturns_id = textBox1.Text.ToString();

            var model = db.TKReturns.Select(m => m);
            model = model.Where(m => m.TKReturns_id == TKReturns_id);

            foreach (var item in model)
            {
                //减少仓库中的货物数量
                var model2 = db.warehouses.FirstOrDefault(m => m.warehouse_name == item.TKReturns_name);
                model2.warehouse_number = model2.warehouse_number - Convert.ToDecimal(item.TKReturns_number);

                //增加总资金
                var model3 = db.funds.FirstOrDefault(m => m.funds_id == 1);
                model3.funds_number = model3.funds_number + Convert.ToDecimal(item.TKReturns_number) * Convert.ToDecimal(item.TKReturns_price);

                //对应收入单销售额减少
                var model4 = db.pays.FirstOrDefault(m => m.pay_purchase_id == TKReturns_id);
                model4.pay_number = model4.pay_number - Convert.ToDecimal(item.TKReturns_number) * Convert.ToDecimal(item.TKReturns_price);

                //删除销售单中对应单号的所有数据

                db.TKReturns.Remove(item);
            }

            //验证新单格式
            if (textBox2.Text.Trim() == "")
            {
                MessageBox.Show("销售单号不能为空！");
                return;
            }

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count - 1; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value == null)
                    {
                        MessageBox.Show("第" + (i + 1) + "行" + (j + 1) + "列处不能为空！");
                        return;
                    }
                }
            }

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if (!Regex.IsMatch(dataGridView1.Rows[i].Cells[1].Value.ToString(), @"^[+-]?(?!0\d)\d+(\.[0-9]+)?$"))
                {
                    MessageBox.Show("数量必须为数字！");
                    return;
                }

                if (!Regex.IsMatch(dataGridView1.Rows[i].Cells[2].Value.ToString(), @"^[+-]?(?!0\d)\d+(\.[0-9]+)?$"))
                {
                    MessageBox.Show("单价必须为数字！");
                    return;
                }

                if (!Regex.IsMatch(dataGridView1.Rows[i].Cells[3].Value.ToString(), "^[\u4e00-\u9fa5]$"))
                {
                    MessageBox.Show("单位必须为汉字！");
                    return;
                }
            }

            //验证是否有此退货单
            var model5 = db.TKReturns.Where(m => m.TKReturns_id == textBox1.Text.Trim());

            if (model5.Count() == 0)
            {
                MessageBox.Show("此销售单不存在，请重新输入！");
                return;
            }

            //验证表单内容是否正确
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                bool flag = false;

                foreach (var item in model5)
                {
                    if (dataGridView1.Rows[i].Cells[0].Value.ToString() == item.TKReturns_name)
                    {
                        flag = true;
                        break;
                    }
                }

                if (flag == false)
                {
                    MessageBox.Show("退货表单信息有误！");
                    return;
                }
            }

            //写入数据库
            DBCL.TKReturn model6 = new DBCL.TKReturn();
            string TKReturns_salesid = "";
            string TKReturns_worker_id = "";
            string TKReturns_data = "";
            string TKReturns_name = "";
            string TKReturns_unit = "";
            string TKReturns_price = "";
            string TKReturns_number = "";
            string TKReturns_others = "";
            
            TKReturns_salesid = textBox2.Text.Trim();
            TKReturns_worker_id = username;
            TKReturns_data = textBox3.Text.Trim();

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                TKReturns_name = dataGridView1.Rows[i].Cells[0].Value.ToString().Trim();
                TKReturns_number = dataGridView1.Rows[i].Cells[1].Value.ToString().Trim();
                TKReturns_price = dataGridView1.Rows[i].Cells[2].Value.ToString().Trim();
                TKReturns_unit = dataGridView1.Rows[i].Cells[3].Value.ToString().Trim();

                if (dataGridView1.Rows[i].Cells[4].Value == null)
                {
                    TKReturns_others = "";
                }
                else
                {
                    TKReturns_others = dataGridView1.Rows[i].Cells[4].Value.ToString().Trim();
                }

                model6.TKReturns_id = TKReturns_id;
                model6.TKReturns_salesid = TKReturns_salesid;
                model6.TKReturns_worker_id = TKReturns_worker_id;
                model6.TKReturns_data = TKReturns_data;
                model6.TKReturns_name = TKReturns_name;
                model6.TKReturns_number = Convert.ToDecimal(TKReturns_number);
                model6.TKReturns_price = Convert.ToDecimal(TKReturns_price);
                model6.TKReturns_unit = TKReturns_unit;
                model6.TKReturns_others = TKReturns_others;
                
                //保存到客户进货单
                try
                {
                    db.TKReturns.Add(model6);
                    db.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("失败！");
                    return;
                }

                //更改仓库货物数量
                var model7 = db.warehouses.FirstOrDefault(m => m.warehouse_name == TKReturns_name);
                model7.warehouse_number = model7.warehouse_number + Convert.ToDecimal(TKReturns_number);
            }

            //修改支出表
            var model8 = db.pays.FirstOrDefault(m => m.pay_purchase_id == TKReturns_id);
            double money = 0;

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                money = money + Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value.ToString());
            }

            model8.pay_number = Convert.ToDecimal(money);


            //修改总金额
            var model9 = db.funds.FirstOrDefault(m => m.funds_id == 1);
            model9.funds_number = model9.funds_number - Convert.ToDecimal(money);
            
            try
            {
                db.SaveChanges();
            }
            catch
            {
                MessageBox.Show("更新失败！");
                return;
            }

            MessageBox.Show("更新成功！");

            //显示新表
            textBox4.Text = username;
            dataGridView1.Rows.Clear();

            var model10 = db.TKReturns.Select(m => m);
            model10 = model10.Where(m => m.TKReturns_id == current_TK_id);
            int line = 0;

            foreach (var item in model)
            {
                textBox1.Text = item.TKReturns_id;
                textBox2.Text = item.TKReturns_salesid;
                textBox3.Text = item.TKReturns_data;

                dataGridView1.Rows.Add();
                dataGridView1.Rows[line].Cells[0].Value = item.TKReturns_name;
                dataGridView1.Rows[line].Cells[1].Value = item.TKReturns_number;
                dataGridView1.Rows[line].Cells[2].Value = item.TKReturns_price;
                dataGridView1.Rows[line].Cells[3].Value = item.TKReturns_unit;
                dataGridView1.Rows[line].Cells[4].Value = item.TKReturns_others;

                line++;
            }

            ifChanged = false;
        }
    }
}
