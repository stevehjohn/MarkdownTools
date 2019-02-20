using MarkdownTools.Parser.Extensions;

namespace MarkdownTools.Parser.Implementation.Evaluators
{
    public class HeadingEvaluator : IEvaluator
    {
        public EvaluatorResult Evaluate(string source)
        {
            if (source.SafeGetChar(0) == '#')
            {
                var level = 1;

                while (source.SafeGetChar(level) == '#' && level < 7)
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