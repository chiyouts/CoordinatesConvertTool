using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;

namespace GeoJsonCoordinatesTranf
{
    public class ConvertThread
    {
        public delegate void SetProgressBar(float progress);
        public SetProgressBar setProgressBar;

        private EventHandler<string> m_overCallBack;
        
        private string m_result = string.Empty;
        private string m_changeType = string.Empty;
        private string m_jsonData = string.Empty;
        private Thread m_convertThread;

        public void StartConvertThread(string changeType, string jsonData, EventHandler<string> callBack)
        {
            m_changeType = changeType;
            m_jsonData = jsonData;
            m_overCallBack = callBack;
            m_convertThread = new Thread(new ThreadStart(ConvertData));
            m_convertThread.Start();
        }

        private void ConvertData()
        {
            JObject jsonData = JObject.Parse(m_jsonData);

            List<JToken> features = jsonData["features"].ToList();

            for (int i = 0; i < features.Count; i++)
            {
                setProgressBar.Invoke((i * 1f) / features.Count);
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
            m_result = jsonData.ToString();
            m_overCallBack.Invoke(this, m_result);
            m_convertThread.Abort();
        }
        
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
