using MarkdownTools.Parser.Implementation.Evaluators;
using MarkdownTools.Parser.Implementation.Evaluators.Interface;
using NUnit.Framework;

namespace MarkdownTools.Parser.Tests.Implementation.Evaluators
{

    [TestFixture]
    public class WhitespaceEvaluatorTests
    {
        private IEvaluator _evaluator;

        [SetUp]
        public void SetUp()
        {
            _evaluator = new WhitespaceEvaluator();
        }

        [TestCase("", 0)]
        [TestCase(" ", 1)]
        [TestCase("    ", 4)]
        [TestCase("Not whitespace    ", 0)]
        [TestCase("\t", 1)]
        [TestCase("\r", 0)]
        [TestCase("\n", 0)]
        [TestCase("\r\n", 0)]
        public void Identifies_runs_of_whitespace(string input, int length)
        {
            if (length == 0)
            {
                Assert.Null(_evaluator.Evaluate(input));
            }
            else
            {
                Assert.That(_evaluator.Evaluate(input).Node.MetaData["Length"], Is.EqualTo($"{length}"));
            }
        }
    }
}