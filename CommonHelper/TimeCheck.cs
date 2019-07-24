using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper
{
   public class TimeCheck
    {
        /// <summary>
        /// 获得时间间隔集合
        /// </summary>
        /// <param name="time1">输入的开始时间</param>
        /// <param name="time2">结束时间</param>
        /// <returns>返回集合</returns>
        public List<long>  CheckTicks(DateTime time1,DateTime time2)
        {           
            //初始开始和结束时间
            TimeSpan ts1 = new TimeSpan(time1.Ticks);
            TimeSpan ts2 = new TimeSpan(time2.Ticks);
            //求时间间隔
            //Duration 取绝对值
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            //根据开始和结束时间获得天数
            int DayNum = ts.Days;
            //根据时间获得小时数
            int HourNum = ts.Hours;
            int useHour = DayNum * 24 + HourNum;
            //根据时间获得分钟数
            int MinNum = useHour*60+ts.Minutes;
            long SecondNum = MinNum * 60 + ts.Seconds;
            //放入集合中
            List<long> listtime = new List<long>();
            listtime.Add(DayNum);
            listtime.Add(useHour);
            listtime.Add(MinNum);
            listtime.Add(SecondNum);
            return listtime;
           
        }                
    }
}
