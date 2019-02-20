using MarkdownTools.Parser.Implementation.Evaluators;
using MarkdownTools.Parser.Implementation.Evaluators.Base;
using NUnit.Framework;

namespace MarkdownTools.Parser.Tests.Implementation.Evaluators
{
    [TestFixture]
    public class CodeBlockIndentedEvaluatorTests
    {
        private BaseEvaluator _evaluator;

        [SetUp]
        public void SetUp()
        {
            _evaluator = new CodeBlockIndentedEvaluator();
        }
    }
}