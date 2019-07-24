// <copyright file="Enums.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CommonHelper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Enums
    {
        /// <summary>
        /// 负责标记ajax请求以后的json数据状态
        /// </summary>
        public enum EAjaxState
        {
            /// <summary>
            /// 成功
            /// </summary>
            Sucess = 0,
            /// <summary>
            /// 错误异常
            /// </summary>
            Error = 1,
            /// <summary>
            /// 未登录
            /// </summary>
            Nologin = 2
        }

        public enum EState
        {
            /// <summary>
            /// 正常
            /// </summary>
            Nomal = 0,
            /// <summary>
            /// 停用(删除)
            /// </summary>
            Stop = 1
        }
        public enum ParentType
        {
            /// <summary>
            /// 系统
            /// </summary>
            系统 = 0,
            /// <summary>
            /// 设备
            /// </summary>
            设备 = 1

        }
        public enum ChildType
        {
            /// <summary>
            /// 未知类型
            /// </summary>
            登录 = 0,
            /// <summary>
            /// 登录
            /// </summary>
            Login = 1,
            /// <summary>
            /// 退出
            /// </summary>
            Exit = 2,
            /// <summary>
            /// 查询
            /// </summary>
            Query = 3,
            /// <summary>
            /// 增加
            /// </summary>
            Add = 4,
            /// <summary>
            /// 修改
            /// </summary>
            Modi = 5,
            /// <summary>
            /// 删除
            /// </summary>
            Delete = 6,
            /// <summary>
            /// 空压机
            /// </summary>
            Centri = 7,
            /// <summary>
            /// 干燥机
            /// </summary>
            Dry = 8,
            /// <summary>
            /// 过滤器
            /// </summary>
            Filter = 9,
            /// <summary>
            /// 冷干机
            /// </summary>
            Cold = 10

        }
        public enum Sex
        {/// <summary>
         /// 男性
         /// </summary>
            男 = 1,
            /// <summary>
            /// 女性
            /// </summary>
            女 = 0,
        }
        public enum OnWork
        {
            在职 = 1,
            离职 = 0,
        }
        public enum Valid
        {
            有效 = 1,
            无效 = 0
        }
        /// <summary>
        /// 趋势分析属性用
        /// </summary>
        public enum FacField
        {
            产气量 = 1,
            电功率 = 2,
            压力 = 3,
            单耗 = 4,
            DRE = 5,
            BOR = 6,
            开机台数 = 7,
            压缩空气露点 = 8,
        }
        public enum Area
        {
            BF1 = 1,
            BF2 = 2,
            冷轧1550 = 3,
            S2030冷轧 = 4,
            冷轧2250 = 5,
            厚板加热炉4220 = 6,
            厚板热处理4220 = 7,
            锅炉房 = 8,
            海水淡化 = 9,
            化产 = 10,
            混铁车 = 11,
            焦炉石灰 = 12,
            炼钢北区 = 13,
            炼钢南区 = 14,
            煤精 = 15,
            球团 = 16,
            烧结 = 17,
            烧结发电锅炉 = 18,
            原料 = 19,
            原料停车汽车修理设施 = 20,
            渣处理单元 = 21,
            转底炉 = 22,
        }
    }
}
