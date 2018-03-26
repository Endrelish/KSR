namespace KSR1.Model
{
    using System;

    public class Data : IComparable<Data>, IDistance
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public string Category { get; private set; }
        public string KnnCategory { get; set; }
        public double Weight { get; set; }

        public Data(double x, double y, string category)
        {
            this.X = x;
            this.Y = y;
            this.Category = category;
        }

        public int CompareTo(Data compareData)
        {
            // A null value means that this object is greater.
            if (compareData == null)
                return 1;

            else
                return this.Weight.CompareTo(compareData.Weight);
        }

        public double Distance { get; set; }

        public void SetDistance(IMetric metric, IDistance second)
        {
            throw new NotImplementedException();
        }
    }
}
