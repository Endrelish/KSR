namespace KSR1.Model
{
    using System.Collections.Generic;

    public interface IMetric
    {
        double Resemblance(Dictionary<string, double> first, Dictionary<string, double> second);
        double Distance(Dictionary<string, double> first, Dictionary<string, double> second);
    }
}