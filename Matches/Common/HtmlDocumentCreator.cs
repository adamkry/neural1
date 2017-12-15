using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Matches.Common
{
    public class HtmlDocumentCreator
    {
        public static HtmlDocument Get(string url)
        {
            var result = new HtmlDocument();
            Thread.Sleep(500);
            string htmlText = new WebClient().DownloadString(url);
            result.LoadHtml(htmlText);
            return result;
        }
    }
}
