using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;

namespace Others
{
    class MyExpression
    {       
        public void CreateSqrExpression()
        {
            //Func<int, double> add = x => Math.Pow(x, 2);

            ParameterExpression param = Expression.Parameter(typeof(double));
            ConstantExpression cons = Expression.Constant((double)2);
            
            Expression exp = Expression.Power(param, cons);

            LambdaExpression lambda = LambdaExpression.Lambda(exp, param);

            Func<double, double> func = (Func<double, double>)lambda.Compile();
            Console.WriteLine("Sqr {0} = {1}", 5, func(5));

        }

        public void CreateWhereExpression()
        {
            List<int> list = new List<int>();
            Action<int> fillList = null;
            fillList = x => { if (x >= 0) { list.Add(10 - x); fillList(--x); } };
            fillList(10);
            //list = list.Where(x => x % 2 == 0).OrderByDescending(x => x).ToList();

            ParameterExpression param = Expression.Parameter(typeof(int));
            ConstantExpression twoConst = Expression.Constant(2);
            ConstantExpression zeroConst = Expression.Constant(0);

            Expression modExp = Expression.Modulo(param, twoConst);
            Expression cond = Expression.Equal(modExp, zeroConst);
            
            LambdaExpression lambda = LambdaExpression.Lambda(cond, param);

            Func<int, bool> func = (Func<int, bool>)lambda.Compile();

            ParameterExpression ordParam = Expression.Parameter(typeof(int));
            LambdaExpression lambdaOrd = LambdaExpression.Lambda(param, param);
            Func<int, int> funcOrd = (Func<int, int>)lambdaOrd.Compile();

            foreach (var v in list.Where(func).OrderByDescending(funcOrd))
                Console.WriteLine(v);
        }
    }
}
