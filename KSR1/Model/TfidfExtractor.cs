namespace KSR1.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TfidfExtractor : IExtractor
    {
        public void FeatureVector(IEnumerable<ReutersMetricObject> reuters)
        {
            var wordsIdf = new Dictionary<string, double>();
            var tfExtractor = new TfExtractor();

            tfExtractor.FeatureVector(reuters);
            foreach (var reuter in reuters)
            {
                foreach (var d in reuter.Vector)
                {
                    reuter.Vector[d.Key] = d.Value * Idf(d.Key, reuters, wordsIdf);
                }
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