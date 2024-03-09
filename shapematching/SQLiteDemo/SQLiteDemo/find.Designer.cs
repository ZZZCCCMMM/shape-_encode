namespace SQLiteDemo
{
    partial class find
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
            this.readbtn = new System.Windows.Forms.Button();
            this.findbtn = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.angle_corner = new System.Windows.Forms.Label();
            this.checkbtn = new System.Windows.Forms.Button();
            this.hWindowControl1 = new HalconDotNet.HWindowControl();
            this.createbtn = new System.Windows.Forms.Button();
            this.excelbtn = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button_txt = new System.Windows.Forms.Button();
            this.backbtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.richTextBoxresult = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // readbtn
            // 
            this.readbtn.Location = new System.Drawing.Point(537, 44);
            this.readbtn.Name = "readbtn";
            this.readbtn.Size = new System.Drawing.Size(87, 40);
            this.readbtn.TabIndex = 0;
            this.readbtn.Text = "读图";
            this.readbtn.UseVisualStyleBackColor = true;
            this.readbtn.Click += new System.EventHandler(this.readbtn_Click);
            // 
            // findbtn
            // 
            this.findbtn.Location = new System.Drawing.Point(671, 120);
            this.findbtn.Name = "findbtn";
            this.findbtn.Size = new System.Drawing.Size(83, 36);
            this.findbtn.TabIndex = 1;
            this.findbtn.Text = "识别";
            this.findbtn.UseVisualStyleBackColor = true;
            this.findbtn.Click += new System.EventHandler(this.findbtn_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(11, 24);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(74, 19);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "左上角";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(11, 58);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(74, 19);
            this.checkBox2.TabIndex = 3;
            this.checkBox2.Text = "左下角";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(11, 83);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(74, 19);
            this.checkBox3.TabIndex = 3;
            this.checkBox3.Text = "右上角";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(11, 108);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(74, 19);
            this.checkBox4.TabIndex = 3;
            this.checkBox4.Text = "右下角";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.checkBox3);
            this.groupBox1.Controls.Add(this.checkBox4);
            this.groupBox1.Location = new System.Drawing.Point(536, 192);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(130, 134);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "角判定";
            // 
            // angle_corner
            // 
            this.angle_corner.AutoSize = true;
            this.angle_corner.Location = new System.Drawing.Point(544, 329);
            this.angle_corner.Name = "angle_corner";
            this.angle_corner.Size = new System.Drawing.Size(67, 15);
            this.angle_corner.TabIndex = 7;
            this.angle_corner.Text = "定位是：";
            // 
            // checkbtn
            // 
            this.checkbtn.Location = new System.Drawing.Point(672, 192);
            this.checkbtn.Name = "checkbtn";
            this.checkbtn.Size = new System.Drawing.Size(83, 36);
            this.checkbtn.TabIndex = 8;
            this.checkbtn.Text = "勾选结果";
            this.checkbtn.UseVisualStyleBackColor = true;
            this.checkbtn.Click += new System.EventHandler(this.checkbtn_Click);
            // 
            // hWindowControl1
            // 
            this.hWindowControl1.BackColor = System.Drawing.Color.Black;
            this.hWindowControl1.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl1.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl1.Location = new System.Drawing.Point(12, 12);
            this.hWindowControl1.Name = "hWindowControl1";
            this.hWindowControl1.Size = new System.Drawing.Size(509, 433);
            this.hWindowControl1.TabIndex = 9;
            this.hWindowControl1.WindowSize = new System.Drawing.Size(509, 433);
            // 
            // createbtn
            // 
            this.createbtn.Location = new System.Drawing.Point(672, 44);
            this.createbtn.Name = "createbtn";
            this.createbtn.Size = new System.Drawing.Size(82, 40);
            this.createbtn.TabIndex = 10;
            this.createbtn.Text = "创建模型";
            this.createbtn.UseVisualStyleBackColor = true;
            this.createbtn.Click += new System.EventHandler(this.createbtn_Click);
            // 
            // excelbtn
            // 
            this.excelbtn.Location = new System.Drawing.Point(537, 360);
            this.excelbtn.Name = "excelbtn";
            this.excelbtn.Size = new System.Drawing.Size(119, 36);
            this.excelbtn.TabIndex = 12;
            this.excelbtn.Text = "导出excel";
            this.excelbtn.UseVisualStyleBackColor = true;
            this.excelbtn.Click += new System.EventHandler(this.excelbtn_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(768, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(232, 434);
            this.dataGridView1.TabIndex = 13;
            // 
            // button_txt
            // 
            this.button_txt.Location = new System.Drawing.Point(687, 360);
            this.button_txt.Name = "button_txt";
            this.button_txt.Size = new System.Drawing.Size(75, 36);
            this.button_txt.TabIndex = 14;
            this.button_txt.Text = "导出txt";
            this.button_txt.UseVisualStyleBackColor = true;
            this.button_txt.Click += new System.EventHandler(this.button_txt_Click);
            // 
            // backbtn
            // 
            this.backbtn.Location = new System.Drawing.Point(687, 249);
            this.backbtn.Name = "backbtn";
            this.backbtn.Size = new System.Drawing.Size(60, 69);
            this.backbtn.TabIndex = 15;
            this.backbtn.Text = "返回登录界面";
            this.backbtn.UseVisualStyleBackColor = true;
            this.backbtn.Click += new System.EventHandler(this.backbtn_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(547, 115);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 41);
            this.button1.TabIndex = 16;
            this.button1.Text = "旋转";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.richTextBoxresult);
            this.panel1.Controls.Add(this.hWindowControl1);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.button_txt);
            this.panel1.Controls.Add(this.backbtn);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.readbtn);
            this.panel1.Controls.Add(this.checkbtn);
            this.panel1.Controls.Add(this.createbtn);
            this.panel1.Controls.Add(this.findbtn);
            this.panel1.Controls.Add(this.excelbtn);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.angle_corner);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1204, 458);
            this.panel1.TabIndex = 17;
            this.panel1.SizeChanged += new System.EventHandler(this.frmMain_SizeChanged);
            // 
            // richTextBoxresult
            // 
            this.richTextBoxresult.Location = new System.Drawing.Point(1013, 12);
            this.richTextBoxresult.Name = "richTextBoxresult";
            this.richTextBoxresult.Size = new System.Drawing.Size(179, 434);
            this.richTextBoxresult.TabIndex = 18;
            this.richTextBoxresult.Text = "";
            // 
            // find
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1204, 458);
            this.Controls.Add(this.panel1);
            this.Name = "find";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "find";
            this.SizeChanged += new System.EventHandler(this.frmMain_SizeChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button readbtn;
        private System.Windows.Forms.Button findbtn;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label angle_corner;
        private System.Windows.Forms.Button checkbtn;
        private HalconDotNet.HWindowControl hWindowControl1;
        private System.Windows.Forms.Button createbtn;
        private System.Windows.Forms.Button excelbtn;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button_txt;
        private System.Windows.Forms.Button backbtn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox richTextBoxresult;
    }
}