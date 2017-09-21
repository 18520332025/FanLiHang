using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FanLiHang.Model;
namespace FanLiHang.Admins.Models.FunctionPowerViewModels
{
    public class ExternalFunctionPowerViewModel : FunctionPower
    {
        public bool Requisite
        {
            get; set;
        }
         

        public string Color
        {
            get;
            set;
        }

        public string Icon
        {
            get;
            set;
        }

        public List<ExternalFunctionPowerViewModel> nodes
        {
            get;
            set;
        }

        public string text
        {
            get
            {
                return FunctionName;
            }
            set
            {
                FunctionName = value;
            }
        }

        public ExternalFunctionPowerViewModelState State
        {
            get; set;
        }
    }

    public class ExternalFunctionPowerViewModelState
    {
        public bool Checked { get; set; }
    }

    public static class ExternalFunctionPowerSort
    {
        public static List<ExternalFunctionPowerViewModel> Sort(IEnumerable<ExternalFunctionPowerViewModel> models)
        {
            List<ExternalFunctionPowerViewModel> list = new List<ExternalFunctionPowerViewModel>();
            foreach (var m in models)
            {
                if (m.Level == 1)
                {
                    list.Add(m);
                }
                m.nodes = models.Where(y => y.FatharFunctionID == m.ID).OrderBy(x => x.SortPath).ToList();
                if (m.nodes.Count == 0)
                    m.nodes = null;
            }
            return list.OrderBy(x => x.SortPath).ToList();
        }
    }
}
