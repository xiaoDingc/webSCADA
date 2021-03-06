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
    public partial interface IAP_Station_HistoryServices:IBaseServices<AP_Station_History>
    {
        #region 针对此表的特殊操作写在此处
 /// <summary>
 /// 统计分析空压站产气量概率图
 /// </summary>
 /// <param name="currtime"></param>
 /// <param name="begtime"></param>
 /// <param name="arr"></param>
 /// <returns></returns>

      object StaProduce(DateTime currtime, DateTime begtime, int[] arr,string sid);
        /// <summary>
        /// 统计分析空压站压力功率图
        /// </summary>
        /// <param name="currtime"></param>
        /// <param name="begtime"></param>
        /// <param name="arr"></param>
        /// <returns></returns>
        object StaMainpq(DateTime currtime, DateTime begtime,string sid);
        /// <summary>
        /// 统计分析空压站upi放散率压力流量图
        /// </summary>
        /// <param name="currtime"></param>
        /// <param name="begtime"></param>
        /// <returns></returns>
        object StaUpiLossMainpq(DateTime currtime, DateTime begtime,string sid);
        /// <summary>
        /// 空压站放散率分布图\
        /// </summary>
        /// <param name="currtime"></param>
        /// <param name="begtime"></param>
        /// <param name="arr"></param>
        /// <returns></returns>
        object StaLoss(DateTime currtime, DateTime begtime, double[] arr,string sid);
        object GasProSer(DateTime currtime, DateTime begtime);
        //函数重载 加id为空压站图界面所需，不加界面的为大数据界面所需  
        List<decimal?[]> SprideSer(DateTime currtime, DateTime begtime,string stationId);
        /// <summary>
        /// 大数据界面蜘蛛图
        /// </summary>
        /// <param name="currtime"></param>
        /// <param name="begtime"></param>
        /// <returns></returns>
        List<decimal?[]> SprideSer(DateTime currtime, DateTime begtime);

        List<List<Model.ViewObject.Supply_View>> StaTrendReal(DateTime currtime, DateTime begtime);
        /// <summary>
        /// 空压站趋势分析实时图Ajax
        /// </summary>
        /// <param name="currtime"></param>
        /// <param name="begtime"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        object TrendStationAjax(DateTime currtime, DateTime begtime, List<string> sta);
        /// <summary>
        /// 空压站趋势分析实时图
        /// </summary>
        /// <param name="currtime"></param>
        /// <param name="begtime"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        object TrendStation(DateTime currtime, DateTime begtime, List<string> stastr);
        /// <summary>
        /// 优化指导
        /// </summary>
        /// <param name="currtime"></param>
        /// <param name="begtime"></param>
        /// <returns></returns>
        object SysEnergyAnalsys(DateTime currtime, DateTime begtime);
        /// <summary>
        /// 空压站气量功率散点
        /// </summary>
        /// <param name="currtime"></param>
        /// <param name="begtime"></param>
        /// <param name="sid"></param>
        /// <returns></returns>
        object StaProduceEpower(DateTime currtime, DateTime begtime, string sid);
      #endregion
    } 
}
