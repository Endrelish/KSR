namespace KSR1
{
    public interface IDistance
    {
        double Distance { get; set; }

        void SetDistance(IMetric metric, IDistance second);
    }
}