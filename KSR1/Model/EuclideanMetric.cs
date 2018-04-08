namespace KSR1.Model
{
    using System;
    using System.Collections.Generic;

    internal class EuclideanMetric : IMetric
    {
        public double Resemblance(Dictionary<string, double> first, Dictionary<string, double> second)
        {
            return 1.0d / Distance(first, second);
        }

        public double Distance(Dictionary<string, double> first, Dictionary<string, double> second)
        {
            double distance = 0.0d;

            foreach (var d in first)
            {
                second.TryGetValue(d.Key, out var score);
                distance += (score - d.Value) * (score - d.Value);
            }
            
            return Math.Sqrt(distance);
        }
    }
}