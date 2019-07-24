using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper
{
   public static class NameReslove
    {
        public static void Reslove(ref string factoryName,ref string areaName,ref string stationName,ref string equipName,ref string fieldName)
        {
            
            if (factoryName != null)
            {
                factoryName = factoryName.Replace("!", "#");
            }
            if (areaName != null)
            {
                areaName = areaName.Replace("!", "#");
            }
            if (stationName != null)
            {
                stationName = stationName.Replace("!", "#");
            }
            if (equipName != null)
            {
                equipName = equipName.Replace("!", "#");
            }
            if (fieldName != null)
            {
                fieldName = fieldName.Replace("!", "#");
            }
        }
    }
}
