namespace KSR1.Model
{
    public interface IDistance
    {
        double Distance { get; set; }

        void SetDistance(IMetric metric, IDistance second);
    }
}