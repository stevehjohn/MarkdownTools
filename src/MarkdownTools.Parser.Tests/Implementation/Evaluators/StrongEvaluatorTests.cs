using MarkdownTools.Parser.Implementation.Evaluators;
using MarkdownTools.Parser.Implementation.Evaluators.Base;
using NUnit.Framework;

namespace MarkdownTools.Parser.Tests.Implementation.Evaluators
{
    [TestFixture]
    public class StrongEvaluatorTests
    {
        private IEvaluator _evaluator;

        [SetUp]
        public void SetUp()
        {
            _evaluator = new StrongEvaluator();
        }

        [TestCase("**this is strong** text", "this is strong")]
        [TestCase("**this isn't", null)]
        [TestCase("**", null)]
        [TestCase("__this is strong__ text", "this is strong")]
        [TestCase("__this isn't", null)]
        [TestCase("__", null)]
        public void Identifies_italics(string input, string expected)
        {
            var result = _evaluator.Evaluate(input);

            if (expected != null)
            {
                Assert.That(result.Node.Content, Is.EqualTo(expected));
            }
            else
            {
                Assert.Null(result);
            }
        }
    }
}