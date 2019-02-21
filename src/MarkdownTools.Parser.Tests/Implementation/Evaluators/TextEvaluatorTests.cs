using MarkdownTools.Parser.Implementation.Evaluators;
using MarkdownTools.Parser.Implementation.Evaluators.Base;
using NUnit.Framework;

namespace MarkdownTools.Parser.Tests.Implementation.Evaluators
{
    [TestFixture]
    public class TextEvaluatorTests
    {
        private IEvaluator _evaluator;

        [SetUp]
        public void SetUp()
        {
            _evaluator = new TextEvaluator();
        }

        [Test]
        public void Evaluator_returns_character()
        {
            var result = _evaluator.Evaluate("Some text");

            Assert.That(result.Node.Content, Is.EqualTo("S"));
        }
    }
}