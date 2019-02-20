using MarkdownTools.Parser.Implementation.Evaluators;
using MarkdownTools.Parser.Implementation.Evaluators.Interface;
using NUnit.Framework;
using System;

namespace MarkdownTools.Parser.Tests.Implementation.Evaluators
{
    [TestFixture]
    public class HorizontalRuleEvaluatorTests
    {
        private IEvaluator _evaluator;

        [SetUp]
        public void SetUp()
        {
            _evaluator = new HorizontalRuleEvaluator();
        }

        [TestCase("---", true)]
        [TestCase("---          ", true)]
        [TestCase("---      \nSome text", true)]
        [TestCase("--- Test", false)]
        [TestCase(" ---", false)]
        [TestCase("-------", true)]
        [TestCase("***", true)]
        [TestCase("****", true)]
        [TestCase("___", true)]
        [TestCase("____", true)]
        public void Identifies_horizontal_rules(string input, bool isRule)
        {
            input = input.Replace("\n", Environment.NewLine);

            if (isRule)
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