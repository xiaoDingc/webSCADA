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
    public partial interface IPG_Area_RealServices:IBaseServices<PG_Area_Real>
    {
        #region 针对此表的特殊操作写在此处
        #region 大数据界面区域服务接口
        /// <summary>
        /// 大数据界面用户用气量数据
        /// </summary>
        /// <param name="currtime">当前时间</param>
        /// <param name="begtime">之前时间</param>
        /// <returns></returns>
        object GasUseSer(DateTime currtime, DateTime begtime);
        #endregion
        #region 趋势分析用户区域服务接口
        /// <summary>
        /// 趋势分析界面用户区域所有数据
        /// </summary>
        /// <param name="currtime"></param>
        /// <param name="begtime"></param>
        /// <param name="aid"></param>
        /// <returns></returns>
        object TrendArea(DateTime currtime, DateTime begtime, List<string> aid);
        /// <summary>
        /// 趋势分析界面用户区域所有数据实时刷新数据
        /// </summary>
        /// <param name="currtime"></param>
        /// <param name="begtime"></param>
        /// <param name="aid">区域标识id集合</param>
        /// <returns></returns>
        object TrendAreaAjax(DateTime currtime, DateTime begtime, List<string> aid);
        #endregion
        #endregion
    }
}
