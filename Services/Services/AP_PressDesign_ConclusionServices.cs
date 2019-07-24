using System;
using System.Collections.Generic;


namespace Services
{
    using System;
    using System.Collections.Generic;
    using Model;
    using IServices;
    using Base;
    using IRepository;
    using System.Linq;
   public partial class AP_PressDesign_ConclusionServices:BaseServices<AP_PressDesign_Conclusion>,IAP_PressDesign_ConclusionServieces
    {
        IAP_PressDesign_ConclusionRepository dal;
        #region 定义构造函数，接收autofac将数据仓储层的具体实现类的对象注入到此类中
        public AP_PressDesign_ConclusionServices(IAP_PressDesign_ConclusionRepository dal)
        {
            this.dal = dal;
            base.baseDal = dal;
        }
        #endregion

        #region 针对此表的特殊操作写在此处
        public List<AP_PressDesign_Conclusion> QueryAll()
        {
           return dal.QueryWhere().ToList();
        }
        public int AllCount()
        {
            return dal.Count();
        }
        public AP_PressDesign_Conclusion FisrtModel()
        {
          return  dal.QueryWhereDesc(x => true, or => or.OrderByDescending(o => o.Id)).FirstOrDefault();
        }
       public List<AP_PressDesign_Conclusion> PressDesignConJson(int limit,int curr)
        {
            var query = dal.QueryWhere();
            var res = query.OrderByDescending(x=>x.Id).Skip(limit*(curr-1)).Take(limit).ToList().ToList();
            return res;
        }
        #endregion
    }
}
