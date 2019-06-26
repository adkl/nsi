using System;

namespace NSI.RuleEvaluator.Comparator
{
    class NotEqualComparator : IComparator
    {
        private const double Epsilon = 1e-10;

        public bool Compare(string value1, string value2)
        {
            return Math.Abs(double.Parse(value1) - double.Parse(value2)) <= Epsilon;
        }
    }
}
