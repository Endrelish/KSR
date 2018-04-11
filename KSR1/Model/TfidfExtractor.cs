namespace KSR1.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using KSR1.Properties;

    public class TfidfExtractor : IExtractor
    {
        public void FeatureVector(IEnumerable<ReutersMetricObject> reuters, Progress progress)
        {
            var wordsIdf = new Dictionary<string, double>();
            var tfExtractor = new TfExtractor();

            tfExtractor.FeatureVector(reuters, new Progress(0));
            foreach (var reuter in reuters)
            {
                var cp = reuter.Vector.Select(v => v.Key).ToList();
                foreach (var d in cp)
                {
                    reuter.Vector[d] *= Idf(d, reuters, wordsIdf);
                }
                progress.Processed++;
            }
        }

        private double Idf(string word, IEnumerable<ReutersMetricObject> reuters, Dictionary<string, double> idf)
        {
            if (idf.TryGetValue(word, out var value)) return value;
            var count = 0;
            foreach (var reuter in reuters)
            {
                if (reuter.SeparatedBody.Contains(word))
                {
                    count++;
                }
            }
            return Math.Log10((double)reuters.Count() / (1 + count));
        }
    }
}