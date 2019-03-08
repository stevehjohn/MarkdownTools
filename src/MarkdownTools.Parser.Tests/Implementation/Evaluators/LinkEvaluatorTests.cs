using MarkdownTools.Parser.Implementation.Evaluators;
using MarkdownTools.Parser.Implementation.Evaluators.Base;
using NUnit.Framework;

namespace MarkdownTools.Parser.Tests.Implementation.Evaluators
{
    [TestFixture]
    public class LinkEvaluatorTests
    {
        private IEvaluator _evaluator;

        [SetUp]
        public void SetUp()
        {
            _evaluator = new LinkEvaluator();
        }

        [TestCase("[A Link to Google](https://www.google.com)", "A Link to Google", "https://www.google.com")]
        [TestCase("[Not a link", null, null)]
        [TestCase("[", null, null)]
        [TestCase("[Not a link either]", null, null)]
        [TestCase("[Nor is this](", null, null)]
        [TestCase("[Or this](http://www", null, null)]
        public void Identifies_links(string input, string text, string link)
        {
            var result = _evaluator.Evaluate(input);

            if (text != null)
            {
                Assert.That(result.Node.Content, Is.EqualTo(text));
                Assert.That(result.Node.MetaData["Link"], Is.EqualTo(link));
            }
            else
            {
                Assert.Null(result);
            }
        }
    }
}