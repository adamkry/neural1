using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matches.Neural.Learning
{
    public interface IParamDefinition<T>
    {
        string Name { get; set; }

        Func<T, IEnumerable<T>, double> GetValue { get; set; }

        double MinValue { get; set; }

        double MaxValue { get; set; }
    }
}
