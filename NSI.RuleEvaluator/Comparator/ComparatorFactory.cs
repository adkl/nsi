namespace NSI.RuleEvaluator.Comparator
{
    class ComparatorFactory
    {
        public IComparator Make(string op)
        {
            switch (op)
            {
                case "=":
                    return new EqualComparator();

                case ">":
                    return new GreaterComparator();

                case ">=":
                    return new GreaterOrEqualComparator();

                case "<":
                    return new LessComparator();

                case "<=":
                    return new LessOrEqualComparator();

                case "!=":
                    return new NotEqualComparator();
            }

            return null;
        }
    }
}
