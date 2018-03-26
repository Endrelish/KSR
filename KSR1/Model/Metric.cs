namespace KSR1.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Metric : IMetric
    {
        protected void PrepareCharacteristics(List<double> first, List<double> second)
        {
            var difference = first.Count() - second.Count();
            Func<int, int, bool> condition = (i, diff) => i > diff;
            Func<int, int> step = i => i - 1;
            var collection = first;
            if (difference == 0)
            {
                return;
            }

            if (difference > 0)
            {
                condition = (i, diff) => i < diff;
                step = i => i + 1;
                collection = second;
            }

            for (var i = 0; condition(i, difference); i = step(i))
            {
                collection.Add(0);
            }
        }

        public abstract double GetMetric(IMetricObject first, IMetricObject second);
    }
}