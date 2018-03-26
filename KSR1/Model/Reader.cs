namespace KSR1.Model
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using HtmlAgilityPack;

    public static class Reader
    {
        public static IEnumerable<ReutersMetricObject> ReadData(string filepath)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.Load(filepath);

            var places = new List<string>() { "west-germany", "usa", "france", "uk", "canada", "japan" };

            var reuters = htmlDocument.DocumentNode.Descendants("REUTERS")
                .Where(r => r.Descendants("PLACES").Select(p => p.Descendants("D")).Count() == 1); // && places.Contains(r.Descendants("PLACES").First().Descendants("D").First().InnerText)

            var reutersMetricObjects = reuters.Select(
                p => new ReutersMetricObject(
                    p.Descendants("PLACE").First().Descendants("D").First().InnerText,
                    p.Descendants("TEXT").First().Descendants("TITLE").First().Descendants("D").First().InnerText,
                    p.Descendants("TEXT").First().Descendants("DATELINE").First().InnerText,
                    p.Descendants("TEXT").First().Descendants("BODY").First().InnerText));

            return reutersMetricObjects;
        }

        public static IEnumerable<string> ReadDataString(string filepath)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.Load(filepath);

            var places = new List<string>() { "west-germany", "usa", "france", "uk", "canada", "japan" };

            var reuters = htmlDocument.DocumentNode.Descendants("REUTERS")
                .Where(r => r.Descendants("PLACES").Select(p => p.Descendants("D")).Count() == 1); // && places.Contains(r.Descendants("PLACES").First().Descendants("D").First().InnerText)

            return reuters.Select(r => r.InnerText);
        }
    }
}