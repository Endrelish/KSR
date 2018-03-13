namespace KSR1
{
    using System.Collections;

    public interface IMetric
    {
        double GetMetric(IMetricObject first, IMetricObject second);
    }
}