using MarkdownTools.Parser.Implementation.Evaluators;
using MarkdownTools.Parser.Implementation.Evaluators.Base;
using NUnit.Framework;

namespace MarkdownTools.Parser.Tests.Implementation.Evaluators
{
    [TestFixture]
    public class ImageEvaluatorTests
    {
        private IEvaluator _evaluator;

        [SetUp]
        public void SetUp()
        {
            _evaluator = new ImageEvaluator();
        }

        [TestCase("![An Image](image.png)", "An Image", "image.png")]
        [TestCase("![Not an image", null, null)]
        [TestCase("![", null, null)]
        [TestCase("![Not an image either]", null, null)]
        [TestCase("![Nor is this](", null, null)]
        [TestCase("![Or this](http://www", null, null)]
        public void Identifies_links(string input, string altText, string link)
        {
            var result = _evaluator.Evaluate(input);

            if (altText != null)
            {
                Assert.That(result.Node.MetaData["AltText"], Is.EqualTo(altText));
                Assert.That(result.Node.MetaData["Source"], Is.EqualTo(link));
            }
            else
            {
                Assert.Null(result);
            }
        }
    }
}