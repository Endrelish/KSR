namespace KSR1.Model
{
    using System.Collections.Generic;

    public interface IExtractor
    {
        void FeatureVector(IEnumerable<ReutersMetricObject> reuters);
    }
}