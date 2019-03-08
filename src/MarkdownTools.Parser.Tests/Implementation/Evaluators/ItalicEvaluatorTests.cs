using MarkdownTools.Parser.Implementation.Evaluators;
using MarkdownTools.Parser.Implementation.Evaluators.Base;
using NUnit.Framework;

namespace MarkdownTools.Parser.Tests.Implementation.Evaluators
{
    [TestFixture]
    public class ItalicEvaluatorTests
    {
        private IEvaluator _evaluator;

        [SetUp]
        public void SetUp()
        {
            _evaluator = new ItalicEvaluator();
        }

        [TestCase("*this is italic* text", "this is italic")]
        [TestCase("*this isn't", null)]
        [TestCase("*", null)]
        [TestCase("_this is italic_ text", "this is italic")]
        [TestCase("_this isn't", null)]
        [TestCase("_", null)]
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