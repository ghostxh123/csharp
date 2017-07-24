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
    public partial class DetailOfSales : Form
    {
        //变量*******************************************************************************************8******************
        private DBCL.DBEntities db = new DBCL.DBEntities();
        string username = "";
        string current_sales_id = "";
        bool ifChanged = false;
        bool power;

        //函数**************************************************************************************************************
        public DetailOfSales()
        {
            InitializeComponent();
        }

        public DetailOfSales(string username, string current_sales_id,bool power)
        {
            InitializeComponent();
            this.current_sales_id = current_sales_id;
            this.username = username;
            this.power = power;
        }

        //界面加载
        private void DetailOfSales_Load(object sender, EventArgs e)
        {
            if(power == false)
            {
                button2.Enabled = false;
                button3.Enabled = false;
            }

            textBox4.Text = username;

            //给datagridview赋值
            var model = db.sales.Select(m => m);
            model = model.Where(m=>m.sales_id == current_sales_id);
            int line = 0;

            foreach(var item in model)
            {
                textBox1.Text = item.sales_id;
                textBox2.Text = item.sales_whereabouts;
                textBox3.Text = item.sales_data;

                dataGridView1.Rows.Add();
                dataGridView1.Rows[line].Cells[0].Value = item.sales_name;
                dataGridView1.Rows[line].Cells[1].Value = item.sales_number;
                dataGridView1.Rows[line].Cells[2].Value = item.sales_price;
                dataGridView1.Rows[line].Cells[3].Value = item.sales_unit;
                dataGridView1.Rows[line].Cells[4].Value = item.sales_real_price;
                dataGridView1.Rows[line].Cells[5].Value = item.sales_others;

                line++;
            }
            ifChanged = false;
        }

        //关闭页面
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //删除数据
        private void button2_Click(object sender, EventArgs e)
        {
            //判断表单是否已更新，若未更新，执行删除，否则，执行更新
            if(ifChanged == true)
            {
                if (MessageBox.Show("表单已修改，是否要更新？（保存后才可删除）", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                {
                    return;
                }
                else
                {
                    string sales_id = textBox1.Text.ToString();

                    var model = db.sales.Select(m => m);
                    model = model.Where(m => m.sales_id == sales_id);

                    foreach (var item in model)
                    {
                        //增加仓库中的货物数量
                        var model2 = db.warehouses.FirstOrDefault(m => m.warehouse_name == item.sales_name);
                        model2.warehouse_number = model2.warehouse_number + Convert.ToDecimal(item.sales_number);

                        //减少总资金
                        var model3 = db.funds.FirstOrDefault(m => m.funds_id == 1);
                        model3.funds_number = model3.funds_number - Convert.ToDecimal(item.sales_number) * Convert.ToDecimal(item.sales_real_price);

                        //对应收入单销售额减少
                        var model4 = db.recipts.FirstOrDefault(m => m.receipt_sales_id == sales_id);
                        model4.receipt_number = model4.receipt_number - Convert.ToDecimal(item.sales_number) * Convert.ToDecimal(item.sales_real_price);

                        //删除销售单中对应单号的所有数据

                        db.sales.Remove(item);
                    }

                    //验证新单格式
                    if (textBox2.Text.Trim() == "")
                    {
                        MessageBox.Show("销售对象不能为空！");
                        return;
                    }

                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j < dataGridView1.Columns.Count - 1; j++)
                        {
                            if (j == 4)
                            {
                                continue;
                            }

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

                        if (dataGridView1.Rows[i].Cells[4].Value != null)
                        {
                            if (!Regex.IsMatch(dataGridView1.Rows[i].Cells[1].Value.ToString(), @"^[+-]?(?!0\d)\d+(\.[0-9]+)?$"))
                            {
                                MessageBox.Show("折扣价必须为数字！");
                                return;
                            }
                        }
                    }

                    //判断仓库里是否有对应的物品
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        string name = dataGridView1.Rows[i].Cells[0].Value.ToString().Trim();
                        string number = dataGridView1.Rows[i].Cells[1].Value.ToString().Trim();

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
                    DBCL.sale model5 = new DBCL.sale();
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

                    //计算总价

                    double result = 0;

                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        if (dataGridView1.Rows[i].Cells[4].Value == null)
                        {
                            result = result + Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value) * Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value);
                        }
                        else
                        {
                            result = result + Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value) * Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);
                        }
                    }

                    sales_whole_price = Convert.ToString(result).Trim();

                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        sales_name = dataGridView1.Rows[i].Cells[0].Value.ToString().Trim();
                        sales_number = dataGridView1.Rows[i].Cells[1].Value.ToString().Trim();
                        sales_price = dataGridView1.Rows[i].Cells[2].Value.ToString().Trim();
                        sales_nuit = dataGridView1.Rows[i].Cells[3].Value.ToString().Trim();

                        if (dataGridView1.Rows[i].Cells[4].Value == null)
                        {
                            sales_real_price = sales_price;
                        }
                        else
                        {
                            sales_real_price = dataGridView1.Rows[i].Cells[4].Value.ToString().Trim();
                        }

                        if (dataGridView1.Rows[i].Cells[5].Value == null)
                        {
                            sales_others = "";
                        }
                        else
                        {
                            sales_others = dataGridView1.Rows[i].Cells[5].Value.ToString().Trim();
                        }

                        //向销售表中添加的信息
                        model5.sales_id = sales_id;
                        model5.sales_whereabouts = sales_whereabouts;
                        model5.sales_worker_id = sales_worker_id;
                        model5.sales_data = sales_data;
                        model5.sales_name = sales_name;
                        model5.sales_unit = sales_nuit;
                        model5.sales_number = Convert.ToDecimal(sales_number);
                        model5.sales_price = Convert.ToDecimal(sales_price);
                        model5.sales_real_price = Convert.ToDecimal(sales_real_price);
                        model5.sales_others = sales_others;
                        model5.sales_whole_price = Convert.ToDecimal(sales_whole_price);

                        try
                        {
                            db.sales.Add(model5);
                            db.SaveChanges();
                        }
                        catch
                        {
                            MessageBox.Show("更新失败！");
                            return;
                        }

                        //仓库表中修改的信息
                        var model6 = db.warehouses.FirstOrDefault(m => m.warehouse_name == sales_name);
                        model6.warehouse_number = model6.warehouse_number - Convert.ToDecimal(sales_number);
                    }

                    //修改收入单
                    var model7 = db.recipts.FirstOrDefault(m => m.receipt_sales_id == sales_id);
                    model7.receipt_number = Convert.ToDecimal(sales_whole_price);

                    //总资金修改
                    var model8 = db.funds.FirstOrDefault(m => m.funds_id == 1);
                    model8.funds_number = model8.funds_number + Convert.ToDecimal(sales_whole_price);

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
                    dataGridView1.Rows.Clear();
                    this.textBox4.Text = username;

                    var model9 = db.sales.Select(m => m);
                    model9 = model9.Where(m => m.sales_id == current_sales_id);
                    int line = 0;

                    foreach (var item in model9)
                    {
                        textBox1.Text = item.sales_id;
                        textBox2.Text = item.sales_whereabouts;
                        textBox3.Text = item.sales_data;

                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[line].Cells[0].Value = item.sales_name;
                        dataGridView1.Rows[line].Cells[1].Value = item.sales_number;
                        dataGridView1.Rows[line].Cells[2].Value = item.sales_price;
                        dataGridView1.Rows[line].Cells[3].Value = item.sales_unit;
                        dataGridView1.Rows[line].Cells[4].Value = item.sales_real_price;
                        dataGridView1.Rows[line].Cells[5].Value = item.sales_others;

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

                    string sales_id = textBox1.Text.ToString();

                    foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                    {
                        string sales_name = r.Cells[0].Value.ToString();
                        string sales_number = r.Cells[1].Value.ToString();
                        string sales_price = r.Cells[4].Value.ToString();

                        //删除销售单中对应的数据
                        var model = db.sales.Select(m => m);
                        model = model.Where(m => m.sales_id == sales_id);
                        model = model.Where(m => m.sales_name == sales_name);

                        foreach (var item in model)
                        {
                            db.sales.Remove(item);
                        }

                        //增加仓库中的货物数量
                        var model2 = db.warehouses.FirstOrDefault(m => m.warehouse_name == sales_name);
                        model2.warehouse_number = model2.warehouse_number + Convert.ToDecimal(sales_number);

                        //减少总资金
                        var model3 = db.funds.FirstOrDefault(m => m.funds_id == 1);
                        model3.funds_number = model3.funds_number - Convert.ToDecimal(sales_number) * Convert.ToDecimal(sales_price);

                        //对应收入单销售额减少
                        var model4 = db.recipts.FirstOrDefault(m => m.receipt_sales_id == sales_id);
                        model4.receipt_number = model4.receipt_number - Convert.ToDecimal(sales_number) * Convert.ToDecimal(sales_price);

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

                var model5 = db.sales.Select(m => m);
                model5 = model5.Where(m => m.sales_id == current_sales_id);
                int line = 0;

                foreach (var item in model5)
                {
                    textBox1.Text = item.sales_id;
                    textBox2.Text = item.sales_whereabouts;
                    textBox3.Text = item.sales_data;

                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[line].Cells[0].Value = item.sales_name;
                    dataGridView1.Rows[line].Cells[1].Value = item.sales_number;
                    dataGridView1.Rows[line].Cells[2].Value = item.sales_price;
                    dataGridView1.Rows[line].Cells[3].Value = item.sales_unit;
                    dataGridView1.Rows[line].Cells[4].Value = item.sales_real_price;
                    dataGridView1.Rows[line].Cells[5].Value = item.sales_others;

                    line++;
                }

                ifChanged = false;
            }
        }

        //判断数据是否发生变化
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            ifChanged = true;
        }

        //更新表单
        private void button3_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("确定更新？","提示",MessageBoxButtons.OKCancel,MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }

            //删除原有表单
            string sales_id = textBox1.Text.ToString();

            var model = db.sales.Select(m => m);
            model = model.Where(m => m.sales_id == sales_id);

            foreach (var item in model)
            {
                //增加仓库中的货物数量
                var model2 = db.warehouses.FirstOrDefault(m => m.warehouse_name == item.sales_name);
                model2.warehouse_number = model2.warehouse_number + Convert.ToDecimal(item.sales_number);

                //减少总资金
                var model3 = db.funds.FirstOrDefault(m => m.funds_id == 1);
                model3.funds_number = model3.funds_number - Convert.ToDecimal(item.sales_number) * Convert.ToDecimal(item.sales_real_price);

                //对应收入单销售额减少
                var model4 = db.recipts.FirstOrDefault(m => m.receipt_sales_id == sales_id);
                model4.receipt_number = model4.receipt_number - Convert.ToDecimal(item.sales_number) * Convert.ToDecimal(item.sales_real_price);

                //删除销售单中对应单号的所有数据

                db.sales.Remove(item);
            }

            //验证新单格式
            if (textBox2.Text.Trim() == "")
            {
                MessageBox.Show("销售对象不能为空！");
                return;
            }

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count - 1; j++)
                {
                    if (j == 4)
                    {
                        continue;
                    }

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

                if (dataGridView1.Rows[i].Cells[4].Value != null)
                {
                    if (!Regex.IsMatch(dataGridView1.Rows[i].Cells[1].Value.ToString(), @"^[+-]?(?!0\d)\d+(\.[0-9]+)?$"))
                    {
                        MessageBox.Show("折扣价必须为数字！");
                        return;
                    }
                }
            }

            //判断仓库里是否有对应的物品
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                string name = dataGridView1.Rows[i].Cells[0].Value.ToString().Trim();
                string number = dataGridView1.Rows[i].Cells[1].Value.ToString().Trim();

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
            DBCL.sale model5 = new DBCL.sale();
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

            //计算总价

            double result = 0;

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if (dataGridView1.Rows[i].Cells[4].Value == null)
                {
                    result = result + Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value) * Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value);
                }
                else
                {
                    result = result + Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value) * Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);
                }
            }

            sales_whole_price = Convert.ToString(result).Trim();

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                sales_name = dataGridView1.Rows[i].Cells[0].Value.ToString().Trim();
                sales_number = dataGridView1.Rows[i].Cells[1].Value.ToString().Trim();
                sales_price = dataGridView1.Rows[i].Cells[2].Value.ToString().Trim();
                sales_nuit = dataGridView1.Rows[i].Cells[3].Value.ToString().Trim();
                
                if (dataGridView1.Rows[i].Cells[4].Value == null)
                {
                    sales_real_price = sales_price;
                }
                else
                {
                    sales_real_price = dataGridView1.Rows[i].Cells[4].Value.ToString().Trim();
                }

                if (dataGridView1.Rows[i].Cells[5].Value == null)
                {
                    sales_others = "";
                }
                else
                {
                    sales_others = dataGridView1.Rows[i].Cells[5].Value.ToString().Trim();
                }

                //向销售表中添加的信息
                model5.sales_id = sales_id;
                model5.sales_whereabouts = sales_whereabouts;
                model5.sales_worker_id = sales_worker_id;
                model5.sales_data = sales_data;
                model5.sales_name = sales_name;
                model5.sales_unit = sales_nuit;
                model5.sales_number = Convert.ToDecimal(sales_number);
                model5.sales_price = Convert.ToDecimal(sales_price);
                model5.sales_real_price = Convert.ToDecimal(sales_real_price);
                model5.sales_others = sales_others;
                model5.sales_whole_price = Convert.ToDecimal(sales_whole_price);

                try
                {
                    db.sales.Add(model5);
                    db.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("更新失败！");
                    return;
                }

                //仓库表中修改的信息
                var model6 = db.warehouses.FirstOrDefault(m => m.warehouse_name == sales_name);
                model6.warehouse_number = model6.warehouse_number - Convert.ToDecimal(sales_number);
            }

            //修改收入单
            var model7 = db.recipts.FirstOrDefault(m=>m.receipt_sales_id == sales_id);
            model7.receipt_number = Convert.ToDecimal(sales_whole_price);

            //总资金修改
            var model8 = db.funds.FirstOrDefault(m => m.funds_id == 1);
            model8.funds_number = model8.funds_number + Convert.ToDecimal(sales_whole_price);

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
            dataGridView1.Rows.Clear();
            this.textBox4.Text = username;

            var model9 = db.sales.Select(m => m);
            model9 = model9.Where(m => m.sales_id == current_sales_id);
            int line = 0;

            foreach (var item in model9)
            {
                textBox1.Text = item.sales_id;
                textBox2.Text = item.sales_whereabouts;
                textBox3.Text = item.sales_data;

                dataGridView1.Rows.Add();
                dataGridView1.Rows[line].Cells[0].Value = item.sales_name;
                dataGridView1.Rows[line].Cells[1].Value = item.sales_number;
                dataGridView1.Rows[line].Cells[2].Value = item.sales_price;
                dataGridView1.Rows[line].Cells[3].Value = item.sales_unit;
                dataGridView1.Rows[line].Cells[4].Value = item.sales_real_price;
                dataGridView1.Rows[line].Cells[5].Value = item.sales_others;

                line++;
            }

            ifChanged = false;
        }
    }
}
