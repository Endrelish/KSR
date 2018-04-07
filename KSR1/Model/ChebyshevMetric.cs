namespace KSR1.Model
{
    using System;

    public class ChebyshevMetric : IMetric
    {
        public double Distance(string first, string second)
        {
            ulong distance = 0;
            if (first.Length < second.Length)
            {
                this.Swap(ref first, ref second);
            }

            int i;

            for (i = 0; i < second.Length; i++)
            {
                var d = (ulong)Math.Abs(first[i] - second[i]);
                if (d > distance)
                {
                    distance = d;
                }
            }

            for (i--; i < first.Length; i++)
            {
                var d = (ulong)Math.Abs(first[i] - 0x20);
                if (d > distance)
                {
                    distance = d;
                }
            }

            return distance;
        }

        public double Resemblance(string first, string second)
        {
            return 1.0d / this.Distance(first, second);
        }

        private void Swap<T>(ref T first, ref T second)
        {
            var a = first;
            first = second;
            second = a;
        }
    }
}