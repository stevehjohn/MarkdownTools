using MarkdownTools.Models;

namespace MarkdownTools.TreeToHtml.Implementation
{
    public interface INodeTreeWalker
    {
        void LoadTree(Node root);

        string ToHtml(Theme theme, string customThemePath = null);
    }
}