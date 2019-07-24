

namespace Services
{
    using System;
    using System.Collections.Generic;

    using Model;
    using Base;
    using IServices;
    using IRepository;
    using System.Linq;
    using System.Text.RegularExpressions;

    public partial class AP_PressDesign_RealServices: BaseServices<AP_PressDesign_Real>, IAP_PressDesign_RealServices
    {
        IAP_PressDesign_RealRepository dal;
        #region 定义构造函数，接收autofac将数据仓储层的具体实现类的对象注入到此类中
        public AP_PressDesign_RealServices(IAP_PressDesign_RealRepository dal)
        {
            this.dal = dal;
            base.baseDal = dal;
        }
        #endregion

        #region 针对此表的特殊操作写在此处
        public object PressDesignJson(int limit,int page)
        {
            var query = dal.QueryWhere();
            var res = query.OrderByDescending(o=>o.ID).Skip((page-1)*limit).Take(limit).ToList().Select(s => new
            {
                DateTime = s.DateTime.ToString(),
                FactoryID = "工厂" + Regex.Replace(s.FactoryID, "[A-Z0]", ""),
                StationID = "空压站" + Regex.Replace(s.StationID, "[A-Z0]", ""),
                EquipID = "空压机" + Regex.Replace(s.EquipID, "[A-Z0]", ""),
                OldPressDesign=s.OldPressDesign,
                PressDesign=s.PressDesign,
            }).ToList();
            var count = query.Count();
            var tranres = new
            {
                code = 0,
                msg = "success",
                data = res,
                count
            };
            return tranres;
        }
        #endregion
    }
}
