using MarkdownTools.Parser.Models;
using System.Collections.Generic;

namespace MarkdownTools.Parser.Implementation.Evaluators.Base
{
    public abstract class BaseEvaluator
    {
        public void Parse(Node parent, string markdown, IList<BaseEvaluator> evaluators)
        {
        }

        public abstract EvaluatorResult Evaluate(string source);
    }
}