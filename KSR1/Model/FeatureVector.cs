namespace KSR1.Model
{
    using System.Collections.Generic;

    public class FeatureVector
    {
        public FeatureVector()
        {
            this.Vector = new Dictionary<string, double>();
        }

        public FeatureVector(Dictionary<string, double> vec)
        {
            this.Vector = vec;
        }

        public Dictionary<string, double> Vector { get; set; }
    }
}