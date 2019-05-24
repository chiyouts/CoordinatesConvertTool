using System;
using System.Collections.Generic;
using System.Text;

namespace GeoJsonCoordinatesTranf
{
    public class PointLatLng
    {
        public double Lat;
        public double Lng;

        public PointLatLng(double lat, double lng)
        {
            this.Lat = lat;
            this.Lng = lng;
        }
    }

    public static class ConvertGPS
    {
        private static double pi = 3.1415926535897932384626;
        private static double a = 6378245.0;
        private static double ee = 0.00669342162296594323;
        private static double bd_pi = 3.14159265358979324 * 3000.0 / 180.0;
        static Boolean outOfChina(double lat, double lon)
        {
            if (lon < 72.004 || lon > 137.8347)
                return true;
            if (lat < 0.8293 || lat > 55.8271)
                return true;
            return false;
        }

        static double transformLat(double x, double y)
        {
            double ret = -100.0 + 2.0 * x + 3.0 * y + 0.2 * y * y + 0.1 * x * y
                    + 0.2 * Math.Sqrt(Math.Abs(x));
            ret += (20.0 * Math.Sin(6.0 * x * pi) + 20.0 * Math.Sin(2.0 * x * pi)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(y * pi) + 40.0 * Math.Sin(y / 3.0 * pi)) * 2.0 / 3.0;
            ret += (160.0 * Math.Sin(y / 12.0 * pi) + 320 * Math.Sin(y * pi / 30.0)) * 2.0 / 3.0;
            return ret;
        }

        static double transformLon(double x, double y)
        {
            double ret = 300.0 + x + 2.0 * y + 0.1 * x * x + 0.1 * x * y + 0.1
                    * Math.Sqrt(Math.Abs(x));
            ret += (20.0 * Math.Sin(6.0 * x * pi) + 20.0 * Math.Sin(2.0 * x * pi)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(x * pi) + 40.0 * Math.Sin(x / 3.0 * pi)) * 2.0 / 3.0;
            ret += (150.0 * Math.Sin(x / 12.0 * pi) + 300.0 * Math.Sin(x / 30.0
                    * pi)) * 2.0 / 3.0;
            return ret;
        }

        /** 
        * 84 to 火星坐标系 (GCJ-02) World Geodetic System ==> Mars Geodetic System 
        *  
        * @param lat 
        * @param lon 
        * @return 
        */

        public static PointLatLng gps84_To_Gcj02(PointLatLng Gpoint)
        {
            if (outOfChina(Gpoint.Lat, Gpoint.Lng))
            {
                return new PointLatLng(0, 0);
            }
            double dLat = transformLat(Gpoint.Lng - 105.0, Gpoint.Lat - 35.0);
            double dLon = transformLon(Gpoint.Lng - 105.0, Gpoint.Lat - 35.0);
            double radLat = Gpoint.Lat / 180.0 * pi;
            double magic = Math.Sin(radLat);
            magic = 1 - ee * magic * magic;
            double sqrtMagic = Math.Sqrt(magic);
            dLat = (dLat * 180.0) / ((a * (1 - ee)) / (magic * sqrtMagic) * pi);
            dLon = (dLon * 180.0) / (a / sqrtMagic * Math.Cos(radLat) * pi);
            double mgLat = Gpoint.Lat + dLat;
            double mgLon = Gpoint.Lng + dLon;
            return new PointLatLng(mgLat, mgLon);
        }
        static PointLatLng transform(PointLatLng Gpoint)
        {
            if (outOfChina(Gpoint.Lat, Gpoint.Lng))
            {
                return new PointLatLng(Gpoint.Lat, Gpoint.Lng);
            }
            double dLat = transformLat(Gpoint.Lng - 105.0, Gpoint.Lat - 35.0);
            double dLon = transformLon(Gpoint.Lng - 105.0, Gpoint.Lat - 35.0);
            double radLat = Gpoint.Lat / 180.0 * pi;
            double magic = Math.Sin(radLat);
            magic = 1 - ee * magic * magic;
            double sqrtMagic = Math.Sqrt(magic);
            dLat = (dLat * 180.0) / ((a * (1 - ee)) / (magic * sqrtMagic) * pi);
            dLon = (dLon * 180.0) / (a / sqrtMagic * Math.Cos(radLat) * pi);
            double mgLat = Gpoint.Lat + dLat;
            double mgLon = Gpoint.Lng + dLon;
            return new PointLatLng(mgLat, mgLon);
        }

        /** 
         * * 火星坐标系 (GCJ-02) to 84 * * @param lon * @param lat * @return 
         * */
        public static PointLatLng gcj02_To_Gps84(PointLatLng Gpoint)
        {
            PointLatLng gps = transform(Gpoint);
            double lontitude = Gpoint.Lng * 2 - gps.Lng;
            double latitude = Gpoint.Lat * 2 - gps.Lat;
            return new PointLatLng(latitude, lontitude);
        }
        /** 
         * 火星坐标系 (GCJ-02) 与百度坐标系 (BD-09) 的转换算法 将 GCJ-02 坐标转换成 BD-09 坐标 
         *  
         * @param gg_lat 
         * @param gg_lon 
         */
        public static PointLatLng gcj02_To_Bd09(PointLatLng Gpoint)
        {
            double x = Gpoint.Lng, y = Gpoint.Lat;
            double z = Math.Sqrt(x * x + y * y) + 0.00002 * Math.Sin(y * bd_pi);
            double theta = Math.Atan2(y, x) + 0.000003 * Math.Cos(x * bd_pi);
            double bd_lon = z * Math.Cos(theta) + 0.0065;
            double bd_lat = z * Math.Sin(theta) + 0.006;
            return new PointLatLng(bd_lat, bd_lon);
        }

        /** 
        * * 火星坐标系 (GCJ-02) 与百度坐标系 (BD-09) 的转换算法 * * 将 BD-09 坐标转换成GCJ-02 坐标 * * @param 
        * bd_lat * @param bd_lon * @return 
        */
        public static PointLatLng bd09_To_Gcj02(PointLatLng bdPoint)
        {
            double x = bdPoint.Lng - 0.0065, y = bdPoint.Lat - 0.006;
            double z = Math.Sqrt(x * x + y * y) - 0.00002 * Math.Sin(y * bd_pi);
            double theta = Math.Atan2(y, x) - 0.000003 * Math.Cos(x * bd_pi);
            double gg_lon = z * Math.Cos(theta);
            double gg_lat = z * Math.Sin(theta);
            return new PointLatLng(gg_lat, gg_lon);
        }

        /** 
         * (BD-09)-->84 
         * @param bd_lat 
         * @param bd_lon 
         * @return 
         */
        public static PointLatLng bd09_To_Gps84(PointLatLng bdPoint)
        {

            PointLatLng gcj02 = bd09_To_Gcj02(bdPoint);
            PointLatLng map84 = gcj02_To_Gps84(gcj02);
            return map84;

        }
        /** 
         * 84-->(BD-09)
         * @param bd_lat 
         * @param bd_lon 
         * @return 
         */
        public static PointLatLng Gps84_To_bd09(PointLatLng gpsPoint)
        {
            PointLatLng gcj02 = gps84_To_Gcj02(gpsPoint);
            PointLatLng bd09 = gcj02_To_Bd09(gcj02);
            return bd09;

        }

    }
}