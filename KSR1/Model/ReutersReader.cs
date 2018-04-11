namespace KSR1.Model
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    using HtmlAgilityPack;

    public static class ReutersReader
    {
        public static IEnumerable<ReutersMetricObject> ReadReuters(string filepath)
        {
            var document = new HtmlDocument();
            document.Load(filepath);

            var places = new List<string> { "west-germany", "usa", "france", "uk", "canada", "japan" };

            var structure = document.DocumentNode.Descendants("REUTERS")
                .Where(r => r.Descendants("PLACES").Select(p => p.Descendants("D").Count()).FirstOrDefault() == 1)
                .Select(
                    r => new
                             {
                                 places =
                                 r.Descendants("PLACES")
                                     .Select(p => p.Descendants("D").Select(d => WebUtility.HtmlDecode(d.InnerText)))
                                     .FirstOrDefault().FirstOrDefault(),
                                 title =
                                 r.Descendants("TEXT")
                                     .Select(
                                         t => t.Descendants("TITLE").Select(tt => WebUtility.HtmlDecode(tt.InnerText)))
                                     .FirstOrDefault().FirstOrDefault(),
                                 dateline =
                                 r.Descendants("TEXT")
                                     .Select(
                                         t => t.Descendants("DATELINE")
                                             .Select(tt => WebUtility.HtmlDecode(tt.InnerText))).FirstOrDefault()
                                     .FirstOrDefault(),
                                 body = r.Descendants("TEXT")
                                     .Select(
                                         t => t.Descendants("BODY").Select(tt => WebUtility.HtmlDecode(tt.InnerText)))
                                     .FirstOrDefault().FirstOrDefault(),
                                 count = r.Descendants("PLACES").Select(p => p.Descendants("D").Count())
                                     .FirstOrDefault()
                             }).Where(r => places.Contains(r.places));

            var reuters = structure
                .Where(
                    a => a.places != null && a.title != null && a.dateline != null && a.body != null
                         && places.Contains(a.places)).Select(
                    a => new ReutersMetricObject(a.places, a.title, a.dateline, a.body));

            return reuters;
        }
    }
}