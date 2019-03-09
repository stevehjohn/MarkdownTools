using MarkdownTools.Parser.Implementation.Evaluators;
using MarkdownTools.Parser.Implementation.Evaluators.Base;
using NUnit.Framework;

namespace MarkdownTools.Parser.Tests.Implementation.Evaluators
{
    [TestFixture]
    public class UnorderedListItemEvaluatorTests
    {
        private IEvaluator _evaluator;

        [SetUp]
        public void SetUp()
        {
            _evaluator = new UnorderedListItemEvaluator();
        }

        [TestCase("* An unordered list item", "An unordered list item")]
        [TestCase("- An unordered list item", "An unordered list item")]
        [TestCase("+ An unordered list item", "An unordered list item")]
        [TestCase(" + An unordered list item", "An unordered list item")]
        [TestCase("  + An unordered list item", "An unordered list item")]
        [TestCase("   + An unordered list item", "An unordered list item")]
        [TestCase("    + Not an unordered list item", null)]
        public void Identifies_unordered_list_items(string input, string expected)
        {
            var result = _evaluator.Evaluate(input);

            if (expected == null)
            {
                Assert.Null(result);
            }
            else
            {
                Assert.That(result.Node.Content, Is.EqualTo(expected));
            }
        }
    }
}