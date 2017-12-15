using Matches.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matches.Common
{
    public class JsonDb
    {
        public static void SaveMatches(List<Match> matches)
        {
            string json = JsonConvert.SerializeObject(matches, Formatting.Indented);
            File.WriteAllText(@"matches.json", json);
        }

        public static List<Match> GetMatches()
        {
            return JsonConvert
                .DeserializeObject<List<Match>>(
                    File.ReadAllText(@"matches.json"));
        }
    }
}
