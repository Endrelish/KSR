﻿namespace KSR1.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class TfExtractor : IExtractor
    {

        public void FeatureVector(IEnumerable<ReutersMetricObject> reuters)
        {
            foreach (var reuter in reuters)
            {
                var counted = this.CountWords(reuter);
                reuter.Vector = new Dictionary<string, double>();
                int max = counted.Values.Max();

                foreach (var c in counted)
                {
                    reuter.Vector.Add(c.Key, (double)c.Value / max);
                }
            }
        }

        private Dictionary<string, int> CountWords(ReutersMetricObject reuter)
        {
            var words = new Dictionary<string, int>();
            reuter.SeparateBody();
            foreach (var s in reuter.SeparatedBody)
            {
                WordFound(words, s);
            }

            return words;
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