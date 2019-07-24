using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper
{
   public class OperatRecord
    {
        IRepository.ISB_OperateHistoryRepository omodel = new Repository.SB_OperateHistoryRepository();
        IRepository.ISB_PageRepository pmodel = new Repository.SB_PageRepository();
        IRepository.IBB_InstrumentRepository imodel = new Repository.BB_InstrumentRepository();
        public int operating(int? InstrumentID, string OperateType, string OperateChildType, string pageURL, Model.SB_User model)
        {
            //GetIpHelper ipmodel = new GetIpHelper();
            var ip = "";
            var time = DateTime.Now;                     
            if (OperateType=="0")
            {
              var pageres= pmodel.QueryWhere(p => p.URL.Equals(pageURL)).FirstOrDefault();
                string[] str = new string[] { "未知", "登录", "退出", "查询", "新增", "修改", "删除" };
                string childType = "";
                switch (OperateChildType)
                {
                    case "0":
                        childType += model.Name+"在时间"+time+"进行了未知的操作";
                        break;
                    case "1":
                        childType += model.Name + "在时间" + time + "进行了"+str[1]+"的操作";
                        break;
                    case "2":
                        childType += model.Name + "在时间" + time + "进行了"+str[2]+"的操作";
                        break;
                    case "3":
                        childType += model.Name + "在时间" + time + "进行了" + str[3] + "的操作";
                        break;
                    case "4":
                        childType += model.Name + "在时间" + time + "进行了" + str[4] + "的操作";
                        break;
                    case "5":
                        childType += model.Name + "在时间" + time + "进行了" + str[5] + "的操作";
                        break;
                    case "6":
                        childType += model.Name + "在时间" + time + "进行了" + str[6] + "的操作";
                        break;
                }

                Model.SB_OperateHistory operatmodel = new Model.SB_OperateHistory() {
                    UserId=model.ID,
                    UserName=model.Name,
                    IPAddress=ip,
                    PageURL=pageURL,
                    PageNameCN=pageres.PageNameCN,
                    OperateTime=time,
                    OperateChildType= OperateChildType,
                    OperateRecord=childType,
                    OperateType=OperateType,
                    ClientID=model.ClientID,
                    Remark="等待",
                    Valid="1"
                };
                omodel.Add(operatmodel);
               int a= omodel.SaveChanges();
                if (a > 0)
                {
                    return a;
                }
                else
                {
                    return 0;
                }
            }
            else if (OperateType == "1")
            {
                var instrures = imodel.QueryWhere(i => i.ID == InstrumentID).FirstOrDefault();
                string[] str = new string[] { "空压机操作", "干燥机操作", "过滤器操作", "冷干机操作", "", "", "" };
                string record = "";
                switch (OperateChildType)
                {
                    case "7":
                        record += model.Name + "在时间" + time + "进行了"+str[0]+"的操作";
                        break;
                    case "8":
                        record += model.Name + "在时间" + time + "进行了" + str[1] + "的操作";
                        break;
                    case "9":
                        record += model.Name + "在时间" + time + "进行了" + str[2] + "的操作";
                        break;
                    case "10":
                        record += model.Name + "在时间" + time + "进行了" + str[3] + "的操作";
                        break;                    
                }
                Model.SB_OperateHistory operatmodel = new Model.SB_OperateHistory()
                {
                    UserId = model.ID,
                    UserName = model.Name,
                    IPAddress = ip,
                    PageURL = pageURL,                  
                    OperateTime = time,
                    OperateChildType = OperateChildType,
                    OperateRecord = record,
                    OperateType = OperateType,
                    ClientID = instrures.ClientID,
                    AreaID=instrures.AreaID,
                    StationID=instrures.StationID,
                    FactoryID=instrures.FactoryID,
                    InstrumentID= instrures.ID,                   
                    Remark = "等待",
                    Valid = "1"
                };
                omodel.Add(operatmodel);
                int a = omodel.SaveChanges();
                if (a > 0)
                {
                    return a;
                }
                else
                {
                    return 0;
                }
            }
            return 0;
        }
    }
}
