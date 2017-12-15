using HtmlAgilityPack;
using Matches.Business;
using Matches.Common;
using Matches.Model;
using Matches.Neural;
using Matches.Neural.Computing;
using Matches.Neural.Learning;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Matches
{
    class Program
    {
        const string commands = "? import exit load";
        static void Main(string[] args)
        {
            string input = String.Empty;
            Console.WriteLine(commands);
            do
            {
                input = Console.ReadLine();
                switch(input)
                {
                    case "?":
                        Console.WriteLine(commands);
                        break;
                    case "import":
                        ImportAllSeasons();
                        break;
                    case "load":
                        LoadAllMatches();
                        break;
                    case "nn":
                        Predict();
                        break;
                }
            }
            while (input != "exit");
        }

        private static void Predict()
        {
            var matches = JsonDb.GetMatches();

            ILearningScenario<Match> scenario = new LearningScenario<Match>();
            scenario.MaxIterations = 1000;
            scenario.MaxError = 0.1;
            scenario.InputParams = new List<IParamDefinition<Match>>();
            scenario.InputParams.Add(MatchParams.GetLastFiveHomeMatches(matches));
            scenario.InputParams.Add(MatchParams.GetLastFiveAwayMatches(matches));
            scenario.OutputParams = new List<IParamDefinition<Match>>();
            scenario.OutputParams.Add(MatchParams.GetResultValue(matches));

            var network = Training.Train(scenario, matches);
            Prediction.Compute(network, scenario, matches);
        }

        private static void LoadAllMatches()
        {
            var matches = JsonDb.GetMatches();
            foreach (var gr in matches.GroupBy(m => m.HomeGoals == m.AwayGoals))
            {
                Console.WriteLine($"{gr.Key} {gr.Count()}");
            }
        }

        static void ImportAllSeasons()
        {
            List<Match> matches = new List<Match>();
            string[] seasonUrls = new[]
            {
                //2002/2003
                "http://www.90minut.pl/liga/0/liga188.html",
                //2003/2004
                "http://www.90minut.pl/liga/0/liga632.html",
                //2004/2005
                "http://www.90minut.pl/liga/0/liga1329.html",
                //2005/2006
                "http://www.90minut.pl/liga/0/liga1944.html",
                //2006/2007
                "http://www.90minut.pl/liga/0/liga2525.html",
                //2007/2008
                "http://www.90minut.pl/liga/0/liga3155.html",
                //2008/2009
                "http://www.90minut.pl/liga/0/liga3782.html",
                //2009/2010
                "http://www.90minut.pl/liga/0/liga4389.html",
                //2010/2011
                "http://www.90minut.pl/liga/0/liga4991.html",
                //2011/2012
                "http://www.90minut.pl/liga/0/liga5617.html",
                //2012/2013
                "http://www.90minut.pl/liga/0/liga6218.html",
                //2013/2014
                "http://www.90minut.pl/liga/0/liga6826.html",
                //2014/2015
                "http://www.90minut.pl/liga/0/liga7466.html",
                //2015/2016
                "http://www.90minut.pl/liga/0/liga8069.html",
                //2016/2017
                "http://www.90minut.pl/liga/0/liga8694.html"
            };
            foreach (string seasonUrl in seasonUrls)
            {
                Console.WriteLine(seasonUrl);
                matches.AddRange(HandleSeason(seasonUrl));
            }
            JsonDb.SaveMatches(matches);
        }

        static List<Match> HandleSeason(string seasonUrl)
        {
            var seasonDocument = HtmlDocumentCreator.Get(seasonUrl);
            var matchUrls = seasonDocument.DocumentNode
                .Descendants("a")
                .Where(a => a.GetAttributeValue("href", "").Contains("id_mecz"))
                .Select(a => a.GetAttributeValue("href", ""))
                .ToList()
                .Distinct();
            List<Match> matches = new List<Match>();
            foreach (string matchUrl in matchUrls)
            {
                matches.Add(HandleMatch(@"http://www.90minut.pl/" + matchUrl));
            }
            return matches;
        }

        private static Match HandleMatch(string matchUrl)
        {
            return new MatchBuilder(matchUrl).ParseFromUrl();
        }        
    }
}
