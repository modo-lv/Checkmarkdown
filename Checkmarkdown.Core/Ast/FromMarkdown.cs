using Checkmarkdown.Core.Elements;
using Checkmarkdown.Core.Elements.Meta;
using Checkmarkdown.Core.Markdown;
using Checkmarkdown.Core.Project;
using Checkmarkdown.Core.Utils;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;

namespace Checkmarkdown.Core.Ast;

/// <summary>Handles conversion from Markdown to Checkmarkdown elements.</summary>
public static class FromMarkdown
{

    /// <summary>Parse a Markdown string into a Checkmarkdown AST.</summary>
    public static Document ToCheckmarkdown(String markdown, ProjectPath? file) {
        var document = MarkdownParser.ParseToAst(markdown);
        return ToCheckmarkdown(document).As<Document>().Also(doc => doc.SourceFile = file);
    }

    /// <summary>Convert a Markdown element to a Checkmarkdown one.</summary>
    public static Element ToCheckmarkdown(IMarkdownObject mdo) {
        Element result = mdo switch {
            MarkdownDocument _ => new Document(),
            EmphasisInline em => new Emphasis(em),
            HeadingBlock h => new Heading(h),
            LineBreakInline _ => new LineBreak(),
            LinkInline l => new Link(l),
            ListBlock l => new Listing(l),
            ListItemBlock li => new ListItem(li),
            LiteralInline t => new Text(t),
            ParagraphBlock p => new Paragraph(p),
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