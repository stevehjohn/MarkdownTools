using MarkdownTools.Parser.Implementation.Evaluators;
using MarkdownTools.Parser.Implementation.Evaluators.Base;
using NUnit.Framework;
using System;

namespace MarkdownTools.Parser.Tests.Implementation.Evaluators
{
    [TestFixture]
    public class CodeBlockEvaluatorTests
    {
        private IEvaluator _evaluator;

        [SetUp]
        public void SetUp()
        {
            _evaluator = new CodeBlockEvaluator();
        }

        [TestCase("", false)]
        [TestCase("    ", false)]
        [TestCase("   Steve", false)]
        [TestCase("    Steve", true, "Steve\n")]
        [TestCase("    Steve\nText", true, "Steve\n")]
        [TestCase("    Steve\n    Text", true, "Steve\nText\n")]
        [TestCase("    Steve\n     Text", true, "Steve\n Text\n")]
        public void Identifies_indented_code_blocks(string input, bool isCodeBlock, string expected = null)
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

        [TestCase("", false)]
        [TestCase(" ", false)]
        [TestCase("Not a code block ", false)]
        [TestCase(" ``` Not yet a code block", false)]
        [TestCase("```JavaScript ", true, null, "JavaScript", null)]
        [TestCase("```JavaScript \nSome Code", true, null, "JavaScript", "Some Code")]
        [TestCase("````JavaScript \nSome Code", true, null, "JavaScript", "Some Code")]
        [TestCase("```JavaScript \nSome Code```", true, null, "JavaScript", "Some Code")]
        [TestCase("```JavaScript \nSome Code```Continues", true, "Continues", "JavaScript", "Some Code")]
        [TestCase("````JavaScript \nSome Code````Continues", true, "Continues", "JavaScript", "Some Code")]
        [TestCase("```JavaScript \nSome \nCode```Continues", true, "Continues", "JavaScript", "Some \nCode")]
        public void Identifies_tick_mark_code_blocks(string input, bool isCodeBlock, string expectedNext = null, string expectedLanguage = null, string expectedContent = null)
        {
            if (!isCodeBlock)
            {
                Assert.Null(_evaluator.Evaluate(input));
            }
            else
            {
                input = input.Replace("\n", Environment.NewLine);
                expectedContent = expectedContent?.Replace("\n", Environment.NewLine);

                var result = _evaluator.Evaluate(input);

                Assert.That(result.EvaluateNext, Is.EqualTo(expectedNext));
                Assert.That(result.Node.MetaData["Language"], Is.EqualTo(expectedLanguage));
                Assert.That(result.Node.Content, Is.EqualTo(expectedContent));
            }
        }
    }
}