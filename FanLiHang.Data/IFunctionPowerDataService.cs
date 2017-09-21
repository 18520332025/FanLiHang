using System;
using System.Collections.Generic;
using System.Text;
using FanLiHang.Model;
namespace FanLiHang.Data
{
    public interface IFunctionPowerDataService
    {
        List<FunctionPower> GetList(int appID);
        bool Add(FunctionPower functionPower);
        bool Update(FunctionPower functionPower);
        FunctionPower GetModel(int ID);
        //bool Delete(FunctionPower functionPower);
    }
}
