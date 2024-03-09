using MySQLiteHelper;
using System;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace SQLiteDemo
{
    public partial class Login : Form
    {
        private SQLiteHelper SQLiteHelpers = null;
        private const string DBAddress = "mydb.db";

        
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
                    item.Font = new Font(item.Font.Name, float.Parse((pos[4] * Math.Min(scaleX, scaleY)).ToString()));//字体
                }
            }

        }
        public Login()
        {
            InitializeComponent();
            #region 窗体缩放
            GetAllInitInfo(this.Controls[0]);
            #endregion
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SQLiteHelpers.Open();
            if (this.textname.Text == "" || this.textname.Text == "")
            {
                MessageBox.Show("请你输入你的用户名或密码！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                SQLiteParameter[] parameter = new SQLiteParameter[]
                {
                    new SQLiteParameter("Name", this.textname.Text.ToString().Trim()),
                    new SQLiteParameter("Pwd", this.textpwd.Text.ToString().Trim()),
                    new SQLiteParameter("ID", 0.ToString())
                };

                string sql = "SELECT * FROM table1 WHERE Pwd = @Pwd AND Name = @Name AND ID=@ID";
                //DataSet dataSet = SQLiteHelpers.ExecuteDataSet(sql, parameter);
                SQLiteDataReader dr = SQLiteHelpers.ExecuteReader(sql, parameter);
                if (dr.Read())
                {
                    Form1 main = new Form1();
                    main.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("你输入的密码错误，请重新输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }  
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            SQLiteHelpers = new SQLiteHelper(DBAddress);
        }

        private void getinbtn_Click(object sender, EventArgs e)
        {
            SQLiteHelpers.Open();
            if (this.textname.Text == "" || this.textname.Text == "")
            {
                MessageBox.Show("请你输入你的用户名或密码！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                SQLiteParameter[] parameter = new SQLiteParameter[]
                {
                    new SQLiteParameter("Name", this.textname.Text.ToString().Trim()),
                    new SQLiteParameter("Pwd", this.textpwd.Text.ToString().Trim())
                };

                string sql = "SELECT * FROM table1 WHERE Pwd = @Pwd and Name = @Name";
                //DataSet dataSet = SQLiteHelpers.ExecuteDataSet(sql, parameter);
                SQLiteDataReader dr = SQLiteHelpers.ExecuteReader(sql, parameter);
                if (dr.Read())
                {
                    find side = new find();
                    side.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("你输入的密码错误，请重新输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
