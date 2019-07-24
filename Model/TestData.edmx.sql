
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/21/2019 09:35:17
-- Generated from EDMX file: E:\WorkSpaces\Posco.DC.SCADA\Model\TestData.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SCADA];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[AP_AdsorptionDryer_History]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AP_AdsorptionDryer_History];
GO
IF OBJECT_ID(N'[dbo].[AP_AdsorptionDryer_Real]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AP_AdsorptionDryer_Real];
GO
IF OBJECT_ID(N'[dbo].[AP_AdsorptionDryer_Statistics]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AP_AdsorptionDryer_Statistics];
GO
IF OBJECT_ID(N'[dbo].[AP_Centrifuge_History]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AP_Centrifuge_History];
GO
IF OBJECT_ID(N'[dbo].[AP_Centrifuge_Real]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AP_Centrifuge_Real];
GO
IF OBJECT_ID(N'[dbo].[AP_Centrifuge_Statistics]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AP_Centrifuge_Statistics];
GO
IF OBJECT_ID(N'[dbo].[AP_Factory_History]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AP_Factory_History];
GO
IF OBJECT_ID(N'[dbo].[AP_Factory_Real]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AP_Factory_Real];
GO
IF OBJECT_ID(N'[dbo].[AP_Factory_Statistics]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AP_Factory_Statistics];
GO
IF OBJECT_ID(N'[dbo].[AP_PressDesign_Conclusion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AP_PressDesign_Conclusion];
GO
IF OBJECT_ID(N'[dbo].[AP_PressDesign_Real]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AP_PressDesign_Real];
GO
IF OBJECT_ID(N'[dbo].[AP_Station_History]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AP_Station_History];
GO
IF OBJECT_ID(N'[dbo].[AP_Station_Real]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AP_Station_Real];
GO
IF OBJECT_ID(N'[dbo].[AP_Station_Statistics]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AP_Station_Statistics];
GO
IF OBJECT_ID(N'[dbo].[AP_WaterPump_History]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AP_WaterPump_History];
GO
IF OBJECT_ID(N'[dbo].[AP_WaterPump_Real]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AP_WaterPump_Real];
GO
IF OBJECT_ID(N'[dbo].[AP_WaterPump_Statistics]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AP_WaterPump_Statistics];
GO
IF OBJECT_ID(N'[dbo].[BB_Area]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BB_Area];
GO
IF OBJECT_ID(N'[dbo].[BB_ChartAlias]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BB_ChartAlias];
GO
IF OBJECT_ID(N'[dbo].[BB_Data_Check]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BB_Data_Check];
GO
IF OBJECT_ID(N'[dbo].[BB_Data_Error_Area]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BB_Data_Error_Area];
GO
IF OBJECT_ID(N'[dbo].[BB_Data_Error_Equ]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BB_Data_Error_Equ];
GO
IF OBJECT_ID(N'[dbo].[BB_Data_Error_Sta]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BB_Data_Error_Sta];
GO
IF OBJECT_ID(N'[dbo].[BB_Data_Exception_Area]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BB_Data_Exception_Area];
GO
IF OBJECT_ID(N'[dbo].[BB_Data_Exception_Equ]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BB_Data_Exception_Equ];
GO
IF OBJECT_ID(N'[dbo].[BB_Data_Exception_MeasureMeter]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BB_Data_Exception_MeasureMeter];
GO
IF OBJECT_ID(N'[dbo].[BB_Data_Exception_Sta]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BB_Data_Exception_Sta];
GO
IF OBJECT_ID(N'[dbo].[BB_Factory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BB_Factory];
GO
IF OBJECT_ID(N'[dbo].[BB_Instrument]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BB_Instrument];
GO
IF OBJECT_ID(N'[dbo].[BB_MeasureMeter]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BB_MeasureMeter];
GO
IF OBJECT_ID(N'[dbo].[BB_Station]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BB_Station];
GO
IF OBJECT_ID(N'[dbo].[BB_Threshold]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BB_Threshold];
GO
IF OBJECT_ID(N'[dbo].[OpcHelperItem]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OpcHelperItem];
GO
IF OBJECT_ID(N'[dbo].[OpcHelperItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OpcHelperItems];
GO
IF OBJECT_ID(N'[dbo].[PG_Area_History]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PG_Area_History];
GO
IF OBJECT_ID(N'[dbo].[PG_Area_Real]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PG_Area_Real];
GO
IF OBJECT_ID(N'[dbo].[PG_Area_Statistics]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PG_Area_Statistics];
GO
IF OBJECT_ID(N'[dbo].[PLCBase]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PLCBase];
GO
IF OBJECT_ID(N'[dbo].[RE_SysRolePage]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RE_SysRolePage];
GO
IF OBJECT_ID(N'[dbo].[RE_SysRoleUser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RE_SysRoleUser];
GO
IF OBJECT_ID(N'[dbo].[Re_UserRole1]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Re_UserRole1];
GO
IF OBJECT_ID(N'[dbo].[RE_UserStation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RE_UserStation];
GO
IF OBJECT_ID(N'[dbo].[SB_Client]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SB_Client];
GO
IF OBJECT_ID(N'[dbo].[SB_Log]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SB_Log];
GO
IF OBJECT_ID(N'[dbo].[SB_OperateHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SB_OperateHistory];
GO
IF OBJECT_ID(N'[dbo].[SB_Page]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SB_Page];
GO
IF OBJECT_ID(N'[dbo].[SB_SysRole]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SB_SysRole];
GO
IF OBJECT_ID(N'[dbo].[SB_TableDictionary]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SB_TableDictionary];
GO
IF OBJECT_ID(N'[dbo].[SB_User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SB_User];
GO
IF OBJECT_ID(N'[dbo].[SB_WarnLog]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SB_WarnLog];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'AP_AdsorptionDryer_History'
CREATE TABLE [dbo].[AP_AdsorptionDryer_History] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [DateTime] datetime  NULL,
    [DayID] char(2)  NULL,
    [FactoryID] char(4)  NULL,
    [StationID] char(4)  NULL,
    [EquipID] char(4)  NULL,
    [Run] decimal(12,2)  NULL,
    [CDryer_CondPre] decimal(12,2)  NULL,
    [CDryer_EvapPre] decimal(12,2)  NULL,
    [CDryer_InletPre] decimal(12,2)  NULL,
    [CDryer_OutPre] decimal(12,2)  NULL,
    [CDyer_InOutValue] decimal(12,2)  NULL,
    [CDyer_YSJ_A] decimal(12,2)  NULL,
    [CDyer_JRQ_A] decimal(12,2)  NULL,
    [CDyer_INT] decimal(12,2)  NULL,
    [CDyer_OutT] decimal(12,2)  NULL,
    [CDyer_LeakT] decimal(12,2)  NULL,
    [CDyer_ATowerT] decimal(12,2)  NULL,
    [CDyer_BTowerT] decimal(12,2)  NULL,
    [CDyer_HeatTowerMidT] decimal(12,2)  NULL,
    [CDyer_HeatTowerOutT] decimal(12,2)  NULL,
    [CDyer_DewPoint] decimal(12,2)  NULL,
    [Loss_Q] decimal(12,2)  NULL,
    [Heater_Power] decimal(12,2)  NULL,
    [Blower_Power] decimal(12,2)  NULL,
    [Purge_T] decimal(12,2)  NULL,
    [Purge_P] decimal(12,2)  NULL,
    [Purge_DP] decimal(12,2)  NULL,
    [Inlet_Q] decimal(12,2)  NULL,
    [Outlet_Q] decimal(12,2)  NULL,
    [Power] decimal(12,2)  NULL,
    [UPI] decimal(12,3)  NULL,
    [Load_Rate] decimal(12,2)  NULL,
    [RUN_Time] decimal(12,2)  NULL,
    [RunTime_Rate] decimal(12,2)  NULL
);
GO

-- Creating table 'AP_AdsorptionDryer_Real'
CREATE TABLE [dbo].[AP_AdsorptionDryer_Real] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [DateTime] datetime  NULL,
    [DayID] char(2)  NULL,
    [FactoryID] char(4)  NULL,
    [StationID] char(4)  NULL,
    [EquipID] char(4)  NULL,
    [Run] decimal(12,2)  NULL,
    [CDryer_CondPre] decimal(12,2)  NULL,
    [CDryer_EvapPre] decimal(12,2)  NULL,
    [CDryer_InletPre] decimal(12,2)  NULL,
    [CDryer_OutPre] decimal(12,2)  NULL,
    [CDyer_InOutValue] decimal(12,2)  NULL,
    [CDyer_YSJ_A] decimal(12,2)  NULL,
    [CDyer_JRQ_A] decimal(12,2)  NULL,
    [CDyer_INT] decimal(12,2)  NULL,
    [CDyer_OutT] decimal(12,2)  NULL,
    [CDyer_LeakT] decimal(12,2)  NULL,
    [CDyer_ATowerT] decimal(12,2)  NULL,
    [CDyer_BTowerT] decimal(12,2)  NULL,
    [CDyer_HeatTowerMidT] decimal(12,2)  NULL,
    [CDyer_HeatTowerOutT] decimal(12,2)  NULL,
    [CDyer_DewPoint] decimal(12,2)  NULL,
    [Loss_Q] decimal(12,2)  NULL,
    [Heater_Power] decimal(12,2)  NULL,
    [Blower_Power] decimal(12,2)  NULL,
    [Purge_T] decimal(12,2)  NULL,
    [Purge_P] decimal(12,2)  NULL,
    [Purge_DP] decimal(12,2)  NULL,
    [Inlet_Q] decimal(12,2)  NULL,
    [Outlet_Q] decimal(12,2)  NULL,
    [Power] decimal(12,2)  NULL,
    [UPI] decimal(12,3)  NULL,
    [Load_Rate] decimal(12,2)  NULL,
    [RUN_Time] decimal(12,2)  NULL,
    [RunTime_Rate] decimal(12,2)  NULL
);
GO

-- Creating table 'AP_AdsorptionDryer_Statistics'
CREATE TABLE [dbo].[AP_AdsorptionDryer_Statistics] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [DateTime] datetime  NULL,
    [DayID] char(2)  NULL,
    [FactoryID] char(4)  NULL,
    [StationID] char(4)  NULL,
    [EquipID] char(4)  NULL,
    [YMDH] char(1)  NULL,
    [Run] decimal(12,2)  NULL,
    [CDryer_CondPre] decimal(12,2)  NULL,
    [CDryer_EvapPre] decimal(12,2)  NULL,
    [CDryer_InletPre] decimal(12,2)  NULL,
    [CDryer_OutPre] decimal(12,2)  NULL,
    [CDyer_InOutValue] decimal(12,2)  NULL,
    [CDyer_YSJ_A] decimal(12,2)  NULL,
    [CDyer_JRQ_A] decimal(12,2)  NULL,
    [CDyer_INT] decimal(12,2)  NULL,
    [CDyer_OutT] decimal(12,2)  NULL,
    [CDyer_LeakT] decimal(12,2)  NULL,
    [CDyer_ATowerT] decimal(12,2)  NULL,
    [CDyer_BTowerT] decimal(12,2)  NULL,
    [CDyer_HeatTowerMidT] decimal(12,2)  NULL,
    [CDyer_HeatTowerOutT] decimal(12,2)  NULL,
    [CDyer_DewPoint] decimal(12,2)  NULL,
    [Loss_Q] decimal(12,2)  NULL,
    [Heater_Power] decimal(12,2)  NULL,
    [Blower_Power] decimal(12,2)  NULL,
    [Purge_T] decimal(12,2)  NULL,
    [Purge_P] decimal(12,2)  NULL,
    [Purge_DP] decimal(12,2)  NULL,
    [Inlet_Q] decimal(12,2)  NULL,
    [Outlet_Q] decimal(12,2)  NULL,
    [Power] decimal(12,2)  NULL,
    [UPI] decimal(12,3)  NULL,
    [Load_Rate] decimal(12,2)  NULL,
    [RUN_Time] decimal(12,2)  NULL,
    [RunTime_Rate] decimal(12,2)  NULL
);
GO

-- Creating table 'AP_Centrifuge_History'
CREATE TABLE [dbo].[AP_Centrifuge_History] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DateTime] datetime  NULL,
    [DayID] char(2)  NULL,
    [FactoryID] char(4)  NULL,
    [StationID] char(4)  NULL,
    [EquipID] char(4)  NULL,
    [Outlet_T] decimal(12,2)  NULL,
    [Outlet_P] decimal(12,2)  NULL,
    [RET_T1] decimal(12,2)  NULL,
    [RET_T2] decimal(12,2)  NULL,
    [RET_T3] decimal(12,2)  NULL,
    [Q] decimal(12,2)  NULL,
    [Accumulated_Q] decimal(12,2)  NULL,
    [Inlet_dp] decimal(12,2)  NULL,
    [C] decimal(12,2)  NULL,
    [InletIOpen] decimal(12,2)  NULL,
    [DiscgargeOpen] decimal(12,2)  NULL,
    [T3Air] decimal(12,2)  NULL,
    [T3Pressure] decimal(12,2)  NULL,
    [HydraulicAir] decimal(12,2)  NULL,
    [PrimaryAir] decimal(12,2)  NULL,
    [TwoStageAir] decimal(12,2)  NULL,
    [ThreeStageAir] decimal(12,2)  NULL,
    [Oil_TemAir] decimal(12,2)  NULL,
    [Load_TemAir] decimal(12,2)  NULL,
    [Unload_TemAir] decimal(12,2)  NULL,
    [Rphase_TemAir] decimal(12,2)  NULL,
    [ePower] decimal(12,2)  NULL,
    [eQ] decimal(12,2)  NULL,
    [Run_Time] decimal(12,2)  NULL,
    [PCE] decimal(12,2)  NULL,
    [DRE] decimal(12,2)  NULL,
    [ERE] decimal(12,2)  NULL,
    [UPI] decimal(12,4)  NULL,
    [Rank] decimal(12,2)  NULL,
    [Monthly_Rank] decimal(12,2)  NULL,
    [Run] decimal(12,2)  NULL,
    [Load] decimal(12,2)  NULL,
    [RunTime_Rate] decimal(12,2)  NULL,
    [Part_Load_Rat] decimal(12,2)  NULL,
    [Power] decimal(12,2)  NULL,
    [standard_P] decimal(12,2)  NULL,
    [LossRatio] decimal(12,2)  NULL
);
GO

-- Creating table 'AP_Centrifuge_Real'
CREATE TABLE [dbo].[AP_Centrifuge_Real] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DateTime] datetime  NULL,
    [DayID] char(2)  NULL,
    [FactoryID] char(4)  NULL,
    [StationID] char(4)  NULL,
    [EquipID] char(4)  NULL,
    [Outlet_T] decimal(12,2)  NULL,
    [Outlet_P] decimal(12,2)  NULL,
    [RET_T1] decimal(12,2)  NULL,
    [RET_T2] decimal(12,2)  NULL,
    [RET_T3] decimal(12,2)  NULL,
    [Q] decimal(12,2)  NULL,
    [Accumulated_Q] decimal(12,2)  NULL,
    [Inlet_dp] decimal(12,2)  NULL,
    [C] decimal(12,2)  NULL,
    [InletIOpen] decimal(12,2)  NULL,
    [DiscgargeOpen] decimal(12,2)  NULL,
    [T3Air] decimal(12,2)  NULL,
    [T3Pressure] decimal(12,2)  NULL,
    [HydraulicAir] decimal(12,2)  NULL,
    [PrimaryAir] decimal(12,2)  NULL,
    [TwoStageAir] decimal(12,2)  NULL,
    [ThreeStageAir] decimal(12,2)  NULL,
    [Oil_TemAir] decimal(12,2)  NULL,
    [Load_TemAir] decimal(12,2)  NULL,
    [Unload_TemAir] decimal(12,2)  NULL,
    [Rphase_TemAir] decimal(12,2)  NULL,
    [ePower] decimal(12,2)  NULL,
    [eQ] decimal(12,2)  NULL,
    [Run_Time] decimal(12,2)  NULL,
    [PCE] decimal(12,2)  NULL,
    [DRE] decimal(12,2)  NULL,
    [ERE] decimal(12,2)  NULL,
    [UPI] decimal(12,4)  NULL,
    [Rank] decimal(12,2)  NULL,
    [Monthly_Rank] decimal(12,2)  NULL,
    [Run] decimal(12,2)  NULL,
    [Load] decimal(12,2)  NULL,
    [RunTime_Rate] decimal(12,2)  NULL,
    [Part_Load_Rat] decimal(12,2)  NULL,
    [Power] decimal(12,2)  NULL,
    [standard_P] decimal(12,2)  NULL,
    [LossRatio] decimal(12,2)  NULL
);
GO

-- Creating table 'AP_Centrifuge_Statistics'
CREATE TABLE [dbo].[AP_Centrifuge_Statistics] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DateTime] datetime  NULL,
    [DayID] char(2)  NULL,
    [FactoryID] char(4)  NULL,
    [StationID] char(4)  NULL,
    [EquipID] char(4)  NULL,
    [YMDH] char(1)  NULL,
    [Outlet_T] decimal(12,2)  NULL,
    [Outlet_P] decimal(12,2)  NULL,
    [RET_T1] decimal(12,2)  NULL,
    [RET_T2] decimal(12,2)  NULL,
    [RET_T3] decimal(12,2)  NULL,
    [Q] decimal(12,2)  NULL,
    [Accumulated_Q] decimal(12,2)  NULL,
    [Inlet_dp] decimal(12,2)  NULL,
    [C] decimal(12,2)  NULL,
    [InletIOpen] decimal(12,2)  NULL,
    [DiscgargeOpen] decimal(12,2)  NULL,
    [T3Air] decimal(12,2)  NULL,
    [T3Pressure] decimal(12,2)  NULL,
    [HydraulicAir] decimal(12,2)  NULL,
    [PrimaryAir] decimal(12,2)  NULL,
    [TwoStageAir] decimal(12,2)  NULL,
    [ThreeStageAir] decimal(12,2)  NULL,
    [Oil_TemAir] decimal(12,2)  NULL,
    [Load_TemAir] decimal(12,2)  NULL,
    [Unload_TemAir] decimal(12,2)  NULL,
    [Rphase_TemAir] decimal(12,2)  NULL,
    [ePower] decimal(12,2)  NULL,
    [eQ] decimal(12,2)  NULL,
    [Run_Time] decimal(12,2)  NULL,
    [PCE] decimal(12,2)  NULL,
    [DRE] decimal(12,2)  NULL,
    [ERE] decimal(12,2)  NULL,
    [UPI] decimal(12,4)  NULL,
    [Rank] decimal(12,2)  NULL,
    [Monthly_Rank] decimal(12,2)  NULL,
    [Run] decimal(12,2)  NULL,
    [Load] decimal(12,2)  NULL,
    [RunTime_Rate] decimal(12,2)  NULL,
    [Part_Load_Rat] decimal(12,2)  NULL,
    [Power] decimal(12,2)  NULL,
    [standard_P] decimal(12,2)  NULL,
    [LossRatio] decimal(12,2)  NULL
);
GO

-- Creating table 'AP_Factory_History'
CREATE TABLE [dbo].[AP_Factory_History] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DateTime] datetime  NULL,
    [DayID] char(2)  NULL,
    [FactoryID] char(4)  NULL,
    [Dewpoint] decimal(12,2)  NULL,
    [SumPower] decimal(12,2)  NULL,
    [ProduceFlowStation] decimal(12,2)  NULL,
    [ProduceFlowCentrifuge] decimal(12,2)  NULL,
    [UseFlow] decimal(12,2)  NULL,
    [PipelinePressure] decimal(12,2)  NULL,
    [FlowLoss] decimal(12,2)  NULL,
    [UPI] decimal(12,4)  NULL,
    [LossRatio] decimal(12,2)  NULL,
    [Main_Rated_Q] decimal(12,2)  NULL,
    [Main_Rated_power] decimal(12,2)  NULL,
    [DRE] decimal(12,2)  NULL,
    [TCE] decimal(12,2)  NULL,
    [RUN_NUM] decimal(12,2)  NULL
);
GO

-- Creating table 'AP_Factory_Real'
CREATE TABLE [dbo].[AP_Factory_Real] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DateTime] datetime  NOT NULL,
    [DayID] char(2)  NULL,
    [FactoryID] char(4)  NULL,
    [Dewpoint] decimal(12,2)  NULL,
    [SumPower] decimal(12,2)  NULL,
    [ProduceFlowStation] decimal(12,2)  NULL,
    [ProduceFlowCentrifuge] decimal(12,2)  NULL,
    [UseFlow] decimal(12,2)  NULL,
    [PipelinePressure] decimal(12,2)  NULL,
    [FlowLoss] decimal(12,2)  NULL,
    [UPI] decimal(12,4)  NULL,
    [LossRatio] decimal(12,2)  NULL,
    [Main_Rated_Q] decimal(12,2)  NULL,
    [Main_Rated_power] decimal(12,2)  NULL,
    [DRE] decimal(12,2)  NULL,
    [TCE] decimal(12,2)  NULL,
    [RUN_NUM] decimal(12,2)  NULL
);
GO

-- Creating table 'AP_Factory_Statistics'
CREATE TABLE [dbo].[AP_Factory_Statistics] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DateTime] datetime  NULL,
    [DayID] char(2)  NULL,
    [FactoryID] char(4)  NULL,
    [YMDH] char(1)  NULL,
    [Dewpoint] decimal(12,2)  NULL,
    [SumPower] decimal(12,2)  NULL,
    [ProduceFlowStation] decimal(12,2)  NULL,
    [ProduceFlowCentrifuge] decimal(12,2)  NULL,
    [UseFlow] decimal(12,2)  NULL,
    [PipelinePressure] decimal(12,2)  NULL,
    [FlowLoss] decimal(12,2)  NULL,
    [UPI] decimal(12,4)  NULL,
    [LossRatio] decimal(12,2)  NULL,
    [Main_Rated_Q] decimal(12,2)  NULL,
    [Main_Rated_power] decimal(12,2)  NULL,
    [DRE] decimal(12,2)  NULL,
    [TCE] decimal(12,2)  NULL,
    [RUN_NUM] decimal(12,2)  NULL
);
GO

-- Creating table 'AP_PressDesign_Conclusion'
CREATE TABLE [dbo].[AP_PressDesign_Conclusion] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Time] datetime  NULL,
    [Conclusion] nvarchar(1500)  NULL,
    [Description] nvarchar(50)  NULL
);
GO

-- Creating table 'AP_PressDesign_Real'
CREATE TABLE [dbo].[AP_PressDesign_Real] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [DateTime] datetime  NULL,
    [FactoryID] nchar(4)  NULL,
    [StationID] nchar(10)  NULL,
    [EquipID] nchar(10)  NULL,
    [PressDesign] decimal(18,2)  NULL,
    [OldPressDesign] decimal(18,2)  NULL
);
GO

-- Creating table 'AP_Station_History'
CREATE TABLE [dbo].[AP_Station_History] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DateTime] datetime  NULL,
    [DayID] char(2)  NULL,
    [FactoryID] char(4)  NULL,
    [StationID] nvarchar(15)  NULL,
    [Air_T] decimal(12,2)  NULL,
    [Air_H] decimal(12,2)  NULL,
    [Water_P] decimal(12,2)  NULL,
    [Water_T] decimal(12,2)  NULL,
    [Water_F] decimal(12,2)  NULL,
    [Main_Q] decimal(12,2)  NULL,
    [Main_P] decimal(12,2)  NULL,
    [Line1_V] decimal(12,2)  NULL,
    [Line1_A] decimal(12,2)  NULL,
    [Line1_PF] decimal(12,2)  NULL,
    [Line1_Power] decimal(12,2)  NULL,
    [Line2_V] decimal(12,2)  NULL,
    [Line2_A] decimal(12,2)  NULL,
    [Line2_PF] decimal(12,2)  NULL,
    [Line2_Power] decimal(12,2)  NULL,
    [RUN_NUM] decimal(12,2)  NULL,
    [REQ_NUM] decimal(12,2)  NULL,
    [ePower] decimal(12,2)  NULL,
    [UPI] decimal(12,4)  NULL,
    [PCE] decimal(12,2)  NULL,
    [DRE] decimal(12,2)  NULL,
    [ERE] decimal(12,2)  NULL,
    [RunTime_Rate] decimal(12,2)  NULL,
    [FLP] decimal(12,2)  NULL,
    [PressureLoss] decimal(12,2)  NULL,
    [Dewpoint] decimal(12,2)  NULL,
    [Main_Rated_Q] decimal(12,2)  NULL,
    [UPI_Save] decimal(12,2)  NULL,
    [Line_Q] decimal(12,2)  NULL,
    [Part_Load_Ratio] decimal(12,2)  NULL,
    [RunTime] decimal(12,2)  NULL,
    [Energy_Save] decimal(12,2)  NULL,
    [Power_Save] decimal(12,2)  NULL,
    [Energy_Save_Rate] decimal(12,2)  NULL,
    [Power_Save_Rate] decimal(12,2)  NULL,
    [Energy_Save_Cost] decimal(12,2)  NULL,
    [Power_Save_Cost] decimal(12,2)  NULL,
    [CompressorPower] decimal(12,2)  NULL,
    [Loss_Q] decimal(12,2)  NULL,
    [Dryer_Power] decimal(12,2)  NULL,
    [Sum_Power] decimal(12,2)  NULL,
    [Main_Rated_power] decimal(12,2)  NULL,
    [ReleaseRate] decimal(12,3)  NULL,
    [eQ_MainSum] decimal(12,2)  NULL,
    [CentrifugeSumQ] decimal(12,2)  NULL,
    [TCE] decimal(12,2)  NULL
);
GO

-- Creating table 'AP_Station_Real'
CREATE TABLE [dbo].[AP_Station_Real] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DateTime] datetime  NULL,
    [DayID] char(2)  NULL,
    [FactoryID] char(4)  NULL,
    [StationID] nvarchar(15)  NULL,
    [Air_T] decimal(12,2)  NULL,
    [Air_H] decimal(12,2)  NULL,
    [Water_P] decimal(12,2)  NULL,
    [Water_T] decimal(12,2)  NULL,
    [Water_F] decimal(12,2)  NULL,
    [Main_Q] decimal(12,2)  NULL,
    [Main_P] decimal(12,2)  NULL,
    [Line1_V] decimal(12,2)  NULL,
    [Line1_A] decimal(12,2)  NULL,
    [Line1_PF] decimal(12,2)  NULL,
    [Line1_Power] decimal(12,2)  NULL,
    [Line2_V] decimal(12,2)  NULL,
    [Line2_A] decimal(12,2)  NULL,
    [Line2_PF] decimal(12,2)  NULL,
    [Line2_Power] decimal(12,2)  NULL,
    [RUN_NUM] decimal(12,2)  NULL,
    [REQ_NUM] decimal(12,2)  NULL,
    [ePower] decimal(12,2)  NULL,
    [UPI] decimal(12,4)  NULL,
    [PCE] decimal(12,2)  NULL,
    [DRE] decimal(12,2)  NULL,
    [ERE] decimal(12,2)  NULL,
    [RunTime_Rate] decimal(12,2)  NULL,
    [FLP] decimal(12,2)  NULL,
    [PressureLoss] decimal(12,2)  NULL,
    [Dewpoint] decimal(12,2)  NULL,
    [Main_Rated_Q] decimal(12,2)  NULL,
    [UPI_Save] decimal(12,2)  NULL,
    [Line_Q] decimal(12,2)  NULL,
    [Part_Load_Ratio] decimal(12,2)  NULL,
    [RunTime] decimal(12,2)  NULL,
    [Energy_Save] decimal(12,2)  NULL,
    [Power_Save] decimal(12,2)  NULL,
    [Energy_Save_Rate] decimal(12,2)  NULL,
    [Power_Save_Rate] decimal(12,2)  NULL,
    [Energy_Save_Cost] decimal(12,2)  NULL,
    [Power_Save_Cost] decimal(12,2)  NULL,
    [CompressorPower] decimal(12,2)  NULL,
    [Loss_Q] decimal(12,2)  NULL,
    [Dryer_Power] decimal(12,2)  NULL,
    [Sum_Power] decimal(12,2)  NULL,
    [Main_Rated_power] decimal(12,2)  NULL,
    [ReleaseRate] decimal(12,3)  NULL,
    [eQ_MainSum] decimal(12,2)  NULL,
    [CentrifugeSumQ] decimal(12,2)  NULL,
    [TCE] decimal(12,2)  NULL
);
GO

-- Creating table 'AP_Station_Statistics'
CREATE TABLE [dbo].[AP_Station_Statistics] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DateTime] datetime  NULL,
    [DayID] char(2)  NULL,
    [FactoryID] char(4)  NULL,
    [StationID] nvarchar(15)  NULL,
    [YMDH] char(1)  NULL,
    [Air_T] decimal(12,2)  NULL,
    [Air_H] decimal(12,2)  NULL,
    [Water_P] decimal(12,2)  NULL,
    [Water_T] decimal(12,2)  NULL,
    [Water_F] decimal(12,2)  NULL,
    [Main_Q] decimal(12,2)  NULL,
    [Main_P] decimal(12,2)  NULL,
    [Line1_V] decimal(12,2)  NULL,
    [Line1_A] decimal(12,2)  NULL,
    [Line1_PF] decimal(12,2)  NULL,
    [Line1_Power] decimal(12,2)  NULL,
    [Line2_V] decimal(12,2)  NULL,
    [Line2_A] decimal(12,2)  NULL,
    [Line2_PF] decimal(12,2)  NULL,
    [Line2_Power] decimal(12,2)  NULL,
    [RUN_NUM] decimal(12,2)  NULL,
    [REQ_NUM] decimal(12,2)  NULL,
    [ePower] decimal(12,2)  NULL,
    [UPI] decimal(12,4)  NULL,
    [PCE] decimal(12,2)  NULL,
    [DRE] decimal(12,2)  NULL,
    [ERE] decimal(12,2)  NULL,
    [RunTime_Rate] decimal(12,2)  NULL,
    [FLP] decimal(12,2)  NULL,
    [PressureLoss] decimal(12,2)  NULL,
    [Dewpoint] decimal(12,2)  NULL,
    [Main_Rated_Q] decimal(12,2)  NULL,
    [UPI_Save] decimal(12,2)  NULL,
    [Line_Q] decimal(12,2)  NULL,
    [Part_Load_Ratio] decimal(12,2)  NULL,
    [RunTime] decimal(12,2)  NULL,
    [Energy_Save] decimal(12,2)  NULL,
    [Power_Save] decimal(12,2)  NULL,
    [Energy_Save_Rate] decimal(12,2)  NULL,
    [Power_Save_Rate] decimal(12,2)  NULL,
    [Energy_Save_Cost] decimal(12,2)  NULL,
    [Power_Save_Cost] decimal(12,2)  NULL,
    [CompressorPower] decimal(12,2)  NULL,
    [Loss_Q] decimal(12,2)  NULL,
    [Dryer_Power] decimal(12,2)  NULL,
    [Sum_Power] decimal(12,2)  NULL,
    [Main_Rated_power] decimal(12,2)  NULL,
    [ReleaseRate] decimal(12,3)  NULL,
    [eQ_MainSum] decimal(12,2)  NULL,
    [CentrifugeSumQ] decimal(12,2)  NULL,
    [TCE] decimal(12,2)  NULL
);
GO

-- Creating table 'AP_WaterPump_History'
CREATE TABLE [dbo].[AP_WaterPump_History] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DateTime] datetime  NULL,
    [DayID] char(2)  NULL,
    [FactoryID] char(4)  NULL,
    [StationID] char(4)  NULL,
    [EquipID] char(4)  NULL,
    [Pump1_Run] bit  NULL,
    [Pump1_Fault] bit  NULL,
    [Pump2_Run] bit  NULL,
    [Pump2_Fault] bit  NULL,
    [Pump3_Run] bit  NULL,
    [Pump3_Fault] bit  NULL,
    [Pump4_Run] bit  NULL,
    [Pump4_Fault] bit  NULL,
    [Cooling_Tower1_Run] bit  NULL,
    [Cooling_Tower1_Fault] bit  NULL,
    [Cooling_Tower2_Run] bit  NULL,
    [Cooling_Tower2_Fault] bit  NULL,
    [Pump1_A] decimal(12,2)  NULL,
    [Pump2_A] decimal(12,2)  NULL,
    [Pump3_A] decimal(12,2)  NULL,
    [Pump4_A] decimal(12,2)  NULL,
    [Cooling_Tower1_A] decimal(12,2)  NULL,
    [Cooling_Tower2_A] decimal(12,2)  NULL
);
GO

-- Creating table 'AP_WaterPump_Real'
CREATE TABLE [dbo].[AP_WaterPump_Real] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DateTime] datetime  NULL,
    [DayID] char(2)  NULL,
    [FactoryID] char(4)  NULL,
    [StationID] char(4)  NULL,
    [EquipID] char(4)  NULL,
    [Pump1_Run] bit  NULL,
    [Pump1_Fault] bit  NULL,
    [Pump2_Run] bit  NULL,
    [Pump2_Fault] bit  NULL,
    [Pump3_Run] bit  NULL,
    [Pump3_Fault] bit  NULL,
    [Pump4_Run] bit  NULL,
    [Pump4_Fault] bit  NULL,
    [Cooling_Tower1_Run] bit  NULL,
    [Cooling_Tower1_Fault] bit  NULL,
    [Cooling_Tower2_Run] bit  NULL,
    [Cooling_Tower2_Fault] bit  NULL,
    [Pump1_A] decimal(12,2)  NULL,
    [Pump2_A] decimal(12,2)  NULL,
    [Pump3_A] decimal(12,2)  NULL,
    [Pump4_A] decimal(12,2)  NULL,
    [Cooling_Tower1_A] decimal(12,2)  NULL,
    [Cooling_Tower2_A] decimal(12,2)  NULL
);
GO

-- Creating table 'AP_WaterPump_Statistics'
CREATE TABLE [dbo].[AP_WaterPump_Statistics] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DateTime] datetime  NULL,
    [DayID] char(2)  NULL,
    [FactoryID] char(4)  NULL,
    [StationID] char(4)  NULL,
    [EquipID] char(4)  NULL,
    [YMDH] char(1)  NULL,
    [Pump1_Run] bit  NULL,
    [Pump1_Fault] bit  NULL,
    [Pump2_Run] bit  NULL,
    [Pump2_Fault] bit  NULL,
    [Pump3_Run] bit  NULL,
    [Pump3_Fault] bit  NULL,
    [Pump4_Run] bit  NULL,
    [Pump4_Fault] bit  NULL,
    [Cooling_Tower1_Run] bit  NULL,
    [Cooling_Tower1_Fault] bit  NULL,
    [Cooling_Tower2_Run] bit  NULL,
    [Cooling_Tower2_Fault] bit  NULL,
    [Pump1_A] decimal(12,2)  NULL,
    [Pump2_A] decimal(12,2)  NULL,
    [Pump3_A] decimal(12,2)  NULL,
    [Pump4_A] decimal(12,2)  NULL,
    [Cooling_Tower1_A] decimal(12,2)  NULL,
    [Cooling_Tower2_A] decimal(12,2)  NULL
);
GO

-- Creating table 'BB_Area'
CREATE TABLE [dbo].[BB_Area] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Code] char(4)  NULL,
    [Name] nvarchar(50)  NULL,
    [Introduction] nvarchar(10)  NULL,
    [Latitude] decimal(12,6)  NULL,
    [Longtitude] decimal(12,6)  NULL,
    [Remark] nvarchar(100)  NULL,
    [FactoryID] int  NULL,
    [ClientID] int  NULL,
    [Valid] char(1)  NULL,
    [InputSize] int  NULL
);
GO

-- Creating table 'BB_ChartAlias'
CREATE TABLE [dbo].[BB_ChartAlias] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DateTime] datetime  NULL,
    [code] int  NULL,
    [factoryId] nvarchar(20)  NULL,
    [stationID] nvarchar(20)  NULL,
    [EquipID] nvarchar(20)  NULL,
    [AreaId] nvarchar(20)  NULL,
    [name] nvarchar(20)  NULL,
    [factoryName] nvarchar(20)  NULL,
    [stationName] nvarchar(20)  NULL,
    [EquipName] nvarchar(20)  NULL,
    [AreaName] nvarchar(20)  NULL,
    [FieldName] nvarchar(20)  NULL,
    [layuiTitle] nvarchar(50)  NULL,
    [layuiArea] nvarchar(50)  NULL,
    [layuiOffset] nvarchar(50)  NULL,
    [chartYUnit] nvarchar(50)  NULL,
    [chartXLegend] nvarchar(50)  NULL
);
GO

-- Creating table 'BB_Data_Check'
CREATE TABLE [dbo].[BB_Data_Check] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [TableName] nvarchar(max)  NULL,
    [FieldName] nvarchar(max)  NULL,
    [StationID] nchar(4)  NULL,
    [EquipID] nchar(4)  NULL,
    [AreaID] nchar(4)  NULL,
    [ResMax] decimal(12,4)  NULL,
    [ResMin] decimal(12,4)  NULL,
    [DisplayOrder] int  NOT NULL,
    [UpperLimit] real  NOT NULL,
    [LowerLimit] real  NOT NULL,
    [BaseValue] real  NULL,
    [DataSource] nvarchar(max)  NULL,
    [Unit] nvarchar(max)  NULL,
    [Valid] nvarchar(max)  NULL,
    [Description] nvarchar(max)  NULL
);
GO

-- Creating table 'BB_Data_Error_Area'
CREATE TABLE [dbo].[BB_Data_Error_Area] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [TableName] nvarchar(200)  NULL,
    [FieldName] nvarchar(200)  NULL,
    [FactoryID] char(4)  NULL,
    [AreaID] char(4)  NULL,
    [AbnormalValue] nvarchar(20)  NULL,
    [DataSource] int  NULL,
    [OccurTime] datetime  NULL
);
GO

-- Creating table 'BB_Data_Error_Equ'
CREATE TABLE [dbo].[BB_Data_Error_Equ] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [TableName] nvarchar(200)  NULL,
    [FieldName] nvarchar(200)  NULL,
    [FactoryID] char(4)  NULL,
    [StationID] char(4)  NULL,
    [EquipID] char(4)  NULL,
    [AbnormalValue] nvarchar(20)  NULL,
    [DataSource] int  NULL,
    [OccurTime] datetime  NULL
);
GO

-- Creating table 'BB_Data_Error_Sta'
CREATE TABLE [dbo].[BB_Data_Error_Sta] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [TableName] nvarchar(200)  NULL,
    [FieldName] nvarchar(200)  NULL,
    [FactoryID] char(4)  NULL,
    [StationID] char(4)  NULL,
    [AbnormalValue] nvarchar(20)  NULL,
    [DataSource] int  NULL,
    [OccurTime] datetime  NULL
);
GO

-- Creating table 'BB_Data_Exception_Area'
CREATE TABLE [dbo].[BB_Data_Exception_Area] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [TableName] nvarchar(200)  NULL,
    [FieldName] nvarchar(200)  NULL,
    [FactoryID] char(4)  NULL,
    [AreaID] char(4)  NULL,
    [AbnormalValue] nvarchar(20)  NULL,
    [DataSource] int  NULL,
    [OccurTime] datetime  NULL
);
GO

-- Creating table 'BB_Data_Exception_Equ'
CREATE TABLE [dbo].[BB_Data_Exception_Equ] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [TableName] nvarchar(200)  NULL,
    [FieldName] nvarchar(200)  NULL,
    [FactoryID] char(4)  NULL,
    [StationID] char(4)  NULL,
    [EquipID] char(4)  NULL,
    [AbnormalValue] nvarchar(20)  NULL,
    [DataSource] int  NULL,
    [OccurTime] datetime  NULL
);
GO

-- Creating table 'BB_Data_Exception_MeasureMeter'
CREATE TABLE [dbo].[BB_Data_Exception_MeasureMeter] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [TableName1] nvarchar(200)  NULL,
    [FieldName1] nvarchar(200)  NULL,
    [FactoryID1] char(4)  NULL,
    [StationID1] char(4)  NULL,
    [EquipID1] char(4)  NULL,
    [AreaID1] char(4)  NULL,
    [value1] nvarchar(20)  NULL,
    [TableName2] nvarchar(200)  NULL,
    [FieldName2] nvarchar(200)  NULL,
    [FactoryID2] char(4)  NULL,
    [StationID2] char(4)  NULL,
    [EquipID2] char(4)  NULL,
    [AreaID2] char(4)  NULL,
    [value2] nvarchar(20)  NULL,
    [Description] nvarchar(200)  NULL,
    [OccurTime] datetime  NULL
);
GO

-- Creating table 'BB_Data_Exception_Sta'
CREATE TABLE [dbo].[BB_Data_Exception_Sta] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [TableName] nvarchar(200)  NULL,
    [FieldName] nvarchar(200)  NULL,
    [FactoryID] char(4)  NULL,
    [StationID] char(4)  NULL,
    [AbnormalValue] nvarchar(20)  NULL,
    [DataSource] int  NULL,
    [OccurTime] datetime  NULL
);
GO

-- Creating table 'BB_Factory'
CREATE TABLE [dbo].[BB_Factory] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [code] char(4)  NULL,
    [Name] nvarchar(10)  NULL,
    [Introduction] nvarchar(10)  NULL,
    [Product] nvarchar(10)  NULL,
    [AnnualOutput] nvarchar(5)  NULL,
    [Latitude] decimal(12,6)  NULL,
    [Longtitude] decimal(12,6)  NULL,
    [Altitude] decimal(12,6)  NULL,
    [Size] decimal(12,6)  NULL,
    [Remark] nvarchar(100)  NULL,
    [ClientID] int  NULL,
    [Valid] char(1)  NULL
);
GO

-- Creating table 'BB_Instrument'
CREATE TABLE [dbo].[BB_Instrument] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Code] char(4)  NULL,
    [Type] char(2)  NULL,
    [Rated_Q] varchar(10)  NULL,
    [DesignPressure] varchar(10)  NULL,
    [Rated_V] varchar(10)  NULL,
    [Rated_A] varchar(10)  NULL,
    [Rated_Power] varchar(10)  NULL,
    [Dewpoint] varchar(10)  NULL,
    [Manufacture] varchar(10)  NULL,
    [RunYear] char(23)  NULL,
    [Remark] nvarchar(100)  NULL,
    [StationID] int  NULL,
    [FactoryID] int  NULL,
    [ClientID] int  NULL,
    [Valid] char(1)  NULL,
    [DateOfProductionTime] datetime  NULL,
    [SerialNumber] nvarchar(20)  NULL,
    [ExFlangeSize] int  NULL,
    [ShapeSize] int  NULL,
    [Refrigerantype] char(2)  NULL,
    [AdsorbentType] char(2)  NULL,
    [FilterNum] int  NULL,
    [GasConsumptionRate] decimal(9,2)  NULL,
    [AreaID] int  NULL,
    [PrimaryIntercoolerPower] decimal(9,2)  NULL,
    [SecondaryIntercoolerPower] decimal(9,2)  NULL,
    [ThirdintercoolerPower] decimal(9,2)  NULL
);
GO

-- Creating table 'BB_MeasureMeter'
CREATE TABLE [dbo].[BB_MeasureMeter] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Code] char(4)  NULL,
    [Type] char(2)  NULL,
    [Variety] char(2)  NULL,
    [MeasurementRange] varchar(10)  NULL,
    [RunYear] char(23)  NULL,
    [CalibrationTime] datetime  NULL,
    [Manufacture] nvarchar(100)  NULL,
    [Location] nvarchar(100)  NULL,
    [Remark] nvarchar(100)  NULL,
    [InstrumentID] int  NULL,
    [AreaID] int  NULL,
    [FactoryID] int  NULL,
    [ClientID] int  NULL,
    [Valid] char(1)  NULL
);
GO

-- Creating table 'BB_Station'
CREATE TABLE [dbo].[BB_Station] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Code] char(4)  NULL,
    [Name] nvarchar(10)  NULL,
    [Introduction] nvarchar(10)  NULL,
    [Latitude] decimal(12,6)  NULL,
    [Longtitude] decimal(12,6)  NULL,
    [Remark] nvarchar(100)  NULL,
    [FactoryID] int  NULL,
    [ClientID] int  NULL,
    [Valid] char(1)  NULL,
    [GasMainSize] int  NULL,
    [DeviceBindMode] char(1)  NULL,
    [StationArea] decimal(12,6)  NULL,
    [NearGasArea] decimal(12,6)  NULL,
    [StationControlSystem] char(1)  NULL
);
GO

-- Creating table 'BB_Threshold'
CREATE TABLE [dbo].[BB_Threshold] (
    [ID] smallint IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NULL,
    [SetValue] decimal(9,2)  NULL,
    [CurrentValue] decimal(9,2)  NULL,
    [classify] nvarchar(5)  NULL,
    [Description] nvarchar(50)  NULL
);
GO

-- Creating table 'OpcHelperItem'
CREATE TABLE [dbo].[OpcHelperItem] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EnumTag] nvarchar(max)  NOT NULL,
    [Tag] nvarchar(max)  NOT NULL,
    [Value] nvarchar(max)  NULL,
    [Time] nvarchar(max)  NULL,
    [Quality] nvarchar(max)  NULL
);
GO

-- Creating table 'OpcHelperItems'
CREATE TABLE [dbo].[OpcHelperItems] (
    [Id] int  NOT NULL,
    [EnumTag] nvarchar(max)  NULL,
    [Tag] nvarchar(max)  NULL,
    [Value] nvarchar(max)  NULL,
    [Time] nvarchar(max)  NULL,
    [Quality] nvarchar(max)  NULL
);
GO

-- Creating table 'PG_Area_History'
CREATE TABLE [dbo].[PG_Area_History] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DateTime] datetime  NOT NULL,
    [DayID] char(2)  NULL,
    [FactoryID] char(4)  NULL,
    [AreaID] char(4)  NULL,
    [Inlet_SQ] decimal(12,2)  NULL,
    [Inlet_AP] decimal(12,2)  NULL
);
GO

-- Creating table 'PG_Area_Real'
CREATE TABLE [dbo].[PG_Area_Real] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DateTime] datetime  NULL,
    [DayID] char(2)  NULL,
    [FactoryID] char(4)  NULL,
    [AreaID] char(4)  NULL,
    [Inlet_SQ] decimal(12,2)  NULL,
    [Inlet_AP] decimal(12,2)  NULL
);
GO

-- Creating table 'PG_Area_Statistics'
CREATE TABLE [dbo].[PG_Area_Statistics] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DateTime] datetime  NULL,
    [DayID] char(2)  NULL,
    [FactoryID] char(4)  NULL,
    [AreaID] char(4)  NULL,
    [YMDH] char(1)  NULL,
    [Inlet_SQ] decimal(12,2)  NULL,
    [Inlet_AP] decimal(12,2)  NULL
);
GO

-- Creating table 'PLCBase'
CREATE TABLE [dbo].[PLCBase] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SignName] nvarchar(100)  NULL,
    [SignType] nvarchar(100)  NULL,
    [PLCSignValue] bit  NULL,
    [SingAlias] nvarchar(100)  NULL
);
GO

-- Creating table 'RE_SysRolePage'
CREATE TABLE [dbo].[RE_SysRolePage] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [SysRoleID] int  NULL,
    [PageID] int  NULL
);
GO

-- Creating table 'RE_SysRoleUser'
CREATE TABLE [dbo].[RE_SysRoleUser] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserID] uniqueidentifier  NULL,
    [SysRoleID] int  NULL
);
GO

-- Creating table 'Re_UserRole1'
CREATE TABLE [dbo].[Re_UserRole1] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserID] uniqueidentifier  NULL,
    [RoleID] uniqueidentifier  NULL
);
GO

-- Creating table 'RE_UserStation'
CREATE TABLE [dbo].[RE_UserStation] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserID] uniqueidentifier  NOT NULL,
    [StationID] int  NULL,
    [FactoryID] int  NULL
);
GO

-- Creating table 'SB_Client'
CREATE TABLE [dbo].[SB_Client] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(20)  NULL,
    [Type] char(2)  NULL,
    [Address] nvarchar(50)  NULL,
    [Telephone] varchar(12)  NULL,
    [CompanyMail] varchar(30)  NULL,
    [Contacts] nvarchar(4)  NULL,
    [ContactsCellone] char(11)  NULL,
    [ContactsMail] varchar(30)  NULL,
    [Remark] nvarchar(100)  NULL,
    [state] char(1)  NULL
);
GO

-- Creating table 'SB_Log'
CREATE TABLE [dbo].[SB_Log] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserID] int  NULL,
    [UserName] nvarchar(5)  NULL,
    [ClientID] int  NULL,
    [IPAddress] varchar(15)  NULL,
    [PageURL] varchar(100)  NULL,
    [PageNameCN] nvarchar(20)  NULL,
    [OperateType] char(1)  NULL,
    [OperateRecord] nvarchar(500)  NULL,
    [OperateTime] datetime  NULL,
    [Remark] nvarchar(100)  NULL,
    [Valid] char(1)  NULL
);
GO

-- Creating table 'SB_OperateHistory'
CREATE TABLE [dbo].[SB_OperateHistory] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserId] uniqueidentifier  NULL,
    [UserName] nvarchar(5)  NULL,
    [IPAddress] varchar(15)  NULL,
    [OperateType] char(2)  NULL,
    [OperateChildType] char(2)  NULL,
    [PageURL] varchar(100)  NULL,
    [PageNameCN] nvarchar(20)  NULL,
    [OperateRecord] nvarchar(500)  NULL,
    [OperateTime] datetime  NULL,
    [Remark] nvarchar(100)  NULL,
    [InstrumentID] int  NULL,
    [AreaID] int  NULL,
    [StationID] int  NULL,
    [FactoryID] int  NULL,
    [ClientID] int  NULL,
    [Valid] char(1)  NULL
);
GO

-- Creating table 'SB_Page'
CREATE TABLE [dbo].[SB_Page] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ShowName] nvarchar(20)  NULL,
    [PageNameCN] nvarchar(20)  NULL,
    [PageNameEN] varchar(50)  NULL,
    [URL] varchar(200)  NULL,
    [ParentID] int  NULL,
    [SortN] int  NULL,
    [Remark] nvarchar(200)  NULL,
    [Valid] char(1)  NULL,
    [IconClass] varchar(50)  NULL
);
GO

-- Creating table 'SB_SysRole'
CREATE TABLE [dbo].[SB_SysRole] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(20)  NULL,
    [Valid] char(1)  NULL,
    [Remark] nvarchar(100)  NULL
);
GO

-- Creating table 'SB_TableDictionary'
CREATE TABLE [dbo].[SB_TableDictionary] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [TableNameEN] varchar(40)  NULL,
    [TableNameCN] nvarchar(20)  NULL,
    [ColumnNameEN] varchar(40)  NULL,
    [ColumnNameCN] nvarchar(20)  NULL,
    [DicCode] varchar(5)  NULL,
    [DicCodeCN] nvarchar(20)  NULL,
    [IsDefault] char(1)  NULL,
    [IsMaintenance] char(1)  NULL,
    [SortNo] int  NULL,
    [Remark] nvarchar(100)  NULL,
    [Valid] char(1)  NULL
);
GO

-- Creating table 'SB_User'
CREATE TABLE [dbo].[SB_User] (
    [ID] uniqueidentifier  NOT NULL,
    [Account] varchar(50)  NULL,
    [PassWord] varchar(20)  NULL,
    [Name] nvarchar(50)  NULL,
    [UserType] char(1)  NULL,
    [Sex] char(1)  NULL,
    [IDNo] varchar(18)  NULL,
    [Cellphone] nchar(11)  NULL,
    [Email] varchar(30)  NULL,
    [ClientID] int  NULL,
    [Remark] nvarchar(100)  NULL,
    [State] char(1)  NULL
);
GO

-- Creating table 'SB_WarnLog'
CREATE TABLE [dbo].[SB_WarnLog] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [WarnType] char(1)  NULL,
    [WarnContent] nvarchar(50)  NULL,
    [WarnTime] datetime  NULL,
    [InstrumentID] int  NULL,
    [AreaID] int  NULL,
    [StationID] int  NULL,
    [FactoryID] int  NULL,
    [Remark] nvarchar(100)  NULL,
    [Status] char(1)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'AP_AdsorptionDryer_History'
ALTER TABLE [dbo].[AP_AdsorptionDryer_History]
ADD CONSTRAINT [PK_AP_AdsorptionDryer_History]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'AP_AdsorptionDryer_Real'
ALTER TABLE [dbo].[AP_AdsorptionDryer_Real]
ADD CONSTRAINT [PK_AP_AdsorptionDryer_Real]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'AP_AdsorptionDryer_Statistics'
ALTER TABLE [dbo].[AP_AdsorptionDryer_Statistics]
ADD CONSTRAINT [PK_AP_AdsorptionDryer_Statistics]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Id] in table 'AP_Centrifuge_History'
ALTER TABLE [dbo].[AP_Centrifuge_History]
ADD CONSTRAINT [PK_AP_Centrifuge_History]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AP_Centrifuge_Real'
ALTER TABLE [dbo].[AP_Centrifuge_Real]
ADD CONSTRAINT [PK_AP_Centrifuge_Real]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AP_Centrifuge_Statistics'
ALTER TABLE [dbo].[AP_Centrifuge_Statistics]
ADD CONSTRAINT [PK_AP_Centrifuge_Statistics]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AP_Factory_History'
ALTER TABLE [dbo].[AP_Factory_History]
ADD CONSTRAINT [PK_AP_Factory_History]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AP_Factory_Real'
ALTER TABLE [dbo].[AP_Factory_Real]
ADD CONSTRAINT [PK_AP_Factory_Real]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AP_Factory_Statistics'
ALTER TABLE [dbo].[AP_Factory_Statistics]
ADD CONSTRAINT [PK_AP_Factory_Statistics]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AP_PressDesign_Conclusion'
ALTER TABLE [dbo].[AP_PressDesign_Conclusion]
ADD CONSTRAINT [PK_AP_PressDesign_Conclusion]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ID] in table 'AP_PressDesign_Real'
ALTER TABLE [dbo].[AP_PressDesign_Real]
ADD CONSTRAINT [PK_AP_PressDesign_Real]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Id] in table 'AP_Station_History'
ALTER TABLE [dbo].[AP_Station_History]
ADD CONSTRAINT [PK_AP_Station_History]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AP_Station_Real'
ALTER TABLE [dbo].[AP_Station_Real]
ADD CONSTRAINT [PK_AP_Station_Real]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AP_Station_Statistics'
ALTER TABLE [dbo].[AP_Station_Statistics]
ADD CONSTRAINT [PK_AP_Station_Statistics]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AP_WaterPump_History'
ALTER TABLE [dbo].[AP_WaterPump_History]
ADD CONSTRAINT [PK_AP_WaterPump_History]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AP_WaterPump_Real'
ALTER TABLE [dbo].[AP_WaterPump_Real]
ADD CONSTRAINT [PK_AP_WaterPump_Real]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AP_WaterPump_Statistics'
ALTER TABLE [dbo].[AP_WaterPump_Statistics]
ADD CONSTRAINT [PK_AP_WaterPump_Statistics]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ID] in table 'BB_Area'
ALTER TABLE [dbo].[BB_Area]
ADD CONSTRAINT [PK_BB_Area]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Id] in table 'BB_ChartAlias'
ALTER TABLE [dbo].[BB_ChartAlias]
ADD CONSTRAINT [PK_BB_ChartAlias]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ID] in table 'BB_Data_Check'
ALTER TABLE [dbo].[BB_Data_Check]
ADD CONSTRAINT [PK_BB_Data_Check]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'BB_Data_Error_Area'
ALTER TABLE [dbo].[BB_Data_Error_Area]
ADD CONSTRAINT [PK_BB_Data_Error_Area]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'BB_Data_Error_Equ'
ALTER TABLE [dbo].[BB_Data_Error_Equ]
ADD CONSTRAINT [PK_BB_Data_Error_Equ]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'BB_Data_Error_Sta'
ALTER TABLE [dbo].[BB_Data_Error_Sta]
ADD CONSTRAINT [PK_BB_Data_Error_Sta]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'BB_Data_Exception_Area'
ALTER TABLE [dbo].[BB_Data_Exception_Area]
ADD CONSTRAINT [PK_BB_Data_Exception_Area]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'BB_Data_Exception_Equ'
ALTER TABLE [dbo].[BB_Data_Exception_Equ]
ADD CONSTRAINT [PK_BB_Data_Exception_Equ]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'BB_Data_Exception_MeasureMeter'
ALTER TABLE [dbo].[BB_Data_Exception_MeasureMeter]
ADD CONSTRAINT [PK_BB_Data_Exception_MeasureMeter]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'BB_Data_Exception_Sta'
ALTER TABLE [dbo].[BB_Data_Exception_Sta]
ADD CONSTRAINT [PK_BB_Data_Exception_Sta]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'BB_Factory'
ALTER TABLE [dbo].[BB_Factory]
ADD CONSTRAINT [PK_BB_Factory]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'BB_Instrument'
ALTER TABLE [dbo].[BB_Instrument]
ADD CONSTRAINT [PK_BB_Instrument]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'BB_MeasureMeter'
ALTER TABLE [dbo].[BB_MeasureMeter]
ADD CONSTRAINT [PK_BB_MeasureMeter]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'BB_Station'
ALTER TABLE [dbo].[BB_Station]
ADD CONSTRAINT [PK_BB_Station]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'BB_Threshold'
ALTER TABLE [dbo].[BB_Threshold]
ADD CONSTRAINT [PK_BB_Threshold]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Id] in table 'OpcHelperItem'
ALTER TABLE [dbo].[OpcHelperItem]
ADD CONSTRAINT [PK_OpcHelperItem]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OpcHelperItems'
ALTER TABLE [dbo].[OpcHelperItems]
ADD CONSTRAINT [PK_OpcHelperItems]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PG_Area_History'
ALTER TABLE [dbo].[PG_Area_History]
ADD CONSTRAINT [PK_PG_Area_History]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PG_Area_Real'
ALTER TABLE [dbo].[PG_Area_Real]
ADD CONSTRAINT [PK_PG_Area_Real]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PG_Area_Statistics'
ALTER TABLE [dbo].[PG_Area_Statistics]
ADD CONSTRAINT [PK_PG_Area_Statistics]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PLCBase'
ALTER TABLE [dbo].[PLCBase]
ADD CONSTRAINT [PK_PLCBase]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ID] in table 'RE_SysRolePage'
ALTER TABLE [dbo].[RE_SysRolePage]
ADD CONSTRAINT [PK_RE_SysRolePage]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'RE_SysRoleUser'
ALTER TABLE [dbo].[RE_SysRoleUser]
ADD CONSTRAINT [PK_RE_SysRoleUser]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Re_UserRole1'
ALTER TABLE [dbo].[Re_UserRole1]
ADD CONSTRAINT [PK_Re_UserRole1]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'RE_UserStation'
ALTER TABLE [dbo].[RE_UserStation]
ADD CONSTRAINT [PK_RE_UserStation]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'SB_Client'
ALTER TABLE [dbo].[SB_Client]
ADD CONSTRAINT [PK_SB_Client]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'SB_Log'
ALTER TABLE [dbo].[SB_Log]
ADD CONSTRAINT [PK_SB_Log]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'SB_OperateHistory'
ALTER TABLE [dbo].[SB_OperateHistory]
ADD CONSTRAINT [PK_SB_OperateHistory]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'SB_Page'
ALTER TABLE [dbo].[SB_Page]
ADD CONSTRAINT [PK_SB_Page]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'SB_SysRole'
ALTER TABLE [dbo].[SB_SysRole]
ADD CONSTRAINT [PK_SB_SysRole]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'SB_TableDictionary'
ALTER TABLE [dbo].[SB_TableDictionary]
ADD CONSTRAINT [PK_SB_TableDictionary]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'SB_User'
ALTER TABLE [dbo].[SB_User]
ADD CONSTRAINT [PK_SB_User]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'SB_WarnLog'
ALTER TABLE [dbo].[SB_WarnLog]
ADD CONSTRAINT [PK_SB_WarnLog]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------