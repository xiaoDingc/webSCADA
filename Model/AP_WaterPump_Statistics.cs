//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class AP_WaterPump_Statistics
    {
    	/// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
    	/// <summary>
        /// 
        /// </summary>
        public Nullable<System.DateTime> DateTime { get; set; }
    	/// <summary>
        /// 
        /// </summary>
        public string DayID { get; set; }
    	/// <summary>
        /// 
        /// </summary>
        public string FactoryID { get; set; }
    	/// <summary>
        /// 
        /// </summary>
        public string StationID { get; set; }
    	/// <summary>
        /// 
        /// </summary>
        public string EquipID { get; set; }
    	/// <summary>
        /// 
        /// </summary>
        public string YMDH { get; set; }
    	/// <summary>
        /// 
        /// </summary>
        public Nullable<bool> Pump1_Run { get; set; }
    	/// <summary>
        /// 
        /// </summary>
        public Nullable<bool> Pump1_Fault { get; set; }
    	/// <summary>
        /// 
        /// </summary>
        public Nullable<bool> Pump2_Run { get; set; }
    	/// <summary>
        /// 
        /// </summary>
        public Nullable<bool> Pump2_Fault { get; set; }
    	/// <summary>
        /// 
        /// </summary>
        public Nullable<bool> Pump3_Run { get; set; }
    	/// <summary>
        /// 
        /// </summary>
        public Nullable<bool> Pump3_Fault { get; set; }
    	/// <summary>
        /// 
        /// </summary>
        public Nullable<bool> Pump4_Run { get; set; }
    	/// <summary>
        /// 
        /// </summary>
        public Nullable<bool> Pump4_Fault { get; set; }
    	/// <summary>
        /// 
        /// </summary>
        public Nullable<bool> Cooling_Tower1_Run { get; set; }
    	/// <summary>
        /// 
        /// </summary>
        public Nullable<bool> Cooling_Tower1_Fault { get; set; }
    	/// <summary>
        /// 
        /// </summary>
        public Nullable<bool> Cooling_Tower2_Run { get; set; }
    	/// <summary>
        /// 
        /// </summary>
        public Nullable<bool> Cooling_Tower2_Fault { get; set; }
    	/// <summary>
        /// 
        /// </summary>
        public Nullable<decimal> Pump1_A { get; set; }
    	/// <summary>
        /// 
        /// </summary>
        public Nullable<decimal> Pump2_A { get; set; }
    	/// <summary>
        /// 
        /// </summary>
        public Nullable<decimal> Pump3_A { get; set; }
    	/// <summary>
        /// 
        /// </summary>
        public Nullable<decimal> Pump4_A { get; set; }
    	/// <summary>
        /// 
        /// </summary>
        public Nullable<decimal> Cooling_Tower1_A { get; set; }
    	/// <summary>
        /// 
        /// </summary>
        public Nullable<decimal> Cooling_Tower2_A { get; set; }
    }
}
