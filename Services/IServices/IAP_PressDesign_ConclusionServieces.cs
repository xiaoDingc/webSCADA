using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    using System;
    using System.Collections.Generic;

    using Base;
    using Model;
   public partial interface IAP_PressDesign_ConclusionServieces:IBaseServices<AP_PressDesign_Conclusion>
    {
        #region 针对此表的特殊操作写在此处
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        List<AP_PressDesign_Conclusion> QueryAll();
        /// <summary>
        /// 查询最新一条
        /// </summary>
        /// <returns></returns>
        AP_PressDesign_Conclusion FisrtModel();
        /// <summary>
        /// 记录内容表表
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        List<AP_PressDesign_Conclusion> PressDesignConJson(int limit, int curr);
        int AllCount();
        #endregion
    }
}
