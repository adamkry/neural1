using Matches.Model;
using Matches.Neural.Learning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matches.Business
{
    public static class MatchParams
    {
        public static MatchParamDefinition GetResultValue(IEnumerable<Match> matches)
        {
            return new MatchParamDefinition
            {
                Name = "Result",
                GetValue = (c, all) => c.HomeGoals - c.AwayGoals
            }
            .SetMinMaxValues(matches);
        }

        public static MatchParamDefinition GetNumberOfGoals(IEnumerable<Match> matches)
        {
            return new MatchParamDefinition
            {
                Name = "GoalNo",
                GetValue = (c, all) => c.HomeGoals + c.AwayGoals
            }
            .SetMinMaxValues(matches);
        }

        public static MatchParamDefinition GetLastFiveHomeMatches(IEnumerable<Match> matches)
        {
            return new MatchParamDefinition
            {
                Name = "Last5",
                GetValue = (c, all) => 
                {
                    var last5 = all
                        .Where(a => a.HomeTeam == c.HomeTeam 
                            && a.Season == c.Season 
                            && a.StartYear == c.StartYear
                            && a.Round < c.Round
                            && a.Round >= c.Round - 5)
                        .ToList();
                    return last5.Count > 0 
                        ? ((double)(last5.Sum(m => m.HomeGoals - m.AwayGoals)))/(double)last5.Count
                        : 0.0;
                }
            }
            .SetMinMaxValues(matches);
        }

        public static MatchParamDefinition GetLastFiveAwayMatches(IEnumerable<Match> matches)
        {
            return new MatchParamDefinition
            {
                Name = "Last5",
                GetValue = (c, all) =>
                {
                    var last5 = all
                        .Where(a => a.AwayTeam == c.AwayTeam
                            && a.Season == c.Season
                            && a.StartYear == c.StartYear
                            && a.Round < c.Round
                            && a.Round >= c.Round - 5)
                        .ToList();
                    return last5.Count > 0
                        ? ((double)(last5.Sum(m => m.AwayGoals - m.HomeGoals))) / (double)last5.Count
                        : 0.0;
                }
            }
            .SetMinMaxValues(matches);
        }
    }

    public class MatchParamDefinition : IParamDefinition<Match>
    {
        public double MaxValue
        {
            get;set;
        }

        public double MinValue
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public Func<Match, IEnumerable<Match>, double> GetValue
        {
            get; set;
        }
    }
}
