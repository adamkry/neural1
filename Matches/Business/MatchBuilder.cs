using HtmlAgilityPack;
using Matches.Common;
using Matches.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matches.Business
{
    public class MatchBuilder
    {
        string url;
        HtmlDocument html;

        public MatchBuilder(string matchUrl)
        {
            url = matchUrl;
        }

        public Match ParseFromUrl()
        {
            html = HtmlDocumentCreator.Get(url);
            Match match = new Match
            {
                Id = url,
                Attendence = GetAttendence(),
                AwayTeam = GetAwayTeam(),
                HomeTeam = GetHomeTeam(),                
                Referee = GetReferee(),                
                Time = GetTime()
            };
            SetLeague(match);
            GetResult(match);
            return match;
        }

        private string GetAwayTeam()
        {
            return html
                .DocumentNode
                .Descendants("nobr")
                .Single()
                .GetParentWhere(n => n.OriginalName == "td")
                .GetNextSiblingWhere(n => n.OriginalName == "td")
                .InnerText;            
        }

        private string GetHomeTeam()
        {
            return html
                .DocumentNode
                .Descendants("nobr")
                .Single()
                .GetParentWhere(n => n.OriginalName == "td")
                .GetPreviousSiblingWhere(n => n.OriginalName == "td")
                .InnerText;
        }

        private void GetResult(Match match)
        {
            string result = html
                .DocumentNode
                .Descendants("nobr")
                .Single()
                .InnerText;

            var goals = result.Split(new char[] { '-' });
            if (goals.Length == 2)
            {
                match.HomeGoals = Int32.Parse(goals[0]);
                match.AwayGoals = Int32.Parse(goals[1]);
            }            
        }

        private string GetReferee()
        {
            return html
                .DocumentNode
                .Descendants("a")
                .Single(a => a.GetAttributeValue("href", "").StartsWith("/sedzia.php?"))
                .InnerText;
        }

        private int GetAttendence()
        {
            int attendence = 0;
            Int32.TryParse(GetTextAfterImg("http://img.90minut.pl/img/att.gif"), out attendence);
            return attendence;
        }

        private DateTime GetTime()
        {
            string timeStr = GetTextAfterImg("http://img.90minut.pl/img/data.gif");
            return ParseTime(timeStr);
        }

        public static DateTime ParseTime(string time)
        {
            string dateStr = time
                    .Replace(" stycznia ", "-01-")
                    .Replace(" lutego ", "-02-")
                    .Replace(" marca ", "-03-")
                    .Replace(" kwietnia ", "-04-")
                    .Replace(" maja ", "-05-")
                    .Replace(" czerwca ", "-06-")
                    .Replace(" lipca ", "-07-")
                    .Replace(" sierpnia ", "-08-")
                    .Replace(" wrze¶nia ", "-09-")
                    .Replace(" pa¼dziernika ", "-10-")
                    .Replace(" listopada ", "-11-")
                    .Replace(" grudnia ", "-12-");
            string exact = "d-MM-yyyy, HH:mm";
            DateTime date = DateTime.ParseExact(dateStr, exact, CultureInfo.InvariantCulture);
            return date;
        }

        private void SetLeague(Match match)
        {
            string mLeague = html
                .DocumentNode
                .Descendants("b")
                .Select(b => b.InnerText)
                .Where(t => t.Contains("Kolejka"))
                .Single();

            var league = mLeague.Split(new char[] { '-' });
            if (league.Length > 1)
            {
                if (!String.IsNullOrWhiteSpace(league[league.Length - 2]))
                {
                    var leagueDesc = league[league.Length - 2].Split(new char[] { ' ' },
                        StringSplitOptions.RemoveEmptyEntries);
                    if (leagueDesc.Length > 1)
                    {
                        match.Season = leagueDesc[leagueDesc.Length - 1].Trim();
                    }
                }
                if (!String.IsNullOrWhiteSpace(league[league.Length - 1]))
                {
                    var leagueDesc = league[league.Length - 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (leagueDesc.Length > 1)
                    {
                        string round = leagueDesc[leagueDesc.Length - 1].Trim();
                        match.Round = Int32.Parse(round);
                    }
                }
            }
            match.StartYear = Int32
                .Parse(match.Season.Substring(0, 4));            
        }

        private string GetTextAfterImg(string imageSrc)
        {
            return html
                .DocumentNode
                .Descendants("img")
                .SingleOrDefault(img => img.GetAttributeValue("src", "") == imageSrc)
                ?.NextSibling
                ?.InnerText
                ?.Replace("&nbsp;", "");
        }
    }
}
