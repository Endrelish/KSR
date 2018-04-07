namespace KSR1.Model
{
    public interface IMetric
    {
        double Resemblance(string first, string second);
        double Distance(string first, string second);
    }
}