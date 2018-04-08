namespace KSR1.Model
{
    using System.Collections.Generic;

    public interface IExtractor
    {
        FeatureVector ExtractFeatures(IEnumerable<ReutersMetricObject> reuters);
    }
}