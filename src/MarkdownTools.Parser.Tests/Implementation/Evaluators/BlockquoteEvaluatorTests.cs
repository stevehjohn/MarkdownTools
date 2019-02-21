using MarkdownTools.Parser.Implementation.Evaluators;
using MarkdownTools.Parser.Implementation.Evaluators.Base;
using NUnit.Framework;
using System;

namespace MarkdownTools.Parser.Tests.Implementation.Evaluators
{
    [TestFixture]
    public class BlockquoteEvaluatorTests
    {
        private IEvaluator _evaluator;

        [SetUp]
        public void SetUp()
        {
            _evaluator = new BlockquoteEvaluator();
        }

        [TestCase("> ", false)]
        [TestCase("> Yo!", true, "Yo!")]
        [TestCase(" > Yo!\n>  Multiline\n  >Quote", true, "Yo! Multiline Quote")]
        public void Identifies_blockquotes(string input, bool isBlockquote, string expected = null)
        {
            input = input.Replace("\n", Environment.NewLine);

            if (isBlockquote)
            {
                Assert.That(_evaluator.Evaluate(input).Node.Content, Is.EqualTo(expected));
            }
            else
            {
                Assert.Null(_evaluator.Evaluate(input));
            }
        }
    }
}