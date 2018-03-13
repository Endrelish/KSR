namespace KSR1
{
    using System.Collections.Generic;

    public interface IMetricObject
    {
        List<double> Characteristics { get; }

        double Resemblance { get; }

        void SetResemblance(object other);
    }
}