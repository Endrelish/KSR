namespace KSR1.Model
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