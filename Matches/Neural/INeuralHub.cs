using Matches.Neural.Learning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matches.Neural
{
    public interface INeuralHub<T>
    {
        List<IParamDefinition<T>> InputParams { get; set; }
        List<IParamDefinition<T>> OutputParams { get; set; }

        IEnumerable<T> DataSource { get; set; }
    }
}
