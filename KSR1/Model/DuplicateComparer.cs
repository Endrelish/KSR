namespace KSR1.Model
{
    using System;
    using System.Collections.Generic;

    public class DuplicateComparer<TKey> : IComparer<TKey>
        where TKey : IComparable
    {
        public int Compare(TKey x, TKey y)
        {
            var result = x.CompareTo(y);

            if (result == 0)
            {
                return 1; // Handle equality as beeing greater
            }

            return result;
        }
    }
}