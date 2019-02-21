using MarkdownTools.Parser.Implementation.Evaluators;
using MarkdownTools.Parser.Implementation.Evaluators.Base;
using NUnit.Framework;
using System;

namespace MarkdownTools.Parser.Tests.Implementation.Evaluators
{
    [TestFixture]
    public class CodeBlockIndentedEvaluatorTests
    {
        private IEvaluator _evaluator;

        [SetUp]
        public void SetUp()
        {
            _evaluator = new CodeBlockIndentedEvaluator();
        }

        [TestCase("", false)]
        [TestCase("    ", false)]
        [TestCase("   Steve", false)]
        [TestCase("    Steve", true, "Steve\n")]
        [TestCase("    Steve\nText", true, "Steve\n")]
        [TestCase("    Steve\n    Text", true, "Steve\nText\n")]
        [TestCase("    Steve\n     Text", true, "Steve\n Text\n")]
        public void Identifies_code_blocks(string input, bool isCodeBlock, string expected = null)
        {
            input = input.Replace("\n", Environment.NewLine);

            if (isCodeBlock)
            {
                Assert.That(_evaluator.Evaluate(input).Node.Content, Is.EqualTo(expected?.Replace("\n", Environment.NewLine)));
            }
            else
            {
                Assert.Null(_evaluator.Evaluate(input));
            }
        }
    }
}