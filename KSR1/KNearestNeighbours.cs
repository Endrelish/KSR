namespace KSR1
{
    public class KNearestNeighbours
    {
        private int k;

        public int K
        {
            get
            {
                return this.k;
            }
            set
            {
                this.k = value;
            }
        }

        private IMetric metric;


    }
}