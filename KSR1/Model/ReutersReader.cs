namespace KSR1.Model
{
    using System.Collections.Generic;
    using System.Linq;

    using HtmlAgilityPack;

    public static class ReutersReader
    {
        public static IEnumerable<ReutersMetricObject> ReadReuters(string filepath)
        {
            var document = new HtmlDocument();
            document.Load(filepath);

            var places = new List<string> { "west-germany", "usa", "france", "uk", "canada", "japan" };

            var ret = document.DocumentNode.Descendants("REUTERS")
                .Where(r => r.Descendants("PLACES").First().Descendants("D").Any(d => places.Contains(d.InnerText)) && r.Descendants("PLACES").First().Descendants("D").Count() == 1)
                .Select(
                    r => new ReutersMetricObject(
                        r.Descendants("PLACES").First().Descendants("D").Select(d => System.Net.WebUtility.HtmlDecode(d.InnerText)).First(),
                        System.Net.WebUtility.HtmlDecode(r.Descendants("TEXT").First().Descendants("TITLE").First().InnerText),
                        System.Net.WebUtility.HtmlDecode(r.Descendants("TEXT").First().Descendants("DATELINE").First().InnerText),
                        System.Net.WebUtility.HtmlDecode(r.Descendants("TEXT").First().Descendants("BODY").First().InnerText)));

            return ret;
        }
    }
}