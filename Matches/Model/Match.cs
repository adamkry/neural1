using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matches.Model
{
    public class Match
    {
        /// <summary>
        /// Url
        /// </summary>
        public string Id { get; set; }
        
        public int Round { get; set; }

        public int StartYear { get; set; }
        
        public string Season { get; set; }

        public int Attendence { get; set; }

        public string Referee { get; set; }

        public string HomeTeam { get; set; }

        public int HomeGoals { get; set; }

        public string AwayTeam { get; set; }

        public int AwayGoals { get; set; }
        
        public DateTime Time { get; set; }
    }
}
