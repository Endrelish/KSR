namespace KSR1
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class ManhattanMetric : Metric
    {
        public override double GetMetric(IMetricObject first, IMetricObject second)
        {
            var firstCharacteristics = first.Characteristics;
            var secondCharacteristics = second.Characteristics;

            this.PrepareCharacteristics(firstCharacteristics, secondCharacteristics);
            
            double metric = 0;
            int count = firstCharacteristics.Count;
            for (int i = 0; i < count; i++)
            {
                metric += Math.Abs(firstCharacteristics[i] - secondCharacteristics[i]);
            }

            return metric;
        }
    }
}