using NSI.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Common.Helpers
{
    public static class FilterHelper
    {
        public static IQueryable<T> ApplyFilterRule<T>(IQueryable<T> data, FilterCriteria rule, Func<string, string, bool, Expression<Func<T, bool>>> filterFunctor)
        {
            if (rule == null || string.IsNullOrWhiteSpace(rule.ColumnName))
            {
                return data;
            }

            Expression<Func<T, bool>> fnc = filterFunctor(rule.ColumnName, rule.FilterTerm, rule.IsExactMatch);

            if (fnc == null)
            {
                throw new ArgumentException("fnc");
            }

            return data.Where(fnc);
        }
    }
}
