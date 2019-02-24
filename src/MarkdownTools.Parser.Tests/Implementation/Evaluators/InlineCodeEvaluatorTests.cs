using MarkdownTools.Parser.Implementation.Evaluators;
using MarkdownTools.Parser.Implementation.Evaluators.Base;
using NUnit.Framework;

namespace MarkdownTools.Parser.Tests.Implementation.Evaluators
{
    [TestFixture]
    public class InlineCodeEvaluatorTests
    {
        private IEvaluator _evaluator;

        [SetUp]
        public void SetUp()
        {
            _evaluator = new InlineCodeEvaluator();
        }

        [TestCase("", null)]
        [TestCase("`inline code`", "inline code")]
        [TestCase("`", null)]
        [TestCase("`Hello", null)]
        [TestCase("``Hello", null)]
        public void Identifies_inline_code(string input, string expected)
        {
            if (expected == null)
            {
                Assert.Null(_evaluator.Evaluate(input));
            }
            else
            {
                Assert.That(_evaluator.Evaluate(input).Node.Content, Is.EqualTo(expected));
            }
        }
    }
}