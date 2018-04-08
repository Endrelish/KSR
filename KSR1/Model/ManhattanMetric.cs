namespace KSR1.Model
{
    using System;
    using System.Collections.Generic;

    public class ManhattanMetric : IMetric
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
                distance += Math.Abs(score - d.Value);
            }

            return distance;
        }
    }
}