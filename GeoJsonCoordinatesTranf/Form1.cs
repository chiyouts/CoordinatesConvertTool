using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GeoJsonCoordinatesTranf
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            m_convertThread = new ConvertThread();
            m_convertThread.setProgressBar += SetProgressBar;
        }

        private ConvertThread m_convertThread;

        private string m_changeType = string.Empty;

        private List<CheckBox> m_changeTypeList = new List<CheckBox>();

        private string m_chooseFileName = string.Empty;

        private string m_changeResultText = string.Empty;

        private void ChooseOringinFilePath_Click(object sender, EventArgs e)
        {
            m_changeResultText = string.Empty;
            ChangeResultTextBox.Clear();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "geojson文件|*.geojson;*.json|所有文件|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                originFilePath.Text = openFileDialog.FileName;
                m_chooseFileName = openFileDialog.FileName.Substring(openFileDialog.FileName.LastIndexOf("\\") + 1);
                m_chooseFileName = m_chooseFileName.Split('.')[0];
            }
            else
            {
                MessageBox.Show("请确认文件路径");
            }
        }

        private void ChangeButton_Click(object sender, EventArgs e)
        {
            if (m_changeType.Equals(string.Empty))
            {
                MessageBox.Show("请选择转换类型");
                return;
            }
            if (m_chooseFileName.Equals(string.Empty))
            {
                MessageBox.Show("请选择需要转换的文件");
                return;
            }
            StreamReader sr = new StreamReader(originFilePath.Text, Encoding.UTF8);
            m_convertThread.StartConvertThread(m_changeType, sr.ReadToEnd(), ConvertOver);
        }


        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (m_changeResultText.Equals(string.Empty))
            {
                MessageBox.Show("未找到转换结果");
                return;
            }

            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            //folder控件描述;
            folderBrowserDialog1.Description = "请选择文件" + m_chooseFileName + ".geojson保存地址：";
            //指定folder根=桌面
            folderBrowserDialog1.RootFolder = Environment.SpecialFolder.Desktop;
            //是否添加新建文件夹的按钮
            folderBrowserDialog1.ShowNewFolderButton = true;


            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectPath = folderBrowserDialog1.SelectedPath;

                //// 创建文件
                FileStream fs = new FileStream(selectPath + "\\" + m_chooseFileName + ".geojson", FileMode.OpenOrCreate, FileAccess.ReadWrite); //可以指定盘符，也可以指定任意文件名
                StreamWriter sw = new StreamWriter(fs); // 创建写入流
                sw.WriteLine(m_changeResultText); // 写入转换后的json
                sw.Close(); //关闭文件

                MessageBox.Show("已保存");
            }
        }

        #region RadioButton

        private void WgsToGcj_CheckedChanged_1(object sender, EventArgs e)
        {
            if (WgsToGcj.Checked)
            {
                m_changeType = WgsToGcj.Name;
            }
        }

        private void WgsToBd_CheckedChanged_1(object sender, EventArgs e)
        {
            if (WgsToBd.Checked)
            {
                m_changeType = WgsToBd.Name;
            }
        }

        private void GcjToWgs_CheckedChanged_1(object sender, EventArgs e)
        {
            if (GcjToWgs.Checked)
            {
                m_changeType = GcjToWgs.Name;
            }
        }

        private void GcjToBd_CheckedChanged_1(object sender, EventArgs e)
        {
            if (GcjToBd.Checked)
            {
                m_changeType = GcjToBd.Name;
            }
        }

        private void BdToWgs_CheckedChanged_1(object sender, EventArgs e)
        {
            if (BdToWgs.Checked)
            {
                m_changeType = BdToWgs.Name;
            }
        }

        private void BdToGcj_CheckedChanged_1(object sender, EventArgs e)
        {
            if (BdToGcj.Checked)
            {
                m_changeType = BdToGcj.Name;
            }
        }
        #endregion
        

        private void SetProgressBar(float progress)
        {
            progress *= 100;
            if (ConvertProgressBar.InvokeRequired)
            {
                Action<int> actionDelegate = (pValue) => { this.ConvertProgressBar.Value = pValue; };
                this.ConvertProgressBar.Invoke(actionDelegate, (int)progress);
            }
        }

        private void ConvertOver(object sender, string result)
        {
            m_changeResultText = result;
            if (ChangeResultTextBox.InvokeRequired)
            {
                Action actionDelegate = () => {
                    this.ChangeResultTextBox.Clear();
                    this.ChangeResultTextBox.AppendText(m_changeResultText);
                };
                this.ChangeResultTextBox.Invoke(actionDelegate);
            }
            MessageBox.Show("转换完成");
            SetProgressBar(0);
        }
    }
}