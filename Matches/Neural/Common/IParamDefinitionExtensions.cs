using Encog.Util.Arrayutil;
using Matches.Neural.Learning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matches.Neural.Common
{
    public static class IParamDefinitionExtensions
    {
        public static double[][] GetValues<T>(this IList<IParamDefinition<T>> @params, IList<T> dataSource)
        {
            double[][] input = new double[dataSource.Count][];
            
            for (int col = 0; col < @params.Count; col++)
            {
                NormalizedField _normalizedParam = new NormalizedField(NormalizationAction.Normalize,
                    "Weights", @params[col].MaxValue, @params[col].MinValue, 1.0, -1.0);

                for (int row = 0; row < dataSource.Count; row++)
                {
                    if (col == 0)
                    {
                        input[row] = new double[@params.Count];
                    }
                    input[row][col] = Normalization.Normalize(@params[col].GetValue(dataSource[row], dataSource), _normalizedParam);
                }
            }            
            return input;
        }
        
        public static double GetNormalizedValue<T>(this IParamDefinition<T> param, T item, IEnumerable<T> items)
        {            
            return normalizedParam.Normalize(param.GetValue(item, items));
        }
    }
}
