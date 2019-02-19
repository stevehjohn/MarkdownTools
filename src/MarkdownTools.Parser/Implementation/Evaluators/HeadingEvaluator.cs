namespace MarkdownTools.Parser.Implementation.Evaluators
{
    public class HeadingEvaluator : IEvaluator
    {
        public EvaluatorResult Evaluate(string source)
        {
            if (source.StartsWith("#"))
            {
                var level = 1;

                while (source[level] == '#' && level < 7)
                {
                    level++;
                }

                if (level < 7)
                {
                    
                }
            }

            return null;
        }

        public bool IsBlockParser => true;
    }
}