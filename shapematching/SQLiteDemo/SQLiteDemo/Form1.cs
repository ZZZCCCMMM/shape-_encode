using Microsoft.Office.Interop.Excel;
using MySQLiteHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Drawing;

namespace SQLiteDemo
{
    public partial class Form1 : Form
    {
        #region 控件缩放
        double formWidth;//窗体原始宽度
        double formHeight;//窗体原始高度
        double scaleX;//水平缩放比例
        double scaleY;//垂直缩放比例
        Dictionary<string, string> ControlsInfo = new Dictionary<string, string>();//控件中心Left,Top,控件Width,控件Height,控件字体Size

        #endregion
        private void frmMain_SizeChanged(object sender, EventArgs e)
        {
            if (ControlsInfo.Count > 0)//如果字典中有数据，即窗体改变
            {
                ControlsChangeInit(this.Controls[0]);//表示pannel控件
                ControlsChange(this.Controls[0]);
            }
        }

        protected void GetAllInitInfo(Control ctrlContainer)
        {
            if (ctrlContainer.Parent == this)//获取窗体的高度和宽度
            {
                formWidth = Convert.ToDouble(ctrlContainer.Width);
                formHeight = Convert.ToDouble(ctrlContainer.Height);
            }
            foreach (Control item in ctrlContainer.Controls)
            {
                if (item.Name.Trim() != "")
                {
                    //添加信息：键值：控件名，内容：据左边距离，距顶部距离，控件宽度，控件高度，控件字体。
                    ControlsInfo.Add(item.Name, (item.Left + item.Width / 2) + "," + (item.Top + item.Height / 2) + "," + item.Width + "," + item.Height + "," + item.Font.Size);
                }
                if ((item as UserControl) == null && item.Controls.Count > 0)
                {
                    GetAllInitInfo(item);
                }
            }

        }
        private void ControlsChangeInit(Control ctrlContainer)
        {
            scaleX = (Convert.ToDouble(ctrlContainer.Width) / formWidth);
            scaleY = (Convert.ToDouble(ctrlContainer.Height) / formHeight);
        }
        /// <summary>
        /// 改变控件大小
        /// </summary>
        /// <param name="ctrlContainer"></param>
        private void ControlsChange(Control ctrlContainer)
        {
            double[] pos = new double[5];//pos数组保存当前控件中心Left,Top,控件Width,控件Height,控件字体Size
            foreach (Control item in ctrlContainer.Controls)//遍历控件
            {
                if (item.Name.Trim() != "")//如果控件名不是空，则执行
                {
                    if ((item as UserControl) == null && item.Controls.Count > 0)//如果不是自定义控件
                    {
                        ControlsChange(item);//循环执行
                    }
                    string[] strs = ControlsInfo[item.Name].Split(',');//从字典中查出的数据，以‘，’分割成字符串组

                    for (int i = 0; i < 5; i++)
                    {
                        pos[i] = Convert.ToDouble(strs[i]);//添加到临时数组
                    }
                    double itemWidth = pos[2] * scaleX;     //计算控件宽度，double类型
                    double itemHeight = pos[3] * scaleY;    //计算控件高度
                    item.Left = Convert.ToInt32(pos[0] * scaleX - itemWidth / 2);//计算控件距离左边距离
                    item.Top = Convert.ToInt32(pos[1] * scaleY - itemHeight / 2);//计算控件距离顶部距离
                    item.Width = Convert.ToInt32(itemWidth);//控件宽度，int类型
                    item.Height = Convert.ToInt32(itemHeight);//控件高度
                    item.Font = new System.Drawing.Font(item.Font.Name, float.Parse((pos[4] * Math.Min(scaleX, scaleY)).ToString()));//字体
                }
            }

        }
        public Form1()
        {
            InitializeComponent();
            #region 窗体缩放
            GetAllInitInfo(this.Controls[0]);
            #endregion
        }

        private SQLiteHelper SQLiteHelpers = null;
        private const string DBAddress = "mydb.db";

        private void Form1_Load(object sender, EventArgs e)
        {
            SQLiteHelpers = new SQLiteHelper(DBAddress);
        }

        /// <summary>
        /// 打开数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_OpenDB_Click(object sender, EventArgs e)
        {
            SQLiteHelpers.Open();
            Label_DBOpenState.Text = "打开";
            Button_Query.Enabled = true;
            Button_CloseDB.Enabled = true;
            Button_Add.Enabled = true;
            Button_Delete.Enabled = true;
            Button_Modify.Enabled = true;
            Button_Table.Enabled = true;

        }

        /// <summary>
        /// 关闭数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_CloseDB_Click(object sender, EventArgs e)
        {
            SQLiteHelpers.Close();
            Label_DBOpenState.Text = "关闭";
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Query_Click(object sender, EventArgs e)
        {
            logla.Text = "请填入要询人员的姓名。";
            //textBoxname.Enabled = true;
            try
            {
                SQLiteParameter[] parameter = new SQLiteParameter[]
                {
                    new SQLiteParameter("Name", this.textBoxname.Text.ToString().Trim())
                };
                string sql = "SELECT * FROM table1 WHERE Name = @Name ";
                DataSet dataSet = SQLiteHelpers.ExecuteDataSet(sql, parameter);
                if (dataSet != null)
                {
                    dataGridView1.DataSource = dataSet.Tables[0];
                }
                logla.Text = "查询人员的信息如下";
                //textBoxname.Enabled = false;
            }
            catch (System.Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Add_Click(object sender, EventArgs e)
        {
            logla.Text = "请填入要添加人员的ID，姓名，密码和身份。";
            //textBoxname.Enabled = true;
            //textBoxid.Enabled = true;
            //textBoxidentity.Enabled = true;
            //textBoxpwd.Enabled = true;
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("ID", textBoxid.Text);
                dic.Add("Name", textBoxname.Text.ToString());
                dic.Add("Pwd", textBoxpwd.Text);
                dic.Add("Identity", textBoxidentity.Text);
                int result = SQLiteHelpers.InsertData("table1", dic);
                logla.Text = "已经添加了" + result + "个结果";
                MessageBox.Show("添加成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //textBoxname.Enabled = false;
                //textBoxid.Enabled = false;
                //textBoxidentity.Enabled = false;
                //textBoxpwd.Enabled = false;
            }

            catch (System.Exception)
            {
                MessageBox.Show("未添加成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Modify_Click(object sender, EventArgs e)
        {
            logla.Text = "请填入要修改人员的姓名和密码及要修改的ID及身份。";
            //textBoxname.Enabled = true;
            //textBoxid.Enabled = true;
            //textBoxidentity.Enabled = true;
            //textBoxpwd.Enabled = true;
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("ID", textBoxid.Text);
                dic.Add("Identity", textBoxidentity.Text);
                //where条件
                string where = "Name = @Name AND Pwd = @Pwd";
                //where条件中对应的参数
                SQLiteParameter[] parameter = new SQLiteParameter[]
                {
                new SQLiteParameter("Name", textBoxname.Text.ToString()),
                new SQLiteParameter("Pwd",textBoxpwd.Text)
                };
                int result = SQLiteHelpers.Update("table1", dic, where, parameter);
                logla.Text = ("修改了" + result + "个结果。");
                //textBoxname.Enabled = false;
                //textBoxid.Enabled = false;
                //textBoxidentity.Enabled = false;
                //textBoxpwd.Enabled = false;
            }

            catch (System.Exception)
            {
                MessageBox.Show("未修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Delete_Click(object sender, EventArgs e)
        {
            logla.Text = "请填入要删除人员的姓名和密码。";
            //textBoxname.Enabled = true;
            //textBoxpwd.Enabled = true;
            try
            {
                //where条件
                string where = "Name = @Name AND Pwd = @Pwd";
                //where条件中对应的参数
                SQLiteParameter[] parameter = new SQLiteParameter[]
                {
                 new SQLiteParameter("Name", textBoxname.Text.ToString()),
                new SQLiteParameter("Pwd",textBoxpwd.Text)
                };

                int result = SQLiteHelpers.Delete("table1", where, parameter);
                logla.Text = ("删除了" + result + "个结果。");
                //textBoxname.Enabled = false;
                //textBoxpwd.Enabled = false;
            }

            catch (System.Exception)
            {
                MessageBox.Show("未删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 判断表是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_TableExists_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "SELECT * FROM ";
                DataSet dataSet = SQLiteHelpers.ExecuteDataSet(sql);
                if (dataSet != null)
                {
                    dataGridView1.DataSource = dataSet.Tables[0];
                }
            }
            catch (System.Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void exit_manage_Click(object sender, EventArgs e)
        {
            Login side = new Login();
            side.Show();
            this.Hide();
        }



        //输出各表中的数据
        //public static void PrintValues(DataSet ds)
        //{
        //    foreach (DataTable table in ds.Tables)
        //    {
        //        Console.WriteLine("表名称：" + table.TableName);
        //        foreach (DataRow row in table.Rows)
        //        {
        //            foreach (DataColumn column in table.Columns)
        //            {
        //                Console.Write(row[column] + "");
        //            }
        //            Console.WriteLine();
        //        }
        //    }
        //}

    }
}
