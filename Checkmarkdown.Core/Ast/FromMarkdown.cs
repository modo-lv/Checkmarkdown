using Checkmarkdown.Core.Elements;
using Checkmarkdown.Core.Elements.Meta;
using Checkmarkdown.Core.Markdown;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;

namespace Checkmarkdown.Core.Ast;

/// <summary>Handles conversion from Markdown to Checkmarkdown elements.</summary>
public static class FromMarkdown {

    /// <summary>Parse a Markdown string into a Checkmarkdown AST.</summary>
    public static Document ToCheckmarkdown(String markdown) {
        var document = MarkdownParser.ParseToAst(markdown);
        return (Document) ToCheckmarkdown(document);
    }

    /// <summary>Convert a Markdown element to a Checkmarkdown one.</summary>
    public static Element ToCheckmarkdown(IMarkdownObject mdo) {
        Element result = mdo switch {
            MarkdownDocument _ => new Document(),
            EmphasisInline em => new Emphasis(em),
            ListItemBlock li => new ListItem(li),
            _ => new Unrecognized(mdo),
        };

        var children = mdo switch {
            LeafBlock node => node.Inline,
            IEnumerable<MarkdownObject> node => node,
            _ => null
        } ?? [];

        result.Children = children.Select(ToCheckmarkdown).ToList();

        return result;
    }
}