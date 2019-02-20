using System.Collections.Generic;
using MarkdownTools.Parser.Implementation.Evaluators.Interface;
using MarkdownTools.Parser.Models;

namespace MarkdownTools.Parser.Implementation.Evaluators
{
    public abstract class BaseEvaluator
    {
        public void Parse(Node parent, string markdown, IList<IEvaluator> evaluators)
        {
        }
    }
}