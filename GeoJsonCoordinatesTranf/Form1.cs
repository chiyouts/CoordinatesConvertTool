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
        }

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
            JObject jsonData = JObject.Parse(sr.ReadToEnd());

            List<JToken> features = jsonData["features"].ToList();

            for (int i = 0; i < features.Count; i++)
            {
                if (!string.IsNullOrEmpty(features[i]["geometry"].ToString()))
                {
                    string geometryType = features[i]["geometry"]["type"].ToString();
                    string[] convertedLngLatList;
                    switch (geometryType)
                    {
                        case "Point":
                            convertedLngLatList = ConvertCoord(features[i]["geometry"]["coordinates"].ToList());
                            features[i]["geometry"]["coordinates"][0] = convertedLngLatList[0];
                            features[i]["geometry"]["coordinates"][1] = convertedLngLatList[1];
                            break;
                        case "MultiPoint":
                            List<JToken> mpCoordinatesList = features[i]["geometry"]["coordinates"].ToList();
                            for (int j = 0; j < mpCoordinatesList.Count; j++)
                            {
                                convertedLngLatList = ConvertCoord(mpCoordinatesList[j].ToList());
                                mpCoordinatesList[j][0] = convertedLngLatList[0];
                                mpCoordinatesList[j][1] = convertedLngLatList[1];
                            }
                            break;
                        case "LineString":
                            List<JToken> lsCoordinatesList = features[i]["geometry"]["coordinates"].ToList();
                            for (int j = 0; j < lsCoordinatesList.Count; j++)
                            {
                                convertedLngLatList = ConvertCoord(lsCoordinatesList[j].ToList());
                                lsCoordinatesList[j][0] = convertedLngLatList[0];
                                lsCoordinatesList[j][1] = convertedLngLatList[1];
                            }
                            break;
                        case "MultiLineString":
                            List<JToken> msCoordinatesList = features[i]["geometry"]["coordinates"].ToList();
                            for (int j = 0; j < msCoordinatesList.Count; j++)
                            {
                                List<JToken> coor = msCoordinatesList[j].ToList();
                                for (int k = 0; k < coor.Count; k++)
                                {
                                    convertedLngLatList = ConvertCoord(coor[k].ToList());
                                    coor[k][0] = convertedLngLatList[0];
                                    coor[k][1] = convertedLngLatList[1];
                                }
                            }
                            break;
                        case "Polygon":
                            List<JToken> pCoordinatesList = features[i]["geometry"]["coordinates"].ToList();
                            for (int j = 0; j < pCoordinatesList.Count; j++)
                            {
                                List<JToken> coor = pCoordinatesList[j].ToList();
                                for (int k = 0; k < coor.Count; k++)
                                {
                                    convertedLngLatList = ConvertCoord(coor[k].ToList());
                                    coor[k][0] = convertedLngLatList[0];
                                    coor[k][1] = convertedLngLatList[1];
                                }
                            }
                            break;
                        case "MultiPolygon":
                            List<JToken> mpcoordinatesList = features[i]["geometry"]["coordinates"].ToList();
                            for (int g = 0; g < mpcoordinatesList.Count; g++)
                            {
                                List<JToken> coorList = mpcoordinatesList[g].ToList();
                                for (int j = 0; j < coorList.Count; j++)
                                {
                                    List<JToken> coor = coorList[j].ToList();
                                    for (int k = 0; k < coor.Count; k++)
                                    {
                                        convertedLngLatList = ConvertCoord(coor[k].ToList());
                                        coor[k][0] = convertedLngLatList[0];
                                        coor[k][1] = convertedLngLatList[1];
                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            ChangeResultTextBox.Text = jsonData.ToString();
            m_changeResultText = jsonData.ToString();
            MessageBox.Show("转换完成");
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

        #region CheckBox
        private void WgsToGcj_CheckedChanged(object sender, EventArgs e)
        {
            if (WgsToGcj.Checked)
            {
                if (!m_changeTypeList.Contains(WgsToGcj))
                {
                    m_changeTypeList.Add(WgsToGcj);
                }
                m_changeType = WgsToGcj.Name;
                ChangeCheckBoxState(m_changeType);
            }
            else
            {
                m_changeType = string.Empty;
            }
        }

        private void GcjToWgs_CheckedChanged(object sender, EventArgs e)
        {
            if (GcjToWgs.Checked)
            {
                if (!m_changeTypeList.Contains(GcjToWgs))
                {
                    m_changeTypeList.Add(GcjToWgs);
                }
                m_changeType = GcjToWgs.Name;
                ChangeCheckBoxState(m_changeType);
            }
            else
            {
                m_changeType = string.Empty;
            }
        }

        private void WgsToBd_CheckedChanged(object sender, EventArgs e)
        {
            if (WgsToBd.Checked)
            {
                if (!m_changeTypeList.Contains(WgsToBd))
                {
                    m_changeTypeList.Add(WgsToBd);
                }
                m_changeType = WgsToBd.Name;
                ChangeCheckBoxState(m_changeType);
            }
            else
            {
                m_changeType = string.Empty;
            }
        }

        private void GcjToBd_CheckedChanged(object sender, EventArgs e)
        {
            if (GcjToBd.Checked)
            {
                if (!m_changeTypeList.Contains(GcjToBd))
                {
                    m_changeTypeList.Add(GcjToBd);
                }
                m_changeType = GcjToBd.Name;
                ChangeCheckBoxState(m_changeType);
            }
            else
            {
                m_changeType = string.Empty;
            }
        }

        private void BdToWgs_CheckedChanged(object sender, EventArgs e)
        {
            if (BdToWgs.Checked)
            {
                if (!m_changeTypeList.Contains(BdToWgs))
                {
                    m_changeTypeList.Add(BdToWgs);
                }
                m_changeType = BdToWgs.Name;
                ChangeCheckBoxState(m_changeType);
            }
            else
            {
                m_changeType = string.Empty;
            }
        }

        private void BdToGcj_CheckedChanged(object sender, EventArgs e)
        {
            if (BdToGcj.Checked)
            {
                if (!m_changeTypeList.Contains(BdToGcj))
                {
                    m_changeTypeList.Add(BdToGcj);
                }
                m_changeType = BdToGcj.Name;
                ChangeCheckBoxState(m_changeType);
            }
            else
            {
                m_changeType = string.Empty;
            }
        }

        private void ChangeCheckBoxState(string checkedName)
        {
            for (int i = 0; i < m_changeTypeList.Count; i++)
            {
                if (m_changeTypeList[i].Name.Equals(checkedName))
                {
                    m_changeTypeList[i].CheckState = CheckState.Checked;
                }
                else
                {
                    m_changeTypeList[i].CheckState = CheckState.Unchecked;
                }
            }
        }
        #endregion


        #region 转换坐标


        /// <summary>
        /// 转换坐标
        /// </summary>
        /// <param name="coord"></param>
        /// <returns></returns>
        private string[] ConvertCoord(string[] coord)
        {
            string[] result = coord;
            PointLatLng pos = new PointLatLng(double.Parse(coord[1]), double.Parse(coord[0]));
            PointLatLng newPos = new PointLatLng(0, 0);
            switch (m_changeType)
            {
                case "WgsToGcj":
                    newPos = ConvertGPS.gps84_To_Gcj02(pos);
                    result[0] = newPos.Lng.ToString();
                    result[1] = newPos.Lat.ToString();
                    break;
                case "WgsToBd":
                    newPos = ConvertGPS.Gps84_To_bd09(pos);
                    result[0] = newPos.Lng.ToString();
                    result[1] = newPos.Lat.ToString();
                    break;
                case "GcjToWgs":
                    newPos = ConvertGPS.gcj02_To_Gps84(pos);
                    result[0] = newPos.Lng.ToString();
                    result[1] = newPos.Lat.ToString();
                    break;
                case "GcjToBd":
                    newPos = ConvertGPS.gcj02_To_Bd09(pos);
                    result[0] = newPos.Lng.ToString();
                    result[1] = newPos.Lat.ToString();
                    break;
                case "BdToWgs":
                    newPos = ConvertGPS.bd09_To_Gps84(pos);
                    result[0] = newPos.Lng.ToString();
                    result[1] = newPos.Lat.ToString();
                    break;
                case "BdToGcj":
                    newPos = ConvertGPS.bd09_To_Gcj02(pos);
                    result[0] = newPos.Lng.ToString();
                    result[1] = newPos.Lat.ToString();
                    break;
                default:
                    break;
            }
            return result;
        }

        /// <summary>
        /// 转换坐标
        /// </summary>
        /// <param name="coord"></param>
        /// <returns></returns>
        private string[] ConvertCoord(List<JToken> coord)
        {
            string[] result = new string[coord.Count];
            result[0] = coord[0].ToString();
            result[1] = coord[1].ToString();
            PointLatLng pos = new PointLatLng(double.Parse(result[1]), double.Parse(result[0]));
            PointLatLng newPos = new PointLatLng(0, 0);
            switch (m_changeType)
            {
                case "WgsToGcj":
                    newPos = ConvertGPS.gps84_To_Gcj02(pos);
                    result[0] = newPos.Lng.ToString();
                    result[1] = newPos.Lat.ToString();
                    break;
                case "WgsToBd":
                    newPos = ConvertGPS.Gps84_To_bd09(pos);
                    result[0] = newPos.Lng.ToString();
                    result[1] = newPos.Lat.ToString();
                    break;
                case "GcjToWgs":
                    newPos = ConvertGPS.gcj02_To_Gps84(pos);
                    result[0] = newPos.Lng.ToString();
                    result[1] = newPos.Lat.ToString();
                    break;
                case "GcjToBd":
                    newPos = ConvertGPS.gcj02_To_Bd09(pos);
                    result[0] = newPos.Lng.ToString();
                    result[1] = newPos.Lat.ToString();
                    break;
                case "BdToWgs":
                    newPos = ConvertGPS.bd09_To_Gps84(pos);
                    result[0] = newPos.Lng.ToString();
                    result[1] = newPos.Lat.ToString();
                    break;
                case "BdToGcj":
                    newPos = ConvertGPS.bd09_To_Gcj02(pos);
                    result[0] = newPos.Lng.ToString();
                    result[1] = newPos.Lat.ToString();
                    break;
                default:
                    break;
            }
            return result;
        }
        #endregion
    }
}