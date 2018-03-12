namespace KSR1
{
    public interface IMetric
    {
        double GetMetric(IMetricObject first, IMetricObject second);
    }
}