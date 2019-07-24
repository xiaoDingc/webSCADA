

namespace IServices
{
    using System;
    using System.Collections.Generic;
    using Base;
    using Model;
   public partial interface IAP_PressDesign_RealServices: IBaseServices<AP_PressDesign_Real>
    {
        #region 针对此表的特殊操作写在此处
        object PressDesignJson(int limit, int page);
        #endregion
    }
}
