using MarkdownTools.Parser.Implementation.Evaluators;
using MarkdownTools.Parser.Implementation.Evaluators.Base;
using NUnit.Framework;

namespace MarkdownTools.Parser.Tests.Implementation.Evaluators
{
    [TestFixture]
    public class HeadingEvaluatorTests
    {
        private IEvaluator _evaluator;

        [SetUp]
        public void SetUp()
        {
            _evaluator = new HeadingEvaluator();
        }

        [TestCase("## This is a heading ##", "This is a heading")]
        [TestCase("## This is a heading ##\r\nNext Line", "This is a heading")]
        public void Removes_end_hashes(string input, string expected)
        {
            Assert.That(_evaluator.Evaluate(input).Node.Content, Is.EqualTo(expected));
        }

        [TestCase("", 0)]
        [TestCase("# H1", 1)]
        [TestCase("## H2", 2)]
        [TestCase("### H3", 3)]
        [TestCase("#### H4", 4)]
        [TestCase("##### H5", 5)]
        [TestCase("###### H6", 6)]
        [TestCase("####### H7", 0)]
        [TestCase("No heading in sight", 0)]
        public void Identifies_headings(string input, int level)
        {
            if (level == 0)
            {
                Assert.Null(_evaluator.Evaluate(input));
            }
            else
            {
                Assert.That(_evaluator.Evaluate(input).Node.MetaData["Level"], Is.EqualTo($"{level}"));
            }
        }
    }
}