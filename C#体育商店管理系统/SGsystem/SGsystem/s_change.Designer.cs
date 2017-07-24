namespace SGsystem
{
    partial class s_change
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.TextBox();
            this.price = new System.Windows.Forms.TextBox();
            this.number = new System.Windows.Forms.TextBox();
            this.unit = new System.Windows.Forms.TextBox();
            this.other = new System.Windows.Forms.TextBox();
            this.save = new System.Windows.Forms.Button();
            this.s_close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "价格";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "数量";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(44, 202);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "单位";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(44, 265);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "备注";
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(123, 40);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(202, 21);
            this.name.TabIndex = 5;
            // 
            // price
            // 
            this.price.Location = new System.Drawing.Point(123, 94);
            this.price.Name = "price";
            this.price.Size = new System.Drawing.Size(202, 21);
            this.price.TabIndex = 6;
            // 
            // number
            // 
            this.number.Location = new System.Drawing.Point(123, 145);
            this.number.Name = "number";
            this.number.Size = new System.Drawing.Size(202, 21);
            this.number.TabIndex = 7;
            // 
            // unit
            // 
            this.unit.Location = new System.Drawing.Point(123, 199);
            this.unit.Name = "unit";
            this.unit.Size = new System.Drawing.Size(202, 21);
            this.unit.TabIndex = 8;
            // 
            // other
            // 
            this.other.Location = new System.Drawing.Point(123, 262);
            this.other.Name = "other";
            this.other.Size = new System.Drawing.Size(202, 21);
            this.other.TabIndex = 9;
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(46, 367);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(83, 34);
            this.save.TabIndex = 10;
            this.save.Text = "保存";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // s_close
            // 
            this.s_close.Location = new System.Drawing.Point(252, 367);
            this.s_close.Name = "s_close";
            this.s_close.Size = new System.Drawing.Size(83, 34);
            this.s_close.TabIndex = 11;
            this.s_close.Text = "关闭";
            this.s_close.UseVisualStyleBackColor = true;
            this.s_close.Click += new System.EventHandler(this.s_close_Click);
            // 
            // s_change
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 440);
            this.Controls.Add(this.s_close);
            this.Controls.Add(this.save);
            this.Controls.Add(this.other);
            this.Controls.Add(this.unit);
            this.Controls.Add(this.number);
            this.Controls.Add(this.price);
            this.Controls.Add(this.name);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "s_change";
            this.Text = "商品修改";
            this.Load += new System.EventHandler(this.s_change_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.TextBox price;
        private System.Windows.Forms.TextBox number;
        private System.Windows.Forms.TextBox unit;
        private System.Windows.Forms.TextBox other;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.Button s_close;
    }
}