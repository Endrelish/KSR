namespace KSR1.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using KSR1.Properties;

    public static class Knn
    {
        public static string Classify(ReutersMetricObject reuter, IEnumerable<ReutersMetricObject> trainingData, IMetric metric)
        {
            var distance = new SortedList<double, string>(new DuplicateComparer<double>());
            var categories = new Dictionary<string, int>();

            foreach (var o in trainingData)
            {
                distance.Add(metric.Distance(reuter.Vector, o.Vector), o.Property);
            }

            for (int i = 0; i < Settings.Default.K; i++)
            {
                categories.TryGetValue(distance.ElementAt(i).Value, out var value);
                categories[distance.ElementAt(i).Value] = value + 1;
            }

            return categories.First(c => c.Value == categories.Max(x => x.Value)).Key;
        }
    }
}