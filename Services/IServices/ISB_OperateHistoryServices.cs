//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace IServices
{
    using System;
    using System.Collections.Generic;
    
    using Base;
    using Model;
      /// <summary>
      /// 负责每个数据表的业务逻辑操作的约定
      /// </summary>
    public partial interface ISB_OperateHistoryServices:IBaseServices<SB_OperateHistory>
    {
        #region 针对此表的特殊操作写在此处
        object OperatSysRecord(int limit, int page, string name, string begtime, string currtime, string type);
        int OperatRecordAdd(int? InstrumentID, string OperateChildType, string VisitUrl, Model.SB_User usermodel, int? AreaID);
      #endregion
    } 
}
