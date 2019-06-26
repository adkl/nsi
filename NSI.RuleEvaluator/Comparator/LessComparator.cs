namespace NSI.RuleEvaluator.Comparator
{
    class LessComparator : IComparator
    {
        public bool Compare(string value1, string value2)
        {
            return double.Parse(value1) < double.Parse(value2);
        }
    }
}
