using Encog.Util.Arrayutil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matches.Neural.Common
{
    public class Normalization
    {
        public static double Normalize(double value, NormalizedField field)
        {            
            return field.Normalize(value);
        }

        public static double Normalize(double value, double min, double max)
        {
            return new NormalizedField(NormalizationAction.Normalize,
                    "Weights", max, min, 1.0, -1.0)
                .Normalize(value);
        }
    }
}
