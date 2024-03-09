namespace SQLiteDemo
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.Button_OpenDB = new System.Windows.Forms.Button();
            this.Button_CloseDB = new System.Windows.Forms.Button();
            this.Button_Query = new System.Windows.Forms.Button();
            this.Button_Add = new System.Windows.Forms.Button();
            this.Button_Modify = new System.Windows.Forms.Button();
            this.Button_Delete = new System.Windows.Forms.Button();
            this.Label_DBOpenStateTitle = new System.Windows.Forms.Label();
            this.Label_DBOpenState = new System.Windows.Forms.Label();
            this.Button_Table = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.identityla = new System.Windows.Forms.Label();
            this.pwdla = new System.Windows.Forms.Label();
            this.nameld = new System.Windows.Forms.Label();
            this.IDla = new System.Windows.Forms.Label();
            this.textBoxidentity = new System.Windows.Forms.TextBox();
            this.textBoxpwd = new System.Windows.Forms.TextBox();
            this.textBoxname = new System.Windows.Forms.TextBox();
            this.textBoxid = new System.Windows.Forms.TextBox();
            this.logla = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.exit_manage = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Button_OpenDB
            // 
            this.Button_OpenDB.Location = new System.Drawing.Point(26, 54);
            this.Button_OpenDB.Margin = new System.Windows.Forms.Padding(4);
            this.Button_OpenDB.Name = "Button_OpenDB";
            this.Button_OpenDB.Size = new System.Drawing.Size(133, 29);
            this.Button_OpenDB.TabIndex = 0;
            this.Button_OpenDB.Text = "打开数据库";
            this.Button_OpenDB.UseVisualStyleBackColor = true;
            this.Button_OpenDB.Click += new System.EventHandler(this.Button_OpenDB_Click);
            // 
            // Button_CloseDB
            // 
            this.Button_CloseDB.Enabled = false;
            this.Button_CloseDB.Location = new System.Drawing.Point(26, 90);
            this.Button_CloseDB.Margin = new System.Windows.Forms.Padding(4);
            this.Button_CloseDB.Name = "Button_CloseDB";
            this.Button_CloseDB.Size = new System.Drawing.Size(133, 29);
            this.Button_CloseDB.TabIndex = 1;
            this.Button_CloseDB.Text = "关闭数据库";
            this.Button_CloseDB.UseVisualStyleBackColor = true;
            this.Button_CloseDB.Click += new System.EventHandler(this.Button_CloseDB_Click);
            // 
            // Button_Query
            // 
            this.Button_Query.Enabled = false;
            this.Button_Query.Location = new System.Drawing.Point(168, 54);
            this.Button_Query.Margin = new System.Windows.Forms.Padding(4);
            this.Button_Query.Name = "Button_Query";
            this.Button_Query.Size = new System.Drawing.Size(133, 29);
            this.Button_Query.TabIndex = 2;
            this.Button_Query.Text = "查询数据";
            this.Button_Query.UseVisualStyleBackColor = true;
            this.Button_Query.Click += new System.EventHandler(this.Button_Query_Click);
            // 
            // Button_Add
            // 
            this.Button_Add.Enabled = false;
            this.Button_Add.Location = new System.Drawing.Point(168, 90);
            this.Button_Add.Margin = new System.Windows.Forms.Padding(4);
            this.Button_Add.Name = "Button_Add";
            this.Button_Add.Size = new System.Drawing.Size(133, 29);
            this.Button_Add.TabIndex = 4;
            this.Button_Add.Text = "插入数据";
            this.Button_Add.UseVisualStyleBackColor = true;
            this.Button_Add.Click += new System.EventHandler(this.Button_Add_Click);
            // 
            // Button_Modify
            // 
            this.Button_Modify.Enabled = false;
            this.Button_Modify.Location = new System.Drawing.Point(167, 127);
            this.Button_Modify.Margin = new System.Windows.Forms.Padding(4);
            this.Button_Modify.Name = "Button_Modify";
            this.Button_Modify.Size = new System.Drawing.Size(133, 29);
            this.Button_Modify.TabIndex = 5;
            this.Button_Modify.Text = "修改数据";
            this.Button_Modify.UseVisualStyleBackColor = true;
            this.Button_Modify.Click += new System.EventHandler(this.Button_Modify_Click);
            // 
            // Button_Delete
            // 
            this.Button_Delete.Enabled = false;
            this.Button_Delete.Location = new System.Drawing.Point(26, 127);
            this.Button_Delete.Margin = new System.Windows.Forms.Padding(4);
            this.Button_Delete.Name = "Button_Delete";
            this.Button_Delete.Size = new System.Drawing.Size(133, 29);
            this.Button_Delete.TabIndex = 6;
            this.Button_Delete.Text = "删除数据";
            this.Button_Delete.UseVisualStyleBackColor = true;
            this.Button_Delete.Click += new System.EventHandler(this.Button_Delete_Click);
            // 
            // Label_DBOpenStateTitle
            // 
            this.Label_DBOpenStateTitle.AutoSize = true;
            this.Label_DBOpenStateTitle.Location = new System.Drawing.Point(26, 11);
            this.Label_DBOpenStateTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label_DBOpenStateTitle.Name = "Label_DBOpenStateTitle";
            this.Label_DBOpenStateTitle.Size = new System.Drawing.Size(127, 15);
            this.Label_DBOpenStateTitle.TabIndex = 7;
            this.Label_DBOpenStateTitle.Text = "数据库打开状态：";
            // 
            // Label_DBOpenState
            // 
            this.Label_DBOpenState.AutoSize = true;
            this.Label_DBOpenState.Location = new System.Drawing.Point(170, 11);
            this.Label_DBOpenState.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label_DBOpenState.Name = "Label_DBOpenState";
            this.Label_DBOpenState.Size = new System.Drawing.Size(37, 15);
            this.Label_DBOpenState.TabIndex = 8;
            this.Label_DBOpenState.Text = "关闭";
            // 
            // Button_Table
            // 
            this.Button_Table.Enabled = false;
            this.Button_Table.Location = new System.Drawing.Point(75, 164);
            this.Button_Table.Margin = new System.Windows.Forms.Padding(4);
            this.Button_Table.Name = "Button_Table";
            this.Button_Table.Size = new System.Drawing.Size(147, 29);
            this.Button_Table.TabIndex = 9;
            this.Button_Table.Text = "查询所有数据";
            this.Button_Table.UseVisualStyleBackColor = true;
            this.Button_Table.Click += new System.EventHandler(this.Button_TableExists_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.identityla);
            this.groupBox1.Controls.Add(this.pwdla);
            this.groupBox1.Controls.Add(this.nameld);
            this.groupBox1.Controls.Add(this.IDla);
            this.groupBox1.Controls.Add(this.textBoxidentity);
            this.groupBox1.Controls.Add(this.textBoxpwd);
            this.groupBox1.Controls.Add(this.textBoxname);
            this.groupBox1.Controls.Add(this.textBoxid);
            this.groupBox1.Location = new System.Drawing.Point(319, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(205, 195);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "信息窗口";
            // 
            // identityla
            // 
            this.identityla.AutoSize = true;
            this.identityla.Location = new System.Drawing.Point(7, 141);
            this.identityla.Name = "identityla";
            this.identityla.Size = new System.Drawing.Size(37, 15);
            this.identityla.TabIndex = 1;
            this.identityla.Text = "身份";
            // 
            // pwdla
            // 
            this.pwdla.AutoSize = true;
            this.pwdla.Location = new System.Drawing.Point(6, 110);
            this.pwdla.Name = "pwdla";
            this.pwdla.Size = new System.Drawing.Size(37, 15);
            this.pwdla.TabIndex = 1;
            this.pwdla.Text = "密码";
            // 
            // nameld
            // 
            this.nameld.AutoSize = true;
            this.nameld.Location = new System.Drawing.Point(7, 68);
            this.nameld.Name = "nameld";
            this.nameld.Size = new System.Drawing.Size(37, 15);
            this.nameld.TabIndex = 1;
            this.nameld.Text = "姓名";
            // 
            // IDla
            // 
            this.IDla.AutoSize = true;
            this.IDla.Location = new System.Drawing.Point(7, 25);
            this.IDla.Name = "IDla";
            this.IDla.Size = new System.Drawing.Size(23, 15);
            this.IDla.TabIndex = 1;
            this.IDla.Text = "ID";
            // 
            // textBoxidentity
            // 
            this.textBoxidentity.Location = new System.Drawing.Point(80, 138);
            this.textBoxidentity.Name = "textBoxidentity";
            this.textBoxidentity.Size = new System.Drawing.Size(100, 25);
            this.textBoxidentity.TabIndex = 0;
            // 
            // textBoxpwd
            // 
            this.textBoxpwd.Location = new System.Drawing.Point(80, 102);
            this.textBoxpwd.Name = "textBoxpwd";
            this.textBoxpwd.Size = new System.Drawing.Size(100, 25);
            this.textBoxpwd.TabIndex = 0;
            // 
            // textBoxname
            // 
            this.textBoxname.Location = new System.Drawing.Point(80, 65);
            this.textBoxname.Name = "textBoxname";
            this.textBoxname.Size = new System.Drawing.Size(100, 25);
            this.textBoxname.TabIndex = 0;
            // 
            // textBoxid
            // 
            this.textBoxid.Location = new System.Drawing.Point(80, 20);
            this.textBoxid.Name = "textBoxid";
            this.textBoxid.Size = new System.Drawing.Size(100, 25);
            this.textBoxid.TabIndex = 0;
            // 
            // logla
            // 
            this.logla.AutoSize = true;
            this.logla.Location = new System.Drawing.Point(26, 206);
            this.logla.Name = "logla";
            this.logla.Size = new System.Drawing.Size(47, 15);
            this.logla.TabIndex = 13;
            this.logla.Text = "Logs:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(22, 224);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(586, 279);
            this.dataGridView1.TabIndex = 14;
            // 
            // exit_manage
            // 
            this.exit_manage.Location = new System.Drawing.Point(540, 71);
            this.exit_manage.Name = "exit_manage";
            this.exit_manage.Size = new System.Drawing.Size(68, 80);
            this.exit_manage.TabIndex = 15;
            this.exit_manage.Text = "退出管理界面";
            this.exit_manage.UseVisualStyleBackColor = true;
            this.exit_manage.Click += new System.EventHandler(this.exit_manage_Click);
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.Button_Query);
            this.panel1.Controls.Add(this.exit_manage);
            this.panel1.Controls.Add(this.Button_OpenDB);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.Button_CloseDB);
            this.panel1.Controls.Add(this.logla);
            this.panel1.Controls.Add(this.Button_Add);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.Button_Modify);
            this.panel1.Controls.Add(this.Button_Table);
            this.panel1.Controls.Add(this.Button_Delete);
            this.panel1.Controls.Add(this.Label_DBOpenState);
            this.panel1.Controls.Add(this.Label_DBOpenStateTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(659, 521);
            this.panel1.TabIndex = 16;
            this.panel1.SizeChanged += new System.EventHandler(this.frmMain_SizeChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(659, 521);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form1_Load);
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

        private System.Windows.Forms.Button Button_OpenDB;
        private System.Windows.Forms.Button Button_CloseDB;
        private System.Windows.Forms.Button Button_Query;
        private System.Windows.Forms.Button Button_Add;
        private System.Windows.Forms.Button Button_Modify;
        private System.Windows.Forms.Button Button_Delete;
        private System.Windows.Forms.Label Label_DBOpenStateTitle;
        private System.Windows.Forms.Label Label_DBOpenState;
        private System.Windows.Forms.Button Button_Table;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label identityla;
        private System.Windows.Forms.Label pwdla;
        private System.Windows.Forms.Label nameld;
        private System.Windows.Forms.Label IDla;
        private System.Windows.Forms.TextBox textBoxidentity;
        private System.Windows.Forms.TextBox textBoxpwd;
        private System.Windows.Forms.TextBox textBoxname;
        private System.Windows.Forms.TextBox textBoxid;
        private System.Windows.Forms.Label logla;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button exit_manage;
        private System.Windows.Forms.Panel panel1;
    }
}

