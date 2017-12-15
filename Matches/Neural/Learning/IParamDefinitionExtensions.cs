using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matches.Neural.Learning
{
    public static class IParamDefinitionExtensions
    {
        public static T SetMinMaxValues<T, TParam>(this T param, IEnumerable<TParam> items)
            where T : IParamDefinition<TParam>
        {
            param.MinValue = Double.MaxValue;
            param.MaxValue = Double.MinValue;
            foreach (var item in items)
            {
                double value = param.GetValue(item, items);
                param.MinValue = Math.Min(param.MinValue, value);
                param.MaxValue = Math.Max(param.MaxValue, value);
            }
            return param;
        }
    }
}
