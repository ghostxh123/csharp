namespace SGsystem
{
    partial class cth
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.jh_delete = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.j_sum = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.j_source = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.j_workid = new System.Windows.Forms.TextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.j_id = new System.Windows.Forms.TextBox();
            this.j_data = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.j_reset = new System.Windows.Forms.Button();
            this.j_submit = new System.Windows.Forms.Button();
            this.j_d = new System.Windows.Forms.DataGridView();
            this.j_d_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.j_d_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.j_d_number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.j_d_unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.j_d_price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.j_d_others = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.j_d)).BeginInit();
            this.SuspendLayout();
            // 
            // jh_delete
            // 
            this.jh_delete.Location = new System.Drawing.Point(306, 378);
            this.jh_delete.Name = "jh_delete";
            this.jh_delete.Size = new System.Drawing.Size(101, 28);
            this.jh_delete.TabIndex = 44;
            this.jh_delete.Text = "删除";
            this.jh_delete.UseVisualStyleBackColor = true;
            this.jh_delete.Click += new System.EventHandler(this.jh_delete_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.j_sum);
            this.groupBox1.Controls.Add(this.label33);
            this.groupBox1.Controls.Add(this.j_source);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(87, 321);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(534, 39);
            this.groupBox1.TabIndex = 43;
            this.groupBox1.TabStop = false;
            // 
            // j_sum
            // 
            this.j_sum.Enabled = false;
            this.j_sum.Location = new System.Drawing.Point(65, 11);
            this.j_sum.Name = "j_sum";
            this.j_sum.Size = new System.Drawing.Size(100, 21);
            this.j_sum.TabIndex = 28;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(356, 14);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(41, 12);
            this.label33.TabIndex = 8;
            this.label33.Text = "供货商";
            // 
            // j_source
            // 
            this.j_source.Location = new System.Drawing.Point(407, 12);
            this.j_source.Name = "j_source";
            this.j_source.Size = new System.Drawing.Size(105, 21);
            this.j_source.TabIndex = 11;
            this.j_source.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.j_source_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "总价";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.j_workid);
            this.groupBox2.Controls.Add(this.label36);
            this.groupBox2.Controls.Add(this.j_id);
            this.groupBox2.Controls.Add(this.j_data);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(87, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(536, 31);
            this.groupBox2.TabIndex = 42;
            this.groupBox2.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(203, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "工号";
            // 
            // j_workid
            // 
            this.j_workid.Enabled = false;
            this.j_workid.Location = new System.Drawing.Point(238, 9);
            this.j_workid.Name = "j_workid";
            this.j_workid.Size = new System.Drawing.Size(100, 21);
            this.j_workid.TabIndex = 27;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(6, 12);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(53, 12);
            this.label36.TabIndex = 7;
            this.label36.Text = "进货单号";
            // 
            // j_id
            // 
            this.j_id.Enabled = false;
            this.j_id.Location = new System.Drawing.Point(65, 9);
            this.j_id.Name = "j_id";
            this.j_id.Size = new System.Drawing.Size(132, 21);
            this.j_id.TabIndex = 26;
            // 
            // j_data
            // 
            this.j_data.Location = new System.Drawing.Point(407, 6);
            this.j_data.Name = "j_data";
            this.j_data.Size = new System.Drawing.Size(123, 21);
            this.j_data.TabIndex = 11;
            this.j_data.CloseUp += new System.EventHandler(this.j_data_CloseUp);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(356, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "日期";
            // 
            // j_reset
            // 
            this.j_reset.Location = new System.Drawing.Point(477, 378);
            this.j_reset.Name = "j_reset";
            this.j_reset.Size = new System.Drawing.Size(101, 28);
            this.j_reset.TabIndex = 41;
            this.j_reset.Text = "关闭";
            this.j_reset.UseVisualStyleBackColor = true;
            this.j_reset.Click += new System.EventHandler(this.j_reset_Click);
            // 
            // j_submit
            // 
            this.j_submit.Location = new System.Drawing.Point(151, 378);
            this.j_submit.Name = "j_submit";
            this.j_submit.Size = new System.Drawing.Size(101, 28);
            this.j_submit.TabIndex = 40;
            this.j_submit.Text = "保存";
            this.j_submit.UseVisualStyleBackColor = true;
            this.j_submit.Click += new System.EventHandler(this.j_submit_Click);
            // 
            // j_d
            // 
            this.j_d.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.j_d.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.j_d_id,
            this.j_d_name,
            this.j_d_number,
            this.j_d_unit,
            this.j_d_price,
            this.j_d_others});
            this.j_d.Location = new System.Drawing.Point(87, 67);
            this.j_d.Name = "j_d";
            this.j_d.RowTemplate.Height = 23;
            this.j_d.Size = new System.Drawing.Size(536, 231);
            this.j_d.TabIndex = 39;
            this.j_d.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.j_d_CellEndEdit);
            // 
            // j_d_id
            // 
            this.j_d_id.HeaderText = "id";
            this.j_d_id.Name = "j_d_id";
            this.j_d_id.Visible = false;
            // 
            // j_d_name
            // 
            this.j_d_name.HeaderText = "名称";
            this.j_d_name.Name = "j_d_name";
            // 
            // j_d_number
            // 
            this.j_d_number.HeaderText = "数量";
            this.j_d_number.Name = "j_d_number";
            // 
            // j_d_unit
            // 
            this.j_d_unit.HeaderText = "单位";
            this.j_d_unit.Name = "j_d_unit";
            // 
            // j_d_price
            // 
            this.j_d_price.HeaderText = "单价";
            this.j_d_price.Name = "j_d_price";
            // 
            // j_d_others
            // 
            this.j_d_others.HeaderText = "备注";
            this.j_d_others.Name = "j_d_others";
            // 
            // cth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 418);
            this.Controls.Add(this.jh_delete);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.j_reset);
            this.Controls.Add(this.j_submit);
            this.Controls.Add(this.j_d);
            this.Name = "cth";
            this.Text = "仓库退货单详情";
            this.Load += new System.EventHandler(this.cth_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.j_d)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button jh_delete;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox j_sum;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.TextBox j_source;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox j_workid;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.TextBox j_id;
        private System.Windows.Forms.DateTimePicker j_data;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button j_reset;
        private System.Windows.Forms.Button j_submit;
        private System.Windows.Forms.DataGridView j_d;
        private System.Windows.Forms.DataGridViewTextBoxColumn j_d_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn j_d_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn j_d_number;
        private System.Windows.Forms.DataGridViewTextBoxColumn j_d_unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn j_d_price;
        private System.Windows.Forms.DataGridViewTextBoxColumn j_d_others;
    }
}