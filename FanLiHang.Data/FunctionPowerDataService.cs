using System;
using System.Collections.Generic;
using System.Text;
using FanLiHang.Dapper.Helper;
using FanLiHang.Model;
using System.Linq;
namespace FanLiHang.Data
{
    public class FunctionPowerDataService : IFunctionPowerDataService
    {
        IDbHelper _dbHelper;
        public FunctionPowerDataService(IDbHelper _dbHelper)
        {
            this._dbHelper = _dbHelper;
        }

        public bool Add(FunctionPower functionPower)
        {
            if (functionPower.FatharFunctionID == 0)
            {
                functionPower.SortPath = functionPower.Sort.ToString();
            }
            else
            {
                var father = GetModel(functionPower.FatharFunctionID);
                functionPower.SortPath = father.SortPath + "_" + functionPower.Sort.ToString();
            }
            return _dbHelper.Insert(functionPower);
        }

        public List<FunctionPower> GetList(int appID)
        {
            List<FunctionPower> list = new List<FunctionPower>();
            var models = _dbHelper.GetModelList<FunctionPower>(null, null);
            return models.Where(x => x.AppInfoID == appID).ToList();
        }

        

        public FunctionPower GetModel(int ID)
        {
            return _dbHelper.GetModel<FunctionPower>(new FunctionPower { ID = ID });
        }

        public bool Update(FunctionPower functionPower)
        {
            var old = GetModel(functionPower.ID);
            if (functionPower.FatharFunctionID == 0)
            {
                functionPower.SortPath = functionPower.Sort.ToString();
            }
            else
            {
                var father = GetModel(functionPower.FatharFunctionID);
                functionPower.SortPath = father.SortPath + "_" + functionPower.Sort.ToString();
            }

            var list = _dbHelper.GetModelList<FunctionPower>(" and SortPath like '" + old.SortPath + "_%'");

            foreach (var item in list)
            {
                var indexof = item.SortPath.IndexOf(old.SortPath);
                item.SortPath = functionPower.SortPath + item.SortPath.Substring(indexof + old.SortPath.Length);
                _dbHelper.Update(item);
            }

            return _dbHelper.Update(functionPower);
        }
    }
}
