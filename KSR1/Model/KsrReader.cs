namespace KSR1.Model
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    using HtmlAgilityPack;

    public static class KsrReader
    {
        public static IEnumerable<ReutersMetricObject> ReadReuters(string filepath)
        {
            var document = new HtmlDocument();
            document.Load(filepath);

            var places = new List<string> { "west-germany", "usa", "france", "uk", "canada", "japan" };

            var structure = document.DocumentNode.LastChild.Descendants("SET").Select(
                r => new
                         {
                             languages =
                             r.Descendants("LANGUAGES").Select(d => WebUtility.HtmlDecode(d.InnerText))
                                 .FirstOrDefault(),
                             text = r.Descendants("TEXT").Select(t => WebUtility.HtmlDecode(t.InnerText))
                                 .FirstOrDefault()
                         });

            var reuters = structure.Select(
                r => new ReutersMetricObject(r.languages, string.Empty, string.Empty, r.text));

            return reuters;
        }
    }
}