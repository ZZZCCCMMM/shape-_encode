using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace SQLiteDemo
{
    public partial class find : Form
    {
        HTuple handle, Width1, Height1;
        HObject ho_Image;
        HTuple hv_Width = new HTuple(), hv_Height = new HTuple();
        HTuple hv_M_result = new HTuple();
        // Local iconic variables 

        HObject ho_ContCircle, ho_Image2;

        // Local control variables 

        HTuple hv_ModelID = new HTuple(), hv_M_recult = new HTuple();
        HTuple hv__Phi = new HTuple();
        bool read_signal = false;
        bool create_signal = false;
        bool identity_signal = false;
        bool check_signal = false;

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


        private void checkbtn_Click(object sender, EventArgs e)
        {
            if (identity_signal == false)
            {
                MessageBox.Show("未能成功识别图片");
            }
            else
            {
                string sb = "定位为";
                foreach (Control cl in groupBox1.Controls)//，与上面的区别在这里哦——循环groupBox1上的控件
                {
                    if (cl is CheckBox)//看看是不是checkbox
                    {
                        CheckBox ck = cl as CheckBox;//将找到的control转化成checkbox
                        if (ck.Checked)//判断是否选中
                        {
                            sb += ck.Text + ",";
                            if (ck.Text == "左上角")
                            {
                                hv_M_result[0] = 1;
                                hv_M_result[1] = 1;
                                hv_M_result[11] = 1;
                            }
                            else if (ck.Text == "左下角")
                            {
                                hv_M_result[101] = 1;
                                hv_M_result[111] = 1;
                                hv_M_result[112] = 1;
                            }
                            else if (ck.Text == "右上角")
                            {
                                hv_M_result[10] = 1;
                                hv_M_result[11] = 1;
                                hv_M_result[21] = 1;
                            }
                            else
                            {
                                hv_M_result[100] = 1;
                                hv_M_result[119] = 1;
                                hv_M_result[120] = 1;
                            }

                        }
                    }
                }

                angle_corner.Text = "" + sb.ToString();
                DataTable dt = new DataTable("Datas");
                DataColumn dc = new DataColumn();
                dc.AutoIncrement = true;//自动增加
                dc.AutoIncrementSeed = 1;//起始为1
                dc.AutoIncrementStep = 1;//步长为1
                dc.AllowDBNull = false;//是否允许空值

                //添加列
                //for (int i = 0;i<11;i++)
                //{
                //dt.Columns.Add();
                //}

                //添加行
                //for (int i = 0;i<11;i++)
                //{
                dt.Columns.Add(hv_M_result.ToString());
                //dt.Rows.Add(hv_M_result[i*10+1], hv_M_result[i*10+1], hv_M_recult[i*10+2],
                //    hv_M_result[i*10+3], hv_M_result[i*10+4], hv_M_result[i*10+5], hv_M_result[i*10+6],
                //    hv_M_result[i*10+7], hv_M_result[i*10+8], hv_M_result[i*10+9], hv_M_result[i*10+10]);
                //}
                dataGridView1.DataSource = dt;
                richTextBoxresult.Text = hv_M_result.ToString();
                check_signal = true;
            }
            

        }

        private void findbtn_Click(object sender, EventArgs e)
        {
            if (create_signal == false)
            {
                MessageBox.Show("未能成功创建模型");
            }
            else
            {
                hv_M_recult.Dispose();
                find_model(ho_Image2, out hv_M_recult);
                identity_signal = true;
            }
        }

        private void createbtn_Click(object sender, EventArgs e)
        {
            HOperatorSet.GenEmptyObj(out ho_ContCircle);
            ho_ContCircle.Dispose(); hv_ModelID.Dispose();
            if (read_signal == false)
            {
                MessageBox.Show("未能成功读入图片");
            }
            else
            {
                create_model(out ho_ContCircle, out hv_ModelID);
                create_signal = true;
            }
            
        }

        private void excelbtn_Click(object sender, EventArgs e)
        {
            if (check_signal == false)
            {
                MessageBox.Show("未能成功定位图片位置");
            }
            else
            {
                string a = "D:\\";
                ExportExcels(a, dataGridView1);
            }
            
        }

        private void readbtn_Click(object sender, EventArgs e)
        {
            HOperatorSet.GenEmptyObj(out ho_Image);
            //循环读图和推理
            ho_Image.Dispose();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "图片文件|*.bmp; *.pcx; *.png; *.jpg; *.gif;*.tif; *.ico; *.dxf; *.cgm; *.cdr; *.wmf; *.eps; *.emf";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                HOperatorSet.ReadImage(out ho_Image2, filePath);
                //拿到实际图片大小
                hv_Width.Dispose(); hv_Height.Dispose();
                HOperatorSet.GetImageSize(ho_Image2, out hv_Width, out hv_Height);
                HOperatorSet.SetPart(handle, 0, 0, hv_Height - 1, hv_Width - 1);
                HDevWindowStack.Push(handle);
                HOperatorSet.DispObj(ho_Image2, handle);
                read_signal = true;
            }
        }

        private void ExportToTxt(DataGridView dataGridView, string filePath)
        {
            if (check_signal == false)
            {
                MessageBox.Show("未能成功定位图片位置");
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    // 写入列标题
                    for (int i = 0; i < dataGridView.Columns.Count; i++)
                    {
                        sw.Write(dataGridView.Columns[i].HeaderText);
                        if (i < dataGridView.Columns.Count - 1)
                        {
                            sw.Write("\t");
                        }
                    }
                    sw.WriteLine();

                    // 写入数据行
                    for (int row = 0; row < dataGridView.Rows.Count; row++)
                    {
                        for (int col = 0; col < dataGridView.Columns.Count; col++)
                        {
                            sw.Write(dataGridView.Rows[row].Cells[col].Value);
                            if (col < dataGridView.Columns.Count - 1)
                            {
                                sw.Write("\t");
                            }
                        }
                        sw.WriteLine();
                    }
                }
            }
            
        }

        private void button_txt_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable("Datas");
            DataColumn dc = new DataColumn();
            dc.AutoIncrement = true;//自动增加
            dc.AutoIncrementSeed = 1;//起始为1
            dc.AutoIncrementStep = 1;//步长为1
            dc.AllowDBNull = false;//是否允许空值
            dt.Columns.Add(hv_M_result.ToString());
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ExportToTxt(dataGridView1, saveFileDialog.FileName);
                MessageBox.Show("导出成功！");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (read_signal == false)
            {
                MessageBox.Show("未能成功读入图片");
            }
            else
            {
                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.ClearWindow(HDevWindowStack.GetActive());
                }
                HOperatorSet.RotateImage(ho_Image2, out ho_Image2, 90, "constant");
                HOperatorSet.GetImageSize(ho_Image2, out hv_Width, out hv_Height);
                HOperatorSet.SetPart(handle, 0, 0, hv_Height - 1, hv_Width - 1);
                HOperatorSet.DispObj(ho_Image2, HDevWindowStack.GetActive());
            }
        }

        private void backbtn_Click(object sender, EventArgs e)
        {
            Login side = new Login();
            side.Show();
            this.Hide();
        }

        public find()
        {
            InitializeComponent();
            handle = hWindowControl1.HalconWindow;
            HOperatorSet.ReadImage(out ho_Image, "目标定位背景");
            HOperatorSet.GetImageSize(ho_Image, out Width1, out Height1);
            HOperatorSet.SetPart(handle, 0, 0, Height1 - 1, Width1 - 1);
            HOperatorSet.DispObj(ho_Image, handle);
            #region 窗体缩放
            GetAllInitInfo(this.Controls[0]);
            #endregion


        }
        private void ExportExcels(string fileName, DataGridView myDGV)
        {
            string saveFileName = "";
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xls";
            saveDialog.Filter = "Excel文件|*.xls";
            saveDialog.FileName = fileName;
            saveDialog.ShowDialog();
            saveFileName = saveDialog.FileName;
            if (saveFileName.IndexOf(":") < 0) return; //被点了取消
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            if (xlApp == null)
            {
                MessageBox.Show("无法创建Excel对象，可能您的机子未安装Excel");
                return;
            }
            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];//取得sheet1
                                                                                                                                  //写入标题
            for (int i = 0; i < myDGV.ColumnCount; i++)
            {
                worksheet.Cells[1, i + 1] = myDGV.Columns[i].HeaderText;
            }
            //写入数值
            for (int r = 0; r < myDGV.Rows.Count; r++)
            {
                for (int i = 0; i < myDGV.ColumnCount; i++)
                {
                    worksheet.Cells[r + 2, i + 1] = myDGV.Rows[r].Cells[i].Value;
                }
                System.Windows.Forms.Application.DoEvents();
            }
            worksheet.Columns.EntireColumn.AutoFit();//列宽自适应
            if (saveFileName != "")
            {
                try
                {
                    workbook.Saved = true;
                    workbook.SaveCopyAs(saveFileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("导出文件时出错,文件可能正被打开！\n" + ex.Message);
                }
            }
            xlApp.Quit();
            GC.Collect();//强行销毁
            MessageBox.Show("文件： " + fileName + ".xls 保存成功", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void create_model(out HObject ho_ContCircle, out HTuple hv_ModelID)
        {
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_ContCircle);
            hv_ModelID = new HTuple();
            ho_ContCircle.Dispose();
            HOperatorSet.GenCircleContourXld(out ho_ContCircle, 204.42, 570.771, 28.8374,
                0, 6.28318, "positive", 1);
            //轮廓模板
            hv_ModelID.Dispose();
            HOperatorSet.CreateGenericShapeModel(out hv_ModelID);
            HOperatorSet.SetGenericShapeModelParam(hv_ModelID, "iso_scale_max", 1.0);
            HOperatorSet.SetGenericShapeModelParam(hv_ModelID, "iso_scale_min", 0.9);
            HOperatorSet.TrainGenericShapeModel(ho_ContCircle, hv_ModelID);
            HOperatorSet.WriteShapeModel(hv_ModelID, "model.shm");
            return;
        }



        public void find_model(HObject ho_Image2, out HTuple hv_M_recult)
        {
            // Local iconic variables 

            HObject ho_ImageEmphasize, ho_Objects, ho_Region;
            HObject ho_SortedRegions, ho_ObjectSelected = null;

            // Local control variables 

            HTuple hv_Width = new HTuple(), hv_Height = new HTuple();
            HTuple hv_ShapeModel3DID = new HTuple(), hv_MatchResultID = new HTuple();
            HTuple hv_NumMatchResult = new HTuple(), hv_Row = new HTuple();
            HTuple hv_Column = new HTuple(), hv__I = new HTuple();
            HTuple hv_tuple_num = new HTuple(), hv_Max = new HTuple();
            HTuple hv_Min = new HTuple(), hv_T_start = new HTuple();
            HTuple hv_T_end = new HTuple(), hv_num = new HTuple();
            HTuple hv_Index = new HTuple(), hv_Row_c = new HTuple();
            HTuple hv_Column_c = new HTuple(), hv_Radius = new HTuple();
            HTuple hv_I = new HTuple(), hv_Indices = new HTuple();
            HTuple hv_k = new HTuple(), hv_result = new HTuple(), hv_i = new HTuple();
            HTuple hv_j = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_ImageEmphasize);
            HOperatorSet.GenEmptyObj(out ho_Objects);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_SortedRegions);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelected);
            hv_M_recult = new HTuple();
            //if (HDevWindowStack.IsOpen())
            //{
            //    HOperatorSet.ClearWindow(HDevWindowStack.GetActive());
            //}

            hv_Width.Dispose(); hv_Height.Dispose();
            HOperatorSet.GetImageSize(ho_Image2, out hv_Width, out hv_Height);
            ho_ImageEmphasize.Dispose();
            HOperatorSet.Emphasize(ho_Image2, out ho_ImageEmphasize, 5, 5, 5);
            hv_ShapeModel3DID.Dispose();
            HOperatorSet.ReadShapeModel("model.shm", out hv_ShapeModel3DID);
            HOperatorSet.SetGenericShapeModelParam(hv_ShapeModel3DID, "restrict_iso_scale_max",
                1.0);
            HOperatorSet.SetGenericShapeModelParam(hv_ShapeModel3DID, "restrict_iso_scale_min",
                0.9);
            HOperatorSet.SetGenericShapeModelParam(hv_ShapeModel3DID, "max_deformation",
                2);
            HOperatorSet.SetGenericShapeModelParam(hv_ShapeModel3DID, "max_overlap", 0.3);
            HOperatorSet.SetGenericShapeModelParam(hv_ShapeModel3DID, "angle_start", -0.39);
            HOperatorSet.SetGenericShapeModelParam(hv_ShapeModel3DID, "angle_end", 0.39);
            HOperatorSet.SetGenericShapeModelParam(hv_ShapeModel3DID, "min_score", 0.80);
            hv_MatchResultID.Dispose(); hv_NumMatchResult.Dispose();
            HOperatorSet.FindGenericShapeModel(ho_ImageEmphasize, hv_ShapeModel3DID, out hv_MatchResultID,
                out hv_NumMatchResult);
            ho_Objects.Dispose();
            HOperatorSet.GetGenericShapeModelResultObject(out ho_Objects, hv_MatchResultID,
                "all", "contours");
            HOperatorSet.DispObj(ho_Objects, handle);
            hv_Row.Dispose();
            HOperatorSet.GetGenericShapeModelResult(hv_MatchResultID, "all", "row", out hv_Row);
            hv_Column.Dispose();
            HOperatorSet.GetGenericShapeModelResult(hv_MatchResultID, "all", "column", out hv_Column);
            hv__I.Dispose();
            hv__I = new HTuple();
            hv_tuple_num.Dispose();
            hv_tuple_num = new HTuple();
            hv_Max.Dispose();
            HOperatorSet.TupleMax(hv_Column, out hv_Max);
            hv_Min.Dispose();
            HOperatorSet.TupleMin(hv_Column, out hv_Min);
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_T_start.Dispose();
                HOperatorSet.TupleGenSequence(hv_Min - (hv_Max / 22), (hv_Max + (hv_Max / 22)) + 10, hv_Max / 11,
                    out hv_T_start);
            }
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                {
                    HTuple
                      ExpTmpLocalVar_T_start = hv_T_start.TupleSelectRange(
                        0, 10);
                    hv_T_start.Dispose();
                    hv_T_start = ExpTmpLocalVar_T_start;
                }
            }
            hv_T_end.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_T_end = hv_T_start + (hv_Max / 11);
            }
            ho_Region.Dispose();
            HOperatorSet.GenRegionContourXld(ho_Objects, out ho_Region, "filled");
            ho_SortedRegions.Dispose();
            HOperatorSet.SortRegion(ho_Region, out ho_SortedRegions, "character", "true",
                "row");
            hv_num.Dispose();
            HOperatorSet.CountObj(ho_SortedRegions, out hv_num);
            HTuple end_val26 = hv_num;
            HTuple step_val26 = 1;
            for (hv_Index = 1; hv_Index.Continue(end_val26, step_val26); hv_Index = hv_Index.TupleAdd(step_val26))
            {
                ho_ObjectSelected.Dispose();
                HOperatorSet.SelectObj(ho_SortedRegions, out ho_ObjectSelected, hv_Index);
                hv_Row_c.Dispose(); hv_Column_c.Dispose(); hv_Radius.Dispose();
                HOperatorSet.SmallestCircle(ho_ObjectSelected, out hv_Row_c, out hv_Column_c,
                    out hv_Radius);
                for (hv_I = 0; (int)hv_I <= (int)((new HTuple(hv_T_start.TupleLength())) - 1); hv_I = (int)hv_I + 1)
                {
                    if ((int)((new HTuple(((hv_T_start.TupleSelect(hv_I))).TupleLess(hv_Column_c))).TupleAnd(
                        new HTuple(hv_Column_c.TupleLess(hv_T_end.TupleSelect(hv_I))))) != 0)
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            {
                                HTuple
                                  ExpTmpLocalVar_tuple_num = hv_tuple_num.TupleConcat(
                                    1);
                                hv_tuple_num.Dispose();
                                hv_tuple_num = ExpTmpLocalVar_tuple_num;
                            }
                        }
                    }
                    else
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            {
                                HTuple
                                  ExpTmpLocalVar_tuple_num = hv_tuple_num.TupleConcat(
                                    0);
                                hv_tuple_num.Dispose();
                                hv_tuple_num = ExpTmpLocalVar_tuple_num;
                            }
                        }
                    }
                }
                hv_Indices.Dispose();
                HOperatorSet.TupleFind(hv_tuple_num, 1, out hv_Indices);
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    {
                        HTuple
                          ExpTmpLocalVar__I = hv__I.TupleConcat(
                            hv_Indices + 1);
                        hv__I.Dispose();
                        hv__I = ExpTmpLocalVar__I;
                    }
                }
                hv_tuple_num.Dispose();
                hv_tuple_num = new HTuple();
            }
            hv_k.Dispose();
            hv_k = 0;
            hv_result.Dispose();
            hv_result = new HTuple();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                {
                    HTuple
                      ExpTmpLocalVar_result = hv_result.TupleConcat(
                        hv__I.TupleSelect(0));
                    hv_result.Dispose();
                    hv_result = ExpTmpLocalVar_result;
                }
            }
            for (hv_i = 1; (int)hv_i <= (int)((new HTuple(hv__I.TupleLength())) - 1); hv_i = (int)hv_i + 1)
            {
                if ((int)(new HTuple(((hv__I.TupleSelect(hv_i))).TupleLess(hv__I.TupleSelect(
                    hv_i - 1)))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_k = hv_k + 1;
                            hv_k.Dispose();
                            hv_k = ExpTmpLocalVar_k;
                        }
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_result = hv_result.TupleConcat(
                                (hv__I.TupleSelect(hv_i)) + (11 * hv_k));
                            hv_result.Dispose();
                            hv_result = ExpTmpLocalVar_result;
                        }
                    }
                }
                else
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_result = hv_result.TupleConcat(
                                (hv__I.TupleSelect(hv_i)) + (11 * hv_k));
                            hv_result.Dispose();
                            hv_result = ExpTmpLocalVar_result;
                        }
                    }
                }
            }
            hv_M_result.Dispose();
            HOperatorSet.TupleGenConst(121, 0, out hv_M_result);
            for (hv_j = 0; (int)hv_j <= (int)((new HTuple(hv_result.TupleLength())) - 1); hv_j = (int)hv_j + 1)
            {
                if (hv_M_result == null)
                    hv_M_result = new HTuple();
                hv_M_result[hv_result.TupleSelect(hv_j)] = 1;
            }

            ho_ImageEmphasize.Dispose();
            ho_Objects.Dispose();
            ho_Region.Dispose();
            ho_SortedRegions.Dispose();
            ho_ObjectSelected.Dispose();

            hv_Width.Dispose();
            hv_Height.Dispose();
            hv_ShapeModel3DID.Dispose();
            hv_MatchResultID.Dispose();
            hv_NumMatchResult.Dispose();
            hv_Row.Dispose();
            hv_Column.Dispose();
            hv__I.Dispose();
            hv_tuple_num.Dispose();
            hv_Max.Dispose();
            hv_Min.Dispose();
            hv_T_start.Dispose();
            hv_T_end.Dispose();
            hv_num.Dispose();
            hv_Index.Dispose();
            hv_Row_c.Dispose();
            hv_Column_c.Dispose();
            hv_Radius.Dispose();
            hv_I.Dispose();
            hv_Indices.Dispose();
            hv_k.Dispose();
            hv_result.Dispose();
            hv_i.Dispose();
            hv_j.Dispose();

            return;
        }

       
    }
}
