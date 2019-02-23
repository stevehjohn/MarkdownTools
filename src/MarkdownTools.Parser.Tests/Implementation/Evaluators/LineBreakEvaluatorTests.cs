using MarkdownTools.Parser.Implementation.Evaluators;
using MarkdownTools.Parser.Implementation.Evaluators.Base;
using NUnit.Framework;

namespace MarkdownTools.Parser.Tests.Implementation.Evaluators
{
    [TestFixture]
    public class LineBreakEvaluatorTests
    {
        private IEvaluator _evaluator;

        [SetUp]
        public void SetUp()
        {
            _evaluator = new LineBreakEvaluator();
        }

        [TestCase("", false)]
        [TestCase("Some Text", false)]
        [TestCase("<br>", true)]
        [TestCase("<br >", true)]
        [TestCase("<br/>", true)]
        [TestCase("<br />", true)]
        public void Identifies_line_breaks(string input, bool isLineBreak)
        {
            if (isLineBreak)
            {
                Assert.NotNull(_evaluator.Evaluate(input));
            }
            else
            {
                Assert.Null(_evaluator.Evaluate(input));
            }
        }
    }
}