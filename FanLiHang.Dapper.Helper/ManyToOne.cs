using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace FanLiHang.Dapper.Helper
{
    public class ManyToOne<T1, T2>
    {
        public ManyToOne(GetKey<T1> getKey, GetList<T1, T2> getList, Expression<GetName<T2>> GetManyName)
        {
            this.GetKeyFunc = getKey;
            this.GetListFunc = getList;
            this.ManyName = ListBindExpressionAnalysis<T2>(GetManyName);
        }


        protected string ListBindExpressionAnalysis<T1>(Expression<GetName<T1>> func)
        {
            if (func.Body.NodeType == ExpressionType.MemberAccess)
            {
                MemberExpression binaryExp = func.Body as MemberExpression;
                return binaryExp.Member.Name;
            }
            else if (func.Body.NodeType == ExpressionType.Convert)
            {
                UnaryExpression unaryExp = func.Body as UnaryExpression;
                if (unaryExp.Operand.NodeType == ExpressionType.MemberAccess)
                {
                    MemberExpression binaryExp = unaryExp.Operand as MemberExpression;
                    return binaryExp.Member.Name;
                }
                return null;
            }
            else
            {
                return null;
            }
        }

        public delegate object GetKey<T1>(T1 t1);

        public delegate object GetName<T1>(T1 t1);

        public delegate List<T2> GetList<T1, T2>(T1 t1);

        public GetList<T1, T2> GetListFunc { get; set; }
        public GetKey<T1> GetKeyFunc { get; set; }
        public string ManyName { get; set; }
    }
}
