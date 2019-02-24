using MarkdownTools.Parser.Implementation.Evaluators;
using MarkdownTools.Parser.Implementation.Evaluators.Base;
using NUnit.Framework;

namespace MarkdownTools.Parser.Tests.Implementation.Evaluators
{
    [TestFixture]
    public class StrikethroughEvaluatorTests
    {
        private IEvaluator _evaluator;

        [SetUp]
        public void SetUp()
        {
            _evaluator = new StrikethroughEvaluator();
        }

        [TestCase("", null)]
        [TestCase("~~", null)]
        [TestCase("Test", null)]
        [TestCase("~Test", null)]
        [TestCase("~~Test", null)]
        [TestCase("~~Test~~", "Test")]
        public void Identifies_strikethrough(string input, string expected)
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