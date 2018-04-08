namespace KSR1.Model
{
    using System;
    using System.Collections.Generic;

    public class ChebyshevMetric : IMetric
    {
        public double Distance(Dictionary<string, double> first, Dictionary<string, double> second)
        {
            double distance = 0.0d;

            foreach (var d in first)
            {
                second.TryGetValue(d.Key, out var score);
                distance = Math.Max(Math.Abs(score - d.Value), distance);
            }

            return distance;
        }

        public double Resemblance(Dictionary<string, double> first, Dictionary<string, double> second)
        {
            return 1.0d / this.Distance(first, second);
        }
        
    }
}