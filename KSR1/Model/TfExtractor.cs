namespace KSR1.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class TfExtractor : IExtractor
    {
        public TfExtractor()
        {
            this.FeatureVector = new Dictionary<string, List<double>>();
        }

        private Dictionary<string, List<double>> FeatureVector { get; }

        public FeatureVector ExtractFeatures(IEnumerable<ReutersMetricObject> reuters)
        {
            foreach (var reuter in reuters)
            {
                this.ExtractText(reuter.Body);
            }
            
            return new FeatureVector(this.FeatureVector.ToDictionary(f => f.Key, f => f.Value.Average()));
        }

        private void AddFeature(string key, double value)
        {
            if (this.FeatureVector.TryGetValue(key, out var list))
            {
                list.Add(value);
            }
            else
            {
                var vector = new List<double> { value };
                this.FeatureVector.Add(key, vector);
            }
        }

        private void ExtractText(string text)
        {
            var words = new Dictionary<string, int>();
            char[] separators = { ' ', '\n', '\t' };
            var separated = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            separated = separated.Where(s => !s.Contains("REUTER")).ToArray();
            foreach (var s in separated)
            {
                this.WordFound(words, s);
            }

            foreach (var word in words)
            {
                this.AddFeature(word.Key, (double)word.Value / separated.Length);
            }
        }

        private void WordFound(Dictionary<string, int> words, string word)
        {
            if (words.ContainsKey(word))
            {
                words[word]++;
            }
            else
            {
                words.Add(word, 1);
            }
        }
    }
}