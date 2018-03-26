namespace KSR1.Model
{
    public interface IMetric
    {
        double GetMetric(IMetricObject first, IMetricObject second);
    }
}