﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TestLWSCADAEntities : DbContext
    {
        public TestLWSCADAEntities()
            : base("name=TestLWSCADAEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AP_AdsorptionDryer_History> AP_AdsorptionDryer_History { get; set; }
        public virtual DbSet<AP_AdsorptionDryer_Real> AP_AdsorptionDryer_Real { get; set; }
        public virtual DbSet<AP_AdsorptionDryer_Statistics> AP_AdsorptionDryer_Statistics { get; set; }
        public virtual DbSet<AP_Centrifuge_History> AP_Centrifuge_History { get; set; }
        public virtual DbSet<AP_Centrifuge_Real> AP_Centrifuge_Real { get; set; }
        public virtual DbSet<AP_Centrifuge_Statistics> AP_Centrifuge_Statistics { get; set; }
        public virtual DbSet<AP_Factory_History> AP_Factory_History { get; set; }
        public virtual DbSet<AP_Factory_Real> AP_Factory_Real { get; set; }
        public virtual DbSet<AP_Factory_Statistics> AP_Factory_Statistics { get; set; }
        public virtual DbSet<AP_PressDesign_Conclusion> AP_PressDesign_Conclusion { get; set; }
        public virtual DbSet<AP_PressDesign_Real> AP_PressDesign_Real { get; set; }
        public virtual DbSet<AP_Station_History> AP_Station_History { get; set; }
        public virtual DbSet<AP_Station_Real> AP_Station_Real { get; set; }
        public virtual DbSet<AP_Station_Statistics> AP_Station_Statistics { get; set; }
        public virtual DbSet<AP_WaterPump_History> AP_WaterPump_History { get; set; }
        public virtual DbSet<AP_WaterPump_Real> AP_WaterPump_Real { get; set; }
        public virtual DbSet<AP_WaterPump_Statistics> AP_WaterPump_Statistics { get; set; }
        public virtual DbSet<BB_Area> BB_Area { get; set; }
        public virtual DbSet<BB_ChartAlias> BB_ChartAlias { get; set; }
        public virtual DbSet<BB_Data_Check> BB_Data_Check { get; set; }
        public virtual DbSet<BB_Data_Error_Area> BB_Data_Error_Area { get; set; }
        public virtual DbSet<BB_Data_Error_Equ> BB_Data_Error_Equ { get; set; }
        public virtual DbSet<BB_Data_Error_Sta> BB_Data_Error_Sta { get; set; }
        public virtual DbSet<BB_Data_Exception_Area> BB_Data_Exception_Area { get; set; }
        public virtual DbSet<BB_Data_Exception_Equ> BB_Data_Exception_Equ { get; set; }
        public virtual DbSet<BB_Data_Exception_MeasureMeter> BB_Data_Exception_MeasureMeter { get; set; }
        public virtual DbSet<BB_Data_Exception_Sta> BB_Data_Exception_Sta { get; set; }
        public virtual DbSet<BB_Factory> BB_Factory { get; set; }
        public virtual DbSet<BB_Instrument> BB_Instrument { get; set; }
        public virtual DbSet<BB_MeasureMeter> BB_MeasureMeter { get; set; }
        public virtual DbSet<BB_Station> BB_Station { get; set; }
        public virtual DbSet<BB_Threshold> BB_Threshold { get; set; }
        public virtual DbSet<OpcHelperItem> OpcHelperItem { get; set; }
        public virtual DbSet<OpcHelperItems> OpcHelperItems { get; set; }
        public virtual DbSet<PG_Area_History> PG_Area_History { get; set; }
        public virtual DbSet<PG_Area_Real> PG_Area_Real { get; set; }
        public virtual DbSet<PG_Area_Statistics> PG_Area_Statistics { get; set; }
        public virtual DbSet<PLCBase> PLCBase { get; set; }
        public virtual DbSet<RE_SysRolePage> RE_SysRolePage { get; set; }
        public virtual DbSet<RE_SysRoleUser> RE_SysRoleUser { get; set; }
        public virtual DbSet<Re_UserRole1> Re_UserRole1 { get; set; }
        public virtual DbSet<RE_UserStation> RE_UserStation { get; set; }
        public virtual DbSet<SB_Client> SB_Client { get; set; }
        public virtual DbSet<SB_Log> SB_Log { get; set; }
        public virtual DbSet<SB_OperateHistory> SB_OperateHistory { get; set; }
        public virtual DbSet<SB_Page> SB_Page { get; set; }
        public virtual DbSet<SB_SysRole> SB_SysRole { get; set; }
        public virtual DbSet<SB_TableDictionary> SB_TableDictionary { get; set; }
        public virtual DbSet<SB_User> SB_User { get; set; }
        public virtual DbSet<SB_WarnLog> SB_WarnLog { get; set; }
        public virtual DbSet<UPIEnergy> UPIEnergy { get; set; }
    }
}