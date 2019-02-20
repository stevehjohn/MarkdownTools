using MarkdownTools.Parser.Implementation.Evaluators;
using MarkdownTools.Parser.Implementation.Evaluators.Interface;
using NUnit.Framework;
using System;

namespace MarkdownTools.Parser.Tests.Implementation.Evaluators
{
    [TestFixture]
    public class NewlineEvaluatorTests
    {
        private IEvaluator _evaluator;

        [SetUp]
        public void SetUp()
        {
            _evaluator = new NewlineEvaluator();
        }

        [TestCase(" ")]
        [TestCase("")]
        [TestCase("Some text\r")]
        [TestCase("Some text\n")]
        [TestCase("Some text\r\n")]
        [TestCase("Some text\n\r")]
        [TestCase("\n")]
        [TestCase("\r")]
        [TestCase("\r\n")]
        [TestCase("\n\r")]
        public void Identifies_new_lines(string input)
        {
            if (input.Equals(Environment.NewLine, StringComparison.Ordinal))
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