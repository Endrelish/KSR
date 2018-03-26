namespace KSR1.Model
{
    using System.Collections.Generic;

    public interface IMetricObject
    {
        double Resemblance(object other);
    }
}